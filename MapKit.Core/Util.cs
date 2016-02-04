using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace MapKit.Core
{
	static class Util
	{
		public static Color GetColor(float alpha, Color baseColor)
		{
            if (alpha >= 1) return baseColor;
            else if (alpha < 0) return Color.Transparent;

			return Color.FromArgb((int)(alpha * 255), baseColor);
		}

        private static bool IsHtmlColor(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression)) return false;
            var length = expression.Length;
            if (expression.Length != 7 || expression[0] != '#') return false;

            for (int i = 1; i < length; i++)
            {
                var c = char.ToUpper(expression[i]);
                if (c < '0' || (c > '9' && c < 'A') || c > 'F')
                    return false;
            }
            return true;
        }

        public static bool TryParseColor(string s, out Color color)
        {
            if (!String.IsNullOrEmpty(s))
            {
                //SystemColors.
                color = Color.FromName(s);
                if (color.IsKnownColor)
                    return true;

                if (Regex.IsMatch(s, "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$"))
                {
                    color = ColorTranslator.FromHtml(s);
                    return true;
                }
            }
            color = Color.Empty;

            return false;
        }

        internal static GeoAPI.Geometries.IGeometry ToPolygon(GeoAPI.Geometries.Envelope queryWindow)
        {
            return new NetTopologySuite.Geometries.Polygon(new NetTopologySuite.Geometries.LinearRing(new[] { 
                new GeoAPI.Geometries.Coordinate(queryWindow.MinX, queryWindow.MinY),
                new GeoAPI.Geometries.Coordinate(queryWindow.MinX, queryWindow.MaxY),
                new GeoAPI.Geometries.Coordinate(queryWindow.MaxX, queryWindow.MaxY),
                new GeoAPI.Geometries.Coordinate(queryWindow.MaxX, queryWindow.MinY),
                new GeoAPI.Geometries.Coordinate(queryWindow.MinX, queryWindow.MinY)}));
        }
    }
}
