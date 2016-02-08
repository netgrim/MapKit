using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;

using MapKit.Demo.Properties;
using Cyrez;
using Cyrez.Graphics;
using Cyrez.Graphics.Control;
using GeoAPI.Geometries;
using MapKit.Core;
using MapKit.Spatialite;

using WinPoint = System.Windows.Point;
using MatrixF = System.Drawing.Drawing2D.Matrix;
using System.IO;
using MapKit.Core.Rendering;

namespace MapKit.Demo
{
	partial class SpatialViewerTestForm : Form
	{
		private AutoResetEvent _rendererSignal;
		private Thread _renderThread;
		private string _fileName;
		private Map _map;
		private Renderer _renderer;
		private Image _frontBuffer;
		private Image _backBuffer;
		private RenderArgs _bgRenderArgs;
		private Matrix _imageTransform;
		private SelectionTool _selectionTool;
        private bool _mapHasChanges;
        private MatrixWindow _matrixWindow;

        public SpatialViewerTestForm()
		{
			InitializeComponent();

			_rendererSignal = new AutoResetEvent(false);
			_renderThread = new Thread(RenderThreadStart);
			_renderThread.Name = "RenderThread";
			_renderThread.IsBackground = true;
			_renderThread.Start();

			ToolStripManager.Merge(cmnLayer, themeEditorComponent.LayerContextMenu);

            LoadSettings();
            InitMap();

            themeEditorComponent.SetMap(_map);

			_selectionTool = new SelectionTool(this);
			viewport1.LeftButtonTool = _selectionTool;

            if ( Settings.Default.ShowMatrixWindow)
                ShowMatrixWindow();
		}

        private void LoadSettings()
        {
            AutoRender = Settings.Default.AutoRender;
            reopenToolStripMenuItem.Checked = Settings.Default.ReopenLastFile;
            showMatrixWindowToolStripMenuItem.Checked = Settings.Default.ShowMatrixWindow;
        }

        private void InitMap()
        {
            _map = new Map();
            _map.NodeTypes.Add(SpatialiteLayer.NodeType.ElementName, SpatialiteLayer.NodeType);
            _map.Changed += new EventHandler(map_Changed);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ProcessCommandLineArgs();
        }

        private void ProcessCommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
                OpenFile(args[1]);
            else if (Settings.Default.ReopenLastFile && File.Exists(Settings.Default.LastFile))
                OpenFile(Settings.Default.LastFile);

        }

		public string Status
		{
			get { return lblStatus.Text; }
			set { lblStatus.Text = value; }
		}

		public Map CurrentMap
		{
			get { return _map; }
		}
        
		private void cmnZoomLayerExtents_Click(object sender, EventArgs e)
		{
			try
			{
				var layer = themeEditorComponent.SelectedThemeNode as Layer;
                if (layer != null)
                {
                    var box = layer.BoundingBox;
                    if(!box.IsNull)
                        viewport1.ZoomWindow(ToBoundingBox(box));
                }
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}

        private Cyrez.Graphics.Geometry.BoundingBox ToBoundingBox(Envelope box)
        {
            return box.IsNull 
                ? new Cyrez.Graphics.Geometry.BoundingBox()
                : new Cyrez.Graphics.Geometry.BoundingBox(box.MinX, box.MinY, box.MaxX, box.MaxY);
        }

        private Envelope ToEnvelope(Cyrez.Graphics.Geometry.BoundingBox box)
        {
            return new Envelope(box.MinX, box.MaxX, box.MinY, box.MaxY);
        }

	    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.RecentFiles = recentFilesManager.RecentFiles;
			Settings.Default.Save();
			if (!PromptToSaveChanges(_map))
				e.Cancel = true;
			else
				_renderThread.Interrupt();
		}


        private void menuFileLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (!PromptToSaveChanges(_map)) return;

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    OpenFile(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

		private void menuLayerAddGroup_Click(object sender, EventArgs e)
		{
			themeEditorComponent.AddNewGroup(null);
		}

		private void mnuAutoTune_Click(object sender, EventArgs e)
		{
			TuneMap(_map);
		}

		private void mnuFileSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(_fileName))
				SaveAs();
			else
				SaveAs(_fileName);
		}

		private void mnuFileSaveAs_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void mnuRendererSmallQueryWindow_Click(object sender, EventArgs e)
		{
            if (_renderer != null)
            {
				_renderer.SmallQueryWindow = mnuRenderSmallQueryWindow.Checked;
				viewport1.Invalidate();
			}
		}

		private void mnuRenderShowGeomMbr_Click(object sender, EventArgs e)
		{
			_renderer.ShowGeometryMBR = mnuRenderShowGeomMbr.Checked;
			viewport1.Invalidate();
		}

		private void mnuRenderShowQueryMBR_Click(object sender, EventArgs e)
		{
            if (_renderer != null)
            {
			_renderer.ShowQueryMBR = mnuRenderShowQueryMBR.Checked;
			viewport1.Invalidate();
		}
		}

		private void mnuStatistics_Click(object sender, EventArgs e)
		{
            if (_renderer != null)
            {
			_renderer.GatherStatistics = true;
			using (Graphics g = viewport1.CreateGraphics())
			{
                _renderer.Translate = new Matrix();
				_renderer.BeginScene(g);
				_renderer.Render();
				_renderer.EndScene();
			}
			_renderer.GatherStatistics = false;
		}
            else
                MessageBox.Show("no renderer");
		}

		private void OpenFile(string filename)
		{
            _map.Changed -= new EventHandler(map_Changed);

            _map.ReadXml(filename);

            _map.Changed += new EventHandler(map_Changed);
            
			_renderer = new Renderer(_map);
			_renderer.ProgressChanged += Renderer_ProgressChanged;
			_renderer.Rendered += Render_Rendered;
			_renderer.LayerFailed += Renderer_LayerFailed;

			_fileName = filename;
			recentFilesManager.SetFileRecent(filename);

            //treeview
            themeEditorComponent.SetMap(_map);

			SetTitle(filename);
			viewport1.SetView(_map.CenterX, _map.CenterY, _map.Zoom, _map.Angle);

            if (Settings.Default.LastFile != filename)
            {
                Settings.Default.LastFile = filename;
                Settings.Default.Save();
            }

            mnuClose.Visible = true;
		}


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel)
                Settings.Default.Save();
        }

		void Renderer_LayerFailed(object sender, LayerFailedEventArgs e)
		{
            var ret = MessageBox.Show(string.Format("Rendering of layer '{0}' failed: {1}\r\nContinue?", e.Layer.NodePath, e.Exception.Message), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (ret == DialogResult.No)
                e.Cancel = true;
		}

		private bool PromptToSaveChanges(Map map)
		{
			if (!_mapHasChanges)
				return true;
			
			var result = MessageBox.Show("Save changes?", Application.ProductName, MessageBoxButtons.YesNoCancel);
			if (result == DialogResult.Yes)
				return Save();
			return (result == DialogResult.No);
		}

		private void recentFilesManager_RecentFileClick(object sender, FileEventArgs e)
		{
			try
			{
                if (!PromptToSaveChanges(_map)) return;
                OpenFile(e.Filename);
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}

		private void Render_Rendered(object sender, EventArgs e)
		{
			if (InvokeRequired)
				BeginInvoke(new EventHandler(Render_Rendered), sender, e );
			else
			{
				lblFrameNumber.Text = _renderer.FrameNumber.ToString();
				
                UpdateStatus(themeEditorComponent.SelectedThemeNode);
                propertyGrid.Refresh();
			}
		}

		private void Renderer_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (InvokeRequired)
				BeginInvoke(new ProgressChangedEventHandler(Renderer_ProgressChanged), sender, e);
			else
			{
				int newValue = e.ProgressPercentage;
				Debug.Assert(newValue <= 100);
				if (((toolStripProgressBar1.Value != newValue) && (newValue >= 0)) && (newValue < 100))
					toolStripProgressBar1.Value = newValue;
			}
		}

		public void RenderingComplete()
		{
			toolStripProgressBar1.Visible = false;
		}

		public void RenderingStarted()
		{
			toolStripProgressBar1.Visible = true;
		}

        private void RenderThreadStart()
        {
            while (_rendererSignal.WaitOne())
            {
                RenderArgs e;
                lock (this)
                {
                    e = _bgRenderArgs;
                    if (e == null)
                        continue;
                }

                var g = Graphics.FromImage(_backBuffer);

                g.Transform = Viewport.GetRotationScaleMatrix(e.Matrix);

                /*var invRotScale = e.Matrix;
                invRotScale.OffsetX = invRotScale.OffsetY = 0;
                invRotScale.Invert();

                var translate = e.Matrix * invRotScale;*/
                var translate = Viewport.GetTranslateMatrix(e.Matrix);

                BeginInvoke(new ThreadStart(RenderingStarted));

                if (_renderer != null)
                {
                    try
                    {
                        _map.Zoom = e.Scale;
                        _map.Angle = viewport1.Angle;
                        //_renderer.Angle = (float);
                        _renderer.Zoom = e.Scale;
                        _renderer.Window = e.ViewBox;
                        _renderer.Matrix = e.Matrix;
                        _renderer.Translate = translate;
                        _renderer.BeginScene(g);
                        _renderer.Render();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                /*}
                else
                {*/
                    //g.Clear(Color.Gray);
                    //draw test rectagle
                    var rectPosition = translate.Transform(new WinPoint(100.0, 200.0));
                    PointF rectPositionF = rectPosition.ToPointF();
                    g.DrawRectangle(Pens.Black, rectPositionF.X, rectPositionF.Y, 100f, 100f);

                    var p = new WinPoint(100, 200);
                    p = translate.Transform(p);
                    g.DrawString("hello world jpgqy", SystemFonts.DefaultFont, Brushes.Black, p.ToPointF(), new System.Drawing.StringFormat { LineAlignment = StringAlignment.Far });

                }

                BeginInvoke(new ThreadStart(RenderingComplete));
                lock (this)
                {
                    var temp = _backBuffer;
                    _backBuffer = _frontBuffer;
                    _frontBuffer = temp;
                    _imageTransform = e.Matrix;
                    _imageTransform.Invert();
                    viewport1.Invalidate();
                }
            }
        }	
		

		private bool Save()
		{
			if (_fileName != null)
				SaveAs(_fileName);
			else
				return SaveAs();
			return true;
		}

		private bool SaveAs()
		{
            saveFileDialog.FileName = _fileName;
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				_fileName = saveFileDialog.FileName;
				SaveAs(saveFileDialog.FileName);
				return true;
			}
			return false;
		}

		private void SaveAs(string filename)
		{
            _map.WriteXml(filename);
            _mapHasChanges = false;
			recentFilesManager.SetFileRecent(filename);
			SetTitle(filename);
		}

		private void SetTitle(string filename)
		{
			Text = filename + " - Spatial Viewer";
		}

		private static void ShowException(Exception ex)
		{
			MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

        void map_Changed(object sender, EventArgs e)
        {
            _mapHasChanges = true;
            if(AutoRender)
                RedrawAsync();
        }

        private void TuneMap(ContainerNode layerGroup)
		{
			foreach (ThemeNode child in layerGroup.Nodes)
			{
                var childGroup = child as ContainerNode;
				if (childGroup != null)
					TuneMap(childGroup);
			}
			
			if (layerGroup.Visible = layerGroup.TotalFeatureCount > 0)
			{
				if ((layerGroup.BoundingBox != null) && (layerGroup.BoundingBox.Area > 0.0))
				{
				}
				if (layerGroup.AvgBoundingBoxArea > 0.0)
				{
					layerGroup.MinScale = 20.0 / Math.Sqrt(layerGroup.AvgBoundingBoxArea * 2.0);
				}
				else
				{
					layerGroup.MinScale = 1.0;
				}
				if (layerGroup.MinBoundingBoxArea > 0.0)
				{
					layerGroup.MinScale = Math.Max(layerGroup.MinScale, 1.0 / layerGroup.MinBoundingBoxArea);
				}
				if (layerGroup.MaxBoundingBoxArea > 0.0)
				{
					layerGroup.MaxScale = Math.Sqrt(layerGroup.MaxBoundingBoxArea * 2.0) * 10.0;
				}
			}
		}

		private void UpdateStatus(ThemeNode node)
		{
			if (node == null)
				lblStatus.Text = string.Empty;
			else
			{
				var renderer = node.Renderer as IGroupRenderer;

                lblStatus.Text = (renderer != null)
                    ? string.Format("{0} \"{1}\" [{2}] in {3}", node, node.LabelOrDefault, renderer.RenderCount > 0 ? renderer.FeatureCount.ToString() : "N/A", renderer.RenderTime)
                    : string.Format("{0} \"{1}\"", node.GetType().Name, node.LabelOrDefault);
				statusStrip1.Refresh();
			}
		}

		private void viewport1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape && _renderer != null)
				_renderer.Cancel();
		}

		private void viewport1_MouseMove(object sender, MouseEventArgs e)
		{
			var p = viewport1.ClientToWorld(e.Location);
			positionLabel.Text = string.Format("client=({0}, {1}) world=({2}, {3}), zoom={4}, angle={5}", new object[] { e.X, e.Y, p.X, p.Y, viewport1.Zoom, viewport1.Angle });
		}

		private void viewport1_Paint(object sender, PaintEventArgs e)
		{
			lock (this)
			{
				if (_frontBuffer != null && _renderer != null)
				{
					var transform = e.Graphics.Transform;
                    
                    e.Graphics.Transform = viewport1.GetRotationScaleMatrix();
                    e.Graphics.MultiplyTransform((_imageTransform * viewport1.GetTranslateMatrix()).ToGdiMatrix());
					e.Graphics.DrawImage(_frontBuffer, 0, 0);
					
                    e.Graphics.Transform = transform;
				}
			}
		}

		private void viewport1_WindowChanged(object sender, WindowChangedEventArgs e)
		{
            if (AutoRender && e.Type != WindowChangedEventType.PanStart && e.Type != WindowChangedEventType.Pan && _renderer != null)
                RedrawAsync();

            if (_matrixWindow != null)
                _matrixWindow.Matrix = viewport1.View;
		}

		private void RedrawAsync()
		{
			Size clientSize = viewport1.ClientSize;
			if (_backBuffer == null || _backBuffer.Size != clientSize)
				_backBuffer = new Bitmap(clientSize.Width, clientSize.Height);

			var box = viewport1.GetWindowBox();

			_bgRenderArgs = new RenderArgs();
			_bgRenderArgs.ViewBox = ToEnvelope(box);

            _bgRenderArgs.Matrix = viewport1.View;
			_bgRenderArgs.Scale = viewport1.Zoom;
			_rendererSignal.Set();
		}

		// Nested Types
		private class RenderArgs
		{
			public Matrix Matrix { get; set; }

			public double Scale { get; set; }

			public Envelope ViewBox { get; set; }

			public System.Drawing.Drawing2D.Matrix Transform { get; set; }
		}

        private void triStateTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateStatus(themeEditorComponent.SelectedThemeNode);
        }

        private void mnuAutoRender_Click(object sender, EventArgs e)
        {
            if (AutoRender)
                RedrawAsync();
        }

        public bool AutoRender
        {
            get { return mnuAutoRender.Checked; }
            set { mnuAutoRender.Checked = value; }
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            RedrawAsync();
        }

        private void reopenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.ReopenLastFile = reopenToolStripMenuItem.Checked;
        }


        private void mnuClose_Click(object sender, EventArgs e)
        {
            CloseMap();
        }

        private void CloseMap()
        {
            if (PromptToSaveChanges(_map))
            {
                themeEditorComponent.Clear();
                InitMap();
                _renderer = null;

                ResetView();

                mnuClose.Visible = false;

                Settings.Default.LastFile = null;
                Settings.Default.Save();
            }
        }

        private void ResetView()
        {
            viewport1.SetView(0, 0, 1, 0);
        }

        private void mnuResetView_Click(object sender, EventArgs e)
        {
            ResetView();
        }

        private void showMatrixWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.ShowMatrixWindow = showMatrixWindowToolStripMenuItem.Checked;
            Settings.Default.Save();
            if (showMatrixWindowToolStripMenuItem.Checked)
                ShowMatrixWindow();
            else if (_matrixWindow != null)
                _matrixWindow.Hide();
        }

        private void ShowMatrixWindow()
        {
            if (_matrixWindow != null)
                _matrixWindow.Show();
            else
            {
                _matrixWindow = new MatrixWindow();
                _matrixWindow.TopMost = true;
                _matrixWindow.Matrix = viewport1.View;
                _matrixWindow.Show();
                _matrixWindow.FormClosed += _matrixWindow_FormClosed;
                _matrixWindow.MatrixChanged += _matrixWindow_MatrixChanged;
            }
        }

        private void _matrixWindow_MatrixChanged(object sender, EventArgs e)
        {
            viewport1.View = _matrixWindow.Matrix;
        }

        private void _matrixWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _matrixWindow.Dispose();
            _matrixWindow = null;
            Settings.Default.ShowMatrixWindow =
                showMatrixWindowToolStripMenuItem.Checked = false;
            Settings.Default.Save();
        }
    }
}
