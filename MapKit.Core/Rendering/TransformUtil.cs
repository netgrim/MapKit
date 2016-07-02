using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using WinPoint = System.Windows.Point;

namespace MapKit.Core.Rendering
{
    static class TransformUtil
    {
        public static System.Windows.Point[] ToWinPointArray(ICoordinateSequence list)
        {
            int count = list.Count;
            var points = new WinPoint[count];

            for (int i = 0; i < count; i++)
                list.GetCoordinate(i).ToWinPoint(ref points[i]);

            return points;
        }

        public static System.Windows.Point[] ToWinPointArray(Coordinate[] array)
        {
            int count = array.Length;
            var points = new WinPoint[count];

            for (int i = 0; i < count; i++)
                array[i].ToWinPoint(ref points[i]);

            return points;
        }

        public static Coordinate[] ToCoordinateArray(WinPoint[] coordinates)
        {
            int count = coordinates.Length;
            var points = new Coordinate[count];

            for (int i = 0; i < count; i++)
                points[i] = coordinates[i].ToCoordinate();

            return points;
        }
    }
}
