using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Uniproject.Classes;

namespace Uniproject.RMC_forms.Masters
{
    public partial class CustomerMaster : Form
    {
        private static readonly LoggerService _loggerService = new LoggerService();         // BhaveshT
        public CustomerMaster()
        {
            InitializeComponent();
        }

        private void CustomerMaster_Load(object sender, EventArgs e)
        {
            try
            {
                clearcontrol();
                //txtcustno.Text = clsFunctions.GetMaxId("select max(Customer_Code) from Customer_Master").ToString();
                clsFunctions_comman.fillGridView("select Customer_Code,Customer_Name,Customer_Address,Phone from Customer_Master", dataGridView1);
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("FetchData : " + ex.Message);
                _loggerService.LogMessage(LogType.Error, ErrorType.Error, "CustomerMaster_Load : " + ex.Message);     //BhaveshT
            }

            //if (backgroundWorker1.IsBusy)
            //{
            //}
            //else
            //{
            //    backgroundWorker1.RunWorkerAsync();

            //}           
        }

        private void clearcontrol()
        {
            txtaddress.Text = "";
            txtcustno.Text = "";
            // clsFunctions.GetMaxId("select max(Customer_Code) from Customer_Master").ToString();
            txtphone.Text = "";
            txtcustname.Text = "";
            btncommand.Text = "Save";
            btncommand.Enabled = true;
            dataGridView1.DataSource = null;
            clsFunctions_comman.fillGridView("select Customer_Code,Customer_Name,Customer_Address,Phone from Customer_Master", dataGridView1);

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clearcontrol();
        }


        private void btncommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcustname.Text != "" && txtcustno.Text != "")
                {
                    if (btncommand.Text == "Save")
                    {
                        if (clsFunctions_comman.fillDatatable("select * from Customer_Master where Customer_Code= '" + txtcustno.Text + "'").Rows.Count == 0)
                        {
                            int i = clsFunctions_comman.Ado("insert into Customer_Master(Customer_Code,Customer_Name,Customer_Address,Phone) values('" + txtcustno.Text + "','" + txtcustname.Text + "','" + txtaddress.Text + "','" + txtphone.Text + "')");
                            MessageBox.Show("Record Save Successfully.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Customer No Already assign to other Customer.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    else
                    {
                        int i = clsFunctions_comman.Ado("update Customer_Master set Customer_Name='" + txtcustname.Text + "',Customer_Address='" + txtaddress.Text + "',Phone='" + txtphone.Text + "' where Customer_Code='" + txtcustno.Text + "'");
                        MessageBox.Show("Record Update Successfully.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter customer name / code.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                clearcontrol();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _loggerService.LogMessage(LogType.Error, ErrorType.Error, "CustomerMaster - btnCommand_Click : " + ex.Message);     //BhaveshT
            }
        }

        private void txtphone_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnclear_Click_1(object sender, EventArgs e)
        {
            clearcontrol();

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtaddress.Text = dataGridView1.CurrentRow.Cells["Customer_Address"].Value.ToString();
            txtcustname.Text = dataGridView1.CurrentRow.Cells["Customer_Name"].Value.ToString();
            txtcustno.Text = dataGridView1.CurrentRow.Cells["Customer_Code"].Value.ToString();
            txtphone.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
            btncommand.Text = "Update";
        }

        private void button1_Click(object sender, EventArgs e)      // Refresh
        {

            //if (backgroundWorker1.IsBusy)
            //{
            //}
            //else
            //{
            //    backgroundWorker1.RunWorkerAsync();
            
            //}

            try
            {
                //dataGridView1.Rows.Clear();
                clsFunctions_comman.fillGridView("select Customer_Code,Customer_Name,Customer_Address,Phone from Customer_Master", dataGridView1);
            }
            catch (Exception ex)
            {
                clsFunctions_comman.ErrorLog("FetchData : " + ex.Message);
                _loggerService.LogMessage(LogType.Error, ErrorType.Error, "CustomerMaster - btnRefresh_Click : " + ex.Message);     //BhaveshT
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var dt = clsFunctions_comman.fillDatatable("select * from Customer_Master");

            // if (dt.Rows.Count != 0) clsFunctions.Ado("delete from Customer_Master");

          //  dt = data.FillDataTable("select * from Customer_Master");
            //  else dt = data.FillDataTable("select * from Customer_Master where Customer_Code>="+ txtcustno.Text);

            if (dt.Rows.Count > 0)
            {
                //int i = clsFunctions.GetMaxId("select max(Customer_Code) from Customer_Master");
                foreach (DataRow row in dt.Rows)
                {
                    //i++;
                    clsFunctions_comman.Ado("insert into Customer_Master (Customer_Code,Customer_Name,Customer_Address,Phone) values('" +
                                  row["Customer_Code"].ToString() + "','" + row["Customer_Name"].ToString() + "','" + row["Customer_Address"].ToString() + "','" + row["Phone"].ToString() + "')");
                }

                //   data.Ado("Update Site_Master Set Phone='000' where Deleted_Rec_Flag='No'");
            }

            clsFunctions_comman.fillGridView("select Customer_Code,Customer_Name,Customer_Address,Phone from Customer_Master", dataGridView1);



        }

        private void txtcustno_TextChanged(object sender, EventArgs e)
        {
            if(btncommand.Text=="Update")
                txtcustno.Enabled = false;
            else txtcustno.Enabled = true;
        }

        private void txtcustno_Click(object sender, EventArgs e)
        {
            if (btncommand.Text == "Update")
                txtcustno.Enabled = false;
            else txtcustno.Enabled = true;
        }
    }
}
