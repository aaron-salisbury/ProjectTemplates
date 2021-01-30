
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HomeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UUIDGeneratorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlatUIColorPickerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineSorterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainContentPanel = new System.Windows.Forms.Panel();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolsMenuItem,
            this.HelpMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(759, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
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
            this.HomeMenuItem.Click += new System.EventHandler(this.HomeMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExitMenuItem.Image")));
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(107, 22);
            this.ExitMenuItem.Text = "E&xit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
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
            // UUIDGeneratorMenuItem
            // 
            this.UUIDGeneratorMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("UUIDGeneratorMenuItem.Image")));
            this.UUIDGeneratorMenuItem.Name = "UUIDGeneratorMenuItem";
            this.UUIDGeneratorMenuItem.Size = new System.Drawing.Size(174, 22);
            this.UUIDGeneratorMenuItem.Text = "&UUID Generator";
            this.UUIDGeneratorMenuItem.Click += new System.EventHandler(this.UUIDGeneratorMenuItem_Click);
            // 
            // FlatUIColorPickerMenuItem
            // 
            this.FlatUIColorPickerMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("FlatUIColorPickerMenuItem.Image")));
            this.FlatUIColorPickerMenuItem.Name = "FlatUIColorPickerMenuItem";
            this.FlatUIColorPickerMenuItem.Size = new System.Drawing.Size(174, 22);
            this.FlatUIColorPickerMenuItem.Text = "Flat UI &Color Picker";
            this.FlatUIColorPickerMenuItem.Click += new System.EventHandler(this.FlatUIColorPickerMenuItem_Click);
            // 
            // LineSorterMenuItem
            // 
            this.LineSorterMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LineSorterMenuItem.Image")));
            this.LineSorterMenuItem.Name = "LineSorterMenuItem";
            this.LineSorterMenuItem.Size = new System.Drawing.Size(174, 22);
            this.LineSorterMenuItem.Text = "Line Sorter";
            this.LineSorterMenuItem.Click += new System.EventHandler(this.LineSorterMenuItem_Click);
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
            this.LogMenuItem.Size = new System.Drawing.Size(180, 22);
            this.LogMenuItem.Text = "&Log";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AboutMenuItem.Image")));
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AboutMenuItem.Text = "&About";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // MainContentPanel
            // 
            this.MainContentPanel.AutoSize = true;
            this.MainContentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContentPanel.Location = new System.Drawing.Point(0, 24);
            this.MainContentPanel.Name = "MainContentPanel";
            this.MainContentPanel.Size = new System.Drawing.Size(759, 387);
            this.MainContentPanel.TabIndex = 1;
            // 
            // ShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 411);
            this.Controls.Add(this.MainContentPanel);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(365, 365);
            this.Name = "ShellForm";
            this.Text = "Form1";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
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
        private System.Windows.Forms.Panel MainContentPanel;
    }
}

