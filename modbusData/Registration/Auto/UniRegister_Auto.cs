using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.Classes.Model;
using Uniproject.frmRegistration;

////this form is used for software registration and create config file with name as vHotMixScadan.ini and its path is 'C:\Users\Vadmin\AppData\Roaming\vtIEpmLP'
namespace Uniproject
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------

    public partial class UniRegister_Auto : Form
    {
        string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;";
        string plantType = string.Empty;

        public string PC_ipAddress = NetworkInfo.GetIpAddress();
        public string PC_macAddress = NetworkInfo.GetMacAddress();
        public string SelectedPlantDeviceID = "";
        public string SelectedPlantInstallStatus = "";
        public string SelectedPlantValidity = "";
        public string anotherExpiry = "";
        public static bool internetConnection = false;
        string jsonData;

        public captcha captcha = new captcha();

        // for PostInstallationDetails : Allocate to these string to send to API for save  - saveInstallationDetails API
        string installtionDate, installtionTime, macId, ipAddress, installedBy, installationType, deviceIMEINo, departmentName, mobileno, plantCode, conCode;

        public System.Data.DataTable dt_plantData = new System.Data.DataTable();

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public UniRegister_Auto()
        {
            if (!clsFunctions.CheckInternetConnection())
            {
                internetConnection = false;
                MessageBox.Show("Please check your internet Connection!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                internetConnection = true;
            }
            InitializeComponent();
            UpdateCaptchaImage();

            this.KeyDown += new KeyEventHandler(tabRegAuto_KeyDown);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string globalmsg = "";

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void btnOK_Click(object sender, EventArgs e)
        {
            int success = 0;
            if (chkOldRegister.Checked == true)
            {
                (new cmn_frmRegistration()).ShowDialog();
                this.Close();
                return;
            }

            if (cmbInstalledBy.Text == "")
            {
                clsFunctions_comman.UniBox("Please enter installation persons name");
                return;
            }

            //-------------- using API for registration --------------  BhaveshT   16 may

            else if (cmbDeptList.Text != "" && chkOldRegister.Checked == false)
            {
                if (txtDeviceID.Text != "")
                {
                    success = GetRegDetailsFromAPI();       // data fetch from API & inserted in setup.mdb here.
                    if (success == 1)
                    {
                        if (Delete_InsertSetup() == 1)      // .ini file gets created here.
                        {
                            if (clsFunctions.aliasName != "")
                            {
                                cmbAuto_Dept.Text = cmbDeptList.Text;
                                clsFunctions.plantCode = clsFunctions.loadSingleValueSetup("Select PlantCode from PlantSetup where DeviceID='"+txtDeviceID.Text+"'");

                                //getpresetDataForUniPro_Setup();
                                //clsFunctions.AdoData_setup(queryForInsertingDataIntoUniPro_Setup);

                                clsFunctions.GetAndUpdateUniproSetupFromAPI(cmbDeptList.Text, clsFunctions.dept, clsFunctions.plantCode);

                                if (cmbAuto_Dept.Text.Contains("RMC"))
                                {
                                    //getDataHeaderTableSync(cmbAuto_Dept.Text, department, lb_PlantCode.Text);
                                    //getDataTransactionTableSync(cmbAuto_Dept.Text, department, lb_PlantCode.Text);

                                    //clsFunctions.GetDataHeaderTableSync(cmbDeptList.Text, clsFunctions.dept, clsFunctions.plantCode);
                                    //clsFunctions.GetDataTransactionTableSync(cmbDeptList.Text, clsFunctions.dept, clsFunctions.plantCode);
                                }
                            }

                            PostInstallationDetails(cmbInstalledBy.Text, "M");
                            PostInstallationDetailsToMotherAPI(cmbInstalledBy.Text, "M");

                            //MessageBox.Show("Software Register Successfully... Please Restart Application to continue..", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information); 

                            //------------------------------ BhaveshT
                            //MessageBox.Show("Software Register Successfully... Now Please Configure your software...", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clsFunctions_comman.UniBox("Unipro Software Registration for '" + cmbDeptList.Text + "' Completed Successfully... Now Please Configure your software...");

                            globalmsg = "Register successfully";

                            this.Close();
                        }
                        else
                        {
                            clsFunctions_comman.UniBox("Registration : Error in Registration - While creating ini file");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Please enter a valid device ID");
                        clsFunctions_comman.UniBox("Plant Details for entered DeviceID not found!\n Please enter a valid Device ID");
                    }
                }
                else
                {
                    MessageBox.Show("Fill All Fields.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                clsFunctions_comman.UniBox("Please select valid Department.");
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        //----------------------------- using API for registration -------------------------------------

        public string IP = "192.168.1.62";          // Sahil's IP, Port
        public string Port = "8080";    // "2323";

        private int GetRegDetailsFromAPI()        //
        {
            int success = 0;
            try
            {
                //string baseurl = "http://192.168.1.8:8080/getwoapi/getAllocatedWorkOrdersForPlant?plantCode=" + plantCode;
                //string baseurl = "http://" + IP + ":" + Port + "/uniproapi/getPlantDetails?device_id=" + txtDeviceID.Text;

                //string baseurl = "http://" + IP + ":" + Port + "/uniproapi/getPlantDetails?device_id=" + txtDeviceID.Text + "&p_type=A";      // 19/02/2024 - BhaveshT

                string URL = "pmcscada.in";

                clsFunctions.aliasName = cmbDeptList.Text;

                string apiName = "";

                string domain = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='" + cmbDeptList.Text + "'");
                List<(string Department, string PlantType)> parsedList = clsFunctions.ParseAliasNames(cmbDeptList.Text);
                foreach (var tuple in parsedList)
                {
                    department = tuple.Department;
                    plantTypeFromTuple = tuple.PlantType;
                }
                clsFunctions.AdoData_setup("Delete from ServerMapping");
                clsFunctions.AdoData_setup("Delete from ServerMapping_Preset");

                //int status = getPresetDataForApis(department, plantTypeFromTuple, domain);
                int status = clsFunctions.GetAndInsertServerMappingDataFromAPI(cmbDeptList.Text, department, plantTypeFromTuple);

                if (cmbDeptList.Text == "PWD - BT")
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", cmbDeptList.Text);
                else
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress", cmbDeptList.Text);

                apiName = clsFunctions.GetAPINameFromPreset("getPlantDetails", cmbDeptList.Text);
                clsFunctions.dept = department;
                //clsFunctions.dept = clsFunctions.GetAPINameFromPreset("deptName", cmbDeptList.Text);

                //string baseurl = "http://" + URL + "/uniproapi/getPlantDetails?device_id=" + txtDeviceID.Text;
                //string baseurl = "http://" + URL + "/pmc_unipro_rest/work_orders/getPlantDetails?device_id=" + txtDeviceID.Text;

                //string baseurl = "" + URL + "" + apiName + txtDeviceID.Text;          // commented on 19/02/2024
                                
                apiName = apiName.Replace("pType", "A");
                apiName = apiName.Replace("d_id", txtDeviceID.Text.Trim());

                if (cmbDeptList.Text == "PWD - BT")
                {
                    apiName = apiName.Replace("dpt", "VIPL");
                }
                else
                {
                    apiName = apiName.Replace("dpt", department);
                }

                string baseurl = "" + domain + "" + apiName;

                //baseurl = "http://192.168.1.64:8089/Mother_API/PlantSetup/getPlantDetails?device_id=22916121613321312003&p_type=A&dept_name=PMC";

                clsFunctions_comman.ErrorLog("Reg URL:" + baseurl);

                using (var client = new HttpClient())
                {
                    var conName = "";
                    var conCode = "";
                    //var deptTitl
                    var plantValidTo = "";
                    var plantValidFrom = "";
                    var plantType = "";
                    var plantCode = "";
                    var u_id = "";

                    var responseTask = client.GetStringAsync(baseurl);      // HTTP 
                    responseTask.Wait();
                    string result = responseTask.Result;

                    ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(result);

                    if (responseTask.Result == "{\"PlantDetailsList\":[]}")
                    {
                        //MessageBox.Show("Plant Details for entered DeviceID not found!");
                        success = 0;
                    }
                    else
                    {
                        foreach (getRegData plantDetails in responseData.PlantDetailsList)
                        {
                            conName = plantDetails.con_firm_name;
                            conCode = plantDetails.con_reg_no;
                            //deptTitleMain = plantDetails.dept_title_main;
                            plantValidTo = plantDetails.plant_valid_to;
                            plantValidFrom = plantDetails.plant_valid_from;
                            plantType = plantDetails.plant_type;
                            plantCode = plantDetails.plant_reg_no;
                            u_id = plantDetails.u_id;

                        }

                        string plantType1 = plantType;
                        //string plantType1 = GetPlantType(plantType);
                        string dept = clsFunctions.dept;       //cmbDeptList.Text;                //shortDeptName(deptTitleMain);

                        //clsFunctions.dept = dept;

                        //--------------- Prepare for saveInstallationDetails --------------
                        deviceIMEINo = txtDeviceID.Text;
                        departmentName = dept;

                        clsFunctions.dept = dept;
                        //--------------------------------------------------------------------    

                        // 19/02/2024 - BhaveshT : Added parameter: PType in insert query - default value - 'A'

                        clsFunctions.AdoData_setup("Delete * from PlantSetup");


                        string insertToPlantSetup = "Insert Into PlantSetup (ContractorCode, PlantCode, DeptName, DeviceID, ContractorName, PlantType, InstalledBy, InstallationType, MacID, Device_IPAddress, InstallationDate, pType, PlantExpiry, U_ID) " +
                            "values ('" + conCode + "', '" + plantCode + "', '" + dept + "', '" + txtDeviceID.Text + "', '" + conName + "', '" + plantType1 + "', '" + cmbInstalledBy.Text + "', 'M', '" + PC_macAddress + "', '" + PC_ipAddress + "'," +
                            " '" + DateTime.Now.ToString("dd/MM/yyyy") + "', 'A', '" + Convert.ToDateTime(plantValidTo).ToString("dd/MM/yyyy") + "', '" + u_id + "' )";

                        clsFunctions.AdoData_setup(insertToPlantSetup);


                        //clsFunctions.AdoData_setup("Delete * from SetupInfo");
                        //string insertToSetupInfo = "Insert Into SetupInfo (dtFromDate, dtToDate, tStatus) values ('" + plantValidFrom + "', '" + plantValidTo + "', 'N')";
                        //clsFunctions.AdoData_setup(insertToSetupInfo);

                        clsFunctions.InsertInServerMappingAtReg(cmbDeptList.Text, txtDeviceID.Text, plantCode);
                        int updateResultPlantCode = clsFunctions.AdoData_setup("UPDATE ServerMapping SET PlantCode = '" + plantCode + "' WHERE AliasName = '" + cmbDeptList.Text + "'");
                        int updateResultDeviceID = clsFunctions.AdoData_setup("UPDATE ServerMapping SET DeviceID = '" + txtDeviceID.Text + "' WHERE AliasName = '" + cmbDeptList.Text + "'");


                        clsFunctions.InsertToPlantRenewalHistory(txtDeviceID.Text, plantValidFrom, plantValidTo, "0");

                        success = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception at GetRegDetailsFromAPI() : " + ex.Message);
                //clsFunctions.ErrorLog("GetRegDetailsFromAPI() : " + ex.Message);
                clsFunctions.InsertToErrorLogTable(ex.HResult, ex.Message);
                success = 0;
            }
            return success;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public string GetPlantType(string number)
        {
            string plantType;
            switch (number)
            {
                case "1":
                    plantType = "RMC - Reversible Drum Mix";
                    break;
                case "2":
                    plantType = "RMC - Batch Mix";
                    break;
                case "3":
                    plantType = "Bitumen - Drum Mix";       //plantType = "Bituman - Drum Mix";
                    break;
                case "4":
                    plantType = "Bitumen - Batch Mix";      //plantType = "Bituman - Batch Mix";
                    break;
                case "5":
                    plantType = "RMC - Wet Mix";
                    break;
                default:
                    plantType = "Unknown plant type";
                    break;
            }
            return plantType;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public string GetPlantTypeFromAlias(string aliasName)
        {
            string plantType = "";

            if (aliasName.Contains("RMC"))
            {
                plantType = "RMC";
            }
            else if (aliasName.Contains("Bit") || aliasName.Contains("BT"))
            {
                plantType = "BT";
            }
            return plantType;
        }

        public class ResponseData
        {
            public List<getRegData> PlantDetailsList { get; set; }
        }
        //----------------------------------------------------------------------------------------------------

        public int Delete_InsertSetup()
        {
            Thread.Sleep(100);

            int j = 0;
            Boolean flag = true;
            if (clsFunctions.regfilestatus == "FileNotFound")
            {
                flag = true;

                //DataTable dt = RegisterSoftware.fillsetupdt("select * from SetupInfo");
                //if (dt.Rows.Count == 0)
                //    flag = true;
                //else if (dt.Rows[0][0].ToString() == "")
                //    flag = true;
                //else flag = false;
            }
            else
            {
                flag = false;
            }

            if (flag == true)
            {
                Thread.Sleep(100);
                clsFunctions.DeviceID = deviceIMEINo;
                //RegisterSoftware.createRegisterFile(clsFunctions.regName, clsFunctions.serialKey);
                //string department = "";
                //List<(string Department, string PlantType)> parsedList = clsFunctions.ParseAliasNames(cmbAuto_Dept.Text);
                //foreach (var tuple in parsedList)
                //{
                //    department = tuple.Department;
                //   // plantTypeFromTuple = tuple.PlantType;
                //}
                string lbPlanttype = GetPlantTypeFromAlias(lb_PlantType.Text);
                RegisterSoftware.createRegisterFile(clsFunctions.DeviceID, clsFunctions.dept, lbPlanttype);

                j = 1;

                //RegisterSoftware.Adosetup("Delete from SetupInfo");
                //string query = "insert into SetupInfo(dtFromDate,dtToDate,tStatus,iValue) values('" + DateTime.Today.ToString() + "','" + DateTime.Today.AddYears(1).ToString() + "','N','0')";
                //if (RegisterSoftware.Adosetup(query) == 1)
                //{
                //    //DataTable dt1 = clsFunctions.fillDatatable("select * from PlantSetup where plantcode='" + txtplantcode.Text + "'");
                //    DataTable dt1 = clsFunctions.fillDatatable_setup("select * from PlantSetup where plantcode='" + txtplantcode.Text +"'");        //BhaveshT
                //    if (dt1.Rows.Count == 0)
                //    {                       
                //        j = RegisterSoftware.Adosetup("INSERT INTO PlantSetup (ContractorCode,PlantCode,PlantType,DeptName ) VALUES('" + txtRegNo.Text + "','" + txtplantcode.Text + "','"+plantType+"','"+cmbDeptList.Text+"')");
                //    }
                //    else
                //    {
                //        j = 1;
                //    }
                //}
                ////if (j == 1) RegisterSoftware.createRegisterFile(clsFunctions.regName, clsFunctions.serialKey);
                //if (j == 1) RegisterSoftware.createRegisterFile(clsFunctions.DeviceID, clsFunctions.dept);
            }
            else
            {
                Thread.Sleep(100);
                clsFunctions.DeviceID = deviceIMEINo;
                //RegisterSoftware.createRegisterFile(clsFunctions.regName, clsFunctions.serialKey);
                if(lb_PlantType.Text == "-")
                {
                    lb_PlantType.Text = clsFunctions.loadSingleValueSetup("Select PlantType from PlantSetup where DeviceID='" + txtDeviceID.Text.Trim() + "'");
                }
                string lbPlanttype = GetPlantTypeFromAlias(lb_PlantType.Text);
                //RegisterSoftware.createRegisterFile(clsFunctions.DeviceID, clsFunctions.dept, lb_PlantType.Text);
                RegisterSoftware.createRegisterFile(clsFunctions.DeviceID, clsFunctions.dept, lbPlanttype);

                j = 1;
            }
            return j;
        }
        
        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        /*
        
        21/12/2023 - BhaveshT

        Creating API based Unipro software registration for all Departments Using Registered mobile no.
		This method will be known as AUTO Registration, and existing registration method is called as MANUAL Registration
                 
         */

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    //clsPatch.CreateNewColumns();
                }
                catch
                {

                }

                clearAuto_label();
                btnGetOTP.Enabled = false;
                pnlAPIData.BackColor = Color.Lime;
                pnlAPIData.BackColor = Color.FromArgb(210, 255, 255, 255); // 210 is the opacity value (0-255)

                sw_version.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                //clsFunctions.AdoData_setup("Delete * from ServerMapping");
                //clsFunctions.regURL = clsFunctions.loadSingleValueSetup("Select ipAddress from ServerMapping where deptname = 'AutoReg'");

                //---- 26/12/2023 : BhaveshT - To creaete new columns ---------------------------------------

                clsFunctions.checkNewColumnInSetup("PlantName", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("Address", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("MobNo", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("InstalledBy", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("MacID", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("Device_IPAddress", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("InstallationType", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("InstallationDate", "DateTime", "PlantSetup");

                // 16/02/2024 - BhaveshT : create pType column with default value
                clsFunctions.checknewcolumnWithDefaultValInSetupDB("pType", "Text(1)", "PlantSetup", "A");

                //clsFunctions.UpdateApiNamesInServerMappingAndPresetForPWD_BT();

                tabRegAuto.SelectedTab = autoRegPage;

                captcha = new captcha();
                captcha.Captcha(6);
                UpdateCaptchaImage();

                // for Manual
                //clsFunctions.FillCombo("SELECT worktype from tblworktype", cmbDeptList);
                clsFunctions.FillCombo_setup("SELECT DISTINCT AliasName from AliasName", cmbDeptList);
                // clsFunctions.FillCombo_setup("SELECT DISTINCT AliasName from ServerMapping_Preset", cmbDeptList);

                // for Auto
                //clsFunctions.FillCombo("SELECT worktype from tblworktype", cmbAuto_Dept);
                clsFunctions.FillCombo_setup("SELECT DISTINCT AliasName from AliasName", cmbAuto_Dept);
                // clsFunctions.FillCombo_setup("SELECT DISTINCT AliasName from ServerMapping_Preset", cmbAuto_Dept);


                // Fetch IP address and MAC address
                PC_ipAddress = NetworkInfo.GetIpAddress();
                PC_macAddress = NetworkInfo.GetMacAddress();
                lb_macId.Text = PC_macAddress;

                AddColumnsTo_dt_plantData();

                clsFunctions.FillCombo_setup("Select Name from Installation_Person", cmbInstalledBy);
                clsFunctions.FillCombo_setup("Select Name from Installation_Person", cmbInstalledBy_Auto);
            }
            catch
            {

            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public void clearAuto_label()
        {
            lb_ConName.Text = "-";
            lb_ConCode.Text = "-";
            lb_PlantName.Text = "-";
            lb_PlantCode.Text = "-";
            lb_PlantType.Text = "-";
            lb_DeviceID.Text = "-";
            lb_validFromDate.Text = "-";
            lb_validTillDate.Text = "-";
            lb_Address.Text = "-";
            txt_Address.Text = "-";
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        string OTP_Received = "";

        private void btnAuto_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void ValidateMobileNumber()
        {
            string mobileNumber = txtAuto_MobileNo.Text;

            // Define a regular expression for a valid mobile number
            Regex mobileRegex = new Regex(@"^[0-9]{10}$");

            if (mobileRegex.IsMatch(mobileNumber) && mobileNumber.Length == 10)
            {
                // Valid mobile number
                mobileErrorLabel.Text = string.Empty;
            }
            else
            {
                // Invalid mobile number
                mobileErrorLabel.Text = "Invalid mobile number. Please enter 10 digits.";
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void UpdateCaptchaImage()
        {
            var captchaImage = captcha.GenerateCaptchaImage(pictureBoxCaptcha.Width, pictureBoxCaptcha.Height);
            pictureBoxCaptcha.Image = captchaImage;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void manualRegPage_Click(object sender, EventArgs e)
        {

        }

        private void cmbDeptList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void autoRegPage_Click(object sender, EventArgs e)
        {

        }

        private void tabRegAuto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)116 && ModifierKeys.HasFlag(Keys.Control))
            {
                // Your action for Ctrl + F5 here
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void txtDeviceID_TextChanged(object sender, EventArgs e)
        {
            // Retrieve the text from the TextBox
            string inputText = txtDeviceID.Text;

            // Remove non-numeric characters
            string numericInput = Regex.Replace(inputText, "[^0-9]", "");

            // Limit the length
            if (numericInput.Length > 30)
                numericInput = numericInput.Substring(0, 30);

            // Update the value in the TextBox
            txtDeviceID.Text = numericInput;

            // Set the cursor position at the end of the text
            txtDeviceID.SelectionStart = txtDeviceID.TextLength;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void btnAuto_Verity_Click(object sender, EventArgs e)
        {
            // verifyRegistration API will be called here - get Plant details against Mobile No. 

            string userInput = captchaAnswer.Text.Trim();

            if (captcha.ValidateCaptcha(userInput))
            {
                //MessageBox.Show("Captcha is correct!");
                //captcha = new captcha(); // Regenerate a new CAPTCHA after successful validation
                //UpdateCaptchaImage();
            }
            else
            {
                MessageBox.Show("Captcha is incorrect. Please try again.");
                captchaAnswer.Clear();
                //captcha = new captcha(); // Regenerate a new CAPTCHA after unsuccessful validation
                //UpdateCaptchaImage();
                return;
            }

            try
            {
                if (OTP_Received == txtAuto_OTP.Text)
                {
                    captchaAnswer.Enabled = false;
                    txtAuto_OTP.Enabled = false;
                    string mobNo = txtAuto_MobileNo.Text;
                    mobNo = mobNo.Trim();
                    GetPlantDataForMobileNo(mobNo);
                    ValidateExpiry();

                    pnlAPIData.Visible = true;
                    btnAuto_Verity.Enabled = false;
                    //// by dinesh
                    //getPresetDataForApis(cmbAuto_Dept.Text, PlantTypeRecievedFromApi,clsFunctions.regURL.Trim());
                    //getpresetDataForUniPro_Setup();
                    //getDataHeaderTableSync();
                }
                else
                {
                    clsFunctions_comman.UniBox("Please enter valid OTP");
                    clsFunctions_comman.ErrorLog("Please enter valid OTP : Inccorrect OTP - " + txtAuto_OTP.Text);
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at btnAuto_Verity_Click : " + ex);
                clsFunctions_comman.ErrorLog("Exception at btnAuto_Verity_Click : " + ex);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public void ValidateExpiry()
        {
            if (lb_validTillDate.Text != "" || lb_validTillDate.Text != "")
            {
                DateTime todayDate = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);

                string renewalDt = Convert.ToDateTime(lb_validTillDate.Text).ToString("dd/MM/yyyy");

                DateTime renewalDate = DateTime.ParseExact(renewalDt, "dd/MM/yyyy", null);

                TimeSpan difference = renewalDate - todayDate;
                int daysDifference = difference.Days;

                //lb_validTillDate.Text = renewalDt;

                if (renewalDate < todayDate)
                {
                    clsFunctions_comman.UniBox("Plant is expired, select another plant");
                    btnAuto_Confirm.Enabled = false;
                    btnGetOTP.Enabled = true;
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        //List<(string Department, string PlantType)> parsedList;
        string department = "";
        string plantTypeFromTuple = "";
        private void btnGetPlant_Click(object sender, EventArgs e)
        {
            try
            {
                clsFunctions.aliasName = cmbAuto_Dept.Text;

                // added by Dinesh on 14.05.24
                if (cmbAuto_Dept.SelectedIndex != -1)
                {
                    btnGetPlant.Enabled = false;
                    if (txtAuto_MobileNo.Text != "" || txtAuto_MobileNo.Text.Length == 10)
                    {
                        // by dinesh
                        
                        List<(string Department, string PlantType)> parsedList = clsFunctions.ParseAliasNames(cmbAuto_Dept.Text);
                        foreach (var tuple in parsedList)
                        {
                            department = tuple.Department;
                            plantTypeFromTuple = tuple.PlantType;
                        }
                        
                        string domain = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='"+cmbAuto_Dept.Text+"'");

                        clsFunctions.AdoData_setup("Delete From ServerMapping");
                        clsFunctions.AdoData_setup("Delete From ServerMapping_Preset");

                        //int status = getPresetDataForApis(department, plantTypeFromTuple, domain);
                        int status = clsFunctions.GetAndInsertServerMappingDataFromAPI(cmbAuto_Dept.Text, department, plantTypeFromTuple);

                        if (status == 0)
                        {
                            MessageBox.Show("Preset Data couldn't be inserted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (cmbAuto_Dept.Text == "PWD - BT")
                            clsFunctions.regURL = clsFunctions.GetAPINameFromPreset("ipaddress1", cmbAuto_Dept.Text);
                        else
                            clsFunctions.regURL = clsFunctions.GetAPINameFromPreset("ipaddress", cmbAuto_Dept.Text);

                        string p = GetPlantTypeFromAlias(cmbAuto_Dept.Text);

                        GetPlantInstallDetails(cmbAuto_Dept.Text, txtAuto_MobileNo.Text, lb_macId.Text, p);

                        if (dt_plantData.Rows.Count != 0)
                        {
                            cmbPlantList.Items.Clear();
                            clsFunctions.FillComboBoxFromColumn(cmbPlantList, dt_plantData, "plant_name");
                        }
                    }
                    else
                    {
                        clsFunctions_comman.UniBox("Please enter valid Registered Mobile No.");
                    }
                }
                else
                    MessageBox.Show("Kindly select Department.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {
                //clsFunctions_comman.UniBox("Exception at UniRegistration_Auto : btnGetOTP_Click - " + ex);

                clsFunctions_comman.UniBox("Details not found for entered Mobile No. - " + ex);
                clsFunctions_comman.ErrorLog("Exception at UniRegistration_Auto : btnGetOTP_Click - " + ex);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void btnGetOTP_Click(object sender, EventArgs e)
        {
            // sendSMS API will be called here - send Registered mobileNo and get OTP by SMS on that mobile no.

            try
            {
                if (txtAuto_MobileNo.Text != "" || txtAuto_MobileNo.Text.Length == 10)
                {
                    string mobNo = txtAuto_MobileNo.Text;

                    OTP_Received = GetOTP_Against_MobileNo(mobNo);

                    if (OTP_Received == "0")
                    {
                        clsFunctions_comman.UniBox("Mobile No. " + txtAuto_MobileNo.Text + " is not Registered. \n\nPlease Enter Registered Mobile No.");
                        return;
                    }

                    receivedOTP.Text = OTP_Received;
                    if (OTP_Received != "")
                    {
                        txtAuto_OTP.Enabled = true;
                        btnGetOTP.Enabled = false;
                        pnlAfterOTPBtn.Enabled = true;
                        btnAuto_Verity.Enabled = true;
                        cmbAuto_Dept.Enabled = false;
                        txtAuto_MobileNo.Enabled = false;
                    }
                }
                else
                {
                    clsFunctions_comman.UniBox("Please enter valid Registered Mobile No.");
                }
            }
            catch (Exception ex)
            {
                //clsFunctions_comman.UniBox("Exception at UniRegistration_Auto : btnGetOTP_Click - " + ex);

                clsFunctions_comman.UniBox("Details not found for entered Mobile No. - " + ex);
                clsFunctions_comman.ErrorLog("Exception at UniRegistration_Auto : btnGetOTP_Click - " + ex);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void tabRegAuto_KeyDown(object sender, KeyEventArgs e)
        {
            //------------------- for Auto/Manual Reg form switching ------------------------
            if (e.Control && e.KeyCode == Keys.Space)
            {

                if (tabRegAuto.SelectedTab == autoRegPage)
                    tabRegAuto.SelectedTab = manualRegPage;

                else if (tabRegAuto.SelectedTab == manualRegPage)
                    tabRegAuto.SelectedTab = autoRegPage;
            }

            //--------------------- For OTP VIsibility ----------------------
            if (e.Alt && e.KeyCode == Keys.F5)
            {
                if (receivedOTP.Visible == false)
                {
                    receivedOTP.Visible = true;
                }
                else if (receivedOTP.Visible == true)
                {
                    receivedOTP.Visible = false;
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void txtAuto_MobileNo_TextChanged_1(object sender, EventArgs e)
        {
            ValidateMobileNumber();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void cmbAuto_Dept_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            btnGetPlant.Enabled = true;
            btnGetOTP.Enabled = false;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void cmbPlantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbPlantList.SelectedIndex>-1)
                btnGetOTP.Enabled = true;
            SelectedPlantDeviceID = GetDeviceIdFromPlantData(cmbPlantList.Text);
            
            SelectedPlantInstallStatus = GetInstallStatusFromPlantData(cmbPlantList.Text);

            if (SelectedPlantInstallStatus == "Y")
            {
                clsFunctions_comman.UniBox("Software is already installed for selected plant: " + cmbPlantList.Text);

                btnGetOTP.Enabled = false;
                txtAuto_MobileNo.Enabled = true;

                txtAuto_OTP.Enabled = false;

                btnGetPlant.Enabled= true;
                cmbPlantList.SelectedIndex = -1;
                SelectedPlantInstallStatus = "";

            }
            else if (SelectedPlantInstallStatus == "N" || SelectedPlantInstallStatus == "")
            {
                btnGetOTP.Enabled = true;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        private string GetDeviceIdFromPlantData(string selectedPlantName)
        {

            string deviceId = "";

            // Iterate through each row in the DataTable
            foreach (DataRow row in dt_plantData.Rows)
            {
                // Check if the plant_name column matches the selected plant name
                if (row["plant_name"].ToString() == selectedPlantName)
                {
                    // Retrieve the deviceId from the corresponding row
                    deviceId = row["device_imei_no"].ToString();
                    break; // Exit the loop once deviceId is found
                }
            }
            return deviceId;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        private string GetInstallStatusFromPlantData(string selectedPlantName)
        {

            string status = "";

            foreach (DataRow row in dt_plantData.Rows)
            {
                if (row["plant_name"].ToString() == selectedPlantName)
                {
                    status = row["installation_status"].ToString();
                    break;
                }
            }

            return status;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void btnAuto_Confirm_Click(object sender, EventArgs e)
        {

            // saveInstallationDetails API will be called here - will send all installation details through API
            try
            {
                if (cmbInstalledBy_Auto.Text == "")
                {
                    clsFunctions_comman.UniBox("Please select installation person name");
                    return;
                }
                if (chkOldReg_Auto.Checked == true)
                {
                    (new cmn_frmRegistration()).ShowDialog();
                    this.Close();
                    return;
                }
                //----------------------------
                else
                {
                    int a = StoreAutoRegDataFromInDatabase();
                    if (a == 1)
                    {
                        if (Delete_InsertSetup() == 1)      // To create dept_Unipro.ini file
                        {
                            if (clsFunctions.aliasName != "")
                            {
                                //int result = clsFunctions.AdoData_setup(queryforServerMappingPreset);
                                //StoreAutoRegDataFromInDatabase();

                                //getpresetDataForUniPro_Setup();
                                int result = clsFunctions.GetAndUpdateUniproSetupFromAPI(cmbAuto_Dept.Text, department, lb_PlantCode.Text);
                                if (result == 0)
                                {
                                    MessageBox.Show("Unipro_Setup data couldn't be inserted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                if (result == 2)
                                {
                                    MessageBox.Show("Unipro_Setup data couldn't be found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                if (cmbAuto_Dept.Text.Contains("RMC"))
                                {
                                    //getDataHeaderTableSync(cmbAuto_Dept.Text,department,lb_PlantCode.Text);
                                    //getDataTransactionTableSync(cmbAuto_Dept.Text, department, lb_PlantCode.Text);

                                    //clsFunctions.GetDataHeaderTableSync(cmbAuto_Dept.Text, department, lb_PlantCode.Text);
                                    //clsFunctions.GetDataTransactionTableSync(cmbAuto_Dept.Text, department, lb_PlantCode.Text);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Preset Data couldn't be inserted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            PostInstallationDetails(cmbInstalledBy_Auto.Text, "A");
                            PostInstallationDetailsToMotherAPI(cmbInstalledBy.Text, "A");

                            clsFunctions_comman.UniBox("Unipro Software registered successfully");
                            clsFunctions_comman.ErrorLog("Unipro Software registered successfully");
                            clsFunctions.IsEgaleReg = "1";
                            globalmsg = "Register successfully";

                            this.Close();
                        }
                        else
                        {
                            clsFunctions_comman.UniBox("Unipro Software registration unsuccessfull : error While creating ini file");
                            clsFunctions_comman.ErrorLog("Unipro Software registration unsuccessfull : error While creating ini file");
                        }
                    }
                    else
                    {
                        clsFunctions_comman.UniBox("Unipro Software registration unsuccessful : error While inserting data in database");
                        clsFunctions_comman.ErrorLog("Unipro Software registration unsuccessful : error While inserting data in database");
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Unipro Software registration unsuccessful - Catch : " + ex.Message);
                clsFunctions_comman.ErrorLog("Unipro Software registration unsuccessfull : Catch : " + ex.Message);

            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void lb_macId_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void chkOldRegister_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void pnlAfterOTPBtn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void lblDeviceID_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOldReg_Auto.Checked == true)
            {
                btnAuto_Verity.Enabled = false;
                btnAuto_Confirm.Enabled = true;

                pnlAfterOTPBtn.Enabled = false;
                cmbInstalledBy_Auto.Enabled = true;
                btnAuto_Exit.Enabled = true;

            }

            else if (chkOldReg_Auto.Checked == false)
            {
                pnlAfterOTPBtn.Enabled = true;
                btnAuto_Confirm.Enabled = false;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void mobileErrorLabel_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxCaptcha_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void pnlAPIData_Paint(object sender, PaintEventArgs e)
        {

        }

        private void receivedOTP_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void captchaAnswer_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabRegAuto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAuto_OTP_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void txtAuto_InstalledBy_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void lb_validTillDate_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void lb_validFromDate_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void lb_Address_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void lb_DeviceID_Click(object sender, EventArgs e)
        {

        }

        private void lb_PlantType_Click(object sender, EventArgs e)
        {

        }

        private void lb_ConName_Click(object sender, EventArgs e)
        {

        }

        private void lb_PlantCode_Click(object sender, EventArgs e)
        {

        }

        private void lb_ConCode_Click(object sender, EventArgs e)
        {

        }

        private void lb_PlantName_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        /*
          21/12/2023 - BhaveshT
          GetPlantDataForMobileNo(string mobileNo) method gets mobileN as parameter and calls API : verifyRegistration
          and gets Plant details against mobile No for registration
         */

        public async void GetPlantDataForMobileNo(string mobileNo)
        {
            //string apiUrl = $"http://192.168.1.13:8080/gtambGetWorkIdAPI/verifyRegistration?regMobNo={mobileNo}";

            //string verifyApiName = getAutoRegVerifyApi();
            string verifyApiName = clsFunctions.GetAPINameFromPreset("AutoReg_Verify", cmbAuto_Dept.Text);

            //clsFunctions.URL = "192.168.1.13:8080";
            //clsFunctions.URL = "pmcscada.in";

            //string apiUrl = "" + clsFunctions.protocol + "://" + clsFunctions.regURL + verifyApiName + mobileNo;
            string apiUrl = clsFunctions.regURL + verifyApiName + SelectedPlantDeviceID;

            // For Test
            //apiUrl = "http://192.168.1.64:8089/UniPro_Rest/plant_reg_history/verifyRegistration?device_id=22916121613321312003";

            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetStringAsync(apiUrl);
                    response.Wait();

                    //if (response.IsSuccessStatusCode) // remove this statement or change the logic
                    {
                        string jsonResponse = response.Result;   //string jsonResponse = await response.Content.ReadAsStringAsync();
                        ParseJsonResponse(jsonResponse);
                        btnAuto_Confirm.Enabled = true;
                    }
                    clsFunctions.AdoData_setup("update ServerMapping_Preset set DeviceID='" + SelectedPlantDeviceID + "', PlantCode='" + lb_PlantCode.Text + "', PlantExpiryDate='"+ lb_validTillDate.Text + "' where AliasName='" + cmbAuto_Dept.Text+"'"); // update plant and deviceid

                    //else
                    //{
                    //    // Handle error
                    //    clsFunctions_comman.UniBox($"Error at verifyRegistration: {response.StatusCode}");
                    //    clsFunctions_comman.ErrorLog($"Error at verifyRegistration: {response.StatusCode}");
                    //}
                }
                catch (Exception ex)
                {
                    clsFunctions_comman.UniBox($"Exception at verifyRegistration: {ex.Message}");
                    clsFunctions_comman.ErrorLog($"Exception at verifyRegistration: {ex.Message}");
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        
        public void ParseJsonResponse(string jsonResponse)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<PlantDetailsResponse>(jsonResponse);

                if (result != null && result.PlantDetails.Count > 0)
                {
                    var plantDetails = result.PlantDetails[0];

                    // Access individual values
                    string conName = plantDetails.con_name;
                    string plantAddress = plantDetails.plant_address;
                    deviceIMEINo = plantDetails.device_imei_no;
                    string validTo = plantDetails.valid_to;
                    string validFrom = plantDetails.valid_from;
                    anotherExpiry = plantDetails.plant_another_expiry;
                    plantCode = plantDetails.plant_code;
                    conCode = plantDetails.con_code;
                    string plantType = plantDetails.plant_type;
                    string plantName = plantDetails.plant_name;
                    string plant_u_id = plantDetails.u_id;

                    // Use the values as needed
                    // For example, display in textboxes or store in variables

                    lb_ConName.Text = conName;
                    lb_ConCode.Text = conCode;
                    lb_PlantName.Text = plantName;
                    lb_PlantCode.Text = plantCode;
                    lb_PlantType.Text = plantType;
                    lb_DeviceID.Text = deviceIMEINo;
                    lb_validFromDate.Text = validFrom;
                    lb_validTillDate.Text = validTo;
                    lb_Address.Text = plantAddress;
                    txt_Address.Text = plantAddress;
                    lb_UID.Text = plant_u_id;

                }
                else
                {
                    clsFunctions_comman.UniBox("verifyRegistration : No data found for entered mobile no.");
                    clsFunctions_comman.ErrorLog("verifyRegistration : No data found for entered mobile no.");
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox($"verifyRegistration : Error parsing JSON: {ex.Message}");
                clsFunctions_comman.ErrorLog($"verifyRegistration : Error parsing JSON: {ex.Message}");
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public class PlantDetail
        {
            [JsonProperty("con_name")]
            public string con_name { get; set; }

            [JsonProperty("plant_address")]
            public string plant_address { get; set; }

            [JsonProperty("device_imei_no")]
            public string device_imei_no { get; set; }

            [JsonProperty("valid_to")]
            public string valid_to { get; set; }

            [JsonProperty("valid_from")]
            public string valid_from { get; set; }

            [JsonProperty("plant_another_expiry")]
            public string plant_another_expiry { get; set; }

            [JsonProperty("plant_code")]
            public string plant_code { get; set; }

            [JsonProperty("con_code")]
            public string con_code { get; set; }

            [JsonProperty("plant_type")]
            public string plant_type { get; set; }

            [JsonProperty("plant_name")]
            public string plant_name { get; set; }

            [JsonProperty("u_id")]
            public string u_id { get; set; }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public class PlantDetailsResponse
        {
            [JsonProperty("PlandtDetails")]
            public List<PlantDetail> PlantDetails { get; set; }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public string GetOTP_Against_MobileNo(string mobNo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string SMSapiName = clsFunctions.GetAPIName("AutoReg_SMS", cmbAuto_Dept.Text);
                    // http://localhost:8089/UniPro_Rest/plant_reg_history/sendSMS?regMobNo=9822908911&device_id=2291612161331813311002&installation_status=N

                    //string SMSapiName = getOTPApis(cmbAuto_Dept.Text).Trim();
                    string SMSapiName = clsFunctions.GetAPINameFromPreset("AutoReg_SMS", cmbAuto_Dept.Text); // commented by dinesh

                    // Create the full URL with query parameters
                    //string fullUrl = $"{baseUrl}?regMobNo={mobNo}";

                    //clsFunctions.URL = "192.168.1.13:8080";
                    //clsFunctions.URL = "pmcscada.in";

                    //string fullUrl = "" + clsFunctions.protocol + "://" + clsFunctions.URL + SMSapiName + mobNo;

                    mobNo = mobNo.Trim();

                    //SMSapiName = SMSapiName.Replace("mobNo", mobNo);
                    //SMSapiName = SMSapiName.Replace("dId", SelectedPlantDeviceID);
                    //SMSapiName = SMSapiName.Replace("flag", "N");

                    SMSapiName = SMSapiName.Replace("mobileNo", mobNo);
                    SMSapiName = SMSapiName.Replace("deviceId", SelectedPlantDeviceID);

                    var status = dt_plantData.Rows[0]["installation_status"].ToString();

                    SMSapiName = SMSapiName.Replace("installationStatus", status);

                    string fullUrl = "" + clsFunctions.regURL + "" + SMSapiName;
                    fullUrl = fullUrl.Replace("\"", "");

                    // For Testing
                    //fullUrl = "http://192.168.1.64:8089/UniPro_Rest/plant_reg_history/sendSMS?regMobNo=8669977679&device_id=22916121613321312003&installation_status=N";
                    //string baseurl = "" + clsFunctions.protocol + "://" + clsFunctions.URL + plantValidityApi + plantCode;

                    // Make the GET request
                    HttpResponseMessage response = client.GetAsync(fullUrl).Result; // Use .Result to block until the task completes

                    // Check if the request was successful (status code 200-299)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseContent = response.Content.ReadAsStringAsync().Result;

                        string otpNo = responseContent.Split(':')[1].Trim('}');

                        if (otpNo.Contains("Already Installed"))
                        {

                        }

                        return otpNo;
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them as needed
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        // PostInstallationDetails to Departmental REST API

        public async Task<string> PostInstallationDetails(string installBy, string installType)
        {
            string URL = "";
            string saveInstallationApiName = "";

            if (cmbDeptList.Text == "")
                cmbDeptList.Text = cmbAuto_Dept.Text;

            if (cmbAuto_Dept.Text == "")
                cmbAuto_Dept.Text = cmbDeptList.Text;

            if (cmbDeptList.Text == "PWD - BT")
                URL = clsFunctions.GetAPINameFromPreset("ipaddress1", cmbDeptList.Text);
            else
                URL = clsFunctions.GetAPINameFromPreset("ipaddress", cmbDeptList.Text);

            if (installType == "M")
            {
                saveInstallationApiName = clsFunctions.GetAPINameFromPreset("AutoReg_Save", cmbDeptList.Text);
                //URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", cmbDeptList.Text);
            }
            else if (installType == "A")
            {
                saveInstallationApiName = clsFunctions.GetAPINameFromPreset("AutoReg_Save", cmbAuto_Dept.Text);
                //URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", cmbAuto_Dept.Text);
            }

            //clsFunctions.URL = "192.168.1.13:8080";
            //clsFunctions.URL = "pmcscada.in";

            //string apiUrl = "" + clsFunctions.protocol + "://" + clsFunctions.regURL + saveInstallationApiName;
            //--------------------------------

            string apiUrl = URL + saveInstallationApiName;
            //--------------------------------
            //string apiUrl = "http://192.168.1.13:8080/gtambGetWorkIdAPI/saveInstallationDetails";

            // Extract data from labels or other UI elements

            installtionDate = DateTime.Now.ToString("dd-MM-yyyy"); // Replace with the actual label name
            installtionTime = DateTime.Now.ToString("HH:mm:ss");
            macId = PC_macAddress;
            ipAddress = PC_ipAddress;
            installedBy = installBy;
            installationType = installType;
            //deviceIMEINo = lb_DeviceID.Text;
            //departmentName = cmbAuto_Dept.Text;
            mobileno = txtAuto_MobileNo.Text;
            //plantCode = lb_PlantCode.Text;
            //conCode = lb_ConCode.Text;

            //---------------------------------------------------------------------------
            // Prepare the JSON data for (A) Auto Registration

            if (installationType == "A")
            {
                //departmentName = cmbDeptList.Text;

                jsonData = $@"
                {{  
                    ""plantCode"":""{lb_PlantCode.Text}"",
                    ""conCode"":""{lb_ConCode.Text}"",
                    ""installtionDate"":""{installtionDate}"",
                    ""installtionTime"":""{installtionTime}"",
                    ""installedBy"":""{installedBy}"",
                    ""macId"":""{macId}"",
                    ""ipAddress"":""{ipAddress}"",
                    ""mobileNo"":""{mobileno}"",
                    ""installationType"": ""{installationType}"",
                    ""deviceIMEINo"":""{lb_DeviceID.Text}"",
                    ""departmentName"":""{clsFunctions.dept}"",
                    ""installationStatus"":""Y""
                }}";
            }

            //---------------------------------------------------------------------------
            // Prepare the JSON data for (M) Manual Registration

            else if (installationType == "M")
            {
                jsonData = $@"
                {{
                    ""installtionDate"":""{installtionDate}"",
                    ""installtionTime"":""{installtionTime}"",
                    ""macId"":""{macId}"",
                    ""ipAddress"":""{ipAddress}"",
                    ""installedBy"":""{installedBy}"",
                    ""installationType"": ""{installationType}"",
                    ""deviceIMEINo"":""{deviceIMEINo}"",
                    ""departmentName"":""{departmentName}"",
                    ""installationStatus"":""Y""
                }}";
            }
            //-----------------------------------------

            using (HttpClient client = new HttpClient())
            {
                // Set the content type
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Create the HTTP content with the JSON data
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    // Send the POST request
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Get the response content as a string
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful response
                        if (responseContent == "{\"Not-Saved\":0}")
                        {
                            //clsFunctions_comman.UniBox($"Installation Details not saved, invalid Device ID : ('" + installationType + "')");
                            clsFunctions_comman.ErrorLog($"UNIPRO_REST: Installation Details not saved, invalid Device ID : ('" + installationType + "')");
                        }
                        else
                        {
                            clsFunctions_comman.ErrorLog($"UNIPRO_REST: Installation Details saved successfully : ('" + installationType + "')");
                            //clsFunctions_comman.UniBox($"Installation Details saved successfully : '" + installationType + "'");
                        }
                        return responseContent;
                    }
                    else
                    {
                        // Handle error
                        if ((response.IsSuccessStatusCode == false))
                        {
                            //clsFunctions_comman.UniBox($"Connection Close at saveInstallationDetails: {response.StatusCode}, {response.Headers}");
                            return string.Empty;
                        }
                        //clsFunctions_comman.UniBox($"Error at saveInstallationDetails: {response.StatusCode}");
                        clsFunctions_comman.ErrorLog($"UNIPRO_REST: Error at saveInstallationDetails: {response.StatusCode}");

                        clsFunctions_comman.ErrorLog($"UNIPRO_REST: Error Response: {responseContent}");
                        return string.Empty; // or any default value indicating an error
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    //clsFunctions_comman.UniBox($"Exception at saveInstallationDetails: {ex.Message}");
                    clsFunctions_comman.ErrorLog($"UNIPRO_REST: Exception at saveInstallationDetails: {ex}");
                    return string.Empty; // or any default value indicating an exception
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        // 25/12/2023 : BhaveshT - Store details into PlantSetup and SetupInfo table of Setup.mdb
        public int StoreAutoRegDataFromInDatabase()
        {
            try
            {
                clsFunctions.dept = clsFunctions.GetAPINameFromPreset("deptName", cmbAuto_Dept.Text);

                //------------------------ 10/04/2024 : BhaveshT - Additional fields inserted :  ------------------------
                // 19/02/2024 - BhaveshT : Added parameter: PType in insert query - default value - 'A'
                
                if(anotherExpiry == "" || anotherExpiry == "-" || anotherExpiry == "0")
                {
                    anotherExpiry = Convert.ToDateTime(lb_validTillDate.Text).ToString("dd/MM/yyyy");
                }

                clsFunctions.AdoData_setup("Delete * from PlantSetup");

                string insertToPlantSetup = "Insert Into PlantSetup (ContractorCode, PlantCode, DeptName, DeviceID, ContractorName, PlantType, PlantName, Address, MobNo, InstalledBy, MacID, Device_IPAddress, InstallationType, InstallationDate, pType, PlantExpiry, AnotherExpiry, U_ID) " +
                    "values ( '" + lb_ConCode.Text + "' , '" + lb_PlantCode.Text + "', '" + clsFunctions.dept + "', '" + lb_DeviceID.Text + "', '" + lb_ConName.Text + "', '" + lb_PlantType.Text + "', '" + lb_PlantName.Text + "' , '" + lb_Address.Text + "'," +
                    " '" + txtAuto_MobileNo.Text + "' , '" + cmbInstalledBy_Auto.Text + "', '" + PC_macAddress + "', '" + PC_ipAddress + "', 'A', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', 'A', '" + Convert.ToDateTime(lb_validTillDate.Text).ToString("dd/MM/yyyy") + "',  '" + Convert.ToDateTime(anotherExpiry).ToString("dd/MM/yyyy") + "', '" + lb_UID.Text + "')";
                
                int a = clsFunctions.AdoData_setup(insertToPlantSetup);
                
                
                ////-----------------------------------------------
                //clsFunctions.AdoData_setup("Delete * from PlantSetup");

//                //// Ensure date strings are in valid format
                //DateTime validTillDate, anotherExpiryDate;
                //bool isValidTillDate = DateTime.TryParseExact(lb_validTillDate.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out validTillDate);
                //bool isAnotherExpiryDate = DateTime.TryParseExact(anotherExpiry, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out anotherExpiryDate);

//                //if (!isValidTillDate || !isAnotherExpiryDate)
                //{
                //    throw new InvalidOperationException("One or more date strings are not recognized as valid DateTime.");
                //}

//                //string insertToPlantSetup = "Insert Into PlantSetup (ContractorCode, PlantCode, DeptName, DeviceID, ContractorName, PlantType, PlantName, Address, MobNo, InstalledBy, MacID, Device_IPAddress, InstallationType, InstallationDate, pType, PlantExpiry, AnotherExpiry) " +
                //    "values ( '" + lb_ConCode.Text + "' , '" + lb_PlantCode.Text + "', '" + clsFunctions.dept + "', '" + lb_DeviceID.Text + "', '" + lb_ConName.Text + "', '" + lb_PlantType.Text + "', '" + lb_PlantName.Text + "' , '" + lb_Address.Text + "'," +
                //    " '" + txtAuto_MobileNo.Text + "' , '" + cmbInstalledBy_Auto.Text + "', '" + PC_macAddress + "', '" + PC_ipAddress + "', 'A', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', 'A', '" + validTillDate.ToString("dd/MM/yyyy") + "',  '" + anotherExpiryDate.ToString("dd/MM/yyyy") + "')";

//                //int a = clsFunctions.AdoData_setup(insertToPlantSetup);
                ////-----------------------------------------------

                int b = clsFunctions.InsertToPlantRenewalHistory(lb_DeviceID.Text, lb_validFromDate.Text, lb_validTillDate.Text, anotherExpiry);

                //-----------------------------------------------
                clsFunctions.InsertInServerMappingAtReg(cmbAuto_Dept.Text, lb_DeviceID.Text, lb_PlantCode.Text);

                if (a == 1 && b == 1)
                {
                    return 1;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                // Handle exception
                clsFunctions_comman.UniBox($"Exception at StoreAutoRegDataFromInDatabase: {ex.Message}");
                clsFunctions_comman.ErrorLog($"Exception at StoreAutoRegDataFromInDatabase: {ex}");

                return 0;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        string PlantTypeRecievedFromApi = "";
        private void GetPlantInstallDetails(string alias, string mobNo, string macId, string plantType) //getPlantInstallationDetails
        {
            try
            {
                var pd = new GetPlantInstallDetails();

                pd.mobile_no = mobNo;
                pd.mac_id = macId;

                string apiUrl = "";
                string baseurl = "";

                string URL = "pmcscada.in";
                string apiName = "";

                if (alias == "PWD - BT")
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", alias);
                else
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress", alias);

                apiName = clsFunctions.GetAPINameFromPreset("getInstallDetails", alias);

                apiUrl = apiUrl.Replace("\"", "");
                ////string domain = clsFunctions.getDepartmentDomain(alias);
                //apiName = getApis(alias);
                apiName = apiName.Replace("mobNo", mobNo);
                apiName = apiName.Replace("macId", macId);
                apiName = apiName.Replace("pType", plantType);
                //apiUrl = domain + apiName ;
              
                apiUrl = "" + URL + "" + apiName;
                apiUrl = apiUrl.Replace("\"", "");

                // For Testing
                //apiUrl = "http://192.168.1.64:8089/UniPro_Rest/plant_reg_history/plant_installation_details?mobile_no=8669977679&mac_id=E4B97A6FA7D2&plant_type=BT";

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(apiUrl).Result) // Blocking call
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            dt_plantData.Rows.Clear();

                            string jsonResponse = response.Content.ReadAsStringAsync().Result; // Blocking call
                            JObject jsonObject = JObject.Parse(jsonResponse);
                            if(jsonResponse == "{\"plant_installation_details\":[]}")
                            {
                                //MessageBox.Show($"Plant details are null for this Mobile Number. The Response {jsonResponse}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                clsFunctions_comman.UniBox($"Plant Not found for this Mobile Number.");                                
                                clsFunctions.ErrorLog($"Plant details are null for this Mobile Number. The Response: {jsonResponse}");

                                btnGetPlant.Enabled = true;
                                return;
                            }

                            JArray plantDetailsArray = (JArray)jsonObject["plant_installation_details"];
                            foreach (JToken plantDetail in plantDetailsArray)
                            {
                                string deviceIMEINo = (string)plantDetail["device_imei_no"];
                                string installationStatus = (string)plantDetail["installation_status"];
                                string plntType = (string)plantDetail["plant_type"];
                                string plantName = (string)plantDetail["plant_name"];

                                plntType = GetPlantType(plntType);  //here
                                PlantTypeRecievedFromApi = plntType;

                                // Add the data to the DataTable
                                DataRow newRow = dt_plantData.NewRow();
                                newRow["device_imei_no"] = deviceIMEINo;
                                newRow["installation_status"] = installationStatus;
                                newRow["plant_type"] = plntType;
                                newRow["plant_name"] = plantName;
                                dt_plantData.Rows.Add(newRow);
                            }
                        }
                        else
                        {
                            clsFunctions.ErrorLog($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                // Handle exceptions
                throw ex;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        private void AddColumnsTo_dt_plantData()
        {

            dt_plantData.Columns.Add("device_imei_no", typeof(string));
            dt_plantData.Columns.Add("installation_status", typeof(string));
            dt_plantData.Columns.Add("plant_type", typeof(string));
            dt_plantData.Columns.Add("plant_name", typeof(string));

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        // PostInstallationDetails To Mother API

        public async Task<string> PostInstallationDetailsToMotherAPI(string installBy, string installType)
        {
            string URL = "";
            string saveInstallationApiName = "";

            if (cmbDeptList.Text == "")
                cmbDeptList.Text = cmbAuto_Dept.Text;

            if (cmbAuto_Dept.Text == "")
                cmbAuto_Dept.Text = cmbDeptList.Text;

            if (cmbDeptList.Text == "PWD - BT")
                URL = clsFunctions.GetAPINameFromPreset("ipaddress1", cmbDeptList.Text);
            else
                URL = clsFunctions.GetAPINameFromPreset("ipaddress", cmbDeptList.Text);

            if (installType == "M")
            {
                saveInstallationApiName = clsFunctions.GetAPINameFromPreset("m_SaveInstall", cmbDeptList.Text);
            }
            else if (installType == "A")
            {
                saveInstallationApiName = clsFunctions.GetAPINameFromPreset("m_SaveInstall", cmbAuto_Dept.Text);
            }
            //--------------------------------

            string apiUrl = URL + saveInstallationApiName;
            //--------------------------------
            //string apiUrl = "http://192.168.164:8084/Mother_API/plant_reg_history/saveInstallationDetails";

            // Extract data from labels or other UI elements

            installtionDate = DateTime.Now.ToString("dd-MM-yyyy"); // Replace with the actual label name
            installtionTime = DateTime.Now.ToString("HH:mm:ss");
            macId = PC_macAddress;
            ipAddress = PC_ipAddress;
            installedBy = installBy;
            installationType = installType;
            mobileno = txtAuto_MobileNo.Text;

            //---------------------------------------------------------------------------
            // Prepare the JSON data for (A) Auto Registration

            if (installationType == "A")
            {
                //departmentName = cmbDeptList.Text;

                jsonData = $@"
                {{  
                    ""plantCode"":""{lb_PlantCode.Text}"",
                    ""conCode"":""{lb_ConCode.Text}"",
                    ""installtionDate"":""{installtionDate}"",
                    ""installtionTime"":""{installtionTime}"",
                    ""installedBy"":""{installedBy}"",
                    ""macId"":""{macId}"",
                    ""ipAddress"":""{ipAddress}"",
                    ""mobileNo"":""{mobileno}"",
                    ""installationType"": ""{installationType}"",
                    ""deviceIMEINo"":""{lb_DeviceID.Text}"",
                    ""departmentName"":""{clsFunctions.dept}"",
                    ""installationStatus"":""Y""
                }}";
            }

            //---------------------------------------------------------------------------
            // Prepare the JSON data for (M) Manual Registration

            else if (installationType == "M")
            {
                jsonData = $@"
                {{
                    ""installtionDate"":""{installtionDate}"",
                    ""installtionTime"":""{installtionTime}"",
                    ""macId"":""{macId}"",
                    ""ipAddress"":""{ipAddress}"",
                    ""installedBy"":""{installedBy}"",
                    ""installationType"": ""{installationType}"",
                    ""deviceIMEINo"":""{deviceIMEINo}"",
                    ""departmentName"":""{departmentName}"",
                    ""installationStatus"":""Y""
                }}";
            }
            //-----------------------------------------

            using (HttpClient client = new HttpClient())
            {
                // Set the content type
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Create the HTTP content with the JSON data
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    // Send the POST request
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Get the response content as a string
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful response
                        if (responseContent == "{\"Not-Saved\":0}")
                        {
                            //clsFunctions_comman.UniBox($"Installation Details not saved, invalid Device ID : ('" + installationType + "')");
                            clsFunctions_comman.ErrorLog($"MotherAPI: Installation Details not saved, invalid Device ID : ('" + installationType + "')");
                        }
                        if (responseContent == "{\"Plant Installation History Store Successfully \":1}")
                        {
                            //clsFunctions_comman.UniBox($"Installation Details not saved, invalid Device ID : ('" + installationType + "')");
                            clsFunctions_comman.ErrorLog($"MotherAPI: Installation Details not saved, invalid Device ID : ('" + installationType + "')");
                        }
                        else
                        {
                            //clsFunctions_comman.ErrorLog($"MotherAPI: Installation Details saved successfully : ('" + installationType + "')");
                            //clsFunctions_comman.UniBox($"Installation Details saved successfully : '" + installationType + "'");
                        }
                        return responseContent;
                    }
                    else
                    {
                        // Handle error
                        if ((response.IsSuccessStatusCode == false))
                        {
                            //clsFunctions_comman.UniBox($"Connection Close at saveInstallationDetails: {response.StatusCode}, {response.Headers}");
                            clsFunctions_comman.ErrorLog($"MotherAPI: Connection Close at saveInstallationDetails: {response.StatusCode}, {response.Headers}");
                            return string.Empty;
                        }
                        //clsFunctions_comman.UniBox($"Error at saveInstallationDetails: {response.StatusCode}");
                        clsFunctions_comman.ErrorLog($"MotherAPI: Error at saveInstallationDetails: {response.StatusCode}");

                        clsFunctions_comman.ErrorLog($"MotherAPI: Error Response: {responseContent}");
                        return string.Empty; // or any default value indicating an error
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    //clsFunctions_comman.UniBox($"Exception at saveInstallationDetails: {ex.Message}");
                    clsFunctions_comman.ErrorLog($"MotherAPI: Exception at saveInstallationDetails: {ex}");
                    return string.Empty; // or any default value indicating an exception
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        string queryforServerMappingPreset = "";

        public  int  getPresetDataForApis(string deptType, string PlantTypeRecievedFromApi,string domain)           // not using
        {
            try
            {
                //string deptType = "";
                string apiUrl = domain + "/Mother_API/ServerMapping_Preset/get_server_mapping_against_dept_name_plant_type?dept_name=d_type&plant_type=p_type";
                //http://192.168.1.13:8089/Mother_API/ServerMapping_Preset/get_server_mapping_against_dept_name_plant_type?dept_name=PWD&plant_type=BT
                                
                if (PlantTypeRecievedFromApi.Contains("RMC"))
                    PlantTypeRecievedFromApi = "RMC";
                else
                    PlantTypeRecievedFromApi = "BT";

                apiUrl = apiUrl.Replace("d_type", deptType);
                
                apiUrl = apiUrl.Replace("p_type",PlantTypeRecievedFromApi);
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

                                queryforServerMappingPreset = $@"INSERT INTO ServerMapping_Preset (SrNo, AliasName, Note1, ipaddress, portno, BT_API, RMC_Trans_API, RMC_Transaction_API, Software_Status_API, plantExpiry, deptname, PlantType, Note2, ipaddress1, port1, BT_API1, RMC_Transaction_API1, RMC_Trans_API1, AutoReg_SMS, AutoReg_Verify, AutoReg_Save, GetWO, GetAllWO, AllocateWO, GetPlantDetails, GetMobNoFromWO, GetProdErrorTemplate, sendSMS, getInstallDetails, DPTStatus, Flag, DeviceID, PlantCode, GetDataHeaderTableSync,UploadDataHeaderTableSync, GetDataTransactionTableSync,UploadDataTransactionTableSync, ServerMapping_Preset, Unipro_Setup, PlantSetup, Plant_LiveStatus_History, m_SaveInstall, m_latestUp_Insert, m_latestUp_Get,Upload_UniproSetupID) VALUES ({preset["SrNo"]}, '{preset["AliasName"]}', '{preset["Note1"]}', '{preset["ipaddress"]}', '{preset["portno"]}', '{preset["BT_API"]}', '{preset["RMC_Trans_API"]}', '{preset["RMC_Transaction_API"]}', '{preset["Software_Status_API"]}', '{preset["plantExpiry"]}', '{preset["deptname"]}', '{preset["PlantType"]}', '{preset["Note2"]}', '{preset["ipaddress1"]}', '{preset["port1"]}', '{preset["BT_API1"]}', '{preset["RMC_Transaction_API1"]}', '{preset["RMC_Trans_API1"]}', '{preset["AutoReg_SMS"]}', '{preset["AutoReg_Verify"]}', '{preset["AutoReg_Save"]}', '{preset["GetWO"]}', '{preset["GetAllWO"]}', '{preset["AllocateWO"]}', '{preset["GetPlantDetails"]}', '{preset["GetMobNoFromWO"]}', '{preset["GetProdErrorTemplate"]}', '{preset["sendSMS"]}', '{preset["getInstallDetails"]}', '{preset["DPTStatus"]}', '{preset["Flag"]}', '{preset["DeviceID"]}', '{preset["PlantCode"]}', '{preset["GetDataHeaderTableSync"]}', '{preset["UploadDataHeaderTableSync"]}', '{preset["GetDataTransactionTableSync"]}', '{preset["UploadDataTransactionTableSync"]}', '{preset["ServerMapping_Preset"]}', '{preset["Unipro_Setup"]}', '{preset["PlantSetup"]}', '{preset["Plant_LiveStatus_History"]}', '{preset["m_SaveInstall"]}', '{preset["m_latestUp_Insert"]}', '{preset["m_latestUp_Get"]}', '{preset["Upload_UniproSetupID"]}')";

                                int result = clsFunctions.AdoData_setup(queryforServerMappingPreset);

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

        string queryForInsertingDataIntoUniPro_Setup = "";
        private void getpresetDataForUniPro_Setup()                 // not using
        {
            try
            {
                //string department = "";
                string plantType = "";
                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='"+cmbAuto_Dept.Text+"'") + clsFunctions.loadSingleValueSetup("Select Unipro_Setup from ServerMapping_Preset where AliasName='" + cmbAuto_Dept.Text + "'").Trim();

                apiUrl = apiUrl.Replace("d_type", department);
                apiUrl = apiUrl.Replace("\"", "");

                apiUrl = apiUrl.Replace("p_type", lb_PlantCode.Text);
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
                                if(jsonResponse== "{\"Unipro_Setup_Data\":null}")
                                {
                                    //MessageBox.Show("At getpresetDataForUniPro_Setup():for fetching UniPro_Setup data, the jsonResponse object : " + jsonResponse);
                                    clsFunctions.ErrorLog("At getpresetDataForUniPro_Setup():for fetching UniPro_Setup data, the jsonResponse object : " + jsonResponse );
                                }
                                // Insert Unipro_Setup_Data
                                var uniproSetup = jsonObject["Unipro_Setup_Data"];
                                if (uniproSetup != null)
                                {
                                    queryForInsertingDataIntoUniPro_Setup = $@"INSERT INTO Unipro_setup (UniproSetupID, Plant_Make, FormName, UploaderName, Path, Pass, Description, DB_Type, PlantType, ImagePath, UILocation, ImageUsed, RecipeFormName, ConnectionString, BatchReport_FileName, DC_FileName, Status) 
                                        VALUES ({uniproSetup["UniproSetupID"]}, '{uniproSetup["Plant_Make"]}', '{uniproSetup["FormName"]}', '{uniproSetup["UploaderName"]}', '{uniproSetup["Path"]}', '{uniproSetup["Pass"]}', '{uniproSetup["Description"]}', '{uniproSetup["DB_Type"]}', '{uniproSetup["PlantType"]}', 
                                                '{uniproSetup["ImagePath"]}', '{uniproSetup["UILocation"]}', '{uniproSetup["ImageUsed"]}', '{uniproSetup["RecipeFormName"]}', '{uniproSetup["ConnectionString"]}', '{uniproSetup["BatchReport_FileName"]}', '{uniproSetup["DC_FileName"]}', 'Y')";

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public async void getDataHeaderTableSync(string cmbAuto_Detp,string department, string lb_PlantCode)            // not using
        {
            try
            {
                if (!NetworkHelper.IsInternetAvailable())
                {
                    MessageBox.Show("Check internet connection.");
                    return;
                }

                string deptType = "";
                if(clsFunctions.regURL.Trim()==""|| clsFunctions.regURL.Trim()=="0")
                {
                    clsFunctions.regURL = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='" + cmbAuto_Detp + "'");
                }

                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='" + cmbAuto_Detp + "'") + clsFunctions.loadSingleValueSetup("Select GetDataHeaderTableSync from ServerMapping_Preset where AliasName='" + cmbAuto_Detp + "'").Trim();
                ///Mother_API/DataHeaderTableSync/header_data_from_plant_code_dept_name?plant_code=p_code&dept_name=d_type

                apiUrl = apiUrl.Replace("deptName", department);
                apiUrl = apiUrl.Replace("plantCode", lb_PlantCode);
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
                       // clsFunctions.AdoData_setup("Delete from DataHeaderTableSync");
                        //clsFunctions.AdoData_setup("Delete from DataHeaderTableSync where Info='Fields' and Type='Client' and plant_code='"+ lb_PlantCode + "' and dept_name='"+ department + "' ");
                        foreach (var header in dataHeaders)
                        {
                            string headerQuery = $@"INSERT INTO DataHeaderTableSync (SoftwareVersion, Info, Batch_No, Batch_Date, Batch_Time, Batch_Time_Text, Batch_Start_Time, Batch_End_Time, Batch_Year, Batcher_Name, Batcher_User_Level, Customer_Code, Recipe_Code, Recipe_Name, Mixing_Time, Mixer_Capacity, strength, Site, Truck_No, Truck_Driver, Production_Qty, Ordered_Qty, Returned_Qty, WithThisLoad, Batch_Size, Order_No, Schedule_Id, Gate1_Target, Gate2_Target, Gate3_Target, Gate4_Target, Gate5_Target, Gate6_Target, Cement1_Target, Cement2_Target, Cement3_Target, Cement4_Target, Filler_Target, Water1_Target, slurry_Target, Water2_Target, Silica_Target, Adm1_Target1, Adm1_Target2, Adm2_Target1, Adm2_Target2, Cost_Per_Mtr_Cube, Total_Cost, Plant_No, Weighed_Net_Weight, Weigh_Bridge_Stat, tExportStatus, tUpload1, tUpload2, WO_Code, Site_ID, Cust_Name, Type, TableName, Flag,plant_code,dept_name) VALUES ('{header["SoftwareVersion"]}', '{header["Info"]}', '{header["Batch_No"]}', '{header["Batch_Date"]}', '{header["Batch_Time"]}', '{header["Batch_Time_Text"]}', '{header["Batch_Start_Time"]}', '{header["Batch_End_Time"]}', '{header["Batch_Year"]}', '{header["Batcher_Name"]}', '{header["Batcher_User_Level"]}', '{header["Customer_Code"]}', '{header["Recipe_Code"]}', '{header["Recipe_Name"]}', '{header["Mixing_Time"]}', '{header["Mixer_Capacity"]}', '{header["strength"]}', '{header["Site"]}', '{header["Truck_No"]}', '{header["Truck_Driver"]}', '{header["Production_Qty"]}', '{header["Ordered_Qty"]}', '{header["Returned_Qty"]}', '{header["WithThisLoad"]}', '{header["Batch_Size"]}', '{header["Order_No"]}', '{header["Schedule_Id"]}', '{header["Gate1_Target"]}', '{header["Gate2_Target"]}', '{header["Gate3_Target"]}', '{header["Gate4_Target"]}', '{header["Gate5_Target"]}', '{header["Gate6_Target"]}', '{header["Cement1_Target"]}', '{header["Cement2_Target"]}', '{header["Cement3_Target"]}', '{header["Cement4_Target"]}', '{header["Filler_Target"]}', '{header["Water1_Target"]}', '{header["slurry_Target"]}', '{header["Water2_Target"]}', '{header["Silica_Target"]}', '{header["Adm1_Target1"]}', '{header["Adm1_Target2"]}', '{header["Adm2_Target1"]}', '{header["Adm2_Target2"]}', '{header["Cost_Per_Mtr_Cube"]}', '{header["Total_Cost"]}', '{header["Plant_No"]}', '{header["Weighed_Net_Weight"]}', '{header["Weigh_Bridge_Stat"]}', '{header["tExportStatus"]}', '{header["tUpload1"]}', '{header["tUpload2"]}', '{header["WO_Code"]}', '{header["Site_ID"]}', '{header["Cust_Name"]}', '{header["Type"]}', '{header["TableName"]}', '{header["Flag"]}','{header["plant_code"]}', '{header["dept_name"]}')";

                            //if (header["SoftwareVersion"].ToString() != "VIPL")
                            {
                                int headerResult = clsFunctions.AdoData_setup(headerQuery);
                                if (headerResult > 0)
                                {
                                    clsFunctions.ErrorLog("DataHeaderTableSync data inserted successfully.");
                                }
                                else
                                {
                                    clsFunctions.ErrorLog("DataHeaderTableSync data insertion failed.");
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                clsFunctions.ErrorLog("at getDataHeaderTableSync(): "+ ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public async void getDataTransactionTableSync(string cmbAuto_Detp, string department, string lb_PlantCode)              // not using
        {
            try
                {
                string deptType = "";
                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='" + cmbAuto_Detp + "'") + clsFunctions.loadSingleValueSetup("Select GetDataTransactionTableSync from ServerMapping_Preset where AliasName='" + cmbAuto_Detp + "'").Trim();
                ///Mother_API/DataHeaderTableSync/header_data_from_plant_code_dept_name?plant_code=p_code&dept_name=d_type

                apiUrl = apiUrl.Replace("deptName", department);

                apiUrl = apiUrl.Replace("plantCode", lb_PlantCode);
                apiUrl = apiUrl.Replace("\"", "");
                if (apiUrl != "")
                {
                    //using (HttpClient client = new HttpClient())
                    //{
                    //    using (HttpResponseMessage response = client.GetAsync(apiUrl).Result)
                    //    {
                    //        if (response.IsSuccessStatusCode)
                    //        {
                    //            string jsonResponse = response.Content.ReadAsStringAsync().Result; // Blocking call
                    //            JObject jsonObject = JObject.Parse(jsonResponse);
                    //            var preset = jsonObject["ServerMapping_Preset"];
                    //        }
                    //    }
                    //}

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        response.EnsureSuccessStatusCode();

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        if (jsonResponse == "{\"DataTransactionTableSync\":[]}")
                        {
                            //MessageBox.Show("At getDataTransactionTableSync: for fetching DataTransactionTableSync preset data, the jsonResponse object : " + jsonResponse);
                            clsFunctions.ErrorLog("At getDataTransactionTableSync: for fetching DataTransactionTableSync preset data, the jsonResponse object : " + jsonResponse);
                        }
                        JObject jsonObject = JObject.Parse(jsonResponse);
                        

                        // Insert into DataTransactionTableSync table
                        var dataTransactions = jsonObject["DataTransactionTableSync"];
                        //clsFunctions.AdoData_setup("Delete from DataTransactionTableSync");

                        foreach (var transaction in dataTransactions)
                        {
                            string transactionQuery = $@"INSERT INTO DataTransactionTableSync (SoftwareVersion, Info, Batch_No, Batch_Index, Batch_Date, Batch_Time, Batch_Time_Text, Batch_Year, Consistancy, Production_Qty, Ordered_Qty, Returned_Qty, WithThisLoad, Batch_Size, Gate1_Actual, Gate1_Target, Gate1_Moisture, Gate2_Actual, Gate2_Target, Gate2_Moisture, Gate3_Actual, Gate3_Target, Gate3_Moisture, Gate4_Actual, Gate4_Target, Gate4_Moisture, Gate5_Actual, Gate5_Target, Gate5_Moisture, Gate6_Actual, Gate6_Target, Gate6_Moisture, Cement1_Actual, Cement1_Target, Cement1_Correction, Cement2_Actual, Cement2_Target, Cement2_Correction, Cement3_Actual, Cement3_Target, Cement3_Correction, Cement4_Actual, Cement4_Target, Cement4_Correction, Filler1_Actual, Filler1_Target, Filler1_Correction, Water1_Actual, Water1_Target, Water1_Correction, Water2_Actual, Water2_Target, Water2_Correction, Silica_Actual, Silica_Target, Silica_Correction, Slurry_Actual, Slurry_Target, Slurry_Correction, Adm1_Actual1, Adm1_Target1, Adm1_Correction1, Adm1_Actual2, Adm1_Target2, Adm1_Correction2, Adm2_Actual1, Adm2_Target1, Adm2_Correction1, Adm2_Actual2, Adm2_Target2, Adm2_Correction2, Pigment_Actual, Pigment_Target, Plant_No, Balance_Wtr, tUpload1, tUpload2, Type, TableName, Flag, Query1, plant_code, dept_name) VALUES ('{transaction["SoftwareVersion"]}', '{transaction["Info"]}', '{transaction["Batch_No"]}', '{transaction["Batch_Index"]}', '{transaction["Batch_Date"]}', '{transaction["Batch_Time"]}', '{transaction["Batch_Time_Text"]}', '{transaction["Batch_Year"]}', '{transaction["Consistancy"]}', '{transaction["Production_Qty"]}', '{transaction["Ordered_Qty"]}', '{transaction["Returned_Qty"]}', '{transaction["WithThisLoad"]}', '{transaction["Batch_Size"]}', '{transaction["Gate1_Actual"]}', '{transaction["Gate1_Target"]}', '{transaction["Gate1_Moisture"]}', '{transaction["Gate2_Actual"]}', '{transaction["Gate2_Target"]}', '{transaction["Gate2_Moisture"]}', '{transaction["Gate3_Actual"]}', '{transaction["Gate3_Target"]}', '{transaction["Gate3_Moisture"]}', '{transaction["Gate4_Actual"]}', '{transaction["Gate4_Target"]}', '{transaction["Gate4_Moisture"]}', '{transaction["Gate5_Actual"]}', '{transaction["Gate5_Target"]}', '{transaction["Gate5_Moisture"]}', '{transaction["Gate6_Actual"]}', '{transaction["Gate6_Target"]}', '{transaction["Gate6_Moisture"]}', '{transaction["Cement1_Actual"]}', '{transaction["Cement1_Target"]}', '{transaction["Cement1_Correction"]}', '{transaction["Cement2_Actual"]}', '{transaction["Cement2_Target"]}', '{transaction["Cement2_Correction"]}', '{transaction["Cement3_Actual"]}', '{transaction["Cement3_Target"]}', '{transaction["Cement3_Correction"]}', '{transaction["Cement4_Actual"]}', '{transaction["Cement4_Target"]}', '{transaction["Cement4_Correction"]}', '{transaction["Filler1_Actual"]}', '{transaction["Filler1_Target"]}', '{transaction["Filler1_Correction"]}', '{transaction["Water1_Actual"]}', '{transaction["Water1_Target"]}', '{transaction["Water1_Correction"]}', '{transaction["Water2_Actual"]}', '{transaction["Water2_Target"]}', '{transaction["Water2_Correction"]}', '{transaction["Silica_Actual"]}', '{transaction["Silica_Target"]}', '{transaction["Silica_Correction"]}', '{transaction["Slurry_Actual"]}', '{transaction["Slurry_Target"]}', '{transaction["Slurry_Correction"]}', '{transaction["Adm1_Actual1"]}', '{transaction["Adm1_Target1"]}', '{transaction["Adm1_Correction1"]}', '{transaction["Adm1_Actual2"]}', '{transaction["Adm1_Target2"]}', '{transaction["Adm1_Correction2"]}', '{transaction["Adm2_Actual1"]}', '{transaction["Adm2_Target1"]}', '{transaction["Adm2_Correction1"]}', '{transaction["Adm2_Actual2"]}', '{transaction["Adm2_Target2"]}', '{transaction["Adm2_Correction2"]}', '{transaction["Pigment_Actual"]}', '{transaction["Pigment_Target"]}', '{transaction["Plant_No"]}', '{transaction["Balance_Wtr"]}', '{transaction["tUpload1"]}', '{transaction["tUpload2"]}', '{transaction["Type"]}', '{transaction["TableName"]}', '{transaction["Flag"]}', '{transaction["Query1"]}', '{transaction["plant_code"]}', '{transaction["dept_name"]}')";

                            //if (transaction["SoftwareVersion"].ToString() != "VIPL")
                            {
                                int transactionResult = clsFunctions.AdoData_setup(transactionQuery);
                                if (transactionResult > 0)
                                {
                                    clsFunctions.ErrorLog("DataTransactionTableSync data inserted successfully.");
                                }
                                else
                                {
                                    clsFunctions.ErrorLog("DataTransactionTableSync data insertion failed.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        
        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        
        //--------------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
