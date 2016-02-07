using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapKit.Core;
using System.Data.SQLite;
using System.Windows.Forms;
using Cyrez.SqliteUtil;
using System.ComponentModel;

namespace MapKit.Spatialite
{
    public class SpatialiteSource : DBConnectionDataSource
    {
        [Browsable(false)]
        public SQLiteConnection SQLiteConnection { get; set; }

        [Browsable(false)]
        public override System.Data.Common.DbConnection Connection
        {
            get
            {
                return SQLiteConnection;
            }
            protected set
            {
                SQLiteConnection = (SQLiteConnection)value;
                base.Connection = value;
            }
        }

        public string Filename { get; set; }


        public override IEnumerable<FeatureType> CreateFeatureTypes()
        {
            using (var form = new NewSpatialiteLayerDialog())
            {
                if (form.ShowDialog() != DialogResult.OK)
                    yield break;

                Filename = form.File;
                SQLiteConnection = ConnectionManager.Default.GetConnection(form.File);

                foreach (var col in form.SelectedSpatialColumns)
                {
                    var layer = new SpatialiteFeatureType();
                    layer.GeometryColumn = col.ColumnName;
                    layer.Indexed = col.SpatialIndex == SpatialIndexMode.RTree;
                    layer.TableName = col.TableName;
                    layer.Name = col.TableName;


                    yield return layer;
                }
            }
        }
    }
}
