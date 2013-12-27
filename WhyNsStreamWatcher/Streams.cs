using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyNsStreamWatcher
{
    class Streams : Page
    {

        // Controls
        private Panel panelStreams = new Panel();
        private Button btnGetFollowingChannels = new Button();
        private FlowLayoutPanel flpStreams = new FlowLayoutPanel();

        private Label lblNextRefresh = new Label();
        private Label lblStreamsOffline = new Label();

        // Stream Clients API

        private TwitchAPI twitchAPI;

        private List<Channel> OldChannels = null;

        // Auto Refresh

        public Thread AutoUpdateThread;


        public Streams(UI ui)
            : base(ui)
        {

            this.name = "Streams";

            CreatePage();
            
            AutoUpdateThread = new Thread(this.AutoUpdate);

            AutoUpdateThread.CurrentUICulture = new CultureInfo("en-us");

            AutoUpdateThread.Start();


        }


        public Panel GetPageControl()
        {

            return this.panelStreams;

        }


        private void CreatePage()
        {


            // Panel: Streams

            this.panelStreams.Visible = false;
            this.panelStreams.Dock = DockStyle.Fill;
            this.panelStreams.BackColor = Color.Transparent;


            // Button : GetFollowingChannels

            //this.btnGetFollowingChannels.Location = new System.Drawing.Point(20, 18);
            //this.btnGetFollowingChannels.Name = "btnGetFollowingChannels";
            //this.btnGetFollowingChannels.AutoSize = true;
            //this.btnGetFollowingChannels.Text = "Show Notification Pop-up (Test)";
            //this.btnGetFollowingChannels.UseVisualStyleBackColor = true;
            //this.btnGetFollowingChannels.Click += new System.EventHandler(this.btnGetFollowingChannels_Click);

            



            // FlowLayoutPanel : Streams
            
            flpStreams.Visible = true;
            flpStreams.Name = "flpStreams";
            flpStreams.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flpStreams.Location = new System.Drawing.Point(10, 10);
            flpStreams.Size = new System.Drawing.Size(426, 540);
            flpStreams.AutoScroll = true;
            flpStreams.WrapContents = false;
            flpStreams.MouseEnter += new EventHandler(flpStreams_MouseEnter);
            //flpStreams.BackColor = Color.FromArgb(48, 48, 48);
            flpStreams.BackColor = Color.Transparent;



            // Label : NextRefresh

            lblNextRefresh.AutoSize = true;
            lblNextRefresh.Name = "lblNextRefresh";
            lblNextRefresh.Text = "Refreshing";
            lblNextRefresh.Location = new System.Drawing.Point(15, 565);
            lblNextRefresh.Font = new Font("Arial", 8, FontStyle.Italic);


            // Label : StreamsOffline
            lblStreamsOffline.AutoSize = true;
            lblStreamsOffline.Name = "lblStreamsOffline";
            lblStreamsOffline.Location = new System.Drawing.Point(flpStreams.Width - lblStreamsOffline.Width -15, 565);
            lblStreamsOffline.Font = new Font("Arial", 8, FontStyle.Italic);


            // Add Controls
            //this.panelStreams.Controls.Add(this.btnGetFollowingChannels);
            this.panelStreams.Controls.Add(this.flpStreams);
            this.panelStreams.Controls.Add(this.lblNextRefresh);
            this.panelStreams.Controls.Add(this.lblStreamsOffline);


        }

        void flpStreams_MouseEnter(object sender, EventArgs e)
        {
            flpStreams.Focus();
        }

        
        private void btnGetFollowingChannels_Click(object sender, EventArgs e)
        {


            NotificationForm nfTest = new NotificationForm(ui);
            nfTest.Show();

            //nfTest = new NotificationForm(ui);
            //nfTest.Show();

            //nfTest = new NotificationForm(ui);
            //nfTest.Show();

            //nfTest = new NotificationForm(ui);
            //nfTest.Show();

        }



        // Clear FlowLayoutPanel Controls
        // Adds to the FlowLayoutPanel Controls
        public delegate void AddControlsCallback(List<Control> controls);
        private void AddControls(List<Control> controls)
        {

            Point pScrollPos = flpStreams.AutoScrollPosition;

            // Adds the Controls
            this.flpStreams.Visible = false;
            this.flpStreams.Controls.Clear();
            this.flpStreams.Controls.AddRange(controls.ToArray());

            flpStreams.AutoScrollPosition = new Point(Math.Abs(pScrollPos.X), Math.Abs(pScrollPos.Y));

            this.flpStreams.Visible = true;

        }


        public delegate void UpdateNextRefreshLabelCallback(String text);
        private void UpdateNextRefreshLabel(string text)
        {

            lblNextRefresh.Text = text;

        }

        public delegate void UpdateOfflineStreamsLabelCallback(String text);
        private void UpdateOfflineStreamsLabel(string text)
        {

            lblStreamsOffline.Text = text;

        }


        public delegate void ShowNotificationCallback(NotificationForm Notification);
        private void ShowNotification(NotificationForm Notification)
        {

            Notification.Show();

        }


        private void AutoUpdate()
        {
            

            bool AutoUpdate = Properties.Settings.Default.StreamsAutoUpdate;
            int UpdateInterval = Properties.Settings.Default.StreamsAutoUpdateInterval;
            string NextRefreshText;




            while (true)
            {

                AutoUpdate = Properties.Settings.Default.StreamsAutoUpdate;

                while (AutoUpdate)
                {

                    if (!Properties.Settings.Default.StreamsAutoUpdate) { break; }



                    List<Control> listStreamControls = UpdateStreams();
                    

                    this.flpStreams.Invoke(new AddControlsCallback(this.AddControls), new object[] { listStreamControls });


                    int SleepTime = 1000;
                    int Countdown = UpdateInterval;

                    while (Countdown >= 0)
                    {
                        if (!Properties.Settings.Default.StreamsAutoUpdate) { break; }

                        if (UpdateInterval != Properties.Settings.Default.StreamsAutoUpdateInterval) {

                            UpdateInterval = Properties.Settings.Default.StreamsAutoUpdateInterval;
                            Countdown = UpdateInterval;
                        }

                        TimeSpan t = TimeSpan.FromMilliseconds(Countdown);
                        NextRefreshText = string.Format("Next Refresh in {0:D1}:{1:D2}", t.Minutes, t.Seconds);

                        this.lblNextRefresh.Invoke(new UpdateNextRefreshLabelCallback(this.UpdateNextRefreshLabel), new object[] { NextRefreshText });

                        Countdown -= SleepTime;

                        Thread.Sleep(SleepTime);

                    }

                    NextRefreshText = "Refreshing";
                    this.lblNextRefresh.Invoke(new UpdateNextRefreshLabelCallback(this.UpdateNextRefreshLabel), new object[] { NextRefreshText });

                }

                NextRefreshText = "Auto Refresh Disabled!";
                this.lblNextRefresh.Invoke(new UpdateNextRefreshLabelCallback(this.UpdateNextRefreshLabel), new object[] { NextRefreshText });

                Thread.Sleep(5000);

            }

        }
        


        private List<Control> UpdateStreams()
        {

            // Creating new TwitchAPI based on username
            twitchAPI = new TwitchAPI(Properties.Settings.Default.TwitchUsername);

            Label lblErrorStatus = new Label();
            lblErrorStatus.AutoSize = true;
            lblErrorStatus.Margin = new Padding(30);
            lblErrorStatus.Font = new Font(lblErrorStatus.Font, FontStyle.Bold);


            if (Properties.Settings.Default.TwitchUsername.Length != 0)
            {

                // List for Channel Controls
                List<Control> listStreamControls = new List<Control>();

                List<Channel> NewChannels;

                // Getting Twitch channels
                List<Channel> TwitchChannels = this.twitchAPI.GetChannelsUserIsFollowing();

                // Merging Channels from Stream Clients
                try
                {
                    NewChannels = new List<Channel>(TwitchChannels);
                }
                catch {

                    NewChannels = null;
                }

                // New List for Channels
                List<Channel> NewChangedChannels = new List<Channel>();

                // New Channel Object for Channel
                Channel ChangedChannel = new Channel();

                if (NewChannels != null)
                { 

                    string OfflineStreamsText = string.Empty;
                    int i = 0;
                    foreach(Channel NewChannel in NewChannels)
                    {

                        ChangedChannel = NewChannel;

                        if (OldChannels != null)
                        {
                            foreach (Channel OldChannel in OldChannels)
                            {

                                if (NewChannel.id == OldChannel.id)
                                {
                                    // Checking for Changes and returning changed channel
                                    ChangedChannel = CheckForStreamChanges(NewChannel, OldChannel);

                                    break;
                                }

                            }
                        }
                        else
                        {
                            // If no OldChannels
                                               
                            // First time Notifications at Application Start
                            if (NewChannel.live)
                            {
                                // Channel Is Live!
                                ChangedChannel.IsLive = true;

                            }

                        }

                        // Adding Changed Channel to List
                        NewChangedChannels.Add(ChangedChannel);


                        // Showing Notification if Any 
                        ShowNotification(ChangedChannel);

                        // Creating control for Channel
                        Control ChannelControl = new ChannelLayout(ChangedChannel);

                        if (ChangedChannel.live)
                        {

                            listStreamControls.Add(ChannelControl);

                        }
                        else if (Properties.Settings.Default.ShowOfflineStreams)
                        {

                            listStreamControls.Add(ChannelControl);
                            OfflineStreamsText = "";
                        }
                        else
                        {
                            i++;

                            OfflineStreamsText = "(" + i + " streams offline)";

                        }


                    }

                    this.lblStreamsOffline.Invoke(new UpdateOfflineStreamsLabelCallback(this.UpdateOfflineStreamsLabel), new object[] { OfflineStreamsText });

                    OldChannels = NewChangedChannels; // Or Should it be NewChannels? Testing will show!

                    return listStreamControls;


                }

                // No data from Stream Clients
                lblErrorStatus.Text = "Something went worng when getting data from Twitch.tv!\n\nMake sure the entered username is valid!\nIf it is, Try again later...";

            }
            else
            {
                // No Username
                lblErrorStatus.Text = "No Username Entered!";                
            }

            // Creating Error Notification and Status
            NotificationForm nf = new NotificationForm(ui);
            nf.Title = "WhyNs Stream Watcher";
            nf.Message = lblErrorStatus.Text;

            Thread.Sleep(1000);

            this.panelStreams.Invoke(new ShowNotificationCallback(this.ShowNotification), new object[] { nf });

            return new List<Control>(new Control[] { lblErrorStatus });




        } // End of UpdateStreams()



        // Checking for Channel / Stream changes
        // By comparing NewChannel and OldChannel Data
        private Channel CheckForStreamChanges(Channel NewChannel, Channel OldChannel) {

            Channel ChangedChannel = NewChannel;

            // Channel Is Live Now!
            if (NewChannel.live && !OldChannel.live)
            {

                ChangedChannel.IsLiveNow = true;

            }


            // Channel Is Offline Now!
            if (!NewChannel.live && OldChannel.live)
            {

                ChangedChannel.IsOfflineNow = true;    

            }

            // Channel Is (still) Live!
            if (NewChannel.live) {

                // Gets and save the Difference in viewers since last refresh
                ChangedChannel.viewersLiveDifference = (NewChannel.viewersLive - OldChannel.viewersLive);

            }

            return ChangedChannel;

        }


        private void ShowNotification(Channel channel)
        {

            ChannelLayout ChannelControl = new ChannelLayout(channel);
            //ChannelControl.Location = new Point(0, 0);

            NotificationForm ChannelNotification = new NotificationForm(ui);
            ChannelNotification.FormBorderStyle = FormBorderStyle.None;
            ChannelNotification.Size = new Size(ChannelControl.Size.Width, ChannelControl.Size.Height);
            ChannelNotification.CustomControl = ChannelControl;


            if (channel.IsLive && Properties.Settings.Default.ChannelNotificationIsLive)
            {

                this.panelStreams.Invoke(new ShowNotificationCallback(this.ShowNotification), new object[] { ChannelNotification });

            }
            else if (channel.IsLiveNow && Properties.Settings.Default.ChannelNotificationIsLiveNow)
            {

                this.panelStreams.Invoke(new ShowNotificationCallback(this.ShowNotification), new object[] { ChannelNotification });

            }
            else if (channel.IsOfflineNow && Properties.Settings.Default.ChannelNotificationIsOfflineNow)
            {

                this.panelStreams.Invoke(new ShowNotificationCallback(this.ShowNotification), new object[] { ChannelNotification });

            }


        }

        


        // Page functions

        override public void ShowPage()
        {

            this.panelStreams.Visible = true;
            this.panelStreams.BringToFront();

        }

        override public void ClosePage()
        {
            this.panelStreams.Visible = false;

        }


    }
}
