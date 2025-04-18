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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RMC_ModBus));
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTarget = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbSet3 = new System.Windows.Forms.GroupBox();
            this.label75 = new System.Windows.Forms.Label();
            this.txtWCorr = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtwater1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtwater2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtfiller = new System.Windows.Forms.TextBox();
            this.btnstop = new System.Windows.Forms.Button();
            this.gbSet4 = new System.Windows.Forms.GroupBox();
            this.txtSlurry = new System.Windows.Forms.TextBox();
            this.txtadmix1 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSillica = new System.Windows.Forms.TextBox();
            this.txtadmix2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbSet2 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCement2 = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.txtCement1 = new System.Windows.Forms.TextBox();
            this.txtCement4 = new System.Windows.Forms.TextBox();
            this.txtCement3 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
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
            this.lblActual = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWater = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAgg = new System.Windows.Forms.TextBox();
            this.panel5.SuspendLayout();
            this.gbSet3.SuspendLayout();
            this.gbSet4.SuspendLayout();
            this.gbSet2.SuspendLayout();
            this.gbSet1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel5.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel5.Controls.Add(this.lblTarget);
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.gbSet3);
            this.panel5.Controls.Add(this.btnstop);
            this.panel5.Controls.Add(this.gbSet4);
            this.panel5.Controls.Add(this.btnClear);
            this.panel5.Controls.Add(this.gbSet2);
            this.panel5.Controls.Add(this.btnStart);
            this.panel5.Controls.Add(this.gbSet1);
            this.panel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(13, 23);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(986, 106);
            this.panel5.TabIndex = 22;
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarget.ForeColor = System.Drawing.Color.LawnGreen;
            this.lblTarget.Location = new System.Drawing.Point(15, 42);
            this.lblTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(93, 24);
            this.lblTarget.TabIndex = 153;
            this.lblTarget.Text = "TARGET";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.Location = new System.Drawing.Point(896, 69);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 32);
            this.btnClose.TabIndex = 151;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = false;
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
            this.gbSet3.Location = new System.Drawing.Point(468, 0);
            this.gbSet3.Margin = new System.Windows.Forms.Padding(4);
            this.gbSet3.Name = "gbSet3";
            this.gbSet3.Padding = new System.Windows.Forms.Padding(4);
            this.gbSet3.Size = new System.Drawing.Size(133, 105);
            this.gbSet3.TabIndex = 157;
            this.gbSet3.TabStop = false;
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
            // btnstop
            // 
            this.btnstop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnstop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnstop.Location = new System.Drawing.Point(831, 37);
            this.btnstop.Margin = new System.Windows.Forms.Padding(4);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(129, 32);
            this.btnstop.TabIndex = 54;
            this.btnstop.Text = "STOP";
            this.btnstop.UseVisualStyleBackColor = false;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
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
            this.gbSet4.Location = new System.Drawing.Point(607, 0);
            this.gbSet4.Margin = new System.Windows.Forms.Padding(4);
            this.gbSet4.Name = "gbSet4";
            this.gbSet4.Padding = new System.Windows.Forms.Padding(4);
            this.gbSet4.Size = new System.Drawing.Size(137, 105);
            this.gbSet4.TabIndex = 156;
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
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnClear.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClear.Location = new System.Drawing.Point(831, 69);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 32);
            this.btnClear.TabIndex = 39;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
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
            this.gbSet2.Location = new System.Drawing.Point(326, 0);
            this.gbSet2.Margin = new System.Windows.Forms.Padding(4);
            this.gbSet2.Name = "gbSet2";
            this.gbSet2.Padding = new System.Windows.Forms.Padding(4);
            this.gbSet2.Size = new System.Drawing.Size(137, 105);
            this.gbSet2.TabIndex = 155;
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
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.ForestGreen;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnStart.Location = new System.Drawing.Point(831, 4);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(129, 32);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
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
            this.gbSet1.Location = new System.Drawing.Point(116, 0);
            this.gbSet1.Margin = new System.Windows.Forms.Padding(4);
            this.gbSet1.Name = "gbSet1";
            this.gbSet1.Padding = new System.Windows.Forms.Padding(4);
            this.gbSet1.Size = new System.Drawing.Size(204, 105);
            this.gbSet1.TabIndex = 154;
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
            // lblActual
            // 
            this.lblActual.AutoSize = true;
            this.lblActual.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActual.ForeColor = System.Drawing.Color.LawnGreen;
            this.lblActual.Location = new System.Drawing.Point(21, 49);
            this.lblActual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActual.Name = "lblActual";
            this.lblActual.Size = new System.Drawing.Size(90, 24);
            this.lblActual.TabIndex = 143;
            this.lblActual.Text = "ACTUAL";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblActual);
            this.panel1.Controls.Add(this.txtBatchNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtWater);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCem);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtAgg);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(13, 161);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(986, 106);
            this.panel1.TabIndex = 152;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(839, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 149;
            this.label4.Text = "Batch No";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.BackColor = System.Drawing.Color.White;
            this.txtBatchNo.Enabled = false;
            this.txtBatchNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchNo.Location = new System.Drawing.Point(834, 49);
            this.txtBatchNo.Margin = new System.Windows.Forms.Padding(5);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(113, 32);
            this.txtBatchNo.TabIndex = 150;
            this.txtBatchNo.Text = "0";
            this.txtBatchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(487, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 147;
            this.label3.Text = "WATER";
            // 
            // txtWater
            // 
            this.txtWater.BackColor = System.Drawing.Color.White;
            this.txtWater.Enabled = false;
            this.txtWater.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWater.Location = new System.Drawing.Point(482, 49);
            this.txtWater.Margin = new System.Windows.Forms.Padding(5);
            this.txtWater.Name = "txtWater";
            this.txtWater.ReadOnly = true;
            this.txtWater.Size = new System.Drawing.Size(113, 32);
            this.txtWater.TabIndex = 148;
            this.txtWater.Text = "0";
            this.txtWater.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(321, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 145;
            this.label2.Text = "CEMENT";
            // 
            // txtCem
            // 
            this.txtCem.BackColor = System.Drawing.Color.White;
            this.txtCem.Enabled = false;
            this.txtCem.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCem.Location = new System.Drawing.Point(316, 49);
            this.txtCem.Margin = new System.Windows.Forms.Padding(5);
            this.txtCem.Name = "txtCem";
            this.txtCem.ReadOnly = true;
            this.txtCem.Size = new System.Drawing.Size(113, 32);
            this.txtCem.TabIndex = 146;
            this.txtCem.Text = "0";
            this.txtCem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(133, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 116;
            this.label1.Text = "AGGRIGATES";
            // 
            // txtAgg
            // 
            this.txtAgg.BackColor = System.Drawing.Color.White;
            this.txtAgg.Enabled = false;
            this.txtAgg.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAgg.Location = new System.Drawing.Point(137, 49);
            this.txtAgg.Margin = new System.Windows.Forms.Padding(5);
            this.txtAgg.Name = "txtAgg";
            this.txtAgg.ReadOnly = true;
            this.txtAgg.Size = new System.Drawing.Size(113, 32);
            this.txtAgg.TabIndex = 116;
            this.txtAgg.Text = "0";
            this.txtAgg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RMC_ModBus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1018, 486);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1036, 533);
            this.MinimumSize = new System.Drawing.Size(1036, 533);
            this.Name = "RMC_ModBus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RMC Modbus";
            this.Load += new System.EventHandler(this.RMC_ModBus_Load_2);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.gbSet3.ResumeLayout(false);
            this.gbSet3.PerformLayout();
            this.gbSet4.ResumeLayout(false);
            this.gbSet4.PerformLayout();
            this.gbSet2.ResumeLayout(false);
            this.gbSet2.PerformLayout();
            this.gbSet1.ResumeLayout(false);
            this.gbSet1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel5;
        private Button btnClose;
        private Label lblActual;
        private Button btnstop;
        private Button btnClear;
        private Button btnStart;
        private Panel panel1;
        private Label label1;
        private TextBox txtAgg;
        private Label label3;
        private TextBox txtWater;
        private Label label2;
        private TextBox txtCem;
        private Label label4;
        private TextBox txtBatchNo;
        private Label lblTarget;
        private GroupBox gbSet3;
        private Label label75;
        private TextBox txtWCorr;
        private Label label15;
        private TextBox txtwater1;
        private Label label18;
        private TextBox txtwater2;
        private Label label12;
        private TextBox txtfiller;
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
    }
}
