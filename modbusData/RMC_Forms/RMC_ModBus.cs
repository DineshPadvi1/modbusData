//using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using Serilog;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Dapper;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Data.Common;
using Uniproject.Classes;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using System.Threading;
using Uniproject.Classes.Model;
using Uniproject.RMC_forms.Masters;
using Uniproject.Masters;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using Uniproject;

namespace PDF_File_Reader
{
    public partial class RMC_ModBus : Form
    {
        public static string sqliteDbPath = System.IO.Path.Combine(Application.StartupPath, "Database\\UniproData.db");
        private BackgroundWorker backgroundWorker;
   
        ConcurrentDictionary<string, object> dataDict = new ConcurrentDictionary<string, object>();  // Dictionary to store data

        // Remove BackgroundWorker from declarations
        // private BackgroundWorker backgroundWorker;
        clsVariables clsVar = new clsVariables();

        // varialbe for sub batch end
        int subBatchEnd = 1;
        //clsVarTuskar clsVarT = new clsVarTuskar();
        public RMC_ModBus()
        {
            InitializeComponent();

            // Setup Serilog
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
            backgroundWorker.WorkerSupportsCancellation = true ;

           


        }
        private void RMC_ModBus_Load_2(object sender, EventArgs e)
        {
            //lblActual.Text = "A\nC\nT\nU";
            btnstop.Enabled = false;
            try
            {
                if (backgroundWorker.IsBusy)
                {
                    //MessageBox.Show("Background worker is already running.");
                    return;
                }
                backgroundWorker.RunWorkerAsync();
            }
            catch { }
            
        txtRDMBatchNO.Text = clsFunctions_comman.GetMaxId("Select MAX(Batch_No) from Batch_Transaction").ToString();
            
            loadComboData();
            addNameSetupToDGV();
        }

        private async Task RMC_ModBus_Load()
        {
            try
            {
                var dt = await LoadDataTo_DataTableAsync();
                await ConvertDataAsync(dt);
                 
            }
            catch (Exception ex)
            {
                Log.Error("at RMC_ModBus_Load: {Message}", ex.Message);
            }
        }

        public bool Stop_backgroundWorker = false;
        private async Task ConvertDataAsync(DataTable dt)  
        {
            try
            {
                //var tasks = new List<Task>();
                foreach (DataRow row in dt.Rows)
                {
                    await Float_Big_Endian_ABCDAsync(
                        string.Join(" ", row["data"]),
                        row["date"].ToString(),
                        row["length"].ToString(),
                        Convert.ToInt32(row["id"])
                    );
                     Task.Delay(100).Wait();

                    if (Stop_backgroundWorker)
                    {
                        Console.WriteLine("BackgroundWorker is stopped!!!");
                        break;
                    }
                }



                /************************************ DO NOT DELETE THIS CODE *******************************************/
                #region
                //if(dt.Rows.Count<=0 && GetRowCountThreadSafe(dgv1)>0)
                //{
                //    // for whole batch end.

                //    if (flagForTrans)
                //    {
                //        string query = "Update Batch_Transaction set Balance_Wtr='1' " +
                //                    "WHERE Batch_Index = (SELECT MAX(Batch_Index) FROM Batch_Transaction WHERE Batch_No = " + GetTextSafe(txtRDMBatchNO) + ") " +
                //                    "AND Batch_No = " + GetTextSafe(txtRDMBatchNO);

                //        await clsFunctions.AdoDataAsync(query);




                //        DataTable dt1 = new DataTable();
                //        dt1 = clsFunctions_comman.fillDatatable(
                //                                "SELECT TOP 1 * FROM Batch_Transaction " +
                //                                "WHERE Batch_Index = (SELECT MAX(Batch_Index) FROM Batch_Transaction WHERE Batch_No = " + GetTextSafe(txtRDMBatchNO) + ") " +
                //                                "AND Batch_No = " + GetTextSafe(txtRDMBatchNO) + ""
                //                            );
                //        DataRow row1 = dt1.Rows[0]; // Since you're using TOP 1



                //        DataTable dt2 = new DataTable();
                //        dt2 = clsFunctions_comman.fillDatatable(
                //                                "SELECT TOP 1 * FROM Batch_Transaction " +
                //                                "WHERE Batch_Index = (SELECT MIN(Batch_Index) FROM Batch_Transaction WHERE Batch_No = " + GetTextSafe(txtRDMBatchNO) + ") " +
                //                                "AND Batch_No = " + GetTextSafe(txtRDMBatchNO) + ""
                //                            );




                //        DataRow row2 = dt2.Rows[0]; // Since you're using TOP 1


                //        string Batch_End_Time = Convert.ToDateTime(row1["Batch_Time"]).ToString("HH:mm:ss");


                //        string insertToTransQuery = "insert into Batch_Dat_Trans (Batch_No,Batch_Date,Batch_Time,Batch_Time_Text,Batch_Start_Time,Batch_End_Time,Batch_Year,Batcher_Name,Batcher_User_Level,Customer_Code,Recipe_Code,"
                //          + "Recipe_Name,Mixing_Time,Mixer_Capacity,strength,Site,Truck_No,Truck_Driver,Production_Qty,Ordered_Qty,Returned_Qty,WithThisLoad,Batch_Size,Order_No,Schedule_Id,Gate1_Target,"
                //          + "Gate2_Target,Gate3_Target,Gate4_Target,Gate5_Target,Gate6_Target,Cement1_Target,Cement2_Target,Cement3_Target,Cement4_Target,Filler_Target,Water1_Target,slurry_Target,Water2_Target,"
                //          + "Silica_Target,Adm1_Target1,Adm1_Target2,Adm2_Target1,Adm2_Target2,Cost_Per_Mtr_Cube,Total_Cost,Plant_No,Weighed_Net_Weight,Weigh_Bridge_Stat,tExportStatus,tUpload1,tUpload2, WO_Code, Cust_Name, Site_ID, InsertType) values('" + GetTextSafe(txtRDMBatchNO)
                //          + "','" + Convert.ToDateTime(row2["Batch_Date"]).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(row2["Batch_Time"]).ToString("hh:mm:ss tt") + "','" + Convert.ToDateTime(row2["Batch_Time"]).ToString("hh:mm:ss tt") + "','" + Convert.ToDateTime(row2["Batch_Time"]).ToString("hh:mm:ss tt") + "','" + Convert.ToDateTime(Batch_End_Time).ToString("hh:mm:ss tt") + "','" + row2["Batch_Year"]
                //          + "','" + 0 + "','" + 0 + "','" + GetTextSafe(cmbContractor) + "','" + GetTextSafe(cmbRecipe) + "','" + GetTextSafe(cmbRecipe) + "','" + /*clsVar.Mixing_Time*/ 0
                //          + "','" + GetTextSafe(txtMixerCapacity) + "','" + /*clsVar.strength*/0 + "','" + GetTextSafe(cmbjobsite) + "','" + GetTextSafe(cmbRDMVehicle) + "','" + "NA" + "','" + /*clsVar.Production_Qty*/0
                //          + "','" + /*clsVar.Ordered_Qty*/0 + "','" + /*clsVar.Returned_Qty*/0 + "','" + /*clsVar.WithThisLoad*/0 + "','" + /*Batch_Size*/GetTextSafe(txtbatchsize) + "','" + /*clsVar.Order_No*/0 + "','" +/* clsVar.Schedule_Id */0 + "','" + /*Agg1_Target*/GetTextSafe(txtagg1)
                //          + "','" + /*Agg2_Target*/GetTextSafe(txtagg2) + "','" + /*Agg3_Target*/GetTextSafe(txtagg3) + "','" + /*Agg4_Target*/GetTextSafe(txtagg4) + "','" + 0 + "','" + 0 + "','" + /*Cem1_Target*/GetTextSafe(txtCement1)
                //          + "','" + /*Cem2_Target*/GetTextSafe(txtCement2) + "','" + /*Cem3_Target*/GetTextSafe(txtCement3) + "','" + /*Cem4_Target*/GetTextSafe(txtCement4) + "','" + /*clsVar.Filler_Target*/0 + "','" + /*Water1_Target*/GetTextSafe(txtwater1) + "','" + /*clsVar.slurry_Target*/0
                //          + "','" + /*Water2_Target*/GetTextSafe(txtwater2) + "','" + /*clsVar.Silica_Target*/0 + "','" + /*admix11_Target*/GetTextSafe(txtadmix1) + "','" + /*admix12_Target*/GetTextSafe(txtadmix2) + "','" +/* Convert.ToDouble(rowForTrans["ADMIX3TRG"])*/0 + "','" + /*clsVar.Adm2_Target2*/0
                //          + "','" + /*clsVar.Cost_Per_Mtr_Cube*/0 + "','" + /*clsVar.Total_Cost*/0 + "','" + GetTextSafe(txtplantcode) + "','" + /*clsVar.Weighed_Net_Weight*/0 + "','" + /*clsVar.Weigh_Bridge_Stat*/0 + "','N','0','0','" + GetTextSafe(txtworkid) + "','" + GetTextSafe(cmbContractor) + "',0, 'A')";


                //        await clsFunctions.AdoDataAsync(insertToTransQuery);


                //        flagForTrans = false;
                //        subBatchEnd = 1;
                //    }

                //}

                /******************************************************************************************************************************/
                #endregion
                //await Task.WhenAll(tasks);  
            }
            catch (Exception ex)
            {
                Log.Error("ConvertData error: {Message}", ex.Message);
            }
        }


        public async Task Float_Big_Endian_ABCDAsync(string input, string datetime1, string length, int id)
        {
            try
            {
                if (length == "179")
                {
                    string[] hexBytes = input.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    byte[] bytes = new byte[hexBytes.Length];
                    for (int i = 0; i < hexBytes.Length; i++)
                    {
                        bytes[i] = byte.Parse(hexBytes[i], NumberStyles.HexNumber);
                    }

                    // Define file path
                    string filePath = Path.Combine(Application.StartupPath, "Float_Big_Endian_ABCD_FloatOutput.txt");

                    List<float> floatValues = new List<float>();

                    /*****************************WORKING COPY***********************************************/
                    //using (StreamWriter writer = new StreamWriter(filePath))
                    //{
                    //await writer.WriteLineAsync("Converted Float Values (Big Endian):\n");

                    ////////for (int i = 0; i < bytes.Length; i += 4)
                    ////////{
                    ////////    if (i + 3 >= bytes.Length) break;

                    ////////    byte[] floatBytes = new byte[4];
                    ////////    floatBytes[0] = bytes[i];
                    ////////    floatBytes[1] = bytes[i + 1];
                    ////////    floatBytes[2] = bytes[i + 2];
                    ////////    floatBytes[3] = bytes[i + 3];

                    ////////    if (BitConverter.IsLittleEndian)
                    ////////        Array.Reverse(floatBytes);

                    ////////    float value = BitConverter.ToSingle(floatBytes, 0);
                    ////////    floatValues.Add(value);

                    ////////    //await writer.WriteLineAsync($"{i / 4:D2}: {value}");
                    ////////}

                    /***************************************************************************************/

                    // Convert the byte array to float values

                    for (int i = 0; i < bytes.Length; i += 4)
                    {
                        byte[] floatBytes = new byte[4];
                        List<string> stringValues = new List<string>();
                        sbyte? twelfthChunkSByteValue = null;  // Store 12th chunk's first byte as sbyte

                        // Check for the 12th chunk (index 44, since chunk index * 4 = byte index)
                        if (i == 44) //f12
                        {
                            Array.Copy(bytes, i, floatBytes, 0, 4);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(floatBytes);

                            // Extract first byte as sbyte (signed)
                            twelfthChunkSByteValue = unchecked((sbyte)floatBytes[0]);

                            stringValues.Add($"12th Chunk (sbyte): {twelfthChunkSByteValue.Value}");
                            Console.WriteLine($"The 12th chunk value (sbyte) is: {twelfthChunkSByteValue.Value}");
                            if (twelfthChunkSByteValue.Value == -125)
                            {
                                //for testing 
                            }
                            // Correctly convert sbyte to float and store
                            floatValues.Add((float)twelfthChunkSByteValue.Value);
                            continue;
                        }


                        // Check for the 13th chunk (index 48, since chunk index * 4 = byte index)
                        if (i == 48) //f13
                        {
                            Array.Copy(bytes, i, floatBytes, 0, 4);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(floatBytes);

                            // Extract first byte as sbyte (signed)
                            twelfthChunkSByteValue = unchecked((sbyte)floatBytes[0]);

                            stringValues.Add($"12th Chunk (sbyte): {twelfthChunkSByteValue.Value}");
                            Console.WriteLine($"The 13th chunk value (sbyte) is: {twelfthChunkSByteValue.Value}");
                            if (twelfthChunkSByteValue.Value == -125)
                            {
                                //for testing 
                            }
                            // Correctly convert sbyte to float and store
                            floatValues.Add((float)twelfthChunkSByteValue.Value);
                            continue;
                        }



                        // Ensure we don't go out of bounds
                        if (i + 3 < bytes.Length)
                        {
                            floatBytes[0] = bytes[i];
                            floatBytes[1] = bytes[i + 1];
                            floatBytes[2] = bytes[i + 2];
                            floatBytes[3] = bytes[i + 3];
                        }
                        else
                        {
                            // Padding for the last chunk
                            int bytesRemaining = bytes.Length - i;
                            for (int j = 0; j < bytesRemaining; j++)
                            {
                                floatBytes[j] = bytes[i + j];
                            }

                            // Add padding (zeros) for the remaining space
                            for (int j = bytesRemaining; j < 4; j++)
                            {
                                floatBytes[j] = 0;
                            }
                        }

                        // Reverse the byte array if the system is little-endian
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(floatBytes);

                        float value = BitConverter.ToSingle(floatBytes, 0);
                        floatValues.Add(value);

                        //await writer.WriteLineAsync($"{i / 4:D2}: {value}");
                    }
                    //}

                    await InsertFloatValuesIntoDatabaseAsync(floatValues, datetime1, length, id);

                    //MessageBox.Show("Float values written to file and database:\n" + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //return;
            }
            catch (Exception ex)
            {
                Log.Error("Float_Big_Endian_ABCD error: {Message}", ex.Message);
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool sub_Batch_Flag = false;
        int counter = 0;
        bool flagForTrans = false;
        string Batch_Start_Time = string.Empty; 
        private async Task InsertFloatValuesIntoDatabaseAsync(List<float> floatValues, string datetime1, string length, int id)
        {
            try
            {
                string datetime11="";

                double length1=0;

                // Aggregates
                double Agg1_Actual = 0;
                double Agg1_Target = 0;

                double Agg2_Actual = 0;
                double Agg2_Target = 0;

                double Agg3_Actual = 0;
                double Agg3_Target = 0;

                double Agg4_Actual = 0;
                double Agg4_Target = 0;

                // Cements
                double Cem1_Actual = 0;
                double Cem1_Target = 0;

                double Cem2_Actual = 0;
                double Cem2_Target = 0;

                double Cem3_Actual = 0;
                double Cem3_Target = 0;

                double Cem4_Actual = 0;
                double Cem4_Target = 0;

                // Water
                double Water1_Actual = 0;
                double Water1_Target = 0;

                double Water2_Actual = 0;
                double Water2_Target = 0;

                // Admixtures
                double admix11_Actual = 0;
                double admix11_Target = 0;

                double admix12_Actual = 0;
                double admix12_Target = 0;

                double Batch_Size = 0;


                //if (length == "119" || length == "87" || length == "65") return;

                // Create DataTable and DataRow
                DataTable dt = new DataTable();
                DataRow row = dt.NewRow();

                string connectionString = $"Data Source={sqliteDbPath};Version=3;";
                using (var connection = new SQLiteConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    var columnNames = new List<string>();
                    var paramNames = new List<string>();

                    // datetime1 and length
                    columnNames.Add("datetime1");
                    paramNames.Add("@datetime1");
                    parameters.Add("@datetime1", datetime1);
                    dt.Columns.Add("datetime1");
                    row["datetime1"] = datetime1;

                    columnNames.Add("length");
                    paramNames.Add("@length");
                    parameters.Add("@length", length);
                    dt.Columns.Add("length");
                    row["length"] = length;

                    for (int i = 0; i < floatValues.Count && i < 200; i++)
                    {
                        string column = $"f{i + 1}";
                        string value = floatValues[i].ToString("G", CultureInfo.InvariantCulture);

                        columnNames.Add(column);
                        paramNames.Add($"@{column}");
                        parameters.Add($"@{column}", value);

                        if (!dt.Columns.Contains(column))
                            dt.Columns.Add(column);
                        row[column] = value;
                    }

                    for (int i = floatValues.Count; i < 200; i++)
                    {
                        string column = $"f{i + 1}";
                        columnNames.Add(column);
                        paramNames.Add($"@{column}");
                        parameters.Add($"@{column}", "");

                        if (!dt.Columns.Contains(column))
                            dt.Columns.Add(column);
                        row[column] = "";
                    }

                    string query = $"INSERT INTO DataForAnalysis ({string.Join(",", columnNames)}) VALUES ({string.Join(",", paramNames)})";
                    await connection.ExecuteAsync(query, parameters);

                    string update = $"UPDATE dataset SET Status=1 WHERE id={id};";
                    await connection.ExecuteAsync(update);

                    Log.Information("DataForAnalysis insertion complete: {Count} floats, datetime1 = {DT}, length = {Len}", floatValues.Count, datetime1, length);
                }


                 
                 
                // Update UI only if safe, no nulls or exceptions
                if (length == "179") // production data
                {

                    // Add the row to DataTable if needed
                    dt.Rows.Add(row);

                    /*********** Auto ***************/

                    //TARGETS

                    // AGGREGATES
                    await UpdateActualValuestoUI(txtagg1, GetSafeValue(row, "f3")); // agg1
                    Agg1_Target = Convert.ToDouble(GetSafeValue(row, "f3"));

                    await UpdateActualValuestoUI(txtagg2, GetSafeValue(row, "f2")); /// agg2
                    Agg2_Target = Convert.ToDouble(GetSafeValue(row, "f2"));


                    await UpdateActualValuestoUI(txtagg3, GetSafeValue(row, "f5"));//GetSafeValue(row, "f4"));
                    Agg3_Target = Convert.ToDouble(GetSafeValue(row, "f5"));


                    await UpdateActualValuestoUI(txtagg4, "0");

                    await UpdateActualValuestoUI(txtagg5, "0");

                    // CEMENT
                    await UpdateActualValuestoUI(txtCement1, GetSafeValue(row, "f6"));
                    Cem1_Target = Convert.ToDouble(GetSafeValue(row, "f6"));


                    await UpdateActualValuestoUI(txtCement2, "0");
                    await UpdateActualValuestoUI(txtCement3, "0");
                    await UpdateActualValuestoUI(txtCement4, "0");

                    // WATER
                    await UpdateActualValuestoUI(txtwater1, GetSafeValue(row, "f7"));
                    Water1_Target = Convert.ToDouble(GetSafeValue(row, "f7"));

                    //batch size
                    await UpdateActualValuestoUI(txtbatchsize, GetSafeValue(row, "f9"));
                    Batch_Size = Convert.ToDouble(GetSafeValue(row, "f9"));
                    /*******************************************************************************************/




                    /*********** ACTUAL ***************/
                    await UpdateActualValuestoUI(txtAGG1_Act, Convert.ToDouble(GetSafeValue(row, "f26")).ToString("0.00"));  // AGG1
                    Agg1_Actual = Convert.ToDouble(GetSafeValue(row, "f26"));
                    dataDict["agg1"] = Agg1_Actual;

                    await UpdateActualValuestoUI(txtAGG2_Act, Convert.ToDouble(GetSafeValue(row, "f25")).ToString("0.00"));  // AGG2
                    Agg2_Actual = Convert.ToDouble(GetSafeValue(row, "f25"));
                    dataDict["agg2"] = Agg2_Actual;

                    await UpdateActualValuestoUI(txtAGG3_Act, Convert.ToDouble(GetSafeValue(row, "f28")).ToString("0.00"));  // CSAND
                    Agg3_Actual = Convert.ToDouble(GetSafeValue(row, "f28"));
                    dataDict["agg3"] = Agg3_Actual;

                    await UpdateActualValuestoUI(txtCEM1_Act, Convert.ToDouble(GetSafeValue(row, "f29")).ToString("0.00"));  // CEM1
                    Cem1_Actual = Convert.ToDouble(GetSafeValue(row, "f29"));
                    dataDict["Cem1"] = Cem1_Actual;

                    await UpdateActualValuestoUI(txtWTR1_Act, Convert.ToDouble(GetSafeValue(row, "f30")).ToString("0.00"));  // Water1
                    Water1_Actual = Convert.ToDouble(GetSafeValue(row, "f30"));
                    dataDict["Water1"] = Water1_Actual;

                    await UpdateActualValuestoUI(txtADM11_Act, "0");  // admix11
                    admix11_Actual = Convert.ToDouble(0);
                    dataDict["Admix11"] = admix11_Actual;

                    await UpdateActualValuestoUI(txtADM12_Act, "0");  // admix22
                    admix12_Actual = Convert.ToDouble(0);
                    dataDict["Admix12"] = admix12_Actual;



                    ///*********** ACTUAL ***************/
                    //UpdateActualValuestoUI(txtAGG1_Act, GetSafeValue(row, "f26"));  //AGG1
                    //Agg1 = Convert.ToDouble(GetSafeValue(row, "f26"));
                    //dataDict["agg1"] = Agg1;


                    //UpdateActualValuestoUI(txtAGG2_Act, GetSafeValue(row, "f25"));  //AGG2
                    //Agg2 = Convert.ToDouble(GetSafeValue(row, "f25"));                   
                    //dataDict["agg2"] = Agg2;

                    //UpdateActualValuestoUI(txtAGG3_Act, GetSafeValue(row, "f28"));   //CSAND
                    //Agg3 = Convert.ToDouble(GetSafeValue(row, "f28"));                  
                    //dataDict["agg3"] = Agg3;


                    //UpdateActualValuestoUI(txtCEM1_Act, GetSafeValue(row, "f29"));   //CEM1
                    //Cem1 = Convert.ToDouble(GetSafeValue(row, "f29"));              
                    //dataDict["Cem1"] = Cem1;

                    //UpdateActualValuestoUI(txtWTR1_Act, GetSafeValue(row, "f30"));   //Water1
                    //Water1 = Convert.ToDouble(GetSafeValue(row, "f30"));
                    //dataDict["Water1"] = Water1;

                    //UpdateActualValuestoUI(txtADM11_Act, "0");//GetSafeValue(row, "f30"));   //admix11
                    //admix11 = Convert.ToDouble(0);
                    //dataDict["Admix11"] = admix11;

                    //UpdateActualValuestoUI(txtADM12_Act, "0");//GetSafeValue(row, "f30"));   //admix22
                    //admix12 = Convert.ToDouble(0);
                    //dataDict["Admix12"] = admix12;
                    

                     
                    
                    

                    string formatted = string.Empty;
                    if (DateTime.TryParse(datetime1, out DateTime date1))
                    {
                         formatted = date1.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                        Console.WriteLine(formatted); // Output: Apr 14, 2025 18:18:05.8720780
                    }
                    else
                    {
                        Console.WriteLine("Invalid datetime format.");
                    }
                    try
                    {
                        // Read value
                        bool isRunning = SharedFlags.Flags.GetOrAdd("IsRunning", false);
                        
                        if (isRunning)
                        {
                            if(row["f12"].ToString() == "-125")
                            {

                            }
                            // to store from here
                            if (row["f12"].ToString() == "-125" && SharedFlags.Temp_Batch_Stop_Hold.GetOrAdd("EndFlagForNewLogic", 0) != -125)
                            {
                                if (SharedFlags.Temp_Batch_Stop_Hold.GetOrAdd("EndFlagForNewLogic",0) != -125)
                                {
                                    SharedFlags.Temp_Batch_Stop_Hold.AddOrUpdate(
                                        "EndFlagForNewLogic",
                                        0, // Value to add if key does not exist
                                        (key, oldValue) => -125 // Value to update to if key exists
                                    );
                                }

                                Log.Information($@"Sub Batch End Value Matched: {row["f12"].ToString()}, Packet date: {datetime1}");




                                string query = $@"
                                            INSERT INTO Batch_Transaction (
                                                Batch_No, Batch_Index, Batch_Date, Batch_Time, Batch_Time_Text, Batch_Year,
                                                Consistancy, Production_Qty, Ordered_Qty, Returned_Qty, WithThisLoad, Batch_Size,
                                                Gate1_Actual, Gate1_Target, Gate1_Moisture,
                                                Gate2_Actual, Gate2_Target, Gate2_Moisture,
                                                Gate3_Actual, Gate3_Target, Gate3_Moisture,
                                                Gate4_Actual, Gate4_Target, Gate4_Moisture,
                                                Gate5_Actual, Gate5_Target, Gate5_Moisture,
                                                Gate6_Actual, Gate6_Target, Gate6_Moisture,
                                                Cement1_Actual, Cement1_Target, Cement1_Correction,
                                                Cement2_Actual, Cement2_Target, Cement2_Correction,
                                                Cement3_Actual, Cement3_Target, Cement3_Correction,
                                                Cement4_Actual, Cement4_Target, Cement4_Correction,
                                                Filler1_Actual, Filler1_Target, Filler1_Correction,
                                                Water1_Actual, Water1_Target, Water1_Correction,
                                                Water2_Actual, Water2_Target, Water2_Correction,
                                                Silica_Actual, Silica_Target, Silica_Correction,
                                                Slurry_Actual, Slurry_Target, Slurry_Correction,
                                                Adm1_Actual1, Adm1_Target1, Adm1_Correction1,
                                                Adm1_Actual2, Adm1_Target2, Adm1_Correction2,
                                                Adm2_Actual1, Adm2_Target1, Adm2_Correction1,
                                                Adm2_Actual2, Adm2_Target2, Adm2_Correction2,
                                                Pigment_Actual, Pigment_Target,
                                                Plant_No, Balance_Wtr, tUpload1, tUpload2
                                            )
                                            VALUES (
                                                '{GetTextSafe(txtRDMBatchNO)}', '{subBatchEnd}', '{Convert.ToDateTime(datetime1).ToString("yyyy-MM-dd")}', '{Convert.ToDateTime(datetime1).ToString("hh:mm:ss tt")}', '{Convert.ToDateTime(datetime1).ToString("hh:mm:ss tt")}', '{Convert.ToDateTime(datetime1).ToString("yyyy")}',
                                                '0', '{/*Quantity*/ 0}', '{/*ordered*/ 0}', '0', '{/*delivered*/ 0}', '{Batch_Size}',

                                                '{Agg1_Actual}', '{Agg1_Target}', '0',
                                                '{Agg2_Actual}', '{Agg2_Target}', '0',
                                                '{Agg3_Actual}', '{Agg3_Target}', '0',
                                                '{Agg4_Actual}', '{Agg4_Target}', '0',
                                                '{0}', '{0}', '0',
                                                '{0}', '{0}', '0',

                                                '{Cem1_Actual}', '{Cem1_Target}', '0',
                                                '{Cem2_Actual}', '{Cem2_Target}', '0',
                                                '{Cem3_Actual}', '{Cem3_Target}', '0',
                                                '{Cem4_Actual}', '{Cem4_Target}', '0',

                                                '{/*Filler1_Actual*/ 0}', '{0}', '0',
                                                '{Water1_Actual}', '{Water1_Target}', '0',
                                                '{Water2_Actual}', '{Water2_Target}', '0',
                                                '{/*Silica_Actual*/ 0}', '{0}', '0',
                                                '{/*Slurry_Actual*/0}', '{0}', '0',

                                                '{admix11_Actual}', '{admix11_Target}', '0',
                                                '{/*Adm1_Actual2*/0}', '{0}', '0',
                                                '{/*Adm2_Actual1*/0}', '{0}', '0',
                                                '{/*Adm2_Actual2*/0}', '{0}', '0',

                                                '0', '0', '{GetTextSafe(txtplantcode)}', '{/*Batch_End*/ 0}', '0', '0'
                                            );";


                                    await clsFunctions.AdoDataAsync(query);
                                    AddRowSafe(dgv1, subBatchEnd, GetTextSafe(txtRDMBatchNO), Convert.ToDateTime(datetime1).ToString("yyyy-MM-dd"), Convert.ToDateTime(datetime1).ToString("hh:mm:ss tt"), Agg1_Actual, Agg2_Actual, Agg3_Actual, Agg4_Actual, 0, 0,
                                                            Cem1_Actual, Cem2_Actual, 0, 0, 0, Water1_Actual, Water2_Actual,
                                                            admix11_Actual, admix12_Actual, 0, 0, 0, 0);

                                    counter = 0;
                                    sub_Batch_Flag = true;
                                    flagForTrans = true;

                                    if(subBatchEnd == 1) // we get batch start time here 
                                    {
                                        Batch_Start_Time = Convert.ToDateTime(datetime1).ToString("hh:mm:ss tt");
                                    }
                                    subBatchEnd++;



                            }
                            else
                            {
                                // update batch end flag here to 0


                                if (row["f12"].ToString() == Convert.ToString(SharedFlags.Temp_Batch_Stop_Hold.GetOrAdd("EndFlagForNewLogic", 0)))
                                {
                                    //skiping for repeate -125 
                                }
                                else
                                {
                                    SharedFlags.Temp_Batch_Stop_Hold.AddOrUpdate(
                                           "EndFlagForNewLogic",
                                           0, // Value to add if key does not exist
                                           (key, oldValue) => 0 // Value to update to if key exists
                                       );
                                }
                            }
                             





                            //if (row["f13"].ToString() == "2.204052E-39" && !sub_Batch_Flag) // sub batch end
                            //{
                            //    //subBatchEnd++;

                            //    sub_Batch_Flag = true;





                            //}

                            //if (row["f13"].ToString() == "1.504633E-36" && sub_Batch_Flag) // new batch start
                            //                                                               //if (row["f13"].ToString() == "1.469368E-39" && sub_Batch_Flag) // new batch start
                            //{
                            //    sub_Batch_Flag = false;
                            //    subBatchEnd++;

                            //}





                             


                        }
                        else
                        {
                            if (flagForTrans)
                            {
                                string query ="Update Batch_Transaction set Balance_Wtr='1' " +
                                            "WHERE Batch_Index = (SELECT MAX(Batch_Index) FROM Batch_Transaction WHERE Batch_No = " + GetTextSafe(txtRDMBatchNO) + ") " +
                                            "AND Batch_No = " + GetTextSafe(txtRDMBatchNO);

                                await clsFunctions.AdoDataAsync(query);

                                double prodQty = 0;

                                try
                                {
                                    string batchNo = GetTextSafe(txtRDMBatchNO);
                                    double batchSize = 0;

                                    // Try parsing batch size
                                    double.TryParse(GetTextSafe(txtbatchsize), out batchSize);

                                    // Fallback if batchNo is empty or invalid
                                    if (string.IsNullOrWhiteSpace(batchNo))
                                        batchNo = "0";

                                    // Build query safely (still not parameterized, but at least sanitized)
                                    string query1 = $"SELECT * FROM Batch_Transaction WHERE Batch_No = {batchNo}";

                                    int rowCount = clsFunctions.loadRowCount(query1);

                                    //prodQty = (rowCount * batchSize); // Multiply safely and cast to int
                                    //var d = rowCount * batchSize;

                                     prodQty = rowCount * batchSize;
                                    string formattedProdQty = prodQty % 1 == 0
                                        ? ((int)prodQty).ToString()          // whole number, no decimal
                                        : prodQty.ToString("0.##");          // up to two decimals, trimmed

                                    prodQty = Convert.ToDouble(formattedProdQty);

                                }
                                catch
                                {
                                    prodQty = 0; // fallback in case of error
                                }



                                DataTable dt1 = new DataTable();
                                dt1 = clsFunctions_comman.fillDatatable(
                                                        "SELECT TOP 1 * FROM Batch_Transaction " +
                                                        "WHERE Batch_Index = (SELECT MAX(Batch_Index) FROM Batch_Transaction WHERE Batch_No = " + GetTextSafe(txtRDMBatchNO) + ") " +
                                                        "AND Batch_No = " + GetTextSafe(txtRDMBatchNO) + ""
                                                    );
                                DataRow row1 = dt1.Rows[0]; // Since you're using TOP 1

                           

                                string Batch_End_Time = Convert.ToDateTime(row1["Batch_Time"]).ToString("HH:mm:ss");
                                

                                string insertToTransQuery = "insert into Batch_Dat_Trans (Batch_No,Batch_Date,Batch_Time,Batch_Time_Text,Batch_Start_Time,Batch_End_Time,Batch_Year,Batcher_Name,Batcher_User_Level,Customer_Code,Recipe_Code,"
                                  + "Recipe_Name,Mixing_Time,Mixer_Capacity,strength,Site,Truck_No,Truck_Driver,Production_Qty,Ordered_Qty,Returned_Qty,WithThisLoad,Batch_Size,Order_No,Schedule_Id,Gate1_Target,"
                                  + "Gate2_Target,Gate3_Target,Gate4_Target,Gate5_Target,Gate6_Target,Cement1_Target,Cement2_Target,Cement3_Target,Cement4_Target,Filler_Target,Water1_Target,slurry_Target,Water2_Target,"
                                  + "Silica_Target,Adm1_Target1,Adm1_Target2,Adm2_Target1,Adm2_Target2,Cost_Per_Mtr_Cube,Total_Cost,Plant_No,Weighed_Net_Weight,Weigh_Bridge_Stat,tExportStatus,tUpload1,tUpload2, WO_Code, Cust_Name, Site_ID, InsertType) values('" + GetTextSafe(txtRDMBatchNO)
                                  + "','" + Convert.ToDateTime(datetime1).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(Batch_Start_Time).ToString("hh:mm:ss tt") + "','" + Convert.ToDateTime(Batch_Start_Time).ToString("hh:mm:ss tt") + "','" + Convert.ToDateTime(Batch_Start_Time).ToString("hh:mm:ss tt") + "','" + Convert.ToDateTime(Batch_End_Time).ToString("hh:mm:ss tt") + "','" + Convert.ToDateTime(datetime1).ToString("yyyy")
                                  + "','" + 0 + "','" + 0 + "','" + GetTextSafe(cmbContractor) + "','" + GetTextSafe(cmbRecipe) + "','" + GetTextSafe(cmbRecipe) + "','" + /*clsVar.Mixing_Time*/ 0
                                  + "','" + GetTextSafe(txtMixerCapacity) + "','" + /*clsVar.strength*/0 + "','" + GetTextSafe(cmbjobsite) + "','" + GetTextSafe(cmbRDMVehicle) + "','" + "NA" + "','" + /*clsVar.Production_Qty*/prodQty
                                  + "','" + /*clsVar.Ordered_Qty*/0 + "','" + /*clsVar.Returned_Qty*/0 + "','" + /*clsVar.WithThisLoad*/0 + "','" + Batch_Size + "','" + /*clsVar.Order_No*/0 + "','" +/* clsVar.Schedule_Id */0 + "','" + Agg1_Target
                                  + "','" + Agg2_Target + "','" + Agg3_Target + "','" + Agg4_Target + "','" + 0 + "','" + 0 + "','" + Cem1_Target
                                  + "','" + Cem2_Target + "','" + Cem3_Target + "','" + Cem4_Target + "','" + /*clsVar.Filler_Target*/0 + "','" + Water1_Target + "','" + /*clsVar.slurry_Target*/0
                                  + "','" + Water2_Target + "','" + /*clsVar.Silica_Target*/0 + "','" + admix11_Target + "','" + admix12_Target + "','" +/* Convert.ToDouble(rowForTrans["ADMIX3TRG"])*/0 + "','" + /*clsVar.Adm2_Target2*/0
                                  + "','" + /*clsVar.Cost_Per_Mtr_Cube*/0 + "','" + /*clsVar.Total_Cost*/0 + "','" + GetTextSafe(txtplantcode) + "','" + /*clsVar.Weighed_Net_Weight*/0 + "','" + /*clsVar.Weigh_Bridge_Stat*/0 + "','N','0','0','" + GetTextSafe(txtworkid) + "','" + GetTextSafe(cmbContractor) + "',0, 'A')";


                                int status = await clsFunctions.AdoDataAsync(insertToTransQuery);
                                if(status==1)
                                    MessageBox.Show("Batch saved successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Batch not saved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                                flagForTrans = false;
                                subBatchEnd = 1;
                            }
                             
                        }
                        
                    }
                    catch(Exception ex) {

                        Log.Error("at isRunning ", ex);

                    }


                }

                //if (length == "137") // start/stop data
                //{
                     
                //}


                //if (length == "") // start/stop data
                //{
                //    string f4 = GetSafeValue(row, "f4");
                //    if (f4 == "512") { }
                //    string f23 = GetSafeValue(row, "f23");
                //    if (f4 == "512" && f23 == "4.591775E-40") // this means batch ended
                //    {
                //        //MessageBox.Show("Batch ended!");
                //    }
                //}

                // insert data into mdb microsoft access database.



                // You can store dt in a class-level variable or log/export it if needed
            }
            catch (Exception ex)
            {
                Log.Error("InsertFloatValuesIntoDatabase error: {Message}", ex);
            }
        }

        //public int insertDataIntoTrans()
        //{
        //    try
        //    {
        //        DataRow rowForTrans = clsVarTuskar.dt_CYCLEDATA.Rows[rowIndex - 1];

        //        if (rowIndex == dgv1.Rows.Count)          //if (reccnt == dgv1.Rows.Count)
        //        {
        //            //DateTime date = DateTime.ParseExact(clsVar.Batch_Date, "M/d/yyyy", CultureInfo.InvariantCulture);
        //            //clsVar.Batch_Date = date.ToString("dd/MM/yyyy");

        //            //var a = DateAndTime.DateValue(Convert.ToDateTime(clsVar.Batch_Date).ToShortDateString());

        //            // Inserting data into Batch_Trans
        //            // 19/12/2023 - BhaveshT : Inserting 'A' for Auto in InsertType column of Batch_Dat_Trans

        //            insertToTransQuery = "insert into Batch_Dat_Trans (Batch_No,Batch_Date,Batch_Time,Batch_Time_Text,Batch_Start_Time,Batch_End_Time,Batch_Year,Batcher_Name,Batcher_User_Level,Customer_Code,Recipe_Code,"
        //                   + "Recipe_Name,Mixing_Time,Mixer_Capacity,strength,Site,Truck_No,Truck_Driver,Production_Qty,Ordered_Qty,Returned_Qty,WithThisLoad,Batch_Size,Order_No,Schedule_Id,Gate1_Target,"
        //                   + "Gate2_Target,Gate3_Target,Gate4_Target,Gate5_Target,Gate6_Target,Cement1_Target,Cement2_Target,Cement3_Target,Cement4_Target,Filler_Target,Water1_Target,slurry_Target,Water2_Target,"
        //                   + "Silica_Target,Adm1_Target1,Adm1_Target2,Adm2_Target1,Adm2_Target2,Cost_Per_Mtr_Cube,Total_Cost,Plant_No,Weighed_Net_Weight,Weigh_Bridge_Stat,tExportStatus,tUpload1,tUpload2, WO_Code, Cust_Name, Site_ID, InsertType) values('" + clsVar.Batch_No
        //                   + "','" + clsVar.Batch_Date + "','" + clsVar.Batch_Time + "','" + clsVar.Batch_Time + "','" + clsVar.Batch_Time + "','" + clsVar.Batch_End_Time + "','" + Convert.ToDateTime(clsVar.Batch_Date).ToString("yyyy")
        //                   + "','" + clsVar.Batcher_Name + "','" + clsVar.Batcher_User_Level + "','" + txtcontractorid.Text + "','" + clsVar.Recipe_Name + "','" + clsVar.Recipe_Name + "','" + clsVar.Mixing_Time
        //                   + "','" + clsVar.Mixer_Capacity + "','" + clsVar.strength + "','" + clsVar.SiteName + "','" + clsVar.Truck_No + "','" + clsVar.Truck_Driver + "','" + clsVar.Production_Qty
        //                   + "','" + clsVar.Ordered_Qty + "','" + clsVar.Returned_Qty + "','" + clsVar.WithThisLoad + "','" + clsVar.Batch_Size + "','" + clsVar.Order_No + "','" + clsVar.Schedule_Id + "','" + Convert.ToDouble(rowForTrans["AGG1TRG"])
        //                   + "','" + Convert.ToDouble(rowForTrans["AGG2TRG"]) + "','" + Convert.ToDouble(rowForTrans["AGG3TRG"]) + "','" + Convert.ToDouble(rowForTrans["AGG4TRG"]) + "','" + clsVar.Gate5_Target + "','" + clsVar.Gate6_Target + "','" + Convert.ToDouble(rowForTrans["CEMENT1TRG"])
        //                   + "','" + Convert.ToDouble(rowForTrans["CEMENT2TRG"]) + "','" + Convert.ToDouble(rowForTrans["CEMENT3TRG"]) + "','" + clsVar.Cement4_Target + "','" + clsVar.Filler_Target + "','" + Convert.ToDouble(rowForTrans["WATER1TRG"]) + "','" + clsVar.slurry_Target
        //                   + "','" + Convert.ToDouble(rowForTrans["WATER2TRG"]) + "','" + clsVar.Silica_Target + "','" + Convert.ToDouble(rowForTrans["ADMIX1TRG"]) + "','" + Convert.ToDouble(rowForTrans["ADMIX2TRG"]) + "','" + Convert.ToDouble(rowForTrans["ADMIX3TRG"]) + "','" + clsVar.Adm2_Target2
        //                   + "','" + clsVar.Cost_Per_Mtr_Cube + "','" + clsVar.Total_Cost + "','" + clsVar.Plant_No + "','" + clsVar.Weighed_Net_Weight + "','" + clsVar.Weigh_Bridge_Stat + "','N','0','0','" + txtworkid.Text + "','" + cmbContractor.Text + "',0, 'A')";

        //            return clsFunctions_comman.Ado(insertToTransQuery);
        //        }
        //        else
        //        {
        //            clsFunctions_comman.Ado("delete from Batch_Transaction where Batch_No=" + cmbbatchno.Text);
        //            MessageBox.Show("Record Not saved", "MSG", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("There is no row at position -1"))
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Exception: Record Not saved" + ex.Message);
        //            clsFunctions_comman.Ado("delete from Batch_Transaction where Batch_No=" + cmbbatchno.Text);
        //            return 0;
        //        }
        //    }
        //}


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
        private int GetRowCountThreadSafe(DataGridView dgv)
        {
            if (dgv.InvokeRequired)
            {
                return (int)dgv.Invoke(new Func<int>(() => dgv.Rows.Count));
            }
            else
            {
                return dgv.Rows.Count;
            }
        }


        // Helper function to safely get values
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

        //private async Task InsertFloatValuesIntoDatabaseAsync_workingbutld(List<float> floatValues, string datetime1, string length, int id)
        //{
        //    try
        //    {
        //        if (length == "119" || length == "87" || length == "65") return;
        //        string connectionString = $"Data Source={sqliteDbPath};Version=3;";
        //        using (var connection = new SQLiteConnection(connectionString))
        //        {
        //            await connection.OpenAsync();

        //            var parameters = new DynamicParameters();
        //            var columnNames = new List<string>();
        //            var paramNames = new List<string>();

        //            // Add datetime1 and length first
        //            columnNames.Add("datetime1");
        //            paramNames.Add("@datetime1");
        //            parameters.Add("@datetime1", datetime1);

        //            columnNames.Add("length");
        //            paramNames.Add("@length");
        //            parameters.Add("@length", length);

        //            // Add float values as f1 to f200
        //            for (int i = 0; i < floatValues.Count && i < 200; i++)
        //            {
        //                string column = $"f{i + 1}";
        //                columnNames.Add(column);
        //                paramNames.Add($"@{column}");
        //                parameters.Add($"@{column}", floatValues[i].ToString("G", CultureInfo.InvariantCulture));


        //                //update ui with its value here 
        //                if (length == "139") // production data
        //                {
        //                    if (column == "f7")
        //                    {
        //                        UpdateActualValuestoUI(column, floatValues[i].ToString("G", CultureInfo.InvariantCulture));
        //                        //await Task.Delay(500);
        //                    }
        //                    else if (column == "f13") 
        //                    {
        //                        UpdateActualValuestoUI(column, floatValues[i].ToString("G", CultureInfo.InvariantCulture));
        //                        //await Task.Delay(500);
        //                    }
        //                    else if (column == "f19")
        //                    {
        //                        UpdateActualValuestoUI(column, floatValues[i].ToString("G", CultureInfo.InvariantCulture));
        //                        //await Task.Delay(500);
        //                    }
        //                }
        //                else if (length == "167") // start / stop data
        //                {
        //                    if (column == "f4" && (floatValues[i].ToString("G", CultureInfo.InvariantCulture) == "512" || floatValues[i].ToString("G", CultureInfo.InvariantCulture) == "0"))
        //                        MessageBox.Show("Batch ended!");
        //                    var d = floatValues[i].ToString("G", CultureInfo.InvariantCulture);
        //                }
        //                else
        //                    Console.WriteLine("length not matched: "+ length);

        //            }
        //            //UpdateActualValuestoUI(datetime1, datetime1);
        //            // Fill remaining columns with empty string if fewer than 200 values
        //            for (int i = floatValues.Count; i < 200; i++)
        //            {
        //                string column = $"f{i + 1}";
        //                columnNames.Add(column);
        //                paramNames.Add($"@{column}");
        //                parameters.Add($"@{column}", "");
        //            }

        //            // Final INSERT query
        //            string query = $"INSERT INTO DataForAnalysis ({string.Join(",", columnNames)}) VALUES ({string.Join(",", paramNames)})";
        //            await connection.ExecuteAsync(query, parameters);
        //            string update = $"Update dataset SET Status=1 where id={id};";
        //            await connection.ExecuteAsync(update);
        //            Log.Information("DataForAnalysis insertion complete: {Count} floats, datetime1 = {DT}, length = {Len}", floatValues.Count, datetime1, length);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("InsertFloatValuesIntoDatabase error: {Message}", ex.Message);
        //    }
        //}

        public async Task UpdateActualValuestoUI(TextBox targetTextBox, string value)
        {
            try
            {
                if (targetTextBox.InvokeRequired)
                {
                    targetTextBox.Invoke(new Action(() =>  UpdateActualValuestoUI(targetTextBox, value)));
                    return;
                }

                targetTextBox.Text = value;
            }
            catch (Exception ex)
            {
                Log.Error("UpdateActualValuestoUI error: {Message}", ex.Message);
            }
        }

        //public void UpdateActualValuestoUI_oldd(string colname, string value)
        //{
        //    try
        //    {
        //        if (txtAgg.InvokeRequired || txtCem.InvokeRequired)
        //        {
        //            txtAgg.Invoke(new Action(() => UpdateActualValuestoUI_oldd(colname, value)));
        //            return;
        //        }

        //        switch (colname)
        //        {
        //            case "f7":
        //                txtAgg.Text = value;
        //                break;
        //            case "f13":
        //                txtCem.Text = value;
        //                break;
        //            case "f19":
        //                txtWater.Text = value;
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("UpdateActualValuestoUI error: {Message}", ex.Message);
        //    }
        //}

        private async Task<DataTable> LoadDataTo_DataTableAsync()
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={sqliteDbPath};Version=3;"))
                {
                    await connection.OpenAsync();


                    string query = "SELECT * FROM dataset WHERE Status = 0 AND length=179 ORDER BY datetime(date) ASC;";
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





        bool flag = false;
        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                flag = false;
                while (!backgroundWorker.CancellationPending)
                {
                    await RMC_ModBus_Load();
                    Task.Delay(100).Wait();
                    //if(Stop_backgroundWorker)
                    //{
                    //   // backgroundWorker.CancelAsync();
                    //    break;
                    //}
                }
            }
            catch (Exception ex)
            {
                Log.Error("at BackgroundWorker_DoWork: {Message}", ex.Message);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // optional: UI updates
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
                MessageBox.Show("Error starting executable: " + ex.Message, "Error");
            }
        }


        private void btnStart_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmbRecipe.Text))
            {
                MessageBox.Show("Please select recipe name", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(cmbContractor.Text))
            {
                MessageBox.Show("Please select contractor", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtplantcode.Text))
            {
                MessageBox.Show("Please enter plant code", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(cmbWorkName.Text))
            {
                MessageBox.Show("Please select work name", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtworkid.Text))
            {
                MessageBox.Show("Please enter work ID", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            disableControllers();


            subBatchEnd = 1;
            btnStart.Enabled = false;
            btnstop.Enabled = true;
            string path = Path.Combine(Application.StartupPath, "Database\\bhumi_final.exe");
            StartExeIfNotRunning(path);



            if (txtRDMBatchNO.InvokeRequired)
            {
                txtRDMBatchNO.Invoke(new MethodInvoker(() =>
                {
                    txtRDMBatchNO.Text = clsFunctions_comman.GetMaxId("Select MAX(Batch_No) from Batch_Transaction").ToString();
                    //dgv1.Rows.Clear();
                }));
            }
            else
            {
                txtRDMBatchNO.Text = clsFunctions_comman.GetMaxId("Select MAX(Batch_No) from Batch_Transaction").ToString();
                //dgv1.Rows.Clear();
            }



            // Toggle value safely (example)
            SharedFlags.Flags.AddOrUpdate("IsRunning", true, (key, oldValue) => !oldValue);

            // setting 0 
            SharedFlags.Temp_Batch_Stop_Hold.AddOrUpdate(
      "EndFlagForNewLogic",
      0, // Value to add if key does not exist
      (key, oldValue) => 0 // Value to update to if key exists
  );


            if (backgroundWorker.IsBusy)
            {
                //MessageBox.Show("Background worker is already running.");
                return;
            }
            backgroundWorker.RunWorkerAsync();
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnstop.Enabled = false;
            dgv1.Rows.Clear();
            //// Set value
            //SharedFlags.Flags["IsRunning"] = true;
             sub_Batch_Flag = false;
             counter = 0;
             //flagForTrans = false;


            // Toggle value safely (example)
            SharedFlags.Flags.AddOrUpdate("IsRunning", false, (key, oldValue) => !oldValue);



            SharedFlags.Temp_Batch_Stop_Hold.AddOrUpdate(
         "EndFlagForNewLogic",
         0, // Value to add if key does not exist
         (key, oldValue) => 0 // Value to update to if key exists
     );




            //if (backgroundWorker.IsBusy)
            //{
            //    backgroundWorker.CancelAsync();

            //}
            //flag = true;
            EnalbeControllers();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadComboData()
        {
            try
            {
                //clsFunctions_comman.FillCombo_D_ASIT("Select Distinct ContractorName from workorder", cmbContractor);

               
                        SetDateTimeToController();
                  

                //------------------
                clsFunctions.checknewcolumn("iscompleted", "Text(255)", "workorder");
                clsFunctions.FillContractorInCombo(cmbContractor, clsFunctions.activeDeptName);
                txtplantcode.Text = clsFunctions.activePlantCode;
                clsFunctions_comman.FillCombo("select distinct tipperno from tblTipperdetails where tipperno <> ''", cmbRDMVehicle);
                clsVar.Plant_No = Convert.ToInt32(txtplantcode.Text);

                clsFunctions_comman.FillCombo_D_ASIT("Select Distinct Recipe_Code from Recipe_Master order by Recipe_Code", cmbRecipe);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception at loadComboData(): " + ex.Message);
                clsFunctions_comman.ErrorLog("Exception at loadComboData(): " + ex.Message);
            }
        }
        public DateTime DateToday;
        public void SetDateTimeToController()             
        {
            try
            { 

                DateToday = DateTime.Now.Date; // assign only the date part (no time)
                txtDateToday.Text = DateToday.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                 
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog($"[Exception] SetDateTimeToController() - {ex.Message}\nStackTrace: {ex.StackTrace}");
            }

        }

        private void addNameSetupToDGV()
        {
            try
            {
                
                DataTable dt = clsFunctions.fillDatatable("select Gate1Name,Gate2Name,Gate3Name,Gate4Name,Gate5Name,Gate6Name,Cem1Name,Cem2Name,Cem3Name,Cem4Name,FillName,Wtr1Name,wtr2Name,Admix1Name,Admix2Name from NameSetup where tInfo='O' and Batch_No=2"); //WHERE Batch_No=3

                dgv1.Columns.Add("Sr_No", "SrNo");
                dgv1.Columns.Add("_batchNo", "BatchNo");
                dgv1.Columns.Add("_date", "Date");

               
                dgv1.Columns.Add("_time", "Time");

                foreach (DataColumn col in dt.Columns)
                {
                    if (dt.Rows[0][col].ToString().Trim() != "")
                    {
                        dgv1.Columns.Add(dt.Rows[0][col].ToString(), dt.Rows[0][col].ToString());
                        if (dt.Rows[0][col].ToString().Trim() == "" || dt.Rows[0][col].ToString().Trim() == "0" || dt.Rows[0][col].ToString().Trim() == "-")
                        {
                            dgv1.Columns[dt.Rows[0][col].ToString()].Visible = false;
                        }
                    }
                }
                dgv1.Columns.Add("WtrCorr", "WtrCorr");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception at addNameSetupToDGV(): " + ex.Message);
                clsFunctions_comman.ErrorLog("Exception at addNameSetupToDGV(): " + ex.Message);
            }
        }

        private void cmbContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsFunctions.FillWorkOrderFromContractor(cmbContractor, cmbWorkName, txtcontractorid, cmbjobsite);
        }

        private void cmbWorkName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (cmbWorkName.Text != "")
                    {
                        DataTable dt = clsFunctions_comman.fillDatatable("Select * from workorder where workname = '" + cmbWorkName.Text + "' AND WorkType = '" + clsFunctions.activeDeptName + "' AND ContractorName = '" + cmbContractor.Text + "' ");

                        if (dt.Rows.Count != 0)
                        {
                            txtworkid.Text = dt.Rows[0]["Workno"].ToString();
                            txtcontractorid.Text = dt.Rows[0]["ContractorID"].ToString();
                            clsVar.Customer_Code = txtcontractorid.Text;
                            lblworktype.Text = dt.Rows[0]["Worktype"].ToString();
                        }
                        else
                        {
                            //------------ When workname is longer than 255, use then use LIKE workname to fetch Work order data. --------------------------

                            try
                            {
                                clsFunctions.GetWorkOrderData_Like(cmbWorkName, txtworkid, cmbContractor, txtplantcode, txtcontractorid, lblworktype);
                                clsVar.Customer_Code = txtcontractorid.Text;
                            }
                            catch
                            { }
                        }
                    }

                    clsFunctions_comman.FillCombo_D_ASIT("Select Distinct SiteName from Site_Master where workorderID='" + txtworkid.Text + "'", cmbjobsite);
                    try
                    { cmbjobsite.SelectedIndex = 0; }
                    catch
                    { }

                }
                catch (Exception ex)
                {
                    clsFunctions_comman.ErrorLog("cmbWorkName_SelectedIndexChanged():- " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Exception at cmbWorkName_SelectedIndexChanged: " + ex.Message);
                clsFunctions_comman.ErrorLog("Exception at cmbWorkName_SelectedIndexChanged : " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
          
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            ReceipeMaster_RMC d = new ReceipeMaster_RMC();
            d.ShowDialog();
        }

        private void btnVehicle_Click(object sender, EventArgs e)
        {
            TipperMaster tipper = new TipperMaster();
            tipper.ShowDialog();
        }

        private void cmbRecipe_Click(object sender, EventArgs e)
        {
            clsFunctions_comman.FillCombo_D_ASIT("Select Distinct Recipe_Code from Recipe_Master order by Recipe_Code", cmbRecipe);
        }

        private void cmbRDMVehicle_Click(object sender, EventArgs e)
        {
            clsFunctions_comman.FillCombo("select distinct tipperno from tblTipperdetails where tipperno <> ''", cmbRDMVehicle);
        }

        private void RMC_ModBus_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnStart.Enabled)
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

        private void cmbRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMixerCapacity.Text = clsFunctions.loadSingleValue("Select Mixer_Capacity From Recipe_Master where Recipe_Name='"+cmbRecipe.Text+"'");
        }

        private void cmbRDMVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDriver.Text = clsFunctions.loadSingleValue("Select DriverName From tblTipperdetails where tipperno='" + cmbRDMVehicle.Text + "'");
        }

        private void disableControllers()
        {

            // Disable controls after successful validation
            cmbRecipe.Enabled = false;
            cmbContractor.Enabled = false;
            txtplantcode.Enabled = false;
            cmbWorkName.Enabled = false;
            txtworkid.Enabled = false;
            cmbjobsite.Enabled = false;
            cmbRDMVehicle.Enabled = false;
        }
        private void EnalbeControllers()
        {

            // Disable controls after successful validation
            cmbRecipe.Enabled = true;
            cmbContractor.Enabled = true;
            txtplantcode.Enabled = true;
            cmbWorkName.Enabled = true;
            txtworkid.Enabled = true;
            cmbjobsite.Enabled = true;
            cmbRDMVehicle.Enabled = true;
        }

        private void lblActual_Paint(object sender, PaintEventArgs e)
        {
            
        }

       


    }



    public static class SharedFlags
    {
        public static ConcurrentDictionary<string, bool> Flags = new ConcurrentDictionary<string, bool>();
        public static ConcurrentDictionary<string,float> Temp_Batch_Stop_Hold = new ConcurrentDictionary<string, float>();
    }

}
