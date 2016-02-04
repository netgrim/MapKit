using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetTopologySuite.Geometries
{
    public class GeometryException : Exception
    {
        public GeometryException(string message)
            :base (message)
        {
        }

    }
}
