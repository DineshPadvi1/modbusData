namespace Uniproject.Masters
{
    partial class WO_API
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WO_API));
            this.btnGetPlant = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.workcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.concode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sitename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnGetAllWO = new System.Windows.Forms.Button();
            this.txtRowCount = new System.Windows.Forms.Label();
            this.lblSelectedRow = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.siteCount = new System.Windows.Forms.Label();
            this.woCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pCode = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetPlant
            // 
            this.btnGetPlant.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetPlant.Location = new System.Drawing.Point(52, 44);
            this.btnGetPlant.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetPlant.Name = "btnGetPlant";
            this.btnGetPlant.Size = new System.Drawing.Size(160, 39);
            this.btnGetPlant.TabIndex = 0;
            this.btnGetPlant.Text = "Get WO";
            this.btnGetPlant.UseVisualStyleBackColor = true;
            this.btnGetPlant.Click += new System.EventHandler(this.btnGetPlant_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(888, 606);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(163, 39);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(52, 606);
            this.btnImport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(163, 39);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "IMPORT";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(1068, 606);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(163, 39);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.workcode,
            this.workname,
            this.conname,
            this.concode,
            this.sitename,
            this.flag});
            this.dataGridView1.Location = new System.Drawing.Point(52, 107);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1179, 463);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // workcode
            // 
            this.workcode.HeaderText = "Work_Code";
            this.workcode.MinimumWidth = 6;
            this.workcode.Name = "workcode";
            this.workcode.ReadOnly = true;
            this.workcode.Width = 150;
            // 
            // workname
            // 
            this.workname.HeaderText = "Work_Name";
            this.workname.MinimumWidth = 6;
            this.workname.Name = "workname";
            this.workname.ReadOnly = true;
            this.workname.Width = 350;
            // 
            // conname
            // 
            this.conname.HeaderText = "Contractor_Name";
            this.conname.MinimumWidth = 6;
            this.conname.Name = "conname";
            this.conname.ReadOnly = true;
            this.conname.Width = 122;
            // 
            // concode
            // 
            this.concode.HeaderText = "Contractor_Code";
            this.concode.MinimumWidth = 6;
            this.concode.Name = "concode";
            this.concode.ReadOnly = true;
            this.concode.Width = 125;
            // 
            // sitename
            // 
            this.sitename.HeaderText = "Site_Name";
            this.sitename.MinimumWidth = 6;
            this.sitename.Name = "sitename";
            this.sitename.Width = 230;
            // 
            // flag
            // 
            this.flag.HeaderText = "flag";
            this.flag.MinimumWidth = 6;
            this.flag.Name = "flag";
            this.flag.Width = 50;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(1092, 43);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(139, 39);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(779, 43);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(288, 40);
            this.txtSearch.TabIndex = 14;
            // 
            // btnGetAllWO
            // 
            this.btnGetAllWO.Enabled = false;
            this.btnGetAllWO.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAllWO.Location = new System.Drawing.Point(233, 44);
            this.btnGetAllWO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetAllWO.Name = "btnGetAllWO";
            this.btnGetAllWO.Size = new System.Drawing.Size(160, 39);
            this.btnGetAllWO.TabIndex = 15;
            this.btnGetAllWO.Text = "Get All WO";
            this.btnGetAllWO.UseVisualStyleBackColor = true;
            this.btnGetAllWO.Click += new System.EventHandler(this.btnGetAllWO_Click);
            // 
            // txtRowCount
            // 
            this.txtRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRowCount.AutoEllipsis = true;
            this.txtRowCount.AutoSize = true;
            this.txtRowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRowCount.ForeColor = System.Drawing.Color.Red;
            this.txtRowCount.Location = new System.Drawing.Point(802, 610);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Size = new System.Drawing.Size(14, 18);
            this.txtRowCount.TabIndex = 16;
            this.txtRowCount.Text = "-";
            // 
            // lblSelectedRow
            // 
            this.lblSelectedRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectedRow.AutoEllipsis = true;
            this.lblSelectedRow.AutoSize = true;
            this.lblSelectedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedRow.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblSelectedRow.Location = new System.Drawing.Point(269, 610);
            this.lblSelectedRow.Name = "lblSelectedRow";
            this.lblSelectedRow.Size = new System.Drawing.Size(14, 18);
            this.lblSelectedRow.TabIndex = 17;
            this.lblSelectedRow.Text = "-";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(415, 671);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 22);
            this.label1.TabIndex = 19;
            this.label1.Text = "Sites in Site Master: ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(704, 671);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 22);
            this.label2.TabIndex = 20;
            this.label2.Text = "WO in WorkOrder:";
            // 
            // siteCount
            // 
            this.siteCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.siteCount.AutoEllipsis = true;
            this.siteCount.AutoSize = true;
            this.siteCount.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siteCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.siteCount.Location = new System.Drawing.Point(599, 671);
            this.siteCount.Name = "siteCount";
            this.siteCount.Size = new System.Drawing.Size(28, 22);
            this.siteCount.TabIndex = 21;
            this.siteCount.Text = "00";
            // 
            // woCount
            // 
            this.woCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.woCount.AutoEllipsis = true;
            this.woCount.AutoSize = true;
            this.woCount.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.woCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.woCount.Location = new System.Drawing.Point(871, 671);
            this.woCount.Name = "woCount";
            this.woCount.Size = new System.Drawing.Size(28, 22);
            this.woCount.TabIndex = 22;
            this.woCount.Text = "00";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(175, 671);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 22);
            this.label3.TabIndex = 23;
            this.label3.Text = "Plant code: ";
            // 
            // pCode
            // 
            this.pCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pCode.AutoEllipsis = true;
            this.pCode.AutoSize = true;
            this.pCode.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pCode.Location = new System.Drawing.Point(288, 671);
            this.pCode.Name = "pCode";
            this.pCode.Size = new System.Drawing.Size(16, 22);
            this.pCode.TabIndex = 24;
            this.pCode.Text = "-";
            // 
            // chkAll
            // 
            this.chkAll.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAll.Location = new System.Drawing.Point(419, 44);
            this.chkAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(107, 39);
            this.chkAll.TabIndex = 25;
            this.chkAll.Text = "Select All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.Click += new System.EventHandler(this.chkAll_Click);
            // 
            // WO_API
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1282, 725);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.pCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.woCount);
            this.Controls.Add(this.siteCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSelectedRow);
            this.Controls.Add(this.txtRowCount);
            this.Controls.Add(this.btnGetAllWO);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnGetPlant);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1300, 772);
            this.Name = "WO_API";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Work Order Synchronization";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WO_APL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnGetPlant;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnGetAllWO;
        private System.Windows.Forms.Label txtRowCount;
        private System.Windows.Forms.Label lblSelectedRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn workcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn workname;
        private System.Windows.Forms.DataGridViewTextBoxColumn conname;
        private System.Windows.Forms.DataGridViewTextBoxColumn concode;
        private System.Windows.Forms.DataGridViewTextBoxColumn sitename;
        private System.Windows.Forms.DataGridViewTextBoxColumn flag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label siteCount;
        private System.Windows.Forms.Label woCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pCode;
        private System.Windows.Forms.Button chkAll;
    }
}