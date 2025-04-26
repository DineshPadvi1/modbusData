using SCADA_Library;
using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Uniproject;

//using RMCPlantCP45.Classes;
using Uniproject.Classes;

namespace RMC_CP30_Simem.Classes
{
    public static class RegisterSoftware_Uni
    {
        private static readonly LoggerService _loggerService = new LoggerService();    //BT
        static string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\Setup.mdb;    Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;";
        static OleDbConnection conn = new OleDbConnection(connstr);

        public static string IsRMCRegister()
        {
             try
            {
                string dept = clsFunctions.loadSingleValueSetup("select DeptName from PlantSetup");
                string pType = clsFunctions.loadSingleValueSetup("select PlantType from PlantSetup");
                

                //06/04/2024 : BhaveshT - No need to show this message on UI
                //if(dept == null||dept==""||dept=="0" ) { MessageBox.Show("Following Department name not found in PlantSetup table dept name = '" + dept+"' This may cause for your Config file(.ini) Please Register it."); }

                clsFunctions_comman.chkdire(clsFunctions_comman.RegFilePath);

                //dynamic file = Directory.GetFiles(clsFunctions_comman.RegFilePath, "vHotMixScadan.ini").FirstOrDefault();
                UniRegister_Auto uniRegister_Auto = new UniRegister_Auto();
                pType = uniRegister_Auto.GetPlantTypeFromAlias(pType);
                dynamic file = Directory.GetFiles(clsFunctions_comman.RegFilePath, dept + "_" + pType + "_Unipro.ini").FirstOrDefault();
                //dynamic file = Directory.GetFiles(clsFunctions_comman.RegFilePath, dept+"_Unipro.ini").FirstOrDefault();// commented by dinesh


                if (file == null)
                {
                    clsFunctions.AdoData_setup("Delete * from PlantSetup");
                    //clsFunctions.AdoData_setup("Delete * from SetupInfo");

                    clsFunctions_comman.regfilestatus = "FileNotFound";
                    clsFunctions_comman.IsRMCReg = "Please register the system";
                    clsFunctions.IsEgaleReg = "Please register the system";
                    clsFunctions_comman.ErrorLog("Please register the system ");     //BhaveshT
                    clsFunctions_comman.ErrorLog(dept + "_" + pType + "_Unipro.ini file not found");
                }
                else
                {
                    string[] lines = GetRegDetails (file);

                    //if(dept != "PMC")       // Commented by BhaveshT 29/01/2024 - Storing DeviceID in ini RegFile for all Departments 
                    //{
                    //    if (clsFunctions_comman.regName == lines[0] & clsFunctions_comman.serialKey == lines[1])
                    //    {
                    //        DateTime Expirydate = default(DateTime);
                    //        DataTable dt = fillsetupdt("select dtToDate from SetupInfo");

                    //        if (dt.Rows.Count != 0)
                    //        {
                    //            Expirydate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                    //            DateTime todaydate = DateTime.Today;
                    //            TimeSpan timediff = Expirydate - todaydate;
                    //            int result = timediff.Days;
                    //            if (result >= 0)
                    //            {
                    //                clsFunctions.IsEgaleReg = "1";

                    //            }
                    //            else
                    //            {
                    //                clsFunctions.IsEgaleReg = "Your software is expired please renew it. Data will not upload untill software is not renew.";
                    //                _loggerService.LogMessage(LogType.Information, ErrorType.Information, "Your software is expired please renew it. Data will not upload untill software is not renew.");     //BhaveshT
                    //                clsFunctions.regfilestatus = "FileFound";
                    //                Adosetup("update SetupInfo set tStatus='Y'");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            clsFunctions.regfilestatus = "FileFound";
                    //            clsFunctions.IsEgaleReg = "Your software is expired please renew it. Data will not upload untill software is not renew";
                    //            _loggerService.LogMessage(LogType.Information, ErrorType.Information, "Your software is expired please renew it. Data will not upload untill software is not renew");
                    //        }
                    //    }
                    //}
                    if (dept != "")             // BhaveshT 22/12/2023
                    {
                        string device_id = clsFunctions.loadSingleValueSetup("select DeviceID from PlantSetup");
                        if (device_id == lines[0] || device_id == lines[1])
                        {
                            DateTime Expirydate = default(DateTime);
                            //DataTable dt = fillsetupdt("select dtToDate from SetupInfo");
                            DataTable dt = fillsetupdt("select PlantExpiry from PlantSetup");

                            if (dt.Rows.Count != 0)
                            {
                                //Expirydate = Convert.ToDateTime(dt.Rows[0][0].ToString());// check here for expiry
                                //Expirydate = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0][0].ToString()).ToString("MM/dd/yyyy");// check here for expiry

                                var strDate = dt.Rows[0][0].ToString();

                                strDate = strDate.Split(" ".ToCharArray())[0];

                                clsFunctions.ErrorLog("strDate: " + strDate);

                                //----------------------------------------------------

                                try
                                {
                                    clsFunctions.ErrorLog("IsRegister: try - dd/MM/yyyy format: " + strDate);

                                    //Expirydate = Convert.ToDateTime(strDate);// check here for expiry
                                    Expirydate = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                }
                                catch(Exception ex)
                                {
                                    clsFunctions.ErrorLog("IsRegister: catch - dd-MM-yyyy format: " + strDate);

                                    try
                                    {
                                        //Expirydate = Convert.ToDateTime(strDate);// check here for expiry
                                        Expirydate = DateTime.ParseExact(strDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                        clsFunctions.ErrorLog("IsRegister: catch - dd-MM-yyyy format converted: " + strDate);
                                    }
                                    catch (Exception ex2)
                                    {
                                        Expirydate = DateTime.ParseExact(strDate, "M/d/yyyy", CultureInfo.InvariantCulture);

                                        clsFunctions.ErrorLog("IsRegister: catch - M/d/yyyy format converted: " + strDate);
                                    }

                                }

                                //----------------------------------------------------

                                //if (DateTime.TryParse(strDate, out Expirydate) == false)
                                //{
                                    

                                //    //MessageBox.Show("TryParse error" + strDate);
                                //}
                                //else
                                //{
                                //    clsFunctions.ErrorLog("TryParse true = " + Expirydate);
                                //}

                                clsFunctions.ErrorLog("Expirydate: " + Expirydate);

                                DateTime todaydate = DateTime.Today;
                                TimeSpan timediff = Expirydate - todaydate;
                                int result = timediff.Days;
                                if (result >= 0)
                                {
                                    clsFunctions.IsEgaleReg = "1";

                                }
                                else
                                {
                                    clsFunctions.IsEgaleReg = "Your software is expired please renew it. Data will not upload untill software is not renew.";
                                    clsFunctions_comman.ErrorLog("Your software is expired please renew it. Data will not upload untill software is not renew.");     //BhaveshT
                                    clsFunctions.regfilestatus = "FileFound";
                                    //Adosetup("update SetupInfo set tStatus='Y'");
                                }
                            }
                            else
                            {
                                clsFunctions.regfilestatus = "FileFound";
                                clsFunctions.IsEgaleReg = "Your software is expired please renew it. Data will not upload untill software is not renew";
                                clsFunctions_comman.ErrorLog("Your software is expired please renew it. Data will not upload untill software is not renew");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Device ID not matched");
                        }
                    }
                    else if (clsFunctions.IsEgaleReg == null) 
                    {
                        File.Copy(file, file + ".bkp");
                        File.Delete(file);
                        MessageBox.Show("old Config File Found please Try again"); 
                        Application.Restart();
                    }
                    else
                    {
                        File.Delete(file);
                        clsFunctions.IsEgaleReg = "Please register the system";
                        clsFunctions_comman.ErrorLog("Please register the system");     //BhaveshT
                    }

                }
               
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("IsRMCRegister: " + ex.Message);     //BhaveshT
                MessageBox.Show("IsRegister : " + ex.Message);  
            }

            
             return clsFunctions.IsEgaleReg;
             
        }

        private static string[] GetRegDetails(string file)
        {
            string Regname = "";
            string Serialkey = "";
            string deviceId = "";
            string[] lines = System.IO.File.ReadAllLines(file);

            string[] dline = new string[3];

            if (lines.Length  >= 2)
            {
                Regname = clsSecurity.Decrypt(lines[0]);
                Serialkey = clsSecurity.Decrypt(lines[1]);

                
                dline[0] = Regname;
                dline[1] = Serialkey;
            }
            else if (lines.Length >= 1)
            {
                deviceId = clsSecurity.Decrypt(lines[0]);
                dline[0] = deviceId;
            }

            return dline;
        }

        //public static int createRegisterFile(string regName, string SerialKey, string dept)
        public static int createRegisterFile(string regName, string SerialKey, string dept)
        {
            regName = clsSecurity.encrypts(regName);
            SerialKey = clsSecurity.encrypts(SerialKey);
            string filedire = clsFunctions.RegFilePath;
            clsFunctions.chkdire(filedire);
            
            //string filePath = string.Format(filedire + "\\vHotMixScadan.ini");    // original
            string filePath = string.Format(filedire + "\\" + dept +"_Unipro.ini");      // BhaveshT

            bool fileExists = File.Exists(filePath);

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(regName + Environment.NewLine + SerialKey);
            }

            return 1;
        }

        //--------------------

        //---------------------

      
        public static DataTable fillsetupdt(string query)
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed) conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public static int Adosetup(string query)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            return cmd.ExecuteNonQuery();
        }
    }
}
