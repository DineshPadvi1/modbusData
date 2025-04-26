using System;
using System.Data;
using System.IO;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 
16/01/2024 - BhaveshT

This class contains methods

1.  public static void CalculateActualNominalErrorPercent(DataGridView dgvbatchdetails, clsVariables clsVar, string prodQty, string batchSize)
2.  public static void CalculateActualNominal(DataGridView dgvbatchdetails, clsVariables clsVar, string prodQty, string batchSize, bool calcualte_prod_qty)
3.  public static bool SendSMS(string projectName, string mobileNo, string smsText) 
4.  public static DataTable GetErrorPerToCompare(string dbtablename)

 */

namespace Uniproject.Classes.RMC
{


    public class clsErrorPercent_Calculation
    {
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public string Agg1ErrorPer = "";
        public string Agg2ErrorPer = "";
        public string Agg3ErrorPer = "";
        public string Agg4ErrorPer = "";
        public string Agg5ErrorPer = "";
        public string Agg6ErrorPer = "";

        public string Cem1ErrorPer = "";
        public string Cem2ErrorPer = "";
        public string Cem3ErrorPer = "";
        public string Cem4ErrorPer = "";        

        public string Wtr1ErrorPer = "";
        public string Wtr2ErrorPer = "";

        public string Adm1ErrorPer = "";
        public string Adm2ErrorPer = "";
        public string Adm3ErrorPer = "";

        public string SlurryErrorPer = "";
        public string SilicaErrorPer = "";
        public string IceErrorPer    = "";
        public string FillerErrorPer = "";

        public string Production_Error = "N";
        public string Production_SMS = "N";

        public double maxErrorPer = 0;

        public DataTable Preset_dt = null;

        public clsAlertSound alertSound;

        
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public void CalculateActualNominalErrorPercent(DataGridView dgvbatchdetails, clsVariables clsVar, string prodQty, string batchSize)
        {
            Production_Error = "N";

            //alertSound = new clsAlertSound();

            try
            {
                maxErrorPer = FindMaxErrorPer(Preset_dt);

                CalculateActualNominal(dgvbatchdetails, clsVar, prodQty, batchSize, false); // Call the existing method to calculate actual and nominal values

                if (dgvbatchdetails.Rows.Count >= 3)
                {
                    double result = Convert.ToDouble(prodQty);

                    // Calculate error percentage for each component
                    for (int i = 0; i < dgvbatchdetails.Columns.Count; i++)
                    {
                        //------------------------------------ Calculating ERROR % VALUES --------------------------------------------

                        if (dgvbatchdetails.Columns[i].Name.EndsWith("Name") || dgvbatchdetails.Columns[i].Name.Contains("Actual") && dgvbatchdetails.Columns[i] != null)
                        {
                            double actualValue = Convert.ToDouble(dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 3].Cells[i].Value);
                            double nominalValue = Convert.ToDouble(dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells[i].Value);

                            // Calculate and update error percentage
                            double errorPercent = ((actualValue - nominalValue) / nominalValue) * 100;

                            errorPercent = Math.Round(errorPercent, 2);

                            // if nominal value is 0, then to avoid errorPercentage become Infinity (NaN), it will make it 0
                            if (nominalValue == 0)
                            {
                                errorPercent = 0;
                            }

                            dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 1].Cells[i].Value = Math.Round(errorPercent, 2);

                            //maxErrorPer = FindMaxErrorPer(Preset_dt);
                            
                            if(errorPercent < maxErrorPer)
                            {
                                Production_Error = "Y";
                                //alertSound.PlayAlertSound();
                                //SystemSounds.Asterisk.Play();
                                

                                //return;
                            }
                            if (errorPercent > maxErrorPer)
                            {
                                //Production_Error = "N";
                                
                            }

                            //Production_Error = "";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at CalculateActualNominalErrorPercent() : " + ex.Message);
                clsFunctions_comman.ErrorLog("Exception at CalculateActualNominalErrorPercent() : " + ex.Message);
            }


        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void CalculateErrorPercent(DataGridView dgvbatchdetails, clsVariables clsVar, string prodQty, string batchSize)
        {
            try
            {

                //alertSound = new clsAlertSound();


                maxErrorPer = FindMaxErrorPer(Preset_dt);

                //CalculateActualNominal(dgvbatchdetails, clsVar, prodQty, batchSize, false); // Call the existing method to calculate actual and nominal values

                if (dgvbatchdetails.Rows.Count >= 3)
                {
                    double result = Convert.ToDouble(prodQty);

                    // Calculate error percentage for each component
                    for (int i = 0; i < dgvbatchdetails.Columns.Count-1; i++)
                    {
                        double actualValue = 0, nominalValue = 0;
                        //------------------------------------ Calculating ERROR % VALUES --------------------------------------------

                        if ( dgvbatchdetails.Columns[i].Name.EndsWith("Name") || dgvbatchdetails.Columns[i].Name.Contains("Actual") || dgvbatchdetails.Columns[i].Name.Contains("Actual1") || dgvbatchdetails.Columns[i].Name.Contains("Actual") )
                        {
                            if((dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells[i].Value) == null)
                            {

                            }
                            else
                            {
                                try { actualValue = Convert.ToDouble(dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 3].Cells[i].Value); }   
                                catch{ actualValue =  0; }   
                                try { nominalValue = Convert.ToDouble(dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells[i].Value); }  
                                catch{ nominalValue = 0; }   

                                // Calculate and update error percentage
                                double errorPercent = ((actualValue - nominalValue) / nominalValue) * 100;

                                errorPercent = Math.Round(errorPercent, 2);

                                // if nominal value is 0, then to avoid errorPercentage become Infinity (NaN), it will make it 0
                                if (nominalValue == 0)
                                {
                                    errorPercent = 0;
                                }

                                dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 1].Cells[i].Value = Math.Round(errorPercent, 2);

                                //maxErrorPer = FindMaxErrorPer(Preset_dt);

                                if (errorPercent < maxErrorPer)
                                {
                                    Production_Error = "Y";
                                    //alertSound.PlayAlertSound();
                                    //SystemSounds.Asterisk.Play();
                                    SystemSounds.Exclamation.Play();
                                    //SystemSounds.Hand.Play();
                                    //SystemSounds.Question.Play();

                                    //return;
                                }
                                if (errorPercent > maxErrorPer)
                                {
                                    //Production_Error = "N";

                                }

                                //Production_Error = "";
                            }



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at CalculateActualNominalErrorPercent() : " + ex.Message);
                clsFunctions_comman.ErrorLog("Exception at CalculateActualNominalErrorPercent() : " + ex.Message);
            }


        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // 17/01/2024 - BhaveshT
        // Converted simple method to class method to make it accesible from multiple Forms
        public static void CalculateActualNominal(DataGridView dgvbatchdetails, clsVariables clsVar, string prodQty, string batchSize, bool calcualte_prod_qty)
        {
            try 
            {
                clsVar.Gate1_sum = 0; clsVar.Gate2_sum = 0; clsVar.Gate3_sum = 0; clsVar.Gate4_sum = 0; clsVar.Gate5_sum = 0; clsVar.Gate6_sum = 0; clsVar.Cement1_sum = 0;
                clsVar.Cement2_sum = 0; clsVar.Cement3_sum = 0; clsVar.Cement4_sum = 0; clsVar.Filler_sum = 0; clsVar.Water1_sum = 0; clsVar.slurry_sum = 0;
                clsVar.Water2_sum = 0; clsVar.Silica_sum = 0; clsVar.Adm11_sum = 0; clsVar.Adm12_sum = 0; clsVar.Adm21_sum = 0; clsVar.Adm22_sum = 0;
                if (dgvbatchdetails.Rows.Count >= 2)
                {
                    foreach (DataGridViewRow row in dgvbatchdetails.Rows)
                    {
                        if (row.Cells["batchtime"].Value.ToString() == "ACTUAL") break;
                        try{clsVar.Gate1_sum = clsVar.Gate1_sum + Convert.ToDouble(row.Cells["Gate1Name"].Value);   }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Gate2_sum = clsVar.Gate2_sum + Convert.ToDouble(row.Cells["Gate2Name"].Value);   }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Gate3_sum = clsVar.Gate3_sum + Convert.ToDouble(row.Cells["Gate3Name"].Value);   }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Gate4_sum = clsVar.Gate4_sum + Convert.ToDouble(row.Cells["Gate4Name"].Value);   }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Gate5_sum = clsVar.Gate5_sum + Convert.ToDouble(row.Cells["Gate5Name"].Value);   }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Gate6_sum = clsVar.Gate6_sum + Convert.ToDouble(row.Cells["Gate6Name"].Value);   }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Cement1_sum = clsVar.Cement1_sum + Convert.ToDouble(row.Cells["Cem1Name"].Value);}catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Cement2_sum = clsVar.Cement2_sum + Convert.ToDouble(row.Cells["Cem2Name"].Value);}catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Cement3_sum = clsVar.Cement3_sum + Convert.ToDouble(row.Cells["Cem3Name"].Value);}catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Cement4_sum = clsVar.Cement4_sum + Convert.ToDouble(row.Cells["Cem4Name"].Value);}catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Filler_sum = clsVar.Filler_sum + Convert.ToDouble(row.Cells["FillName"].Value);  }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Water1_sum = clsVar.Water1_sum + Convert.ToDouble(row.Cells["Wtr1Name"].Value);  }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Water2_sum = clsVar.Water2_sum + Convert.ToDouble(row.Cells["wtr2Name"].Value);  }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Silica_sum = clsVar.Silica_sum + Convert.ToDouble(row.Cells["SilicaName"].Value);}catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Adm11_sum = clsVar.Adm11_sum + Convert.ToDouble(row.Cells["Admix1Name"].Value);  }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Adm12_sum = clsVar.Adm12_sum + Convert.ToDouble(row.Cells["Admix12Name"].Value); }catch (Exception ex)  {clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);}
                        try{clsVar.Adm21_sum = clsVar.Adm21_sum + Convert.ToDouble(row.Cells["Admix2Name"].Value);  }catch (Exception ex)  { clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message); }
                        try{clsVar.Adm22_sum = clsVar.Adm22_sum + Convert.ToDouble(row.Cells["Admix22Name"].Value); } catch (Exception ex) { clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message); }
                        try { clsVar.slurry_sum = clsVar.slurry_sum + Convert.ToDouble(row.Cells["SlurryName"].Value); } catch (Exception ex) { clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message); }

                        try { clsVar.WtrCorr_sum = clsVar.WtrCorr_sum + Convert.ToDouble(row.Cells["WtrCorr"].Value); } 
                        catch (Exception ex) 
                        { 
                            //clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message); 
                        }
                    }

                    //------------------------------------ Calculating ACTUAL VALUES --------------------------------------------

                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Gate1Name"].Value   = clsVar.Gate1_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Gate2Name"].Value   = clsVar.Gate2_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Gate3Name"].Value   = clsVar.Gate3_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Gate4Name"].Value   = clsVar.Gate4_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Gate5Name"].Value   = clsVar.Gate5_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Gate6Name"].Value   = clsVar.Gate6_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Cem1Name"].Value    = clsVar.Cement1_sum; }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Cem2Name"].Value    = clsVar.Cement2_sum; }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Cem3Name"].Value    = clsVar.Cement3_sum; }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Cem4Name"].Value    = clsVar.Cement4_sum; }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["FillName"].Value    = clsVar.Filler_sum;  }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Wtr1Name"].Value    = clsVar.Water1_sum;  }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["wtr2Name"].Value    = clsVar.Water2_sum;  }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["SilicaName"].Value  = clsVar.Silica_sum;  }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Admix1Name"].Value  = clsVar.Adm11_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Admix12Name"].Value = clsVar.Adm12_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Admix2Name"].Value  = clsVar.Adm21_sum;   }catch{}
                    try{dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["Admix22Name"].Value = clsVar.Adm22_sum;   }catch{}
                    try { dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["SlurryName"].Value = clsVar.slurry_sum; } catch { }

                    try { dgvbatchdetails.Rows[dgvbatchdetails.Rows.Count - 3].Cells["WtrCorr"].Value = clsVar.WtrCorr_sum; } catch { }

                    double result = Convert.ToDouble(prodQty);
                    if (calcualte_prod_qty)
                    {
                        result = Math.Round(Convert.ToDouble(batchSize) * (dgvbatchdetails.RowCount - 2f), 2);
                        prodQty = result.ToString();
                    }

                    //------------------------------------ Calculating NOMINAL VALUES --------------------------------------------

                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Gate1Name"].Value = Math.Round((clsVar.Gate1_nom * result), 0);  }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Gate2Name"].Value = Math.Round((clsVar.Gate2_nom * result), 0);  }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Gate3Name"].Value = Math.Round((clsVar.Gate3_nom * result), 0);  }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Gate4Name"].Value = Math.Round((clsVar.Gate4_nom * result), 0);  }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Gate5Name"].Value = Math.Round((clsVar.Gate5_nom * result), 0);  }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Gate6Name"].Value = Math.Round((clsVar.Gate6_nom * result), 0);  }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Cem1Name"].Value = Math.Round((clsVar.Cement1_nom * result), 0); }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Cem2Name"].Value = Math.Round((clsVar.Cement2_nom * result), 0); }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Cem3Name"].Value = Math.Round((clsVar.Cement3_nom * result), 0); }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Cem4Name"].Value = Math.Round((clsVar.Cement4_nom * result), 0); }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["FillName"].Value = Math.Round((clsVar.Filler_nom * result), 0);  }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Wtr1Name"].Value = Math.Round((clsVar.Water1_nom * result), 0);  }catch { try { dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Water"].Value = Math.Round((clsVar.Water1_nom * result), 0); } catch { } }
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["wtr2Name"].Value = Math.Round((clsVar.Water2_nom * result), 0);  }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["SilicaName"].Value = Math.Round((clsVar.Silica_nom * result), 0);}catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Admix1Name"].Value = Math.Round((clsVar.Adm11_nom * result), 2); }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Admix12Name"].Value = Math.Round((clsVar.Adm12_nom * result), 2);}catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Admix2Name"].Value = Math.Round((clsVar.Adm21_nom * result), 2); }catch{}
                   try{dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["Admix22Name"].Value = Math.Round((clsVar.Adm22_nom * result), 2);}catch{}
                   try { dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["SlurryName"].Value = Math.Round((clsVar.slurry_nom * result), 0); } catch { }

                   try { dgvbatchdetails.Rows[dgvbatchdetails.RowCount - 2].Cells["WtrCorr"].Value = Math.Round((clsVar.WtrCorr_nom * result), 0); } catch { }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.UniBox("Exception at clsErr.CalculateActualNominal() : " + ex.Message);

                if(!ex.Message.Contains("WtrCorr cannot be found"))
                {
                    clsFunctions_comman.ErrorLog("Exception at clsErr.CalculateActualNominal() : " + ex.Message);
                }
            }

        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static async Task<bool> SendSMS(string projectName, string BatchNo, string Date, string mobileNo, string smsText)
        {
            try
            {
                if(smsText == "" || mobileNo == "" | Date == "" | BatchNo == "")
                {
                    clsFunctions_comman.ErrorLog("One of required parameter is empty to send Error Alert SMS : " + Date + "," + BatchNo + "," + mobileNo + "," + smsText );
                    return false;
                }

                //-------------------------
                string URL = "pmcscada.in";             // for SMS sending for all dept
                string apiName = "";
                //URL = clsFunctions.GetAPINameFromServerMapping("ipaddress", clsFunctions.aliasName);
                apiName = clsFunctions.GetAPINameFromServerMapping("sendSMS", clsFunctions.aliasName);

                string url = "" + URL + "" + apiName;
                //-----------------------

                // string url = "http://192.168.1.64:8080/SMS_Utility/saveData";
                //string url = "http://pmcscada.in/SMS_Utility/saveData";
                //url = "http://192.168.1.13:8089/SMS_Utility/saveData";

                string smsMessage = smsText.Replace("@batchno", BatchNo);
                smsMessage = smsMessage.Replace("@datetime", Date);

                //preparing for the parameters that needs to be sent along with apiURL
                var parameters = $"projectName={Uri.EscapeDataString(projectName)}&mobileNo={Uri.EscapeDataString(mobileNo)}&message={Uri.EscapeDataString(smsMessage)}";

                Uri apiUrl = new Uri($"{url}?{parameters}");
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
                        clsFunctions_comman.AlertSMSLog("Alert SMS sent successfully : " + smsMessage);
                        string output = reader.ReadToEnd();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {   
                clsFunctions.ErrorLog("clsErrorPercent_Calculation : SendSMS() - " + ex.Message);
                return false;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void GetErrorPerToCompare(string dbtablename)
        {
            try
            {


                string PresetData_Value = "";

                Preset_dt = clsFunctions_comman.fillDatatable("Select dbtable_name, dbcolumn_name, presetdata_value from Preset_Data where dbtable_name = '"+ dbtablename + "' ");

                if (Preset_dt.Rows.Count != 0)
                {
                    DataRow row1 = Preset_dt.Rows[0];

                    string temp = Preset_dt.Rows[0][2].ToString();

                    Agg1ErrorPer = Preset_dt.Rows[0][2].ToString();        //row1["agg1_error_percent"].ToString();
                    Agg2ErrorPer = Preset_dt.Rows[1][2].ToString();        //row1["agg2_error_percent"].ToString();
                    Agg3ErrorPer = Preset_dt.Rows[2][2].ToString();        //row1["agg3_error_percent"].ToString();
                    Agg4ErrorPer = Preset_dt.Rows[3][2].ToString();        //row1["agg4_error_percent"].ToString();
                    Agg5ErrorPer = Preset_dt.Rows[4][2].ToString();        //row1["agg5_error_percent"].ToString();
                    Agg6ErrorPer = Preset_dt.Rows[5][2].ToString();        //row1["agg6_error_percent"].ToString();

                    Cem1ErrorPer = Preset_dt.Rows[6][2].ToString();        //row1["cem1_error_percent"].ToString();
                    Cem2ErrorPer = Preset_dt.Rows[7][2].ToString();        //row1["cem2_error_percent"].ToString();
                    Cem3ErrorPer = Preset_dt.Rows[8][2].ToString();        //row1["cem3_error_percent"].ToString();
                    Cem4ErrorPer = Preset_dt.Rows[9][2].ToString();        //row1["cem4_error_percent"].ToString();

                    Wtr1ErrorPer = Preset_dt.Rows[10][2].ToString();       //row1["water1_error_percent"].ToString();
                    Wtr2ErrorPer = Preset_dt.Rows[11][2].ToString();       //row1["water2_error_percent"].ToString();

                    Adm1ErrorPer = Preset_dt.Rows[12][2].ToString();       //row1["admix1_error_percent"].ToString();
                    Adm2ErrorPer = Preset_dt.Rows[13][2].ToString();       //row1["admix2_error_percent"].ToString();
                    Adm3ErrorPer = Preset_dt.Rows[14][2].ToString();       //row1["admix3_error_percent"].ToString();

                    SlurryErrorPer = Preset_dt.Rows[15][2].ToString();     //row1["slurry_error_percent"].ToString();
                    SilicaErrorPer = Preset_dt.Rows[16][2].ToString();     //row1["silica_error_percent"].ToString();
                    IceErrorPer = Preset_dt.Rows[17][2].ToString();        //row1["ice_error_percent"].ToString();

                    FillerErrorPer = Preset_dt.Rows[18][2].ToString();
                }
            }
            catch(Exception ex)
            {
                //clsFunctions_comman.UniBox("Exception at GetErrorPerToCompare() : " + ex.Message);
                clsFunctions_comman.ErrorLog("Exception at GetErrorPerToCompare() : " + ex.Message );
            }

        }



        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static double FindMaxErrorPer(DataTable dt)
        {
            double maxError = double.MinValue;

            foreach (DataRow row in dt.Rows)
            {
                double value;
                if (double.TryParse(row["presetdata_value"].ToString(), out value))
                {
                    //maxError = Math.Abs(value);

                    double error = Math.Abs(value);
                    //maxError = Math.Max(maxError, error);
                    maxError = Math.Max(maxError, value);

                    //double error = Math.Abs(value - errorPercent);
                    //maxError = Math.Max(maxError, error);
                }
            }

            return maxError;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public string RoundAndConvertToString(object value)
        {
            if (value != null && double.TryParse(value.ToString(), out double numericValue))
            {
                // Round to 2 decimal places
                double roundedValue = Math.Round(numericValue, 2);
                // Convert to string and return
                return roundedValue.ToString();
            }

            // If the conversion fails, return the original value as a string
            return value?.ToString() ?? string.Empty;
        }


        //------------------------------------------------------------------------------------------------------------------------------------------------------------------




        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
