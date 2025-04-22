namespace Uniproject.RMC_forms.Masters
{
    partial class ReceipeMaster_RMC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceipeMaster_RMC));
            this.cmbrecipecode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.Columnname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Paramater = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecipeB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecipeA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecipeC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbatchsize = new System.Windows.Forms.TextBox();
            this.lblinfo = new System.Windows.Forms.Label();
            this.loginpanel = new System.Windows.Forms.Panel();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnlogin = new System.Windows.Forms.Button();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRecipeName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnaddnew = new System.Windows.Forms.Button();
            this.btnOrdered = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCommand = new System.Windows.Forms.Button();
            this.btnclear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.loginpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbrecipecode
            // 
            this.cmbrecipecode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbrecipecode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbrecipecode.FormattingEnabled = true;
            this.cmbrecipecode.Location = new System.Drawing.Point(195, 10);
            this.cmbrecipecode.Margin = new System.Windows.Forms.Padding(5);
            this.cmbrecipecode.Name = "cmbrecipecode";
            this.cmbrecipecode.Size = new System.Drawing.Size(333, 33);
            this.cmbrecipecode.TabIndex = 52;
            this.cmbrecipecode.SelectedIndexChanged += new System.EventHandler(this.cmbdesign_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 25);
            this.label1.TabIndex = 51;
            this.label1.Text = "Recipe Code";
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columnname,
            this.Paramater,
            this.RecipeB,
            this.RecipeA,
            this.RecipeC});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv1.Location = new System.Drawing.Point(4, 167);
            this.dgv1.Margin = new System.Windows.Forms.Padding(4);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowHeadersWidth = 62;
            this.dgv1.Size = new System.Drawing.Size(1269, 663);
            this.dgv1.TabIndex = 54;
            this.dgv1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellEndEdit);
            this.dgv1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv1_EditingControlShowing);
            // 
            // Columnname
            // 
            this.Columnname.HeaderText = "ColumnName";
            this.Columnname.MinimumWidth = 8;
            this.Columnname.Name = "Columnname";
            this.Columnname.Visible = false;
            this.Columnname.Width = 150;
            // 
            // Paramater
            // 
            this.Paramater.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Paramater.HeaderText = "Parameter";
            this.Paramater.MinimumWidth = 8;
            this.Paramater.Name = "Paramater";
            this.Paramater.ReadOnly = true;
            this.Paramater.Width = 126;
            // 
            // RecipeB
            // 
            this.RecipeB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.RecipeB.DefaultCellStyle = dataGridViewCellStyle2;
            this.RecipeB.HeaderText = "RecipeS";
            this.RecipeB.MinimumWidth = 8;
            this.RecipeB.Name = "RecipeB";
            // 
            // RecipeA
            // 
            this.RecipeA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.RecipeA.DefaultCellStyle = dataGridViewCellStyle3;
            this.RecipeA.HeaderText = "RecipeD";
            this.RecipeA.MinimumWidth = 8;
            this.RecipeA.Name = "RecipeA";
            this.RecipeA.ReadOnly = true;
            this.RecipeA.Visible = false;
            // 
            // RecipeC
            // 
            this.RecipeC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.RecipeC.DefaultCellStyle = dataGridViewCellStyle4;
            this.RecipeC.HeaderText = "Recipe_Diff";
            this.RecipeC.MinimumWidth = 8;
            this.RecipeC.Name = "RecipeC";
            this.RecipeC.ReadOnly = true;
            this.RecipeC.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 106);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 25);
            this.label2.TabIndex = 55;
            this.label2.Text = "Batch Size";
            // 
            // txtbatchsize
            // 
            this.txtbatchsize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbatchsize.Location = new System.Drawing.Point(192, 103);
            this.txtbatchsize.Margin = new System.Windows.Forms.Padding(4);
            this.txtbatchsize.Name = "txtbatchsize";
            this.txtbatchsize.Size = new System.Drawing.Size(185, 30);
            this.txtbatchsize.TabIndex = 56;
            this.txtbatchsize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbatchsize_KeyPress);
            // 
            // lblinfo
            // 
            this.lblinfo.AutoSize = true;
            this.lblinfo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinfo.ForeColor = System.Drawing.Color.DarkRed;
            this.lblinfo.Location = new System.Drawing.Point(9, 140);
            this.lblinfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(255, 24);
            this.lblinfo.TabIndex = 57;
            this.lblinfo.Text = " *  Enter Recipe value for 1 m";
            // 
            // loginpanel
            // 
            this.loginpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loginpanel.Controls.Add(this.btncancel);
            this.loginpanel.Controls.Add(this.btnlogin);
            this.loginpanel.Controls.Add(this.txtpass);
            this.loginpanel.Controls.Add(this.label4);
            this.loginpanel.Location = new System.Drawing.Point(136, 247);
            this.loginpanel.Margin = new System.Windows.Forms.Padding(4);
            this.loginpanel.Name = "loginpanel";
            this.loginpanel.Size = new System.Drawing.Size(335, 141);
            this.loginpanel.TabIndex = 62;
            this.loginpanel.Visible = false;
            // 
            // btncancel
            // 
            this.btncancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(227, 94);
            this.btncancel.Margin = new System.Windows.Forms.Padding(5);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(100, 37);
            this.btncancel.TabIndex = 66;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnlogin
            // 
            this.btnlogin.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.Location = new System.Drawing.Point(121, 94);
            this.btnlogin.Margin = new System.Windows.Forms.Padding(5);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(100, 37);
            this.btnlogin.TabIndex = 65;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // txtpass
            // 
            this.txtpass.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpass.Location = new System.Drawing.Point(23, 48);
            this.txtpass.Margin = new System.Windows.Forms.Padding(4);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(269, 32);
            this.txtpass.TabIndex = 64;
            this.txtpass.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 24);
            this.label4.TabIndex = 63;
            this.label4.Text = "Enter Password";
            // 
            // txtRecipeName
            // 
            this.txtRecipeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecipeName.Location = new System.Drawing.Point(192, 60);
            this.txtRecipeName.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecipeName.Name = "txtRecipeName";
            this.txtRecipeName.Size = new System.Drawing.Size(335, 30);
            this.txtRecipeName.TabIndex = 64;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 62);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 25);
            this.label3.TabIndex = 63;
            this.label3.Text = "Recipe Name";
            // 
            // btnaddnew
            // 
            this.btnaddnew.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnaddnew.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddnew.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnaddnew.Location = new System.Drawing.Point(549, 10);
            this.btnaddnew.Margin = new System.Windows.Forms.Padding(5);
            this.btnaddnew.Name = "btnaddnew";
            this.btnaddnew.Size = new System.Drawing.Size(144, 37);
            this.btnaddnew.TabIndex = 65;
            this.btnaddnew.Text = "add new";
            this.btnaddnew.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnaddnew.UseVisualStyleBackColor = false;
            this.btnaddnew.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOrdered
            // 
            this.btnOrdered.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnOrdered.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnOrdered.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOrdered.Location = new System.Drawing.Point(784, 10);
            this.btnOrdered.Margin = new System.Windows.Forms.Padding(4);
            this.btnOrdered.Name = "btnOrdered";
            this.btnOrdered.Size = new System.Drawing.Size(229, 37);
            this.btnOrdered.TabIndex = 66;
            this.btnOrdered.Text = "Order Master";
            this.btnOrdered.UseVisualStyleBackColor = false;
            this.btnOrdered.Click += new System.EventHandler(this.btnOrdered_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAdd.Location = new System.Drawing.Point(602, 101);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 37);
            this.btnAdd.TabIndex = 53;
            this.btnAdd.Text = "Reload";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCommand
            // 
            this.btnCommand.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnCommand.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnCommand.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCommand.Location = new System.Drawing.Point(387, 101);
            this.btnCommand.Margin = new System.Windows.Forms.Padding(5);
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.Size = new System.Drawing.Size(112, 37);
            this.btnCommand.TabIndex = 60;
            this.btnCommand.Text = "Save";
            this.btnCommand.UseVisualStyleBackColor = false;
            this.btnCommand.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnclear.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnclear.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnclear.Location = new System.Drawing.Point(501, 101);
            this.btnclear.Margin = new System.Windows.Forms.Padding(5);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(100, 37);
            this.btnclear.TabIndex = 61;
            this.btnclear.Text = "Clear";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClose.Location = new System.Drawing.Point(697, 101);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 37);
            this.btnClose.TabIndex = 67;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ReceipeMaster_RMC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1272, 834);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOrdered);
            this.Controls.Add(this.btnaddnew);
            this.Controls.Add(this.txtRecipeName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loginpanel);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.btnCommand);
            this.Controls.Add(this.lblinfo);
            this.Controls.Add(this.txtbatchsize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbrecipecode);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReceipeMaster_RMC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recipe Master";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DesignMaster_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesignMaster_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.loginpanel.ResumeLayout(false);
            this.loginpanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbrecipecode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbatchsize;
        private System.Windows.Forms.Label lblinfo;
        private System.Windows.Forms.Panel loginpanel;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.TextBox txtRecipeName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Paramater;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeB;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeA;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeC;
        private System.Windows.Forms.Button btnaddnew;
        private System.Windows.Forms.Button btnOrdered;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCommand;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Button btnClose;
    }
}