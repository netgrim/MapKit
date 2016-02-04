using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using WinPoint = System.Windows.Point;
using GdiMatrix = System.Drawing.Drawing2D.Matrix;
using GdiPoint = System.Drawing.Point;
using WinMatrix = System.Windows.Media.Matrix;
using Point2D = Cyrez.Graphics.Geometry.Point2D;

namespace Cyrez.Graphics
{
	public static class Extension
	{
		public static PointF ToPointF(this WinPoint p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

		public static WinMatrix ToWinMatrix(this GdiMatrix m)
		{
			return new System.Windows.Media.Matrix(m.Elements[0], m.Elements[1], m.Elements[2], m.Elements[3], m.Elements[4], m.Elements[5]);
		}

		public static GdiMatrix ToGdiMatrix(this WinMatrix m)
		{
			return new GdiMatrix((float)m.M11, (float)m.M12, (float)m.M21, (float)m.M22, (float)m.OffsetX, (float)m.OffsetY);
		}

        public static Point2D ToPoint2D(this WinPoint p)
        {
            return new Point2D(p.X, p.Y);
        }

        public static Point2D ToPoint2D(this GdiPoint p)
        {
            return new Point2D(p.X, p.Y);
        }

		public static WinPoint ToWinPoint(this GdiPoint point)
		{
			return new WinPoint(point.X, point.Y);
		}

	}
}
