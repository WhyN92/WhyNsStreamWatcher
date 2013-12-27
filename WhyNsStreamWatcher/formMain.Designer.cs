namespace WhyNsStreamWatcher
{
    partial class formMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMain = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowOfflineStreams = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTopbarMainForm = new System.Windows.Forms.Panel();
            this.btnMinimizeMainForm = new System.Windows.Forms.Button();
            this.btnCloseMainForm = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panelTopbarMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "notifyIcon1";
            this.notifyIconMain.Visible = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMain,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(204, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiMain
            // 
            this.tsmiMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSettings,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.tsmiMain.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsmiMain.Image = global::WhyNsStreamWatcher.Properties.Resources.WhyNs_Stream_Watcher_Icon1;
            this.tsmiMain.Name = "tsmiMain";
            this.tsmiMain.Size = new System.Drawing.Size(152, 20);
            this.tsmiMain.Text = "hyNs: Stream Watcher";
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(116, 22);
            this.tsmiSettings.Text = "Settings";
            this.tsmiSettings.Click += new System.EventHandler(this.tsmiSettings_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.appExit);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowOfflineStreams});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // tsmiShowOfflineStreams
            // 
            this.tsmiShowOfflineStreams.Name = "tsmiShowOfflineStreams";
            this.tsmiShowOfflineStreams.Size = new System.Drawing.Size(184, 22);
            this.tsmiShowOfflineStreams.Text = "Show offline streams";
            this.tsmiShowOfflineStreams.Click += new System.EventHandler(this.tsmiShowOfflineStreams_Click);
            // 
            // panelTopbarMainForm
            // 
            this.panelTopbarMainForm.BackColor = System.Drawing.Color.Silver;
            this.panelTopbarMainForm.Controls.Add(this.btnMinimizeMainForm);
            this.panelTopbarMainForm.Controls.Add(this.btnCloseMainForm);
            this.panelTopbarMainForm.Controls.Add(this.menuStrip1);
            this.panelTopbarMainForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopbarMainForm.Location = new System.Drawing.Point(0, 0);
            this.panelTopbarMainForm.Name = "panelTopbarMainForm";
            this.panelTopbarMainForm.Size = new System.Drawing.Size(440, 25);
            this.panelTopbarMainForm.TabIndex = 8;
            // 
            // btnMinimizeMainForm
            // 
            this.btnMinimizeMainForm.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizeMainForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMinimizeMainForm.FlatAppearance.BorderSize = 0;
            this.btnMinimizeMainForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMinimizeMainForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnMinimizeMainForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizeMainForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Strikeout))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimizeMainForm.Location = new System.Drawing.Point(396, 2);
            this.btnMinimizeMainForm.Name = "btnMinimizeMainForm";
            this.btnMinimizeMainForm.Size = new System.Drawing.Size(21, 21);
            this.btnMinimizeMainForm.TabIndex = 10;
            this.btnMinimizeMainForm.Text = " ";
            this.btnMinimizeMainForm.UseVisualStyleBackColor = false;
            this.btnMinimizeMainForm.Click += new System.EventHandler(this.btnMinimizeMainForm_Click);
            // 
            // btnCloseMainForm
            // 
            this.btnCloseMainForm.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseMainForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCloseMainForm.FlatAppearance.BorderSize = 0;
            this.btnCloseMainForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCloseMainForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCloseMainForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseMainForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseMainForm.Location = new System.Drawing.Point(416, 2);
            this.btnCloseMainForm.Name = "btnCloseMainForm";
            this.btnCloseMainForm.Size = new System.Drawing.Size(21, 21);
            this.btnCloseMainForm.TabIndex = 9;
            this.btnCloseMainForm.Text = "X";
            this.btnCloseMainForm.UseVisualStyleBackColor = false;
            this.btnCloseMainForm.Click += new System.EventHandler(this.btnCloseMainForm_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(440, 622);
            this.ControlBox = false;
            this.Controls.Add(this.panelTopbarMainForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "formMain";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelTopbarMainForm.ResumeLayout(false);
            this.panelTopbarMainForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panelTopbarMainForm;
        private System.Windows.Forms.ToolStripMenuItem tsmiMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowOfflineStreams;
        private System.Windows.Forms.Button btnCloseMainForm;
        private System.Windows.Forms.Button btnMinimizeMainForm;
    }
}

