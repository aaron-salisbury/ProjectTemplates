
namespace Win98App.Forms.SampleTools
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SortTypeComboBox = new System.Windows.Forms.ComboBox();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.SortButton = new System.Windows.Forms.Button();
            this.SortTextTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "LINE SORTER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sort Type";
            // 
            // SortTypeComboBox
            // 
            this.SortTypeComboBox.FormattingEnabled = true;
            this.SortTypeComboBox.Location = new System.Drawing.Point(95, 57);
            this.SortTypeComboBox.Name = "SortTypeComboBox";
            this.SortTypeComboBox.Size = new System.Drawing.Size(136, 21);
            this.SortTypeComboBox.TabIndex = 2;
            this.SortTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.SortTypeComboBox_SelectedIndexChanged);
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.Location = new System.Drawing.Point(95, 92);
            this.SelectAllButton.Margin = new System.Windows.Forms.Padding(0);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.SelectAllButton.TabIndex = 3;
            this.SelectAllButton.Text = "Select All";
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // SortButton
            // 
            this.SortButton.Location = new System.Drawing.Point(176, 92);
            this.SortButton.Margin = new System.Windows.Forms.Padding(0);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(75, 23);
            this.SortButton.TabIndex = 4;
            this.SortButton.Text = "Sort";
            this.SortButton.UseVisualStyleBackColor = true;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // SortTextTextBox
            // 
            this.SortTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SortTextTextBox.Location = new System.Drawing.Point(10, 106);
            this.SortTextTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.SortTextTextBox.Multiline = true;
            this.SortTextTextBox.Name = "SortTextTextBox";
            this.SortTextTextBox.Size = new System.Drawing.Size(561, 468);
            this.SortTextTextBox.TabIndex = 5;
            // 
            // LineSorterUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SortTextTextBox);
            this.Controls.Add(this.SortButton);
            this.Controls.Add(this.SelectAllButton);
            this.Controls.Add(this.SortTypeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LineSorterUC";
            this.Size = new System.Drawing.Size(574, 577);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SortTypeComboBox;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.TextBox SortTextTextBox;
    }
}
