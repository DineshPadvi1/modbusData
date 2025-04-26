using RMC_CP30_Simem.Classes;
using System;
using System.Data;
using System.Windows.Forms;
using Uniproject.Classes;

////this form is used for software registration and create config file with name as vHotMixScadan.ini and its path is 'C:\Users\Vadmin\AppData\Roaming\vtIEpmLP'
namespace Uniproject.frmRegistration    //Uniproject
{
    public partial class cmn_frmRegistration : Form
    {
        string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Database\\Setup.mdb; Persist Security Info=true ;Jet OLEDB:Database Password=vasssetup;";
        string plantType = string.Empty;
        public cmn_frmRegistration()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void btnOK_Click(object sender, EventArgs e)
        {
            int f = 0;

            //if (txtplantcode.Text != "" &&   txtRegNo.Text != "" && plantType!=string.Empty)
            if (txtplantcode.Text != "" && txtRegNo.Text != "" && txtInstalledBy_Manual.Text != "" && cmbDeptList.Text != "" && txtDeviceID_Manual.Text != "")
            {
                f = RegisterSoftware1();
                if (f == 1)
                {
                    if (Delete_InsertSetup() == 1)
                    {
                        clsFunctions_comman.UniBox("Unipro Software Registration for " + cmbDeptList.Text + " Completed Successfully... Please Restart Application to continue..");
                        //MessageBox.Show("Software Register Successfully... Please Restart Application to continue..", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error in Registration", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else {
                MessageBox.Show("Fill All Fields.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
        }

        public int Delete_InsertSetup()
        {
            int j = 0;
            Boolean flag = true;
            //if (clsFunctions.regfilestatus == "FileNotFound")
            if (clsFunctions.regfilestatus == null)
            {
                //DataTable dt = RegisterSoftware_Uni.fillsetupdt("select * from SetupInfo");
                DataTable dt = RegisterSoftware_Uni.fillsetupdt("select * from PlantSetup");
                if (dt.Rows.Count == 0)
                    flag = true;
                else if (dt.Rows[0][0].ToString() == "")
                    flag = true;
                else flag = false;
            }
            else
            {
                flag = false;
            }

            if (flag == true)
            {
                //RegisterSoftware_Uni.Adosetup("Delete from SetupInfo");
                //string query = "insert into SetupInfo(dtFromDate,dtToDate,tStatus,iValue) values('" + DateTime.Today.ToString() + "','" + DateTime.Today.AddYears(1).ToString() + "','N'," + txtRegNo.Text + ")";
                //if (RegisterSoftware_Uni.Adosetup(query) == 1)
                {
                    //DataTable dt1 = clsFunctions.fillDatatable("select * from PlantSetup where plantcode='" + txtplantcode.Text + "'");
                    DataTable dt1 = clsFunctions.fillDatatable_setup("select * from PlantSetup where plantcode='" + txtplantcode.Text +"'");        //BhaveshT
                    if (dt1.Rows.Count == 0)
                    {
                        // 19/02/2024 - BhaveshT : Inserted defalut value PType = 'A'

                        //j = RegisterSoftware_Uni.Adosetup("INSERT INTO PlantSetup (ContractorCode,plantcode,PlantType,deptName ) VALUES('"+ txtRegNo.Text+ "','" + txtplantcode.Text + "','"+plantType+"','"+cmbDeptList.Text+"')");    //added ContractorCode field for inserting the data by dinesh
                        j = RegisterSoftware_Uni.Adosetup("INSERT INTO PlantSetup (DeviceID, ContractorCode, plantcode, PlantType, deptName, InstalledBy, macID, Device_IPAddress, InstallationDate, InstallationType, pType, PlantExpiry ) " +
                            "VALUES('" + txtDeviceID_Manual.Text + "','" + txtRegNo.Text + "','" + txtplantcode.Text + "','" + cmbPlantType.Text + "','" + cmbDeptList.Text + "', '"+ txtInstalledBy_Manual.Text +"','"+ clsFunctions_comman.PC_macAddress +"', '"+ clsFunctions_comman.PC_ipAddress + "', '"+ DateTime.Now.ToString("dd/MM/yyyy") +"', 'M', 'A', '" + DateTime.Today.AddYears(1).ToString() + "')");    //BhaveshT 26/12/2023

                    }
                    else
                    {
                        RegisterSoftware_Uni.Adosetup("Delete * from PlantSetup");

                        //j = RegisterSoftware_Uni.Adosetup("INSERT INTO PlantSetup (ContractorCode,plantcode,PlantType,deptName ) VALUES('" + txtRegNo.Text + "','" + txtplantcode.Text + "','" + plantType + "','" + cmbDeptList.Text + "')");    //added ContractorCode field for inserting the data by dinesh

                        j = RegisterSoftware_Uni.Adosetup("INSERT INTO PlantSetup (DeviceID, ContractorCode, plantcode, PlantType, deptName, InstalledBy, macID, Device_IPAddress, InstallationDate, InstallationType, PlantExpiry ) " +
                            "VALUES('" + txtDeviceID_Manual.Text + "','" + txtRegNo.Text + "','" + txtplantcode.Text + "','" + cmbPlantType.Text + "','" + cmbDeptList.Text + "', '" + txtInstalledBy_Manual.Text + "','" + clsFunctions_comman.PC_macAddress + "', '" + clsFunctions_comman.PC_ipAddress + "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', 'M', '" + DateTime.Today.AddYears(1).ToString() + "')");    //BhaveshT 26/12/2023

                    }

                    clsFunctions.InsertToPlantRenewalHistory(txtDeviceID_Manual.Text, DateTime.Today.ToString("dd/MM/yyyy"), DateTime.Today.AddYears(1).ToString("dd/MM/yyyy"), "0");

                    string aliasName = clsFunctions.CreateAliasName(cmbDeptList.Text, cmbPlantType.Text);

                    clsFunctions.InsertInServerMappingAtReg(aliasName, txtDeviceID_Manual.Text, txtplantcode.Text);

                }
                //if (j == 1) RegisterSoftware_Uni.createRegisterFile(clsFunctions_comman.regName, clsFunctions_comman.serialKey);
                if (j == 1)
                {
                    //RegisterSoftware_Uni.createRegisterFile(clsFunctions_comman.regName, clsFunctions_comman.serialKey, cmbDeptList.Text);
                    UniRegister_Auto uniRegister_Auto = new UniRegister_Auto();
                    string lbPlanttype = uniRegister_Auto.GetPlantTypeFromAlias(cmbPlantType.Text);
                    RegisterSoftware.createRegisterFile(txtDeviceID_Manual.Text, cmbDeptList.Text, lbPlanttype);
                    //RegisterSoftware.createRegisterFile(txtDeviceID_Manual.Text, cmbDeptList.Text, cmbPlantType.Text); //by dinesh
                }
                    
            }
            else
            {
                //RegisterSoftware_Uni.createRegisterFile(clsFunctions_comman.regName, clsFunctions_comman.serialKey);
                UniRegister_Auto uniRegister_Auto = new UniRegister_Auto();
                string lbPlanttype = uniRegister_Auto.GetPlantTypeFromAlias(cmbPlantType.Text);
                RegisterSoftware.createRegisterFile(txtDeviceID_Manual.Text, cmbDeptList.Text, lbPlanttype);
                //RegisterSoftware_Uni.createRegisterFile(clsFunctions_comman.regName, clsFunctions_comman.serialKey, cmbDeptList.Text); commented by dinesh
                j = 1;
            }
            return j;

        }
        public int RegisterSoftware1()
        {
            //if (clsFunctions.regName == txtregname.Text & clsFunctions.serialKey == txtserialkey.Text)        // comment by BhaveshT
            if (Uniproject.Classes.clsFunctions_comman.regName == txtregname.Text & Uniproject.Classes.clsFunctions_comman.serialKey == txtserialkey.Text)    //BhaveshT      15may
            {
                clsFunctions.IsRegSoftware = 1;
            }
            else
            {
                MessageBox.Show("Please enter valid Reg.Name or Serial No","VIPL");
                clsFunctions.IsRegSoftware = 0;
            }
            return clsFunctions.IsRegSoftware;
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            clsFunctions.FillCombo("SELECT worktype from tblworktype", cmbDeptList);
        }

        private void txtRegNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void rdbBT_CheckedChanged(object sender, EventArgs e)
        {
            plantType = "";
            rdbBT.Checked = !rdbRmc.Checked;
            plantType = "BT";
        }

        private void rdbRmc_CheckedChanged(object sender, EventArgs e)
        {
            plantType = "";
            rdbRmc.Checked = !rdbBT.Checked;
            plantType = "RMC";
        }
    }
}
