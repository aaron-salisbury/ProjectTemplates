namespace WinXPApp.Views
{
    partial class HomeView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroLabelIntro = new MetroFramework.Controls.MetroLabel();
            this.metroLabelIntro1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabelDesign = new MetroFramework.Controls.MetroLabel();
            this.metroLabelDesign1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabelDesign2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabelDesign3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabelLog = new MetroFramework.Controls.MetroLabel();
            this.metroLabelLog1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabelLog2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabelAppearance = new MetroFramework.Controls.MetroLabel();
            this.metroLabelAppearance1 = new MetroFramework.Controls.MetroLabel();
            this.mfLink = new MetroFramework.Controls.MetroLink();
            this.metroLabelAppearance2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabelAppearance3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabelUse = new MetroFramework.Controls.MetroLabel();
            this.metroLabelUse1 = new MetroFramework.Controls.MetroLabel();
            this.mitLink = new MetroFramework.Controls.MetroLink();
            this.SuspendLayout();
            // 
            // metroLabelIntro
            // 
            this.metroLabelIntro.AutoSize = true;
            this.metroLabelIntro.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabelIntro.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabelIntro.Location = new System.Drawing.Point(0, 0);
            this.metroLabelIntro.Name = "metroLabelIntro";
            this.metroLabelIntro.Size = new System.Drawing.Size(98, 15);
            this.metroLabelIntro.TabIndex = 1;
            this.metroLabelIntro.Text = "INTRODUCTION";
            // 
            // metroLabelIntro1
            // 
            this.metroLabelIntro1.AutoSize = true;
            this.metroLabelIntro1.Location = new System.Drawing.Point(0, 20);
            this.metroLabelIntro1.Name = "metroLabelIntro1";
            this.metroLabelIntro1.Size = new System.Drawing.Size(585, 19);
            this.metroLabelIntro1.TabIndex = 0;
            this.metroLabelIntro1.Text = "This is a sample application generated from Aaron Salisbury\'s Windows XP App Solu" +
    "tion Template.";
            // 
            // metroLabelDesign
            // 
            this.metroLabelDesign.AutoSize = true;
            this.metroLabelDesign.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabelDesign.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabelDesign.Location = new System.Drawing.Point(0, 75);
            this.metroLabelDesign.Name = "metroLabelDesign";
            this.metroLabelDesign.Size = new System.Drawing.Size(51, 15);
            this.metroLabelDesign.TabIndex = 3;
            this.metroLabelDesign.Text = "DESIGN";
            // 
            // metroLabelDesign1
            // 
            this.metroLabelDesign1.AutoSize = true;
            this.metroLabelDesign1.Location = new System.Drawing.Point(0, 96);
            this.metroLabelDesign1.Name = "metroLabelDesign1";
            this.metroLabelDesign1.Size = new System.Drawing.Size(875, 19);
            this.metroLabelDesign1.TabIndex = 16;
            this.metroLabelDesign1.Text = "This application follows three-tier architecture. There is a core library that ac" +
    "ts as a shim for logging, dependency injection, and session authorization.";
            // 
            // metroLabelDesign2
            // 
            this.metroLabelDesign2.AutoSize = true;
            this.metroLabelDesign2.Location = new System.Drawing.Point(0, 121);
            this.metroLabelDesign2.Name = "metroLabelDesign2";
            this.metroLabelDesign2.Size = new System.Drawing.Size(805, 19);
            this.metroLabelDesign2.TabIndex = 2;
            this.metroLabelDesign2.Text = "The presentation layer implements an MVP design pattern. MVP separates views from" +
    " models, which allows for projects that are cleaner, ";
            // 
            // metroLabelDesign3
            // 
            this.metroLabelDesign3.AutoSize = true;
            this.metroLabelDesign3.Location = new System.Drawing.Point(0, 145);
            this.metroLabelDesign3.Name = "metroLabelDesign3";
            this.metroLabelDesign3.Size = new System.Drawing.Size(219, 19);
            this.metroLabelDesign3.TabIndex = 17;
            this.metroLabelDesign3.Text = "easier to extend, and more testable.";
            // 
            // metroLabelLog
            // 
            this.metroLabelLog.AutoSize = true;
            this.metroLabelLog.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabelLog.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabelLog.Location = new System.Drawing.Point(0, 199);
            this.metroLabelLog.Name = "metroLabelLog";
            this.metroLabelLog.Size = new System.Drawing.Size(62, 15);
            this.metroLabelLog.TabIndex = 6;
            this.metroLabelLog.Text = "LOGGING";
            // 
            // metroLabelLog1
            // 
            this.metroLabelLog1.AutoSize = true;
            this.metroLabelLog1.Location = new System.Drawing.Point(0, 219);
            this.metroLabelLog1.Name = "metroLabelLog1";
            this.metroLabelLog1.Size = new System.Drawing.Size(850, 19);
            this.metroLabelLog1.TabIndex = 5;
            this.metroLabelLog1.Text = "Structured logging is done via an ILogger available from the app’s service provid" +
    "er. The ILogger is implemented using the Patterns and Practices";
            // 
            // metroLabelLog2
            // 
            this.metroLabelLog2.AutoSize = true;
            this.metroLabelLog2.Location = new System.Drawing.Point(1, 243);
            this.metroLabelLog2.Name = "metroLabelLog2";
            this.metroLabelLog2.Size = new System.Drawing.Size(109, 19);
            this.metroLabelLog2.TabIndex = 18;
            this.metroLabelLog2.Text = "Logging module.";
            // 
            // metroLabelAppearance
            // 
            this.metroLabelAppearance.AutoSize = true;
            this.metroLabelAppearance.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabelAppearance.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabelAppearance.Location = new System.Drawing.Point(0, 291);
            this.metroLabelAppearance.Name = "metroLabelAppearance";
            this.metroLabelAppearance.Size = new System.Drawing.Size(81, 15);
            this.metroLabelAppearance.TabIndex = 9;
            this.metroLabelAppearance.Text = "APPEARANCE";
            // 
            // metroLabelAppearance1
            // 
            this.metroLabelAppearance1.AutoSize = true;
            this.metroLabelAppearance1.Location = new System.Drawing.Point(0, 311);
            this.metroLabelAppearance1.Name = "metroLabelAppearance1";
            this.metroLabelAppearance1.Size = new System.Drawing.Size(206, 19);
            this.metroLabelAppearance1.TabIndex = 8;
            this.metroLabelAppearance1.Text = "The WinForms app leverages the ";
            // 
            // mfLink
            // 
            this.mfLink.Location = new System.Drawing.Point(198, 310);
            this.mfLink.Name = "mfLink";
            this.mfLink.Size = new System.Drawing.Size(107, 23);
            this.mfLink.TabIndex = 10;
            this.mfLink.Text = "MetroModernUI";
            this.mfLink.UseSelectable = true;
            this.mfLink.UseStyleColors = true;
            this.mfLink.Click += new System.EventHandler(this.ModernUILink_Click);
            // 
            // metroLabelAppearance2
            // 
            this.metroLabelAppearance2.AutoSize = true;
            this.metroLabelAppearance2.Location = new System.Drawing.Point(298, 311);
            this.metroLabelAppearance2.Name = "metroLabelAppearance2";
            this.metroLabelAppearance2.Size = new System.Drawing.Size(330, 19);
            this.metroLabelAppearance2.TabIndex = 11;
            this.metroLabelAppearance2.Text = "NuGet package for various themes, controls, and styles.";
            // 
            // metroLabelAppearance3
            // 
            this.metroLabelAppearance3.AutoSize = true;
            this.metroLabelAppearance3.Location = new System.Drawing.Point(0, 336);
            this.metroLabelAppearance3.Name = "metroLabelAppearance3";
            this.metroLabelAppearance3.Size = new System.Drawing.Size(708, 19);
            this.metroLabelAppearance3.TabIndex = 15;
            this.metroLabelAppearance3.Text = "The appearance can be changed on the fly. Make sure you visit the settings to ada" +
    "pt the appearance to your preference.";
            // 
            // metroLabelUse
            // 
            this.metroLabelUse.AutoSize = true;
            this.metroLabelUse.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabelUse.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabelUse.Location = new System.Drawing.Point(0, 387);
            this.metroLabelUse.Name = "metroLabelUse";
            this.metroLabelUse.Size = new System.Drawing.Size(29, 15);
            this.metroLabelUse.TabIndex = 13;
            this.metroLabelUse.Text = "USE";
            // 
            // metroLabelUse1
            // 
            this.metroLabelUse1.AutoSize = true;
            this.metroLabelUse1.Location = new System.Drawing.Point(0, 407);
            this.metroLabelUse1.Name = "metroLabelUse1";
            this.metroLabelUse1.Size = new System.Drawing.Size(425, 19);
            this.metroLabelUse1.TabIndex = 12;
            this.metroLabelUse1.Text = "Aaron Salisbury\'s Windows XP App Solution Template is released under";
            // 
            // mitLink
            // 
            this.mitLink.Location = new System.Drawing.Point(422, 406);
            this.mitLink.Name = "mitLink";
            this.mitLink.Size = new System.Drawing.Size(100, 23);
            this.mitLink.TabIndex = 14;
            this.mitLink.Text = "The MIT License.";
            this.mitLink.UseSelectable = true;
            this.mitLink.UseStyleColors = true;
            this.mitLink.Click += new System.EventHandler(this.LicenseLink_Click);
            // 
            // HomeView
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.metroLabelLog2);
            this.Controls.Add(this.metroLabelDesign3);
            this.Controls.Add(this.metroLabelDesign1);
            this.Controls.Add(this.metroLabelAppearance3);
            this.Controls.Add(this.metroLabelUse1);
            this.Controls.Add(this.metroLabelUse);
            this.Controls.Add(this.mitLink);
            this.Controls.Add(this.metroLabelAppearance2);
            this.Controls.Add(this.metroLabelAppearance1);
            this.Controls.Add(this.metroLabelAppearance);
            this.Controls.Add(this.metroLabelLog1);
            this.Controls.Add(this.metroLabelLog);
            this.Controls.Add(this.metroLabelDesign);
            this.Controls.Add(this.metroLabelDesign2);
            this.Controls.Add(this.metroLabelIntro);
            this.Controls.Add(this.metroLabelIntro1);
            this.Controls.Add(this.mfLink);
            this.Name = "HomeView";
            this.Size = new System.Drawing.Size(891, 536);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabelIntro;
        private MetroFramework.Controls.MetroLabel metroLabelIntro1;
        private MetroFramework.Controls.MetroLabel metroLabelDesign;
        private MetroFramework.Controls.MetroLabel metroLabelDesign1;
        private MetroFramework.Controls.MetroLabel metroLabelDesign2;
        private MetroFramework.Controls.MetroLabel metroLabelDesign3;
        private MetroFramework.Controls.MetroLabel metroLabelLog;
        private MetroFramework.Controls.MetroLabel metroLabelLog1;
        private MetroFramework.Controls.MetroLabel metroLabelLog2;
        private MetroFramework.Controls.MetroLabel metroLabelAppearance;
        private MetroFramework.Controls.MetroLabel metroLabelAppearance1;
        private MetroFramework.Controls.MetroLink mfLink;
        private MetroFramework.Controls.MetroLabel metroLabelAppearance2;
        private MetroFramework.Controls.MetroLabel metroLabelAppearance3;
        private MetroFramework.Controls.MetroLabel metroLabelUse;
        private MetroFramework.Controls.MetroLabel metroLabelUse1;
        private MetroFramework.Controls.MetroLink mitLink;
    }
}
