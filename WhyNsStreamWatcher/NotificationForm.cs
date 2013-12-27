using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhyNsStreamWatcher
{
    public enum NotificationScreenPosition
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight

    }

    class NotificationForm : Form
    {

        private UI ui;

        private  Rectangle WorkingArea = Screen.PrimaryScreen.WorkingArea;

        private bool TaskBar;
        private int TaskBarHeight;

        private Point PositionStart;
        private Point PositionEnd;



        public NotificationScreenPosition ScreenPosition;
        public Padding EdgeSpacing;
        public Control CustomControl = null; 

        // Timers
        private Timer timerShowAnimate = new Timer();
        private Timer timerDisplayed = new Timer();
        private Timer timerCloseAnimate = new Timer();

        // Animation 
        int i = 0;
        private double OpacityGain;
        int ActiveNotificationsHeight = 0;
        

        // Close Control

        Button btnCloseNotification = new Button();


        // Default Controls
        
        Label lblTitle = new Label();
        Label lblMessage = new Label();

        public string Title = "This is the Title";
        public string Message = "This is a Message";


        public NotificationForm(UI ui) {

            this.ui = ui;

            // Timers
            timerShowAnimate.Interval = 1;
            timerShowAnimate.Tick += new EventHandler(timerShowAnimate_Tick);

            int Interval = Properties.Settings.Default.NotificationDisplayTime;
            timerDisplayed.Interval = Interval == 0 ? 1 : Interval;
            timerDisplayed.Tick += new EventHandler(timerDisplayed_Tick);

            timerCloseAnimate.Interval = 1;
            timerCloseAnimate.Tick += new EventHandler(timerCloseAnimate_Tick);

            

            // Default Settings for a Notification

            ScreenPosition = Properties.Settings.Default.NotificationScreenPosition;

            EdgeSpacing = new Padding(10,10,10,10);
            this.Size = new Size(350, 100);
            this.BackColor = Color.White;

           this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Load += new EventHandler(NotificationForm_Load);


            // Close Control

            btnCloseNotification.Text = "x";
            btnCloseNotification.Size = new Size(19, 20);
            btnCloseNotification.BackColor = System.Drawing.Color.Transparent;
            btnCloseNotification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            btnCloseNotification.FlatAppearance.BorderSize = 0;
            btnCloseNotification.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            btnCloseNotification.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btnCloseNotification.UseVisualStyleBackColor = false;
            btnCloseNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            btnCloseNotification.Click += btnCloseNotification_Click;


            // Default Controls


            lblTitle.AutoSize = true;
            lblTitle.Font = new Font(lblTitle.Font, FontStyle.Bold);
            lblTitle.Location = new Point(10,10);


            lblMessage.AutoSize = true;
            //lblMessage.Font = new Font(lblTitle.Font, FontStyle.Bold);
            lblMessage.Location = new Point(10, 40);





            this.ControlAdded += NotificationForm_ControlAdded;

            
        }

        void NotificationForm_ControlAdded(object sender, ControlEventArgs e)
        {
            
        }


        void NotificationForm_Load(object sender, EventArgs e)
        {


            if (this.CustomControl == null)
            {

                lblTitle.Text = Title;
                lblMessage.Text = Message;
                btnCloseNotification.Location = new Point(this.Width - btnCloseNotification.Width - 2, -2);
                this.Controls.Add(btnCloseNotification);
                this.Controls.Add(lblTitle);
                this.Controls.Add(lblMessage);

            }
            else {

                this.Controls.Add(this.CustomControl);
            
            }

            this.StartPosition = FormStartPosition.Manual;

            NotificationPosition(this.ScreenPosition);

            this.Location = this.PositionStart;
            

            // Animation
            OpacityGain = (double)1 / (double)(this.Height + TaskBarHeight + ActiveNotificationsHeight);
            
            // Start Show Animation
            timerShowAnimate.Start();
            timerDisplayed.Start();
            
        }

        public void AddControl(Control c) {

            this.Controls.Add(c);
        
        }


        void btnCloseNotification_Click(object sender, EventArgs e)
        {

            ui.RemoveActiveNotification(this);
            this.Close();
        }

        


        private void NotificationPosition(NotificationScreenPosition ScreenPosition)
        {


            List<NotificationForm> ActiveNotifications = ui.NewActiveNotification(this);

            foreach(NotificationForm an in  ActiveNotifications){

                if (an != this){

                    Padding Edge = an.EdgeSpacing;

                    ActiveNotificationsHeight += an.Height + Edge.Top;

                }
            }

            TaskBarHeight = Screen.PrimaryScreen.Bounds.Bottom - Screen.PrimaryScreen.WorkingArea.Bottom;

            switch (ScreenPosition)
            {
                // Top Left
                case NotificationScreenPosition.TopLeft:

                    TaskBar = false;
                    
                    this.PositionStart = new Point(
                        Screen.PrimaryScreen.Bounds.Left + EdgeSpacing.Left,
                        Screen.PrimaryScreen.Bounds.Top - this.Height
                    );

                    this.PositionEnd = new Point(
                        Screen.PrimaryScreen.Bounds.Left + EdgeSpacing.Left,
                        Screen.PrimaryScreen.Bounds.Top - this.Height + EdgeSpacing.Top
                    );


                    break;
                // Top Right
                case NotificationScreenPosition.TopRight:

                    TaskBar = false;

                    this.PositionStart = new Point(
                        Screen.PrimaryScreen.Bounds.Right - this.Width - EdgeSpacing.Right,
                        Screen.PrimaryScreen.Bounds.Top - this.Height
                    );

                    this.PositionEnd = new Point(
                        Screen.PrimaryScreen.Bounds.Right - this.Width - EdgeSpacing.Right,
                        Screen.PrimaryScreen.Bounds.Top - this.Height + EdgeSpacing.Top
                    );

                    break;
                // Bottom Left
                case NotificationScreenPosition.BottomLeft:

                    TaskBar = true;
                    

                    this.PositionStart = new Point(
                        Screen.PrimaryScreen.Bounds.Left + EdgeSpacing.Left,
                        Screen.PrimaryScreen.Bounds.Bottom
                    );

                    this.PositionEnd = new Point(
                        Screen.PrimaryScreen.Bounds.Left + EdgeSpacing.Left,
                        Screen.PrimaryScreen.Bounds.Bottom - EdgeSpacing.Bottom
                    );

                    break;
                // Bottom Right
                case NotificationScreenPosition.BottomRight:

                    TaskBar = true;


                    this.PositionStart = new Point(
                        Screen.PrimaryScreen.Bounds.Right - this.Width - EdgeSpacing.Right,
                        Screen.PrimaryScreen.Bounds.Bottom
                    );

                    this.PositionEnd = new Point(
                        Screen.PrimaryScreen.Bounds.Right - this.Width - EdgeSpacing.Right,
                        Screen.PrimaryScreen.Bounds.Bottom - EdgeSpacing.Bottom
                    );

                    break;
            
            }

            if (!TaskBar) { TaskBarHeight = 0; };
        
        }



        // Timer Methods

        // Show Animation
        void timerShowAnimate_Tick(object sender, EventArgs e)
        {
            i += 1;
            this.Opacity += OpacityGain;

            if (ScreenPosition == NotificationScreenPosition.TopLeft || ScreenPosition == NotificationScreenPosition.TopRight)
            {
                this.Location = new Point(PositionEnd.X, PositionEnd.Y + i);
            }

            if (ScreenPosition == NotificationScreenPosition.BottomLeft || ScreenPosition == NotificationScreenPosition.BottomRight) {

                this.Location = new Point(PositionEnd.X, PositionEnd.Y - i);
            }



            if (i >= (this.Height + TaskBarHeight + ActiveNotificationsHeight))
            {

                this.timerShowAnimate.Stop();
                this.TopMost = true;  // Might need a setting for this!
                //timerDisplayed.Start();
            }

        }

        // Display Timer
        private void timerDisplayed_Tick(object sender, EventArgs e)
        {
            timerDisplayed.Stop();
            ui.RemoveActiveNotification(this);
            timerCloseAnimate.Start();
        }


        // Close Animation
        private void timerCloseAnimate_Tick(object sender, EventArgs e)
        {
            this.Opacity -= .01;

            if (this.Opacity <= 0)
            {
                timerCloseAnimate.Stop();
                this.Close();
            }
        }



    }
}
