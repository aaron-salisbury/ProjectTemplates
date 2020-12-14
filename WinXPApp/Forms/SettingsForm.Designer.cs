
namespace WinXPApp.Forms
{
    partial class SettingsForm
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
            this.okButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.themeComboBox = new MetroFramework.Controls.MetroComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.stylesPanelTop = new System.Windows.Forms.Panel();
            this.stylesPanelMiddle = new System.Windows.Forms.Panel();
            this.stylesPanelBottom = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(402, 354);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseSelectable = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(72, 93);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(52, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Theme:";
            // 
            // themeComboBox
            // 
            this.themeComboBox.FormattingEnabled = true;
            this.themeComboBox.ItemHeight = 23;
            this.themeComboBox.Location = new System.Drawing.Point(131, 90);
            this.themeComboBox.Name = "themeComboBox";
            this.themeComboBox.Size = new System.Drawing.Size(121, 29);
            this.themeComboBox.TabIndex = 2;
            this.themeComboBox.UseSelectable = true;
            this.themeComboBox.SelectedIndexChanged += new System.EventHandler(this.themeComboBox_SelectedIndexChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(72, 147);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(44, 19);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "Styles:";
            // 
            // stylesPanelTop
            // 
            this.stylesPanelTop.Location = new System.Drawing.Point(131, 127);
            this.stylesPanelTop.Margin = new System.Windows.Forms.Padding(0);
            this.stylesPanelTop.Name = "stylesPanelTop";
            this.stylesPanelTop.Size = new System.Drawing.Size(200, 70);
            this.stylesPanelTop.TabIndex = 5;
            // 
            // stylesPanelMiddle
            // 
            this.stylesPanelMiddle.Location = new System.Drawing.Point(131, 182);
            this.stylesPanelMiddle.Name = "stylesPanelMiddle";
            this.stylesPanelMiddle.Size = new System.Drawing.Size(200, 70);
            this.stylesPanelMiddle.TabIndex = 6;
            // 
            // stylesPanelBottom
            // 
            this.stylesPanelBottom.Location = new System.Drawing.Point(131, 237);
            this.stylesPanelBottom.Name = "stylesPanelBottom";
            this.stylesPanelBottom.Size = new System.Drawing.Size(200, 70);
            this.stylesPanelBottom.TabIndex = 7;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.stylesPanelBottom);
            this.Controls.Add(this.stylesPanelMiddle);
            this.Controls.Add(this.stylesPanelTop);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.themeComboBox);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.okButton);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton okButton;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox themeComboBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.Panel stylesPanelTop;
        private System.Windows.Forms.Panel stylesPanelMiddle;
        private System.Windows.Forms.Panel stylesPanelBottom;
    }
}