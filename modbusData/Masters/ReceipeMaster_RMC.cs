using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Uniproject.Classes;

namespace Uniproject.RMC_forms.Masters
{
    public partial class ReceipeMaster_RMC : Form
    {
        private static readonly LoggerService _loggerService = new LoggerService();         // BhaveshT

        double agg1_A = 0, agg2_A = 0, agg3_A = 0, agg4_A = 0, agg5_A = 0, agg6_A = 0, cement1_A = 0, cement2_A = 0, cement3_A = 0, cement4_A = 0, filler1_A = 0, Water1_A = 0, Water2_A = 0, Admix1_A = 0, Admix2_A = 0, Admix3_A = 0, Admix4_A = 0;
        double agg1_B = 0, agg2_B = 0, agg3_B = 0, agg4_B = 0, agg5_B = 0, agg6_B = 0, cement1_B = 0, cement2_B = 0, cement3_B = 0, cement4_B = 0, filler1_B = 0, Water1_B = 0, Water2_B = 0, Admix1_B = 0, Admix2_B = 0, Admix3_B = 0, Admix4_B = 0;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrdered_Click(object sender, EventArgs e)
        {
            //OrderMaster order = new OrderMaster();
            //order.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearTextBox();
            cmbrecipecode.DropDownStyle = ComboBoxStyle.Simple;
        }

        double agg1_C = 0, agg2_C = 0, agg3_C = 0, agg4_C = 0, agg5_C = 0, agg6_C = 0, cement1_C = 0, cement2_C = 0, cement3_C = 0, cement4_C = 0, filler1_C = 0, Water1_C = 0, Water2_C = 0, Admix1_C = 0, Admix2_C = 0, Admix3_C = 0, Admix4_C = 0;

        public ReceipeMaster_RMC()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------

        private void DesignMaster_Load(object sender, EventArgs e)
        {

            lblinfo.Text = lblinfo.Text + Convert.ToChar(0179).ToString();

            loadparamter();
        }

        //-------------------------------------------------------------------------------------

        private void loadparamter()
        {
            clsFunctions_comman.FillCombo_D_ASIT("select Recipe_Code from Recipe_Master order by Recipe_Code", cmbrecipecode);
            var dt = clsFunctions_comman.fillDatatable("Select Gate1Name,Gate2Name,Gate3Name,Gate4Name,Gate5Name,Gate6Name,Cem1Name,Cem2Name,Cem3Name,Cem4Name,FillName,Wtr1Name,wtr2Name,Admix1Name,Admix12Name,Admix2Name,Admix22Name from NameSetUp Where tInfo = 'O' and Batch_No = 2");

            foreach (DataColumn col in dt.Columns)
            {
                if (dt.Rows[0][col].ToString().Trim() != "" && dt.Rows[0][col].ToString().Trim() != "-" && dt.Rows[0][col].ToString().Trim() != "0")
                    dgv1.Rows.Add(col.ColumnName, dt.Rows[0][col].ToString());
            }
            dgv1.Rows.Add("", "TOTAL :", "0", "0", "0");
            dgv1.Rows[dgv1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Green;
            dgv1.Rows[dgv1.Rows.Count - 1].ReadOnly = true;
        }

        //-------------------------------------------------------------------------------------

        private void DesignMaster_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.A && Keys.Control == e.Modifiers)
            //{
            //    if (cmbrecipecode.Text == "") btnAdd.PerformClick();
            //    loginpanel.Visible = true;
            //    txtpass.Focus();
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //RMCPlant obj = new RMCPlant();
            //obj.MoveReceipe();

            //MoveReceipe();

            //MessageBox.Show("Recipes Load Succesfully");
        }

        //-------------------------------------------------------------------------------------

      

        //-------------------------------------------------------------------------------------

        private void clearTextBox()
        {
            txtbatchsize.Text = "";
            cmbrecipecode.Text = "";
            txtRecipeName.Text = "";
            cmbrecipecode.SelectedItem = null;
            dgv1.Rows.Clear();
            dgv1.Columns["RecipeA"].Visible = false;
            dgv1.Columns["RecipeC"].Visible = false;
            loginpanel.Visible = false;
            btnAdd.Enabled = true;
            btnCommand.Text = "Save";
            loadparamter();
        }

        //-------------------------------------------------------------------------------------

        private void btncancel_Click(object sender, EventArgs e)
        {
            txtpass.Text = "";
            loginpanel.Visible = false;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtpass.Text == "restplan")
            {
                dgv1.Columns["RecipeA"].Visible = true;
                dgv1.Columns["RecipeC"].Visible = true;
                loginpanel.Visible = false;
                txtpass.Text = "";
            }
            else
            {
                txtpass.Text = "";
                MessageBox.Show("Invalid Password", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clearTextBox();
            cmbrecipecode.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void dgv1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (dgv1.CurrentCell.Value == null) dgv1.CurrentCell.Value = 0;
            else if (dgv1.CurrentCell.Value.ToString().Trim() == "") dgv1.CurrentCell.Value = 0;

            foreach (DataGridViewRow Row in dgv1.Rows)
            {
                Row.Cells["RecipeC"].Value = Convert.ToDouble(Row.Cells["RecipeB"].Value) - Convert.ToDouble(Row.Cells["RecipeA"].Value);
            }
            calculateTotal();
        }

        private void calculateTotal()
        {
            double SumofA = 0;
            double SumofB = 0;

            foreach (DataGridViewRow row in dgv1.Rows)
            {
                if (row.Cells["Paramater"].Value.ToString() != "TOTAL :")
                {
                    if (row.Cells["RecipeB"].Value != null)
                    {
                        SumofA += Convert.ToDouble(row.Cells["RecipeB"].Value.ToString());
                        //SumofB += Convert.ToDouble(row.Cells["RecipeB"].Value.ToString());
                    }
                }
                else
                {
                    row.Cells["RecipeB"].Value = SumofA;
                    //row.Cells["RecipeB"].Value = SumofB;

                    //if (SumofB >= SumofA) row.Cells["RecipeC"].Value = SumofB - SumofA;
                    //else row.Cells["RecipeC"].Value = SumofA - SumofB;
                }
            }
            ////////////////////////////////////////////////////////
            //DataGridViewRow targetRow = null;
            //foreach (DataGridViewRow row in dgv1.Rows)
            //{
            //    if (row.Cells["Paramater"].Value != null && row.Cells["Parameter"].Value.ToString() == "Total :")
            //    {
            //        targetRow = row;
            //        break;
            //    }
            //}
            //if (targetRow != null)
            //{
            //    targetRow.Cells["RecipeB"].Value = "Your New Value";
            //}

        }

        //-------------------------------------------------------------------------------------

        private void txtbatchsize_KeyPress(object sender, KeyPressEventArgs e)
        {
            Column1_KeyPress(sender, e);
        }

        private void dgv1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            TextBox tb = e.Control as TextBox;
            if (tb != null)
            {
                tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            if (cmbrecipecode.Text != "" && Convert.ToDouble(txtbatchsize.Text) != 0)
            {
                SaveRecipetodb(btnCommand.Text);
            }
            else
            {
                MessageBox.Show("Enter correct Recipe or BatchSize");
            }

            clearTextBox();
            cmbrecipecode.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        //-------------------------------------------------------------------------------------

        private void LoadRecipeFromdb(DataRow dtrow_A, DataRow dtrow_B, DataRow dtrow_C)
        {
            foreach (DataGridViewRow row in dgv1.Rows)
            {
                switch (row.Cells["Columnname"].Value.ToString())
                {
                    case "Gate1Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Gate1_Target"].ToString();
                       row.Cells["RecipeB"].Value = dtrow_B["Gate1_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Gate1_Target"].ToString();
                        break;
                    case "Gate2Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Gate2_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Gate2_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Gate2_Target"].ToString();
                        break;
                    case "Gate3Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Gate3_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Gate3_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Gate3_Target"].ToString();
                        break;
                    case "Gate4Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Gate4_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Gate4_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Gate4_Target"].ToString();
                        break;
                    case "Gate5Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Gate5_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Gate5_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Gate5_Target"].ToString();
                        break;
                    case "Gate6Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Gate6_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Gate6_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Gate6_Target"].ToString();
                        break;
                    case "Cem1Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Cement1_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Cement1_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Cement1_Target"].ToString();
                        break;
                    case "Cem2Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Cement2_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Cement2_Target"].ToString();
                       row.Cells["Recipec"].Value = dtrow_C["Cement2_Target"].ToString();
                        break;
                    case "Cem3Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Cement3_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Cement3_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Cement3_Target"].ToString();
                        break;
                    case "Cem4Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Cement4_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Cement4_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Cement4_Target"].ToString();
                        break;
                    case "FillName":
                        row.Cells["RecipeA"].Value = dtrow_A["Filler_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Filler_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Filler_Target"].ToString();
                        break;
                    case "Wtr1Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Water1_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Water1_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Water1_Target"].ToString();
                        break;
                    case "wtr2Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Water2_Target"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Water2_Target"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Water2_Target"].ToString();
                        break;
                    case "Admix1Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Adm1_Target1"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Adm1_Target1"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Adm1_Target1"].ToString();
                        break;
                    case "Admix12Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Adm1_Target2"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Adm1_Target2"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Adm1_Target2"].ToString();
                        break;
                    case "Admix2Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Adm2_Target1"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Adm2_Target1"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Adm2_Target1"].ToString();
                        break;
                    case "Admix22Name":
                        row.Cells["RecipeA"].Value = dtrow_A["Adm2_Target2"].ToString();
                        row.Cells["RecipeB"].Value = dtrow_B["Adm2_Target2"].ToString();
                        row.Cells["Recipec"].Value = dtrow_C["Adm2_Target2"].ToString();
                        break;
                }

            }
            txtbatchsize.Text = dtrow_A["Mixer_Capacity"].ToString();

        }

        //-------------------------------------------------------------------------------------

        private void SaveRecipetodb(string txt)
        {
            foreach (DataGridViewRow row in dgv1.Rows)
            {
                switch (row.Cells["Columnname"].Value.ToString())
                {
                    case "Gate1Name":
                        agg1_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        agg1_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        agg1_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Gate2Name":
                        agg2_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        agg2_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        agg2_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Gate3Name":
                        agg3_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        agg3_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        agg3_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Gate4Name":
                        agg4_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        agg4_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        agg4_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Gate5Name":
                        agg5_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        agg5_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        agg5_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Gate6Name":
                        agg6_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        agg6_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        agg6_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Cem1Name":
                        cement1_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        cement1_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        cement1_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Cem2Name":
                        cement2_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        cement2_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        cement2_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Cem3Name":
                        cement3_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        cement3_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        cement3_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Cem4Name":
                        cement4_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        cement4_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        cement4_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "FillName":
                        filler1_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        filler1_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        filler1_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Wtr1Name":
                        Water1_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        Water1_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        Water1_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "wtr2Name":
                        Water2_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        Water2_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        Water2_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Admix1Name":
                        Admix1_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        Admix1_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        Admix1_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Admix12Name":
                        Admix2_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        Admix2_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        Admix2_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Admix2Name":
                        Admix3_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        Admix3_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        Admix3_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                    case "Admix22Name":
                        Admix4_B = Convert.ToDouble(row.Cells["RecipeA"].Value);
                        Admix4_A = Convert.ToDouble(row.Cells["RecipeB"].Value);
                        Admix4_C = Convert.ToDouble(row.Cells["RecipeC"].Value);
                        break;
                }

            }

            //-------------------------------------------------------------------------------------

            try
            {

                if (btnCommand.Text == "Save")
                {
                    int j = clsFunctions_comman.Ado("insert into Recipe_Master (Gate1_Target,Gate2_Target,Gate3_Target,Gate4_Target,Gate5_Target,Gate6_Target,Cement1_Target,Cement2_Target,Cement3_Target,Cement4_Target,Filler_Target,Water1_Target,Water2_Target,Silica_Target,Adm1_Target1,Adm1_Target2,Adm2_Target1,Adm2_Target2,slurry_Target,Recipe_Name,Mixer_Capacity,Recipe_Code)" +
                         "Values('" + agg1_A + "','" + agg2_A + "','" + agg3_A + "','" + agg4_A + "','" + agg5_A + "','" + agg6_A + "','" + cement1_A + "','" + cement2_A + "','" + cement3_A + "','" + cement4_A + "','" + filler1_A + "','" + Water1_A + "','" + Water2_A + "','0','" + Admix1_A + "','" + Admix2_A + "','" + Admix3_A + "','" + Admix4_A + "','0','"
                                       + txtRecipeName.Text + "','" + txtbatchsize.Text + "','" + cmbrecipecode.Text + "')");

                    //int j = clsFunctions_comman.Ado("insert into Recipe_Master (Gate1_Target,Gate2_Target,Gate3_Target,Gate4_Target,Gate5_Target,Gate6_Target,Cement1_Target,Cement2_Target,Cement3_Target,Cement4_Target,Filler_Target,Water1_Target,Water2_Target,Silica_Target,Adm1_Target1,Adm1_Target2,Adm2_Target1,Adm2_Target2,slurry_Target,Recipe_Name,Mixer_Capacity,Recipe_Code)" +
                    //   "Values('" + agg1_B + "','" + agg2_B + "','" + agg3_B + "','" + agg4_B + "','" + agg5_B + "','" + agg6_B + "','" + cement1_B + "','" + cement2_B + "','" + cement3_B + "','" + cement4_B + "','" + filler1_B + "','" + Water1_B + "','" + Water2_B + "','0','" + Admix1_B + "','" + Admix2_B + "','" + Admix3_B + "','" + Admix4_B + "','0','"+ txtRecipeName.Text + "','" + txtbatchsize.Text + "','" + cmbrecipecode.Text + "')");

                }
                else
                {
                    int j = 0;

                    j = clsFunctions_comman.Ado("Update Recipe_Master Set Gate1_Target ='" + agg1_A + "',Gate2_Target='" + agg2_A + "',Gate3_Target='" + agg3_A + "',Gate4_Target='"
                                   + agg4_A + "',Gate5_Target='" + agg5_A + "',Gate6_Target='" + agg6_A + "',Cement1_Target='" + cement1_A + "',Cement2_Target='"
                                   + cement2_A + "',Cement3_Target='" + cement3_A + "',Cement4_Target='" + cement4_A + "',Filler_Target='" + filler1_A + "',Water1_Target='"
                                   + Water1_A + "',Water2_Target='" + Water2_A + "',Silica_Target='0',Adm1_Target1='" + Admix1_A + "',Adm1_Target2='"
                                   + Admix2_A + "',Adm2_Target1='" + Admix3_A + "',Adm2_Target2='" + Admix4_A + "',slurry_Target='0',Recipe_Name='"
                                   + txtRecipeName.Text + "',Mixer_Capacity=" + txtbatchsize.Text + " where Recipe_Code='" + cmbrecipecode.Text + "'");

                    //j = clsFunctions_comman.Ado("Update Recipe_Master Set Gate1_Target ='" + agg1_B + "',Gate2_Target='" + agg2_B + "',Gate3_Target='" + agg3_B + "',Gate4_Target='"
                    //           + agg4_B + "',Gate5_Target='" + agg5_B + "',Gate6_Target='" + agg6_B + "',Cement1_Target='" + cement1_B + "',Cement2_Target='"
                    //           + cement2_B + "',Cement3_Target='" + cement3_B + "',Cement4_Target='" + cement4_B + "',Filler_Target='" + filler1_B + "',Water1_Target='"
                    //           + Water1_B + "',Water2_Target='" + Water2_B + "',Silica_Target='0',Adm1_Target1='" + Admix1_B + "',Adm1_Target2='"
                    //           + Admix2_B + "',Adm2_Target1='" + Admix3_B + "',Adm2_Target2='" + Admix4_B + "',slurry_Target='0',Recipe_Name='"
                    //           + txtRecipeName.Text + "',Mixer_Capacity=" + txtbatchsize.Text + " where Recipe_Code='" + cmbrecipecode.Text + "'");

                    //j = clsFunctions_comman.Ado("Update Recipe_MasterDiff Set Gate1_Target ='" + agg1_C + "',Gate2_Target='" + agg2_C + "',Gate3_Target='" + agg3_C + "',Gate4_Target='"
                    //           + agg4_C + "',Gate5_Target='" + agg5_C + "',Gate6_Target='" + agg6_C + "',Cement1_Target='" + cement1_C + "',Cement2_Target='"
                    //           + cement2_C + "',Cement3_Target='" + cement3_C + "',Cement4_Target='" + cement4_C + "',Filler_Target='" + filler1_C + "',Water1_Target='"
                    //           + Water1_C + "',Water2_Target='" + Water2_C + "',Silica_Target='0',Adm1_Target1='" + Admix1_C + "',Adm1_Target2='"
                    //           + Admix2_C + "',Adm2_Target1='" + Admix3_C + "',Adm2_Target2='" + Admix4_C + "',slurry_Target='0',Recipe_Name='"
                    //           + txtRecipeName.Text + "',Mixer_Capacity=" + txtbatchsize.Text + " where Recipe_Code='" + cmbrecipecode.Text + "'");

                    MessageBox.Show("Recipe Update Successfully", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnclear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("Recipe Master :" + ex.Message);
                MessageBox.Show("Recipe Not Saved", "MSG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _loggerService.LogMessage(LogType.Error, ErrorType.Error, "RecipeMaster_RMC - SaveRecipeToDB() : " + ex.Message);     //BhaveshT
            }
        }

        //-------------------------------------------------------------------------------------

        private void cmbdesign_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbrecipecode.Text != "")
            {
                var dt_A = clsFunctions_comman.fillDatatable("Select * from Recipe_Master where Recipe_Code='" + cmbrecipecode.Text + "'");
              //  var dt_B = clsFunctions_comman.fillDatatable("Select * from Recipe_MasterB where Recipe_Code='" + cmbrecipecode.Text + "'");
              //  var dt_Diff = clsFunctions_comman.fillDatatable("Select * from Recipe_MasterDiff where Recipe_Code='" + cmbrecipecode.Text + "'");
                if (dt_A.Rows.Count != 0)
                {
                    btnCommand.Text = "Update";
                    txtRecipeName.Text = dt_A.Rows[0]["Recipe_Name"].ToString();
                    txtbatchsize.Text = dt_A.Rows[0]["Mixer_Capacity"].ToString();
                    LoadRecipeFromdb(dt_A.Rows[0], dt_A.Rows[0], dt_A.Rows[0]);// dt_B.Rows[0], dt_Diff.Rows[0]);
                }
                calculateTotal();
            }
        }

        //-------------------------------------------------------------------------------------


    }
}
