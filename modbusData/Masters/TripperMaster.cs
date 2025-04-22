using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Uniproject.Classes;
namespace Uniproject.Masters
{
    public partial class TipperMaster : Form
    {

        private static readonly LoggerService _loggerService = new LoggerService();         // BhaveshT
        public TipperMaster()
        {
            InitializeComponent();
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    //Added validation for Textbox: txttipperno
                    if (ValidateVehicleNo(txttipperno.Text))
                    {
                        txttipperno.Text = txttipperno.Text.Replace(" ", "");

                        if (btnCommand.Text == "&Save")
                        {
                            if (clsFunctions.AdoData("Insert into tbltipperdetails (tipperno,make,capacity,DriverName) values('"
                                + txttipperno.Text + "','" + txtmake.Text + "','" + txtcapacity.Text + "','" + txtDriver.Text + "')") == 1)
                            {
                                MessageBox.Show("Details saved successfully.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            if (clsFunctions.AdoData("Update tbltipperdetails set tipperno='" + txttipperno.Text + "',make='" + txtmake.Text
                                + "',capacity='" + txtcapacity.Text + "', DriverName='"+ txtDriver.Text +"'  where Id= " + lblid.Text) == 1)
                            {
                                MessageBox.Show("Details updated successfully.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        ClearText();
                        LoadtipperDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _loggerService.LogMessage(LogType.Error, ErrorType.Error, "TripperMaster - btnCommand_Click : " + ex.Message);     //BhaveshT
            }
        }


        //---------------------------------------------------------------------------------------------------
        /*
        28/10/2023 - BhaveshT
        Function - ValidateVehicleNo() Parameter - string
        Created ValidateVehicleNo function which accept vehicleNo as a string and validate, only Alphabet, Numbers, '-' and 
        the input text of max length 15 will be accepted, if it found not acceptable character or symbol in string it return FALSE
         */
        public bool ValidateVehicleNo(string input)
        {
            input.Replace(" ", "");
            // Check for valid characters using a regular expression
            if (!Regex.IsMatch(input, @"^[a-zA-Z0-9\s\-]+$"))
            {
                MessageBox.Show("Enter valid vehicle no. : Special Character not allowed");
                return false;
            }

            // Check the length
            if (input.Length >= 11)
            {
                MessageBox.Show("Vehicle no. must be less than 10 in length ");
                return false;
            }

            // If the input passes both checks, it's valid
            return true;
        }
        //---------------------------------------------------------------------------------------------------

        private bool Validation()
        {
            if (txttipperno.Text == "")
            {
                MessageBox.Show("Please enter Vehicle No.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtcapacity.Text == "")
            {
                MessageBox.Show("Please enter Capacity", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void TripperMaster_Load(object sender, EventArgs e)
        {
            try
            {
                ////Loading Form icon here.   --> By Dinesh
                //string myIcon = clsFunctions.loadSingleValueSetup("select imagepath from ImageIconPath where image_used='Y' and UILocation='commonForms'");
                //Icon icon = new Icon(@myIcon);
                //this.Icon = icon;
            }
            catch(Exception ex)
            {
                _loggerService.LogMessage(LogType.Error, ErrorType.Error, "TripperMaster - TripperMaster_Load : " + ex.Message);     //BhaveshT
            }
            LoadtipperDetails();
        }

        private void LoadtipperDetails()
        {
            try
            {
                clsFunctions.checknewcolumn("DriverName", "Text(20)", "tbltipperdetails");

                DataTable dt = clsFunctions.fillDatatable("select * from tbltipperdetails order by ID");
                dgvtipperdetails.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvtipperdetails.Rows.Add(row["ID"], row["tipperno"], row["make"], row["capacity"], row["DriverName"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _loggerService.LogMessage(LogType.Error, ErrorType.Error, "TripperMaster - LoadtipperDetails() : " + ex.Message);     //BhaveshT
            }
        }

        private void ClearText()
        {
            lblid.Text = "";
            txttipperno.Text = "";
            txtmake.Text = "";
            txtcapacity.Text = "";
            txtDriver.Text = "";
            btnCommand.Text = "&Save";
        }

        private void dgvtripperdetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblid.Text = dgvtipperdetails.CurrentRow.Cells["cell_Id"].Value.ToString();
                txttipperno.Text = dgvtipperdetails.CurrentRow.Cells["cell_tripperno"].Value.ToString();
                txtmake.Text = dgvtipperdetails.CurrentRow.Cells["cell_make"].Value.ToString();
                txtcapacity.Text = dgvtipperdetails.CurrentRow.Cells["cell_capacity"].Value.ToString();
                txtDriver.Text = dgvtipperdetails.CurrentRow.Cells["cell_driver"].Value.ToString();
                btnCommand.Text = "&Update";
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _loggerService.LogMessage(LogType.Error, ErrorType.Error, "TripperMaster - dgvtripperdetails_CellDoubleClick : " + ex.Message);     //BhaveshT
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void txtcapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txttipperno_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
        25/10/2023 - BhaveshT
        Added validation on Textbox: txttipperno
        Created KeyPress & TextChanged event to accept only Alphabet, Numbers, '-' and the input text of max length 13 will be accepted
         */
        //private void txttipperno_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // Allow only alphabets, numbers, hyphen, and control keys
        //    if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //
        //    // Limit input to 13 characters
        //    if (txttipperno.Text.Length >= 13 && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}
        //
        //private void txttipperno_TextChanged(object sender, EventArgs e)
        //{
        //    // Remove any characters beyond 13
        //    if (txttipperno.Text.Length > 13)
        //    {
        //        txttipperno.Text = txttipperno.Text.Substring(0, 13);
        //    }
        //}
    }
}
