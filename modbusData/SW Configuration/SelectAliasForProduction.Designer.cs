namespace Uniproject.UtilityTools
{
    partial class SelectAliasForProduction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAliasForProduction));
            this.cmbDeptForProduction = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSetProductionDept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbDeptForProduction
            // 
            this.cmbDeptForProduction.BackColor = System.Drawing.Color.White;
            this.cmbDeptForProduction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeptForProduction.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDeptForProduction.FormattingEnabled = true;
            this.cmbDeptForProduction.Location = new System.Drawing.Point(50, 46);
            this.cmbDeptForProduction.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDeptForProduction.Name = "cmbDeptForProduction";
            this.cmbDeptForProduction.Size = new System.Drawing.Size(221, 31);
            this.cmbDeptForProduction.TabIndex = 34;
            this.cmbDeptForProduction.SelectedIndexChanged += new System.EventHandler(this.cmbDeptForProduction_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(277, 50);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 17);
            this.label16.TabIndex = 35;
            this.label16.Text = "*";
            // 
            // btnSetProductionDept
            // 
            this.btnSetProductionDept.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSetProductionDept.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSetProductionDept.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetProductionDept.ForeColor = System.Drawing.Color.White;
            this.btnSetProductionDept.Location = new System.Drawing.Point(300, 46);
            this.btnSetProductionDept.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetProductionDept.Name = "btnSetProductionDept";
            this.btnSetProductionDept.Size = new System.Drawing.Size(143, 31);
            this.btnSetProductionDept.TabIndex = 36;
            this.btnSetProductionDept.Text = "START WORK";
            this.btnSetProductionDept.UseVisualStyleBackColor = false;
            this.btnSetProductionDept.Click += new System.EventHandler(this.btnSetProductionDept_Click);
            // 
            // SelectAliasForProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(501, 138);
            this.ControlBox = false;
            this.Controls.Add(this.btnSetProductionDept);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbDeptForProduction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(519, 185);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(519, 185);
            this.Name = "SelectAliasForProduction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Department for Production";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SelectAliasForProduction_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDeptForProduction;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSetProductionDept;
    }
}