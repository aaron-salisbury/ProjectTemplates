
namespace WinXPApp.Forms.SampleTools
{
    partial class LineSorterUC
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
            this.cbSortTypes = new MetroFramework.Controls.MetroComboBox();
            this.btnSelectAll = new MetroFramework.Controls.MetroButton();
            this.btnSort = new MetroFramework.Controls.MetroButton();
            this.tbTextToSort = new MetroFramework.Controls.MetroTextBox();
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
            // cbSortTypes
            // 
            this.cbSortTypes.FormattingEnabled = true;
            this.cbSortTypes.ItemHeight = 23;
            this.cbSortTypes.Location = new System.Drawing.Point(113, 37);
            this.cbSortTypes.Name = "cbSortTypes";
            this.cbSortTypes.Size = new System.Drawing.Size(167, 29);
            this.cbSortTypes.TabIndex = 2;
            this.cbSortTypes.UseSelectable = true;
            this.cbSortTypes.SelectedIndexChanged += new System.EventHandler(this.CbSortTypes_SelectedIndexChanged);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(113, 96);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseSelectable = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(205, 95);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 4;
            this.btnSort.Text = "Sort";
            this.btnSort.UseSelectable = true;
            this.btnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // tbTextToSort
            // 
            this.tbTextToSort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tbTextToSort.CustomButton.Image = null;
            this.tbTextToSort.CustomButton.Location = new System.Drawing.Point(297, 1);
            this.tbTextToSort.CustomButton.Name = "";
            this.tbTextToSort.CustomButton.Size = new System.Drawing.Size(437, 437);
            this.tbTextToSort.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbTextToSort.CustomButton.TabIndex = 1;
            this.tbTextToSort.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbTextToSort.CustomButton.UseSelectable = true;
            this.tbTextToSort.CustomButton.Visible = false;
            this.tbTextToSort.Lines = new string[0];
            this.tbTextToSort.Location = new System.Drawing.Point(4, 129);
            this.tbTextToSort.MaxLength = 32767;
            this.tbTextToSort.Multiline = true;
            this.tbTextToSort.Name = "tbTextToSort";
            this.tbTextToSort.PasswordChar = '\0';
            this.tbTextToSort.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbTextToSort.SelectedText = "";
            this.tbTextToSort.SelectionLength = 0;
            this.tbTextToSort.SelectionStart = 0;
            this.tbTextToSort.ShortcutsEnabled = true;
            this.tbTextToSort.Size = new System.Drawing.Size(735, 439);
            this.tbTextToSort.TabIndex = 5;
            this.tbTextToSort.UseSelectable = true;
            this.tbTextToSort.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbTextToSort.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // LineSorterUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tbTextToSort);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.cbSortTypes);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Name = "LineSorterUC";
            this.Size = new System.Drawing.Size(742, 573);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox cbSortTypes;
        private MetroFramework.Controls.MetroButton btnSelectAll;
        private MetroFramework.Controls.MetroButton btnSort;
        private MetroFramework.Controls.MetroTextBox tbTextToSort;
    }
}
