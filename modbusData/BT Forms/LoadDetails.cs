using Dapper;
using Microsoft.VisualBasic.FileIO;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.Classes.Model;
using Uniproject.Classes.RMC;

///This Class is use for fetch data from Excel File, Linhoff.mdf and Protool.mdb 
//using System.Windows;

//using Excel = Microsoft.Office.Interop.Excel;

namespace Uniproject.Masters
{
    public partial class LoadDetails : Form
    {
        string FileNameDateFormat;
        public static string enddate;
        public static string endtime, lastbatchno;
        public static DateTime comparetime1 = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
        public static int tipperinterval = Convert.ToInt32(clsFunctions.gettruckinterval("Select truckinterval from tipper_interval"));//Convert.ToInt32(clsFunctions.loadSingleValue("Select truckinterval from tipper_interval"));
        static Int32 RowPointer;

        public int L_LoadNo;      // 22/02/2024 - BhaveshT
        public int L_BatchNo;     // 23/02/2024 - BhaveshT

        clsErrorPercent_Calculation clsErr = new clsErrorPercent_Calculation();

        //clsSaveToDM clsDM = new clsSaveToDM();      // 15/01/2025 - BhaveshT

        public static string sqliteDbPath = System.IO.Path.Combine(Application.StartupPath, "Database\\UniproData.db");
        private BackgroundWorker backgroundWorker;  

        /************************* BackgroundWorker for Live Data uploading by Dinesh ******************/
        private BackgroundWorker bgWorkerForLiveDataUpload = new BackgroundWorker();
        private bool keepRunning = true; // Control flag
        private System.Windows.Forms.Timer dataFillTimer; //for testing
        private bool isBlinking = false;
        private System.Windows.Forms.Timer blinkTimer;
        /***********************************************************************************************/


 

        ConcurrentDictionary<string, List<double>> sensorData = new ConcurrentDictionary<string, List<double>>()
        {
            ["filler"] = new List<double>(),
            ["filler_1"] = new List<double>(),
            ["agg_kg"] = new List<double>(),
            ["asp_tank_1"] = new List<double>(),
            ["smoke_temp"] = new List<double>(),
            ["hot_bin1_temp"] = new List<double>(),
            ["total_weight"] = new List<double>(),
            ["asphalt"] = new List<double>(),
            ["FO_temp"] = new List<double>(),
            ["asp_pipe"] = new List<double>(),
            ["hot_bin_1"] = new List<double>(),
            ["hot_bin_2"] = new List<double>(),
            ["hot_bin_3"] = new List<double>(),
            ["hot_bin_4"] = new List<double>(),
            ["batch_counter"] = new List<double>()
        };



    //--------------------------------------------------------------------------------------------

    public LoadDetails()
        {
            InitializeComponent();
            InitializeRefreshTimer(); //This method is use for Disable the reload button for 10 seconds after clicking on button.
            Log.Logger = new LoggerConfiguration()
              .WriteTo.File(Path.Combine(Application.StartupPath, "log.txt"), rollingInterval: RollingInterval.Day)
              .CreateLogger();

            sqliteDbPath = Path.Combine(Application.StartupPath, "Database", "UniproData.db");

            if (!File.Exists(sqliteDbPath))
            {
                Log.Error("Database not found at: {Path}", sqliteDbPath);
                MessageBox.Show("Database file not found!");
                return;
            }
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = false;
            backgroundWorker.WorkerSupportsCancellation = true;



            if (tipperinterval == null || tipperinterval == 0)
            {
                tipperinterval = 1;
            }
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            this.Text = this.Text + "       " + frmEagleBatchMAster.heading;

            FileNameDateFormat = DateTime.Now.ToString("ddMMyyyy");

            // by dinesh
            // Configure BackgroundWorker
            bgWorkerForLiveDataUpload.WorkerSupportsCancellation = true;
           // bgWorkerForLiveDataUpload.DoWork += BgWorkerForLiveDataUpload_DoWork;
            InitializeBlinkTimer();

            //for tesitng only
            //InitializeTimer();

 
        }

        string currenttime = "";

        //--------------------------------------------------------------------------------------------

        private void LoadDetails_Load(object sender, EventArgs e)
        {
            try
            {
                loadcombodata();

                DateTime nowDate = DateTime.Now;
                time.Text = nowDate.ToString("HH:mm:ss");
                currenttime = nowDate.ToString("HH:mm:ss");
                txtdate.Text = nowDate.ToString("dd/MM/yyyy");

                btnstart.BackColor = Color.Blue;
                button1.BackColor = Color.Gray;

                btnstart.Enabled = true;
                button1.Enabled = false;
                //button1.Enabled = false;

                //try
                //{
                //    if (backgroundWorker.IsBusy)
                //    {
                //        //MessageBox.Show("Background worker is already running.");
                //        return;
                //    }
                //    backgroundWorker.RunWorkerAsync();
                //}
                //catch { }
            }
            catch { }            
        }
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Log.Information("Background task cancelled.");
            }
            else if (e.Error != null)
            {
                Log.Error("BackgroundWorker Error: {Message}", e.Error.Message);
            }
            else
            {
                Log.Information("Background task completed successfully.");
            }
        }

        bool flag = false;
        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                flag = false;
                while (!backgroundWorker.CancellationPending || !Stop_backgroundWorker)
                {
                    await BT_ModBus_Load();
                    Task.Delay(100).Wait();
                    //if(Stop_backgroundWorker)
                    //{
                    //   // backgroundWorker.CancelAsync();
                    //    break;
                    //}
                    if (Stop_backgroundWorker)
                    {
                        Console.WriteLine("BackgroundWorker is stopped!!!");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("at BackgroundWorker_DoWork: {Message}", ex.Message);
            }
        }

        bool flagForLoadEnd = false;
        bool Stop_backgroundWorker = false;
        private async Task BT_ModBus_Load()
        {
            string tdate = string.Empty;
            string insertquery1 = string.Empty;
            try
            {
               
                var dt = await LoadDataTo_DataTableAsync();
                foreach (DataRow dt_row in dt.Rows)
                {
                    if (Stop_backgroundWorker)
                    {
                        Console.WriteLine("foreach stopped!!!");
                        break;
                    }

                    var time11 = Convert.ToDateTime(dt_row["date"].ToString()).ToString("hh:mm:ss tt");
                    tdate  = Convert.ToDateTime(dt_row["date"].ToString()).ToString("yyyy-MM-dd");
                    await UpdateActualValuestoUI(txtTime, time11);
                    await UpdateActualValuestoUI(txtdate, tdate);
                    batchtime = txtTime.Text;
                    string tempbno = txtBatchNo.Text;

                    await UpdateActualValuestoUI(textBox1, tdate);
                    //SetTextBoxTextSafe(txtBatchNo, "1");



                    await UpdateActualValuestoUI(txtBatchDuration, "0");

                    await UpdateActualValuestoUI(txtHB1, GetSafeValue(dt_row, "hot_bin_1"));
                    AddValue("hot_bin_1", Convert.ToDouble(GetSafeValue(dt_row, "hot_bin_1")));  //

                    await UpdateActualValuestoUI(txtHB2, GetSafeValue(dt_row, "hot_bin_2"));
                    AddValue("hot_bin_2", Convert.ToDouble(GetSafeValue(dt_row, "hot_bin_2")));

                    await UpdateActualValuestoUI(txtHB3, GetSafeValue(dt_row, "hot_bin_3"));
                    AddValue("hot_bin_3", Convert.ToDouble(GetSafeValue(dt_row, "hot_bin_3")));


                    await UpdateActualValuestoUI(txtHB4, GetSafeValue(dt_row, "hot_bin_4"));
                    AddValue("hot_bin_4", Convert.ToDouble(GetSafeValue(dt_row, "hot_bin_4")));

                    //txtHB1Per.Text = dt_row["f5"].ToString();
                    //txtHB2Per.Text = dt_row["f7"].ToString();

                    //txtHB3Per.Text = dt_row["f9"].ToString();
                    //txtHB4Per.Text = dt_row["f11"].ToString();

                    //txtHB5.Text = dt_row["f12"].ToString();
                    //txtHB6.Text = dt_row["f14"].ToString();

                    //txtHB5Per.Text = dt_row["f13"].ToString();
                    //txtHB6Per.Text = dt_row["f15"].ToString();


                    await UpdateActualValuestoUI(txtFiller, GetSafeValue(dt_row, "filler"));
                    AddValue("filler", Convert.ToDouble(GetSafeValue(dt_row, "filler")));

                    //txtFillerPer.Text = dt_row["f17"].ToString();

                    //txtRAP.Text = dt_row["f18"].ToString();
                    //txtRAPPer.Text = dt_row["f19"].ToString();


                    await UpdateActualValuestoUI(txtAsphalt, GetSafeValue(dt_row, "asphalt"));
                    AddValue("asphalt", Convert.ToDouble(GetSafeValue(dt_row, "asphalt")));
                    //txtAsphaltPer.Text = dt_row["f21"].ToString(); 

                    //txtNet.Text = dt_row["f22"].ToString();

                    /***************************************************************/

                    //txtMixMatTemp.Text = dt_row["f33"].ToString();

                    await UpdateActualValuestoUI(txtSmoke, GetSafeValue(dt_row, "smoke_temp"));
                    AddValue("smoke_temp", Convert.ToDouble(GetSafeValue(dt_row, "smoke_temp")));

                    await UpdateActualValuestoUI(txtHotBinTemp, GetSafeValue(dt_row, "hot_bin1_temp"));
                    AddValue("hot_bin1_temp", Convert.ToDouble(GetSafeValue(dt_row, "hot_bin1_temp")));



                    await UpdateActualValuestoUI(txtTank1, GetSafeValue(dt_row, "asp_tank_1"));
                    AddValue("asp_tank_1", Convert.ToDouble(GetSafeValue(dt_row, "asp_tank_1")));

                    if (dt_row["batch_change"].ToString()=="1")
                    {
                        

                        double maxFiller = 0;
                        double maxFiller1 = 0;
                        double maxAggKg = 0;
                        double maxAspTank1 = 0;
                        double maxSmokeTemp = 0;
                        double maxHotBin1Temp = 0;
                        double maxTotalWeight = 0;
                        double maxAsphalt = 0;
                        double maxFOTemp = 0;
                        double maxAspPipe = 0;
                        double maxHotBin1 = 0;
                        double maxHotBin2 = 0;
                        double maxHotBin3 = 0;
                        double maxHotBin4 = 0;



                        var maxValues = await GetMaxValuesAsync(sensorData);

                         maxFiller = maxValues["filler"];
                         maxFiller1 = maxValues["filler_1"];
                         maxAggKg = maxValues["agg_kg"];
                         maxAspTank1 = maxValues["asp_tank_1"];
                         maxSmokeTemp = maxValues["smoke_temp"];
                         maxHotBin1Temp = maxValues["hot_bin1_temp"];
                         maxTotalWeight = maxValues["total_weight"];
                         maxAsphalt = maxValues["asphalt"];
                         maxFOTemp = maxValues["FO_temp"];
                         maxAspPipe = maxValues["asp_pipe"];
                         maxHotBin1 = maxValues["hot_bin_1"];
                         maxHotBin2 = maxValues["hot_bin_2"];
                         maxHotBin3 = maxValues["hot_bin_3"];
                         maxHotBin4 = maxValues["hot_bin_4"];

                        //await  GetRecordsFromXLS(true);

                        try
                        {
                            string batchno = "0";
                            string srno = "0";
                            string exportstatus = "Y";


                            string regno = "";  
                            string plantcode = "";  
                            string oprlat = "18.6720225"; 
                            string oprlong = "73.8138366";  
                            string oprjurisdiction = "Pune";
                            string oprdivision = "Pune";
                            string oprworkname = GetTextSafe(cmbworkname);//cmbworkname.Text;

                             

                            string deviceid = "";
                             
                            int viplupload = 0;
                            double netmixa = 0;
                             
                            int loadno = 0;
                            int truck_count = 0;

                            int preloadno = 0;
                            

                            plantcode = GetTextSafe(txtPlantCode);//txtPlantCode.Text;
                            deviceid = clsFunctions.activeDeviceID;
                            oprworkname = GetTextSafe(cmbworkname);//cmbworkname.Text;
                            regno = txtConCode.Text;  //GetTextSafe(txtConCode);//txtConCode.Text;

                            loadno = Convert.ToInt32(clsFunctions.GetMaxId("SELECT SUM(batchendflag)+1 FROM tblHotMixPlant"));
                            truck_count = loadno;

                            L_LoadNo = loadno;
                          

                            string ttime1 = GetTextSafe(txtTime);//txtTime.Text;
                              

                            //if(string.IsNullOrEmpty(txtBatchNo.Text))

                            batchno = clsFunctions.loadSingleValueAsync("select Max(batchno)+1  from tblHotMixPlant where tdate=#" + tdate + "#;").Result;//
                            if(string.IsNullOrEmpty(batchno))
                            {
                                batchno = "1";
                            }
                            L_BatchNo = Convert.ToInt32(batchno);

                            await UpdateActualValuestoUI(txtBatchNo, batchno);


                            string time_comp = DateTime.Now.AddMinutes(-5).ToString("HH:mm:ss");
                            string date_comp = DateTime.Today.ToString("MM/dd/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");
                            //var d = txtTime.Text;
                            bool a = IsExist(Convert.ToDouble(txtBatchNo.Text), txtdate.Text, txtTime.Text);

                            //txtdate.Text = Convert.ToDateTime(dt.Rows[0]["DATE1"].ToString()).ToString("dd/MM/yyyy");//Comment by bhavesh
                            {
                                //row["TRUCK_NO"].ToString();
                                lblbatchno.Text = txtBatchNo.Text;
                                //batchtime = txtTime.Text;
                                //batchno = txtBatchNo.Text;
                                Batch_Duration_InSec = txtBatchDuration.Text;
                                if (string.IsNullOrEmpty(Batch_Duration_InSec))
                                    Batch_Duration_InSec = "0";
                                Weight_KgPerBatch = txtNet.Text;
                                if (string.IsNullOrEmpty(Weight_KgPerBatch))
                                    Weight_KgPerBatch = "0";
                                F1_InPer_val = "0";
                                F2_InPer_val = "0";
                                F3_InPer_val = "0";
                                F4_InPer_val = "0";
                                F1_InKg_val = Convert.ToString(maxHotBin1);//txtHB1.Text;
                                F2_Inkg_val = Convert.ToString(maxHotBin2); //txtHB2.Text;
                                F3_Inkg_val = Convert.ToString(maxHotBin3);//txtHB3.Text;
                                F4_Inkg_val = Convert.ToString(maxHotBin4);//txtHB4.Text;
                                bitumenkg_val = Convert.ToString(maxAsphalt); //txtAsphalt.Text;

                                bitumenper_val = "0";
                                fillerkg_val = Convert.ToString(maxFiller);//txtFiller.Text;
                                fillerper_val = "0";

                                tank1_val = Convert.ToString(maxAspTank1);//txtTank1.Text;//bhavesh
                                tank2_val = "0";

                                exhausttemp_val = Convert.ToString(maxSmokeTemp);//txtSmoke.Text;

                                mixtemp_val = GetTextSafe(txtHotBinTemp);

                                if (Convert.ToDouble(tank1_val) < 0) tank1_val = "0";
                                if (Convert.ToDouble(tank2_val) < 0) tank2_val = "0";
                                if (Convert.ToDouble(exhausttemp_val) < 0) exhausttemp_val = "0";
                                if (Convert.ToDouble(mixtemp_val) < 0) mixtemp_val = "0";

                                aggregateinkg = (Convert.ToDouble(txtHB1.Text) + Convert.ToDouble(txtHB2.Text) + Convert.ToDouble(txtHB3.Text) + Convert.ToDouble(txtHB4.Text)).ToString();

                                //txtAggWt.Text = aggregateinkg.ToString();
                                await UpdateActualValuestoUI(txtAggWt, aggregateinkg.ToString());

                                Commulativebitumen = Commulativebitumen + Convert.ToDouble(txtNet.Text);
                                aggregatetph_val = "0";
                                bitumenkgmin_val = "0";
                                fillerkgmin_val = "0";
                                moisture_val = "0";
                                aggregateton_val = "0";

                                //netmix_val = (Convert.ToDouble(txtNet.Text) / 1000).ToString();

                                try
                                {
                                    string txtNet1 = ((Convert.ToDouble(txtHB1.Text) + Convert.ToDouble(txtHB2.Text) + Convert.ToDouble(txtHB3.Text) + Convert.ToDouble(txtHB4.Text) + Convert.ToDouble(fillerkg_val) + Convert.ToDouble(bitumenkg_val))).ToString();

                                    await UpdateActualValuestoUI(txtNet, txtNet1);

                                    Weight_KgPerBatch = txtNet.Text;
                                    try { netmix_val = (Convert.ToDouble(txtNet.Text) / 1000).ToString(); } catch { netmix_val = "0";  }



                                    //netmix_val = ((Convert.ToDouble(aggregateinkg) + Convert.ToDouble(fillerkg_val) + Convert.ToDouble(bitumenkg_val)) / 1000).ToString();
                                }
                                catch (Exception ex)
                                {
                                    clsFunctions.ErrorLog("[Exception] GetRecordsFromXLS - while calculating netmix_val : " + ex.Message);
                                    netmix_val = (Convert.ToDouble(txtNet.Text) / 1000).ToString();
                                }


                                // if (a == true )
                                //if (a == true && Convert.ToDateTime(batchtime) >= Convert.ToDateTime(currenttime))
                                {
                                    if (button1.Enabled == false && btnstart.Enabled == true)
                                    {
                                        //clsFunctions.ErrorLog("Condition False \"StopBtn.Enabled == false && btnstart.Enabled == true\" "); 
                                    }//added by annu
                                    else//added by annu
                                    {
                                        if (previoustime == "" || previoustime != batchtime)
                                        {
                                            previoustime = batchtime;
                                        }
                                        else
                                        {
                                            previoustime = Convert.ToDateTime(batchtime).AddSeconds(30).ToString();
                                        }

                                        srno = (dgvloaddetails.Rows.Count + 1).ToString();

                                        //dgvloaddetails.Rows.Insert(0, srno, batchtime, batchno, Batch_Duration_InSec, Weight_KgPerBatch, F1_InPer_val, F2_InPer_val, F3_InPer_val,
                                        //                F4_InPer_val, F1_InKg_val, F2_Inkg_val, F3_Inkg_val, F4_Inkg_val, bitumenkg_val, bitumenper_val, fillerkg_val,
                                        //                fillerper_val, mixtemp_val, exhausttemp_val, tank1_val, tank2_val, aggregateinkg, Commulativebitumen, aggregatetph_val,
                                        //                bitumenkgmin_val, fillerkgmin_val, moisture_val, aggregateton_val, netmix_val);

                                        InsertRowAtTopSafe(dgvloaddetails, srno, batchtime, batchno, Batch_Duration_InSec, Weight_KgPerBatch,
    F1_InPer_val, F2_InPer_val, F3_InPer_val, F4_InPer_val, F1_InKg_val, F2_Inkg_val, F3_Inkg_val, F4_Inkg_val,
    bitumenkg_val, bitumenper_val, fillerkg_val, fillerper_val, mixtemp_val, exhausttemp_val, tank1_val, tank2_val,
    aggregateinkg, Commulativebitumen, aggregatetph_val, bitumenkgmin_val, fillerkgmin_val, moisture_val,
    aggregateton_val, netmix_val);



                                        string insertquery = "Insert into tblHotMixPlant(regno,plantcode,oprlat,oprlong,oprjurisdiction,oprdivision,oprworkname,"
                      + "tdate,ttime,aggregatetph,bitumenkgmin,fillerkgmin,bitumenper,fillerper,F1_InPer,F2_InPer,F3_InPer,F4_InPer,moisture,mixtemp,exhausttemp,"
                      + "tank1,tank2,tipper,aggregateton,bitumenkg,fillerkg,netmix,batchno,srno,imeino,exportstatus,viplupload,worktype,workcode,material,"
                      + "Batch_Duration_InSec,Weight_KgPerBatch,HB1_KgPerBatch,HB2_KgPerBatch,HB3_KgPerBatch,HB4_KgPerBatch,Aggregate_Kg,Truck_Count,jobsite,loadno, InsertType)"
                      + " values('" + regno + "','" + plantcode + "','" + oprlat + "','" + oprlong + "','" + oprjurisdiction + "','" + oprdivision + "','" + oprworkname + "','"
                      + tdate + "','" + batchtime + "'," + aggregatetph_val + "," + bitumenkgmin_val + "," + fillerkgmin_val + "," + bitumenper_val + "," + fillerper_val + "," + F1_InPer_val + ","
                      + F2_InPer_val + "," + F3_InPer_val + "," + F4_InPer_val + "," + moisture_val + "," + mixtemp_val + "," + exhausttemp_val + "," + tank1_val + "," + tank2_val + ",'" + GetTextSafe(txttipperno) + "',"
                      + aggregateton_val + "," + bitumenkg_val + "," + fillerkg_val + "," + netmix_val + "," + batchno + "," + srno + ",'" + deviceid + "','" + exportstatus + "',"
                      + viplupload + ",0,'" + GetTextSafe(lblworkkode)+ "','" + GetTextSafe(Cmbmaterialtype) + "'," + Batch_Duration_InSec + "," + Weight_KgPerBatch + "," + F1_InKg_val + "," + F2_Inkg_val + "," + F3_Inkg_val + "," + F4_Inkg_val + "," + aggregateinkg + "," + truck_count + ",'" + GetTextSafe(cmbJobSite) + "'," + loadno + ", 'A')";
                                        
                                        
                                        insertquery1 = insertquery;

                                        await clsFunctions.AdoDataAsync(insertquery);

                                        flagForLoadEnd = true;


                                            // i++;
                                            string tempquery = insertquery.Replace("tblHotMixPlant", "bkp");
                                        await clsFunctions.AdoDataAsync(tempquery);

                                        //clearing the list only
                                        foreach (var key in sensorData.Keys)
                                        {
                                            if (sensorData.TryGetValue(key, out var list))
                                            {
                                                lock (list) // Optional: if lists might be accessed concurrently
                                                {
                                                    list.Clear();
                                                }
                                            }
                                        }

                                        enddate = tdate;
                                        endtime = ttime1;

                                        Thread.Sleep(1000);
                                    }
                                }
                                //else
                                //{
                                //    clsFunctions.ErrorLog("Condition False \"a == true && Convert.ToDateTime(batchtime) >= Convert.ToDateTime(currenttime)\" ");
                                //}
                            }



                        }
                         catch (Exception ex)
                        {
                            Log.Error(insertquery1, ex);
                            MessageBox.Show(ex.Message, "| [Exception] LoadDetails - GetRecordsFromXLS() ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsFunctions.ErrorLog("[Exception] LoadDetails - GetRecordsFromXLS() : " + ex.Message);

                        }

                    }

                    //updating the flag
                    string connectionString = $"Data Source={sqliteDbPath};Version=3;";
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        string update = $"UPDATE data SET status = 1 WHERE id = {dt_row["id"].ToString()};";
                        //var d = dt_row["id"].ToString();
                        await connection.ExecuteAsync(update);
                    }

                    await Task.Delay(500);
                     
 
                }
            }
            catch (Exception ex)
            {
                Log.Error("at RMC_ModBus_Load: {Message}", ex.Message);
            }
        }

        public async Task UpdateActualValuestoUI(TextBox targetTextBox, string value)
        {
            try
            {
                if (targetTextBox.InvokeRequired)
                {
                    targetTextBox.Invoke(new Action(() => UpdateActualValuestoUI(targetTextBox, value)));
                    return;
                }
                if(value!="0")
                    targetTextBox.Text = value;
            }
            catch (Exception ex)
            {
                Log.Error("UpdateActualValuestoUI error: {Message}", ex.Message);
            }
        }
        private static string GetSafeValue(DataRow r, string colName)
        {
            try
            {
                if (r.Table.Columns.Contains(colName))
                {
                    var value = r[colName];
                    if (value != DBNull.Value && !string.IsNullOrWhiteSpace(value?.ToString()))
                    {
                        return value.ToString();
                    }
                }
            }
            catch
            {
                // Ignore exception, return default
            }
            return "0";
        }
        //public void SetTextBoxTextSafe(TextBox textBox, string value)
        //{
        //    if (textBox.InvokeRequired)
        //    {
        //        textBox.Invoke(new Action(() => textBox.Text = value));
        //    }
        //    else
        //    {
        //        textBox.Text = value;
        //    }
        //}
        void AddValue(string key, double value)
        {
            if (sensorData.TryGetValue(key, out var list))
            {
                lock (list) // Lock on the list only
                {
                    list.Add(value); // Always added safely
                }
            }
            else
            {
                // Optional: throw or log missing key
                Console.WriteLine($"Key '{key}' not found.");
            }
        }

        public async Task<Dictionary<string, double>> GetMaxValuesAsync(ConcurrentDictionary<string, List<double>> sensorData)
        {
            return await Task.Run(() =>
            {
                var result = new Dictionary<string, double>();

                foreach (var kvp in sensorData)
                {
                    var list = kvp.Value;
                    lock (list) // Lock only the list to ensure thread safety
                    {
                        double max = list.Count > 0 ? list.Max() : 0.0;
                        result[kvp.Key] = max;
                    }
                }

                return result;
            });
        }

        private void AddRowSafe(DataGridView dgv, params object[] values)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new Action(() => dgv.Rows.Add(values)));
            }
            else
            {
                dgv.Rows.Add(values);
            }
        }
        private void InsertRowAtTopSafe(DataGridView dgv, params object[] values)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new Action(() => dgv.Rows.Insert(0, values)));
            }
            else
            {
                dgv.Rows.Insert(0, values);
            }
        }



        private async Task<DataTable> LoadDataTo_DataTableAsync()
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={sqliteDbPath};Version=3;"))
                {
                    await connection.OpenAsync(); 
                     
                    string query = @"SELECT * FROM data WHERE status = '0' ORDER BY datetime(Date) ASC;";

                    //string query = "SELECT * FROM dataset WHERE Status = 0 ORDER BY datetime(date) ASC;";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader); // Still sync but very fast in most cases
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("LoadDataTo_DataTableAsync error: {Message}", ex.Message);
                return null;
            }
        }


        //--------------------------------------------------------------------------------------------

        private void loadcombodata()
        {
            clsFunctions.activeDesciption = clsFunctions.getDescription();
            if (clsFunctions.activeDesciption != "")
            {
                label30.Text = clsFunctions.activeDesciption;
            }
            else
            {
                label30.Text = "Invalid DBType";
            }
            clsFunctions.checknewcolumn("iscompleted", "Text(255)", "workorder");

            //clsFunctions.FillCombo("Select Distinct workname from workorder where iscompleted <> 'Y' ", cmbworkname);
            //clsFunctions.FillCombo_setup("Select Distinct plantcode from PlantSetup", cmbplantcode);

            //clsFunctions.FillWorkOrdersInCombo(cmbworkname, clsFunctions.activeDeptName);

            clsFunctions.FillContractorInCombo(cmbConName, clsFunctions.activeDeptName);
            txtPlantCode.Text = clsFunctions.activePlantCode;

            clsFunctions.FillCombo("Select Distinct recipename from tblRecipeMaster", Cmbmaterialtype);
            clsFunctions.FillCombo("Select Distinct loadNo from tblHotMixPlant where exportstatus='N' and loadNo<>0", cmbbatchno);
            clsFunctions.FillCombo("Select Distinct tipperno from tblTipperdetails", cmbtipper);

            cmbtipper.Items.Insert(0, "Select Tipper");
            cmbtipper.SelectedIndex = 0;

        }


        //--------------------------------------------------------------------------------------------

        private void cmbworkname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbworkname.SelectedItem != null)
            {
                //filling job site combobox
                string workOrderId = clsFunctions.loadSingleValue("select workno from WorkOrder where workname='" + cmbworkname.SelectedItem + "'");
                try
                {
                    clsFunctions.FillCombo("Select SiteName from site_master where workorderID='" + workOrderId + "'", cmbJobSite);
                    cmbJobSite.SelectedIndex = 0;
                }
                catch { cmbJobSite.SelectedText = "NA"; }
                
                DataTable dt = clsFunctions.fillDatatable("Select workno,worktype,contractorName, ContractorID from WorkOrder where workname='" + cmbworkname.SelectedItem + "' AND ContractorName = '" + cmbConName.Text + "' ");
                
                if (dt.Rows.Count != 0)
                {
                    txtConCode.Text = dt.Rows[0]["ContractorID"].ToString();
                    lblworkkode.Text = dt.Rows[0]["workno"].ToString();
                    lblworktype.Text = clsFunctions.loadSingleValue("select wt_code from tblworktype where worktype like '" + dt.Rows[0]["worktype"].ToString() + "'");
                    lblwid.Text = lblworkkode.Text;
                }
                else
                {
                    //------------ When workname is longer than 255, use then use LIKE workname to fetch Work order data. --------------------------

                    try
                    {
                        clsFunctions.GetWorkOrderData_Like_for_BT(cmbworkname, lblwid, cmbConName, txtPlantCode, txtConCode, lblworktype);
                    }
                    catch
                    { }
                }

            }
        }

        //--------------------------------------------------------------------------------------------

        private void btnstart_Click(object sender, EventArgs e)
        {  
            try
            {
                currenttime = Convert.ToDateTime(time.Text).ToString("HH:mm:ss");
            }
            catch { }

            if (string.IsNullOrEmpty(cmbworkname.Text) || string.IsNullOrEmpty(Cmbmaterialtype.Text) || ((cmbtipper.Text).Contains("Select Tipper"))) { MessageBox.Show("Please select all fields"); return; }
            btnstart.Enabled = false;
            cmbworkname.Enabled = false;
            cmbtipper.Enabled = false;
            //added by dinesh
            cmbJobSite.Enabled = false;
            Cmbmaterialtype.Enabled = false;
            button1.Enabled = true;

            try
            {
                clsFunctions.ErrorLog("[INFO] LoadDetails - START Button Clicked.");

                try
                {
                    if (backgroundWorker.IsBusy)
                    {
                        //MessageBox.Show("Background worker is already running.");
                        return;
                    }
                    backgroundWorker.RunWorkerAsync();
                    Stop_backgroundWorker = false;
                }
                catch { }

                string path = Path.Combine(Application.StartupPath, "Database\\Jayshiv.exe");
                StartExeIfNotRunning(path);

                //string time2;
                //int maxbatch = clsFunctions.GetMaxId("Select MAX(batchno), MAX(ttime) from tblHotMixPlant where  tipper='" + cmbtipper.Text + "'");//clsFunctions.loadSingleValue("select MAX(batchnno) from tblHotMixPlant where tipper = '" + cmbtipper.SelectedItem.ToString() + "'");
                //string tippettime = clsFunctions.loadSingleValue("Select MAX(ttime) from tblHotMixPlant where batchno = " + maxbatch + " AND tipper = '" + cmbtipper.Text + "'");
                txtTruckCount.Text = Convert.ToInt32(clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant ")).ToString();

                //if (tippettime == "")
                //{
                //    tippettime = Convert.ToDateTime(time.Text).ToString("HH:mm:ss");//clsFunctions.loadSingleValue("Select MAX(ttime) from tblHotMixPlant where batchno = " + maxbatch + " AND tipper = '" + cmbtipper.Text + "'");
                //    time2 = tippettime;
                //}
                //else
                //{
                //    time2 = Convert.ToDateTime(tippettime).AddMinutes(tipperinterval).ToString("HH:mm:ss");
                //    //time2 = Convert.ToDateTime(tippettime).AddMinutes(59).ToString("HH:mm:ss"); 
                //}

                //if (Convert.ToDateTime(comparetime1) >= Convert.ToDateTime(time2))
                {
                    try
                    {
                        //FileNameDateFormat = Convert.ToDateTime(txtdate.Text).ToString("ddMMyyyy");
                    }
                    catch { };

                    clearData();

                    btnstart.BackColor = Color.Green;
                    button1.BackColor = Color.Gray;

                    btnstart.Enabled = false;
                    button1.Enabled = true;
                    txttipperno.Text = cmbtipper.SelectedItem.ToString();

                    // Start the background worker when the form loads   
                    //StartLiveDataUpload();
                    //return; //testing...
                    //clsFunctions.GetConnectionstrSetup();

                    //------------------------------------------------------

                    ////--  Select Plant Type for Source Data Type and collection
                    //if (clsFunctions.activeDesciption.ToString() == "Protool")       //if (clsFunctions.PlantType.ToString() == "1")
                    //{
                    //    label30.Text = "BT - Plant Sync(Protool)";
                    //    timer1.Enabled = true;
                    //}

                    ////------------------------------------------------------

                    //if (clsFunctions.activeDesciption.Contains("Asphalt"))
                    //{
                    //    label30.Text = "BT - Plant Sync(Asphalt Report)";
                    //    timer1.Enabled = true;
                    //}

                    //------------------------------------------------------

                    if (clsFunctions.activeDesciption.ToString() == "EXCEL")      //if (clsFunctions.PlantType.ToString() == "2")
                    {
                        //try
                        //{
                        //    System.Data.OleDb.OleDbConnection AccessConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\EXCELUTIL.mdb");
                        //    AccessConnection.Open();
                        //    Thread.Sleep(200);
                        //    System.Data.OleDb.OleDbCommand AccessCommand1 = new System.Data.OleDb.OleDbCommand("Delete From BaseTable", AccessConnection);
                        //    ////clsFunctions.ErrorLog("Insert into BaseTable Select * from ImportTable Where timevalue(f1)>=#" + strTime + " #  and f1 not in(Select f1 from BaseTable )");
                        //    AccessCommand1.ExecuteNonQuery();
                        //    AccessConnection.Close();
                        //}
                        //catch { }

                        label30.Text = "BT - Plant Sync(Excel)";
                       // timer1.Enabled = false;
                        //setTimer();
                    }

                    //------------------------------------------------------

                    //else if (clsFunctions.activeDesciption.ToString() == "Linnhoff")      //(clsFunctions.PlantType.ToString() == "Linnhoff")
                    //{
                    //    label30.Text = "BT - Plant Sync(Linnhoff)";
                    //    timer1.Enabled = false;
                    //    setLinnhoffTimer();
                    //}

                    //------------------------------------------------------

                }
                //else
                //   MessageBox.Show("select another vehical");
            }
            catch { }
        }

        //--------------------------------------------------------------------------------------------

        private System.Timers.Timer LinnhoffTimer;

        private void setLinnhoffTimer() //timer for linnhoff DB
        {
            try
            {
                //Indicator.BackColor = Color.Green;
                LinnhoffTimer = new System.Timers.Timer();
                LinnhoffTimer.Interval = 3000;

                // H2000up the Elapsed event for the timer. 
                LinnhoffTimer.Elapsed += OnLinnhoffTimedEvent;

                // Have the timer fire repeated events (true is the default)
                LinnhoffTimer.AutoReset = true;

                // Start the timer
                LinnhoffTimer.Enabled = true;

                Indicator.BackColor = Color.Gray;
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog(ex.Message + "setLinnhoffTimer()");
            }
        }

        //--------------------------------------------------------------------------------------------

        public void GetRecordsFromProtool()
        {
            try
            {                
                int batchno = 0;
                string exportstatus = "Y";

                //------------Comment by bhavesh----------------------------------
                // Random rnd = new Random();
                //string mix= rnd.Next(120, 140).ToString();
                //string bitu = rnd.Next(150, 170).ToString();
                //-------------------------------------------------

                //timer1.Enabled = true;
                //dgvloaddetails.Rows.Clear();
                string time_comp = Convert.ToDateTime(time.Text).AddMinutes(-5).ToString("HH:mm:ss");//DateTime.Now.AddMinutes(-5).ToString("HH:mm:ss");

                //commented by dinesh
                //string date_comp = Convert.ToDateTime(txtdate.Text).ToString("MM/dd/yyyy");//DateTime.Today.ToString("MM/dd/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");
                //added by Dinesh
                string date_comp = Convert.ToDateTime(txtdate.Text).ToString("yyyy/MM/dd");//DateTime.Today.ToString("MM/dd/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");

                //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 = #" + date_comp + "# AND TIME1 > #" + time_comp + "# order by LOAD_NO,BATCH_NO";

                // Original commented by BT 30122023
                //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";
                //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 order by LOAD_NO,BATCH_NO";

                DataTable dt = new DataTable();
                try
                {
                    //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";  // FILLER2 column as FLAG
                    string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";   // test column as FLAG
                    try
                    {
                        dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);

                        if (clsFunctions.protoolFlag == true)
                        {
                            //clsFunctions_comman.ErrorLog("FILLER2 not found, using 'test' column as FLAG ");
                            //this query is commented by dinesh because only date is used for filter the data, so used time also to filer only desired date and time data.
                            //selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 = #" + date_comp + "# order by LOAD_NO,BATCH_NO";

                            selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 = #" + date_comp + "# AND TIME1>=#"+ time_comp + "# order by LOAD_NO,BATCH_NO";

                            dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);
                        }
                    }
                    catch
                    {
                        clsFunctions_comman.ErrorLog("FILLER2 not found, using 'test' column as FLAG ");
                        selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";
                        dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);
                    }
                }
                catch
                {
                    clsFunctions_comman.ErrorLog("FILLER2 not found, using 'test' column as FLAG ");
                    string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";
                    dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);
                }

                //if dt is null then function return to next execution   by dinesh
                //if (dt == null || dt.Rows.Count == 0)
                //{
                //    //timer1.Stop();
                //    return;
                //}

                txttipperno.Text = cmbtipper.SelectedItem.ToString();//row["TRUCK_NO"].ToString();
                if (dt.Rows.Count > 0)
                {
                    //txtdate.Text = Convert.ToDateTime(dt.Rows[0]["DATE1"].ToString()).ToString("dd/MM/yyyy");//Comment by bhavesh
                    foreach (DataRow row in dt.Rows)
                    {
                        lblbatchno.Text = row["BATCH_NO"].ToString();
                        batchtime = Convert.ToDateTime(row["TIME1"].ToString()).ToString("HH:mm:ss");

                        //bool vehicleResponse = clsFunctions.LoadDetailsCompareDateTime(cmbtipper.Text, batchtime);
                        //if (vehicleResponse)
                        //{
                        //    if (clsFunctions.PlantType != "Linnhoff")
                        //    {
                        //        //aTimer.Interval = 1;
                        //        try
                        //        {
                        //            timer1.Enabled = false;
                        //            //DialogResult dialogResult = MessageBox.Show("Please select another vehicle");
                        //            //aTimer.Stop();
                        //            //aTimer.AutoReset = false;
                        //            //aTimer.Enabled = false;
                        //            return;
                        //        }
                        //        catch { return; }
                        //    }
                        //}
                        //--------------------------

                        //srno = row["BATCH_NO"].ToString();
                        batchno = Convert.ToInt32(lblbatchno.Text);

                        Batch_Duration_InSec = row["CYCLETIME"].ToString();
                        Weight_KgPerBatch = row["TOTALWT"].ToString();
                        F1_InPer_val = "0";
                        F2_InPer_val = "0";
                        F3_InPer_val = "0";
                        F4_InPer_val = "0";
                        F1_InKg_val = row["AG1"].ToString();
                        F2_Inkg_val = row["AG2"].ToString();
                        F3_Inkg_val = row["AG3"].ToString();
                        F4_Inkg_val = row["AG4"].ToString();
                        bitumenkg_val = row["ASPHALT"].ToString();

                        bitumenper_val = "0";
                        fillerkg_val = row["FILLER1"].ToString();
                        fillerper_val = "0";

                        tank2_val = row["CH7_TEMP"].ToString();
                        //exhausttemp_val = row["CH2_TEMP"].ToString();//((Convert.ToInt32(mixtemp_val)+Convert.ToInt32(tank1_val))/2).ToString();//bhavesh.

                        //21/02/2024 - As discussed with Govind sir & Mangesh sir ------------------------
                        //exhausttemp_val = row["CH4_TEMP"].ToString();         // 21/02/2024 - commented by BhaveshT

                        //-- OWrking at all ----
                        //exhausttemp_val = row["CH3_TEMP"].ToString();           // added by BhaveshT (CH3 - Bitumen Pipe)
                        //mixtemp_val = row["CH4_TEMP"].ToString();               // added by BhaveshT (CH4 - Hot Bin 1)
                        //tank1_val = row["CH5_TEMP"].ToString();                 // added by BhaveshT (CH5 - Bitumen Tank 1)

                        // for Jagson Buildcon - BT --- 14/02/2025

                        exhausttemp_val = row["CH2_TEMP"].ToString();           // added by BhaveshT (CH3 - Bitumen Pipe)
                        mixtemp_val = row["CH4_TEMP"].ToString();               // added by BhaveshT (CH4 - Hot Bin 1)
                        tank1_val = row["CH3_TEMP"].ToString();                 // added by BhaveshT (CH5 - Bitumen Tank 1)


                        //------------------------------------------------------------------------------------------------

                        //((Convert.ToInt32(mixtemp_val)+Convert.ToInt32(tank1_val))/2).ToString();//bhavesh.
                        //mixtemp_val = (Convert.ToDouble(tank1_val)-(Convert.ToDouble(tank1_val)*12/100)).ToString();  //bhavesh
                        //mixtemp_val = (Convert.ToDouble(tank1_val) - (Convert.ToDouble(tank1_val) * 12 / 100)).ToString();  //bhavesh       // commented on 16/01/2024
                        //mixtemp_val = Convert.ToDouble(tank1_val).ToString();  // 21/02/2024 - commented by BhaveshT
                        //-----------------------------

                        //mixtemp_val = (Convert.ToDouble(tank1_val) - (Convert.ToDouble(tank1_val) * 12 / 100)).ToString();  //bhavesh       // commented on 16/01/2024
                        //mixtemp_val = Convert.ToDouble(tank1_val).ToString();  //BhaveshT                                                  // Added on 16/01/2024 - BhaveshT

                        if (Convert.ToDouble(tank1_val) < 0) tank1_val = "0";
                        if (Convert.ToDouble(tank2_val) < 0) tank2_val = "0";
                        if (Convert.ToDouble(exhausttemp_val) < 0) exhausttemp_val = "0";
                        if (Convert.ToDouble(mixtemp_val) < 0) mixtemp_val = "0";

                        aggregateinkg = row["BATCHWT"].ToString();
                        Commulativebitumen = Commulativebitumen + Convert.ToDouble(bitumenkg_val);
                        aggregatetph_val = "0";
                        bitumenkgmin_val = "0";
                        fillerkgmin_val = "0";
                        moisture_val = "0";
                        aggregateton_val = "0";
                        netmix_val = (Convert.ToDouble(Weight_KgPerBatch) / 1000).ToString();

                        srno = (dgvloaddetails.Rows.Count + 1).ToString();

                        dgvloaddetails.Rows.Insert(0, srno, batchtime, batchno, Batch_Duration_InSec, Weight_KgPerBatch, F1_InPer_val, F2_InPer_val, F3_InPer_val,
                                                F4_InPer_val, F1_InKg_val, F2_Inkg_val, F3_Inkg_val, F4_Inkg_val, bitumenkg_val, bitumenper_val, fillerkg_val,
                                                fillerper_val, mixtemp_val, exhausttemp_val, tank1_val, tank2_val, aggregateinkg, Commulativebitumen, aggregatetph_val,
                                                bitumenkgmin_val, fillerkgmin_val, moisture_val, aggregateton_val, netmix_val);
                    }

                    // From Protool DB to Unipro DB
                    SaveDataToHotMixScada(dt);

                    // string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# AND TIME1 > #" + time_comp + "# ";
                    string update_query;
                    try
                    {
                        //update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# ";
                        if (clsFunctions.protoolFlag == false)
                        {
                            update_query = "UPDATE Production SET test='0' WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# ";
                            clsFunctions.UpdateProduction(update_query);
                        }
                        if (clsFunctions.protoolFlag == true)
                        {
                            update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# ";
                            clsFunctions.UpdateProduction(update_query);
                        }
                    }
                    catch
                    {
                        update_query = "UPDATE Production SET test='0' WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# ";
                        clsFunctions.UpdateProduction(update_query);
                    }
                    //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1>#25-12-2022#";
                }
                //added by Dinesh
                dt.Clear();
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("GetRecordsFromProtool "+ex.Message);
                // MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------------
        
        // 21/10/2024 : BhaveshT ----------------

        public void GetRecordsFromAsphaltMDB()
        {
            try
            {
                int batchno = 0;
                string exportstatus = "Y";

                string time_comp = Convert.ToDateTime(time.Text).AddMinutes(-5).ToString("HH:mm:ss");//DateTime.Now.AddMinutes(-5).ToString("HH:mm:ss");

                string date_comp = Convert.ToDateTime(txtdate.Text).ToString("yyyy/MM/dd");//DateTime.Today.ToString("MM/dd/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");

                DataTable dt = new DataTable();
                try
                {
                    //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";   // test column as FLAG
                    
                    string selectquery = "SELECT Load_no, BatchNo, Dat, Tim, TruckNo, TruckCnt, AG1_DACT, AG2_DACT, AG3_DACT, AG4_DACT, Filler1_DACT, " +
                        "Asphalt_DACT, AggregateWt_DACT, TatalWt_DACT, AsphaltT1, AsphaltT2, SmokeTemp, HotbinTemp, Filler2_SET FROM Production_Report " +
                        "WHERE Load_no<>0 AND Filler2_SET='0' AND Dat=#" + date_comp + "# order by Load_no,BatchNo";   // test column as FLAG

                    try
                    {
                        dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);
                    }
                    catch
                    {

                    }
                }
                catch
                {

                }

                txttipperno.Text = cmbtipper.SelectedItem.ToString();       //row["TRUCK_NO"].ToString();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        lblbatchno.Text = row["BatchNo"].ToString();       //lblbatchno.Text = row["BATCH_NO"].ToString();
                        batchtime = Convert.ToDateTime(row["Tim"].ToString()).ToString("HH:mm:ss");   //batchtime = Convert.ToDateTime(row["TIME1"].ToString()).ToString("HH:mm:ss");

                        //srno = row["BATCH_NO"].ToString();
                        batchno = Convert.ToInt32(lblbatchno.Text);

                        //Batch_Duration_InSec = row["CYCLETIME"].ToString();
                        Weight_KgPerBatch = row["TatalWt_DACT"].ToString();      //Weight_KgPerBatch = row["TOTALWT"].ToString();
                        F1_InPer_val = "0";
                        F2_InPer_val = "0";
                        F3_InPer_val = "0";
                        F4_InPer_val = "0";

                        F1_InKg_val = row["AG1_DACT"].ToString();        //F1_InKg_val = row["AG1"].ToString();
                        F2_Inkg_val = row["AG2_DACT"].ToString();        //F2_Inkg_val = row["AG2"].ToString();
                        F3_Inkg_val = row["AG3_DACT"].ToString();        //F3_Inkg_val = row["AG3"].ToString();
                        F4_Inkg_val = row["AG4_DACT"].ToString();        //F4_Inkg_val = row["AG4"].ToString();
                                                
                        bitumenkg_val = row["Asphalt_DACT"].ToString();       //bitumenkg_val = row["ASPHALT"].ToString();

                        bitumenper_val = "0";
                        fillerkg_val = row["Filler1_DACT"].ToString();
                        fillerper_val = "0";

                        tank1_val = row["AsphaltT1"].ToString();//bhavesh
                        tank2_val = row["AsphaltT2"].ToString();
                        //exhausttemp_val = row["CH2_TEMP"].ToString();//((Convert.ToInt32(mixtemp_val)+Convert.ToInt32(tank1_val))/2).ToString();//bhavesh.

                        //21/02/2024 - As discussed with Govind sir & Mangesh sir ------------------------
                        //exhausttemp_val = row["CH4_TEMP"].ToString();         // 21/02/2024 - commented by BhaveshT
                        exhausttemp_val = row["SmokeTemp"].ToString();           // added by BhaveshT (CH3 - Bitumen Pipe)
                        mixtemp_val = row["HotbinTemp"].ToString();               // added by BhaveshT (CH4 - Hot Bin 1)
                        //------------------------------------------------------------------------------------------------

                        //((Convert.ToInt32(mixtemp_val)+Convert.ToInt32(tank1_val))/2).ToString();//bhavesh.
                        //mixtemp_val = (Convert.ToDouble(tank1_val)-(Convert.ToDouble(tank1_val)*12/100)).ToString();  //bhavesh
                        //mixtemp_val = (Convert.ToDouble(tank1_val) - (Convert.ToDouble(tank1_val) * 12 / 100)).ToString();  //bhavesh       // commented on 16/01/2024
                        //mixtemp_val = Convert.ToDouble(tank1_val).ToString();  // 21/02/2024 - commented by BhaveshT
                        //-----------------------------

                        //mixtemp_val = (Convert.ToDouble(tank1_val) - (Convert.ToDouble(tank1_val) * 12 / 100)).ToString();  //bhavesh       // commented on 16/01/2024
                        //mixtemp_val = Convert.ToDouble(tank1_val).ToString();  //BhaveshT                                                  // Added on 16/01/2024 - BhaveshT

                        if (Convert.ToDouble(tank1_val) < 0) tank1_val = "0";
                        if (Convert.ToDouble(tank2_val) < 0) tank2_val = "0";
                        if (Convert.ToDouble(exhausttemp_val) < 0) exhausttemp_val = "0";
                        if (Convert.ToDouble(mixtemp_val) < 0) mixtemp_val = "0";

                        aggregateinkg = row["AggregateWt_DACT"].ToString();
                        Commulativebitumen = Commulativebitumen + Convert.ToDouble(bitumenkg_val);
                        aggregatetph_val = "0";
                        bitumenkgmin_val = "0";
                        fillerkgmin_val = "0";
                        moisture_val = "0";
                        aggregateton_val = "0";
                        netmix_val = (Convert.ToDouble(Weight_KgPerBatch) / 1000).ToString();

                        //--------------------------- Display in TextBoxes ----------------------------

                        try
                        {
                            txtTime.Text = batchtime;
                            txtBatchDuration.Text = Batch_Duration_InSec;
                            txtBatchNo.Text = lblbatchno.Text;

                            txtHB1.Text = F1_InKg_val;
                            txtHB2.Text = F2_Inkg_val;
                            txtHB3.Text = F3_Inkg_val;
                            txtHB4.Text = F4_Inkg_val;

                            txtAsphalt.Text = bitumenkg_val;
                            txtFiller.Text = fillerkg_val;

                            txtSmoke.Text = exhausttemp_val;
                            txtHotBinTemp.Text = mixtemp_val;
                            txtTank1.Text = tank1_val;

                            txtNet.Text = netmix_val;
                            txtAggWt.Text = aggregateinkg;

                            TempValidatorAlert();
                        }
                        catch (Exception ex)
                        {

                        }

                        //-------------------------------------------------------


                        srno = (dgvloaddetails.Rows.Count + 1).ToString();

                        dgvloaddetails.Rows.Insert(0, srno, batchtime, batchno, Batch_Duration_InSec, Weight_KgPerBatch, F1_InPer_val, F2_InPer_val, F3_InPer_val,
                                                F4_InPer_val, F1_InKg_val, F2_Inkg_val, F3_Inkg_val, F4_Inkg_val, bitumenkg_val, bitumenper_val, fillerkg_val,
                                                fillerper_val, mixtemp_val, exhausttemp_val, tank1_val, tank2_val, aggregateinkg, Commulativebitumen, aggregatetph_val,
                                                bitumenkgmin_val, fillerkgmin_val, moisture_val, aggregateton_val, netmix_val);
                    }

                    // From AsphaltReport DB to Unipro DB
                    SaveDataToHotMixScadaForAsphaltMDB(dt);

                    // string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# AND TIME1 > #" + time_comp + "# ";
                    string update_query;
                    try
                    {
                        //update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# ";
                        //if (clsFunctions.protoolFlag == false)
                        //{
                        //    update_query = "UPDATE Production_Report SET Filler2_DACT ='0' WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# ";
                        //    clsFunctions.UpdateProduction(update_query);
                        //}
                        
                    }
                    catch
                    {
                        update_query = "UPDATE Production SET test='0' WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# ";
                        clsFunctions.UpdateProduction(update_query);
                    }
                    //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1>#25-12-2022#";
                }
                //added by Dinesh
                dt.Clear();
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("GetRecordsFromProtool "+ex.Message);
                // MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------------

        // commented by BT on 19012024
        //public void GetRecordsFromProtool()
        //{
        //    try
        //    {
        //        string exportstatus = "Y";
        //        //------------Comment by bhavesh----------------------------------


        //        // Random rnd = new Random();
        //        //string mix= rnd.Next(120, 140).ToString();
        //        //string bitu = rnd.Next(150, 170).ToString();

        //        //-------------------------------------------------

        //        //timer1.Enabled = true;
        //        //dgvloaddetails.Rows.Clear();
        //        string time_comp = Convert.ToDateTime(time.Text).AddMinutes(-5).ToString("HH:mm:ss");//DateTime.Now.AddMinutes(-5).ToString("HH:mm:ss");

        //        //commented by dinesh
        //        //string date_comp = Convert.ToDateTime(txtdate.Text).ToString("MM/dd/yyyy");//DateTime.Today.ToString("MM/dd/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");
        //        //added by Dinesh
        //        string date_comp = Convert.ToDateTime(txtdate.Text).ToString("yyyy/MM/dd");//DateTime.Today.ToString("MM/dd/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");

        //        //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 = #" + date_comp + "# AND TIME1 > #" + time_comp + "# order by LOAD_NO,BATCH_NO";

        //        // Original commented by BT 30122023
        //        //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";

        //        //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 order by LOAD_NO,BATCH_NO";

        //        DataTable dt = new DataTable();
        //        try
        //        {
        //            //string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";  // FILLER2 column as FLAG

        //            string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";   // test column as FLAG
        //            try
        //            {
        //                dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);

        //                if (clsFunctions.protoolFlag == true)
        //                {
        //                    //clsFunctions_comman.ErrorLog("FILLER2 not found, using 'test' column as FLAG ");

        //                    selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";

        //                    dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);
        //                }
        //            }
        //            catch
        //            {
        //                clsFunctions_comman.ErrorLog("FILLER2 not found, using 'test' column as FLAG ");

        //                selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";

        //                dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);
        //            }
        //        }
        //        catch
        //        {
        //            clsFunctions_comman.ErrorLog("FILLER2 not found, using 'test' column as FLAG ");

        //            string selectquery = "SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# order by LOAD_NO,BATCH_NO";

        //            dt = frmEagleBatchMAster.OledbfillDatatable(selectquery);
        //        }



        //        //if dt is null then function return to next execution   by dinesh
        //        //if (dt == null || dt.Rows.Count == 0)
        //        //{
        //        //    //timer1.Stop();
        //        //    return;
        //        //}

        //        txttipperno.Text = cmbtipper.SelectedItem.ToString();//row["TRUCK_NO"].ToString();
        //        if (dt.Rows.Count != 0)
        //        {
        //            //txtdate.Text = Convert.ToDateTime(dt.Rows[0]["DATE1"].ToString()).ToString("dd/MM/yyyy");//Comment by bhavesh
        //            foreach (DataRow row in dt.Rows)
        //            {

        //                lblbatchno.Text = row["BATCH_NO"].ToString();
        //                batchtime = Convert.ToDateTime(row["TIME1"].ToString()).ToString("HH:mm:ss");

        //                bool vehicleResponse = clsFunctions.LoadDetailsCompareDateTime(cmbtipper.Text, batchtime);
        //                if (vehicleResponse)
        //                {
        //                    if (clsFunctions.PlantType != "Linnhoff")
        //                    {
        //                        //aTimer.Interval = 1;
        //                        try
        //                        {
        //                            timer1.Enabled = false;
        //                            //DialogResult dialogResult = MessageBox.Show("Please select another vehicle");
        //                            //aTimer.Stop();
        //                            //aTimer.AutoReset = false;
        //                            //aTimer.Enabled = false;
        //                            return;
        //                        }
        //                        catch { return; }
        //                    }
        //                }


        //                srno = row["BATCH_NO"].ToString();
        //                Batch_Duration_InSec = row["CYCLETIME"].ToString();
        //                Weight_KgPerBatch = row["TOTALWT"].ToString();
        //                F1_InPer_val = "0";
        //                F2_InPer_val = "0";
        //                F3_InPer_val = "0";
        //                F4_InPer_val = "0";
        //                F1_InKg_val = row["AG1"].ToString();
        //                F2_Inkg_val = row["AG2"].ToString();
        //                F3_Inkg_val = row["AG3"].ToString();
        //                F4_Inkg_val = row["AG4"].ToString();
        //                bitumenkg_val = row["ASPHALT"].ToString();

        //                bitumenper_val = "0";
        //                fillerkg_val = row["FILLER1"].ToString();
        //                fillerper_val = "0";


        //                tank1_val = row["CH5_TEMP"].ToString();//bhavesh
        //                tank2_val = row["CH7_TEMP"].ToString();
        //                //exhausttemp_val = row["CH2_TEMP"].ToString();//((Convert.ToInt32(mixtemp_val)+Convert.ToInt32(tank1_val))/2).ToString();//bhavesh.
        //                exhausttemp_val = row["CH4_TEMP"].ToString();//((Convert.ToInt32(mixtemp_val)+Convert.ToInt32(tank1_val))/2).ToString();//bhavesh.
        //                //mixtemp_val = (Convert.ToDouble(tank1_val)-(Convert.ToDouble(tank1_val)*12/100)).ToString();  //bhavesh
        //                mixtemp_val = (Convert.ToDouble(tank1_val) - (Convert.ToDouble(tank1_val) * 12 / 100)).ToString();  //bhavesh

        //                if (Convert.ToDouble(tank1_val) < 0) tank1_val = "0";
        //                if (Convert.ToDouble(tank2_val) < 0) tank2_val = "0";
        //                if (Convert.ToDouble(exhausttemp_val) < 0) exhausttemp_val = "0";
        //                if (Convert.ToDouble(mixtemp_val) < 0) mixtemp_val = "0";

        //                aggregateinkg = row["BATCHWT"].ToString();
        //                Commulativebitumen = Commulativebitumen + Convert.ToDouble(bitumenkg_val);
        //                aggregatetph_val = "0";
        //                bitumenkgmin_val = "0";
        //                fillerkgmin_val = "0";
        //                moisture_val = "0";
        //                aggregateton_val = "0";
        //                netmix_val = (Convert.ToDouble(Weight_KgPerBatch) / 1000).ToString();

        //                dgvloaddetails.Rows.Insert(0, batchtime, srno, Batch_Duration_InSec, Weight_KgPerBatch, F1_InPer_val, F2_InPer_val, F3_InPer_val,
        //                                        F4_InPer_val, F1_InKg_val, F2_Inkg_val, F3_Inkg_val, F4_Inkg_val, bitumenkg_val, bitumenper_val, fillerkg_val,
        //                                        fillerper_val, mixtemp_val, exhausttemp_val, tank1_val, tank2_val, aggregateinkg, Commulativebitumen, aggregatetph_val,
        //                                        bitumenkgmin_val, fillerkgmin_val, moisture_val, aggregateton_val, netmix_val);


        //            }
        //            // From Protool DB to Unipro DB
        //            SaveDataToHotMixScada(dt);

        //            // string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# AND TIME1 > #" + time_comp + "# ";

        //            string update_query;
        //            try
        //            {
        //                //update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# ";
        //                if(clsFunctions.protoolFlag == false)
        //                {
        //                    update_query = "UPDATE Production SET test='0' WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# ";
        //                    clsFunctions.UpdateProduction(update_query);
        //                }
        //                if (clsFunctions.protoolFlag == true)
        //                {
        //                    update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + date_comp + "# ";
        //                    clsFunctions.UpdateProduction(update_query);
        //                }
        //            }
        //            catch
        //            {

        //                update_query = "UPDATE Production SET test='0' WHERE LOAD_NO<>0 AND test='0' AND DATE1=#" + date_comp + "# ";
        //                clsFunctions.UpdateProduction(update_query);
        //            }

        //            //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1>#25-12-2022#";



        //        }

        //        //added by Dinesh
        //        dt.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        //clsFunctions.ErrorLog("GetRecordsFromProtool "+ex.Message);
        //        // MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        //--------------------------------------------------------------------------------------------

        private bool ChkValidation()
        {
            if (cmbbatchno.SelectedItem == null)
            {
                MessageBox.Show("Please select Batch No.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbworkname.SelectedItem == null)
            {
                MessageBox.Show("Please select Work Name.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtPlantCode.Text == "")
            {
                MessageBox.Show("Please set Plant for Production.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txttipperno.Text.Trim() == "")
            {
                MessageBox.Show("Please select tipper No.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Cmbmaterialtype.SelectedItem == null)
            {
                MessageBox.Show("Please select Material.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        //--------------------------------------------------------------------------------------------

        //private void btnstop_Click(object sender, EventArgs e)
        //{

        //}

        private void clearData()
        {
            //cmbworkname.SelectedItem = null;
            //cmbtipper.SelectedIndex = 0;
            //txtregno.Text = "";
            //cmbplantcode.SelectedIndex = 0;
            //txttipperno.Text = "";
            //txtoprlat.Text = "";
            //txtoprlong.Text="";
            //txtoprjurisdiction.Text="";
            //txtoprdivision.Text="";
            //txtdate.Text = "";
            dgvloaddetails.Rows.Clear();
            // btnstop.Enabled = false;
            // btnstart.Enabled = true;
            // Cmbmaterialtype.SelectedItem = null;
            //btnstart.BackColor = Color.WhiteSmoke;
        }

        //--------------------------------------------------------------------------------------------

        string batchtime = "";
        string Batch_Duration_InSec = "0";
        string Weight_KgPerBatch = "0";
        string aggregatetph_val = "0";
        string bitumenkgmin_val = "0";
        string fillerkgmin_val = "0";
        string bitumenper_val = "0";
        string fillerper_val = "0";
        string F1_InPer_val = "0";
        string F2_InPer_val = "0";
        string F3_InPer_val = "0";
        string F4_InPer_val = "0";
        string F1_InKg_val = "0";
        string F2_Inkg_val = "0";
        string F3_Inkg_val = "0";
        string F4_Inkg_val = "0";
        string moisture_val = "0";
        string mixtemp_val = "0";
        string exhausttemp_val = "0";
        string tank1_val = "0";
        string tank2_val = "0";
        string aggregateton_val = "0";
        string bitumenkg_val = "0";
        string fillerkg_val = "0";
        string netmix_val = "0";
        string aggregateinkg = "0";
        double Commulativebitumen = 0;
        string srno = "0";

        //--------------------------------------------------------------------------------------------

        public void ShowdatatoGridview(string text)
        {
            if (1 == 6)
            {
            }
        }

        //--------------------------------------------------------------------------------------------

        private void cmbbatchno_SelectedIndexChanged(object sender, EventArgs e)
        {
            string srno = "";
            string batchno = "";

            if (cmbbatchno.SelectedItem != null)
            {
                dgvloaddetails.Rows.Clear();
                string selectquery = "select batchno,Batch_Duration_InSec,Weight_KgPerBatch,tipper,tdate,srno,ttime,aggregatetph,bitumenkgmin,fillerkgmin,bitumenper,fillerper,F1_InPer,F2_InPer,F3_InPer,F4_InPer,HB1_KgPerBatch,HB2_KgPerBatch,HB3_KgPerBatch,HB4_KgPerBatch,moisture,mixtemp,exhausttemp,tank1,tank2,aggregateton,Aggregate_Kg,bitumenkg,fillerkg,netmix from tblHotMixPlant where loadno=" + cmbbatchno.Text + " and exportstatus='N' order by srno";
                DataTable dt = clsFunctions.fillDatatable(selectquery);
                if (dt.Rows.Count != 0)
                {
                    txtdate.Text = Convert.ToDateTime(dt.Rows[0]["tdate"].ToString()).ToString("dd/MM/yyyy");
                    foreach (DataRow row in dt.Rows)
                    {
                        txttipperno.Text = row["tipper"].ToString();
                        lblbatchno.Text = row["batchno"].ToString();
                        batchno = row["batchno"].ToString();
                        batchtime = Convert.ToDateTime(row["ttime"].ToString()).ToString("HH:mm:ss");
                        srno = row["srno"].ToString();
                        Batch_Duration_InSec = row["Batch_Duration_InSec"].ToString();
                        Weight_KgPerBatch = row["Weight_KgPerBatch"].ToString();
                        F1_InPer_val = row["F1_InPer"].ToString();
                        F2_InPer_val = row["F2_InPer"].ToString();
                        F3_InPer_val = row["F3_InPer"].ToString();
                        F4_InPer_val = row["F4_InPer"].ToString();
                        F1_InKg_val = row["HB1_KgPerBatch"].ToString();
                        F2_Inkg_val = row["HB2_KgPerBatch"].ToString();
                        F3_Inkg_val = row["HB3_KgPerBatch"].ToString();
                        F4_Inkg_val = row["HB4_KgPerBatch"].ToString();
                        bitumenkg_val = row["bitumenkg"].ToString();
                        bitumenper_val = row["bitumenper"].ToString();
                        fillerkg_val = row["fillerkg"].ToString();
                        fillerper_val = row["fillerper"].ToString();
                        mixtemp_val = row["mixtemp"].ToString();
                        exhausttemp_val = row["exhausttemp"].ToString();
                        tank1_val = row["tank1"].ToString();
                        tank2_val = row["tank2"].ToString();
                        aggregateinkg = row["Aggregate_Kg"].ToString();
                        Commulativebitumen = Commulativebitumen + Convert.ToDouble(bitumenkg_val);
                        aggregatetph_val = row["aggregatetph"].ToString();
                        bitumenkgmin_val = row["bitumenkgmin"].ToString();
                        fillerkgmin_val = row["fillerkgmin"].ToString();
                        moisture_val = row["moisture"].ToString();
                        aggregateton_val = row["aggregateton"].ToString();
                        netmix_val = row["netmix"].ToString();

                        dgvloaddetails.Rows.Insert(0, srno, batchtime, batchno, Batch_Duration_InSec, Weight_KgPerBatch, F1_InPer_val, F2_InPer_val, F3_InPer_val,
                                                F4_InPer_val, F1_InKg_val, F2_Inkg_val, F3_Inkg_val, F4_Inkg_val, bitumenkg_val, bitumenper_val, fillerkg_val,
                                                fillerper_val, mixtemp_val, exhausttemp_val, tank1_val, tank2_val, aggregateinkg, Commulativebitumen, aggregatetph_val,
                                                bitumenkgmin_val, fillerkgmin_val, moisture_val, aggregateton_val, netmix_val);
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------------

        public int truck_count;  // Added for Protool
        private void cmbtipper_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tippettime = clsFunctions.loadSingleValue("select ttime from tblHotMixPlant where tipper = '" + cmbtipper.SelectedItem.ToString() + "'");
            string time2 = DateTime.Now.ToString("HH:mm:ss"); ;
            if (tippettime != "0") time2 = Convert.ToDateTime(tippettime).AddMinutes(tipperinterval).ToString("HH:mm:ss");
            //time2 = DateTime.Now.ToString("HH:mm:ss");
            string comparetime1 = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")).ToString("HH:mm:ss");      // Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));

            if (cmbtipper.SelectedIndex != 0)
            {
                if (Convert.ToDateTime(comparetime1) >= Convert.ToDateTime(time2))// if (Convert.ToDateTime(tipperinterval) >= Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")))
                {
                    txttipperno.Text = cmbtipper.SelectedItem.ToString();
                    //btnstart.Enabled = true;
                }
                else
                {
                    //btnstart.Enabled = false;
                    MessageBox.Show("Do you want to select this Vehicle");       //MessageBox.Show("Please select another Vehicle");
                    txttipperno.Text = cmbtipper.SelectedItem.ToString();
                    //txttipperno.Text = "";
                }
            }

            //--------------------------------------------------------------------------------------------

            //---------------
            //BT new
            //DataTable tipperData = clsFunctions.fillDatatable("SELECT ttime, tdate FROM tblHotMixPlant WHERE tipper = '" + cmbtipper.SelectedItem.ToString() + "'");
            //
            //if (tipperData.Rows.Count > 0)
            //{
            //    DataRow row = tipperData.Rows[0];
            //
            //    string tipperTime = row["ttime"].ToString();
            //    string tipperDate = row["tdate"].ToString();
            //
            //    DateTime tipperDateTime = DateTime.ParseExact(tipperDate + " " + tipperTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            //    DateTime comparisonTime = DateTime.Now.AddMinutes(tipperinterval);
            //
            //    if (tipperDateTime <= comparisonTime)
            //    {
            //        txttipperno.Text = cmbtipper.SelectedItem.ToString();
            //        btnstart.Enabled = true;
            //    }
            //    else
            //    {
            //        btnstart.Enabled = false;
            //        MessageBox.Show("Please select another Vehicle");
            //    }
            //}
        }

        //--------------------------------------------------------------------------------------------

        private void SaveDataToHotMixScada(DataTable dt1)       // for Protool
        {
            //adding if condition for cheking whether dt1 datatable contains the data or not
            if (dt1.Rows.Count > 0)
            {
                try
                {
                    string regno = ""; //string regno = "99";
                    string plantcode = ""; //string plantcode = "";
                    string oprlat = "18.6720225";// string oprlat = "";     //added default values by dinesh
                    string oprlong = "73.8138366"; //string oprlong = "";    //added default values by dinesh
                    string oprjurisdiction = "Pune";// string oprjurisdiction = "";   //added default values by dinesh
                    string oprdivision = "Pune"; //string oprdivision = "";            //added default values by dinesh
                    string oprworkname = cmbworkname.Text;
                    //added by dinesh
                    // string strSiteName = cmbJobSite.Text;

                    string deviceid = "";
                    string exportstatus = "Y";
                    int viplupload = 0;
                    double netmixa = 0;
                    int batchno = 0;
                    int loadno = 0;
                    int truck_count = 0;
                    int preloadno = 0;
                    //int srno = 0;

                    //DataTable plntdt = clsFunctions.fillDatatable_setup("select * from PlantSetup");
                    //if (plntdt.Rows.Count > 0)
                    //{
                    //    DataRow r = plntdt.Rows[0];
                    //    //regno = r["regno"].ToString();
                    //    //plantcode = r["plantcode"].ToString();
                    //    //imeino = r["imeino"].ToString();
                    //    //regno = r["ContractorCode"].ToString();
                    //    plantcode = r["PlantCode"].ToString();
                    //    deviceid = r["DeviceID"].ToString();
                    //}
                    //DataTable workdt = clsFunctions.fillDatatable("select * from WorkOrder");
                    //if (workdt.Rows.Count > 0)
                    //{
                    //    DataRow r = workdt.Rows[0];
                    //    oprworkname = r["workname"].ToString();
                    //    //commented by dinesh
                    //    // oprlat = r["oprlatitude"].ToString();
                    //    //oprlong = r["oprlongitude"].ToString();
                    //    // oprjurisdiction = r["oprjurisdiction"].ToString();
                    //    // oprdivision = r["oprdivision"].ToString();
                    //
                    //}

                    // 10/01/2024 - BhaveshT
                    oprworkname = cmbworkname.Text;
                    regno = txtConCode.Text;
                    plantcode = txtPlantCode.Text;
                    deviceid = clsFunctions.activeDeviceID;

                    //------------------------------------------
                    // 01/01/2024 : Bhavesh - Added code for loadno & truck_count as per BatchDataHTML form

                    //loadno = clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant where tdate = #" + DateTime.Now.ToString("yyyy/MM/dd") + "#; ");
                    //loadno = clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant");

                    loadno = Convert.ToInt32(clsFunctions.GetMaxId("SELECT SUM(batchendflag)+1 FROM tblHotMixPlant"));//where tdate =#" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "# "));

                    truck_count = loadno;
                    //------------------------------------------

                    int localSrno = Convert.ToInt32(srno) - dgvloaddetails.Rows.Count;

                    int i = -1;
                    foreach (DataRow row in dt1.Rows)
                    {
                        i++;
                        string ttime1 = Convert.ToDateTime(row["TIME1"]).ToString("HH:mm:ss");
                        batchno = Convert.ToInt32(row["BATCH_NO"].ToString());
                        localSrno++;

                        // As discussed with Govind sir - 01012024 - Dont take loadno from client DB
                        //loadno = Convert.ToInt32(row["LOAD_NO"].ToString());
                        //if (preloadno != loadno)
                        //{
                        //    batchno = clsFunctions.GetMaxId("select Max(batchno)+1 from tblHotMixPlant");
                        //}                        
                        //batchno = srno;//Convert.ToInt32(row["BATCH_NO"].ToString());

                        L_LoadNo = loadno;        // for endBatch - BhaveshT : 23/02/2024
                        L_BatchNo = batchno;      // for endBatch - BhaveshT

                        string update_date = Convert.ToDateTime(row["DATE1"]).ToString("dd-MM-yyyy");
                        string tdate = Convert.ToDateTime(row["DATE1"]).ToString("dd/MM/yyyy");
                        string ttime = ttime1;
                        double aggregatetph = Convert.ToDouble(0);
                        double bitumenkgmin = Convert.ToDouble(0); // means bitumen kg per batch
                        double fillerkgmin = Convert.ToDouble(0);
                        double bitumenkg = Convert.ToDouble(row["ASPHALT"]);

                        double F1_Inkg = Convert.ToDouble(row["AG1"]);
                        double F2_Inkg = Convert.ToDouble(row["AG2"]);
                        double F3_Inkg = Convert.ToDouble(row["AG3"]);
                        double F4_Inkg = Convert.ToDouble(row["AG4"]);
                        double Weight_KgPerBatch = Convert.ToDouble(row["TOTALWT"].ToString());
                        double fillerkg = Convert.ToDouble(row["FILLER1"]);

                        // changes added by annu 
                        double F1_InPer = 0;
                        double F2_InPer = 0;
                        double F3_InPer = 0;
                        double F4_InPer = 0;
                        double fillerper = 0;
                        double bitumenper = 0;
                        //
                        // int i = 0;
                        double mixtemp = 0;
                        double exhausttemp = 0;
                        double tank1 = 0;
                        double tank2 = 0;
                        string tipper = cmbtipper.SelectedItem.ToString();
                        // for (i = 0; i < dgvloaddetails.Rows.Count; i++)
                        // {
                        //dgvloaddetails.Rows[recno].Cells["Aggregateinkg"].Value
                        //Added by bhavesh

                        mixtemp = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_mixtemp"].Value);     //Convert.ToDouble(row["CH4_TEMP"]);
                        exhausttemp = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_exhausttemp"].Value);     //Convert.ToDouble(row["CH2_TEMP"]);
                        tank1 = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_tank1"].Value);     //Convert.ToDouble(row["CH5_TEMP"]);
                        tank2 = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_tank2"].Value);     //Convert.ToDouble(row["CH7_TEMP"]);    // 28/10/2024

                        //----------- Rouded off temperature -----------------------

                        mixtemp = Math.Round(mixtemp);
                        exhausttemp = Math.Round(exhausttemp);
                        tank1 = Math.Round(tank1);
                        tank2 = Math.Round(tank2);


                        //tipper = cmbtipper.SelectedItem.ToString();//row["TRUCK_NO"].ToString();
                        //
                        double moisture = Convert.ToDouble(0);
                        //double mixtemp = //Convert.ToDouble(row["CH4_TEMP"]);
                        //double exhausttemp ="" ;//Convert.ToDouble(row["CH2_TEMP"]);
                        //double tank1 ="" ;//Convert.ToDouble(row["CH5_TEMP"]);
                        //double tank2 = "0";//Convert.ToDouble(row["CH7_TEMP"]);
                        //string tipper = row["TRUCK_NO"].ToString();
                        double aggregateton = Convert.ToDouble(0);

                        //netmixa = netmixa + Weight_KgPerBatch;
                        double netmix = Weight_KgPerBatch / 1000;

                        int Batch_Duration_InSec = Convert.ToInt32(row["CYCLETIME"].ToString());
                        double Aggregate_Kg = Convert.ToDouble(row["BATCHWT"].ToString());

                        // As discussed with Govind sir - 01012024 - Dont take truck_count from client DB - Bhavesh
                        //int truck_count = Convert.ToInt32(row["TRUCK_COUNT"].ToString());
                        //int truck_count;
                        //
                        //try {  truck_count = Convert.ToInt32(clsFunctions.loadSinglevalue_HotMix("Select Max(Truck_Count)+1 from tblHotMixPlant")); } catch { truck_count = 1; }
                        //if (truck_count == 0) { truck_count = 1; }


                        //--------------------- Encrypt data to insert in PWD_DM -------------------------------------- 15/01/2025 : BhaveshT

                        if (clsFunctions.aliasName.Contains("PWD"))
                        {
                            try
                            {
                                //clsDM.E_interatorID = clsScadaFunctions.clsMethod.encrypts("2d5i6y");
                                //clsDM.E_workID = clsScadaFunctions.clsMethod.encrypts(lblworkkode.Text);
                                //clsDM.E_PlantId = clsScadaFunctions.clsMethod.encrypts(plantcode);

                                //clsDM.E_pwd_work = clsScadaFunctions.clsMethod.encrypts("Y");
                                //clsDM.E_typeOfwork = clsScadaFunctions.clsMethod.encrypts(Cmbmaterialtype.Text);
                                //clsDM.E_RFI = clsScadaFunctions.clsMethod.encrypts(txttipperno.Text);

                                //clsDM.E_exhust = clsScadaFunctions.clsMethod.encrypts(txtHotBinTemp.Text);
                                //clsDM.E_bitumen = clsScadaFunctions.clsMethod.encrypts(txtTank1.Text);
                                //clsDM.E_mix = clsScadaFunctions.clsMethod.encrypts(txtMixMatTemp.Text);

                                //clsDM.E_flag1 = "0";
                                //clsDM.E_IO = clsScadaFunctions.clsMethod.encrypts("255");
                                //clsDM.E_extr = clsScadaFunctions.clsMethod.encrypts(truck_count.ToString());
                                //clsDM.E_loadno = clsScadaFunctions.clsMethod.encrypts(loadno.ToString());
                                //clsDM.E_batchNo = clsScadaFunctions.clsMethod.encrypts(batchno.ToString());

                                //clsDM.E_date = clsScadaFunctions.clsMethod.encrypts(Convert.ToDateTime(tdate).ToString("yyyy-MM-dd"));
                                //clsDM.E_time = clsScadaFunctions.clsMethod.encrypts(Convert.ToDateTime(ttime).ToString("HH:mm:ss tt"));

                                //string dateTimeString = tdate + " " + ttime;
                                //clsDM.E_datetime = clsScadaFunctions.clsMethod.encrypts(DateTime.Parse(dateTimeString).ToString("yyyy-MM-dd HH:mm:ss"));

                                //clsDM.E_workType = "PWDDM";
                                //clsDM.E_InsertType = clsScadaFunctions.clsMethod.encrypts("A");

                                //clsDM.InsertEncryptedDataForDM();

                            }
                            catch (Exception ex)
                            {
                                clsFunctions_comman.ErrorLog("LoadDetails: at allocating variables for PWD-DM - " + ex.Message);
                            }
                        }

                        //added jobsite value for inserting in iblHotMixPlant

                        string insertquery = "INSERT INTO tblHotMixPlant (regno,plantcode,oprlat,oprlong,oprjurisdiction,oprdivision,oprworkname,"
                                + "tdate,ttime,aggregatetph,bitumenkgmin,fillerkgmin,bitumenper,fillerper,F1_InPer,F2_InPer,F3_InPer,F4_InPer,moisture,mixtemp,exhausttemp,"
                                + "tank1,tank2,tipper,aggregateton,bitumenkg,fillerkg,netmix,batchno,srno,imeino,exportstatus,viplupload,worktype,workcode,material,"
                                + "Batch_Duration_InSec,Weight_KgPerBatch,HB1_KgPerBatch,HB2_KgPerBatch,HB3_KgPerBatch,HB4_KgPerBatch,Aggregate_Kg,Truck_Count,jobsite,loadno,InsertType)"
                                + " values('" + regno + "','" + plantcode + "','" + oprlat + "','" + oprlong + "','" + oprjurisdiction + "','" + oprdivision + "','" + oprworkname + "','"
                                + tdate + "','" + ttime + "'," + aggregatetph + "," + bitumenkgmin + "," + fillerkgmin + "," + bitumenper + "," + fillerper + "," + F1_InPer + ","
                                + F2_InPer + "," + F3_InPer + "," + F4_InPer + "," + moisture + "," + mixtemp + "," + exhausttemp + "," + tank1 + "," + tank2 + ",'" + tipper + "',"
                                + aggregateton + "," + bitumenkg + "," + fillerkg + "," + netmix + "," + batchno + "," + localSrno + ",'" + deviceid + "','" + exportstatus + "',"
                                + viplupload + ",0,'" + lblworkkode.Text + "','" + Cmbmaterialtype.Text + "'," + Batch_Duration_InSec + "," + Weight_KgPerBatch + "," + F1_Inkg + "," + F2_Inkg + "," + F3_Inkg + "," + F4_Inkg + "," + Aggregate_Kg + "," + truck_count + ",'" + cmbJobSite.Text + "'," + loadno + ",'A')";

                        clsFunctions.AdoData(insertquery);

                        // i++;
                        string tempquery = insertquery.Replace("tblHotMixPlant", "bkp");
                        clsFunctions.AdoData(tempquery);
                        // }
                        preloadno = loadno;

                        //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + update_date + "# AND TIME1 = #" + ttime + "#; ";

                        //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";   // Previous commented by BT
                        string update_query;
                        //if (clsFunctions.protoolFlag == false)
                        update_query = "UPDATE Production SET test='1' WHERE LOAD_NO<>0 AND test='0' AND DATE1 =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";       //BhaveshT 01012024
                        //else (clsFunctions.protoolFlag == false)
                        //    update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";   // Previous commented by BT

                        clsFunctions.UpdateProduction(update_query);
                        enddate = update_date;
                        endtime = ttime;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clsFunctions.ErrorLog("Exception in SaveDataToHotMixScada: " + ex.Message);
                }
            }
        }

        //--------------------------------------------------------------------------------------------

        private void SaveDataToHotMixScadaForAsphaltMDB(DataTable dt1)       // for Asphalt MDB
        {
            //adding if condition for cheking whether dt1 datatable contains the data or not
            if (dt1.Rows.Count > 0)
            {
                try
                {
                    
                    string oprlat = "NA";                       // string oprlat = "";     //added default values by dinesh
                    string oprlong = "NA";                      //string oprlong = "";    //added default values by dinesh
                    string oprjurisdiction = "Pune";   // string oprjurisdiction = "";   //added default values by dinesh
                    string oprdivision = "Pune";                //string oprdivision = "";            //added default values by dinesh

                    string exportstatus = "Y";
                    int viplupload = 0;
                    int batchno = 0;
                    int loadno = 0;
                    int truck_count = 0;
                    int preloadno = 0;
                    //int srno = 0;

                    // 10/01/2024 - BhaveshT
                    string oprworkname = cmbworkname.Text;
                    string regno = txtConCode.Text;
                    string plantcode = txtPlantCode.Text;
                    string deviceid = clsFunctions.activeDeviceID;

                    //------------------------------------------
                    // 01/01/2024 : Bhavesh - Added code for loadno & truck_count as per BatchDataHTML form

                    loadno = Convert.ToInt32(clsFunctions.GetMaxId("SELECT SUM(batchendflag)+1 FROM tblHotMixPlant"));//where tdate =#" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "# "));

                    truck_count = loadno;
                    //------------------------------------------

                    int localSrno = Convert.ToInt32(srno) - dgvloaddetails.Rows.Count;

                    int i = -1;
                    foreach (DataRow row in dt1.Rows)
                    {
                        i++;
                        string ttime1 = Convert.ToDateTime(row["Tim"]).ToString("HH:mm:ss");
                        batchno = Convert.ToInt32(row["BatchNo"].ToString());
                        localSrno++;

                        // As discussed with Govind sir - 01012024 - Dont take loadno from client DB
                        
                        L_LoadNo = loadno;        // for endBatch - BhaveshT : 23/02/2024
                        L_BatchNo = batchno;      // for endBatch - BhaveshT

                        string update_date = Convert.ToDateTime(row["Dat"]).ToString("dd-MM-yyyy");
                        string tdate = Convert.ToDateTime(row["Dat"]).ToString("dd/MM/yyyy");
                        string ttime = ttime1;
                        double aggregatetph = Convert.ToDouble(0);
                        double bitumenkgmin = Convert.ToDouble(0); // means bitumen kg per batch
                        double fillerkgmin = Convert.ToDouble(0);
                        double bitumenkg = Convert.ToDouble(row["Asphalt_DACT"]);

                        double F1_Inkg = Convert.ToDouble(row["AG1_DACT"]);
                        double F2_Inkg = Convert.ToDouble(row["AG2_DACT"]);
                        double F3_Inkg = Convert.ToDouble(row["AG3_DACT"]);
                        double F4_Inkg = Convert.ToDouble(row["AG4_DACT"]);

                        double Weight_KgPerBatch = Convert.ToDouble(row["AggregateWt_DACT"].ToString());
                        double fillerkg = Convert.ToDouble(row["Filler1_DACT"]);

                        // changes added by annu 
                        double F1_InPer = 0;
                        double F2_InPer = 0;
                        double F3_InPer = 0;
                        double F4_InPer = 0;
                        double fillerper = 0;
                        double bitumenper = 0;
                        //
                        // int i = 0;
                        double mixtemp = 0;
                        double exhausttemp = 0;
                        double tank1 = 0;
                        double tank2 = 0;
                        string tipper = cmbtipper.SelectedItem.ToString();

                        mixtemp = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_mixtemp"].Value);             //Convert.ToDouble(row["CH4_TEMP"]);
                        exhausttemp = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_exhausttemp"].Value);     //Convert.ToDouble(row["CH2_TEMP"]);
                        tank1 = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_tank1"].Value);                 //Convert.ToDouble(row["CH5_TEMP"]);
                        tank2 = 0;

                        double moisture = Convert.ToDouble(0);
                        double aggregateton = Convert.ToDouble(0);

                        //netmixa = netmixa + Weight_KgPerBatch;
                        double netmix = Weight_KgPerBatch / 1000;

                        int Batch_Duration_InSec = 0; //Convert.ToInt32(row["CYCLETIME"].ToString());
                        double Aggregate_Kg = Convert.ToDouble(row["AggregateWt_DACT"].ToString());

                        // As discussed with Govind sir - 01012024 - Dont take truck_count from client DB - Bhavesh
                        //int truck_count = Convert.ToInt32(row["TRUCK_COUNT"].ToString());
                        //int truck_count;
                        //
                        //try {  truck_count = Convert.ToInt32(clsFunctions.loadSinglevalue_HotMix("Select Max(Truck_Count)+1 from tblHotMixPlant")); } catch { truck_count = 1; }
                        //if (truck_count == 0) { truck_count = 1; }

                        //added jobsite value for inserting in iblHotMixPlant

                        string insertquery = "INSERT INTO tblHotMixPlant (regno, plantcode, oprlat, oprlong, oprjurisdiction, oprdivision, oprworkname,"
                                + "tdate, ttime, aggregatetph, bitumenkgmin, fillerkgmin, bitumenper, fillerper," 
                                + "F1_InPer, F2_InPer, F3_InPer, F4_InPer, moisture, mixtemp, exhausttemp,"
                                + "tank1, tank2, tipper, aggregateton, bitumenkg, fillerkg, netmix, batchno, srno," 
                                + "imeino, exportstatus, viplupload, worktype, workcode, material,"
                                + "Batch_Duration_InSec, Weight_KgPerBatch, HB1_KgPerBatch, HB2_KgPerBatch, HB3_KgPerBatch, HB4_KgPerBatch," 
                                + "Aggregate_Kg, Truck_Count, jobsite, loadno, InsertType)"
                                
                                + " values('" + regno + "','" + plantcode + "','" + oprlat + "','" + oprlong + "','" + oprjurisdiction + "','" + oprdivision + "','" + oprworkname + "','"
                                + tdate + "','" + ttime + "'," + aggregatetph + "," + bitumenkgmin + "," + fillerkgmin + "," + bitumenper + "," + fillerper + "," 
                                + F1_InPer + "," + F2_InPer + "," + F3_InPer + "," + F4_InPer + "," + moisture + "," + mixtemp + "," + exhausttemp + "," 
                                + tank1 + "," + tank2 + ",'" + tipper + "'," + aggregateton + "," + bitumenkg + "," + fillerkg + "," + netmix + "," + batchno + "," + localSrno + ",'" 
                                + deviceid + "','" + exportstatus + "'," + viplupload + ",0,'" + lblworkkode.Text + "','" + Cmbmaterialtype.Text + "'," 
                                + Batch_Duration_InSec + "," + Weight_KgPerBatch + "," + F1_Inkg + "," + F2_Inkg + "," + F3_Inkg + "," + F4_Inkg + "," 
                                + Aggregate_Kg + "," + truck_count + ",'" + cmbJobSite.Text + "'," + loadno + ",'A')";


                        clsFunctions.AdoData(insertquery);

                        string tempquery = insertquery.Replace("tblHotMixPlant", "bkp");
                        clsFunctions.AdoData(tempquery);

                        preloadno = loadno;
                        string update_query;
                        
                        //if (clsFunctions.protoolFlag == false)
                        update_query = "UPDATE Production_Report SET Filler2_SET ='1' WHERE LOAD_NO<>0 AND Filler2_SET ='0' AND Dat =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";       //BhaveshT 01012024
                        
                        clsFunctions.UpdateProductionReport(update_query);
                        enddate = update_date;
                        endtime = ttime;
                    }
                }
                catch (Exception ex)
                {
                    clsFunctions.ErrorLog("Exception in SaveDataToHotMixScadaFromAsphaltMDB: " + ex.Message);
                }
            }
        }

        //--------------------------------------------------------------------------------------------


        //Commented by BT on 19012024
        //private void SaveDataToHotMixScada(DataTable dt1)       // for Protool
        //{
        //    //adding if condition for cheking whether dt1 datatable contains the data or not
        //    if (dt1.Rows.Count > 0)
        //    {
        //        try
        //        {
        //            string regno = ""; //string regno = "99";
        //            string plantcode = ""; //string plantcode = "";
        //            string oprlat = "18.6720225";// string oprlat = "";     //added default values by dinesh
        //            string oprlong = "73.8138366"; //string oprlong = "";    //added default values by dinesh
        //            string oprjurisdiction = "Pune";// string oprjurisdiction = "";   //added default values by dinesh
        //            string oprdivision = "Pune"; //string oprdivision = "";            //added default values by dinesh
        //            string oprworkname = cmbworkname.Text;
        //            //added by dinesh
        //            // string strSiteName = cmbJobSite.Text;

        //            string deviceid = "";
        //            string exportstatus = "Y";
        //            int viplupload = 0;
        //            double netmixa = 0;
        //            int batchno = 0;
        //            int loadno = 0;
        //            int truck_count = 0;

        //            int preloadno = 0;
        //            int srno = 0;
        //            DataTable plntdt = clsFunctions.fillDatatable_setup("select * from PlantSetup");
        //            if (plntdt.Rows.Count > 0)
        //            {
        //                DataRow r = plntdt.Rows[0];
        //                //regno = r["regno"].ToString();
        //                //plantcode = r["plantcode"].ToString();
        //                //imeino = r["imeino"].ToString();
        //                //regno = r["ContractorCode"].ToString();
        //                plantcode = r["PlantCode"].ToString();
        //                deviceid = r["DeviceID"].ToString();
        //            }
        //            //DataTable workdt = clsFunctions.fillDatatable("select * from WorkOrder");
        //            //if (workdt.Rows.Count > 0)
        //            //{
        //            //    DataRow r = workdt.Rows[0];
        //            //    oprworkname = r["workname"].ToString();
        //            //    //commented by dinesh
        //            //    // oprlat = r["oprlatitude"].ToString();
        //            //    //oprlong = r["oprlongitude"].ToString();
        //            //    // oprjurisdiction = r["oprjurisdiction"].ToString();
        //            //    // oprdivision = r["oprdivision"].ToString();
        //            //
        //            //}

        //            // 10/01/2024 - BhaveshT
        //            oprworkname = cmbworkname.Text;
        //            regno = lbConCode.Text;

        //            //------------------------------------------
        //            // 01/01/2024 : Bhavesh - Added code for loadno & truck_count as per BatchDataHTML form

        //            //loadno = clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant where tdate = #" + DateTime.Now.ToString("yyyy/MM/dd") + "#; ");
        //            loadno = clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant");
        //            truck_count = loadno;
        //            //------------------------------------------

        //            int i = -1;
        //            foreach (DataRow row in dt1.Rows)
        //            {
        //                i++;

        //                string ttime1 = Convert.ToDateTime(row["TIME1"]).ToString("HH:mm:ss");
        //                srno = Convert.ToInt32(row["BATCH_NO"].ToString());

        //                // As discussed with Govind sir - 01012024 - Dont take loadno from client DB
        //                //loadno = Convert.ToInt32(row["LOAD_NO"].ToString());


        //                //if (preloadno != loadno)
        //                //{
        //                //    batchno = clsFunctions.GetMaxId("select Max(batchno)+1 from tblHotMixPlant");
        //                //}
        //                batchno = srno;//Convert.ToInt32(row["BATCH_NO"].ToString());
        //                string update_date = Convert.ToDateTime(row["DATE1"]).ToString("dd-MM-yyyy");
        //                string tdate = Convert.ToDateTime(row["DATE1"]).ToString("dd/MM/yyyy");
        //                string ttime = ttime1;
        //                double aggregatetph = Convert.ToDouble(0);
        //                double bitumenkgmin = Convert.ToDouble(0); // means bitumen kg per batch
        //                double fillerkgmin = Convert.ToDouble(0);
        //                double bitumenkg = Convert.ToDouble(row["ASPHALT"]);


        //                double F1_Inkg = Convert.ToDouble(row["AG1"]);
        //                double F2_Inkg = Convert.ToDouble(row["AG2"]);
        //                double F3_Inkg = Convert.ToDouble(row["AG3"]);
        //                double F4_Inkg = Convert.ToDouble(row["AG4"]);
        //                double Weight_KgPerBatch = Convert.ToDouble(row["TOTALWT"].ToString());
        //                double fillerkg = Convert.ToDouble(row["FILLER1"]);



        //                // changes added by annu 
        //                double F1_InPer = 0;
        //                double F2_InPer = 0;
        //                double F3_InPer = 0;
        //                double F4_InPer = 0;
        //                double fillerper = 0;
        //                double bitumenper = 0;
        //                //
        //                // int i = 0;
        //                double mixtemp = 0;
        //                double exhausttemp = 0;
        //                double tank1 = 0;
        //                double tank2 = 0;
        //                string tipper = cmbtipper.SelectedItem.ToString();
        //                // for (i = 0; i < dgvloaddetails.Rows.Count; i++)
        //                // {
        //                //dgvloaddetails.Rows[recno].Cells["Aggregateinkg"].Value
        //                //Added by bhavesh

        //                mixtemp = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_mixtemp"].Value);//Convert.ToDouble(row["CH4_TEMP"]);
        //                exhausttemp = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_exhausttemp"].Value);//Convert.ToDouble(row["CH2_TEMP"]);
        //                tank1 = Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_tank1"].Value);//Convert.ToDouble(row["CH5_TEMP"]);
        //                tank2 = 0;//Convert.ToDouble(row["CH7_TEMP"]);
        //                          //   tipper = cmbtipper.SelectedItem.ToString();//row["TRUCK_NO"].ToString();

        //                //
        //                double moisture = Convert.ToDouble(0);
        //                //double mixtemp = //Convert.ToDouble(row["CH4_TEMP"]);
        //                //double exhausttemp ="" ;//Convert.ToDouble(row["CH2_TEMP"]);
        //                //double tank1 ="" ;//Convert.ToDouble(row["CH5_TEMP"]);
        //                //double tank2 = "0";//Convert.ToDouble(row["CH7_TEMP"]);
        //                //string tipper = row["TRUCK_NO"].ToString();
        //                double aggregateton = Convert.ToDouble(0);


        //                // netmixa = netmixa + Weight_KgPerBatch;
        //                double netmix = Weight_KgPerBatch / 1000;

        //                int Batch_Duration_InSec = Convert.ToInt32(row["CYCLETIME"].ToString());
        //                double Aggregate_Kg = Convert.ToDouble(row["BATCHWT"].ToString());

        //                // As discussed with Govind sir - 01012024 - Dont take truck_count from client DB - Bhavesh
        //                //int truck_count = Convert.ToInt32(row["TRUCK_COUNT"].ToString());

        //                //int truck_count;
        //                //
        //                //try {  truck_count = Convert.ToInt32(clsFunctions.loadSinglevalue_HotMix("Select Max(Truck_Count)+1 from tblHotMixPlant")); } catch { truck_count = 1; }
        //                //if (truck_count == 0) { truck_count = 1; }

        //                //added jobsite value for inserting in iblHotMixPlant

        //                string insertquery = "Insert into tblHotMixPlant(regno,plantcode,oprlat,oprlong,oprjurisdiction,oprdivision,oprworkname,"
        //                        + "tdate,ttime,aggregatetph,bitumenkgmin,fillerkgmin,bitumenper,fillerper,F1_InPer,F2_InPer,F3_InPer,F4_InPer,moisture,mixtemp,exhausttemp,"
        //                        + "tank1,tank2,tipper,aggregateton,bitumenkg,fillerkg,netmix,batchno,srno,imeino,exportstatus,viplupload,worktype,workcode,material,"
        //                        + "Batch_Duration_InSec,Weight_KgPerBatch,HB1_KgPerBatch,HB2_KgPerBatch,HB3_KgPerBatch,HB4_KgPerBatch,Aggregate_Kg,Truck_Count,jobsite,loadno,InsertType)"
        //                        + " values('" + regno + "','" + plantcode + "','" + oprlat + "','" + oprlong + "','" + oprjurisdiction + "','" + oprdivision + "','" + oprworkname + "','"
        //                        + tdate + "','" + ttime + "'," + aggregatetph + "," + bitumenkgmin + "," + fillerkgmin + "," + bitumenper + "," + fillerper + "," + F1_InPer + ","
        //                        + F2_InPer + "," + F3_InPer + "," + F4_InPer + "," + moisture + "," + mixtemp + "," + exhausttemp + "," + tank1 + "," + tank2 + ",'" + tipper + "',"
        //                        + aggregateton + "," + bitumenkg + "," + fillerkg + "," + netmix + "," + batchno + "," + srno + ",'" + deviceid + "','" + exportstatus + "',"
        //                        + viplupload + ",0,'" + lblworkkode.Text + "','" + Cmbmaterialtype.Text + "'," + Batch_Duration_InSec + "," + Weight_KgPerBatch + "," + F1_Inkg + "," + F2_Inkg + "," + F3_Inkg + "," + F4_Inkg + "," + Aggregate_Kg + "," + truck_count + ",'" + cmbJobSite.Text + "'," + loadno + ",'A')";



        //                clsFunctions.AdoData(insertquery);



        //                // i++;
        //                string tempquery = insertquery.Replace("tblHotMixPlant", "bkp");
        //                clsFunctions.AdoData(tempquery);
        //                // }
        //                preloadno = loadno;

        //                // string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + update_date + "# AND TIME1 = #" + ttime + "#; ";

        //                //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";   // Previous commented by BT
        //                string update_query;
        //                //if (clsFunctions.protoolFlag == false)
        //                    update_query = "UPDATE Production SET test='1' WHERE LOAD_NO<>0 AND test='0' AND DATE1 =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";       //BhaveshT 01012024
        //                //else (clsFunctions.protoolFlag == false)
        //                //    update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";   // Previous commented by BT

        //                clsFunctions.UpdateProduction(update_query);
        //                enddate = update_date;
        //                endtime = ttime;

        //            }


        //        }
        //        catch (Exception ex)
        //        {
        //            //  MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            //clsFunctions.ErrorLog(" SaveDataToHotMixScada " + ex.Message);

        //        }
        //    }
        //}

        //--------------------------------------------------------------------------------------------

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (btnstart.BackColor != Color.Gray)
            {
                if (clsFunctions.activeDesciption.ToString() == "Protool")
                {
                    GetRecordsFromProtool();
                }

                if (clsFunctions.activeDesciption.Contains("Asphalt"))
                {
                    GetRecordsFromAsphaltMDB();
                }
            }
        }

        //--------------------------------------------------------------------------------------------

        string errStatus = "N";

        private void button1_Click(object sender, EventArgs e)      // STOP Button
        {
            //added by dinesh
            //stopping background worker for live data update
           // StopLiveDataUpload();
            try //Added by BK for Indicator of Error Percent.
            {
                //timer1.Enabled = false;
                //if (aTimer != null)
                //{
                //    aTimer.Stop();
                //    aTimer.AutoReset = false;
                //    aTimer.Enabled = false;
                //}

                ////--------------

                clsFunctions.ErrorLog("[INFO] LoadDetails - STOP Button Clicked.");
            
                //--------------

                //clsBTdata objBT = new clsBTdata();
                //clsSMSAlerts clsSMS = new clsSMSAlerts();
                //errStatus = objBT.checkErrPer(Cmbmaterialtype.Text, dgvloaddetails);

                //if (Production_Error == "N")
                //{
                //    MessageBox.Show("Production Error acceptable");
                //    return;
                //}

                //if (clsErr.Production_Error == "Y")
                //{
                //    MessageBox.Show("Production Error have issue");
                //    clsSMS.SendErrorSMSAlert(txtworkcode.Text, cmbdocketno.Text, txtbatchdate.Text);
                //    return;
                //}
                //if (errStatus.ToUpper() == "Y")
                //{
                //    lblerrperindecator.Text = "Error percentage is Out of Range ";
                //    //MessageBox.Show("Error percentage is Out of Range ","UNIPRO",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //    clsSMS.SendErrorSMSAlert(lblwid.Text, truck_count.ToString(), txtdate.Text, clsErr);
                //}
                //else
                //{
                //    lblerrperindecator.Text = "";
                //}
            }
            catch 
            {
            
            }
            Stop_backgroundWorker = true;
            cmbworkname.Enabled = true;
            cmbtipper.Enabled = true;
            Cmbmaterialtype.Enabled = true;
            button1.Enabled = false;
            btnstart.Enabled = true;
            cmbJobSite.Enabled = true;

            //------------------------------------------------------

            if (clsFunctions.activeDesciption != "Linnhoff")       //if (clsFunctions.PlantType != "Linnhoff")
            {
                //aTimer.Interval = 1;
                try
                {
                    //if (aTimer != null)
                    //{
                    //    aTimer.Stop();
                    //    aTimer.AutoReset = false;
                    //    aTimer.Enabled = false;
                    //}

                    //if (dgvloaddetails.Rows.Count != 0)
                    if(flagForLoadEnd)
                    {
                        flagForLoadEnd = false;
                        InsertBatchEndRow(L_LoadNo, enddate, endtime, L_BatchNo);

                        //  15/01/2025 : BhaveshT -------------------

                        if (clsFunctions.aliasName.Contains("PWD"))
                        {
                            //clsDM.UpdateBatchEndFlag(L_LoadNo, L_BatchNo, Cmbmaterialtype.Text);
                        }

                        //--------------------------------------

                        //string update_query = "UPDATE tblHotMixPlant SET batchendflag=1 WHERE loadno<>0 AND tdate=#" + Convert.ToDateTime(enddate).ToString("MM/dd/yyyy") + "# AND ttime = #" + endtime + "#; ";
                        //string update_query = "UPDATE tblHotMixPlant SET batchendflag=1 WHERE loadno<>0 AND tdate=#" + Convert.ToDateTime(enddate).ToString("yyyy/MM/dd") + "# AND ttime = #" + endtime + "#; ";

                        //  clsFunctions.UpdateProduction(update_query);
                        //clsFunctions.AdoData(update_query);
                        //if (lblerrperindecator.Text != "")
                        //{
                        //    string errperquery = "UPDATE tblHotMixPlant SET Production_Error = 'Y' WHERE loadno<>0 AND tdate=#" + Convert.ToDateTime(enddate).ToString("yyyy/MM/dd") + "# AND ttime = #" + endtime + "#; ";
                        //    clsFunctions.AdoData(errperquery);
                        //}
                    }
                    dgvloaddetails.Rows.Clear();
                    
                }
                catch { }
            }

            //------------------------------------------------------

            //else if (clsFunctions.activeDesciption == "Linnhoff")          //else if (clsFunctions.PlantType == "Linnhoff")
            //{
            //    try
            //    {
            //        LinnhoffTimer.Stop();
            //        LinnhoffTimer.AutoReset = false;
            //        LinnhoffTimer.Enabled = false;
            //    }
            //    catch { }
            //}
            btnstart.BackColor = Color.Gray;
            button1.BackColor = Color.Green;
            btnstart.Enabled = true;
            //button1.Enabled = false;
            //FileUpdatePointer((FileNameDateFormat + ".csv"), Convert.ToDouble(txtCount.Text));

            //------------------------------------------------------

            // int batchenflag = dgvloaddetails.Rows
            //if (dgvloaddetails.Rows.Count != 0)
            //{
            //    InsertBatchEndRow(L_LoadNo, enddate, txtTime.Text, L_BatchNo);

            //    //  15/01/2025 : BhaveshT -------------------

            //    if (clsFunctions.aliasName.Contains("PWD"))
            //    {
            //        //clsDM.UpdateBatchEndFlag(L_LoadNo, L_BatchNo, Cmbmaterialtype.Text);
            //    }

            //    //--------------------------------------

            //    //string update_query = "UPDATE tblHotMixPlant SET batchendflag=1 WHERE loadno<>0 AND batchno = " + lastbatchno + " AND tdate=#" + Convert.ToDateTime(enddate).ToString("MM/dd/yyyy") + "# AND ttime = #" + txtTime.Text + "#; ";
            //    //  clsFunctions.UpdateProduction(update_query);
            //    //clsFunctions.ErrorLog("endflag query "+update_query);
            //    //clsFunctions.AdoData(update_query);
            //    if (lblerrperindecator.Text != ""|| errStatus.ToUpper()=="Y")
            //    {
            //        string errperquery = "UPDATE tblHotMixPlant SET Production_Error='Y'WHERE loadno<>0 AND batchno = " + lastbatchno + " AND tdate=#" + Convert.ToDateTime(enddate).ToString("MM/dd/yyyy") + "# AND ttime = #" + txtTime.Text + "#;";
            //        clsFunctions.AdoData(errperquery);
            //    }
            //    else
            //    {
            //        string errperquery = "UPDATE tblHotMixPlant SET Production_Error='N'WHERE loadno<>0 AND batchno = " + lastbatchno + " AND tdate=#" + Convert.ToDateTime(enddate).ToString("MM/dd/yyyy") + "# AND ttime = #" + txtTime.Text + "#;";
            //        clsFunctions.AdoData(errperquery);
            //    }
            //}
            dgvloaddetails.Rows.Clear();
            lblerrperindecator.Text = "";
        }

        //--------------------------------------------------------------------------------------------

        private System.Timers.Timer aTimer;

        private void setTimer()
        {
            try
            {
                Indicator.BackColor = Color.Green;

                aTimer = new System.Timers.Timer();
                aTimer.Interval = 3000;

                // H2000up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;

                // Have the timer fire repeated events (true is the default)
                aTimer.AutoReset = true;

                // Start the timer
                aTimer.Enabled = true;

                Indicator.BackColor = Color.Gray;
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog(ex.Message + "VIPL Get Record From XLS");
                // MessageBox.Show(ex.Message, "VIPL Get Record From XLS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------------

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                //iCount = Convert.ToDouble(txtCount.Text);
                //setCountXLSLastRecord("1");

                aTimer.Enabled = false;

                if (button1.Enabled == true&&btnstart.Enabled==false)
                {
                    getFileFrom();
                    aTimer.Enabled = true;
                    //RowPointer = RowPointer + 1;
                    //setCountXLS(RowPointer.ToString());
                }
                else { aTimer.Close(); }
                //aTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " | LoadDetails - OnTimedEvent", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions.ErrorLog("[Exception] LoadDetails - OnTimedEvent : " + ex.Message );
            }
        }

        //--------------------------------------------------------------------------------------------

        //------------------------- 17/01/2024 - Added by BhaveshT
        private void InitializeRefreshTimer()
        {
            refreshBtnTimer = new System.Windows.Forms.Timer();
            refreshBtnTimer.Interval = 10000; // 10 seconds
            refreshBtnTimer.Tick += refreshBtnTimer_Tick;
        }

        //--------------------------------------------------------------------------------------------

        private void OnLinnhoffTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                //iCount = Convert.ToDouble(txtCount.Text);
                //setCountXLSLastRecord("1");

                LinnhoffTimer.Enabled = false;

                if (button1.Enabled == true)
                {
                    getLinnhoffdata();
                }
                LinnhoffTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL OnLinnhoffTimeEvene", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------------

        //---------------- Added by BK on 02012024 : solved cycletime issue -------------------

        string temptimeforcycle;

        //delegate void SetTextCallback(string text2, string text3, string text4);
        delegate void SetTextCallbacklinhof();
        private void getLinnhoffdata()
        {
            if (txtdate.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    //getExcelFile(XLSFile, xlsFileName, Dir);
                    getLinnhoffdata();
                });
                //return;
            }
            else
                if (txtdate.Text != null)
                {
                    if (txtTime.Text != "")
                    {
                        txtBatchNo.Text = clsFunctions.loadSingleValue("Select Max(batchno)+1 from tblhotmixplant where tdate = #" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "#");
                        //string batchtime = (Convert.ToDateTime(txtTime.Text).AddSeconds(1)).ToString("HH:mm:ss");
                        //clsBTdata obj1 = new clsBTdata("Select Top 1 * from Report_150416 where (Timecol)>'" + Convert.ToDateTime(textBox1.Text).ToString("yyyy-MM-dd") + " " + batchtime + "' and Unique_No<>0 order by Unique_No");
                        clsBTdata obj1 = new clsBTdata("Select Top 1 * from Report_150416 where Unique_No>='" + txtBatchNo.Text + "' and Unique_No<>0 order by Unique_No");

                    }
                    else
                    {
                        clsBTdata obj1 = new clsBTdata("Select Top 1 * from Report_150416 where (LocalCol)>='" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + " " + time.Text + "' and Unique_No<>0 order by Unique_No");
                    }//DataTable dt1 = clsBTdata.data;
                }

            Thread.Sleep(300);
            foreach (DataRow dr in clsBTdata.data.Rows)
            {
                string strDate;

                strDate = Convert.ToDateTime(dr["LocalCol"]).ToString("dd/MM/yyyy");
                DateTime conv = Convert.ToDateTime(strDate.ToString());
                //string m = conv.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        getLinnhoffdata();
                    });
                    return;
                }
                if (txtdate.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        getLinnhoffdata();
                    });
                    return;
                }
                else
                {

                }
                if (txtTime.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        getLinnhoffdata();
                    });
                    return;
                }
                else
                {
                    txtTime.Text = Convert.ToDateTime(dr["LocalCol"]).ToString("HH:mm:ss"); //Convert.ToDateTime(time.Text).AddSeconds(1).ToString("HH:mm:ss");
                }
                textBox1.Text = Convert.ToDateTime(dr["LocalCol"]).ToString("dd/MM/yyyy");

                // txtTime.Text = time.Text;
                batchtime = txtTime.Text;
                long tempbno1 = clsFunctions.GetMaxLongId("Select Max(batchno) from tblhotmixplant");

                string tempbatchno = tempbno1.ToString();
                txtBatchNo.Text = dr["Unique_No"].ToString();
                //if (!IsExist(Convert.ToDouble(txtBatchNo.Text), txtdate.Text, txtTime.Text)) { continue; }
                if (temptimeforcycle == null) txtBatchDuration.Text = "0";
                else
                    //txtBatchDuration.Text = (Convert.ToDouble(dr["MSecCol"])).ToString(); //Comment by BK because MicroSecond inserted here.
                    txtBatchDuration.Text = (DateTime.Parse(txtTime.Text).Subtract(DateTime.Parse(temptimeforcycle))).Duration().TotalSeconds.ToString();
                
                temptimeforcycle = txtTime.Text;

                txtHB1.Text = dr["Agg_K1"].ToString();
                txtHB2.Text = dr["Agg_K2"].ToString();

                txtHB3.Text = dr["Agg_K3"].ToString();
                txtHB4.Text = dr["Agg_K4"].ToString();

                //txtHB1Per.Text = dr["f5"].ToString();
                //txtHB2Per.Text = dr["f7"].ToString();

                //txtHB3Per.Text = dr["f9"].ToString();
                //txtHB4Per.Text = dr["f11"].ToString();

                //txtHB5.Text = dr["f12"].ToString();
                //txtHB6.Text = dr["f14"].ToString();

                //txtHB5Per.Text = dr["f13"].ToString();
                //txtHB6Per.Text = dr["f15"].ToString();

                txtFiller.Text = Convert.ToDouble(Convert.ToDouble(dr["Fill_F1"].ToString()) + Convert.ToDouble(dr["Fill_F2"].ToString())).ToString();
                //txtFillerPer.Text = dr["f17"].ToString();

                //txtRAP.Text = dr["f18"].ToString();
                //txtRAPPer.Text = dr["f19"].ToString();

                txtAsphalt.Text = ((Convert.ToDouble(dr["Bitu_B1"]) + Convert.ToDouble(dr["Bitu_B2"]))).ToString(); //Update on 23Dec23

                //txtAsphaltPer.Text = dr["f21"].ToString();

                txtMixMatTemp.Text = dr["Prod_Temp"].ToString();
                //txtMixMatTemp.Text = dr["f30"].ToString();
                txtSmoke.Text = dr["Sand_Temp"].ToString();
                txtHotBinTemp.Text = "0"; dr["Bitumen_Temp"].ToString();
                txtTank1.Text = dr["Bitumen_Temp"].ToString();

                txtBatchSize.Text = dr["Total"].ToString();
                //txtTruckNo.Text = xlRange.Cells[i, 33].Value2().ToString();
                //txtTruckCount.Text = txtCount.Text;

                txtNet.Text = dr["Total"].ToString();

                TempValidatorAlert();

                if (textBox1.Text != "" && time.Text != "" && txtBatchNo.Text != "")
                {
                    bool CheckDataExistInTable = IsExist(Convert.ToDouble(txtBatchNo.Text), textBox1.Text, txtTime.Text);

                    if (CheckDataExistInTable == true)
                    {
                        //if (xlsReadStatus == true)
                        {
                            GetRecordsFromXLS(true);
                            //aTimer.Enabled = true;
                        }
                    }
                    else
                    {
                        //MessageBox.Show(" - Linhoff - CheckDataExistInTable not true");
                        clsFunctions_comman.ErrorLog(" - Linhoff - CheckDataExistInTable not true");
                    }
                }
                else
                {
                    MessageBox.Show("Error - Linhoff - date, time or BatchNo is empty");
                    clsFunctions_comman.ErrorLog("Error - Linhoff - date, time or BatchNo is empty");
                }
                // goto ExitLoop;
            }
            // DataTable dt = clsSqlFunctions.FillDataTable("Select * from Report_150416 where datetime(Timecol)<='"+txtdate.Text+" "+time.Text+"' order by Unique_No");
        }

        //--------------------------------------------------------------------------------------------

        //---------------- Commented by BK on 02012024 : for cycletime issue -------------------
        ////delegate void SetTextCallback(string text2, string text3, string text4);
        //delegate void SetTextCallbacklinhof();
        //private void getLinnhoffdata()
        //{
        //    if (txtdate.InvokeRequired)
        //    {
        //        this.Invoke((MethodInvoker)delegate
        //        {
        //            //getExcelFile(XLSFile, xlsFileName, Dir);
        //            getLinnhoffdata();
        //        });
        //        //return;

        //    }
        //    else
        //    if (txtdate.Text != null)
        //    {
        //        if (txtTime.Text!="")
        //        {
        //            txtBatchNo.Text = clsFunctions.loadSingleValue("Select Max(batchno)+1 from tblhotmixplant where tdate = #"+Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd")+"#");
        //            //string batchtime = (Convert.ToDateTime(txtTime.Text).AddSeconds(1)).ToString("HH:mm:ss");
        //            //clsBTdata obj1 = new clsBTdata("Select Top 1 * from Report_150416 where (Timecol)>'" + Convert.ToDateTime(textBox1.Text).ToString("yyyy-MM-dd") + " " + batchtime + "' and Unique_No<>0 order by Unique_No");
        //            clsBTdata obj1 = new clsBTdata("Select Top 1 * from Report_150416 where Unique_No>='" + txtBatchNo.Text+"' and Unique_No<>0 order by Unique_No");

        //        }
        //        else
        //        {
        //            clsBTdata obj1 = new clsBTdata("Select Top 1 * from Report_150416 where (LocalCol)>='" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + " " + time.Text + "' and Unique_No<>0 order by Unique_No");
        //        }//DataTable dt1 = clsBTdata.data;
        //    }
        //    Thread.Sleep(300);
        //    foreach (DataRow dr in clsBTdata.data.Rows)
        //    {
        //         string strDate;

        //            strDate = Convert.ToDateTime(dr["LocalCol"]).ToString("dd/MM/yyyy");
        //             DateTime conv = Convert.ToDateTime(strDate.ToString());
        //             //string m = conv.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
        //             if (this.InvokeRequired)
        //            {
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    getLinnhoffdata();
        //                });
        //                return;
        //            }
        //            if (txtdate.InvokeRequired)
        //            {
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    getLinnhoffdata();
        //                });
        //                return;
        //            }
        //            else
        //            {

        //            }
        //            if (txtTime.InvokeRequired)
        //            {
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    getLinnhoffdata();
        //                });
        //                return;

        //            }
        //            else
        //            {
        //            txtTime.Text = Convert.ToDateTime(dr["LocalCol"]).ToString("HH:mm:ss"); //Convert.ToDateTime(time.Text).AddSeconds(1).ToString("HH:mm:ss");
        //            }
        //            textBox1.Text= Convert.ToDateTime(dr["LocalCol"]).ToString("dd/MM/yyyy");
        //        // txtTime.Text = time.Text;
        //        batchtime = txtTime.Text;
        //            long tempbno1 = clsFunctions.GetMaxLongId("Select Max(batchno) from tblhotmixplant");

        //            string tempbatchno = tempbno1.ToString();
        //            txtBatchNo.Text = dr["Unique_No"].ToString();
        //            //if (!IsExist(Convert.ToDouble(txtBatchNo.Text), txtdate.Text, txtTime.Text)) { continue; }
        //            txtBatchDuration.Text = dr["MSecCol"].ToString();

        //            txtHB1.Text = dr["Agg_K1"].ToString();
        //            txtHB2.Text = dr["Agg_K2"].ToString();

        //            txtHB3.Text = dr["Agg_K3"].ToString();
        //            txtHB4.Text = dr["Agg_K4"].ToString();

        //            //txtHB1Per.Text = dr["f5"].ToString();
        //            //txtHB2Per.Text = dr["f7"].ToString();

        //            //txtHB3Per.Text = dr["f9"].ToString();
        //            //txtHB4Per.Text = dr["f11"].ToString();

        //            //txtHB5.Text = dr["f12"].ToString();
        //            //txtHB6.Text = dr["f14"].ToString();

        //            //txtHB5Per.Text = dr["f13"].ToString();
        //            //txtHB6Per.Text = dr["f15"].ToString();


        //            txtFiller.Text = Convert.ToDouble( Convert.ToDouble(dr["Fill_F1"].ToString())+ Convert.ToDouble(dr["Fill_F2"].ToString())).ToString();
        //            //txtFillerPer.Text = dr["f17"].ToString();

        //            //txtRAP.Text = dr["f18"].ToString();
        //            //txtRAPPer.Text = dr["f19"].ToString();

        //            txtAsphalt.Text = ((Convert.ToDouble(dr["Bitu_B1"]) + Convert.ToDouble(dr["Bitu_B2"]))/10).ToString();

        //            //txtAsphaltPer.Text = dr["f21"].ToString();

        //            txtMixMatTemp.Text = dr["Prod_Temp"].ToString();
        //            //txtMixMatTemp.Text = dr["f30"].ToString();
        //            txtSmoke.Text = dr["Sand_Temp"].ToString();
        //            txtHotBinTemp.Text = "0"; dr["Bitumen_Temp"].ToString();
        //            txtTank1.Text = dr["Bitumen_Temp"].ToString();

        //            txtBatchSize.Text = dr["Total"].ToString();
        //            //txtTruckNo.Text = xlRange.Cells[i, 33].Value2().ToString();

        //            //txtTruckCount.Text = txtCount.Text;

        //            txtNet.Text = dr["Total"].ToString();


        //        if (txtdate.Text != "" && time.Text != "" && txtBatchNo.Text != "")
        //        {
        //            bool CheckDataExistInTable = IsExist(Convert.ToDouble(txtBatchNo.Text), txtdate.Text, txtTime.Text);

        //            if (CheckDataExistInTable == true)
        //            {
        //                //if (xlsReadStatus == true)
        //                {
        //                    GetRecordsFromXLS(true);
        //                    //aTimer.Enabled = true;
        //                }

        //            }
        //            else
        //            {
        //                MessageBox.Show(" - Linhoff - CheckDataExistInTable not true");
        //                clsFunctions_comman.ErrorLog(" - Linhoff - CheckDataExistInTable not true");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Error - Linhoff - date, time or BatchNo is empty");
        //            clsFunctions_comman.ErrorLog("Error - Linhoff - date, time or BatchNo is empty");
        //        }

        //        // goto ExitLoop;

        //    }
        //  // DataTable dt = clsSqlFunctions.FillDataTable("Select * from Report_150416 where datetime(Timecol)<='"+txtdate.Text+" "+time.Text+"' order by Unique_No");
        //}

        //--------------------------------------------------------------------------------------------

        private void tbfromLinnhoff()
        {

        }
        private void getFileFrom()
        {
            try
            {
                FileNameDateFormat = Convert.ToDateTime(txtdate.Text).ToString("ddMMyyyy");
                string FolderYear;
                string MonthFolder;

                FolderYear = Convert.ToDateTime(txtdate.Text).ToString("yyyy");
                MonthFolder = Convert.ToDateTime(txtdate.Text).ToString("MMMM");

                string[] files = System.IO.Directory.GetFiles(clsFunctions.DataImportPath + "\\" + FolderYear + "\\" + MonthFolder, FileNameDateFormat + ".csv");

                int Flag = 0;
                string dir = clsFunctions.DataImportPath + "\\" + FolderYear + "\\" + MonthFolder;
                string[] files1;

                int numFiles;
                files1 = Directory.GetFiles(dir);
                numFiles = files.Length;

                //for (int i = 0; i < numFiles; i++)
                {
                    string fileName;
                    //fileName = files[0].ToString();
                    //fileName=clsFunctions.DataImportPath + "\" + FileNameDateFormat +  ".csv";

                    fileName = clsFunctions.DataImportPath + "\\" + FolderYear + "\\" + MonthFolder + "\\" + FileNameDateFormat + ".csv";

                    string xlsFile = FileNameDateFormat + ".csv";
                    //"D:\\VinayWork\\PMC_SCADA 12%mix\\Reports\\Datewise\\2023\\March\\04032023.csv";
                    //clsFunctions.ErrorLog("search file from path is "+xlsFile);

                    if (File.Exists(fileName))
                    {
                        getExcelFile(fileName, xlsFile, dir);
                    }
                    else
                    {
                        MessageBox.Show("File not Found please Restart the Application" + fileName);
                        clsFunctions.ErrorLog("file not exists File = " + fileName);
                        //Dispose();
                    }
                }

                if (Flag == 0)
                {
                    //Response.Write("No Matches Found");
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Error in getFileFrom " + ex.Message);
                //MessageBox.Show(ex.Message, "VIPL Get GetFileForm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------------

        public static string recordtime;
        delegate void SetTextCallback(string text2, string text3, string text4);

        public void getExcelFile(string XLSFile, string xlsFileName, string Dir)
        {
            //string xlsFileName = File.Copy(xlsFileName1);
            double iXLSRowCount;
            string strCellValue = "";
            Boolean CellBlank = true;
            try
            {
                if (time.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        getExcelFile(XLSFile, xlsFileName, Dir);
                    });
                    //return;
                }
                else
                {
                    if (txtBatchNo.Text == "")
                        txtBatchNo.Text = "0";
                    //time.Text = Convert.ToDateTime(time.Text).AddSeconds(3).ToString("HH:mm:ss");
                    // currenttime = Convert.ToDateTime(time.Text).AddSeconds(3).ToString("HH:mm:ss");
                    // if(IsExist(Convert.ToDouble(txtBatchNo.Text),txtdate.Text,time.Text))
                    //---------------------------------23Nov23 Added by BK for resolving readonly file issue.
                    try
                    {
                        string temppath = Application.StartupPath + "\\tempfile";
                        // Ensure the destination folder exists
                        if (!Directory.Exists(temppath))
                        {
                            Directory.CreateDirectory(temppath);
                        }
                        try
                        {
                            // Copy the file to the destination folder
                            File.Copy(Dir + "\\" + xlsFileName, temppath + "\\" + xlsFileName, true);
                            DirectoryInfo tempDir = new DirectoryInfo(temppath + "\\" + xlsFileName);
                            DirectorySecurity dirSecurity = tempDir.GetAccessControl();
                            dirSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                            tempDir.SetAccessControl(dirSecurity);
                        }
                        catch
                        {
                        }
                        try
                        {
                            ImportAceessFromExcel(xlsFileName, temppath, Convert.ToDateTime(time.Text).ToString("HH:mm:ss"));
                        }
                        catch (Exception e) { clsFunctions.ErrorLog("Exception on file importAccessFrom Excel method ->"+e.StackTrace); }
                        // Check if the file exists before attempting to delete
                        if (File.Exists(temppath + "\\" + xlsFileName))
                        {
                            /// Delete the file
                            File.Delete(temppath + "\\" + xlsFileName);
                        }
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show("error in file operation" + e);
                        clsFunctions.ErrorLog("error in file operation" + e.StackTrace);
                    }
                    //-----------------------------------End BK Block

                    //ImportAceessFromExcel(xlsFileName, Dir, Convert.ToDateTime(time.Text).ToString("HH:mm:ss"));//Old Code Comment by BK 23Nov23
                }
            }
            catch (Exception e)
            {
                try { clsFunctions.ErrorLog("Error in move excel to mdb " + e); } catch { }
            }
            Boolean xlsReadStatus = true;
            //string BatchEndStatus;
            try
            {
                {
                    //iterate over the rows and columns and print to the console as it appears in the file
                    //excel is not zero based!!
                    {
                        {
                            //System.Data.OleDb.OleDbConnection AccessConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\temp\\Protool_Vb\\Protool_pc.mdb");
                            //AccessConnection.Open();
                            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\EXCELUTIL.mdb");
                            DataTable dt = new DataTable();
                            {
                                if (conn.State == ConnectionState.Closed) conn.Open();

                                OleDbDataReader reader = null;
                                string xyz = Convert.ToDateTime(time.Text).ToString("HH:mm:ss");
                                OleDbCommand cmd = new OleDbCommand("Select * from BaseTable where timevalue(f1)>=#" + xyz + " # and Status='N' order by timevalue(f1),F2 asc", conn);
                                //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                                reader = cmd.ExecuteReader();
                                //dt.Load(reader);
                                //int numRows = dt.Rows.Count;
                                //da.Fill(dt);

                                while (reader.Read())
                                {
                                    //DBPath = dt.Rows[0]["path"].ToString();
                                    string strDate;

                                    // string[] Data = reader[0].ToString().Split('Z');

                                    strDate = reader["f1"].ToString();
                                    //clsFunctions.ErrorLog("inside reader strdate = "+strDate );
                                    // double d = double.Parse(strDate);
                                    //string d = Convert.ToDateTime(strDate).ToString("dd/MM/yyyy HH:mm:ss");

                                    DateTime conv = Convert.ToDateTime(strDate.ToString());
                                    //string m = n.ToString("yyyy-MM-dd'T'HH:mm:ssZ");

                                    //DateTime conv = Convert.ToDateTime(strDate.ToString("dd/MM/yyyy HH:mm:ss"));

                                    string m = conv.ToString("yyyy-MM-dd'T'HH:mm:ss");

                                    //DateTime conv = Convert.ToDateTime(d);//DateTime.FromOADate(d);

                                    if (this.InvokeRequired)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            getExcelFile(XLSFile, xlsFileName, Dir);
                                        });
                                        return;
                                    }
                                    if (txtdate.InvokeRequired)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            getExcelFile(XLSFile, xlsFileName, Dir);
                                        });
                                        return;
                                    }
                                    else
                                    {
                                        FileNameDateFormat = Convert.ToDateTime(txtdate.Text).ToString("ddMMyyyy");
                                    }
                                    if (time.InvokeRequired)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            getExcelFile(XLSFile, xlsFileName, Dir);
                                        });
                                        return;
                                    }
                                    else
                                    {
                                        time.Text = Convert.ToDateTime(time.Text).ToString("HH:mm:ss");
                                        currenttime = Convert.ToDateTime(time.Text).ToString("HH:mm:ss");
                                        recordtime = time.Text;
                                    }
                                    txtTime.Text = conv.ToString("HH:mm:ss");  //xlRange.Cells[i, 2].Value2.ToString();
                                    //txtTime.Text = recordtime;//recordtime.ToString("HH:mm:ss");  //xlRange.Cells[i, 2].Value2.ToString();

                                    batchtime = txtTime.Text;
                                    string tempbno = txtBatchNo.Text;

                                    txtBatchNo.Text = reader["f2"].ToString();
                                    if (!IsExist(Convert.ToDouble(txtBatchNo.Text), txtdate.Text, txtTime.Text)) { continue; }
                                    txtBatchDuration.Text = reader["f3"].ToString();

                                    txtHB1.Text = reader["f4"].ToString();
                                    txtHB2.Text = reader["f6"].ToString();

                                    txtHB3.Text = reader["f8"].ToString();
                                    txtHB4.Text = reader["f10"].ToString();

                                    txtHB1Per.Text = reader["f5"].ToString();
                                    txtHB2Per.Text = reader["f7"].ToString();

                                    txtHB3Per.Text = reader["f9"].ToString();
                                    txtHB4Per.Text = reader["f11"].ToString();

                                    txtHB5.Text = reader["f12"].ToString();
                                    txtHB6.Text = reader["f14"].ToString();

                                    txtHB5Per.Text = reader["f13"].ToString();
                                    txtHB6Per.Text = reader["f15"].ToString();

                                    //txtFiller.Text = reader["f18"].ToString();
                                    //txtFiller.Text = reader["f16"].ToString(); it was commented for sidhivinayak client. by dinesh

                                    txtFiller.Text = reader["f16"].ToString();      // required this for JayshivShankar

                                    txtFillerPer.Text = reader["f17"].ToString();

                                    txtRAP.Text = reader["f18"].ToString();
                                    txtRAPPer.Text = reader["f19"].ToString();

                                    txtAsphalt.Text = reader["f20"].ToString();
                                    txtAsphaltPer.Text = reader["f21"].ToString();

                                    //------------------- Working for All clients ---------------

                                    //txtMixMatTemp.Text = reader["f29"].ToString();
                                    ////txtMixMatTemp.Text = reader["f30"].ToString();
                                    //txtSmoke.Text = reader["f28"].ToString();
                                    //txtHotBinTemp.Text = reader["f29"].ToString();
                                    //txtTank1.Text = reader["f30"].ToString();
                                    //
                                    //txtBatchSize.Text = reader["f31"].ToString();

                                    txtNet.Text = reader["f22"].ToString();

                                    //--------------- 10/02/2025 - Bhavesh ------------------- changes as told by Govind sir for JayShivShankar

                                    try
                                    {
                                        txtMixMatTemp.Text = reader["f33"].ToString();
                                        txtSmoke.Text = reader["f32"].ToString();
                                        txtHotBinTemp.Text = reader["f33"].ToString();
                                        txtTank1.Text = reader["f34"].ToString();
                                        txtBatchSize.Text = reader["f35"].ToString();

                                        // for Siddhivinayak - different row ----------------- 17/02/2025

                                        if (txtTank1.Text == "" && txtBatchSize.Text == "0")
                                        {
                                            clsFunctions.ErrorLog("[INFO] BT-ExcelBase - Different row.");

                                            txtSmoke.Text = reader["f28"].ToString();
                                            txtMixMatTemp.Text = reader["f29"].ToString();
                                            txtHotBinTemp.Text = reader["f29"].ToString();
                                            txtTank1.Text = reader["f30"].ToString();
                                            txtBatchSize.Text = reader["f33"].ToString();

                                        }

                                        // use old column if data not found in new columns

                                        // if (txtMixMatTemp.Text == "" && txtBatchSize.Text == "0" || txtBatchSize.Text == "")
                                        else if (txtMixMatTemp.Text == "" || Convert.ToInt32(txtMixMatTemp.Text) > 500 || txtMixMatTemp.Text == "0" && txtBatchSize.Text == "0" || txtBatchSize.Text == "")   
                                        {
                                            txtMixMatTemp.Text = reader["f29"].ToString();
                                            txtSmoke.Text = reader["f28"].ToString();
                                            txtHotBinTemp.Text = reader["f29"].ToString();
                                            txtTank1.Text = reader["f30"].ToString();                                            
                                            txtBatchSize.Text = reader["f31"].ToString();

                                            // for siddhivinayak ----------------- 17/02/2025

                                            if(txtSmoke.Text == "0")
                                            {
                                                txtSmoke.Text = reader["f29"].ToString();
                                                txtMixMatTemp.Text = reader["f30"].ToString();
                                                txtHotBinTemp.Text = reader["f30"].ToString();
                                                txtTank1.Text = reader["f31"].ToString();
                                                txtBatchSize.Text = reader["f34"].ToString();
                                            }
                                        }

                                        TempValidatorAlert();

                                    }
                                    catch (Exception ex)
                                    {
                                        clsFunctions_comman.ErrorLog("[Exception] getExcelFile: ExcelUtil to TextBox data mapping - " + ex.Message);
                                        MessageBox.Show(ex.Message);
                                    }
                                    
                                    //-------------------------------------

                                    //txtTruckNo.Text = xlRange.Cells[i, 33].Value2().ToString();
                                    //txtTruckCount.Text = txtCount.Text;


                                    dt.EndLoadData();
                                    reader.Close();
                                    conn.Close();

                                    goto ExitLoop;
                                }
                            }
                        }
                    }
                }                
            //cleanup

            ExitLoop:
                if (CellBlank == true)
                {
                    if (txtdate.Text != "" && txtTime.Text != "" && txtBatchNo.Text != "")
                    {
                        //bool CheckDataExistInTable = IsExist(Convert.ToDouble(txtBatchNo.Text), txtdate.Text, txtTime.Text);
                        bool CheckDataExistInTable = IsExist(Convert.ToDouble(txtBatchNo.Text), txtdate.Text, Convert.ToDateTime(txtTime.Text).ToString("HH:mm"));

                        if (CheckDataExistInTable == true)
                        {
                            if (xlsReadStatus == true)
                            {
                                GetRecordsFromXLS(xlsReadStatus);
                                aTimer.Enabled = true;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                clsFunctions.ErrorLog("[Exception] LoadDetails - getExcelFile() : " + ex.Message);

                //   MessageBox.Show(ex.Message, "getExcelFile", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //--------------------------------------------------------------------------------------------

        string previoustime = "";

        public async Task GetRecordsFromXLS(Boolean FridStatus)       // For ExcelBase, Linnhoff
        {
            try
            {
                string batchno = "0";
                string srno = "0";
                string exportstatus = "Y";

                //------------Comment by bhavesh----------------------------------
                //Random rnd = new Random();
                //string mix= rnd.Next(120, 140).ToString();
                //string bitu = rnd.Next(150, 170).ToString();
                //-------------------------------------------------

                //timer1.Enabled = true;
                //dgvloaddetails.Rows.Clear();
                string time_comp = DateTime.Now.AddMinutes(-5).ToString("HH:mm:ss");
                string date_comp = DateTime.Today.ToString("MM/dd/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");

                bool a = IsExist(Convert.ToDouble(txtBatchNo.Text), txtdate.Text, txtTime.Text);

                //txtdate.Text = Convert.ToDateTime(dt.Rows[0]["DATE1"].ToString()).ToString("dd/MM/yyyy");//Comment by bhavesh
                {
                    //row["TRUCK_NO"].ToString();
                    lblbatchno.Text = txtBatchNo.Text;
                    //batchtime = txtTime.Text;
                    batchno = txtBatchNo.Text;
                    Batch_Duration_InSec = txtBatchDuration.Text;
                    Weight_KgPerBatch = txtNet.Text;
                    F1_InPer_val = "0";
                    F2_InPer_val = "0";
                    F3_InPer_val = "0";
                    F4_InPer_val = "0";
                    F1_InKg_val = txtHB1.Text;
                    F2_Inkg_val = txtHB2.Text;
                    F3_Inkg_val = txtHB3.Text;
                    F4_Inkg_val = txtHB4.Text;
                    bitumenkg_val = txtAsphalt.Text;

                    bitumenper_val = "0";
                    fillerkg_val = txtFiller.Text;
                    fillerper_val = "0";

                    tank1_val = txtTank1.Text;//bhavesh
                    tank2_val = "0";

                    exhausttemp_val = txtSmoke.Text;
                    mixtemp_val = txtMixMatTemp.Text;

                    if (Convert.ToDouble(tank1_val) < 0) tank1_val = "0";
                    if (Convert.ToDouble(tank2_val) < 0) tank2_val = "0";
                    if (Convert.ToDouble(exhausttemp_val) < 0) exhausttemp_val = "0";
                    if (Convert.ToDouble(mixtemp_val) < 0) mixtemp_val = "0";

                    aggregateinkg = (Convert.ToDouble(txtHB1.Text) + Convert.ToDouble(txtHB2.Text) + Convert.ToDouble(txtHB3.Text) + Convert.ToDouble(txtHB4.Text)).ToString();

                    //txtAggWt.Text = aggregateinkg.ToString();
                    await UpdateActualValuestoUI(txtAggWt, aggregateinkg.ToString());

                    Commulativebitumen = Commulativebitumen + Convert.ToDouble(txtNet.Text);
                    aggregatetph_val = "0";
                    bitumenkgmin_val = "0";
                    fillerkgmin_val = "0";
                    moisture_val = "0";
                    aggregateton_val = "0";

                    //netmix_val = (Convert.ToDouble(txtNet.Text) / 1000).ToString();

                    try
                    {
                        string txtNet1 = ( ( Convert.ToDouble(txtHB1.Text) + Convert.ToDouble(txtHB2.Text) + Convert.ToDouble(txtHB3.Text) + Convert.ToDouble(txtHB4.Text) + Convert.ToDouble(fillerkg_val) + Convert.ToDouble(bitumenkg_val) ) ).ToString();

                        await UpdateActualValuestoUI(txtNet, txtNet1);

                        Weight_KgPerBatch = txtNet.Text;
                        netmix_val = (Convert.ToDouble(txtNet.Text) / 1000).ToString();

                        

                        //netmix_val = ((Convert.ToDouble(aggregateinkg) + Convert.ToDouble(fillerkg_val) + Convert.ToDouble(bitumenkg_val)) / 1000).ToString();
                    }
                    catch (Exception ex)
                    {
                        clsFunctions.ErrorLog("[Exception] GetRecordsFromXLS - while calculating netmix_val : " + ex.Message);
                        netmix_val = (Convert.ToDouble(txtNet.Text) / 1000).ToString();
                    }


                    // if (a == true )
                    if (a == true && Convert.ToDateTime(batchtime) >= Convert.ToDateTime(currenttime))
                    {
                        if (button1.Enabled == false && btnstart.Enabled == true) 
                        { 
                            //clsFunctions.ErrorLog("Condition False \"StopBtn.Enabled == false && btnstart.Enabled == true\" "); 
                        }//added by annu
                        else//added by annu
                        {
                            if (previoustime == "" || previoustime != batchtime)
                            {
                                previoustime = batchtime;
                            }
                            else
                            {
                                previoustime = Convert.ToDateTime(batchtime).AddSeconds(30).ToString();
                            }

                            srno = (dgvloaddetails.Rows.Count + 1).ToString();

                            dgvloaddetails.Rows.Insert(0, srno, batchtime, batchno, Batch_Duration_InSec, Weight_KgPerBatch, F1_InPer_val, F2_InPer_val, F3_InPer_val,
                                            F4_InPer_val, F1_InKg_val, F2_Inkg_val, F3_Inkg_val, F4_Inkg_val, bitumenkg_val, bitumenper_val, fillerkg_val,
                                            fillerper_val, mixtemp_val, exhausttemp_val, tank1_val, tank2_val, aggregateinkg, Commulativebitumen, aggregatetph_val,
                                            bitumenkgmin_val, fillerkgmin_val, moisture_val, aggregateton_val, netmix_val);

                            SaveDataToHotMixScadaFromXLS();
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        clsFunctions.ErrorLog("Condition False \"a == true && Convert.ToDateTime(batchtime) >= Convert.ToDateTime(currenttime)\" ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "| [Exception] LoadDetails - GetRecordsFromXLS() ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions.ErrorLog("[Exception] LoadDetails - GetRecordsFromXLS() : " + ex.Message);

            }
        }///GetRecordsFromXLS end of function

        //--------------------------------------------------------------------------------------------


        private string GetTextSafe(Control control)
        {
            if (control.InvokeRequired)
            {
                return (string)control.Invoke(new Func<string>(() => control.Text));
            }
            else
            {
                return control.Text;
            }
        }

        private void SaveDataToHotMixScadaFromXLS_PLC(
     double maxFiller,
     double maxFiller1,
     double maxAggKg,
     double maxAspTank1,
     double maxSmokeTemp,
     double maxHotBin1Temp,
     double maxTotalWeight,
     double maxAsphalt,
     double maxFOTemp,
     double maxAspPipe,
     double maxHotBin1,
     double maxHotBin2,
     double maxHotBin3,
     double maxHotBin4)
        {
            try
            {
                string regno = ""; //string regno = "99";
                string plantcode = ""; //string plantcode = "";
                string oprlat = "18.6720225";// string oprlat = "";     //added default values by dinesh
                string oprlong = "73.8138366"; //string oprlong = "";    //added default values by dinesh
                string oprjurisdiction = "Pune";// string oprjurisdiction = "";   //added default values by dinesh
                string oprdivision = "Pune"; //string oprdivision = "";            //added default values by dinesh
                string oprworkname = GetTextSafe(cmbworkname);//cmbworkname.Text;

                //added by dinesh
                //string strSiteName = cmbJobSite.Text;

                string deviceid = "";
                string exportstatus = "Y";
                int viplupload = 0;
                double netmixa = 0;
                int batchno = 0;
                int loadno = 0;
                int truck_count = 0;

                int preloadno = 0;
                //int srno = 0;

                //DataTable plntdt = clsFunctions.fillDatatable_setup("select * from PlantSetup");
                //if (plntdt.Rows.Count > 0)
                //{
                //    DataRow r = plntdt.Rows[0];
                //    plantcode = r["PlantCode"].ToString();
                //    deviceid = r["DeviceID"].ToString();
                //}

                plantcode = GetTextSafe(txtPlantCode);//txtPlantCode.Text;
                deviceid = clsFunctions.activeDeviceID;
                oprworkname = GetTextSafe(cmbworkname);//cmbworkname.Text;
                regno = GetTextSafe(txtConCode);//txtConCode.Text;

                loadno = clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant");
                truck_count = loadno;

                ///old Code.
                ///string regno = ""; //string regno = "99";
                ///string plantcode = ""; //string plantcode = "";
                ///string oprlat = "";// string oprlat = "";   
                ///string oprlong = ""; //string oprlong = ""; 
                ///string oprjurisdiction = "";// string oprjurisdiction = "";  
                ///string oprdivision = ""; //string oprdivision = "";          
                ///string oprworkname = cmbworkname.Text;
                ///string imeino = "";
                ///string exportstatus = "Y";
                ///int viplupload = 0;
                ///double netmixa = 0;
                ///int batchno = 0;
                ///int loadno = 0;//Convert.ToInt32(clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant ")); //0;
                ///int preloadno = 0;// Convert.ToInt32(clsFunctions.GetMaxId("select Max(loadno)  from tblHotMixPlant "));//0;
                ///int srno = 0;
                ///DataTable plntdt = clsFunctions.fillDatatable_setup("select * from PlantSetup");
                ///if (plntdt.Rows.Count > 0)
                ///{
                ///    DataRow r = plntdt.Rows[0];
                ///    regno = r["ContractorCode"].ToString();
                ///    plantcode = r["PlantCode"].ToString();
                ///    imeino = r["DeviceID"].ToString();
                ///}
                ///DataTable workdt = clsFunctions.fillDatatable("select * from WorkOrder");
                ///if (workdt.Rows.Count > 0)
                ///{
                ///    DataRow r = workdt.Rows[0];
                ///    oprworkname = r["workname"].ToString();
                ///    oprlat = "80.54533";                    // r["oprlatitude"].ToString();//commented by dinesh
                ///    oprlong = "80.3203";                 // r["oprlongitude"].ToString();  //commented by dinesh
                ///    oprjurisdiction = "Pune";         // r["oprjurisdiction"].ToString();  //commented by dinesh
                ///    oprdivision = "Pune";           // r["oprdivision"].ToString();        //commented by dinesh
                ///}

                int i = -1;
                //foreach (DataRow row in dt1.Rows)
                {
                    i++;

                    string ttime1 = GetTextSafe(txtTime);//txtTime.Text;
                    batchno = Convert.ToInt32(GetTextSafe(txtBatchNo));
                    loadno = Convert.ToInt32(GetTextSafe(txtTruckCount));//Convert.ToInt32(clsFunctions.GetMaxId("select sum(batchendflag)+1 from tblHotMixPlant "));//where tdate =#" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "# "));
                    truck_count = loadno;

                    // loadno = Convert.ToInt32(clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant "));
                    //if (preloadno != loadno)
                    //{
                    //    batchno = clsFunctions.GetMaxId("select Max(batchno)+1 from tblHotMixPlant");
                    //}
                    //batchno = srno;//Convert.ToInt32(row["BATCH_NO"].ToString());

                    lastbatchno = (batchno).ToString();

                    L_LoadNo = loadno;        // for endBatch - BhaveshT : 23/02/2024
                    L_BatchNo = batchno;      // for endBatch - BhaveshT

                    string update_date = GetTextSafe(txtdate);//txtdate.Text;
                    string tdate = GetTextSafe(txtdate);
                    string ttime = ttime1;
                    double aggregatetph = Convert.ToDouble(0);
                    double bitumenkgmin = Convert.ToDouble(0); // means bitumen kg per batch
                    double fillerkgmin = Convert.ToDouble(0);
                    double bitumenkg = Convert.ToDouble(txtAsphalt.Text);

                    double F1_Inkg = Convert.ToDouble(txtHB1.Text);
                    double F2_Inkg = Convert.ToDouble(txtHB2.Text);
                    double F3_Inkg = Convert.ToDouble(txtHB3.Text);
                    double F4_Inkg = Convert.ToDouble(txtHB4.Text);

                    double Weight_KgPerBatch = Convert.ToDouble(txtNet.Text);

                    double fillerkg = Convert.ToDouble(txtFiller.Text);

                    // changes added by annu
                    double F1_InPer = Convert.ToDouble(txtHB1Per.Text);
                    double F2_InPer = Convert.ToDouble(txtHB2Per.Text);
                    double F3_InPer = Convert.ToDouble(txtHB3Per.Text);
                    double F4_InPer = Convert.ToDouble(txtHB4Per.Text);
                    double fillerper = Convert.ToDouble(txtFillerPer.Text);
                    double bitumenper = Convert.ToDouble(txtAsphaltPer.Text);

                    //int i = 0;
                    double mixtemp = 0;
                    double exhausttemp = 0;
                    double tank1 = 0;
                    double tank2 = 0;
                    string tipper = cmbtipper.SelectedItem.ToString();
                    // for (i = 0; i < dgvloaddetails.Rows.Count; i++)
                    // {
                    //dgvloaddetails.Rows[recno].Cells["Aggregateinkg"].Value
                    //Added by bhavesh

                    mixtemp = Convert.ToDouble(txtMixMatTemp.Text); //Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_mixtemp"].Value);//Convert.ToDouble(row["CH4_TEMP"]);
                    exhausttemp = Convert.ToDouble(txtSmoke.Text);//Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_exhausttemp"].Value);//Convert.ToDouble(row["CH2_TEMP"]);
                    tank1 = Convert.ToDouble(txtTank1.Text);//Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_tank1"].Value);//Convert.ToDouble(row["CH5_TEMP"]);
                    tank2 = 0;
                    //Convert.ToDouble(row["CH7_TEMP"]);
                    //tipper = cmbtipper.SelectedItem.ToString();//row["TRUCK_NO"].ToString();

                    //----------- Rouded off temperature -----------------------

                    mixtemp = Math.Round(mixtemp);
                    exhausttemp = Math.Round(exhausttemp);
                    tank1 = Math.Round(tank1);


                    double moisture = Convert.ToDouble(0);
                    double aggregateton = Convert.ToDouble(0);

                    //netmixa = netmixa + Weight_KgPerBatch;
                    double netmix = Weight_KgPerBatch / 1000;

                    int Batch_Duration_InSec = Convert.ToInt32(txtBatchDuration.Text);
                    double Aggregate_Kg = Convert.ToDouble(txtAggWt.Text);
                    //int truck_count = Convert.ToInt32(loadno);


                    //--------------------- Encrypt data to insert in PWD_DM -------------------------------------- 15/01/2025 : BhaveshT

                    if (clsFunctions.aliasName.Contains("PWD"))
                    {
                        try
                        {
                            //clsDM.E_interatorID = clsScadaFunctions.clsMethod.encrypts("2d5i6y");
                            //clsDM.E_workID = clsScadaFunctions.clsMethod.encrypts(lblworkkode.Text);
                            //clsDM.E_PlantId = clsScadaFunctions.clsMethod.encrypts(plantcode);

                            //clsDM.E_pwd_work = clsScadaFunctions.clsMethod.encrypts("Y");
                            //clsDM.E_typeOfwork = clsScadaFunctions.clsMethod.encrypts(Cmbmaterialtype.Text);
                            //clsDM.E_RFI = clsScadaFunctions.clsMethod.encrypts(txttipperno.Text);

                            ////clsDM.E_exhust = clsScadaFunctions.clsMethod.encrypts(txtHotBinTemp.Text);
                            ////clsDM.E_bitumen = clsScadaFunctions.clsMethod.encrypts(txtTank1.Text);
                            ////clsDM.E_mix = clsScadaFunctions.clsMethod.encrypts(txtMixMatTemp.Text);

                            //clsDM.E_exhust = clsScadaFunctions.clsMethod.encrypts(exhausttemp.ToString());
                            //clsDM.E_bitumen = clsScadaFunctions.clsMethod.encrypts(tank1.ToString());
                            //clsDM.E_mix = clsScadaFunctions.clsMethod.encrypts(mixtemp.ToString());

                            //clsDM.E_flag1 = "0";
                            //clsDM.E_IO = clsScadaFunctions.clsMethod.encrypts("255");
                            //clsDM.E_extr = clsScadaFunctions.clsMethod.encrypts(truck_count.ToString());
                            //clsDM.E_loadno = clsScadaFunctions.clsMethod.encrypts(loadno.ToString());
                            //clsDM.E_batchNo = clsScadaFunctions.clsMethod.encrypts(batchno.ToString());

                            //clsDM.E_date = clsScadaFunctions.clsMethod.encrypts(Convert.ToDateTime(tdate).ToString("yyyy-MM-dd"));
                            //clsDM.E_time = clsScadaFunctions.clsMethod.encrypts(Convert.ToDateTime(ttime).ToString("HH:mm:ss tt"));

                            //string dateTimeString = tdate + " " + ttime;
                            //clsDM.E_datetime = clsScadaFunctions.clsMethod.encrypts(DateTime.Parse(dateTimeString).ToString("yyyy-MM-dd HH:mm:ss"));

                            //clsDM.E_workType = "PWDDM";
                            //clsDM.E_InsertType = clsScadaFunctions.clsMethod.encrypts("A");

                            //clsDM.InsertEncryptedDataForDM();

                        }
                        catch (Exception ex)
                        {
                            clsFunctions_comman.ErrorLog("LoadDetails: at allocating variables for PWD-DM - " + ex.Message);
                        }
                    }

                    //--------------------------------------------------------------------------------------

                    string insertquery = "Insert into tblHotMixPlant(regno,plantcode,oprlat,oprlong,oprjurisdiction,oprdivision,oprworkname,"
                       + "tdate,ttime,aggregatetph,bitumenkgmin,fillerkgmin,bitumenper,fillerper,F1_InPer,F2_InPer,F3_InPer,F4_InPer,moisture,mixtemp,exhausttemp,"
                       + "tank1,tank2,tipper,aggregateton,bitumenkg,fillerkg,netmix,batchno,srno,imeino,exportstatus,viplupload,worktype,workcode,material,"
                       + "Batch_Duration_InSec,Weight_KgPerBatch,HB1_KgPerBatch,HB2_KgPerBatch,HB3_KgPerBatch,HB4_KgPerBatch,Aggregate_Kg,Truck_Count,jobsite,loadno, InsertType)"
                       + " values('" + regno + "','" + plantcode + "','" + oprlat + "','" + oprlong + "','" + oprjurisdiction + "','" + oprdivision + "','" + oprworkname + "','"
                       + tdate + "','" + ttime + "'," + aggregatetph + "," + bitumenkgmin + "," + fillerkgmin + "," + bitumenper + "," + fillerper + "," + F1_InPer + ","
                       + F2_InPer + "," + F3_InPer + "," + F4_InPer + "," + moisture + "," + mixtemp + "," + exhausttemp + "," + tank1 + "," + tank2 + ",'" + tipper + "',"
                       + aggregateton + "," + bitumenkg + "," + fillerkg + "," + netmix + "," + batchno + "," + srno + ",'" + deviceid + "','" + exportstatus + "',"
                       + viplupload + ",0,'" + lblworkkode.Text + "','" + Cmbmaterialtype.Text + "'," + Batch_Duration_InSec + "," + Weight_KgPerBatch + "," + F1_Inkg + "," + F2_Inkg + "," + F3_Inkg + "," + F4_Inkg + "," + Aggregate_Kg + "," + truck_count + ",'" + cmbJobSite.Text + "'," + loadno + ", 'A')";

                    clsFunctions.AdoData(insertquery);

                    // i++;
                    string tempquery = insertquery.Replace("tblHotMixPlant", "bkp");
                    clsFunctions.AdoData(tempquery);
                    // }
                    preloadno = loadno;

                    //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + update_date + "# AND TIME1 = #" + ttime + "#; ";
                    //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";
                    //clsFunctions.UpdateProduction(update_query);

                    enddate = update_date;
                    endtime = ttime;
                    //lastbatchno = (batchno).ToString();

                    if (clsFunctions.PlantType == "2")
                    {
                        FileUpdatePointer(tdate, ttime, batchno);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "VIPL SaveDataToHotMixScadaFromXLS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions.ErrorLog("Exception in SaveDataToHotMixScadaFromXLS: " + ex.Message);
            }
        }

        public static void StartExeIfNotRunning(string exePath, string arguments = "")
        {
            try
            {
                string exeNameWithoutExtension = Path.GetFileNameWithoutExtension(exePath);

                // Check if any process with this name is already running
                bool isRunning = Process.GetProcessesByName(exeNameWithoutExtension).Any();

                if (!isRunning)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = exePath,
                        Arguments = arguments,
                        UseShellExecute = true
                    };

                    Process.Start(startInfo);
                }
                else
                {
                    //MessageBox.Show($"{exeNameWithoutExtension} is already running.", "Info");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting executable: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void SaveDataToHotMixScadaFromXLS()     // For Linnhoff
        {
            try
            {
                string regno = ""; //string regno = "99";
                string plantcode = ""; //string plantcode = "";
                string oprlat = "18.6720225";// string oprlat = "";     //added default values by dinesh
                string oprlong = "73.8138366"; //string oprlong = "";    //added default values by dinesh
                string oprjurisdiction = "Pune";// string oprjurisdiction = "";   //added default values by dinesh
                string oprdivision = "Pune"; //string oprdivision = "";            //added default values by dinesh
                string oprworkname = cmbworkname.Text;

                //added by dinesh
                //string strSiteName = cmbJobSite.Text;

                string deviceid = "";
                string exportstatus = "Y";
                int viplupload = 0;
                double netmixa = 0;
                int batchno = 0;
                int loadno = 0;
                int truck_count = 0;

                int preloadno = 0;
                //int srno = 0;

                //DataTable plntdt = clsFunctions.fillDatatable_setup("select * from PlantSetup");
                //if (plntdt.Rows.Count > 0)
                //{
                //    DataRow r = plntdt.Rows[0];
                //    plantcode = r["PlantCode"].ToString();
                //    deviceid = r["DeviceID"].ToString();
                //}

                plantcode = txtPlantCode.Text;
                deviceid = clsFunctions.activeDeviceID;
                oprworkname = cmbworkname.Text;
                regno = txtConCode.Text;

                loadno = clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant");
                truck_count = loadno;

                ///old Code.
                ///string regno = ""; //string regno = "99";
                ///string plantcode = ""; //string plantcode = "";
                ///string oprlat = "";// string oprlat = "";   
                ///string oprlong = ""; //string oprlong = ""; 
                ///string oprjurisdiction = "";// string oprjurisdiction = "";  
                ///string oprdivision = ""; //string oprdivision = "";          
                ///string oprworkname = cmbworkname.Text;
                ///string imeino = "";
                ///string exportstatus = "Y";
                ///int viplupload = 0;
                ///double netmixa = 0;
                ///int batchno = 0;
                ///int loadno = 0;//Convert.ToInt32(clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant ")); //0;
                ///int preloadno = 0;// Convert.ToInt32(clsFunctions.GetMaxId("select Max(loadno)  from tblHotMixPlant "));//0;
                ///int srno = 0;
                ///DataTable plntdt = clsFunctions.fillDatatable_setup("select * from PlantSetup");
                ///if (plntdt.Rows.Count > 0)
                ///{
                ///    DataRow r = plntdt.Rows[0];
                ///    regno = r["ContractorCode"].ToString();
                ///    plantcode = r["PlantCode"].ToString();
                ///    imeino = r["DeviceID"].ToString();
                ///}
                ///DataTable workdt = clsFunctions.fillDatatable("select * from WorkOrder");
                ///if (workdt.Rows.Count > 0)
                ///{
                ///    DataRow r = workdt.Rows[0];
                ///    oprworkname = r["workname"].ToString();
                ///    oprlat = "80.54533";                    // r["oprlatitude"].ToString();//commented by dinesh
                ///    oprlong = "80.3203";                 // r["oprlongitude"].ToString();  //commented by dinesh
                ///    oprjurisdiction = "Pune";         // r["oprjurisdiction"].ToString();  //commented by dinesh
                ///    oprdivision = "Pune";           // r["oprdivision"].ToString();        //commented by dinesh
                  ///}
      
                int i = -1;
                //foreach (DataRow row in dt1.Rows)
                {
                    i++;

                    string ttime1 = txtTime.Text;
                    batchno = Convert.ToInt32(txtBatchNo.Text);
                    loadno = Convert.ToInt32(txtTruckCount.Text);//Convert.ToInt32(clsFunctions.GetMaxId("select sum(batchendflag)+1 from tblHotMixPlant "));//where tdate =#" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "# "));
                    truck_count = loadno;

                    // loadno = Convert.ToInt32(clsFunctions.GetMaxId("select Max(loadno)+1  from tblHotMixPlant "));
                    //if (preloadno != loadno)
                    //{
                    //    batchno = clsFunctions.GetMaxId("select Max(batchno)+1 from tblHotMixPlant");
                    //}
                    //batchno = srno;//Convert.ToInt32(row["BATCH_NO"].ToString());

                    lastbatchno = (batchno).ToString();

                    L_LoadNo = loadno;        // for endBatch - BhaveshT : 23/02/2024
                    L_BatchNo = batchno;      // for endBatch - BhaveshT

                    string update_date = txtdate.Text;
                    string tdate = txtdate.Text;
                    string ttime = ttime1;
                    double aggregatetph = Convert.ToDouble(0);
                    double bitumenkgmin = Convert.ToDouble(0); // means bitumen kg per batch
                    double fillerkgmin = Convert.ToDouble(0);
                    double bitumenkg = Convert.ToDouble(txtAsphalt.Text);

                    double F1_Inkg = Convert.ToDouble(txtHB1.Text);
                    double F2_Inkg = Convert.ToDouble(txtHB2.Text);
                    double F3_Inkg = Convert.ToDouble(txtHB3.Text);
                    double F4_Inkg = Convert.ToDouble(txtHB4.Text);

                    double Weight_KgPerBatch = Convert.ToDouble(txtNet.Text);

                    double fillerkg = Convert.ToDouble(txtFiller.Text);

                    // changes added by annu
                    double F1_InPer = Convert.ToDouble(txtHB1Per.Text);
                    double F2_InPer = Convert.ToDouble(txtHB2Per.Text);
                    double F3_InPer = Convert.ToDouble(txtHB3Per.Text);
                    double F4_InPer = Convert.ToDouble(txtHB4Per.Text);
                    double fillerper = Convert.ToDouble(txtFillerPer.Text);
                    double bitumenper = Convert.ToDouble(txtAsphaltPer.Text);
                    
                    //int i = 0;
                    double mixtemp = 0;
                    double exhausttemp = 0;
                    double tank1 = 0;
                    double tank2 = 0;
                    string tipper = cmbtipper.SelectedItem.ToString();
                    // for (i = 0; i < dgvloaddetails.Rows.Count; i++)
                    // {
                    //dgvloaddetails.Rows[recno].Cells["Aggregateinkg"].Value
                    //Added by bhavesh

                    mixtemp = Convert.ToDouble(txtMixMatTemp.Text); //Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_mixtemp"].Value);//Convert.ToDouble(row["CH4_TEMP"]);
                    exhausttemp = Convert.ToDouble(txtSmoke.Text);//Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_exhausttemp"].Value);//Convert.ToDouble(row["CH2_TEMP"]);
                    tank1 = Convert.ToDouble(txtTank1.Text);//Convert.ToDouble(dgvloaddetails.Rows[i].Cells["cell_tank1"].Value);//Convert.ToDouble(row["CH5_TEMP"]);
                    tank2 = 0;
                    //Convert.ToDouble(row["CH7_TEMP"]);
                    //tipper = cmbtipper.SelectedItem.ToString();//row["TRUCK_NO"].ToString();

                    //----------- Rouded off temperature -----------------------

                    mixtemp = Math.Round(mixtemp);
                    exhausttemp = Math.Round(exhausttemp);
                    tank1 = Math.Round(tank1);


                    double moisture = Convert.ToDouble(0);
                    double aggregateton = Convert.ToDouble(0);

                    //netmixa = netmixa + Weight_KgPerBatch;
                    double netmix = Weight_KgPerBatch / 1000;

                    int Batch_Duration_InSec = Convert.ToInt32(txtBatchDuration.Text);
                    double Aggregate_Kg = Convert.ToDouble(txtAggWt.Text);
                    //int truck_count = Convert.ToInt32(loadno);


                    //--------------------- Encrypt data to insert in PWD_DM -------------------------------------- 15/01/2025 : BhaveshT

                    if (clsFunctions.aliasName.Contains("PWD"))
                    {
                        try
                        {
                            //clsDM.E_interatorID = clsScadaFunctions.clsMethod.encrypts("2d5i6y");
                            //clsDM.E_workID = clsScadaFunctions.clsMethod.encrypts(lblworkkode.Text);
                            //clsDM.E_PlantId = clsScadaFunctions.clsMethod.encrypts(plantcode);

                            //clsDM.E_pwd_work = clsScadaFunctions.clsMethod.encrypts("Y");
                            //clsDM.E_typeOfwork = clsScadaFunctions.clsMethod.encrypts(Cmbmaterialtype.Text);
                            //clsDM.E_RFI = clsScadaFunctions.clsMethod.encrypts(txttipperno.Text);

                            ////clsDM.E_exhust = clsScadaFunctions.clsMethod.encrypts(txtHotBinTemp.Text);
                            ////clsDM.E_bitumen = clsScadaFunctions.clsMethod.encrypts(txtTank1.Text);
                            ////clsDM.E_mix = clsScadaFunctions.clsMethod.encrypts(txtMixMatTemp.Text);

                            //clsDM.E_exhust = clsScadaFunctions.clsMethod.encrypts(exhausttemp.ToString());
                            //clsDM.E_bitumen = clsScadaFunctions.clsMethod.encrypts(tank1.ToString());
                            //clsDM.E_mix = clsScadaFunctions.clsMethod.encrypts(mixtemp.ToString());

                            //clsDM.E_flag1 = "0";
                            //clsDM.E_IO = clsScadaFunctions.clsMethod.encrypts("255");
                            //clsDM.E_extr = clsScadaFunctions.clsMethod.encrypts(truck_count.ToString());
                            //clsDM.E_loadno = clsScadaFunctions.clsMethod.encrypts(loadno.ToString());
                            //clsDM.E_batchNo = clsScadaFunctions.clsMethod.encrypts(batchno.ToString());

                            //clsDM.E_date = clsScadaFunctions.clsMethod.encrypts(Convert.ToDateTime(tdate).ToString("yyyy-MM-dd"));
                            //clsDM.E_time = clsScadaFunctions.clsMethod.encrypts(Convert.ToDateTime(ttime).ToString("HH:mm:ss tt"));

                            //string dateTimeString = tdate + " " + ttime;
                            //clsDM.E_datetime = clsScadaFunctions.clsMethod.encrypts(DateTime.Parse(dateTimeString).ToString("yyyy-MM-dd HH:mm:ss"));

                            //clsDM.E_workType = "PWDDM";
                            //clsDM.E_InsertType = clsScadaFunctions.clsMethod.encrypts("A");

                            //clsDM.InsertEncryptedDataForDM();

                        }
                        catch (Exception ex)
                        {
                            clsFunctions_comman.ErrorLog("LoadDetails: at allocating variables for PWD-DM - " + ex.Message);
                        }
                    }

                    //--------------------------------------------------------------------------------------

                    string insertquery = "Insert into tblHotMixPlant(regno,plantcode,oprlat,oprlong,oprjurisdiction,oprdivision,oprworkname,"
                       + "tdate,ttime,aggregatetph,bitumenkgmin,fillerkgmin,bitumenper,fillerper,F1_InPer,F2_InPer,F3_InPer,F4_InPer,moisture,mixtemp,exhausttemp,"
                       + "tank1,tank2,tipper,aggregateton,bitumenkg,fillerkg,netmix,batchno,srno,imeino,exportstatus,viplupload,worktype,workcode,material,"
                       + "Batch_Duration_InSec,Weight_KgPerBatch,HB1_KgPerBatch,HB2_KgPerBatch,HB3_KgPerBatch,HB4_KgPerBatch,Aggregate_Kg,Truck_Count,jobsite,loadno, InsertType)"
                       + " values('" + regno + "','" + plantcode + "','" + oprlat + "','" + oprlong + "','" + oprjurisdiction + "','" + oprdivision + "','" + oprworkname + "','"
                       + tdate + "','" + ttime + "'," + aggregatetph + "," + bitumenkgmin + "," + fillerkgmin + "," + bitumenper + "," + fillerper + "," + F1_InPer + ","
                       + F2_InPer + "," + F3_InPer + "," + F4_InPer + "," + moisture + "," + mixtemp + "," + exhausttemp + "," + tank1 + "," + tank2 + ",'" + tipper + "',"
                       + aggregateton + "," + bitumenkg + "," + fillerkg + "," + netmix + "," + batchno + "," + srno + ",'" + deviceid + "','" + exportstatus + "',"
                       + viplupload + ",0,'" + lblworkkode.Text + "','" + Cmbmaterialtype.Text + "'," + Batch_Duration_InSec + "," + Weight_KgPerBatch + "," + F1_Inkg + "," + F2_Inkg + "," + F3_Inkg + "," + F4_Inkg + "," + Aggregate_Kg + "," + truck_count + ",'" + cmbJobSite.Text + "'," + loadno + ", 'A')";

                    clsFunctions.AdoData(insertquery);

                    // i++;
                    string tempquery = insertquery.Replace("tblHotMixPlant", "bkp");
                    clsFunctions.AdoData(tempquery);
                    // }
                    preloadno = loadno;

                    //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1=#" + update_date + "# AND TIME1 = #" + ttime + "#; ";
                    //string update_query = "UPDATE Production SET FILLER2=1 WHERE LOAD_NO<>0 AND FILLER2=0 AND DATE1 =#" + Convert.ToDateTime(update_date).ToString("MM/dd/yyyy") + "#";
                    //clsFunctions.UpdateProduction(update_query);

                    enddate = update_date;
                    endtime = ttime;
                    //lastbatchno = (batchno).ToString();

                    if (clsFunctions.PlantType == "2")
                    {
                        FileUpdatePointer(tdate, ttime, batchno);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "VIPL SaveDataToHotMixScadaFromXLS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions.ErrorLog("Exception in SaveDataToHotMixScadaFromXLS: " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------

        //function call
        //--------------------------------------
        private bool IsExist(double batchno, string tdate, string ttime)   // Function Returns bool values.
        {
            try
            {
                try
                {
                    tdate = Convert.ToDateTime(tdate).ToString("yyyy-MM-dd");
                }
                catch { }

                DataTable dt = clsFunctions.fillDatatable("Select * From tblHotMixPlant where batchno = " + batchno + " and tdate =#" + tdate + "# and ttime = #" + ttime + "#");
                if (dt.Rows.Count > 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "VIPL ISExist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void FileUpdatePointer(string strDate, string strTime, double BatchNo)   // Function Returns bool values.
        {
            try
            {
                DateTime dtstr = Convert.ToDateTime(strDate.ToString());
                string sDate = dtstr.ToString("yyyy-MM-dd");

                DateTime timestr = Convert.ToDateTime(strTime.ToString());
                string sTime = timestr.ToString("HH:mm:ss");

                string insertquery = "Update BaseTable set Status='Y'  where datevalue(f1) = #" + sDate + "# and  timevalue(f1) = #" + sTime + "# and f2=" + BatchNo + "";

                OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\EXCELUTIL.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=;");
                if (conn.State == ConnectionState.Closed) conn.Open();
                OleDbCommand cmd = new OleDbCommand(insertquery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL FileUpdatePointer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------------

        private void ImportAceessFromExcel(string xlsFileName, string Dir, string strTime)
        {
            dgvloaddetails.Refresh();
            int ok1 = -1;
            System.Data.OleDb.OleDbConnection AccessConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\EXCELUTIL.mdb");

            try
            {
                AccessConnection.Open();
                try
                {
                    Thread.Sleep(200);
                    System.Data.OleDb.OleDbCommand AccessCommanddrp = new System.Data.OleDb.OleDbCommand("Drop table ImportTable ", AccessConnection);
                    AccessCommanddrp.ExecuteNonQuery();
                }
                catch
                {
                    try
                    {       // working
                        System.Data.OleDb.OleDbCommand AccessCommand = new System.Data.OleDb.OleDbCommand("SELECT *  INTO [ImportTable] FROM [Text;FMT=Delimited;DATABASE=" + Dir + ";HDR=No].[" + xlsFileName + "]", AccessConnection);
                        if (ConnectionState.Closed == AccessConnection.State) AccessConnection.Open();
                        AccessCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                try
                {

                    /***************NEW UPDATD CODE***************************/
                    //for testing
                    //DataTable table = ReadCsvToDataTable(Path.Combine(Dir,xlsFileName));
                    string csvPath = Dir; // Example: "C:\\CSVFolder"
                    string fileName = xlsFileName; // Example: "data.csv"

                    //string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Dir};Extended Properties='text;HDR=Yes;FMT=Delimited'";

                    //DataTable rawTable = new DataTable();

                    //using (OleDbConnection conn = new OleDbConnection(connectionString))
                    //{
                    //    conn.Open();
                    //    string query = $"SELECT * FROM [{xlsFileName}]";

                    //    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                    //    {
                    //        adapter.Fill(rawTable);
                    //    }
                    //}

                    /**************************** Solution ***********************************************/

                    //try
                    //{
                    //    Thread.Sleep(200);
                    //    System.Data.OleDb.OleDbCommand AccessCommand2 = new System.Data.OleDb.OleDbCommand("truncate table ImportTable ", AccessConnection);

                    //    AccessCommand2.ExecuteNonQuery();
                    //}
                    //catch { }

                    //try
                    //{
                       //DataTable table = ReadCsvToDataTable(Path.Combine(Dir, xlsFileName));
                    //    // Step 2: Insert all rows into ImportTable
                    //    foreach (DataRow row in table.Rows)
                    //    {
                    //        string columns = string.Join(", ", table.Columns.Cast<DataColumn>().Select(col => col.ColumnName));
                    //        string values = string.Join(", ",
                    //            row.ItemArray.Select(val1 => $"'{val1.ToString().Replace("'", "''")}'")); // escape single quotes

                    //        string insert = $"INSERT INTO ImportTable ({columns}) VALUES ({values})";
                    //        new OleDbCommand(insert, AccessConnection).ExecuteNonQuery();
                    //    }
                    //}
                    //catch
                    //{

                    //}
                    /***********************************************************/

                    System.Data.OleDb.OleDbCommand AccessCommand = new System.Data.OleDb.OleDbCommand("SELECT *  INTO [ImportTable] FROM [Text;FMT=Delimited;DATABASE=" + Dir + ";HDR=No].[" + xlsFileName + "]", AccessConnection); //here inserting data from excel
                    AccessCommand.ExecuteNonQuery();
                }
                catch
                {
                    System.Data.OleDb.OleDbCommand AccessCommanddrp = new System.Data.OleDb.OleDbCommand("Drop table ImportTable ", AccessConnection);
                    AccessCommanddrp.ExecuteNonQuery();
                }

                try
                {
                    Thread.Sleep(200);
                    string abc = "Insert into BaseTable Select * from ImportTable Where timevalue(f1)>=#" + Convert.ToDateTime(strTime).ToString("HH:mm:ss") + "#  and f1 not in(Select f1 from BaseTable)";
                    var query = "INSERT INTO BaseTable SELECT * FROM ImportTable WHERE f1 >= #" + Convert.ToDateTime(strTime).ToString("yyyy-MM-dd HH:mm:ss") + "# AND f1 NOT IN (SELECT f1 FROM BaseTable)";
                    
                    
                    System.Data.OleDb.OleDbCommand AccessCommand1 = new System.Data.OleDb.OleDbCommand(
     "INSERT INTO BaseTable " +
     "SELECT * FROM ImportTable " +
     "WHERE CDate(f1) >= #" + Convert.ToDateTime(strTime).ToString("yyyy-MM-dd HH:mm:ss") + "# " +
     "AND f1 NOT IN (SELECT f1 FROM BaseTable)",
     AccessConnection
 
     //here
                    
    //                System.Data.OleDb.OleDbCommand AccessCommand1 = new System.Data.OleDb.OleDbCommand(
    //"INSERT INTO BaseTable SELECT * FROM ImportTable WHERE f1 >= #" + Convert.ToDateTime(strTime).ToString("yyyy-MM-dd HH:mm:ss") + "# AND f1 NOT IN (SELECT f1 FROM BaseTable)",
    //AccessConnection
);
                    // System.Data.OleDb.OleDbCommand AccessCommand1 = new System.Data.OleDb.OleDbCommand("Insert into BaseTable Select * from ImportTable Where timevalue(f1)>=#" + Convert.ToDateTime(strTime).ToString("HH:mm:ss") + "#  and f1 not in(Select f1 from BaseTable) ", AccessConnection);
                    AccessCommand1.ExecuteNonQuery();
                }
                catch
                {
                    /*********************** Solution ****************/

                    //try
                    //{
                    //    Thread.Sleep(200);
                    //    System.Data.OleDb.OleDbCommand AccessCommand2 = new System.Data.OleDb.OleDbCommand("truncate table ImportTable ", AccessConnection);

                    //    AccessCommand2.ExecuteNonQuery();
                    //}
                    //catch { }



                    //try
                    //{
                    //    DataTable table = ReadCsvToDataTable(Path.Combine(Dir, xlsFileName));
                    //    // Step 2: Insert all rows into ImportTable
                    //    foreach (DataRow row in table.Rows)
                    //    {
                    //        string columns = string.Join(", ", table.Columns.Cast<DataColumn>().Select(col => col.ColumnName));
                    //        string values = string.Join(", ",
                    //            row.ItemArray.Select(val1 => $"'{val1.ToString().Replace("'", "''")}'")); // escape single quotes

                    //        string insert = $"INSERT INTO ImportTable ({columns}) VALUES ({values})";
                    //        new OleDbCommand(insert, AccessConnection).ExecuteNonQuery();
                    //    }
                    //}
                    //catch
                    //{

                    //}

                    /*****************************************************************/

                    System.Data.OleDb.OleDbCommand AccessCommand = new System.Data.OleDb.OleDbCommand("SELECT *  INTO [ImportTable] FROM [Text;FMT=Delimited;DATABASE=" + Dir + ";HDR=No].[" + xlsFileName + "]", AccessConnection);

                    AccessCommand.ExecuteNonQuery();

                    // working
                    System.Data.OleDb.OleDbCommand AccessCommand1 = new System.Data.OleDb.OleDbCommand("Insert into BaseTable Select * from ImportTable Where timevalue(f1)>=#" + Convert.ToDateTime(strTime).ToString("HH:mm:ss") + "# AND CDate(f1) not in(Select f1 from BaseTable) ", AccessConnection);
                    //System.Data.OleDb.OleDbCommand AccessCommand1 = new System.Data.OleDb.OleDbCommand("Insert into BaseTable Select * from ImportTable Where timevalue(f1)>=#" + Convert.ToDateTime(strTime).ToString("HH:mm:ss") + "# and f1 not in(Select f1 from BaseTable) ", AccessConnection);
                    AccessCommand1.ExecuteNonQuery();
                }

                try
                {
                    Thread.Sleep(200);
                    System.Data.OleDb.OleDbCommand AccessCommand2 = new System.Data.OleDb.OleDbCommand("Drop table ImportTable ", AccessConnection);

                    AccessCommand2.ExecuteNonQuery();
                }
                catch { }
                AccessConnection.Close();
                bool val = true; ok1 = 0;
            }
            catch (Exception ex)
            {
                System.Data.OleDb.OleDbConnection AccessConnection1 = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\EXCELUTIL.mdb");
                System.Data.OleDb.OleDbCommand AccessCommand2 = new System.Data.OleDb.OleDbCommand("Drop table ImportTable ", AccessConnection);
                AccessConnection1.Close();

                //clsFunctions.ErrorLog(ex.Message+"Access Import");
                if (ok1 != 0)
                { ok1 = 2; }
                else
                    ImportAceessFromExcel(xlsFileName, Dir, strTime);
                if (ok1 == 2) return;
            }
        }

        //--------------------------------------------------------------------------------------------

        private void LoadDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            //added by dinesh
            //stopping background worker for live data update
            //StopLiveDataUpload();
            //try 
            //{ if(aTimer != null)
            //    if (aTimer.Enabled == true) aTimer.Enabled = false; 
            //} catch { }
            if (!btnstart.Enabled)
            {
                MessageBox.Show("Batch is started cannot close the form. Please stop batch and then try to close the form!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true; // This will cancel the form from closing
                return;
            }

            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
            Stop_backgroundWorker = true;


        }


        public DataTable ReadCsvToDataTable(string csvFilePath)
        {
            DataTable dt = new DataTable();

            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Assuming no headers
                bool firstRow = true;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    // Create columns dynamically (F1, F2, etc.)
                    if (firstRow)
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            dt.Columns.Add("F" + (i + 1));
                        }
                        firstRow = false;
                    }

                    dt.Rows.Add(fields);
                }
            }

            return dt;
        }

        //-------------------------------------------------------------------

        private void refreshBtnTimer_Tick(object sender, EventArgs e)
        {
            // Enable the button after 10 seconds
            button2.Enabled = true;

            // Stop the timer
            refreshBtnTimer.Stop();
        }

        //--------------------------------------------------------------------------------------------

        private void button2_Click(object sender, EventArgs e)      // Refresh
        {
            try
            {
                // Disable the button
                button2.Enabled = false;

                // Start the timer
                refreshBtnTimer.Start();

                //-----------------------------
                //by dinesh on 06/02/2024
                DateTime currentDate = DateTime.Parse(txtdate.Text);
                if (currentDate != DateTime.Now.Date)
                {

                    DateTime nowDate = DateTime.Now;
                    time.Text = "00:00:00";
                    currenttime = nowDate.ToString("HH:mm:ss");
                    txtdate.Text = nowDate.ToString("dd/MM/yyyy");
                }
                /////////////////////////////////////////////////////////////////
                FileNameDateFormat = Convert.ToDateTime(txtdate.Text).ToString("ddMMyyyy");
                try
                {
                    System.Data.OleDb.OleDbConnection AccessConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\EXCELUTIL.mdb");
                    AccessConnection.Open();
                    Thread.Sleep(200);
                    System.Data.OleDb.OleDbCommand AccessCommand1 = new System.Data.OleDb.OleDbCommand("Delete From BaseTable", AccessConnection);
                     AccessCommand1.ExecuteNonQuery();
                    AccessConnection.Close();
                }
                catch { }
                timer1.Enabled = false;
                setTimer();

                //string temppath = Application.StartupPath + "\\tempfile";
                //string xlsFileName = FileNameDateFormat + ".csv";

                //if (File.Exists(temppath + "\\" + xlsFileName))
                //{
                //    /// Delete the file
                //    File.Delete(temppath + "\\" + xlsFileName);
                //}
            }
            catch
            {
                //try
                //{
                //    System.Data.OleDb.OleDbConnection AccessConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\EXCELUTIL.mdb");
                //    AccessConnection.Open();
                //    Thread.Sleep(200);
                //    System.Data.OleDb.OleDbCommand AccessCommand1 = new System.Data.OleDb.OleDbCommand("Delete From BaseTable", AccessConnection);
                //    ////clsFunctions.ErrorLog("Insert into BaseTable Select * from ImportTable Where timevalue(f1)>=#" + strTime + " #  and f1 not in(Select f1 from BaseTable )");
                //    AccessCommand1.ExecuteNonQuery();
                //    AccessConnection.Close();
                //}
                //catch { }
                //timer1.Enabled = false;
                //setTimer();


                //    //------------------------------------
                //    aTimer.Enabled = false;
                //
                //if (button1.Enabled == true)
                //{
                //    getFileFrom();
                //
                //
                //    //RowPointer = RowPointer + 1;
                //
                //    //setCountXLS(RowPointer.ToString());
                //}
                //aTimer.Enabled = true;

                //getFileFrom();
                //string FolderYear;
                //string MonthFolder;

                //FolderYear = DateTime.Now.ToString("yyyy");
                //MonthFolder = DateTime.Now.ToString("MMMM");


                ////string fileName = clsFunctions.DataImportPath + "\\" + FolderYear + "\\" + MonthFolder + "\\" + FileNameDateFormat + ".csv";

                ////string xlsFile = FileNameDateFormat + ".csv";

                //string fileName = clsFunctions.DataImportPath + "\\" + FolderYear + "\\" + MonthFolder + "\\" + FileNameDateFormat + ".csv";

                //string xlsFile = FileNameDateFormat + ".csv";

                //ImportAceessFromExcel(xlsFile,fileName,Convert.ToDateTime(time.Text).ToString("HH:mm:ss"));
            }
        }

        //--------------------------------------------------------------------------------------------

        private void cmbConName_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsFunctions.FillWorkOrderFromContractor(cmbConName, cmbworkname, txtConCode, cmbJobSite);
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            //--------------- 03/07/2024 : BhaveshT -  Shortcut key to enable Date Selection Alt + F7 ---------------

            if (e.Alt && e.KeyCode == Keys.F7)
            {
                if (txtdate.Enabled == true)
                {
                    txtdate.Enabled = false;
                }
                else if (txtdate.Enabled == false)
                {
                    txtdate.Enabled = true;
                }
            }
        }

        //--------------------------------------------------------------------------------------------

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsFunctions.ErrorLog("[INFO] LoadDetails - CLOSE Button Clicked.");
            this.Close();
        }

        //--------------------------------------------------------------------------------------------

        // 22/02/2024 - BhaveshT : To insert blank row at end of Load to insert batchEndFlag = 1

        public void InsertBatchEndRow(int LoadNo, string lastDate, string lastTime, int BatchNo)
        {
            try
            {
                clsFunctions.ErrorLog("[INFO] LoadDetails - Inside InsertBatchEndRow(" + LoadNo + ")");

                //---------------

                if (DateTime.TryParse(lastTime, out DateTime time))
                {
                    time = time.AddSeconds(10);
                    lastTime = time.ToString("hh:mm:ss tt");
                }
                else
                {
                    lastTime = DateTime.Now.ToString("hh:mm:ss tt");
                }
                //-----------------

                string InsertBatchEndRow = "Insert into tblHotMixPlant(regno,plantcode,oprlat,oprlong,oprjurisdiction,oprdivision,oprworkname,"
                           + "tdate,ttime,aggregatetph,bitumenkgmin,fillerkgmin,bitumenper,fillerper,F1_InPer,F2_InPer,F3_InPer,F4_InPer,moisture,mixtemp,exhausttemp,"
                           + "tank1,tank2,tipper,aggregateton,bitumenkg,fillerkg,netmix,batchno,srno,imeino,exportstatus,viplupload,worktype,workcode,material,"
                           + "Batch_Duration_InSec,Weight_KgPerBatch,HB1_KgPerBatch,HB2_KgPerBatch,HB3_KgPerBatch,HB4_KgPerBatch,Aggregate_Kg,Truck_Count,jobsite,loadno, InsertType, batchendflag)"
                           + " values('" + txtConCode.Text + "','" + txtPlantCode.Text + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + cmbworkname.Text + "','"
                           + Convert.ToDateTime(lastDate).ToString("dd/MM/yyyy") + "','" + lastTime + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ","
                           + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ",'" + cmbtipper.Text + "',"
                           + 0 + "," + 0 + "," + 0 + "," + 0 + "," + BatchNo + "," + 0 + ",'" + 0 + "','Y',"
                           + 0 + ",0,'" + lblworkkode.Text + "','" + Cmbmaterialtype.Text + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + LoadNo + ",'" + cmbJobSite.Text + "'," + LoadNo + ", 'A', " + 1 + ")";

                int a = clsFunctions.AdoData(InsertBatchEndRow);

                if (a == 1)
                {
                    clsFunctions.ErrorLog("[INFO] LoadDetails - Inserted Batch End Row for = " + LoadNo + " ");
                }

                else if (a == 0)
                {
                    clsFunctions.ErrorLog("[INFO] LoadDetails - Batch End Row not inserted for = " + LoadNo + " ");
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at InsertBatchEndRow(" + LoadNo + "): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------

        /*************** by Dinesh on 20/03/2025 ***************/
        //private void StartLiveDataUpload()
        //{
        //    try
        //    {
        //        if (!bgWorkerForLiveDataUpload.IsBusy)
        //        {
        //            keepRunning = true;
        //            bgWorkerForLiveDataUpload.RunWorkerAsync();
        //        }
        //    }
        //    catch { }
        //}

        //--------------------------------------------------------------------------------------------

        //private void StopLiveDataUpload()
        //{
        //    try
        //    {
        //        keepRunning = false;
        //        if (bgWorkerForLiveDataUpload.IsBusy)
        //        {
        //            keepRunning = false;
        //            bgWorkerForLiveDataUpload.CancelAsync(); // This will set CancellationPending to true
        //        }

        //        lblisLive.Invoke((MethodInvoker)(() =>
        //        {

        //            {
        //                // Stop blinking and reset to default Silver color
        //                StopBlinking();
        //                lblisLive.ForeColor = Color.Silver;
        //                lblisLive.Text = "Idle";
        //            }
        //        }));
        //    }
        //    catch { }
        //}

        //--------------------------------------------------------------------------------------------

        //By dinesh

        //private async void BgWorkerForLiveDataUpload_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    while (keepRunning)
        //    {
        //        if (bgWorkerForLiveDataUpload.CancellationPending)
        //        {
        //            e.Cancel = true;
        //            break;
        //        }

        //        try
        //        {
        //            if (NetworkHelper.IsInternetAvailable())
        //            {
        //                // Read UI data safely
        //                var live = ReadLiveBitumenDataSafely();
        //                if (live == null)
        //                    continue;
        //                if (live.con_code == 0 || live.plant_code == 0 || string.IsNullOrEmpty(live.work_code) || string.IsNullOrEmpty(live.tipper_no) || live.opr_date == null)
        //                {
        //                    // Log the invalid data with details about why the data is invalid
        //                    await commonClass.ErrorLogAsync($"Invalid live data found. Skipping live data upload. " +
        //                                                    $"con_code: {live.con_code}, plant_code: {live.plant_code}, " +
        //                                                    $"work_code: {live.work_code}, tipper_no: {live.tipper_no}, " +
        //                                                    $"opr_date: {live.opr_date}");
        //                    continue;
        //                }

        //                // Sending live data asynchronously
        //                LiveBitumen liveBitumen = new LiveBitumen();
        //                var result = await liveBitumen.SendLiveDataOfBitumenAsync(live);
        //                // Update the label color based on the result
        //                lblisLive.Invoke((MethodInvoker)(() =>
        //                {
        //                    if (result)
        //                    {
        //                        // Start blinking in red
        //                        StartBlinking();
        //                    }
        //                    else
        //                    {
        //                        // Stop blinking and reset to default Silver color
        //                        StopBlinking();
        //                        lblisLive.ForeColor = Color.Silver;
        //                        lblisLive.Text = "Idle";
        //                    }
        //                }));
        //                // Wait for 1 second before next iteration
        //                //await Task.Delay(1000);
        //            }
        //            else
        //            {
        //                lblisLive.Invoke((MethodInvoker)(() =>
        //                {

        //                        lblisLive.ForeColor = Color.Silver;
        //                        lblisLive.Text = "No Internet..!";

        //                }));
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            await commonClass.ErrorLogAsync($"Error in BgWorkerForLiveDataUpload: {ex.Message}");
        //        }
        //        await Task.Delay(20000);
        //    }
        //}

        //--------------------------------------------------------------------------------------------

        // Helper method to safely read UI data
        //private LiveBitumenModel ReadLiveBitumenDataSafely()
        //{
        //    try
        //    {
        //        if (InvokeRequired)
        //        {
        //            return (LiveBitumenModel)Invoke(new Func<LiveBitumenModel>(ReadLiveBitumenDataSafely));
        //        }

        //        return new LiveBitumenModel
        //        {
        //            con_code = LiveBitumen.SafeParseInt(txtConCode?.Text),
        //            plant_code = LiveBitumen.SafeParseInt(txtPlantCode?.Text),
        //            work_code = lblwid?.Text?.Trim() ?? "NA",
        //            batch_id = LiveBitumen.SafeParseInt(txtBatchNo?.Text),
        //            opr_work_name = cmbworkname?.Text?.Trim() ?? "NA",
        //            opr_date = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"),
        //            opr_time = DateTime.Now.ToString("HH:mm:ss"),
        //            //opr_time = LiveBitumen.SafeParseDate(txtTime?.Text).ToString("HH:mm:ss"),
        //            tipper_no = cmbtipper?.Text?.Trim() ?? "NA",
        //            f1 = 0.0,
        //            f2 = 0.0,
        //            f3 = 0.0,
        //            f4 = 0.0,
        //            filler_inkg = LiveBitumen.SafeParseDouble(txtFiller?.Text),
        //            bituman_inkg = LiveBitumen.SafeParseDouble(txtAsphalt?.Text),
        //            bituman_percent = 0.0,
        //            netmix_ton = 0.0,
        //            tank1_temp = LiveBitumen.SafeParseDouble(txtTank1?.Text),
        //            tank2_temp = 0.0,//LiveBitumen.SafeParseDouble(txtHotBinTemp?.Text),
        //            exhaust_temp = LiveBitumen.SafeParseDouble(txtSmoke?.Text),
        //            mix_temp = LiveBitumen.SafeParseDouble(txtMixMatTemp?.Text),
        //            sr_no = LiveBitumen.SafeParseInt(txtBatchNo?.Text),
        //            filler_percent = 0.0,
        //            truck_count = L_LoadNo,
        //            work_type = "0",
        //            material_type = Cmbmaterialtype?.Text?.Trim() ?? "NA",
        //            device_imei_no = clsFunctions.activeDeviceID,
        //            moisture_percent = 0.0,
        //            aggregate_ton = LiveBitumen.SafeParseDouble(aggregatetph_val),
        //            aggregate_intph = LiveBitumen.SafeParseDouble(aggregatetph_val),
        //            bituman_inkgpermin = LiveBitumen.SafeParseDouble(bitumenkgmin_val),
        //            filler_inkgpermin = LiveBitumen.SafeParseDouble(fillerkgmin_val),
        //            batch_duration_insec = LiveBitumen.SafeParseInt(txtBatchDuration?.Text),
        //            weight_kgperbatch = Convert.ToDouble(Weight_KgPerBatch),
        //            hb1_kgperbatch = LiveBitumen.SafeParseDouble(txtHB1?.Text),
        //            hb2_kgperbatch = LiveBitumen.SafeParseDouble(txtHB2?.Text),
        //            hb3_kgperbatch = LiveBitumen.SafeParseDouble(txtHB3?.Text),
        //            hb4_kgperbatch = LiveBitumen.SafeParseDouble(txtHB4?.Text),
        //            aggregate_kg = LiveBitumen.SafeParseDouble(txtAggWt?.Text),
        //            batch_end_flag = 0,
        //            site_name = cmbJobSite?.Text?.Trim() ?? "NA",
        //            data_insert = "A"
        //        };
        //    }catch(Exception ex)
        //    {
        //        commonClass.ErrorLogAsync("at:ReadLiveBitumenDataSafely() " + ex.Message);
        //        return null;

        //    }
        //}

        //--------------------------------------------------------------------------------------------

        // Start the blinking effect
        private void StartBlinking()
        {
            try
            {
                if (blinkTimer == null) InitializeBlinkTimer();
                isBlinking = true;
                lblisLive.Text = "Live..!";
                lblisLive.ForeColor = Color.Red;
                blinkTimer.Start();
            }
            catch { }
        }

        // Stop the blinking effect
        private void StopBlinking()
        {
            try
            {
                isBlinking = false;
                blinkTimer?.Stop();
                lblisLive.Visible = true; // Ensure visibility when stopped
            }
            catch { }
        }

        //--------------------------------------------------------------------------------------------

        // Initialize the blinking Timer
        private void InitializeBlinkTimer()
        {
            try
            {
                blinkTimer = new System.Windows.Forms.Timer();
                blinkTimer.Interval = 500; // Blink every 500ms (0.5 seconds)
                blinkTimer.Tick += (s, e) => ToggleBlink();
            }
            catch { }
        }

        // Toggle the label's visibility for the blinking effect
        private void ToggleBlink()
        {
            try
            {
                if (isBlinking)
                {
                    lblisLive.Visible = !lblisLive.Visible;
                }
            }
            catch { }
        }

        //--------------------------------------------- For Testing -----------------------------------------------

        /************ ONLY FOR TESTING ************/
        private void InitializeTimer()
        {
            dataFillTimer = new System.Windows.Forms.Timer();
            dataFillTimer.Interval = 500; // 500 milliseconds = 0.5 seconds
            dataFillTimer.Tick += DataFillTimer_Tick;
            dataFillTimer.Start();
        }

        private void DataFillTimer_Tick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateTextBoxes));
            }
            else
            {
                UpdateTextBoxes();
            }
        }

        //--------------------------------------------- For Testing -----------------------------------------------

        private void UpdateTextBoxes()
        {
            // Populate the textboxes with random test data
            //txtConCode.Text = new Random().Next(1000, 9999).ToString();
            //txtPlantCode.Text = new Random().Next(1, 100).ToString();
            //lblwid.Text = "Work_" + new Random().Next(1, 50);
            ///todaysmaxbatchno.Text = new Random().Next(1, 500).ToString();
            //cmbworkname.Text = "Project_" + new Random().Next(1, 20);
            //txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //cmbtipper.Text = "Tipper_" + new Random().Next(1, 100);
            txtFiller.Text = (new Random().NextDouble() * 10).ToString("0.00");
            txtAsphalt.Text = (new Random().NextDouble() * 10).ToString("0.00");

            //-------------------- NORMAL TEMPERATURE ------------------

            //txtTank1.Text = (new Random().Next(120, 180)).ToString();
            //txtTank2.Text = (new Random().Next(120, 180)).ToString();
            //txtSmoke.Text = (new Random().Next(50, 200)).ToString();
            //txtMixMatTemp.Text = (new Random().Next(100, 200)).ToString();

            //-------------------- HIGH TEMPERATURE ------------------

            //txtTank1.Text = (new Random().Next(320, 480)).ToString();
            //txtTank2.Text = (new Random().Next(320, 480)).ToString();
            //txtSmoke.Text = (new Random().Next(340, 400)).ToString();
            //txtMixMatTemp.Text = (new Random().Next(300, 400)).ToString();

            //-------------------- LOW HIGH TEMPERATURE ------------------

            txtTank1.Text = (new Random().Next(-5, 20)).ToString();
            txtSmoke.Text = (new Random().Next(250, 270)).ToString();
            txtMixMatTemp.Text = (new Random().Next(3, 12)).ToString();

            //---------------------------------------------------------

            txtBatchDuration.Text = new Random().Next(30, 300).ToString();
            txtHB1.Text = (new Random().NextDouble() * 10).ToString("0.00");
            txtHB2.Text = (new Random().NextDouble() * 10).ToString("0.00");
            txtHB3.Text = (new Random().NextDouble() * 10).ToString("0.00");
            txtHB4.Text = (new Random().NextDouble() * 10).ToString("0.00");
            txtAggWt.Text = (new Random().NextDouble() * 100).ToString("0.00");
            //cmbJobSite.Text = "Site_" + new Random().Next(1, 10);
            //Cmbmaterialtype.Text = "Material_" + new Random().Next(1, 5);

            TempValidatorAlert();
        }

        //--------------------------------------------------------------------------------------------

        public void TempValidatorAlert()
        {
            try
            {
                clsBTdata.ValidateTemperature(txtMixMatTemp, lb_validateMixTemp, clsVariables.minMixTemp, clsVariables.maxMixTemp);
                clsBTdata.ValidateTemperature(txtTank1, lb_validateBitumenTemp, clsVariables.minBitumenTemp, clsVariables.maxBitumenTemp);
                clsBTdata.ValidateTemperature(txtSmoke, lb_validateExhaustTemp, clsVariables.minExhaustTemp, clsVariables.maxExhaustTemp);
            }
            catch (Exception ex)
            {

            }
        }

        //--------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------


    }
}
