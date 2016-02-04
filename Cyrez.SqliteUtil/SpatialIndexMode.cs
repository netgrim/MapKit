using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyrez.SqliteUtil
{
    public enum SpatialIndexMode
    {
        None = 0,
        RTree = 1,
        MBRCache = 2,
    }
}
