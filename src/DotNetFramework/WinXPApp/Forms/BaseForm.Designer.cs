
namespace WinXPApp.Forms
{
    partial class BaseForm
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
            this.components = new System.ComponentModel.Container();
            this.BaseMetroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BaseMetroStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // BaseMetroStyleManager
            // 
            this.BaseMetroStyleManager.Owner = this;
            this.BaseMetroStyleManager.Style = MetroFramework.MetroColorStyle.Green;
            this.BaseMetroStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.BaseMetroStyleManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal MetroFramework.Components.MetroStyleManager BaseMetroStyleManager;
    }
}