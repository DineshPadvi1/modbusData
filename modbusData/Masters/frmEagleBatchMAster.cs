using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.Masters;

namespace Uniproject
{
    public partial class frmEagleBatchMAster : Form
    {
        static OleDbConnection conn = new OleDbConnection(clsFunctions.GetConnectionstrSetup());
        int batchno = 0;
        int loadno = 0;
        int preloadno = 0;
        int srno = 0;
        public static string heading = "";

        public  frmEagleBatchMAster()
        {
            InitializeComponent();

            DataTable st = clsFunctions.fillDatatable_setup("Select Distinct regno,plantcode from PlantSetup where status <> 'InValid'");

            this.Text = "Batch Mix plant";

            if (st.Rows.Count > 0)
            {
                //later work
                this.Text = "BMP Version = " + st.Rows[0]["imeino"].ToString() + "         For Contarctor M/s." + st.Rows[0]["SOAPend"].ToString() + "        Plant Code=" + st.Rows[0]["plantcode"].ToString() + "        Contractor Code =" + st.Rows[0]["regno"].ToString();
                heading = this.Text;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
             

        private void minimizeTSMI_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

              private void frmEagleBatchMAster_Load(object sender, EventArgs e)
              {

                  if (clsFunctions.IsEgaleReg.Contains("expired"))
                  {
                      lblexpire.Text = clsFunctions.IsEgaleReg;
                  }
                  else
                  {
                       lblexpire.Text = "";
                       batchno = clsFunctions.GetMaxId("select Max(loadno) from tblHotMixPlant");
                       srno = clsFunctions.GetMaxId("select Max(srno) from tblHotMixPlant where loadno=" + batchno);
                       //SaveDataToHotMixScada();           //Comment by bhavesh          
                       //timer1.Enabled = true;
                  }
              }

              private void plantInformationToolStripMenuItem_Click(object sender, EventArgs e)
              {
                  //PlantMaster pm = new PlantMaster();
                  //pm.ShowDialog();
              }

              private void designMixToolStripMenuItem_Click(object sender, EventArgs e)
              {
                  WorkMaster wm = new WorkMaster();
                  wm.ShowDialog();
              }

              private void tripperInformationToolStripMenuItem_Click(object sender, EventArgs e)
              {
                  TipperMaster tm = new TipperMaster();
                  tm.ShowDialog();
              }

              private void startToolStripMenuItem_Click(object sender, EventArgs e)
              {
                  string type = clsFunctions.GetType();

                  if (type.ToUpper().Contains("TCP"))
                  {
                      //LoadDetailsTCPIP_old ld = new LoadDetailsTCPIP_old();
                      //ld.ShowDialog();
                      //ld.BringToFront();
                  }
                  else if (type.ToUpper().Contains("PROTOOL"))
                  {
                      LoadDetails ld = new LoadDetails();
                      ld.ShowDialog();
                      ld.BringToFront();
                  }
                  else if (type.ToUpper().Contains("HTML"))
                     {

                        

                //BatchDataHTML ld = new BatchDataHTML();
                //        ld.ShowDialog();
                //        ld.BringToFront();
            }
                  else if (type.ToUpper().Contains("EXCEL"))
                  {
                      //LoadDetails ld = new LoadDetails();
                      //ld.ShowDialog();
                      //ld.BringToFront();

                        frmEagleBatchMAster mdiForm = new frmEagleBatchMAster();

                // This automatically adds form2 into form1's MdiChildren collection
                        LoadDetails ld = new LoadDetails();
                ld.MdiParent = mdiForm;
            }
                  else if (type.ToUpper().Contains("JAGSON"))
                  {

                  }
                  else
                  {
                      MessageBox.Show("Please check Path Setting of Setup Default Form Will Load ");
                      LoadDetails ld = new LoadDetails();
                      ld.ShowDialog();
                      ld.BringToFront();

                  }
                  
              }

        // 29/12/2023 : BhaveshT - Created Function to add new column if not exist.
        public static void checknewcolumn(string clm, string datatype, string table)
        {
            using (OleDbConnection conn1 = new OleDbConnection(clsFunctions.GetConnectionstrSetup()))
            {
                if (conn1.State == ConnectionState.Closed) conn1.Open();
                DataTable dt = new DataTable();
                try
                {
                    string cmdText = "Select " + clm + " from " + table + " ";
                    if (conn1.State == ConnectionState.Closed)
                        conn1.Open();
                    new OleDbDataAdapter(new OleDbCommand(cmdText, conn1)).Fill(new DataTable());
                }
                catch (Exception ex)
                {
                    string addColumn = "alter table " + table + " add column " + clm + " " + datatype;

                    string setZeroValue = "alter table " + table + " alter column " + clm + " SET DEFAULT 0 ";

                    string makeZero = "Update " + table + " SET " + clm + " = 0 WHERE " + clm + " IS NULL;";


                    // Add Column
                    if (conn1.State == ConnectionState.Closed)
                        conn1.Open();
                     new OleDbCommand(addColumn, conn1).ExecuteNonQuery();

                    // Set DEFAULT value 0
                    if (conn1.State == ConnectionState.Closed)
                        conn1.Open();
                    new OleDbCommand(setZeroValue, conn1).ExecuteNonQuery();

                    // Make all existing 0 where it is NULL
                    if (conn1.State == ConnectionState.Closed)
                        conn1.Open();
                    new OleDbCommand(makeZero, conn1).ExecuteNonQuery();

                }
            }
        }


        public static DataTable CheckNewColumn(string query)
        {
            using (OleDbConnection conn1 = new OleDbConnection(clsFunctions.GetConnectionstrSetup()))
            {
                if (conn1.State == ConnectionState.Closed) conn1.Open();
                DataTable dt = new DataTable();
                try
                {
                    OleDbCommand cmd = new OleDbCommand(query, conn1);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error at:- Filldatatable " + ex.Message);
                }
                return dt;
            }
        }

        public static DataTable OledbfillDatatable(string query)
        {
            using (OleDbConnection conn1 = new OleDbConnection(clsFunctions.GetConnectionstrSetup()))
            {
                if (conn1.State == ConnectionState.Closed) conn1.Open();
                DataTable dt = new DataTable();
                try
                {
                    OleDbCommand cmd = new OleDbCommand(query, conn1);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    if(query.Contains("FILLER2"))
                        clsFunctions.protoolFlag = true;
                    //else
                    //    clsFunctions.protoolFlag = false;
                }
                catch (Exception ex)
                {
                    clsFunctions.protoolFlag = true;
                    //goto a;
                    //MessageBox.Show("Error at:- Filldatatable " + ex.Message);
                    return dt;
                }
                return dt;
            }
        }

        //------------------------------
        
        //-----------------------------

              private void SaveDataToHotMixScada()
              {
                
                  try
                  {
                      string regno = "999";
                      string plantcode = "81";
                      string oprlat = "18.6720225";
                      string oprlong = "73.8138366";
                      string oprjurisdiction ="Pune";
                      string oprdivision = "Pune";
                      string oprworkname = "";
                      string imeino = "";
                      string exportstatus = "N";
                      int viplupload = 0;
                      double netmixa=0;

                     // DataTable dt1 = OledbfillDatatable("SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO,f1,f2,f3,f4,fillerper,bitumenper FROM Production where LOAD_NO<>0 and LOAD_NO>=" + batchno + " and BATCH_NO>" + srno + " order by LOAD_NO,BATCH_NO");//Annu
                      DataTable dt1 = OledbfillDatatable("SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production where LOAD_NO<>0 and LOAD_NO>=" + batchno + " and BATCH_NO>" + srno + " order by LOAD_NO,BATCH_NO");
                      if (dt1.Rows.Count == 0) {

                          batchno++;
                          srno = 0;
                         // dt1 = OledbfillDatatable("SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO, f1,f2,f3,f4,fillerper,bitumenper FROM Production where LOAD_NO<>0 and LOAD_NO>=" + batchno + " and BATCH_NO>" + srno + " order by LOAD_NO,BATCH_NO");//Annu
                          dt1 = OledbfillDatatable("SELECT TIME1,TOTALWT,BATCHWT,TRUCK_COUNT,CYCLETIME,BATCH_NO,LOAD_NO,DATE1,ASPHALT,FILLER1,AG1,AG2,AG3,AG4,CH4_TEMP,CH3_TEMP,CH2_TEMP,CH5_TEMP,CH7_TEMP,TRUCK_NO FROM Production where LOAD_NO<>0 and LOAD_NO>=" + batchno + " and BATCH_NO>" + srno + " order by LOAD_NO,BATCH_NO");
                    
                      }
                      
                      foreach (DataRow row in dt1.Rows)
                      {
                          string ttime1 = Convert.ToDateTime(row["TIME1"]).ToString("HH:mm:ss");
                          srno = Convert.ToInt16(row["BATCH_NO"].ToString());
                          loadno = Convert.ToInt16(row["LOAD_NO"].ToString()); 
                          if (preloadno != loadno)
                          {
                              batchno = clsFunctions.GetMaxId("select Max(batchno)+1 from tblHotMixPlant");
                          }
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
                         
                         // comment by Annu as f1 to bitu not in client protool db
                          //double F1_InPer = Convert.ToDouble(row["f1"]);
                          //double F2_InPer = Convert.ToDouble(row["f2"]);
                          //double F3_InPer = Convert.ToDouble(row["f3"]);
                          //double F4_InPer = Convert.ToDouble(row["f4"]);
                          //double fillerper = Convert.ToDouble(row["fillerper"]);
                          //double bitumenper = Convert.ToDouble(row["bitumenper"]);

                          //double totalWgKg = F1_InPer + F2_InPer + F3_InPer + F4_InPer + fillerper + bitumenper;

                          //F1_InPer = Math.Round((F1_InPer * 100) / totalWgKg, 2);
                          //F2_InPer = Math.Round((F2_InPer * 100) / totalWgKg, 2);
                          //F3_InPer = Math.Round((F3_InPer * 100) / totalWgKg, 2);
                          //F4_InPer = Math.Round((F4_InPer * 100) / totalWgKg, 2);
                          //fillerper = Math.Round((fillerper * 100) / totalWgKg, 2);
                          //bitumenper = Math.Round((bitumenper * 100) / totalWgKg, 2);

                          //if (Double.IsNaN(F1_InPer)) F1_InPer = 0;
                          //if (Double.IsNaN(F2_InPer)) F2_InPer = 0;
                          //if (Double.IsNaN(F3_InPer)) F3_InPer = 0;
                          //if (Double.IsNaN(F4_InPer)) F4_InPer = 0;
                          //if (Double.IsNaN(fillerper)) fillerper = 0;
                          //if (Double.IsNaN(bitumenper)) bitumenper = 0;
                          // comment end 

                          // changes added by annu 
                          double F1_InPer = 0;
                          double F2_InPer = 0;
                          double F3_InPer = 0;
                          double F4_InPer = 0;
                          double fillerper =0;
                          double bitumenper = 0;
                          //
                          double moisture = Convert.ToDouble(0);
                          double mixtemp = Convert.ToDouble(row["CH4_TEMP"]);
                          double exhausttemp = Convert.ToDouble(row["CH2_TEMP"]);
                          double tank1 = Convert.ToDouble(row["CH5_TEMP"]);
                          double tank2 = Convert.ToDouble(row["CH7_TEMP"]);
                          string tipper = row["TRUCK_NO"].ToString();
                          double aggregateton = Convert.ToDouble(0);                   
                         
                         
                         // netmixa = netmixa + Weight_KgPerBatch;
                          double netmix = Weight_KgPerBatch / 1000;

                          int Batch_Duration_InSec = Convert.ToInt32(row["CYCLETIME"].ToString());                          
                          double Aggregate_Kg = Convert.ToDouble(row["BATCHWT"].ToString());
                          int truck_count = Convert.ToInt32(row["TRUCK_COUNT"].ToString());

                          string insertquery = "Insert into tblHotMixPlant(regno,plantcode,oprlat,oprlong,oprjurisdiction,oprdivision,oprworkname,"
                             + "tdate,ttime,aggregatetph,bitumenkgmin,fillerkgmin,bitumenper,fillerper,F1_InPer,F2_InPer,F3_InPer,F4_InPer,moisture,mixtemp,exhausttemp,"
                             + "tank1,tank2,tipper,aggregateton,bitumenkg,fillerkg,netmix,batchno,srno,imeino,exportstatus,viplupload,worktype,"
                             + "Batch_Duration_InSec,Weight_KgPerBatch,HB1_KgPerBatch,HB2_KgPerBatch,HB3_KgPerBatch,HB4_KgPerBatch,Aggregate_Kg,Truck_Count,loadno)"
                             + " values('" + regno + "','" + plantcode + "','" + oprlat + "','" + oprlong + "','" + oprjurisdiction + "','" + oprdivision + "','" + oprworkname + "','"
                             + tdate + "','" + ttime + "'," + aggregatetph + "," + bitumenkgmin + "," + fillerkgmin + "," + bitumenper + "," + fillerper + "," + F1_InPer + ","
                             + F2_InPer + "," + F3_InPer + "," + F4_InPer + "," + moisture + "," + mixtemp + "," + exhausttemp + "," + tank1 + "," + tank2 + ",'" + tipper + "',"
                             + aggregateton + "," + bitumenkg + "," + fillerkg + "," + netmix + "," + batchno + "," + srno + ",'" + imeino + "','" + exportstatus + "',"
                             + viplupload + ",0," + Batch_Duration_InSec + "," + Weight_KgPerBatch + "," + F1_Inkg + "," + F2_Inkg + "," + F3_Inkg + "," + F4_Inkg + "," + Aggregate_Kg + "," + truck_count + "," + loadno + ")";

                          clsFunctions.AdoData(insertquery);

                          string tempquery = insertquery.Replace("tblHotMixPlant", "bkp");
                          clsFunctions.AdoData(tempquery);
                          preloadno = loadno;
                      }                                      
                     
                   }
                  catch (Exception ex)
                  {
                      MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);                     

                  }


              }

              private void timer1_Tick(object sender, EventArgs e)
              {
                  //if (clsFunctions.desc.ToUpper().Contains("Protool"))
                  //{
                  //    if (backgroundWorker1.IsBusy) { }
                  //    else
                  //    {
                  //        backgroundWorker1.RunWorkerAsync();
                  //    }
                  //}
              }

              private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
              {
                  LoadDetails loadObj = new LoadDetails();
                  //SaveDataToHotMixScada();
                 loadObj.GetRecordsFromProtool();
              }

              //private void recipeMasterToolStripMenuItem_Click(object sender, EventArgs e)
              //{
              //    BTRecipeMaster2 Rm = new BTRecipeMaster2();
              //    Rm.ShowDialog();
              //}

              private void reportToolStripMenuItem_Click(object sender, EventArgs e)
              {
                  //BMPScadaReport obj1 = new BMPScadaReport();
                  //obj1.ShowDialog();
              }

            
    }
}
