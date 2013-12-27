using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
using System.Threading;



namespace WhyNsStreamWatcher
{
    public partial class formMain : Form
    {
        // User Interface

        private UI ui = new UI();


        // Pages

        private Streams pageStreams;
        private Settings pageSettings;
        private About pageAbout;


        // LogPath -> Need to make a new class and settings!

        public static string loggerPath = "d:\\WhyNs - Stream Watcher.log";

        public formMain()
        {

            InitializeComponent();
            

        }
              

        private void Form1_Load(object sender, EventArgs e)
        {



            // Form Settings

            this.Icon = Properties.Resources.WhyNs_Stream_Watcher_Icon_32x32;
                            
     




            // Top Bar Settings

            VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
            panelTopbarMainForm.Paint += new PaintEventHandler(delegate (Object o, PaintEventArgs a) {

                renderer.DrawEdge(a.Graphics, panelTopbarMainForm.ClientRectangle,
                    Edges.Bottom, EdgeStyle.Raised, EdgeEffects.Flat);

            });

            panelTopbarMainForm.MouseDown   += new MouseEventHandler(delegate(Object o, MouseEventArgs a) { this.OnMouseDown(a); });
            panelTopbarMainForm.MouseUp     += new MouseEventHandler(delegate(Object o, MouseEventArgs a) { this.OnMouseUp(a); });
            panelTopbarMainForm.MouseMove   += new MouseEventHandler(delegate(Object o, MouseEventArgs a) { this.OnMouseMove (a); });

            tsmiMain.Text += " " + ApplicationInfo.Version;





            // Tray Icon ContextMenuStrip Settings

            ContextMenuStrip cmsTrayIcon = new ContextMenuStrip();

            cmsTrayIcon.Name = "cmsTrayIcon";

            cmsTrayIcon.Items.Add("Restore", null, new EventHandler(restoreForm));
            cmsTrayIcon.Items.Add("Exit", null, new EventHandler(delegate (Object o, EventArgs a) {

                appExit();

            }));




            // Tray Icon Settings

            notifyIconMain.Icon = Properties.Resources.WhyNs_Stream_Watcher_Icon_16x16;
            notifyIconMain.ContextMenuStrip = cmsTrayIcon;
            notifyIconMain.Text = this.Text;
            notifyIconMain.MouseDoubleClick += new MouseEventHandler(restoreForm);





            // View Settings
            tsmiShowOfflineStreams.Checked = Properties.Settings.Default.ShowOfflineStreams;




            // Creating Pages

            pageStreams = new Streams(ui);
            pageSettings = new Settings(ui);
            pageAbout = new About(ui);


            // Adding Pages to Form (Should be hidden)

            this.Controls.Add(pageStreams.GetPageControl());
            this.Controls.Add(pageSettings.GetPageControl());
            this.Controls.Add(pageAbout.GetPageControl());



            // Setting Main Default Page

            ui.SetMainPage(pageStreams);


            // Load Default Page
            ui.ShowPage(this.pageStreams);
            




            // Hide Application to tray at launch if settings is true
            if (Properties.Settings.Default.TrayOnLaunch)
            {

                formToTray();

            }



        } // End of Form Load




        // Show Settings Page
        private void tsmiSettings_Click(object sender, EventArgs e)
        {

            ui.ShowPage(this.pageSettings);
            
        }


        // Show About Page
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ui.ShowPage(this.pageAbout);

        }





        // Toggle Showing Offline Streams
        private void tsmiShowOfflineStreams_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            if (Properties.Settings.Default.ShowOfflineStreams)
            {

                tsmi.Checked = false;
                
            }
            else
            {

                tsmi.Checked = true;

            }


            Properties.Settings.Default.ShowOfflineStreams = tsmi.Checked;
            Properties.Settings.Default.Save();

        }


        // Send form to System Tray
        private void formToTray(bool ShowBalloonTip = false)
        {

            NotificationForm Notification = new NotificationForm(ui);

            Notification.Title = "WhyNs Stream Watcher";
            Notification.Message = "The Application has been minimized to System Tray.\n\n(Right click icon to restore)";
            notifyIconMain.Visible = true;

            if (ShowBalloonTip) {
            
                Notification.Show();
            
            }

            this.ShowInTaskbar = false;    
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        // Restore Form from System Tray
        private void restoreForm(object sender, EventArgs e)
        {

            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        // Minimize Form
        private void btnMinimizeMainForm_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;

        }


        // Close Form
        private void appExit()
        {
            pageStreams.AutoUpdateThread.Abort();

            notifyIconMain.Visible = false;
            Application.Exit();
            
        }

        private void btnCloseMainForm_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.TrayOnClose)
            {

                formToTray(true);


            }
            else
            {

                appExit();

            }

        }

        public void appExit(object sender, EventArgs e)
        {
            appExit();
        
        }





        // Drag Form

        Point mouseDownPoint = Point.Empty;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            mouseDownPoint = new Point(e.X, e.Y);

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            mouseDownPoint = Point.Empty;

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mouseDownPoint.IsEmpty)
                return;
            
            this.Location = new Point(this.Location.X + (e.X - mouseDownPoint.X), this.Location.Y + (e.Y - mouseDownPoint.Y));

        }



        // Logger Functions // Need to make Class and Settings!


        public static void Logger(string textLine) {
           
            string logText = textLine;
            
            if(logText.Length != 0){
        
                //System.IO.StreamWriter file = new System.IO.StreamWriter(loggerPath, true);
                //file.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " " + logText);

                //file.Close();
            }
        }

        public static void LoggerReset()
        {

            //System.IO.StreamWriter file = new System.IO.StreamWriter(loggerPath, false);
            //file.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " > Log Reset");
            //file.Close();

        }





        



















    }
}
