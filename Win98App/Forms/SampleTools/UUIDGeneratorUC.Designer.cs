
namespace Win98App.Forms.SampleTools
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CapitalizeCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.NewGuidTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "UUID GENERATOR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Capitalize";
            // 
            // CapitalizeCheckBox
            // 
            this.CapitalizeCheckBox.AutoSize = true;
            this.CapitalizeCheckBox.Location = new System.Drawing.Point(91, 43);
            this.CapitalizeCheckBox.Name = "CapitalizeCheckBox";
            this.CapitalizeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.CapitalizeCheckBox.TabIndex = 2;
            this.CapitalizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(105, 97);
            this.GenerateButton.Margin = new System.Windows.Forms.Padding(0);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateButton.TabIndex = 3;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(186, 97);
            this.CopyButton.Margin = new System.Windows.Forms.Padding(0);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 23);
            this.CopyButton.TabIndex = 4;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // NewGuidTextBox
            // 
            this.NewGuidTextBox.Enabled = false;
            this.NewGuidTextBox.Location = new System.Drawing.Point(105, 125);
            this.NewGuidTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.NewGuidTextBox.Name = "NewGuidTextBox";
            this.NewGuidTextBox.Size = new System.Drawing.Size(235, 20);
            this.NewGuidTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "New UUID";
            // 
            // UUIDGeneratorUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NewGuidTextBox);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.CapitalizeCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UUIDGeneratorUC";
            this.Size = new System.Drawing.Size(565, 564);
            this.Load += new System.EventHandler(this.UUIDGeneratorUC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CapitalizeCheckBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.TextBox NewGuidTextBox;
        private System.Windows.Forms.Label label3;
    }
}
