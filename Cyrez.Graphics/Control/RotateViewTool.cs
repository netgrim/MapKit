using System;
using System.Drawing;
using MatrixF = System.Drawing.Drawing2D.Matrix;

namespace Cyrez.Graphics.Control
{
	public class RotateViewTool : Tool
	{

		public override void PointerMove(ToolEventArgs e)
		{
            if (e.Location == base.LastPosition)
                return;

			if (LastPosition.X - LastDown.X != 0 || LastPosition.Y - LastDown.Y != 0)
			{
                var thetaRadian = Math.Atan2(e.X - LastDown.X, e.Y - LastDown.Y) - Math.Atan2(LastPosition.X - LastDown.X, LastPosition.Y - LastDown.Y);
				//var theta = Math.PI / 4;
				var center = Viewport.ClientToWorld().Transform(LastDown.ToWinPoint());
				var theta = (thetaRadian / Math.PI * 180);
				//_view.RotateAtPrepend(theta / Math.PI * 180, center.X, center.Y);

                Viewport.RotateAtPrepend(theta, center.X, center.Y);
                Viewport.Angle = (Viewport.Angle + theta) % 360;
				/*Viewport.Camera.RotateAt((float)theta, new PointF((float)center.X, (float)center.Y));
                Viewport.Angle = (Viewport.Angle - theta) % 360;
				var camera = Viewport.Camera;

				var inv = (MatrixF)camera.Clone();
				inv.Invert();

				var points = new[] { new PointF(camera.OffsetX, camera.OffsetY) };
				inv.TransformVectors(points);
				var t = points[0];

				Viewport.Translate(t.X, t.Y);
				Viewport.Camera.Translate(-camera.OffsetX, -camera.OffsetY, System.Drawing.Drawing2D.MatrixOrder.Append);
                */
				Viewport.PerformWindowChanged(WindowChangedEventType.Rotate);
				Viewport.Invalidate();
			}
			
			base.PointerMove(e);
		}
	}
}
