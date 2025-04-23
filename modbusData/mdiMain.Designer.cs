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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.ReportShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblHeading = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.expiryMessage = new System.Windows.Forms.Label();
            this.lb_macId = new System.Windows.Forms.Label();
            this.toolTipExpiryDate = new System.Windows.Forms.ToolTip(this.components);
            this.lb_workDept = new System.Windows.Forms.Label();
            this.updateBox = new System.Windows.Forms.RichTextBox();
            this.uploadInstruction = new System.Windows.Forms.Label();
            this.PlantInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.WO_Mst = new System.Windows.Forms.ToolStripMenuItem();
            this.Customer_Contractor = new System.Windows.Forms.ToolStripMenuItem();
            this.ReceipeMst = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleMst = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
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
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(10, 10);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1742, 30);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "Work Links";
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
            this.menuStrip2.Location = new System.Drawing.Point(0, 30);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip2.Size = new System.Drawing.Size(1742, 89);
            this.menuStrip2.TabIndex = 20;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // ReportShow
            // 
            this.ReportShow.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportShow.Name = "ReportShow";
            this.ReportShow.Size = new System.Drawing.Size(80, 85);
            this.ReportShow.Text = "Reports";
            this.ReportShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ReportShow.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(82, 85);
            this.toolStripMenuItem1.Text = "Website";
            this.toolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMenuItem1.Visible = false;
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
            // expiryMessage
            // 
            this.expiryMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.expiryMessage.AutoSize = true;
            this.expiryMessage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expiryMessage.ForeColor = System.Drawing.Color.DarkGreen;
            this.expiryMessage.Location = new System.Drawing.Point(1359, 29);
            this.expiryMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.expiryMessage.Name = "expiryMessage";
            this.expiryMessage.Size = new System.Drawing.Size(388, 24);
            this.expiryMessage.TabIndex = 24;
            this.expiryMessage.Text = "Software is expired from days, please renew!";
            this.expiryMessage.Visible = false;
            // 
            // lb_macId
            // 
            this.lb_macId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_macId.AutoSize = true;
            this.lb_macId.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_macId.ForeColor = System.Drawing.Color.DarkGreen;
            this.lb_macId.Location = new System.Drawing.Point(1359, 53);
            this.lb_macId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_macId.Name = "lb_macId";
            this.lb_macId.Size = new System.Drawing.Size(79, 24);
            this.lb_macId.TabIndex = 26;
            this.lb_macId.Text = "MacID : ";
            this.lb_macId.Visible = false;
            // 
            // lb_workDept
            // 
            this.lb_workDept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_workDept.AutoSize = true;
            this.lb_workDept.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_workDept.ForeColor = System.Drawing.Color.DarkGreen;
            this.lb_workDept.Location = new System.Drawing.Point(1359, 77);
            this.lb_workDept.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_workDept.Name = "lb_workDept";
            this.lb_workDept.Size = new System.Drawing.Size(117, 24);
            this.lb_workDept.TabIndex = 28;
            this.lb_workDept.Text = "Work Dept : ";
            this.lb_workDept.Visible = false;
            // 
            // updateBox
            // 
            this.updateBox.BackColor = System.Drawing.Color.AliceBlue;
            this.updateBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.updateBox.Enabled = false;
            this.updateBox.Font = new System.Drawing.Font("Calibri", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBox.Location = new System.Drawing.Point(875, 12);
            this.updateBox.Name = "updateBox";
            this.updateBox.ReadOnly = true;
            this.updateBox.Size = new System.Drawing.Size(257, 90);
            this.updateBox.TabIndex = 30;
            this.updateBox.Text = "Unipro: v2.0.1.2\nUniUploader: v2.0.1.1\nIf You Not Having The Updated Version Then" +
    " Please Contact With Vasundhara Support Person";
            this.updateBox.Visible = false;
            // 
            // uploadInstruction
            // 
            this.uploadInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadInstruction.AutoSize = true;
            this.uploadInstruction.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadInstruction.ForeColor = System.Drawing.Color.DarkGreen;
            this.uploadInstruction.Location = new System.Drawing.Point(1359, 4);
            this.uploadInstruction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.uploadInstruction.Name = "uploadInstruction";
            this.uploadInstruction.Size = new System.Drawing.Size(171, 24);
            this.uploadInstruction.TabIndex = 32;
            this.uploadInstruction.Text = "Please upload data";
            this.uploadInstruction.Visible = false;
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
            this.Controls.Add(this.updateBox);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.menuStrip2);
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
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem PlantInfo;
        private System.Windows.Forms.ToolStripMenuItem WO_Mst;
        private System.Windows.Forms.ToolStripMenuItem Customer_Contractor;
        private System.Windows.Forms.ToolStripMenuItem ReceipeMst;
        private System.Windows.Forms.ToolStripMenuItem VehicleMst;
        private System.Windows.Forms.ToolStripMenuItem ReportShow;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.ToolStripStatusLabel lblDateRenewal;
        private System.Windows.Forms.ToolStripStatusLabel internetStatusName;
        private System.Windows.Forms.ToolStripStatusLabel FireWallStatus;
        private System.Windows.Forms.ToolStripStatusLabel internetStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel lblFirewallStatus;
        private System.Windows.Forms.Label expiryMessage;
        private System.Windows.Forms.Label lb_macId;
        private System.Windows.Forms.ToolStripStatusLabel stripAbout;
        private System.Windows.Forms.ToolTip toolTipExpiryDate;
        //private System.Windows.Forms.ToolStripStatusLabel lblDateRenewal;
        private System.Windows.Forms.ToolStripStatusLabel lblServiceStatus;
        public System.Windows.Forms.ToolStripStatusLabel lblservicestatus_;
        private System.Windows.Forms.Label lb_workDept;
        private System.Windows.Forms.RichTextBox updateBox;
        private System.Windows.Forms.ToolStripStatusLabel lblServerStatus;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.Label InstallDateLabel;
        private System.Windows.Forms.Label lbInstallationDate;
        private System.Windows.Forms.ToolStripStatusLabel lblServiceStatusPWD;
        private System.Windows.Forms.Label uploadInstruction;
        private System.Windows.Forms.ToolStripStatusLabel stripHelp;
    }
}


