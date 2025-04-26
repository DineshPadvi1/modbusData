using System;
using System.Windows.Forms;
using Uniproject.Classes;
using System.Data;

namespace Uniproject.UtilityTools
{
   /*
    * 02/01/2024 - BhaveshT
    * Created SelectAliasForProduction form - to select Alias Name at the time of opening software.
    */

    public partial class SelectAliasForProduction : Form
    {
        public SelectAliasForProduction()
        {
            InitializeComponent();
        }

        public static string a = "";
        public static string insertQuery = "";

        mdiMain md = new mdiMain();

        private void SelectAliasForProduction_Load(object sender, EventArgs e)
        {
            try
            {
                string pType = mdiMain.plantType;
                
                if(pType.Contains("RMC"))
                {
                    a = "RMC";
                }
                else if(pType.Contains("Bitu") || pType.Contains("BT"))
                {
                    a = "BT";
                }

                //clsFunctions.FillCombo_setup("Select AliasName from ServerMapping_Preset where DPTStatus = 'Y' AND AliasName LIKE '*" + a + "*';", cmbDeptForProduction);

                clsFunctions.FillCombo_setup("SELECT AliasName FROM ServerMapping WHERE AliasName LIKE '%" + a + "%';", cmbDeptForProduction);
                
                string selectedAlias = clsFunctions.loadSingleValueSetup("Select AliasName FROM ServerMapping WHERE Flag = 'Y'");
                if(selectedAlias != "")
                    cmbDeptForProduction.Text = selectedAlias;

            }
            catch (Exception ex)
            {

            }
        }

        private void btnSetProductionDept_Click(object sender, EventArgs e)
        {
            try
              {
                if (cmbDeptForProduction.Text != "")
                {
                    clsFunctions.AdoData_setup("UPDATE ServerMapping SET Flag = 'N'");

                    string updateQuery = "UPDATE ServerMapping SET Flag = 'Y' WHERE AliasName = '" + cmbDeptForProduction.Text + "' ";

                    int a = clsFunctions.AdoData_setup(updateQuery);

                    if (a == 1)
                    {
                        MessageBox.Show("Department for Production set to : '"+ cmbDeptForProduction.Text + "' ");
                        
                        clsFunctions.activeDeptName = clsFunctions.GetActiveDeptNameFromServerMapping();
                        clsFunctions.activePlantCode = clsFunctions.GetActivePlantCodeFromServerMapping();
                        clsFunctions.aliasName = clsFunctions.GetActiveAliasNameFromServerMapping();
                        clsFunctions.activeDeviceID = clsFunctions.GetActiveDeviceIdFromServerMapping();

                        this.Close();
                    }
                    else
                    {
                        clsFunctions_comman.UniBox("Error while selecting Department for Production : '" + cmbDeptForProduction.Text + "' ");
                    }
                }
                else
                {
                    clsFunctions_comman.UniBox("Please select Department for Production ");
                }

                //md.SetPlantInfoLogo();

            }
            catch (Exception ex)
            {

            }
        }

        private void cmbDeptForProduction_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbDeptForProduction.Text = cmbDeptForProduction.SelectedText;

        }
    }
}
