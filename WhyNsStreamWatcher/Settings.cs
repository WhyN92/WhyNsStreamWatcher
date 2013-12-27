using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Reflection;

namespace WhyNsStreamWatcher
{
    class Settings : Page
    {

        // Layout Controls

        private Panel panelSettings;
        private SplitContainer scSettings;
        private TreeView tvSettingsMenu;

        private Button btnApplySettings;

        private Label lblSaveStatus;

        // Settings Controls

        private List<Control> controlsGeneral;
        private CheckBox chbTrayOnClose, chbStartWithWindows, chbTrayOnLunch;

        private TextBox tbNotificationDisplayTime;
        private ComboBox cobNotificationScreenPosition; 
        

        private List<Control> controlsStreamClients;
        private CheckBox chbStreamAutoUpdate;
        private ComboBox cobStreamsAutoUpdateInterval;

        private CheckBox chbChannelNotificationIsLive;
        private CheckBox chbChannelNotificationIsLiveNow;
        private CheckBox chbChannelNotificationIsOfflineNow;


        private List<Control> controlsTwitch;
        private TextBox tbTwitchUsername;

        // Fonts

        Font fontPageTitle = new Font("Arial", 12, FontStyle.Bold);
        Font fontTitle = new Font("Arial", 12, FontStyle.Regular);
        Font fontUnderTitle = new Font("Arial", 10, FontStyle.Regular);

        public Settings(UI ui)
            : base(ui)
        {
            this.name = "Settings";
                        
            CreatePage();

        }

        public Panel GetPageControl()
        {

            return this.panelSettings;

        }


        private void CreatePage()
        {


            // Panel: Settings

            panelSettings = new Panel();
            panelSettings.Visible = false;
            panelSettings.Dock = DockStyle.Fill;
            panelSettings.BackColor = Color.White;

            // SplitContainer: Settings

            scSettings = new SplitContainer();
            scSettings.BackColor = Color.Black;
            scSettings.Dock = DockStyle.Fill;

            scSettings.SplitterWidth = 1;
            scSettings.SplitterDistance = 50;
            scSettings.BorderStyle = BorderStyle.None;
            scSettings.IsSplitterFixed = true;

            scSettings.Panel1.BackColor = Color.White;
            scSettings.Panel2.BackColor = Color.WhiteSmoke;


            // FlowLayoutPanel : MenuPanel

            FlowLayoutPanel flpMenuPanel = new FlowLayoutPanel();
            flpMenuPanel.FlowDirection = FlowDirection.TopDown;
            flpMenuPanel.Dock = DockStyle.Fill;
            flpMenuPanel.Padding = new Padding(5, 20, 5, 0);

            // Page Title

            Label lblPageTitle = new Label();
            lblPageTitle.AutoSize = true;
            lblPageTitle.Text = this.name;
            lblPageTitle.Margin = new Padding(0, 0, 0, 15);
            lblPageTitle.Font = fontPageTitle;
            lblPageTitle.Anchor = AnchorStyles.None;

            // TreeView: SettingsMenu

            tvSettingsMenu = new TreeView();
            tvSettingsMenu.Anchor = AnchorStyles.None;
            tvSettingsMenu.Height = 150;
            tvSettingsMenu.AfterSelect += new TreeViewEventHandler(tvSettingsMenu_AfterSelect);
            tvSettingsMenu.BorderStyle = BorderStyle.None;
            tvSettingsMenu.HotTracking = true;
            tvSettingsMenu.ItemHeight = 20;

            tvSettingsMenu.ShowLines = false;

            tvSettingsMenu.GotFocus += tvSettingsMenu_GotFocus;

            TreeNode tn;

            tn = new TreeNode("General");
            tvSettingsMenu.Nodes.Add(tn);
            TreeNode tn1 = new TreeNode("Twitch.tv");
            TreeNode tn2 = new TreeNode("Ustream.tv");
            TreeNode[] tnArray = new TreeNode[] { tn1, tn2 };

            tn = new TreeNode("Stream Clients", tnArray);
            tvSettingsMenu.Nodes.Add(tn);

            tn = new TreeNode("Layout");
            tvSettingsMenu.Nodes.Add(tn);

            // Label : SaveStatus

            lblSaveStatus = new Label();
            lblSaveStatus.Anchor = AnchorStyles.None;
            lblSaveStatus.AutoSize = true;
            lblSaveStatus.Text = "";
            lblSaveStatus.ForeColor = Color.Green;


            // Button : ApplySettings

            btnApplySettings = new Button();
            btnApplySettings.Text = "Apply Settings";
            btnApplySettings.Anchor = AnchorStyles.None;
            btnApplySettings.Margin = new Padding(0, 20, 0, 20);
            btnApplySettings.Click += new EventHandler(btnApplySettings_Click);


            // Button : CloseSettings

            Button btnCloseSettingsPanel = new Button();
            btnCloseSettingsPanel.Text = "Close";
            btnCloseSettingsPanel.Anchor = AnchorStyles.None;
            btnCloseSettingsPanel.Click += new EventHandler(delegate(Object o, EventArgs a)
            {

                this.ui.ClosePage(this);

            });




            // Adding Controls

            flpMenuPanel.Controls.Add(lblPageTitle);
            flpMenuPanel.Controls.Add(tvSettingsMenu);
            flpMenuPanel.Controls.Add(lblSaveStatus);
            flpMenuPanel.Controls.Add(btnApplySettings);
            flpMenuPanel.Controls.Add(btnCloseSettingsPanel);


            scSettings.Panel1.Controls.Add(flpMenuPanel);

            panelSettings.Controls.Add(scSettings);

        }

        private void tvSettingsMenu_GotFocus(object sender, EventArgs e)
        {
            scSettings.Panel2.Focus();
        }


        private void btnApplySettings_Click(object sender, EventArgs e)
        {

            // General
            Properties.Settings.Default.TrayOnClose = chbTrayOnClose.Checked;
            Properties.Settings.Default.StartWithWindows = chbStartWithWindows.Checked;
            StartWithWindowsToggle();

            Properties.Settings.Default.TrayOnLaunch = chbTrayOnLunch.Checked;

            try
            {
                Properties.Settings.Default.NotificationDisplayTime = (Convert.ToInt16(tbNotificationDisplayTime.Text) * 1000);
            }
            catch {

                MessageBox.Show("Only Numbers in \"General-> Notifications-> Display time in seconds\""); 
            
            }

            Properties.Settings.Default.NotificationScreenPosition = (NotificationScreenPosition)((myComboBoxItem)cobNotificationScreenPosition.SelectedItem).Value;

            // Stream Clients
            Properties.Settings.Default.StreamsAutoUpdate = chbStreamAutoUpdate.Checked;
            Properties.Settings.Default.StreamsAutoUpdateInterval = (int)((myComboBoxItem)cobStreamsAutoUpdateInterval.SelectedItem).Value;

            Properties.Settings.Default.ChannelNotificationIsLive = chbChannelNotificationIsLive.Checked;
            Properties.Settings.Default.ChannelNotificationIsLiveNow = chbChannelNotificationIsLiveNow.Checked;
            Properties.Settings.Default.ChannelNotificationIsOfflineNow = chbChannelNotificationIsOfflineNow.Checked;


            // Twitch.tv
            Properties.Settings.Default.TwitchUsername = tbTwitchUsername.Text;

            // Ustream.tv

            // Layout


            // Save The User Settings
            Properties.Settings.Default.Save();
            lblSaveStatus.Text = "Changes Saved";

        }




        private void CreatePageSettingControls()
        {
            Label lblTitle;


            // General

            controlsGeneral = new List<Control>();

            lblTitle = new Label();
            lblTitle.AutoSize = true;
            lblTitle.Text = "General";
            lblTitle.Margin = new Padding(0, 0, 0, 15);
            lblTitle.Font = fontTitle;
            controlsGeneral.Add(lblTitle);

                chbTrayOnClose = new CheckBox();
                chbTrayOnClose.AutoSize = true;
                chbTrayOnClose.Text = "Minimize Application to System Tray on close";
                chbTrayOnClose.Checked = Properties.Settings.Default.TrayOnClose;

                chbStartWithWindows = new CheckBox();
                chbStartWithWindows.AutoSize = true;
                chbStartWithWindows.Text = "Launch Application with Windows";
                chbStartWithWindows.Checked = Properties.Settings.Default.StartWithWindows;

                chbTrayOnLunch = new CheckBox();
                chbTrayOnLunch.AutoSize = true;
                chbTrayOnLunch.Text = "Minimize Application to System Tray on Launch";
                chbTrayOnLunch.Checked = Properties.Settings.Default.TrayOnLaunch;


            controlsGeneral.Add(chbTrayOnClose);
            controlsGeneral.Add(chbStartWithWindows);
            controlsGeneral.Add(chbTrayOnLunch); // Need to make it only work at Start with Windows Lunch


                Label lblGeneralNotificationsUnderTitle = new Label();
                lblGeneralNotificationsUnderTitle.AutoSize = true;
                lblGeneralNotificationsUnderTitle.Text = "Notifications";
                lblGeneralNotificationsUnderTitle.Margin = new Padding(0, 25, 0, 15);
                lblGeneralNotificationsUnderTitle.Font = fontUnderTitle;


                    FlowLayoutPanel flpNotificationDisplayTime = new FlowLayoutPanel();
                    flpNotificationDisplayTime.FlowDirection = FlowDirection.LeftToRight;
                    flpNotificationDisplayTime.AutoSize = true;
                    flpNotificationDisplayTime.AutoSizeMode = AutoSizeMode.GrowAndShrink;


                        Label lblNotificationDisplayTime = new Label();
                        lblNotificationDisplayTime.Text = "Display time in seconds:";
                        lblNotificationDisplayTime.AutoSize = true;
                        lblNotificationDisplayTime.Anchor = AnchorStyles.None;
                        lblNotificationDisplayTime.Margin = new Padding(0);

                        tbNotificationDisplayTime = new TextBox();
                        tbNotificationDisplayTime.Text = (Properties.Settings.Default.NotificationDisplayTime / 1000).ToString();
                        tbNotificationDisplayTime.Anchor = AnchorStyles.None;
                        tbNotificationDisplayTime.Width = 50;
                        tbNotificationDisplayTime.Margin = new Padding(0);

                    flpNotificationDisplayTime.Controls.Add(lblNotificationDisplayTime);
                    flpNotificationDisplayTime.Controls.Add(tbNotificationDisplayTime);


                    FlowLayoutPanel flpNotificationScreenPosition = new FlowLayoutPanel();
                    flpNotificationScreenPosition.FlowDirection = FlowDirection.LeftToRight;
                    flpNotificationScreenPosition.AutoSize = true;
                    flpNotificationScreenPosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;


                        Label lblNotificationScreenPosition = new Label();
                        lblNotificationScreenPosition.AutoSize = true;
                        lblNotificationScreenPosition.Text = "Screen corner position";
                        lblNotificationScreenPosition.Anchor = AnchorStyles.None;
                        lblNotificationScreenPosition.Margin = new Padding(0);


                        cobNotificationScreenPosition = new ComboBox();
                        cobNotificationScreenPosition.Anchor = AnchorStyles.None;
                        cobNotificationScreenPosition.Margin = new Padding(0);

                        cobNotificationScreenPosition.Items.Add(new myComboBoxItem("Top-Left", NotificationScreenPosition.TopLeft));
                        cobNotificationScreenPosition.Items.Add(new myComboBoxItem("Top-Right", NotificationScreenPosition.TopRight));
                        cobNotificationScreenPosition.Items.Add(new myComboBoxItem("Bottom-Left", NotificationScreenPosition.BottomLeft));
                        cobNotificationScreenPosition.Items.Add(new myComboBoxItem("Bottom-Right", NotificationScreenPosition.BottomRight));

                        foreach (myComboBoxItem item in cobNotificationScreenPosition.Items)
                        {
                            if ((NotificationScreenPosition)item.Value == Properties.Settings.Default.NotificationScreenPosition)
                            {
                                cobNotificationScreenPosition.SelectedItem = item;
                            }
                        }

                    flpNotificationScreenPosition.Controls.Add(lblNotificationScreenPosition);
                    flpNotificationScreenPosition.Controls.Add(cobNotificationScreenPosition);


                controlsGeneral.Add(lblGeneralNotificationsUnderTitle);
                controlsGeneral.Add(flpNotificationDisplayTime);
                controlsGeneral.Add(flpNotificationScreenPosition);

            // Stream Clients

            controlsStreamClients = new List<Control>();

            lblTitle = new Label();
            lblTitle.AutoSize = true;
            lblTitle.Text = "Stream Clients";
            lblTitle.Margin = new Padding(0, 0, 0, 15);
            lblTitle.Font = fontTitle;
            controlsStreamClients.Add(lblTitle);

                chbStreamAutoUpdate = new CheckBox();
                chbStreamAutoUpdate.AutoSize = true;
                chbStreamAutoUpdate.Text = "Auto Refresh Streams";
                chbStreamAutoUpdate.Checked = Properties.Settings.Default.StreamsAutoUpdate;

                FlowLayoutPanel flpStreamsAutoUpdateInterval = new FlowLayoutPanel();
                flpStreamsAutoUpdateInterval.FlowDirection = FlowDirection.LeftToRight;
                flpStreamsAutoUpdateInterval.AutoSize = true;
                flpStreamsAutoUpdateInterval.AutoSizeMode = AutoSizeMode.GrowAndShrink;


                    Label lblStreamsAutoUpdateInterval = new Label();
                    lblStreamsAutoUpdateInterval.AutoSize = true;
                    lblStreamsAutoUpdateInterval.Text = "Refresh Rate";
                    lblStreamsAutoUpdateInterval.Anchor = AnchorStyles.None;
                    lblStreamsAutoUpdateInterval.Margin = new Padding(0);


                    cobStreamsAutoUpdateInterval = new ComboBox();
                    cobStreamsAutoUpdateInterval.Anchor = AnchorStyles.None;
                    cobStreamsAutoUpdateInterval.Margin = new Padding(0);
                    
                    // Only for testing!
                    if(!ApplicationDeployment.IsNetworkDeployed) { 
                        cobStreamsAutoUpdateInterval.Items.Add(new myComboBoxItem("10 sec", 10000));
                    }

                    cobStreamsAutoUpdateInterval.Items.Add(new myComboBoxItem("1 minute", 60000));
                    cobStreamsAutoUpdateInterval.Items.Add(new myComboBoxItem("5 minutes", 300000));
                    cobStreamsAutoUpdateInterval.Items.Add(new myComboBoxItem("10 minutes", 600000));
                    cobStreamsAutoUpdateInterval.Items.Add(new myComboBoxItem("15 minutes", 900000));

                    foreach (myComboBoxItem item in cobStreamsAutoUpdateInterval.Items) {
                        if ((int)item.Value == Properties.Settings.Default.StreamsAutoUpdateInterval) {
                            cobStreamsAutoUpdateInterval.SelectedItem = item;
                        }
                    }

                flpStreamsAutoUpdateInterval.Controls.Add(lblStreamsAutoUpdateInterval);
                flpStreamsAutoUpdateInterval.Controls.Add(cobStreamsAutoUpdateInterval);
            

                        // Stream Clients : Notifications

                        Label lblNotificationsUnderTitle = new Label();
                        lblNotificationsUnderTitle.AutoSize = true;
                        lblNotificationsUnderTitle.Text = "Notifications";
                        lblNotificationsUnderTitle.Margin = new Padding(0, 25, 0, 15);
                        lblNotificationsUnderTitle.Font = fontUnderTitle;


                        Label lblNotificationsText = new Label();
                        lblNotificationsText.AutoSize = true;
                        lblNotificationsText.Text = "Notify about:";
                        lblNotificationsText.Margin = new Padding(0, 0, 0, 10);


                        chbChannelNotificationIsLive = new CheckBox();
                        chbChannelNotificationIsLive.AutoSize = true;
                        chbChannelNotificationIsLive.Text = "Live channels at application launch";
                        chbChannelNotificationIsLive.Checked = Properties.Settings.Default.ChannelNotificationIsLive;
                        chbChannelNotificationIsLive.Margin = new Padding(10, 0, 0, 0);

                        chbChannelNotificationIsLiveNow = new CheckBox();
                        chbChannelNotificationIsLiveNow.AutoSize = true;
                        chbChannelNotificationIsLiveNow.Text = "Channels go Live";
                        chbChannelNotificationIsLiveNow.Checked = Properties.Settings.Default.ChannelNotificationIsLiveNow;
                        chbChannelNotificationIsLiveNow.Margin = new Padding(10, 0, 0, 0);

                        chbChannelNotificationIsOfflineNow = new CheckBox();
                        chbChannelNotificationIsOfflineNow.AutoSize = true;
                        chbChannelNotificationIsOfflineNow.Text = "Channels go Offline";
                        chbChannelNotificationIsOfflineNow.Checked = Properties.Settings.Default.ChannelNotificationIsOfflineNow;
                        chbChannelNotificationIsOfflineNow.Margin = new Padding(10, 0, 0, 0);

                        controlsStreamClients.Add(chbStreamAutoUpdate);
                        controlsStreamClients.Add(flpStreamsAutoUpdateInterval);
                        controlsStreamClients.Add(lblNotificationsUnderTitle);
                        controlsStreamClients.Add(lblNotificationsText);
                        controlsStreamClients.Add(chbChannelNotificationIsLive);
                        controlsStreamClients.Add(chbChannelNotificationIsLiveNow);
                        controlsStreamClients.Add(chbChannelNotificationIsOfflineNow);



                // Twitch.tv
                
                controlsTwitch = new List<Control>();

                lblTitle = new Label();
                lblTitle.AutoSize = true;
                lblTitle.Text = "Twitch.tv";
                lblTitle.Margin = new Padding(0, 0, 0, 15);
                lblTitle.Font = fontTitle;
                controlsTwitch.Add(lblTitle);    


                    FlowLayoutPanel flpUsername = new FlowLayoutPanel();
                    flpUsername.FlowDirection = FlowDirection.LeftToRight;
                    flpUsername.AutoSize = true;
                    flpUsername.AutoSizeMode = AutoSizeMode.GrowAndShrink;


                    Label lblTwitchUsernameText = new Label();
                    lblTwitchUsernameText.Text = "Your Twitch.tv Username:";
                    lblTwitchUsernameText.AutoSize = true;
                    lblTwitchUsernameText.Anchor = AnchorStyles.None;
                    lblTwitchUsernameText.Margin = new Padding(0);

                    tbTwitchUsername = new TextBox();
                    tbTwitchUsername.Text = Properties.Settings.Default.TwitchUsername;
                    tbTwitchUsername.Anchor = AnchorStyles.None;
                    tbTwitchUsername.Margin = new Padding(0);

                    flpUsername.Controls.Add(lblTwitchUsernameText);
                    flpUsername.Controls.Add(tbTwitchUsername);

                controlsTwitch.Add(flpUsername);


                // Ustream.tv
                


            // Layout 



        }




        private void tvSettingsMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lblSaveStatus.Text = "";
            scSettings.Panel2.Controls.Clear();
            scSettings.Panel2.Focus();

            foreach (TreeNode n in tvSettingsMenu.Nodes)
            {

                n.NodeFont = new Font(tvSettingsMenu.Font, FontStyle.Regular);
                if (n.Nodes.Count != 0)
                {

                    foreach (TreeNode sn in n.Nodes)
                    {
                        sn.NodeFont = new Font(tvSettingsMenu.Font, FontStyle.Regular);

                    }
                }

            }

            TreeNode node = tvSettingsMenu.SelectedNode;

            node.NodeFont = new Font(tvSettingsMenu.Font, FontStyle.Bold);
            tvSettingsMenu.BackColor = tvSettingsMenu.BackColor;


            FlowLayoutPanel flpSettings = new FlowLayoutPanel();
            flpSettings.FlowDirection = FlowDirection.TopDown;
            flpSettings.Dock = DockStyle.Fill;
            flpSettings.Padding = new Padding(20, 20, 20, 0);
            flpSettings.WrapContents = false;



            switch (node.Text)
            {
                case "General":

                    flpSettings.Controls.AddRange(controlsGeneral.ToArray());


                    break;

                case "Stream Clients":

                    flpSettings.Controls.AddRange(controlsStreamClients.ToArray());


                    break;

                case "Twitch.tv":

                    flpSettings.Controls.AddRange(controlsTwitch.ToArray());


                    break;
                default:

                    Label lblNoSettings = new Label();
                    lblNoSettings.Font = new Font(lblNoSettings.Font, FontStyle.Bold);
                    lblNoSettings.Text = "No Settings Available";
                    lblNoSettings.AutoSize = true;

                    flpSettings.Controls.Add(lblNoSettings);

                    break;

            }


            scSettings.Panel2.Controls.Add(flpSettings);


        }


        private void StartWithWindowsToggle()
        {

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
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

                string startupShortcutPath = string.Empty;
                startupShortcutPath = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "\\", description, ".appref-ms");

                string startmenuShotcutPath = string.Empty;
                startmenuShotcutPath = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "\\", company, "\\", description, ".appref-ms");

                if (company != string.Empty && description != string.Empty)
                {

                    if (Properties.Settings.Default.StartWithWindows)
                    {

                        System.IO.File.Copy(startmenuShotcutPath, startupShortcutPath, true);

                    }
                    else
                    {

                        System.IO.File.Delete(startupShortcutPath);


                    }


                }
            }
            else
            {
                if (Properties.Settings.Default.StartWithWindows)
                {
                    MessageBox.Show("To Launch Application with Windows, use ClickOnce Shortcut to open this Application! (Look in Start Menu)");
                    Properties.Settings.Default.StartWithWindows = false;
                    chbStartWithWindows.Checked = false;
                }
            }

        }
       

        override public void ShowPage()
        {

            CreatePageSettingControls();

            this.tvSettingsMenu.SelectedNode = this.tvSettingsMenu.Nodes[1];
            this.tvSettingsMenu.SelectedNode = this.tvSettingsMenu.Nodes[0];

            this.panelSettings.Visible = true;
            this.panelSettings.BringToFront();


        }

        override public void ClosePage()
        {
            this.panelSettings.Visible = false;

        }

    }
}
