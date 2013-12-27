using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyNsStreamWatcher
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            formMain.LoggerReset();
            formMain.Logger("Application Start");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool ok;
            var m = new System.Threading.Mutex(true, "WhyNsStreamWatcher", out ok);
            if (!ok)
            {
                MessageBox.Show(
                    "The Application is already running!\n(If there was an update before this, close the Application and reopen)",
                    ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                );

            }
            else {

                //CreateClickOnceShortCutOnDesktop();
                Application.Run(new formMain());
            
            }

            
        }




        /*
        private static void CreateClickOnceShortCutOnDesktop()
        {

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {

                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                if (ad.IsFirstRun)
                {

                    Assembly code = Assembly.GetExecutingAssembly();
                    string company = string.Empty;
                    string description = string.Empty;

                    if (Attribute.IsDefined(code, typeof(AssemblyCompanyAttribute)))
                    {

                        AssemblyCompanyAttribute ascompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(code, typeof(AssemblyCompanyAttribute));
                        company = ascompany.Company;

                    }
                    if (Attribute.IsDefined(code, typeof(AssemblyDescriptionAttribute)))
                    {

                        AssemblyDescriptionAttribute asdescription = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(code, typeof(AssemblyDescriptionAttribute));
                        description = asdescription.Description;

                    }

                    if (company != string.Empty && description != string.Empty)
                    {

                        string desktopPath = string.Empty;
                        desktopPath = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\", description, ".appref-ms");

                        string shotcutName = string.Empty;
                        shotcutName = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "\\", company, "\\", description, ".appref-ms");

                        System.IO.File.Copy(shotcutName, desktopPath, true);
                    }

                }

            }

        }
        */


    }
}
