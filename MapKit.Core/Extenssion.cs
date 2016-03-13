using System.Drawing;
using WinPoint = System.Windows.Point;
using GdiMatrix = System.Drawing.Drawing2D.Matrix;
using GdiPoint = System.Drawing.Point;
using WinMatrix = System.Windows.Media.Matrix;
using NetTopologySuite.Geometries;
using GeoAPI.Geometries;

namespace MapKit.Core
{
    static class Extension
	{
		public static PointF ToPointF(this WinPoint p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

		public static PointF ToPointF(this IPoint p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

        public static PointF[] ToPointFArray(this WinPoint[] list)
        {
            var points = new PointF[list.Length];
            for (int i = 0; i < list.Length; i++)
                points[i] = list[i].ToPointF();
            return points;
        }

		public static WinMatrix ToWinMatrix(this GdiMatrix m)
		{
			return new System.Windows.Media.Matrix(m.Elements[0], m.Elements[1], m.Elements[2], m.Elements[3], m.Elements[4], m.Elements[5]);
		}

		public static GdiMatrix ToGdiMatrix(this WinMatrix m)
		{
			return new GdiMatrix((float)m.M11, (float)m.M12, (float)m.M21, (float)m.M22, (float)m.OffsetX, (float)m.OffsetY);
		}



        //public static Point2D ToPoint2D(this WinPoint p)
        //{
        //    return new Point2D(p.X, p.Y);
        //}

		public static void ToWinPoint(this Coordinate p, ref WinPoint winPoint)
		{
			winPoint.X = p.X;
			winPoint.Y = p.Y;
		}

		public static void ToWinPoint(this IPoint p, ref WinPoint winPoint)
		{
			winPoint.X = p.X;
			winPoint.Y = p.Y;
		}

		public static WinPoint ToWinPoint(this IPoint p)
		{
			return new WinPoint(p.X, p.Y);
		}

		public static WinPoint ToWinPoint(this Coordinate p)
		{
			return new WinPoint(p.X, p.Y);
		}

		public static WinPoint ToWinPoint(this GdiPoint point)
		{
			return new WinPoint(point.X, point.Y);
		}

		public static Polygon ToRectanglePolygon(this Rectangle rectangle)
		{
			return ((RectangleF)rectangle).ToRectanglePolygon();
		}

		public static Polygon ToRectanglePolygon(this RectangleF rectangle)
		{
			var point0 = new Coordinate(rectangle.X, rectangle.Y);
			var point2 = new Coordinate(rectangle.Right, rectangle.Bottom);
            var point1 = new Coordinate(point2.X, point0.Y);
			var point3 = new Coordinate(point0.X, point2.Y);

			//generate rectangle polygon
			return new Polygon(new LinearRing(new[] { point0, point1, point2, point3, point0 }));
		}

        public static Coordinate ToCoordinate(this WinPoint point)
        {
            return new Coordinate(point.X, point.Y);
        }
	}
}
