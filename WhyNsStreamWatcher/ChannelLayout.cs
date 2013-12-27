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
    public partial class ChannelLayout : UserControl
    {

        private Channel channel;

        public ChannelLayout(Channel c)
        {
            InitializeComponent();

            this.channel = c;

            
            linklblChannelDisplayName.Text = this.channel.display_name;

            try
            {
                pbChannelPicture.Load(this.channel.logo);
            }
            catch {

                pbChannelPicture.Load("http://static-cdn.jtvnw.net/jtv_user_pictures/xarth/404_user_150x150.png");
            }


            if (this.channel.live)
            {

                LiveChannel();


            }
            else {


                OfflineChannel();
            
            }



            if (this.channel.gameLive == null || this.channel.game == null)
            {

                this.panelGamePlaying.Visible = false;
            
            }


        }


        private void LiveChannel(){

            lblLiveStatus.Text = "LIVE";
            lblLiveStatus.ForeColor = Color.Green;
            
            lblGamePlaying.Text = this.channel.gameLive;

            lblStatus.Text = this.channel.status;

            lblLiveViewers.Text = this.channel.viewersLive.ToString("##,#");

            if (this.channel.viewersLive == 0) {

                lblLiveViewers.Text = "0";
            }
            

            if (this.channel.viewersLiveDifference > 0){

                lblLiveViewers.ForeColor = Color.Green;
            
            }
            else if (this.channel.viewersLiveDifference < 0)
            {

                lblLiveViewers.ForeColor = Color.Red;
            
            }



            if (this.channel.IsLiveNow)
            {

                lblLiveStatus.Text = "Now LIVE";

            }


        }


        private void OfflineChannel()
        {

            if(this.channel.IsOfflineNow){

                LiveChannel();

                lblLiveStatus.Text = "Now Offline";
                lblLiveStatus.ForeColor = Color.Red;

                lblGamePlaying.Text = this.channel.game;

                panelLiveViewers.Visible = false;

            }else{


                lblLiveStatus.Text = "Offline";
                lblLiveStatus.ForeColor = Color.Red;
                //lblLiveStatus.Font = new System.Drawing.Font(lblLiveStatus.Font, FontStyle.Regular);


                panelGamePlaying.Visible = false;

                lblStatus.Text = "\nLive " + TimeSinceWhenToNow(this.channel.updated_at);

                //lblStatus.Visible = false;


                panelLiveViewers.Visible = false;


            }

        }


        private string TimeSinceWhenToNow(DateTime when) {


            TimeSpan ts = DateTime.UtcNow.Subtract(when);

            string TimeSinceText = "";

            if (ts.TotalHours < 1)
            {

                string TimeType = (int)ts.TotalMinutes == 1 ? "minute" : "minutes";
                TimeSinceText = string.Format("{0} {1} ago", (int)ts.TotalMinutes, TimeType);

            }
            else if (ts.TotalDays < 1)
            {

                string TimeType1 = (int)ts.Hours == 1 ? "hour" : "hours";
                string TimeType2 = (int)ts.Minutes == 1 ? "minute" : "minutes";
                TimeSinceText = string.Format("{0} {1} and {2} {3} ago", (int)ts.Hours, TimeType1, (int)ts.Minutes, TimeType2);

            }
            else if (ts.TotalDays >= 1)
            {
                string TimeType = (int)ts.TotalDays == 1 ? "day" : "days";
                TimeSinceText = string.Format("{0} {1} ago", (int)ts.TotalDays, TimeType);

            }



            return TimeSinceText;

        }


        private void OpenChannelWebsite() {

            System.Diagnostics.Process.Start(this.channel.url);
        
        }



        private void linklblChannelDisplayName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenChannelWebsite();
        }

        private void pbChannelPicture_Click(object sender, EventArgs e)
        {
            OpenChannelWebsite();
        }





    }
}
