
namespace WinXPApp
{
    partial class ShellForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellForm));
            this.mainTabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.homeView = new WinXPApp.Views.HomeView();
            this.ToolsTabPage = new MetroFramework.Controls.MetroTabPage();
            this.toolsNavigatorView = new WinXPApp.Views.SampleTools.ToolsNavigatorView();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.logsView = new WinXPApp.Views.LogsView();
            this.settingsLink = new MetroFramework.Controls.MetroLink();
            this.helpLink = new MetroFramework.Controls.MetroLink();
            this.panelSettingsHelp = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainTabControl.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.ToolsTabPage.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.panelSettingsHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.metroTabPage1);
            this.mainTabControl.Controls.Add(this.ToolsTabPage);
            this.mainTabControl.Controls.Add(this.metroTabPage3);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.mainTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.mainTabControl.ItemSize = new System.Drawing.Size(111, 31);
            this.mainTabControl.Location = new System.Drawing.Point(20, 60);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 1;
            this.mainTabControl.Size = new System.Drawing.Size(897, 527);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.homeView);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.metroTabPage1.Size = new System.Drawing.Size(889, 488);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "welcome";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // introductionUC1
            // 
            this.homeView.BackColor = System.Drawing.Color.Transparent;
            this.homeView.Location = new System.Drawing.Point(7, 25);
            this.homeView.Name = "introductionUC1";
            this.homeView.Size = new System.Drawing.Size(876, 457);
            this.homeView.TabIndex = 2;
            // 
            // ToolsTabPage
            // 
            this.ToolsTabPage.Controls.Add(this.toolsNavigatorView);
            this.ToolsTabPage.HorizontalScrollbarBarColor = true;
            this.ToolsTabPage.HorizontalScrollbarHighlightOnWheel = false;
            this.ToolsTabPage.HorizontalScrollbarSize = 10;
            this.ToolsTabPage.Location = new System.Drawing.Point(4, 35);
            this.ToolsTabPage.Name = "ToolsTabPage";
            this.ToolsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ToolsTabPage.Size = new System.Drawing.Size(889, 488);
            this.ToolsTabPage.TabIndex = 1;
            this.ToolsTabPage.Text = "tools";
            this.ToolsTabPage.VerticalScrollbarBarColor = true;
            this.ToolsTabPage.VerticalScrollbarHighlightOnWheel = false;
            this.ToolsTabPage.VerticalScrollbarSize = 10;
            // 
            // toolsNavigatorUC1
            // 
            this.toolsNavigatorView.BackColor = System.Drawing.Color.Transparent;
            this.toolsNavigatorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolsNavigatorView.Location = new System.Drawing.Point(3, 3);
            this.toolsNavigatorView.Name = "toolsNavigatorUC1";
            this.toolsNavigatorView.Size = new System.Drawing.Size(883, 482);
            this.toolsNavigatorView.TabIndex = 2;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.logsView);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.metroTabPage3.Size = new System.Drawing.Size(889, 488);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "log";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // logUC1
            // 
            this.logsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logsView.BackColor = System.Drawing.Color.Transparent;
            this.logsView.Location = new System.Drawing.Point(22, 10);
            this.logsView.Name = "logUC1";
            this.logsView.Size = new System.Drawing.Size(848, 479);
            this.logsView.TabIndex = 2;
            // 
            // settingsLink
            // 
            this.settingsLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsLink.Location = new System.Drawing.Point(11, 0);
            this.settingsLink.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.settingsLink.Name = "settingsLink";
            this.settingsLink.Size = new System.Drawing.Size(77, 23);
            this.settingsLink.TabIndex = 1;
            this.settingsLink.Text = "SETTINGS";
            this.settingsLink.UseSelectable = true;
            this.settingsLink.Click += new System.EventHandler(this.SettingsLink_Click);
            // 
            // helpLink
            // 
            this.helpLink.Location = new System.Drawing.Point(89, 0);
            this.helpLink.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(75, 23);
            this.helpLink.TabIndex = 2;
            this.helpLink.Text = "HELP";
            this.helpLink.UseSelectable = true;
            this.helpLink.Click += new System.EventHandler(this.HelpLink_Click);
            // 
            // panelSettingsHelp
            // 
            this.panelSettingsHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSettingsHelp.Controls.Add(this.panel1);
            this.panelSettingsHelp.Controls.Add(this.helpLink);
            this.panelSettingsHelp.Controls.Add(this.settingsLink);
            this.panelSettingsHelp.Location = new System.Drawing.Point(690, 6);
            this.panelSettingsHelp.Name = "panelSettingsHelp";
            this.panelSettingsHelp.Size = new System.Drawing.Size(167, 30);
            this.panelSettingsHelp.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(94, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 15);
            this.panel1.TabIndex = 4;
            // 
            // ShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 607);
            this.Controls.Add(this.panelSettingsHelp);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(425, 260);
            this.Name = "ShellForm";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Form1";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.mainTabControl.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.ToolsTabPage.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.panelSettingsHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl mainTabControl;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage ToolsTabPage;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroLink helpLink;
        private MetroFramework.Controls.MetroLink settingsLink;
        private System.Windows.Forms.Panel panelSettingsHelp;
        private System.Windows.Forms.Panel panel1;
        private Views.HomeView homeView;
        private Views.LogsView logsView;
        private Views.SampleTools.ToolsNavigatorView toolsNavigatorView;
    }
}

