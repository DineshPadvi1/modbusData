using System;
using Uniproject.Classes;

namespace Uniproject
{
    partial class mdiMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.internetStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFirewallStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.FireWallStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblServiceStatusPWD = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblServiceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblservicestatus_ = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stripAbout = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDateRenewal = new System.Windows.Forms.ToolStripStatusLabel();
            this.stripHelp = new System.Windows.Forms.ToolStripStatusLabel();
            this.internetStatusName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHeading = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTipExpiryDate = new System.Windows.Forms.ToolTip(this.components);
            this.updateBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pnl_left = new System.Windows.Forms.Panel();
            this.errorBatchCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.countUploaded = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.countPending = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPlcComm = new System.Windows.Forms.Button();
            this.btnSelectDept = new System.Windows.Forms.Button();
            this.SW_Config = new System.Windows.Forms.Button();
            this.btnPLCsetting = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnWOfromAPI = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_ScadaPortSetting = new System.Windows.Forms.Button();
            this.dgv_history = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.export = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pending = new System.Windows.Forms.DataGridView();
            this.col_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_workcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_export = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_uplaoder = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.uploadInstruction = new System.Windows.Forms.Label();
            this.lb_workDept = new System.Windows.Forms.Label();
            this.lb_macId = new System.Windows.Forms.Label();
            this.expiryMessage = new System.Windows.Forms.Label();
            this.PlantInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.WO_Mst = new System.Windows.Forms.ToolStripMenuItem();
            this.Customer_Contractor = new System.Windows.Forms.ToolStripMenuItem();
            this.ReceipeMst = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleMst = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.picPlantInfo = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.pnl_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_history)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pending)).BeginInit();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlantInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internetStatus,
            this.lblFirewallStatus,
            this.FireWallStatus,
            this.toolStripStatusLabel2,
            this.lblServiceStatusPWD,
            this.toolStripStatusLabel3,
            this.lblServiceStatus,
            this.lblservicestatus_,
            this.lblServerStatus,
            this.toolStripStatusLabel5,
            this.stripAbout,
            this.lblDateRenewal,
            this.stripHelp});
            this.statusStrip1.Location = new System.Drawing.Point(0, 871);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1742, 7);
            this.statusStrip1.TabIndex = 7;
            // 
            // internetStatus
            // 
            this.internetStatus.Name = "internetStatus";
            this.internetStatus.Size = new System.Drawing.Size(0, 1);
            // 
            // lblFirewallStatus
            // 
            this.lblFirewallStatus.Name = "lblFirewallStatus";
            this.lblFirewallStatus.Size = new System.Drawing.Size(0, 1);
            this.lblFirewallStatus.Click += new System.EventHandler(this.lblFirewallStatus_Click);
            // 
            // FireWallStatus
            // 
            this.FireWallStatus.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FireWallStatus.Name = "FireWallStatus";
            this.FireWallStatus.Size = new System.Drawing.Size(62, 1);
            this.FireWallStatus.Text = ":Firewall";
            this.FireWallStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 1);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // lblServiceStatusPWD
            // 
            this.lblServiceStatusPWD.Name = "lblServiceStatusPWD";
            this.lblServiceStatusPWD.Size = new System.Drawing.Size(0, 1);
            this.lblServiceStatusPWD.Visible = false;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 1);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // lblServiceStatus
            // 
            this.lblServiceStatus.Name = "lblServiceStatus";
            this.lblServiceStatus.Size = new System.Drawing.Size(0, 1);
            // 
            // lblservicestatus_
            // 
            this.lblservicestatus_.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblservicestatus_.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblservicestatus_.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblservicestatus_.Name = "lblservicestatus_";
            this.lblservicestatus_.RightToLeftAutoMirrorImage = true;
            this.lblservicestatus_.Size = new System.Drawing.Size(70, 1);
            this.lblservicestatus_.Text = ":Uploader";
            this.lblservicestatus_.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblservicestatus_.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.lblservicestatus_.Click += new System.EventHandler(this.lblservicestatus_Click);
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(0, 1);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.RightToLeftAutoMirrorImage = true;
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(52, 1);
            this.toolStripStatusLabel5.Text = ":Server";
            this.toolStripStatusLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel5.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // stripAbout
            // 
            this.stripAbout.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stripAbout.Name = "stripAbout";
            this.stripAbout.Size = new System.Drawing.Size(52, 1);
            this.stripAbout.Text = "ABOUT";
            // 
            // lblDateRenewal
            // 
            this.lblDateRenewal.Name = "lblDateRenewal";
            this.lblDateRenewal.Size = new System.Drawing.Size(0, 1);
            this.lblDateRenewal.Visible = false;
            // 
            // stripHelp
            // 
            this.stripHelp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stripHelp.Name = "stripHelp";
            this.stripHelp.Size = new System.Drawing.Size(38, 1);
            this.stripHelp.Text = "HELP";
            // 
            // internetStatusName
            // 
            this.internetStatusName.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.internetStatusName.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.internetStatusName.Name = "internetStatusName";
            this.internetStatusName.Size = new System.Drawing.Size(184, 15);
            this.internetStatusName.Text = "Internet Connection  Status :";
            this.internetStatusName.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Blue;
            this.lblHeading.Location = new System.Drawing.Point(387, 2);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(339, 24);
            this.lblHeading.TabIndex = 22;
            this.lblHeading.Text = "Batching Data Synchronisation - SCADA";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // updateBox
            // 
            this.updateBox.BackColor = System.Drawing.Color.AliceBlue;
            this.updateBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.updateBox.Enabled = false;
            this.updateBox.Font = new System.Drawing.Font("Calibri", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBox.Location = new System.Drawing.Point(1441, 116);
            this.updateBox.Name = "updateBox";
            this.updateBox.ReadOnly = true;
            this.updateBox.Size = new System.Drawing.Size(257, 90);
            this.updateBox.TabIndex = 30;
            this.updateBox.Text = "Unipro: v2.0.1.2\nUniUploader: v2.0.1.1\nIf You Not Having The Updated Version Then" +
    " Please Contact With Vasundhara Support Person";
            this.updateBox.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(10, 10);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1742, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "Work Links";
            // 
            // pnl_left
            // 
            this.pnl_left.Controls.Add(this.errorBatchCount);
            this.pnl_left.Controls.Add(this.label1);
            this.pnl_left.Controls.Add(this.countUploaded);
            this.pnl_left.Controls.Add(this.label8);
            this.pnl_left.Controls.Add(this.countPending);
            this.pnl_left.Controls.Add(this.label6);
            this.pnl_left.Controls.Add(this.btnPlcComm);
            this.pnl_left.Controls.Add(this.btnSelectDept);
            this.pnl_left.Controls.Add(this.SW_Config);
            this.pnl_left.Controls.Add(this.btnPLCsetting);
            this.pnl_left.Controls.Add(this.label2);
            this.pnl_left.Controls.Add(this.picPlantInfo);
            this.pnl_left.Controls.Add(this.btnWOfromAPI);
            this.pnl_left.Controls.Add(this.button4);
            this.pnl_left.Controls.Add(this.btn_ScadaPortSetting);
            this.pnl_left.Controls.Add(this.dgv_history);
            this.pnl_left.Controls.Add(this.dgv_pending);
            this.pnl_left.Controls.Add(this.button2);
            this.pnl_left.Controls.Add(this.btn_uplaoder);
            this.pnl_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_left.Location = new System.Drawing.Point(0, 24);
            this.pnl_left.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_left.Name = "pnl_left";
            this.pnl_left.Size = new System.Drawing.Size(134, 847);
            this.pnl_left.TabIndex = 34;
            // 
            // errorBatchCount
            // 
            this.errorBatchCount.AutoSize = true;
            this.errorBatchCount.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorBatchCount.ForeColor = System.Drawing.Color.Red;
            this.errorBatchCount.Location = new System.Drawing.Point(82, 407);
            this.errorBatchCount.Name = "errorBatchCount";
            this.errorBatchCount.Size = new System.Drawing.Size(28, 15);
            this.errorBatchCount.TabIndex = 40;
            this.errorBatchCount.Text = "000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(21, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 39;
            this.label1.Text = "Error: ";
            // 
            // countUploaded
            // 
            this.countUploaded.AutoSize = true;
            this.countUploaded.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countUploaded.ForeColor = System.Drawing.Color.Green;
            this.countUploaded.Location = new System.Drawing.Point(82, 439);
            this.countUploaded.Name = "countUploaded";
            this.countUploaded.Size = new System.Drawing.Size(28, 15);
            this.countUploaded.TabIndex = 46;
            this.countUploaded.Text = "000";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(21, 439);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 15);
            this.label8.TabIndex = 45;
            this.label8.Text = "Uploaded:";
            // 
            // countPending
            // 
            this.countPending.AutoSize = true;
            this.countPending.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countPending.ForeColor = System.Drawing.Color.MediumBlue;
            this.countPending.Location = new System.Drawing.Point(82, 423);
            this.countPending.Name = "countPending";
            this.countPending.Size = new System.Drawing.Size(28, 15);
            this.countPending.TabIndex = 44;
            this.countPending.Text = "000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MediumBlue;
            this.label6.Location = new System.Drawing.Point(21, 423);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 43;
            this.label6.Text = "Pending:";
            // 
            // btnPlcComm
            // 
            this.btnPlcComm.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlcComm.Location = new System.Drawing.Point(3, 653);
            this.btnPlcComm.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlcComm.Name = "btnPlcComm";
            this.btnPlcComm.Size = new System.Drawing.Size(126, 24);
            this.btnPlcComm.TabIndex = 38;
            this.btnPlcComm.Text = "ModBus Test";
            this.btnPlcComm.UseVisualStyleBackColor = true;
            this.btnPlcComm.Visible = false;
            this.btnPlcComm.Click += new System.EventHandler(this.btnPlcComm_Click);
            // 
            // btnSelectDept
            // 
            this.btnSelectDept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDept.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectDept.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSelectDept.Location = new System.Drawing.Point(4, 551);
            this.btnSelectDept.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectDept.Name = "btnSelectDept";
            this.btnSelectDept.Size = new System.Drawing.Size(126, 34);
            this.btnSelectDept.TabIndex = 37;
            this.btnSelectDept.Text = "Select Dept";
            this.btnSelectDept.UseVisualStyleBackColor = true;
            this.btnSelectDept.Visible = false;
            // 
            // SW_Config
            // 
            this.SW_Config.BackColor = System.Drawing.Color.SteelBlue;
            this.SW_Config.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SW_Config.ForeColor = System.Drawing.Color.White;
            this.SW_Config.Location = new System.Drawing.Point(4, 459);
            this.SW_Config.Margin = new System.Windows.Forms.Padding(2);
            this.SW_Config.Name = "SW_Config";
            this.SW_Config.Size = new System.Drawing.Size(126, 28);
            this.SW_Config.TabIndex = 35;
            this.SW_Config.Text = "SW Config";
            this.SW_Config.UseVisualStyleBackColor = false;
            this.SW_Config.Click += new System.EventHandler(this.SW_Config_Click);
            // 
            // btnPLCsetting
            // 
            this.btnPLCsetting.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPLCsetting.Location = new System.Drawing.Point(4, 589);
            this.btnPLCsetting.Margin = new System.Windows.Forms.Padding(2);
            this.btnPLCsetting.Name = "btnPLCsetting";
            this.btnPLCsetting.Size = new System.Drawing.Size(126, 28);
            this.btnPLCsetting.TabIndex = 33;
            this.btnPLCsetting.Text = "PORT/PLC Setting";
            this.btnPLCsetting.UseVisualStyleBackColor = true;
            this.btnPLCsetting.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(416, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(339, 24);
            this.label2.TabIndex = 22;
            this.label2.Text = "Batching Data Synchronisation - SCADA";
            // 
            // btnWOfromAPI
            // 
            this.btnWOfromAPI.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWOfromAPI.Location = new System.Drawing.Point(4, 491);
            this.btnWOfromAPI.Margin = new System.Windows.Forms.Padding(2);
            this.btnWOfromAPI.Name = "btnWOfromAPI";
            this.btnWOfromAPI.Size = new System.Drawing.Size(126, 24);
            this.btnWOfromAPI.TabIndex = 27;
            this.btnWOfromAPI.Text = "WO Sync";
            this.btnWOfromAPI.UseVisualStyleBackColor = true;
            this.btnWOfromAPI.Click += new System.EventHandler(this.btnWOfromAPI_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(4, 519);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(126, 28);
            this.button4.TabIndex = 11;
            this.button4.Text = "Parameters";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            // 
            // btn_ScadaPortSetting
            // 
            this.btn_ScadaPortSetting.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ScadaPortSetting.Location = new System.Drawing.Point(4, 621);
            this.btn_ScadaPortSetting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ScadaPortSetting.Name = "btn_ScadaPortSetting";
            this.btn_ScadaPortSetting.Size = new System.Drawing.Size(126, 28);
            this.btn_ScadaPortSetting.TabIndex = 11;
            this.btn_ScadaPortSetting.Text = "Port Setting";
            this.btn_ScadaPortSetting.UseVisualStyleBackColor = true;
            this.btn_ScadaPortSetting.Visible = false;
            // 
            // dgv_history
            // 
            this.dgv_history.BackgroundColor = System.Drawing.Color.White;
            this.dgv_history.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_history.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.workcode,
            this.export});
            this.dgv_history.Location = new System.Drawing.Point(0, 279);
            this.dgv_history.Name = "dgv_history";
            this.dgv_history.RowHeadersVisible = false;
            this.dgv_history.RowHeadersWidth = 62;
            this.dgv_history.Size = new System.Drawing.Size(131, 124);
            this.dgv_history.TabIndex = 25;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 8;
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 40;
            // 
            // workcode
            // 
            this.workcode.HeaderText = "Uploaded";
            this.workcode.MinimumWidth = 8;
            this.workcode.Name = "workcode";
            this.workcode.Width = 50;
            // 
            // export
            // 
            this.export.HeaderText = "WorkCode";
            this.export.MinimumWidth = 8;
            this.export.Name = "export";
            this.export.Width = 120;
            // 
            // dgv_pending
            // 
            this.dgv_pending.BackgroundColor = System.Drawing.Color.White;
            this.dgv_pending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pending.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_ID,
            this.col_workcode,
            this.col_export});
            this.dgv_pending.Location = new System.Drawing.Point(0, 147);
            this.dgv_pending.Name = "dgv_pending";
            this.dgv_pending.RowHeadersVisible = false;
            this.dgv_pending.RowHeadersWidth = 62;
            this.dgv_pending.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_pending.Size = new System.Drawing.Size(131, 130);
            this.dgv_pending.TabIndex = 23;
            // 
            // col_ID
            // 
            this.col_ID.HeaderText = "ID";
            this.col_ID.MinimumWidth = 8;
            this.col_ID.Name = "col_ID";
            this.col_ID.Visible = false;
            this.col_ID.Width = 40;
            // 
            // col_workcode
            // 
            this.col_workcode.HeaderText = "Pending";
            this.col_workcode.MinimumWidth = 8;
            this.col_workcode.Name = "col_workcode";
            this.col_workcode.Width = 50;
            // 
            // col_export
            // 
            this.col_export.HeaderText = "WorkCode";
            this.col_export.MinimumWidth = 8;
            this.col_export.Name = "col_export";
            this.col_export.Width = 120;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 19);
            this.button2.TabIndex = 30;
            // 
            // btn_uplaoder
            // 
            this.btn_uplaoder.BackColor = System.Drawing.Color.Lavender;
            this.btn_uplaoder.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_uplaoder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_uplaoder.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_uplaoder.Location = new System.Drawing.Point(4, 113);
            this.btn_uplaoder.Margin = new System.Windows.Forms.Padding(2);
            this.btn_uplaoder.Name = "btn_uplaoder";
            this.btn_uplaoder.Size = new System.Drawing.Size(126, 32);
            this.btn_uplaoder.TabIndex = 1;
            this.btn_uplaoder.Text = "Upload Status";
            this.btn_uplaoder.UseVisualStyleBackColor = false;
            this.btn_uplaoder.Click += new System.EventHandler(this.btn_uplaoder_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.Color.White;
            this.menuStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(80, 60);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlantInfo,
            this.WO_Mst,
            this.Customer_Contractor,
            this.ReceipeMst,
            this.VehicleMst,
            this.ReportShow,
            this.toolStripMenuItem1});
            this.menuStrip2.Location = new System.Drawing.Point(134, 24);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip2.Size = new System.Drawing.Size(1608, 89);
            this.menuStrip2.TabIndex = 35;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // uploadInstruction
            // 
            this.uploadInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadInstruction.AutoSize = true;
            this.uploadInstruction.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadInstruction.ForeColor = System.Drawing.Color.DarkGreen;
            this.uploadInstruction.Location = new System.Drawing.Point(1347, 9);
            this.uploadInstruction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.uploadInstruction.Name = "uploadInstruction";
            this.uploadInstruction.Size = new System.Drawing.Size(171, 24);
            this.uploadInstruction.TabIndex = 40;
            this.uploadInstruction.Text = "Please upload data";
            // 
            // lb_workDept
            // 
            this.lb_workDept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_workDept.AutoSize = true;
            this.lb_workDept.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_workDept.ForeColor = System.Drawing.Color.DarkGreen;
            this.lb_workDept.Location = new System.Drawing.Point(1347, 82);
            this.lb_workDept.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_workDept.Name = "lb_workDept";
            this.lb_workDept.Size = new System.Drawing.Size(117, 24);
            this.lb_workDept.TabIndex = 39;
            this.lb_workDept.Text = "Work Dept : ";
            // 
            // lb_macId
            // 
            this.lb_macId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_macId.AutoSize = true;
            this.lb_macId.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_macId.ForeColor = System.Drawing.Color.DarkGreen;
            this.lb_macId.Location = new System.Drawing.Point(1347, 58);
            this.lb_macId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_macId.Name = "lb_macId";
            this.lb_macId.Size = new System.Drawing.Size(79, 24);
            this.lb_macId.TabIndex = 38;
            this.lb_macId.Text = "MacID : ";
            // 
            // expiryMessage
            // 
            this.expiryMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.expiryMessage.AutoSize = true;
            this.expiryMessage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expiryMessage.ForeColor = System.Drawing.Color.DarkGreen;
            this.expiryMessage.Location = new System.Drawing.Point(1347, 34);
            this.expiryMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.expiryMessage.Name = "expiryMessage";
            this.expiryMessage.Size = new System.Drawing.Size(388, 24);
            this.expiryMessage.TabIndex = 37;
            this.expiryMessage.Text = "Software is expired from days, please renew!";
            this.expiryMessage.Visible = false;
            // 
            // PlantInfo
            // 
            this.PlantInfo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlantInfo.Image = global::modbusData.Properties.Resources.icons8_foundation_501;
            this.PlantInfo.Name = "PlantInfo";
            this.PlantInfo.Size = new System.Drawing.Size(133, 85);
            this.PlantInfo.Text = "Live Batch Data";
            this.PlantInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.PlantInfo.Click += new System.EventHandler(this.PlantInfo_Click_1);
            // 
            // WO_Mst
            // 
            this.WO_Mst.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WO_Mst.Image = global::modbusData.Properties.Resources.icons8_remote_64;
            this.WO_Mst.Name = "WO_Mst";
            this.WO_Mst.Size = new System.Drawing.Size(107, 85);
            this.WO_Mst.Text = "Work Order";
            this.WO_Mst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.WO_Mst.Click += new System.EventHandler(this.WO_Mst_Click);
            // 
            // Customer_Contractor
            // 
            this.Customer_Contractor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Customer_Contractor.Image = global::modbusData.Properties.Resources.icons8_contractor_48;
            this.Customer_Contractor.Name = "Customer_Contractor";
            this.Customer_Contractor.Size = new System.Drawing.Size(99, 85);
            this.Customer_Contractor.Text = "Contractor";
            this.Customer_Contractor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Customer_Contractor.Click += new System.EventHandler(this.Customer_Contractor_Click);
            // 
            // ReceipeMst
            // 
            this.ReceipeMst.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReceipeMst.Image = global::modbusData.Properties.Resources.icons8_material_641;
            this.ReceipeMst.Name = "ReceipeMst";
            this.ReceipeMst.Size = new System.Drawing.Size(127, 85);
            this.ReceipeMst.Text = "Design/Recipe";
            this.ReceipeMst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ReceipeMst.Click += new System.EventHandler(this.ReceipeMst_Click_1);
            // 
            // VehicleMst
            // 
            this.VehicleMst.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VehicleMst.Image = global::modbusData.Properties.Resources.icons8_concrete_mixer_641;
            this.VehicleMst.Name = "VehicleMst";
            this.VehicleMst.Size = new System.Drawing.Size(94, 85);
            this.VehicleMst.Text = "Vehicle";
            this.VehicleMst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VehicleMst.Click += new System.EventHandler(this.VehicleMst_Click_1);
            // 
            // ReportShow
            // 
            this.ReportShow.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportShow.Image = global::modbusData.Properties.Resources.report;
            this.ReportShow.Name = "ReportShow";
            this.ReportShow.Size = new System.Drawing.Size(94, 85);
            this.ReportShow.Text = "Reports";
            this.ReportShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ReportShow.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.Image = global::modbusData.Properties.Resources.exe;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(94, 85);
            this.toolStripMenuItem1.Text = "Website";
            this.toolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMenuItem1.Visible = false;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // picPlantInfo
            // 
            this.picPlantInfo.Image = global::modbusData.Properties.Resources.compLogo1;
            this.picPlantInfo.Location = new System.Drawing.Point(0, 0);
            this.picPlantInfo.Margin = new System.Windows.Forms.Padding(2);
            this.picPlantInfo.Name = "picPlantInfo";
            this.picPlantInfo.Size = new System.Drawing.Size(134, 109);
            this.picPlantInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPlantInfo.TabIndex = 28;
            this.picPlantInfo.TabStop = false;
            // 
            // mdiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1742, 878);
            this.Controls.Add(this.uploadInstruction);
            this.Controls.Add(this.lb_workDept);
            this.Controls.Add(this.lb_macId);
            this.Controls.Add(this.expiryMessage);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.pnl_left);
            this.Controls.Add(this.updateBox);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "mdiMain";
            this.Text = "RMC_SCADA_Phoenix";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mdiMain_FormClosed);
            this.Load += new System.EventHandler(this.mdiMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnl_left.ResumeLayout(false);
            this.pnl_left.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_history)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pending)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlantInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.ToolStripStatusLabel lblDateRenewal;
        private System.Windows.Forms.ToolStripStatusLabel internetStatusName;
        private System.Windows.Forms.ToolStripStatusLabel FireWallStatus;
        private System.Windows.Forms.ToolStripStatusLabel internetStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblFirewallStatus;
        private System.Windows.Forms.ToolStripStatusLabel stripAbout;
        private System.Windows.Forms.ToolTip toolTipExpiryDate;
        //private System.Windows.Forms.ToolStripStatusLabel lblDateRenewal;
        private System.Windows.Forms.ToolStripStatusLabel lblServiceStatus;
        public System.Windows.Forms.ToolStripStatusLabel lblservicestatus_;
        private System.Windows.Forms.RichTextBox updateBox;
        private System.Windows.Forms.ToolStripStatusLabel lblServerStatus;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.Label InstallDateLabel;
        private System.Windows.Forms.Label lbInstallationDate;
        private System.Windows.Forms.ToolStripStatusLabel lblServiceStatusPWD;
        private System.Windows.Forms.ToolStripStatusLabel stripHelp;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel pnl_left;
        private System.Windows.Forms.Label errorBatchCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label countUploaded;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label countPending;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPlcComm;
        private System.Windows.Forms.Button btnSelectDept;
        private System.Windows.Forms.Button SW_Config;
        private System.Windows.Forms.Button btnPLCsetting;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.PictureBox picPlantInfo;
        private System.Windows.Forms.Button btnWOfromAPI;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_ScadaPortSetting;
        private System.Windows.Forms.DataGridView dgv_history;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn workcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn export;
        private System.Windows.Forms.DataGridView dgv_pending;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_workcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_export;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_uplaoder;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem PlantInfo;
        private System.Windows.Forms.ToolStripMenuItem WO_Mst;
        private System.Windows.Forms.ToolStripMenuItem Customer_Contractor;
        private System.Windows.Forms.ToolStripMenuItem ReceipeMst;
        private System.Windows.Forms.ToolStripMenuItem VehicleMst;
        private System.Windows.Forms.ToolStripMenuItem ReportShow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label uploadInstruction;
        private System.Windows.Forms.Label lb_workDept;
        private System.Windows.Forms.Label lb_macId;
        private System.Windows.Forms.Label expiryMessage;
    }
}


