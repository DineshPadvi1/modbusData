using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Uniproject.Classes;

namespace Uniproject.Masters
{
    public partial class WO_API : Form
    {
        public WO_API()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------------------------------------------------------

        public bool isButtonClicked = false;
        private void WO_APL_Load(object sender, EventArgs e)
        {
            try
            {
                clsFunctions.activeDeptName = clsFunctions.GetActiveDeptNameFromServerMapping();

                countShow();
                plantCode = clsFunctions.GetActivePlantCodeFromServerMapping();        //clsFunctions.loadSingleValueSetup("Select plantcode from plantsetup");
                //this.FormBorderStyle = FormBorderStyle.None;
                //this.ControlBox = true;
                
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.RowHeadersWidth = 4;
                dataGridView1.AllowUserToAddRows = false;

                DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
                checkboxColumn.DisplayIndex = 0;
                checkboxColumn.Width = 40;
                checkboxColumn.HeaderText = "Select";
                checkboxColumn.Name = "chkSelect";
                dataGridView1.Columns.Add(checkboxColumn);

                dataGridView1.Show();
            }
            catch(Exception ex) 
            {
                clsFunctions_comman.UniBox("WO_APL_Load : " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API_Load : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        public string plantCode = clsFunctions.GetActivePlantCodeFromServerMapping();    //clsFunctions.loadSingleValueSetup("select plantcode from PlantSetup");      // getting plantCode from database

        //------------------------------------------------------------------------------------------------------------

        public void countShow()         //BhaveshT
        {
            try
            {
                pCode.Text = plantCode;
                var siteCount1 = clsFunctions.loadSingleValue("SELECT COUNT(*) FROM Site_Master");
                siteCount.Text = siteCount1.ToString();
                var woCount1 = clsFunctions.loadSingleValue("SELECT COUNT(*) FROM WorkOrder");
                woCount.Text = woCount1.ToString();
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("countShow() : " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - countshow() : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        // 23/07/2024 : BhaveshT - GetWODetails() - Modified this method to get multiple sites from JSON and display in DataGridView for selection.

        private void GetWODetails()   // get workCode, workName, contractor code, contractor name and site Name from API     
        {
            try
            {
                //string baseurl = "http://192.168.1.8:8080/getwoapi/getAllocatedWorkOrdersForPlant?plantCode=" + plantCode;
                //string baseurl = "http://" + IP + ":" + Port + "/getwoapi/getAllocatedWorkOrdersForPlant?plantCode=" + plantCode;
                //string getWorkOrderForPlant = clsFunctions.loadSingleValueSetup("SELECT endpoints FROM Api_Details where endpoints_PlantType='BOTH' AND used_for='getWObyPlantCode'");
                //string baseurl = clsFunctions.protocol + "://" + clsFunctions.URL + getWorkOrderForPlant + plantCode;
                //string baseurl = "http://pmcscada.in/uniproapi/getAllocatedWorkOrdersForPlant?plantCode=" + plantCode;    // commented by dinesh

                string baseurl = "";
                //--------------------------------------

                clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

                string URL = "pmcscada.in";
                string apiName = "";
                //URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);

                //-----------------------
                if (clsFunctions.aliasName == "PWD - BT")
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", clsFunctions.aliasName);
                else
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress", clsFunctions.aliasName);
                //-----------------------

                apiName = clsFunctions.GetAPINameFromServerMapping("GetWO", clsFunctions.aliasName);

                baseurl = "" + URL + "" + apiName + plantCode;

                baseurl = removeWWW(baseurl);      // For LIVE

                //baseurl = "http://192.168.1.13:8089/UniPro_Rest/work_orders/get_work_orders_site_against_plant_code?plant_code=" + plantCode + " ";      // For TESTING
                //baseurl = URL + "/pmc_unipro_rest/work_orders/get_work_orders_site_against_plant_code?plant_code=" + plantCode + " ";      // For TESTING

                //--------------------------------------

                //string pmc = "pmcscada.in";
                //var url = new Uri(string.Format(baseurl, clsFunctions.IP, clsFunctions.Port));      //clsFunctions.PlantCode

                //using (var client = new HttpClient())
                //{
                //    var responseTask = client.GetStringAsync(baseurl);      // HTTP 
                //    responseTask.Wait();
                //    string result = responseTask.Result;

                //    if (result == "") return;

                //    List<WODetails> plants = JsonConvert.DeserializeObject<List<WODetails>>(result);
                //    foreach (var plant in plants)
                //    {
                //        string plantRegNo = plants[0].plantRegNo;

                //        foreach (var wo in plant.allocatedWorkOrders)
                //        {
                //            string woCode = wo.woCode;
                //            string woName = wo.woName;
                //            string conCode = wo.conCode;
                //            string conName = wo.conName;
                //            string siteName = wo.siteName;

                //            if(siteName == "-")
                //            {
                //                siteName = "NA";
                //            }

                //            int rowIndex = dataGridView1.Rows.Add();

                //            dataGridView1.Rows[rowIndex].Cells["workcode"].Value = wo.woCode;                              
                //            dataGridView1.Rows[rowIndex].Cells["workname"].Value = wo.woName;                              
                //            dataGridView1.Rows[rowIndex].Cells["concode"].Value = wo.conCode;                              
                //            dataGridView1.Rows[rowIndex].Cells["conname"].Value = wo.conName;                              
                //            dataGridView1.Rows[rowIndex].Cells["sitename"].Value = wo.siteName;                            
                //            dataGridView1.Rows[rowIndex].Cells["flag"].Value = 'Y';
                //        }
                //    }
                //}

                // To get multiple site_names from JSON and display in DataGridView for selection. -------------------

                using (var client = new HttpClient())
                {
                    var responseTask = client.GetStringAsync(baseurl); // HTTP 
                    responseTask.Wait();
                    string result = responseTask.Result;

                    if (string.IsNullOrEmpty(result)) return;

                    WODetails plant = JsonConvert.DeserializeObject<WODetails>(result); // Deserialize to a single WODetails object

                    string plantRegNo = plant.plantRegNo;

                    foreach (var wo in plant.allocatedWorkOrders)
                    {
                        foreach (var site in wo.site_names)
                        {
                            string siteName = site.site_name == "-" ? "NA" : site.site_name;

                            int rowIndex = dataGridView1.Rows.Add();
                            dataGridView1.Rows[rowIndex].Cells["workcode"].Value = wo.wo_code;
                            dataGridView1.Rows[rowIndex].Cells["workname"].Value = wo.wo_name;
                            dataGridView1.Rows[rowIndex].Cells["concode"].Value = wo.con_code;
                            dataGridView1.Rows[rowIndex].Cells["conname"].Value = wo.con_name;
                            dataGridView1.Rows[rowIndex].Cells["sitename"].Value = siteName;
                            dataGridView1.Rows[rowIndex].Cells["flag"].Value = 'Y';
                        }

                        // If there are no site names, add a row with "NA" as site name
                        if (wo.site_names.Count == 0)
                        {
                            int rowIndex = dataGridView1.Rows.Add();
                            dataGridView1.Rows[rowIndex].Cells["workcode"].Value = wo.wo_code;
                            dataGridView1.Rows[rowIndex].Cells["workname"].Value = wo.wo_name;
                            dataGridView1.Rows[rowIndex].Cells["concode"].Value = wo.con_code;
                            dataGridView1.Rows[rowIndex].Cells["conname"].Value = wo.con_name;
                            dataGridView1.Rows[rowIndex].Cells["sitename"].Value = "NA";
                            dataGridView1.Rows[rowIndex].Cells["flag"].Value = 'Y';
                        }
                    }
                }
                //-------------------------------

            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at GetWODetails() : " + ex.Message);
                clsFunctions_comman.ErrorLog("Exception at GetWODetails() : " + ex.Message);
            }
        }

        //------------------------------------------------------------------------------------------------------------

        public static string removeWWW(string url)
        {
            // string url = "http:/www.pmcscada.in/uniproapi/getAllWorkOrders";

            // Check if the URL starts with "http://" and contains "www."
            if (url.StartsWith("http://") && url.Contains("www."))
            {
                // Remove the "www." portion
                int startIndex = url.IndexOf("www.");
                int length = 4; // Length of "www."
                url = url.Remove(startIndex, length);
            }
            return url;
        }

        //------------------------------------------------------------------------------------------------------------

        private async void GetAllWO_old()     //get all workCode, workName, contractor code, contractor name and sitename from API      
        {
            try
            {
                //string baseurl = "http://pmcscada.in/uniproapi/getAllWorkOrders"; //commented by dinesh

                //--------------------------------------
                string baseurl = "";
                clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

                string URL = "pmcscada.in";
                string apiName = "";
                //URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);

                //-----------------------
                if (clsFunctions.aliasName == "PWD - BT")
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", clsFunctions.aliasName);
                else
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress", clsFunctions.aliasName);
                //-----------------------

                apiName = clsFunctions.GetAPINameFromServerMapping("GetAllWO", clsFunctions.aliasName);

                baseurl = "" + URL + "" + apiName;

                // Only for Testing ---------------------
                baseurl = "http://192.168.1.13:8089/UniPro_Rest/work_orders/get_all_work_orders_data";
                //--------------------------------------

                baseurl = removeWWW(baseurl);

                using (var client = new HttpClient())
                {
                    var responseTask = client.GetStringAsync(baseurl);      // HTTP 
                    responseTask.Wait();
                    string result = responseTask.Result;
                    int i = 0;

                    List<getAllWO> plants = JsonConvert.DeserializeObject<List<getAllWO>>(result);
                    foreach (var plant in plants)
                    {
                        foreach (var wo in plants)
                        {
                            string woCode = plants[i].woCode;
                            string woName = plants[i].woName;
                            string conCode = plants[i].conCode;
                            string conName = plants[i].conName;
                            string siteName = plants[i].siteName;

                            int rowIndex = dataGridView1.Rows.Add();

                            dataGridView1.Rows[rowIndex].Cells["workcode"].Value = woCode;
                            dataGridView1.Rows[rowIndex].Cells["workname"].Value = woName;
                            dataGridView1.Rows[rowIndex].Cells["concode"].Value = conCode;
                            dataGridView1.Rows[rowIndex].Cells["conname"].Value = conName;
                            dataGridView1.Rows[rowIndex].Cells["siteName"].Value = siteName;
                            dataGridView1.Rows[rowIndex].Cells["flag"].Value = 'N';

                            i++;

                            if (i == plants.Count)                              // BhaveshT
                            { 
                                return; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at GetAllWO(): " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - GetAllWO() : " + ex.Message);     //BhaveshT
            }
        }

        //public class SiteName
        //{
        //    public string Site_Name { get; set; }
        //}
        //public class GetAllWO1
        //{
        //    public string WoCode { get; set; }
        //    public string WoName { get; set; }
        //    public string ConCode { get; set; }
        //    public string ConName { get; set; }
        //    public List<SiteName> SiteNames { get; set; }
        //}

        //public class AllWorkOrdersData
        //{
        //    public List<GetAllWO1> All_Work_Orders_Data { get; set; }
        //}

        //------------------------------------------------------------------------------------------------------------

        // 24/07/2024 : BhaveshT - GetAllWO() - Modified this method to get multiple sites from JSON and display in DataGridView for selection.

        private async void GetAllWO()     // Get all workCode, workName, contractor code, contractor name, and siteName from API
        {
            try
            {
                //--------------------------------------
                string baseurl = "";
                clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

                string URL = "pmcscada.in";
                string apiName = "";
                //URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);

                //-----------------------
                if (clsFunctions.aliasName == "PWD - BT")
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", clsFunctions.aliasName);
                else
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress", clsFunctions.aliasName);
                //-----------------------

                apiName = clsFunctions.GetAPINameFromServerMapping("GetAllWO", clsFunctions.aliasName);

                baseurl = "" + URL + "" + apiName ;

                // Only for Testing ---------------------
                //baseurl = "http://192.168.1.13:8089/UniPro_Rest/work_orders/get_all_work_orders_data";
                //baseurl = URL + "/pmc_unipro_rest/work_orders/get_all_work_orders_data";
                //--------------------------------------

                baseurl = removeWWW(baseurl);

                //-----------------------
                // Make HTTP request
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(baseurl);
                    var allWorkOrdersData = JsonConvert.DeserializeObject<AllWorkOrdersData>(response);

                    if (allWorkOrdersData != null && allWorkOrdersData.All_Work_Orders_Data != null)
                    {
                        foreach (var workOrder in allWorkOrdersData.All_Work_Orders_Data)
                        {
                            if (workOrder.SiteNames != null && workOrder.SiteNames.Count > 0)
                            {
                                foreach (var site in workOrder.SiteNames)
                                {
                                    int rowIndex = dataGridView1.Rows.Add();

                                    dataGridView1.Rows[rowIndex].Cells["workcode"].Value = workOrder.WoCode ?? "N/A";
                                    dataGridView1.Rows[rowIndex].Cells["workname"].Value = workOrder.WoName ?? "N/A";
                                    dataGridView1.Rows[rowIndex].Cells["concode"].Value = workOrder.ConCode ?? "N/A";
                                    dataGridView1.Rows[rowIndex].Cells["conname"].Value = workOrder.ConName ?? "N/A";
                                    dataGridView1.Rows[rowIndex].Cells["siteName"].Value = site.Site_Name ?? "N/A";
                                    dataGridView1.Rows[rowIndex].Cells["flag"].Value = 'N';
                                }
                            }
                            else
                            {
                                int rowIndex = dataGridView1.Rows.Add();

                                dataGridView1.Rows[rowIndex].Cells["workcode"].Value = workOrder.WoCode ?? "N/A";
                                dataGridView1.Rows[rowIndex].Cells["workname"].Value = workOrder.WoName ?? "N/A";
                                dataGridView1.Rows[rowIndex].Cells["concode"].Value = workOrder.ConCode ?? "N/A";
                                dataGridView1.Rows[rowIndex].Cells["conname"].Value = workOrder.ConName ?? "N/A";
                                dataGridView1.Rows[rowIndex].Cells["siteName"].Value = "No Sites";
                                dataGridView1.Rows[rowIndex].Cells["flag"].Value = 'N';
                            }
                        }
                    }
                    else
                    {
                        clsFunctions_comman.UniBox("No work orders data found.");
                        clsFunctions_comman.ErrorLog("No work orders data found.");
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at GetAllWO(): " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - GetAllWO() : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        public class SiteName
        {
            [JsonProperty("site_name")]
            public string Site_Name { get; set; }
        }

        //--------------------------------------

        public class GetAllWO1
        {
            [JsonProperty("wo_code")]
            public string WoCode { get; set; }

            [JsonProperty("wo_name")]
            public string WoName { get; set; }

            [JsonProperty("con_code")]
            public string ConCode { get; set; }

            [JsonProperty("con_name")]
            public string ConName { get; set; }

            [JsonProperty("site_names")]
            public List<SiteName> SiteNames { get; set; }
        }

        //--------------------------------------

        public class AllWorkOrdersData
        {
            [JsonProperty("All_Work_Orders_Data")]
            public List<GetAllWO1> All_Work_Orders_Data { get; set; }
        }

        //------------------------------------------------------------------------------------------------------------

        private void btnGetPlant_Click(object sender, EventArgs e)          // Get Work Orders by Plant code
        {
            try
            {
                isButtonClicked = false;
                dataGridView1.Rows.Clear();
                //btnGetPlant.Enabled = false;
                //btnGetAllWO.Enabled = false;
                GetWODetails();
                btnImport.Enabled = true;
                int rowCount = dataGridView1.RowCount;
                txtRowCount.Text = Convert.ToString(rowCount-1);
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at btnGetPlant_Click: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - btnGetPlant_Click : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void btnGetAllWO_Click(object sender, EventArgs e)          // Get All Work Orders
        {
            try
            {
                countShow();
                isButtonClicked = true; 
                dataGridView1.Rows.Clear();
                //btnGetAllWO.Enabled = false;
                //btnGetPlant.Enabled = false;
                GetAllWO();
                btnImport.Enabled = true;
                int rowCount = dataGridView1.RowCount;
                txtRowCount.Text = Convert.ToString(rowCount-1);
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at btnGetAllWO_Click: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - btnGetAllWO_Click : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void selectTickedRows()
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chkSelect = dataGridView1.Rows[i].Cells["chkSelect"] as DataGridViewCheckBoxCell;
                    if (chkSelect != null && chkSelect.Value != null && (bool)chkSelect.Value)
                    {
                        dataGridView1.Rows[i].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at selectTickedRows: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - selectTickedRows() : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void tickSelectedRow()
        {
            try
            {
                // Loop through each row in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Check if the row is selected
                    if (row.Selected)
                    {
                        // Get the checkbox cell in the row
                        DataGridViewCheckBoxCell checkboxCell = row.Cells["chkSelect"] as DataGridViewCheckBoxCell;
                        // Check the checkbox in the cell
                        checkboxCell.Value = true;                              // checkboxCell.TrueValue;
                        row.DefaultCellStyle.BackColor = Color.LightGreen;      // Change the background color of the row
                    }
                }
            }
            catch(Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at tickSelectedRow: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - tickSelectedRow() : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        // 24/07/2024 : BhaveshT - Modified IMPORT button_Click code to insert all the jobsite which are not imported.

        private void btnImport_Click(object sender, EventArgs e)        // Import button
        {
            selectTickedRows();
            int siteNamePass = 0;
            int custPass = 0;
            int WCpass=0;
            List <string> workCodeList = new List<string>();

            //-----------------------------------------------------------------------

            string workType = clsFunctions.dept;
            int s = dataGridView1.SelectedRows.Count;

            if(dataGridView1.SelectedRows.Count==0) 
            {
                clsFunctions_comman.UniBox("Please select a row.");
                return;
            }
            try
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    var row = dataGridView1.SelectedRows[i];
                    if (row != null)
                    {
                        var workcode = row.Cells["workcode"].Value.ToString();
                        var workname = row.Cells["workname"].Value.ToString();
                        var contr_code = row.Cells["concode"].Value.ToString();
                        var contr_name = row.Cells["conname"].Value.ToString();
                        var site_name = row.Cells["sitename"].Value.ToString();
                        var flag = row.Cells["flag"].Value.ToString();
                        workcode = workcode.Trim();
                        workname = "" + workname.Replace("'", "''") + ""; // by dinesh
                        workname = workname.Trim(); 

                        contr_code = contr_code.Trim();
                        contr_name = contr_name.Trim();
                        site_name = site_name.Trim();

                        //---------------------------------
                        workname = workname.Replace(",", " ");
                        //---------------------------------

                        //Converting siteName from '-' to 'NA' while inserting in Site_Master table of Unipro database
                        if (site_name == "-")
                        {
                            site_name = "NA";
                        }

                        string modifiedSiteName = site_name.Replace("[", "").Replace("]", "");

                        //------------------------ Insert to Site Master ---------------

                        string insertSite = "INSERT INTO Site_Master (workorderID, sitename, Flag) values ('" + workcode + "','" + modifiedSiteName + "','" + flag + "')";
                        string chkSite = "SELECT COUNT(*) FROM Site_Master WHERE workorderID = '" + workcode + "' AND SiteName = '"+ modifiedSiteName + "' ";     //checking if site already exists or not

                        int count1 = Convert.ToInt32(clsFunctions.loadSingleValue(chkSite));

                        if (count1 == 0)
                        {
                            siteNamePass = clsFunctions.AdoData(insertSite);
                        }
                        if (count1 != 0)
                        {
                            //clsFunctions_comman.UniBox("Site already exists.");
                            siteNamePass = 0;
                        }

                        //----------------------- Insert to Customer Master ------------------------

                        string insertCustomer = " INSERT INTO Customer_Master (Customer_Code, Customer_Name, flag) values ('" + contr_code + "', '" + contr_name + "', '" + flag + "') ";

                        string chkCust = "SELECT COUNT(*) FROM Customer_Master WHERE Customer_Code = '" + contr_code + "'";     //checking if site already exists or not
                        int conCount = Convert.ToInt32(clsFunctions.loadSingleValue(chkCust));

                        if (conCount == 0)
                        {
                            custPass = clsFunctions.AdoData(insertCustomer);
                        }
                        if (conCount != 0)
                        {
                            //clsFunctions_comman.UniBox("Customer already exists.");
                            custPass = 0;
                        }

                        //-------------------------- Insert to WorkOrder -----------------------------

                        //string insertWO = "INSERT INTO WorkOrder (workno, workname, worktype, ContractorID, ContractorName, Flag, iscompleted) values ('" + workcode + "', '" + workname + "', 'PMC', '" + contr_code + "','" + contr_name + "','" + flag + "','N')";
                        string insertWO = "INSERT INTO WorkOrder (workno, workname, worktype, ContractorID, ContractorName, Flag, iscompleted) values ('" + workcode + "', '" + workname + "', '" + clsFunctions.activeDeptName + "', '" + contr_code + "','" + contr_name + "','" + flag + "','N')";

                        string chkWO = "SELECT COUNT(*) FROM WorkOrder WHERE workno = '" + workcode + "' AND WorkType = '"+ clsFunctions.activeDeptName +"' AND ContractorName = '"+ contr_name + "' ";     //checking if already exists or not
                        int count = Convert.ToInt32(clsFunctions.loadSingleValue(chkWO));
                    
                        if (count == 0)
                        {
                            WCpass = clsFunctions.AdoData(insertWO);
                            if (isButtonClicked)
                            {
                                PostWCPC(workcode, plantCode);       //allocation
                                isButtonClicked = false;
                            }
                        }
                        if (count != 0)
                        {
                            //clsFunctions_comman.UniBox("Work Details already exists.");
                            WCpass = 0;
                        }

                        if (siteNamePass == 0 && custPass == 0 && WCpass == 0)
                        {
                            //clsFunctions_comman.UniBox(workcode + " work order already exists.");
                        }
                        else
                            workCodeList.Add(workcode);                                                      // Working here for showing the list after saving work ordr 
                    }
                    else
                    {
                        clsFunctions_comman.UniBox("Please Select a Row");
                    }
                }
                if (WCpass == 1)  //(siteNamePass == 1 && custPass == 1 && WCpass == 1)
                    clsFunctions_comman.UniBox("Work Details saved successfully.");

                //btnGetPlant.Enabled = false;
                //btnImport.Enabled = false;
                dataGridView1.ClearSelection();
                countShow();
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at btnImport_Click: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - btnImport_Click : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void PostWCPC(string workcode, string plantCode)     //allocateWorkOrder
        {
            try
            {
                // http://192.168.1.8:8080/getwoapi/allocateWorkOrder?workCode=121235&plantCode=2
                var WP = new PostWCPC();

                WP.plantRegNo = plantCode;
                WP.woCode = workcode;

                //string posturl = "http://" + IP + ":" + Port + "/getwoapi/allocateWorkOrder?workCode=" + workcode + "&plantCode=" + plantCode;
                //http://192.168.1.62:8080/uniproapi/allocateWorkOrder?workCode=121235&plantCode=5
                //string PostWCPCAPI = clsFunctions.loadSingleValueSetup("Select endpoints from Api_details where endpoints_PlantType='BOTH' and used_for='importWO'");
                //PostWCPCAPI.Trim();


                //string posturl = "" + clsFunctions.protocol + "://" + clsFunctions.URL + PostWCPCAPI + plantCode;

                //--------------------------------------
                string posturl = "";

                string baseurl = "";
                clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

                string URL = "pmcscada.in";
                string apiName = "";
                //URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);

                //-----------------------
                if (clsFunctions.aliasName == "PWD - BT")
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress1", clsFunctions.aliasName);
                else
                    URL = clsFunctions.GetAPINameFromPreset("ipaddress", clsFunctions.aliasName);
                //-----------------------

                apiName = clsFunctions.GetAPINameFromServerMapping("AllocateWO", clsFunctions.aliasName);

                // Only for testing ----------------------------------

                //apiName = "http://192.168.1.13:8089/UniPro_Rest/work_orders/allocateWorkOrder?workCode=\" \"&plantCode=";
                // /vipl_unipro_rest/work_orders/allocateWorkOrder?workCode=" "&plantCode=

                //apiName = apiName.Replace(" ", workcode);
                //posturl = apiName + plantCode;

                // For Live --------------------------------------

                apiName = apiName.Replace(" ", workcode);
                posturl = "" + URL + "" + apiName + plantCode;

                //---------------------------------------------------

                posturl = posturl.Replace("\"", "");        // 27/03/2024 - BhaveshT : added to solve issue, WO not getting allocated.
                                

                //string posturl = "http://" + IP + "/uniproapi/allocateWorkOrder?workCode=" + workcode + "&plantCode=" + plantCode; //commented by dinesh
                posturl = removeWWW(posturl);

                using (var client = new HttpClient())
                {
                    var jsonobj = Newtonsoft.Json.JsonConvert.SerializeObject(WP);
                    var inpujson = new StringContent(jsonobj, System.Text.Encoding.UTF8, "application/json");
                    
                    var postTask = client.PostAsync(posturl, inpujson);         // HTTP POST
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        clsFunctions_comman.UniBox("WorkCode: "+ workcode + " \n Allocated successfully to PlantCode: " + plantCode + " ");        //BhaveshT
                        clsFunctions_comman.ErrorLog("WorkCode: " + workcode + " \n Allocated successfully to PlantCode: " + plantCode + " ");     //BhaveshT
                    }
                    else
                    {
                        clsFunctions_comman.UniBox("Error occured while allocating WorkCode");
                        clsFunctions_comman.ErrorLog("WO_API - PostWCPC() : Error occured while allocating WorkCode");     //BhaveshT
                    }
                }
            } 
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at PostWCPC(): " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - PostWCPC() : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void btnExit_Click(object sender, EventArgs e)      // Exit button
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)      // Clear button
        {
            try
            {
                countShow();

                dataGridView1.Rows.Clear();
                //btnGetPlant.Enabled = true;
                //btnGetAllWO.Enabled = true;
                txtSearch.Text = string.Empty;
                txtRowCount.Text = string.Empty;
                lblSelectedRow.Text = string.Empty;
            }
            catch(Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at btnClear_Click: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - btnClear_Click : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void btnSearch_Click(object sender, EventArgs e)        // Search button
        {
            try
            {
                string searchValue = txtSearch.Text.Trim();

                dataGridView1.ClearSelection();

                if (string.IsNullOrEmpty(searchValue))
                    return;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            row.Selected = true;
                            tickSelectedRow();
                            break;
                        }
                    }
                }
                int selectedRow = dataGridView1.SelectedRows.Count;
                lblSelectedRow.Text = Convert.ToString(selectedRow-1);
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at Search: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - btnSearch_Click : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void UpdateTickedRowsCount()
        {
            try
            {
                int count = 1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell cell = row.Cells["chkSelect"] as DataGridViewCheckBoxCell;
                    if (cell.Value != null && (bool)cell.Value)
                    {
                        count++;
                    }
                }
                lblSelectedRow.Text = count.ToString();
            }
            catch(Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at UpdateTickedRowsCount(): " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - UpdateTickedRowsCount() : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && !(dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn))
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = null;
                    dataGridView1.Rows[e.RowIndex].Selected = true;

                    // Set the event handled to prevent the selection of rows
                    //e.Handled = true;
                }
            }
            catch(Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at dataGridView1_CellMouseDown: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - dataGridView1_CellMouseDown : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    UpdateTickedRowsCount();
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at dataGridView1_CellValueChanged: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - dataGridView1_CellValueChanged : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void chkAll_Click(object sender, EventArgs e)      //Button to check all checkboxes     //BhaveshT
        {
            try
            {
                dataGridView1.ClearSelection();
                if ((chkAll.Text).Contains("Select"))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    //foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["chkSelect"];

                        chk.Value = true;
                        chkAll.Text = "Unselect All";
                    }
                    dataGridView1.Refresh();
                    return;
                }

                if ((chkAll.Text).Contains("Unselect"))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    //foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["chkSelect"];

                        chk.Value = false;
                        chkAll.Text = "Select All";
                    }
                    dataGridView1.Refresh();
                    return;
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at chkAll_Click: " + ex.Message);
                clsFunctions_comman.ErrorLog("WO_API - chkAll_Click : " + ex.Message);     //BhaveshT
            }
        }

        //--------------- 24/07/2024 : BhaveshT -  Shortcut key to enable btnGetAllWO : Alt + F7 ---------------------

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F7)
            {
                if (btnGetAllWO.Enabled == true)
                {
                    btnGetAllWO.Enabled = false;
                }
                else if (btnGetAllWO.Enabled == false)
                {
                    btnGetAllWO.Enabled = true;
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------


    }
}

