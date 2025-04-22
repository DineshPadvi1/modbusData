using System;

namespace Uniproject.Classes
{
    //class clsVariables
    public class clsVariables       // made this class public - BhaveshT 01/11/2023
    {
        public int rowIndex = 0;                   // 24/11/2023   -   BhaveshtT
        public int Batch_No = 0;
        public int Batch_Index=0;
        public string Batch_Date="";
        public string Batch_Time="";
        public string Batch_Time_Text="";
        public string Batch_Start_Time="";
        public string Batch_End_Time="";
        public string Batch_Year="";
        public string Batcher_Name="-";
        public string Batcher_User_Level="-";
        public string Customer_Code="";
        public string Recipe_Code="";
        public string Recipe_Name="";
        public string Mixing_Time="";
        public string Mixer_Capacity="";
        public string strength="";
        public string Site="";
        public string SiteName = "";
        public string Truck_No="";
        public string Truck_Driver="";
        public double Production_Qty=0;
        public double Ordered_Qty=0;
        public double Returned_Qty=0;
        public double WithThisLoad=0;
        public double Batch_Size=0;
        public double Order_No=0;
        public double Schedule_Id=0;

        public double Gate1_Target=0;
        public double Gate2_Target=0;
        public double Gate3_Target=0;
        public double Gate4_Target=0;
        public double Gate5_Target=0;
        public double Gate6_Target=0;
        public double Cement1_Target=0;
        public double Cement2_Target=0;
        public double Cement3_Target=0;
        public double Cement4_Target=0;
        public double Filler_Target=0;
        public double Water1_Target=0;
        public double slurry_Target=0;
        public double Water2_Target=0;
        public double Silica_Target=0;
        public double Adm1_Target1=0;
        public double Adm1_Target2=0;
        public double Adm2_Target1=0;
        public double Adm2_Target2=0;
        public double Cost_Per_Mtr_Cube=0;
        public double Total_Cost=0;
        public double Plant_No = 0;                                 // Convert.ToDouble(clsFunctions.loadSingleValueSetup("Select plantcode from PlantSetup"));//0;
        public double Weighed_Net_Weight=0;
        public double Weigh_Bridge_Stat=0;
        public string tExportStatus="Y";
        public int tUpload1=0;
        public int tUpload2=0;


        //-------------------------------
        public double Gate1_R_Target = 0;
        public double Gate2_R_Target = 0;
        public double Gate3_R_Target = 0;
        public double Gate4_R_Target = 0;
        public double Cement1_R_Target = 0;
        public double Filler1_R_Target = 0;
        public double Water1_R_Target = 0;
        public double Adm1_R_Target1 = 0;

        //--------------------------------

        public double Consistancy=0;
        public double Gate1_Actual=0;
        public double Gate2_Actual=0;
        public double Gate3_Actual=0;
        public double Gate4_Actual=0;
        public double Gate5_Actual=0;
        public double Gate6_Actual=0;
        public double Cement1_Actual=0;
        public double Cement2_Actual=0;
        public double Cement3_Actual=0;
        public double Cement4_Actual=0;
        public double Filler_Actual=0;
        public double Water1_Actual=0;
        public double slurry_Actual=0;
        public double Water2_Actual=0;
        public double Silica_Actual=0;
        public double Adm1_Actual1=0;
        public double Adm1_Actual2=0;
        public double Adm2_Actual1=0;
        public double Adm2_Actual2=0;
        public double Gate1_Moisture=0;
        public double Gate2_Moisture=0;
        public double Gate3_Moisture=0;
        public double Gate4_Moisture=0;
        public double Gate5_Moisture=0;
        public double Gate6_Moisture=0;

        //----------------- 16/01/2024 - BhaveshT for Preset_Data, Error correction ---------------------
        public double Gate1_Correction = 0;
        public double Gate2_Correction = 0;
        public double Gate3_Correction = 0;
        public double Gate4_Correction = 0;
        public double Gate5_Correction = 0;
        public double Gate6_Correction = 0;
        //-----------------------------------------------------------------------------------------------

        public double Cement1_Correction=0;
        public double Cement2_Correction=0;
        public double Cement3_Correction=0;
        public double Cement4_Correction=0;
        public double Filler_Correction=0;
        public double Water1_Correction=0;
        public double slurry_Correction=0;
        public double Water2_Correction=0;
        public double Silica_Correction=0;
        public double Adm1_Correction1=0;
        public double Adm1_Correction2=0;
        public double Adm2_Correction1=0;
        public double Adm2_Correction2=0;
        //===========================================================================================================================
        public double Temp_Gate1_Actual=0;
        public double Temp_Gate2_Actual=0;
        public double Temp_Gate3_Actual=0;
        public double Temp_Gate4_Actual=0;
        public double Temp_Gate5_Actual=0;
        public double Temp_Gate6_Actual=0;
        public double Temp_Cement1_Actual=0;
        public double Temp_Cement2_Actual=0;
        public double Temp_Cement3_Actual=0;
        public double Temp_Cement4_Actual=0;
        public double Temp_Filler_Actual=0;
        public double Temp_Water1_Actual=0;
        public double Temp_slurry_Actual=0;
        public double Temp_Water2_Actual=0;
        public double Temp_Silica_Actual=0;
        public double Temp_Adm1_Actual1=0;
        public double Temp_Adm1_Actual2=0;
        public double Temp_Adm2_Actual1=0;
        public double Temp_Adm2_Actual2=0;
        public double Temp_Water1_Correction = 0;
        public double Temp_Water2_Correction = 0;

        //===========================================================================================================================
       
        public string E_Batch_Date="0";
        public string E_Batch_Time="0";
        public string E_Batch_Time_Text="0";
        public string E_Batch_Start_Time="0";

        public static string _Batch_Start_Time = "0";

        public string E_Batch_End_Time="0";
        public string E_Batch_Year="0";
        public string E_Batcher_Name="0";
        public string E_Batcher_User_Level="0";
        public string E_Customer_Code="0";
        public string E_Recipe_Code="0";
        public string E_Recipe_Name="0";
        public string E_Mixing_Time="0";
        public string E_Mixer_Capacity="0";
        public string E_strength="0";
        public string E_Site="0";
        public string E_Truck_No="0";
        public string E_Truck_Driver="0";
        public string E_Production_Qty="0";

        public static double prodqty = 0.0;
        public static double bsize = 0.0;

        public static string recipe = "TEST";

        public string E_Ordered_Qty="0";
        public string E_Returned_Qty="0";
        public string E_WithThisLoad="0";
        public string E_Batch_Size="0";
        public string E_Order_No="0";
        public string E_Schedule_Id="0";
        public string E_Gate1_Target="0";
        public string E_Gate2_Target="0";
        public string E_Gate3_Target="0";
        public string E_Gate4_Target="0";
        public string E_Gate5_Target="0";
        public string E_Gate6_Target="0";
        public string E_Cement1_Target="0";
        public string E_Cement2_Target="0";
        public string E_Cement3_Target="0";
        public string E_Cement4_Target="0";
        public string E_Filler_Target="0";
        public string E_Water1_Target="0";
        public string E_slurry_Target="0";
        public string E_Water2_Target="0";
        public string E_Silica_Target="0";
        public string E_Adm1_Target1="0";
        public string E_Adm1_Target2="0";
        public string E_Adm2_Target1="0";
        public string E_Adm2_Target2="0";
        public string E_Cost_Per_Mtr_Cube="0";
        public string E_Total_Cost="0";
        public string E_Plant_No="0";
        public string E_Weighed_Net_Weight="0";
        public string E_Weigh_Bridge_Stat="0";

        public string E_Consistancy="0";
        public string E_Gate1_Actual="0";
        public string E_Gate2_Actual="0";
        public string E_Gate3_Actual="0";
        public string E_Gate4_Actual="0";
        public string E_Gate5_Actual="0";
        public string E_Gate6_Actual="0";
        public string E_Cement1_Actual="0";
        public string E_Cement2_Actual="0";
        public string E_Cement3_Actual="0";
        public string E_Cement4_Actual="0";
        public string E_Filler_Actual="0";
        public string E_Water1_Actual="0";
        public string E_slurry_Actual="0";
        public string E_Water2_Actual="0";
        public string E_Silica_Actual="0";
        public string E_Adm1_Actual1="0";
        public string E_Adm1_Actual2="0";
        public string E_Adm2_Actual1="0";
        public string E_Adm2_Actual2="0";
        public string E_Gate1_Moisture="0";
        public string E_Gate2_Moisture="0";
        public string E_Gate3_Moisture="0";
        public string E_Gate4_Moisture="0";
        public string E_Gate5_Moisture="0";
        public string E_Gate6_Moisture="0";
        public string E_Cement1_Correction="0";
        public string E_Cement2_Correction="0";
        public string E_Cement3_Correction="0";
        public string E_Cement4_Correction="0";
        public string E_Filler_Correction="0";
        public string E_Water1_Correction="0";
        public string E_slurry_Correction="0";
        public string E_Water2_Correction="0";
        public string E_Silica_Correction="0";
        public string E_Adm1_Correction1="0";
        public string E_Adm1_Correction2="0";
        public string E_Adm2_Correction1="0";
        public string E_Adm2_Correction2="0";
        public string E_Pigment_Actual="0";
        public string E_Pigment_Target="0";
        public string E_Balance_Wtr="0";
        //========================================================================================================

        public double Gate1_sum = 0;
        public double Gate2_sum = 0;
        public double Gate3_sum = 0;
        public double Gate4_sum = 0;
        public double Gate5_sum = 0;
        public double Gate6_sum = 0;
        public double Cement1_sum = 0;
        public double Cement2_sum = 0;
        public double Cement3_sum = 0;
        public double Cement4_sum = 0;
        public double Filler_sum = 0;
        public double Water1_sum = 0;
        public double slurry_sum = 0;
        public double Water2_sum = 0;
        public double Silica_sum = 0;
        public double Adm11_sum = 0;
        public double Adm12_sum = 0;
        public double Adm21_sum = 0;
        public double Adm22_sum = 0;
        public double WtrCorr_sum = 0;

        //======================================= From RMC CP18 HYPER VAR =================================================================

        //======================================================================================================== public double CYL = 0;
        public double CYLT = 0;
        //========================== To Add Diffreance betwwen A and C Receipe ====================
        public double agg1_Diff = 0;
        public double agg2_Diff = 0;
        public double agg3_Diff = 0;
        public double agg4_Diff = 0;
        public double cement1_Diff = 0;
        public double filler1_Diff = 0;
        public double filler2_Diff = 0;
        public double water1_Diff = 0;
        public double water2_Diff = 0;
        public double water3_Diff = 0;
        public double addmix1_Diff = 0;
        public double addmix2_Diff = 0;
        public double ice_Diff = 0;
        //===================================== To Save Batch ======================================
        public string time = "";
        public double agg1 = 0;
        public double agg2 = 0;
        public double agg3 = 0;
        public double agg4 = 0;
        public double cement_1 = 0;
        public double filler1 = 0;
        public double filler2 = 0;
        public double water1 = 0;
        public double water2 = 0;
        public double addmix1 = 0;
        public double addmix2 = 0;
        public double Wat_Corr = 0;
        public double ice = 0;
        //===================================== To Save Actual value =================================
        public double agg1_act = 0;
        public double agg2_act = 0;
        public double agg3_act = 0;
        public double agg4_act = 0;
        public double cement1_act = 0;
        public double filler1_act = 0;
        public double filler2_act = 0;
        public double water1_act = 0;
        public double water2_act = 0;
        public double water3_act = 0;
        public double addmix1_act = 0;
        public double addmix2_act = 0;
        public double ice_act = 0;
        //===================================== To Save Batch =========================================
        public double agg1_nom = 0;
        public double agg2_nom = 0;
        public double agg3_nom = 0;
        public double agg4_nom = 0;
        public double cement1_nom = 0;
        public double filler1_nom = 0;
        public double filler2_nom = 0;
        public double water1_nom = 0;
        public double water2_nom = 0;
        public double water3_nom = 0;
        public double addmix1_nom = 0;
        public double addmix2_nom = 0;
        public double ice_nom = 0;

        public double agg1_nom_Show = 0;
        public double agg2_nom_Show = 0;
        public double agg3_nom_Show = 0;
        public double agg4_nom_Show = 0;
        public double cement1_nom_Show = 0;
        public double filler1_nom_Show = 0;
        public double filler2_nom_Show = 0;
        public double water1_nom_Show = 0;
        public double water2_nom_Show = 0;
        public double water3_nom_Show = 0;
        public double addmix1_nom_Show = 0;
        public double addmix2_nom_Show = 0;
        public double ice_nom_Show = 0;
        //=============================================Other Varibales for coading=======================
        //public string Truck_No = "0";
        public string Recipe = "0";
        public string WorkNo = "0";
        public string PWD_SiteNo = "0";
        public string VIPL_PlantNo = "0";
        public string ContractorNo = "0";
        public string Customer_No = "0";
        public double Prod_Qty = 0;
        //public double Batch_Size = 0;
        public string Date_t = "";
        public string Time_t = "";

        public int Sr_No = 1;
        public string Cust_Name = "";
        //public string tExportStatus = "Y";
        public string PWD_Flag = "0";
        public string VIPL_Flag = "0";
        //================================================================================================
        //=======================================Variable for encrpted data===============================
        //================================================================================================
        //public string E_Truck_No = "";
        public string E_Recipe = "";
        public string E_WorkNo = "";
        public string E_PWD_SiteNo = "";
        public string E_VIPL_PlantNo = "";
        public string E_ContractorNo = "";
        public string E_Customer_No = "";
        public string E_Prod_Qty = "";
        //public string E_Batch_Size = "";
        public string E_Date = "";
        public string E_time = "";

        public string E_EndTime = "";

        public string E_agg1 = "";
        public string E_agg2 = "";
        public string E_agg3 = "";
        public string E_agg4 = "";
        public string E_cement_1 = "";
        public string E_filler1 = "";
        public string E_filler2 = "";
        public string E_water1 = "";
        public string E_water2 = "";
        public string E_Wat_Corr = "";
        public string E_addmix1 = "";
        public string E_addmix2 = "";
        public string E_ice = "";
        //===================================== To Save Actual value =================================
        public string E_agg1_act = "";
        public string E_agg2_act = "";
        public string E_agg3_act = "";
        public string E_agg4_act = "";
        public string E_cement1_act = "";
        public string E_filler1_act = "";
        public string E_filler2_act = "";
        public string E_water1_act = "";
        public string E_water2_act = "";
        public string E_water3_act = "";
        public string E_addmix1_act = "";
        public string E_addmix2_act = "";
        public string E_ice_act = "";
        //===================================== To Save Batch =========================================
        public string E_agg1_nom = "";
        public string E_agg2_nom = "";
        public string E_agg3_nom = "";
        public string E_agg4_nom = "";
        public string E_cement1_nom = "";
        public string E_filler1_nom = "";
        public string E_filler2_nom = "";
        public string E_water1_nom = "";
        public string E_water2_nom = "";
        public string E_water3_nom = "";
        public string E_addmix1_nom = "";
        public string E_addmix2_nom = "";
        public string E_ice_nom = "";

        //------------------ column names -------------------- 25/11/2023 BhaveshT


        public string col_query1 = "0";
        public string col_query2 = "0";

        public string col_Batch_Date = "0";

        public string col_Batch_No = "0";       // from Transaction

        public string col_Batch_No_head = "0";       // from Header

        public string col_Batch_Index = "0";
        public string col_Batch_Time = "0";
        public string col_Batch_Time_Text = "0";
        public string col_Batch_Start_Time = "0";
        public string col_Batch_End_Time = "0";
        public string col_Batch_Year = "0";
        public string col_Batcher_Name = "0";
        public string col_Batcher_User_Level = "0";
        public string col_Customer_Code = "0";
        public string col_Recipe_Code = "0";
        public string col_Recipe_Name = "0";
        public string col_Mixing_Time = "0";
        public string col_Mixer_Capacity = "0";
        public string col_strength = "0";
        public string col_Site = "0";
        public string col_Truck_No = "0";
        public string col_Truck_Driver = "0";
        public string col_Production_Qty = "0";
        public string col_Ordered_Qty = "0";
        public string col_Returned_Qty = "0";
        public string col_WithThisLoad = "0";
        public string col_Batch_Size = "0";
        public string col_Order_No = "0";
        public string col_Schedule_Id = "0";

        public string col_Gate1_Target = "AGG1";               //public string col_Gate1_Target = "0";
        public string col_Gate2_Target = "AGG2";               //public string col_Gate2_Target = "0";
        public string col_Gate3_Target = "AGG3";               //public string col_Gate3_Target = "0";
        public string col_Gate4_Target = "AGG4";               //public string col_Gate4_Target = "0";
        public string col_Gate5_Target = "AGG5";               //public string col_Gate5_Target = "0";
        public string col_Gate6_Target = "AGG6";               //public string col_Gate6_Target = "0";
        public string col_Cement1_Target = "CEM1";             //public string col_Cement1_Target = "0";
        public string col_Cement2_Target = "CEM2";             //public string col_Cement2_Target = "0";
        public string col_Cement3_Target = "CEM3";             //public string col_Cement3_Target = "0";
        public string col_Cement4_Target = "CEM4";             //public string col_Cement4_Target = "0";
        public string col_Filler_Target = "FILLER";              //public string col_Filler_Target = "0";
        public string col_Water1_Target = "WATER1";              //public string col_Water1_Target = "0";
        public string col_slurry_Target = "SLURRY";              //public string col_slurry_Target = "0";
        public string col_Water2_Target = "WATER2";              //public string col_Water2_Target = "0";
        public string col_Silica_Target = "SILICA";              //public string col_Silica_Target = "0";
        public string col_Adm1_Target1 = "ADMIX1";               //public string col_Adm1_Target1 = "0";
        public string col_Adm1_Target2 = "ADMIX2";               //public string col_Adm1_Target2 = "0";
        public string col_Adm2_Target1 = "ADMIX3";               //public string col_Adm2_Target1 = "0";
        public string col_Adm2_Target2 = "ADMIX4";               //public string col_Adm2_Target2 = "0";

        public string col_Cost_Per_Mtr_Cube = "0";
        public string col_Total_Cost = "0";
        public string col_Plant_No = "0";
        public string col_Weighed_Net_Weight = "0";
        public string col_Weigh_Bridge_Stat = "0";

        public string col_Consistancy = "0";
        public string col_Gate1_Actual = "0";
        public string col_Gate2_Actual = "0";
        public string col_Gate3_Actual = "0";
        public string col_Gate4_Actual = "0";
        public string col_Gate5_Actual = "0";
        public string col_Gate6_Actual = "0";
        public string col_Cement1_Actual = "0";
        public string col_Cement2_Actual = "0";
        public string col_Cement3_Actual = "0";
        public string col_Cement4_Actual = "0";
        public string col_Filler_Actual = "0";
        public string col_Water1_Actual = "0";
        public string col_slurry_Actual = "0";
        public string col_Water2_Actual = "0";
        public string col_Silica_Actual = "0";
        public string col_Adm1_Actual1 = "0";
        public string col_Adm1_Actual2 = "0";
        public string col_Adm2_Actual1 = "0";
        public string col_Adm2_Actual2 = "0";
        public string col_Gate1_Moisture = "0";
        public string col_Gate2_Moisture = "0";
        public string col_Gate3_Moisture = "0";
        public string col_Gate4_Moisture = "0";
        public string col_Gate5_Moisture = "0";
        public string col_Gate6_Moisture = "0";
        public string col_Cement1_Correction = "0";
        public string col_Cement2_Correction = "0";
        public string col_Cement3_Correction = "0";
        public string col_Cement4_Correction = "0";
        public string col_Filler_Correction = "0";
        public string col_Water1_Correction = "0";
        public string col_slurry_Correction = "0";
        public string col_Water2_Correction = "0";
        public string col_Silica_Correction = "0";
        public string col_Adm1_Correction1 = "0";
        public string col_Adm1_Correction2 = "0";
        public string col_Adm2_Correction1 = "0";
        public string col_Adm2_Correction2 = "0";
        public string col_Pigment_Actual = "0";
        public string col_Pigment_Target = "0";
        public string col_Balance_Wtr = "0";
        public string col_SubJobCode = "0";

        //------------------------

        public double Gate1_nom = 0;
        public double Gate2_nom = 0;
        public double Gate3_nom = 0;
        public double Gate4_nom = 0;
        public double Gate5_nom = 0;
        public double Gate6_nom = 0;
        public double Cement1_nom = 0;
        public double Cement2_nom = 0;
        public double Cement3_nom = 0;
        public double Cement4_nom = 0;
        public double Filler_nom = 0;
        public double Water1_nom = 0;
        public double slurry_nom = 0;
        public double Water2_nom = 0;
        public double Silica_nom = 0;
        public double Adm11_nom = 0;
        public double Adm12_nom = 0;
        public double Adm21_nom = 0;
        public double Adm22_nom = 0;


        //------------ 21/03/2024 : BhaveshT ------------
        //- Added variables for errorPercent, default errPEr set as told by Vinay sir ------------

        public double Gate1_errPer = 3;
        public double Gate2_errPer = 3;
        public double Gate3_errPer = 3;
        public double Gate4_errPer = 3;
        public double Gate5_errPer = 3;
        public double Gate6_errPer = 3;
        public double Cement1_errPer = 1;
        public double Cement2_errPer = 1;
        public double Cement3_errPer = 1;
        public double Cement4_errPer = 1;
        public double Filler_errPer = 1;
        public double Water1_errPer = 1;
        public double slurry_errPer = 1;
        public double Water2_errPer = 1;
        public double Silica_errPer = 1;
        public double Adm11_errPer = 1;
        public double Adm12_errPer = 1;
        public double Adm21_errPer = 1;
        public double Adm22_errPer = 1;
        public double WtrCorr_nom = 0;

        //-------------For Index ----------- 24/06/2024 : BhaveshT

        public string Gate1_Index = "0";
        public string Gate2_Index = "0";
        public string Gate3_Index = "0";
        public string Gate4_Index = "0";
        public string Gate5_Index = "0";
        public string Gate6_Index = "0";

        public string Cement1_Index = "0";
        public string Cement2_Index = "0";
        public string Cement3_Index = "0";
        public string Cement4_Index = "0";

        public string Filler_Index = "0";
        public string Water1_Index = "0";
        public string slurry_Index = "0";
        public string Water2_Index = "0";
        public string Silica_Index = "0";

        public string Adm11_Index = "0";
        public string Adm12_Index = "0";
        public string Adm21_Index = "0";
        public string Adm22_Index = "0";

        //------------------------

        public static double _Gate1_Target = 0;
        public static double _Gate2_Target = 0;
        public static double _Gate3_Target = 0;
        public static double _Gate4_Target = 0;
        public static double _Cement1_Target = 0;
        public static double _Filler_Target = 0;
        public static double _Water1_Target = 0;
        public static double _Adm1_Target1 = 0;

        //------------------------

        //-------------For Bitumen Column Names ----------- 14/02/2025 : BhaveshT

        public string col_Exhaust_Temp = "0";
        public string col_Mix_Temp = "0";
        public string col_Tank1_Temp = "0";
        public string col_Tank2_Temp = "0";

        //------------------------

        //------------------------

        //------------------------
    }
}
