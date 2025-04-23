using System.Drawing;
using System.Windows.Forms;

namespace PDF_File_Reader
{
    partial class RMC_ModBus
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RMC_ModBus));
            this.txtAGG1_Act = new System.Windows.Forms.TextBox();
            this.txtAGG4_Act = new System.Windows.Forms.TextBox();
            this.txtAGG2_Act = new System.Windows.Forms.TextBox();
            this.txtAGG3_Act = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.txtAGG5_Act = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.gbAct1 = new System.Windows.Forms.GroupBox();
            this.txtAGG6_Act = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.gbAct2 = new System.Windows.Forms.GroupBox();
            this.txtCEM2_Act = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.txtCEM1_Act = new System.Windows.Forms.TextBox();
            this.txtCEM3_Act = new System.Windows.Forms.TextBox();
            this.txtCEM4_Act = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.gbSet1 = new System.Windows.Forms.GroupBox();
            this.txtagg1 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtagg4 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txtagg2 = new System.Windows.Forms.TextBox();
            this.txtagg6 = new System.Windows.Forms.TextBox();
            this.txtagg3 = new System.Windows.Forms.TextBox();
            this.txtagg5 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.txtWCorr_Act = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.btnstop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label57 = new System.Windows.Forms.Label();
            this.lblisLive = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDateToday = new System.Windows.Forms.DateTimePicker();
            this.cmbContractor = new System.Windows.Forms.ComboBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmbWorkName = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.cmbjobsite = new System.Windows.Forms.ComboBox();
            this.lblworktype = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtBatchIndex = new System.Windows.Forms.Label();
            this.txtplantcode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtcontractorid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtworkid = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtbatchsize = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlComman = new System.Windows.Forms.Panel();
            this.btnVehicle = new System.Windows.Forms.Button();
            this.btnRecipe = new System.Windows.Forms.Button();
            this.label53 = new System.Windows.Forms.Label();
            this.cmbRecipe = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtRcp = new System.Windows.Forms.TextBox();
            this.txtFILLER_Act = new System.Windows.Forms.TextBox();
            this.txtWTR2_Act = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.txtWCorr = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblActual = new System.Windows.Forms.Label();
            this.gbSet3 = new System.Windows.Forms.GroupBox();
            this.txtwater1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtwater2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtfiller = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.gbAct4 = new System.Windows.Forms.GroupBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txtADM21_Act = new System.Windows.Forms.TextBox();
            this.txtADM22_Act = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.txtADM11_Act = new System.Windows.Forms.TextBox();
            this.txtADM12_Act = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.gbSet4 = new System.Windows.Forms.GroupBox();
            this.txtSlurry = new System.Windows.Forms.TextBox();
            this.txtadmix1 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSillica = new System.Windows.Forms.TextBox();
            this.txtadmix2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.gbSet2 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCement2 = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.txtCement1 = new System.Windows.Forms.TextBox();
            this.txtCement4 = new System.Windows.Forms.TextBox();
            this.txtCement3 = new System.Windows.Forms.TextBox();
            this.gbAct3 = new System.Windows.Forms.GroupBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txtWTR1_Act = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlRDMText = new System.Windows.Forms.Panel();
            this.lb_Recipe = new System.Windows.Forms.Label();
            this.txtRDM_ProdQty = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtRDMBatchNO = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbRDMVehicle = new System.Windows.Forms.ComboBox();
            this.gbAct1.SuspendLayout();
            this.gbAct2.SuspendLayout();
            this.gbSet1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlComman.SuspendLayout();
            this.gbSet3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.gbAct4.SuspendLayout();
            this.gbSet4.SuspendLayout();
            this.gbSet2.SuspendLayout();
            this.gbAct3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlRDMText.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAGG1_Act
            // 
            this.txtAGG1_Act.BackColor = System.Drawing.Color.White;
            this.txtAGG1_Act.Enabled = false;
            this.txtAGG1_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAGG1_Act.Location = new System.Drawing.Point(11, 26);
            this.txtAGG1_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtAGG1_Act.Name = "txtAGG1_Act";
            this.txtAGG1_Act.ReadOnly = true;
            this.txtAGG1_Act.Size = new System.Drawing.Size(70, 26);
            this.txtAGG1_Act.TabIndex = 101;
            this.txtAGG1_Act.Text = "0";
            this.txtAGG1_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAGG4_Act
            // 
            this.txtAGG4_Act.BackColor = System.Drawing.Color.White;
            this.txtAGG4_Act.Enabled = false;
            this.txtAGG4_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAGG4_Act.Location = new System.Drawing.Point(11, 70);
            this.txtAGG4_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtAGG4_Act.Name = "txtAGG4_Act";
            this.txtAGG4_Act.ReadOnly = true;
            this.txtAGG4_Act.Size = new System.Drawing.Size(70, 26);
            this.txtAGG4_Act.TabIndex = 104;
            this.txtAGG4_Act.Text = "0";
            this.txtAGG4_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAGG2_Act
            // 
            this.txtAGG2_Act.BackColor = System.Drawing.Color.White;
            this.txtAGG2_Act.Enabled = false;
            this.txtAGG2_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAGG2_Act.Location = new System.Drawing.Point(83, 26);
            this.txtAGG2_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtAGG2_Act.Name = "txtAGG2_Act";
            this.txtAGG2_Act.ReadOnly = true;
            this.txtAGG2_Act.Size = new System.Drawing.Size(70, 26);
            this.txtAGG2_Act.TabIndex = 102;
            this.txtAGG2_Act.Text = "0";
            this.txtAGG2_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAGG3_Act
            // 
            this.txtAGG3_Act.BackColor = System.Drawing.Color.White;
            this.txtAGG3_Act.Enabled = false;
            this.txtAGG3_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAGG3_Act.Location = new System.Drawing.Point(155, 26);
            this.txtAGG3_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtAGG3_Act.Name = "txtAGG3_Act";
            this.txtAGG3_Act.ReadOnly = true;
            this.txtAGG3_Act.Size = new System.Drawing.Size(70, 26);
            this.txtAGG3_Act.TabIndex = 103;
            this.txtAGG3_Act.Text = "0";
            this.txtAGG3_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.ForeColor = System.Drawing.Color.White;
            this.label66.Location = new System.Drawing.Point(160, 12);
            this.label66.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(40, 13);
            this.label66.TabIndex = 90;
            this.label66.Text = "AGG3";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.White;
            this.label67.Location = new System.Drawing.Point(17, 58);
            this.label67.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(40, 13);
            this.label67.TabIndex = 91;
            this.label67.Text = "AGG4";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.ForeColor = System.Drawing.Color.White;
            this.label68.Location = new System.Drawing.Point(88, 12);
            this.label68.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(40, 13);
            this.label68.TabIndex = 92;
            this.label68.Text = "AGG2";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.ForeColor = System.Drawing.Color.White;
            this.label69.Location = new System.Drawing.Point(19, 12);
            this.label69.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(40, 13);
            this.label69.TabIndex = 93;
            this.label69.Text = "AGG1";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.ForeColor = System.Drawing.Color.White;
            this.label70.Location = new System.Drawing.Point(160, 58);
            this.label70.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(40, 13);
            this.label70.TabIndex = 114;
            this.label70.Text = "AGG6";
            // 
            // txtAGG5_Act
            // 
            this.txtAGG5_Act.BackColor = System.Drawing.Color.White;
            this.txtAGG5_Act.Enabled = false;
            this.txtAGG5_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAGG5_Act.Location = new System.Drawing.Point(83, 70);
            this.txtAGG5_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtAGG5_Act.Name = "txtAGG5_Act";
            this.txtAGG5_Act.ReadOnly = true;
            this.txtAGG5_Act.Size = new System.Drawing.Size(70, 26);
            this.txtAGG5_Act.TabIndex = 113;
            this.txtAGG5_Act.Text = "0";
            this.txtAGG5_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.ForeColor = System.Drawing.Color.White;
            this.label71.Location = new System.Drawing.Point(88, 58);
            this.label71.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(40, 13);
            this.label71.TabIndex = 112;
            this.label71.Text = "AGG5";
            // 
            // gbAct1
            // 
            this.gbAct1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.gbAct1.Controls.Add(this.txtAGG1_Act);
            this.gbAct1.Controls.Add(this.txtAGG4_Act);
            this.gbAct1.Controls.Add(this.txtAGG2_Act);
            this.gbAct1.Controls.Add(this.txtAGG3_Act);
            this.gbAct1.Controls.Add(this.label66);
            this.gbAct1.Controls.Add(this.label67);
            this.gbAct1.Controls.Add(this.label68);
            this.gbAct1.Controls.Add(this.label69);
            this.gbAct1.Controls.Add(this.label70);
            this.gbAct1.Controls.Add(this.txtAGG5_Act);
            this.gbAct1.Controls.Add(this.txtAGG6_Act);
            this.gbAct1.Controls.Add(this.label71);
            this.gbAct1.Location = new System.Drawing.Point(106, -1);
            this.gbAct1.Margin = new System.Windows.Forms.Padding(4);
            this.gbAct1.Name = "gbAct1";
            this.gbAct1.Padding = new System.Windows.Forms.Padding(4);
            this.gbAct1.Size = new System.Drawing.Size(261, 105);
            this.gbAct1.TabIndex = 142;
            this.gbAct1.TabStop = false;
            // 
            // txtAGG6_Act
            // 
            this.txtAGG6_Act.BackColor = System.Drawing.Color.White;
            this.txtAGG6_Act.Enabled = false;
            this.txtAGG6_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAGG6_Act.Location = new System.Drawing.Point(155, 70);
            this.txtAGG6_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtAGG6_Act.Name = "txtAGG6_Act";
            this.txtAGG6_Act.ReadOnly = true;
            this.txtAGG6_Act.Size = new System.Drawing.Size(70, 26);
            this.txtAGG6_Act.TabIndex = 115;
            this.txtAGG6_Act.Text = "0";
            this.txtAGG6_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.ForeColor = System.Drawing.Color.White;
            this.label62.Location = new System.Drawing.Point(17, 14);
            this.label62.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(40, 13);
            this.label62.TabIndex = 94;
            this.label62.Text = "CEM1";
            // 
            // gbAct2
            // 
            this.gbAct2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.gbAct2.Controls.Add(this.label62);
            this.gbAct2.Controls.Add(this.txtCEM2_Act);
            this.gbAct2.Controls.Add(this.label63);
            this.gbAct2.Controls.Add(this.txtCEM1_Act);
            this.gbAct2.Controls.Add(this.txtCEM3_Act);
            this.gbAct2.Controls.Add(this.txtCEM4_Act);
            this.gbAct2.Controls.Add(this.label64);
            this.gbAct2.Controls.Add(this.label65);
            this.gbAct2.Location = new System.Drawing.Point(375, -1);
            this.gbAct2.Margin = new System.Windows.Forms.Padding(4);
            this.gbAct2.Name = "gbAct2";
            this.gbAct2.Padding = new System.Windows.Forms.Padding(4);
            this.gbAct2.Size = new System.Drawing.Size(202, 105);
            this.gbAct2.TabIndex = 143;
            this.gbAct2.TabStop = false;
            // 
            // txtCEM2_Act
            // 
            this.txtCEM2_Act.BackColor = System.Drawing.Color.White;
            this.txtCEM2_Act.Enabled = false;
            this.txtCEM2_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCEM2_Act.Location = new System.Drawing.Point(88, 26);
            this.txtCEM2_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtCEM2_Act.Name = "txtCEM2_Act";
            this.txtCEM2_Act.ReadOnly = true;
            this.txtCEM2_Act.Size = new System.Drawing.Size(67, 26);
            this.txtCEM2_Act.TabIndex = 106;
            this.txtCEM2_Act.Text = "0";
            this.txtCEM2_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.White;
            this.label63.Location = new System.Drawing.Point(94, 14);
            this.label63.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(40, 13);
            this.label63.TabIndex = 95;
            this.label63.Text = "CEM2";
            // 
            // txtCEM1_Act
            // 
            this.txtCEM1_Act.BackColor = System.Drawing.Color.White;
            this.txtCEM1_Act.Enabled = false;
            this.txtCEM1_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCEM1_Act.Location = new System.Drawing.Point(11, 26);
            this.txtCEM1_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtCEM1_Act.Name = "txtCEM1_Act";
            this.txtCEM1_Act.ReadOnly = true;
            this.txtCEM1_Act.Size = new System.Drawing.Size(67, 26);
            this.txtCEM1_Act.TabIndex = 105;
            this.txtCEM1_Act.Text = "0";
            this.txtCEM1_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCEM3_Act
            // 
            this.txtCEM3_Act.BackColor = System.Drawing.Color.White;
            this.txtCEM3_Act.Enabled = false;
            this.txtCEM3_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCEM3_Act.Location = new System.Drawing.Point(11, 70);
            this.txtCEM3_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtCEM3_Act.Name = "txtCEM3_Act";
            this.txtCEM3_Act.ReadOnly = true;
            this.txtCEM3_Act.Size = new System.Drawing.Size(67, 26);
            this.txtCEM3_Act.TabIndex = 118;
            this.txtCEM3_Act.Text = "0";
            this.txtCEM3_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCEM4_Act
            // 
            this.txtCEM4_Act.BackColor = System.Drawing.Color.White;
            this.txtCEM4_Act.Enabled = false;
            this.txtCEM4_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCEM4_Act.Location = new System.Drawing.Point(88, 70);
            this.txtCEM4_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtCEM4_Act.Name = "txtCEM4_Act";
            this.txtCEM4_Act.ReadOnly = true;
            this.txtCEM4_Act.Size = new System.Drawing.Size(67, 26);
            this.txtCEM4_Act.TabIndex = 119;
            this.txtCEM4_Act.Text = "0";
            this.txtCEM4_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.ForeColor = System.Drawing.Color.White;
            this.label64.Location = new System.Drawing.Point(92, 57);
            this.label64.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(40, 13);
            this.label64.TabIndex = 117;
            this.label64.Text = "CEM4";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.ForeColor = System.Drawing.Color.White;
            this.label65.Location = new System.Drawing.Point(16, 57);
            this.label65.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(40, 13);
            this.label65.TabIndex = 116;
            this.label65.Text = "CEM3";
            // 
            // gbSet1
            // 
            this.gbSet1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.gbSet1.Controls.Add(this.txtagg1);
            this.gbSet1.Controls.Add(this.label28);
            this.gbSet1.Controls.Add(this.txtagg4);
            this.gbSet1.Controls.Add(this.label35);
            this.gbSet1.Controls.Add(this.txtagg2);
            this.gbSet1.Controls.Add(this.txtagg6);
            this.gbSet1.Controls.Add(this.txtagg3);
            this.gbSet1.Controls.Add(this.txtagg5);
            this.gbSet1.Controls.Add(this.label33);
            this.gbSet1.Controls.Add(this.label44);
            this.gbSet1.Controls.Add(this.label32);
            this.gbSet1.Controls.Add(this.label30);
            this.gbSet1.Location = new System.Drawing.Point(1140, -1);
            this.gbSet1.Margin = new System.Windows.Forms.Padding(4);
            this.gbSet1.Name = "gbSet1";
            this.gbSet1.Padding = new System.Windows.Forms.Padding(4);
            this.gbSet1.Size = new System.Drawing.Size(204, 105);
            this.gbSet1.TabIndex = 147;
            this.gbSet1.TabStop = false;
            // 
            // txtagg1
            // 
            this.txtagg1.BackColor = System.Drawing.Color.White;
            this.txtagg1.Enabled = false;
            this.txtagg1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtagg1.Location = new System.Drawing.Point(11, 26);
            this.txtagg1.Margin = new System.Windows.Forms.Padding(5);
            this.txtagg1.Name = "txtagg1";
            this.txtagg1.ReadOnly = true;
            this.txtagg1.Size = new System.Drawing.Size(52, 23);
            this.txtagg1.TabIndex = 101;
            this.txtagg1.Text = "0";
            this.txtagg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(20, 14);
            this.label28.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(40, 13);
            this.label28.TabIndex = 93;
            this.label28.Text = "AGG1";
            // 
            // txtagg4
            // 
            this.txtagg4.BackColor = System.Drawing.Color.White;
            this.txtagg4.Enabled = false;
            this.txtagg4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtagg4.Location = new System.Drawing.Point(11, 70);
            this.txtagg4.Margin = new System.Windows.Forms.Padding(5);
            this.txtagg4.Name = "txtagg4";
            this.txtagg4.ReadOnly = true;
            this.txtagg4.Size = new System.Drawing.Size(52, 23);
            this.txtagg4.TabIndex = 104;
            this.txtagg4.Text = "0";
            this.txtagg4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(83, 58);
            this.label35.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(40, 13);
            this.label35.TabIndex = 112;
            this.label35.Text = "AGG5";
            // 
            // txtagg2
            // 
            this.txtagg2.BackColor = System.Drawing.Color.White;
            this.txtagg2.Enabled = false;
            this.txtagg2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtagg2.Location = new System.Drawing.Point(75, 26);
            this.txtagg2.Margin = new System.Windows.Forms.Padding(5);
            this.txtagg2.Name = "txtagg2";
            this.txtagg2.ReadOnly = true;
            this.txtagg2.Size = new System.Drawing.Size(52, 23);
            this.txtagg2.TabIndex = 102;
            this.txtagg2.Text = "0";
            this.txtagg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtagg6
            // 
            this.txtagg6.BackColor = System.Drawing.Color.White;
            this.txtagg6.Enabled = false;
            this.txtagg6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtagg6.Location = new System.Drawing.Point(137, 70);
            this.txtagg6.Margin = new System.Windows.Forms.Padding(5);
            this.txtagg6.Name = "txtagg6";
            this.txtagg6.ReadOnly = true;
            this.txtagg6.Size = new System.Drawing.Size(52, 23);
            this.txtagg6.TabIndex = 115;
            this.txtagg6.Text = "0";
            this.txtagg6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtagg3
            // 
            this.txtagg3.BackColor = System.Drawing.Color.White;
            this.txtagg3.Enabled = false;
            this.txtagg3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtagg3.Location = new System.Drawing.Point(137, 26);
            this.txtagg3.Margin = new System.Windows.Forms.Padding(5);
            this.txtagg3.Name = "txtagg3";
            this.txtagg3.ReadOnly = true;
            this.txtagg3.Size = new System.Drawing.Size(52, 23);
            this.txtagg3.TabIndex = 103;
            this.txtagg3.Text = "0";
            this.txtagg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtagg5
            // 
            this.txtagg5.BackColor = System.Drawing.Color.White;
            this.txtagg5.Enabled = false;
            this.txtagg5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtagg5.Location = new System.Drawing.Point(75, 70);
            this.txtagg5.Margin = new System.Windows.Forms.Padding(5);
            this.txtagg5.Name = "txtagg5";
            this.txtagg5.ReadOnly = true;
            this.txtagg5.Size = new System.Drawing.Size(52, 23);
            this.txtagg5.TabIndex = 113;
            this.txtagg5.Text = "0";
            this.txtagg5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.White;
            this.label33.Location = new System.Drawing.Point(141, 14);
            this.label33.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(40, 13);
            this.label33.TabIndex = 90;
            this.label33.Text = "AGG3";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.White;
            this.label44.Location = new System.Drawing.Point(145, 58);
            this.label44.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(40, 13);
            this.label44.TabIndex = 114;
            this.label44.Text = "AGG6";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(20, 58);
            this.label32.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(40, 13);
            this.label32.TabIndex = 91;
            this.label32.Text = "AGG4";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(81, 14);
            this.label30.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(40, 13);
            this.label30.TabIndex = 92;
            this.label30.Text = "AGG2";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.ForeColor = System.Drawing.Color.White;
            this.label72.Location = new System.Drawing.Point(8, 57);
            this.label72.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(58, 13);
            this.label72.TabIndex = 128;
            this.label72.Text = "W CORR.";
            // 
            // txtWCorr_Act
            // 
            this.txtWCorr_Act.BackColor = System.Drawing.Color.White;
            this.txtWCorr_Act.Enabled = false;
            this.txtWCorr_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWCorr_Act.Location = new System.Drawing.Point(9, 70);
            this.txtWCorr_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtWCorr_Act.Name = "txtWCorr_Act";
            this.txtWCorr_Act.ReadOnly = true;
            this.txtWCorr_Act.Size = new System.Drawing.Size(67, 26);
            this.txtWCorr_Act.TabIndex = 126;
            this.txtWCorr_Act.Text = "0";
            this.txtWCorr_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.White;
            this.label55.Location = new System.Drawing.Point(17, 14);
            this.label55.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(38, 13);
            this.label55.TabIndex = 98;
            this.label55.Text = "WAT1";
            // 
            // btnstop
            // 
            this.btnstop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnstop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnstop.Location = new System.Drawing.Point(1778, 37);
            this.btnstop.Margin = new System.Windows.Forms.Padding(4);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(129, 32);
            this.btnstop.TabIndex = 54;
            this.btnstop.Text = "STOP";
            this.btnstop.UseVisualStyleBackColor = false;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1916, 508);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = ".";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.Location = new System.Drawing.Point(4, 4);
            this.dgv1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv1.RowTemplate.ReadOnly = true;
            this.dgv1.Size = new System.Drawing.Size(1908, 500);
            this.dgv1.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 338);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1924, 537);
            this.panel1.TabIndex = 48;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1924, 537);
            this.tabControl1.TabIndex = 47;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ForeColor = System.Drawing.Color.White;
            this.label57.Location = new System.Drawing.Point(83, 57);
            this.label57.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(48, 13);
            this.label57.TabIndex = 96;
            this.label57.Text = "FILLER";
            // 
            // lblisLive
            // 
            this.lblisLive.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblisLive.AutoSize = true;
            this.lblisLive.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblisLive.ForeColor = System.Drawing.Color.Silver;
            this.lblisLive.Location = new System.Drawing.Point(1482, 48);
            this.lblisLive.Name = "lblisLive";
            this.lblisLive.Size = new System.Drawing.Size(67, 28);
            this.lblisLive.TabIndex = 90;
            this.lblisLive.Text = "Idle..!";
            this.lblisLive.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(1371, 87);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(104, 24);
            this.label21.TabIndex = 170;
            this.label21.Text = "Date Today";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.lblisLive);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.txtDateToday);
            this.panel3.Controls.Add(this.cmbContractor);
            this.panel3.Controls.Add(this.lblInfo);
            this.panel3.Controls.Add(this.cmbWorkName);
            this.panel3.Controls.Add(this.label42);
            this.panel3.Controls.Add(this.label40);
            this.panel3.Controls.Add(this.cmbjobsite);
            this.panel3.Controls.Add(this.lblworktype);
            this.panel3.Controls.Add(this.label31);
            this.panel3.Controls.Add(this.txtBatchIndex);
            this.panel3.Controls.Add(this.txtplantcode);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.txtcontractorid);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtworkid);
            this.panel3.Controls.Add(this.label39);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1924, 139);
            this.panel3.TabIndex = 21;
            // 
            // txtDateToday
            // 
            this.txtDateToday.Enabled = false;
            this.txtDateToday.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateToday.Location = new System.Drawing.Point(1486, 84);
            this.txtDateToday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDateToday.Name = "txtDateToday";
            this.txtDateToday.Size = new System.Drawing.Size(146, 29);
            this.txtDateToday.TabIndex = 169;
            // 
            // cmbContractor
            // 
            this.cmbContractor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbContractor.FormattingEnabled = true;
            this.cmbContractor.Location = new System.Drawing.Point(127, 8);
            this.cmbContractor.Margin = new System.Windows.Forms.Padding(4);
            this.cmbContractor.Name = "cmbContractor";
            this.cmbContractor.Size = new System.Drawing.Size(563, 32);
            this.cmbContractor.TabIndex = 94;
            this.cmbContractor.SelectedIndexChanged += new System.EventHandler(this.cmbContractor_SelectedIndexChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(1393, 48);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(13, 18);
            this.lblInfo.TabIndex = 93;
            this.lblInfo.Text = "-";
            // 
            // cmbWorkName
            // 
            this.cmbWorkName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWorkName.FormattingEnabled = true;
            this.cmbWorkName.Location = new System.Drawing.Point(127, 52);
            this.cmbWorkName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWorkName.Name = "cmbWorkName";
            this.cmbWorkName.Size = new System.Drawing.Size(1225, 32);
            this.cmbWorkName.TabIndex = 71;
            this.cmbWorkName.SelectedIndexChanged += new System.EventHandler(this.cmbWorkName_SelectedIndexChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label42.Location = new System.Drawing.Point(6, 55);
            this.label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(115, 24);
            this.label42.TabIndex = 60;
            this.label42.Text = "Work Name:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label40.Location = new System.Drawing.Point(6, 97);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(81, 24);
            this.label40.TabIndex = 61;
            this.label40.Text = "Job Site:";
            // 
            // cmbjobsite
            // 
            this.cmbjobsite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbjobsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbjobsite.FormattingEnabled = true;
            this.cmbjobsite.Location = new System.Drawing.Point(127, 94);
            this.cmbjobsite.Margin = new System.Windows.Forms.Padding(4);
            this.cmbjobsite.Name = "cmbjobsite";
            this.cmbjobsite.Size = new System.Drawing.Size(563, 32);
            this.cmbjobsite.TabIndex = 72;
            // 
            // lblworktype
            // 
            this.lblworktype.AutoSize = true;
            this.lblworktype.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblworktype.Location = new System.Drawing.Point(1389, 8);
            this.lblworktype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblworktype.Name = "lblworktype";
            this.lblworktype.Size = new System.Drawing.Size(94, 24);
            this.lblworktype.TabIndex = 52;
            this.lblworktype.Text = "WorkType";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label31.Location = new System.Drawing.Point(1075, 10);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(108, 24);
            this.label31.TabIndex = 74;
            this.label31.Text = "Plant Code:";
            // 
            // txtBatchIndex
            // 
            this.txtBatchIndex.AutoSize = true;
            this.txtBatchIndex.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtBatchIndex.Location = new System.Drawing.Point(1518, 11);
            this.txtBatchIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtBatchIndex.Name = "txtBatchIndex";
            this.txtBatchIndex.Size = new System.Drawing.Size(103, 24);
            this.txtBatchIndex.TabIndex = 92;
            this.txtBatchIndex.Text = "BatchIndex";
            this.txtBatchIndex.Visible = false;
            // 
            // txtplantcode
            // 
            this.txtplantcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtplantcode.Location = new System.Drawing.Point(1194, 8);
            this.txtplantcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtplantcode.Name = "txtplantcode";
            this.txtplantcode.ReadOnly = true;
            this.txtplantcode.Size = new System.Drawing.Size(172, 29);
            this.txtplantcode.TabIndex = 75;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(736, 96);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 24);
            this.label13.TabIndex = 90;
            this.label13.Text = "Work Code:";
            // 
            // txtcontractorid
            // 
            this.txtcontractorid.BackColor = System.Drawing.Color.White;
            this.txtcontractorid.Enabled = false;
            this.txtcontractorid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtcontractorid.Location = new System.Drawing.Point(882, 8);
            this.txtcontractorid.Margin = new System.Windows.Forms.Padding(4);
            this.txtcontractorid.Name = "txtcontractorid";
            this.txtcontractorid.ReadOnly = true;
            this.txtcontractorid.Size = new System.Drawing.Size(169, 29);
            this.txtcontractorid.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(728, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 24);
            this.label6.TabIndex = 57;
            this.label6.Text = "Contractor Code:";
            // 
            // txtworkid
            // 
            this.txtworkid.BackColor = System.Drawing.Color.White;
            this.txtworkid.Enabled = false;
            this.txtworkid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtworkid.Location = new System.Drawing.Point(876, 93);
            this.txtworkid.Margin = new System.Windows.Forms.Padding(4);
            this.txtworkid.Name = "txtworkid";
            this.txtworkid.ReadOnly = true;
            this.txtworkid.Size = new System.Drawing.Size(476, 29);
            this.txtworkid.TabIndex = 56;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(6, 11);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(106, 24);
            this.label39.TabIndex = 63;
            this.label39.Text = "Contractor:";
            // 
            // txtbatchsize
            // 
            this.txtbatchsize.BackColor = System.Drawing.Color.White;
            this.txtbatchsize.Enabled = false;
            this.txtbatchsize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbatchsize.Location = new System.Drawing.Point(478, 7);
            this.txtbatchsize.Margin = new System.Windows.Forms.Padding(4);
            this.txtbatchsize.Name = "txtbatchsize";
            this.txtbatchsize.ReadOnly = true;
            this.txtbatchsize.Size = new System.Drawing.Size(161, 29);
            this.txtbatchsize.TabIndex = 100;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClear.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClear.Location = new System.Drawing.Point(1778, 69);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 32);
            this.btnClear.TabIndex = 39;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.ForestGreen;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnStart.Location = new System.Drawing.Point(1778, 4);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(129, 32);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlComman
            // 
            this.pnlComman.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlComman.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pnlComman.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlComman.Controls.Add(this.btnVehicle);
            this.pnlComman.Controls.Add(this.btnRecipe);
            this.pnlComman.Controls.Add(this.label53);
            this.pnlComman.Controls.Add(this.cmbRecipe);
            this.pnlComman.Controls.Add(this.label22);
            this.pnlComman.Controls.Add(this.txtRcp);
            this.pnlComman.Controls.Add(this.txtbatchsize);
            this.pnlComman.Location = new System.Drawing.Point(0, 141);
            this.pnlComman.Margin = new System.Windows.Forms.Padding(4);
            this.pnlComman.Name = "pnlComman";
            this.pnlComman.Size = new System.Drawing.Size(1920, 53);
            this.pnlComman.TabIndex = 21;
            // 
            // btnVehicle
            // 
            this.btnVehicle.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnVehicle.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVehicle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnVehicle.Location = new System.Drawing.Point(1778, 12);
            this.btnVehicle.Margin = new System.Windows.Forms.Padding(4);
            this.btnVehicle.Name = "btnVehicle";
            this.btnVehicle.Size = new System.Drawing.Size(129, 32);
            this.btnVehicle.TabIndex = 153;
            this.btnVehicle.Text = "Add Vehicle";
            this.btnVehicle.UseVisualStyleBackColor = false;
            this.btnVehicle.Visible = false;
            this.btnVehicle.Click += new System.EventHandler(this.btnVehicle_Click);
            // 
            // btnRecipe
            // 
            this.btnRecipe.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnRecipe.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecipe.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRecipe.Location = new System.Drawing.Point(1645, 12);
            this.btnRecipe.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecipe.Name = "btnRecipe";
            this.btnRecipe.Size = new System.Drawing.Size(109, 32);
            this.btnRecipe.TabIndex = 152;
            this.btnRecipe.Text = "Add Recipe";
            this.btnRecipe.UseVisualStyleBackColor = false;
            this.btnRecipe.Visible = false;
            this.btnRecipe.Click += new System.EventHandler(this.btnRecipe_Click);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label53.Location = new System.Drawing.Point(32, 15);
            this.label53.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(71, 24);
            this.label53.TabIndex = 110;
            this.label53.Text = "Recipe:";
            // 
            // cmbRecipe
            // 
            this.cmbRecipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRecipe.FormattingEnabled = true;
            this.cmbRecipe.Location = new System.Drawing.Point(128, 4);
            this.cmbRecipe.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRecipe.Name = "cmbRecipe";
            this.cmbRecipe.Size = new System.Drawing.Size(199, 32);
            this.cmbRecipe.TabIndex = 109;
            this.cmbRecipe.Tag = "cmbTuskRcp";
            this.cmbRecipe.Click += new System.EventHandler(this.cmbRecipe_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(369, 15);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(100, 24);
            this.label22.TabIndex = 103;
            this.label22.Text = "Batch Size:";
            // 
            // txtRcp
            // 
            this.txtRcp.BackColor = System.Drawing.Color.White;
            this.txtRcp.Enabled = false;
            this.txtRcp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRcp.Location = new System.Drawing.Point(128, 5);
            this.txtRcp.Margin = new System.Windows.Forms.Padding(4);
            this.txtRcp.Name = "txtRcp";
            this.txtRcp.ReadOnly = true;
            this.txtRcp.Size = new System.Drawing.Size(199, 24);
            this.txtRcp.TabIndex = 100;
            this.txtRcp.Visible = false;
            // 
            // txtFILLER_Act
            // 
            this.txtFILLER_Act.BackColor = System.Drawing.Color.White;
            this.txtFILLER_Act.Enabled = false;
            this.txtFILLER_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFILLER_Act.Location = new System.Drawing.Point(87, 70);
            this.txtFILLER_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtFILLER_Act.Name = "txtFILLER_Act";
            this.txtFILLER_Act.ReadOnly = true;
            this.txtFILLER_Act.Size = new System.Drawing.Size(67, 26);
            this.txtFILLER_Act.TabIndex = 107;
            this.txtFILLER_Act.Text = "0";
            this.txtFILLER_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWTR2_Act
            // 
            this.txtWTR2_Act.BackColor = System.Drawing.Color.White;
            this.txtWTR2_Act.Enabled = false;
            this.txtWTR2_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWTR2_Act.Location = new System.Drawing.Point(87, 26);
            this.txtWTR2_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtWTR2_Act.Name = "txtWTR2_Act";
            this.txtWTR2_Act.ReadOnly = true;
            this.txtWTR2_Act.Size = new System.Drawing.Size(67, 26);
            this.txtWTR2_Act.TabIndex = 109;
            this.txtWTR2_Act.Text = "0";
            this.txtWTR2_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.ForeColor = System.Drawing.Color.White;
            this.label75.Location = new System.Drawing.Point(5, 59);
            this.label75.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(58, 13);
            this.label75.TabIndex = 130;
            this.label75.Text = "W CORR.";
            // 
            // txtWCorr
            // 
            this.txtWCorr.BackColor = System.Drawing.Color.White;
            this.txtWCorr.Enabled = false;
            this.txtWCorr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWCorr.Location = new System.Drawing.Point(9, 71);
            this.txtWCorr.Margin = new System.Windows.Forms.Padding(5);
            this.txtWCorr.Name = "txtWCorr";
            this.txtWCorr.ReadOnly = true;
            this.txtWCorr.Size = new System.Drawing.Size(52, 23);
            this.txtWCorr.TabIndex = 129;
            this.txtWCorr.Text = "0";
            this.txtWCorr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(15, 14);
            this.label15.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 98;
            this.label15.Text = "WAT1";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.Location = new System.Drawing.Point(1843, 69);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 32);
            this.btnClose.TabIndex = 151;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarget.ForeColor = System.Drawing.Color.LawnGreen;
            this.lblTarget.Location = new System.Drawing.Point(1028, 37);
            this.lblTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(93, 24);
            this.lblTarget.TabIndex = 144;
            this.lblTarget.Text = "TARGET";
            // 
            // lblActual
            // 
            this.lblActual.AutoSize = true;
            this.lblActual.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActual.ForeColor = System.Drawing.Color.LawnGreen;
            this.lblActual.Location = new System.Drawing.Point(4, 41);
            this.lblActual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActual.Name = "lblActual";
            this.lblActual.Size = new System.Drawing.Size(90, 24);
            this.lblActual.TabIndex = 143;
            this.lblActual.Text = "ACTUAL";
            // 
            // gbSet3
            // 
            this.gbSet3.BackColor = System.Drawing.SystemColors.HotTrack;
            this.gbSet3.Controls.Add(this.label75);
            this.gbSet3.Controls.Add(this.txtWCorr);
            this.gbSet3.Controls.Add(this.label15);
            this.gbSet3.Controls.Add(this.txtwater1);
            this.gbSet3.Controls.Add(this.label18);
            this.gbSet3.Controls.Add(this.txtwater2);
            this.gbSet3.Controls.Add(this.label12);
            this.gbSet3.Controls.Add(this.txtfiller);
            this.gbSet3.Location = new System.Drawing.Point(1492, -1);
            this.gbSet3.Margin = new System.Windows.Forms.Padding(4);
            this.gbSet3.Name = "gbSet3";
            this.gbSet3.Padding = new System.Windows.Forms.Padding(4);
            this.gbSet3.Size = new System.Drawing.Size(133, 105);
            this.gbSet3.TabIndex = 150;
            this.gbSet3.TabStop = false;
            // 
            // txtwater1
            // 
            this.txtwater1.BackColor = System.Drawing.Color.White;
            this.txtwater1.Enabled = false;
            this.txtwater1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwater1.Location = new System.Drawing.Point(9, 26);
            this.txtwater1.Margin = new System.Windows.Forms.Padding(5);
            this.txtwater1.Name = "txtwater1";
            this.txtwater1.ReadOnly = true;
            this.txtwater1.Size = new System.Drawing.Size(52, 23);
            this.txtwater1.TabIndex = 108;
            this.txtwater1.Text = "0";
            this.txtwater1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(75, 58);
            this.label18.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 13);
            this.label18.TabIndex = 96;
            this.label18.Text = "FILLER";
            // 
            // txtwater2
            // 
            this.txtwater2.BackColor = System.Drawing.Color.White;
            this.txtwater2.Enabled = false;
            this.txtwater2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwater2.Location = new System.Drawing.Point(71, 27);
            this.txtwater2.Margin = new System.Windows.Forms.Padding(5);
            this.txtwater2.Name = "txtwater2";
            this.txtwater2.ReadOnly = true;
            this.txtwater2.Size = new System.Drawing.Size(52, 23);
            this.txtwater2.TabIndex = 109;
            this.txtwater2.Text = "0";
            this.txtwater2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(75, 14);
            this.label12.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 99;
            this.label12.Text = "WAT2";
            // 
            // txtfiller
            // 
            this.txtfiller.BackColor = System.Drawing.Color.White;
            this.txtfiller.Enabled = false;
            this.txtfiller.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfiller.Location = new System.Drawing.Point(71, 71);
            this.txtfiller.Margin = new System.Windows.Forms.Padding(5);
            this.txtfiller.Name = "txtfiller";
            this.txtfiller.ReadOnly = true;
            this.txtfiller.Size = new System.Drawing.Size(52, 23);
            this.txtfiller.TabIndex = 107;
            this.txtfiller.Text = "0";
            this.txtfiller.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.lblTarget);
            this.panel5.Controls.Add(this.lblActual);
            this.panel5.Controls.Add(this.gbSet3);
            this.panel5.Controls.Add(this.gbAct4);
            this.panel5.Controls.Add(this.gbSet4);
            this.panel5.Controls.Add(this.gbSet2);
            this.panel5.Controls.Add(this.gbAct3);
            this.panel5.Controls.Add(this.gbSet1);
            this.panel5.Controls.Add(this.gbAct2);
            this.panel5.Controls.Add(this.gbAct1);
            this.panel5.Controls.Add(this.btnstop);
            this.panel5.Controls.Add(this.btnClear);
            this.panel5.Controls.Add(this.btnStart);
            this.panel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 233);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1924, 106);
            this.panel5.TabIndex = 21;
            // 
            // gbAct4
            // 
            this.gbAct4.BackColor = System.Drawing.SystemColors.HotTrack;
            this.gbAct4.Controls.Add(this.label73);
            this.gbAct4.Controls.Add(this.txtADM21_Act);
            this.gbAct4.Controls.Add(this.txtADM22_Act);
            this.gbAct4.Controls.Add(this.label74);
            this.gbAct4.Controls.Add(this.label61);
            this.gbAct4.Controls.Add(this.txtADM11_Act);
            this.gbAct4.Controls.Add(this.txtADM12_Act);
            this.gbAct4.Controls.Add(this.label58);
            this.gbAct4.Location = new System.Drawing.Point(804, -1);
            this.gbAct4.Margin = new System.Windows.Forms.Padding(4);
            this.gbAct4.Name = "gbAct4";
            this.gbAct4.Padding = new System.Windows.Forms.Padding(4);
            this.gbAct4.Size = new System.Drawing.Size(169, 105);
            this.gbAct4.TabIndex = 145;
            this.gbAct4.TabStop = false;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.ForeColor = System.Drawing.Color.White;
            this.label73.Location = new System.Drawing.Point(11, 58);
            this.label73.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(48, 13);
            this.label73.TabIndex = 124;
            this.label73.Text = "ADM21";
            // 
            // txtADM21_Act
            // 
            this.txtADM21_Act.BackColor = System.Drawing.Color.White;
            this.txtADM21_Act.Enabled = false;
            this.txtADM21_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtADM21_Act.Location = new System.Drawing.Point(8, 70);
            this.txtADM21_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtADM21_Act.Name = "txtADM21_Act";
            this.txtADM21_Act.ReadOnly = true;
            this.txtADM21_Act.Size = new System.Drawing.Size(52, 26);
            this.txtADM21_Act.TabIndex = 126;
            this.txtADM21_Act.Text = "0";
            this.txtADM21_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtADM22_Act
            // 
            this.txtADM22_Act.BackColor = System.Drawing.Color.White;
            this.txtADM22_Act.Enabled = false;
            this.txtADM22_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtADM22_Act.Location = new System.Drawing.Point(69, 70);
            this.txtADM22_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtADM22_Act.Name = "txtADM22_Act";
            this.txtADM22_Act.ReadOnly = true;
            this.txtADM22_Act.Size = new System.Drawing.Size(52, 26);
            this.txtADM22_Act.TabIndex = 127;
            this.txtADM22_Act.Text = "0";
            this.txtADM22_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.ForeColor = System.Drawing.Color.White;
            this.label74.Location = new System.Drawing.Point(72, 57);
            this.label74.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(48, 13);
            this.label74.TabIndex = 125;
            this.label74.Text = "ADM22";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.ForeColor = System.Drawing.Color.White;
            this.label61.Location = new System.Drawing.Point(12, 12);
            this.label61.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(48, 13);
            this.label61.TabIndex = 97;
            this.label61.Text = "ADM11";
            // 
            // txtADM11_Act
            // 
            this.txtADM11_Act.BackColor = System.Drawing.Color.White;
            this.txtADM11_Act.Enabled = false;
            this.txtADM11_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtADM11_Act.Location = new System.Drawing.Point(8, 26);
            this.txtADM11_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtADM11_Act.Name = "txtADM11_Act";
            this.txtADM11_Act.ReadOnly = true;
            this.txtADM11_Act.Size = new System.Drawing.Size(52, 26);
            this.txtADM11_Act.TabIndex = 110;
            this.txtADM11_Act.Text = "0";
            this.txtADM11_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtADM12_Act
            // 
            this.txtADM12_Act.BackColor = System.Drawing.Color.White;
            this.txtADM12_Act.Enabled = false;
            this.txtADM12_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtADM12_Act.Location = new System.Drawing.Point(69, 26);
            this.txtADM12_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtADM12_Act.Name = "txtADM12_Act";
            this.txtADM12_Act.ReadOnly = true;
            this.txtADM12_Act.Size = new System.Drawing.Size(52, 26);
            this.txtADM12_Act.TabIndex = 111;
            this.txtADM12_Act.Text = "0";
            this.txtADM12_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.White;
            this.label58.Location = new System.Drawing.Point(72, 12);
            this.label58.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(48, 13);
            this.label58.TabIndex = 100;
            this.label58.Text = "ADM12";
            // 
            // gbSet4
            // 
            this.gbSet4.BackColor = System.Drawing.SystemColors.HotTrack;
            this.gbSet4.Controls.Add(this.txtSlurry);
            this.gbSet4.Controls.Add(this.txtadmix1);
            this.gbSet4.Controls.Add(this.label54);
            this.gbSet4.Controls.Add(this.label16);
            this.gbSet4.Controls.Add(this.txtSillica);
            this.gbSet4.Controls.Add(this.txtadmix2);
            this.gbSet4.Controls.Add(this.label7);
            this.gbSet4.Controls.Add(this.label52);
            this.gbSet4.Location = new System.Drawing.Point(1631, -1);
            this.gbSet4.Margin = new System.Windows.Forms.Padding(4);
            this.gbSet4.Name = "gbSet4";
            this.gbSet4.Padding = new System.Windows.Forms.Padding(4);
            this.gbSet4.Size = new System.Drawing.Size(137, 105);
            this.gbSet4.TabIndex = 149;
            this.gbSet4.TabStop = false;
            // 
            // txtSlurry
            // 
            this.txtSlurry.BackColor = System.Drawing.Color.White;
            this.txtSlurry.Enabled = false;
            this.txtSlurry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlurry.Location = new System.Drawing.Point(71, 26);
            this.txtSlurry.Margin = new System.Windows.Forms.Padding(5);
            this.txtSlurry.Name = "txtSlurry";
            this.txtSlurry.ReadOnly = true;
            this.txtSlurry.Size = new System.Drawing.Size(52, 23);
            this.txtSlurry.TabIndex = 123;
            this.txtSlurry.Text = "0";
            this.txtSlurry.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtadmix1
            // 
            this.txtadmix1.BackColor = System.Drawing.Color.White;
            this.txtadmix1.Enabled = false;
            this.txtadmix1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtadmix1.Location = new System.Drawing.Point(9, 26);
            this.txtadmix1.Margin = new System.Windows.Forms.Padding(5);
            this.txtadmix1.Name = "txtadmix1";
            this.txtadmix1.ReadOnly = true;
            this.txtadmix1.Size = new System.Drawing.Size(52, 23);
            this.txtadmix1.TabIndex = 110;
            this.txtadmix1.Text = "0";
            this.txtadmix1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.White;
            this.label54.Location = new System.Drawing.Point(69, 11);
            this.label54.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(54, 13);
            this.label54.TabIndex = 122;
            this.label54.Text = "SLURRY";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(11, 11);
            this.label16.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 13);
            this.label16.TabIndex = 97;
            this.label16.Text = "ADMIX1";
            // 
            // txtSillica
            // 
            this.txtSillica.BackColor = System.Drawing.Color.White;
            this.txtSillica.Enabled = false;
            this.txtSillica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSillica.Location = new System.Drawing.Point(71, 70);
            this.txtSillica.Margin = new System.Windows.Forms.Padding(5);
            this.txtSillica.Name = "txtSillica";
            this.txtSillica.ReadOnly = true;
            this.txtSillica.Size = new System.Drawing.Size(52, 23);
            this.txtSillica.TabIndex = 121;
            this.txtSillica.Text = "0";
            this.txtSillica.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtadmix2
            // 
            this.txtadmix2.BackColor = System.Drawing.Color.White;
            this.txtadmix2.Enabled = false;
            this.txtadmix2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtadmix2.Location = new System.Drawing.Point(11, 70);
            this.txtadmix2.Margin = new System.Windows.Forms.Padding(5);
            this.txtadmix2.Name = "txtadmix2";
            this.txtadmix2.ReadOnly = true;
            this.txtadmix2.Size = new System.Drawing.Size(51, 23);
            this.txtadmix2.TabIndex = 111;
            this.txtadmix2.Text = "0";
            this.txtadmix2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 58);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 100;
            this.label7.Text = "ADMIX2";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.White;
            this.label52.Location = new System.Drawing.Point(72, 58);
            this.label52.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(53, 13);
            this.label52.TabIndex = 120;
            this.label52.Text = "SILLICA";
            // 
            // gbSet2
            // 
            this.gbSet2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.gbSet2.Controls.Add(this.label20);
            this.gbSet2.Controls.Add(this.txtCement2);
            this.gbSet2.Controls.Add(this.label51);
            this.gbSet2.Controls.Add(this.label19);
            this.gbSet2.Controls.Add(this.label50);
            this.gbSet2.Controls.Add(this.txtCement1);
            this.gbSet2.Controls.Add(this.txtCement4);
            this.gbSet2.Controls.Add(this.txtCement3);
            this.gbSet2.Location = new System.Drawing.Point(1350, -1);
            this.gbSet2.Margin = new System.Windows.Forms.Padding(4);
            this.gbSet2.Name = "gbSet2";
            this.gbSet2.Padding = new System.Windows.Forms.Padding(4);
            this.gbSet2.Size = new System.Drawing.Size(137, 105);
            this.gbSet2.TabIndex = 148;
            this.gbSet2.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(16, 14);
            this.label20.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 13);
            this.label20.TabIndex = 94;
            this.label20.Text = "CEM1";
            // 
            // txtCement2
            // 
            this.txtCement2.BackColor = System.Drawing.Color.White;
            this.txtCement2.Enabled = false;
            this.txtCement2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCement2.Location = new System.Drawing.Point(72, 26);
            this.txtCement2.Margin = new System.Windows.Forms.Padding(5);
            this.txtCement2.Name = "txtCement2";
            this.txtCement2.ReadOnly = true;
            this.txtCement2.Size = new System.Drawing.Size(52, 23);
            this.txtCement2.TabIndex = 106;
            this.txtCement2.Text = "0";
            this.txtCement2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.White;
            this.label51.Location = new System.Drawing.Point(16, 58);
            this.label51.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(40, 13);
            this.label51.TabIndex = 116;
            this.label51.Text = "CEM3";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(77, 14);
            this.label19.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 13);
            this.label19.TabIndex = 95;
            this.label19.Text = "CEM2";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.White;
            this.label50.Location = new System.Drawing.Point(77, 58);
            this.label50.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(40, 13);
            this.label50.TabIndex = 117;
            this.label50.Text = "CEM4";
            // 
            // txtCement1
            // 
            this.txtCement1.BackColor = System.Drawing.Color.White;
            this.txtCement1.Enabled = false;
            this.txtCement1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCement1.Location = new System.Drawing.Point(9, 26);
            this.txtCement1.Margin = new System.Windows.Forms.Padding(5);
            this.txtCement1.Name = "txtCement1";
            this.txtCement1.ReadOnly = true;
            this.txtCement1.Size = new System.Drawing.Size(52, 23);
            this.txtCement1.TabIndex = 105;
            this.txtCement1.Text = "0";
            this.txtCement1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCement4
            // 
            this.txtCement4.BackColor = System.Drawing.Color.White;
            this.txtCement4.Enabled = false;
            this.txtCement4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCement4.Location = new System.Drawing.Point(72, 70);
            this.txtCement4.Margin = new System.Windows.Forms.Padding(5);
            this.txtCement4.Name = "txtCement4";
            this.txtCement4.ReadOnly = true;
            this.txtCement4.Size = new System.Drawing.Size(52, 23);
            this.txtCement4.TabIndex = 119;
            this.txtCement4.Text = "0";
            this.txtCement4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCement3
            // 
            this.txtCement3.BackColor = System.Drawing.Color.White;
            this.txtCement3.Enabled = false;
            this.txtCement3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCement3.Location = new System.Drawing.Point(9, 70);
            this.txtCement3.Margin = new System.Windows.Forms.Padding(5);
            this.txtCement3.Name = "txtCement3";
            this.txtCement3.ReadOnly = true;
            this.txtCement3.Size = new System.Drawing.Size(52, 23);
            this.txtCement3.TabIndex = 118;
            this.txtCement3.Text = "0";
            this.txtCement3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbAct3
            // 
            this.gbAct3.BackColor = System.Drawing.SystemColors.HotTrack;
            this.gbAct3.Controls.Add(this.label72);
            this.gbAct3.Controls.Add(this.txtWCorr_Act);
            this.gbAct3.Controls.Add(this.label55);
            this.gbAct3.Controls.Add(this.label57);
            this.gbAct3.Controls.Add(this.txtFILLER_Act);
            this.gbAct3.Controls.Add(this.txtWTR2_Act);
            this.gbAct3.Controls.Add(this.label60);
            this.gbAct3.Controls.Add(this.txtWTR1_Act);
            this.gbAct3.Location = new System.Drawing.Point(585, -1);
            this.gbAct3.Margin = new System.Windows.Forms.Padding(4);
            this.gbAct3.Name = "gbAct3";
            this.gbAct3.Padding = new System.Windows.Forms.Padding(4);
            this.gbAct3.Size = new System.Drawing.Size(210, 105);
            this.gbAct3.TabIndex = 144;
            this.gbAct3.TabStop = false;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.ForeColor = System.Drawing.Color.White;
            this.label60.Location = new System.Drawing.Point(86, 14);
            this.label60.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(38, 13);
            this.label60.TabIndex = 99;
            this.label60.Text = "WAT2";
            // 
            // txtWTR1_Act
            // 
            this.txtWTR1_Act.BackColor = System.Drawing.Color.White;
            this.txtWTR1_Act.Enabled = false;
            this.txtWTR1_Act.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWTR1_Act.Location = new System.Drawing.Point(9, 26);
            this.txtWTR1_Act.Margin = new System.Windows.Forms.Padding(5);
            this.txtWTR1_Act.Name = "txtWTR1_Act";
            this.txtWTR1_Act.ReadOnly = true;
            this.txtWTR1_Act.Size = new System.Drawing.Size(67, 26);
            this.txtWTR1_Act.TabIndex = 108;
            this.txtWTR1_Act.Text = "0";
            this.txtWTR1_Act.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(249, 7);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 20);
            this.label11.TabIndex = 103;
            this.label11.Text = "Vehicle";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.Controls.Add(this.pnlRDMText);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.pnlComman);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1924, 338);
            this.panel2.TabIndex = 47;
            // 
            // pnlRDMText
            // 
            this.pnlRDMText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRDMText.BackColor = System.Drawing.Color.YellowGreen;
            this.pnlRDMText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRDMText.Controls.Add(this.lb_Recipe);
            this.pnlRDMText.Controls.Add(this.txtRDM_ProdQty);
            this.pnlRDMText.Controls.Add(this.label17);
            this.pnlRDMText.Controls.Add(this.txtRDMBatchNO);
            this.pnlRDMText.Controls.Add(this.label9);
            this.pnlRDMText.Controls.Add(this.cmbRDMVehicle);
            this.pnlRDMText.Controls.Add(this.label11);
            this.pnlRDMText.Location = new System.Drawing.Point(0, 195);
            this.pnlRDMText.Margin = new System.Windows.Forms.Padding(4);
            this.pnlRDMText.Name = "pnlRDMText";
            this.pnlRDMText.Size = new System.Drawing.Size(1924, 37);
            this.pnlRDMText.TabIndex = 22;
            // 
            // lb_Recipe
            // 
            this.lb_Recipe.AutoSize = true;
            this.lb_Recipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Recipe.Location = new System.Drawing.Point(792, 10);
            this.lb_Recipe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Recipe.Name = "lb_Recipe";
            this.lb_Recipe.Size = new System.Drawing.Size(0, 18);
            this.lb_Recipe.TabIndex = 118;
            // 
            // txtRDM_ProdQty
            // 
            this.txtRDM_ProdQty.BackColor = System.Drawing.Color.White;
            this.txtRDM_ProdQty.Enabled = false;
            this.txtRDM_ProdQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtRDM_ProdQty.Location = new System.Drawing.Point(663, 3);
            this.txtRDM_ProdQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtRDM_ProdQty.Name = "txtRDM_ProdQty";
            this.txtRDM_ProdQty.Size = new System.Drawing.Size(123, 29);
            this.txtRDM_ProdQty.TabIndex = 116;
            this.txtRDM_ProdQty.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(572, 7);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 20);
            this.label17.TabIndex = 117;
            this.label17.Text = "Prod. Qty.";
            this.label17.Visible = false;
            // 
            // txtRDMBatchNO
            // 
            this.txtRDMBatchNO.BackColor = System.Drawing.Color.White;
            this.txtRDMBatchNO.Enabled = false;
            this.txtRDMBatchNO.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtRDMBatchNO.Location = new System.Drawing.Point(97, 1);
            this.txtRDMBatchNO.Margin = new System.Windows.Forms.Padding(4);
            this.txtRDMBatchNO.Name = "txtRDMBatchNO";
            this.txtRDMBatchNO.Size = new System.Drawing.Size(113, 29);
            this.txtRDMBatchNO.TabIndex = 113;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(6, 7);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 20);
            this.label9.TabIndex = 112;
            this.label9.Text = "Batch No";
            // 
            // cmbRDMVehicle
            // 
            this.cmbRDMVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRDMVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRDMVehicle.FormattingEnabled = true;
            this.cmbRDMVehicle.Location = new System.Drawing.Point(336, 1);
            this.cmbRDMVehicle.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRDMVehicle.Name = "cmbRDMVehicle";
            this.cmbRDMVehicle.Size = new System.Drawing.Size(194, 32);
            this.cmbRDMVehicle.TabIndex = 111;
            this.cmbRDMVehicle.Click += new System.EventHandler(this.cmbRDMVehicle_Click);
            // 
            // RMC_ModBus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1924, 875);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1036, 533);
            this.Name = "RMC_ModBus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RMC Modbus";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RMC_ModBus_Load_2);
            this.gbAct1.ResumeLayout(false);
            this.gbAct1.PerformLayout();
            this.gbAct2.ResumeLayout(false);
            this.gbAct2.PerformLayout();
            this.gbSet1.ResumeLayout(false);
            this.gbSet1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlComman.ResumeLayout(false);
            this.pnlComman.PerformLayout();
            this.gbSet3.ResumeLayout(false);
            this.gbSet3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.gbAct4.ResumeLayout(false);
            this.gbAct4.PerformLayout();
            this.gbSet4.ResumeLayout(false);
            this.gbSet4.PerformLayout();
            this.gbSet2.ResumeLayout(false);
            this.gbSet2.PerformLayout();
            this.gbAct3.ResumeLayout(false);
            this.gbAct3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlRDMText.ResumeLayout(false);
            this.pnlRDMText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBox txtAGG1_Act;
        private TextBox txtAGG4_Act;
        private TextBox txtAGG2_Act;
        private TextBox txtAGG3_Act;
        private Label label66;
        private Label label67;
        private Label label68;
        private Label label69;
        private Label label70;
        private TextBox txtAGG5_Act;
        private Label label71;
        private GroupBox gbAct1;
        private TextBox txtAGG6_Act;
        private Label label62;
        private GroupBox gbAct2;
        private TextBox txtCEM2_Act;
        private Label label63;
        private TextBox txtCEM1_Act;
        private TextBox txtCEM3_Act;
        private TextBox txtCEM4_Act;
        private Label label64;
        private Label label65;
        private GroupBox gbSet1;
        private TextBox txtagg1;
        private Label label28;
        private TextBox txtagg4;
        private Label label35;
        private TextBox txtagg2;
        private TextBox txtagg6;
        private TextBox txtagg3;
        private TextBox txtagg5;
        private Label label33;
        private Label label44;
        private Label label32;
        private Label label30;
        private Label label72;
        private TextBox txtWCorr_Act;
        private Label label55;
        private Button btnstop;
        private Timer timer1;
        private TabPage tabPage1;
        public DataGridView dgv1;
        private Panel panel1;
        private TabControl tabControl1;
        private Label label57;
        private Label lblisLive;
        private Label label21;
        private Panel panel3;
        private DateTimePicker txtDateToday;
        public ComboBox cmbContractor;
        private Label lblInfo;
        private ComboBox cmbWorkName;
        private Label label42;
        private Label label40;
        public ComboBox cmbjobsite;
        private Label lblworktype;
        private Label label31;
        private Label txtBatchIndex;
        private TextBox txtplantcode;
        private Label label13;
        private TextBox txtcontractorid;
        private Label label6;
        private TextBox txtworkid;
        private Label label39;
        private TextBox txtbatchsize;
        private Button btnClear;
        private Button btnStart;
        private Panel pnlComman;
        private Label label53;
        public ComboBox cmbRecipe;
        private Label label22;
        private TextBox txtRcp;
        private TextBox txtFILLER_Act;
        private TextBox txtWTR2_Act;
        private Label label75;
        private TextBox txtWCorr;
        private Label label15;
        private Button btnClose;
        private Label lblTarget;
        private Label lblActual;
        private GroupBox gbSet3;
        private TextBox txtwater1;
        private Label label18;
        private TextBox txtwater2;
        private Label label12;
        private TextBox txtfiller;
        private Panel panel5;
        private GroupBox gbAct4;
        private Label label73;
        private TextBox txtADM21_Act;
        private TextBox txtADM22_Act;
        private Label label74;
        private Label label61;
        private TextBox txtADM11_Act;
        private TextBox txtADM12_Act;
        private Label label58;
        private GroupBox gbSet4;
        private TextBox txtSlurry;
        private TextBox txtadmix1;
        private Label label54;
        private Label label16;
        private TextBox txtSillica;
        private TextBox txtadmix2;
        private Label label7;
        private Label label52;
        private GroupBox gbSet2;
        private Label label20;
        private TextBox txtCement2;
        private Label label51;
        private Label label19;
        private Label label50;
        private TextBox txtCement1;
        private TextBox txtCement4;
        private TextBox txtCement3;
        private GroupBox gbAct3;
        private Label label60;
        private TextBox txtWTR1_Act;
        private Label label11;
        private Panel panel2;
        private Panel pnlRDMText;
        private Label lb_Recipe;
        private TextBox txtRDM_ProdQty;
        private Label label17;
        private TextBox txtRDMBatchNO;
        private Label label9;
        private ComboBox cmbRDMVehicle;
        private Button btnVehicle;
        private Button btnRecipe;
    }
}
