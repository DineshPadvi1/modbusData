using System;
using System.Data;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.Classes.Model;

namespace Uniproject.UtilityTools
{
    public partial class Sync : Form
    {
        public Sync()
        {
            InitializeComponent();
        }

        //----------------------------------------------------

        private void Sync_Load(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------

        private void btnFetchSerMapPreset_Click(object sender, EventArgs e)
        {
            if (NetworkHelper.IsInternetAvailable())
            {
                int a = clsFunctions.GetAndUpdateServerMappingDataFromAPI(clsFunctions.aliasName, clsFunctions.activeDeptName);

                if (a == 1)
                    clsFunctions_comman.UniBox("API and Websites data successfully updated for " + clsFunctions.aliasName + "");

                if (a == 0)
                    clsFunctions_comman.UniBox("API and Websites data not updated for " + clsFunctions.aliasName + ", please try again later.");

            }
            else
            {
                MessageBox.Show("Check internet connection.");
                return;
            }

        }

        //----------------------------------------------------

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

        //----------------------------------------------------

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

        //----------------------------------------------------

        private void btnFetchUniSetup_Click(object sender, EventArgs e)
        {
            bool flag = clsFunctions.AskPrompt();

            if (flag)
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
                }
                else
                {
                    MessageBox.Show("Check internet connection.");
                    return;
                }
            }
            else
            {
                // Password is incorrect, display an error message
                MessageBox.Show("Enter Correct password");
            }

        }

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


        //----------------------------------------------------

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //----------------------------------------------------


    }
}
