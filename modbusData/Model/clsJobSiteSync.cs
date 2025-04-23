using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Uniproject.Classes.Model
{
    public static class clsJobSiteSync
    {

        private static readonly HttpClient client = new HttpClient();

        //------------------------------------------------------------------------------------------------------------

        public static async Task<string> InsertWorkOrderSiteAsync(string woCode, string siteName, string latitude, string longitude)
        {
            string URL = "pmcscada.in";
            string apiName, baseurl = "";

            woCode = woCode.Trim();
            siteName = siteName.Trim();
            latitude = latitude.Trim();
            longitude = longitude.Trim();

            var requestBody = new
            {
                wo_code = woCode,
                site_name = siteName,
                latitude = latitude,
                longitude = longitude
            };

            //-----------------------
            //if (clsFunctions.aliasName == "PWD - BT")
            //    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", clsFunctions.aliasName);
            //else
            //    URL = clsFunctions.GetAPINameFromPreset("ipaddress", clsFunctions.aliasName);
            ////-----------------------

            //apiName = clsFunctions.GetAPINameFromServerMapping("Post_SiteName", clsFunctions.aliasName);

            //baseurl = "" + URL + "" + apiName;                // For LIVE

            //baseurl = "http://192.168.1.13:8089/UniPro_Rest/work_orders/insert_work_order_site";      // For TESTING

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(baseurl, content);                                
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic responseJson = JsonConvert.DeserializeObject(responseString);
                return responseJson.message;
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at InsertWorkOrderSiteAsync() " + ex.Message);

                return "Error: " + ex.Message;
            }
        }

        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------


    }
}
