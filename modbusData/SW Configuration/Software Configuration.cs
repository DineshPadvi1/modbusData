using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.Classes.Model;
using Uniproject.Registration;
using static Uniproject.Registration.RegistrationWizardForm;

namespace Uniproject.UtilityTools
{
    /* 
     * 07/12/2023 - BhaveshT
     * Form: Software Configuration
     * This form will be displayed after Software Registration done using UniRegister_Auto 
     *
     */

    public partial class Software_Configuration : Form
    {
        public Software_Configuration()
        {
            InitializeComponent();
            dgvSetDept.DefaultCellStyle.Font = new Font("Calibri", 10); // Change the size as needed

            dgvSetDept.DefaultCellStyle.ForeColor = Color.Black;
            dgvSetDept.AllowUserToAddRows = false;
            dgvSetDept.RowHeadersVisible = false;
        }

        public static string conName;
        public static string conCode;
        public static string DeviceID;
        public static string plantCode;
        public static string plantType;
        public static string dept;

        //----------------------------------------------------
        private void Software_Configuration_Load(object sender, EventArgs e)
        {
            try
            {
                //--------------- Loading Docket Hrs. -----------------
                try
                {
                    txtDocketHrs.Text = Convert.ToString(clsFunctions.GetDocketHours());
                }
                catch (Exception ex)
                {
                    txtDocketHrs.Text = "24";
                }

                //--------------- Loading Docket Hrs. -----------------
                try
                {
                    txtTipperInterval.Text = clsFunctions.GetTipperInterval();
                }
                catch (Exception ex)
                {
                    txtTipperInterval.Text = "24";
                }

                //--------------------------
                FetchUniproFromDBAndShow();


            }
            catch (Exception ex)
            {

            }
        }

        //----------------------------------------------------

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

            lblWelcome.Text = conName;
            lblData.Text = "Device ID: " + DeviceID + " | Plant Code: " + plantCode + " | Contractor Code: " + conCode + " | Plant Type: " + plantType + " | Department: " + dept;

            //Device ID: 00000000000  | Plant Code:  000 | Contractor Code: 000 | Plant Type: RMC / BT | Department: PMC

            clsFunctions.FillCombo_setup("Select Distinct Plant_Make from Unipro_Setup", cmbPlantMake);
            string strPlantMake = clsFunctions.loadSinglevalue_setup("Select Plant_Make from Unipro_Setup where Status = 'Y'");
            cmbPlantMake.Text = strPlantMake;
            cmbDescription.Items.Insert(0, "-");

            string strTagType = clsFunctions.loadSinglevalue_setup("Select [Description] from Unipro_Setup where Status = 'Y'");
            cmbDescription.Text = strTagType;  //expects either 'OPC' or 'Serial'
        }

        //----------------------------------------------------

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

                if(dt_configData.Rows.Count != 0)
                {
                    DataRow row1 = dt_configData.Rows[0];

                    lb_UniSetupID.Text = row1["UniproSetupID"].ToString();

                    lblDataType.Text = row1["DB_Type"].ToString();
                    lblPlantType.Text = row1["PlantType"].ToString();
                    txtPath.Text = row1["Path"].ToString();
                    txtPass.Text = row1["Pass"].ToString();
                    lblFormName.Text = row1["FormName"].ToString();
                    lblUploaderName.Text = row1["UploaderName"].ToString();
                    txtConnString.Text = row1["ConnectionString"].ToString();

                    //txtConnString.Text = GenerateConnectionString(lblDataType.Text, txtPath.Text, txtPass.Text);
                }
            }
            catch (Exception ex)
            {

            }
        }
        //----------------------------------------------------

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
                    string qry = "Update Unipro_Setup set Status = 'Y' , Path = '" + txtPath.Text + "', ConnectionString='" + txtConnString.Text + "', Pass = '" + txtPass.Text + "' where Plant_Make = '" + cmbPlantMake.Text + "' AND Description = '" + cmbDescription.Text + "'";

                    int b = clsFunctions.AdoData_setup(qry);

                    //--------------------- 05/06/2024 : BhaveshT - pType will be changed to 'M' if using Manual module.

                    if (cmbDescription.Text == "RMC-M" || cmbDescription.Text == "BT-M")
                    {
                        int f = clsFunctions.AdoData_setup("Update PlantSetup SET pType = 'M'");
                        clsFunctions_comman.UniBox("PType changed to = 'M'");
                    }
                    else
                    {
                        int f = clsFunctions.AdoData_setup("Update PlantSetup SET pType = 'A'");
                        clsFunctions_comman.UniBox("PType changed to = 'A'");
                    }

                    //---------------------------------------------------------------------------------------------------

                    if (a == 1 || b == 1)
                    {

                        //added by dinesh
                        plantCode1 = clsFunctions.loadSingleValueSetup("Select PlantCode from ServerMapping where Flag='Y'");
                        dept1 = clsFunctions.loadSinglevalue_setup("Select deptname from ServerMapping where Flag='Y'");//for getting data

                        uploadUpload_UniproSetupID(lb_UniSetupID.Text, plantType, dept);
                        clsFunctions_comman.UniBox("Configuration successfull... Please Restart Application to continue...");
                        //this.Close();
                    }

                    string insertUniSetupId = "Update PlantSetup set UniproSetupID = '" + lb_UniSetupID.Text + "'";

                    clsFunctions.AdoData_setup(insertUniSetupId);


                }
            }
            catch(Exception ex)
            {
                clsFunctions_comman.ErrorLog("Error while software configuration : " + ex.Message);
                clsFunctions_comman.UniBox("Error while software configuration : " + ex.Message);
            }

        }
        //----------------------------------------------------

        private void picPMC_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://pmcscada.in/");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening website: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //----------------------------------------------------

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://vasundharasoftware.com/");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error opening website: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //----------------------------------------------------

        // 08/12/2023 - Created this method to build Connection string from select DBType, DBPath and DBPassword and return it
        private string GenerateConnectionString(string databaseType, string dbPath, string dbPassword)
        {
            switch (databaseType)
            {
                case "MS ACCESS":
                    return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database Password={dbPassword};";

                case "MS SQL":
                    return $"Data Source={dbPath};Initial Catalog=MCI360.mdf;User ID=sa;Password={dbPassword};";

                case "CONMAT":
                    return $"Data Source={dbPath};Initial Catalog=YourDatabaseName;User ID=YourUsername;Password={dbPassword};";

                case "MY SQL":
                    //server = localhost; user id = root; database = ids; password = root; port = 3307;

                    //Server=localhost;user id = root;Database=ids;Uid=root;password=VvSnf4GF!A?Z@;

                    return $"server={dbPath};user id=root;database=ids;password={dbPassword};";
                    //return $"Server={dbPath};Database=ids;Uid=root;Pwd={dbPassword};";   //bkp

                case "POSTGRES":
                    return $"Host={dbPath};Port=5432;Database=ids;User Id=ids;Password={dbPassword};";

                default:
                    return "NA";
            }
        }
        //----------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbDescription.SelectedIndex = -1;
            cmbPlantMake.SelectedIndex = -1;

            lblDataType.Text = "";
            lblPlantType.Text = "";
            txtPath.Text = "";
            txtPass.Text = "";
            lblFormName.Text = "";
            lblUploaderName.Text = "";
            txtConnString.Text = "";
        }
        //----------------------------------------------------
        private void btn_configure_Click(object sender, EventArgs e)
        {
            try
            {
                //if (IsFormOpen("DbInfo"))
                //{
                //    MessageBox.Show("The form is already open!");
                //}
                //else
                //{
                //    DbInfo dbinfo = new DbInfo();
                //    //dbinfo.MdiParent = this;
                //    dbinfo.Show();
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception at DBConfig_Click: " + ex.Message);
                
            }
        }
        //----------------------------------------------------
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
        //----------------------------------------------------
        private void btnTableFieldMapping_Click(object sender, EventArgs e)
        {
            if (IsFormOpen("TableFieldMapping"))
            {
                MessageBox.Show("The form is already open!");
            }
            else
            {
                //TableFieldMapping tblmap = new TableFieldMapping();
                ////tblmap.MdiParent = this;
                //tblmap.Show();
            }
        }
        //----------------------------------------------------

        // This button just created for testing purpose, to view Auto Reg form
        private void btn_AutoRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormOpen("UniRegister_Auto"))
                {
                    MessageBox.Show("The form is already open!");
                }
                else
                {
                    RegistrationWizardForm UniReg = new RegistrationWizardForm();
                    UniReg.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception at UniRegister_Auto: " + ex.Message);

            }
        }
        //----------------------------------------------------
        private void txtConnString_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            //txtConnString.Text = GenerateConnectionString(lblDataType.Text, txtPath.Text, txtPass.Text);
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            //txtConnString.Text = GenerateConnectionString(lblDataType.Text, txtPath.Text, txtPass.Text);
        }
        //----------------------------------------------------

        private void btnSelectDept_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormOpen("SelectAliasForProduction"))
                {
                    MessageBox.Show("The form is already open!");
                }
                else
                {
                    SelectAliasForProduction selectDept = new SelectAliasForProduction();
                    selectDept.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception at SelectAliasForProduction: " + ex.Message);

            }
        }
        string publicAliasName = "";
        //----------------------------------------------------
        /* 08/01/2024 - BhaveshT
         * private void LoadData(DataTable departmentNames)
         * This method gets DataTable as parameter and inserts it in string AliasName, DPTStauts and add it in rows of DGV: dgvSetDept
         */
        private void LoadData(DataTable departmentNames)
        {
            try
            {
                bool selected = false;

                foreach (DataRow row in departmentNames.Rows)
                {
                    string deptName = row["AliasName"].ToString();
                    string chkVal = row["DPTStatus"].ToString();
                    if (chkVal == "")
                        chkVal = "N";
                    string plantCode = row["PlantCode"].ToString();
                    string deviceId = row["DeviceID"].ToString();


                    if (chkVal == "N") selected = false;
                    if (chkVal == "Y") selected = true;

                    dgvSetDept.Rows.Add(deptName, selected, plantCode, deviceId);
                }
            }
            catch(Exception ex)
            {

            }
        }

        //----------------------------------------------------

        private void btnSetDept_Click(object sender, EventArgs e)
        {
            if (pnlSetDept.Visible == false)
                pnlSetDept.Visible = true;
            else if (pnlSetDept.Visible == true)
                pnlSetDept.Visible = false;

            dgvSetDept.Rows.Clear();
            //pnlSetDept.Visible = true;
            //InitializeDgvSetDept();

            DataTable DeptDT = GetDepartmentNamesFromDatabase();
            LoadData(DeptDT);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        string department = "";
        string plantTypeFromTuple = "";

        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        private void btnUpdateSetDept_Click(object sender, EventArgs e)
        {
            //List<string> selectedDepartments = GetSelectedDepartments(); //commented by dinesh
            List<string> selectedDepartments = GetSelectedDepartments();
            List<(string DeptName, string PlantCode, string DeviceId)> selectedDepartments1 =null;

            if (selectedDepartments.Count == 0)
            {
                clsFunctions_comman.UniBox("Please select atleast one department");
                return;
            }

            UniRegister_Auto getServerMapping_PresetData = new UniRegister_Auto();
            string PlantTypeRecievedFromApi = "";
            /****************** By Dinesh **********************/
            foreach (string dept in selectedDepartments)
            {
                if (!clsFunctions.isAliasExists(dept))
                {
                    if (dept.Contains("RMC"))
                        PlantTypeRecievedFromApi = "RMC";
                    else
                        PlantTypeRecievedFromApi = "BT";
                    string domain = clsFunctions.loadSingleValueSetup("Select domain1 from AliasName where AliasName='" + dept.Trim() + "'");

                    List<(string Department, string PlantType)> parsedList = clsFunctions.ParseAliasNames(dept);
                    foreach (var tuple in parsedList)
                    {
                        department = tuple.Department;
                        plantTypeFromTuple = tuple.PlantType;
                    }

                    //getServerMapping_PresetData.getPresetDataForApis(department, PlantTypeRecievedFromApi, domain);
                    clsFunctions.GetAndInsertServerMappingDataFromAPI(dept, department, PlantTypeRecievedFromApi);

                    selectedDepartments1 = GetSelectedDeptPlntDeviceid();

                    foreach (var departments in selectedDepartments1)
                    {                        
                        if (departments.DeptName.Contains(dept))
                        {
                            //string message = $"DeptName: {department.DeptName}, PlantCode: {department.PlantCode}, DeviceId: {department.DeviceId}";
                            //MessageBox.Show(message, "Selected Department", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clsFunctions.AdoData_setup("update ServerMapping_Preset set DeviceID='" + departments.DeviceId + "', PlantCode='" + departments.PlantCode + "' where AliasName='" + departments.DeptName + "'"); // update plant and deviceid
                            GetPlantDataForMobileNo(departments.DeviceId, departments.DeptName);

                        }
                    }
                }
            }
            
            //------------------------------------

            InsertDeptDataInServerMapping(selectedDepartments);
            UpdateDPTStatus(selectedDepartments);
            pnlSetDept.Visible = false;
            clsFunctions_comman.UniBox("Departments updated successfully.");

        }

        private void getHeaderData()
        {
            //GetParameterDataForTableSync();
            //UniRegister_Auto uniRegister_Auto = new UniRegister_Auto();

            //uniRegister_Auto.getDataHeaderTableSync(cmbAuto_Dept, department, pCode);
            //uniRegister_Auto.getDataTransactionTableSync(cmbAuto_Dept, department, pCode);
        }

        //----------------------------------------------------
        /* 08/01/2024 - BhaveshT
         * private DataTable GetDepartmentNamesFromDatabase()
         * This method fetches AliasName & DPTStatus from ServerMapping_Preset where AliasName is like PlantType and stores it into DataTable.
         */
        private DataTable GetDepartmentNamesFromDatabase()
        {
            DataTable dt = new DataTable();

            string pType = plantType;
            string a = "";
            if (pType.Contains("RMC"))
            {
                a = "RMC";
            }
            else if (pType.Contains("Bitu") || pType.Contains("BT"))
            {
                a = "BT";
            }
            //dt = clsFunctions.fillDatatable_setup("SELECT AliasName, DPTStatus, DeviceID, PlantCode FROM ServerMapping_Preset WHERE AliasName LIKE '%" + a + "%';"); // commetned by dinesh
            dt = clsFunctions.fillDatatable_setup("SELECT a.AliasName, s.DPTStatus, s.DeviceID, s.PlantCode FROM AliasName AS a LEFT JOIN ServerMapping_Preset AS s ON a.AliasName = s.AliasName WHERE a.AliasName LIKE '%" + a + "%'");


            //dt = clsFunctions.fillDatatable_setup("SELECT AliasName, DPTStatus, DeviceID, PlantCode FROM ServerMapping_Preset WHERE AliasName LIKE '%" + a + "%';");

            //dt = clsFunctions.fillDatatable_setup("SELECT AliasName, DPTStatus FROM ServerMapping WHERE AliasName LIKE '%" + a + "%';");

            return dt;
        }
        //----------------------------------------------------
        /* 08/01/2024 - BhaveshT
         * private void UpdateDPTStatus(List<string> selectedDeptNames):
         * gets List of selected deptName. Updates DPTStatus = 'N' for all, then, Updates DPTStatus = 'Y' for selected deptName (AliasNames)
         */
        private void UpdateDPTStatus(List<string> selectedDeptNames)
        {

            clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET DPTStatus = 'N'");
            foreach (string deptName in selectedDeptNames)
            {
                string updateQuery = "UPDATE ServerMapping_Preset SET DPTStatus = 'Y' WHERE AliasName = '"+ deptName + "' ";

                int a = clsFunctions.AdoData_setup(updateQuery);
                
            }
        }
        //----------------------------------------------------


        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        /* 23/01/2024 - BhaveshT
         * private void InsertDeptDataInServerMapping(List<string> selectedDeptNames):
         * gets List of selected deptName. Updates DPTStatus = 'N' for all, then, Updates DPTStatus = 'Y' for selected deptName (AliasNames)
         */
        private void InsertDeptDataInServerMapping(List<string> selectedDepartments)
        {
            //--------------------------
            try
            {
            DataTable dt = new DataTable();
            string predept = "";
            clsFunctions.AdoData_setup("Delete * from ServerMapping");

            // 06/01/2021 - BhaveshT : InsertQuery to Insert data from ServerMapping_Preset into ServerMapping for selected AliasName
            dt = clsFunctions.fillDatatable_setup("Select * FROM ServerMapping_Preset WHERE AliasName IN ('" + string.Join("', '", selectedDepartments) + "')");

                //if (dt.Rows.Count != 0)

                foreach (DataRow row in dt.Rows)
                {
                    //foreach (DataRow row in dt.Rows)
                    foreach (DataGridViewRow r in dgvSetDept.Rows)
                    {
                        //------------
                        //foreach (DataGridViewRow r in dgvSetDept.Rows)
                        
                        string deviceId = "";
                        string plantcode = "";
                        

                        deviceId = (r.Cells["cell_deviceId"].Value) == null ? "" : (r.Cells["cell_deviceId"].Value).ToString();
                        plantcode = (r.Cells["cell_plantCode"].Value) == null ? "" : (r.Cells["cell_plantCode"].Value).ToString();

                        if (Convert.ToBoolean(r.Cells["cell_chk"].Value))
                        {
                            DataRow[] filteredRows = dt.Select("AliasName = '" + row["AliasName"].ToString() + "'");

                            if (filteredRows.Length > 0)
                            {
                                if((r.Cells["DeptName"].Value).ToString() == row["AliasName"].ToString()) //if ((row["AliasName"].ToString()) == row["AliasName"].ToString() )// && dptname != predept)     //if ( dptname == row["AliasName"].ToString())
                                {
                                    (dt.Select("AliasName = '" + row["AliasName"].ToString() + "'"))[0]["DeviceID"] = (object)deviceId;
                                    (dt.Select("AliasName = '" + row["AliasName"].ToString() + "'"))[0]["PlantCode"] = (object)plantcode;

                                }
                            }
                        }
                    }

                    
                        // Construct the insert query for ServerMapping table using data from the selected row
                        string insertQuery = $@"INSERT INTO ServerMapping (SrNo, AliasName, Note1, ipaddress, portno, BT_API, RMC_Trans_API, RMC_Transaction_API, Software_Status_API, plantExpiry, deptname, PlantType, Note2, ipaddress1, port1, BT_API1, RMC_Transaction_API1, RMC_Trans_API1, AutoReg_SMS, AutoReg_Verify, AutoReg_Save, GetWO, GetAllWO, AllocateWO, GetPlantDetails, GetMobNoFromWO, GetProdErrorTemplate, sendSMS, getInstallDetails, DPTStatus, Flag, DeviceID, PlantCode, GetDataHeaderTableSync,UploadDataHeaderTableSync, GetDataTransactionTableSync,UploadDataTransactionTableSync, ServerMapping_Preset, Unipro_Setup, PlantSetup, Plant_LiveStatus_History, m_SaveInstall, m_latestUp_Insert, m_latestUp_Get,PlantExpiryDate,Upload_UniproSetupID) VALUES ({row["SrNo"]}, '{row["AliasName"]}', '{row["Note1"]}', '{row["ipaddress"]}', '{row["portno"]}', '{row["BT_API"]}', '{row["RMC_Trans_API"]}', '{row["RMC_Transaction_API"]}', '{row["Software_Status_API"]}', '{row["plantExpiry"]}', '{row["deptname"]}', '{row["PlantType"]}', '{row["Note2"]}', '{row["ipaddress1"]}', '{row["port1"]}', '{row["BT_API1"]}', '{row["RMC_Transaction_API1"]}', '{row["RMC_Trans_API1"]}', '{row["AutoReg_SMS"]}', '{row["AutoReg_Verify"]}', '{row["AutoReg_Save"]}', '{row["GetWO"]}', '{row["GetAllWO"]}', '{row["AllocateWO"]}', '{row["GetPlantDetails"]}', '{row["GetMobNoFromWO"]}', '{row["GetProdErrorTemplate"]}', '{row["sendSMS"]}', '{row["getInstallDetails"]}', '{row["DPTStatus"]}', '{row["Flag"]}', '{row["DeviceID"]}', '{row["PlantCode"]}', '{row["GetDataHeaderTableSync"]}', '{row["UploadDataHeaderTableSync"]}', '{row["GetDataTransactionTableSync"]}', '{row["UploadDataTransactionTableSync"]}', '{row["ServerMapping_Preset"]}', '{row["Unipro_Setup"]}', '{row["PlantSetup"]}', '{row["Plant_LiveStatus_History"]}', '{row["m_SaveInstall"]}', '{row["m_latestUp_Insert"]}', '{row["m_latestUp_Get"]}', '{row["PlantExpiryDate"]}', '{row["Upload_UniproSetupID"]}')";


                        //string insertQuery = "INSERT INTO ServerMapping (SrNo, AliasName, Note1, ipaddress, portno, BT_API, RMC_Trans_API, RMC_Transaction_API, Software_Status_API, plantExpiry," +
                        //          " deptname, PlantType, Note2, ipaddress1, port1, BT_API1, RMC_Transaction_API1, RMC_Trans_API1, AutoReg_SMS, AutoReg_Verify, AutoReg_Save, GetWO, GetAllWO, AllocateWO, getPlantDetails, GetMobNoFromWO, GetProdErrorTemplate, sendSMS, DPTStatus, Flag, DeviceID, PlantCode) " +
                        //             "VALUES ('" + row["SrNo"].ToString() + "', '" + row["AliasName"].ToString() + "', '" + row["Note1"].ToString() + "', '" + row["ipaddress"].ToString() + "'," +
                        //             " '" + row["Portno"].ToString() + "', '" + row["BT_API"].ToString() + "', '" + row["RMC_Trans_API"].ToString() + "', '" + row["RMC_Transaction_API"].ToString() + "', " +
                        //             " '" + row["Software_Status_API"].ToString() + "', '" + row["plantExpiry"].ToString() + "', '" + row["deptname"].ToString() + "', '" + row["PlantType"].ToString() + "', " +
                        //             " '" + row["Note2"].ToString() + "', '" + row["ipaddress1"].ToString() + "', '" + row["port1"].ToString() + "', '" + row["BT_API1"].ToString() + "', " +
                        //             " '" + row["RMC_Transaction_API1"].ToString() + "', '" + row["RMC_Trans_API1"].ToString() + "', '" + row["AutoReg_SMS"].ToString() + "', " +
                        //             " '" + row["AutoReg_Verify"].ToString() + "', '" + row["AutoReg_Save"].ToString() + "', '" + row["GetWO"].ToString() + "', '" + row["GetAllWO"].ToString() + "', " +
                        //             " '" + row["AllocateWO"].ToString() + "', '" + row["getPlantDetails"].ToString() + "','" + row["GetMobNoFromWO"].ToString() + "','" + row["GetProdErrorTemplate"].ToString() + "'," +
                        //             " '" + row["sendSMS"].ToString() + "', 'N', 'N', '" + row["DeviceID"].ToString() + "', '" + row["PlantCode"].ToString() + "')";

                        int a = clsFunctions.AdoData_setup(insertQuery);

                          int a1 = clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET PlantCode = '" + row["PlantCode"].ToString() + "' WHERE AliasName = '" + row["AliasName"].ToString() + "' ");
                          int a2 = clsFunctions.AdoData_setup("UPDATE ServerMapping_Preset SET DeviceID  = '" + row["DeviceID"].ToString() + "' WHERE AliasName = '" + row["AliasName"].ToString() + "' ");
                       
                }
            }
            catch (Exception ex)
            {

            }
            //--------------
        }


        //-----------------------------------------------------------------------------------------------


        /* 08/01/2024 - BhaveshT
         * private List<string> GetSelectedDepartments() - This method creates List of selected deptName.
         */
        private List<string> GetSelectedDepartments()
        {
            List<string> selectedDepartments = new List<string>();

            foreach (DataGridViewRow row in dgvSetDept.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["cell_chk"].Value);

                if (isChecked)
                {
                    if(row.Cells["cell_plantCode"].Value.ToString() != "" || row.Cells["cell_deviceId"].Value.ToString() != "")
                    {
                        string deptName = row.Cells["DeptName"].Value.ToString();
                        selectedDepartments.Add(deptName);
                    }
                    else
                    {
                        clsFunctions_comman.UniBox("Please enter Plant Code or DeviceID");
                        //return selectedDepartments; commneted by dinesh
                    }
                    
                }
            }

            return selectedDepartments;
        }

        //-----------------------------------------------------------------------------------------------

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

        //----------------------------------------------------

        private void btnSetDocketHrs_Click(object sender, EventArgs e)
        {
            int a = 0;
            try
            {
                a = clsFunctions.AdoData_setup("Update tipper_interval set DocketHours = "+ txtDocketHrs.Text +" ");
                if(a > 0)
                {
                    clsFunctions_comman.UniBox("Docket Hours set to "+txtDocketHrs.Text+" Hours. ");
                }
                else
                {
                    clsFunctions_comman.UniBox("Failed to set Docket Hours.");
                }
                txtDocketHrs.Text = Convert.ToString(clsFunctions.GetDocketHours());
            }
            catch (Exception ex)
            {
                txtDocketHrs.Text = "24";
            }
        }

        //-----------------------------------------------------------------------------------------------

        public List<(string DeptName, string PlantCode, string DeviceId)> GetSelectedDeptPlntDeviceid()
        {
            List<(string DeptName, string PlantCode, string DeviceId)> selectedDepartments = new List<(string DeptName, string PlantCode, string DeviceId)>();

            foreach (DataGridViewRow row in dgvSetDept.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["cell_chk"].Value);

                if (isChecked)
                {
                    string plantCode = Convert.ToString(row.Cells["cell_plantCode"].Value);
                    string deviceId = Convert.ToString(row.Cells["cell_deviceId"].Value);

                    if (!string.IsNullOrWhiteSpace(plantCode) || !string.IsNullOrWhiteSpace(deviceId))
                    {
                        string deptName = Convert.ToString(row.Cells["DeptName"].Value);
                        selectedDepartments.Add((deptName, plantCode, deviceId));
                    }
                    else
                    {
                        MessageBox.Show("Please enter Plant Code or DeviceID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //return selectedDepartments;
                    }
                }
            }

            return selectedDepartments;
        }

        //-----------------------------------------------------------------------------------------------

        public async void GetPlantDataForMobileNo(string SelectedPlantDeviceID, string cmbAuto_Dept)
        {
            //string apiUrl = $"http://192.168.1.13:8080/gtambGetWorkIdAPI/verifyRegistration?regMobNo={mobileNo}";

            string verifyApiName = clsFunctions.GetAPINameFromPreset("AutoReg_Verify", cmbAuto_Dept);

            string domain = "";
            if (cmbAuto_Dept == "PWD - BT")
                domain = clsFunctions.GetAPINameFromPreset("ipaddress1", cmbAuto_Dept);
            else
                domain = clsFunctions.GetAPINameFromPreset("ipaddress", cmbAuto_Dept);

            //string apiUrl = "" + clsFunctions.protocol + "://" + clsFunctions.regURL + verifyApiName + mobileNo;
            string apiUrl = domain + verifyApiName + SelectedPlantDeviceID;

            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetStringAsync(apiUrl);
                    response.Wait();

                    //if (response.IsSuccessStatusCode) // remove this statement or change the logic
                    {
                        string jsonResponse = response.Result;   //string jsonResponse = await response.Content.ReadAsStringAsync();
                        ParseJsonResponse(jsonResponse, cmbAuto_Dept);
                        //btnAuto_Confirm.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    clsFunctions_comman.UniBox($"Exception at verifyRegistration: {ex.Message}");
                    clsFunctions_comman.ErrorLog($"Exception at verifyRegistration: {ex.Message}");
                }
            }
        }

        //-----------------------------------------------------------------------------------------------

        public void ParseJsonResponse(string jsonResponse,string cmbAuto_Dept)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<PlantDetailsResponse>(jsonResponse);

                if (result != null && result.PlantDetails.Count > 0)
                {
                    var plantDetails = result.PlantDetails[0];                   
                    string validTo = plantDetails.valid_to;

                    clsFunctions.AdoData_setup("update ServerMapping_Preset set PlantExpiryDate =#"+validTo+"# where AliasName='" + cmbAuto_Dept + "'"); // update plant 

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

        //-----------------------------------------------------------------------------------------------

        mdiMain mdiMain = new mdiMain();
        string plantCode1 = clsFunctions.loadSingleValueSetup("Select PlantCode from ServerMapping where Flag='Y'");
        string dept1 = clsFunctions.loadSinglevalue_setup("Select deptname from ServerMapping where Flag='Y'");

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

        //-----------------------------------------------------------------------------------------------

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

        //-----------------------------------------------------------------------------------------------

        public async void uploadUpload_UniproSetupID(string UniproSetupID,string plantType, string departmenttype)
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
                if(responseContent !=null)
                {
                    if(responseContent.Contains("UniPro_Setup_Id Update Successfully"))
                    {
                        clsFunctions.ErrorLog("UniPro_Setup_Id updated successfully : " + UniproSetupID);
                        
                    }
                    if(responseContent.Contains("Plant Details Not Present Against Plant_Code"))
                    {
                        clsFunctions.ErrorLog("Response from the server: " + responseContent);
                        MessageBox.Show("Response from the server: " + responseContent, "Server Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if(responseContent.Contains("UniPro_Setup_Id Not Update"))
                    {
                        clsFunctions.ErrorLog("Response from the server: " + responseContent);
                        MessageBox.Show("Response from the server: " + responseContent, "Server Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //-----------------------------------------------------------------------------------------------

        private void Software_Configuration_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string plantcode = clsFunctions.loadSinglevalue_setup("Select PlantCode from ServerMapping where Flag='Y'");
            //string department = clsFunctions.loadSinglevalue_setup("Select deptname from ServerMapping where Flag='Y'");
            //clsFunctions.AdoData_setup("Update DataHeaderTableSync set Flag='Y' where plant_code='"+ plantcode + "' and dept_name='"+department+ "' and Type='Client'");
            //clsFunctions.AdoData_setup("Update DataHeaderTableSync set Flag='Y' where Type='VIPL' and info='Fields' ");
            //
            //clsFunctions.AdoData_setup("Update DataTransactionTableSync set Flag='Y' where plant_code='" + plantcode + "' and dept_name='" + department + "' and Type='Client'");
            //clsFunctions.AdoData_setup("Update DataTransactionTableSync set Flag='Y' where  Type='VIPL' and info='Fields' ");

        }

        //-----------------------------------------------------------------------------------------------

        private void btnFetchUniSetup_Click(object sender, EventArgs e)
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

        //-----------------------------------------------------------------------------------------------
        private void btnSetTipperInterval_Click(object sender, EventArgs e)
        {
            int a = 0;
            try
            {
                a = clsFunctions.AdoData_setup("Update tipper_interval set truckinterval = " + txtTipperInterval.Text + " ");
                if (a > 0)
                {
                    clsFunctions_comman.UniBox("Tipper Interval set to " + txtTipperInterval.Text + " minutes. ");
                }
                else
                {
                    clsFunctions_comman.UniBox("Failed to set Tipper Interval.");
                }
                txtTipperInterval.Text = clsFunctions.GetTipperInterval();
            }
            catch (Exception ex)
            {
                txtTipperInterval.Text = "59";
            }
        }

        //-----------------------------------------------------------------------------------------------

        private void btnFetchSerMapPreset_Click(object sender, EventArgs e)
        {
            try
            {
                if (NetworkHelper.IsInternetAvailable())
                {
                    clsFunctions.GetAndUpdateServerMappingDataFromAPI(clsFunctions.aliasName, clsFunctions.activeDeptName);
                }
                else
                {
                    MessageBox.Show("Check internet connection.");
                    return;
                }
            }
            catch
            {

            }
        }

        private void lblData_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenMappingManager_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbDescription_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
    }
}
