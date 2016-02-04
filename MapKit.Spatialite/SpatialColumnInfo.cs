using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.Spatialite
{
	public class SpatialColumnInfo
	{
		public string TableName { get; set; }

		public string ColumnName { get; set; }

		public int SRID { get; set; }

		public string Type { get; set; }

		public SpatialIndexMode SpatialIndex { get; set; }

		public int Dimension { get; set; }
	}
}
