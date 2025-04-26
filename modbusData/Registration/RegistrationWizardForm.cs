using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.frmRegistration;

namespace Uniproject.Registration
{
    public partial class RegistrationWizardForm : Form
    {
        private int currentStep = 0;
        private List<Panel> steps = new List<Panel>();

        public captcha captcha = new captcha();
        string department = "";
        string plantTypeFromTuple = "";

        //string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;";
        string plantType = string.Empty;

        public string PC_ipAddress = NetworkInfo.GetIpAddress();
        public string PC_macAddress = NetworkInfo.GetMacAddress();
        public string SelectedPlantDeviceID = "";
        public string SelectedPlantInstallStatus = "";
        public string SelectedPlantValidity = "";
        public string anotherExpiry = "";
        public static bool internetConnection = false;
        string jsonData;
        string OTP_Received = "";

        // for PostInstallationDetails : Allocate to these string to send to API for save  - saveInstallationDetails API
        string installtionDate, installtionTime, macId, ipAddress, installedBy, installationType, deviceIMEINo, departmentName, mobileno, plantCode, conCode;

        public System.Data.DataTable dt_plantData = new System.Data.DataTable();

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public RegistrationWizardForm()
        {
            InitializeComponent();
            InitializeSteps();
            ShowStep(currentStep);
        }

        private void InitializeSteps()
        {
            steps.Add(panelStep1);
            steps.Add(panelStep2);
            steps.Add(panelStep3);
        }

        //-----------------------------------------------------------------------------------------------------------

        private void ShowStep(int stepIndex)
        {
            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].Visible = i == stepIndex;
            }

            btnBack.Enabled = stepIndex > 0;
            btnNext.Enabled = stepIndex < steps.Count - 1;
            btnFinish.Enabled = stepIndex == steps.Count - 1;
        }
        
        //-----------------------------------------------------------------------------------------------------------

        private bool ValidateStep(int stepIndex)
        {
            switch (stepIndex)
            {
                case 0:
                    // Validate Department and Mobile Number
                    if (cmbAuto_Dept.SelectedItem == null || string.IsNullOrWhiteSpace(txtAuto_MobileNo.Text) || cmbPlantList.SelectedItem == null)
                    {
                        clsFunctions_comman.UniBox("Please select a department, plant and enter a mobile number.");
                        return false;
                    }
                    break;
                case 1:
                    // Validate Plant Selection, OTP, and Captcha
                    if (cmbPlantList.SelectedItem == null || string.IsNullOrWhiteSpace(captchaAnswer.Text))
                    {
                        clsFunctions_comman.UniBox("Please select a plant, enter OTP, and captcha.");
                        return false;
                    }
                    break;
            }
            return true;
        }

        //-----------------------------------------------------------------------------------------------------------

        private void UpdateCaptchaImage()
        {
            var captchaImage = captcha.GenerateCaptchaImage(pictureBoxCaptcha.Width, pictureBoxCaptcha.Height);
            pictureBoxCaptcha.Image = captchaImage;
        }

        //-----------------------------------------------------------------------------------------------------------

        private async void RegistrationWizardForm_Load(object sender, EventArgs e)
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

                //---------------------------------------------------------------------------------------------------------------------

                panelStep1.BackColor = Color.Blue;
                panelStep1.BackColor = Color.FromArgb(210, 255, 255, 255);

                panelStep2.BackColor = Color.Blue;
                panelStep2.BackColor = Color.FromArgb(210, 255, 255, 255);

                panelStep3.BackColor = Color.Blue;
                panelStep3.BackColor = Color.FromArgb(210, 255, 255, 255);

                //---------------------------------------------------------------------------------------------------------------------

                sw_version.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                // 16/02/2024 - BhaveshT : create pType column with default value
                clsFunctions.checknewcolumnWithDefaultValInSetupDB("pType", "Text(1)", "PlantSetup", "A");

                captcha = new captcha();
                captcha.Captcha(6);
                UpdateCaptchaImage();

                // for Auto
                clsFunctions.FillCombo_setup("SELECT DISTINCT AliasName from AliasName", cmbAuto_Dept);

                // Fetch IP address and MAC address
                PC_ipAddress = NetworkInfo.GetIpAddress();
                PC_macAddress = NetworkInfo.GetMacAddress();
                lb_macId.Text = PC_macAddress;
                lb_IPaddress.Text = PC_ipAddress;

                AddColumnsTo_dt_plantData();
                clsFunctions.FillCombo_setup("Select Name from Installation_Person", cmbInstalledBy_Auto);

                //----------------------
                LocationService locationService = new LocationService();
                string location = await locationService.GetCurrentLocationAsync();
                lb_PlantLocation.Text = location;
            }
            catch
            {

            }
        }

        //-----------------------------------------------------------------------------------------------------------

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

        //-----------------------------------------------------------------------------------------------------------

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
                        //pnlAfterOTPBtn.Enabled = true;
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
                clsFunctions_comman.UniBox("Details not found for entered Mobile No.");
                clsFunctions_comman.ErrorLog("Exception at UniRegistration_Auto : btnGetOTP_Click - " + ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------

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

                    //string fullUrl = "" + clsFunctions.protocol + "://" + clsFunctions.URL + SMSapiName + mobNo;

                    //SMSapiName = SMSapiName.Replace("mobNo", mobNo);
                    //SMSapiName = SMSapiName.Replace("dId", SelectedPlantDeviceID);
                    //SMSapiName = SMSapiName.Replace("flag", "N");

                    mobNo = mobNo.Trim();
                    SMSapiName = SMSapiName.Replace("mobileNo", mobNo);
                    SMSapiName = SMSapiName.Replace("deviceId", SelectedPlantDeviceID);

                    var status = dt_plantData.Rows[0]["installation_status"].ToString();
                    SMSapiName = SMSapiName.Replace("installationStatus", status);

                    string fullUrl = "" + clsFunctions.regURL + "" + SMSapiName;
                    fullUrl = fullUrl.Replace("\"", "");

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
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        //-----------------------------------------------------------------------------------------------------------

        private void cmbPlantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlantList.SelectedIndex > -1)
                btnGetOTP.Enabled = true;
            SelectedPlantDeviceID = GetDeviceIdFromPlantData(cmbPlantList.Text);

            SelectedPlantInstallStatus = GetInstallStatusFromPlantData(cmbPlantList.Text);

            if (SelectedPlantInstallStatus == "Y")
            {
                clsFunctions_comman.UniBox("Software is already installed for selected plant: " + cmbPlantList.Text);

                btnGetOTP.Enabled = false;
                txtAuto_MobileNo.Enabled = true;

                txtAuto_OTP.Enabled = false;

                btnGetPlant.Enabled = true;
                cmbPlantList.SelectedIndex = -1;
                SelectedPlantInstallStatus = "";

            }
            else if (SelectedPlantInstallStatus == "N" || SelectedPlantInstallStatus == "")
            {
                btnGetOTP.Enabled = true;
            }
        }

        //-----------------------------------------------------------------------------------------------------------

        private void cmbAuto_Dept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        //-----------------------------------------------------------------------------------------------------------

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
                clsFunctions_comman.UniBox("Captcha is incorrect. Please try again.");
                captchaAnswer.Clear();
                captcha = new captcha(); // Regenerate a new CAPTCHA after unsuccessful validation
                UpdateCaptchaImage();
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

                    //pnlAPIData.Visible = true;
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
                
        //-----------------------------------------------------------------------------------------------------------

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ValidateStep(currentStep))
            {
                if (currentStep < steps.Count - 1)
                {
                    currentStep++;
                    ShowStep(currentStep);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentStep > 0)
            {
                currentStep--;
                ShowStep(currentStep);
            }
        }

        //-----------------------------------------------------------------------------------------------------------

        private void btnFinish_Click(object sender, EventArgs e)
        {
            // Perform registration logic here
            clsFunctions_comman.UniBox("Registration completed!");
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------

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

                        string domain = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='" + cmbAuto_Dept.Text + "'");

                        clsFunctions.AdoData_setup("Delete From ServerMapping");
                        clsFunctions.AdoData_setup("Delete From ServerMapping_Preset");

                        //int status = getPresetDataForApis(department, plantTypeFromTuple, domain);
                        int status = clsFunctions.GetAndInsertServerMappingDataFromAPI(cmbAuto_Dept.Text, department, plantTypeFromTuple);

                        if (status == 0)
                        {
                            clsFunctions_comman.UniBox("ServerMapping_Preset Data couldn't be inserted.");
                            clsFunctions_comman.ErrorLog("ServerMapping_Preset Data couldn't be inserted.");
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
                    clsFunctions_comman.UniBox("Kindly select Department.");

            }
            catch (Exception ex)
            {
                //clsFunctions_comman.UniBox("Exception at UniRegistration_Auto : btnGetOTP_Click - " + ex);

                clsFunctions_comman.UniBox("Details not found for entered Mobile No.");
                clsFunctions_comman.ErrorLog("Exception at UniRegistration_Auto : btnGetOTP_Click - " + ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------
       
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

        //-----------------------------------------------------------------------------------------------------------

        public class ResponseData
        {
            public List<getRegData> PlantDetailsList { get; set; }
        }

        //----------------------------------------------------------------------------------------------------

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

                apiName = "http://192.168.1.13:8089/UniPro_Rest/plant_reg_history/plant_installation_details?mobile_no=mobNo&mac_id=macId&plant_type=pType";

                apiUrl = apiUrl.Replace("\"", "");
                ////string domain = clsFunctions.getDepartmentDomain(alias);
                //apiName = getApis(alias);
                apiName = apiName.Replace("mobNo", mobNo);
                apiName = apiName.Replace("macId", macId);
                apiName = apiName.Replace("pType", plantType);
                //apiUrl = domain + apiName ;

                //apiUrl = "" + URL + "" + apiName;

                apiUrl = apiUrl.Replace("\"", "");
                apiUrl = apiName;

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(apiUrl).Result) // Blocking call
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            dt_plantData.Rows.Clear();

                            string jsonResponse = response.Content.ReadAsStringAsync().Result; // Blocking call
                            JObject jsonObject = JObject.Parse(jsonResponse);
                            if (jsonResponse == "{\"plant_installation_details\":[]}")
                            {
                                //MessageBox.Show($"Plant details are null for this Mobile Number. The Response {jsonResponse}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                clsFunctions_comman.UniBox($"Plant Not found for this Mobile Number.");
                                clsFunctions.ErrorLog($"Plant details are null for this Mobile Number. The Response: {jsonResponse}");

                                btnGetPlant.Enabled = true;
                                return;
                            }

                            //-----------------------------------
                            if (jsonResponse == "{\"plant_installation_details\":[\"Entered Mobile Number Is Invalid\"]}")
                            {
                                clsFunctions_comman.UniBox($"Entered Mobile Number Is Invalid");
                                clsFunctions.ErrorLog($"Entered Mobile Number Is Invalid. The Response: {jsonResponse}");

                                btnGetPlant.Enabled = true;
                                return;
                            }
                            //-----------------------------------

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
            catch (Exception ex)
            {
                // Handle exceptions
                throw ex;
            }
        }

        //-----------------------------------------------------------------------------------------------------------
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

        private void btnAuto_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------
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
                        btnFinish.Enabled = true;
                    }
                    clsFunctions.AdoData_setup("update ServerMapping_Preset set DeviceID='" + SelectedPlantDeviceID + "', PlantCode='" + lb_PlantCode.Text + "', PlantExpiryDate='" + lb_validTillDate.Text + "' where AliasName='" + cmbAuto_Dept.Text + "'"); // update plant and deviceid

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
        }


        //------------------------------------------------------

        private void RegistrationWizardForm_KeyDown(object sender, KeyEventArgs e)
        {
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

        public class PlantDetailsResponse
        {
            [JsonProperty("PlandtDetails")]
            public List<PlantDetail> PlantDetails { get; set; }
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

        //-----------------------------------------------------------------------------------------------------------

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

        //-----------------------------------------------------------------------------------------------------------

        private void AddColumnsTo_dt_plantData()
        {
            dt_plantData.Columns.Add("device_imei_no", typeof(string));
            dt_plantData.Columns.Add("installation_status", typeof(string));
            dt_plantData.Columns.Add("plant_type", typeof(string));
            dt_plantData.Columns.Add("plant_name", typeof(string));
        }

        //-----------------------------------------------------------------------------------------------------------

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
                //if (chkOldReg_Auto.Checked == true)
                //{
                //    (new cmn_frmRegistration()).ShowDialog();
                //    this.Close();
                //    return;
                //}
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
                                    clsFunctions_comman.UniBox("Unipro_Setup data couldn't be inserted!");
                                }
                                if (result == 2)
                                {
                                    clsFunctions_comman.UniBox("Unipro_Setup data couldn't be found!");
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
                                clsFunctions_comman.UniBox("Preset Data couldn't be inserted!");
                            }

                            //PostInstallationDetails(cmbInstalledBy_Auto.Text, "A");
                            //PostInstallationDetailsToMotherAPI(cmbInstalledBy_Auto.Text, "A");

                            clsFunctions_comman.UniBox("Unipro Software registered successfully");
                            clsFunctions_comman.ErrorLog("Unipro Software registered successfully");
                            clsFunctions.IsEgaleReg = "1";
                            //globalmsg = "Register successfully";

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

        //-----------------------------------------------------------------------------------------------------------
        
        // 25/12/2023 : BhaveshT - Store details into PlantSetup and SetupInfo table of Setup.mdb
        public int StoreAutoRegDataFromInDatabase()
        {
            try
            {
                clsFunctions.dept = clsFunctions.GetAPINameFromPreset("deptName", cmbAuto_Dept.Text);

                //------------------------ 10/04/2024 : BhaveshT - Additional fields inserted :  ------------------------
                // 19/02/2024 - BhaveshT : Added parameter: PType in insert query - default value - 'A'

                clsFunctions.AdoData_setup("Delete * from PlantSetup");
                string insertToPlantSetup = "Insert Into PlantSetup (ContractorCode, PlantCode, DeptName, DeviceID, ContractorName, PlantType, PlantName, Address, MobNo, InstalledBy, MacID, Device_IPAddress, InstallationType, InstallationDate, pType, PlantExpiry, AnotherExpiry, Plant_Location) " +
                    "values ( '" + lb_ConCode.Text + "' , '" + lb_PlantCode.Text + "', '" + clsFunctions.dept + "', '" + lb_DeviceID.Text + "', '" + lb_ConName.Text + "', '" + lb_PlantType.Text + "', '" + lb_PlantName.Text + "' , '" + lb_Address.Text + "'," +
                    " '" + txtAuto_MobileNo.Text + "' , '" + cmbInstalledBy_Auto.Text + "', '" + PC_macAddress + "', '" + PC_ipAddress + "', 'A', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', 'A', '" + Convert.ToDateTime(lb_validTillDate.Text).ToString("dd/MM/yyyy") + "',  " +
                    " '" + Convert.ToDateTime(anotherExpiry).ToString("dd/MM/yyyy") + "', '" + lb_PlantLocation.Text + "')";

                int a = clsFunctions.AdoData_setup(insertToPlantSetup);

                int b = clsFunctions.InsertToPlantRenewalHistory(lb_DeviceID.Text, lb_validFromDate.Text, lb_validTillDate.Text, anotherExpiry);

                //clsFunctions.AdoData_setup("Delete * from SetupInfo");
                //string insertToSetupInfo = "Insert Into SetupInfo (dtFromDate, dtToDate, tStatus) values ('" + Convert.ToDateTime(lb_validFromDate.Text).ToString("dd/MM/yyyy") + "', '" + Convert.ToDateTime(lb_validTillDate.Text).ToString("dd/MM/yyyy") + "', 'N')";
                //int b = clsFunctions.AdoData_setup(insertToSetupInfo);

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

        //-----------------------------------------------------------------------------------------------------------

        public int Delete_InsertSetup()
        {
            Thread.Sleep(100);

            int j = 0;
            Boolean flag = true;
            if (clsFunctions.regfilestatus == "FileNotFound")
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            if (flag == true)
            {
                Thread.Sleep(100);
                clsFunctions.DeviceID = deviceIMEINo;
                string lbPlanttype = GetPlantTypeFromAlias(lb_PlantType.Text);
                RegisterSoftware.createRegisterFile(clsFunctions.DeviceID, clsFunctions.dept, lbPlanttype);

                j = 1;
            }
            else
            {
                Thread.Sleep(100);
                clsFunctions.DeviceID = deviceIMEINo;
                if (lb_PlantType.Text == "-")
                {
                    lb_PlantType.Text = clsFunctions.loadSingleValueSetup("Select PlantType from PlantSetup where DeviceID='" + lb_DeviceID.Text.Trim() + "'");
                }
                string lbPlanttype = GetPlantTypeFromAlias(lb_PlantType.Text);
                RegisterSoftware.createRegisterFile(clsFunctions.DeviceID, clsFunctions.dept, lbPlanttype);

                j = 1;
            }
            return j;
        }

        //-----------------------------------------------------------------------------------------------------------
        // PostInstallationDetails to Departmental REST API

        public async Task<string> PostInstallationDetails(string installBy, string installType)
        {
            string URL = "";
            string saveInstallationApiName = "";

            if (cmbAuto_Dept.Text == "PWD - BT")
                URL = clsFunctions.GetAPINameFromPreset("ipaddress1", cmbAuto_Dept.Text);
            else
                URL = clsFunctions.GetAPINameFromPreset("ipaddress", cmbAuto_Dept.Text);

            if (installType == "M")
            {
                saveInstallationApiName = clsFunctions.GetAPINameFromPreset("AutoReg_Save", cmbAuto_Dept.Text);
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

            apiUrl = "http://192.168.1.13:8089/UniPro_Rest/plant_reg_history/saveInstallationDetails";

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
                    ""installationStatus"":""Y"",
			        ""plant_location"":""{lb_PlantLocation.Text}""
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
                            clsFunctions_comman.ErrorLog($"UNIPRO_REST: Installation Details not saved : ('" + installationType + "')");
                        }
                        else if (responseContent == "{\"Saved\":1}")
                        {
                            clsFunctions_comman.ErrorLog($"UNIPRO_REST: Installation Details saved successfully : ('" + installationType + "')");
                            //clsFunctions_comman.UniBox($"Installation Details saved successfully : '" + installationType + "'");
                        }
                        else
                        {

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

        //-----------------------------------------------------------------------------------------------------------
        // PostInstallationDetails To Mother API

        public async Task<string> PostInstallationDetailsToMotherAPI(string installBy, string installType)
        {
            string URL = "";
            string saveInstallationApiName = "";

            if (cmbAuto_Dept.Text == "PWD - BT")
                URL = clsFunctions.GetAPINameFromPreset("ipaddress1", cmbAuto_Dept.Text);
            else
                URL = clsFunctions.GetAPINameFromPreset("ipaddress", cmbAuto_Dept.Text);

            if (installType == "M")
            {
                saveInstallationApiName = clsFunctions.GetAPINameFromPreset("m_SaveInstall", cmbAuto_Dept.Text);
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


            apiUrl = "http://192.168.1.13:8089/Mother_API/plant_reg_history/saveInstallationDetails";

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
                    ""installationStatus"":""Y"",
                    ""plant_location"":""{lb_PlantLocation.Text}""
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
			        ""plant_location"":""{lb_PlantLocation.Text}"",
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
                        if (responseContent == "{\"Plant Installation History Not Store \":0}")
                        {
                            //clsFunctions_comman.UniBox($"Installation Details not saved, invalid Device ID : ('" + installationType + "')");
                            clsFunctions_comman.ErrorLog($"MotherAPI: Installation Details not saved : ('" + installationType + "')");
                        }
                        else if (responseContent == "{\"Plant Installation History Store Successfully \":1}")
                        {
                            clsFunctions_comman.ErrorLog($"MotherAPI: Installation Details saved successfully : ('" + installationType + "')");
                            //clsFunctions_comman.UniBox($"Installation Details saved successfully : '" + installationType + "'");
                        }
                        else
                        {

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

        //-----------------------------------------------------------------------------------------------------------

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

        //-----------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------


    }
}
