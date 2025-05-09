using System;
using System.Windows.Forms;
using Uniproject.Classes;

namespace Uniproject.UtilityTools
{
    public partial class WelcomeScreen : Form
    {
        public WelcomeScreen()
        {
            InitializeComponent();
            InitializeLoading();
        }

        private void WelcomeScreen_Load(object sender, EventArgs e)
        {
            try
            {
                string mode = clsFunctions.loadSingleValueSetup("Select PlantType from PlantSetup");
                if (mode.Contains("Bit"))
                {
                    this.BackgroundImage = modbusData.Properties.Resources.BT; // Set the background image
                }
                else
                {
                    this.BackgroundImage = modbusData.Properties.Resources.RMC_LOADING1; // Set the background image
                }
                clsFunctions_comman.ErrorLog("Inside WelcomeScreen.cs");

                lb_version.Text = "| v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] WelcomeScreen - WelcomeScreen_Load : " + ex.Message);
            }
        }

        //----------

        private int step = 0;
        private Timer timer;

        private void InitializeLoading()
        {
            try
            {
                timer = new Timer();
                timer.Interval = 1500;          // Each step will take 2 sec
                timer.Tick += Timer_Tick;

                lbl_Message.Text = "Checking Device ID...";

                timer.Start();
            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] WelcomeScreen - InitializeLoading : " + ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                step++;
                switch (step)
                {
                    case 1:
                        lbl_Message.Text = "Finding UID...";
                        break;
                    case 2:
                        lbl_Message.Text = "Checking with Server...";
                        break;
                    case 3:
                        lbl_Message.Text = "Validation done.";
                        break;
                    case 4:
                        timer.Stop();
                        clsFunctions.flag = true;

                        this.Close();       // Close the splash screen

                        break;
                }

                //if (clsFunctions.flag == true)
                //{
                //    // Run Software.
                //    SingleApplication.Run(new mdiMain());             // Modified by BhaveshT 
                //}

            }
            catch (Exception ex)
            {
                clsFunctions.ErrorLog("[Exception] WelcomeScreen - Timer_Tick : " + ex.Message);
            }
        }

    }
}
