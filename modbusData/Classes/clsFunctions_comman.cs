using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Uniproject.UtilityTools;
namespace Uniproject.Classes
{
    public static class clsFunctions_comman
    {
        //public static string connCP2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\Wincc_batch.mdb;Persist Security Info=true ;";
        // public static string connCP2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\HotMixScada.mdb;Persist Security Info=true ;";//Before changing name and password
        public static string connCP2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\UniPro.mdb;Persist Security Info=true ;Jet OLEDB:Database Password='Unipro0073';";
        //return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath + "; Persist Security Info=true ;Jet OLEDB:Database Password='" + DBPass + "';";

        public static string RegFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\vtIEpmLP";
        static OleDbConnection con = new OleDbConnection(connCP2);

        public static string regName = "VIPLUNIPRO";
        public static string serialKey = "PRO99-UVBK8-NII11-IPR54-UL54O";
        public static string IsRMCReg = "";
        public static int IsRegSoftware;
        public static int batchno = 0;
        public static string regfilestatus;
        public static bool Show_Missing_Batch = false;

        public static string RptName;                           // added by BhaveshT
        public static string ChallanName;                       // 30/08/2023
        public static string loadForm;
        public static string FileType;
        public static string CSV_Version;

        //public static string PC_ipAddress = NetworkInfo.GetIpAddress();
        //public static string PC_macAddress = NetworkInfo.GetMacAddress();
        public static string softwareStatus = "ON";

        public static string lastDate = "";                     // added by BhaveshT
        public static string lastTime = "";                     // 27/05/2024

        public static bool pingResponse = false;

        // Created by BhaveshT : To get active formName from DbInfo table to load that form
        public static string loadFormName()
        {
            try
            {
                //loadForm = clsFunctions.loadSingleValueSetup("select FormName from Unipro_Setup where status='Y'");
                // if (loadForm == null || loadForm == "")
                //{
                    loadForm = clsFunctions.loadSingleValueSetup("select FormName from DbInfo where status='Y'");
                    ErrorLog(loadForm + " Selected into DbInfo where status = Y");

                //}
                //else
                //{
                //    ErrorLog(loadForm + " Selected into Unipro_Setup where status = Y ");
                //}
                //loadForm = clsFunctions.loadSingleValueSetup("select FormName from DbInfo where status='Y'");
                return loadForm;
            }
            catch 
            {
                loadForm = clsFunctions.loadSingleValueSetup("select FormName from DbInfo where status='Y'");
                return loadForm;
            }
        }

        public static void UniBox(string message)
        {
            UniBox box = new UniBox(message);
            box.ShowDialog();

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------
        // 28/12/2023 - Created by BhaveshT : To get active formName from Software_Setup table to load that form
        public static string loadFormNameFromUniproSetup()
        {
            try
            {
                loadForm = clsFunctions.loadSingleValueSetup("select FormName from Unipro_Setup where Status = 'Y'");
                return loadForm;
            }
            catch
            {
                return loadForm;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------

        public static void ErrorLog(string sMessage)
        {

            //------------------------------------------ 01/07/2024 : BhaveshT - changed Error_Log file name -----------------------------------------------
            
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


            //------------------------------------------ 01/07/2024 : BhaveshT - changed Error_Log file name -----------------------------------------------
            //StreamWriter objSw = null;
            //try
            //{
            //    string sFolderName = Application.StartupPath + @"\Logs\";
            //    chkdire(sFolderName);
            //    string sFilePath = sFolderName + "Error_RMC.log";

            //    objSw = new StreamWriter(sFilePath, true);
            //    objSw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " " + sMessage + Environment.NewLine);

            //}
            //catch
            //{

            //}
            //finally
            //{
            //    if (objSw != null)
            //    {
            //        objSw.Flush();
            //        objSw.Dispose();
            //    }
            //}
        }

        //-------------- 19/01/2024 - BhaveshT --------------
        public static void AlertSMSLog(string sMessage)
        {
            StreamWriter objSw = null;
            try
            {
                string sFolderName = Application.StartupPath + @"\Logs\";
                chkdire(sFolderName);
                string sFilePath = sFolderName + "Alert_SMS.log";

                objSw = new StreamWriter(sFilePath, true);
                objSw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt -") + " " + sMessage + Environment.NewLine);

            }
            catch
            {

            }
            finally
            {
                if (objSw != null)
                {
                    objSw.Flush();
                    objSw.Dispose();
                }
            }
        }


        //-------------- Added by BhaveshT - 30/08/2023 --------------
        public static void getRptName()
        {
            try
            {
                RptName = clsFunctions.loadSingleValueSetup("SELECT Report_Name from ReportMapping where Report_Type = 'Batch Report' AND Flag = 'Y'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("clsFunctions_comman.getRptName() - " + ex.Message);
            }
        }

        public static void getChallanName()
        {
            try
            {
                ChallanName = clsFunctions.loadSingleValueSetup("SELECT Report_Name from ReportMapping where Report_Type = 'Delivery Challan' AND Report_Flag = 'Y'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("clsFunctions_comman.getChallanName() - " + ex.Message);
            }
        }
        //-------------------------------------------------------------

        /*
         BhaveshT - 04/11/2023
        Function name: getFileType() | Return type: string
        Created this function to get File Type from DbInfo table of Setup db to avoid to use in combobox of BatchData_Tuskar form
        which will work on multiple File types data upload
         */
        public static string getFileType()
        {
            try
            {
                //FileType = clsFunctions.loadSingleValueSetup("SELECT Description from DbInfo where FormName = 'BatchData_Tuskar' AND Status = 'Y'");
                //FileType = clsFunctions.loadSingleValueSetup("SELECT Description from Unipro_Setup where FormName = 'BatchData_Tuskar' AND Status = 'Y'");      //29/12/2023

                FileType = clsFunctions.loadSingleValueSetup("SELECT Description from Unipro_Setup where Status = 'Y'");      //29/12/2023
            }
            catch (Exception ex)
            {
                MessageBox.Show("clsFunctions_comman.getRptName() - " + ex.Message);
            }
            return FileType;
        }
        //-------------------------------------------------------------

        //-------------------------------------------------------------

        /*
         BhaveshT - 04/11/2023
        Function name: getFileType() | Return type: string
        Created this function to get File Type from DbInfo table of Setup db to avoid to use in combobox of BatchData_Tuskar form
        which will work on multiple File types data upload
         */
        public static string getCSV_Version()
        {
            try
            {
                //CSV_Version = clsFunctions.loadSingleValueSetup("SELECT Description from DbInfo where FormName = 'UploadData_FileBase' AND Status = 'Y'");
                CSV_Version = clsFunctions.loadSingleValueSetup("SELECT Description from Unipro_Setup where FormName = 'UploadData_FileBase' AND Status = 'Y'");      //29/12/2023
            }
            catch (Exception ex)
            {
                MessageBox.Show("clsFunctions_comman.getCSV_Version() - " + ex.Message);
            }
            return CSV_Version;
        }
        //-------------------------------------------------------------

        public static void chkdire(string sFolderName)
        {
            if (!Directory.Exists(sFolderName))
                Directory.CreateDirectory(sFolderName);
        }

        public static void DATAVAL(string sMessage)
        {
            StreamWriter objSw = null;
            try
            {
                string sFolderName = Application.StartupPath + @"\DATAVAL\";
                if (!Directory.Exists(sFolderName))
                    Directory.CreateDirectory(sFolderName);
                string sFilePath = sFolderName + "DATA.txt";

                objSw = new StreamWriter(sFilePath, true);
                objSw.WriteLine(sMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("DATAVAL" + ex.Message);
            }
            finally
            {
                if (objSw != null)
                {
                    objSw.Flush();
                    objSw.Dispose();
                }
            }
        }

        public static void UnsavedBatch(string sMessage, string filename)
        {
            StreamWriter objSw = null;
            try
            {
                string sFolderName = Application.StartupPath + @"\UnSaveBatch\";
                if (!Directory.Exists(sFolderName))
                    Directory.CreateDirectory(sFolderName);
                string sFilePath = sFolderName + filename + ".txt";

                objSw = new StreamWriter(sFilePath, true);
                objSw.WriteLine(sMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("UnSavedBatch" + ex.Message);
            }
            finally
            {
                if (objSw != null)
                {
                    objSw.Flush();
                    objSw.Dispose();
                }
            }
        }

        public static void strfile(string sMessage)
        {
            StreamWriter objSw = null;
            try
            {
                string sFolderName = Application.StartupPath + @"\strfile\";
                if (!Directory.Exists(sFolderName))
                    Directory.CreateDirectory(sFolderName);
                string sFilePath = sFolderName + "Batchstring.txt";

                objSw = new StreamWriter(sFilePath, true);
                objSw.WriteLine(sMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("strfile" + ex.Message);
            }
            finally
            {
                if (objSw != null)
                {
                    objSw.Flush();
                    objSw.Dispose();
                }
            }
        }

        public static void FillCombo(string Query, ComboBox cmb)
        {
            try
            {
                DataTable dt = clsFunctions_comman.fillDatatable(Query);
                cmb.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //string str_U_En = clsSecurity.Decrypt(dr[0].ToString().Trim());
                        string str_U_En = dr[0].ToString().Trim();
                        cmb.Items.Add(str_U_En);
                        cmb.SelectedIndex = 0;
                        //cmb.Items.Add(str_U_En.ToUpper());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("FillCombo : " + ex.Message);
            }

        }
        public static void mergecombo(string eqry, string qry, ComboBox cmb)
        {
            try
            {
               // FillCombo_Upper(eqry, cmb);

                DataTable dt2 = clsFunctions_comman.fillDatatable(qry);
                // cmb.Items.Clear();
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        cmb.Items.Add(dr[0].ToString().Trim().ToUpper());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FillCombo : " + ex.Message);
            }

        }
        public static void FillCombo_D_ASIT(string Query, ComboBox cmb)
        {
            try
            {
                DataTable dt = clsFunctions_comman.fillDatatable(Query);
                cmb.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        cmb.Items.Add(dr[0].ToString().Trim().ToUpper());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FillCombo : " + ex.Message);
            }
        }

         
        public static DataTable fillDatatable(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("FillDataTable : " + ex.Message);
                ErrorLog("FillDataTable : " + ex.Message);
            }
            return dt;
        }

        public static string loadSinglevalue(string query)
        {
            var dt = fillDatatable(query);
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static int loadintvalue(string query)
        {
            var dt = fillDatatable(query);
            if (dt.Rows.Count != 0)
            {
                if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                    return 0;
                else
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static int Ado(string query)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                OleDbCommand cmd = new OleDbCommand(query, con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e) { ErrorLog("Inside clsFuncion_common ADO : " + e.Message + " | Query : " + query); return 0; }
        }

        public static int GetMaxId(string query)
        {
            DataTable dt = clsFunctions_comman.fillDatatable(query);

            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "") return 1;
                else return Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
        }

        public static void fillGridView(string Query, DataGridView dgv1)
        {
            try
            {
                DataTable dt = fillDatatable(Query);
                dgv1.DataSource = dt;
            }
            catch { }
        }

        public static string GetDataType(string query)
        {
            var dt = fillDatatable(query);
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0][0].GetType().ToString();
            }
            else
            {
                return "";
            }
        }

        // added by dinesh Date:07/08/2023
        public static OleDbDataAdapter Fill_DataSet(string Query)
        {
            OleDbCommand cmd = new OleDbCommand(Query, con);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            return da;
        }

        // 11/11/2023 - BhaveshT
        // function from GlobalMethod
        public static string GetMaxNo(string query)
        {
            string no = "1";
            DataTable dt = fillDatatable(query);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString().Trim() != "")
                {
                    no = dt.Rows[0][0].ToString();
                }
            }
            return no;
        }

        //-------------------------------------------------------------
        /* BhaveshT - 18/12/2023 */
        public static bool IsBatchNoExistsInTable(string Batch_No)
        {
            //int batchCount = clsFunctions_comman.loadintvalue("SELECT COUNT(*) AS BatchCount FROM Batch_Transaction WHERE Batch_No = " + Batch_No);       //cmbbatchno.Text); 
            int batchCount = clsFunctions_comman.loadintvalue("SELECT COUNT(*) AS BatchCount FROM Batch_Dat_Trans WHERE Batch_No = " + Batch_No);       //cmbbatchno.Text); 
            //return batchCount > 0;
            if (batchCount != 0)
                return true;
            else
                return false;
        }

        //-------------------------------------------------------------
        /* BhaveshT - 27/03/2025 */

        public static bool IsBatchNoExistsInTransaction(string Batch_No, string BatchIndex)
        {
            int batchCount = clsFunctions_comman.loadintvalue("SELECT COUNT(*) AS BatchCount FROM Batch_Transaction WHERE Batch_No = " + Batch_No + " AND Batch_Index = " + BatchIndex);       //cmbbatchno.Text); 
            //return batchCount > 0;
            if (batchCount != 0)
                return true;
            else
                return false;
        }

        //--------------------------------------------------------------------------
        // 08/12/2023 - BhaveshT : Created custom messageBox

        //public static void UniBox(string message)
        //{
        //    UniBox box = new UniBox(message);
        //    box.ShowDialog();

        //}
        //--------------------------------------------------------------------------
        // 25/12/2023 - BhaveshT : to get URL according selected dept. for Auto Registration
        public static string SetRegURLfromDept(string urlForReg)
        {



            //--------------------------------------------------------------------------

            if (urlForReg.Contains("PMC"))
            {
                return "pmcscada.in";
            }
            else if (urlForReg.Contains("PCMC"))
            {
                return "pcmcscada.in";
            }
            else if (urlForReg.Contains("PWD"))
            {
                return "117.240.186.60:8080";
            }
            else if (urlForReg.Contains("ISCADA"))
            {
                return "pwdscada.in";
            }
            else if (urlForReg.Contains("CLOUD"))
            {
                return "scadaindia.com";
            }

            return "Invalid Dept";

        }

        //--------------------------------------------------------------------------

        // 09/04/2024 - BhaveshT : This method will insert Software Status and Date_Time in Software_Status table of Setup DB : Plant_LiveStatus_History
        

        //--------------------------------------------------------------------------

        public static string ReplaceBitumanWithBitumen(string input)
        {
            // Check if the input contains "Bituman"
            if (input.Contains("Bituman"))
            {
                // Replace "Bituman" with "Bitumen"
                input = input.Replace("Bituman", "Bitumen");
            }

            return input;
        }

        //---------------------- 12/02/2024 - BhaveshT : Validating vehicleNo ------------------------------
        public static string ReplaceSpecialCharactersForVehicle(string input)
        {
            input = input.Replace(" ","");

            if (input == "-" || input == "NA")
                input = "MH12AB1234";

            // Use regular expression to replace special characters with empty ""
            string pattern = @"[^a-zA-Z0-9-]+";

            string replacement = "";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(input, replacement);

            return result;
        }

        //--------------------------------------------------------------------------

        // 24/05/2024 - BhaveshT : This method will insert date & Time with AliasName in LastUpdateRecord table of Unipro DB
        public static void InsertLastUpdateRecord(string alias, string lDate, string lTime)
        {
            try
            {
                clsFunctions.AdoData("Delete * from LastUpdateRecord where Dept = '" + alias + "'");

                clsFunctions.AdoData("Insert into LastUpdateRecord (Dept, l_Date, l_Time) values ('" + alias + "', '" + lDate + "', '" + lTime + "' )");

            }
            catch (Exception e)
            {
                clsFunctions_comman.ErrorLog("Exception: InsertLastUpdateRecord" + e.Message);
            }
        }


        //--------------------------------------------------------------------------
        // 27/05/2024 - BhaveshT : This method will insert date & Time with AliasName in LastUpdateRecord table of Unipro DB
        public static void FetchLastUpdateRecord(string alias)
        {
            try
            {
                DataTable dt = clsFunctions.fillDatatable("Select * from LastUpdateRecord where Dept = '"+ alias +"' ");
                
                if(dt != null)
                {
                    DataRow row = dt.Rows[0];

                    clsFunctions_comman.lastDate = Convert.ToDateTime(row["l_Date"]).ToString("dd/MM/yyyy");
                    clsFunctions_comman.lastTime = Convert.ToDateTime(row["l_Time"]).ToString("hh:mm:ss tt");
                }
            }
            catch (Exception e)
            {
                clsFunctions_comman.ErrorLog("Exception: FetchLastUpdateRecord" + e.Message);
            }
        }

        internal static bool InsertBatchingData(string entryTimeStamp, string tagValuesReceived)
        {
            try
            {
                string insertSqlQuery = "";

                insertSqlQuery = "INSERT INTO [BatchingDataLog] ";
                insertSqlQuery += "( [Timestamp], [TagValuesCSV]" + ")";
                insertSqlQuery += " VALUES ";
                insertSqlQuery += "(";
                insertSqlQuery += "'" + entryTimeStamp + "', '" + tagValuesReceived + "'";
                insertSqlQuery += ")";

                int result = clsFunctions.AdoData(insertSqlQuery);

                if (result != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //LastErrorText = "Exception in function InsertBatchingData: " + ex.Message;
                clsFunctions_comman.ErrorLog("Exception: InsertBatchingData" + ex.Message);
                return false;
            }
        }

        internal static bool InsertRawCommData(DateTime EntryTimeStamp, string RequestBytes, string ResponseBytes, string TagValue, string TagName)
        {
            string insertErrorLog = "";
            try
            {
                insertErrorLog = "INSERT INTO S7CommRawData ([Timestamp], [Request], [Response], [TagValue], [TagName]) " +
                                        " VALUES ('" + EntryTimeStamp.ToString("dd/MM/yyyy HH:mm:ss.ffffff") + "'" +
                                        ", '" + RequestBytes + "'" +
                                        ", '" + ResponseBytes + "'" +
                                        ", '" + TagValue + "'" +
                                        ", '" + TagName + "'" +
                                        ")";

                int result = clsFunctions.AdoData(insertErrorLog);

                return true;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception: InsertBatchingData" + ex.Message);
                return false;
            }
        }

        //--------------------------------------------------------------------------

        //--------------------------------------------------------------------------

        //--------------------------------------------------------------------------

        //--------------------------------------------------------------------------


    }
}
