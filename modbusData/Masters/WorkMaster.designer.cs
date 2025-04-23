namespace Uniproject.Masters
{
    partial class WorkMaster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkMaster));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblResponse = new System.Windows.Forms.Label();
            this.btnDeleteWO = new System.Windows.Forms.Button();
            this.chkisCompleted = new System.Windows.Forms.CheckBox();
            this.btnSaveSite = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbCust = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dgvJobSite = new System.Windows.Forms.DataGridView();
            this.srNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboprdivision = new System.Windows.Forms.ComboBox();
            this.cmboprjurisdiction = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtoprlongitude = new System.Windows.Forms.TextBox();
            this.txtoprlatitude = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvworkdetails = new System.Windows.Forms.DataGridView();
            this.cell_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_workno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_workname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_worktype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_cust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iscomplete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbworktype = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblid = new System.Windows.Forms.Label();
            this.btnclear = new System.Windows.Forms.Button();
            this.btnCommand = new System.Windows.Forms.Button();
            this.txtworkname = new System.Windows.Forms.TextBox();
            this.txtworkno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobSite)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvworkdetails)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.lblResponse);
            this.tabPage1.Controls.Add(this.btnDeleteWO);
            this.tabPage1.Controls.Add(this.chkisCompleted);
            this.tabPage1.Controls.Add(this.btnSaveSite);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.cmbCust);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.dgvJobSite);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dgvworkdetails);
            this.tabPage1.Controls.Add(this.cmbworktype);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.lblid);
            this.tabPage1.Controls.Add(this.btnclear);
            this.tabPage1.Controls.Add(this.btnCommand);
            this.tabPage1.Controls.Add(this.txtworkname);
            this.tabPage1.Controls.Add(this.txtworkno);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblResponse
            // 
            resources.ApplyResources(this.lblResponse, "lblResponse");
            this.lblResponse.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblResponse.Name = "lblResponse";
            // 
            // btnDeleteWO
            // 
            resources.ApplyResources(this.btnDeleteWO, "btnDeleteWO");
            this.btnDeleteWO.ForeColor = System.Drawing.Color.Maroon;
            this.btnDeleteWO.Name = "btnDeleteWO";
            this.btnDeleteWO.UseVisualStyleBackColor = true;
            this.btnDeleteWO.Click += new System.EventHandler(this.btnDeleteWO_Click);
            // 
            // chkisCompleted
            // 
            resources.ApplyResources(this.chkisCompleted, "chkisCompleted");
            this.chkisCompleted.Name = "chkisCompleted";
            this.chkisCompleted.UseVisualStyleBackColor = true;
            // 
            // btnSaveSite
            // 
            this.btnSaveSite.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSaveSite.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.btnSaveSite, "btnSaveSite");
            this.btnSaveSite.Name = "btnSaveSite";
            this.btnSaveSite.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Name = "label11";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // cmbCust
            // 
            this.cmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbCust, "cmbCust");
            this.cmbCust.ForeColor = System.Drawing.Color.Maroon;
            this.cmbCust.FormattingEnabled = true;
            this.cmbCust.Name = "cmbCust";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // dgvJobSite
            // 
            this.dgvJobSite.BackgroundColor = System.Drawing.Color.White;
            this.dgvJobSite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobSite.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srNo,
            this.jsName,
            this.ID});
            resources.ApplyResources(this.dgvJobSite, "dgvJobSite");
            this.dgvJobSite.Name = "dgvJobSite";
            this.dgvJobSite.RowTemplate.Height = 24;
            // 
            // srNo
            // 
            resources.ApplyResources(this.srNo, "srNo");
            this.srNo.Name = "srNo";
            // 
            // jsName
            // 
            resources.ApplyResources(this.jsName, "jsName");
            this.jsName.Name = "jsName";
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            this.ID.Name = "ID";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboprdivision);
            this.groupBox1.Controls.Add(this.cmboprjurisdiction);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtoprlongitude);
            this.groupBox1.Controls.Add(this.txtoprlatitude);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cmboprdivision
            // 
            this.cmboprdivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboprdivision.ForeColor = System.Drawing.Color.Maroon;
            this.cmboprdivision.FormattingEnabled = true;
            this.cmboprdivision.Items.AddRange(new object[] {
            resources.GetString("cmboprdivision.Items")});
            resources.ApplyResources(this.cmboprdivision, "cmboprdivision");
            this.cmboprdivision.Name = "cmboprdivision";
            // 
            // cmboprjurisdiction
            // 
            this.cmboprjurisdiction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboprjurisdiction.ForeColor = System.Drawing.Color.Maroon;
            this.cmboprjurisdiction.FormattingEnabled = true;
            this.cmboprjurisdiction.Items.AddRange(new object[] {
            resources.GetString("cmboprjurisdiction.Items")});
            resources.ApplyResources(this.cmboprjurisdiction, "cmboprjurisdiction");
            this.cmboprjurisdiction.Name = "cmboprjurisdiction";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Name = "label8";
            // 
            // txtoprlongitude
            // 
            this.txtoprlongitude.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.txtoprlongitude, "txtoprlongitude");
            this.txtoprlongitude.Name = "txtoprlongitude";
            // 
            // txtoprlatitude
            // 
            this.txtoprlatitude.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.txtoprlatitude, "txtoprlatitude");
            this.txtoprlatitude.Name = "txtoprlatitude";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dgvworkdetails
            // 
            this.dgvworkdetails.AllowUserToAddRows = false;
            this.dgvworkdetails.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgvworkdetails, "dgvworkdetails");
            this.dgvworkdetails.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvworkdetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvworkdetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cell_id,
            this.cell_workno,
            this.cell_workname,
            this.cell_worktype,
            this.cell_cust,
            this.iscomplete});
            this.dgvworkdetails.Name = "dgvworkdetails";
            this.dgvworkdetails.ReadOnly = true;
            this.dgvworkdetails.RowHeadersVisible = false;
            this.dgvworkdetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvworkdetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvworkdetails_CellDoubleClick);
            // 
            // cell_id
            // 
            resources.ApplyResources(this.cell_id, "cell_id");
            this.cell_id.Name = "cell_id";
            this.cell_id.ReadOnly = true;
            // 
            // cell_workno
            // 
            this.cell_workno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.cell_workno, "cell_workno");
            this.cell_workno.Name = "cell_workno";
            this.cell_workno.ReadOnly = true;
            // 
            // cell_workname
            // 
            this.cell_workname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.cell_workname, "cell_workname");
            this.cell_workname.Name = "cell_workname";
            this.cell_workname.ReadOnly = true;
            // 
            // cell_worktype
            // 
            resources.ApplyResources(this.cell_worktype, "cell_worktype");
            this.cell_worktype.Name = "cell_worktype";
            this.cell_worktype.ReadOnly = true;
            // 
            // cell_cust
            // 
            resources.ApplyResources(this.cell_cust, "cell_cust");
            this.cell_cust.Name = "cell_cust";
            this.cell_cust.ReadOnly = true;
            // 
            // iscomplete
            // 
            resources.ApplyResources(this.iscomplete, "iscomplete");
            this.iscomplete.Name = "iscomplete";
            this.iscomplete.ReadOnly = true;
            // 
            // cmbworktype
            // 
            this.cmbworktype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbworktype.ForeColor = System.Drawing.Color.Maroon;
            this.cmbworktype.FormattingEnabled = true;
            resources.ApplyResources(this.cmbworktype, "cmbworktype");
            this.cmbworktype.Name = "cmbworktype";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Name = "label10";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Name = "label7";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Name = "label14";
            // 
            // lblid
            // 
            resources.ApplyResources(this.lblid, "lblid");
            this.lblid.Name = "lblid";
            // 
            // btnclear
            // 
            resources.ApplyResources(this.btnclear, "btnclear");
            this.btnclear.ForeColor = System.Drawing.Color.Maroon;
            this.btnclear.Name = "btnclear";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnCommand
            // 
            resources.ApplyResources(this.btnCommand, "btnCommand");
            this.btnCommand.ForeColor = System.Drawing.Color.Maroon;
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.UseVisualStyleBackColor = true;
            this.btnCommand.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // txtworkname
            // 
            this.txtworkname.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.txtworkname, "txtworkname");
            this.txtworkname.Name = "txtworkname";
            // 
            // txtworkno
            // 
            this.txtworkno.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.txtworkno, "txtworkno");
            this.txtworkno.Name = "txtworkno";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.ForeColor = System.Drawing.Color.Maroon;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // WorkMaster
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.tabControl1);
            this.Name = "WorkMaster";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WorkMaster_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobSite)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvworkdetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtworkname;
        private System.Windows.Forms.TextBox txtworkno;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Button btnCommand;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblid;
        private System.Windows.Forms.ComboBox cmbworktype;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvworkdetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmboprdivision;
        private System.Windows.Forms.ComboBox cmboprjurisdiction;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtoprlongitude;
        private System.Windows.Forms.TextBox txtoprlatitude;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dgvJobSite;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbCust;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn srNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn jsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.CheckBox chkisCompleted;
        private System.Windows.Forms.Button btnSaveSite;
        private System.Windows.Forms.Button btnDeleteWO;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_workno;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_workname;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_worktype;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_cust;
        private System.Windows.Forms.DataGridViewTextBoxColumn iscomplete;
        private System.Windows.Forms.Button btnClose;
    }
}