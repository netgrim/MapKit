namespace MapKit.UI
{
	partial class ThemeEditorComponent
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
            this.components = new System.ComponentModel.Container();
            this.cmnLayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnLayerAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnLayerSep = new System.Windows.Forms.ToolStripSeparator();
            this.cmnLayerRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnLayerPropertiesSep = new System.Windows.Forms.ToolStripSeparator();
            this.cmnLayerProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewReorderHandler = new Cyrez.UI.TreeViewReorderHandler();
            this.cmnThemeNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnThemeNodeAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnThemeNodeExpandSep = new System.Windows.Forms.ToolStripSeparator();
            this.cmnThemeNodeExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnThemeNodeCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnThemeNodeRemoveSep = new System.Windows.Forms.ToolStripSeparator();
            this.cmnThemeNodeRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnThemeNodeRenameSep = new System.Windows.Forms.ToolStripSeparator();
            this.cmnThemeNodeRename = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnThemeNodeProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnLayer.SuspendLayout();
            this.cmnThemeNode.SuspendLayout();
            // 
            // cmnLayer
            // 
            this.cmnLayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnLayerAdd,
            this.cmnLayerSep,
            this.cmnLayerRemove,
            this.cmnLayerPropertiesSep,
            this.cmnLayerProperties});
            this.cmnLayer.Name = "contextMenuStrip1";
            this.cmnLayer.Size = new System.Drawing.Size(142, 54);
            // 
            // cmnLayerAdd
            // 
            this.cmnLayerAdd.MergeIndex = 1;
            this.cmnLayerAdd.Name = "cmnLayerAdd";
            this.cmnLayerAdd.Size = new System.Drawing.Size(142, 54);
            this.cmnLayerAdd.Text = "Add";
            // 
            // cmnLayerSep
            // 
            this.cmnLayerSep.MergeIndex = 2;
            this.cmnLayerSep.Name = "cmnLayerSep";
            this.cmnLayerSep.Size = new System.Drawing.Size(142, 54);
            // 
            // cmnLayerRemove
            // 
            this.cmnLayerRemove.Name = "cmnLayerRemove";
            this.cmnLayerRemove.Size = new System.Drawing.Size(141, 22);
            this.cmnLayerRemove.Text = "Remove";
            // 
            // cmnLayerPropertiesSep
            // 
            this.cmnLayerPropertiesSep.MergeIndex = 4;
            this.cmnLayerPropertiesSep.Name = "cmnLayerPropertiesSep";
            this.cmnLayerPropertiesSep.Size = new System.Drawing.Size(138, 6);
            // 
            // cmnLayerProperties
            // 
            this.cmnLayerProperties.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cmnLayerProperties.MergeIndex = 5;
            this.cmnLayerProperties.Name = "cmnLayerProperties";
            this.cmnLayerProperties.Size = new System.Drawing.Size(141, 22);
            this.cmnLayerProperties.Text = "Properties...";
            // 
            // treeViewReorderHandler
            // 
            this.treeViewReorderHandler.DragDropLineColor = System.Drawing.Color.Black;
            this.treeViewReorderHandler.DragDropLineWidth = 2F;
            this.treeViewReorderHandler.TreeView = null;
            this.treeViewReorderHandler.NodeMoved += new System.EventHandler<Cyrez.UI.NodeMovedEventArgs>(this.treeViewReorderHandler_NodeMoved);
            this.treeViewReorderHandler.DropNodeValidating += new System.EventHandler<Cyrez.UI.DropNodeValidatingEventArgs>(this.treeViewReorderHandler_DropNodeValidating);
            // 
            // cmnThemeNode
            // 
            this.cmnThemeNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnThemeNodeAdd,
            this.cmnThemeNodeExpandSep,
            this.cmnThemeNodeExpand,
            this.cmnThemeNodeCollapse,
            this.cmnThemeNodeRemoveSep,
            this.cmnThemeNodeRemove,
            this.cmnThemeNodeRenameSep,
            this.cmnThemeNodeRename,
            this.cmnThemeNodeProperties});
            this.cmnThemeNode.Name = "contextMenuStrip1";
            this.cmnThemeNode.Size = new System.Drawing.Size(137, 154);
            // 
            // cmnThemeNodeAdd
            // 
            this.cmnThemeNodeAdd.Name = "cmnThemeNodeAdd";
            this.cmnThemeNodeAdd.Size = new System.Drawing.Size(136, 22);
            this.cmnThemeNodeAdd.Text = "Add...";
            // 
            // cmnThemeNodeExpandSep
            // 
            this.cmnThemeNodeExpandSep.Name = "cmnThemeNodeExpandSep";
            this.cmnThemeNodeExpandSep.Size = new System.Drawing.Size(133, 6);
            // 
            // cmnThemeNodeExpand
            // 
            this.cmnThemeNodeExpand.Name = "cmnThemeNodeExpand";
            this.cmnThemeNodeExpand.Size = new System.Drawing.Size(136, 22);
            this.cmnThemeNodeExpand.Text = "Expand all";
            // 
            // cmnThemeNodeCollapse
            // 
            this.cmnThemeNodeCollapse.Name = "cmnThemeNodeCollapse";
            this.cmnThemeNodeCollapse.Size = new System.Drawing.Size(136, 22);
            this.cmnThemeNodeCollapse.Text = "Collapse All";
            // 
            // cmnThemeNodeRemoveSep
            // 
            this.cmnThemeNodeRemoveSep.Name = "cmnThemeNodeRemoveSep";
            this.cmnThemeNodeRemoveSep.Size = new System.Drawing.Size(133, 6);
            // 
            // cmnThemeNodeRemove
            // 
            this.cmnThemeNodeRemove.Name = "cmnThemeNodeRemove";
            this.cmnThemeNodeRemove.Size = new System.Drawing.Size(136, 22);
            this.cmnThemeNodeRemove.Text = "Remove";
            // 
            // cmnThemeNodeRenameSep
            // 
            this.cmnThemeNodeRenameSep.Name = "cmnThemeNodeRenameSep";
            this.cmnThemeNodeRenameSep.Size = new System.Drawing.Size(133, 6);
            // 
            // cmnThemeNodeRename
            // 
            this.cmnThemeNodeRename.Name = "cmnThemeNodeRename";
            this.cmnThemeNodeRename.Size = new System.Drawing.Size(136, 22);
            this.cmnThemeNodeRename.Text = "Rename";
            // 
            // cmnThemeNodeProperties
            // 
            this.cmnThemeNodeProperties.Name = "cmnThemeNodeProperties";
            this.cmnThemeNodeProperties.Size = new System.Drawing.Size(136, 22);
            this.cmnThemeNodeProperties.Text = "Properties...";
            this.cmnLayer.ResumeLayout(false);
            this.cmnThemeNode.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.ContextMenuStrip cmnLayer;
        private System.Windows.Forms.ToolStripMenuItem cmnLayerAdd;
        private System.Windows.Forms.ToolStripSeparator cmnLayerSep;
		private System.Windows.Forms.ToolStripMenuItem cmnLayerRemove;
		private System.Windows.Forms.ToolStripSeparator cmnLayerPropertiesSep;
        private System.Windows.Forms.ToolStripMenuItem cmnLayerProperties;
        private Cyrez.UI.TreeViewReorderHandler treeViewReorderHandler;
        private System.Windows.Forms.ContextMenuStrip cmnThemeNode;
        private System.Windows.Forms.ToolStripMenuItem cmnThemeNodeAdd;
        private System.Windows.Forms.ToolStripSeparator cmnThemeNodeRemoveSep;
        private System.Windows.Forms.ToolStripMenuItem cmnThemeNodeRemove;
        private System.Windows.Forms.ToolStripSeparator cmnThemeNodeExpandSep;
        private System.Windows.Forms.ToolStripMenuItem cmnThemeNodeExpand;
        private System.Windows.Forms.ToolStripMenuItem cmnThemeNodeCollapse;
        private System.Windows.Forms.ToolStripSeparator cmnThemeNodeRenameSep;
        private System.Windows.Forms.ToolStripMenuItem cmnThemeNodeRename;
        private System.Windows.Forms.ToolStripMenuItem cmnThemeNodeProperties;
    }
}
