using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WhyNsStreamWatcher
{
    public static class ApplicationInfo
    {

        private static Assembly code = Assembly.GetExecutingAssembly();

        public static bool IsClickOnce
        {
        
            get
            {
                return System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed;

            }
        
        }
        
        public static string Name
        {
        
            get
            {


                string Title = "N/A";
                if (Attribute.IsDefined(code, typeof(AssemblyDescriptionAttribute)))
                {

                    AssemblyTitleAttribute asTitle = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(code, typeof(AssemblyTitleAttribute));
                    Title = asTitle.Title;

                }

                return Title;

            }
        
        }

        public static string Version
        {
        
            get
            {
                if (IsClickOnce)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
                else
                {

                    return "N/A";
                }
                    
            }
        
        }

        public static string Publisher
        {

            get
            {
                string Company = "N/A";
                if (Attribute.IsDefined(code, typeof(AssemblyCompanyAttribute)))
                {

                    AssemblyCompanyAttribute asCompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(code, typeof(AssemblyCompanyAttribute));
                    Company = asCompany.Company;

                }

                return Company;

            }

        }

        public static string Author
        {
        
            get
            {
                return "Charlie Christiansen (WhyN)";

            }
        
        }

        public static string Website
        {

            get
            {
                return "http://streamwatcher.whyns.dk";

            }

        }  


    }
}
