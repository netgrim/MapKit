using System.Windows.Forms;
using MapKit.Core;
using Cyrez.UI;
namespace MapKit.UI
{
    partial class ThemeEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
		private PropertyGrid propertyGrid;
		private ThemeEditorComponent themeEditorComponent;
		private TriStateTreeView treeView;
		private SplitContainer splitContainer1;

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
			this.treeView = new TriStateTreeView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.themeEditorComponent = new MapKit.UI.ThemeEditorComponent();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.LabelEdit = true;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(195, 400);
			this.treeView.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
			this.splitContainer1.Size = new System.Drawing.Size(585, 400);
			this.splitContainer1.SplitterDistance = 195;
			this.splitContainer1.TabIndex = 1;
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(386, 400);
			this.propertyGrid.TabIndex = 0;
			// 
			// themeEditorComponent
			// 
			this.themeEditorComponent.PropertyGrid = this.propertyGrid;
			this.themeEditorComponent.TreeView = this.treeView;
			// 
			// ThemeEditor
			// 
			this.Controls.Add(this.splitContainer1);
			this.Name = "ThemeEditor";
			this.Size = new System.Drawing.Size(585, 400);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        #endregion


    }
}
