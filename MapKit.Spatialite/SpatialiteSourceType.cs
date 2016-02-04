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

        public override IEnumerable<ThemeNode> CreateNew()
        {
            using (var form = new NewSpatialiteLayerDialog())
                if (form.ShowDialog() == DialogResult.OK)
                    foreach (var col in form.SelectedSpatialColumns)
                    {
                        var layer = new SpatialiteLayer();
                        layer.GeometryColumn = col.ColumnName;
                        layer.Indexed = col.SpatialIndex == SpatialIndexMode.RTree;
                        layer.Table = col.TableName;
                        layer.File = form.File;
                        layer.Name = col.TableName;

                        if (col.Type == SpatialiteUtil.PointType || col.Type == SpatialiteUtil.MultiPointType || col.Type == SpatialiteUtil.GeometryCollectionType)
                            layer.Nodes.Add(new Marker());
                        if (col.Type == SpatialiteUtil.LineStringType || col.Type == SpatialiteUtil.MultiLineStringType || col.Type == SpatialiteUtil.GeometryCollectionType)
                            layer.Nodes.Add(new Stroke());
                        if (col.Type == SpatialiteUtil.PolygonType || col.Type == SpatialiteUtil.MultiPolygonType || col.Type == SpatialiteUtil.GeometryCollectionType)
                            layer.Nodes.Add(new SolidFill());

                       yield return layer;
                    }
        }
    }
}
