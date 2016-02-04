using System.Windows.Forms;
using System.Drawing;
using MatrixF = System.Drawing.Drawing2D.Matrix;

namespace Cyrez.Graphics.Control
{
	public class ZoomTool: Tool
	{


		public override void VScroll(MouseEventArgs e)
		{
			var wheelZoom = e.Delta / SystemInformation.MouseWheelScrollDelta * Viewport.ZoomFactor;
			var scale = wheelZoom > 0 ? wheelZoom : -1 / wheelZoom;

			var newZoom = Viewport.Zoom * scale;
			if (newZoom < Viewport.MinZoom)
				scale = (float)(Viewport.MinZoom / Viewport.Zoom);
			if (newZoom > Viewport.MaxZoom)
				scale = (float)(Viewport.MaxZoom / Viewport.Zoom);

			Viewport.Zoom *= scale;
            
            var p = Viewport.ClientToWorld().Transform(Viewport.PointToClient(Cursor.Position).ToWinPoint());

            Viewport.ScaleAtPrepend(scale, scale, p.X, p.Y);
            
            var type = scale > 1 ? WindowChangedEventType.ZoomIn : WindowChangedEventType.ZoomOut;

			Viewport.PerformWindowChanged(type);
			Viewport.Invalidate();

			base.VScroll(e);
		}
	}
}
