using System;

namespace Uniproject.Classes
{
    public class clsPatch
    {

        public static System.Data.DataTable dt1 = new System.Data.DataTable();

        // 19/01/2024 - BhaveshT : Added method to create newly added columns to tables if not exists.
        public static void CreateNewColumns()
        {
            try
            {
                //----------------------- 19/12/2023 - BhaveshT: To check if column exists or not, if not exists - add column --------------

                clsFunctions.checknewcolumn("InsertType", "Text(1)", "Batch_Dat_Trans");
                clsFunctions.checknewcolumn("InsertType", "Text(1)", "tblHotMixPlant");

                clsFunctions.checknewcolumn("InsertType", "Text(1)", "BKP");
                clsFunctions.checknewcolumn("InsertType", "Text(1)", "tblHotMixPlant1");

                //---- Modified 01/07/2024 : BhaveshT - To create new columns in PlantSetup ---------------------------------------

                clsFunctions.CreateNewTableInSetup("PlantSetup");
                clsFunctions.CreateNewTableInSetup("PlantSetup");

                clsFunctions.checkNewColumnInSetup("ContractorCode", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("PlantCode", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("DeptName", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("DeviceID", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("ContractorName", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("PlantType", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("PlantName", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("Address", "Text", "PlantSetup");

                clsFunctions.checkNewColumnInSetup("MobNo", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("InstalledBy", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("MacID", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("Device_IPAddress", "Text", "PlantSetup");

                clsFunctions.checkNewColumnInSetup("InstallationType", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("InstallationDate", "DateTime", "PlantSetup");

                clsFunctions.checkNewColumnInSetup("Software_Status", "Text", "PlantSetup");
                
                clsFunctions.checkNewColumnInSetup("PlantExpiry", "DateTime", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("AnotherExpiry", "DateTime", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("UniproSetupID", "NUMERIC", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("AutoUploadStatus", "Text", "PlantSetup");

                clsFunctions.checkNewColumnInSetup("Plant_Location", "Text", "PlantSetup");

                // 26/07/2024 : BhaveshT - Columns to store version of installed Unipro, UniUploader & UniUploader_PWD_RMC exe -------

                clsFunctions.checkNewColumnInSetup("Unipro_Version", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("UniUp_Version", "Text", "PlantSetup");
                clsFunctions.checkNewColumnInSetup("UniUpPWD_Version", "Text", "PlantSetup");

                

                //----------------------- 01/07/2024 : BhaveshT - To create new columns for Production Error Flag ----------------------

                clsFunctions.checknewcolumnWithDefaultVal("Production_Error", "TEXT(1)", "Batch_Dat_Trans", "N");
                clsFunctions.checknewcolumnWithDefaultVal("Production_Error", "TEXT(1)", "tblHotMixPlant", "N");
                clsFunctions.checknewcolumnWithDefaultVal("Production_SMS", "TEXT(1)", "Batch_Dat_Trans", "N");
                clsFunctions.checknewcolumnWithDefaultVal("Production_SMS", "TEXT(1)", "tblHotMixPlant", "N");

                //----------------------- 01/07/2024 : BhaveshT - To create new table & columns to store Mobile No. against WO ----------------------

                clsFunctions.CreateNewTableInUnipro("WO_MobileNo");

                clsFunctions.checknewcolumn("SrNo", "Text", "WO_MobileNo");
                clsFunctions.checknewcolumn("WorkOrderNo", "Text", "WO_MobileNo");
                clsFunctions.checknewcolumn("MobileNo", "Text", "WO_MobileNo");
                clsFunctions.checknewcolumnWithDefaultVal("Flag", "TEXT(1)", "WO_MobileNo", "0");

                //----------------------- 15/02/2024 : BhaveshT - Create pType if not exists to store A/M : default 'A' told by - Govind sir ------

                clsFunctions.checknewcolumnWithDefaultValInSetupDB("pType", "Text(1)", "PlantSetup", "A");

                //---------------------------------------------------------------------------------------------------------------------------------

                clsFunctions.checknewcolumnWithDefaultVal("recipeUploaded", "NUMERIC", "Recipe_Master", "0");
                clsFunctions.checknewcolumnWithDefaultVal("recipeUploaded", "NUMERIC", "tblRecipeMaster", "0");

                //----------------------- 30/05/2024 : BhaveshT - Altered COLUMN workname to dataType: MEMO ---------------------------------------

                //clsFunctions.checkAlterColumn("workname", "MEMO", "WorkOrder");            //clsFunctions.AdoData("ALTER TABLE WorkOrder ALTER COLUMN workname MEMO");


                //----------------------- 20/03/2024 : BhaveshT - To create table 'RecipeForServer_RMC' and its columns ---------------------------

                clsFunctions.CreateNewTableInUnipro("RecipeForServer_RMC");
                clsFunctions.CreateNewTableInUnipro("RecipeForServer_RMC");

                clsFunctions.checknewcolumnWithDefaultVal("con_id", "Text", "RecipeForServer_RMC", "000");
                clsFunctions.checknewcolumnWithDefaultVal("plant_id", "Text", "RecipeForServer_RMC", "000");
                clsFunctions.checknewcolumnWithDefaultVal("wo_id", "Text", "RecipeForServer_RMC", "000");
                clsFunctions.checknewcolumnWithDefaultVal("plant_type", "Text", "RecipeForServer_RMC", "RMC");

                clsFunctions.checknewcolumn("recipe_name", "Text(30)", "RecipeForServer_RMC");          // normalized for VIPL
                clsFunctions.checknewcolumn("recipe_name_v", "Text(30)", "RecipeForServer_RMC");        // original as clientDB

                clsFunctions.checknewcolumnWithDefaultVal("agg1_name", "Text(10)", "RecipeForServer_RMC", "AGG1");
                clsFunctions.checknewcolumnWithDefaultVal("agg1_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("agg1_error_percent", "DOUBLE", "RecipeForServer_RMC", "3");

                clsFunctions.checknewcolumnWithDefaultVal("agg2_name", "Text(10)", "RecipeForServer_RMC", "AGG2");
                clsFunctions.checknewcolumnWithDefaultVal("agg2_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("agg2_error_percent", "DOUBLE", "RecipeForServer_RMC", "3");

                clsFunctions.checknewcolumnWithDefaultVal("agg3_name", "Text(10)", "RecipeForServer_RMC", "AGG3");
                clsFunctions.checknewcolumnWithDefaultVal("agg3_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("agg3_error_percent", "DOUBLE", "RecipeForServer_RMC", "3");

                clsFunctions.checknewcolumnWithDefaultVal("agg4_name", "Text(10)", "RecipeForServer_RMC", "AGG4");
                clsFunctions.checknewcolumnWithDefaultVal("agg4_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("agg4_error_percent", "DOUBLE", "RecipeForServer_RMC", "3");

                clsFunctions.checknewcolumnWithDefaultVal("agg5_name", "Text(10)", "RecipeForServer_RMC", "AGG5");
                clsFunctions.checknewcolumnWithDefaultVal("agg5_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("agg5_error_percent", "DOUBLE", "RecipeForServer_RMC", "3");

                clsFunctions.checknewcolumnWithDefaultVal("agg6_name", "Text(10)", "RecipeForServer_RMC", "AGG6");
                clsFunctions.checknewcolumnWithDefaultVal("agg6_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("agg6_error_percent", "DOUBLE", "RecipeForServer_RMC", "3");

                clsFunctions.checknewcolumnWithDefaultVal("cem1_name", "Text(10)", "RecipeForServer_RMC", "CEM1");
                clsFunctions.checknewcolumnWithDefaultVal("cem1_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("cem1_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("cem2_name", "Text(10)", "RecipeForServer_RMC", "CEM2");
                clsFunctions.checknewcolumnWithDefaultVal("cem2_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("cem2_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("cem3_name", "Text(10)", "RecipeForServer_RMC", "CEM3");
                clsFunctions.checknewcolumnWithDefaultVal("cem3_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("cem3_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("cem4_name", "Text(10)", "RecipeForServer_RMC", "CEM4");
                clsFunctions.checknewcolumnWithDefaultVal("cem4_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("cem4_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("water1_name", "Text(10)", "RecipeForServer_RMC", "WATER1");
                clsFunctions.checknewcolumnWithDefaultVal("water1_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("water1_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("water2_name", "Text(10)", "RecipeForServer_RMC", "WATER2");
                clsFunctions.checknewcolumnWithDefaultVal("water2_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("water2_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("admix1_name", "Text(10)", "RecipeForServer_RMC", "ADM1");
                clsFunctions.checknewcolumnWithDefaultVal("admix1_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("admix1_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("admix2_name", "Text(10)", "RecipeForServer_RMC", "ADM2");
                clsFunctions.checknewcolumnWithDefaultVal("admix2_qty", "DOUBLE", "RecipeForServer_RMC", "0"); 
                clsFunctions.checknewcolumnWithDefaultVal("admix2_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("admix3_name", "Text(10)", "RecipeForServer_RMC", "ADM3");
                clsFunctions.checknewcolumnWithDefaultVal("admix3_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("admix3_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("slurry_name", "Text(10)", "RecipeForServer_RMC", "SLURRY");
                clsFunctions.checknewcolumnWithDefaultVal("slurry_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("slurry_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("silica_name", "Text(10)", "RecipeForServer_RMC", "SILICA");
                clsFunctions.checknewcolumnWithDefaultVal("silica_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("silica_error_percent", "DOUBLE", "RecipeForServer_RMC", "1");

                clsFunctions.checknewcolumnWithDefaultVal("ice_name", "Text(10)", "RecipeForServer_RMC", "ICE");
                clsFunctions.checknewcolumnWithDefaultVal("ice_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("ice_error_percent", "DOUBLE", "RecipeForServer_RMC", "0"); 

                clsFunctions.checknewcolumnWithDefaultVal("total_qty", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("total_error_percent", "DOUBLE", "RecipeForServer_RMC", "0");


                clsFunctions.checknewcolumnWithDefaultVal("filler_name", "Text(10)", "RecipeForServer_RMC", "FILLER");
                clsFunctions.checknewcolumnWithDefaultVal("filler_error_percent", "DOUBLE", "RecipeForServer_RMC", "0");
                clsFunctions.checknewcolumnWithDefaultVal("filler_qty", "DOUBLE", "RecipeForServer_RMC", "0");


                clsFunctions.checknewcolumnWithDefaultVal("recipeUploaded", "DOUBLE", "RecipeForServer_RMC", "0");

                //---------------------------------------------------------------------------------------------------------------------------------
                clsFunctions.checknewcolumnWithDefaultVal("recipeUploaded", "NUMERIC", "Recipe_Master", "0");
                clsFunctions.checknewcolumnWithDefaultVal("recipeUploaded", "NUMERIC", "tblRecipeMaster", "0");

                //----------------------- 24/05/2024 : BhaveshT - Create LastUpdatedRecord to store Dept | Date | Time : told by - Vinay sir ------

                clsFunctions.CreateNewTableInUnipro("LastUpdateRecord");

                clsFunctions.checknewcolumn("Dept", "Text(10)", "LastUpdateRecord");
                clsFunctions.checknewcolumn("l_Date", "DateTime", "LastUpdateRecord");
                clsFunctions.checknewcolumn("l_Time", "DateTime", "LastUpdateRecord");

                //----------------------- 01/07/2024 : BhaveshT - Create tipper_interval table and its columns ---------------------------

                clsFunctions.CreateNewTableInSetup("tipper_interval");

                clsFunctions.checkNewColumnInSetup("truckinterval", "Text(30)", "tipper_interval");
                clsFunctions.checknewcolumnWithDefaultValInSetupDB("DocketHours", "NUMERIC", "tipper_interval", "24");

                //----------------------- 01/07/2024 : BhaveshT - Create Installation_Person table and its columns ---------------------------

                clsFunctions.CreateNewTableInSetup("Installation_Person");

                clsFunctions.checkNewColumnInSetup("Name", "Text(30)", "Installation_Person");
                clsFunctions.checkNewColumnInSetup("Designation", "Text(30)", "Installation_Person");
                clsFunctions.checkNewColumnInSetup("MobileNo", "Text(30)", "Installation_Person");
                clsFunctions.checkNewColumnInSetup("Flag", "Text(5)", "Installation_Person");

                //----------------------- 01/07/2024 : BhaveshT - Create Plant_LiveStatus_History table and its columns ---------------------------

                clsFunctions.CreateNewTableInSetup("Plant_LiveStatus_History");

                clsFunctions.checkNewColumnInSetup("DeviceID", "Text(30)", "Plant_LiveStatus_History");
                clsFunctions.checkNewColumnInSetup("PlantCode", "Text(30)", "Plant_LiveStatus_History");
                clsFunctions.checkNewColumnInSetup("PlantType", "Text(30)", "Plant_LiveStatus_History");
                clsFunctions.checkNewColumnInSetup("Date_Time", "DateTime", "Plant_LiveStatus_History");
                clsFunctions.checkNewColumnInSetup("Software_Status", "Text(5)", "Plant_LiveStatus_History");

                //----------------------- 01/07/2024 : BhaveshT - Create Plant_Renewal_History table and its columns ---------------------------

                clsFunctions.CreateNewTableInSetup("Plant_Renewal_History");

                clsFunctions.checkNewColumnInSetup("Date_Time", "DateTime", "Plant_Renewal_History");
                clsFunctions.checkNewColumnInSetup("DeviceID", "Text(30)", "Plant_Renewal_History");
                clsFunctions.checkNewColumnInSetup("ValidFrom", "DateTime", "Plant_Renewal_History");
                clsFunctions.checkNewColumnInSetup("PlantExpiry", "DateTime", "Plant_Renewal_History");
                clsFunctions.checkNewColumnInSetup("AnotherExpiry", "DateTime", "Plant_Renewal_History");
                clsFunctions.checkNewColumnInSetup("Flag", "Text(5)", "Plant_Renewal_History");

                //----------------------- 01/07/2024 : BhaveshT - Create Unipro_Setup table and its columns ---------------------------

                clsFunctions.CreateNewTableInSetup("Unipro_Setup");

                clsFunctions.checkNewColumnInSetup("UniproSetupID", "NUMERIC", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("Plant_Make", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("FormName", "TEXT", "Unipro_Setup");

                clsFunctions.checkNewColumnInSetup("UploaderName", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("Path", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("Pass", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("Description", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("DB_Type", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("PlantType", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("ImagePath", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("UILocation", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("ImageUsed", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("RecipeFormName", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("ConnectionString", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("BatchReport_FileName", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("DC_FileName", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("Status", "TEXT", "Unipro_Setup");
                clsFunctions.checkNewColumnInSetup("UploaderName", "TEXT", "Unipro_Setup");


                //----------------------- 01/07/2024 : BhaveshT - Create ReportMapping table and its columns ---------------------------

                clsFunctions.CreateNewTableInSetup("ReportMapping");

                clsFunctions.checkNewColumnInSetup("Report_Type", "TEXT(30)", "ReportMapping");
                clsFunctions.checkNewColumnInSetup("Report_Name", "TEXT(30)", "ReportMapping");
                clsFunctions.checkNewColumnInSetup("Flag", "TEXT(30)", "ReportMapping");


                //----------------------- 01/07/2024 : BhaveshT - Create AliasName table and its columns ---------------------------

                clsFunctions.CreateNewTableInSetup("AliasName");

                clsFunctions.checkNewColumnInSetup("AliasName", "TEXT(30)", "AliasName");
                clsFunctions.checkNewColumnInSetup("domain1", "TEXT", "AliasName");
                clsFunctions.checkNewColumnInSetup("domain2", "TEXT", "AliasName");


                //----------------------- 01/07/2024 : BhaveshT - To Create new columns in table: ServerMapping ---------------------------

                clsFunctions.CreateNewTableInSetup("ServerMapping");


                clsFunctions.checkNewColumnInSetup("SrNo", "NUMERIC", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("AliasName", "TEXT(30)", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("Note1", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("ipaddress", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("portno", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("BT_API", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("RMC_Trans_API", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("RMC_Transaction_API", "TEXT", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("Software_Status_API", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("plantExpiry", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("deptname", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("PlantType", "TEXT", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("Note2", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("ipaddress1", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("port1", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("BT_API1", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("RMC_Transaction_API1", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("RMC_Trans_API1", "TEXT", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("AutoReg_SMS", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("AutoReg_Verify", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("AutoReg_Save", "TEXT", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("GetWO", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("GetAllWO", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("AllocateWO", "TEXT", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("GetPlantDetails", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("GetMobNoFromWO", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("GetProdErrorTemplate", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("sendSMS", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("getInstallDetails", "TEXT", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("DPTStatus", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("Flag", "TEXT", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("DeviceID", "TEXT(30)", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("PlantCode", "TEXT(30)", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("GetDataHeaderTableSync", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("UploadDataHeaderTableSync", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("GetDataTransactionTableSync", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("UploadDataTransactionTableSync", "MEMO", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("ServerMapping_Preset", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("Unipro_Setup", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("PlantSetup", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("Plant_LiveStatus_History", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("m_SaveInstall", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("m_latestUp_Insert", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("m_latestUp_Get", "MEMO", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("PlantExpiryDate", "MEMO", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("Upload_UniproSetupID", "MEMO", "ServerMapping");

                clsFunctions.checkNewColumnInSetup("sendRecipe_API", "MEMO", "ServerMapping");      // 26/07/2024
                clsFunctions.checkNewColumnInSetup("Post_SiteName", "MEMO", "ServerMapping");       // 31/07/2024

                clsFunctions.checkNewColumnInSetup("ipaddress2", "TEXT", "ServerMapping");              //  12/08/2024 - for scadaindia
                clsFunctions.checkNewColumnInSetup("BT_API2", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("RMC_Trans_API2", "TEXT", "ServerMapping");
                clsFunctions.checkNewColumnInSetup("RMC_Transaction_API2", "TEXT", "ServerMapping");


                //----------------------- 01/07/2024 : BhaveshT - To Create new columns in table: ServerMapping_Preset ---------------------------

                clsFunctions.CreateNewTableInSetup("ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("SrNo", "NUMERIC", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("AliasName", "TEXT(30)", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("Note1", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("ipaddress", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("portno", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("BT_API", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("RMC_Trans_API", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("RMC_Transaction_API", "TEXT", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("Software_Status_API", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("plantExpiry", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("deptname", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("PlantType", "TEXT", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("Note2", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("ipaddress1", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("port1", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("BT_API1", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("RMC_Transaction_API1", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("RMC_Trans_API1", "TEXT", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("AutoReg_SMS", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("AutoReg_Verify", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("AutoReg_Save", "TEXT", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("GetWO", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("GetAllWO", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("AllocateWO", "TEXT", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("GetPlantDetails", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("GetMobNoFromWO", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("GetProdErrorTemplate", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("sendSMS", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("getInstallDetails", "TEXT", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("DPTStatus", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("Flag", "TEXT", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("DeviceID", "TEXT(30)", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("PlantCode", "TEXT(30)", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("GetDataHeaderTableSync", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("UploadDataHeaderTableSync", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("GetDataTransactionTableSync", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("UploadDataTransactionTableSync", "MEMO", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("ServerMapping_Preset", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("Unipro_Setup", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("PlantSetup", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("Plant_LiveStatus_History", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("m_SaveInstall", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("m_latestUp_Insert", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("m_latestUp_Get", "MEMO", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("PlantExpiryDate", "MEMO", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("Upload_UniproSetupID", "MEMO", "ServerMapping_Preset");

                clsFunctions.checkNewColumnInSetup("sendRecipe_API", "MEMO", "ServerMapping_Preset");             // 26/07/2024
                clsFunctions.checkNewColumnInSetup("Post_SiteName", "MEMO", "ServerMapping_Preset");              // 31/07/2024

                clsFunctions.checkNewColumnInSetup("ipaddress2", "TEXT", "ServerMapping_Preset");                 //  12/08/2024 - for scadaindia
                clsFunctions.checkNewColumnInSetup("BT_API2", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("RMC_Trans_API2", "TEXT", "ServerMapping_Preset");
                clsFunctions.checkNewColumnInSetup("RMC_Transaction_API2", "TEXT", "ServerMapping_Preset");


                //----------------------- 02/07/2024 : BhaveshT - To Create new columns in table: DataHeaderTableSync & DataTransactionTableSync ---------------------------

                clsFunctions.checkNewColumnInSetup("plant_code", "TEXT", "DataHeaderTableSync");
                clsFunctions.checkNewColumnInSetup("dept_name", "TEXT", "DataHeaderTableSync");
                clsFunctions.checkNewColumnInSetup("DataHeaderUpload", "NUMERIC", "DataHeaderTableSync");

                clsFunctions.checkNewColumnInSetup("plant_code", "TEXT", "DataTransactionTableSync");
                clsFunctions.checkNewColumnInSetup("dept_name", "TEXT", "DataTransactionTableSync");
                clsFunctions.checkNewColumnInSetup("DataHeaderUpload", "NUMERIC", "DataTransactionTableSync");


                //---------------------------------------------------------------------------------------------------------------------------------

                // Scripts to insert static data to tables and columns.

                //-----------------------
                // AliasName - 
                // INSERT INTO AliasName (AliasName) VALUES ('PWD - BT'), ('PWD - RMC'), ('PMC - BT'), ('PMC - RMC'), ('PCMC - BT'), ('PCMC - RMC'), ('ISCADA - BT'),
                //      ('ISCADA - RMC'), ('PMRDA - BT'), ('PMRDA - RMC'), ('MIDC - BT'), ('MIDC - RMC'), ('MHADA - BT'), ('MHADA - RMC'), ('VIPL - BT'), ('VIPL - RMC');

                try
                {
                    dt1 = clsFunctions.fillDatatable_setup("Select AliasName from AliasName");
                }
                catch (Exception e) 
                {
                    clsFunctions.CreateNewTableInSetup("AliasName");
                }

                if(dt1.Rows.Count == 0)
                {
                    dt1 = null;
                    string[] aliasNames = new string[]
                    {
                        "PWD - BT",
                        "PWD - RMC",
                        "PMC - BT",
                        "PMC - RMC",
                        "PCMC - BT",
                        "PCMC - RMC",
                        "ISCADA - BT",
                        "ISCADA - RMC",
                        "PMRDA - BT",
                        "PMRDA - RMC",
                        "MIDC - BT",
                        "MIDC - RMC",
                        "MHADA - BT",
                        "MHADA - RMC",
                        "VIPL - BT",
                        "VIPL - RMC",
                        "INDISCADA - BT",
                        "INDISCADA - RMC",
                        "MSPHC - BT",
                        "MSPHC - RMC"
                    };

                    // Call the method to insert alias names
                    clsFunctions.InsertDataToColumn(aliasNames, "AliasName", "AliasName");
                }

                if (dt1.Rows.Count == 16)
                {
                    dt1 = null;
                    string[] aliasNames = new string[]
                    {
                        "INDISCADA - BT",
                        "INDISCADA - RMC",
                        "MSPHC - BT",
                        "MSPHC - RMC"
                    };

                    // Call the method to insert alias names
                    clsFunctions.InsertDataToColumn(aliasNames, "AliasName", "AliasName");
                }

                //-----------------------
                // Installation_Person

                try
                {
                    dt1 = null;
                    dt1 = clsFunctions.fillDatatable_setup("Select * from Installation_Person");
                }
                catch (Exception e)
                {
                    clsFunctions.CreateNewTableInSetup("Installation_Person");
                }

                if (dt1.Rows.Count == 0)
                {
                    // List of Engineers : 1.Ashish Birajdar 2.Ketan Palande 3.Karan Jaybhay 4.Amol Shinde

                    string[] InstallPNames = new string[]
                    {
                        "Bhavesh Thorat",
                        "Dinesh Padvi",
                        "Govind Jadhav",
                        "Ketan Shelke",
                        "Ashish Birajdar",
                        "Ketan Palande",
                        "Annu Verma",
                        "Rohit Dakave",
                        "Karan Jaybhay",
                        "Amol Shinde",
                        "Charul Sonar",
                        "Rupali Piske"

                    };

                    // Call the method to insert alias names
                    clsFunctions.InsertDataToColumn(InstallPNames, "Installation_Person", "Name");

                    clsFunctions.AdoData_setup("Update Installation_Person set flag = 'Y'");
                }

                //------------- 16/07/2024 : BhaveshT - To Create new table & columns: PWD_DM_Decrypt ----------------

                clsFunctions.CreateNewTableInUnipro("PWD_DM_Decrypt");

                clsFunctions.checknewcolumn("F1","TEXT", "PWD_DM_Decrypt");       // INTEGRATOR_ID
                clsFunctions.checknewcolumn("F2", "TEXT", "PWD_DM_Decrypt");      // WORK_ID
                clsFunctions.checknewcolumn("F3", "TEXT", "PWD_DM_Decrypt");      // PLANT_ID
                clsFunctions.checknewcolumn("F4", "TEXT", "PWD_DM_Decrypt");      // PWD_WORK_YN
                clsFunctions.checknewcolumn("F5", "TEXT", "PWD_DM_Decrypt");      // MATERIAL_TYPE
                clsFunctions.checknewcolumn("F6", "TEXT", "PWD_DM_Decrypt");      // RFI_CARD_ID
                clsFunctions.checknewcolumn("F7", "TEXT", "PWD_DM_Decrypt");      // TEMPERATURE_1
                clsFunctions.checknewcolumn("F8", "TEXT", "PWD_DM_Decrypt");      // TEMPERATURE_2
                clsFunctions.checknewcolumn("F9", "TEXT", "PWD_DM_Decrypt");      // TEMPERATURE_3
                clsFunctions.checknewcolumn("F10", "TEXT", "PWD_DM_Decrypt");     // IO
                clsFunctions.checknewcolumn("F11", "TEXT", "PWD_DM_Decrypt");     // EXTRA
                clsFunctions.checknewcolumn("F12", "TEXT", "PWD_DM_Decrypt");     // REAL_TIME
                clsFunctions.checknewcolumn("F13", "TEXT", "PWD_DM_Decrypt");     // loadno

                clsFunctions.checknewcolumn("flag1", "NUMERIC", "PWD_DM_Decrypt");   // upload flag PWD
                clsFunctions.checknewcolumn("flag2", "TEXT", "PWD_DM_Decrypt");      // upload flag VIPL

                clsFunctions.checknewcolumnWithDefaultVal("InsertType", "TEXT", "PWD_DM_Decrypt", "A");     // InsertType = A/M


                //------------- 08/04/2025 : BhaveshT - to store RDM type ----------------

                clsFunctions.checknewcolumn("RDM_Type", "TEXT", "tblPortSetting");     // RDM

                //------------- 14/08/2024 : BhaveshT - To Create new table & columns: Strval_index ----------------

                clsFunctions.CreateNewTableInUnipro("Strval_Index");

                dt1 = clsFunctions.fillDatatable("select * from Strval_Index");

                if (dt1.Rows.Count == 0)
                {
                    clsFunctions.checknewcolumn("ID", "AUTOINCREMENT", "Strval_Index");
                    clsFunctions.checknewcolumnWithDefaultVal("agg1", "NUMERIC", "Strval_Index", "23");
                    clsFunctions.checknewcolumnWithDefaultVal("agg2", "NUMERIC", "Strval_Index", "27");
                    clsFunctions.checknewcolumnWithDefaultVal("agg3", "NUMERIC", "Strval_Index", "31");
                    clsFunctions.checknewcolumnWithDefaultVal("agg4", "NUMERIC", "Strval_Index", "35");
                    clsFunctions.checknewcolumnWithDefaultVal("agg5", "NUMERIC", "Strval_Index", "67");
                    clsFunctions.checknewcolumnWithDefaultVal("water", "NUMERIC", "Strval_Index", "60");
                    clsFunctions.checknewcolumnWithDefaultVal("lockflag", "NUMERIC", "Strval_Index", "40");
                    clsFunctions.checknewcolumnWithDefaultVal("strlen", "NUMERIC", "Strval_Index", "90");
                    clsFunctions.checknewcolumnWithDefaultVal("saveflag", "NUMERIC", "Strval_Index", "64");
                    clsFunctions.checknewcolumnWithDefaultVal("adm1", "NUMERIC", "Strval_Index", "11");
                    clsFunctions.checknewcolumnWithDefaultVal("valid1", "TEXT", "Strval_Index", "0");
                    clsFunctions.checknewcolumnWithDefaultVal("valid2", "TEXT", "Strval_Index", "0");

                    clsFunctions.AdoData("Insert into Strval_Index (agg1, valid1, valid2) values (23, '1,3,80', '1,3,9')");
                }

                clsFunctions.checknewcolumnWithDefaultVal("cem1", "NUMERIC", "Strval_Index", "23");
                clsFunctions.checknewcolumnWithDefaultVal("cem2", "NUMERIC", "Strval_Index", "27");

                //------------- 14/08/2024 : BhaveshT - To Create new table & columns: Modbus_address ----------------

                clsFunctions.CreateNewTableInUnipro("Modbus_address");

                dt1 = clsFunctions.fillDatatable("select * from Modbus_address");

                if (dt1.Rows.Count == 0)
                {
                    clsFunctions.checknewcolumn("ind", "AUTOINCREMENT", "Modbus_address");
                    clsFunctions.checknewcolumnWithDefaultVal("maddress", "TEXT", "Modbus_address", "0");
                    clsFunctions.checknewcolumnWithDefaultVal("IOCARDpanneladdress", "TEXT", "Modbus_address", "0");

                    clsFunctions.AdoData("Insert into Modbus_address(maddress) values(1)");
                    clsFunctions.AdoData("Insert into Modbus_address(maddress) values(3)");
                    clsFunctions.AdoData("Insert into Modbus_address(maddress) values(0)");
                    clsFunctions.AdoData("Insert into Modbus_address(maddress) values(49)");
                    clsFunctions.AdoData("Insert into Modbus_address(maddress) values(0)");
                    clsFunctions.AdoData("Insert into Modbus_address(maddress) values(40)");
                    clsFunctions.AdoData("Insert into Modbus_address(maddress) values(20)");
                    clsFunctions.AdoData("Insert into Modbus_address(maddress) values(27)");

                }

                //------------- 28/12/2024 : BhaveshT - To Create new column in PlantSetup ----------------

                clsFunctions.checkNewColumnInSetup("U_ID", "Text", "PlantSetup");


                //---------------------------------------------------------------------------------------------------------------------------------

                //---------------------------------------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at clsPatch.CreateNewColumns() : " + ex.Message);
            }

        }
        

    }
}
