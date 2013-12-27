using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyNsStreamWatcher
{
    class ChannelLayoutPanel : Panel
    {



        public ChannelLayoutPanel(Channel channel) {


            // 

            Panel panelStreamText = new Panel();

            this.BorderStyle = BorderStyle.FixedSingle;


            this.Width = (410 - 25); // Shound not be a Hardcoded witdh

            this.Padding = (new Padding(0));
            //tlpStream.Margin = (new Padding(0));

            PictureBox pbStreamLogo = new PictureBox();

            string streamLogoUrl = channel.logo;
            pbStreamLogo.Size = new Size(100, 100);
            pbStreamLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbStreamLogo.Load(streamLogoUrl);
            pbStreamLogo.Margin = (new Padding(0, 0, 0, 2));

            pbStreamLogo.Cursor = Cursors.Hand;

            pbStreamLogo.Click += new EventHandler((sender, e) => openURL(sender, e, channel.url));

            this.Controls.Add(pbStreamLogo);

            panelStreamText.Location = new Point(pbStreamLogo.Width + 10, 10);
            panelStreamText.Width = this.Width - pbStreamLogo.Width;

            LinkLabel lblDisplayname = new LinkLabel();
            lblDisplayname.Name = "lblDisplayname";
            lblDisplayname.Text = channel.display_name;
            lblDisplayname.Width = 10;
            lblDisplayname.AutoSize = true;
            lblDisplayname.Font = new Font(lblDisplayname.Font, FontStyle.Bold);
            lblDisplayname.LinkClicked += new LinkLabelLinkClickedEventHandler((sender, e) => openURL(sender, e, channel.url));


            formMain.Logger(">>> Channel Displayname: " + lblDisplayname.Text);



            Label lblLiveStatus = new Label();
            lblLiveStatus.Name = "lblLiveStatus";
            lblLiveStatus.Left = lblDisplayname.PreferredWidth + 5;
            lblLiveStatus.Top = 0;

            Label lblStatus = new Label();
            lblStatus.Name = "lblStatus";
            lblStatus.Left = 0;
            lblStatus.Top = 46;


            lblStatus.MaximumSize = new Size(this.Width - pbStreamLogo.Width - 20, 0);

            lblStatus.AutoSize = true;

            //lblStatus.Font = new Font(lblStatus.Font, FontStyle.Italic);

            if (channel.live)
            {

                lblLiveStatus.Text = "Live";
                lblLiveStatus.ForeColor = Color.Green;
                lblLiveStatus.Font = new Font(lblDisplayname.Font, FontStyle.Bold);



                Label lblLiveViewers = new Label();
                lblLiveViewers.Name = "lblLiveViewers";
                lblLiveViewers.Width = 0;
                lblLiveViewers.AutoSize = true;
                lblLiveViewers.Text = channel.viewersLive + " Viewers";
                lblLiveViewers.Location = new Point(panelStreamText.Width - lblLiveViewers.PreferredWidth - 20, 0);
                panelStreamText.Controls.Add(lblLiveViewers);

                Label lblPlaying = new Label();
                lblPlaying.Name = "lblLiveViewers";
                lblPlaying.Left = 0;
                lblPlaying.Top = 23;
                lblPlaying.AutoSize = true;
                lblPlaying.Text = "Playing: " + channel.gameLive;
                panelStreamText.Controls.Add(lblPlaying);

                lblStatus.Text = channel.status;

            }
            else
            {

                lblLiveStatus.Text = "Offline";
                lblLiveStatus.ForeColor = Color.Red;

                //lblStatus.Text = "<lblStatus:Offline>";

            }

            formMain.Logger(">>> LiveStatus: " + lblLiveStatus.Text);
            formMain.Logger(">>> Status: " + lblStatus.Text);

            panelStreamText.Controls.Add(lblDisplayname);
            panelStreamText.Controls.Add(lblLiveStatus);

            panelStreamText.Controls.Add(lblStatus);

            this.Controls.Add(panelStreamText);

        }

        private void openURL(object sender, EventArgs e, string url)
        {
            System.Diagnostics.Process.Start(url);
        }



    }
}
