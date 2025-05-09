namespace Uniproject
{
    partial class frmEagleBatchMAster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEagleBatchMAster));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plantInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designMixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipperInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblexpire = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.GhostWhite;
            this.menuStrip1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.setUpToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.minimizeTSMI,
            this.quitToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1347, 36);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(72, 32);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // setUpToolStripMenuItem
            // 
            this.setUpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plantInformationToolStripMenuItem,
            this.designMixToolStripMenuItem,
            this.tipperInformationToolStripMenuItem,
            this.recipeMasterToolStripMenuItem});
            this.setUpToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setUpToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.setUpToolStripMenuItem.Name = "setUpToolStripMenuItem";
            this.setUpToolStripMenuItem.Size = new System.Drawing.Size(103, 32);
            this.setUpToolStripMenuItem.Text = "Masters";
            // 
            // plantInformationToolStripMenuItem
            // 
            this.plantInformationToolStripMenuItem.Name = "plantInformationToolStripMenuItem";
            this.plantInformationToolStripMenuItem.Size = new System.Drawing.Size(236, 32);
            this.plantInformationToolStripMenuItem.Text = "Plant Master";
            this.plantInformationToolStripMenuItem.Click += new System.EventHandler(this.plantInformationToolStripMenuItem_Click);
            // 
            // designMixToolStripMenuItem
            // 
            this.designMixToolStripMenuItem.Name = "designMixToolStripMenuItem";
            this.designMixToolStripMenuItem.Size = new System.Drawing.Size(236, 32);
            this.designMixToolStripMenuItem.Text = "Work Master";
            this.designMixToolStripMenuItem.Click += new System.EventHandler(this.designMixToolStripMenuItem_Click);
            // 
            // tipperInformationToolStripMenuItem
            // 
            this.tipperInformationToolStripMenuItem.Name = "tipperInformationToolStripMenuItem";
            this.tipperInformationToolStripMenuItem.Size = new System.Drawing.Size(236, 32);
            this.tipperInformationToolStripMenuItem.Text = "Tipper Master";
            this.tipperInformationToolStripMenuItem.Click += new System.EventHandler(this.tripperInformationToolStripMenuItem_Click);
            // 
            // recipeMasterToolStripMenuItem
            // 
            this.recipeMasterToolStripMenuItem.Name = "recipeMasterToolStripMenuItem";
            this.recipeMasterToolStripMenuItem.Size = new System.Drawing.Size(236, 32);
            this.recipeMasterToolStripMenuItem.Text = "Recipe Master";
            //this.recipeMasterToolStripMenuItem.Click += new System.EventHandler(this.recipeMasterToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(91, 32);
            this.reportToolStripMenuItem.Text = "Report";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // minimizeTSMI
            // 
            this.minimizeTSMI.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeTSMI.ForeColor = System.Drawing.Color.Maroon;
            this.minimizeTSMI.Name = "minimizeTSMI";
            this.minimizeTSMI.Size = new System.Drawing.Size(116, 32);
            this.minimizeTSMI.Text = "Minimize";
            this.minimizeTSMI.Click += new System.EventHandler(this.minimizeTSMI_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(68, 32);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(14, 32);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblexpire
            // 
            this.lblexpire.AutoSize = true;
            this.lblexpire.BackColor = System.Drawing.Color.DarkCyan;
            this.lblexpire.Font = new System.Drawing.Font("Calibri", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblexpire.ForeColor = System.Drawing.Color.White;
            this.lblexpire.Location = new System.Drawing.Point(586, 52);
            this.lblexpire.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblexpire.Name = "lblexpire";
            this.lblexpire.Size = new System.Drawing.Size(87, 35);
            this.lblexpire.TabIndex = 7;
            this.lblexpire.Text = "label1";
            // 
            // frmEagleBatchMAster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1347, 672);
            this.ControlBox = false;
            this.Controls.Add(this.lblexpire);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEagleBatchMAster";
            this.Text = "Hot Mix SCADA ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEagleBatchMAster_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plantInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeTSMI;
        private System.Windows.Forms.ToolStripMenuItem designMixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipperInformationToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem recipeMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblexpire;
    }
}