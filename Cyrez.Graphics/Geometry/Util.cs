using System;
using System.Collections.Generic;
using System.Text;

namespace Cyrez.Graphics.Geometry
{
	public static class Util
	{

        /// <summary>Converts an angle to radians.</summary>
        /// <param name="angle">The angle in degrees.</param>
        public static double ToRadians(double angle)
        {
            return angle * (Math.PI / 180.0);
        }

        /// <summary>Converts an angle to degrees.</summary>
        /// <param name="angle">The angle in radians.</param>
        public static double ToDegrees(double angle)
        {
            return angle / (Math.PI / 180.0);
        }

   
    }
}
