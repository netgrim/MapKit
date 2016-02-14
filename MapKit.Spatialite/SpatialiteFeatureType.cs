using System.Data;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;
using System.Diagnostics;
using System.Xml;
using MapKit.Core;
using Cyrez.SqliteUtil;

namespace MapKit.Spatialite
{
    //[TypeConverter(typeof(SpatialiteSourceConverter))]
    //[Editor(typeof(SpatialiteSourceEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class SpatialiteFeatureType : FeatureType
	{
        internal const string ElementName = "spatialiteLayer";
        internal const string FidField = "fidColumn";
        internal const string GeomField = "geometryColumn";
        internal const string SqlField = "sql";
        internal const string TableField = "table";
        internal const string IndexedField = "indexed";

        private SQLiteCommand _cmd;
        private const string DefaultFidColumn = "rowid";
        private const string DefaultGeometryColumn = "geometry";
        private int _useCounter = 0;

        static SpatialiteFeatureType()
        {
            NodeType = new SpatialiteSourceType();
        }

        public static NodeType NodeType { get; private set; }

        public SpatialiteFeatureType()
            :base(typeof(SpatialiteFeatureType).Name)
		{
            GeometryColumn = DefaultGeometryColumn;
            FidColumn = DefaultFidColumn;
			Indexed = true;
		}

        // Properties
        public SQLiteConnection Connection { get; set; }

        public string FidColumn { get; set; }
        public string GeometryColumn { get; set; }
        public bool Indexed { get; set; }
        public string Sql { get; set; }
        public string TableName { get; set; }
        public bool AntiAlias { get; set; }

        private void SetParameters(SQLiteCommand cmd, Envelope window)
		{
			cmd.Parameters[0].Value = window.MinX;
			cmd.Parameters[1].Value = window.MinY;
			cmd.Parameters[2].Value = window.MaxX;
			cmd.Parameters[3].Value = window.MaxY;
		}
                        
        public string GenerateDefaultQuery()
        {
            throw new NotImplementedException();
            //var geometryExpression = Map.SRID != 0 ? string.Format("Transform({0}, {1}) ", GeometryColumn , Map.SRID) + GeometryColumn : GeometryColumn;
            //var sql = string.Format("select {0}, {1}, srid({3})\nfrom {2}",  this.FidColumn, geometryExpression, this.Table, GeometryColumn);
            //string where = null;
            
            //if (Indexed)
            //    where = "\nwhere " + SpatialiteUtil.GetAnyInteract(Table, GeometryColumn);
            //return sql + where;
        }

		public override Envelope GetBoundingBox()
		{
			if (string.IsNullOrWhiteSpace(TableName)) return null;

			using (SQLiteCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = string.Format("SELECT min(MbrMinX({0})) as minx, min(MbrMinY({0})) as miny, max(MbrMaxX({0})) as maxx,  max(MbrMaxY({0})) as maxy from {1}", this.GeometryColumn, this.TableName);
				using (SQLiteDataReader reader = cmd.ExecuteReader())
				{
					if (!reader.Read())
						throw new Exception("No data found");
					return new Envelope(reader.GetDouble(0), reader.GetDouble(2), reader.GetDouble(1), reader.GetDouble(3));
				}
			}
		}

		private SQLiteCommand GetOrCreateCommand(SQLiteConnection connection)
		{
            return _cmd ?? (_cmd = CreateCommand(connection));
		}

        private SQLiteCommand CreateCommand(SQLiteConnection connection)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = Sql ?? GenerateDefaultQuery();
            AddParameters(cmd);
            return cmd;
        }

        private static void AddParameters(SQLiteCommand cmd)
        {
            cmd.Parameters.Add("x1", DbType.Double);
            cmd.Parameters.Add("y1", DbType.Double);
            cmd.Parameters.Add("x2", DbType.Double);
            cmd.Parameters.Add("y2", DbType.Double);
        }

		public override int HitCount(Envelope window)
		{
			if(string.IsNullOrWhiteSpace(TableName) || Connection == null) return 0;

			using (SQLiteCommand cmd = this.Connection.CreateCommand())
			{
                //cmd.ExecuteReader(CommandBehavior.SchemaOnly)
                cmd.CommandText = string.Format("SELECT count(*) from {0} where {1} AND disjoint({2}, polyfromtext('POLYGON((' || @x1 || ' ' || @y1 || ',' || @x1 || ' ' || @y2 || ',' || @x2 || ' ' || @y2 || ',' || @x2 || ' ' || @y1 || ',' || @x1 || ' ' || @y1 || '))')) = 0", this.TableName, SpatialiteUtil.GetAnyInteract(TableName, GeometryColumn), GeometryColumn);
				AddParameters(cmd);
                SetParameters(cmd, window);
				return Convert.ToInt32(cmd.ExecuteScalar());
			}
		}


        
        //public override FeatureType GetFeatureType()
        //{
        //    var featureType = new FeatureType(Name);

        //    var cmd = _cmd ?? (_cmd = GetOrCreateCommand(Connection));

        //    using (var reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
        //    {
        //        var fieldCount = reader.FieldCount;
        //        for (int i = 0; i < fieldCount; i++)
        //            featureType.AddAttributes(reader.GetName(i), reader.GetFieldType(i));
        //    }

        //    return featureType;
        //}

        //public override IEnumerable<Feature> GetFeatures(Envelope window)
        //{
        //    var cmd = _useCounter > 0 ? CreateCommand(Connection) : GetOrCreateCommand(Connection);
        //    _useCounter++;

        //    try
        //    {

        //        SetParameters(cmd, window);
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            var geomOrdinal = reader.GetOrdinal(GeometryColumn);
        //            if (geomOrdinal < 0) throw new Exception("Geometry column not found: " + GeometryColumn);

        //            var fidOrdinal = string.IsNullOrEmpty(FidColumn) ? -1 : reader.GetOrdinal(FidColumn);
        //            var buffer = new byte[1024];

        //            var fieldCount = reader.FieldCount;
        //            var values = new object[fieldCount];
        //            var geoReader = new GaiaGeoReader();

        //            while (reader.Read())
        //            {
        //                long fid;
        //                if (fidOrdinal >= 0)
        //                {
        //                    fid = reader.GetInt64(fidOrdinal);
        //                    values[fidOrdinal] = fid;
        //                }
        //                else
        //                    fid = 0;

        //                for (int i = 0; i < fieldCount; i++)
        //                    if (i != fidOrdinal && i != geomOrdinal)
        //                        values[i] = reader.GetValue(i);

        //                var len = (int)reader.GetBytes(geomOrdinal, 0L, null, 0, 0);
        //                if (len > buffer.Length)
        //                    buffer = new byte[len];

        //                reader.GetBytes(geomOrdinal, 0L, buffer, 0, len);

        //                IGeometry geom;
        //                try
        //                {
        //                    geom = geoReader.Read(buffer, 0, len);
        //                }
        //                catch (GeometryException ex)
        //                {
        //                    Trace.WriteLine(string.Format("Invalid geometry while reading {0} Fid={1}: {2}", ToString(), fid, ex.Message));
        //                    continue;
        //                }

        //                //var geom = GeometryReader.ReadBlobGeometry(new MemoryStream(buffer, 0, buffer.Length));
        //                values[geomOrdinal] = geom;
        //                //featureType.GeometryType = geom.OgcGeometryType;
        //                var feature = NewFeature(fid, values);
        //                feature.Geometry = geom;

        //                yield return feature;
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        _useCounter--;
        //    }
        //}

        protected override void WriteXmlContent(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Sql))
                writer.WriteElementString(SqlField, Sql);
            base.WriteXmlContent(writer);
        }
        

        protected override void WriteXmlAttributes(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(TableName))
                writer.WriteAttributeString(TableField, TableName);
            if (!string.IsNullOrEmpty(FidColumn) && FidColumn != DefaultFidColumn)
                writer.WriteAttributeString(FidField, FidColumn);
            if (!string.IsNullOrEmpty(GeometryColumn) && GeometryColumn != DefaultGeometryColumn)
                writer.WriteAttributeString(GeomField, GeometryColumn);

            if (!Indexed)
            {
                writer.WriteStartAttribute(IndexedField);
                writer.WriteValue(Indexed);
                writer.WriteEndAttribute();
            }

            //base.WriteXmlAttributes(writer);
        }

        protected override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == SpatialiteLayer.FidField) FidColumn = reader.Value;
            else if (reader.LocalName == SpatialiteLayer.GeomField) GeometryColumn = reader.Value;
            else if (reader.LocalName == SpatialiteLayer.TableField) TableName = reader.Value;
            else if (reader.LocalName == SpatialiteLayer.IndexedField) Indexed = Convert.ToBoolean(reader.Value);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
        
        protected override bool TryReadXmlElement(XmlReader reader)
        {
            if (reader.IsStartElement(SpatialiteLayer.SqlField))
                Sql = reader.ReadElementString().Trim();
            else
                return base.TryReadXmlElement(reader);

            return true;
        }

	}
}