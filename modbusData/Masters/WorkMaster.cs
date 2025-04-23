using System;
using System.Data;
using System.Windows.Forms;
using Uniproject.Classes;
using Uniproject.Classes.Model;

namespace Uniproject.Masters
{
    public partial class WorkMaster : Form
    {   
        string tempLoad;

        public WorkMaster()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(tabControl1_KeyDown);
        }
        public string js = "";
        string temp;
        string maxid;

        //------------------------------------------------------------------------------------------------------------

        private async void btnCommand_Click(object sender, EventArgs e)
        {
            string ischecked = "N";

            if (chkisCompleted.Checked)
            {
                ischecked = "Y";
            }
            else
                ischecked = "N";

            cmboprjurisdiction.SelectedIndex = 0;
            cmboprdivision.SelectedIndex = 0;
            try
            {

               // string a = (txtworkno.Text).Trim();
                if (Validation())
                {                   
                    if (btnCommand.Text == "&Save") {
                        //commented by dinesh
                        //if (clsFunctions.AdoData("Insert into WorkOrder (workno,workname,worktype,ContractorID,ContractorName) values('"
                        //    + (txtworkno.Text).Trim() + "','" + (txtworkname.Text) + "','" + cmbworktype.SelectedItem + "','" + cmbCust.SelectedText + "','" + cmbCust.SelectedValue + "')") == 1)
                        
                        if (clsFunctions.AdoData("Insert into WorkOrder (workno,workname,worktype,ContractorID,ContractorName,iscompleted) values('"
                             + (txtworkno.Text).Trim() + "','" + (txtworkname.Text) + "','" + cmbworktype.SelectedItem + "','" + cmbCust.SelectedValue + "','" + cmbCust.Text + "','"+ ischecked+"')") == 1)
                        {
                           //int workID = clsFunctions.GetMaxId("Select workorderID from WorkOrder where workno='" + (txtworkno.Text).Trim() + "' and workname='" + (txtworkname.Text) + "'");
                            string workID = clsFunctions.loadSingleValue("Select Workno from WorkOrder where workno='" + (txtworkno.Text).Trim() + "' and workname='" + (txtworkname.Text) + "'");
                            for (int i=0;i<dgvJobSite.Rows.Count-1;i++)
                            {
                                string js = dgvJobSite.Rows[i].Cells["jsName"].Value.ToString();

                                //-------------------------------

                                if (clsFunctions.IsSiteExist(workID, js) == 0)
                                    clsFunctions.AdoData("Insert into Site_Master(workorderID,SiteName)  values('" + workID + "','" + js + "')");
                                else
                                    MessageBox.Show("JobSite is already exist. \nJobSite: '" + js + "'\nWorkCode: '" + workID + "'" );

                                //-------------------------------

                            }
                            MessageBox.Show("Details saved successfully.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvJobSite.Rows.Clear();
                        }                    
                    }
                    else 
                    {
                        //updating the WorkOrder Details here.
                        
                        if (clsFunctions.AdoData("Update WorkOrder set workno='" + (txtworkno.Text).Trim() + "',workname='" + (txtworkname.Text).Trim() 
                             +  "', worktype='" + cmbworktype.SelectedItem + "', ContractorID='"+ cmbCust.SelectedValue + "' ,ContractorName='"+ cmbCust.Text + "', iscompleted= '" + ischecked + "'  where workorderID= " + lblid.Text + " AND WorkType = '" + cmbworktype.Text + "'" ) == 1)
                        {
                            //updating Site_Master
                            //int workID = clsFunctions.GetMaxId("Select id from Site_Master where SiteName='" + (tempLoad).Trim()  + "'");
                            
                            for (int i = 0; i < dgvJobSite.Rows.Count - 1; i++)
                            {
                                try
                                {
                                    maxid = clsFunctions.loadSingleValue("SELECT MAX(id)+1 FROM Site_Master");
                                    if (maxid == "") maxid = "1";
                                    string js = dgvJobSite.Rows[i].Cells["jsName"].Value.ToString();
                                    temp = js;
                                    if (dgvJobSite.Rows[i].Cells["ID"].Value.ToString() != null)
                                    {
                                        string sid = dgvJobSite.Rows[i].Cells["ID"].Value.ToString();
                                        clsFunctions.AdoData("update Site_Master SET SiteName='" + js + "' where id=" + sid + "");

                                        //clsFunctions.AdoData("update Site_Master SET SiteName='" + js + "' where id=" + sid + "");
                                    }
                                }
                                catch(Exception ex) 
                                {
                                    //string sid = dgvJobSite.Rows[i].Cells["ID"].Value.ToString();

                                    //-------------------------------

                                    if (clsFunctions.IsSiteExist(txtworkno.Text, temp) == 0)
                                    {
                                        clsFunctions.AdoData("insert into Site_Master (id,SiteName,workorderID,flag) values(" + maxid + ",'" + temp + "','" + txtworkno.Text + "','Y')");


                                        //-------------------------------

                                        //clsFunctions.AdoData("insert into Site_Master (id,SiteName,workorderID,flag) values(" + maxid + ",'" + temp + "','" + txtworkno.Text + "','Y')");
                                        clsFunctions_comman.ErrorLog("WorkMaster - btnCommand_Click : " + ex.Message);

                                        var woCode = txtworkno.Text;
                                        var siteName = temp;
                                        var latitude = "0";
                                        var longitude = "0";

                                        string responseMessage = await clsJobSiteSync.InsertWorkOrderSiteAsync(woCode, siteName, latitude, longitude);

                                        if (responseMessage.Contains("Site Stored Successfully"))
                                        {
                                            lblResponse.Text = responseMessage;
                                            clsFunctions_comman.ErrorLog("WorkMaster: " + siteName + " : " + lblResponse.Text);
                                        }
                                        else if (responseMessage.Contains("Is Already Exist"))
                                        {
                                            lblResponse.Text = responseMessage;
                                            clsFunctions_comman.ErrorLog("WorkMaster: " + lblResponse.Text);
                                        }
                                        else if (responseMessage.Contains("Wo_Code Not Exist"))
                                        {
                                            lblResponse.Text = responseMessage;
                                            clsFunctions_comman.ErrorLog("WorkMaster: " + lblResponse.Text);
                                        }
                                        else if (responseMessage.Contains("Site Not Stored"))
                                        {
                                            lblResponse.Text = responseMessage;
                                            clsFunctions_comman.ErrorLog("WorkMaster: " + lblResponse.Text);
                                        }
                                        else
                                        {
                                            lblResponse.Text = "ERROR While storing Site_Name at Server.";     // + responseMessage;
                                            clsFunctions_comman.ErrorLog("WorkMaster: " + lblResponse.Text);
                                        }


                                        clsFunctions_comman.ErrorLog("WorkMaster - btnCommand_Click : " + ex.Message);
                                        // clsFunctions.AdoData("update Site_Master SET SiteName='" + temp + "' where id=" + maxid + "");

                                    }
                                    else
                                    {
                                        MessageBox.Show("JobSite is already exist. \n\nJobSite: '" + temp + "'\nWorkCode: '" + txtworkno.Text + "'");
                                        
                                    }

                                };

                            }
                            MessageBox.Show("Details updated successfully.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvJobSite.Rows.Clear();
                        }
                    }
                    ClearText();
                    LoadWorkDetails();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions_comman.ErrorLog("WorkMaster - btnCommand_Click : " + ex.Message);   
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private bool Validation()
        {
            if (txtworkno.Text == "")
            {
                MessageBox.Show("Please enter Work No.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtworkname.Text == "")
            {
                MessageBox.Show("Please enter Work Name.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Commented by Dinesh
            //if (txtoprlatitude.Text == "")
            //{
            //    MessageBox.Show("Please enter Operating Latitude.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            //Commented by dinesh
            //if (txtoprlongitude.Text == "")
            //{
            //    MessageBox.Show("Please enter Operating Longitude.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            if (cmboprjurisdiction.SelectedItem == null)
            {
                MessageBox.Show("Please enter Operating Jurisdiction.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmboprdivision.SelectedItem == null)
            {
                MessageBox.Show("Please enter Operating Devision.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbworktype.SelectedItem == null)
            {
                MessageBox.Show("Please select Work Type.", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dgvJobSite.Rows.Count < 2)
            {
                MessageBox.Show("Please add atleast one job site", "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            return true;
        }

        //------------------------------------------------------------------------------------------------------------

        private void WorkMaster_Load(object sender, EventArgs e)
        {
            try
            {
                //Loading Form icon here.   --> By Dinesh
                //string myIcon = clsFunctions.loadSingleValueSetup("select imagepath from ImageIconPath where image_used='Y' and UILocation='commonForms'");
                //Icon icon = new Icon(@myIcon);
                //this.Icon = icon;

                clsFunctions.activeDeptName = clsFunctions.GetActiveDeptNameFromServerMapping();
            }
            catch(Exception ex)
            {
                clsFunctions_comman.ErrorLog("WorkMaster - WorkMaster_Load : " + ex.Message);  
            }
            LoadWorkDetails();

            //clsFunctions.FillCombo("Select description from tblJuri_Divi_Names", cmboprjurisdiction);
            //clsFunctions.FillCombo("Select description from tblJuri_Divi_Names", cmboprdivision);

            clsFunctions.FillComboValueID("Select Customer_Code,Customer_Name from Customer_Master", cmbCust, "Customer_Name", "Customer_Code");

            //clsFunctions.FillComboValueID("SELECT cm.Customer_Code, cm.Customer_Name, wo.ContractorName FROM Customer_Master cm LEFT JOIN WorkOrder wo ON cm.Customer_Code = wo.ContractorID", cmbCust, "Customer_Name", "Customer_Code");

        }

        //------------------------------------------------------------------------------------------------------------

        private void LoadWorkDetails()
        {
            try
            {
                clsFunctions.activeDeptName = clsFunctions.GetActiveDeptNameFromServerMapping();

                DataTable dt = Uniproject.Classes.clsFunctions.fillDatatable("select * from WorkOrder WHERE WorkType = '"+ clsFunctions.activeDeptName + "' Order by workOrderID");
                dgvworkdetails.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvworkdetails.Rows.Add(row["workOrderID"], row["workno"], row["workname"], row["worktype"], row["ContractorName"], row["iscompleted"] );
                }

                //clsFunctions.FillCombo("select Distinct worktype from tblworktype",cmbworktype);
                clsFunctions.FillCombo_setup("select Distinct DeptName from ServerMapping where Flag = 'Y'", cmbworktype);
                cmbworktype.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions_comman.ErrorLog("WorkMaster - LoadWorkDetails() : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void ClearText()
        {
            lblid.Text = "";
            txtworkno.Text = "";
            txtworkname.Text = "";
            txtoprlatitude.Text = "";
            txtoprlongitude.Text = "";
            cmboprjurisdiction.SelectedItem = null;
            cmboprdivision.SelectedItem = null;
            cmbworktype.SelectedItem = null;
            btnCommand.Text = "&Save";
        }

        //------------------------------------------------------------------------------------------------------------

        private void dgvworkdetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
             {
                lblResponse.Text = "";

                int selectedIndex = 0;
                int cnt = 0;
                 string desiredText= dgvworkdetails.CurrentRow.Cells["cell_cust"].Value.ToString();
                
                lblid.Text = dgvworkdetails.CurrentRow.Cells["cell_Id"].Value.ToString();
                txtworkno.Text = dgvworkdetails.CurrentRow.Cells["cell_workno"].Value.ToString();
                txtworkname.Text = dgvworkdetails.CurrentRow.Cells["cell_workname"].Value.ToString();
                //txtoprlatitude.Text = dgvworkdetails.CurrentRow.Cells["cell_oprlat"].Value.ToString();
                //txtoprlongitude.Text = dgvworkdetails.CurrentRow.Cells["cell_oprlong"].Value.ToString();
                //cmboprjurisdiction.SelectedItem = dgvworkdetails.CurrentRow.Cells["cell_oprjuri"].Value;
               // cmboprdivision.SelectedItem = dgvworkdetails.CurrentRow.Cells["cell_oprdivision"].Value;
                cmbworktype.SelectedItem = dgvworkdetails.CurrentRow.Cells["cell_worktype"].Value;

                if (dgvworkdetails.CurrentRow.Cells["iscomplete"].Value.ToString() == "Y")
                {
                   chkisCompleted.Checked = true;
                }
                else
                    chkisCompleted.Checked = false;

                btnCommand.Text = "&Update";
                tabControl1.SelectedIndex = 0;

                //DataTable dt =  clsFunctions.fillDatatable("Select * from Site_Master where workorderID='" +lblid.Text+"'");     // datatype changed to from number to text  //BhaveshT
                DataTable dt = clsFunctions.fillDatatable("Select * from Site_Master where workorderID='" + txtworkno.Text + "'");     // datatype changed to from number to text  //BhaveshT


                //clearing the jobsite gridview -> by Dinesh
                dgvJobSite.Rows.Clear();
                bindJobSiteGrid(dt);
               // tempLoad= dgvJobSite.CurrentRow.Cells["jsName"].Value.ToString();

                //added by dinesh
                //FindStrngExact matches the Text from combobox and returns its index
                selectedIndex = cmbCust.FindStringExact(desiredText);
                cmbCust.SelectedIndex = selectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VIPL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsFunctions_comman.ErrorLog("WorkMaster - dgvworkdetails_CellDoubleClick : " + ex.Message);     //BhaveshT
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void btnclear_Click(object sender, EventArgs e)
        {
            //cmbCust.Items.Clear();
            ClearText();
            //added by dinesh
            dgvJobSite.Rows.Clear();
            lblResponse.Text = string.Empty;
        }

       public void bindJobSiteGrid(DataTable dt)
        {
            int i = 1;
            int j = 3;
            foreach (DataRow row in dt.Rows)
            {
                dgvJobSite.Rows.Add(i, row["sitename"], row["id"]);
                //dgvJobSite.Rows.Add(j, row["workorderID"]);
                i++;
                //j++;
            }
        }

        //------------------------------------------------------------------------------------------------------------

        /* 
         02/04/2024 - BhaveshT 
         Added Delete All button to delete all records from WorkOrder, Contractor_Master & Site_Master table of unipro DB. 
        */

        private void btnDeleteWO_Click(object sender, EventArgs e)
        {
            clsFunctions.AdoData("Delete * from WorkOrder");
            clsFunctions.AdoData("Delete * from Customer_Master");
            clsFunctions.AdoData("Delete * from Site_Master");

            clsFunctions_comman.UniBox("Deleted WorkOrders, Contractors & Sites. \nNow IMPORT Work Orders");
            this.Close();
        }

        //------------------------------------------------------------------------------------------------------------

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            // 16/04/2024 : BhaveshT - DeleteAll button will be visible & invisible on shortcut click: Alt + F7

            if (e.Alt && e.KeyCode == Keys.F7)
            {
                if (btnDeleteWO.Visible == false)
                {
                    btnDeleteWO.Visible = true;
                }
                else if (btnDeleteWO.Visible == true)
                {
                    btnDeleteWO.Visible = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------




    }
}
