namespace WinXPApp.Views.SampleTools
{
    partial class LineSorterView
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
            this.SortTypeComboBox = new MetroFramework.Controls.MetroComboBox();
            this.btnSelectAll = new MetroFramework.Controls.MetroButton();
            this.btnSort = new MetroFramework.Controls.MetroButton();
            this.SortTextTextBox = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(4, 4);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(94, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "LINE SORTER";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(4, 40);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(65, 19);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "Sort Type";
            // 
            // SortTypeComboBox
            // 
            this.SortTypeComboBox.FormattingEnabled = true;
            this.SortTypeComboBox.ItemHeight = 23;
            this.SortTypeComboBox.Location = new System.Drawing.Point(113, 37);
            this.SortTypeComboBox.Name = "SortTypeComboBox";
            this.SortTypeComboBox.Size = new System.Drawing.Size(167, 29);
            this.SortTypeComboBox.TabIndex = 2;
            this.SortTypeComboBox.UseSelectable = true;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(113, 96);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseSelectable = true;
            this.btnSelectAll.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(205, 95);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 4;
            this.btnSort.Text = "Sort";
            this.btnSort.UseSelectable = true;
            this.btnSort.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // SortTextTextBox
            // 
            this.SortTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.SortTextTextBox.CustomButton.Image = null;
            this.SortTextTextBox.CustomButton.Location = new System.Drawing.Point(295, 1);
            this.SortTextTextBox.CustomButton.Name = "";
            this.SortTextTextBox.CustomButton.Size = new System.Drawing.Size(439, 439);
            this.SortTextTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.SortTextTextBox.CustomButton.TabIndex = 1;
            this.SortTextTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.SortTextTextBox.CustomButton.UseSelectable = true;
            this.SortTextTextBox.CustomButton.Visible = false;
            this.SortTextTextBox.Lines = new string[0];
            this.SortTextTextBox.Location = new System.Drawing.Point(4, 129);
            this.SortTextTextBox.MaxLength = 32767;
            this.SortTextTextBox.Multiline = true;
            this.SortTextTextBox.Name = "SortTextTextBox";
            this.SortTextTextBox.PasswordChar = '\0';
            this.SortTextTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SortTextTextBox.SelectedText = "";
            this.SortTextTextBox.SelectionLength = 0;
            this.SortTextTextBox.SelectionStart = 0;
            this.SortTextTextBox.ShortcutsEnabled = true;
            this.SortTextTextBox.Size = new System.Drawing.Size(735, 441);
            this.SortTextTextBox.TabIndex = 5;
            this.SortTextTextBox.UseSelectable = true;
            this.SortTextTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.SortTextTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // LineSorterUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.SortTextTextBox);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.SortTypeComboBox);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Name = "LineSorterView";
            this.Size = new System.Drawing.Size(742, 573);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox SortTypeComboBox;
        private MetroFramework.Controls.MetroButton btnSelectAll;
        private MetroFramework.Controls.MetroButton btnSort;
        private MetroFramework.Controls.MetroTextBox SortTextTextBox;
    }
}
