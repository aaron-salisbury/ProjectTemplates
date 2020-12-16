
namespace WinXPApp.Forms
{
    partial class UUIDGeneratorUC
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.txtNewUUID = new MetroFramework.Controls.MetroTextBox();
            this.btnGenerate = new MetroFramework.Controls.MetroButton();
            this.btnCopy = new MetroFramework.Controls.MetroButton();
            this.cbCapitalize = new MetroFramework.Controls.MetroCheckBox();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(4, 32);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(66, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Capitalize";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(4, 4);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(127, 19);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "UUID GENERATOR";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(4, 101);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(69, 19);
            this.metroLabel3.TabIndex = 3;
            this.metroLabel3.Text = "New UUID";
            // 
            // txtNewUUID
            // 
            // 
            // 
            // 
            this.txtNewUUID.CustomButton.Image = null;
            this.txtNewUUID.CustomButton.Location = new System.Drawing.Point(229, 1);
            this.txtNewUUID.CustomButton.Name = "";
            this.txtNewUUID.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtNewUUID.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNewUUID.CustomButton.TabIndex = 1;
            this.txtNewUUID.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNewUUID.CustomButton.UseSelectable = true;
            this.txtNewUUID.CustomButton.Visible = false;
            this.txtNewUUID.Enabled = false;
            this.txtNewUUID.Lines = new string[0];
            this.txtNewUUID.Location = new System.Drawing.Point(125, 101);
            this.txtNewUUID.MaxLength = 32767;
            this.txtNewUUID.Name = "txtNewUUID";
            this.txtNewUUID.PasswordChar = '\0';
            this.txtNewUUID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNewUUID.SelectedText = "";
            this.txtNewUUID.SelectionLength = 0;
            this.txtNewUUID.SelectionStart = 0;
            this.txtNewUUID.ShortcutsEnabled = true;
            this.txtNewUUID.Size = new System.Drawing.Size(251, 23);
            this.txtNewUUID.TabIndex = 4;
            this.txtNewUUID.UseSelectable = true;
            this.txtNewUUID.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNewUUID.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(125, 72);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseSelectable = true;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(207, 71);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseSelectable = true;
            this.btnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // cbCapitalize
            // 
            this.cbCapitalize.AutoSize = true;
            this.cbCapitalize.Checked = true;
            this.cbCapitalize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCapitalize.Location = new System.Drawing.Point(125, 36);
            this.cbCapitalize.Name = "cbCapitalize";
            this.cbCapitalize.Size = new System.Drawing.Size(26, 15);
            this.cbCapitalize.TabIndex = 0;
            this.cbCapitalize.Text = " ";
            this.cbCapitalize.UseSelectable = true;
            // 
            // UUIDGeneratorUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cbCapitalize);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtNewUUID);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Name = "UUIDGeneratorUC";
            this.Size = new System.Drawing.Size(588, 512);
            this.Load += new System.EventHandler(this.UUIDGeneratorUC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox txtNewUUID;
        private MetroFramework.Controls.MetroButton btnGenerate;
        private MetroFramework.Controls.MetroButton btnCopy;
        private MetroFramework.Controls.MetroCheckBox cbCapitalize;
    }
}
