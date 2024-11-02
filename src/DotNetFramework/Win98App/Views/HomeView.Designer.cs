namespace Win98App.Views
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
            this.label1 = new System.Windows.Forms.Label();
            this.IntroLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LoggingLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LicenseLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "INTRODUCTION";
            // 
            // IntroLabel
            // 
            this.IntroLabel.AutoSize = true;
            this.IntroLabel.Location = new System.Drawing.Point(-3, 15);
            this.IntroLabel.Name = "IntroLabel";
            this.IntroLabel.Size = new System.Drawing.Size(468, 13);
            this.IntroLabel.TabIndex = 1;
            this.IntroLabel.Text = "This is a sample application generated from Aaron Salisbury\'s Windows 98 App Solu" +
    "tion Template.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(704, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "This application follows three-tier architecture. There is a core library that ac" +
    "ts as a shim for logging, dependency injection, and session authorization.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(-3, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "DESIGN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-3, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(649, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "The presentation layer implements an MVP design pattern. MVP separates views from" +
    " models, which allows for projects that are cleaner, ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-3, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "easier to extend, and more testable.";
            // 
            // LoggingLabel
            // 
            this.LoggingLabel.AutoSize = true;
            this.LoggingLabel.Location = new System.Drawing.Point(-3, 178);
            this.LoggingLabel.Name = "LoggingLabel";
            this.LoggingLabel.Size = new System.Drawing.Size(632, 13);
            this.LoggingLabel.TabIndex = 8;
            this.LoggingLabel.Text = "The application shares an implementation of the pluggable and reusable Logging ap" +
    "plication block of the Microsoft Enterprise Library.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(-3, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "LOGGING";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(-3, 243);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(342, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Aaron Salisbury\'s Windows 98 App Solution Template is released under";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(-3, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "USE";
            // 
            // LicenseLink
            // 
            this.LicenseLink.AutoSize = true;
            this.LicenseLink.BackColor = System.Drawing.Color.Transparent;
            this.LicenseLink.Location = new System.Drawing.Point(336, 243);
            this.LicenseLink.Name = "LicenseLink";
            this.LicenseLink.Size = new System.Drawing.Size(91, 13);
            this.LicenseLink.TabIndex = 11;
            this.LicenseLink.TabStop = true;
            this.LicenseLink.Text = "The MIT License.";
            this.LicenseLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LicenseLink_LinkClicked);
            // 
            // HomeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.LicenseLink);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.LoggingLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IntroLabel);
            this.Controls.Add(this.label1);
            this.Name = "HomeView";
            this.Size = new System.Drawing.Size(785, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label IntroLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LoggingLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel LicenseLink;
    }
}
