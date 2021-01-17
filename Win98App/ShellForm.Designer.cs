
namespace Win98App
{
    partial class ShellForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellForm));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HomeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UUIDGeneratorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlatUIColorPickerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineSorterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolsMenuItem,
            this.HelpMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HomeMenuItem,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // HomeMenuItem
            // 
            this.HomeMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("HomeMenuItem.Image")));
            this.HomeMenuItem.Name = "HomeMenuItem";
            this.HomeMenuItem.Size = new System.Drawing.Size(107, 22);
            this.HomeMenuItem.Text = "&Home";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExitMenuItem.Image")));
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(107, 22);
            this.ExitMenuItem.Text = "E&xit";
            // 
            // ToolsMenuItem
            // 
            this.ToolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UUIDGeneratorMenuItem,
            this.FlatUIColorPickerMenuItem,
            this.LineSorterMenuItem});
            this.ToolsMenuItem.Name = "ToolsMenuItem";
            this.ToolsMenuItem.Size = new System.Drawing.Size(46, 20);
            this.ToolsMenuItem.Text = "&Tools";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LogMenuItem,
            this.AboutMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpMenuItem.Text = "&Help";
            // 
            // LogMenuItem
            // 
            this.LogMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LogMenuItem.Image")));
            this.LogMenuItem.Name = "LogMenuItem";
            this.LogMenuItem.Size = new System.Drawing.Size(107, 22);
            this.LogMenuItem.Text = "&Log";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AboutMenuItem.Image")));
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(107, 22);
            this.AboutMenuItem.Text = "&About";
            // 
            // UUIDGeneratorMenuItem
            // 
            this.UUIDGeneratorMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("UUIDGeneratorMenuItem.Image")));
            this.UUIDGeneratorMenuItem.Name = "UUIDGeneratorMenuItem";
            this.UUIDGeneratorMenuItem.Size = new System.Drawing.Size(180, 22);
            this.UUIDGeneratorMenuItem.Text = "&UUID Generator";
            // 
            // FlatUIColorPickerMenuItem
            // 
            this.FlatUIColorPickerMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("FlatUIColorPickerMenuItem.Image")));
            this.FlatUIColorPickerMenuItem.Name = "FlatUIColorPickerMenuItem";
            this.FlatUIColorPickerMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FlatUIColorPickerMenuItem.Text = "Flat UI &Color Picker";
            // 
            // LineSorterMenuItem
            // 
            this.LineSorterMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LineSorterMenuItem.Image")));
            this.LineSorterMenuItem.Name = "LineSorterMenuItem";
            this.LineSorterMenuItem.Size = new System.Drawing.Size(180, 22);
            this.LineSorterMenuItem.Text = "Line Sorter";
            // 
            // ShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShellForm";
            this.Text = "Form1";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HomeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UUIDGeneratorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FlatUIColorPickerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LineSorterMenuItem;
    }
}

