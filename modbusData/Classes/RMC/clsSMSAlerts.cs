using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Uniproject.Masters;

namespace Uniproject.Classes.RMC
{
    public class clsSMSAlerts
    {

        public static string smsTemplate = "";
        public DataTable dt_mobNo = new DataTable();
        public string mobileNumbers = "";
        public bool isSMSsent = false;
        clsErrorPercent_Calculation clsErr = new clsErrorPercent_Calculation();

        public void GetContactNoFromWO(string WO)
        {
            try
            {
                //string apiName = clsFunctions.GetAPIName("getMobNoFromWO","BOTH");


                //string url = "http://192.168.1.64:8089/UniPro_Rest/rmc/get_mobile_numbers_from_wo_code?work_code=";
                //string url = "http://192.168.1.64:8089"+ apiName +"";
                //string url = "http://103.175.176.26:8080" + apiName + "";       // static url

                //--------------------------------------
                clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

                string URL = "pmcscada.in";
                string apiName = "";
                URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);
                apiName = clsFunctions.GetAPINameFromServerMapping("GetMobNoFromWO", clsFunctions.aliasName);

                string url = "" + URL + "" + apiName;

                //baseurl = WO_API.removeWWW(baseurl);
                //--------------------------------------

                //preparing for the parameters that needs to be sent along with apiURL
                //string parameters = $"projectName={Uri.EscapeDataString(projectName)}&mobileNo={Uri.EscapeDataString(mobileNo)}&message={Uri.EscapeDataString(smsText)}";

                Uri apiUrl = new Uri($"{url}{WO}");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET";
                request.Accept = "application/json";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException($"Failed : HTTP error code : {response.StatusCode}");
                    }

                    using (Stream responseStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string mobNoString = reader.ReadToEnd();

                        JObject jsonObject = JObject.Parse(mobNoString);

                        mobileNumbers = jsonObject["mobile_numbers"].ToString();

                        // Remove "null" values and extra commas
                        mobileNumbers = mobileNumbers.Replace("null,", "").Replace("null", "");

                        if(!mobileNumbers.Contains("Not"))
                        {
                            InsertMobNoIntoDB(WO, mobileNumbers);
                        }
                        else
                        {
                            mobileNumbers = "";
                        }
                        //dt_mobNo = ParseJsonAndCreateDataTable(output);


                        //Console.WriteLine("Output from Server .... \n");
                        //Console.WriteLine(mobNoString);
                    }
                }
            }
            catch(Exception ex)
            {

                clsFunctions_comman.ErrorLog("Exception at GetContactNoFromWO('"+WO+"') : " + ex.Message );
                //clsFunctions_comman.UniBox("Exception while get Contact No. from WO : " + ex.Message);
            }

            

        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        
        public static string ParseJsonResponse(string jsonResponse)
        {
            // Add your JSON parsing logic here
            // For simplicity, assuming a straightforward structure
            if (jsonResponse.Contains("\"sms_template\""))
            {
                int startIndex = jsonResponse.IndexOf(":") + 1;
                int endIndex = jsonResponse.LastIndexOf("\"");
                return jsonResponse.Substring(startIndex, endIndex - startIndex).Trim('"', ' ', '\n', '\r');
            }
            return null;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async void SendErrorSMSAlert(string WO, string BatchNo, string date, clsErrorPercent_Calculation clsErr)
        {
            try
            {
                mobileNumbers = CheckMobileNoAginstWOinDB(WO);

                if (mobileNumbers == "")
                {
                    GetContactNoFromWO(WO);

                    //string smsTemplate = await GetSMSTemplateForProductionErrorAlert();
                    string smsTemplate = await GetSMSTemplateForProductionErrorAlert();

                    isSMSsent = await clsErrorPercent_Calculation.SendSMS("Unipro", BatchNo, date, mobileNumbers, smsTemplate); //Commented sendSMS Functionality because Currently not in use.

                    if (isSMSsent == true)
                    {
                        clsErr.Production_SMS = "Y";
                    }
                    else if (isSMSsent == false)
                    {
                        clsErr.Production_SMS = "N";
                    }
                }
                else
                {
                    //string smsTemplate = await GetSMSTemplateForProductionErrorAlert();
                    string smsTemplate = await GetSMSTemplateForProductionErrorAlert();

                    isSMSsent = await clsErrorPercent_Calculation.SendSMS("Unipro", BatchNo, date, mobileNumbers, smsTemplate); ////Commented sendSMS Functionality because Currently not in use.


                    if (isSMSsent == true)
                    {
                        clsErr.Production_SMS = "Y";
                    }
                    else if (isSMSsent == false)
                    {
                        clsErr.Production_SMS = "N";
                    }

                }
            }
            catch(Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at SendErrorSMSAlert(): " + ex.Message);
                //clsFunctions_comman.UniBox("Exception at SendErrorSMSAlert(): " + ex.Message);
            }
            


        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable ParseJsonAndCreateDataTable(string jsonString)
        {
            // Parse JSON string
            JObject jsonObject = JObject.Parse(jsonString);

            // Create DataTable
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MobileNumber", typeof(string));

            // Extract mobile_numbers array from JSON
            JArray mobileNumbersArray = (JArray)jsonObject["mobile_numbers"];

            // Populate DataTable
            foreach (JToken mobileNumberToken in mobileNumbersArray)
            {
                string mobileNumber = mobileNumberToken.ToString();
                dataTable.Rows.Add(mobileNumber);
            }

            return dataTable;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void InsertMobNoIntoDB(string WO, string mobNoStr)//, string tableName, string connectionString)
        {

            clsFunctions_comman.Ado("INSERT INTO WO_MobileNo (WorkOrderNo, MobileNo) VALUES ( '"+ WO + "', '"+ mobNoStr + "' )");

        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public string CheckMobileNoAginstWOinDB(string WO)
        {
            string str = "";
            try
            {
                if (WO != "")
                {
                    dt_mobNo = clsFunctions_comman.fillDatatable("Select * from WO_MobileNo where WorkOrderNo = '" + WO + "' ");
                    if (dt_mobNo != null)
                    {
                        str = clsFunctions_comman.loadSinglevalue("Select MobileNo from WO_MobileNo where WorkOrderNo = '" + WO + "'");
                    }
                }
                return str;
            }
            catch(Exception ex)
            {

            }
            return str;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------
        static async Task<string> GetSMSTemplateForProductionErrorAlert()
        {
            try
            {
                //string functionality = "Production Alert";
                //string apiName = clsFunctions.GetAPIName("getProdErrorTemplete", "BOTH");

                clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

                string URL = "pmcscada.in";
                string apiName = "";
                URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);
                apiName = clsFunctions.GetAPINameFromServerMapping("GetProdErrorTemplate", clsFunctions.aliasName);

                string apiUrl = "" + URL + "" + apiName;

                // string apiUrl = $"http://192.168.1.64:8089{apiName}";
                //string apiUrl = $"http://103.175.176.26:8080/{apiName}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                string jsonResponse =  reader.ReadToEnd();

                                // Parse JSON to extract the SMS template string
                                smsTemplate = ParseJsonResponse(jsonResponse);

                                if (!string.IsNullOrEmpty(smsTemplate))
                                {
                                    return smsTemplate;
                                }
                                else
                                {
                                    return smsTemplate = "";     //throw new Exception($"Error: SMS template not found for Production Alert.");
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"HTTP Error: {response.StatusCode} - {response.StatusDescription}");
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at GetSMSTemplateForProductionErrorAlert() : " + ex.Message);
                //clsFunctions_comman.UniBox("Exception to Get SMS Template For Production Error Alert : " + ex.Message);
            }
            return smsTemplate;
        }

        //--------------------------
        static async Task<string> GetSMSTemplateForProductionErrorAlert_Old()
        {
            try
            {
                string functionality = "Production Alert";
                string apiName = clsFunctions.GetAPINameFromServerMapping("getProdErrorTemplate", "BOTH");

                //string apiUrl = $"http://192.168.1.64:8089{apiName}";
                string apiUrl = $"http://103.175.176.26:8080/{apiName}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Parse JSON to extract the SMS template string
                        smsTemplate = ParseJsonResponse(jsonResponse);

                        if (!string.IsNullOrEmpty(smsTemplate))
                        {
                            return smsTemplate;
                        }
                        else
                        {
                            throw new Exception($"Error: SMS template not found for '{functionality}'.");
                        }
                    }
                    else
                    {
                        throw new Exception($"HTTP Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at GetSMSTemplateForProductionErrorAlert() : " + ex.Message);
                clsFunctions_comman.UniBox("Exception to Get SMS Template For Production Error Alert : " + ex.Message);
            }
            return smsTemplate;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
