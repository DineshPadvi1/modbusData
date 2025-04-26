using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uniproject.Classes.RMC;
using Uniproject.SW_Configuration;
using Uniproject.UtilityTools;

namespace Uniproject.Classes
{
    public static class clsFunctions
    {

        public static string batchconnstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\";
        // public static string batchconnstrR7 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\Database\\";      //BT
        public static string batchconnstrR7 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GetConnectionstrSetup_Path(); //BK14april

        public static string imgPath = Application.StartupPath + "\\img";
        public static string solutionPath = clsFunctions.GetConnectionstrSetup_Path();
        public static string RegFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\vtIEpmLP";

        public static string regName = "VIPLHMSCADA";
        public static string serialKey = "QTR56-MLYH1-PTS8T-4SMH9-PG89Y";

        public static string DeviceID = "";                 // BhaveshT
        public static string dept = "";                     // 18 may
        public static string aliasName = "";
        public static string activeDeptName = "";
        public static string activePlantCode = "";
        public static string activeDeviceID = "";
        public static string activeDesciption = "";
        public static string activePlantType = "";

        public static bool flag = false;

        public static string HMI_Unique_ID = "";

        public static string pingResponse = "";
        public static DataTable LatestUpdates_Dt = new DataTable();

        public static string expiryMessage = "";

        public static int DocketHours = 24;         // 24/04/2024 : BhaveshT
        public static DateTime timeNow;
        public static DateTime minDateTime;

        public static bool shouldClose = false;

        //-------------------------------------------


        public static string TipperInterval = "59";         // 25/04/2024 : BhaveshT

        public static bool isExpiryCheckedForPType = false;

        //static OleDbConnection con = new OleDbConnection(batchconnstr + "HotMixScada.mdb;");
        internal static OleDbConnection con = new OleDbConnection(batchconnstr + "UniPro.mdb;Persist Security Info=true ;Jet OLEDB:Database Password=Unipro0073; ");//

        // 24/11/2023 BhaveshT - made it public
        public static OleDbConnection conns = new OleDbConnection(batchconnstr + "Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;");//Annu

        //private static OleDbConnection con = new OleDbConnection(clsFunctions.batchconnstr + "UniPro.mdb;Persist Security Info=true ;Jet OLEDB:Database Password=Unipro0073; ");
        //private static OleDbConnection conns = new OleDbConnection(clsFunctions.batchconnstr + "Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;");

        private static string mydbstr = nameof(mydbstr);

        public static string loggerfunc()
        {
            string sFi1ePath = "";
            try
            {
                // added by BhaveshT
                string sF01derName = Application.StartupPath + @"\Logs\";
                if (!Directory.Exists(sF01derName))
                    Directory.CreateDirectory(sF01derName);

                sFi1ePath = sF01derName + "Error_Unipro.log";
            }
            catch (Exception e)
            {
                clsFunctions_comman.ErrorLog("error in logger function" + e.Message);
            }
            return sFi1ePath;
        }

        // 08/02/2024 - BhaveshT : Use Unipro_Setup table instead of Connection_setup ------------------------

        public static string getConnectionString = GetConnString();


        public static string GetConnString()
        {
            try
            {
                getConnectionString = Convert.ToString(clsFunctions.loadSingleValueSetup("SELECT ConnectionString FROM Unipro_Setup where Status = 'Y' "));
                return getConnectionString;
            }
            catch (Exception ex)
            {
                return getConnectionString;
            }
        }

        //------------------------

        public static void getdateTimeString()
        {
            try
            {
                //timeNow = Convert.ToDateTime(DateTime.Now.ToString("hh:mm:ss tt"));
                //minDateTime = Convert.ToDateTime(DateTime.Now.ToString());

                //timeNow = DateTime.Parse(DateTime.Now.ToLongTimeString());
                //minDateTime = DateTime.Parse(DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("getdateTimeString" + ex.Message);
            }
        }

        public static string IP = "";
        public static string Port = "";

        public static string VIPLBatchNo = "";
        public static string ClientBatchNo = "";
        public static string ClientDate = "";
        public static string ClientTime = "";
        public static DataTable viplList = new DataTable();
        public static DataTable clientList = new DataTable();
        public static DataTable viplTransList = new DataTable();
        public static DataTable clientTransList = new DataTable();
        public static string desc = "";
        public static string PlantType;
        public static string DataImportPath;
        public static string IsEgaleReg;
        public static int IsRegSoftware;
        public static string regfilestatus;
        public static OleDbConnection client_conn;
        public static MySqlConnection MySqlcilent;
        public static string regNo;
        public static string VITPLTableName;
        public static string SourceTableName;
        public static string VITPLTableNameBD;
        public static string SourceTableNameBD;

        // 24/11/2023 BhaveshT - made it public
        public static DataTable sourceTable;
        public static DataTable sourceTrnsTable;

        public static string batchendflag;
        public static string endtime;
        public static string CustCode;
        public static string sitename;
        public static string plantcode;

        //added by Dinesh date:21/12/2023
        static DateTime Expirydate = default(DateTime);

        //added by Dinesh for fetching api from table

        public static string setIpAddress = "";
        public static string setPort = "";
        public static string URL = "";
        public static string regURL = "";
        public static string protocol = "http";
        //for Bitumen
        public static string endpoint = "";
        public static string plantEndPoint = "";

        // for RMC
        public static string batchDetails_endpoint = "";
        public static string batch_endpoint = "";
        public static bool validHostName = false;
        //this varibale is for checking expiry of plant api
        public static string apiLink = "";
        public static bool protoolFlag = false;

        static clsFunctions()
        {
            getdateTimeString();
            //DataTable dataTable = new DataTable();
            //try
            //{
            //}
            //catch (Exception ex)
            //{
            //}
        }


        public static string plantCode = "";    // clsFunctions.loadSingleValueSetup("Select PlantCode from PlantSetup"); // 19 may

        //----------------------- Insert Error Details in Plant_Error_Log Table ------------------------

        public static void InsertToErrorLogTable(int Error_No, string Error_Message)
        {
            try
            {
                string insertErrorLog = "Insert into Plant_Error_Log (PlantCode, Date_Time, Error_No, Error_Message, isSended) " +
                          "values ('" + clsFunctions.plantCode + "', '" + DateTime.Now + "', '" + Error_No + "', '" + Error_Message + "', '0')";

                clsFunctions.AdoData(insertErrorLog);
                clsFunctions_comman.ErrorLog(" -> error code " + Error_No + "==>" + Error_Message);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsFunctions.ErrorLog("InsertToErrorLogTable() : " + ex.Message);
                clsFunctions.InsertToErrorLogTable(ex.HResult, ex.Message);
            }
        }

        //-----------------------------------------------------------------------------------------------

        public static string GetConnectionstrSetup_Path()
        {
            OleDbConnection connection = new OleDbConnection(clsFunctions.batchconnstr + "Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;");
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // 08 / 02 / 2024 - BhaveshT : Use UniproSetup table instead of DBInfo ------------------------------

                new OleDbDataAdapter(new OleDbCommand("Select path,pass from Unipro_Setup where status='Y'", connection)).Fill(dataTable);
                //new OleDbDataAdapter(new OleDbCommand("Select path,pass from DbInfo where status='Y'", connection)).Fill(dataTable);

                if ((uint)dataTable.Rows.Count <= 0)
                    return "";
                string str = dataTable.Rows[0]["path"].ToString();
                clsFunctions.DataImportPath = dataTable.Rows[0]["path"].ToString();
                dataTable.Rows[0]["pass"].ToString();
                clsFunctions.PlantType = dataTable.Rows[0]["pass"].ToString();
                return str;
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("Error - At GetConnectionSetup_Path() :" + ex.Message);
                return "";
            }
        }
        //-----------------------------------------------------------------------------------------------

        public static string GetConnectionstrSetup_Pass()
        {
            OleDbConnection connection = new OleDbConnection(clsFunctions.batchconnstr + "Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;");
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // 08/02/2024 - BhaveshT : Use Unipro_Setup table instead of DBInfo
                new OleDbDataAdapter(new OleDbCommand("Select path,pass from Unipro_Setup where status='Y'", connection)).Fill(dataTable);
                //new OleDbDataAdapter(new OleDbCommand("Select path,pass from DbInfo where status='Y'", connection)).Fill(dataTable);

                if ((uint)dataTable.Rows.Count <= 0U)
                    return "";
                string str = dataTable.Rows[0]["pass"].ToString();

                return str;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Error - At GetConnectionSetup_Pass() :" + ex.Message);
                return "";
            }
        }

        //-----------------------------------------------------------------------------------------------

        public static void setConString(string str)
        {
            clsFunctions.client_conn = new OleDbConnection(str);
        }

        public static void setConString(string str, string dbtype)
        {
            if (!dbtype.Contains("MS ACCESS"))
                return;
            clsFunctions.client_conn = new OleDbConnection(str);
        }

        public static DataTable getTables_client()
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("Column1", typeof(string));
            try
            {
                clsFunctions.client_conn.Open();
                foreach (DataRow dataRow in clsFunctions.client_conn.GetSchema("Tables").Select("TABLE_TYPE='TABLE' AND TABLE_NAME NOT LIKE 'MSys*'"))
                    dataTable2.Rows.Add(dataRow["TABLE_NAME"]);
            }
            catch (Exception ex)
            {
                clsFunctions.client_conn.Close();
                int num = (int)MessageBox.Show("Error--- At getTables_client: " + ex?.ToString());
            }
            clsFunctions.client_conn.Close();
            return dataTable2;
        }

        public static DataTable getColumns_client(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                clsFunctions.client_conn.Open();
                dataTable = clsFunctions.client_conn.GetSchema("Columns", new string[4]
                {
          null,
          null,
          query,
          null
                });
                ((OleDbType)dataTable.Rows[0]["DATA_TYPE"]).ToString();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Error --- At getColumns_client : " + ex?.ToString());
                clsFunctions.client_conn.Close();
            }
            clsFunctions.client_conn.Close();
            return dataTable;
        }

        public static DataTable getColumns_VIPL(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                dataTable = clsFunctions.con.GetSchema("Columns", new string[4]
                {
          null,
          null,
          query,
          null
                });
            }
            catch (Exception ex)
            {
                clsFunctions.con.Close();
                int num = (int)MessageBox.Show("Error--- At getColumns_VIPL: " + ex?.ToString());
            }
            clsFunctions.con.Close();
            return dataTable;
        }

        public static int AdoData_setup(string query)
        {
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                return new OleDbCommand(query, clsFunctions.conns).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("AdoData_setup : " + ex.Message + "\n query Description  ===========> " + query);
                return 0;
            }
        }
        public static void WriteToTXTfile(string Message, string txtFile)
        {
            //await Task.Run(() =>
            // {
            StreamWriter objSw = null;
            try
            {
                string sFolderName = Application.StartupPath + @"\Txt\HeaderandTransaction\";
                if (!Directory.Exists(sFolderName))
                {
                    Directory.CreateDirectory(sFolderName);
                }
                string sFilePath = sFolderName + txtFile + ".txt";

                objSw = new StreamWriter(sFilePath, true);
                objSw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ") + Message + Environment.NewLine);

            }
            catch (Exception ex)
            {
                ErrorLog("WriteDataToFile : " + ex.Message);
            }
            finally
            {
                if (objSw != null)
                {
                    objSw.Flush();
                    objSw.Dispose();
                }
            }
            //});
        }
        public static string GetConnectionstrSetupaccdb()
        {
            OleDbConnection connection = new OleDbConnection(clsFunctions.batchconnstr + "Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;");
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                new OleDbDataAdapter(new OleDbCommand("Select path,pass from DbInfo where status='Y'", connection)).Fill(dataTable);
                if ((uint)dataTable.Rows.Count <= 0U)
                    return "";
                string str1 = dataTable.Rows[0]["path"].ToString();
                clsFunctions.DataImportPath = dataTable.Rows[0]["path"].ToString();
                string str2 = dataTable.Rows[0]["pass"].ToString();
                clsFunctions.PlantType = dataTable.Rows[0]["pass"].ToString();
                return "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + str1 + "; Persist Security Info=true ;Jet OLEDB:Database Password='" + str2 + "';";
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("Error - At GetConnectionSetup() :" + ex.Message);
                return "";
            }
        }

        public static string GetConnectionstrSetup()
        {
            OleDbConnection connection = new OleDbConnection(clsFunctions.batchconnstr + "Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;");
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // 08/02/2024 - BhaveshT : Use Unipro_Setup table instead of DBInfo
                new OleDbDataAdapter(new OleDbCommand("Select path,pass from Unipro_Setup where status='Y'", connection)).Fill(dataTable);
                //new OleDbDataAdapter(new OleDbCommand("Select path,pass from DbInfo where status='Y'", connection)).Fill(dataTable);

                if ((uint)dataTable.Rows.Count <= 0U)
                    return "";
                string str1 = dataTable.Rows[0]["path"].ToString();
                clsFunctions.DataImportPath = dataTable.Rows[0]["path"].ToString();
                string str2 = dataTable.Rows[0]["pass"].ToString();
                clsFunctions.PlantType = dataTable.Rows[0]["pass"].ToString();
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + str1 + "; Persist Security Info=true ;Jet OLEDB:Database Password='" + str2 + "';";
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("Error - At GetConnectionSetup() :" + ex.Message);
                return "";
            }
        }

        public static string GetType()
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();

                // 08/02/2024 - BhaveshT : Use Unipro_Setup table instead of DBInfo

                new OleDbDataAdapter(new OleDbCommand("Select * from Unipro_Setup where status='Y'", clsFunctions.conns)).Fill(dataTable);
                //new OleDbDataAdapter(new OleDbCommand("Select * from DbInfo where status='Y'", clsFunctions.conns)).Fill(dataTable);

                if ((uint)dataTable.Rows.Count <= 0U)
                    return "";
                dataTable.Rows[0]["path"].ToString();
                clsFunctions.DataImportPath = dataTable.Rows[0]["path"].ToString();
                dataTable.Rows[0]["pass"].ToString();
                clsFunctions.PlantType = dataTable.Rows[0]["pass"].ToString();
                clsFunctions.desc = dataTable.Rows[0]["Description"].ToString();
                return clsFunctions.desc;
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("Error - At GetConnectionSetup() :" + ex.Message);
                return "";
            }
        }

        public static string gettruckinterval(string qry)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                new OleDbDataAdapter(new OleDbCommand(qry, clsFunctions.conns)).Fill(dataTable);
                if ((uint)dataTable.Rows.Count > 0U)
                    return dataTable.Rows[0]["truckinterval"].ToString();
                return "";
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("getTruckInterval Error - At GetConnectionSetup() :" + ex.Message);
                return "";
            }
        }

        //--------------------------------------------------------------

        public static void ErrorLog(string sMessage)
        {
            StreamWriter streamWriter = (StreamWriter)null;

            try
            {

                string path = Application.StartupPath + "\\Logs\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //streamWriter = new StreamWriter(path + "Error_log" + DateTime.Now.ToString("dd_MM_yyyy") + ".log", true);
                //streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy") + " " + sMessage + Environment.NewLine);

                // 19/01/2024 - BhaveshT : renamed the file

                streamWriter = new StreamWriter(path + "Error_Unipro_" + DateTime.Now.ToString("MMMM-yyyy") + ".log", true);
                streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt - ") + " " + sMessage + Environment.NewLine);

            }
            catch (Exception ex)
            {
                //int num = (int)MessageBox.Show("error: " + ex.Message + " Description " + sMessage);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
        }

        //--------------------------------------------------------------

        // 22/07/2024 : BhaveshT - This Log function will write logs about service status in separate txt File: ErrorLog_UniproService.

        public static void ErrorServiceLog(string sMessage)
        {
            StreamWriter streamWriter = (StreamWriter)null;
            try
            {

                string path = Application.StartupPath + "\\Logs\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //streamWriter = new StreamWriter(path + "Error_log" + DateTime.Now.ToString("dd_MM_yyyy") + ".log", true);
                //streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy") + " " + sMessage + Environment.NewLine);

                // 19/01/2024 - BhaveshT : renamed the file

                streamWriter = new StreamWriter(path + "ErrorLog_UniproService_" + DateTime.Now.ToString("MMMM-yyyy") + ".log", true);
                streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt - ") + " " + sMessage + Environment.NewLine);

            }
            catch (Exception ex)
            {
                //int num = (int)MessageBox.Show("error: " + ex.Message + " Description " + sMessage);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
        }

        //--------------------------------------------------------------

        // 01/08/2024 : BhaveshT - This Log function will write logs about service status in separate txt File: ErrorServiceLog_PWD_RMC.

        public static void ErrorServiceLog_PWD_RMC(string sMessage)
        {
            StreamWriter streamWriter = (StreamWriter)null;
            try
            {

                string path = Application.StartupPath + "\\Logs\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //streamWriter = new StreamWriter(path + "Error_log" + DateTime.Now.ToString("dd_MM_yyyy") + ".log", true);
                //streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy") + " " + sMessage + Environment.NewLine);

                // 19/01/2024 - BhaveshT : renamed the file

                streamWriter = new StreamWriter(path + "ErrorLog_UniproService_PWD_RMC_" + DateTime.Now.ToString("MMMM-yyyy") + ".log", true);
                streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt - ") + " " + sMessage + Environment.NewLine);

            }
            catch (Exception ex)
            {
                //int num = (int)MessageBox.Show("error: " + ex.Message + " Description " + sMessage);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
        }

        //--------------------------------------------------------------

        public static void chkdire(string sFolderName)
        {
            try
            {
                if (Directory.Exists(sFolderName))
                    return;
                Directory.CreateDirectory(sFolderName);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("chkdire : " + ex.Message + " Description " + sFolderName);
            }
        }

        //--------------------------------------------------------------

        public static void DATAVAL(string sMessage)
        {
            StreamWriter streamWriter = (StreamWriter)null;

            try
            {
                string folderName = DateTime.Now.ToString("MMM") + "_" + DateTime.Now.Year.ToString();

                string path = Application.StartupPath + "\\DATAVAL\\" + folderName;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                streamWriter = new StreamWriter(path + "\\DATA " + DateTime.Now.ToString("dd-MM-yyyy") + ".log", true);
                streamWriter.WriteLine($"{DateTime.Now:hh:mm:ss tt} " + sMessage);

            }
            catch (Exception ex)
            {
                //int num = (int)MessageBox.Show(nameof(DATAVAL) + ex.Message);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
        }

        //--------------------------------------------------------------

        public static void DATAVAL_HoldingRegister(string sMessage)
        {
            StreamWriter streamWriter = (StreamWriter)null;

            try
            {
                string folderName = DateTime.Now.ToString("MMM") + "_" + DateTime.Now.Year.ToString();

                string path = Application.StartupPath + "\\DATAVAL\\" + folderName;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                streamWriter = new StreamWriter(path + "\\DATA_HoldingRegister " + DateTime.Now.ToString("dd-MM-yyyy") + ".log", true);
                streamWriter.WriteLine($"{DateTime.Now:hh:mm:ss tt} " + sMessage);

            }
            catch (Exception ex)
            {
                //int num = (int)MessageBox.Show(nameof(DATAVAL) + ex.Message);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
        }

        //--------------------------------------------------------------

        public static void DATAVAL_InputStatus(string sMessage)
        {
            StreamWriter streamWriter = (StreamWriter)null;

            try
            {
                string folderName = DateTime.Now.ToString("MMM") + "_" + DateTime.Now.Year.ToString();

                string path = Application.StartupPath + "\\DATAVAL\\" + folderName;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                streamWriter = new StreamWriter(path + "\\DATA_InputStatus " + DateTime.Now.ToString("dd-MM-yyyy") + ".log", true);
                streamWriter.WriteLine($"{DateTime.Now:hh:mm:ss tt} " + sMessage);

            }
            catch (Exception ex)
            {
                //int num = (int)MessageBox.Show(nameof(DATAVAL) + ex.Message);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
        }
        //--------------------------------------------------------------

        public static void checknewcolumn(string clm, string datatype, string table)
        {
            try
            {
                string cmdText = "Select " + clm + " from " + table + " ";
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                new OleDbDataAdapter(new OleDbCommand(cmdText, clsFunctions.con)).Fill(new DataTable());
            }
            catch (Exception ex)
            {
                clsFunctions.AdoData("alter table " + table + " add column " + clm + " " + datatype);
            }
        }

        //--Added on 27-Apr-2024 to change datatype of given column of given table
        public static void checkAlterColumn(string clm, string datatype, string table)
        {
            try
            {
                string cmdText = "SELECT " + clm + " FROM " + table + " ";
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                clsFunctions.AdoData("ALTER TABLE " + table + " ALTER COLUMN " + clm + " " + datatype);
            }
            catch { }
        }

        public static void checkDropColumn(string clm, string table)
        {
            try
            {
                string cmdText = "SELECT " + clm + " FROM " + table + " ";
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                clsFunctions.AdoData("ALTER TABLE " + table + " DROP COLUMN " + clm + " ");
            }
            catch { }
        }

        //------------30/12/2023 : BhaveshT - To create new column in Unipro database with default value -------------------------
        public static void checknewcolumnWithDefaultVal(string clm, string datatype, string table, string defaultVal)
        {
            try
            {
                string cmdText = "Select " + clm + " from " + table + " ";
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                new OleDbDataAdapter(new OleDbCommand(cmdText, clsFunctions.con)).Fill(new DataTable());
            }
            catch (Exception ex)
            {
                //clsFunctions.AdoData("alter table " + table + " add column " + clm + " " + datatype);

                clsFunctions.AdoData("ALTER TABLE " + table + " ADD COLUMN " + clm + " " + datatype + " DEFAULT " + defaultVal + " ");
            }
        }

        //------- 30/12/2023 : BhaveshT - To create new Table in Setup database -----------------------------
        public static void CreateNewTableInSetup(string tableName)
        {
            try
            {
                string cmdText = $"SELECT TOP 1 * FROM {tableName}";

                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();

                new OleDbDataAdapter(new OleDbCommand(cmdText, clsFunctions.conns)).Fill(new DataTable());

            }
            catch (Exception ex)
            {
                string createTableCmd = $"CREATE TABLE {tableName}";

                clsFunctions.AdoData_setup(createTableCmd);
                clsFunctions.checkNewColumnInSetup("ID", "AUTOINCREMENT", tableName);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        //------- 19/01/2024 : BhaveshT - To create new Table in Unipro database -----------------------------
        public static void CreateNewTableInUnipro(string tableName)
        {
            try
            {
                string cmdText = $"SELECT TOP 1 * FROM {tableName}";

                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();

                new OleDbDataAdapter(new OleDbCommand(cmdText, clsFunctions.con)).Fill(new DataTable());

            }
            catch (Exception ex)
            {
                string createTableCmd = $"CREATE TABLE {tableName}";

                clsFunctions_comman.Ado(createTableCmd);
                clsFunctions.checknewcolumn("ID", "AUTOINCREMENT", tableName);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        //------- 26/12/2023 : BhaveshT - To add new column in Setup database -----------------------------
        public static void checkNewColumnInSetup(string clm, string datatype, string table)
        {
            try
            {
                string cmdText = "Select " + clm + " from " + table + " ";
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                new OleDbDataAdapter(new OleDbCommand(cmdText, clsFunctions.conns)).Fill(new DataTable());
            }
            catch (Exception ex)
            {
                clsFunctions.AdoData_setup("alter table " + table + " add column " + clm + " " + datatype);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void FillCombo(string Query, ComboBox cmb)
        {
            try
            {
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand(Query, clsFunctions.con));
                DataTable dataTable = new DataTable();

                cmb.DataSource = null;
                cmb.Items.Clear();
                oleDbDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count <= 0)
                    return;
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    if (row[0].ToString().Trim() != "")
                        cmb.Items.Add(row[0]);
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("FillCombo : " + ex.Message);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void FillCombo_setup(string Query, ComboBox cmb)
        {
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand(Query, clsFunctions.conns));
                DataTable dataTable = new DataTable();
                cmb.Items.Clear();
                oleDbDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count <= 0)
                    return;
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    if (row[0].ToString().Trim() != "")
                        cmb.Items.Add(row[0]);
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("FillCombo : " + ex.Message);
            }
        }

        public static void FillComboValueID(
          string Query,
          ComboBox cmb,
          string DisplayMember,
          string ValueMember)
        {
            try
            {
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand(Query, clsFunctions.con));
                DataTable dataTable = new DataTable();
                cmb.Items.Clear();
                oleDbDataAdapter.Fill(dataTable);
                cmb.DisplayMember = DisplayMember;
                cmb.ValueMember = ValueMember;
                cmb.DataSource = (object)dataTable;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("FillCombo : " + ex.Message);
            }
        }

        public static string loadSingleValue(string Query)
        {
            string str = "0";
            try
            {
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand(Query, clsFunctions.con));
                DataTable dataTable = new DataTable();
                oleDbDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                    str = dataTable.Rows[0][0].ToString();
                return str;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("FillCombo : " + ex.Message + "query Description " + Query);
                return "0";
            }
        }

        public static string loadSinglevalue_HotMix(string query)
        {
            DataTable dataTable = clsFunctions.fillDatatable(query);
            if ((uint)dataTable.Rows.Count > 0U)
                return dataTable.Rows[0][0].ToString();
            return "0";
        }

        public static string loadSinglevalue_setup(string query)
        {
            DataTable dataTable = clsFunctions.fillDatatable_setup(query);
            if ((uint)dataTable.Rows.Count > 0U)
                return dataTable.Rows[0][0].ToString();
            return "0";
        }

        public static bool GetUniqueData()
        {
            try
            {
                DataTable dataTable = clsFunctions.fillDatatable("Select IP,Port from APIDetails");
                if ((uint)dataTable.Rows.Count <= 0U)
                    return false;
                clsFunctions.IP = dataTable.Rows[0]["IP"].ToString();
                clsFunctions.Port = dataTable.Rows[0]["Port"].ToString();
                return true;
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("GetUniqueData : " + ex.Message);
                return false;
            }
        }

        public static DataTable fillDatatable(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();

                new OleDbDataAdapter(new OleDbCommand(query, clsFunctions.con)).Fill(dataTable);
            }
            catch (Exception ex)
            {
                ////clsFunctions.ErrorLog("FillDataTable : " + ex.Message + "query Description " + query);
            }
            return dataTable;
        }

        public static DataTable fillDatatable_client(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                new OleDbDataAdapter(new OleDbCommand(query, clsFunctions.client_conn)).Fill(dataTable);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("fillDatatable_client : " + ex.Message + "query Description " + query);
            }
            return dataTable;
        }

        public static string loadSinglevalue_client(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                new OleDbDataAdapter(new OleDbCommand(query, clsFunctions.client_conn)).Fill(dataTable);
                if ((uint)dataTable.Rows.Count > 0U)
                    return dataTable.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("fillDatatable_client : " + ex.Message + "query Description " + query);
            }
            return "0";
        }

        public static DataTable fillDatatable_setup(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                new OleDbDataAdapter(new OleDbCommand(query, clsFunctions.conns)).Fill(dataTable);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("FillDataTable : " + ex.Message + "query Description " + query);
            }
            return dataTable;
        }

        public static bool checkdata(string query)
        {
            try
            {
                if (clsFunctions.fillDatatable(query).Rows.Count == 0)
                    return false;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("FillDataTable : " + ex.Message + "query Description " + query);
            }
            return true;
        }

        public static int AdoDataClient(string query)
        {
            try
            {
                if (clsFunctions.client_conn.State == ConnectionState.Closed)
                    clsFunctions.client_conn.Open();
                return new OleDbCommand(query, clsFunctions.client_conn).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ////clsFunctions.ErrorLog("AdoData : " + ex.Message + "\n query Description  ===========> " + query);
                return 0;
            }
        }

        public static int AdoData(string query)
        {
            try
            {
                if (clsFunctions.con.State == ConnectionState.Closed)
                    clsFunctions.con.Open();
                return new OleDbCommand(query, clsFunctions.con).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("Exception: clsFunctions.AdoData - " + ex.Message + " | query => " + query);
                return 0;
            }
        }

        public static int ADOSetup(string query)
        {
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                return new OleDbCommand(query, clsFunctions.conns).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("ADOSetup : " + ex.Message + "\n query Description  ===========> " + query);
                return 0;
            }
        }

        public static int UpdateProduction(string query)
        {
        a: try
            {
                if (!(clsFunctions.PlantType.ToString() == "1"))
                    return 0;
                OleDbConnection connection = new OleDbConnection(clsFunctions.GetConnectionstrSetup());
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return new OleDbCommand(query, connection).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("UpdateProduction : " + ex.Message + "query Description " + query);
                query = query.Replace("test='1'", "FILLER2=1");
                query = query.Replace("test='0'", "FILLER2=0");
                goto a;
                return 0;
            }
        }

        public static int UpdateProductionReport(string query)
        {
        a: try
            {
                //if (!(clsFunctions.PlantType.ToString() == "1"))
                //    return 0;

                OleDbConnection connection = new OleDbConnection(clsFunctions.GetConnectionstrSetup());
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return new OleDbCommand(query, connection).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("UpdateProductionReport : " + ex.Message + "| query - " + query);

                return 0;
            }
        }


        public static int GetMaxId(string query)
        {
            try
            {
                DataTable dataTable = clsFunctions.fillDatatable(query);
                if (dataTable.Rows[0][0].ToString() == "" || dataTable.Rows[0][0].ToString() == "")
                    return 1;
                return Convert.ToInt32(dataTable.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("GetMaxId : " + ex.Message + "query Description " + query);
                return 0;
            }
        }

        public static int GetMaxPlus1(string query)     // BhaveshT 25082023
        {
            try
            {
                DataTable dataTable = clsFunctions.fillDatatable(query);
                if (dataTable.Rows[0][0].ToString() == "" || dataTable.Rows[0][0].ToString() == "")
                    return 1;
                return Convert.ToInt32((Convert.ToDouble(dataTable.Rows[0][0]) + 1).ToString());
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("GetMaxId : " + ex.Message + "query Description " + query);
                return 0;
            }
        }

        public static long GetMaxLongId(string query)
        {
            try
            {
                DataTable dataTable = clsFunctions.fillDatatable(query);
                if (dataTable.Rows[0][0].ToString() == "" || dataTable.Rows[0][0].ToString() == "")
                    return 1;
                return (long)(Convert.ToInt32(dataTable.Rows[0][0].ToString()) + 1);
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog("GetMaxLongId : " + ex.Message + "query Description " + query);
                return 0;
            }
        }

        public static void fillGridView(string Query, DataGridView dgv1)
        {
            try
            {
                DataTable dataTable = clsFunctions.fillDatatable(Query);
                dgv1.DataSource = (object)null;
                dgv1.DataSource = (object)dataTable;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Fill Grid View : " + ex.Message + "query Description " + Query);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string UpdateRenewalDt(string renewalDt)
        {
            if (renewalDt != "")
            {
                DataTable dataTable = new DataTable();
                try
                {
                    if (clsFunctions.conns.State == ConnectionState.Closed)
                        clsFunctions.conns.Open();
                    //clsFunctions.AdoData_setup("UPDATE setupinfo SET dtToDate = #" + renewalDt.ToString() + "#");
                    clsFunctions.AdoData_setup("UPDATE PlantSetup SET PlantExpiry = #" + renewalDt.ToString() + "#");

                    return "1";
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show("Error - At UpdateRenewalDt() :" + ex.Message);
                    //clsFunctions.ErrorLog("Error - At UpdateRenewalDt() :" + ex.Message);
                    return "0";
                }
            }
            else
            {
                clsFunctions_comman.ErrorLog("UpdateRenewalDt: renewal Date can't be empty: " + renewalDt);
                return "0";
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string UpdateRenewalDtAtServerMapping(string renewalDt)       // 02/08/2024
        {
            if (renewalDt != "")
            {
                DataTable dataTable = new DataTable();
                try
                {
                    string rDt = Convert.ToDateTime(renewalDt).ToString("yyy-MM-dd");

                    if (clsFunctions.conns.State == ConnectionState.Closed)
                        clsFunctions.conns.Open();
                    //clsFunctions.AdoData_setup("UPDATE setupinfo SET dtToDate = #" + renewalDt.ToString() + "#");

                    clsFunctions.AdoData_setup("UPDATE ServerMapping SET PlantExpiryDate = '" + rDt + "' WHERE Deptname = '" + clsFunctions.activeDeptName + "' AND AliasName = '" + clsFunctions.aliasName + "' ");
                    clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET PlantExpiryDate = '" + rDt + "' WHERE Deptname = '" + clsFunctions.activeDeptName + "' AND AliasName = '" + clsFunctions.aliasName + "' ");

                    return "1";
                }
                catch (Exception ex)
                {
                    //int num = (int)MessageBox.Show("Error - At UpdateRenewalDtAtServerMapping() :" + ex.Message);
                    clsFunctions.ErrorLog("Error - At UpdateRenewalDtAtServerMapping() :" + ex.Message);

                    return "0";
                }
            }
            else
            {
                clsFunctions_comman.ErrorLog("UpdateRenewalDt: renewal Date can't be empty: " + renewalDt);
                return "0";
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public static SerialPort SetSerialPortValue(SerialPort serialPort1)
        {
            try
            {

                clsFunctions.ErrorLog("[INFO] clsFunctions - Inside SetSerialPortValue()");

                DataTable dataTable = clsFunctions.fillDatatable("select * from tblportsetting");
                if ((uint)dataTable.Rows.Count <= 0U)
                    return serialPort1;

                serialPort1.PortName = dataTable.Rows[0]["Commport"].ToString();

                serialPort1.BaudRate = Convert.ToInt32(dataTable.Rows[0]["bundrate"]);
                string str1 = dataTable.Rows[0]["parity"].ToString();
                if (!(str1 == "None"))
                {
                    if (!(str1 == "Even"))
                    {
                        if (!(str1 == "Mark"))
                        {
                            if (!(str1 == "Odd"))
                            {
                                if (str1 == "Space")
                                    serialPort1.Parity = Parity.Space;
                            }
                            else
                                serialPort1.Parity = Parity.Odd;
                        }
                        else
                            serialPort1.Parity = Parity.Mark;
                    }
                    else
                        serialPort1.Parity = Parity.Even;
                }
                else
                    serialPort1.Parity = Parity.None;
                serialPort1.DataBits = (int)Convert.ToInt16(dataTable.Rows[0]["databit"]);
                string str2 = dataTable.Rows[0]["stopbit"].ToString();
                if (!(str2 == "None"))
                {
                    if (!(str2 == "One"))
                    {
                        if (!(str2 == "OnePointFive"))
                        {
                            if (str2 == "Two")
                                serialPort1.StopBits = StopBits.Two;
                        }
                        else
                            serialPort1.StopBits = StopBits.OnePointFive;
                    }
                    else
                        serialPort1.StopBits = StopBits.One;
                }
                else
                    serialPort1.StopBits = StopBits.None;
                serialPort1.DtrEnable = true;
                serialPort1.RtsEnable = true;
                return serialPort1;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Set Serial Port Value : " + ex.Message);

                clsFunctions.ErrorLog("[Exception] SetSerialPortValue : " + ex.Message);

                return serialPort1;
            }
        }

        static clsVariables clsvar = new clsVariables();
        public static void loadworkdata(ComboBox workname)
        {
            clsFunctions.checknewcolumn("iscompleted", "Text(255)", "workorder");
            clsFunctions.FillCombo("Select Distinct workname from workorder where iscompleted <> 'Y' ", workname);
        }
        public static string loadSingleValueSetup(string Query)
        {
            string str = "0";
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand(Query, clsFunctions.conns));
                DataTable dataTable = new DataTable();
                oleDbDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                    str = dataTable.Rows[0][0].ToString();
                return str;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("loadSingleValueSetup : " + ex.Message + "query Description " + Query);
                return str;
            }
        }


        public static void SelectDistictBatch(ComboBox cmb, string DateToday, string DBType)
        {
            clsFunctions.VIPLBatchNo = clsFunctions.loadSingleValueSetup("Select Batch_No FROM DataHeaderTableSync where Type='VIPL' and Flag='Y'");
            clsFunctions.ClientBatchNo = clsFunctions.loadSingleValueSetup("Select Batch_No FROM DataHeaderTableSync where Type='Client' and Flag='Y'");
            clsFunctions.ClientDate = clsFunctions.loadSingleValueSetup("Select Batch_Date FROM DataHeaderTableSync where Type='Client' and Flag='Y'");
            clsFunctions.ClientTime = clsFunctions.loadSingleValueSetup("Select Batch_Time FROM DataHeaderTableSync where Type='Client' and Flag='Y'");
            try
            {
                string query = "";

                cmb.Items.Clear();
                if (clsFunctions.client_conn.State == ConnectionState.Closed)
                    clsFunctions.client_conn.Open();
                string str = clsFunctions.loadSinglevalue_HotMix("Select max(" + clsFunctions.VIPLBatchNo + ") from " + clsFunctions.VITPLTableName);

                //-------------------------------------------------------

                clsFunctions.DocketHours = clsFunctions.GetDocketHours();
                clsFunctions.minDateTime = clsFunctions.SubtractHours(Convert.ToDateTime(clsFunctions.minDateTime), clsFunctions.DocketHours);

                clsFunctions.minDateTime = Convert.ToDateTime(Convert.ToDateTime(clsFunctions.minDateTime).ToString("yyyy-MM-dd HH:mm:ss"));
                DateToday = Convert.ToDateTime(DateToday).ToString("yyyy-MM-dd");

                //-------------------------------------------------------

                if (DBType == "MS ACCESS")
                {
                    if (str != "")
                    {
                        //query = "select distinct " + clsFunctions.ClientBatchNo + " from " + clsFunctions.SourceTableName + " where " + clsFunctions.ClientBatchNo + ">" + str + " order by " + clsFunctions.ClientBatchNo + " DESC";
                        //query = "select * from " + clsFunctions.SourceTableName + " order by " + clsFunctions.ClientBatchNo + " DESC";          //BhaveshT - 18/12/2023
                        //query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + "= #" + DateToday + "#  order by " + clsFunctions.ClientBatchNo + " DESC";      //BhaveshT - 01/06/2024

                        //query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + ">= #" + clsFunctions.minDateTime.ToString("yyyy-MM-dd") + "# AND " + clsFunctions.ClientTime + " > #" + clsFunctions.minDateTime.ToString("hh:mm:ss tt") + "# order by " + clsFunctions.ClientBatchNo + " DESC";      //BhaveshT - 25/06/2024
                        query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + ">= #" + clsFunctions.minDateTime.ToString("yyyy-MM-dd") + "# order by " + clsFunctions.ClientBatchNo + " DESC";      //BhaveshT - 03/09/2024
                    }
                    else
                    {
                        //query = "select * from " + clsFunctions.SourceTableName + " order by " + clsFunctions.ClientBatchNo + " DESC";
                        //query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + "= #" + DateToday + "#  order by " + clsFunctions.ClientBatchNo + " DESC";       //BhaveshT - 01/06/2024

                        //query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + ">= #" + clsFunctions.minDateTime.ToString("yyyy-MM-dd") + "# AND " + clsFunctions.ClientTime + "> #" + clsFunctions.minDateTime.ToString("HH:mm:ss") + "# order by " + clsFunctions.ClientBatchNo + " DESC";      //BhaveshT - 25/06/2024
                        query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + ">= #" + clsFunctions.minDateTime.ToString("yyyy-MM-dd") + "# order by " + clsFunctions.ClientBatchNo + " DESC";      //BhaveshT - 03/09/2024
                    }
                }

                if (DBType == "ACC DB")
                {
                    if (str != "")
                    {
                        //query = "select distinct " + clsFunctions.ClientBatchNo + " from " + clsFunctions.SourceTableName + " where " + clsFunctions.ClientBatchNo + ">" + str + " order by " + clsFunctions.ClientBatchNo + " DESC";
                        //query = "select * from " + clsFunctions.SourceTableName + " order by " + clsFunctions.ClientBatchNo + " DESC";          //BhaveshT - 18/12/2023
                        //query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + "= #" + DateToday + "#  order by " + clsFunctions.ClientBatchNo + " DESC";      //BhaveshT - 01/06/2024
                        //query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + ">= #" + clsFunctions.minDateTime.ToString("yyyy-MM-dd") + "# AND " + clsFunctions.ClientTime + " < #" + clsFunctions.minDateTime.ToString("hh:mm:ss tt") + "# order by " + clsFunctions.ClientBatchNo + " DESC";      //BhaveshT - 25/06/2024

                        query = "SELECT * FROM " + clsFunctions.SourceTableName +
                               " WHERE " + clsFunctions.ClientDate + " >= #" + clsFunctions.minDateTime.ToString("yyyy-MM-dd") + "#" +
                               " AND Format(" + clsFunctions.ClientTime + ", 'hh:mm:ss tt') < #" + clsFunctions.minDateTime.ToString("hh:mm:ss tt") + "#" +
                               " ORDER BY " + clsFunctions.ClientBatchNo + " DESC";                                                                                     //BhaveshT - 16/08/2024 
                    }
                    else
                    {
                        //query = "select * from " + clsFunctions.SourceTableName + " order by " + clsFunctions.ClientBatchNo + " DESC";
                        //query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + "= #" + DateToday + "#  order by " + clsFunctions.ClientBatchNo + " DESC";       //BhaveshT - 01/06/2024
                        //query = "select * from " + clsFunctions.SourceTableName + " WHERE " + clsFunctions.ClientDate + ">= #" + clsFunctions.minDateTime.ToString("yyyy-MM-dd") + "# AND " + clsFunctions.ClientTime + "< #" + clsFunctions.minDateTime.ToString("hh:mm:ss tt") + "# order by " + clsFunctions.ClientBatchNo + " DESC";      //BhaveshT - 25/06/2024

                        query = "SELECT * FROM " + clsFunctions.SourceTableName +
                               " WHERE " + clsFunctions.ClientDate + " >= #" + clsFunctions.minDateTime.ToString("yyyy-MM-dd") + "#" +
                               " AND Format(" + clsFunctions.ClientTime + ", 'hh:mm:ss tt') < #" + clsFunctions.minDateTime.ToString("hh:mm:ss tt") + "#" +
                               " ORDER BY " + clsFunctions.ClientBatchNo + " DESC";                                                                                     //BhaveshT - 16/08/2024 

                    }
                }

                DataTable dataTable = clsFunctions.fillDatatable_client(query);
                cmb.Items.Clear();
                if (dataTable.Rows.Count <= 0)
                    return;
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    if (row[clsFunctions.ClientBatchNo].ToString().Trim() != "")
                    {
                        if (!cmb.Items.Contains(row[clsFunctions.ClientBatchNo]))
                        {
                            cmb.Items.Add(row[clsFunctions.ClientBatchNo]);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("SelectDistictBatch exception:" + ex.Message);
            }
        }

        public static void SelectDistictBatch_OLD(ComboBox cmb)
        {
            string str = "where silica_act<>1";
            try
            {
                cmb.Items.Clear();
                if (clsFunctions.client_conn.State == ConnectionState.Closed)
                    clsFunctions.client_conn.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand("SELECT distinct batch_no FROM batch_act_data " + str + "  order by batch_no ", clsFunctions.client_conn));
                DataTable dataTable = new DataTable();
                cmb.Items.Clear();
                oleDbDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count <= 0)
                    return;
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    if (row[0].ToString().Trim() != "")
                        cmb.Items.Add(row[0]);
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("SelectDistictBatch exception:" + ex.Message);
            }
        }

        public static void FetchTableNameBatchOld()
        {
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM DataHeaderTableSync where Type='VIPL' AND Info='Fields'", clsFunctions.conns));
                DataTable dataTable = new DataTable();
                oleDbDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count <= 0)
                    return;
                clsFunctions.VITPLTableName = dataTable.Rows[0]["VITPLTableName"].ToString();
                clsFunctions.SourceTableName = dataTable.Rows[0]["SourceTableName"].ToString();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("FetchTableNameBatch exception:" + ex.Message);
            }
        }

        public static void FetchTableNameBatch()
        {
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                string cmdText1 = "SELECT * FROM DataHeaderTableSync where Type='VIPL' AND Info='Fields' AND Flag='Y'";
                string cmdText2 = "SELECT * FROM DataHeaderTableSync where Type='Client' AND Info='Fields' AND Flag='Y'";
                OleDbCommand selectCommand1 = new OleDbCommand(cmdText1, clsFunctions.conns);
                OleDbCommand selectCommand2 = new OleDbCommand(cmdText2, clsFunctions.conns);
                OleDbDataAdapter oleDbDataAdapter1 = new OleDbDataAdapter(selectCommand1);
                OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(selectCommand2);
                DataTable dataTable1 = new DataTable();
                DataTable dataTable2 = new DataTable();
                oleDbDataAdapter1.Fill(dataTable1);
                oleDbDataAdapter2.Fill(dataTable2);
                if (dataTable1.Rows.Count <= 0)
                    return;
                clsFunctions.VITPLTableName = dataTable1.Rows[0][58].ToString();
                clsFunctions.SourceTableName = dataTable2.Rows[0][58].ToString();
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("given for one or more"))
                    return;
                int num = (int)MessageBox.Show("FetchTableNameBatch exception:" + ex.Message);
            }
        }

        public static void FetchTableNameBatchDetails()
        {
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                string cmdText1 = "SELECT * FROM DataTransactionTableSync where Type='VIPL' AND Info='Fields' AND Flag='Y'";
                string cmdText2 = "SELECT * FROM DataTransactionTableSync where Type='Client' AND Info='Fields' AND Flag='Y'";
                OleDbCommand selectCommand1 = new OleDbCommand(cmdText1, clsFunctions.conns);
                OleDbCommand selectCommand2 = new OleDbCommand(cmdText2, clsFunctions.conns);
                OleDbDataAdapter oleDbDataAdapter1 = new OleDbDataAdapter(selectCommand1);
                OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(selectCommand2);
                DataTable dataTable1 = new DataTable();
                DataTable dataTable2 = new DataTable();
                oleDbDataAdapter1.Fill(dataTable1);
                oleDbDataAdapter2.Fill(dataTable2);
                if (dataTable1.Rows.Count <= 0)
                    return;
                clsFunctions.VITPLTableNameBD = dataTable1.Rows[0][78].ToString();
                clsFunctions.SourceTableNameBD = dataTable2.Rows[0][78].ToString();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("FetchTableNameBatchDetails exception:" + ex.Message);
            }
        }

        public static void FetchTableNameBatchDetailsOld()
        {
            try
            {
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand("SELECT distinct top 1 VITPLTableName ,SourceTableName FROM DataTransactionTableSync;", clsFunctions.conns));
                DataTable dataTable = new DataTable();
                oleDbDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count <= 0)
                    return;
                clsFunctions.VITPLTableNameBD = dataTable.Rows[0]["VITPLTableName"].ToString();
                clsFunctions.SourceTableNameBD = dataTable.Rows[0]["SourceTableName"].ToString();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("FetchTableNameBatchDetails exception:" + ex.Message);
            }
        }

        //public static DataTable FetchTableRecords(string batch_no)
        //{
        //    string HeadorTrans = "DataHeaderTableSync";
        //    string cmdText1 = "SELECT  * FROM DataHeaderTableSync where Type='VIPL' AND Info='Fields' AND Flag='Y';";
        //    string cmdText2 = "SELECT  * FROM DataHeaderTableSync where Type='Client' AND Info='Fields' AND Flag='Y';";
        //    try
        //    {
        //        if (clsFunctions.conns.State == ConnectionState.Closed)
        //            clsFunctions.conns.Open();
        //        OleDbCommand selectCommand1 = new OleDbCommand(cmdText1, clsFunctions.conns);
        //        OleDbCommand selectCommand2 = new OleDbCommand(cmdText2, clsFunctions.conns);
        //        OleDbDataAdapter oleDbDataAdapter1 = new OleDbDataAdapter(selectCommand1);
        //        OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(selectCommand2);
        //        oleDbDataAdapter1.Fill(clsFunctions.viplList);
        //        oleDbDataAdapter2.Fill(clsFunctions.clientList);
        //        string dbType = mdiMain.DBType;
        //        if (!(dbType == "MS ACCESS"))
        //        {
        //            if (!(dbType == "MYSQL") && dbType == "MS-SQL SERVER")
        //                clsFunctions.sourceTable = clsSqlFunctions.fillDatatable_client(clsFunctions.multiQuery(batch_no, HeadorTrans));

        //            if ((dbType == "ACC DB"))
        //                clsFunctions.sourceTable = clsFunctions.fillDatatable_client(clsFunctions.multiQuery(batch_no, HeadorTrans));

        //        }
        //        else if (dbType == "MS ACCESS")
        //        {
        //            clsFunctions.sourceTable = clsAquaMP30.fillDatatable(clsFunctions.multiQuery(batch_no, HeadorTrans));
        //        }

        //        else
        //            clsFunctions.sourceTable = clsFunctions.fillDatatable_client(clsFunctions.multiQuery(batch_no, HeadorTrans));
        //        return clsFunctions.sourceTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        return (DataTable)null;
        //    }
        //}

        public static string multiQuery(string batch_no, string HeadorTrans)
        {
            string str = "";
            string dbType = mdiMain.DBType;
            if (!(dbType == "MS ACCESS") && !(dbType == "ACC DB"))
            {
                if (!(dbType == "MYSQL") && dbType == "MS-SQL SERVER")
                {
                    if (HeadorTrans == "DataHeaderTableSync")
                        str = "SELECT " + clsFunctions.clientList.Rows[0]["Batch_No"]?.ToString() + ",Convert(Date," + clsFunctions.clientList.Rows[0]["Batch_Date"]?.ToString() + ") as bdate,Convert(Time," + clsFunctions.clientList.Rows[0]["Batch_Time"]?.ToString() + ") as btime,Convert(Time," + clsFunctions.clientList.Rows[0]["Batch_Time_Text"]?.ToString() + ") as btimetext,(" + clsFunctions.clientList.Rows[0]["Batch_Start_Time"]?.ToString() + ") as starttime," + clsFunctions.clientList.Rows[0]["Batch_End_Time"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batch_Year"]?.ToString() + " as bYear," + clsFunctions.clientList.Rows[0]["Batcher_Name"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batcher_User_Level"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Customer_Code"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Recipe_Code"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Recipe_Name"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Mixing_Time"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Mixer_Capacity"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["strength"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Site"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Truck_No"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Truck_Driver"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Production_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Ordered_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Returned_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["WithThisLoad"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batch_Size"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Order_No"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Schedule_Id"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate3_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate4_Target"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Gate5_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate6_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement3_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement4_Target"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Filler_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Water1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["slurry_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Water2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Silica_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm1_Target1"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Adm1_Target2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm2_Target1"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm2_Target2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cost_Per_Mtr_Cube"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Total_Cost"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Plant_No"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Weighed_Net_Weight"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Weigh_Bridge_Stat"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tExportStatus"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tUpload1"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tUpload2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["WO_Code"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Site_ID"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cust_Name"]?.ToString() + " FROM " + clsFunctions.SourceTableName + " where " + clsFunctions.clientList.Rows[0]["Batch_No"]?.ToString() + " = " + batch_no + ";";
                    else
                        str = "SELECT " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Index"].ToString() + ",Convert(Date," + clsFunctions.clientTransList.Rows[0]["Batch_Date"].ToString() + ") as bdate ,Convert(Time," + clsFunctions.clientTransList.Rows[0]["Batch_Time"].ToString() + ") as btime,Convert(Time," + clsFunctions.clientTransList.Rows[0]["Batch_Time_Text"].ToString() + ") as BTimeText," + clsFunctions.clientTransList.Rows[0]["Batch_Year"].ToString() + " as BatchYear," + clsFunctions.clientTransList.Rows[0]["Consistancy"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Production_Qty"].ToString() + " as ProdQty," + clsFunctions.clientTransList.Rows[0]["Ordered_Qty"].ToString() + " as ordered_Qty1," + clsFunctions.clientTransList.Rows[0]["Returned_Qty"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["WithThisLoad"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Size"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Pigment_Actual"].ToString() + " ," + clsFunctions.clientTransList.Rows[0]["Pigment_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Plant_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Balance_Wtr"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload2"].ToString() + " FROM " + clsFunctions.SourceTableNameBD + " where " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + " = " + batch_no + ";";
                }
            }
            else if (HeadorTrans == "DataHeaderTableSync")
            {
                str = "SELECT " + clsFunctions.clientList.Rows[0]["Batch_No"]?.ToString() + ",datevalue(" + clsFunctions.clientList.Rows[0]["Batch_Date"]?.ToString() + ") as bdate,timevalue(" + clsFunctions.clientList.Rows[0]["Batch_Time"]?.ToString() + ") as btime," + clsFunctions.clientList.Rows[0]["Batch_Time_Text"]?.ToString() + " as btimetext,(" + clsFunctions.clientList.Rows[0]["Batch_Start_Time"]?.ToString() + ") as starttime," + clsFunctions.clientList.Rows[0]["Batch_End_Time"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batch_Year"]?.ToString() + " as bYear," + clsFunctions.clientList.Rows[0]["Batcher_Name"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batcher_User_Level"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Customer_Code"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Recipe_Code"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Recipe_Name"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Mixing_Time"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Mixer_Capacity"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["strength"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Site"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Truck_No"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Truck_Driver"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Production_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Ordered_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Returned_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["WithThisLoad"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batch_Size"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Order_No"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Schedule_Id"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate3_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate4_Target"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Gate5_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate6_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement3_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement4_Target"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Filler_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Water1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["slurry_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Water2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Silica_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm1_Target1"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Adm1_Target2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm2_Target1"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm2_Target2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cost_Per_Mtr_Cube"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Total_Cost"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Plant_No"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Weighed_Net_Weight"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Weigh_Bridge_Stat"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tExportStatus"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tUpload1"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tUpload2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["WO_Code"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Site_ID"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cust_Name"]?.ToString() + " FROM " + clsFunctions.SourceTableName + " where " + clsFunctions.clientList.Rows[0]["Batch_No"]?.ToString() + " = " + batch_no + ";";
                return str;
            }
            if (HeadorTrans == "DataHeaderTableSync" && (dbType == "MYSQL"))
                str = "SELECT " + clsFunctions.clientList.Rows[0]["Batch_No"]?.ToString() + ",DATE(" + clsFunctions.clientList.Rows[0]["Batch_Date"]?.ToString() + ") as bdate,TIME(" + clsFunctions.clientList.Rows[0]["Batch_Time"]?.ToString() + ") as btime," + clsFunctions.clientList.Rows[0]["Batch_Time_Text"]?.ToString() + " as btimetext,(" + clsFunctions.clientList.Rows[0]["Batch_Start_Time"]?.ToString() + ") as starttime," + clsFunctions.clientList.Rows[0]["Batch_End_Time"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batch_Year"]?.ToString() + " as bYear," + clsFunctions.clientList.Rows[0]["Batcher_Name"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batcher_User_Level"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Customer_Code"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Recipe_Code"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Recipe_Name"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Mixing_Time"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Mixer_Capacity"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["strength"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Site"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Truck_No"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Truck_Driver"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Production_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Ordered_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Returned_Qty"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["WithThisLoad"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Batch_Size"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Order_No"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Schedule_Id"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate3_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate4_Target"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Gate5_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Gate6_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement3_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cement4_Target"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Filler_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Water1_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["slurry_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Water2_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Silica_Target"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm1_Target1"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Adm1_Target2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm2_Target1"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Adm2_Target2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cost_Per_Mtr_Cube"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Total_Cost"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Plant_No"]?.ToString() + "," + clsFunctions.clientList.Rows[0]["Weighed_Net_Weight"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Weigh_Bridge_Stat"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tExportStatus"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tUpload1"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["tUpload2"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["WO_Code"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Site_ID"]?.ToString() + ", " + clsFunctions.clientList.Rows[0]["Cust_Name"]?.ToString() + " FROM " + clsFunctions.SourceTableName + " where " + clsFunctions.clientList.Rows[0]["Batch_No"]?.ToString() + " = " + batch_no + ";";


            if (HeadorTrans != "DataHeaderTableSync" && (dbType == "MYSQL"))
                str = "SELECT " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Index"].ToString() + ",DATE(" + clsFunctions.clientTransList.Rows[0]["Batch_Date"].ToString() + ") as bdate ,TIME(" + (clsFunctions.clientTransList.Rows[0]["Batch_Time"].ToString()) + ") as btime," + clsFunctions.clientTransList.Rows[0]["Batch_Time_Text"].ToString() + " as BTimeText," + clsFunctions.clientTransList.Rows[0]["Batch_Year"].ToString() + " as BatchYear," + clsFunctions.clientTransList.Rows[0]["Consistancy"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Production_Qty"].ToString() + " as ProdQty," + clsFunctions.clientTransList.Rows[0]["Ordered_Qty"].ToString() + " as ordered_Qty1," + clsFunctions.clientTransList.Rows[0]["Returned_Qty"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["WithThisLoad"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Size"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Pigment_Actual"].ToString() + " ," + clsFunctions.clientTransList.Rows[0]["Pigment_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Plant_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Balance_Wtr"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload2"].ToString() + " FROM " + clsFunctions.SourceTableNameBD + " where " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + " = " + batch_no + ";";


            else

                //str = "SELECT " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Index"].ToString() + ",datevalue(" + clsFunctions.clientTransList.Rows[0]["Batch_Date"].ToString() + ") as bdate ,timevalue(" + clsFunctions.clientTransList.Rows[0]["Batch_Time"].ToString() + ") as btime," + clsFunctions.clientTransList.Rows[0]["Batch_Time_Text"].ToString() + " as BTimeText," + clsFunctions.clientTransList.Rows[0]["Batch_Year"].ToString() + " as BatchYear," + clsFunctions.clientTransList.Rows[0]["Consistancy"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Production_Qty"].ToString() + " as ProdQty," + clsFunctions.clientTransList.Rows[0]["Ordered_Qty"].ToString() + " as ordered_Qty1," + clsFunctions.clientTransList.Rows[0]["Returned_Qty"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["WithThisLoad"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Size"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Pigment_Actual"].ToString() + " ," + clsFunctions.clientTransList.Rows[0]["Pigment_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Plant_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Balance_Wtr"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload2"].ToString() + " FROM " + clsFunctions.SourceTableNameBD + " where " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + " = " + batch_no + " ORDER BY " + clsFunctions.clientTransList.Rows[0]["Batch_Index"].ToString() + " ASC ;";
                str = "SELECT " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Index"].ToString() + ",datevalue(" + clsFunctions.clientTransList.Rows[0]["Batch_Date"].ToString() + ") as bdate ,timevalue(" + (clsFunctions.clientTransList.Rows[0]["Batch_Time"].ToString()) + ") as btime," + clsFunctions.clientTransList.Rows[0]["Batch_Time_Text"].ToString() + " as BTimeText," + clsFunctions.clientTransList.Rows[0]["Batch_Year"].ToString() + " as BatchYear," + clsFunctions.clientTransList.Rows[0]["Consistancy"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Production_Qty"].ToString() + " as ProdQty," + clsFunctions.clientTransList.Rows[0]["Ordered_Qty"].ToString() + " as ordered_Qty1," + clsFunctions.clientTransList.Rows[0]["Returned_Qty"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["WithThisLoad"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Size"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Pigment_Actual"].ToString() + " ," + clsFunctions.clientTransList.Rows[0]["Pigment_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Plant_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Balance_Wtr"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload2"].ToString() + " FROM " + clsFunctions.SourceTableNameBD + " where " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + " = " + batch_no + ";";



            //str = "SELECT " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Index"].ToString() + ",datevalue(" + clsFunctions.clientTransList.Rows[0]["Batch_Date"].ToString() + ") as bdate ,timevalue(" + clsFunctions.clientTransList.Rows[0]["Batch_Time"].ToString() + ") as btime," + clsFunctions.clientTransList.Rows[0]["Batch_Time_Text"].ToString() + " as BTimeText," + clsFunctions.clientTransList.Rows[0]["Batch_Year"].ToString() + " as BatchYear," + clsFunctions.clientTransList.Rows[0]["Consistancy"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Production_Qty"].ToString() + " as ProdQty," + clsFunctions.clientTransList.Rows[0]["Ordered_Qty"].ToString() + " as ordered_Qty1," + clsFunctions.clientTransList.Rows[0]["Returned_Qty"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["WithThisLoad"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Batch_Size"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate1_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate2_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate3_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate4_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate5_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Gate6_Moisture"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement3_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Cement4_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Filler1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water1_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Water2_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Silica_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Actual"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Slurry_Correction"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm1_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Actual2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Target2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Adm2_Correction2"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Pigment_Actual"].ToString() + " ," + clsFunctions.clientTransList.Rows[0]["Pigment_Target"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["Plant_No"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload1"].ToString() + "," + clsFunctions.clientTransList.Rows[0]["tUpload2"].ToString() + " FROM " + clsFunctions.SourceTableNameBD + " where " + clsFunctions.clientTransList.Rows[0]["Batch_No"].ToString() + " = " + batch_no + " ORDER BY " + clsFunctions.clientTransList.Rows[0]["Batch_Index"].ToString() + " ASC ;";
            return str;
        }

        //public static DataTable FetchTransactionTableRecords(string batch_no, DataGridView dgv)
        //{
        //    string HeadorTrans = "DataTransactionTableSync";
        //    string cmdText1 = "SELECT  * FROM DataTransactionTableSync where Type='VIPL' AND Info='Fields' AND Flag='Y';";
        //    string cmdText2 = "SELECT  * FROM DataTransactionTableSync where Type='Client' AND Info='Fields' AND Flag='Y';";
        //    try
        //    {
        //        if (clsFunctions.conns.State == ConnectionState.Closed)
        //            clsFunctions.conns.Open();
        //        OleDbCommand selectCommand1 = new OleDbCommand(cmdText1, clsFunctions.conns);
        //        OleDbCommand selectCommand2 = new OleDbCommand(cmdText2, clsFunctions.conns);
        //        OleDbDataAdapter oleDbDataAdapter1 = new OleDbDataAdapter(selectCommand1);
        //        OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(selectCommand2);
        //        oleDbDataAdapter1.Fill(clsFunctions.viplTransList);
        //        oleDbDataAdapter2.Fill(clsFunctions.clientTransList);
        //        string dbType = mdiMain.DBType;
        //        if (!(dbType == "MS ACCESS") && !(dbType == "ACC DB"))
        //        {
        //            if (!(dbType == "MYSQL") && dbType == "MS-SQL SERVER")
        //                clsFunctions.sourceTrnsTable = clsSqlFunctions.fillDatatable_client(clsFunctions.multiQuery(batch_no, HeadorTrans));
        //        }
        //        //else if (dbType == "MS ACCESS")
        //        //{
        //        //    clsFunctions.sourceTrnsTable = clsAquaMP30.fillDatatable(clsFunctions.multiQuery(batch_no, HeadorTrans));
        //        //}
        //        else
        //            clsFunctions.sourceTrnsTable = clsFunctions.fillDatatable_client(clsFunctions.multiQuery(batch_no, HeadorTrans));
        //        return clsFunctions.sourceTrnsTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        /*
         * ---------------------------- This method inserts Data from MCI70_Batch.mdb to Unipro.mdb for Stetter module ----------------------------
         
            17/05/2024 : BhaveshT - modifications in uploadData method to store moisture & correction values in DB.
                                  - seperated date & time variable to store batchYear correctly.
                                  - added ErrorLog in catch block.
         */

        public static int uploadData(
            string batch_no,
            string custName,
            string SiteID,
            string Workcode,
            string[] tergetValues,
            clsErrorPercent_Calculation clsErr,
            string BatchDate)
        {
            try
            {
                clsVariables clsVariables = new clsVariables();
                clsSMSAlerts clsSMS = new clsSMSAlerts();

                if (clsErr.Production_Error == "N")
                {
                    //MessageBox.Show("Production Error is acceptable");
                    clsErr.Production_SMS = "N";
                }

                else if (clsErr.Production_Error == "Y")
                {
                    //MessageBox.Show("Production Error have issue");
                    clsSMS.SendErrorSMSAlert(Workcode, batch_no, BatchDate, clsErr);
                    //return;
                }

                string str = "";
                int num1 = 0;

                string index1;
                int index2 = 0;

                int num2 = 0;
                foreach (DataRow row1 in (InternalDataCollectionBase)clsFunctions.sourceTable.Rows)
                {
                    ++num1;

                    try
                    {
                        index1 = clsFunctions.loadSingleValueSetup(" SELECT Batch_End_Time FROM DataHeaderTableSync where Type='Client' AND Info='Fields' AND Flag='Y'");
                        index2 = clsFunctions.sourceTable.Rows.Count - 1;
                        clsFunctions.endtime = clsFunctions.sourceTable.Rows[index2][index1].ToString();
                    }
                    catch
                    {
                        try
                        {
                            clsFunctions.endtime = clsFunctions.loadSinglevalue_client("Select batch_time_end from batch_data_end where batch_no_end = " + batch_no + " ");
                        }
                        catch { }
                    }

                    clsFunctions.batchendflag = num1 != index2 ? "0" : "1";

                    row1[clsFunctions.clientList.Rows[0][clsFunctions.ClientBatchNo].ToString() ?? ""].ToString();

                    if (!(str == row1[clsFunctions.clientList.Rows[0][clsFunctions.ClientBatchNo].ToString() ?? ""].ToString()))
                    {
                        // Added Production_Error & Production_SMS flag

                        string truckNo = (row1[16].ToString()).Replace(" ", "");
                        truckNo = clsFunctions_comman.ReplaceSpecialCharactersForVehicle(truckNo);

                        //----------------------- Insert into Batch_Dat_Trans ----------------------------------------------

                        if (clsFunctions.AdoData("Insert into " + clsFunctions.VITPLTableName + "(" + clsFunctions.viplList.Rows[0]["Batch_No"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Date"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Time"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Time_Text"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Start_Time"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_End_Time"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Year"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batcher_Name"]?.ToString() + "," +
                            " " + clsFunctions.viplList.Rows[0]["Batcher_User_Level"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Customer_Code"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Recipe_Code"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Recipe_Name"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Mixing_Time"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Mixer_Capacity"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["strength"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Site"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Truck_No"]?.ToString() + ", " +
                            "" + clsFunctions.viplList.Rows[0]["Truck_Driver"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Production_Qty"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Ordered_Qty"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Returned_Qty"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["WithThisLoad"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Size"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Order_No"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Schedule_Id"]?.ToString() + ", " +
                            "" + clsFunctions.viplList.Rows[0]["Gate1_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate2_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate3_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate4_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate5_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate6_Target"]?.ToString() + ", " +
                            "" + clsFunctions.viplList.Rows[0]["Cement1_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cement2_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cement3_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cement4_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Filler_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Water1_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["slurry_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Water2_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Silica_Target"]?.ToString() + ", " +
                            "" + clsFunctions.viplList.Rows[0]["Adm1_Target1"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Adm1_Target2"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Adm2_Target1"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Adm2_Target2"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cost_Per_Mtr_Cube"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Total_Cost"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Plant_No"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Weighed_Net_Weight"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Weigh_Bridge_Stat"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["tExportStatus"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["tUpload1"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["tUpload2"]?.ToString() + ", Wo_Code,Site_ID,Cust_Name, InsertType, Production_Error, Production_SMS)" +

                            " values('" + row1[0].ToString() + "', '" + Convert.ToDateTime(row1[1]).ToString("dd/MM/yyyy") + "', '" + Convert.ToDateTime(row1[2]).ToString("HH:mm:ss tt") + "','" + Convert.ToDateTime(row1[2]).ToString("HH:mm:ss tt") + "', '" + row1[4].ToString() + "', '" + Convert.ToDateTime(clsFunctions.endtime).ToString("hh:mm:ss tt") + "', '" + row1[6].ToString() + "', '" + row1[7].ToString() + "',  '" + row1[8].ToString() + "',  '" + clsFunctions.CustCode + "', '" + row1[10].ToString() + "', '" + row1[11].ToString() + "', '" + row1[12].ToString() + "','" + row1[13].ToString() + "', '" + row1[14].ToString() + "', '" + clsFunctions.sitename + "', '" + truckNo + "' ,'" + row1[17].ToString() + "', '" + Convert.ToString(Math.Round(Convert.ToDouble(row1[18]), 2)) + "', '" + row1[19].ToString() + "','" + row1[20].ToString() + "','" + Convert.ToString(Math.Round(Convert.ToDouble(row1[21]), 2)) + "', '" + Convert.ToString(Math.Round(Convert.ToDouble(row1[22]), 2)) + "','" + row1[23].ToString() + "', '-', " +
                            "'" + tergetValues[0] + "', '" + tergetValues[1] + "', '" + tergetValues[2] + "','" + tergetValues[3] + "','" + row1[29]?.ToString() + "', '" + row1[30]?.ToString() + "', '" + tergetValues[4] + "', '" + tergetValues[5] + "','" + tergetValues[6] + "','" + tergetValues[14] + "','" + tergetValues[7] + "','" + tergetValues[8] + "','" + row1[37]?.ToString() + "','" + tergetValues[9] + "','" + row1[39]?.ToString() + "','" + tergetValues[10] + "','" + tergetValues[11] + "','" + tergetValues[12] + "'," +
                            "'" + tergetValues[13] + "','" + row1[44].ToString() + "','" + row1[45].ToString() + "','" + clsFunctions.activePlantCode + "', '" + row1[47].ToString() + "','" + row1[48].ToString() + "', 'N', 0, 0,'" + Workcode + "','" + SiteID + "','" + custName + "', 'A', '" + clsErr.Production_Error + "', '" + clsErr.Production_SMS + "')") != 1)
                            return 0;

                        //----------------------- Converted in for loop after adding 3 rows of ACT, NOM & ERROR % -----------------------

                        for (int i = 0; i < clsFunctions.sourceTrnsTable.Rows.Count - 3; i++)
                        {
                            DataRow row2 = clsFunctions.sourceTrnsTable.Rows[i];
                            ++num2;

                            //// // 20/02/2024 - BhaveshT : BatchEndFlag
                            clsFunctions.batchendflag = clsFunctions.sourceTrnsTable.Rows.Count - 3 != num2 ? "0" : "1";

                            string[] strArray = new string[77];
                            strArray[0] = "Insert into ";
                            strArray[1] = clsFunctions.VITPLTableNameBD;
                            strArray[2] = " values('";
                            strArray[3] = row2[0].ToString();
                            strArray[4] = "','";
                            strArray[5] = num2.ToString();
                            strArray[6] = "','";

                            DateTime date = Convert.ToDateTime(row2[2]);

                            strArray[7] = date.ToString("dd/MM/yyyy");
                            strArray[8] = "','";

                            DateTime Time = Convert.ToDateTime(row2[2]);
                            strArray[9] = Time.ToString("HH:mm:ss tt");
                            strArray[10] = "','";
                            strArray[11] = Time.ToString("HH:mm:ss tt");
                            strArray[12] = "','";
                            //dateTime = DateTime.Now;
                            strArray[13] = date.Year.ToString();
                            strArray[14] = "','0','";
                            strArray[15] = Convert.ToString(Math.Round(Convert.ToDouble(row2[7]), 2));              // Prod Qty     
                            strArray[16] = "','";
                            strArray[17] = row2[8].ToString();                                                      // Ordered_Qty
                            strArray[18] = "','0','0','";
                            strArray[19] = Convert.ToString(Math.Round(Convert.ToDouble(row2[11]), 2));             //BatchSize
                            strArray[20] = "','";
                            strArray[21] = row2[12].ToString();                                                     // Gate1_Actual
                            strArray[22] = "','";
                            strArray[23] = row2[13].ToString();                                                     // Gate1_Target
                            strArray[24] = "','0','";                                                               // Gate1_Moisture

                            strArray[25] = row2[15].ToString();                                                     // Gate2_Actual
                            strArray[26] = "','";
                            strArray[27] = row2[16].ToString();                                                     // Gate2_Target
                            strArray[28] = "','" + row2[17].ToString() + "','";     //"','0','";                    // Gate2_Moisture

                            strArray[29] = row2[18].ToString();                                                     // Gate3_Actual
                            strArray[30] = "','";
                            strArray[31] = row2[19].ToString();                                                     // Gate3_Target
                            strArray[32] = "','" + row2[20].ToString() + "','";     //"','0','";                    // Gate3_Moisture

                            strArray[33] = row2[21].ToString();                                                     // Gate4_Actual
                            strArray[34] = "','";
                            strArray[35] = row2[22].ToString();                                                     // Gate4_Target
                            strArray[36] = "','" + row2[23].ToString() + "','";     //"','0','";                    // Gate4_Moisture

                            strArray[37] = row2[24].ToString();                                                     // Gate5_Actual
                            strArray[38] = "','";
                            strArray[39] = row2[25].ToString();                                                     // Gate5_Target
                            strArray[40] = "','" + row2[26].ToString() + "','0','0','0','";                           // Gate5_Moisture

                            strArray[41] = row2[30].ToString();                                                     // Cement1_Actual
                            strArray[42] = "','";
                            strArray[43] = row2[31].ToString();                                                     // Cement1_Target
                            strArray[44] = "','" + row2[32].ToString() + "','";     //"','0','";                      // Cement1_Correction

                            strArray[45] = row2[33].ToString();                                                     // Cement2_Actual
                            strArray[46] = "','";
                            strArray[47] = row2[34].ToString();                                                     // Cement2_Target
                            strArray[48] = "','" + row2[35].ToString() + "','";     //"','0','";                     // Cement2_Correction

                            strArray[49] = row2[36].ToString();                                                     // Cement3_Actual
                            strArray[50] = "','";
                            strArray[51] = row2[37].ToString();                                                     // Cement3_Target

                            strArray[52] = "','" + row2[38].ToString() + "','" + row2[39].ToString() + "','" + row2[40].ToString() + "','" + row2[41].ToString() + "','";                          // Cement3_Correction

                            //strArray[52] = "','" + row2[38].ToString() + "','0','0','0','";                          // BKP

                            strArray[53] = row2[42].ToString();                                                     // Filler1_Actual
                            strArray[54] = "','";
                            strArray[55] = row2[43].ToString();                                                     // Filler1_Target
                            strArray[56] = "','" + row2[44].ToString() + "','";      //"','0','";                   // Filler1_Correction

                            int a = Convert.ToInt32(row2[47]);                                                      // Water1_Correction
                            strArray[57] = Convert.ToString(Convert.ToInt32(row2[45]) - (a));                       // Water1_Actual
                            strArray[58] = "','";
                            strArray[59] = row2[46].ToString();                                                     // Water1_Target
                            strArray[60] = "','";
                            strArray[61] = row2[47].ToString();                                                     // Water1_Correction

                            int b = Convert.ToInt32(row2[50]);                                                      // Water2_Correction
                            strArray[62] = "'," + "0" + Convert.ToString(Convert.ToInt32(row2[48]) - (b)) + "," + row2[49].ToString() + "," + row2[50].ToString() + ",'0','0','0','0','0',0,'";     // Water2_Actual,	Water2_Target,	Water2_Correction

                            strArray[63] = row2[57].ToString();                                                     // Adm1_Actual1
                            strArray[64] = "','";
                            strArray[65] = Convert.ToString(Math.Round(Convert.ToDouble(row2[58]), 3));             // Adm1_Target1
                            strArray[66] = "','" + row2[59].ToString() + "','";     // "','0','";                   // Adm1_Correction1

                            strArray[67] = Convert.ToString(Math.Round(Convert.ToDouble(row2[60]), 3));             // Adm1_Actual2
                            strArray[68] = "','";
                            strArray[69] = row2[61].ToString();                                                     // Adm1_Target2
                            strArray[70] = "','" + row2[62].ToString() + "','" + row2[63].ToString() + "','" + row2[64].ToString() + "','" + row2[65].ToString() + "','" + row2[66].ToString() + "','" + row2[67].ToString() + "','" + row2[68].ToString() + "','0','0','";      // Adm1_Correction2


                            strArray[71] = clsFunctions.activePlantCode;
                            strArray[72] = "','";
                            strArray[73] = clsFunctions.batchendflag;
                            strArray[74] = "',0,0";
                            strArray[75] = "";                                      // UploadData.temJobsite;
                            strArray[76] = ")";

                            clsFunctions.AdoData(string.Concat(strArray));

                            if (clsFunctions.batchendflag == "1" && num2 == clsFunctions.sourceTrnsTable.Rows.Count - 3)
                            {
                                return 1;
                            }

                        }
                    }
                }



                //---------------------------- Commented by BT 20/01/2024 ----------------------------
                //foreach (DataRow row1 in (InternalDataCollectionBase)clsFunctions.sourceTable.Rows)         
                //{
                //    ++num1;
                //    string index1 = clsFunctions.loadSingleValueSetup(" SELECT Batch_End_Time FROM DataHeaderTableSync where Type='Client' AND Info='Fields' AND Flag='Y'");
                //    int index2 = clsFunctions.sourceTable.Rows.Count - 1;
                //    clsFunctions.endtime = clsFunctions.sourceTable.Rows[index2][index1].ToString();
                //    clsFunctions.batchendflag = num1 != index2 ? "0" : "1";
                //    row1[clsFunctions.clientList.Rows[0][clsFunctions.ClientBatchNo].ToString() ?? ""].ToString();
                //    if (!(str == row1[clsFunctions.clientList.Rows[0][clsFunctions.ClientBatchNo].ToString() ?? ""].ToString()))
                //    {
                //        if (clsFunctions.AdoData("Insert into " + clsFunctions.VITPLTableName + "(" + clsFunctions.viplList.Rows[0]["Batch_No"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Date"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Time"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Time_Text"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Start_Time"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_End_Time"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Year"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batcher_Name"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batcher_User_Level"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Customer_Code"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Recipe_Code"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Recipe_Name"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Mixing_Time"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Mixer_Capacity"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["strength"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Site"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Truck_No"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Truck_Driver"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Production_Qty"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Ordered_Qty"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Returned_Qty"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["WithThisLoad"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Batch_Size"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Order_No"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Schedule_Id"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate1_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate2_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate3_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate4_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate5_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Gate6_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cement1_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cement2_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cement3_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cement4_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Filler_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Water1_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["slurry_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Water2_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Silica_Target"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Adm1_Target1"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Adm1_Target2"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Adm2_Target1"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Adm2_Target2"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Cost_Per_Mtr_Cube"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Total_Cost"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Plant_No"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Weighed_Net_Weight"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["Weigh_Bridge_Stat"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["tExportStatus"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["tUpload1"]?.ToString() + ", " + clsFunctions.viplList.Rows[0]["tUpload2"]?.ToString() + ", Wo_Code,Site_ID,Cust_Name, InsertType) values('" + row1[0].ToString() + "', '" + Convert.ToDateTime(row1[1]).ToString("dd/MM/yyyy") + "', '" + Convert.ToDateTime(row1[2]).ToString("HH:mm:ss tt") + "','" + Convert.ToDateTime(row1[2]).ToString("HH:mm:ss tt") + "', '" + row1[4].ToString() + "', '" + clsFunctions.endtime + "', '" + row1[6].ToString() + "', '" + row1[7].ToString() + "',  '" + row1[8].ToString() + "',  '" + clsFunctions.CustCode + "', '" + row1[10].ToString() + "', '" + row1[11].ToString() + "', '" + row1[12].ToString() + "','" + row1[13].ToString() + "', '" + row1[14].ToString() + "', '" + clsFunctions.sitename + "', '" + row1[16].ToString() + "','" + row1[17].ToString() + "', '" + row1[18].ToString() + "', '" + row1[19].ToString() + "','" + row1[20].ToString() + "','" + row1[21].ToString() + "', '" + row1[22].ToString() + "','" + row1[23].ToString() + "', '-', '" + tergetValues[0] + "', '" + tergetValues[1] + "', '" + tergetValues[2] + "','" + tergetValues[3] + "','" + row1[29]?.ToString() + "', '" + row1[30]?.ToString() + "', '" + tergetValues[4] + "', '" + tergetValues[5] + "','" + tergetValues[6] + "','" + row1[34]?.ToString() + "','" + tergetValues[7] + "','" + tergetValues[8] + "','" + row1[37]?.ToString() + "','" + tergetValues[9] + "','" + row1[39]?.ToString() + "','" + tergetValues[10] + "','" + tergetValues[11] + "','" + row1[42].ToString() + "','" + row1[43].ToString() + "','" + row1[44].ToString() + "','" + row1[45].ToString() + "','" + clsFunctions.plantcode + "', '" + row1[47].ToString() + "','" + row1[48].ToString() + "', 'N', 0, 0,'" + Workcode + "','" + SiteID + "','" + custName + "', 'A')") != 1)
                //            return 0;
                //        int num2 = 0;
                //        foreach (DataRow row2 in (InternalDataCollectionBase)clsFunctions.sourceTrnsTable.Rows)
                //        {
                //            ++num2;
                //            clsFunctions.batchendflag = clsFunctions.sourceTrnsTable.Rows.Count != num2 ? "0" : "1";
                //            string[] strArray = new string[77];
                //            strArray[0] = "Insert into ";
                //            strArray[1] = clsFunctions.VITPLTableNameBD;
                //            strArray[2] = " values('";
                //            strArray[3] = row2[0].ToString();
                //            strArray[4] = "','";
                //            strArray[5] = num2.ToString();
                //            strArray[6] = "','";
                //            DateTime dateTime = Convert.ToDateTime(row2[2]);
                //            strArray[7] = dateTime.ToString("dd/MM/yyyy");      //dateTime.ToString("yyyy/MM/dd");
                //            strArray[8] = "','";
                //            dateTime = Convert.ToDateTime(row2[3]);
                //            strArray[9] = dateTime.ToString("HH:mm:ss tt");
                //            strArray[10] = "','";
                //            strArray[11] = dateTime.ToString("HH:mm:ss tt");        //row2[4].ToString();
                //            strArray[12] = "','";
                //            dateTime = DateTime.Now;
                //            strArray[13] = dateTime.Year.ToString();
                //            strArray[14] = "','0','";
                //            strArray[15] = row2[7].ToString();
                //            strArray[16] = "','";
                //            strArray[17] = row2[8].ToString();
                //            strArray[18] = "','0','0','";
                //            strArray[19] = row2[11].ToString();
                //            strArray[20] = "','";
                //            strArray[21] = row2[12].ToString();
                //            strArray[22] = "','";
                //            strArray[23] = row2[13].ToString();
                //            strArray[24] = "','0','";
                //            strArray[25] = row2[15].ToString();
                //            strArray[26] = "','";
                //            strArray[27] = row2[16].ToString();
                //            strArray[28] = "','0','";
                //            strArray[29] = row2[18].ToString();
                //            strArray[30] = "','";
                //            strArray[31] = row2[19].ToString();
                //            strArray[32] = "','0','";
                //            strArray[33] = row2[21].ToString();
                //            strArray[34] = "','";
                //            strArray[35] = row2[22].ToString();
                //            strArray[36] = "','0','";
                //            strArray[37] = row2[24].ToString();
                //            strArray[38] = "','";
                //            strArray[39] = row2[25].ToString();
                //            strArray[40] = "','0','0','0','0','";
                //            strArray[41] = row2[30].ToString();
                //            strArray[42] = "','";
                //            strArray[43] = row2[31].ToString();
                //            strArray[44] = "','0','";
                //            strArray[45] = row2[33].ToString();
                //            strArray[46] = "','";
                //            strArray[47] = row2[34].ToString();
                //            strArray[48] = "','0','";
                //            strArray[49] = row2[36].ToString();
                //            strArray[50] = "','";
                //            strArray[51] = row2[37].ToString();
                //            strArray[52] = "','0','0','0','0','";
                //            strArray[53] = row2[42].ToString();
                //            strArray[54] = "','";
                //            strArray[55] = row2[43].ToString();
                //            strArray[56] = "','0','";
                //            int a = Convert.ToInt32(row2[47]);                        //for water correction
                //            strArray[57] = Convert.ToString(Convert.ToInt32(row2[45]) - (a));
                //            strArray[58] = "','";
                //            //string a = row2[47].ToString();                        //for water correction
                //            strArray[59] = row2[46].ToString();
                //            strArray[60] = "','";

                //            strArray[61] = row2[47].ToString();
                //            int b = Convert.ToInt32(row2[50]);                         //water correction

                //            //strArray[62] = "'," + "0"+Convert.ToString(Convert.ToInt32(row2[48]) - (b)) + ","+row2[49].ToString()+"," + row2[50].ToString()+",'0','0','0','0','0','0','";
                //            strArray[62] = "'," + "0" + Convert.ToString(Convert.ToInt32(row2[48]) - (b)) + "," + row2[49].ToString() + "," + row2[50].ToString() + ",'0','0','0','0','0','";

                //            strArray[63] = row2[57].ToString();
                //            strArray[64] = "','";
                //            strArray[65] = row2[58].ToString();
                //            strArray[66] = "','0','";
                //            strArray[67] = row2[60].ToString();
                //            strArray[68] = "','";
                //            strArray[69] = row2[61].ToString();
                //            strArray[70] = "','0','0','0','0','0','0','0','0','0','0','";
                //            strArray[71] = clsFunctions.plantcode;
                //            strArray[72] = "','";
                //            strArray[73] = clsFunctions.batchendflag;
                //            strArray[74] = "',0,0";
                //            strArray[75] = "";         //UploadData.temJobsite;
                //            strArray[76] = ")";
                //            clsFunctions.AdoData(string.Concat(strArray));
                //        }
                //        str = row1[clsFunctions.clientList.Rows[0][clsFunctions.ClientBatchNo]?.ToString() ?? ""].ToString();
                //    }
                //}
                //---------------------------------------------------------------------------------------
                return 1;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception : clsFunctions.uploadData() - BatchNo: " + batch_no + " | " + ex.Message);
                return 0;
            }
        }

        public static void batchDetailsFetch()
        {
        }

        //prerequisites Created Date:29/08/2023 by dinesh
        //this function checks whether configuration of tables are done or not
        public static string Preprocessing()
        {
            DataTable dt = new DataTable();
            try
            {
                //------------
                //dt = fillDatatable_setup("SELECT * FROM Connection_setup where Connection_Setting=Yes ");
                //if (dt.Rows.Count == 0)
                //{
                //    dt.Rows.Clear();
                //    return "Connection_setup";
                //}
                //else
                //    dt.Rows.Clear();

                //------------
                //dt = fillDatatable_setup("SELECT * FROM DbInfo where status='Y'");
                //if (dt.Rows.Count == 0)
                //{
                //    dt.Rows.Clear();
                //    return "DbInfo";
                //}
                //else
                //    dt.Rows.Clear();

                //------------ For Unipro_Setup

                //dt = fillDatatable_setup("SELECT * FROM Unipro_Setup where status='Y'");
                //if (dt.Rows.Count == 0)
                //{
                //    dt.Rows.Clear();
                //    return "DbInfo";
                //}
                //else
                //    dt.Rows.Clear();
                //------------
                //dt = fillDatatable_setup("SELECT * FROM ServerMapping where Flag='Y'");
                //if (dt.Rows.Count == 0)
                //{
                //    dt.Rows.Clear();
                //    return "ServerMapping";
                //}
                //else
                //    dt.Rows.Clear();

                //------------
                dt = fillDatatable_setup("SELECT truckinterval From tipper_interval where truckinterval<='60'");
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Clear();
                    return "tipper_interval";
                }
                else
                    dt.Rows.Clear();

                //------------
                //dt = fillDatatable_setup("SELECT * FROM ImageIconPath where image_used='Y'");
                //if (dt.Rows.Count == 0)
                //{
                //    dt.Rows.Clear();
                //    return "ImageIconPath";
                //}
                //else
                //{
                //    dt.Rows.Clear();
                //    return "0";
                //}

            }
            catch (Exception ex)
            {

            }
            return "0";
        }

        /*Created Date:30/08/2023 by dinesh
         * this function checks whether flash screen is running or not
         */
        public static bool IsSplashScreenRunning()
        {
            Form splashScreen = Application.OpenForms["SplashScreen1"];
            return splashScreen != null && splashScreen.Visible;
        }

        /* Created date: 30/08/2023 by dinesh
         * this function accepts string as table name and returns tab name
         */
        public static int CheckTabIndex(string tab)
        {
            try
            {

                switch (tab)
                {
                    //case "DbInfo":
                    //    return 0;
                    //break;
                    case "tipper_interval":
                        return 4;
                    //break;
                    case "ServerMapping":
                        return 2;
                        // break;
                        //case "Connection_setup":
                        //    return 5;
                        // break; 
                        //case "ImageIconPath":
                        //    return 1;
                }


                if (tab == "ServerMapping")
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {

            }
            return 0;
        }
        public static DataTable getAddress()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = clsFunctions.fillDatatable("select maddress from Modbus_address");
                return dt;
            }

            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("getAddress(): " + ex.Message);
            }
            return dt;
        }

        /*Created Date: 21/12/2023 by dInEsH
         * This Function ckecks the expiry of a plant
         */
        static string expired = "";
        //public static bool PlantExpiryChecker()
        //{
        //    try
        //    {
        //        if (CheckInternetConnection())
        //        {
        //            if (Expirydate <= DateTime.Today && expired == "")
        //            {
        //                Expirydate = Convert.ToDateTime(CheckPlantExpiry());
        //                //Expirydate = Convert.ToDateTime("2023-12-08");
        //                expired = Expirydate.ToString("dd/MM/yyyy");
        //            }

        //            // Expirydate = Convert.ToDateTime(dt);
        //            DateTime todaydate = DateTime.Today;
        //            TimeSpan timediff = Expirydate - todaydate;
        //            int result = timediff.Days;


        //            //int checkMe = Convert.ToInt32(clsGlobalFunction.Chk_Software_Validation());
        //            // int checkMe = 0;
        //            if (result <= 0)
        //            {
        //                //Plant is expired.
        //                return true;
        //            }
        //            else
        //            {
        //                //Plant is not expired.
        //                return false;
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        ErrorLog("PlantExpiryChecker(): " + e.Message);
        //    }
        //    return false;
        //}
        public static bool CheckInternetConnection()
        {
            try
            {
                bool a = NetworkInterface.GetIsNetworkAvailable();
                // using (var client = new System.Net.WebClient())
                // using (var stream = client.OpenRead("http://www.google.com"))
                {
                    //return true;

                }
                return a;
            }
            catch
            {
                return false;

            }
        }



        /*Created Date:21/12/2023 by dInEsH
         * This function is optimized for stopping any service
         * accepts an argu which is serviceName
         */


        public static bool StopService(string serviceName)
        {
            ServiceController serviceController = null;

            try
            {
                var allSevices = ServiceController.GetServices();

                for (int i = 0; i < allSevices.Length; i++)
                {
                    if (allSevices[i].ServiceName == serviceName)
                    {
                        serviceController = new ServiceController(serviceName);
                    }
                }

                if (serviceController != null)
                {
                    switch (serviceController.Status)
                    {
                        case ServiceControllerStatus.Running:

                            try
                            {
                                if (serviceController != null)
                                {
                                    if (serviceController.Status == ServiceControllerStatus.Running)
                                    {
                                        serviceController.Stop();
                                        //serviceController.WaitForStatus(ServiceControllerStatus.StopPending);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {

                                return false;
                            }
                            break;

                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;

        }
        /*Created Date: 21/12/2023 by dinesh
         * this method is responsible for start the service
         */
        public static bool StartService(string serviceName)
        {
            ServiceController serviceController = null;

            try
            {
                var allSevices = ServiceController.GetServices();

                for (int i = 0; i < allSevices.Length; i++)
                {
                    if (allSevices[i].ServiceName == serviceName)
                    {
                        serviceController = new ServiceController(serviceName);
                    }
                }

                if (serviceController != null)
                {
                    switch (serviceController.Status)
                    {
                        case ServiceControllerStatus.Stopped:

                            try
                            {
                                if (serviceController != null)
                                {
                                    if (serviceController.Status == ServiceControllerStatus.Stopped)
                                    {
                                        serviceController.Start();
                                        serviceController.WaitForStatus(ServiceControllerStatus.StartPending);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {

                                return false;
                            }
                            break;
                        case ServiceControllerStatus.Running:
                            //MessageBox.Show("Service is already running");
                            break;

                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;

        }


        /* Created Date: 21/12/2023 by dinesh
         * this function sets the API urls with respect to its plant type
         */

        public static string planttype = "";
        public static void SetAPIs()
        {
            try
            {
                planttype = loadSingleValueSetup("select PlantType from PlantSetup");
                if (planttype.Contains("RMC"))
                {
                    //SetRMCAPI();
                }
                if (planttype.Contains("Bituman") || planttype.Contains("Bitumen") || planttype.Contains("BT"))
                {
                    //SetBitumenAPI();
                }
            }
            catch (Exception ex)
            {
                ErrorLog("SetAPIs():- " + ex);

            }
        }

        /* Created date:21/12/2023 by dinesh
         * this function returns the ip address or website as string of column ipaddress contains so
         */

        public static string GetIPAddressOrWebsiteName(string ipAddressOrWebsite)
        {
            // Define a regular expression pattern to match an IP address
            string ipAddressPattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

            // Check if the input matches the IP address pattern
            if (Regex.IsMatch(ipAddressOrWebsite, ipAddressPattern))
            {
                // The input is an IP address
                return "IP Address: " + ipAddressOrWebsite;
            }
            else
            {
                try
                {

                    //edited by dinesh 04/12/2023
                    Uri uri;
                    uri = new Uri(ipAddressOrWebsite);

                    try
                    {
                        // Attempt to create a Uri from the URL
                        uri = new Uri(ipAddressOrWebsite);
                    }
                    catch (UriFormatException ex)
                    {
                        Console.WriteLine($"Invalid URL: {ex.Message}");
                        return "";
                    }
                    string host = uri.Host;
                    IPHostEntry hostEntry = null;
                    try
                    {
                        // Perform DNS lookup for the host
                        hostEntry = Dns.GetHostEntry(host);

                        //foreach (IPAddress address in hostEntry.AddressList)
                        //{
                        //    Console.WriteLine($"IP Address: {address}");
                        //}
                        return "Website: " + ipAddressOrWebsite;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    //// Try to resolve the input as a DNS host name (website)
                    //IPHostEntry hostEntry = Dns.GetHostEntry(ipAddressOrWebsite);
                    ///return "Website: " + hostEntry.HostName;
                    //// If successful, it's a website
                    return "Website: " + ipAddressOrWebsite;
                }
                catch (Exception)
                {
                    // If resolving fails, it's neither a valid IP address nor a website
                    //validHostName = true;
                    return "Invalid input: " + ipAddressOrWebsite;
                }
            }
        }

        /* Created Date: 21/12/2023 by dinesh
         * this function loads ip and port to the variables
         */

        public static void GetServerdetails()
        {
            string Exepath = Application.StartupPath;
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Exepath + "\\Database\\Setup.mdb;Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;";
            OleDbConnection conn = new OleDbConnection(connstr);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                OleDbCommand cmd = new OleDbCommand("Select * from ServerMapping where Flag='Y'", conn); //new OleDbCommand("Select VIPLIp,VIPLPort,PWDIp,PWDPort from SetupInfo where Flag='Y'", conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    setIpAddress = dt.Rows[0]["ipaddress"].ToString(); //dt.Rows[0]["VIPLIp"].ToString();
                    setPort = dt.Rows[0]["portno"].ToString(); //dt.Rows[0]["VIPLPort"].ToString();
                    //PWD_IP = dt.Rows[0]["PWDIp"].ToString();
                    //PWD_Port = dt.Rows[0]["PWDPort"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog("Error - At GetConnectionSetup() :" + ex.Message);
            }
        }

        /* Created Date: 21/12/2023 by dinesh
         * this method sets variables that requires to form a complete api link of RMC
         */
        //public static void SetRMCAPI()
        //{
        //    GetServerdetails();
        //    setIpAddress.Trim();
        //    setPort.Trim();

        //    try
        //    {
        //        //added by dinesh
        //        string check = GetIPAddressOrWebsiteName(setIpAddress);
        //        if (check.Contains("Invalid input:"))
        //        {
        //            validHostName = true;
        //            ErrorLog("Invalid Website " + check);
        //        }
        //        else if (check.Contains("Website: "))
        //        {
        //            URL = setIpAddress = check.Substring(8, Math.Abs(Convert.ToInt32(check.Length) - 8));
        //            URL = URL.Trim();
        //            setPort = "";
        //            Uri uri;
        //            bool isValidUri = Uri.TryCreate(URL, UriKind.Absolute, out uri);
        //            if (isValidUri)
        //            {
        //                //// getting api details here.
        //                //batchDetails_endpoint = loadSingleValueSetup("SELECT endpoints FROM Api_details where endpoints_PlantType='" + planttype + "' and used_for='BBATCHDETAILS'");
        //                //batchDetails_endpoint.Trim();

        //                //batch_endpoint = loadSingleValueSetup("SELECT endpoints FROM Api_details where endpoints_PlantType='" + planttype + "' and used_for='BBATCH'");
        //                //batch_endpoint.Trim();

        //                plantEndPoint = loadSingleValueSetup("Select endpoints FROM Api_details where endpoints_PlantType='BOTH' AND used_for ='plantExpiry'");
        //                plantEndPoint.Trim();

        //                if (URL.Contains("https"))
        //                {
        //                    URL = uri.Host;
        //                    protocol = "https";
        //                    apiLink = protocol + "://" + URL + plantEndPoint;
        //                    //PlantValidDate= Convert.ToDateTime(clsGlobalFunction.CheckPlantExpiry());

        //                }
        //                else
        //                {
        //                    URL = uri.Host;
        //                    protocol = "http";
        //                    apiLink = protocol + "://" + URL + plantEndPoint;
        //                    //PlantValidDate = Convert.ToDateTime(clsGlobalFunction.CheckPlantExpiry());
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // for ip and port only
        //            URL = setIpAddress + ":" + setPort;

        //            //batchDetails_endpoint = loadSingleValueSetup("SELECT endpoints FROM Api_details where endpoints_PlantType='" + planttype + "' and used_for='BBATCHDETAILS'");
        //            //batchDetails_endpoint.Trim();

        //            //batch_endpoint = loadSingleValueSetup("SELECT endpoints FROM Api_details where endpoints_PlantType='" + planttype + "' and used_for='BBATCH'");
        //            //batch_endpoint.Trim();

        //            plantEndPoint = loadSingleValueSetup("Select endpoints FROM Api_details where endpoints_PlantType='BOTH' AND used_for ='plantExpiry'");
        //            plantEndPoint.Trim();
        //            apiLink = protocol + "://" + URL + plantEndPoint;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog("Please check website name in setup " + ex.Message);
        //    }
        //}

        /* Created Date: 21/12/2023 by dinesh
         * this method sets variables that requires to form a complete api link of BT
         */
        //public static void SetBitumenAPI()
        //{
        //    try
        //    {
        //        GetServerdetails();
        //        setIpAddress.Trim();
        //        setPort.Trim();
        //        //added by dinesh
        //        try
        //        {
        //            string check = GetIPAddressOrWebsiteName(setIpAddress);

        //            if (check.Contains("Invalid input:"))
        //            {
        //                validHostName = true;
        //                ErrorLog("Invalid Website " + check);
        //            }
        //            else if (check.Contains("Website: "))
        //            {
        //                URL = setIpAddress = check.Substring(8, Math.Abs(Convert.ToInt32(check.Length) - 8));
        //                URL = URL.Trim();
        //                setPort = "";
        //                Uri uri;
        //                bool isValidUri = Uri.TryCreate(URL, UriKind.Absolute, out uri);
        //                if (isValidUri)
        //                {
        //                    // if (planttype == "")
        //                    // getting api details here.
        //                    endpoint = loadSingleValueSetup("SELECT endpoints FROM Api_details where endpoints_PlantType='" + planttype + "' and used_for='batchmix'");
        //                    endpoint.Trim();
        //                    plantEndPoint = loadSingleValueSetup("Select endpoints FROM Api_details where endpoints_PlantType='BOTH' AND used_for ='plantExpiry'");
        //                    plantEndPoint.Trim();

        //                    if (URL.Contains("https"))
        //                    {
        //                        URL = uri.Host;
        //                        protocol = "https";
        //                        apiLink = protocol + "://" + URL + plantEndPoint;
        //                    }
        //                    else
        //                    {
        //                        URL = uri.Host;
        //                        protocol = "http";
        //                        apiLink = protocol + "://" + URL + plantEndPoint;
        //                    }


        //                }
        //            }
        //            else
        //            {
        //                URL = setIpAddress + ":" + setPort;
        //                endpoint = loadSingleValueSetup("SELECT endpoints FROM Api_details where endpoints_PlantType='" + planttype + "' and used_for='batchmix'");
        //                endpoint.Trim();
        //                plantEndPoint = loadSingleValueSetup("Select endpoints FROM Api_details where endpoints_PlantType='BOTH' AND used_for ='plantExpiry'");
        //                plantEndPoint.Trim();
        //                apiLink = protocol + "://" + URL + plantEndPoint;

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorLog("Please check website name in setup " + ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //--------------- 25/12/2023 : BhaveshT - Method to fetch API Name and return -------------------------------------------------

        //public static string GetAPIName(string usedFor, string endpoint_plantType)
        //{
        //    usedFor = clsFunctions.loadSingleValueSetup("Select endpoints from API_Details where used_for ='"+ usedFor + "' and endpoints_plantType = '"+ endpoint_plantType + "'");
        //    //apiName = clsFunctions.loadSingleValueSetup("Select " + apiName + " from ServerMapping_Preset where AliasName = '" + aliasName + "' ");
        //
        //    return usedFor;
        //}

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        //--------------- 08/01/2024 : BhaveshT - Method to fetch API Name and return -------------------------------------------------
        public static string GetAPINameFromPreset(string ipaddress, string aliasName)
        {
            // 12/02/2024 - BhaveshT : if apiname's parameter is ipaddress and required for pwd, it will form url from IPaddress & Port
            if (ipaddress.Contains("ipaddress") && aliasName.Contains("PWD - RMC"))
            {
                string ip = clsFunctions.loadSingleValueSetup("Select " + ipaddress + " from ServerMapping_Preset where AliasName = '" + aliasName + "' ");
                // by dinesh 
                ip = ip.Replace("#", "");
                string port = clsFunctions.loadSingleValueSetup("Select portno from ServerMapping_Preset where AliasName = '" + aliasName + "' ");

                ipaddress = ip + ":" + port;

            }
            else
            {
                //apiName = clsFunctions.loadSingleValueSetup("Select endpoints from API_Details where used_for ='"+ apiName + "' and endpoints_plantType = '"+ endpoint_plantType + "'");
                ipaddress = clsFunctions.loadSingleValueSetup("Select " + ipaddress + " from ServerMapping_Preset where AliasName = '" + aliasName + "' ");

            }
            return ipaddress;

            ////apiName = clsFunctions.loadSingleValueSetup("Select endpoints from API_Details where used_for ='"+ apiName + "' and endpoints_plantType = '"+ endpoint_plantType + "'");
            //ipaddress = clsFunctions.loadSingleValueSetup("Select " + ipaddress + " from ServerMapping_Preset where AliasName = '" + aliasName + "' ");
            //
            //return ipaddress;
        }

        //--------------- 23/01/2024 : BhaveshT - Method to fetch API Name and return -------------------------------------------------
        public static string GetAPINameFromServerMapping(string ipaddress, string aliasName)
        {
            // 12/02/2024 - BhaveshT : if apiname's parameter is ipaddress and required for pwd, it will form url from IPaddress & Port
            if (ipaddress.Contains("ipaddress") && aliasName.Contains("PWD - RMC"))
            {
                string ip = clsFunctions.loadSingleValueSetup("Select " + ipaddress + " from ServerMapping where AliasName = '" + aliasName + "' ");
                string port = clsFunctions.loadSingleValueSetup("Select portno from ServerMapping where AliasName = '" + aliasName + "' ");

                ipaddress = ip + ":" + port;

            }
            else
            {
                //apiName = clsFunctions.loadSingleValueSetup("Select endpoints from API_Details where used_for ='"+ apiName + "' and endpoints_plantType = '"+ endpoint_plantType + "'");
                ipaddress = clsFunctions.loadSingleValueSetup("Select " + ipaddress + " from ServerMapping where AliasName = '" + aliasName + "' ");

            }

            return ipaddress;
        }

        //--------------- 23/01/2024 : BhaveshT - Method to fetch AliasName and return -------------------------------------------------
        public static string GetActiveAliasNameFromServerMapping()
        {
            string deptname = "";
            try
            {
                deptname = clsFunctions.loadSingleValueSetup("Select AliasName from ServerMapping where Flag = 'Y' ");

                if (deptname == "")
                {
                    clsFunctions_comman.UniBox("Please set Department for Production");
                    return "";
                }
                return deptname;
            }
            catch (Exception ex)
            {
                deptname = "";
            }
            return deptname;
        }

        //--------------- 23/01/2024 : BhaveshT - Method to fetch deptname and return -------------------------------------------------
        public static string GetActiveDeptNameFromServerMapping()
        {
            string deptname = "";
            try
            {
                deptname = clsFunctions.loadSingleValueSetup("Select deptname from ServerMapping where Flag = 'Y' ");

                if (deptname == "")
                {
                    clsFunctions_comman.UniBox("Please set Department for Production");
                    return "";
                }
                return deptname;
            }
            catch (Exception ex)
            {
                deptname = "";
            }
            return deptname;
        }

        //--------------- 23/01/2024 : BhaveshT - Method to fill WorkOrderNames in comboBox according activeDeptName --------------------------
        public static string FillWorkOrdersInCombo(ComboBox workNameCombo, string activeDept)
        {
            string a = "0";
            try
            {
                clsFunctions_comman.FillCombo("Select Distinct workname from workorder where WorkType = '" + activeDept + "' AND iscompleted <> 'Y' ", workNameCombo);
                return a = "1";
            }
            catch (Exception ex)
            {
                return a = "0";
            }
        }

        //------------------------- BhaveshT ------------------------------------------------------------------------

        public static void OpenAliasSelector()
        {
            DataTable dt = clsFunctions.fillDatatable_setup("Select Distinct AliasName from ServerMapping");

            if (dt.Rows.Count > 1)
            {
                SelectAliasForProduction selectDept = new SelectAliasForProduction();
                selectDept.Show();
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static bool enableLoadDetailsBtn(string StartTime)
        {
            DataTable dt = clsFunctions.fillDatatable("Select * from tblHotMixPlant where tdate = #" + DateTime.Parse(StartTime).ToString("yyyy/MM/dd") + "#");
            string tempdate = DateTime.Now.Date.ToString("yyyy/MM/dd");
            string temptime = StartTime;
            string time2 = Convert.ToDateTime(StartTime).ToString();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    tempdate = DateTime.Parse(row["tdate"].ToString()).ToString("yyyy/MM/dd");
                    temptime = DateTime.Parse(row["ttime"].ToString()).ToString("HH:mm:ss");
                    time2 = Convert.ToDateTime(temptime).AddSeconds(10).ToString();
                }
            }
            //     string n = Convert.ToDateTime(tempdate).ToString("dd-MM-yyyy");
            if (Convert.ToDateTime(Convert.ToDateTime(time2).ToString("HH:mm:ss")) <= Convert.ToDateTime(StartTime) && Convert.ToDateTime(tempdate) <= DateTime.Now.Date)
                return true;
            else
                return false;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool LoadDetailsCompareDateTime(string vehicleNo, string endTime)
        {
            bool YesNo = false;
            try
            {
                //DataTable dt = clsFunctions.fillDatatable("select ttime from tblHotMixPlant where tipper='" + vehicleNo + "' AND  tdate = #" + DateTime.Parse(endTime).ToString("yyyy/MM/dd") + "#");
                DataTable dt = clsFunctions.fillDatatable("select ttime from tblHotMixPlant where tipper='" + vehicleNo + "' AND  tdate = #" + DateTime.Parse(endTime).ToString("yyyy/MM/dd") + "#");

                if (dt.Rows.Count != 0)
                {

                    //string startTime = Convert.ToDateTime(dt.Rows[0][0]).ToString("HH:mm:ss");

                    string startTime = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1][0]).ToString("HH:mm:ss");

                    //endTime = DateTime.Now.ToString();
                    if (startTime != "")
                    {
                        TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
                        double ss = Convert.ToDouble(duration.TotalHours);
                        if (ss <= 1)
                        {
                            DialogResult dialogResult = MessageBox.Show("Please select another vehicle");

                            //if (dialogResult == DialogResult.Yes)
                            //{
                            YesNo = true;
                            //}
                            //else if (dialogResult == DialogResult.No)
                            //{
                            //    YesNo = false;
                            //    MessageBox.Show("Please Select vehicle!!");
                            //}
                        }
                    }
                }
            }
            catch
            {
            }
            return YesNo;
        }

        //--------------------------------- 24/01/2024 : BhaveshT - To get Version of exe ---------------------------------------------
        public static string GetUniUploaderVersion()
        {
            string filePath = "";
            try
            {
                filePath = Application.StartupPath + "\\UniUploader.exe"; // Replace this with the actual path to UniUploader.exe

                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);
                //return $"File Version: {versionInfo.FileVersion}\nProduct Version: {versionInfo.ProductVersion}";
                return $"v{versionInfo.FileVersion}";
            }
            catch (Exception ex)
            {
                return "Not Installed";
            }
        }

        //--------------------------------- 25/07/2024 : BhaveshT - To get Version of UniUploader_PWD_RMC.exe ---------------------------------------------
        public static string GetUniUploaderVersionPWD()
        {
            string filePath = "";
            try
            {
                filePath = Application.StartupPath + "\\UniUploader_PWD_RMC.exe"; // Replace this with the actual path to UniUploader.exe

                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);
                //return $"File Version: {versionInfo.FileVersion}\nProduct Version: {versionInfo.ProductVersion}";
                return $"v{versionInfo.FileVersion}";
            }
            catch (Exception ex)
            {
                return "Not Installed";
            }
        }

        //--------------- 27/01/2024 : BhaveshT - Method to fetch PlantCode and return -------------------------------------------------
        public static string GetActivePlantCodeFromServerMapping()
        {
            string pCode = "";
            try
            {
                pCode = clsFunctions.loadSingleValueSetup("Select PlantCode from ServerMapping where Flag = 'Y' ");

                if (pCode == "")
                {
                    clsFunctions_comman.UniBox("Please set Department for Production");
                    return "";
                }
                activePlantCode = pCode;
                return pCode;
            }
            catch (Exception ex)
            {
                pCode = "";
            }
            return pCode;
        }

        //--------------- 07/10/2024 : BhaveshT - Method to fetch PlantType and return -------------------------------------------------
        public static string GetActivePlantTypeFromServerMapping()
        {
            string ptype = "";
            try
            {
                ptype = clsFunctions.loadSingleValueSetup("Select PlantType from ServerMapping where Flag = 'Y' ");

                if (ptype == "")
                {
                    clsFunctions_comman.UniBox("Please set Department for Production");
                    return "";
                }
                activePlantType = ptype;
                return ptype;
            }
            catch (Exception ex)
            {
                ptype = "";
            }
            return ptype;
        }


        //--------------- 27/01/2024 : BhaveshT - Method to fetch deviceId and return -------------------------------------------------
        public static string GetActiveDeviceIdFromServerMapping()
        {
            string deviceId = "";
            try
            {
                deviceId = clsFunctions.loadSingleValueSetup("Select DeviceID from ServerMapping where Flag = 'Y' ");

                if (deviceId == "")
                {
                    clsFunctions_comman.UniBox("Please set Department for Production");
                    return "";
                }
                return deviceId;
            }
            catch (Exception ex)
            {
                deviceId = "";
            }
            return deviceId;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void InsertInServerMappingAtReg(string aliasName, string deviceId, string plantCode)
        {
            try
            {
                DataTable dt = new DataTable();
                clsFunctions.AdoData_setup("Delete * from ServerMapping");

                string insertQuery = "";
                // 06/01/2021 - BhaveshT : InsertQuery to Insert data from ServerMapping_Preset into ServerMapping for selected AliasName
                dt = clsFunctions.fillDatatable_setup("Select * FROM ServerMapping_Preset WHERE AliasName = '" + aliasName + "'");


                //foreach (DataRow row in dt.Rows)
                //{
                //    //-
                //    //string insertQuery = "INSERT INTO ServerMapping (SrNo, AliasName, Note1, ipaddress, portno, BT_API, RMC_Trans_API, RMC_Transaction_API, Software_Status_API, plantExpiry," +
                //    //              " deptname, PlantType, Note2, ipaddress1, port1, BT_API1, RMC_Transaction_API1, RMC_Trans_API1, AutoReg_SMS, AutoReg_Verify, AutoReg_Save, GetWO, GetAllWO, AllocateWO, getPlantDetails, GetMobNoFromWO, GetProdErrorTemplate, sendSMS, DPTStatus, Flag, DeviceID, PlantCode) " +
                //    //                 "VALUES ('" + row["SrNo"].ToString() + "', '" + row["AliasName"].ToString() + "', '" + row["Note1"].ToString() + "', '" + row["ipaddress"].ToString() + "'," +
                //    //                 " '" + row["Portno"].ToString() + "', '" + row["BT_API"].ToString() + "', '" + row["RMC_Trans_API"].ToString() + "', '" + row["RMC_Transaction_API"].ToString() + "', " +
                //    //                 " '" + row["Software_Status_API"].ToString() + "', '" + row["plantExpiry"].ToString() + "', '" + row["deptname"].ToString() + "', '" + row["PlantType"].ToString() + "', " +
                //    //                 " '" + row["Note2"].ToString() + "', '" + row["ipaddress1"].ToString() + "', '" + row["port1"].ToString() + "', '" + row["BT_API1"].ToString() + "', " +
                //    //                 " '" + row["RMC_Transaction_API1"].ToString() + "', '" + row["RMC_Trans_API1"].ToString() + "', '" + row["AutoReg_SMS"].ToString() + "', " +
                //    //                 " '" + row["AutoReg_Verify"].ToString() + "', '" + row["AutoReg_Save"].ToString() + "', '" + row["GetWO"].ToString() + "', '" + row["GetAllWO"].ToString() + "', " +
                //    //                 " '" + row["AllocateWO"].ToString() + "', '" + row["getPlantDetails"].ToString() + "','" + row["GetMobNoFromWO"].ToString() + "','" + row["GetProdErrorTemplate"].ToString() + "'," +
                //    //                 " '" + row["sendSMS"].ToString() + "', 'N', 'Y', '" + deviceId + "', '" + plantCode + "')";


                //    string insertQuery = "INSERT INTO ServerMapping (SrNo, AliasName, Note1, ipaddress, portno, BT_API, RMC_Trans_API, RMC_Transaction_API, Software_Status_API, plantExpiry," +
                //                  " deptname, PlantType, Note2, ipaddress1, port1, BT_API1, RMC_Transaction_API1, RMC_Trans_API1, AutoReg_SMS, AutoReg_Verify, AutoReg_Save, GetWO, GetAllWO, AllocateWO, getPlantDetails, GetMobNoFromWO, GetProdErrorTemplate, sendSMS, DPTStatus, Flag, DeviceID, PlantCode," +
                //                  " DataHeaderTableSync, DataTransactionTableSync, ServerMapping_Preset, Unipro_Setup, PlantSetup, Plant_LiveStatus_History, m_SaveInstall, m_latestUp_Insert, m_latestUp_Get ) " +
                //                     "VALUES ('" + row["SrNo"].ToString() + "', '" + row["AliasName"].ToString() + "', '" + row["Note1"].ToString() + "', '" + row["ipaddress"].ToString() + "'," +
                //                     " '" + row["Portno"].ToString() + "', '" + row["BT_API"].ToString() + "', '" + row["RMC_Trans_API"].ToString() + "', '" + row["RMC_Transaction_API"].ToString() + "', " +
                //                     " '" + row["Software_Status_API"].ToString() + "', '" + row["plantExpiry"].ToString() + "', '" + row["deptname"].ToString() + "', '" + row["PlantType"].ToString() + "', " +
                //                     " '" + row["Note2"].ToString() + "', '" + row["ipaddress1"].ToString() + "', '" + row["port1"].ToString() + "', '" + row["BT_API1"].ToString() + "', " +
                //                     " '" + row["RMC_Transaction_API1"].ToString() + "', '" + row["RMC_Trans_API1"].ToString() + "', '" + row["AutoReg_SMS"].ToString() + "', " +
                //                     " '" + row["AutoReg_Verify"].ToString() + "', '" + row["AutoReg_Save"].ToString() + "', '" + row["GetWO"].ToString() + "', '" + row["GetAllWO"].ToString() + "', " +
                //                     " '" + row["AllocateWO"].ToString() + "', '" + row["getPlantDetails"].ToString() + "','" + row["GetMobNoFromWO"].ToString() + "','" + row["GetProdErrorTemplate"].ToString() + "'," +
                //                     " '" + row["sendSMS"].ToString() + "', 'N', 'Y', '" + deviceId + "', '" + plantCode + "'," +

                //                     " '" + row["DataHeaderTableSync"].ToString() + "','" + row["DataTransactionTableSync"].ToString() + "','" + row["ServerMapping_Preset"].ToString() + "',     " +
                //                     " '" + row["Unipro_Setup"].ToString() + "','" + row["PlantSetup"].ToString() + "','" + row["Plant_LiveStatus_History"].ToString() + "',     " +
                //                     " '" + row["m_SaveInstall"].ToString() + "','" + row["m_latestUp_Insert"].ToString() + "','" + row["m_latestUp_Get"].ToString() + "')";


                //    int a = clsFunctions.AdoData_setup(insertQuery);

                //    int a1 = clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET PlantCode = '" + plantCode + "' WHERE AliasName = '" + aliasName + "' ");
                //    int a2 = clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET DeviceID  = '" + deviceId + "' WHERE AliasName = '" + aliasName + "' ");

                //    clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET DPTStatus = 'N' ");
                //    clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET DPTStatus = 'Y' WHERE AliasName = '" + aliasName + "'  ");

                /********************************************************************************************************************************************************/
                // DataTable dt = clsFunctions.fillDatatable_setup("SELECT * FROM ServerMapping_Preset WHERE AliasName = '" + aliasName + "'");

                // Iterate through the rows of the DataTable
                foreach (DataRow row in dt.Rows)
                {
                    // Construct the insert query for ServerMapping table using data from the selected row

                    insertQuery = $@"INSERT INTO ServerMapping (SrNo, AliasName, Note1, ipaddress, portno, BT_API, RMC_Trans_API, RMC_Transaction_API, Software_Status_API, plantExpiry, deptname, PlantType, Note2, 
                                    ipaddress1, port1, BT_API1, RMC_Transaction_API1, RMC_Trans_API1, AutoReg_SMS, AutoReg_Verify, AutoReg_Save, GetWO, GetAllWO, AllocateWO, GetPlantDetails, GetMobNoFromWO, 
                                    GetProdErrorTemplate, sendSMS, getInstallDetails, DPTStatus, Flag, DeviceID, PlantCode, GetDataHeaderTableSync,UploadDataHeaderTableSync, GetDataTransactionTableSync,
                                    UploadDataTransactionTableSync, ServerMapping_Preset, Unipro_Setup, PlantSetup, Plant_LiveStatus_History, m_SaveInstall, m_latestUp_Insert, m_latestUp_Get,PlantExpiryDate,Upload_UniproSetupID ,
                                    sendRecipe_API, Post_SiteName, ipaddress2, BT_API2, RMC_Trans_API2, RMC_Transaction_API2) 
                                    VALUES ({row["SrNo"]}, '{row["AliasName"]}', '{row["Note1"]}', '{row["ipaddress"]}', '{row["portno"]}', '{row["BT_API"]}', '{row["RMC_Trans_API"]}', '{row["RMC_Transaction_API"]}', '{row["Software_Status_API"]}', 
                                        '{row["plantExpiry"]}', '{row["deptname"]}', '{row["PlantType"]}', '{row["Note2"]}', '{row["ipaddress1"]}', '{row["port1"]}', '{row["BT_API1"]}', '{row["RMC_Transaction_API1"]}', '{row["RMC_Trans_API1"]}', 
                                        '{row["AutoReg_SMS"]}', '{row["AutoReg_Verify"]}', '{row["AutoReg_Save"]}', '{row["GetWO"]}', '{row["GetAllWO"]}', '{row["AllocateWO"]}', '{row["GetPlantDetails"]}', '{row["GetMobNoFromWO"]}', '{row["GetProdErrorTemplate"]}', 
                                        '{row["sendSMS"]}', '{row["getInstallDetails"]}', '{row["DPTStatus"]}', 'Y', '{row["DeviceID"]}', '{row["PlantCode"]}', '{row["GetDataHeaderTableSync"]}', '{row["UploadDataHeaderTableSync"]}', '{row["GetDataTransactionTableSync"]}', 
                                        '{row["UploadDataTransactionTableSync"]}', '{row["ServerMapping_Preset"]}', '{row["Unipro_Setup"]}', '{row["PlantSetup"]}', '{row["Plant_LiveStatus_History"]}', '{row["m_SaveInstall"]}', '{row["m_latestUp_Insert"]}', 
                                        '{row["m_latestUp_Get"]}', '{row["PlantExpiryDate"]}', '{row["Upload_UniproSetupID"]}', '{row["sendRecipe_API"]}', '{row["Post_SiteName"]}', '{row["ipaddress2"]}', '{row["BT_API2"]}', '{row["RMC_Trans_API2"]}', '{row["RMC_Transaction_API2"]}')";


                    // Execute the insert query
                    int insertResult = clsFunctions.AdoData_setup(insertQuery);
                    // Update ServerMapping with expiry date                            updating the expiry date in servermapping table
                    //string expiry = clsFunctions.loadSingleValue("Select ");
                    //int updateResultPlantCode1 = clsFunctions.AdoData_setup("UPDATE ServerMapping SET PlantExpiryDate=#"++"# WHERE AliasName = '" + aliasName + "'");

                    // Update ServerMapping_Preset with new values
                    int updateResultPlantCode = clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET PlantCode = '" + plantCode + "' WHERE AliasName = '" + aliasName + "'");
                    int updateResultDeviceID = clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET DeviceID = '" + deviceId + "' WHERE AliasName = '" + aliasName + "'");

                    // Update DPTStatus in ServerMapping_Preset
                    clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET DPTStatus = 'N'");
                    clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET DPTStatus = 'Y' WHERE AliasName = '" + aliasName + "'");
                }
                //}
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at InsertInServerMappingAtReg() - " + ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------


        //----------------------- Insert Error Details in Plant_Error_Log Table ------------------------

        public static bool InsertTotblOpcTagIndicesTable(DataTable dtNameSetupNames, List<string> listOfOpcTagNames)
        {
            string sqlInsertQuery = string.Empty;

            try
            {
                sqlInsertQuery = "INSERT INTO tblOpcTagIndices (";

                for (int columnIndex = 0; columnIndex < dtNameSetupNames.Rows.Count; columnIndex++)
                {
                    sqlInsertQuery += " [" + dtNameSetupNames.Rows[columnIndex][0] + "],";
                }
                sqlInsertQuery += " [IsActive], [PlcProgramModel],";
                sqlInsertQuery = sqlInsertQuery.TrimEnd(',');
                sqlInsertQuery += ")";
                sqlInsertQuery += " VALUES (";
                for (int i = 0; i < dtNameSetupNames.Rows.Count; i++)
                {
                    int opcTagIndex = -1;
                    string opcTagName = dtNameSetupNames.Rows[i]["OPCTagName"].ToString();
                    if (opcTagName == "<None>")
                    {
                        sqlInsertQuery += " " + -1 + ",";
                    }
                    else
                    {
                        if (listOfOpcTagNames.Contains(opcTagName))
                        {
                            opcTagIndex = listOfOpcTagNames.IndexOf(opcTagName);
                        }
                        sqlInsertQuery += " " + opcTagIndex + ",";
                    }
                }
                sqlInsertQuery = sqlInsertQuery.TrimEnd(',');
                sqlInsertQuery += " ,1";   //default active when inserting new record 
                sqlInsertQuery += " ,'" + mdiMain.SelectedOPCServerProgID + "'"; //set PlcProgramModel to current OPC Server ProgID

                sqlInsertQuery += ")";

                int result = AdoData(sqlInsertQuery);

                if (result != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions.InsertToErrorLogTable(ex.HResult, ex.Message);
                return false;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static bool InsertTotblOpcTagIndicesTable1(DataTable dtNameSetupNames, List<string> listOfOpcTagNames)
        {
            string sqlInsertQuery = string.Empty;

            try
            {
                sqlInsertQuery = "INSERT INTO tblOpcTagIndices (";

                for (int columnIndex = 0; columnIndex < dtNameSetupNames.Rows.Count; columnIndex++)
                {
                    sqlInsertQuery += " [" + dtNameSetupNames.Rows[columnIndex][0] + "],";
                }
                sqlInsertQuery += " [IsActive], [PlcProgramModel],";
                sqlInsertQuery = sqlInsertQuery.TrimEnd(',');
                sqlInsertQuery += ")";
                sqlInsertQuery += " VALUES (";
                for (int i = 0; i < dtNameSetupNames.Rows.Count; i++)
                {
                    int opcTagIndex = -1;
                    string opcTagIndexValue = "";
                    string opcTagName = dtNameSetupNames.Rows[i]["OPCTagName"].ToString();
                    if (opcTagName == "<None>")
                    {
                        //sqlInsertQuery += " " + -1 + ",";
                        sqlInsertQuery += " '" + -1 + "',";
                    }
                    else
                    {
                        if (listOfOpcTagNames.Contains(opcTagName))
                        {
                            opcTagIndex = listOfOpcTagNames.IndexOf(opcTagName);
                            opcTagIndexValue = listOfOpcTagNames[opcTagIndex];
                        }
                        //sqlInsertQuery += " " + opcTagIndex + ",";
                        sqlInsertQuery += " '" + opcTagIndexValue + "',";
                    }
                }
                sqlInsertQuery = sqlInsertQuery.TrimEnd(',');
                sqlInsertQuery += " ,1";   //default active when inserting new record 
                sqlInsertQuery += " ,'" + mdiMain.SelectedOPCServerProgID + "'"; //set PlcProgramModel to current OPC Server ProgID

                sqlInsertQuery += ")";

                sqlInsertQuery = sqlInsertQuery.Replace("''", "'-1'");


                int result = AdoData(sqlInsertQuery);

                if (result != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions.InsertToErrorLogTable(ex.HResult, ex.Message);
                return false;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------



        //------------ 15/02/2024 : BhaveshT - To create new column in Setup database with default value -------
        public static void checknewcolumnWithDefaultValInSetupDB(string clm, string datatype, string table, string defaultVal)
        {
            try
            {
                string cmdText = "Select " + clm + " from " + table + " ";
                if (clsFunctions.conns.State == ConnectionState.Closed)
                    clsFunctions.conns.Open();
                new OleDbDataAdapter(new OleDbCommand(cmdText, clsFunctions.conns)).Fill(new DataTable());
            }
            catch (Exception ex)
            {
                //clsFunctions.AdoData("alter table " + table + " add column " + clm + " " + datatype);

                clsFunctions.AdoData_setup("ALTER TABLE " + table + " ADD COLUMN " + clm + " " + datatype + " DEFAULT '" + defaultVal + "'");

                clsFunctions_comman.ErrorLog("clsPatch: Created column: '" + clm + "' In '" + table + "' with Datatype: '" + datatype + "', default value: '" + defaultVal + "'");

                if (clm == "pType")
                {
                    clsFunctions.AdoData_setup("UPDATE PlantSetup SET pType = 'A'");
                }
                if (clm == "DocketHours")
                {
                    clsFunctions.AdoData_setup("INSERT INTO tipper_interval (truckinterval, DocketHours) VALUES ('59', 24)");
                }


            }
        }

        //------------------ 19/02/2024 - BhaveshT : to get pType from PlantSetup ----------------

        public static string GetPTypeFromSetup()
        {
            string pType = "";
            try
            {
                pType = clsFunctions.loadSingleValueSetup("SELECT pType from PlantSetup");
                return pType;
            }
            catch
            {
                clsFunctions.checknewcolumnWithDefaultValInSetupDB("pType", "Text(1)", "PlantSetup", "A");
                return pType;
            }

            return pType;

        }
        //-------------------------------------------------------------

        /*
         BhaveshT - 22/02/2024
        Function name: getFileType() | Return type: string
        Created this function to get File Type from DbInfo table of Setup db to avoid to use in combobox of BatchData_Tuskar form
        which will work on multiple File types data upload
         */
        public static string getDescription()
        {
            try
            {
                activeDesciption = clsFunctions.loadSingleValueSetup("SELECT Description from Unipro_Setup where Status = 'Y'");      //29/12/2023
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("clsFunctions.getDescription() - " + ex.Message);
                return "";
                //MessageBox.Show("clsFunctions.getDescription() - " + ex.Message);
            }
            return activeDesciption;
        }
        //-------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 15/03/2023 - This function creates Returns alias name according dept & plant Type
        public static string CreateAliasName(string dept, string plantType)
        {

            string aliasName = "";
            string pType = plantType;
            string p = "";
            if (pType.Contains("RMC"))
            {
                p = "RMC";
            }
            else if (pType.Contains("Bitu") || pType.Contains("BT"))
            {
                p = "BT";
            }

            if (dept.Contains("Private"))
            {
                dept = "ISCADA";
            }

            try
            {
                aliasName = clsFunctions.loadSingleValueSetup("SELECT AliasName FROM ServerMapping_Preset WHERE AliasName LIKE '%" + dept + "%' AND AliasName LIKE '%" + p + "%' ;");

                return aliasName;
            }
            catch
            {

                return aliasName;
            }

            return aliasName;
        }
        //---------------------------- 23/03/2024 : BhaveshT ------------------------------------------

        public static bool UpdateApiNamesInServerMappingAndPresetForPWD_BT()
        {
            try
            {
                clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

                if (clsFunctions.aliasName == "PWD - BT")
                {
                    string a = clsFunctions.GetAPINameFromServerMapping("GetPlantDetails", clsFunctions.aliasName);
                    if (a == "NA")
                    {
                        clsFunctions.AdoData_setup("DELETE * FROM ServerMapping WHERE AliasName = 'PWD - BT' ");

                        clsFunctions.AdoData_setup("DELETE * FROM ServerMapping_Preset WHERE AliasName = 'PWD - BT' ");

                        clsFunctions.AdoData_setup("INSERT INTO ServerMapping_Preset (SrNo, AliasName, Note1, ipaddress, portno, BT_API, RMC_Trans_API, RMC_Transaction_API," +
                            " Software_Status_API, plantExpiry, deptname, PlantType, Note2, ipaddress1, port1, BT_API1, RMC_Transaction_API1, RMC_Trans_API1, AutoReg_SMS, " +
                            "AutoReg_Verify, AutoReg_Save, GetWO, GetAllWO, AllocateWO, getPlantDetails, GetMobNoFromWO, GetProdErrorTemplate, sendSMS, DPTStatus, Flag) " +
                            "VALUES ('1', 'PWD - BT', 'PWD NAGPUR SERVER IP', '117.240.186.59', '7918', 'NA', 'NA', 'NA',  '/vipl_unipro_rest/machine_status/set_machine_status', " +
                            "'/vipl_unipro_rest/work_orders/getPlantDetails?device_id=', 'PWD', 'BT',  'VIPL SERVER', 'http://pwdscada.in', 'NA', '/vipl_unipro_rest/version2/batch_mix/new7301/insert'," +
                            " 'NA', 'NA', '/vipl_unipro_rest/plant_reg_history/sendSMS?regMobNo=',  '/vipl_unipro_rest/plant_reg_history/verifyRegistration?regMobNo=', " +
                            "'/vipl_unipro_rest/plant_reg_history/saveInstallationDetails', '/vipl_unipro_rest/work_orders/getAllocatedWorkOrdersForPlant?plantCode=', " +
                            "'/vipl_unipro_rest/work_orders/getAllWorkOrders',  '/vipl_unipro_rest/work_orders/allocateWorkOrder?workCode=\" \"&plantCode=', '/vipl_unipro_rest/work_orders/getPlantDetails?device_id='," +
                            "'/vipl_unipro_rest/rmc/get_mobile_numbers_from_wo_code?work_code=','/vipl_unipro_rest/rmc/get_sms_template?functionality=Production Alert', '/SMS_Utility/saveData', 'N', 'N')");


                        int ak = clsFunctions.AdoData_setup("DELETE * FROM PlantSetup WHERE DeptName = 'PWD'");

                        clsFunctions_comman.UniBox("Department = PWD-BT and APIs were old, updated APIs.\n" +
                            "Please Re-Register the software using DeviceID");

                        Application.Exit();

                    }
                }
                else
                {

                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        //--------------- 26/03/2024 : BhaveshT - Method to fill Contractor Name in comboBox according activeDeptName --------------------------
        public static string FillContractorInCombo(ComboBox ContractorNameCombo, string activeDept)
        {
            string a = "0";
            try
            {
                clsFunctions_comman.FillCombo("Select Distinct ContractorName from workorder where WorkType = '" + activeDept + "' AND iscompleted <> 'Y' ", ContractorNameCombo);
                return a = "1";
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] clsFunctions.FillContractorInCombo - " + ex.Message);
                return a = "0";
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        /*
         
         02/04/2024 - BhaveshT
         Created method: FillWorkOrderFromContractor(cmbConName, cmbworkname, txtConCode, cmbJobSite)
         - which is used on every form in Contractor SelectedIndexChanged event to fill WorkOrder in comboBox as per selected ContractorName.
         
         */
        public static void FillWorkOrderFromContractor(ComboBox cmbConName, ComboBox cmbWorkName, TextBox txtConCode, ComboBox cmbJobSite)
        {
            try
            {
                txtConCode.Text = "";
                try
                {
                    cmbJobSite.SelectedIndex = -1;
                }
                catch { }

                clsFunctions.FillCombo("Select Distinct workname from workorder where WorkType = '" + clsFunctions.activeDeptName + "' AND ContractorName = '" + cmbConName.Text + "' AND iscompleted <> 'Y' ", cmbWorkName);

                try
                {
                    cmbWorkName.SelectedIndex = 0;
                }
                catch { }
            }

            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at FillWorkOrderFromContractor : " + ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------

        //24/06/2024 : BhaveshT - Get Docket Hours from tipper_interval table.

        public static int GetDocketHours()
        {
            try
            {
                string a = clsFunctions.loadSingleValueSetup("Select DocketHours from tipper_interval");

                DocketHours = Convert.ToInt32(a);
                return DocketHours;
            }
            catch (Exception ex)
            {
                return 24;
            }
        }

        //-------------------------------------------------------------------------------------------------

        //24/06/2024 : BhaveshT

        public static DateTime SubtractHours(DateTime dateTime, int hours)
        {
            return dateTime.AddHours(-hours);
        }

        //-------------------------------------------------------------------------------------------------
        //25/06/2024 : BhaveshT - Get Docket Hours from tipper_interval table.

        public static string GetTipperInterval()
        {
            try
            {
                TipperInterval = clsFunctions.loadSingleValueSetup("Select truckinterval from tipper_interval");

                return TipperInterval;
            }
            catch (Exception ex)
            {
                return "59";
            }
        }
        //-------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void FillComboBoxFromColumn(ComboBox comboBox, DataTable dataTable, string columnName)
        {
            // Clear existing items in the ComboBox
            comboBox.Items.Clear();

            // Check if the specified column exists in the DataTable
            if (!dataTable.Columns.Contains(columnName))
            {
                throw new ArgumentException($"Column '{columnName}' does not exist in the DataTable.");
            }

            // Get the specified column
            DataColumn column = dataTable.Columns[columnName];

            // Add each value from the column to the ComboBox
            foreach (DataRow row in dataTable.Rows)
            {
                object value = row[column];
                if (value != DBNull.Value)
                {
                    comboBox.Items.Add(value);
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        //09/04/2024 : BhaveshT ----------------------
        public static void SetSelectedAliasToVariables()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = clsFunctions.fillDatatable_setup("Select AliasName, Deptname, PlantType, DeviceID, PlantCode from ServerMapping where Flag = 'Y' ");

                if (dt.Rows.Count != 0)
                {
                    // Check if the plant_name column matches the selected plant name
                    foreach (DataRow row in dt.Rows)
                    {
                        clsFunctions.activeDeptName = row["Deptname"].ToString();
                        clsFunctions.aliasName = row["AliasName"].ToString();
                        clsFunctions.activePlantCode = row["PlantCode"].ToString();
                        clsFunctions.activeDeviceID = row["DeviceID"].ToString();
                        clsFunctions.activePlantType = row["PlantType"].ToString();
                    }
                }
                else if (dt.Rows.Count == 0)
                {
                    clsFunctions.OpenAliasSelector();
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Error while fetching data for SetSelectedAliasToVariables()");
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 13/04/2024 : BhaveshT - This method will insert  plant renwal details in table: Plant_Renewal_History

        public static int InsertToPlantRenewalHistory(string deviceID, string validFrom, string plantExpiry, string anotherExpiry)
        {
            int a = 0;
            try
            {
                string query = "";
                if (anotherExpiry == "0")
                {
                    query = "INSERT INTO Plant_Renewal_History (DeviceID, Date_Time, ValidFrom, PlantExpiry) " +
                    "values ('" + deviceID + "', '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "', '" + Convert.ToDateTime(validFrom).ToString("dd/MM/yyyy") + "', " +
                    "'" + Convert.ToDateTime(plantExpiry).ToString("dd/MM/yyyy") + "') ";

                }
                else
                {
                    query = "INSERT INTO Plant_Renewal_History (DeviceID, Date_Time, ValidFrom, PlantExpiry, AnotherExpiry) " +
                    "values ('" + deviceID + "', '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "', '" + Convert.ToDateTime(validFrom).ToString("dd/MM/yyyy") + "', " +
                    "'" + Convert.ToDateTime(plantExpiry).ToString("dd/MM/yyyy") + "', '" + Convert.ToDateTime(anotherExpiry).ToString("dd/MM/yyyy") + "') ";

                }

                a = clsFunctions.AdoData_setup(query);

                return a;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at InsertToPlantRenewalHistory: " + ex.Message);
                return a;
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        /* 16/04/2024 : BhaveshT 
           - This methods gets apiUrl as parameter, checks that that API is responding or not,
              if responding then returns true else return false.
        */

        public static bool CheckAPIStatus(string apiUrl)
        {
            //--------------------------------------
            //string pingResponse = "";

            if (pingResponse == "")
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response;

                    try
                    {
                        response = client.GetAsync(apiUrl).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            pingResponse = "API is working";
                            clsFunctions_comman.ErrorLog("API is working");
                            return true;
                        }
                        else
                        {
                            pingResponse = "API is not working";
                            clsFunctions_comman.ErrorLog("API is not working");
                            return false;
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        pingResponse = "API is not working";
                        clsFunctions_comman.ErrorLog("API is not working");
                        //clsFunctions.isExpiryCheckedForPType = false;
                        return false;
                    }
                }
            }
            else if (pingResponse == "API is not working")
            {
                return false;
            }

            else if (pingResponse == "API is working")
            {
                return true;
            }

            return false;

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        /* 17/04/2024 : BhaveshT 
           - This methods fetches latest_update from API and store it into DataTable and returns that DataTable
        */
        public static DataTable GetUpdate_FromServer()
        {
            try
            {
                string apiUrl = "http://192.168.1.64:8084/Mother_API/latest_update/get_last_update_record";

                //----------------------------------------------------------------------------
                clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

                string URL = "";
                string apiName = "";

                //if (clsFunctions.aliasName == "PWD - BT")
                //    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", clsFunctions.aliasName);
                //else
                //    URL = clsFunctions.GetAPINameFromPreset("ipaddress", clsFunctions.aliasName);

                apiName = clsFunctions.GetAPINameFromServerMapping("m_latestUp_Get", clsFunctions.aliasName);
                //----------------------------------------------------------------------------

                URL = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='" + clsFunctions.aliasName + "'");

                apiUrl = URL + apiName;

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                        response.EnsureSuccessStatusCode();
                        string responseBody = response.Content.ReadAsStringAsync().Result;

                        // Parse the JSON response
                        JObject jsonResponse = JObject.Parse(responseBody);
                        JArray latestUpdateData = (JArray)jsonResponse["Latest_Update_Data"]["List_Of_Latest_Update_Data"];

                        // Select the first data entry
                        JObject firstUpdate = latestUpdateData.First as JObject;

                        // Create a new DataTable
                        DataTable dataTable = new DataTable();

                        // Check if the DataTable is empty before adding columns and rows
                        if (dataTable.Columns.Count == 0)
                        {
                            // Add columns to the DataTable
                            dataTable.Columns.Add("update_date", typeof(string));
                            dataTable.Columns.Add("update_description", typeof(string));
                            dataTable.Columns.Add("update_type", typeof(string));
                            dataTable.Columns.Add("update_author", typeof(string));
                            dataTable.Columns.Add("update_unipro_version", typeof(string));
                            dataTable.Columns.Add("update_uniuploder_version", typeof(string));
                            dataTable.Columns.Add("update_status", typeof(string));
                            dataTable.Columns.Add("update_priority", typeof(string));
                            dataTable.Columns.Add("affected_modules", typeof(string));
                            dataTable.Columns.Add("message", typeof(string));
                            dataTable.Columns.Add("update_unipro_patching_system_version", typeof(string));
                            dataTable.Columns.Add("update_uniuploder_pwd_rmc_version", typeof(string));
                        }

                        // Add data to the DataRow and then add the DataRow to the DataTable
                        dataTable.Rows.Add(
                            firstUpdate["update_date"]?.ToString(),
                            firstUpdate["update_description"]?.ToString(),
                            firstUpdate["update_type"]?.ToString(),
                            firstUpdate["update_author"]?.ToString(),
                            firstUpdate["update_unipro_version"]?.ToString(),
                            firstUpdate["update_uniuploder_version"]?.ToString(),
                            firstUpdate["update_status"]?.ToString(),
                            firstUpdate["update_priority"]?.ToString(),
                            firstUpdate["affected_modules"]?.ToString(),
                            firstUpdate["message"]?.ToString(),
                            firstUpdate["update_unipro_patching_system_version"]?.ToString(),
                            firstUpdate["update_uniuploder_pwd_rmc_version"]?.ToString()
                        );

                        return dataTable;
                    }
                    catch (HttpRequestException ex)
                    {
                        clsFunctions_comman.ErrorLog($"GetUpdate_FromServer(): Error: {ex.Message}");
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static bool isAliasExists(string AliasName)      //, string Batch_Index)
        {
            //DataTable dt = new DataTable();
            DataTable dt = clsFunctions.fillDatatable_setup("Select AliasName From ServerMapping_Preset where AliasName='" + AliasName + "' ");

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------


        public static List<(string Department, string PlantType)> ParseAliasNames(string aliasNames)
        {
            List<(string Department, string PlantType)> parsedList = new List<(string Department, string PlantType)>();

            // Split the string into individual alias names
            string[] individualNames = aliasNames.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string aliasName in individualNames)
            {
                string[] parts = aliasName.Trim().Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    parsedList.Add((parts[0].Trim(), parts[1].Trim()));
                }
                else
                {
                    // Handle invalid format or missing data
                    clsFunctions.ErrorLog("Invalid AliasName format: " + aliasName);
                }
            }
            return parsedList;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 21/06/2024 : BhaveshT - Modified parameter names.

        public static async void UploadDataHeaderTableSync(string aliasName, string deptName, string plantCode)
        {
            try
            {
                string deptType = "";

                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='" + aliasName + "'") + clsFunctions.loadSingleValueSetup("Select UploadDataHeaderTableSync from ServerMapping where AliasName='" + aliasName + "'").Trim();
                ///Mother_API/DataHeaderTableSync/header_data_from_plant_code_dept_name?plant_code=p_code&dept_name=d_type

                //plantCode = clsFunctions.loadSingleValueSetup("Select PlantCode from ServerMapping where Flag='Y'");
                //string deptName = clsFunctions.loadSingleValueSetup("Select deptname from ServerMapping where Flag='Y'");

                //DataTable dt = fillDatatable_setup("Select * from DataHeaderTableSync where Info='Fields' and Type='Client' and plant_code='"+ plantCode + "' and dept_name='"+deptName+ "' and DataHeaderUpload=0");     // Dinesh
                DataTable dt = fillDatatable_setup("Select * from DataHeaderTableSync where Info='Fields' and Type='Client' and Flag='Y'");         // 22/06/2024 : BhaveshT

                DataHeader dh = new DataHeader();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dh.software_version = row["SoftwareVersion"].ToString();
                        dh.info = row["Info"].ToString();
                        dh.batch_no = row["Batch_No"].ToString();
                        dh.batch_date = row["Batch_Date"].ToString();
                        dh.batch_time = row["Batch_Time"].ToString();
                        dh.batch_time_text = row["Batch_Time_Text"].ToString();
                        dh.batch_start_time = row["Batch_Start_Time"].ToString();
                        dh.batch_end_time = row["Batch_End_Time"].ToString();
                        dh.batch_year = row["Batch_Year"].ToString();
                        dh.batcher_name = row["Batcher_Name"].ToString();
                        dh.batch_user_level = row["Batcher_User_Level"].ToString();
                        dh.customer_code = row["Customer_Code"].ToString();
                        dh.recipe_code = row["Recipe_Code"].ToString();
                        dh.recipe_name = row["Recipe_Name"].ToString();
                        dh.mixing_time = row["Mixing_Time"].ToString();
                        dh.mixer_capacity = row["Mixer_Capacity"].ToString();
                        dh.strength = row["strength"].ToString();
                        dh.site = row["Site"].ToString();
                        dh.truck_no = row["Truck_No"].ToString();
                        dh.truck_driver = row["Truck_Driver"].ToString();
                        dh.production_qty = row["Production_Qty"].ToString();
                        dh.ordered_qty = row["Ordered_Qty"].ToString();
                        dh.returned_qty = row["Returned_Qty"].ToString();
                        dh.withthisload = row["WithThisLoad"].ToString();
                        dh.batch_size = row["Batch_Size"].ToString();
                        dh.order_no = row["Order_No"].ToString();
                        dh.schedule_id = row["Schedule_Id"].ToString();
                        dh.gate1_target = row["Gate1_Target"].ToString();
                        dh.gate2_target = row["Gate2_Target"].ToString();
                        dh.gate3_target = row["Gate3_Target"].ToString();
                        dh.gate4_target = row["Gate4_Target"].ToString();
                        dh.gate5_target = row["Gate5_Target"].ToString();
                        dh.gate6_target = row["Gate6_Target"].ToString();
                        dh.cement1_target = row["Cement1_Target"].ToString();
                        dh.cement2_target = row["Cement2_Target"].ToString();
                        dh.cement3_target = row["Cement3_Target"].ToString();
                        dh.cement4_target = row["Cement4_Target"].ToString();
                        dh.filler_target = row["Filler_Target"].ToString();
                        dh.water1_target = row["Water1_Target"].ToString();
                        dh.slurry_target = row["slurry_Target"].ToString();
                        dh.water2_target = row["Water2_Target"].ToString();
                        dh.silica_target = row["Silica_Target"].ToString();
                        dh.adm1_target1 = row["Adm1_Target1"].ToString();
                        dh.adm1_target2 = row["Adm1_Target2"].ToString();
                        dh.adm2_target1 = row["Adm2_Target1"].ToString();
                        dh.adm2_target2 = row["Adm2_Target2"].ToString();
                        dh.cost_per_mtr_cube = row["Cost_Per_Mtr_Cube"].ToString();
                        dh.total_cost = row["Total_Cost"].ToString();
                        dh.plant_no = row["Plant_No"].ToString();
                        dh.weighed_net_weight = row["Weighed_Net_Weight"].ToString();
                        dh.weigh_bridge_stat = row["Weigh_Bridge_Stat"].ToString();
                        dh.t_export_status = row["tExportStatus"].ToString();
                        dh.t_upload1 = row["tUpload1"].ToString();
                        dh.t_upload2 = row["tUpload2"].ToString();
                        dh.wo_code = row["WO_Code"].ToString();
                        dh.site_id = row["Site_ID"].ToString();
                        dh.cust_name = row["Cust_Name"].ToString();
                        dh.type = row["Type"].ToString();
                        dh.table_name = row["TableName"].ToString();
                        dh.flag = row["Flag"].ToString();

                        //dh.plant_code = row["plant_code"].ToString();
                        //dh.dept_name = row["dept_name"].ToString();

                        dh.plant_code = plantCode;         // BhaveshT
                        dh.dept_name = deptName;

                        using (var Client = new HttpClient())
                        {
                            var Jsonobj = Newtonsoft.Json.JsonConvert.SerializeObject(dh);
                            var jsoninput = new StringContent(Jsonobj, System.Text.Encoding.UTF8, "application/json");

                            var postTask = Client.PostAsync(apiUrl, jsoninput); //to change api here                                                                                                                                                          

                            postTask.Wait();
                            var result = postTask.Result;
                            // Check if the request was successful (status code 200-299)
                            if (result.IsSuccessStatusCode)
                            {
                                //for cheking errors response

                                string jsonResponse = await result.Content.ReadAsStringAsync();
                                if (jsonResponse.Contains("Client Header Table Data Stored Into The Master Table As Well As Transaction Table"))
                                {
                                    clsFunctions_comman.ErrorLog("DataHeader: Data Stored Successfully to server");
                                    clsFunctions_comman.UniBox("DataHeader: Data Stored Successfully to server");
                                }
                                if (jsonResponse.Contains("Client Header Table Data Already Present In Master Table Now Update Into Transaction Table"))
                                {
                                    clsFunctions.AdoData_setup("Update DataHeaderTableSync set DataHeaderUpload=1  where Info='Fields' and Type='Client' and plant_code='" + plantCode + "' and dept_name='" + deptName + "' ");
                                    string inform = "Uploaded: Info='Fields' and Type='Client' and plant_code='" + plantCode + "' and dept_name='" + deptName + "' and Software Version='" + dh.software_version + "'";
                                    WriteToTXTfile(inform, "DataHeaderUpload");

                                    clsFunctions_comman.ErrorLog("DataHeader: Data updated Successfully to server");
                                    clsFunctions_comman.UniBox("DataHeader: Data updated Successfully to server");
                                }

                                if (jsonResponse.Contains("Client Header Table Data Stored Into The Master Table But Not In Transaction Table"))
                                {
                                    clsFunctions_comman.ErrorLog("DataHeader: Data store in Header table but not in Transaction table of server");
                                    clsFunctions_comman.UniBox("DataHeader: Data store in Header table but not in Transaction table of server");
                                }

                                if (jsonResponse.Contains("Client Header Table Data Not Insert/Update"))
                                {
                                    clsFunctions_comman.ErrorLog("DataHeader: Header Table Data Not got Inserted/Updated on server.");
                                    clsFunctions_comman.UniBox("DataHeader: Header Table Data Not got Inserted/Updated on server.");
                                }
                            }
                            else
                            {
                                clsFunctions_comman.ErrorLog("UploadDataHeaderTable - Upload Unsuccessful.");
                                clsFunctions_comman.UniBox("UploadDataHeaderTable - Upload Unsuccessful.");
                            }
                        }
                    }
                }
                else
                {
                    clsFunctions_comman.ErrorLog("There is no data in DataHeaderTable to upload on server.");
                    clsFunctions_comman.UniBox("There is no data in DataHeaderTable to upload on server.");
                }
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    // Log inner exception
                    clsFunctions_comman.ErrorLog("Inner Exception: " + innerException.Message);

                    // Move to the next inner exception
                    innerException = innerException.InnerException;
                }
                clsFunctions_comman.ErrorLog("at UploadDataHeaderTableSync(): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 21/06/2024 : BhaveshT - Modified parameter names and method.

        public static async void UploadDataTransactionTableSync(string aliasName, string deptName, string plantCode)
        {
            try
            {
                string deptType = "";

                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='" + aliasName + "'") + clsFunctions.loadSingleValueSetup("Select UploadDataTransactionTableSync from ServerMapping where AliasName='" + aliasName + "'").Trim();
                ///Mother_API/DataHeaderTableSync/header_data_from_plant_code_dept_name?plant_code=p_code&dept_name=d_type
                ///
                //string plantcode = clsFunctions.loadSingleValueSetup("Select PlantCode from ServerMapping where Flag='Y'");
                //string deptName = clsFunctions.loadSingleValueSetup("Select deptname from ServerMapping where Flag='Y'");

                //DataTable dt = fillDatatable_setup("Select * from DataTransactionTableSync where Info='Fields' and Type='Client' and plant_code='" + plantCode + "' and dept_name='" + deptName + "' and DataHeaderUpload=0");
                DataTable dt = fillDatatable_setup("Select * from DataTransactionTableSync where Info='Fields' and Type='Client' and Flag='Y'");         // 22/06/2024 : BhaveshT

                DataTransaction dtransaction = new DataTransaction();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dtransaction.software_version = row["SoftwareVersion"].ToString();
                        dtransaction.info = row["Info"].ToString();
                        dtransaction.batch_no = row["Batch_No"].ToString();
                        dtransaction.batch_index = row["Batch_Index"].ToString();
                        dtransaction.batch_date = row["Batch_Date"].ToString();
                        dtransaction.batch_time = row["Batch_Time"].ToString();
                        dtransaction.batch_time_text = row["Batch_Time_Text"].ToString();
                        dtransaction.batch_year = row["Batch_Year"].ToString();
                        dtransaction.consistancy = row["Consistancy"].ToString();
                        dtransaction.production_qty = row["Production_Qty"].ToString();
                        dtransaction.ordered_qty = row["Ordered_Qty"].ToString();
                        dtransaction.returned_qty = row["Returned_Qty"].ToString();
                        dtransaction.withthisload = row["WithThisLoad"].ToString();
                        dtransaction.batch_size = row["Batch_Size"].ToString();
                        dtransaction.gate1_actual = row["Gate1_Actual"].ToString();
                        dtransaction.gate1_target = row["Gate1_Target"].ToString();
                        dtransaction.gate1_moisture = row["Gate1_Moisture"].ToString();
                        dtransaction.gate2_actual = row["Gate2_Actual"].ToString();
                        dtransaction.gate2_target = row["Gate2_Target"].ToString();
                        dtransaction.gate2_moisture = row["Gate2_Moisture"].ToString();
                        dtransaction.gate3_actual = row["Gate3_Actual"].ToString();
                        dtransaction.gate3_target = row["Gate3_Target"].ToString();
                        dtransaction.gate3_moisture = row["Gate3_Moisture"].ToString();
                        dtransaction.gate4_actual = row["Gate4_Actual"].ToString();
                        dtransaction.gate4_target = row["Gate4_Target"].ToString();
                        dtransaction.gate4_moisture = row["Gate4_Moisture"].ToString();
                        dtransaction.gate5_actual = row["Gate5_Actual"].ToString();
                        dtransaction.gate5_target = row["Gate5_Target"].ToString();
                        dtransaction.gate5_moisture = row["Gate5_Moisture"].ToString();
                        dtransaction.gate6_actual = row["Gate6_Actual"].ToString();
                        dtransaction.gate6_target = row["Gate6_Target"].ToString();
                        dtransaction.gate6_moisture = row["Gate6_Moisture"].ToString();
                        dtransaction.cement1_actual = row["Cement1_Actual"].ToString();
                        dtransaction.cement1_target = row["Cement1_Target"].ToString();
                        dtransaction.cement1_correction = row["Cement1_Correction"].ToString();
                        dtransaction.cement2_actual = row["Cement2_Actual"].ToString();
                        dtransaction.cement2_target = row["Cement2_Target"].ToString();
                        dtransaction.cement2_correction = row["Cement2_Correction"].ToString();
                        dtransaction.cement3_actual = row["Cement3_Actual"].ToString();
                        dtransaction.cement3_target = row["Cement3_Target"].ToString();
                        dtransaction.cement3_correction = row["Cement3_Correction"].ToString();
                        dtransaction.cement4_actual = row["Cement4_Actual"].ToString();
                        dtransaction.cement4_target = row["Cement4_Target"].ToString();
                        dtransaction.cement4_correction = row["Cement4_Correction"].ToString();
                        dtransaction.filler1_actual = row["Filler1_Actual"].ToString();
                        dtransaction.filler1_target = row["Filler1_Target"].ToString();
                        dtransaction.filler1_correction = row["Filler1_Correction"].ToString();
                        dtransaction.water1_actual = row["Water1_Actual"].ToString();
                        dtransaction.water1_target = row["Water1_Target"].ToString();
                        dtransaction.water1_correction = row["Water1_Correction"].ToString();
                        dtransaction.water2_actual = row["Water2_Actual"].ToString();
                        dtransaction.water2_target = row["Water2_Target"].ToString();
                        dtransaction.water2_correction = row["Water2_Correction"].ToString();
                        dtransaction.silica_actual = row["Silica_Actual"].ToString();
                        dtransaction.silica_target = row["Silica_Target"].ToString();
                        dtransaction.silica_correction = row["Silica_Correction"].ToString();
                        dtransaction.slurry_actual = row["Slurry_Actual"].ToString();
                        dtransaction.slurry_target = row["Slurry_Target"].ToString();
                        dtransaction.slurry_correction = row["Slurry_Correction"].ToString();
                        dtransaction.adm1_actual1 = row["Adm1_Actual1"].ToString();
                        dtransaction.adm1_target1 = row["Adm1_Target1"].ToString();
                        dtransaction.adm1_correction1 = row["Adm1_Correction1"].ToString();
                        dtransaction.adm1_actual2 = row["Adm1_Actual2"].ToString();
                        dtransaction.adm1_target2 = row["Adm1_Target2"].ToString();
                        dtransaction.adm1_correction2 = row["Adm1_Correction2"].ToString();
                        dtransaction.adm2_actual1 = row["Adm2_Actual1"].ToString();
                        dtransaction.adm2_target1 = row["Adm2_Target1"].ToString();
                        dtransaction.adm2_correction1 = row["Adm2_Correction1"].ToString();
                        dtransaction.adm2_actual2 = row["Adm2_Actual2"].ToString();
                        dtransaction.adm2_target2 = row["Adm2_Target2"].ToString();
                        dtransaction.adm2_correction2 = row["Adm2_Correction2"].ToString();
                        dtransaction.pigment_actual = row["Pigment_Actual"].ToString();
                        dtransaction.pigment_target = row["Pigment_Target"].ToString();
                        dtransaction.plant_no = row["Plant_No"].ToString();
                        dtransaction.balance_wtr = row["Balance_Wtr"].ToString();
                        dtransaction.t_upload1 = row["TUpload1"].ToString();
                        dtransaction.t_upload2 = row["TUpload2"].ToString();
                        dtransaction.type = row["Type"].ToString();
                        dtransaction.table_name = row["TableName"].ToString();
                        dtransaction.flag = row["Flag"].ToString();
                        dtransaction.query1 = row["Query1"].ToString();

                        //dtransaction.plant_code = row["Plant_Code"].ToString();
                        //dtransaction.dept_name = row["Dept_Name"].ToString();

                        dtransaction.plant_code = plantCode;         // BhaveshT
                        dtransaction.dept_name = deptName;

                        using (var Client = new HttpClient())
                        {
                            var Jsonobj = Newtonsoft.Json.JsonConvert.SerializeObject(dtransaction);
                            var jsoninput = new StringContent(Jsonobj, System.Text.Encoding.UTF8, "application/json");

                            var postTask = Client.PostAsync(apiUrl, jsoninput); //to change api here                                                                                                                                                          

                            postTask.Wait();
                            var result = postTask.Result;
                            // Check if the request was successful (status code 200-299)
                            if (result.IsSuccessStatusCode)
                            {
                                //for cheking errors response

                                string jsonResponse = await result.Content.ReadAsStringAsync();
                                if (jsonResponse.Contains("Client Transaction Table Data Stored Into The Master Table As Well As Transaction Table"))
                                {
                                    clsFunctions_comman.ErrorLog("DataTransaction: Data Stored Successfully to server");
                                    clsFunctions_comman.UniBox("DataTransaction: Data Stored Successfully to server");
                                }
                                if (jsonResponse.Contains("Client Transaction Table Data Already Present In Master Table Now Update Into Transaction Table"))
                                {
                                    clsFunctions.AdoData_setup("Update DataTransactionTableSync set DataHeaderUpload=1  where Info='Fields' and Type='Client' and plant_code='" + plantCode + "' and dept_name='" + deptName + "' ");
                                    string inform = "Uploaded : Info='Fields' and Type='Client' and plant_code='" + plantCode + "' and dept_name='" + deptName + "' and Software Version= '" + dtransaction.software_version + "'";
                                    WriteToTXTfile(inform, "DataTransactionUpload");

                                    clsFunctions_comman.ErrorLog("DataTransaction: Data updated Successfully to server");
                                    clsFunctions_comman.UniBox("DataTransaction: Data updated Successfully to server");
                                }

                                if (jsonResponse.Contains("Client Transaction Table Data Stored Into The Master Table But Not In Transaction Table"))
                                {
                                    clsFunctions_comman.ErrorLog("DataTransaction: Data store in Master table but not in Transaction table of server");
                                    clsFunctions_comman.UniBox("DataTransaction: Data store in Master table but not in Transaction table of server");
                                }

                                if (jsonResponse.Contains("Client Transaction Table Data Not Insert/Update"))
                                {
                                    clsFunctions_comman.ErrorLog("DataTransaction: Transaction Table Data Not got Inserted/Updated on server.");
                                    clsFunctions_comman.UniBox("DataTransaction: Transaction Table Data Not got Inserted/Updated on server.");
                                }
                            }
                            else
                            {
                                clsFunctions_comman.ErrorLog("UploadDataTransactionTableSync - Upload Unsuccessful.");
                                clsFunctions_comman.UniBox("UploadDataTransactionTableSync - Upload Unsuccessful.");
                            }
                        }
                    }
                }
                else
                {
                    clsFunctions_comman.ErrorLog("There is no data in DataTransactionTable to upload on server.");
                    clsFunctions_comman.UniBox("There is no data in DataTransactionTable to upload on server.");
                }
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    // Log inner exception
                    clsFunctions_comman.ErrorLog("Inner Exception: " + innerException.Message);

                    // Move to the next inner exception
                    innerException = innerException.InnerException;
                }
                clsFunctions_comman.ErrorLog("at UploadDataTransactionTableSync(): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 20/06/2024 : BhaveshT
        // GetAndUpdateServerMappingDataFromAPI(aliasName, dept) - This method gets serverMapping data from MotherAPI against aliasName and update it into ServerMapping & ServerMapping_Preset tables of setup DB.

        public static int GetAndUpdateServerMappingDataFromAPI(string aliasName, string dept)
        {
            try
            {
                string plantType, query1, query2 = "";

                string domain = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='" + aliasName + "'");

                string apiUrl = domain + "/Mother_API/ServerMapping_Preset/get_server_mapping_against_dept_name_plant_type?dept_name=d_type&plant_type=p_type";       // For LIVE

                //string apiUrl = "http://192.168.1.13:8089/Mother_API/ServerMapping_Preset/get_server_mapping_against_dept_name_plant_type?dept_name=d_type&plant_type=p_type";      // for TESTING

                if (aliasName.Contains("RMC"))
                    plantType = "RMC";
                else
                    plantType = "BT";

                apiUrl = apiUrl.Replace("d_type", dept);
                apiUrl = apiUrl.Replace("p_type", plantType);
                apiUrl = apiUrl.Replace("\"", "");

                if (apiUrl != "")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = client.GetAsync(apiUrl).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                                if (jsonResponse == "{\"ServerMapping_Preset\":null}")
                                {
                                    MessageBox.Show("At getPresetDataForApis(): for fetching servermapping preset data, the jsonResponse object : " + jsonResponse);
                                    clsFunctions.ErrorLog("At getPresetDataForApis(): for fetching servermapping preset data, the jsonResponse object : " + jsonResponse);
                                    return 0;
                                }

                                JObject jsonObject = JObject.Parse(jsonResponse);
                                var preset = jsonObject["ServerMapping_Preset"];

                                query1 = $@"UPDATE ServerMapping SET 
                                    SrNo = {preset["SrNo"]},
                                    AliasName = '{preset["AliasName"]}', 
                                    Note1 = '{preset["Note1"]}', 
                                    ipaddress = '{preset["ipaddress"]}', 
                                    portno = '{preset["portno"]}', 
                                    BT_API = '{preset["BT_API"]}', 
                                    RMC_Trans_API = '{preset["RMC_Trans_API"]}', 
                                    RMC_Transaction_API = '{preset["RMC_Transaction_API"]}', 
                                    Software_Status_API = '{preset["Software_Status_API"]}', 
                                    plantExpiry = '{preset["plantExpiry"]}', 
                                    deptname = '{preset["deptname"]}', 
                                    PlantType = '{preset["PlantType"]}', 
                                    Note2 = '{preset["Note2"]}', 
                                    ipaddress1 = '{preset["ipaddress1"]}', 
                                    port1 = '{preset["port1"]}', 
                                    BT_API1 = '{preset["BT_API1"]}', 
                                    RMC_Transaction_API1 = '{preset["RMC_Transaction_API1"]}', 
                                    RMC_Trans_API1 = '{preset["RMC_Trans_API1"]}', 
                                    AutoReg_SMS = '{preset["AutoReg_SMS"]}', 
                                    AutoReg_Verify = '{preset["AutoReg_Verify"]}', 
                                    AutoReg_Save = '{preset["AutoReg_Save"]}', 
                                    GetWO = '{preset["GetWO"]}', 
                                    GetAllWO = '{preset["GetAllWO"]}', 
                                    AllocateWO = '{preset["AllocateWO"]}', 
                                    GetPlantDetails = '{preset["GetPlantDetails"]}', 
                                    GetMobNoFromWO = '{preset["GetMobNoFromWO"]}', 
                                    GetProdErrorTemplate = '{preset["GetProdErrorTemplate"]}', 
                                    sendSMS = '{preset["sendSMS"]}', 
                                    getInstallDetails = '{preset["getInstallDetails"]}', 
                                    GetDataHeaderTableSync = '{preset["GetDataHeaderTableSync"]}', 
                                    UploadDataHeaderTableSync = '{preset["UploadDataHeaderTableSync"]}', 
                                    GetDataTransactionTableSync = '{preset["GetDataTransactionTableSync"]}', 
                                    UploadDataTransactionTableSync = '{preset["UploadDataTransactionTableSync"]}', 
                                    ServerMapping_Preset = '{preset["ServerMapping_Preset"]}', 
                                    Unipro_Setup = '{preset["Unipro_Setup"]}', 
                                    PlantSetup = '{preset["PlantSetup"]}', 
                                    Plant_LiveStatus_History = '{preset["Plant_LiveStatus_History"]}', 
                                    m_SaveInstall = '{preset["m_SaveInstall"]}', 
                                    m_latestUp_Insert = '{preset["m_latestUp_Insert"]}', 
                                    m_latestUp_Get = '{preset["m_latestUp_Get"]}', 
                                    Upload_UniproSetupID = '{preset["Upload_UniproSetupID"]}', 
                                    sendRecipe_API = '{preset["sendRecipe_API"]}',
                                    Post_SiteName = '{preset["Post_SiteName"]}',
                                    ipaddress2 = '{preset["ipaddress2"]}',
                                    BT_API2 = '{preset["BT_API2"]}',
                                    RMC_Trans_API2 = '{preset["RMC_Trans_API2"]}',
                                    RMC_Transaction_API2 = '{preset["RMC_Transaction_API2"]}'
                                    WHERE AliasName = '{aliasName}' AND deptname = '{dept}'";

                                query2 = $@"UPDATE ServerMapping_Preset SET 
                                    SrNo = {preset["SrNo"]},
                                    AliasName = '{preset["AliasName"]}', 
                                    Note1 = '{preset["Note1"]}', 
                                    ipaddress = '{preset["ipaddress"]}', 
                                    portno = '{preset["portno"]}', 
                                    BT_API = '{preset["BT_API"]}', 
                                    RMC_Trans_API = '{preset["RMC_Trans_API"]}', 
                                    RMC_Transaction_API = '{preset["RMC_Transaction_API"]}', 
                                    Software_Status_API = '{preset["Software_Status_API"]}', 
                                    plantExpiry = '{preset["plantExpiry"]}', 
                                    deptname = '{preset["deptname"]}', 
                                    PlantType = '{preset["PlantType"]}', 
                                    Note2 = '{preset["Note2"]}', 
                                    ipaddress1 = '{preset["ipaddress1"]}', 
                                    port1 = '{preset["port1"]}', 
                                    BT_API1 = '{preset["BT_API1"]}', 
                                    RMC_Transaction_API1 = '{preset["RMC_Transaction_API1"]}', 
                                    RMC_Trans_API1 = '{preset["RMC_Trans_API1"]}', 
                                    AutoReg_SMS = '{preset["AutoReg_SMS"]}', 
                                    AutoReg_Verify = '{preset["AutoReg_Verify"]}', 
                                    AutoReg_Save = '{preset["AutoReg_Save"]}', 
                                    GetWO = '{preset["GetWO"]}', 
                                    GetAllWO = '{preset["GetAllWO"]}', 
                                    AllocateWO = '{preset["AllocateWO"]}', 
                                    GetPlantDetails = '{preset["GetPlantDetails"]}', 
                                    GetMobNoFromWO = '{preset["GetMobNoFromWO"]}', 
                                    GetProdErrorTemplate = '{preset["GetProdErrorTemplate"]}', 
                                    sendSMS = '{preset["sendSMS"]}', 
                                    getInstallDetails = '{preset["getInstallDetails"]}', 
                                    GetDataHeaderTableSync = '{preset["GetDataHeaderTableSync"]}', 
                                    UploadDataHeaderTableSync = '{preset["UploadDataHeaderTableSync"]}', 
                                    GetDataTransactionTableSync = '{preset["GetDataTransactionTableSync"]}', 
                                    UploadDataTransactionTableSync = '{preset["UploadDataTransactionTableSync"]}', 
                                    ServerMapping_Preset = '{preset["ServerMapping_Preset"]}', 
                                    Unipro_Setup = '{preset["Unipro_Setup"]}', 
                                    PlantSetup = '{preset["PlantSetup"]}', 
                                    Plant_LiveStatus_History = '{preset["Plant_LiveStatus_History"]}', 
                                    m_SaveInstall = '{preset["m_SaveInstall"]}', 
                                    m_latestUp_Insert = '{preset["m_latestUp_Insert"]}', 
                                    m_latestUp_Get = '{preset["m_latestUp_Get"]}', 
                                    Upload_UniproSetupID = '{preset["Upload_UniproSetupID"]}' ,
                                    sendRecipe_API = '{preset["sendRecipe_API"]}',
                                    Post_SiteName = '{preset["Post_SiteName"]}',
                                    ipaddress2 = '{preset["ipaddress2"]}',
                                    BT_API2 = '{preset["BT_API2"]}',
                                    RMC_Trans_API2 = '{preset["RMC_Trans_API2"]}',
                                    RMC_Transaction_API2 = '{preset["RMC_Transaction_API2"]}'
                                    WHERE AliasName = '{aliasName}' AND deptname = '{dept}'";

                                int result1 = clsFunctions.AdoData_setup(query1);
                                int result2 = clsFunctions.AdoData_setup(query2);

                                if (result1 == 1 && result1 == 1)
                                {
                                    clsFunctions_comman.ErrorLog("ServerMapping & ServerMapping_Preset data updated.");
                                    return 1;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                clsFunctions_comman.UniBox("GetAndUpdateServerMappingDataFromAPI - Update Unsuccessful.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please see for the api because api couldn't be loaded for fetching ServerMapping_Preset api.");
                }
                return 1;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("at GetAndUpdateServerMappingDataFromAPI(): " + ex.Message);
                return 0;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 21/06/2024 : BhaveshT
        // GetAndInsertServerMappingDataFromAPI(aliasName, dept) - This method gets serverMapping data from MotherAPI against aliasName and inserts it into ServerMapping & ServerMapping_Preset tables of setup DB.

        public static int GetAndInsertServerMappingDataFromAPI(string aliasName, string deptType, string plantType)
        {
            try
            {
                string query = "";

                string domain = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='" + aliasName + "'");

                // For LIVE
                string apiUrl = domain + "/Mother_API/ServerMapping_Preset/get_server_mapping_against_dept_name_plant_type?dept_name=d_type&plant_type=p_type";

                // for TESTING
                //string apiUrl = "http://192.168.1.13:8089/Mother_API/ServerMapping_Preset/get_server_mapping_against_dept_name_plant_type?dept_name=d_type&plant_type=p_type";      

                apiUrl = apiUrl.Replace("d_type", deptType);
                apiUrl = apiUrl.Replace("p_type", plantType);
                apiUrl = apiUrl.Replace("\"", "");

                //apiUrl = "http://192.168.1.13:8089/Mother_API/ServerMapping_Preset/get_server_mapping_against_dept_name_plant_type?dept_name=PWD&plant_type=BT"; //for reff.

                if (apiUrl != "")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = client.GetAsync(apiUrl).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonResponse = response.Content.ReadAsStringAsync().Result; // Blocking call

                                if (jsonResponse == "{\"ServerMapping_Preset\":null}")
                                {
                                    MessageBox.Show("At getPresetDataForApis(): for fetching servermapping preset data, the jsonResponse object : " + jsonResponse);
                                    clsFunctions.ErrorLog("At getPresetDataForApis(): for fetching servermapping preset data, the jsonResponse object : " + jsonResponse);
                                    return 0;
                                }

                                JObject jsonObject = JObject.Parse(jsonResponse);
                                var preset = jsonObject["ServerMapping_Preset"];

                                query = $@"INSERT INTO ServerMapping_Preset (SrNo, AliasName, Note1, ipaddress, portno, BT_API, RMC_Trans_API, RMC_Transaction_API, Software_Status_API, plantExpiry, deptname, PlantType, Note2, 
                                        ipaddress1, port1, BT_API1, RMC_Transaction_API1, RMC_Trans_API1, AutoReg_SMS, AutoReg_Verify, AutoReg_Save, GetWO, GetAllWO, AllocateWO, GetPlantDetails, GetMobNoFromWO, GetProdErrorTemplate, sendSMS, getInstallDetails, DPTStatus, Flag, DeviceID, 
                                        PlantCode, GetDataHeaderTableSync,UploadDataHeaderTableSync, GetDataTransactionTableSync,UploadDataTransactionTableSync, ServerMapping_Preset, Unipro_Setup, PlantSetup, Plant_LiveStatus_History, m_SaveInstall, m_latestUp_Insert, m_latestUp_Get,Upload_UniproSetupID, sendRecipe_API,
                                        Post_SiteName, ipaddress2, BT_API2, RMC_Trans_API2, RMC_Transaction_API2) 
                                     VALUES ({preset["SrNo"]}, '{preset["AliasName"]}', '{preset["Note1"]}', '{preset["ipaddress"]}', '{preset["portno"]}', '{preset["BT_API"]}', '{preset["RMC_Trans_API"]}', '{preset["RMC_Transaction_API"]}', '{preset["Software_Status_API"]}', '{preset["plantExpiry"]}', 
                                            '{preset["deptname"]}', '{preset["PlantType"]}', '{preset["Note2"]}', '{preset["ipaddress1"]}', '{preset["port1"]}', '{preset["BT_API1"]}', '{preset["RMC_Transaction_API1"]}', '{preset["RMC_Trans_API1"]}', '{preset["AutoReg_SMS"]}', '{preset["AutoReg_Verify"]}', 
                                            '{preset["AutoReg_Save"]}', '{preset["GetWO"]}', '{preset["GetAllWO"]}', '{preset["AllocateWO"]}', '{preset["GetPlantDetails"]}', '{preset["GetMobNoFromWO"]}', '{preset["GetProdErrorTemplate"]}', '{preset["sendSMS"]}', '{preset["getInstallDetails"]}', '{preset["DPTStatus"]}', 
                                            '{preset["Flag"]}', '{preset["DeviceID"]}', '{preset["PlantCode"]}', '{preset["GetDataHeaderTableSync"]}', '{preset["UploadDataHeaderTableSync"]}', '{preset["GetDataTransactionTableSync"]}', '{preset["UploadDataTransactionTableSync"]}', '{preset["ServerMapping_Preset"]}', 
                                            '{preset["Unipro_Setup"]}', '{preset["PlantSetup"]}', '{preset["Plant_LiveStatus_History"]}', '{preset["m_SaveInstall"]}', '{preset["m_latestUp_Insert"]}', '{preset["m_latestUp_Get"]}', '{preset["Upload_UniproSetupID"]}', '{preset["sendRecipe_API"]}', '{preset["Post_SiteName"]}',
                                            '{preset["ipaddress2"]}', '{preset["BT_API2"]}', '{preset["RMC_Trans_API2"]}', '{preset["RMC_Transaction_API2"]}')";

                                int result = clsFunctions.AdoData_setup(query);
                                return result;
                            }
                            else
                            {
                                clsFunctions_comman.UniBox("GetAndInsertServerMappingDataFromAPI - Insert Unsuccessful.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please see for the api becuase api couldn't be loaded for fetching ServerMapping_Preset api.");
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 20/06/2024 : BhaveshT
        // GetAndUpdateUniproSetupFromAPI(aliasName, dept) - This method gets Unipro_Setup data from MotherAPI against dept & plantCode and update it into Unipro_Setup table of setup DB.

        public static int GetAndUpdateUniproSetupFromAPI(string aliasName, string dept, string plantCode)
        {
            string query = "";
            try
            {
                string plantType = "";
                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='" + aliasName + "'") + clsFunctions.loadSingleValueSetup("Select Unipro_Setup from ServerMapping_Preset where AliasName='" + aliasName + "'").Trim();

                if (aliasName == "PWD - BT")
                {
                    apiUrl = apiUrl.Replace("d_type", "VIPL");
                }
                else
                {
                    apiUrl = apiUrl.Replace("d_type", dept);
                }

                apiUrl = apiUrl.Replace("\"", "");
                apiUrl = apiUrl.Replace("p_type", plantCode);

                if (apiUrl != "")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = client.GetAsync(apiUrl).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonResponse = response.Content.ReadAsStringAsync().Result; // Blocking call
                                JObject jsonObject = JObject.Parse(jsonResponse);
                                if (jsonResponse == "{\"Unipro_Setup_Data\":null}")
                                {
                                    //MessageBox.Show("At getpresetDataForUniPro_Setup():for fetching UniPro_Setup data, the jsonResponse object : " + jsonResponse);
                                    clsFunctions.ErrorLog("At getpresetDataForUniPro_Setup():for fetching UniPro_Setup data, the jsonResponse object : " + jsonResponse);
                                    return 2;
                                }
                                // Insert Unipro_Setup_Data
                                var uniproSetup = jsonObject["Unipro_Setup_Data"];
                                if (uniproSetup != null)
                                {
                                    clsFunctions.AdoData_setup("Delete * from Unipro_Setup");

                                    query = $@"INSERT INTO Unipro_setup (UniproSetupID, Plant_Make, FormName, UploaderName, Path, Pass, Description, DB_Type, PlantType, ImagePath, UILocation, ImageUsed, RecipeFormName, ConnectionString, BatchReport_FileName, DC_FileName, Status) 
                                            VALUES ({uniproSetup["UniproSetupID"]}, '{uniproSetup["Plant_Make"]}', '{uniproSetup["FormName"]}', '{uniproSetup["UploaderName"]}', '{uniproSetup["Path"]}', '{uniproSetup["Pass"]}', '{uniproSetup["Description"]}', '{uniproSetup["DB_Type"]}', '{uniproSetup["PlantType"]}', 
                                                   '{uniproSetup["ImagePath"]}', '{uniproSetup["UILocation"]}', '{uniproSetup["ImageUsed"]}', '{uniproSetup["RecipeFormName"]}', '{uniproSetup["ConnectionString"]}', '{uniproSetup["BatchReport_FileName"]}', '{uniproSetup["DC_FileName"]}', 'Y')";

                                    return clsFunctions.AdoData_setup(query);

                                }
                            }
                            else
                            {
                                clsFunctions_comman.UniBox("GetAndUpdateUniproSetupFromAPI - Update Unsuccessful.");
                                clsFunctions_comman.ErrorLog("GetAndUpdateUniproSetupFromAPI - Update Unsuccessful.");
                                return 0;
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("at GetAndUpdateUniproSetupFromAPI(): " + ex.Message);
                return 0;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 20/06/2024 : BhaveshT

        public static async void GetDataHeaderTableSync(string aliasName, string dept, string plantCode)
        {
            try
            {
                string deptType = "";

                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='" + aliasName + "'") + clsFunctions.loadSingleValueSetup("Select GetDataHeaderTableSync from ServerMapping_Preset where AliasName='" + aliasName + "'").Trim();
                ///Mother_API/DataHeaderTableSync/header_data_from_plant_code_dept_name?plant_code=p_code&dept_name=d_type

                apiUrl = apiUrl.Replace("deptName", dept);
                apiUrl = apiUrl.Replace("plantCode", plantCode);
                apiUrl = apiUrl.Replace("\"", "");

                if (apiUrl != "")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        response.EnsureSuccessStatusCode();

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject jsonObject = JObject.Parse(jsonResponse);

                        // Insert into DataHeaderTableSync table
                        var dataHeaders = jsonObject["DataHeaderTableSync"];
                        clsFunctions.AdoData_setup("Delete from DataHeaderTableSync");

                        int headerResult = 0;
                        //clsFunctions.AdoData_setup("Delete from DataHeaderTableSync where Info='Fields' and Type='Client' and plant_code='"+ lb_PlantCode + "' and dept_name='"+ department + "' ");
                        foreach (var header in dataHeaders)
                        {
                            string headerQuery = $@"INSERT INTO DataHeaderTableSync (SoftwareVersion, Info, Batch_No, Batch_Date, Batch_Time, Batch_Time_Text, Batch_Start_Time, Batch_End_Time, Batch_Year, Batcher_Name, Batcher_User_Level, 
                                   Customer_Code, Recipe_Code, Recipe_Name, Mixing_Time, Mixer_Capacity, strength, Site, Truck_No, Truck_Driver, Production_Qty, Ordered_Qty, Returned_Qty, WithThisLoad, Batch_Size, Order_No, Schedule_Id, 
                                   Gate1_Target, Gate2_Target, Gate3_Target, Gate4_Target, Gate5_Target, Gate6_Target, Cement1_Target, Cement2_Target, Cement3_Target, Cement4_Target, Filler_Target, Water1_Target, slurry_Target, Water2_Target, 
                                   Silica_Target, Adm1_Target1, Adm1_Target2, Adm2_Target1, Adm2_Target2, Cost_Per_Mtr_Cube, Total_Cost, Plant_No, Weighed_Net_Weight, Weigh_Bridge_Stat, tExportStatus, tUpload1, tUpload2, WO_Code, Site_ID, Cust_Name, Type, TableName, Flag,plant_code,dept_name) 
                                VALUES ('{header["SoftwareVersion"]}', '{header["Info"]}', '{header["Batch_No"]}', '{header["Batch_Date"]}', '{header["Batch_Time"]}', '{header["Batch_Time_Text"]}', '{header["Batch_Start_Time"]}', '{header["Batch_End_Time"]}', '{header["Batch_Year"]}', '{header["Batcher_Name"]}', '{header["Batcher_User_Level"]}', 
                                        '{header["Customer_Code"]}', '{header["Recipe_Code"]}', '{header["Recipe_Name"]}', '{header["Mixing_Time"]}', '{header["Mixer_Capacity"]}', '{header["strength"]}', '{header["Site"]}', '{header["Truck_No"]}', '{header["Truck_Driver"]}', '{header["Production_Qty"]}', '{header["Ordered_Qty"]}', 
                                        '{header["Returned_Qty"]}', '{header["WithThisLoad"]}', '{header["Batch_Size"]}', '{header["Order_No"]}', '{header["Schedule_Id"]}', '{header["Gate1_Target"]}', '{header["Gate2_Target"]}', '{header["Gate3_Target"]}', '{header["Gate4_Target"]}', '{header["Gate5_Target"]}', '{header["Gate6_Target"]}', 
                                        '{header["Cement1_Target"]}', '{header["Cement2_Target"]}', '{header["Cement3_Target"]}', '{header["Cement4_Target"]}', '{header["Filler_Target"]}', '{header["Water1_Target"]}', '{header["slurry_Target"]}', '{header["Water2_Target"]}', '{header["Silica_Target"]}', '{header["Adm1_Target1"]}', 
                                        '{header["Adm1_Target2"]}', '{header["Adm2_Target1"]}', '{header["Adm2_Target2"]}', '{header["Cost_Per_Mtr_Cube"]}', '{header["Total_Cost"]}', '{header["Plant_No"]}', '{header["Weighed_Net_Weight"]}', '{header["Weigh_Bridge_Stat"]}', '{header["tExportStatus"]}', '{header["tUpload1"]}', '{header["tUpload2"]}',
                                        '{header["WO_Code"]}', '{header["Site_ID"]}', '{header["Cust_Name"]}', '{header["Type"]}', '{header["TableName"]}', '{header["Flag"]}','{header["plant_code"]}', '{header["dept_name"]}')";

                            headerResult = clsFunctions.AdoData_setup(headerQuery);

                        }
                        if (headerResult > 0)
                        {
                            clsFunctions_comman.UniBox("DataHeaderTableSync data inserted successfully.");
                        }
                        else
                        {
                            clsFunctions_comman.UniBox("DataHeaderTableSync data insertion failed.");
                        }
                    }
                }
                else
                {
                    clsFunctions_comman.ErrorLog("GetDataHeaderTableSync() - API URL is empty : apiUrl - " + apiUrl);
                    clsFunctions_comman.UniBox("GetDataHeaderTableSync() - API URL is empty : apiUrl - " + apiUrl);
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("at GetDataHeaderTableSync(): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 20/06/2024 : BhaveshT

        public static async void GetDataTransactionTableSync(string aliasName, string dept, string plantCode)
        {
            try
            {
                string deptType = "";
                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='" + aliasName + "'") + clsFunctions.loadSingleValueSetup("Select GetDataTransactionTableSync from ServerMapping_Preset where AliasName='" + aliasName + "'").Trim();
                ///Mother_API/DataHeaderTableSync/header_data_from_plant_code_dept_name?plant_code=p_code&dept_name=d_type

                apiUrl = apiUrl.Replace("deptName", dept);

                apiUrl = apiUrl.Replace("plantCode", plantCode);
                apiUrl = apiUrl.Replace("\"", "");
                if (apiUrl != "")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        response.EnsureSuccessStatusCode();

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        if (jsonResponse == "{\"DataTransactionTableSync\":[]}")
                        {
                            //MessageBox.Show("At getDataTransactionTableSync: for fetching DataTransactionTableSync preset data, the jsonResponse object : " + jsonResponse);
                            clsFunctions_comman.ErrorLog("At getDataTransactionTableSync: for fetching DataTransactionTableSync preset data, the jsonResponse object : " + jsonResponse);
                        }
                        JObject jsonObject = JObject.Parse(jsonResponse);

                        // Insert into DataTransactionTableSync table
                        var dataTransactions = jsonObject["DataTransactionTableSync"];
                        clsFunctions.AdoData_setup("Delete from DataTransactionTableSync");

                        int transactionResult = 0;

                        foreach (var transaction in dataTransactions)
                        {
                            string transactionQuery = $@"INSERT INTO DataTransactionTableSync (SoftwareVersion, Info, Batch_No, Batch_Index, Batch_Date, Batch_Time, Batch_Time_Text, Batch_Year, Consistancy, Production_Qty, Ordered_Qty, Returned_Qty, WithThisLoad, Batch_Size, 
                                    Gate1_Actual, Gate1_Target, Gate1_Moisture, Gate2_Actual, Gate2_Target, Gate2_Moisture, Gate3_Actual, Gate3_Target, Gate3_Moisture, Gate4_Actual, Gate4_Target, Gate4_Moisture, Gate5_Actual, Gate5_Target, Gate5_Moisture, Gate6_Actual, Gate6_Target, Gate6_Moisture, 
                                    Cement1_Actual, Cement1_Target, Cement1_Correction, Cement2_Actual, Cement2_Target, Cement2_Correction, Cement3_Actual, Cement3_Target, Cement3_Correction, Cement4_Actual, Cement4_Target, Cement4_Correction, Filler1_Actual, Filler1_Target, Filler1_Correction, 
                                    Water1_Actual, Water1_Target, Water1_Correction, Water2_Actual, Water2_Target, Water2_Correction, Silica_Actual, Silica_Target, Silica_Correction, Slurry_Actual, Slurry_Target, Slurry_Correction, Adm1_Actual1, Adm1_Target1, Adm1_Correction1, Adm1_Actual2, 
                                    Adm1_Target2, Adm1_Correction2, Adm2_Actual1, Adm2_Target1, Adm2_Correction1, Adm2_Actual2, Adm2_Target2, Adm2_Correction2, Pigment_Actual, Pigment_Target, Plant_No, Balance_Wtr, tUpload1, tUpload2, Type, TableName, Flag, Query1, plant_code, dept_name) 
                                VALUES ('{transaction["SoftwareVersion"]}', '{transaction["Info"]}', '{transaction["Batch_No"]}', '{transaction["Batch_Index"]}', '{transaction["Batch_Date"]}', '{transaction["Batch_Time"]}', '{transaction["Batch_Time_Text"]}', '{transaction["Batch_Year"]}', '{transaction["Consistancy"]}', 
                                        '{transaction["Production_Qty"]}', '{transaction["Ordered_Qty"]}', '{transaction["Returned_Qty"]}', '{transaction["WithThisLoad"]}', '{transaction["Batch_Size"]}', '{transaction["Gate1_Actual"]}', '{transaction["Gate1_Target"]}', '{transaction["Gate1_Moisture"]}', 
                                        '{transaction["Gate2_Actual"]}', '{transaction["Gate2_Target"]}', '{transaction["Gate2_Moisture"]}', '{transaction["Gate3_Actual"]}', '{transaction["Gate3_Target"]}', '{transaction["Gate3_Moisture"]}', '{transaction["Gate4_Actual"]}', '{transaction["Gate4_Target"]}',
                                        '{transaction["Gate4_Moisture"]}', '{transaction["Gate5_Actual"]}', '{transaction["Gate5_Target"]}', '{transaction["Gate5_Moisture"]}', '{transaction["Gate6_Actual"]}', '{transaction["Gate6_Target"]}', '{transaction["Gate6_Moisture"]}', '{transaction["Cement1_Actual"]}', 
                                        '{transaction["Cement1_Target"]}', '{transaction["Cement1_Correction"]}', '{transaction["Cement2_Actual"]}', '{transaction["Cement2_Target"]}', '{transaction["Cement2_Correction"]}', '{transaction["Cement3_Actual"]}', '{transaction["Cement3_Target"]}', '{transaction["Cement3_Correction"]}', 
                                        '{transaction["Cement4_Actual"]}', '{transaction["Cement4_Target"]}', '{transaction["Cement4_Correction"]}', '{transaction["Filler1_Actual"]}', '{transaction["Filler1_Target"]}', '{transaction["Filler1_Correction"]}', '{transaction["Water1_Actual"]}', '{transaction["Water1_Target"]}', 
                                        '{transaction["Water1_Correction"]}', '{transaction["Water2_Actual"]}', '{transaction["Water2_Target"]}', '{transaction["Water2_Correction"]}', '{transaction["Silica_Actual"]}', '{transaction["Silica_Target"]}', '{transaction["Silica_Correction"]}', '{transaction["Slurry_Actual"]}', 
                                        '{transaction["Slurry_Target"]}', '{transaction["Slurry_Correction"]}', '{transaction["Adm1_Actual1"]}', '{transaction["Adm1_Target1"]}', '{transaction["Adm1_Correction1"]}', '{transaction["Adm1_Actual2"]}', '{transaction["Adm1_Target2"]}', '{transaction["Adm1_Correction2"]}', 
                                        '{transaction["Adm2_Actual1"]}', '{transaction["Adm2_Target1"]}', '{transaction["Adm2_Correction1"]}', '{transaction["Adm2_Actual2"]}', '{transaction["Adm2_Target2"]}', '{transaction["Adm2_Correction2"]}', '{transaction["Pigment_Actual"]}', '{transaction["Pigment_Target"]}', 
                                        '{transaction["Plant_No"]}', '{transaction["Balance_Wtr"]}', '{transaction["tUpload1"]}', '{transaction["tUpload2"]}', '{transaction["Type"]}', '{transaction["TableName"]}', '{transaction["Flag"]}', '{transaction["Query1"]}', '{transaction["plant_code"]}', '{transaction["dept_name"]}')";


                            transactionResult = clsFunctions.AdoData_setup(transactionQuery);

                        }
                        if (transactionResult > 0)
                        {
                            clsFunctions_comman.UniBox("DataTransactionTableSync data inserted successfully.");
                            clsFunctions_comman.ErrorLog("DataTransactionTableSync data inserted successfully.");
                            AutoSetClientDB();
                        }
                        else
                        {
                            clsFunctions_comman.UniBox("DataTransactionTableSync data insertion failed.");
                            clsFunctions_comman.ErrorLog("DataTransactionTableSync data insertion failed.");
                        }
                    }
                }
                else
                {
                    clsFunctions_comman.UniBox("GetDataTransactionTableSync() - API URL is empty : apiUrl - " + apiUrl);
                    clsFunctions_comman.ErrorLog("GetDataTransactionTableSync() - API URL is empty : apiUrl - " + apiUrl);
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("at GetDataTransactionTableSync(): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 21/06/2024 : BhaveshT

        public static void FetchHeaderAndTransaction(string aliasName, string activeDeptName, string activePlantCode)
        {
            try
            {
                clsFunctions.GetDataHeaderTableSync(clsFunctions.aliasName, clsFunctions.activeDeptName, clsFunctions.activePlantCode);
                clsFunctions.GetDataTransactionTableSync(clsFunctions.aliasName, clsFunctions.activeDeptName, clsFunctions.activePlantCode);

            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("at FetchHeaderAndTransaction(): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 21/06/2024 : BhaveshT

        public static void UploadHeaderAndTransaction(string aliasName, string activeDeptName, string activePlantCode)
        {
            try
            {
                clsFunctions.UploadDataHeaderTableSync(clsFunctions.aliasName, clsFunctions.activeDeptName, clsFunctions.activePlantCode);
                clsFunctions.UploadDataTransactionTableSync(clsFunctions.aliasName, clsFunctions.activeDeptName, clsFunctions.activePlantCode);
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("at UploadHeaderAndTransaction(): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 01/07/2024 : BhaveshT - to insert data from list to column of table.

        public static void InsertDataToColumn(string[] dataList, string tableName, string columnName)
        {
            string query = "";
            try
            {
                if (tableName == "AliasName")
                {
                    foreach (var data in dataList)
                    {
                        // Create the SQL query for each alias name

                        query = $"INSERT INTO " + tableName + " (" + columnName + ", domain1) VALUES ('" + data + "', 'https://pmcscada.in') ;";
                        //query = $"INSERT INTO " + tableName + " (" + columnName + ", domain1) VALUES ('" + data + "', 'http://192.168.1.14:8080') ;";

                        // Call the AdoData_setup method to execute the query
                        int rowsAffected = clsFunctions.AdoData_setup(query);
                    }
                }
                else
                {
                    foreach (var data in dataList)
                    {
                        // Create the SQL query for each alias name
                        query = $"INSERT INTO " + tableName + " (" + columnName + ") VALUES ('" + data + "') ;";
                        // Call the AdoData_setup method to execute the query
                        int rowsAffected = clsFunctions.AdoData_setup(query);
                    }
                }

            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("Exception at InsertDataToColumn(): Query: " + query + " - " + ex.Message);
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string RemoveSpecialCharAndAlphabet(string input)
        {
            if (input == "000.")
                return "0";
            else
                // Use a regular expression to replace any character that is not a digit
                return Regex.Replace(input, "[^0-9.]", "");
        }

        internal static DataTable GetPLCTagInfo()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM [PLCTagsInfo]";

                var cmd = new OleDbCommand(query, con);
                var da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal static void InsertToPlctagInfoTable(DataTable objPLCTagsInfoData)
        {
            try
            {
                string insertSqlQuery = "";

                for (int i = 0; i < objPLCTagsInfoData.Rows.Count; i++)
                {
                    insertSqlQuery = "INSERT INTO [PLCTagsInfo] ";
                    insertSqlQuery += "( [TagID], [TagName], [TagAddress]," +
                                  "  [DisplayName], [TagDataType], [DB_Number], [StartAddress], [ByteLength], [BitAddress] " +
                                  ")";

                    insertSqlQuery += " VALUES ";
                    insertSqlQuery += "(";


                    for (int j = 0; j < objPLCTagsInfoData.Columns.Count; j++)
                    {
                        insertSqlQuery += "'" + objPLCTagsInfoData.Rows[i][j] + "',";
                    }

                    insertSqlQuery = insertSqlQuery.TrimEnd(',');
                    insertSqlQuery += ")";

                    AdoData(insertSqlQuery);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        internal static DataTable GetRecipeMasterData(string selectedRecipeName)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM [Recipe_Master] WHERE [Recipe_Code] = '" + selectedRecipeName.Trim() + "'";

                var cmd = new OleDbCommand(query, con);
                var da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal static bool InsertToPlcTagIndicesTable(DataTable dtNameSetupNames, List<string> listOfPlcTagNames)
        {
            string sqlInsertQuery = string.Empty;

            try
            {
                sqlInsertQuery = "INSERT INTO tblPlcTagIndices (";

                for (int columnIndex = 0; columnIndex < dtNameSetupNames.Rows.Count; columnIndex++)
                {
                    sqlInsertQuery += " [" + dtNameSetupNames.Rows[columnIndex][0] + "],";
                }
                sqlInsertQuery += " [IsActive], [PlcProgramModel],";
                sqlInsertQuery = sqlInsertQuery.TrimEnd(',');
                sqlInsertQuery += ")";
                sqlInsertQuery += " VALUES (";
                for (int i = 0; i < dtNameSetupNames.Rows.Count; i++)
                {
                    int plcTagIndex = -1;
                    string plcTagIndexValue = "";
                    string plcTagName = dtNameSetupNames.Rows[i]["OPCTagName"].ToString();
                    if (plcTagName == "<None>")
                    {
                        sqlInsertQuery += " '" + -1 + "',";
                    }
                    else
                    {
                        if (listOfPlcTagNames.Contains(plcTagName))
                        {
                            plcTagIndex = listOfPlcTagNames.IndexOf(plcTagName);
                            plcTagIndexValue = listOfPlcTagNames[plcTagIndex];
                        }
                        //sqlInsertQuery += " " + plcTagIndex + ",";
                        sqlInsertQuery += " '" + plcTagIndexValue + "',";
                    }
                }
                sqlInsertQuery = sqlInsertQuery.TrimEnd(',');
                sqlInsertQuery += " ,1";   //default active when inserting new record 
                sqlInsertQuery += " ,'" + mdiMain.SelectedPlcCpuType + "'"; //set PlcProgramModel to current OPC Server ProgID

                sqlInsertQuery += ")";

                int result = AdoData(sqlInsertQuery);

                if (result != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions.InsertToErrorLogTable(ex.HResult, ex.Message);
                return false;
            }
        }

        //-------------------------------------------------------------------------------------------------


        // 24/06/2024 : BhaveshT - GetMatIndex(): Function to allocate Index values from Namesetup table to variables and use to extract data from Text File.

        public static bool GetMatIndex(clsVariables clsVar, System.Windows.Forms.Label labelMessage)
        {
            try
            {
                DataTable dt = clsFunctions.fillDatatable("select * from NameSetup where Batch_No = 1");

                if (dt.Rows.Count > 0)
                {
                    labelMessage.Text = ".";

                    clsVar.Gate1_Index = dt.Rows[0]["Gate1Name"].ToString();
                    clsVar.Gate2_Index = dt.Rows[0]["Gate2Name"].ToString();
                    clsVar.Gate3_Index = dt.Rows[0]["Gate3Name"].ToString();
                    clsVar.Gate4_Index = dt.Rows[0]["Gate4Name"].ToString();
                    clsVar.Gate5_Index = dt.Rows[0]["Gate5Name"].ToString();
                    clsVar.Gate6_Index = dt.Rows[0]["Gate6Name"].ToString();

                    clsVar.Cement1_Index = dt.Rows[0]["Cem1Name"].ToString();
                    clsVar.Cement2_Index = dt.Rows[0]["Cem2Name"].ToString();
                    clsVar.Cement3_Index = dt.Rows[0]["Cem3Name"].ToString();
                    clsVar.Cement4_Index = dt.Rows[0]["Cem4Name"].ToString();

                    clsVar.Filler_Index = dt.Rows[0]["FillName"].ToString();
                    clsVar.Water1_Index = dt.Rows[0]["Wtr1Name"].ToString();
                    clsVar.Adm11_Index = dt.Rows[0]["Admix1Name"].ToString();

                    return true;
                }
                else
                {
                    MessageBox.Show("Please set Text File Mapping Indexes for Materials (TxtIndex is NULL in Namesetup).");
                    labelMessage.Text = "Please set Text File Mapping Indexes for Materials.";

                    return false;
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at GetMatIndex() - " + ex.Message);
                MessageBox.Show("Exception at GetMatIndex() - " + ex.Message);
                labelMessage.Text = "Please set Text File Mapping Indexes for Materials.";

                return false;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        /* Created Date: 18/06/2024 by Dinesh
         * This method will check UniPro patching system exe and if found then 
         * it will return version of it.
         */

        public static string GetUniUniProPatchingVersion()
        {
            string filePath = "";
            try
            {
                filePath = Application.StartupPath + "\\UniPro_Patching_System.exe"; // Replace this with the actual path to UniUploader.exe

                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);
                //return $"File Version: {versionInfo.FileVersion}\nProduct Version: {versionInfo.ProductVersion}";
                return $"{versionInfo.FileVersion}";
            }
            catch (Exception ex)
            {
                return "Not found";
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string GetUniUploader_PWD_RMCVersion()
        {
            string filePath = "";
            try
            {
                filePath = Application.StartupPath + "\\UniUploader_PWD_RMC.exe"; // Replace this with the actual path to UniUploader.exe

                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);
                //return $"File Version: {versionInfo.FileVersion}\nProduct Version: {versionInfo.ProductVersion}";
                return $"{versionInfo.FileVersion}";
            }
            catch (Exception ex)
            {
                return "Not Installed";
            }
        }

        //----------------------- 06/07/2024 : BhaveshT - This method asks for password, if it is correct - returns true else return false. --------------------------------

        public static bool AskPrompt()
        {
            string password = Classes.Prompt.ShowDialog("Enter password", "Password");

            if (password == "Unipro@73")
            {
                return true;
            }
            else if (password == "")
            {
                return false;
            }
            else if (password != "" || password != "Unipro@73")
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        //--------------------------------------------------------------------------

        // 11/07/2024 : BhaveshT - This function will correctly check if the file exists and return true or false accordingly.

        public static bool CheckIfFileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void CheckIfPathCorrect()
        {
            try
            {
                string checkDBType = clsFunctions.loadSingleValueSetup("Select DB_Type from Unipro_Setup where Status = 'Y'");
                string checkDBPath = clsFunctions.loadSingleValueSetup("Select path from Unipro_Setup where Status = 'Y'");


                if (checkDBType.ToLower() == "ms access" || checkDBType.ToLower() == "acc db")
                {
                    bool fileExists = clsFunctions.CheckIfFileExists(checkDBPath);

                    if (fileExists)
                    {
                        //MessageBox.Show("File exist.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Client Database Path. Please set correct path of database.");

                        QuickConfigure q = new QuickConfigure();
                        q.Show();

                        //if (IsFormOpen("QuickConfigure"))
                        //{
                        //    MessageBox.Show("The form is already open!");
                        //}
                        //else
                        //{
                        //    QuickConfigure q = new QuickConfigure();
                        //    q.Show();
                        //}

                    }
                }
            }
            catch
            {

            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 22/07/2024 : BhaveshT - This functions checks client DB mapping data is availabe or not. If not then show message and open Sync form to import it.

        public static bool CheckClientDBSet()
        {
            try
            {
                string checkDBType = clsFunctions.loadSingleValueSetup("Select DB_Type from Unipro_Setup where Status = 'Y'");

                string checkHead = clsFunctions.loadSingleValueSetup("Select SoftwareVersion FROM DataHeaderTableSync WHERE SoftwareVersion <> 'VIPL' AND Flag = 'Y' ");
                string checkTrans = clsFunctions.loadSingleValueSetup("Select SoftwareVersion FROM DataTransactionTableSync WHERE SoftwareVersion <> 'VIPL' AND Flag = 'Y' ");

                if (!clsFunctions.planttype.Contains("RMC"))
                {
                    return true;
                }

                else if (checkDBType.ToLower() == "ms access" || checkDBType.ToLower() == "acc db")
                {
                    if (checkHead.ToLower() == "" || checkTrans.ToLower() == "" || checkHead.ToLower() == "0" || checkTrans.ToLower() == "0")
                    {
                        MessageBox.Show("Invalid Client Database Path. Please set correct path of database.");

                        Sync q = new Sync();
                        q.Show();

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 22/07/2024 : BhaveshT - This method Sets flag for fetched rows for client table field mapping in DataHeaderTableSync & DataTransactionTableSync tables of Setup DB.

        public static void AutoSetClientDB()
        {
            try
            {
                string checkHead = clsFunctions.loadSingleValueSetup("Select SoftwareVersion FROM DataHeaderTableSync WHERE SoftwareVersion <> 'VIPL'");
                string checkTrans = clsFunctions.loadSingleValueSetup("Select SoftwareVersion FROM DataTransactionTableSync WHERE SoftwareVersion <> 'VIPL'");

                if (checkHead != "" || checkTrans != "")
                {
                    clsFunctions.AdoData_setup("UPDATE DataHeaderTableSync SET Flag = 'N' WHERE SoftwareVersion <> 'VIPL'");
                    clsFunctions.AdoData_setup("UPDATE DataTransactionTableSync SET Flag = 'N' WHERE SoftwareVersion <> 'VIPL'");

                    clsFunctions.AdoData_setup("UPDATE DataHeaderTableSync SET Flag = 'Y' WHERE SoftwareVersion = '" + checkHead + "' ");
                    clsFunctions.AdoData_setup("UPDATE DataTransactionTableSync SET Flag = 'Y' WHERE SoftwareVersion = '" + checkTrans + "' ");

                    clsFunctions.AdoData_setup("Update DataHeaderTableSync set Flag='Y' where Type='VIPL' and info='Fields' ");
                    clsFunctions.AdoData_setup("Update DataTransactionTableSync set Flag='Y' where Type='VIPL' and info='Fields' ");

                }
            }
            catch
            {

            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 27/07/2024 : BhaveshT - This function will fetch exe versions and update it into columns of setup DB.

        public static void StoreExeVersions()
        {
            string swVersion, upVersion, upPWD_Version = "";
            int a;

            try
            {
                swVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                upVersion = GetUniUploaderVersion();
                upPWD_Version = GetUniUploaderVersionPWD();

                if (swVersion != "" && upVersion != "" && upPWD_Version != "")
                {
                    upVersion = upVersion.Replace("v", "");
                    upPWD_Version = upPWD_Version.Replace("v", "");

                    a = clsFunctions.AdoData_setup("Update PlantSetup SET Unipro_Version = '" + swVersion + "', " +
                        "UniUp_Version = '" + upVersion + "', UniUpPWD_Version = '" + upPWD_Version + "' ");

                    if (a == 1)
                    {
                        clsFunctions_comman.ErrorLog("StoreExeVersions(): stored successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("StoreExeVersions(): Exception - " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void OpenWebsiteForReports()
        {
            string websiteUrl = "";
            clsFunctions.activeDeptName = clsFunctions.GetActiveDeptNameFromServerMapping();
            clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

            dept = clsFunctions.activeDeptName;

            //Open website which is in ServerMapping, not according Department selected at software registration.

            //------------------------ updated 04/03/2024 - BhaveshT ------------------------
            try
            {
                if (dept.Contains("PMC"))
                {
                    //websiteUrl = clsFunctions.loadSingleValueSetup("Select ipaddress from ServerMapping where flag = 'Y'");
                    websiteUrl = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);
                }
                else if (dept.Contains("PWD") && (clsFunctions.aliasName.Contains("RMC")))
                {
                    //websiteUrl = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);                    
                    //websiteUrl = websiteUrl + "/PWDSCADA/";

                    websiteUrl = "http://maha.pwdscada.in/";

                }
                else if (dept.Contains("PCMC"))
                {
                    websiteUrl = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);
                }
                else if (dept.Contains("VIPL"))
                {
                    websiteUrl = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);
                }
                else if (dept.Contains("PWD") && clsFunctions.aliasName.Contains("BT") || clsFunctions.aliasName.Contains("Bitu"))
                {
                    websiteUrl = "http://nagpurscada.mahapwd.com/";
                }
                else if (dept.Contains("PMRDA"))
                {
                    websiteUrl = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);
                }
                else
                {
                    websiteUrl = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);
                }

                //string website = "http://" + websiteUrl;

                Process.Start(websiteUrl);

            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog($"OpenWebsiteForReports: Error opening website: {ex.Message}");
                MessageBox.Show($"Error opening website: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        //This Function is Optimized for checking whether same form is open or not which is already open. -- By Dinesh

        private static bool IsFormOpen(string formName)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == formName)
                {
                    return true;
                }
            }
            return false;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        //public static S7.Net.CpuType GetCpuTypeFromString(string strCpuType)
        //{
        //    CpuType SelectedPlcCpuType = CpuType.S7200Smart;

        //    //switch (mdiMain.SelectedPlcCpuType)
        //    switch (mdiMain.SelectedPlcCpuType)
        //    {
        //        case "S7200":
        //            SelectedPlcCpuType = CpuType.S7200;
        //            break;

        //        case "Logo0BA8":
        //            SelectedPlcCpuType = CpuType.Logo0BA8;
        //            break;

        //        case "S7200SMART":
        //            SelectedPlcCpuType = CpuType.S7200Smart;
        //            break;

        //        case "S7300":
        //            SelectedPlcCpuType = CpuType.S7300;
        //            break;

        //        case "S7400":
        //            SelectedPlcCpuType = CpuType.S7400;
        //            break;

        //        case "S71200":
        //            SelectedPlcCpuType = CpuType.S71200;
        //            break;

        //        case "S71500":
        //            SelectedPlcCpuType = CpuType.S71500;
        //            break;

        //        case "S7200Smart":
        //            SelectedPlcCpuType = CpuType.S7200Smart;
        //            break;

        //        default:
        //            break;
        //    }
        //    return SelectedPlcCpuType;
        //}

        //------------------------------------- 12/11/2024 : BhaveshT - For Controller base PlantScada -----------------------------------------------------------------------

        public static void InitializeSerialPort(ref SerialPort spController, string portName, Label statusLabel, SerialDataReceivedEventHandler dataReceivedHandler)
        {
            try
            {
                // Check if the SerialPort is null or closed, and only initialize if necessary
                if (!clsFunctions.IsSerialPortOpen(spController))
                {
                    // Initialize SerialPort with desired settings
                    int baudRate = 9600;

                    SetSerialPortValue(spController);

                    //spController = new SerialPort(portName, baudRate)
                    //{
                    //    Parity = Parity.None,
                    //    StopBits = StopBits.One,
                    //    DataBits = 8,
                    //    Handshake = Handshake.None,
                    //    ReadTimeout = 500,
                    //    WriteTimeout = 500
                    //};

                    // Attach the passed DataReceived event handler
                    spController.DataReceived += dataReceivedHandler;

                    try
                    {
                        if (!clsFunctions.IsSerialPortOpen(spController))
                        {
                            // Attempt to open the port
                            spController.Open();

                            //spController.Close();

                            ErrorLog("[INFO] Serial Port opened.");
                            statusLabel.Text = "Serial Port opened.";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log and display any exceptions related to opening the port
                        MessageBox.Show($"Error opening serial port: {ex.Message}");
                        statusLabel.Text = ex.Message;
                        ErrorLog("[Exception] InitializeSerialPort: " + ex.Message);

                    }
                }
                else
                {
                    // Log if the port is already open
                    ErrorLog("Serial Port is already open.");
                    statusLabel.Text = "Serial Port is already open.";
                }
            }
            catch (Exception ex)
            {
                // Log any other exceptions related to serial port initialization
                ErrorLog("Exception - InitializeSerialPort: " + ex.Message);
                statusLabel.Text = "Exception - InitializeSerialPort: " + ex.Message;
            }
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static bool IsSerialPortOpen(SerialPort spController)
        {
            // Check if spController is initialized and open

            return spController != null && spController.IsOpen;
            //return spController.IsOpen;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        // 13/12/2024 : BhaveshT - To write data from PLC response to txtFile -

        public static void PLCDATA(string tag, string value)
        {
            StreamWriter streamWriter = (StreamWriter)null;
            try
            {

                string path = Application.StartupPath + "\\DATAVAL\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //streamWriter = new StreamWriter(path + "Error_log" + DateTime.Now.ToString("dd_MM_yyyy") + ".log", true);
                //streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy") + " " + sMessage + Environment.NewLine);

                // 19/01/2024 - BhaveshT : renamed the file

                streamWriter = new StreamWriter(path + "PLC_Data_Unipro_" + DateTime.Now.ToString("dd-MM-yyyy") + ".log", true);
                streamWriter.WriteLine(DateTime.Now.ToString("hh:mm:ss tt - ") + " Tag = " + tag + " | Value =  " + value + Environment.NewLine);

            }
            catch (Exception ex)
            {
                //int num = (int)MessageBox.Show("error: " + ex.Message + " Description " + sMessage);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static int IsSiteExist(string woCode, string site)
        {
            int count = 0;
            try
            {
                woCode = woCode.Trim();
                site = site.Trim();

                count = clsFunctions_comman.loadintvalue("SELECT COUNT(*) FROM Site_Master WHERE WorkOrderID = '" + woCode + "' AND SiteName = '" + site + "' ");
                return count;
            }
            catch
            {
                return count;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string Get_UniqueID()
        {
            try
            {
                HMI_Unique_ID = clsFunctions.loadSingleValueSetup("SELECT U_ID from PlantSetup");
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("[Exception] clsFunctions.Get_UniqueID() - " + ex.Message);
                return "";

            }
            return HMI_Unique_ID;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static int Update_UniqueID(string uniqueId)
        {
            int a = 0;

            try
            {
                a = clsFunctions.AdoData_setup("Update PlantSetup SET U_ID = '" + uniqueId + "' ");
                return a;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("[Exception] clsFunctions.Update_UniqueID() - " + ex.Message + " | UID: " + uniqueId);
                return a;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        /* 
            21/01/2025 : BhaveshT - GetWorkOrderData_Like()

            This method fetches work order data from the database based on a LIKE query for workname, matching WorkType, and ContractorName.
            It populates the provided textboxes and label with relevant data from the first row of the result.
        */

        public static void GetWorkOrderData_Like(ComboBox WorknName, TextBox WorkCode, ComboBox ContractorName, TextBox PlantCode, TextBox ContractorCode, Label WorkType)
        {
            //------------ When workname is longer than 255, use then use LIKE workname to fetch Work order data. --------------------------

            try
            {
                DataTable dt = new DataTable();

                string workname = "%" + WorknName.Text + "%";

                string query = "SELECT * FROM workorder " +
                               "WHERE workname LIKE '" + workname.Replace("'", "''") + "' " +
                               "AND WorkType = '" + clsFunctions.activeDeptName.Replace("'", "''") + "' " +
                               "AND ContractorName = '" + ContractorName.Text.Replace("'", "''") + "' ";

                // Fetch the data into a DataTable
                dt = Uniproject.Classes.clsFunctions_comman.fillDatatable(query);

                if (dt.Rows.Count != 0)
                {
                    //SiteCode.Text = dt.Rows[0]["workorderid"].ToString();
                    WorkCode.Text = dt.Rows[0]["Workno"].ToString();
                    ContractorCode.Text = dt.Rows[0]["ContractorID"].ToString();
                    WorkType.Text = dt.Rows[0]["Worktype"].ToString();
                    PlantCode.Text = clsFunctions.activePlantCode;
                }
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] clsFunctions.GetWorkOrderData_Like() - " + ex.Message);
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        /* 
            21/01/2025 : BhaveshT - GetWorkOrderData_Like_for_BT()

            This method fetches work order data from the database based on a LIKE query for workname, matching WorkType, and ContractorName.
            It populates the provided textboxes and label with relevant data from the first row of the result.
        */

        public static void GetWorkOrderData_Like_for_BT(ComboBox WorknName, Label WorkCode, ComboBox ContractorName, TextBox PlantCode, TextBox ContractorCode, Label WorkType)
        {
            //------------ When workname is longer than 255, use then use LIKE workname to fetch Work order data. --------------------------

            try
            {
                DataTable dt = new DataTable();

                string workname = "%" + WorknName.Text + "%";

                string query = "SELECT * FROM workorder " +
                               "WHERE workname LIKE '" + workname.Replace("'", "''") + "' " +
                               "AND WorkType = '" + clsFunctions.activeDeptName.Replace("'", "''") + "' " +
                               "AND ContractorName = '" + ContractorName.Text.Replace("'", "''") + "' ";

                // Fetch the data into a DataTable
                dt = Uniproject.Classes.clsFunctions_comman.fillDatatable(query);

                if (dt.Rows.Count != 0)
                {
                    //SiteCode.Text = dt.Rows[0]["workorderid"].ToString();
                    WorkCode.Text = dt.Rows[0]["Workno"].ToString();
                    ContractorCode.Text = dt.Rows[0]["ContractorID"].ToString();
                    WorkType.Text = dt.Rows[0]["Worktype"].ToString();
                    PlantCode.Text = clsFunctions.activePlantCode;
                }
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] clsFunctions.GetWorkOrderData_Like() - " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        //03/02/2025 : BhaveshT - CheckNameSetup() - this method ensures that Parameter names are set.

        public static int CheckNameSetup()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = clsFunctions.fillDatatable("SELECT Gate1Name FROM NameSetUp WHERE tInfo='O' ORDER BY Batch_No");

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Please Set Parameter names.");
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] clsFunctions.CheckNameSetup() - " + ex.Message);
                return 0;
            }
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------


        // 14/02/2025 : BhaveshT - GetBitumenColumnNames(): Function to allocate Column Names from tblOpcTagIndices table to variables and use to extract data from client DB in Protool module.

        public static bool GetBitumenColumnNames(clsVariables clsVar, System.Windows.Forms.Label labelMessage)
        {
            try
            {
                DataTable dt = clsFunctions_comman.fillDatatable("SELECT PlcProgramModel, Temperature1, Temperature2, Temperature3, Temperature4 " +
                    "FROM tblOpcTagIndices where PlcProgramModel = 'Protool' ");

                if (dt.Rows.Count > 0)
                {
                    labelMessage.Text = ".";

                    clsVar.col_Exhaust_Temp = dt.Rows[0]["Temperature1"].ToString();
                    clsVar.col_Mix_Temp = dt.Rows[0]["Temperature2"].ToString();
                    clsVar.col_Tank1_Temp = dt.Rows[0]["Temperature3"].ToString();
                    clsVar.col_Tank2_Temp = dt.Rows[0]["Temperature4"].ToString();

                    return true;
                }
                else
                {
                    MessageBox.Show("Please set Bitumen Parameter.");
                    labelMessage.Text = "Please set Bitumen Parameter.";

                    return false;
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at GetBitumenColumnNames() - " + ex.Message);
                MessageBox.Show("Exception at GetBitumenColumnNames() - " + ex.Message);
                labelMessage.Text = "Please set Bitumen Parameter.";

                return false;
            }
        }

        //--------------------------------------------------------------

        // 28/02/2025 : BhaveshT - This Log function will write about PLC in Folder : Logs_PLC

        public static void Logs_PLC(string sMessage)
        {
            StreamWriter streamWriter = (StreamWriter)null;

            try
            {
                //string folderName = DateTime.Now.ToString("MMM") + "_" + DateTime.Now.Year.ToString();
                string folderName = DateTime.Now.ToString("MMM") + "_" + DateTime.Now.Year.ToString();

                string path = Application.StartupPath + "\\Logs_PLC\\" + folderName;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                streamWriter = new StreamWriter(path + "\\Logs_PLC " + DateTime.Now.ToString("dd-MM-yyyy") + ".log", true);
                streamWriter.WriteLine($"{DateTime.Now:hh:mm:ss tt} " + sMessage);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
        }

        public static int loadRowCount(string query)
        {
            var dt = fillDatatable(query);
            return dt?.Rows.Count ?? 0;
        }
        public static async Task<int> AdoDataAsync(string query)
        {
            try
            {
                if (clsFunctions.con.State == ConnectionState.Closed || clsFunctions.con.State == ConnectionState.Broken)
                    await clsFunctions.con.OpenAsync();
                return await new OleDbCommand(query, clsFunctions.con).ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("Exception: clsFunctions.AdoData - " + ex.Message + " | query => " + query);
                return 0;
            }
        }

        //--------------------------------------------------------------

        //--------------------------------------------------------------

        //--------------------------------------------------------------



    }
}