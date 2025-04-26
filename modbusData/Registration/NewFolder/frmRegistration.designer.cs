namespace Uniproject.frmRegistration    //Uniproject
{
    partial class cmn_frmRegistration
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRegNo = new System.Windows.Forms.TextBox();
            this.txtplantcode = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtserialkey = new System.Windows.Forms.TextBox();
            this.txtregname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbRmc = new System.Windows.Forms.RadioButton();
            this.rdbBT = new System.Windows.Forms.RadioButton();
            this.cmbDeptList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInstalledBy_Manual = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPlantType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDeviceID_Manual = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(362, 254);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Contractor Code";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(131, 254);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Plant Code";
            // 
            // txtRegNo
            // 
            this.txtRegNo.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegNo.Location = new System.Drawing.Point(365, 281);
            this.txtRegNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegNo.Name = "txtRegNo";
            this.txtRegNo.Size = new System.Drawing.Size(177, 30);
            this.txtRegNo.TabIndex = 3;
            this.txtRegNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegNo_KeyPress);
            // 
            // txtplantcode
            // 
            this.txtplantcode.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtplantcode.Location = new System.Drawing.Point(132, 281);
            this.txtplantcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtplantcode.Name = "txtplantcode";
            this.txtplantcode.Size = new System.Drawing.Size(177, 30);
            this.txtplantcode.TabIndex = 2;
            this.txtplantcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegNo_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Green;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(132, 522);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(135, 36);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "&REGISTER";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(281, 522);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(132, 36);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "&EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(113, 587);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(405, 23);
            this.label7.TabIndex = 14;
            this.label7.Text = "Click on REGISTER button to register your software";
            // 
            // txtserialkey
            // 
            this.txtserialkey.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtserialkey.Location = new System.Drawing.Point(133, 409);
            this.txtserialkey.Margin = new System.Windows.Forms.Padding(4);
            this.txtserialkey.Name = "txtserialkey";
            this.txtserialkey.Size = new System.Drawing.Size(409, 30);
            this.txtserialkey.TabIndex = 5;
            this.txtserialkey.UseSystemPasswordChar = true;
            // 
            // txtregname
            // 
            this.txtregname.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtregname.Location = new System.Drawing.Point(132, 346);
            this.txtregname.Margin = new System.Windows.Forms.Padding(4);
            this.txtregname.Name = "txtregname";
            this.txtregname.Size = new System.Drawing.Size(410, 30);
            this.txtregname.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(129, 383);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "Serial key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(128, 320);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 23);
            this.label2.TabIndex = 15;
            this.label2.Text = "Reg Name";
            // 
            // rdbRmc
            // 
            this.rdbRmc.AutoSize = true;
            this.rdbRmc.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbRmc.Location = new System.Drawing.Point(628, 587);
            this.rdbRmc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbRmc.Name = "rdbRmc";
            this.rdbRmc.Size = new System.Drawing.Size(69, 27);
            this.rdbRmc.TabIndex = 17;
            this.rdbRmc.TabStop = true;
            this.rdbRmc.Text = "RMC";
            this.rdbRmc.UseVisualStyleBackColor = true;
            this.rdbRmc.Visible = false;
            this.rdbRmc.CheckedChanged += new System.EventHandler(this.rdbRmc_CheckedChanged);
            // 
            // rdbBT
            // 
            this.rdbBT.AutoSize = true;
            this.rdbBT.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbBT.Location = new System.Drawing.Point(628, 624);
            this.rdbBT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbBT.Name = "rdbBT";
            this.rdbBT.Size = new System.Drawing.Size(55, 27);
            this.rdbBT.TabIndex = 18;
            this.rdbBT.TabStop = true;
            this.rdbBT.Text = "BT ";
            this.rdbBT.UseVisualStyleBackColor = true;
            this.rdbBT.Visible = false;
            this.rdbBT.CheckedChanged += new System.EventHandler(this.rdbBT_CheckedChanged);
            // 
            // cmbDeptList
            // 
            this.cmbDeptList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeptList.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDeptList.FormattingEnabled = true;
            this.cmbDeptList.Location = new System.Drawing.Point(132, 139);
            this.cmbDeptList.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDeptList.Name = "cmbDeptList";
            this.cmbDeptList.Size = new System.Drawing.Size(157, 31);
            this.cmbDeptList.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(131, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 23);
            this.label3.TabIndex = 20;
            this.label3.Text = "Department Name";
            // 
            // txtInstalledBy_Manual
            // 
            this.txtInstalledBy_Manual.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstalledBy_Manual.Location = new System.Drawing.Point(133, 473);
            this.txtInstalledBy_Manual.Margin = new System.Windows.Forms.Padding(4);
            this.txtInstalledBy_Manual.Name = "txtInstalledBy_Manual";
            this.txtInstalledBy_Manual.Size = new System.Drawing.Size(409, 30);
            this.txtInstalledBy_Manual.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(129, 447);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 23);
            this.label4.TabIndex = 22;
            this.label4.Text = "Installed By";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(334, 112);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 23);
            this.label8.TabIndex = 24;
            this.label8.Text = "Plant Type";
            // 
            // cmbPlantType
            // 
            this.cmbPlantType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlantType.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlantType.FormattingEnabled = true;
            this.cmbPlantType.Items.AddRange(new object[] {
            "RMC - Reversible Drum Mix",
            "RMC - Batch Mix",
            "Bitumen - Drum Mix",
            "Bitumen - Batch Mix",
            "RMC - Wet Mix"});
            this.cmbPlantType.Location = new System.Drawing.Point(334, 139);
            this.cmbPlantType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPlantType.Name = "cmbPlantType";
            this.cmbPlantType.Size = new System.Drawing.Size(208, 31);
            this.cmbPlantType.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(187, 33);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(310, 41);
            this.label9.TabIndex = 25;
            this.label9.Text = "Unipro - Registration";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(550, 139);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 17);
            this.label16.TabIndex = 34;
            this.label16.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(297, 139);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 17);
            this.label10.TabIndex = 35;
            this.label10.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(317, 281);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 17);
            this.label11.TabIndex = 36;
            this.label11.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(550, 281);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 17);
            this.label12.TabIndex = 37;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(550, 346);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 17);
            this.label13.TabIndex = 38;
            this.label13.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(550, 409);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 17);
            this.label14.TabIndex = 39;
            this.label14.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(550, 473);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 17);
            this.label15.TabIndex = 40;
            this.label15.Text = "*";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(550, 207);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(15, 17);
            this.label17.TabIndex = 43;
            this.label17.Text = "*";
            // 
            // txtDeviceID_Manual
            // 
            this.txtDeviceID_Manual.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceID_Manual.Location = new System.Drawing.Point(132, 207);
            this.txtDeviceID_Manual.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeviceID_Manual.Name = "txtDeviceID_Manual";
            this.txtDeviceID_Manual.Size = new System.Drawing.Size(410, 30);
            this.txtDeviceID_Manual.TabIndex = 41;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(131, 180);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 23);
            this.label18.TabIndex = 42;
            this.label18.Text = "Device ID";
            // 
            // cmn_frmRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            //this.BackgroundImage = global::Uniproject.Properties.Resources.v915_wit_011_k1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(701, 654);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtDeviceID_Manual);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbPlantType);
            this.Controls.Add(this.txtInstalledBy_Manual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDeptList);
            this.Controls.Add(this.rdbBT);
            this.Controls.Add(this.rdbRmc);
            this.Controls.Add(this.txtserialkey);
            this.Controls.Add(this.txtregname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtplantcode);
            this.Controls.Add(this.txtRegNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "cmn_frmRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRegistration";
            this.TransparencyKey = System.Drawing.Color.LightGray;
            this.Load += new System.EventHandler(this.frmRegistration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRegNo;
        private System.Windows.Forms.TextBox txtplantcode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtserialkey;
        private System.Windows.Forms.TextBox txtregname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbRmc;
        private System.Windows.Forms.RadioButton rdbBT;
        private System.Windows.Forms.ComboBox cmbDeptList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInstalledBy_Manual;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPlantType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDeviceID_Manual;
        private System.Windows.Forms.Label label18;
    }
}