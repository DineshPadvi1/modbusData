using System;
using System.Windows.Forms;
using Uniproject.Classes;

namespace Uniproject.UtilityTools
{
    public partial class SelectClientDBVersion : Form
    {
        public SelectClientDBVersion()
        {
            InitializeComponent();
        }

        private void SelectClientDBVersion_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboData();
                cmbDataHeader.Text = clsFunctions.loadSingleValueSetup(" Select SoftwareVersion FROM DataHeaderTableSync WHERE SoftwareVersion <> 'VIPL' AND Flag = 'Y' ");
                cmbDataTransaction.Text = clsFunctions.loadSingleValueSetup(" Select SoftwareVersion FROM DataTransactionTableSync WHERE SoftwareVersion <> 'VIPL' AND Flag = 'Y' ");
            }
            catch { }
        }


        public void LoadComboData()
        {
            try
            {
                clsFunctions.FillCombo_setup("SELECT DISTINCT SoftwareVersion FROM DataHeaderTableSync WHERE SoftwareVersion <> 'VIPL'", cmbDataHeader);
                clsFunctions.FillCombo_setup("SELECT DISTINCT SoftwareVersion FROM DataTransactionTableSync WHERE SoftwareVersion <> 'VIPL'", cmbDataTransaction);
            }
            catch
            {

            }
        }

        private void btnSetMapping_Click(object sender, EventArgs e)
        {
            if (cmbDataHeader.Text == "" || cmbDataTransaction.Text == "")
            {
                MessageBox.Show("Please select DB Version");
            }

            else
            {
                try
                {
                    clsFunctions.AdoData_setup("UPDATE DataHeaderTableSync SET Flag = 'N' WHERE SoftwareVersion <> 'VIPL'");
                    clsFunctions.AdoData_setup("UPDATE DataTransactionTableSync SET Flag = 'N' WHERE SoftwareVersion <> 'VIPL'");

                    clsFunctions.AdoData_setup("UPDATE DataHeaderTableSync SET Flag = 'Y' WHERE SoftwareVersion = '" + cmbDataHeader.Text + "' ");
                    clsFunctions.AdoData_setup("UPDATE DataTransactionTableSync SET Flag = 'Y' WHERE SoftwareVersion = '" + cmbDataTransaction.Text + "' ");


                    clsFunctions.AdoData_setup("Update DataHeaderTableSync set Flag='Y' where Type='VIPL' and info='Fields' ");
                    clsFunctions.AdoData_setup("Update DataTransactionTableSync set Flag='Y' where Type='VIPL' and info='Fields' ");

                    clsFunctions_comman.ErrorLog("DB Version Set Successfully : " + cmbDataHeader.Text);
                    MessageBox.Show("DB Version Set Successfully");
                    this.Close();
                }
                catch
                {
                    clsFunctions_comman.ErrorLog("Error while setting DB Version : " + cmbDataHeader.Text);
                    MessageBox.Show("Error while setting DB Version : " + cmbDataHeader.Text);
                    this.Close();
                }
            }

        }
    }
}
