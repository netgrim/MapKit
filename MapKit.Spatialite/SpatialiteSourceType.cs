using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapKit.Core;
using System.Xml;
using System.Windows.Forms;

namespace MapKit.Spatialite
{
    public class SpatialiteSourceType : DataSourceType
    {

        public SpatialiteSourceType()
            :base("Spatialite", SpatialiteLayer.ElementName, typeof(SpatialiteLayer))
        {
        }

        public override DataSource CreateDataSource()
        {
            return new SpatialiteSource() { Name = "New Spatialite source", SourceType = this };
        }
    }
}
