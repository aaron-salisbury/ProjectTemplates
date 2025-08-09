namespace WinXPApp.Views.SampleTools
{
    partial class FlatUIColorPickerView
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
            this.NameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.HexTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.CopyButton = new MetroFramework.Controls.MetroButton();
            this.ColorsTLP = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(4, 40);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(45, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Name";
            // 
            // NameTextBox
            // 
            // 
            // 
            // 
            this.NameTextBox.CustomButton.Image = null;
            this.NameTextBox.CustomButton.Location = new System.Drawing.Point(220, 1);
            this.NameTextBox.CustomButton.Name = "";
            this.NameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.NameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.NameTextBox.CustomButton.TabIndex = 1;
            this.NameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.NameTextBox.CustomButton.UseSelectable = true;
            this.NameTextBox.CustomButton.Visible = false;
            this.NameTextBox.Enabled = false;
            this.NameTextBox.Lines = new string[0];
            this.NameTextBox.Location = new System.Drawing.Point(94, 36);
            this.NameTextBox.MaxLength = 32767;
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.PasswordChar = '\0';
            this.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.NameTextBox.SelectedText = "";
            this.NameTextBox.SelectionLength = 0;
            this.NameTextBox.SelectionStart = 0;
            this.NameTextBox.ShortcutsEnabled = true;
            this.NameTextBox.Size = new System.Drawing.Size(242, 23);
            this.NameTextBox.TabIndex = 1;
            this.NameTextBox.UseSelectable = true;
            this.NameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.NameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(4, 83);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(31, 19);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "Hex";
            // 
            // HexTextBox
            // 
            // 
            // 
            // 
            this.HexTextBox.CustomButton.Image = null;
            this.HexTextBox.CustomButton.Location = new System.Drawing.Point(220, 1);
            this.HexTextBox.CustomButton.Name = "";
            this.HexTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.HexTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.HexTextBox.CustomButton.TabIndex = 1;
            this.HexTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.HexTextBox.CustomButton.UseSelectable = true;
            this.HexTextBox.CustomButton.Visible = false;
            this.HexTextBox.Enabled = false;
            this.HexTextBox.Lines = new string[0];
            this.HexTextBox.Location = new System.Drawing.Point(94, 79);
            this.HexTextBox.MaxLength = 32767;
            this.HexTextBox.Name = "HexTextBox";
            this.HexTextBox.PasswordChar = '\0';
            this.HexTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.HexTextBox.SelectedText = "";
            this.HexTextBox.SelectionLength = 0;
            this.HexTextBox.SelectionStart = 0;
            this.HexTextBox.ShortcutsEnabled = true;
            this.HexTextBox.Size = new System.Drawing.Size(242, 23);
            this.HexTextBox.TabIndex = 3;
            this.HexTextBox.UseSelectable = true;
            this.HexTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.HexTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel3.Location = new System.Drawing.Point(4, 4);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(160, 19);
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Text = "FLAT UI COLOR PICKER";
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(343, 79);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 23);
            this.CopyButton.TabIndex = 5;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseSelectable = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // ColorsTLP
            // 
            this.ColorsTLP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorsTLP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ColorsTLP.CausesValidation = false;
            this.ColorsTLP.ColumnCount = 2;
            this.ColorsTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ColorsTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ColorsTLP.Location = new System.Drawing.Point(4, 134);
            this.ColorsTLP.Name = "ColorsTLP";
            this.ColorsTLP.RowCount = 2;
            this.ColorsTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ColorsTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ColorsTLP.Size = new System.Drawing.Size(621, 414);
            this.ColorsTLP.TabIndex = 6;
            // 
            // FlatUIColorPickerUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ColorsTLP);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.HexTextBox);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.metroLabel1);
            this.Name = "FlatUIColorPickerView";
            this.Size = new System.Drawing.Size(628, 563);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox NameTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox HexTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton CopyButton;
        private System.Windows.Forms.TableLayoutPanel ColorsTLP;
    }
}
