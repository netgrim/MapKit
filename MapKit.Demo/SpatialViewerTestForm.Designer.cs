namespace MapKit.Demo
{
	partial class SpatialViewerTestForm
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripMenuItem mnuFile;
            System.Windows.Forms.ToolStripMenuItem mnuEdit;
            System.Windows.Forms.ToolStripMenuItem mnuView;
            System.Windows.Forms.ToolStripMenuItem mnuTools;
            this.mnuFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileLine2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoRender = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRenderQuality = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRenderQualityHigh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRenderQualityLow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRenderShowGeomMbr = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRenderShowQueryMBR = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRenderSmallQueryWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoTune = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.triStateTreeView1 = new Cyrez.UI.TriStateTreeView();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.viewport1 = new Cyrez.Graphics.Control.Viewport();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.sqliteOpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.recentFilesManager = new Cyrez.RecentFilesManager();
            this.cmnLayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnZoomLayerExtents = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.positionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFrame = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFrameNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reopenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeEditorComponent = new MapKit.UI.ThemeEditorComponent();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            mnuView = new System.Windows.Forms.ToolStripMenuItem();
            mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.cmnLayer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileLoad,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            toolStripSeparator1,
            this.mnuRecentFiles,
            this.menuFileLine2,
            this.mnuFileExit});
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new System.Drawing.Size(37, 20);
            mnuFile.Text = "File";
            // 
            // mnuFileLoad
            // 
            this.mnuFileLoad.Name = "mnuFileLoad";
            this.mnuFileLoad.Size = new System.Drawing.Size(136, 22);
            this.mnuFileLoad.Text = "Load";
            this.mnuFileLoad.Click += new System.EventHandler(this.menuFileLoad_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(136, 22);
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(136, 22);
            this.mnuFileSaveAs.Text = "Save As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // mnuRecentFiles
            // 
            this.mnuRecentFiles.Enabled = false;
            this.mnuRecentFiles.Name = "mnuRecentFiles";
            this.mnuRecentFiles.Size = new System.Drawing.Size(136, 22);
            this.mnuRecentFiles.Text = "Recent Files";
            // 
            // menuFileLine2
            // 
            this.menuFileLine2.Name = "menuFileLine2";
            this.menuFileLine2.Size = new System.Drawing.Size(133, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(136, 22);
            this.mnuFileExit.Text = "Exit";
            // 
            // mnuEdit
            // 
            mnuEdit.Name = "mnuEdit";
            mnuEdit.Size = new System.Drawing.Size(39, 20);
            mnuEdit.Text = "Edit";
            // 
            // mnuView
            // 
            mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutoRender,
            this.toolStripSeparator2,
            this.mnuRenderQuality,
            this.mnuRenderShowGeomMbr,
            this.mnuRenderShowQueryMBR,
            this.mnuRenderSmallQueryWindow,
            this.toolStripMenuItem1,
            this.mnuRefresh});
            mnuView.Name = "mnuView";
            mnuView.Size = new System.Drawing.Size(44, 20);
            mnuView.Text = "View";
            // 
            // mnuAutoRender
            // 
            this.mnuAutoRender.CheckOnClick = true;
            this.mnuAutoRender.Name = "mnuAutoRender";
            this.mnuAutoRender.Size = new System.Drawing.Size(186, 22);
            this.mnuAutoRender.Text = "Auto-Render";
            this.mnuAutoRender.Click += new System.EventHandler(this.mnuAutoRender_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // mnuRenderQuality
            // 
            this.mnuRenderQuality.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRenderQualityHigh,
            this.mnuRenderQualityLow});
            this.mnuRenderQuality.Name = "mnuRenderQuality";
            this.mnuRenderQuality.Size = new System.Drawing.Size(186, 22);
            this.mnuRenderQuality.Text = "Quality";
            // 
            // mnuRenderQualityHigh
            // 
            this.mnuRenderQualityHigh.Name = "mnuRenderQualityHigh";
            this.mnuRenderQualityHigh.Size = new System.Drawing.Size(100, 22);
            this.mnuRenderQualityHigh.Text = "High";
            // 
            // mnuRenderQualityLow
            // 
            this.mnuRenderQualityLow.Name = "mnuRenderQualityLow";
            this.mnuRenderQualityLow.Size = new System.Drawing.Size(100, 22);
            this.mnuRenderQualityLow.Text = "Low";
            // 
            // mnuRenderShowGeomMbr
            // 
            this.mnuRenderShowGeomMbr.CheckOnClick = true;
            this.mnuRenderShowGeomMbr.Name = "mnuRenderShowGeomMbr";
            this.mnuRenderShowGeomMbr.Size = new System.Drawing.Size(186, 22);
            this.mnuRenderShowGeomMbr.Text = "Show Geometry MBR";
            this.mnuRenderShowGeomMbr.Click += new System.EventHandler(this.mnuRenderShowGeomMbr_Click);
            // 
            // mnuRenderShowQueryMBR
            // 
            this.mnuRenderShowQueryMBR.CheckOnClick = true;
            this.mnuRenderShowQueryMBR.Name = "mnuRenderShowQueryMBR";
            this.mnuRenderShowQueryMBR.Size = new System.Drawing.Size(186, 22);
            this.mnuRenderShowQueryMBR.Text = "Show Query MBR";
            this.mnuRenderShowQueryMBR.Click += new System.EventHandler(this.mnuRenderShowQueryMBR_Click);
            // 
            // mnuRenderSmallQueryWindow
            // 
            this.mnuRenderSmallQueryWindow.CheckOnClick = true;
            this.mnuRenderSmallQueryWindow.Name = "mnuRenderSmallQueryWindow";
            this.mnuRenderSmallQueryWindow.Size = new System.Drawing.Size(186, 22);
            this.mnuRenderSmallQueryWindow.Text = "Small Query Window";
            this.mnuRenderSmallQueryWindow.Click += new System.EventHandler(this.mnuRendererSmallQueryWindow_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.Name = "mnuRefresh";
            this.mnuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuRefresh.Size = new System.Drawing.Size(186, 22);
            this.mnuRefresh.Text = "Refresh";
            this.mnuRefresh.Click += new System.EventHandler(this.mnuRefresh_Click);
            // 
            // mnuTools
            // 
            mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStatistics,
            this.mnuAutoTune});
            mnuTools.Name = "mnuTools";
            mnuTools.Size = new System.Drawing.Size(48, 20);
            mnuTools.Text = "Tools";
            // 
            // mnuStatistics
            // 
            this.mnuStatistics.Name = "mnuStatistics";
            this.mnuStatistics.Size = new System.Drawing.Size(158, 22);
            this.mnuStatistics.Text = "Gather Statistics";
            this.mnuStatistics.Click += new System.EventHandler(this.mnuStatistics_Click);
            // 
            // mnuAutoTune
            // 
            this.mnuAutoTune.Name = "mnuAutoTune";
            this.mnuAutoTune.Size = new System.Drawing.Size(158, 22);
            this.mnuAutoTune.Text = "Auto-Tune";
            this.mnuAutoTune.Click += new System.EventHandler(this.mnuAutoTune_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.viewport1);
            this.splitContainer1.Size = new System.Drawing.Size(552, 270);
            this.splitContainer1.SplitterDistance = 184;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.triStateTreeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer2.Size = new System.Drawing.Size(184, 270);
            this.splitContainer2.SplitterDistance = 124;
            this.splitContainer2.TabIndex = 1;
            // 
            // triStateTreeView1
            // 
            this.triStateTreeView1.CascadeState = false;
            this.triStateTreeView1.CheckBoxes = true;
            this.triStateTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triStateTreeView1.LabelEdit = true;
            this.triStateTreeView1.Location = new System.Drawing.Point(0, 0);
            this.triStateTreeView1.Name = "triStateTreeView1";
            this.triStateTreeView1.Size = new System.Drawing.Size(184, 124);
            this.triStateTreeView1.TabIndex = 0;
            this.triStateTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.triStateTreeView1_AfterSelect);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(184, 142);
            this.propertyGrid.TabIndex = 0;
            // 
            // viewport1
            // 
            this.viewport1.Angle = 0D;
            this.viewport1.DebugDrawAxies = true;
            this.viewport1.DebugDrawGrid = true;
            this.viewport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewport1.Location = new System.Drawing.Point(0, 0);
            this.viewport1.MaxZoom = 200D;
            this.viewport1.MinZoom = 0.001D;
            this.viewport1.Name = "viewport1";
            this.viewport1.Size = new System.Drawing.Size(364, 270);
            this.viewport1.TabIndex = 0;
            this.viewport1.Zoom = 1D;
            this.viewport1.ZoomFactor = 1.2F;
            this.viewport1.WindowChanged += new System.EventHandler<Cyrez.Graphics.Control.WindowChangedEventArgs>(this.viewport1_WindowChanged);
            this.viewport1.Paint += new System.Windows.Forms.PaintEventHandler(this.viewport1_Paint);
            this.viewport1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.viewport1_KeyDown);
            this.viewport1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.viewport1_MouseMove);
            this.viewport1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.viewport1_MouseUp);
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(552, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuFile,
            mnuEdit,
            mnuView,
            mnuTools,
            this.optionsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(552, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // sqliteOpenFileDlg
            // 
            this.sqliteOpenFileDlg.Filter = "Sqlite database (*.db,*.sqlite)|*.db;*.sqlite";
            // 
            // recentFilesManager
            // 
            this.recentFilesManager.MaxRecentFiles = 10;
            this.recentFilesManager.menu = this.mnuRecentFiles;
            this.recentFilesManager.RecentFiles = global::MapKit.Demo.Properties.Settings.Default.RecentFiles;
            this.recentFilesManager.RecentFileClick += new System.EventHandler<Cyrez.FileEventArgs>(this.recentFilesManager_RecentFileClick);
            // 
            // cmnLayer
            // 
            this.cmnLayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnZoomLayerExtents});
            this.cmnLayer.Name = "contextMenuStrip1";
            this.cmnLayer.Size = new System.Drawing.Size(161, 26);
            // 
            // cmnZoomLayerExtents
            // 
            this.cmnZoomLayerExtents.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.cmnZoomLayerExtents.MergeIndex = 0;
            this.cmnZoomLayerExtents.Name = "cmnZoomLayerExtents";
            this.cmnZoomLayerExtents.Size = new System.Drawing.Size(160, 22);
            this.cmnZoomLayerExtents.Text = "Zoom to Extents";
            this.cmnZoomLayerExtents.Click += new System.EventHandler(this.cmnZoomLayerExtents_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "Theme.xml";
            this.saveFileDialog.Filter = "Configuration Theme (*.xml)|*.xml|Tous (*.*)|*.*";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "Theme.xml";
            this.openFileDialog.Filter = "Configuration Theme (*.xml)|*.xml|Tous (*.*)|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.positionLabel,
            this.lblFrame,
            this.lblFrameNumber,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 319);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(552, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(463, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // positionLabel
            // 
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(21, 17);
            this.positionLabel.Text = "XY";
            // 
            // lblFrame
            // 
            this.lblFrame.Name = "lblFrame";
            this.lblFrame.Size = new System.Drawing.Size(40, 17);
            this.lblFrame.Text = "Frame";
            // 
            // lblFrameNumber
            // 
            this.lblFrameNumber.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.lblFrameNumber.Name = "lblFrameNumber";
            this.lblFrameNumber.Size = new System.Drawing.Size(13, 17);
            this.lblFrameNumber.Text = "0";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reopenToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // reopenToolStripMenuItem
            // 
            this.reopenToolStripMenuItem.CheckOnClick = true;
            this.reopenToolStripMenuItem.Name = "reopenToolStripMenuItem";
            this.reopenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reopenToolStripMenuItem.Text = "Reopen";
            this.reopenToolStripMenuItem.Click += new System.EventHandler(this.reopenToolStripMenuItem_Click);
            // 
            // themeEditorComponent
            // 
            this.themeEditorComponent.Map = null;
            this.themeEditorComponent.PropertyGrid = this.propertyGrid;
            this.themeEditorComponent.TreeView = this.triStateTreeView1;
            // 
            // SpatialViewerTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 341);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "SpatialViewerTestForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.cmnLayer.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private Cyrez.Graphics.Control.Viewport viewport1;
		private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
		private System.Windows.Forms.ToolStripMenuItem mnuRecentFiles;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
		private System.Windows.Forms.ToolStripMenuItem mnuFileLoad;
		private Cyrez.RecentFilesManager recentFilesManager;
		private System.Windows.Forms.ToolStripSeparator menuFileLine2;
		private System.Windows.Forms.OpenFileDialog sqliteOpenFileDlg;
		private Cyrez.UI.TriStateTreeView triStateTreeView1;
		private MapKit.UI.ThemeEditorComponent themeEditorComponent;
        private System.Windows.Forms.ContextMenuStrip cmnLayer;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel positionLabel;
		private System.Windows.Forms.ToolStripMenuItem mnuRenderQuality;
		private System.Windows.Forms.ToolStripMenuItem mnuRenderQualityHigh;
		private System.Windows.Forms.ToolStripMenuItem mnuRenderQualityLow;
		private System.Windows.Forms.ToolStripMenuItem mnuRenderShowGeomMbr;
		private System.Windows.Forms.ToolStripMenuItem mnuRenderShowQueryMBR;
        private System.Windows.Forms.ToolStripMenuItem mnuRenderSmallQueryWindow;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripStatusLabel lblFrame;
		private System.Windows.Forms.ToolStripStatusLabel lblFrameNumber;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.ToolStripMenuItem mnuStatistics;
		private System.Windows.Forms.ToolStripMenuItem mnuAutoTune;
		private System.Windows.Forms.ToolStripMenuItem cmnZoomLayerExtents;
        private System.Windows.Forms.ToolStripMenuItem mnuAutoRender;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reopenToolStripMenuItem;
	}
}

