using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace Uniproject.Classes
{
    class RegisterSoftware
    {
        private static readonly LoggerService _loggerService = new LoggerService();    //BT
        static string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\Setup.mdb;    Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;";
        static OleDbConnection conn = new OleDbConnection(connstr);
        public static string IsEgaleRegister()
        {
            //clsFunctions.ErrorLog11("Inside program - regis software");
            string deviceId = "";
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath  + "\\Database\\Setup.mdb;    Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;";
            OleDbConnection conn = new OleDbConnection(connstr);
            
            try
            {
                //clsFunctions.ErrorLog11("Inside program - regis software - inside try 1 ");
                string dept = clsFunctions.loadSingleValueSetup("select DeptName from PlantSetup");
                if (dept != "0" || dept != "")
                {
                    //------------------------
                    //string deptName = clsFunctions.loadSingleValueSetup("select DeptName from PlantSetup");     // BhaveshT
                    //if (deptName == null || deptName == "" || deptName == "0")
                    //
                    if(dept == "PMC")
                    {
                        deviceId = clsFunctions.loadSingleValueSetup("select DeviceID from PlantSetup");              // BhaveshT


                        if (deviceId == null || deviceId == "" || deviceId == "0")
                        {
                            clsFunctions.IsEgaleReg = "Please register the system";
                            _loggerService.LogMessage(LogType.Information, ErrorType.Information, "Please register the system");     //BhaveshT
                            return clsFunctions.IsEgaleReg;
                        }
                    }
                    else if(dept != "PMC")
                    {

                    }
                    
                }
                else
                    //clsFunctions.ErrorLog11("Inside program - regis software - DeptName not found in plantsetup ");
                    clsFunctions.InsertToErrorLogTable(9730,"DeptName not found in plantsetup");
                //------------------------

                clsFunctions.chkdire(clsFunctions.RegFilePath);
                string file =Convert.ToString( Directory.GetFiles(clsFunctions.RegFilePath, ""+ dept +"_Unipro.ini").FirstOrDefault());
                if (file == null)
                {
                    //clsFunctions.ErrorLog11("Inside program - regis software - reg file == null ");
                    clsFunctions.regfilestatus = "FileNotFound";
                    clsFunctions.IsEgaleReg = "Please register the system";

                    clsFunctions.AdoData_setup("Delete * from PlantSetup");
                    //clsFunctions.AdoData_setup("Delete * from SetupInfo");

                    clsFunctions_comman.ErrorLog("Please register the system");     //BhaveshT
                }
                else
                {
                    string[] lines = GetRegDetails(file);
                    //clsFunctions.ErrorLog11("Inside program - regis software - GetRegDetails");
                    //if (clsFunctions.regName == lines[0] & clsFunctions.serialKey == lines[1])
                    //if (clsFunctions.DeviceID == lines[0])
                    if (deviceId == lines[0])
                    {
                        //clsFunctions.ErrorLog11("Inside program - regis software - if (deviceId == lines[0]) ");

                        DateTime Expirydate = default(DateTime);
                        //DataTable dt = fillsetupdt("select dtToDate from SetupInfo");
                        DataTable dt = fillsetupdt("select PlantExpiry from PlantSetup");

                        if (dt.Rows.Count != 0)
                        {
                            Expirydate = Convert.ToDateTime(dt.Rows[0][0].ToString());
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
                                _loggerService.LogMessage(LogType.Information, ErrorType.Information, "Your software is expired please renew it. Data will not upload untill software is not renew.");     //BhaveshT
                                clsFunctions.regfilestatus = "FileFound";
                                //Adosetup("update SetupInfo set tStatus='Y'");
                            }
                        }
                        else
                        {
                            clsFunctions.regfilestatus = "FileFound";
                            clsFunctions.IsEgaleReg = "Your software is expired please renew it. Data will not upload untill software is not renew.";
                            _loggerService.LogMessage(LogType.Information, ErrorType.Information, "Your software is expired please renew it. Data will not upload untill software is not renew.");     //BhaveshT
                        }
                    }
                    else
                    {
                        File.Delete(file);
                        clsFunctions.IsEgaleReg = "Please register the system";
                        _loggerService.LogMessage(LogType.Information, ErrorType.Information, "Please register the system");     //BhaveshT
                    }
                }
            }
            catch (Exception ex)
            {
                //clsFunctions.ErrorLog11("Inside program - regis software - catch" + ex.Message+"");
                _loggerService.LogMessage(LogType.Information, ErrorType.Information,"IsEgaleRegister: "+ ex.Message);     //BhaveshT
                                                       
            }
            return clsFunctions.IsEgaleReg;
        }

        private static string[] GetRegDetails(string file)
        {
            //clsFunctions.ErrorLog11("Inside program - regis software - GetRegDetails ");
            //string Regname = "";
            //string Serialkey = "";
            string deviceId = "";
            string[] lines = System.IO.File.ReadAllLines(file);
            if (lines.Length >= 2)
            {
                //Regname = Decrypt(lines[0]);
                //Serialkey = Decrypt(lines[1]);
                deviceId = Decrypt(lines[0]);
            }
            string[] dline = new string[2];
            dline[0] = deviceId;
            //dline[0] = Regname;
            //dline[1] = Serialkey;

            return dline;
        }

        //public static int createRegisterFile(string regName, string SerialKey)
        public static int createRegisterFile(string DeviceID, string dept,string pType)
        {
            DeviceID = encrypts(DeviceID);
            //regName = encrypts(regName);
            //SerialKey = encrypts(SerialKey);
            string filedire = clsFunctions.RegFilePath;
            clsFunctions.chkdire(filedire);
            
            //string filePath = string.Format(filedire + "\\"+ dept +"_vHotMixScadan.ini");       //BhaveshT
            string filePath = string.Format(filedire + "\\" + dept + "_"+pType + "_Unipro.ini");       //changes by dinesh
           // string filePath = string.Format(filedire + "\\" + dept + "_Unipro.ini");       //commented by dinesh

            bool fileExists = File.Exists(filePath);

            Thread.Sleep(100);

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                //writer.WriteLine(regName + Environment.NewLine + SerialKey);
                writer.WriteLine(DeviceID);
                clsFunctions_comman.ErrorLog("" + dept + "_" + pType + "_Unipro.ini file created");     //BhaveshT
                
            }

            return 1;
        }
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

  
        public static string Decrypt(string mystring)
        {
            string temp = null;
            int tempascii = 0;
            string tempstring = null;
            char c = '\0';
            // ERROR: Not supported in C#: OnErrorStatement

            for (int X = 0; X <= mystring.Length - 1; X++)
            {
                temp = mystring.Substring (X, 1);
                c = Convert.ToChar(temp);
                tempascii = Convert.ToInt32((c));
                if (tempascii != 72 & tempascii != 77)
                {
                    if (tempascii >= 1 & tempascii <= 256)
                    {
                        //-
                        //            ElseIf tempascii >= 51 And tempascii <= 256 Then
                        //                tempascii = tempascii - 33 '+
                        tempascii = tempascii - 33;
                    }
                }
                //tempascii = tempascii + 33;

                tempstring = tempstring + Convert.ToChar(tempascii);
            }

            return tempstring;
        }

        public static string encrypts(string mystring)
        {
            string temp = null;
            int tempascii = 0;
            string tempstring = null;
            // ERROR: Not supported in C#: OnErrorStatement

            for (int X = 0; X <= mystring.Length - 1; X++)
            {
                temp = mystring.Substring(X, 1);
                tempascii = Convert.ToInt32(Convert.ToChar(temp));
                if (tempascii != 72 & tempascii != 77)
                {
                    if (tempascii >= 1 & tempascii <= 256)
                    {
                        //-
                        //            ElseIf tempascii >= 51 And tempascii <= 256 Then
                        //                tempascii = tempascii - 33 '+
                        tempascii = tempascii + 33;
                    }
                }
                //tempascii = tempascii + 33;
                tempstring = tempstring + Convert.ToChar(tempascii);
            }

            return tempstring;
        }
    }
}
