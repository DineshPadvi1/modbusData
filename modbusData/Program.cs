using PDF_File_Reader;
using RMC_CP30_Simem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uniproject;
using Uniproject.Classes;
using Uniproject.UtilityTools;

namespace modbusData
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //////////Application.EnableVisualStyles();
            ////////// Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new mdiMain());
            //////////SingleApplication.Run(new mdiMain());//S
            //Application.Run(new RMC_ModBus());
            //Application.Run(new Form1());



            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                clsPatch.CreateNewColumns();

                // To check that Software is registered or not.
                clsFunctions.IsEgaleReg = RegisterSoftware_Uni.IsRMCRegister();

                if (clsFunctions.IsEgaleReg == null)
                {
                    clsFunctions.ErrorLog("clsFunctions.IsEgaleReg = null");
                    clsFunctions_comman.UniBox("clsFunctions.IsEgaleReg = null");
                    return;
                }

                if (clsFunctions.IsEgaleReg != "1")
                {
                    //clsFunctions.ErrorLog11("inside program - RegisterSoftware_Uni.IsRMCRegister()");
                    //clsFunctions.IsEgaleReg=RegisterSoftware_Uni.IsRMCRegister();// for Other
                }

                //------------------------------------------------------------------------------------------------------------

                if (clsFunctions.IsEgaleReg.Contains("Your software is expired please renew it. Data will not upload untill software is not renew"))
                {
                    // Display Software is Expired message.

                    clsFunctions_comman.ErrorLog("Your software is expired please renew it. Data will not upload untill software is not renew");
                    clsFunctions_comman.UniBox("Your software is expired please renew it. Data will not upload untill software is not renew");

                    //--------------------------------

                    SingleApplication.Run(new mdiMain());//SingleApplication.Run(new frmEagleBatchMAster()); // Edited by sanjay
                    return;
                }

                //------------------------------------------------------------------------------------------------------------

                else if (clsFunctions.IsEgaleReg != "1")
                {
                    // Open Registration Form because Software is not registered.

                    clsFunctions_comman.UniBox(clsFunctions.IsEgaleReg);

                    SingleApplication.Run(new UniRegister_Auto());              // BhaveshT 22/12/2023
                    //SingleApplication.Run(new RegistrationWizardForm());              // BhaveshT 19/07/2024  - testing registration wizard

                    if (clsFunctions.IsEgaleReg == "1")
                    {
                        clsFunctions.IsEgaleReg = "RegSuccessNowExit";
                        Application.Exit();
                    }
                }

                //------------------------------------------------------------------------------------------------------------

                if (clsFunctions.IsEgaleReg == "1")
                {
                    // Run WelcomeScreen
                    WelcomeScreen wc = new WelcomeScreen();             // Added by BhaveshT 10/02/2025
                    wc.ShowDialog();

                    if (clsFunctions.flag == true)
                    {
                        // Run Software.
                        clsFunctions_comman.ErrorLog("Inside Program.cs");
                        SingleApplication.Run(new mdiMain());                       // Modified by BhaveshT 
                    }

                    //// Run Software.
                    //clsFunctions_comman.ErrorLog("Inside Program.cs");
                    //SingleApplication.Run(new mdiMain());                       // Modified by BhaveshT 

                }

                //------------------------------------------------------------------------------------------------------------

            }
            catch (Exception e)
            {
                clsFunctions_comman.ErrorLog("Exception Inside program main - exception " + e);
                Application.Exit();
            }

        }
    }
}
