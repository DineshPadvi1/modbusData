using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Uniproject.Classes
{
    public class clsBTdata
    {
        public static DataTable data;
        public static SqlConnection connSql;

        public static string selected_PLC_Name = clsFunctions.loadSingleValue("select Plc_name from PLC_Setting where Setflag='Y'");

        //clsVariables clsVar = new clsVariables();

        public clsBTdata(string linhoffquery)
        {
            try
            {   //connSql = new SqlConnection(clsFunctions.GetConnectionstrSetup_Path());           // 22/02/2024 - commented by BhaveshT

                connSql = new SqlConnection(clsFunctions.getConnectionString);


                // string con = clsFunctions.loadSingleValueSetup("SELECT DB FROM Connection_setup where DB_Type='SQL'");
                // clsSqlFunctions.setConString(con);
                data = FillDataTable(linhoffquery);

            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] clsBTdata - " + ex.Message + " | Query - " + clsFunctions.getConnectionString);
            }

        }
        public clsBTdata()
        {
            //data = (DataTable)dt;
        }

        public static DataTable batchdata = data;
        public static DataTable FillDataTable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                if (connSql.State == ConnectionState.Closed) connSql.Open();
                SqlCommand cmd = new SqlCommand(query, connSql);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                connSql.Close();
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] clsBTdata - FillDataTable: " + ex.Message + " | Query - " + query);
            }
            return dt;
        }


        public void AddBaseRows(DataGridView batchdgv)
        {
            batchdgv.Rows.Add("", "ACTUAL", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            batchdgv.Rows[batchdgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Brown;
            batchdgv.Rows.Add("", "NOMINAL", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            batchdgv.Rows[batchdgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Green;
        }

        private string checkclmname(DataGridViewColumn dgvclm)
        {
            if (dgvclm.Name.ToLower().Contains("f1") && dgvclm.Name.ToLower().Contains("kg") && !dgvclm.Name.ToLower().Contains("p"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "f1";
            }
            else if (dgvclm.Name.ToLower().Contains("f2") && dgvclm.Name.ToLower().Contains("kg") && !dgvclm.Name.ToLower().Contains("p"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "f2";
            }
            else if (dgvclm.Name.ToLower().Contains("f3") && dgvclm.Name.ToLower().Contains("kg") && !dgvclm.Name.ToLower().Contains("p"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "f3";
            }
            else if (dgvclm.Name.ToLower().Contains("f4") && dgvclm.Name.ToLower().Contains("kg") && !dgvclm.Name.ToLower().Contains("p"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "f4";
            }
            else if (dgvclm.Name.ToLower().Contains("fil") && dgvclm.Name.ToLower().Contains("kg") && !dgvclm.Name.ToLower().Contains("p"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "filler";
            }
            else if (dgvclm.Name.ToLower().Contains("bit") && dgvclm.Name.ToLower().Contains("kg") && !dgvclm.Name.ToLower().Contains("p"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "bitumen";
            }
            else if (dgvclm.Name.ToLower().Contains("mix") && dgvclm.Name.ToLower().Contains("temp"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "mixTemp";
            }
            else if (dgvclm.Name.ToLower().Contains("exh") && dgvclm.Name.ToLower().Contains("temp"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "exhausttemp";
            }
            else if (dgvclm.Name.ToLower().Contains("bit") && dgvclm.Name.ToLower().Contains("temp") || dgvclm.Name.ToLower().Contains("tank1") && dgvclm.Name.ToLower().Contains("temp"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "tank1temp";
            }
            else if (dgvclm.Name.ToLower().Contains("bit") && dgvclm.Name.ToLower().Contains("temp") || dgvclm.Name.ToLower().Contains("tank2") && dgvclm.Name.ToLower().Contains("temp"))
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = "tank2temp";
            }
            else
            {
                Columnindex = dgvclm.Name;
                return mtrl_Name = Columnindex;
            }
            //clsFunctions.ErrorLog("Column not found in gridview");
            //return "";
        }

        string Columnindex = "";
        string mtrl_Name = "";
        string errIndicator = "N";

        public string checkErrPer(string recipename, DataGridView dgv)
        {
            try
            {
                double presetdata_value = 1;
                double presetdata_value2 = 1;
                double f1nom, f2nom, f3nom, f4nom, fillernom, bitunom, mixtempfrom, mixtempto, exhaustfrom, exhaustto, tank1from, tank2from, tank1to, tank2to;
                double f1Act = 0, f2Act = 0, f3Act = 0, f4Act = 0, fillerAct = 0, bituAct = 0, mixtempact = 0, exhausttempact = 0, tank1act = 0, tank2act = 0;
                double f1diffper, f2diffper, f3diffper, f4diffper, fillerdiffper, bitudiffper, mixtempavg, exhaustavg, tank1avg, tank2avg;

                DataTable dt = clsFunctions_comman.fillDatatable("SELECT * FROM tblRecipeMaster where recipename = '" + recipename + "'");
                DataTable presetdt = clsFunctions_comman.fillDatatable("SELECT * FROM Preset_Data WHERE dbtable_name = 'tblRecipeMaster' ");

                if (dgv.Rows.Count <= 0)
                {
                    return "";
                }

                foreach (DataGridViewColumn dgvclm in dgv.Columns)
                {
                    if (dgvclm.Name.Contains("per"))
                        continue;

                    mtrl_Name = checkclmname(dgvclm);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow rcpRow = dt.Rows[0];
                        switch (mtrl_Name.ToLower())
                        {
                            case "f1":
                                f1nom = Convert.ToDouble(rcpRow["f1"]) * dgv.Rows.Count;
                                foreach (DataGridViewRow dr in dgv.Rows)
                                {
                                    f1Act += Convert.ToDouble(dr.Cells[Columnindex].Value);
                                }
                                f1diffper = (f1Act * 100) / f1nom - 100;
                                DataRow[] filteredRows = presetdt.Select("dbcolumn_name = 'f1'");
                                if (filteredRows.Length > 0)
                                {
                                    presetdata_value = Convert.ToDouble(filteredRows[0]["presetdata_value"].ToString());
                                }
                                if (presetdata_value > f1diffper && f1diffper != 0) return errIndicator = "Y";
                                break;

                            case "f2":
                                f2nom = Convert.ToDouble(rcpRow["f2"]) * dgv.Rows.Count;
                                foreach (DataGridViewRow dr in dgv.Rows)
                                {
                                    f2Act += Convert.ToDouble(dr.Cells[Columnindex].Value);
                                }
                                f2diffper = (f2Act * 100) / f2nom - 100;
                                filteredRows = presetdt.Select("dbcolumn_name = 'f2'");
                                if (filteredRows.Length > 0)
                                {
                                    presetdata_value = Convert.ToDouble(filteredRows[0]["presetdata_value"].ToString());
                                }
                                if (presetdata_value > f2diffper && f2diffper != 0) return errIndicator = "Y";
                                break;

                            case "f3":
                                f3nom = Convert.ToDouble(rcpRow["f3"]) * dgv.Rows.Count;
                                foreach (DataGridViewRow dr in dgv.Rows)
                                {
                                    f3Act += Convert.ToDouble(dr.Cells[Columnindex].Value);
                                }
                                f3diffper = (f3Act * 100) / f3nom - 100;
                                filteredRows = presetdt.Select("dbcolumn_name = 'f3'");
                                if (filteredRows.Length > 0)
                                {
                                    presetdata_value = Convert.ToDouble(filteredRows[0]["presetdata_value"].ToString());
                                }
                                if (presetdata_value > f3diffper && f3diffper != 0)
                                {
                                    return errIndicator = "Y";
                                }
                                break;

                            case "f4":
                                f4nom = Convert.ToDouble(rcpRow["f4"]) * dgv.Rows.Count;
                                foreach (DataGridViewRow dr in dgv.Rows)
                                {
                                    f4Act += Convert.ToDouble(dr.Cells[Columnindex].Value);
                                }
                                f4diffper = (f4Act * 100) / f4nom - 100;
                                filteredRows = presetdt.Select("dbcolumn_name = 'f4'");
                                if (filteredRows.Length > 0)
                                {
                                    presetdata_value = Convert.ToDouble(filteredRows[0]["presetdata_value"].ToString());
                                }
                                if (presetdata_value > f4diffper && f4diffper != 0)
                                {
                                    return errIndicator = "Y";
                                }
                                break;

                            case "filler":
                                fillernom = Convert.ToDouble(rcpRow["filler"]) * dgv.Rows.Count;
                                foreach (DataGridViewRow dr in dgv.Rows)
                                {
                                    fillerAct += Convert.ToDouble(dr.Cells[Columnindex].Value);
                                }
                                fillerdiffper = (fillerAct * 100) / fillernom - 100;
                                filteredRows = presetdt.Select("dbcolumn_name = 'filler'");
                                if (filteredRows.Length > 0)
                                {
                                    presetdata_value = Convert.ToDouble(filteredRows[0]["presetdata_value"].ToString());
                                }
                                if (presetdata_value > fillerdiffper && fillerdiffper != 0)
                                {
                                    return errIndicator = "Y";
                                }
                                break;

                            case "bitumen":
                                bitunom = Convert.ToDouble(rcpRow["bitumen"]) * dgv.Rows.Count;
                                foreach (DataGridViewRow dr in dgv.Rows)
                                {
                                    bituAct += Convert.ToDouble(dr.Cells[Columnindex].Value);
                                }
                                bitudiffper = (bituAct * 100) / bitunom - 100;
                                filteredRows = presetdt.Select("dbcolumn_name = 'bituman'");
                                if (filteredRows.Length > 0)
                                {
                                    presetdata_value = Convert.ToDouble(filteredRows[0]["presetdata_value"].ToString());
                                }
                                if (presetdata_value > bitudiffper && bitudiffper != 0)
                                {
                                    return errIndicator = "Y";
                                }
                                break;

                            case "mixtemp":
                                // bitunom = Convert.ToDouble(rcpRow["bitumen"]) * dgv.Rows.Count;
                                foreach (DataGridViewRow dr in dgv.Rows)
                                {
                                    mixtempact += Convert.ToDouble(dr.Cells[Columnindex].Value);
                                }
                                // bitudiffper = (bituAct * 100) / bitunom - 100;

                                mixtempavg = mixtempact / dgv.Rows.Count;
                                filteredRows = presetdt.Select("dbcolumn_name = 'mix_temp_from'");
                                DataRow[] filteredRows2 = presetdt.Select("dbcolumn_name = 'mix_temp_to'");
                                if (filteredRows.Length > 0 && filteredRows2.Length > 0)
                                {
                                    presetdata_value = Convert.ToDouble(filteredRows[0]["presetdata_value"].ToString());
                                    presetdata_value2 = Convert.ToDouble(filteredRows2[0]["presetdata_value"].ToString());
                                }
                                if (presetdata_value > mixtempavg && presetdata_value2 < mixtempavg && mixtempavg != 0)
                                {
                                    return errIndicator = "Y";
                                }
                                break;

                            case "exhausttemp":
                                foreach (DataGridViewRow dr in dgv.Rows)
                                {
                                    exhausttempact += Convert.ToDouble(dr.Cells[Columnindex].Value);
                                }
                                exhaustavg = exhausttempact / dgv.Rows.Count;
                                filteredRows = presetdt.Select("dbcolumn_name = 'mix_temp_from'");
                                filteredRows2 = presetdt.Select("dbcolumn_name = 'mix_temp_to'");
                                if (filteredRows.Length > 0 && filteredRows2.Length > 0)
                                {
                                    presetdata_value = Convert.ToDouble(filteredRows[0]["presetdata_value"].ToString());
                                    presetdata_value2 = Convert.ToDouble(filteredRows2[0]["presetdata_value"].ToString());
                                }
                                if (presetdata_value > exhaustavg && presetdata_value2 < exhaustavg && exhaustavg != 0)
                                {
                                    return errIndicator = "Y";
                                }
                                break;
                        }
                    }
                }
                return errIndicator;
            }
            catch (Exception ex)
            {
                return errIndicator;
            }
        }

        public void CalculateActaulNomianl(DataGridView batchdgv)
        {
            clsBTvar btvar = new clsBTvar();

            if (batchdgv.Rows.Count >= 2)
            {
                foreach (DataGridViewRow row in batchdgv.Rows)
                {
                    if (row.Cells[1].Value.ToString() == "ACTUAL") break;
                    //int ID = Convert.ToInt32(row.Cells[0].Value);
                    //string regno = Convert.ToString(row.Cells[1].Value);
                    //string plantcode = Convert.ToString(row.Cells[2].Value);
                    //double oprlat = Convert.ToDouble(row.Cells[3].Value);
                    //double oprlong = Convert.ToDouble(row.Cells[4].Value);
                    //string oprjurisdiction = Convert.ToString(row.Cells[5].Value);
                    //string oprdivision = Convert.ToString(row.Cells[6].Value);
                    //string oprworkname = Convert.ToString(row.Cells[7].Value);
                    //DateTime tdate = Convert.ToDateTime(row.Cells[8].Value);
                    //TimeSpan ttime = TimeSpan.Parse(Convert.ToString(row.Cells[9].Value));
                    btvar.AggregateTPH_Sum = btvar.AggregateTPH + Convert.ToDouble(row.Cells[2].Value);            /*Cyclesum;*/
                    btvar.BitumenKgMinSum = btvar.BitumenKgMin + Convert.ToDouble(row.Cells[3].Value);                /*weightsum;*/
                    btvar.FillerKgMinSum = btvar.FillerKgMin + Convert.ToDouble(row.Cells[4].Value);                 /*f1persum;*/
                    btvar.BitumenPerSum = btvar.BitumenPer + Convert.ToDouble(row.Cells[5].Value);                     /*f2persum;*/
                    btvar.FillerPerSum = btvar.FillerPer + Convert.ToDouble(row.Cells[6].Value);                        /*f3persum;*/
                    btvar.F1_InPerSum = btvar.F1_InPer + Convert.ToDouble(row.Cells[7].Value);                         /*f4persum;*/
                    btvar.F2_InPerSum = btvar.F2_InPer + Convert.ToDouble(row.Cells[8].Value);                         /*f1kgsum;*/
                    btvar.F3_InPerSum = btvar.F3_InPer + Convert.ToDouble(row.Cells[9].Value);                         /*f2kgsum;*/
                    btvar.F4_InPerSum = btvar.F4_InPer + Convert.ToDouble(row.Cells[10].Value);                        /* f3kg_sum;*/
                    btvar.MoistureSum = btvar.Moisture + Convert.ToDouble(row.Cells[19].Value);
                    btvar.MixTempSum = btvar.MixTemp + Convert.ToDouble(row.Cells[20].Value);
                    btvar.ExhaustTempSum = btvar.ExhaustTemp + Convert.ToDouble(row.Cells[21].Value);
                    btvar.Tank1Sum = btvar.Tank1 + Convert.ToDouble(row.Cells[22].Value);
                    btvar.Tank2Sum = btvar.Tank2 + Convert.ToDouble(row.Cells[23].Value);
                    btvar.AggregateTonSum = btvar.AggregateTon + Convert.ToDouble(row.Cells[25].Value);
                    btvar.BitumenKgSum = btvar.BitumenKg + Convert.ToDouble(row.Cells[26].Value);
                    btvar.FillerKgSum = btvar.FillerKg + Convert.ToDouble(row.Cells[27].Value);
                    btvar.NetMixSum = btvar.NetMix + Convert.ToDouble(row.Cells[28].Value);
                    btvar.BatchDurationInSSum = btvar.BatchDurationInS + Convert.ToInt32(row.Cells[31].Value);
                    btvar.WeightKgPerBatchSum = btvar.WeightKgPerBatch + Convert.ToDouble(row.Cells[32].Value);
                    btvar.HB1_KgPerBatchSum = btvar.HB1_KgPerBatch + Convert.ToDouble(row.Cells[33].Value);
                    btvar.HB2_KgPerBatchSum = btvar.HB2_KgPerBatch + Convert.ToDouble(row.Cells[34].Value);
                    btvar.HB3_KgPerBatchSum = btvar.HB3_KgPerBatch + Convert.ToDouble(row.Cells[35].Value);
                    btvar.HB4_KgPerBatchSum = btvar.HB4_KgPerBatch + Convert.ToDouble(row.Cells[36].Value);
                    btvar.aggregate_KgSum = btvar.aggregate_Kg + Convert.ToDouble(row.Cells[37].Value);
                    //int truck_Count         = Convert.ToInt32(row.Cells[38].Value);
                    //string imeino           = Convert.ToString(row.Cells[39].Value);
                    //string worktype         = Convert.ToString(row.Cells[40].Value);
                    //string workcode         = Convert.ToString(row.Cells[41].Value);
                    //string material         = Convert.ToString(row.Cells[42].Value);
                    //string exportstatus     = Convert.ToString(row.Cells[43].Value);
                    //string viplupload       = Convert.ToString(row.Cells[44].Value);
                    //string batchendflag     = Convert.ToString(row.Cells[45].Value);
                    //string loadno           = Convert.ToString(row.Cells[46].Value);
                    //string jobsite          = Convert.ToString(row.Cells[47].Value);
                    //string insertType       = Convert.ToString(row.Cells[48].Value);

                }

                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[2].Value = btvar.BatchDurationInSSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[3].Value = btvar.WeightKgPerBatchSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[4].Value = btvar.F1_InPerSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[5].Value = btvar.F2_InPerSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[6].Value = btvar.F3_InPerSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[7].Value = btvar.F4_InPerSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[8].Value = btvar.HB1_KgPerBatchSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[9].Value = btvar.HB2_KgPerBatchSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[10].Value = btvar.HB3_KgPerBatchSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[11].Value = btvar.HB4_KgPerBatchSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[12].Value = btvar.BitumenKgSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[13].Value = btvar.BitumenPerSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[14].Value = btvar.FillerKgSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[15].Value = btvar.FillerPerSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[16].Value = btvar.MixTempSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[17].Value = btvar.ExhaustTempSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[18].Value = btvar.Tank1Sum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[19].Value = btvar.Tank2Sum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[20].Value = btvar.aggregate_KgSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[21].Value = btvar.AggregateTPH_Sum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[22].Value = btvar.BitumenKgMinSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[23].Value = btvar.FillerKgMinSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[24].Value = btvar.MoistureSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[25].Value = btvar.AggregateTonSum;
                batchdgv.Rows[batchdgv.Rows.Count - 2].Cells[26].Value = btvar.NetMixSum;

                double result = (batchdgv.RowCount - 2f);// *Convert.ToDouble(txtbatchsize1.Text) * (batchdgv.RowCount - 2f);
                //result = Convert.ToDouble(txtprodqty1.Text);

                int rcnt = batchdgv.Rows.Count - 2;
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[2].Value = Math.Round((clsVar.Gate2_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[3].Value = Math.Round((clsVar.Gate3_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[4].Value = Math.Round((clsVar.Gate4_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[5].Value = Math.Round((clsVar.Gate5_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[6].Value = Math.Round((clsVar.Gate6_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[7].Value = Math.Round((clsVar.Cement1_Target * result), 2);                
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[8].Value = Math.Round((clsVar.Cement2_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[9].Value = Math.Round((clsVar.Cement3_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[10].Value = Math.Round((clsVar.Cement4_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[11].Value = Math.Round((clsVar.Filler_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[12].Value = Math.Round((clsVar.Water1_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[13].Value = Math.Round((clsVar.Water2_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[14].Value = Math.Round((clsVar.Silica_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[15].Value = Math.Round((clsVar.Adm1_Target1 * result), 3);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[16].Value = Math.Round((clsVar.Adm1_Target2 * result), 3);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[17].Value = Math.Round((clsVar.Adm2_Target1 * result), 3);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[18].Value = Math.Round((clsVar.Adm2_Target2 * result), 3);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[19].Value = Math.Round((clsVar.slurry_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[20].Value = Math.Round((clsVar.slurry_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[21].Value = Math.Round((clsVar.slurry_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[22].Value = Math.Round((clsVar.slurry_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[23].Value = Math.Round((clsVar.slurry_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[24].Value = Math.Round((clsVar.slurry_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[25].Value = Math.Round((clsVar.slurry_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[26].Value = Math.Round((clsVar.slurry_Target * result), 2);
                //batchdgv.Rows[batchdgv.RowCount - 1].Cells[27].Value = Math.Round((clsVar.slurry_Target * result), 2);


            }
        }


        //----------------- 27/05/2024 : BhaveshT - function to validate temperature entered in txtBox of BT-M module. ------------------------------

        //---------------------------------- Aggregate Temperature ---------------------------------        
        public static void validateAggregateTemperature(string BT_Grade, TextBox TemperatureBox)
        {
            try
            {
                if (BT_Grade == "VG-10")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 165 || Convert.ToInt32(TemperatureBox.Text) < 140)
                    {
                        MessageBox.Show("For VG-10 Grade: Enter Aggregate Temperature between 140-165");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-20")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 170 || Convert.ToInt32(TemperatureBox.Text) < 145)
                    {
                        MessageBox.Show("For VG-20 Grade: Enter Aggregate Temperature between 145-170");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-30")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 170 || Convert.ToInt32(TemperatureBox.Text) < 150)
                    {
                        MessageBox.Show("For VG-30 Grade: Enter Aggregate Temperature between 150-170");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-40")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 175 || Convert.ToInt32(TemperatureBox.Text) < 160)
                    {
                        MessageBox.Show("For VG-40 Grade: Enter Aggregate Temperature between 160-175");
                        TemperatureBox.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at clsBTdata.validateAggregateTemperature() - " + ex.Message);
            }
        }

        //---------------------------------- Mix Temperature ---------------------------------
        public static void validateMixTemperature(string BT_Grade, TextBox TemperatureBox)
        {
            try
            {
                if (BT_Grade == "VG-10")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 160 || Convert.ToInt32(TemperatureBox.Text) < 140)
                    {
                        MessageBox.Show("For VG-10 Grade: Enter Mix Temperature between 140-160");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-20")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 165 || Convert.ToInt32(TemperatureBox.Text) < 145)
                    {
                        MessageBox.Show("For VG-20 Grade: Enter Mix Temperature between 145-165");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-30")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 165 || Convert.ToInt32(TemperatureBox.Text) < 150)
                    {
                        MessageBox.Show("For VG-30 Grade: Enter Mix Temperature between 150-165");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-40")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 170 || Convert.ToInt32(TemperatureBox.Text) < 160)
                    {
                        MessageBox.Show("For VG-40 Grade: Enter Mix Temperature between 160-170");
                        TemperatureBox.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at clsBTdata.validateMixTemperature() - " + ex.Message);
            }
        }

        //---------------------------------- Asphalt Temperature ---------------------------------
        public static void validateAsphaltTemperature(string BT_Grade, TextBox TemperatureBox)
        {
            try
            {
                if (BT_Grade == "VG-10")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 160 || Convert.ToInt32(TemperatureBox.Text) < 140)
                    {
                        MessageBox.Show("For VG-10 Grade: Enter Asphalt Temperature between 140-160");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-20")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 165 || Convert.ToInt32(TemperatureBox.Text) < 145)
                    {
                        MessageBox.Show("For VG-20 Grade: Enter Asphalt Temperature between 145-165");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-30")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 165 || Convert.ToInt32(TemperatureBox.Text) < 150)
                    {
                        MessageBox.Show("For VG-30 Grade: Enter Asphalt Temperature between 150-165");
                        TemperatureBox.Text = "0";
                    }
                }
                //----------
                if (BT_Grade == "VG-40")
                {
                    if (Convert.ToInt32(TemperatureBox.Text) > 170 || Convert.ToInt32(TemperatureBox.Text) < 160)
                    {
                        MessageBox.Show("For VG-40 Grade: Enter Asphalt Temperature between 160-170");
                        TemperatureBox.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Exception at clsBTdata.validateAsphaltTemperature() - " + ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------

        // 23/04/2025 - This method validates minimum & maximum Temperature

        public static void ValidateTemperature(TextBox TempBox, Label messageLabel, int minTemp, int maxTemp)
        {
            try
            {
                //if (!TempBox.Text.IsNullOrEmpty() && TempBox.Text != "0")
                    if (!String.IsNullOrEmpty(TempBox.Text) && TempBox.Text != "0")
                    {
                    if (Convert.ToInt32(TempBox.Text) > maxTemp)
                    {
                        //UpdateTextBox(TempBox, "255");
                        //UpdateLabelText(messageLabel, "Temperature is very high");
                        UpdateLabelText(messageLabel, "HIGH");
                    }
                    else if (Convert.ToInt32(TempBox.Text) < minTemp)
                    {
                        //UpdateTextBox(TempBox, "255");
                        //UpdateLabelText(messageLabel, "Temperature is very low");
                        UpdateLabelText(messageLabel, "LOW");
                    }
                    else
                    {
                        UpdateLabelText(messageLabel, "");
                    }

                    if (selected_PLC_Name == "V-HMI1" && clsFunctions_comman.loadForm.Contains("PlantScada"))
                    {
                        if (messageLabel.Visible == true)
                            UpdateLabelVisibility(messageLabel, false);
                        else
                            UpdateLabelVisibility(messageLabel, true);
                    }
                }
                else
                {
                    //UpdateLabelText(messageLabel, "Invalid Temperature");
                    UpdateLabelText(messageLabel, "INVALID");
                }
            }
            catch
            {

            }
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------

        public static void UpdateLabelText(System.Windows.Forms.Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => label.Text = text));
            }
            else
            {
                label.Text = text;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------

        public static void UpdateLabelVisibility(System.Windows.Forms.Label label, bool text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => label.Visible = text));
            }
            else
            {
                label.Visible = text;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------------

    }
}
