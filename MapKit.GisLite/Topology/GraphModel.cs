using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data.SQLite;

using System.Text.RegularExpressions;
using System.IO;
using Cyrez.SqliteUtil;

namespace MapKit.GisLite
{
	class GraphModel
	{
		//private string Path;
		private SQLiteConnection _connection;

        public GraphModel(SQLiteConnection connection)
		{
			_connection = connection;
		}

		private static void ValidateStructure(SQLiteConnection connection)
		{
			if (!SqliteUtil.TableExists(connection, "node"))
				CreateNodeTable(connection);
			else
				UpdateNodeTable(connection);

			if (!SqliteUtil.TableExists(connection, "entity"))
				CreateEntityTable(connection);
			else
				UpdateEntityTable(connection);

			if (!SqliteUtil.TableExists(connection, "edge"))
				CreateEdgeTable(connection);
			else
				UpdateEdgeTable(connection);

			//if (!SqliteUtil.TableExists(connection, "edge"))
			//    CreateEntityNodeTable(connection);
			//else
			//    UpdatEntityNodeTable(connection);

		}

		//private static void CreateEntityNodeTable(SQLiteConnection connection)
		//{
		//    using (var cmd = connection.CreateCommand())
		//    {
		//        cmd.CommandText = "CREATE TABLE entity_node (" +
		//            "entity_id NUMBER NOT NULL REFERENCES entity(id) ON DELETE CASCADE, " +
		//            "node1_pnt TEXT, " +
		//            "node_id INTEGER NOT NULL REFERENCES node(id) ON DELETE CASCADE, " +
		//             )";
		//        cmd.ExecuteNonQuery();
		//    }

		//    UpdateEntityNodeTable(connection);
		//}

		private static void CreateEntityTable(SQLiteConnection connection)
		{
			using (var cmd = connection.CreateCommand())
			{
				cmd.CommandText = "CREATE TABLE entity ("+
					"id INTEGER PRIMARY KEY AUTOINCREMENT, " +
					"name TEXT NOT NULL, " +
					"def_id integer NOT NULL, " +
					"def_type TEXT NOT NULL)";
				cmd.ExecuteNonQuery();
			}

			UpdateEntityTable(connection);
		}

		private static void CreateNodeTable(SQLiteConnection connection)
		{
			using (var cmd = connection.CreateCommand())
			{
				cmd.CommandText = @"CREATE TABLE node (id INTEGER PRIMARY KEY AUTOINCREMENT)";
				cmd.ExecuteNonQuery();
			}
			UpdateNodeTable(connection);
		}

		public IEnumerable<long> GetAdjacent(long nodeId)
		{
			using (var cmd = _connection.CreateCommand())
			{
				cmd.CommandText = @"SELECT CASE :node_id WHEN node1_id THEN node2_id ELSE node1_id END FROM edge WHERE node1_id = :node_id OR node2_id = :node_id ORDER BY weight DESC";
				cmd.Parameters.Add(new SQLiteParameter("node_id", nodeId));
				using (var reader = cmd.ExecuteReader())
					while (reader.Read())
						yield return reader.GetInt64(0);
			}
		}

		static SQLiteCommand _findEntityByNameCommand;
		static SQLiteCommand _insertEntityCommand;

		internal long GetOrCreateEntityId(string name)
		{
			if (_findEntityByNameCommand == null)
			{
				_findEntityByNameCommand = _connection.CreateCommand();
				_findEntityByNameCommand.CommandText = @"SELECT id FROM entity WHERE name = :name";
				_findEntityByNameCommand.Parameters.Add(new SQLiteParameter("name", name));
			}
			else
				_findEntityByNameCommand.Parameters[0].Value = name;

			using (var reader = _findEntityByNameCommand.ExecuteReader())
				if (reader.Read())
					return reader.GetInt64(0);

			var match = Regex.Match(name, ".*?([CDEF])([0-9]+)");
			if (!match.Success) throw new FormatException();

			if (_insertEntityCommand == null)
			{
				_insertEntityCommand = _connection.CreateCommand();
				_insertEntityCommand.CommandText = "INSERT INTO entity(name, def_type, def_id) VALUES (:name, :def_type, :def_id)";
				var parameters = new[]{
					new SQLiteParameter("name", name),
					new SQLiteParameter("def_type", match.Groups[1]),
					new SQLiteParameter("def_id", match.Groups[2])};
				_insertEntityCommand.Parameters.AddRange(parameters);
			}
			else
			{
				var parameters = _insertEntityCommand.Parameters;
				parameters[0].Value = name;
				parameters[1].Value = match.Groups[1];
				parameters[2].Value = match.Groups[2];
			}
			_insertEntityCommand.ExecuteNonQuery();
			return _connection.LastInsertRowId;
		}




        private void CreateConnectivityTable(SQLiteConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "CREATE TABLE connectivity(from_id,from_pnt,to_id,to_pnt,chan_id,flow_info)";
            }
        }

        public static void MakeDefIdUnique(SQLiteConnection connection)
        {
            var sql = @"update feature 
set def_id = (select id 
from ent_def 
where ent_def.old_id = feature.old_def_id and ent_def.def_type = feature.def_type)";

            SqliteUtil.ExecuteNonQuery(connection, sql);
        }


        private static void CreateFeatureTable(SQLiteConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "CREATE TABLE Feature (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "name TEXT NOT NULL, " +
                    "def_id integer NOT NULL, " +
                    "def_type TEXT NOT NULL)";
                cmd.ExecuteNonQuery();
            }

            UpdateEntityTable(connection);
        }

        private static void UpdateEntityTable(SQLiteConnection connection)
        {

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"CREATE UNIQUE INDEX IF NOT EXISTS ix_entity_name ON entity(name)";
                cmd.ExecuteNonQuery();
            }
        }

        private static void UpdateEdgeTable(SQLiteConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"CREATE INDEX IF NOT EXISTS ix_node1_id ON edge(node1_id)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE INDEX IF NOT EXISTS ix_node2_id ON edge(node2_id)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE INDEX IF NOT EXISTS ix_entity_id ON edge(fid)";
                cmd.ExecuteNonQuery();
            }
        }

        private static void UpdateNodeTable(SQLiteConnection connection)
        {
        }

        private static void CreateEdgeTable(SQLiteConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "CREATE TABLE edge (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "fid NUMBER NOT NULL REFERENCES feature(id) ON DELETE CASCADE, " +
                    "node1_id INTEGER NOT NULL REFERENCES node(id) ON DELETE CASCADE, " +
                    "node2_id INTEGER REFERENCES node(id) ON DELETE CASCADE, " +
                    "direction NUMBER NOT NULL DEFAULT 0, " +
                    "phase TEXT, " +
                    "state NUMBER NOT NULL DEFAULT 1, " +
                    "type NUMBER NOT NULL)";
                cmd.ExecuteNonQuery();
            }

            UpdateEdgeTable(connection);
        }
		
	}
}
