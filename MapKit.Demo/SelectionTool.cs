using System.Drawing;
using Cyrez.Graphics.Control;
using Cyrez.Graphics;
using WinPoint = System.Windows.Point;

using GeoAPI.Geometries;

namespace MapKit.Demo
{
	class SelectionTool : RectangleTool
	{
		
		public SelectionTool (SpatialViewerTestForm form)
		{
			//Map = map;
			Form = form;
			PickBoxSize = new Size(5, 5);
		}

		public Size PickBoxSize { get; set; }

		internal SpatialViewerTestForm Form { get; set; }


		protected override void OnComplete()
		{
			//var rect = DragRectangle.ToRectanglePolygon();

			var bottom = DragRectangle.Top - DragRectangle.Height;
			var points = new[]{
				new WinPoint(DragRectangle.Left, bottom),
				new WinPoint (DragRectangle.Left, DragRectangle.Top),
				new WinPoint(DragRectangle.Right, DragRectangle.Top),
				new WinPoint (DragRectangle.Right, bottom)};

			Viewport.ClientToWorld().Transform(points);

			var box = new Envelope();
			box.ExpandToInclude(ToCoordinate(points[0]));
			box.ExpandToInclude(ToCoordinate(points[1]));
			box.ExpandToInclude(ToCoordinate(points[2]));
			box.ExpandToInclude(ToCoordinate(points[3]));
			
			Select(box);
		}

        static Coordinate ToCoordinate(WinPoint p)
        {
            return new Coordinate(p.X, p.Y);
        }

		public override void PointerUp(ToolEventArgs e)
		{
			if (!IsDragging)
				Select(GetPickBox(e.Location));
			base.PointerUp(e);
		}

		private void Select(Envelope box)
		{
			Form.Status = "HitTest: " + Form.CurrentMap.HitCount(box, Viewport.Zoom);
		}

		private Envelope GetPickBox(Point p)
		{
			var halfWidth = PickBoxSize.Width / 2f / Viewport.Zoom;
			var halfHeight = PickBoxSize.Height / 2f / Viewport.Zoom;

			var w = Viewport.ClientToWorld(p);

			return new Envelope(w.X - halfWidth, w.X + halfWidth, w.Y - halfHeight, w.Y + halfHeight);
		}
	}
}
