using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyNsStreamWatcher
{
    class About : Page
    {

        private Panel panelPage;

        public About(UI ui)
            : base(ui)
        {

            this.name = "About";

            CreatePage();

        }

        public Panel GetPageControl()
        {

            return this.panelPage;

        }
        

        private void CreatePage()
        {
            

            // Panel : Page

            panelPage = new Panel();
            panelPage.Visible = false;
            panelPage.Dock = DockStyle.Fill;
            panelPage.BackColor = Color.White;


            AboutLayout aboutLayout = new AboutLayout();
            aboutLayout.btnClosePagePublic.Click += new EventHandler(delegate(Object o, EventArgs a)
            {

                this.ui.ClosePage(this);

            });

            panelPage.Controls.Add(aboutLayout);



        }

        // Page functions

        override public void ShowPage()
        {

            this.panelPage.Visible = true;
            this.panelPage.BringToFront();

        }

        override public void ClosePage()
        {
            this.panelPage.Visible = false;

        }

    }
}
