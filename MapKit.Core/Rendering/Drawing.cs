using System;
using System.Drawing;

namespace MapKit.Core.Rendering
{
    static class Drawing
    {
        private static Random _rnd;

        static Drawing()
        {
            _rnd = new Random();
        }

        public static Color Random
        {
            get { return Color.FromArgb(_rnd.Next(255), _rnd.Next(255), _rnd.Next(255)); }
        }
    }
}
