namespace Uniproject.UtilityTools
{
    partial class SelectClientDBVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectClientDBVersion));
            this.label16 = new System.Windows.Forms.Label();
            this.cmbDataHeader = new System.Windows.Forms.ComboBox();
            this.btnSetMapping = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDataTransaction = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(442, 47);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 17);
            this.label16.TabIndex = 38;
            this.label16.Text = "*";
            // 
            // cmbDataHeader
            // 
            this.cmbDataHeader.BackColor = System.Drawing.Color.White;
            this.cmbDataHeader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataHeader.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDataHeader.FormattingEnabled = true;
            this.cmbDataHeader.Location = new System.Drawing.Point(56, 47);
            this.cmbDataHeader.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDataHeader.Name = "cmbDataHeader";
            this.cmbDataHeader.Size = new System.Drawing.Size(380, 31);
            this.cmbDataHeader.TabIndex = 37;
            // 
            // btnSetMapping
            // 
            this.btnSetMapping.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSetMapping.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSetMapping.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetMapping.ForeColor = System.Drawing.Color.White;
            this.btnSetMapping.Location = new System.Drawing.Point(146, 165);
            this.btnSetMapping.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetMapping.Name = "btnSetMapping";
            this.btnSetMapping.Size = new System.Drawing.Size(201, 31);
            this.btnSetMapping.TabIndex = 42;
            this.btnSetMapping.Text = "SET DB VERSION";
            this.btnSetMapping.UseVisualStyleBackColor = false;
            this.btnSetMapping.Click += new System.EventHandler(this.btnSetMapping_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(442, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 17);
            this.label1.TabIndex = 41;
            this.label1.Text = "*";
            // 
            // cmbDataTransaction
            // 
            this.cmbDataTransaction.BackColor = System.Drawing.Color.White;
            this.cmbDataTransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataTransaction.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDataTransaction.FormattingEnabled = true;
            this.cmbDataTransaction.Location = new System.Drawing.Point(56, 94);
            this.cmbDataTransaction.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDataTransaction.Name = "cmbDataTransaction";
            this.cmbDataTransaction.Size = new System.Drawing.Size(380, 31);
            this.cmbDataTransaction.TabIndex = 40;
            // 
            // SelectClientDBVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 242);
            this.Controls.Add(this.btnSetMapping);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDataTransaction);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbDataHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(519, 289);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(519, 289);
            this.Name = "SelectClientDBVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Client DB Version";
            this.Load += new System.EventHandler(this.SelectClientDBVersion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbDataHeader;
        private System.Windows.Forms.Button btnSetMapping;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDataTransaction;
    }
}