using System;
using System.Data;
using System.Net.Http;
using System.Text;

/*

15/04/2024 - BhaveshT

 To POST data to API which is inserted in table: Plant_LiveStatus_History about plant status
    - Created a static class: post_PlantLiveStatusHistory & method: LiveStatus_Sync(). When data inserted in table 
        using clsFunction.InsertToPlantRenewalHistory(), it will also POST to API those details through LiveStatus_Sync() method.

 */

namespace Uniproject.Classes.MotherDB
{
    public static class post_PlantLiveStatusHistory
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static bool LiveStatus_Sync()
        {
            string device_id ="", plant_code="", plant_type= "", date_time="", software_status="";

            string apiUrl = "http://192.168.1.64:8084/Mother_API/plant_status/insert";
            
            //----------------------------------------------------------------------------
            clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();
            clsFunctions.activeDeviceID = clsFunctions.GetActiveDeviceIdFromServerMapping();

            string URL = "";
            string apiName = "";
            
            //if (clsFunctions.aliasName == "PWD - BT")
            //    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", clsFunctions.aliasName);
            //else
            //    URL = clsFunctions.GetAPINameFromPreset("ipaddress", clsFunctions.aliasName);


            apiName = clsFunctions.GetAPINameFromServerMapping("Plant_LiveStatus_History", clsFunctions.aliasName);
            //----------------------------------------------------------------------------
            URL = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='" + clsFunctions.aliasName + "'");

            apiUrl = URL + apiName;

            DataTable dt = new DataTable();
            
            dt = clsFunctions.fillDatatable_setup("SELECT * FROM Plant_LiveStatus_History WHERE ID = (SELECT MAX(ID) FROM Plant_LiveStatus_History)");

            foreach (DataRow row in dt.Rows)
            {
                device_id = row["DeviceID"].ToString();
                plant_code = row["PlantCode"].ToString();
                plant_type = row["PlantType"].ToString();
                date_time = row["Date_Time"].ToString();
                software_status = row["Software_Status"].ToString();
            }

            // Prepare data to send
            var requestData = new
            {
                device_id = device_id,
                plant_code = plant_code,
                plant_type = plant_type,
                date_time = Convert.ToDateTime(date_time).ToString("yyyy-MM-dd HH:mm:ss"),
                software_status = software_status
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Send POST request synchronously
                var response = _httpClient.PostAsync(apiUrl, content).Result;

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    clsFunctions_comman.ErrorLog($"LiveStatus_Sync(): Failed to send data. Status code: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog($"LiveStatus_Sync(): An error occurred: {ex.Message}");
                return false;
            }
        }



    }
}
