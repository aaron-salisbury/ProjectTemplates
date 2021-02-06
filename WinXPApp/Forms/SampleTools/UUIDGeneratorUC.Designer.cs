
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
            this.NewGuidTextBox = new MetroFramework.Controls.MetroTextBox();
            this.GenerateButton = new MetroFramework.Controls.MetroButton();
            this.CopyButton = new MetroFramework.Controls.MetroButton();
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
            // NewGuidTextBox
            // 
            // 
            // 
            // 
            this.NewGuidTextBox.CustomButton.Image = null;
            this.NewGuidTextBox.CustomButton.Location = new System.Drawing.Point(229, 1);
            this.NewGuidTextBox.CustomButton.Name = "";
            this.NewGuidTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.NewGuidTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.NewGuidTextBox.CustomButton.TabIndex = 1;
            this.NewGuidTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.NewGuidTextBox.CustomButton.UseSelectable = true;
            this.NewGuidTextBox.CustomButton.Visible = false;
            this.NewGuidTextBox.Enabled = false;
            this.NewGuidTextBox.Lines = new string[0];
            this.NewGuidTextBox.Location = new System.Drawing.Point(125, 101);
            this.NewGuidTextBox.MaxLength = 32767;
            this.NewGuidTextBox.Name = "NewGuidTextBox";
            this.NewGuidTextBox.PasswordChar = '\0';
            this.NewGuidTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.NewGuidTextBox.SelectedText = "";
            this.NewGuidTextBox.SelectionLength = 0;
            this.NewGuidTextBox.SelectionStart = 0;
            this.NewGuidTextBox.ShortcutsEnabled = true;
            this.NewGuidTextBox.Size = new System.Drawing.Size(251, 23);
            this.NewGuidTextBox.TabIndex = 4;
            this.NewGuidTextBox.UseSelectable = true;
            this.NewGuidTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.NewGuidTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(125, 72);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateButton.TabIndex = 1;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseSelectable = true;
            this.GenerateButton.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(206, 72);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 23);
            this.CopyButton.TabIndex = 2;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseSelectable = true;
            this.CopyButton.Click += new System.EventHandler(this.BtnCopy_Click);
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
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.NewGuidTextBox);
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
        private MetroFramework.Controls.MetroTextBox NewGuidTextBox;
        private MetroFramework.Controls.MetroButton GenerateButton;
        private MetroFramework.Controls.MetroButton CopyButton;
        private MetroFramework.Controls.MetroCheckBox cbCapitalize;
    }
}
