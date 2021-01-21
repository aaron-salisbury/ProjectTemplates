﻿
namespace WinXPApp.Forms.SampleTools
{
    partial class ToolsNavigatorUC
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
            this.ToolsNavigator = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.UUIDGeneratorPage = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.FlatUIColorPickerPage = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.LineSorterPage = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.uuidGeneratorUC1 = new WinXPApp.Forms.UUIDGeneratorUC();
            this.flatUIColorPickerUC1 = new WinXPApp.Forms.SampleTools.FlatUIColorPickerUC();
            this.lineSorterUC1 = new WinXPApp.Forms.SampleTools.LineSorterUC();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsNavigator)).BeginInit();
            this.ToolsNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UUIDGeneratorPage)).BeginInit();
            this.UUIDGeneratorPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatUIColorPickerPage)).BeginInit();
            this.FlatUIColorPickerPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineSorterPage)).BeginInit();
            this.LineSorterPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolsNavigator
            // 
            this.ToolsNavigator.AllowPageReorder = false;
            this.ToolsNavigator.Bar.BarOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.ToolsNavigator.Bar.ItemOrientation = ComponentFactory.Krypton.Toolkit.ButtonOrientation.FixedTop;
            this.ToolsNavigator.Bar.TabBorderStyle = ComponentFactory.Krypton.Toolkit.TabBorderStyle.SquareEqualMedium;
            this.ToolsNavigator.Bar.TabStyle = ComponentFactory.Krypton.Toolkit.TabStyle.StandardProfile;
            this.ToolsNavigator.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.ToolsNavigator.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.ToolsNavigator.Button.NextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.ToolsNavigator.CausesValidation = false;
            this.ToolsNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolsNavigator.Group.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonNavigatorStack;
            this.ToolsNavigator.Location = new System.Drawing.Point(0, 0);
            this.ToolsNavigator.Name = "ToolsNavigator";
            this.ToolsNavigator.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.BarRibbonTabGroup;
            this.ToolsNavigator.Outlook.ItemOrientation = ComponentFactory.Krypton.Toolkit.ButtonOrientation.FixedTop;
            this.ToolsNavigator.PageBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.ToolsNavigator.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.UUIDGeneratorPage,
            this.FlatUIColorPickerPage,
            this.LineSorterPage});
            this.ToolsNavigator.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.ToolsNavigator.Panel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.FormMain;
            this.ToolsNavigator.SelectedIndex = 1;
            this.ToolsNavigator.Size = new System.Drawing.Size(1109, 680);
            this.ToolsNavigator.Stack.BorderEdgeStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonNavigatorStack;
            this.ToolsNavigator.Stack.ItemOrientation = ComponentFactory.Krypton.Toolkit.ButtonOrientation.FixedTop;
            this.ToolsNavigator.Stack.StackAlignment = ComponentFactory.Krypton.Toolkit.RelativePositionAlign.Near;
            this.ToolsNavigator.StateDisabled.HeaderGroup.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateDisabled.HeaderGroup.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left;
            this.ToolsNavigator.StateDisabled.Page.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateDisabled.Panel.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateNormal.HeaderGroup.Back.Color1 = System.Drawing.Color.Transparent;
            this.ToolsNavigator.StateNormal.HeaderGroup.Back.Color2 = System.Drawing.Color.Transparent;
            this.ToolsNavigator.StateNormal.HeaderGroup.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.ToolsNavigator.StateNormal.HeaderGroup.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.ToolsNavigator.StateNormal.HeaderGroup.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left;
            this.ToolsNavigator.StateNormal.HeaderGroup.HeaderBar.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateNormal.HeaderGroup.HeaderOverflow.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateNormal.HeaderGroup.HeaderOverflow.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateNormal.HeaderGroup.HeaderOverflow.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.ToolsNavigator.StateNormal.HeaderGroup.HeaderPrimary.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.ToolsNavigator.StateNormal.HeaderGroup.HeaderSecondary.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateNormal.OverflowButton.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.ToolsNavigator.StateNormal.Page.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateNormal.Panel.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateSelected.Tab.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.ToolsNavigator.StateSelected.Tab.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.ToolsNavigator.TabIndex = 0;
            this.ToolsNavigator.Text = "kryptonNavigator1";
            // 
            // UUIDGeneratorPage
            // 
            this.UUIDGeneratorPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.UUIDGeneratorPage.Controls.Add(this.uuidGeneratorUC1);
            this.UUIDGeneratorPage.Flags = 65534;
            this.UUIDGeneratorPage.LastVisibleSet = true;
            this.UUIDGeneratorPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.UUIDGeneratorPage.Name = "UUIDGeneratorPage";
            this.UUIDGeneratorPage.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.UUIDGeneratorPage.Size = new System.Drawing.Size(987, 680);
            this.UUIDGeneratorPage.StateCommon.RibbonTab.Content.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.UUIDGeneratorPage.Text = "UUID Generator";
            this.UUIDGeneratorPage.ToolTipTitle = "Page ToolTip";
            this.UUIDGeneratorPage.UniqueName = "10D913FD55EF459B2F9B9133D967B6AD";
            // 
            // FlatUIColorPickerPage
            // 
            this.FlatUIColorPickerPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.FlatUIColorPickerPage.Controls.Add(this.flatUIColorPickerUC1);
            this.FlatUIColorPickerPage.Flags = 65534;
            this.FlatUIColorPickerPage.LastVisibleSet = true;
            this.FlatUIColorPickerPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.FlatUIColorPickerPage.Name = "FlatUIColorPickerPage";
            this.FlatUIColorPickerPage.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.FlatUIColorPickerPage.Size = new System.Drawing.Size(987, 680);
            this.FlatUIColorPickerPage.StateSelected.Tab.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.FlatUIColorPickerPage.StateSelected.Tab.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.FlatUIColorPickerPage.Text = "Flat UI Color Picker";
            this.FlatUIColorPickerPage.ToolTipTitle = "Page ToolTip";
            this.FlatUIColorPickerPage.UniqueName = "55602C67A2E2493E938EE943118A6448";
            // 
            // LineSorterPage
            // 
            this.LineSorterPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.LineSorterPage.Controls.Add(this.lineSorterUC1);
            this.LineSorterPage.Flags = 65534;
            this.LineSorterPage.LastVisibleSet = true;
            this.LineSorterPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.LineSorterPage.Name = "LineSorterPage";
            this.LineSorterPage.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.LineSorterPage.Size = new System.Drawing.Size(987, 680);
            this.LineSorterPage.StateNormal.HeaderGroup.HeaderSecondary.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.LineSorterPage.StateSelected.MiniButton.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.LineSorterPage.StateSelected.OverflowButton.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.LineSorterPage.Text = "Line Sorter";
            this.LineSorterPage.ToolTipTitle = "Page ToolTip";
            this.LineSorterPage.UniqueName = "92573B42E81B4600738615C6F4B69199";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(100, 100);
            this.kryptonPage1.Text = "kryptonPage1";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "9E4C4B1A80474180C9BA9362816BDD43";
            // 
            // uuidGeneratorUC1
            // 
            this.uuidGeneratorUC1.BackColor = System.Drawing.Color.Transparent;
            this.uuidGeneratorUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uuidGeneratorUC1.Location = new System.Drawing.Point(20, 10);
            this.uuidGeneratorUC1.Name = "uuidGeneratorUC1";
            this.uuidGeneratorUC1.Size = new System.Drawing.Size(967, 670);
            this.uuidGeneratorUC1.TabIndex = 0;
            // 
            // flatUIColorPickerUC1
            // 
            this.flatUIColorPickerUC1.AutoSize = true;
            this.flatUIColorPickerUC1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flatUIColorPickerUC1.BackColor = System.Drawing.Color.Transparent;
            this.flatUIColorPickerUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flatUIColorPickerUC1.Location = new System.Drawing.Point(20, 10);
            this.flatUIColorPickerUC1.Name = "flatUIColorPickerUC1";
            this.flatUIColorPickerUC1.Size = new System.Drawing.Size(967, 670);
            this.flatUIColorPickerUC1.TabIndex = 0;
            // 
            // lineSorterUC1
            // 
            this.lineSorterUC1.BackColor = System.Drawing.Color.Transparent;
            this.lineSorterUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineSorterUC1.Location = new System.Drawing.Point(20, 10);
            this.lineSorterUC1.Margin = new System.Windows.Forms.Padding(50);
            this.lineSorterUC1.Name = "lineSorterUC1";
            this.lineSorterUC1.Size = new System.Drawing.Size(967, 670);
            this.lineSorterUC1.SortTypes = null;
            this.lineSorterUC1.TabIndex = 0;
            // 
            // ToolsNavigatorUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ToolsNavigator);
            this.Name = "ToolsNavigatorUC";
            this.Size = new System.Drawing.Size(1109, 680);
            ((System.ComponentModel.ISupportInitialize)(this.ToolsNavigator)).EndInit();
            this.ToolsNavigator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UUIDGeneratorPage)).EndInit();
            this.UUIDGeneratorPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlatUIColorPickerPage)).EndInit();
            this.FlatUIColorPickerPage.ResumeLayout(false);
            this.FlatUIColorPickerPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineSorterPage)).EndInit();
            this.LineSorterPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator ToolsNavigator;
        private ComponentFactory.Krypton.Navigator.KryptonPage UUIDGeneratorPage;
        private ComponentFactory.Krypton.Navigator.KryptonPage FlatUIColorPickerPage;
        private ComponentFactory.Krypton.Navigator.KryptonPage LineSorterPage;
        private UUIDGeneratorUC uuidGeneratorUC1;
        private FlatUIColorPickerUC flatUIColorPickerUC1;
        private LineSorterUC lineSorterUC1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
    }
}
