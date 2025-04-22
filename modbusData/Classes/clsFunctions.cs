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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Uniproject.Classes
{
    public static class clsFunctions
    {

        public static string batchconnstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\";
        // public static string batchconnstrR7 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\Database\\";      //BT
        public static string batchconnstrR7 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ""; //BK14april

        public static string imgPath = Application.StartupPath + "\\img";
        public static string solutionPath = "";//clsFunctions.GetConnectionstrSetup_Path();
        public static string RegFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\vtIEpmLP";

        public static string regName = "VIPLHMSCADA";
        public static string serialKey = "QTR56-MLYH1-PTS8T-4SMH9-PG89Y";

        public static string DeviceID = "";                 // BhaveshT
        public static string dept = "";                     // 18 may
        public static string aliasName = "";
        public static string activeDeptName = "PMC";
        public static string activePlantCode = "3"; //warning please remove this from here
        public static string activeDeviceID = "12345566";
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
                //clsFunctions_comman.ErrorLog("error in logger function" + e.Message);
            }
            return sFi1ePath;
        }

        // 08/02/2024 - BhaveshT : Use Unipro_Setup table instead of Connection_setup ------------------------

        public static string getConnectionString = GetConnString();


        public static string GetConnString()
        {
            try
            {
                //getConnectionString = Convert.ToString(clsFunctions.loadSingleValueSetup("SELECT ConnectionString FROM Unipro_Setup where Status = 'Y' "));
                return getConnectionString;
            }
            catch (Exception ex)
            {
                return getConnectionString;
            }
        }
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
        //public static MySqlConnection MySqlcilent;
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

    }
}