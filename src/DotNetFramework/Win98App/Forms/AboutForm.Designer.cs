
namespace Win98App.Forms
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.AppNameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AppDescriptionLabel = new System.Windows.Forms.Label();
            this.AppLink = new System.Windows.Forms.LinkLabel();
            this.OkButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AppNameLabel
            // 
            this.AppNameLabel.AutoSize = true;
            this.AppNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppNameLabel.Location = new System.Drawing.Point(12, 32);
            this.AppNameLabel.Name = "AppNameLabel";
            this.AppNameLabel.Size = new System.Drawing.Size(214, 31);
            this.AppNameLabel.TabIndex = 0;
            this.AppNameLabel.Text = "App Name Label";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.AppNameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 100);
            this.panel1.TabIndex = 1;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(15, 137);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(71, 13);
            this.VersionLabel.TabIndex = 2;
            this.VersionLabel.Text = "Version Label";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ABOUT THIS APPLICATION";
            // 
            // AppDescriptionLabel
            // 
            this.AppDescriptionLabel.AutoSize = true;
            this.AppDescriptionLabel.Location = new System.Drawing.Point(15, 157);
            this.AppDescriptionLabel.Name = "AppDescriptionLabel";
            this.AppDescriptionLabel.Size = new System.Drawing.Size(111, 13);
            this.AppDescriptionLabel.TabIndex = 4;
            this.AppDescriptionLabel.Text = "App Description Label";
            // 
            // AppLink
            // 
            this.AppLink.AutoSize = true;
            this.AppLink.Location = new System.Drawing.Point(15, 179);
            this.AppLink.Name = "AppLink";
            this.AppLink.Size = new System.Drawing.Size(113, 13);
            this.AppLink.TabIndex = 5;
            this.AppLink.TabStop = true;
            this.AppLink.Text = "Application Web Page";
            this.AppLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AppLink_LinkClicked);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OkButton.Location = new System.Drawing.Point(422, 251);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(509, 286);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.AppLink);
            this.Controls.Add(this.AppDescriptionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(525, 325);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(525, 325);
            this.Name = "AboutForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AppNameLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label AppDescriptionLabel;
        private System.Windows.Forms.LinkLabel AppLink;
        private System.Windows.Forms.Button OkButton;
    }
}