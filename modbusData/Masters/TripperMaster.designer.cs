namespace Uniproject.Masters
{
    partial class TipperMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipperMaster));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDriver = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvtipperdetails = new System.Windows.Forms.DataGridView();
            this.cell_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_tripperno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_make = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_capacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_driver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.lblid = new System.Windows.Forms.Label();
            this.btnclear = new System.Windows.Forms.Button();
            this.btnCommand = new System.Windows.Forms.Button();
            this.txtcapacity = new System.Windows.Forms.TextBox();
            this.txtmake = new System.Windows.Forms.TextBox();
            this.txttipperno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtipperdetails)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1249, 631);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtDriver);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dgvtipperdetails);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.lblid);
            this.tabPage1.Controls.Add(this.btnclear);
            this.tabPage1.Controls.Add(this.btnCommand);
            this.tabPage1.Controls.Add(this.txtcapacity);
            this.tabPage1.Controls.Add(this.txtmake);
            this.tabPage1.Controls.Add(this.txttipperno);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 37);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1241, 590);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Save Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(321, 153);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 28);
            this.label6.TabIndex = 36;
            this.label6.Text = "in ton";
            // 
            // txtDriver
            // 
            this.txtDriver.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDriver.ForeColor = System.Drawing.Color.Maroon;
            this.txtDriver.Location = new System.Drawing.Point(160, 103);
            this.txtDriver.Margin = new System.Windows.Forms.Padding(4);
            this.txtDriver.MaxLength = 20;
            this.txtDriver.Name = "txtDriver";
            this.txtDriver.Size = new System.Drawing.Size(255, 36);
            this.txtDriver.TabIndex = 3;
            this.txtDriver.Text = "NA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 107);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 28);
            this.label5.TabIndex = 35;
            this.label5.Text = "Driver";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(107, 153);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 28);
            this.label4.TabIndex = 33;
            this.label4.Text = "*";
            // 
            // dgvtipperdetails
            // 
            this.dgvtipperdetails.AllowUserToAddRows = false;
            this.dgvtipperdetails.AllowUserToDeleteRows = false;
            this.dgvtipperdetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtipperdetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cell_id,
            this.cell_tripperno,
            this.cell_make,
            this.cell_capacity,
            this.cell_driver});
            this.dgvtipperdetails.Location = new System.Drawing.Point(467, 4);
            this.dgvtipperdetails.Margin = new System.Windows.Forms.Padding(4);
            this.dgvtipperdetails.Name = "dgvtipperdetails";
            this.dgvtipperdetails.ReadOnly = true;
            this.dgvtipperdetails.RowHeadersVisible = false;
            this.dgvtipperdetails.RowHeadersWidth = 51;
            this.dgvtipperdetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvtipperdetails.Size = new System.Drawing.Size(771, 583);
            this.dgvtipperdetails.TabIndex = 32;
            this.dgvtipperdetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvtripperdetails_CellDoubleClick);
            // 
            // cell_id
            // 
            this.cell_id.HeaderText = "ID";
            this.cell_id.MinimumWidth = 6;
            this.cell_id.Name = "cell_id";
            this.cell_id.ReadOnly = true;
            this.cell_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cell_id.Visible = false;
            this.cell_id.Width = 125;
            // 
            // cell_tripperno
            // 
            this.cell_tripperno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cell_tripperno.HeaderText = "Vehicle No";
            this.cell_tripperno.MinimumWidth = 6;
            this.cell_tripperno.Name = "cell_tripperno";
            this.cell_tripperno.ReadOnly = true;
            this.cell_tripperno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cell_make
            // 
            this.cell_make.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cell_make.HeaderText = "Make";
            this.cell_make.MinimumWidth = 6;
            this.cell_make.Name = "cell_make";
            this.cell_make.ReadOnly = true;
            this.cell_make.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cell_make.Width = 69;
            // 
            // cell_capacity
            // 
            this.cell_capacity.HeaderText = "Capacity";
            this.cell_capacity.MinimumWidth = 6;
            this.cell_capacity.Name = "cell_capacity";
            this.cell_capacity.ReadOnly = true;
            this.cell_capacity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cell_capacity.Width = 125;
            // 
            // cell_driver
            // 
            this.cell_driver.HeaderText = "Driver Name";
            this.cell_driver.MinimumWidth = 6;
            this.cell_driver.Name = "cell_driver";
            this.cell_driver.ReadOnly = true;
            this.cell_driver.Width = 125;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(123, 22);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 28);
            this.label14.TabIndex = 31;
            this.label14.Text = "*";
            // 
            // lblid
            // 
            this.lblid.AutoSize = true;
            this.lblid.Location = new System.Drawing.Point(427, 20);
            this.lblid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(36, 28);
            this.lblid.TabIndex = 30;
            this.lblid.Text = "00";
            this.lblid.Visible = false;
            // 
            // btnclear
            // 
            this.btnclear.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclear.ForeColor = System.Drawing.Color.Maroon;
            this.btnclear.Location = new System.Drawing.Point(150, 221);
            this.btnclear.Margin = new System.Windows.Forms.Padding(4);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(109, 39);
            this.btnclear.TabIndex = 6;
            this.btnclear.Text = "&Clear";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnCommand
            // 
            this.btnCommand.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommand.ForeColor = System.Drawing.Color.Maroon;
            this.btnCommand.Location = new System.Drawing.Point(36, 221);
            this.btnCommand.Margin = new System.Windows.Forms.Padding(4);
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.Size = new System.Drawing.Size(109, 39);
            this.btnCommand.TabIndex = 5;
            this.btnCommand.Text = "&Save";
            this.btnCommand.UseVisualStyleBackColor = true;
            this.btnCommand.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // txtcapacity
            // 
            this.txtcapacity.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcapacity.ForeColor = System.Drawing.Color.Maroon;
            this.txtcapacity.Location = new System.Drawing.Point(160, 146);
            this.txtcapacity.Margin = new System.Windows.Forms.Padding(4);
            this.txtcapacity.MaxLength = 3;
            this.txtcapacity.Name = "txtcapacity";
            this.txtcapacity.Size = new System.Drawing.Size(153, 36);
            this.txtcapacity.TabIndex = 4;
            this.txtcapacity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcapacity_KeyPress);
            // 
            // txtmake
            // 
            this.txtmake.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmake.ForeColor = System.Drawing.Color.Maroon;
            this.txtmake.Location = new System.Drawing.Point(160, 60);
            this.txtmake.Margin = new System.Windows.Forms.Padding(4);
            this.txtmake.MaxLength = 20;
            this.txtmake.Name = "txtmake";
            this.txtmake.Size = new System.Drawing.Size(255, 36);
            this.txtmake.TabIndex = 2;
            // 
            // txttipperno
            // 
            this.txttipperno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txttipperno.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttipperno.ForeColor = System.Drawing.Color.Maroon;
            this.txttipperno.Location = new System.Drawing.Point(160, 16);
            this.txttipperno.Margin = new System.Windows.Forms.Padding(4);
            this.txttipperno.MaxLength = 10;
            this.txttipperno.Name = "txttipperno";
            this.txttipperno.Size = new System.Drawing.Size(255, 36);
            this.txttipperno.TabIndex = 1;
            this.txttipperno.TextChanged += new System.EventHandler(this.txttipperno_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 153);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "Capacity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Make";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vehicle No.";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Maroon;
            this.btnClose.Location = new System.Drawing.Point(264, 221);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 39);
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TipperMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(1249, 631);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TipperMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehicle Master";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TripperMaster_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtipperdetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcapacity;
        private System.Windows.Forms.TextBox txtmake;
        private System.Windows.Forms.TextBox txttipperno;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Button btnCommand;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblid;
        private System.Windows.Forms.DataGridView dgvtipperdetails;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDriver;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_tripperno;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_make;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_capacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_driver;
        private System.Windows.Forms.Button btnClose;
    }
}