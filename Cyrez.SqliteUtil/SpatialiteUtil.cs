using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace Cyrez.SqliteUtil
{

	public static class SpatialiteUtil
	{
        public const string PointType = "POINT";
        public const string LineStringType = "LINESTRING";
        public const string PolygonType = "POLYGON";
        public const string MultiPointType = "MULTIPOINT";
        public const string MultiLineStringType = "MULTILINESTRING";
        public const string MultiPolygonType = "MULTIPOLYGON";
        public const string GeometryCollectionType = "GEOMETRYCOLLECTION";

		public static IEnumerable<string> GetSpatialTable(SQLiteConnection connection, string database = "main")
		{
			foreach (var row in SqliteUtil.Query(connection, "SELECT DISTINCT(f_table_name) FROM " + database + ".geometry_columns"))
				yield return (string)row[0];
		}

        public static void InitSpatial(SQLiteConnection c)
        {
            SqliteUtil.ExecuteNonQuery(c, "SELECT load_extension('libspatialite-2.dll')");
        }

		public static IEnumerable<string> GetSpatialColumns(SQLiteConnection connection, string tableName, string database = "main")
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = "SELECT f_geometry_column FROM " + database + ".geometry_columns WHERE UPPER(f_table_name) = @tablename";
			cmd.Parameters.Add("tableName", System.Data.DbType.String).Value = tableName.ToUpper();

			return SqliteUtil.GetValues<string>(cmd);
		}

		public static SpatialColumnInfo GetColumnInfo(SQLiteConnection connection, string tableName, string columnName, string database = "main")
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = "SELECT f_table_name, f_geometry_column, type, coord_dimension, srid, spatial_index_enabled FROM " + database + ".geometry_columns WHERE UPPER(f_table_name) = @tablename AND UPPER(f_geometry_column) = @columnName";
			cmd.Parameters.Add("tableName", System.Data.DbType.String).Value = tableName.ToUpper();
			cmd.Parameters.Add("columnName", System.Data.DbType.String).Value = columnName.ToUpper();

			foreach (var row in SqliteUtil.GetRows(cmd))
			{
				var info = new SpatialColumnInfo();
				info.TableName = (string)row[0];
				info.ColumnName = (string)row[1];
				info.Type = (string)row[2];
				info.Dimension = row[3] is string ? ((string)row[3]).Length : Convert.ToInt32(row[3]);
				info.SRID = Convert.ToInt32(row[4]);
				info.SpatialIndex = (SpatialIndexMode)Convert.ToInt32(row[5]);
				return info;
			}
			return null;
		}

		public static bool CreateSpatialIndex(SQLiteConnection connection,  string table, string column)
		{
			var sql = "SELECT CreateSpatialIndex('" + table + "', '" + column + "')";
			if (Convert.ToInt32(SqliteUtil.ExecuteScalar(connection, sql)) != 1)
				throw new Exception("Failed to create spatial index");
			Console.WriteLine("Spatial index created.");
			return true;
		}

		public static bool CreateMbrCache(SQLiteConnection connection, string table, string column)
		{
			var sql = "SELECT CreateMbrCache('" + table + "', '" + column + "')";
			return SqliteUtil.ExecuteScalar(connection, sql) == 1;
		}

		public static bool DisableSpatialIndex(SQLiteConnection connection, string table, string column)
		{
			var sql = "SELECT DisableSpatialIndex('" + table + "', '" + column + "')";
			if (Convert.ToInt32(SqliteUtil.ExecuteScalar(connection, sql)) != 1)
				throw new Exception("Failed to disable spatial index");

			SqliteUtil.ExecuteNonQuery(connection, "drop table idx_" + table + "_" + column);

			Console.WriteLine("Spatial index disabled.");
			return true;
		}

		public static bool DiscardSpatialIndex(SQLiteConnection connection, string table, string column)
		{
			var sql = "SELECT DiscardSpatialIndex('" + table + "', '" + column + "')";
			if (Convert.ToInt32(SqliteUtil.ExecuteScalar(connection, sql)) != 1)
				throw new Exception("Failed to drop spatial index");
			Console.WriteLine("Spatial index drop.");
			return true;
		}

		public static bool RecoverGeometryColumn(SQLiteConnection connection, string table, string column, int srid, string geometryType, string dimension)
		{
			var sql = string.Format("SELECT RecoverGeometryColumn('{0}', '{1}', {2}, '{3}', '{4}')", table, column, srid, geometryType, dimension);
			if (Convert.ToInt32(SqliteUtil.ExecuteScalar(connection, sql)) == 1)
			{
				Console.WriteLine("Geometry column recovered.");
				return true;
			}

			Console.WriteLine("Failed to recover geometry column");
			return false;
		}

		public static bool RebuildGeometryTriggers(SQLiteConnection connection, string table, string column)
		{
			var sql = string.Format("SELECT RebuildGeometryTriggers('{0}', '{1}')", table, column);
			if (Convert.ToInt32(SqliteUtil.ExecuteScalar(connection, sql)) == 1)
			{
				Console.WriteLine("Geometry triggers rebuilt.");
				return true;
			}
			
			Console.WriteLine("Failed to rebuild geometry trigger");
			return false;
		}

        public static SQLiteConnection OpenConnection(string filename)
        {
            var connection = SqliteUtil.OpenConnection(filename);
            InitSpatial(connection);
            return connection;
        }

        public static string GetAnyInteract(string tableName, string columnName)
        {
            return string.Format("rowid in (SELECT pkid FROM idx_{0}_{1} WHERE xmin < @x2 AND xmax > @x1 AND ymin < @y2 AND ymax > @y1)", tableName, columnName);
        }
	}

}
