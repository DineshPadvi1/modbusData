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

namespace PDF_File_Reader
{
    public partial class RMC_ModBus : Form
    {
        public  string sqliteDbPath = System.IO.Path.Combine(Application.StartupPath, "Database\\UniproData.db");
        private BackgroundWorker backgroundWorker;

        public RMC_ModBus()
        {
            InitializeComponent();

            // Setup Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(Path.Combine(Application.StartupPath, "log.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;

            this.Load += RMC_ModBus_Load;
             sqliteDbPath = Path.Combine(
    Application.StartupPath,
    "Database",
    "UniproData.db"
);

// Add this check before using the database
if (!File.Exists(sqliteDbPath))
{
    Log.Error("Database not found at: {Path}", sqliteDbPath);
    MessageBox.Show("Database file not found!");
    return;
}
}

        private async void RMC_ModBus_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = LoadDataTo_DataTable();
                ConvertData(dt);
                //InsertData(dt);
            }
            catch (Exception ex)
            {
                Log.Error("at RMC_ModBus_Load: {Message}", ex.Message);
            }
        }

        private void ConvertData(DataTable dt)
        {
            try
            {
                string connectionString = $"Data Source={sqliteDbPath};Version=3;";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataRow row in dt.Rows)
                    {
                        Float_Big_Endian_ABCD(string.Join(" ", row["data"]), row["date"].ToString(), row["length"].ToString(), Convert.ToInt32( row["id"]));
                    }
                }

                Log.Information("Data inserted successfully using Dapper.");
            }
            catch (Exception ex)
            {
                Log.Error("InsertData error: {Message}", ex.Message);
            }
        }


        public void Float_Big_Endian_ABCD(string input,string datetime1,string length, int id)
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

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Converted Float Values (Big Endian):\n");

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

                        writer.WriteLine($"{i / 4:D2}: {value}");
                    }
                }

                InsertFloatValuesIntoDatabase(floatValues,  datetime1,  length, id);

                //MessageBox.Show("Float values written to file and database:\n" + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log.Error("Float_Big_Endian_ABCD error: {Message}", ex.Message);
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertFloatValuesIntoDatabase(List<float> floatValues, string datetime1, string length,int id)
        {
            try
            {
                string connectionString = $"Data Source={sqliteDbPath};Version=3;";
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    var columnNames = new List<string>();
                    var paramNames = new List<string>();

                    // Add datetime1 and length first
                    columnNames.Add("datetime1");
                    paramNames.Add("@datetime1");
                    parameters.Add("@datetime1", datetime1);

                    columnNames.Add("length");
                    paramNames.Add("@length");
                    parameters.Add("@length", length);

                    // Add float values as f1 to f200
                    for (int i = 0; i < floatValues.Count && i < 200; i++)
                    {
                        string column = $"f{i + 1}";
                        columnNames.Add(column);
                        paramNames.Add($"@{column}");
                        parameters.Add($"@{column}", floatValues[i].ToString("G", CultureInfo.InvariantCulture));
                    }

                    // Fill remaining columns with empty string if fewer than 200 values
                    for (int i = floatValues.Count; i < 200; i++)
                    {
                        string column = $"f{i + 1}";
                        columnNames.Add(column);
                        paramNames.Add($"@{column}");
                        parameters.Add($"@{column}", "");
                    }

                    // Final INSERT query
                    string query = $"INSERT INTO DataForAnalysis ({string.Join(",", columnNames)}) VALUES ({string.Join(",", paramNames)})";
                    connection.Execute(query, parameters);
                    string update = $"Update dataset SET Status=1 where id={id};";
                    connection.Execute(update);
                    Log.Information("DataForAnalysis insertion complete: {Count} floats, datetime1 = {DT}, length = {Len}", floatValues.Count, datetime1, length);
                }
            }
            catch (Exception ex)
            {
                Log.Error("InsertFloatValuesIntoDatabase error: {Message}", ex.Message);
            }
        }







        private void InsertData(DataTable dt)
        {
            try
            {
                string connectionString = $"Data Source={sqliteDbPath};Version=3;";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataRow row in dt.Rows)
                    {
                        var parameters = new DynamicParameters();

                        // Build column list and parameter list
                        var columnNames = new List<string>();
                        var paramNames = new List<string>();

                        for (int i = 1; i <= 200; i++)
                        {
                            string column = $"f{i}";
                            columnNames.Add(column);
                            paramNames.Add($"@{column}");

                            parameters.Add($"@{column}", row[column] ?? DBNull.Value);
                        }

                        string query = $"INSERT INTO DataForAnalysis ({string.Join(",", columnNames)}) " +
                                       $"VALUES ({string.Join(",", paramNames)})";

                        connection.Execute(query, parameters);
                    }
                }

                Log.Information("Data inserted successfully using Dapper.");
            }
            catch (Exception ex)
            {
                Log.Error("InsertData error: {Message}", ex.Message);
            }
        }


        private DataTable  LoadDataTo_DataTable()
        {
            try
            {
                string connectionString = $"Data Source={sqliteDbPath};Version=3;";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM dataset where Status=0;";

                    using (var command = new SQLiteCommand(query, connection))
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return dataTable;
                    }
                }
                return null; 
            }
            catch (Exception ex)
            {
                
                Log.Error("LoadDataIntoGrid error: {Message}", ex.Message);
                return null;
            }
        }





        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (!backgroundWorker.CancellationPending)
                {
                    // background task logic
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
    }
}
