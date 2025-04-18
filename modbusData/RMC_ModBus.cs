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

namespace PDF_File_Reader
{
    public partial class RMC_ModBus : Form
    {
        public  string sqliteDbPath = System.IO.Path.Combine(Application.StartupPath, "Database\\UniproData.db");
        private BackgroundWorker backgroundWorker;

        // Remove BackgroundWorker from declarations
        // private BackgroundWorker backgroundWorker;

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

            //this.Load += RMC_ModBus_Load;
        }
        private void RMC_ModBus_Load_2(object sender, EventArgs e)
        {
            //RMC_ModBus_Load();
        }

        private async Task RMC_ModBus_Load()
        {
            try
            {
                var dt = await LoadDataTo_DataTableAsync();
                await ConvertDataAsync(dt);
                //InsertData(dt);
            }
            catch (Exception ex)
            {
                Log.Error("at RMC_ModBus_Load: {Message}", ex.Message);
            }
        }

        private async Task ConvertDataAsync(DataTable dt)  
        {
            try
            {
                var tasks = new List<Task>();
                foreach (DataRow row in dt.Rows)
                {
                    await Float_Big_Endian_ABCDAsync(
                        string.Join(" ", row["data"]),
                        row["date"].ToString(),
                        row["length"].ToString(),
                        Convert.ToInt32(row["id"])
                    );
                     Task.Delay(10).Wait(); // Optional delay to prevent overwhelming the database
                }
                await Task.WhenAll(tasks); // Process rows concurrently
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
                string[] hexBytes = input.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                byte[] bytes = new byte[hexBytes.Length];
                for (int i = 0; i < hexBytes.Length; i++)
                {
                    bytes[i] = byte.Parse(hexBytes[i], NumberStyles.HexNumber);
                }

                // Define file path
                string filePath = Path.Combine(Application.StartupPath, "Float_Big_Endian_ABCD_FloatOutput.txt");

                List<float> floatValues = new List<float>();

                //using (StreamWriter writer = new StreamWriter(filePath))
                //{
                    //await writer.WriteLineAsync("Converted Float Values (Big Endian):\n");

                    for (int i = 0; i < bytes.Length; i += 4)
                    {
                        if (i + 3 >= bytes.Length) break;

                        byte[] floatBytes = new byte[4];
                        floatBytes[0] = bytes[i];
                        floatBytes[1] = bytes[i + 1];
                        floatBytes[2] = bytes[i + 2];
                        floatBytes[3] = bytes[i + 3];

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
            catch (Exception ex)
            {
                Log.Error("Float_Big_Endian_ABCD error: {Message}", ex.Message);
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task InsertFloatValuesIntoDatabaseAsync(List<float> floatValues, string datetime1, string length, int id)
        {
            try
            {
                string datetime11="";

                double length1=0;

                double Agg1=0;
                double Agg2=0;
                double Agg3=0;
                double Agg4=0;

                double Cem1=0;
                double Cem2=0;
                double Cem3=0;
                double Cem4=0;

                double Water1=0;
                double Water2=0;


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



                // Add the row to DataTable if needed
                dt.Rows.Add(row);

                 
                // Update UI only if safe, no nulls or exceptions
                if (length == "139") // production data
                {
                    /*********** MANUAL ***************/

                    // AGGREGATES
                    UpdateActualValuestoUI(txtagg1, GetSafeValue(row, "f5"));
                    
                    UpdateActualValuestoUI(txtagg2, "0");
                    UpdateActualValuestoUI(txtagg3, "0");
                    UpdateActualValuestoUI(txtagg4, "0");
                    UpdateActualValuestoUI(txtagg5, "0");

                    // CEMENT
                    UpdateActualValuestoUI(txtCement1, GetSafeValue(row, "f11"));
                    UpdateActualValuestoUI(txtCement2, "0");
                    UpdateActualValuestoUI(txtCement3, "0");
                    UpdateActualValuestoUI(txtCement4, "0");

                    // WATER
                    UpdateActualValuestoUI(txtwater1, GetSafeValue(row, "f17"));

                    /*********** ACTUAL ***************/
                    UpdateActualValuestoUI(txtAgg, GetSafeValue(row, "f7"));
                    Agg1 = Convert.ToDouble(GetSafeValue(row, "f7"));

                    UpdateActualValuestoUI(txtCem, GetSafeValue(row, "f13"));
                    Cem1 = Convert.ToDouble(GetSafeValue(row, "f13"));

                    UpdateActualValuestoUI(txtWater, GetSafeValue(row, "f19"));
                    Water1 = Convert.ToDouble(GetSafeValue(row, "f19"));

                    string formatted = string.Empty;
                    if (DateTime.TryParse(datetime1, out DateTime date1))
                    {
                         formatted = date1.ToString("yyyy HH:mm:ss.fffffff");
                        Console.WriteLine(formatted); // Output: Apr 14, 2025 18:18:05.8720780
                    }
                    else
                    {
                        Console.WriteLine("Invalid datetime format.");
                    }
                    try
                    {

                        string query = $@"
INSERT INTO analyse 
(datetime1, length, Agg1, Agg2, Agg3, Agg4, Cem1, Cem2, Cem3, Cem4, Water1, Water2) 
VALUES 
('{formatted}', {length1}, {Agg1}, {Agg2}, {Agg3}, {Agg4}, {Cem1}, {Cem2}, {Cem3}, {Cem4}, {Water1}, {Water2});
";
                        //clsFunctions.AdoData(query);
                    }
                    catch { }


                }

                if (length == "167") // start/stop data
                {
                    string f4 = GetSafeValue(row, "f4");
                    if (f4 == "512") { }
                    string f23 = GetSafeValue(row, "f23");
                    if (f4 == "512" /*&& f23 == "4.591775E-40" */) // this means batch ended
                    {
                        //MessageBox.Show("Batch ended!");
                    }
                }

                // insert data into mdb microsoft access database.



                // You can store dt in a class-level variable or log/export it if needed
            }
            catch (Exception ex)
            {
                Log.Error("InsertFloatValuesIntoDatabase error: {Message}", ex.Message);
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

        public void UpdateActualValuestoUI(TextBox targetTextBox, string value)
        {
            try
            {
                if (targetTextBox.InvokeRequired)
                {
                    targetTextBox.Invoke(new Action(() => UpdateActualValuestoUI(targetTextBox, value)));
                    return;
                }

                targetTextBox.Text = value;
            }
            catch (Exception ex)
            {
                Log.Error("UpdateActualValuestoUI error: {Message}", ex.Message);
            }
        }

        public void UpdateActualValuestoUI_oldd(string colname, string value)
        {
            try
            {
                if (txtAgg.InvokeRequired || txtCem.InvokeRequired)
                {
                    txtAgg.Invoke(new Action(() => UpdateActualValuestoUI_oldd(colname, value)));
                    return;
                }

                switch (colname)
                {
                    case "f7":
                        txtAgg.Text = value;
                        break;
                    case "f13":
                        txtCem.Text = value;
                        break;
                    case "f19":
                        txtWater.Text = value;
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error("UpdateActualValuestoUI error: {Message}", ex.Message);
            }
        }

        private async Task<DataTable> LoadDataTo_DataTableAsync()
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={sqliteDbPath};Version=3;"))
                {
                    await connection.OpenAsync();

                    string query = "SELECT * FROM dataset WHERE Status = 0 ORDER BY datetime(date) ASC;";
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
                while (!backgroundWorker.CancellationPending||!flag)
                {
                    await RMC_ModBus_Load();
                    Task.Delay(10).Wait();
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(backgroundWorker.IsBusy)
            {
                MessageBox.Show("Background worker is already running.");
                return;
            }
            backgroundWorker.RunWorkerAsync();
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            if(backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();

            }
            flag = true;
        }
    }
}
