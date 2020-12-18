
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
            WinXPCore.SampleTools.LineSorter lineSorter1 = new WinXPCore.SampleTools.LineSorter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellForm));
            this.mainTabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.introductionUC1 = new WinXPApp.Forms.IntroductionUC();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.toolsTabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.uuidGeneratorUC1 = new WinXPApp.Forms.UUIDGeneratorUC();
            this.metroTabPage5 = new MetroFramework.Controls.MetroTabPage();
            this.flatUIColorPickerUC1 = new WinXPApp.Forms.SampleTools.FlatUIColorPickerUC();
            this.metroTabPage6 = new MetroFramework.Controls.MetroTabPage();
            this.lineSorterUC1 = new WinXPApp.Forms.SampleTools.LineSorterUC();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.logUC1 = new WinXPApp.Forms.LogUC();
            this.settingsLink = new MetroFramework.Controls.MetroLink();
            this.helpLink = new MetroFramework.Controls.MetroLink();
            this.panelSettingsHelp = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainTabControl.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.toolsTabControl.SuspendLayout();
            this.metroTabPage4.SuspendLayout();
            this.metroTabPage5.SuspendLayout();
            this.metroTabPage6.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.panelSettingsHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.metroTabPage1);
            this.mainTabControl.Controls.Add(this.metroTabPage2);
            this.mainTabControl.Controls.Add(this.metroTabPage3);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.mainTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.mainTabControl.ItemSize = new System.Drawing.Size(111, 31);
            this.mainTabControl.Location = new System.Drawing.Point(20, 60);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 1;
            this.mainTabControl.Size = new System.Drawing.Size(810, 470);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.introductionUC1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.metroTabPage1.Size = new System.Drawing.Size(802, 431);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "welcome";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // introductionUC1
            // 
            this.introductionUC1.BackColor = System.Drawing.Color.Transparent;
            this.introductionUC1.Location = new System.Drawing.Point(7, 25);
            this.introductionUC1.Name = "introductionUC1";
            this.introductionUC1.Size = new System.Drawing.Size(722, 393);
            this.introductionUC1.TabIndex = 2;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.toolsTabControl);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.metroTabPage2.Size = new System.Drawing.Size(802, 431);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "tools";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // toolsTabControl
            // 
            this.toolsTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.toolsTabControl.Controls.Add(this.metroTabPage4);
            this.toolsTabControl.Controls.Add(this.metroTabPage5);
            this.toolsTabControl.Controls.Add(this.metroTabPage6);
            this.toolsTabControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.toolsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolsTabControl.FontSize = MetroFramework.MetroTabControlSize.Small;
            this.toolsTabControl.Location = new System.Drawing.Point(3, 3);
            this.toolsTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.toolsTabControl.Name = "toolsTabControl";
            this.toolsTabControl.SelectedIndex = 0;
            this.toolsTabControl.Size = new System.Drawing.Size(796, 425);
            this.toolsTabControl.TabIndex = 2;
            this.toolsTabControl.UseSelectable = true;
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.Controls.Add(this.uuidGeneratorUC1);
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.HorizontalScrollbarSize = 10;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 37);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(788, 384);
            this.metroTabPage4.TabIndex = 3;
            this.metroTabPage4.Text = "UUID GENERATOR";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            this.metroTabPage4.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.VerticalScrollbarSize = 10;
            // 
            // uuidGeneratorUC1
            // 
            this.uuidGeneratorUC1.BackColor = System.Drawing.Color.Transparent;
            this.uuidGeneratorUC1.Location = new System.Drawing.Point(30, 25);
            this.uuidGeneratorUC1.Name = "uuidGeneratorUC1";
            this.uuidGeneratorUC1.Size = new System.Drawing.Size(599, 304);
            this.uuidGeneratorUC1.TabIndex = 2;
            // 
            // metroTabPage5
            // 
            this.metroTabPage5.Controls.Add(this.flatUIColorPickerUC1);
            this.metroTabPage5.HorizontalScrollbarBarColor = true;
            this.metroTabPage5.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage5.HorizontalScrollbarSize = 10;
            this.metroTabPage5.Location = new System.Drawing.Point(4, 37);
            this.metroTabPage5.Name = "metroTabPage5";
            this.metroTabPage5.Size = new System.Drawing.Size(788, 384);
            this.metroTabPage5.TabIndex = 4;
            this.metroTabPage5.Text = "FLAT UI COLOR PICKER";
            this.metroTabPage5.VerticalScrollbarBarColor = true;
            this.metroTabPage5.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage5.VerticalScrollbarSize = 10;
            // 
            // flatUIColorPickerUC1
            // 
            this.flatUIColorPickerUC1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flatUIColorPickerUC1.AutoSize = true;
            this.flatUIColorPickerUC1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flatUIColorPickerUC1.BackColor = System.Drawing.Color.Transparent;
            this.flatUIColorPickerUC1.Location = new System.Drawing.Point(30, 25);
            this.flatUIColorPickerUC1.Name = "flatUIColorPickerUC1";
            this.flatUIColorPickerUC1.Size = new System.Drawing.Size(421, 314);
            this.flatUIColorPickerUC1.TabIndex = 2;
            // 
            // metroTabPage6
            // 
            this.metroTabPage6.Controls.Add(this.lineSorterUC1);
            this.metroTabPage6.HorizontalScrollbarBarColor = true;
            this.metroTabPage6.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage6.HorizontalScrollbarSize = 10;
            this.metroTabPage6.Location = new System.Drawing.Point(4, 37);
            this.metroTabPage6.Name = "metroTabPage6";
            this.metroTabPage6.Size = new System.Drawing.Size(788, 384);
            this.metroTabPage6.TabIndex = 5;
            this.metroTabPage6.Text = "LINE SORTER";
            this.metroTabPage6.VerticalScrollbarBarColor = true;
            this.metroTabPage6.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage6.VerticalScrollbarSize = 10;
            // 
            // lineSorterUC1
            // 
            this.lineSorterUC1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineSorterUC1.BackColor = System.Drawing.Color.Transparent;
            lineSorter1.SelectedSortType = "Alphabetical";
            lineSorter1.TextToSort = null;
            this.lineSorterUC1.LineSorter = lineSorter1;
            this.lineSorterUC1.Location = new System.Drawing.Point(31, 25);
            this.lineSorterUC1.Name = "lineSorterUC1";
            this.lineSorterUC1.Size = new System.Drawing.Size(754, 360);
            this.lineSorterUC1.TabIndex = 2;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.logUC1);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.metroTabPage3.Size = new System.Drawing.Size(802, 431);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "log";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // logUC1
            // 
            this.logUC1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logUC1.BackColor = System.Drawing.Color.Transparent;
            this.logUC1.Location = new System.Drawing.Point(22, 10);
            this.logUC1.Name = "logUC1";
            this.logUC1.Size = new System.Drawing.Size(764, 425);
            this.logUC1.TabIndex = 2;
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
            this.panelSettingsHelp.Location = new System.Drawing.Point(603, 6);
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
            this.ClientSize = new System.Drawing.Size(850, 550);
            this.Controls.Add(this.panelSettingsHelp);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(425, 260);
            this.Name = "ShellForm";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "SampleApp";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.mainTabControl.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.toolsTabControl.ResumeLayout(false);
            this.metroTabPage4.ResumeLayout(false);
            this.metroTabPage5.ResumeLayout(false);
            this.metroTabPage5.PerformLayout();
            this.metroTabPage6.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.panelSettingsHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl mainTabControl;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private MetroFramework.Controls.MetroTabPage metroTabPage5;
        private MetroFramework.Controls.MetroTabPage metroTabPage6;
        private MetroFramework.Controls.MetroTabControl toolsTabControl;
        private MetroFramework.Controls.MetroLink helpLink;
        private MetroFramework.Controls.MetroLink settingsLink;
        private System.Windows.Forms.Panel panelSettingsHelp;
        private System.Windows.Forms.Panel panel1;
        private Forms.IntroductionUC introductionUC1;
        private Forms.UUIDGeneratorUC uuidGeneratorUC1;
        private Forms.LogUC logUC1;
        private Forms.SampleTools.FlatUIColorPickerUC flatUIColorPickerUC1;
        private Forms.SampleTools.LineSorterUC lineSorterUC1;
    }
}

