using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyNsStreamWatcher
{
    public partial class AboutLayout : UserControl
    {

        public Button btnClosePagePublic;

        public AboutLayout()
        {
            InitializeComponent();

            btnClosePagePublic = btnClosePage;
        
            lblPageTitle.Text = "About";

            lblProductName.Text = ApplicationInfo.Name;

            lblProductVersion.Text = "Version " + ApplicationInfo.Version; 

            lblAuthor.Text = ApplicationInfo.Author;

            linklblWebsite.Text = ApplicationInfo.Website;
            linklblWebsite.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblWebsite_LinkClicked);


        }



        private void AboutLayout_Load(object sender, EventArgs e)
        {



        }

        void linklblWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(ApplicationInfo.Website);
        }


    

    }
}
