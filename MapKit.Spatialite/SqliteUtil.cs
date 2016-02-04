using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace MapKit.Spatialite
{
	public class ColumnInfo
	{
		public int Cid { get; set; }
		public string Name { get; set; }
		public string DataType { get; set; }
		public string DefaultValue { get; set; }
		public bool IsPrimaryKey { get; set; }
		public bool NotNull { get; set; }
	}

	public class IndexInfo
	{
		public IndexInfo(string name, bool unique)
		{
			Name = name;
			Unique = unique;
		}

		public string Name { get; private set; }

		public bool Unique { get; private set; }
	}

	public static class SqliteUtil
	{
		public static IEnumerable<string> GetTableNames(SQLiteConnection connection, string database = "main")
		{
			using (var cmd = connection.CreateCommand())
			{
				cmd.CommandText = "SELECT name FROM " + database + ".sqlite_master WHERE type='table'";
				using (var reader = cmd.ExecuteReader())
					while (reader.Read())
						yield return reader.GetString(0);
			}
			
		}

		public static bool TableExists(SQLiteConnection connection, string tableName)
		{
			using (var cmd = connection.CreateCommand())
			{
				cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name = :tableName";
				cmd.Parameters.Add(new SQLiteParameter("tableName", tableName));
				return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
			}
		}

		public static IEnumerable<ColumnInfo> GetColumnInfos(SQLiteConnection connection, string table)
		{
			using (var cmd = connection.CreateCommand())
			{
				cmd.CommandText = string.Format("PRAGMA table_info({0})", table);
				using (var reader = cmd.ExecuteReader())
					while (reader.Read())
						yield return new ColumnInfo()
						{
							Cid = Convert.ToInt32(reader["cid"]),
							Name = Convert.ToString(reader["name"]),
							DataType = Convert.ToString(reader["type"]),
							NotNull = Convert.ToBoolean(reader["notnull"]),
							DefaultValue = Convert.ToString(reader["dflt_value"]),
							IsPrimaryKey = Convert.ToBoolean(reader["pk"])
						};
			}
		}

		//public static IEnumerable<IndexInfo> GetIndexInfos(SQLiteConnection connection, string table)
		//{
		//    using (var cmd = connection.CreateCommand())
		//    {
		//        cmd.CommandText = string.Format("PRAGMA index_list({0})", table);
		//        using (var reader = cmd.ExecuteReader())
		//            while (reader.Read())
		//                yield return new IndexInfo()
		//                {
		//                    Seq = Convert.ToInt32(reader["seq"]),
		//                    Name = Convert.ToString(reader["name"]),
		//                    Unique = Convert.ToBoolean(reader["unique"])
		//                };
		//    }
		//}

		public static string GetValidTableName(string name)
		{
			if(string.IsNullOrEmpty(name)) return null;

			var regex = new Regex(@"(^[0-9]+|\W+|^sqlite_)");
			name = regex.Replace(name, "_");
			name = name.Trim('_');
			if (string.IsNullOrEmpty(name)) return null;
			return name;
		}

		public static string GetValidIdentifier(string name)
		{
			if (string.IsNullOrEmpty(name)) return null;

			var regex = new Regex(@"(^[0-9]+|\W+)");
			name = regex.Replace(name, "_");
			name = name.Trim('_');
			if (string.IsNullOrEmpty(name)) return null;
			return name;
		}

		public static IEnumerable<object[]> Query(SQLiteConnection connection, string sql)
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			return GetRows(cmd);
		}

		public static int ExecuteNonQuery(SQLiteConnection c, string sql)
		{
            using (var cmd = c.CreateCommand())
            {
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
		}

		public static int ExecuteScalar(SQLiteConnection c, string sql)
		{
			var cmd = c.CreateCommand();
			cmd.CommandText = sql;
			return Convert.ToInt32(cmd.ExecuteScalar());
		}

		public static IEnumerable<object[]> GetRows(SQLiteCommand cmd)
		{
			using (var reader = cmd.ExecuteReader())
			{
				var values = new object[reader.FieldCount];
				while (reader.Read())
				{
					reader.GetValues(values);
					yield return values;
				}
			}
		}

		public static IEnumerable<T> GetValues<T>(SQLiteCommand cmd, int ordinal = 0)
		{
			using (var reader = cmd.ExecuteReader())
				while (reader.Read())
					yield return (T)reader.GetValue(0);
		}

		public static bool ColumnExists(SQLiteConnection connection, string tableName, string columnName)
		{
			using (var cmd = connection.CreateCommand())
			{
				cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);
				using (var reader = cmd.ExecuteReader())
					while (reader.Read())
						if(string.Compare(Convert.ToString(reader["name"]), columnName , true )==0)
							return true;
			}
			return false;
		}

		public static IEnumerable<object> GetIndexList(SQLiteConnection connection, string tableName)
		{
			using (var cmd = connection.CreateCommand())
			{
				cmd.CommandText = string.Format("PRAGMA index_list({0})", tableName);
				using (var reader = cmd.ExecuteReader())
					while (reader.Read())
						yield return new IndexInfo((string)reader[0], Convert.ToBoolean(reader[1]));
			}
		}

        public static SQLiteConnection OpenConnection(string filename)
        {
            var builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = filename; 
            
            var connection = new SQLiteConnection(builder.ConnectionString);
            connection.Open();
            
            return connection;
        }
    }
}
