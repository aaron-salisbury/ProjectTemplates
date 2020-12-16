
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
            this.btnDownload = new MetroFramework.Controls.MetroButton();
            this.tbLogs = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(663, 0);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseSelectable = true;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // tbLogs
            // 
            this.tbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tbLogs.CustomButton.Image = null;
            this.tbLogs.CustomButton.Location = new System.Drawing.Point(352, 1);
            this.tbLogs.CustomButton.Name = "";
            this.tbLogs.CustomButton.Size = new System.Drawing.Size(379, 379);
            this.tbLogs.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbLogs.CustomButton.TabIndex = 1;
            this.tbLogs.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbLogs.CustomButton.UseSelectable = true;
            this.tbLogs.CustomButton.Visible = false;
            this.tbLogs.Lines = new string[0];
            this.tbLogs.Location = new System.Drawing.Point(6, 29);
            this.tbLogs.MaxLength = 32767;
            this.tbLogs.Multiline = true;
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.PasswordChar = '\0';
            this.tbLogs.ReadOnly = true;
            this.tbLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLogs.SelectedText = "";
            this.tbLogs.SelectionLength = 0;
            this.tbLogs.SelectionStart = 0;
            this.tbLogs.ShortcutsEnabled = true;
            this.tbLogs.Size = new System.Drawing.Size(732, 381);
            this.tbLogs.TabIndex = 2;
            this.tbLogs.UseSelectable = true;
            this.tbLogs.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbLogs.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // LogUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tbLogs);
            this.Controls.Add(this.btnDownload);
            this.Name = "LogUC";
            this.Size = new System.Drawing.Size(738, 425);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton btnDownload;
        private MetroFramework.Controls.MetroTextBox tbLogs;
    }
}
