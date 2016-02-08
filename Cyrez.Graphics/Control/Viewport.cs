using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Cyrez.Graphics;
using Cyrez.Graphics.Geometry;

using System.Windows.Media;
using MatrixF = System.Drawing.Drawing2D.Matrix;
using WinPoint = System.Windows.Point;

namespace Cyrez.Graphics.Control
{
	public partial class Viewport : UserControl
	{
        private Matrix _view;
		
		public Viewport()
		{
			InitializeComponent();
			SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

			Zoom = 1;
			MaxZoom = double.MaxValue;
			MinZoom = 0;
			ZoomFactor = 2f;
			Angle = 0;
			DebugDrawAxies = true;
			DebugDrawGrid = true;
			
			LeftButtonTool = new PointerTool();
			MiddleButtonTool = new PanTool();
			RightButtonTool = new RotateViewTool();
			WheelScrollTool = new ZoomTool();

			_view = new Matrix();
            _view.Translate(0, -ClientSize.Height);
            _view.Scale(1, -1);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Matrix View
		{
			get { return _view; }
            set
            {
                _view = value;
                Invalidate();
            }
		}

        public MatrixF SetViewScreen(Matrix viewScreen)
        {
            //var invProj = _projection;
            //invProj.Invert();

            ////rotation/scale matrix
            //var m1 = viewScreen * invProj;
            //m1.OffsetX = m1.OffsetY = 0;
            
            //var invM1 = m1;
            //invM1.Invert();
            //_view = viewScreen * invM1;

            return new MatrixF();//(float)m1.M11, (float)m1.M12, (float)m1.M21, (float)m1.M22, 0, 0);
        }


		public Matrix ClientToWorld()
		{
			var invView = _view;
            invView.Invert();
            return invView;
		}

		public Matrix ClientToCamera()
		{
			var clientToCamera = new Matrix();//_camera.Elements[0], _camera.Elements[1], _camera.Elements[2], _camera.Elements[3], _camera.Elements[4], _camera.Elements[5]);
			clientToCamera.Invert();
			return clientToCamera;
		}

		public WinPoint ClientToWorld(Point p)
		{
			return ClientToWorld().Transform(new WinPoint(p.X, p.Y));
		}

		//public PointF ClientToWorldF(PointF p)
		//{
		//    var world = (Matrix)_view.Clone();
		//    world.Invert();
		//    var points = new PointF[] { p };
		//    world.TransformPoints(points);

		//    return points[0];
		//}

		private Tool _leftButtonTool;
		
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tool LeftButtonTool
		{
			get { return _leftButtonTool; }
			set
			{
				if (_leftButtonTool != null)
					_leftButtonTool.Deactivate();
				if (value != null)
					value.Activate(this);
				_leftButtonTool = value;
			}
		}

		private Tool _rightButtonTool;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tool RightButtonTool
		{
			get { return _rightButtonTool; }
			set
			{
				if (_rightButtonTool != null)
					_rightButtonTool.Deactivate();
				if (value != null)
					value.Activate(this);
				_rightButtonTool = value;
			}
		}

		private Tool _middleButtonTool;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tool MiddleButtonTool
		{
			get { return _middleButtonTool; }
			set
			{
				if (_middleButtonTool != null)
					_middleButtonTool.Deactivate();
				if (value != null)
					value.Activate(this);
				_middleButtonTool = value;
			}
		}
		
		private Tool _wheelScrollTool;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tool WheelScrollTool
		{
			get { return _wheelScrollTool; }
			set
			{
				if (_wheelScrollTool != null)
					_wheelScrollTool.Deactivate();
				if (value != null)
					value.Activate(this);
				_wheelScrollTool = value;
			}
		}				


		#region event

		protected override void OnMouseDown(MouseEventArgs e)
		{

			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				if (_leftButtonTool != null)
					_leftButtonTool.PointerDown(new ToolEventArgs(e, ClientToWorld(e.Location)));

			if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle)
				if (_middleButtonTool != null)
					_middleButtonTool.PointerDown(new ToolEventArgs(e, ClientToWorld(e.Location)));

			if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
				if (_rightButtonTool != null)
					_rightButtonTool.PointerDown(new ToolEventArgs(e, ClientToWorld(e.Location)));
			     
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left && _leftButtonTool != null)
				_leftButtonTool.PointerUp(new ToolEventArgs(e, ClientToWorld(e.Location)));
			if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle && _middleButtonTool != null)
				_middleButtonTool.PointerUp(new ToolEventArgs(e, ClientToWorld(e.Location)));
			if ((e.Button & MouseButtons.Right) == MouseButtons.Right && _rightButtonTool != null)
				_rightButtonTool.PointerUp(new ToolEventArgs(e, ClientToWorld(e.Location)));
			
			base.OnMouseUp(e);
		}


		public event EventHandler<WindowChangedEventArgs> WindowChanged;
		private void OnWindowChanged(WindowChangedEventArgs e)
		{
			if (WindowChanged != null)
				WindowChanged(this, e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left && _leftButtonTool != null)
				_leftButtonTool.PointerMove(new ToolEventArgs(e, ClientToWorld(e.Location)));
			if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle && _middleButtonTool != null)
				_middleButtonTool.PointerMove(new ToolEventArgs(e, ClientToWorld(e.Location)));
			if ((e.Button & MouseButtons.Right) == MouseButtons.Right && _rightButtonTool != null)
				_rightButtonTool.PointerMove(new ToolEventArgs(e, ClientToWorld(e.Location)));

			base.OnMouseMove(e);
		}
				

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);

            //using (var brush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.RoyalBlue))
            //    e.Graphics.FillRectangle(brush, e.ClipRectangle);

            var offset = GetTranslateMatrix();

            e.Graphics.Transform = GetRotationScaleMatrix();
						
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //debug text orientation
            //var p = new WinPoint(0, 50);
            //p = offset.Transform(p);
            //e.Graphics.DrawString("hello world jpgqy", SystemFonts.DefaultFont, Brushes.Black, p.ToPointF(), new StringFormat { LineAlignment = StringAlignment.Far });

			if (DebugDrawGrid)
			{
                var m = offset;
			    m.ScalePrepend(50, 50);
                using (var pen = new Pen(Color.LightGray, 1/50))
				for (int i = 0; i < 10; i++)
				{
					//e.Graphics.DrawLine(Pens.LightGray, i * 50, 0, i * 50, 9 * 50);
					//e.Graphics.DrawLine(Pens.LightGray, 0, i * 50, 9 * 50, i * 50);
					var a = new[] { new WinPoint(i, 0), new WinPoint(i, 9) };
					m.Transform(a);

					e.Graphics.DrawLine(pen, (float)a[0].X, (float)a[0].Y, (float)a[1].X, (float)a[1].Y);
					a = new[] { new WinPoint(0, i), new WinPoint(9, i) };
					m.Transform(a);
					e.Graphics.DrawLine(pen, (float)a[0].X, (float)a[0].Y, (float)a[1].X, (float)a[1].Y);

					//e.Graphics.DrawLine(pen, i, 0, i, 9);
					//e.Graphics.DrawLine(pen, 0, i, 9, i);
				}
			}

			//e.Graphics.ResetTransform();

			base.OnPaint(e);
						
			if (DebugDrawAxies)
			{
				//using(var brush = new LinearGradientBrush(new PointF(0,0), new PointF(0,1000), Color.Blue, Color.Transparent))
				//using(var pen = new Pen(brush))
				var a = new[]{new WinPoint(0, 0), new WinPoint(100, 0)};
                offset.Transform(a);

				e.Graphics.DrawLine(Pens.Red, (float)a[0].X, (float)a[0].Y, (float)a[1].X, (float)a[1].Y);
				//using (var brush = new LinearGradientBrush(new Point(0, 0), new Point(1000, 0), Color.Red, Color.Transparent))
				//using (var pen = new Pen(brush))
				a = new[] { new WinPoint(0, 0), new WinPoint(0, 100) };
                offset.Transform(a);

                e.Graphics.DrawLine(Pens.Blue, (float)a[0].X, (float)a[0].Y, (float)a[1].X, (float)a[1].Y);
			}

			if (_leftButtonTool != null && _leftButtonTool.IsDown)
				_leftButtonTool.Paint(e);
			if (_middleButtonTool != null && _middleButtonTool.IsDown)
				_middleButtonTool.Paint(e);
			if (_rightButtonTool != null && _rightButtonTool.IsDown)
				_rightButtonTool.Paint(e);
			if (_wheelScrollTool != null && _wheelScrollTool.IsDown)
				_wheelScrollTool.Paint(e);

			//e.Graphics.ScaleTransform(1, -1);
			//using (var f = new Font("Sppc", 10))
			//{
			//    var sf = new StringFormat(StringFormatFlags.NoClip);

			//    var size = e.Graphics.MeasureString("B", f, 0, StringFormat.GenericTypographic);
			//    e.Graphics.DrawRectangle(Pens.Blue, 100f, 50f, (float)size.Width, (float)size.Height);

			//    //e.Graphics.DrawString("B", f, Brushes.Red, 100, 50, StringFormat.GenericTypographic);
			//}
			


			//using (var pen = new Pen(Color.Black, 0.1f))
			//{
			//    e.Graphics.DrawLine(pen, 100, 50, 200, 50);
			//    e.Graphics.DrawLine(pen, 100, 55, 100, 45);
			//    e.Graphics.DrawLine(pen, 105, 55, 105, 45);
			//    e.Graphics.DrawLine(pen, 110, 55, 110, 45);
			//    e.Graphics.DrawLine(pen, 115, 55, 115, 45);
			//}


		}


		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (_wheelScrollTool != null)
				_wheelScrollTool.VScroll(new ToolEventArgs(e, ClientToWorld(e.Location)));
			base.OnMouseWheel(e);
		}

		#endregion



		public double MaxZoom { get; set; }

		public double MinZoom { get; set; }



		public bool DebugDrawGrid { get; set; }

		private double _zoom = 1;
		public double Zoom
		{
			get { return _zoom; }
			set
			{
				_zoom = value;
			}
		}

		public float ZoomFactor { get; set; }

		public double Angle { get; set; }

		public bool DebugDrawAxies { get; set; }



		/// <summary>
		/// Returns the bounding box of the window.
		/// </summary>
		/// <returns>The bounding box of the window.</returns>
		public BoundingBox GetWindowBox()
		{
			var box = new BoundingBox();

			box.Adjust(ClientToWorld(new Point(0, 0)).ToPoint2D());
			box.Adjust(ClientToWorld(new Point(0, Height)).ToPoint2D());
			box.Adjust(ClientToWorld(new Point(Width, 0)).ToPoint2D());
			box.Adjust(ClientToWorld(new Point(Width, Height)).ToPoint2D());

			return box;
		}



		public void ZoomWindow(BoundingBox box)
		{
			var boxRatio =  box.Width / box.Height;
			var clientRatio = (double)ClientSize.Width / ClientSize.Height;
			
			//var widthRatio = box.Width / ClientSize.Width;
			//var heightRatio = box.Height / ClientSize.Height;

			double zoom;
			if (boxRatio > clientRatio)
				zoom = ClientSize.Width / box.Width;
			else
				zoom = ClientSize.Height / box.Height;

			SetView(box.Center, zoom, 0, WindowChangedEventType.ZoomWindow);
		}

        private void SetView(WinPoint center, double zoom, double angle, WindowChangedEventType windowChangedEventType)
		{
            SetView(center.X, center.Y, zoom, angle, windowChangedEventType);
		}

		public void SetView(double centerX, double centerY, double zoom, double angle)
		{
            SetView(centerX, centerY, zoom, angle, WindowChangedEventType.ByCode);
		}

        private void SetView(double centerX, double centerY, double zoom, double angle, WindowChangedEventType windowChangedEventType)
        {
            _view = new Matrix();
            _view.Translate(-centerX, -centerY);
            _view.Rotate(angle);
            _view.Scale(zoom, -zoom);
            _view.Translate(ClientSize.Width / 2.0, ClientSize.Height / 2.0);
            
            Zoom = zoom;
            Angle = angle;

            OnWindowChanged(new WindowChangedEventArgs(windowChangedEventType));
            Invalidate();
        }

        internal void PerformWindowChanged(WindowChangedEventType windowChangedEventType)
		{
			OnWindowChanged(new WindowChangedEventArgs(windowChangedEventType));
		}

		public void CenterView(WinPoint center)
		{
			SetView(center, Zoom, Angle, WindowChangedEventType.CenterView);
		}

		public void TranslatePrepend(double x, double y)
		{
			_view.TranslatePrepend(x, y);
		}

		public void Translate(double x, double y)
		{
			_view.Translate(x, y);
		}

		public void ZoomExtent()
		{
			OnWindowChanged(new WindowChangedEventArgs(WindowChangedEventType.ZoomExtent));
		}

        public void ScaleAtPrepend(float scaleX, float scaleY, double centerX, double centerY)
        {
            _view.ScaleAtPrepend(scaleX, scaleY, centerX, centerY);
        }

        public void RotateAtPrepend(double angle, double centerX, double centerY)
        {
            _view.RotateAtPrepend(angle, centerX, centerY);
        }

        public MatrixF GetRotationScaleMatrix()
        {
            return GetRotationScaleMatrix(_view);
        }

        public static MatrixF GetRotationScaleMatrix(Matrix transform)
        {
            transform.OffsetX = transform.OffsetY = 0;

            var gdiTransform = transform.ToGdiMatrix();
            gdiTransform.Scale(1, -1);
            return gdiTransform;
        }

        /// <summary>
        /// Get the offset component of the current view matrix
        /// </summary>
        /// <returns></returns>
        public Matrix GetTranslateMatrix()
        {
            return GetTranslateMatrix(_view);
        }

        /// <summary>
        /// Extract the offset component of the specified matrix
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Matrix GetTranslateMatrix(Matrix m)
        {
            var translate = m;
            translate.OffsetX = translate.OffsetY = 0;
            translate.Invert();

            translate = m * translate;
            translate.Scale(1, -1);

            return translate;
        }

        public void RotateAt(double angle, double centerX, double centerY)
        {
            _view.RotateAt(angle, centerX, centerY);
        }
    }
}
