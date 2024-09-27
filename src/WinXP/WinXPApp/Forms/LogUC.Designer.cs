
namespace WinXPApp.Forms
{
    partial class LogUC
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
            this.DownloadButton = new MetroFramework.Controls.MetroButton();
            this.LogsTextBox = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // DownloadButton
            // 
            this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadButton.Location = new System.Drawing.Point(663, 0);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(75, 23);
            this.DownloadButton.TabIndex = 1;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseSelectable = true;
            this.DownloadButton.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // LogsTextBox
            // 
            this.LogsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.LogsTextBox.CustomButton.Image = null;
            this.LogsTextBox.CustomButton.Location = new System.Drawing.Point(352, 1);
            this.LogsTextBox.CustomButton.Name = "";
            this.LogsTextBox.CustomButton.Size = new System.Drawing.Size(379, 379);
            this.LogsTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.LogsTextBox.CustomButton.TabIndex = 1;
            this.LogsTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.LogsTextBox.CustomButton.UseSelectable = true;
            this.LogsTextBox.CustomButton.Visible = false;
            this.LogsTextBox.Lines = new string[0];
            this.LogsTextBox.Location = new System.Drawing.Point(6, 29);
            this.LogsTextBox.MaxLength = 32767;
            this.LogsTextBox.Multiline = true;
            this.LogsTextBox.Name = "LogsTextBox";
            this.LogsTextBox.PasswordChar = '\0';
            this.LogsTextBox.ReadOnly = true;
            this.LogsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogsTextBox.SelectedText = "";
            this.LogsTextBox.SelectionLength = 0;
            this.LogsTextBox.SelectionStart = 0;
            this.LogsTextBox.ShortcutsEnabled = true;
            this.LogsTextBox.Size = new System.Drawing.Size(732, 381);
            this.LogsTextBox.TabIndex = 2;
            this.LogsTextBox.UseSelectable = true;
            this.LogsTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.LogsTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // LogUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.LogsTextBox);
            this.Controls.Add(this.DownloadButton);
            this.Name = "LogUC";
            this.Size = new System.Drawing.Size(738, 425);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton DownloadButton;
        private MetroFramework.Controls.MetroTextBox LogsTextBox;
    }
}
