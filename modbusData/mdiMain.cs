using PDF_File_Reader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.Masters;
using Uniproject.RMC_forms.Masters;
using Uniproject.SW_Configuration;

namespace Uniproject
{
    public partial class mdiMain : Form
    {
        public static string DBType = "";
        public static string con = "";

        public LoggerService loggerService = new LoggerService();      //BT

        public static string plantType = "";

        //private const string ServiceName = "VasundharaUploaderService";

        private const string ServiceName = "VasundharaUploaderClient";
        // static private ServiceController serviceController;


        private const string PWDServiceName = "VasundharaUploaderClientPWD";
        // static private ServiceController PWDserviceController;

        //--------------------------------------------------------------------------------------------------------------------------------------------

        public mdiMain()
        {
            InitializeComponent();

            //serviceController = new ServiceController(ServiceName);
            //PWDserviceController = new ServiceController(PWDServiceName);

            this.KeyPreview = true;

            clsFunctions_comman.loadForm = clsFunctions_comman.loadFormNameFromUniproSetup();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //This Function is Optimized for checking whether same form is open or not which is already open. -- By Dinesh
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

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //checking whether internet connection is active or not 
        private bool CheckInternetConnection()
        {
            try
            {
                bool a = NetworkInterface.GetIsNetworkAvailable();
                {
                    // check expiry only if connected to interet.
                    if (a == true)
                    {
                        if (clsFunctions.isExpiryCheckedForPType == false)
                        {
                            //if (clsFunctions.planttype != "Bituman - Drum Mix" && clsFunctions.planttype != "Bitumen - Drum Mix")
                            //GetRenewalDt_CheckPType();
                           
                            //else
                            //clsFunctions.isExpiryCheckedForPType = true;
                        }
                    }
                }
                return a;
            }
            catch
            {
                return false;
            }
        }
        public string myIcon = "";

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //public void Servicestatus()
        //{
        //    string serviceName = "VasundharaUploaderClient";

        //    ServiceController sc = new ServiceController(serviceName);

        //    try
        //    {
        //        ServiceControllerStatus status = sc.Status;

        //        if (status == ServiceControllerStatus.Running)
        //        {
        //            lblServiceStatus.Image = global::Uniproject.Properties.Resources.on;
        //            lblServiceStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
        //        }
        //        else if (status == ServiceControllerStatus.Stopped)
        //        {
        //            lblServiceStatus.Image = global::Uniproject.Properties.Resources.off;
        //            lblServiceStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
        //        }
        //        else
        //        {
        //            this.lblServiceStatus.Text = "The service is in a different state: " + status.ToString();
        //            //Console.WriteLine("The service is in a different state: " + status.ToString());
        //        }
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        this.lblServiceStatus.Text = "Service not found ";
        //        lblServiceStatus.BackColor = Color.LightPink;

        //        //clsFunctions.ErrorLog("Error service: " + ex.Message);
        //    }
        //}

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //public void ServiceStatus_PWD()
        //{
        //    lblServiceStatusPWD.Visible = true;

        //    string serviceName = "VasundharaUploaderClientPWD";

        //    ServiceController sc = new ServiceController(serviceName);

        //    try
        //    {
        //        ServiceControllerStatus status = sc.Status;

        //        if (status == ServiceControllerStatus.Running)
        //        {
        //            lblServiceStatusPWD.Image = global::Uniproject.Properties.Resources.on;
        //            lblServiceStatusPWD.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
        //        }
        //        else if (status == ServiceControllerStatus.Stopped)
        //        {
        //            lblServiceStatusPWD.Image = global::Uniproject.Properties.Resources.off;
        //            lblServiceStatusPWD.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
        //        }
        //        else
        //        {
        //            this.lblServiceStatusPWD.Text = "ServiceStatus_PWD: The service is in a different state: " + status.ToString();
        //        }
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        this.lblServiceStatusPWD.Text = "ServiceStatus_PWD: Service not found ";
        //        lblServiceStatusPWD.BackColor = Color.LightPink;
        //    }
        //}

        //--------------------------------------------------------------------------------------------------------------------------------------------

        /* Created date: 29/08/2023
         * this function checks prerequisites for the unipro 
         */

        //private string ImPrerequisite()
        //{
        //    string TableName = "";
        //    try
        //    {
        //        TableName = clsFunctions.Preprocessing();
        //        return TableName;
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return "0";
        //}

        string conName, conCode, DeviceID, dept, macId;

       
        //---------------------------------------------------- mdiMain Form Load event --------------------------------------------------

        private void mdiMain_Load(object sender, EventArgs e)
        {
           

            //if (CheckInternetConnection())
            //    clsFunctions_comman.pingResponse = clsFunctions.CheckAPIStatus("https://pmcscada.in/");

            //------------------------------------------------------------------------------------------------

            clsFunctions_comman.softwareStatus = "ON";

            // main form loaded log
            try
            {
                //clsFunctions.OpenAliasSelector();
                //clsFunctions.SetSelectedAliasToVariables();

                //clsPatch.CreateNewColumns();
                fireWallChecker();
                // This thread is for checking service is stopped or not if stopped then it will start the service.
                // thread is created by Dinesh on 05/02/2024

               

                //Date: 20/12/2023 added by Dinesh 
                //clsFunctions.SetAPIs();
                SetButtonsVisibility();
                ShowPlantDetails();
                //SetDeptLogo();
                

                // 08/02/2024 - BhaveshT : Use Unipro_Setup table instead of Connection_setup ------------------------

                DBType = clsFunctions.loadSingleValueSetup("SELECT DB_Type FROM Unipro_Setup where Status = 'Y' ");

                clsFunctions_comman.ErrorLog("mdiMain form has been loaded.");

            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("(catch)mdiMain form has been loaded - " + ex.Message + "");
            }

            //----------------------------------------------------------------------------------------------------

            timer1.Enabled = true;
            string strTempPath;
            //  strTempPath = clsFunctions.GetConnectionstrSetup_Path();

            // unsubscribing the Event of toolStripMenuItem1 here
            //PlantInfo.Click -= new EventHandler(toolStripMenuItem1_Click);


            //added code for checking whether Plant Type is BT or RMC     ---By Dinesh
            plantType = Uniproject.Classes.clsFunctions.loadSingleValueSetup("select PlantType from PlantSetup");
            // clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();

            string renewaldt = clsFunctions.loadSingleValueSetup("select PlantExpiry from PlantSetup");       // 13/04/2024 - BhaveshT

            DateTime date = DateTime.Parse(renewaldt);
            string renewalDt = date.ToString("dd/MM/yyyy");
            //lblDateRenewal.Text = renewalDt;

            // clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();


            // 25/07/2024 : BhaveshT - This thread will start uploader service for PWD_RMC -------------------

            if (clsFunctions.aliasName == "PWD - RMC")
            {
                try
                {
                   
                }
                catch (Exception ex)
                {
                    clsFunctions.ErrorLog("at starting thread for checking PWD_RMC service status for every 30 min once.");
                }
            }

            //----------------------------------

            // clsFunctions_comman.InsertSoftwareStatus(clsFunctions.activeDeviceID, clsFunctions.activePlantCode, clsFunctions.activeDeptName, "ON");

            lb_workDept.Text = "Work Dept: " + clsFunctions.aliasName;
            //lb_workDept.Text = "Work Dept: SCADAINDIA - RMC";

            try
            {
                // clsFunctions.LatestUpdates_Dt = clsFunctions.GetUpdate_FromServer();
                DisplayUpdatesInBox(clsFunctions.LatestUpdates_Dt);
            }
            catch { }

            AutoSyncWO();

            //clsFunctions.StoreExeVersions();

            //clsFunctions.CheckIfPathCorrect();      //BhaveshT
            //clsFunctions.CheckClientDBSet();        //BhaveshT

            //if(clsFunctions.CheckNameSetup() == 0)
            //{
            //    if (IsFormOpen("SetParameter"))
            //    {
            //        MessageBox.Show("The form is already open!");
            //    }
            //    else
            //    {
            //        SetParameter tblmap = new SetParameter();
            //        tblmap.MdiParent = this;
            //        tblmap.Show();
            //    }
            //}
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        // 03/01/2025 : Added By BhaveshT - Set Dept. wise logo ----------------------------------------------------------------------



        //--------------------------------------------------------------------------------------------------------------------------------------------

        // 17/04/2024 - BhaveshT : This method gets DataTable and fill needed into in Latest updates richText box. --------------------

        public void DisplayUpdatesInBox(DataTable dt)
        {
            try
            {
                if (dt == null)
                {
                    updateBox.Text = "";
                }
                else
                {
                    DataRow row = dt.Rows[0];

                    updateBox.Text = "Unipro: v" + row["update_unipro_version"].ToString();

                    lblHeading.Text = row["message"].ToString();

                    updateBox.Visible = false;
                }
            }
            catch
            { }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //------------ 20/12/2023 - BhaveshT :  To display plant details on mdiMain form --------------------
        public void ShowPlantDetails()
        {

            DataTable dt_plantSetup = clsFunctions.fillDatatable_setup("Select * from PlantSetup");

            DataRow row = dt_plantSetup.Rows[0];

            conName = row["ContractorName"].ToString();
            conCode = row["ContractorCode"].ToString();
            DeviceID = row["DeviceID"].ToString();
            plantCode = row["PlantCode"].ToString();
            plantType = row["PlantType"].ToString();
            dept = row["DeptName"].ToString();
            macId = row["MacID"].ToString();
            //if (dept != "PMC")
            //{
            //    return;
            //}

            this.Text = "Device ID: " + DeviceID + " | Contractor Name: " + conName + " | Plant Code: " + plantCode + " | Contractor Code: " + conCode + " | Plant Type: " + plantType + " | Dept: " + dept;
            //this.Text = "Device ID: " + DeviceID + " | Contractor Name: " + conName + " | Plant Code: " + plantCode + " | Contractor Code: " + conCode + " | Plant Type: " + plantType + " | Dept: SCADAINDIA";

            lb_macId.Text = "MacID: " + macId;

            // uploadInstruction.Text = "Please upload data before " + clsFunctions.GetDocketHours() + " hrs. ";

            //-------------------------------

            //Device ID: 00000000000  | Plant Code:  000 | Contractor Code: 000 | Plant Type: RMC / BT | Department: PMC

            ////-----------------------
            //try
            //{
            //    lbInstallationDate.Text = clsFunctions.loadSingleValueSetup("Select InstallationDate from PlantSetup");
            //    lbInstallationDate.Text = Convert.ToDateTime(lbInstallationDate.Text).ToString("dd-MM-yyyy");
            //
            //    InstallDateLabel.Text = InstallDateLabel.Text + " " + lbInstallationDate.Text;
            //
            //    if (lbInstallationDate.Text == "" || lbInstallationDate.Text == null)
            //    {
            //        lbInstallationDate.Visible = false;
            //        InstallDateLabel.Visible = false;
            //    }
            //}
            //catch
            //{
            //    lbInstallationDate.Visible = false;
            //    InstallDateLabel.Visible = false;
            //}

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        // 21/12/2023 - BhaveshT : Set visibility of specific menu buttons which will get visible only if specific form is used. ---------------------
        private void SetButtonsVisibility()
        {
            loadForm = clsFunctions_comman.loadFormNameFromUniproSetup();

            //loadForm = clsFunctions_comman.loadFormNameFromUniproSetup();

            if (loadForm == "PlantScadaV5")
            {
                //btnPLCsetting.Visible = true;
                //btn_ScadaPortSetting.Visible = true;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //30/05/2024 : BhaveshT - To Auto Sync WorkOrders on software start.
        public void AutoSyncWO()
        {
            try
            {
                bool a = NetworkInterface.GetIsNetworkAvailable();
                if (a == false)
                {
                    clsFunctions_comman.ErrorLog("Please connect to the Internet to Auto Import Work Orders.");
                    return;
                }
                else
                {
                    //clsFunctions.activePlantCode = clsFunctions.GetActivePlantCodeFromServerMapping();
                    //clsWOAutoSync.SyncWODetails(clsFunctions.activePlantCode);
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at mdiMain AutoSyncWO(): while importing WO - " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        public string plantCode = clsFunctions.loadSingleValueSetup("select plantcode from PlantSetup");      // getting plantCode from database
                                                                                                              //public string IP = clsFunctions.loadSingleValueSetup("select ipaddress from ServerMapping where Flag='Y'");
                                                                                                              //public string Port = "8080";

        /* Modified Date:21/12/2023 by dinesh
         * Modified Code -> made this function able to fetch api link from the database
         */


        public class ResponseData
        {
            //  public List<getRegData> PlantDetailsList { get; set; }
        }

        //--------------------------------------------------------- Not in use -----------------------------------------------------------------
        /*
         Unipro v2 - mdiMain
    
         19/02/2024 - BhaveshT (As told by Vinay sir)
         This method will use getPlantDetails API to get plantDetails against parameters: DeviceID & p_type (A/M).
         - after getting validity: if plant is expired & p_type is 'A', software will remain usable.
         - after getting validity: if plant is expired & p_type is 'M', software will get closed.
         
         */



        //----------------------------------------------------------------------------------------------------

        //private void getDataUploadStatus()      // Uploaded
        //{
        //    try
        //    {
        //        // 10/01/2024 - BhaveshT : This will fetch and fill data from Batch_Transaction table for RMC

        //        if (plantType.Contains("RMC"))
        //        {
        //            //DataTable dt = clsFunctions.fillDatatable("select WO_Code, tUpload1 from Batch_Dat_Trans where tUpload1=1 order by Batch_No");
        //            DataTable dt = clsFunctions.fillDatatable("select WO_Code, Batch_No from Batch_Dat_Trans where tUpload1=1 order by Batch_No");

        //            dgv_history.Rows.Clear();
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                //dgv_history.Rows.Add("", row["WO_Code"], row["tUpload1"]);
        //                dgv_history.Rows.Add("", row["Batch_No"], row["WO_Code"]);
        //            }

        //            errorBatchCount.Text = clsFunctions_comman.loadSinglevalue("SELECT COUNT(*) AS BatchCount FROM Batch_Dat_Trans WHERE tUpload1 NOT IN (1, 0)");
        //            countUploaded.Text = (dgv_history.RowCount - 1).ToString();
        //        }

        //        //----------------------------------------------------------------------------------------------------

        //        // 10/01/2024 - BhaveshT : This will fetch and fill data from Batch_Transaction table for Bitumen

        //        else if (plantType.Contains("Bit") || plantType.Contains("BT"))
        //        {
        //            dgv_history.Columns["workcode"].Width = 80;
        //            //DataTable dt = clsFunctions.fillDatatable("select workcode, viplupload from tblHotmixPlant where viplupload=1 order by ID");
        //            DataTable dt = clsFunctions.fillDatatable("select workcode, tipper from tblHotmixPlant where viplupload=1 order by ID");

        //            dgv_history.Rows.Clear();
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                //dgv_history.Rows.Add("", row["workcode"], row["viplupload"]);
        //                dgv_history.Rows.Add("", row["tipper"], row["workcode"]);
        //            }

        //            errorBatchCount.Text = clsFunctions_comman.loadSinglevalue("SELECT COUNT(*) AS BatchCount FROM tblHotMixPlant WHERE viplupload NOT IN (1, 0)");

        //            countUploaded.Text = (dgv_history.RowCount - 1).ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        clsFunctions_comman.ErrorLog("mdiMain - getDataUploadStatus() : " + ex.Message);     //BhaveshT
        //    }
        //}

        //----------------------------------------------------------------------------------------------------

        //private void getDataPendingStatus()     // pending
        //{
        //    try
        //    {
        //        // 10/01/2024 - BhaveshT : This will fetch and fill data from Batch_Transaction table for RMC

        //        if (plantType.Contains("RMC"))
        //        {
        //            //DataTable dt = clsFunctions.fillDatatable("select WO_Code, tUpload1 from Batch_Dat_Trans where tUpload1=0 order by Batch_No");
        //            DataTable dt = clsFunctions.fillDatatable("select WO_Code, Batch_No from Batch_Dat_Trans where tUpload1=0 order by Batch_No");

        //            dgv_pending.Rows.Clear();
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                //dgv_pending.Rows.Add("", row["WO_Code"], row["tUpload1"]);
        //                dgv_pending.Rows.Add("", row["Batch_No"], row["WO_Code"]);
        //            }
        //            countPending.Text = (dgv_pending.RowCount - 1).ToString();

        //        }

        //        //----------------------------------------------------------------------------------------------------
        //        // 10/01/2024 - BhaveshT : This will fetch and fill data from Batch_Transaction table for Bitumen

        //        else if (plantType.Contains("Bit") || plantType.Contains("BT"))
        //        {
        //            dgv_pending.Columns["col_workcode"].Width = 80;
        //            //DataTable dt = clsFunctions.fillDatatable("select workcode, viplupload from tblHotmixPlant where viplupload=0 order by ID");
        //            DataTable dt = clsFunctions.fillDatatable("select workcode, tipper from tblHotmixPlant where viplupload=0 order by ID");

        //            dgv_pending.Rows.Clear();
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                //dgv_pending.Rows.Add("", row["workcode"], row["viplupload"]);
        //                dgv_pending.Rows.Add("", row["tipper"], row["workcode"]);
        //            }
        //            countPending.Text = (dgv_pending.RowCount - 1).ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        clsFunctions_comman.ErrorLog("mdiMain - getDataPendingStatus() : " + ex.Message);     //BhaveshT
        //    }
        //}

        //--------------------------------------------------------------------------------------------------------------------------------------------



        //------------------------------ OpenPortSetup for DM ----------------------------------------------------------------------------------------

        private void tsmport_Click(object sender, EventArgs e)
        {
            //if (loadForm == "PlantScadaV5")
            //{
            //    if (IsFormOpen("PortSetup_DM"))
            //    {
            //        MessageBox.Show("The form is already open!");
            //    }
            //    else
            //    {
            //        PortSetup_DM ps = new PortSetup_DM();
            //        ps.MdiParent = this;
            //        ps.Show();
            //    }
            //}

            //---------------------------------------------------------------------------------

        }






        //---------------------------------------------------------------------------------



        //------------------------------------------------------------- Live Batch Data menu --------------------------------------------------------------------

        public static string loadForm;

        public static bool IsOPCSetup;


        public static string SelectedOPCServerProgID;
        public static DataTable TagListXML;
        public static double ScanTime = 0.8;

        public static string PLC_IP_Address;
        public static string PLC_IP_Port;
        public static string PLC_Rack;
        public static string PLC_Slot;
        public static string SelectedPlcCpuType;

        public const string CONST_PLCTagsListXMLFile = "TagsList.txt";
        public const string CONST_OPCTagsListXMLFile = "TagsList.xml";
        public const string CONST_INI_FILENAME = "OpcSettings.ini";

        public const string CONST_SECTION_OPCSERVER = "OPCServerSettings";
        public const string CONST_KEY_PROGID = "ProgID";

        public const string CONST_SECTION_COMM = "CommSetting";
        public const string CONST_KEY_PORT_NO = "PortNo";

        public const string CONST_PLC_INI_FILENAME = "PlcCommSettings.ini";
        public const string CONST_SECTION_PLC_TCPIP = "PLCTcpIpSettings";
        public const string CONST_KEY_PLC_IP = "Plc_IP_Address";
        public const string CONST_KEY_PLC_Port = "Plc_IP_Port";
        public const string CONST_KEY_PLC_Rack = "Plc_Rack";
        public const string CONST_KEY_PLC_Slot = "Plc_Slot";
        public const string CONST_KEY_PLC_CPUTYPE = "Plc_CPU_Type";


        public const string CONST_SerialTagsListXMLFile = "SerialTagsList.xml";
        public const string CONST_TagsListXMLFile = "TagsList.xml";

        //public static bool IsSerialSetup;
        //public static int SelectedCommPortNumber;
        //public static DataTable SerialTagListXML;


        //------------------------------------------ Live Batch Data Click ---------------------------------------
        private void PlantInfo_Click_1(object sender, EventArgs e)
        {
            
            //----------------------------------- 08/02/2024 - BhaveshT : Use UniproSetup table instead of DBInfo ------------------------------------------
            //loadForm = clsFunctions_comman.loadFormName();

            loadForm = clsFunctions_comman.loadFormNameFromUniproSetup();
            clsFunctions_comman.ErrorLog("loadForm: " + loadForm);

            clsFunctions_comman.FileType = clsFunctions_comman.getFileType();

            //loadForm = "sany";
            clsFunctions.activeDeptName = clsFunctions.GetActiveDeptNameFromServerMapping();
            clsFunctions.activePlantCode = clsFunctions.GetActivePlantCodeFromServerMapping();

            //loadForm = "BatchDetailsHyper_WriteK";      // For Testing

            //-----------------------------------------------------------------------------------------------------------------------------------------

            // BY DINESH
            //selecting status as 'Y' for satisfying the condition to load the Form
            //loadForm = clsFunctions.loadSingleValueSetup("select FormName from DbInfo where status='Y'");


            //-------------------------------------------------------------------

            //-------------------------------------------------------------------

            //--- 
            if (IsFormOpen("RMC_ModBus"))
            {
                MessageBox.Show("The form is already open!");
            }
            else
            {
                RMC_ModBus rmc = new RMC_ModBus();
                rmc.MdiParent = this;
                rmc.Show();
            }

            //-------------------------------------------------------------------
            
        }

        //------------------------------------------------------------------- Work Master -------------------------------------------------------------------------



        //-------------------------------------------------------------------


        //---------------------------------------------------------------------- Recipe Master ----------------------------------------------------------------------



        private void ReceipeMst_Click_1(object sender, EventArgs e)
        {
            //loadForm = clsFunctions.loadSingleValueSetup("select FormName from DbInfo where status='Y'");
            if (plantType.Contains("RMC"))
            {
                if (IsFormOpen("ReceipeMaster_RMC"))
                {
                    MessageBox.Show("The form is already open!");
                }
                else
                {
                    //if (loadForm == "LoadDetailsmanu")
                    {
                        ReceipeMaster_RMC dm = new ReceipeMaster_RMC();
                        dm.MdiParent = this;
                        dm.Show();
                    }
                    //else
                    {

                    }
                }
            }

            //-------------------------------------------------------------------

            // 13/02/2024 - BhaveshT : This recipe master form should be used for all BT Modules/forms


        }

        //--------------------------------------------------------------------------------------------------------------------------------------------
        // 08/02/2024 - BhaveshT : Use Unipro_Setup table instead of DBInfo -------------------------------------

        /* Created Date:30/08/2023 by dinesh
         * this function verifies and notifies the prerequisites for the unipro
         */



        //--------------------------------------------------------------------------------------------------------------------------------------------

        private void VehicleMst_Click_1(object sender, EventArgs e)
        {
            if (IsFormOpen("TipperMaster"))
            {
                MessageBox.Show("The form is already open!");
            }
            else
            {
                TipperMaster tipper = new TipperMaster();
                tipper.MdiParent = this;
                tipper.Show();
            }
        }

        //-------------------------------------------------------------------

        //private void btn_uplaoder_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        errorBatchCount.Text = "000";
        //        countPending.Text = "000";
        //        countUploaded.Text = "000";

        //        dgv_history.Rows.Clear();
        //        dgv_pending.Rows.Clear();
        //        getDataUploadStatus();
        //        getDataPendingStatus();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsFunctions.ErrorLog("[Exception] mdiMain - btn Uploader Refresh Click(): " + ex.Message);
        //        throw;
        //    }
        //}

        //--------------------------------------------------------------------------------------------------------------------------------------------

        /* Function Modified Date:21/12/2023 By dInEsH
         * This function now can check for Plant expiry.
         * and if it found expired plant Service will be stopped.
         * 
         *Note: This function had no comments before I added comments.
         */

        bool isPlantValid = false;
        bool serviceActiveStatus = false;
        bool validStatus = false;
        bool invalidStatus = false;


        //----------------------------------------- timer1 ----------------------------------------

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 16/02/2024 - BhaveshT : Commented Phase 1 code because, UniUploader itself checks expiry, no need to check in software.

            /**************************************** Phase 1 ******************************************************/
            // obtaining plant expiry date
            //isPlantValid = clsFunctions.PlantExpiryChecker();
            //
            //if (isPlantValid && !validStatus)
            //{
            //    //plant is expired -> goes for stop service
            //    Task<bool> stopServiceTask = Task.Run(() => clsFunctions.StopService("VasundharaUploaderClient"));
            //    serviceActiveStatus = await stopServiceTask;
            //    validStatus = true;
            //    MessageBox.Show("Plant is expired, Please renew the plant!\n will not be uploaded until the license is renewed.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //
            //}
            //else
            //{
            //    // added by Dinesh Date:21/12/2023
            //    if (!isPlantValid && !invalidStatus)
            //    {
            //        try
            //        {
            //            invalidStatus = true;
            //            Task<bool> stopServiceTask = Task.Run(() => clsFunctions.StartService("VasundharaUploaderClient"));
            //            serviceActiveStatus = await stopServiceTask;
            //
            //
            //        }
            //        catch (Exception ex) { clsFunctions.ErrorLog("lblservicestatus_Click()" + ex.Message); }
            //    }
            //}
            /*************************************************************************************************************************/



            // This will change color of expiryMessage in every timer interval -----------------------

            if (expiryMessage.ForeColor == Color.DarkGreen)
            {
                expiryMessage.ForeColor = Color.Red;
                uploadInstruction.ForeColor = Color.DarkGreen;
            }
            else if (expiryMessage.ForeColor == Color.Red)
            {
                expiryMessage.ForeColor = Color.DarkGreen;
                uploadInstruction.ForeColor = Color.Red;
            }

            //-----------------------

            //15/04/2024 : BhaveshT - mdiMain: added label to show selected AliasName as work department.

            lb_workDept.Text = "Work Dept: " + clsFunctions.aliasName;
            //lb_workDept.Text = "Work Dept: SCADAINDIA - RMC";

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------



        //--------------------------------------------------------------------------------------------------------------------------------------------

        private async void mdiMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // clsFunctions_comman.InsertSoftwareStatus(clsFunctions.activeDeviceID, clsFunctions.activePlantCode, clsFunctions.activeDeptName, "OFF");

                //added by dinesh for terminating the thread that is running for checking whether service is running or not.if running then it will start it.
           

                try
                {
                    //----------- Forcefully stop the thread_pwd ---------------

                    try
                    {
                         
                    }
                    catch (Exception ex)
                    {
                        clsFunctions_comman.ErrorLog("mdiMain formClosed: exception while thread_pwd abort: " + ex.Message);
                    }

                    //-------------------------- To stop UniUploader service --------------------------



                    //-------------------------- To stop UniUploader_PWD service --------------------------

                    //if (clsFunctions.aliasName == "PWD - RMC")
                    //{
                    //    Task<bool> stopPWDServiceTask = Task.Run(() => clsFunctions.StopService("VasundharaUploaderClientPWD"));
                    //    serviceActiveStatus = await stopPWDServiceTask;
                    //    clsFunctions_comman.ErrorLog("UniUploader_PWD service stopped on mdiMain_FormClosed Event");
                    //}


                    //--------------------------

                    // main form closing log
                    clsFunctions_comman.ErrorLog("mdiMain form has been closed.");
                }
                catch (Exception ex)
                {
                    clsFunctions_comman.ErrorLog("At mdiMain_FormClosed()" + ex.Message);
                }

            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("mdiMain_FormClosed(): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------

        //-------------------------------------------------------------------


        //-------------------------------------------------------------------

        ///*Created Date : 05/02/2024 by dinesh
        //* this function checks for uploader service is start or not
        //*/
        //    if (IsFormOpen("PlcSetting"))
        //    {
        //        MessageBox.Show("The form is already open!");
        //    }
        //    else
        //    {
        //        PlcSetting plc = new PlcSetting();
        //        plc.MdiParent = this;
        //        plc.Show();
        //    }
        //}

        //--------------------------------------------------------------------------------------------------------------------------------------------

        /*Created Date : 05/02/2024 by dinesh - this function checks for uploader service is start or not   */


        //--------------------------------------------------------------------------------------------------------------------------------------------



        //--------------------------------------------------------------------------------------------------------------------------------------------

        /* Created date:05/02/2024 by Dinesh
        * this funtion checks whether firewall is disable or not
        */
        static string GetFirewallStatus()
        {
            try
            {
                Type type = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
                dynamic firewallManager = Activator.CreateInstance(type);

                if (firewallManager != null)
                {
                    bool isFirewallEnabled = firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled;
                    return isFirewallEnabled ? "Enabled" : "Disabled";
                }
                else
                {
                    return "Unable to access Windows Firewall settings.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        private void fireWallChecker()
        {
            try
            {
                string Fstatus = "";
                Fstatus = GetFirewallStatus();

                if (Fstatus == "Enabled")
                {
                    //Console.WriteLine("The service is running.");
                    // lblFirewallStatus.Image = global::Uniproject.Properties.Resources.on;
                    lblFirewallStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
                }
                else
                {
                    //Console.WriteLine("The service is stopped.");
                    // lblFirewallStatus.Image = global::Uniproject.Properties.Resources.off;
                    lblFirewallStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("at fireWallChecker() : " + ex.Message);
            }
        }

        //-------------------------------------------------------------------

        static string EnableFirewall()
        {
            try
            {
                Type type = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
                dynamic firewallManager = Activator.CreateInstance(type);

                if (firewallManager != null)
                {
                    firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled = true;
                    return "Windows Firewall has been enabled.";
                }
                else
                {
                    return "Unable to access Windows Firewall settings.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        private void lblservicestatus_Click(object sender, EventArgs e)
        {

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------------------------------------------------------------------



        //--------------------------------------------------------------------------------------------------------------------------------------------

        private void lblFirewallStatus_Click(object sender, EventArgs e)
        {
            try
            {
                string Fstatus = GetFirewallStatus();
                if (Fstatus == "Enabled")
                {
                    //-------------Disable Firewall--------------
                    Process process = new Process();
                    process.StartInfo.FileName = "netsh.exe";
                    process.StartInfo.Arguments = "advfirewall set allprofiles state off";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;

                    // Set the "run as administrator" verb
                    process.StartInfo.Verb = "runas";

                    process.Start();

                    // Wait for the command to finish and get the output
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    // Display the output in a message box
                    MessageBox.Show("Firewall Disabled " + output, "Firewall Disabled");
                }
                else
                {
                    string fstatus = EnableFirewall();
                    if (fstatus == "Windows Firewall has been enabled.")
                    {
                        MessageBox.Show("Firewall is Enabled.");
                    }
                }
                fireWallChecker();
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] mdiMain - lblFirewallStatus_Click(): " + ex.Message);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------
 //----------------------------------------------------------------------------------------------------------------------------------
// -----------------------------------------------------------------------------------------------------------------------------------
  
        //--------------------------------------------------------------------------------------------------------------------------------------------

        string department = "";
        string cmbAuto_Dept = "";
        string pCode = "";
        string plantTypeFromTuple = "";
 

        //--------------------------------------------------------------------------------------------------------------------------------------------

        internal static string GetFromINI(string sSection, string sKey, string sINIFile, string sDefault = "")
        {
            StringBuilder sb = new StringBuilder(2048);
            sb.Append(' ', 2048);
            string sValue = sb.ToString();

            int iLength = GetPrivateProfileString(sSection, sKey, sDefault, sb, sValue.Length, sINIFile);

            if (iLength == 0)
            {
                return sDefault;
            }
            else
            {
                return sb.ToString();
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------
         
        //--------------------------------------------------------------------------------------------------------------------------------------------

        // 05/07/2024 : BhaveshT - converted following code into function written by Dinesh.

        

        //--------------------------------------------------------------------------------------------------------------------------------------------
        /* Created date 21/06/2024 by dinesh this function is used for finding VIPL_Updater_Pro exe and excecute it */

        private bool ExecuteVIPLUpdaterPro()
        {
            string exePath = Path.Combine(Application.StartupPath, "VIPL_Updater_Pro.exe");

            if (File.Exists(exePath))
            {
                try
                {
                    Process.Start(exePath);
                    // MessageBox.Show("VIPL_Updater_Pro.exe started successfully.");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    clsFunctions.ErrorLog("[Exception] mdiMain - ExecuteVIPLUpdaterPro(): An error occurred while starting VIPL_Updater_Pro.exe: " + ex.Message);

                    return false;
                }
            }
            else
            {
                MessageBox.Show("VIPL_Updater_Pro.exe not found.");
                return false;
            }
        }

        private void WO_Mst_Click(object sender, EventArgs e)
        {
            if (IsFormOpen("WorkMaster"))
            {
                MessageBox.Show("The form is already open!");
            }
            else
            {
                WorkMaster workMaster = new WorkMaster();
                workMaster.MdiParent = this;
                workMaster.Show();
            }
        }

        private void Customer_Contractor_Click(object sender, EventArgs e)
        {
            if (IsFormOpen("CustomerMaster"))
            {
                MessageBox.Show("The form is already open!");
            }
            else
            {
                CustomerMaster cm = new CustomerMaster();
                cm.MdiParent = this;
                cm.Show();
            }
        }

        private void btn_uplaoder_Click(object sender, EventArgs e)
        {
            try
            {
                errorBatchCount.Text = "000";
                countPending.Text = "000";
                countUploaded.Text = "000";

                dgv_history.Rows.Clear();
                dgv_pending.Rows.Clear();
                getDataUploadStatus();
                getDataPendingStatus();
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] mdiMain - btn Uploader Refresh Click(): " + ex.Message);
                throw;
            }
        }
        private void getDataUploadStatus()      // Uploaded
        {
            try
            {
                // 10/01/2024 - BhaveshT : This will fetch and fill data from Batch_Transaction table for RMC

                if (plantType.Contains("RMC"))
                {
                    //DataTable dt = clsFunctions.fillDatatable("select WO_Code, tUpload1 from Batch_Dat_Trans where tUpload1=1 order by Batch_No");
                    DataTable dt = clsFunctions.fillDatatable("select WO_Code, Batch_No from Batch_Dat_Trans where tUpload1=1 order by Batch_No");

                    dgv_history.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        //dgv_history.Rows.Add("", row["WO_Code"], row["tUpload1"]);
                        dgv_history.Rows.Add("", row["Batch_No"], row["WO_Code"]);
                    }

                    errorBatchCount.Text = clsFunctions_comman.loadSinglevalue("SELECT COUNT(*) AS BatchCount FROM Batch_Dat_Trans WHERE tUpload1 NOT IN (1, 0)");
                    countUploaded.Text = (dgv_history.RowCount - 1).ToString();
                }

                //----------------------------------------------------------------------------------------------------

                // 10/01/2024 - BhaveshT : This will fetch and fill data from Batch_Transaction table for Bitumen

                else if (plantType.Contains("Bit") || plantType.Contains("BT"))
                {
                    dgv_history.Columns["workcode"].Width = 80;
                    //DataTable dt = clsFunctions.fillDatatable("select workcode, viplupload from tblHotmixPlant where viplupload=1 order by ID");
                    DataTable dt = clsFunctions.fillDatatable("select workcode, tipper from tblHotmixPlant where viplupload=1 order by ID");

                    dgv_history.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        //dgv_history.Rows.Add("", row["workcode"], row["viplupload"]);
                        dgv_history.Rows.Add("", row["tipper"], row["workcode"]);
                    }

                    errorBatchCount.Text = clsFunctions_comman.loadSinglevalue("SELECT COUNT(*) AS BatchCount FROM tblHotMixPlant WHERE viplupload NOT IN (1, 0)");

                    countUploaded.Text = (dgv_history.RowCount - 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions_comman.ErrorLog("mdiMain - getDataUploadStatus() : " + ex.Message);     //BhaveshT
            }
        }
        private void getDataPendingStatus()     // pending
        {
            try
            {
                // 10/01/2024 - BhaveshT : This will fetch and fill data from Batch_Transaction table for RMC

                if (plantType.Contains("RMC"))
                {
                    //DataTable dt = clsFunctions.fillDatatable("select WO_Code, tUpload1 from Batch_Dat_Trans where tUpload1=0 order by Batch_No");
                    DataTable dt = clsFunctions.fillDatatable("select WO_Code, Batch_No from Batch_Dat_Trans where tUpload1=0 order by Batch_No");

                    dgv_pending.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        //dgv_pending.Rows.Add("", row["WO_Code"], row["tUpload1"]);
                        dgv_pending.Rows.Add("", row["Batch_No"], row["WO_Code"]);
                    }
                    countPending.Text = (dgv_pending.RowCount - 1).ToString();

                }

                //----------------------------------------------------------------------------------------------------
                // 10/01/2024 - BhaveshT : This will fetch and fill data from Batch_Transaction table for Bitumen

                else if (plantType.Contains("Bit") || plantType.Contains("BT"))
                {
                    dgv_pending.Columns["col_workcode"].Width = 80;
                    //DataTable dt = clsFunctions.fillDatatable("select workcode, viplupload from tblHotmixPlant where viplupload=0 order by ID");
                    DataTable dt = clsFunctions.fillDatatable("select workcode, tipper from tblHotmixPlant where viplupload=0 order by ID");

                    dgv_pending.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        //dgv_pending.Rows.Add("", row["workcode"], row["viplupload"]);
                        dgv_pending.Rows.Add("", row["tipper"], row["workcode"]);
                    }
                    countPending.Text = (dgv_pending.RowCount - 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions_comman.ErrorLog("mdiMain - getDataPendingStatus() : " + ex.Message);     //BhaveshT
            }
        }

        private void btnWOfromAPI_Click(object sender, EventArgs e)
        {
            if (IsFormOpen("WO_API"))
            {
                MessageBox.Show("The form is already open!");
            }
            else
            {
                //UtilityTools.WO_API work = new WO_API();
                WO_API work = new WO_API();
                work.MdiParent = this;
                work.Show();
            }
        }

        private void btnPlcComm_Click(object sender, EventArgs e)
        {

        }

        private void SW_Config_Click(object sender, EventArgs e)
        {
            try
            {
                // 05/12/2023 - BhaveshT
                // Display a password prompt dialog to ask password after clicking on SW Configure Button

                // Without password for testing
                //Software_Configuration work = new Software_Configuration();
                //work.Show();

                string password = Prompt.ShowDialog("Enter password", "SW Config");

                // Compare the entered password with the pre-defined password
                if (password == "Unipro@73")
                {
                    if (IsFormOpen("QuickConfigure"))
                    {
                        MessageBox.Show("The form is already open!");
                    }
                    else
                    {
                        QuickConfigure q = new QuickConfigure();
                        q.Show();
                    }

                    //if (IsFormOpen("Software_Configuration"))
                    //{
                    //    MessageBox.Show("The form is already open!");
                    //}
                    //else
                    //{
                    //    Software_Configuration sc = new Software_Configuration();
                    //    sc.Show();
                    //}
                }
                else
                {
                    // Password is incorrect, display an error message
                    MessageBox.Show("Incorrect password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception at SWConfig_Click: " + ex.Message);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------------------------------------------------------------------

        internal static void WriteToINI(string sectionName, string keyName, string sValue, string strINIFilePath)
        {
            _ = WritePrivateProfileString(sectionName, keyName, sValue, strINIFilePath);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern long WritePrivateProfileString(string sSection, string sKey, string sValue, string sFilePath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        //--------------------------------------------------------------------------------------------------------------------------------------------

        /* Created Date: 30/08/2023 by Dinesh
         * this class is created just to know which tabindex should be open when DbInfo form is call and open
         */
        public static class tabbing
        {
            public static int tabbingIndex = 0;
            public static void FillMe(int ind)
            {
                tabbingIndex = ind;
            }
            public static void CallMe()
            {
                mdiMain mdi = new mdiMain();
                //mdi.PreFunctionning();
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------------------------------------


    }

}