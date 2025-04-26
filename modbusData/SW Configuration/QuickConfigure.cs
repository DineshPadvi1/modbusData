using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.Classes.Model;
using Uniproject.UtilityTools;

namespace Uniproject.SW_Configuration
{
    public partial class QuickConfigure : Form
    {
        public QuickConfigure()
        {
            InitializeComponent();
        }

        public static string conName;
        public static string conCode;
        public static string DeviceID;
        public static string plantCode;
        public static string plantType;
        public static string dept;

        private void QuickConfigure_Load(object sender, EventArgs e)
        {
            try
            {
                FetchUniproFromDBAndShow();

            }
            catch 
            {

            }
        }

        //---------------------------------------------------------------------------------------------------

        public void FetchUniproFromDBAndShow()
        {
            DataTable dt_plantSetup = clsFunctions.fillDatatable_setup("Select * from PlantSetup");

            DataRow row = dt_plantSetup.Rows[0];

            conName = row["ContractorName"].ToString();
            conCode = row["ContractorCode"].ToString();
            DeviceID = row["DeviceID"].ToString();
            plantCode = row["PlantCode"].ToString();
            plantType = row["PlantType"].ToString();
            dept = row["DeptName"].ToString();

            
            clsFunctions.FillCombo_setup("Select Distinct Plant_Make from Unipro_Setup", cmbPlantMake);
            string strPlantMake = clsFunctions.loadSinglevalue_setup("Select Plant_Make from Unipro_Setup where Status = 'Y'");
            cmbPlantMake.Text = strPlantMake;
            cmbDescription.Items.Insert(0, "-");

            string strTagType = clsFunctions.loadSinglevalue_setup("Select [Description] from Unipro_Setup where Status = 'Y'");
            cmbDescription.Text = strTagType;  //expects either 'OPC' or 'Serial'
        }

        //---------------------------------------------------------------------------------------------------

        string plantCode1 = clsFunctions.loadSingleValueSetup("Select PlantCode from ServerMapping where Flag='Y'");
        string dept1 = clsFunctions.loadSinglevalue_setup("Select deptname from ServerMapping where Flag='Y'");

        //---------------------------------------------------------------------------------------------------
        public async void uploadUpload_UniproSetupID(string UniproSetupID, string plantType, string departmenttype)
        {
            try
            {
                if (plantType.Contains("RMC"))
                    plantType = "RMC";
                else
                    plantType = "BT";

                string apiUrl = clsFunctions.loadSinglevalue_setup("Select domain1 from AliasName where AliasName='" + dept1.Trim() + " - " + plantType.Trim() + "'") + clsFunctions.loadSinglevalue_setup("Select Upload_UniproSetupID from ServerMapping where deptname='" + dept1 + "' and PlantType='" + plantType + "'");
                apiUrl = apiUrl.Replace("d_type", dept1);
                apiUrl = apiUrl.Replace("p_code", plantCode1);
                apiUrl = apiUrl + UniproSetupID;

                var data = new
                {
                    plant_code = plantCode1,
                    dept_name = dept1,
                    unipro_setup_id = UniproSetupID
                };

                // Serialize the data object to JSON
                string jsonData = JsonConvert.SerializeObject(data);

                // Create the HttpClient
                HttpClient client = new HttpClient();
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Make the PUT request
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                // Check if the request was successful
                response.EnsureSuccessStatusCode();

                // Optionally, you can read the response content
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent != null)
                {
                    if (responseContent.Contains("UniPro_Setup_Id Update Successfully"))
                    {
                        clsFunctions.ErrorLog("UniPro_Setup_Id updated successfully : " + UniproSetupID);

                    }
                    if (responseContent.Contains("Plant Details Not Present Against Plant_Code"))
                    {
                        clsFunctions.ErrorLog("Response from the server: " + responseContent);
                        //MessageBox.Show("Response from the server: " + responseContent, "Server Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (responseContent.Contains("UniPro_Setup_Id Not Update"))
                    {
                        clsFunctions.ErrorLog("Response from the server: " + responseContent);
                        //MessageBox.Show("Response from the server: " + responseContent, "Server Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    // Log inner exception
                    clsFunctions.ErrorLog("Inner Exception: " + innerException.Message);

                    // Move to the next inner exception
                    innerException = innerException.InnerException;
                }

                clsFunctions.ErrorLog("uploadUpload_UniproSetupID : " + ex.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------

        public static string CreateConnectionString(string dbType, string dbPath, string dbPassword)
        {
            try
            {
                switch (dbType.ToLower())
                {
                    case "sqlite":
                        return $"Data Source={dbPath};Password={dbPassword};";

                    case "sqlserver":
                        return $"Server={dbPath};Integrated Security=False;Password={dbPassword};";

                    case "mysql":
                        return $"Server={dbPath};Uid=root;Pwd={dbPassword};";

                    case "postgresql":
                        return $"Host={dbPath};Username=postgres;Password={dbPassword};";

                    case "ms sql":
                        return $"Server={dbPath};Integrated Security=False;Password={dbPassword};";

                    case "ms access":
                        return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database Password={dbPassword};";

                    case "acc db":
                        return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database Password={dbPassword};";
                    
                    case "sqlce":
                        return $"Data Source={dbPath};Password={dbPassword};";
                    
                    default:
                        throw new ArgumentException("Unsupported database type.");
                }
            }
            catch(Exception ex)
            {
                clsFunctions.ErrorLog("Exception at CreateConnectionString(): - " + ex.Message);
                return "";
            }
        }

        //---------------------------------------------------------------------------------------------------

        private void btnDBVesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormOpen("SelectClientDBVersion"))
                {
                    MessageBox.Show("The form is already open!");
                }
                else
                {
                    SelectClientDBVersion s = new SelectClientDBVersion();
                    //s.MdiParent = this;
                    s.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception at btnDBVesion_Click: " + ex.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------

        private void btnFetchSerMapPreset_Click(object sender, EventArgs e)
        {
            if (NetworkHelper.IsInternetAvailable())
            {
                int a = clsFunctions.GetAndUpdateServerMappingDataFromAPI(clsFunctions.aliasName, clsFunctions.activeDeptName);

                if (a == 1)
                    clsFunctions_comman.UniBox("API and Websites data successfully updated for " + clsFunctions.aliasName + "");

                if (a == 0)
                    clsFunctions_comman.UniBox("API and Websites data not updated for " + clsFunctions.aliasName + ", please try again later.");

            }
            else
            {
                MessageBox.Show("Check internet connection.");
                return;
            }
        }

        //---------------------------------------------------------------------------------------------------

        private void btnFetch_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to fetch Header and Transaction data?", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (clsFunctions.CheckInternetConnection())
                {
                    //clsFunctions.AdoData_setup("delete from DataHeaderTableSync where plant_code='" + clsFunctions.activePlantCode + "' and dept_name='" + clsFunctions.activeDeptName + "' and Type='Client'");
                    //clsFunctions.AdoData_setup("delete from DataTransactionTableSync where plant_code='" + clsFunctions.activePlantCode + "' and dept_name='" + clsFunctions.activeDeptName + "' and Type='Client'");
                    //mdiMain.buttonFetchClick();

                    clsFunctions.FetchHeaderAndTransaction(clsFunctions.aliasName, clsFunctions.activeDeptName, clsFunctions.activePlantCode);
                }
                else
                    MessageBox.Show("Please check your internet connection!");

                //MessageBox.Show("Data Fetched successfully!");

            }
            else if (result == DialogResult.No)
            {
                // Code to handle if No is selected
            }
        }

        //---------------------------------------------------------------------------------------------------

        private void btnSync_Click(object sender, EventArgs e)
        {
            if (NetworkHelper.IsInternetAvailable())
            {
                //for uploading data

                DialogResult result = MessageBox.Show("Do you really want to upload Header and Transaction data?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Code to fetch Header and Transaction data
                    if (clsFunctions.CheckInternetConnection())
                    {
                        clsFunctions.AdoData_setup("update DataHeaderTableSync set DataHeaderUpload=0 where plant_code='" + clsFunctions.activePlantCode + "' and dept_name='" + clsFunctions.activeDeptName + "' and Type='Client'");
                        clsFunctions.AdoData_setup("update DataTransactionTableSync set DataHeaderUpload=0 where plant_code='" + clsFunctions.activePlantCode + "' and dept_name='" + clsFunctions.activeDeptName + "' and Type='Client'");

                        //mdiMain.buttonSyncClick();
                        clsFunctions.UploadHeaderAndTransaction(clsFunctions.aliasName, clsFunctions.activeDeptName, clsFunctions.activePlantCode);
                    }
                    else
                        MessageBox.Show("Please check your internet connection!");

                    //MessageBox.Show("Data uploaded successfully!");
                }
                else if (result == DialogResult.No)
                {
                    // Code to handle if No is selected
                }
            }
            else
            {
                MessageBox.Show("Check internet connection.");
                return;
            }
        }

        //---------------------------------------------------------------------------------------------------

        private void btnFetchUniSetup_Click(object sender, EventArgs e)
        {
            bool flag = clsFunctions.AskPrompt();

            if (flag)
            {
                if (NetworkHelper.IsInternetAvailable())
                {
                    int a = clsFunctions.GetAndUpdateUniproSetupFromAPI(clsFunctions.aliasName, clsFunctions.activeDeptName, clsFunctions.activePlantCode);

                    if (a == 1)
                    {
                        clsFunctions_comman.UniBox("UniproSetup data Fetched & inserted successfully.");
                        clsFunctions_comman.ErrorLog("UniproSetup data Fetched & inserted successfully.");
                    }
                    else if (a == 0)
                    {
                        clsFunctions_comman.UniBox("UniproSetup data not inserted.");
                        clsFunctions_comman.ErrorLog("UniproSetup data not inserted.");
                    }

                    FetchUniproFromDBAndShow();
                }
                else
                {
                    MessageBox.Show("Check internet connection.");
                    return;
                }
            }
            else
            {
                // Password is incorrect, display an error message
                MessageBox.Show("Enter Correct password");
            }
        }

        //---------------------------------------------------------------------------------------------------

        private bool IsFormOpen(string formName)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == formName)
                {
                    return true;
                }
            }
            return false;
        }

        //---------------------------------------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //---------------------------------------------------------------------------------------------------

        private void cmbPlantMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt_configData = clsFunctions.fillDatatable_setup("Select * from Unipro_Setup where Plant_Make =  '" + cmbPlantMake.SelectedItem + "'");

                clsFunctions.FillCombo_setup("Select Description from Unipro_Setup where Plant_Make = '" + cmbPlantMake.Text + "'", cmbDescription);

                try
                {
                    if (cmbDescription.Items.Count == 0)
                    {
                        cmbDescription.Items.Insert(0, "-");
                    }
                    cmbDescription.SelectedIndex = 0;
                }
                catch { }

                if (dt_configData.Rows.Count != 0)
                {
                    DataRow row1 = dt_configData.Rows[0];

                    lb_UniSetupID.Text = row1["UniproSetupID"].ToString();

                    lblDataType.Text = row1["DB_Type"].ToString();
                    lblPlantType.Text = row1["PlantType"].ToString();
                    txtPath.Text = row1["Path"].ToString();
                    txtPass.Text = row1["Pass"].ToString();
                    lblFormName.Text = row1["FormName"].ToString();
                    txtConnString.Text = row1["ConnectionString"].ToString();

                    //txtConnString.Text = GenerateConnectionString(lblDataType.Text, txtPath.Text, txtPass.Text);

                    if (lblDataType.Text.ToLower() == "ms access" || lblDataType.Text.ToLower() == "acc db" || lblDataType.Text.ToLower() == "mysql")
                    {
                        btnBuildString.Enabled = true;
                        pnlHeaderTrans.Enabled = true;
                        btnDBVesion.Enabled = true;
                        
                    }
                    else
                    {
                        btnBuildString.Enabled = false;
                        pnlHeaderTrans.Enabled = false;
                        btnDBVesion.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //---------------------------------------------------------------------------------------------------

        private void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPlantMake.Text == "" || cmbDescription.Text == "")
                {
                    clsFunctions_comman.UniBox("Please select all fields!");
                    return;
                }
                else
                {
                    txtConnString.Text = "" + txtConnString.Text.Replace("'", "''") + "";

                    int a = clsFunctions.AdoData_setup("Update Unipro_Setup SET Status = 'N'");
                    string qry = "Update Unipro_Setup set Status = 'Y' , Path = '" + txtPath.Text + "', Description = '" + cmbDescription.Text + "', ConnectionString='" + txtConnString.Text + "'," +
                        " Pass = '" + txtPass.Text + "' where Plant_Make = '" + cmbPlantMake.Text + "' ";

                    int b = clsFunctions.AdoData_setup(qry);

                    //--------------------- 05/06/2024 : BhaveshT - pType will be changed to 'M' if using Manual module.

                    if (cmbDescription.Text == "RMC-M" || cmbDescription.Text == "BT-M")
                    {
                        int f = clsFunctions.AdoData_setup("Update PlantSetup SET pType = 'M'");
                        //clsFunctions_comman.UniBox("PType changed to = 'M'");
                    }
                    else
                    {
                        int f = clsFunctions.AdoData_setup("Update PlantSetup SET pType = 'A'");
                        //clsFunctions_comman.UniBox("PType changed to = 'A'");
                    }

                    //---------------------------------------------------------------------------------------------------

                    if (a == 1 || b == 1)
                    {
                        //added by dinesh
                        plantCode1 = clsFunctions.loadSingleValueSetup("Select PlantCode from ServerMapping where Flag='Y'");
                        dept1 = clsFunctions.loadSinglevalue_setup("Select deptname from ServerMapping where Flag='Y'");        //for getting data

                        uploadUpload_UniproSetupID(lb_UniSetupID.Text, plantType, dept);
                        clsFunctions_comman.UniBox("Configuration successfull... Please Restart Application to continue...");
                        //this.Close();
                    }

                    string insertUniSetupId = "Update PlantSetup set UniproSetupID = '" + lb_UniSetupID.Text + "'";
                    clsFunctions.AdoData_setup(insertUniSetupId);
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Error while software configuration : " + ex.Message);
                clsFunctions_comman.UniBox("Error while software configuration : " + ex.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------

        private void btnBuildString_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = CreateConnectionString(lblDataType.Text, txtPath.Text, txtPass.Text);
                txtConnString.Text = connectionString;
            }
            catch (Exception ex)
            {

            }
        }

        //---------------------------------------------------------------------------------------------------

        private void QuickConfigure_KeyDown(object sender, KeyEventArgs e)
        {
            //--------------- 03/07/2024 : BhaveshT -  Shortcut key to enable Date Selection Alt + F7 ---------------

            if (e.Alt && e.KeyCode == Keys.F7)
            {
                if (IsFormOpen("Software_Configuration"))
                {
                    MessageBox.Show("The form is already open!");
                }
                else
                {
                    Software_Configuration sc = new Software_Configuration();
                    sc.Show();
                }
            }
        }

        //--------------- 10/07/2024 : BhaveshT -  Shortcut key to open Software_Configuration form Alt + F7 ---------------

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Alt && e.KeyCode == Keys.F7)
            {
                if (IsFormOpen("Software_Configuration"))
                {
                    MessageBox.Show("The form is already open!");
                }
                else
                {
                    Software_Configuration sc = new Software_Configuration();
                    sc.Show();
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        private void txtPath_Leave(object sender, EventArgs e)
        {
            try
            {
                bool fileExists = clsFunctions.CheckIfFileExists(txtPath.Text);

                if (fileExists)
                {
                    //MessageBox.Show("File exist.");
                }
                else
                {
                    MessageBox.Show("Invalid Client Database Path.");
                    
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbDescription.SelectedIndex = -1;
            cmbPlantMake.SelectedIndex = -1;

            lblDataType.Text = "";
            lblPlantType.Text = "";
            txtPath.Text = "";
            txtPass.Text = "";
            lblFormName.Text = "";
            txtConnString.Text = "";
        }

        //---------------------------------------------------------------------------------------------------

        /* 22/07/2024 : BhaveshT
           Created events: txtPath_TextChanged & txtPass_TextChanged and used CreateConnectionString() function into it to
           get conncetion string from path & password directly when it is entered and no need to click on CREATE button.
        */

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblDataType.Text.ToLower() == "ms access" || lblDataType.Text.ToLower() == "acc db")
                {
                    string connectionString = CreateConnectionString(lblDataType.Text, txtPath.Text, txtPass.Text);
                    txtConnString.Text = connectionString;
                }
            }
            catch
            {

            }
        }

        //---------------------------------------------------------------------------------------------------

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblDataType.Text.ToLower() == "ms access" || lblDataType.Text.ToLower() == "acc db")
                {
                    string connectionString = CreateConnectionString(lblDataType.Text, txtPath.Text, txtPass.Text);
                    txtConnString.Text = connectionString;
                }
            }
            catch
            {

            }
        }

        private void txtConnString_TextChanged(object sender, EventArgs e)
        {

        }

        //---------------------------------------------------------------------------------------------------


        //---------------------------------------------------------------------------------------------------


        //---------------------------------------------------------------------------------------------------


        //---------------------------------------------------------------------------------------------------


    }
}
