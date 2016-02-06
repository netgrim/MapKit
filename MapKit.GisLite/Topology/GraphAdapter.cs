using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;

namespace MapKit.GisLite.Topology
{
    public class GraphAdapter
    {
        //private string Path;
        private SQLiteConnection _connection;
        //private SQLiteCommand _findFeatureTypeId;
        private SQLiteCommand _getEdgeWithNodeCommand;
        SQLiteCommand _getNodeId;
        SQLiteCommand _insertNodeCommand;
        SQLiteCommand _deleteEntityNode;
        SQLiteCommand _insertEdge;
        SQLiteCommand _insertSingleNodeEdgeCommand;
        SQLiteCommand _setEntityNodeCommand;

        public GraphAdapter(SQLiteConnection connection)
        {
            _connection = connection;
        }

        internal bool TryGetNodeId(long fid, string pnt, out long nodeId)
        {
            if (_getNodeId == null)
            {
                _getNodeId = _connection.CreateCommand();
                //_getNodeId.CommandText = @"SELECT MIN(node_id), MAX(node_id) FROM (SELECT CASE :pnt WHEN node1_pnt THEN node1_id ELSE node2_id END node_id FROM edge WHERE entity_id = :entity_id AND (node1_pnt = :pnt OR node2_pnt = :pnt))";
                _getNodeId.CommandText = @"SELECT node_id FROM entity_pnt_node WHERE fid = :fid AND pnt_id = :pnt";
                var parameters = new[]{
				new SQLiteParameter("fid", fid),
				new SQLiteParameter("pnt", pnt)};
                _getNodeId.Parameters.AddRange(parameters);
            }
            else
            {
                var parameters = _getNodeId.Parameters;
                parameters[0].Value = fid;
                parameters[1].Value = pnt;
            }

            using (var reader = _getNodeId.ExecuteReader())
                if (reader.Read())
                {
                    nodeId = reader.GetInt64(0);
                    return true;
                }

            nodeId = 0;
            return false;
        }

        internal long GetNewNodeId()
        {
            if (_insertNodeCommand == null)
            {
                _insertNodeCommand = _connection.CreateCommand();
                _insertNodeCommand.CommandText = "INSERT INTO node (id) VALUES (NULL)";
            }
            _insertNodeCommand.ExecuteNonQuery();
            return _connection.LastInsertRowId;
        }

        internal void DeleteEntityNode(long entityId, string pnt)
        {
            if (_deleteEntityNode == null)
            {
                _deleteEntityNode = _connection.CreateCommand();
                _deleteEntityNode.CommandText = "DELETE FROM edge WHERE entity_id = :entityId AND node1_pnt = :pnt AND node2_id IS NULL AND type = " + (int)EdgeType.Single;
                var parameters = new[] {
					new SQLiteParameter("entityId", entityId),
					new SQLiteParameter("pnt", pnt)};
                _deleteEntityNode.Parameters.AddRange(parameters);
            }
            else
            {
                var parameters = _deleteEntityNode.Parameters;
                parameters[0].Value = entityId;
                parameters[1].Value = pnt;
            }
            int cnt = _deleteEntityNode.ExecuteNonQuery();
        }

        internal long AddEdge(long fid, long node1Id, long node2Id, EdgeDirection direction, string phase, int? state, EdgeType type)
        {
            if (_insertEdge == null)
            {
                _insertEdge = _connection.CreateCommand();
                _insertEdge.CommandText = "INSERT INTO edge (fid, node1_id, node2_id, direction, state, type) VALUES (:fid, :node1_id, :node2_id, :direction, :state, :type)";
                var parameters = new[] {
					new SQLiteParameter("fid", fid),
					new SQLiteParameter("node1_id", node1Id),
					new SQLiteParameter("node2_id", node2Id),
					new SQLiteParameter("direction", direction),
					new SQLiteParameter("state", state),
					new SQLiteParameter("type", type)};
                _insertEdge.Parameters.AddRange(parameters);
            }
            else
            {
                var parameters = _insertEdge.Parameters;
                parameters[0].Value = fid;
                parameters[1].Value = node1Id;
                parameters[2].Value = node2Id;
                parameters[3].Value = direction;
                parameters[4].Value = state;
                parameters[5].Value = type;
            }

            _insertEdge.ExecuteNonQuery();
            return _connection.LastInsertRowId;
        }

        internal void SetEntityNode(long fid, string pnt, long nodeId)
        {
            if (_setEntityNodeCommand == null)
            {
                _setEntityNodeCommand = _connection.CreateCommand();
                _setEntityNodeCommand.CommandText = "INSERT OR REPLACE INTO entity_pnt_node (fid, pnt_id, node_id) VALUES (:fid, :pnt, :nodeId)";
                var parameters = new[] {
					new SQLiteParameter("fid", fid),
					new SQLiteParameter("pnt", pnt),
					new SQLiteParameter("nodeId", nodeId)};
                _setEntityNodeCommand.Parameters.AddRange(parameters);
            }
            else
            {
                var parameters = _setEntityNodeCommand.Parameters;
                parameters[0].Value = fid;
                parameters[1].Value = pnt;
                parameters[2].Value = nodeId;
            }
            var affected = _setEntityNodeCommand.ExecuteNonQuery();
            Debug.Assert(affected == 1);
        }

        internal long AddSingleNodeEdge(long entityId, long nodeId)
        {
            if (_insertSingleNodeEdgeCommand == null)
            {
                _insertSingleNodeEdgeCommand = _connection.CreateCommand();
                _insertSingleNodeEdgeCommand.CommandText = "INSERT INTO edge (fid, node1_id, type) VALUES (:fid, :nodeId, " + (int)EdgeType.Single + ")";
                var parameters = new[] {
					new SQLiteParameter("fid", entityId),
					new SQLiteParameter("nodeId", nodeId)};
                _insertSingleNodeEdgeCommand.Parameters.AddRange(parameters);
            }
            else
            {
                var parameters = _insertSingleNodeEdgeCommand.Parameters;
                parameters[0].Value = entityId;
                parameters[1].Value = nodeId;
            }
            _insertSingleNodeEdgeCommand.ExecuteNonQuery();
            long edgeId = _connection.LastInsertRowId;

            return edgeId;
        }

        internal long CountEdgeWith(long nodeId)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = @"SELECT COUNT(*) FROM edge WHERE node1_id = :nodeId OR node2_id = :nodeId";
                cmd.Parameters.Add(new SQLiteParameter("nodeId", nodeId));
                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        internal long CountEdgeWith(long fid, long nodeId)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = @"SELECT COUNT(*) FROM edge WHERE fid = :fid AND (node1_id = :nodeId OR node2_id = :nodeId)";
                cmd.Parameters.Add(new SQLiteParameter("fid", fid));
                cmd.Parameters.Add(new SQLiteParameter("nodeId", nodeId));
                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        internal int ReplaceNode(long oldNodeId, long newNodeId)
        {
            using (var cmd = _connection.CreateCommand())
            {
                //update node1
                cmd.CommandText = @"UPDATE edge SET node1_id = :newNodeId WHERE node1_id = :oldNodeId";
                cmd.Parameters.Add(new SQLiteParameter("oldNodeId", oldNodeId));
                cmd.Parameters.Add(new SQLiteParameter("newNodeId", newNodeId));
                var ret = cmd.ExecuteNonQuery();

                //update node2
                cmd.CommandText = @"UPDATE edge SET node2_id = :newNodeId WHERE node2_id = :oldNodeId";
                ret += cmd.ExecuteNonQuery();

                //update entity_pnt_node
                cmd.CommandText = @"UPDATE entity_pnt_node SET node_id = :newNodeId WHERE node_id = :oldNodeId";
                ret += cmd.ExecuteNonQuery();

                return ret;
            }
        }

        public IEnumerable<Edge> GetEdgeWithNode(long nodeId)
        {
            if (_getEdgeWithNodeCommand == null)
            {
                _getEdgeWithNodeCommand = _connection.CreateCommand();
                _getEdgeWithNodeCommand.CommandText = "SELECT id, node1_id, ifnull(node2_id, node1_id), phase, direction, state, def_id FROM edge WHERE node1_id = :node_id OR node2_id = :node_id";
                _getEdgeWithNodeCommand.Parameters.Add(new SQLiteParameter("node_id", nodeId));
            }
            else
                _getEdgeWithNodeCommand.Parameters[0].Value = nodeId;

            using (var reader = _getEdgeWithNodeCommand.ExecuteReader())
            {
                var values = new object[reader.FieldCount];
                while (reader.Read())
                {
                    reader.GetValues(values);
                    
                    var edgeId = (long)values[0];
                    
                    yield return new Edge(edgeId)
                    {
                        Node1Id = (long)values[1],
                        Node2Id = (long)values[2],
                        Phase = Convert.IsDBNull(values[3]) ? null : (string)values[3],
                        Direction = Convert.IsDBNull (values[4]) ? EdgeDirection.Both : (EdgeDirection)Convert.ToInt32(values[4]),
                        State = Convert.IsDBNull (values[5]) ? true : Convert.ToBoolean(values[5]),
                        DefId = Convert.IsDBNull (values[6]) ? 0 : Convert.ToInt32(values[6])
                    };
                }
            }
        }

        //SQLiteCommand _insertdSingleNodeEdgeCommand;
        //internal long AddSingleNodeEdge(long entityId, string pnt, long nodeId)
        //{
        //    if (_setEntityNodeCommand == null)
        //    {
        //        _setEntityNodeCommand = _connection.CreateCommand();
        //        _setEntityNodeCommand.CommandText = "INSERT INTO edge (entity_id, node1_id, type) VALUES (:entityId, :nodeId, " + (int)EdgeType.Single + ")";
        //        var parameters = new[] {
        //            new SQLiteParameter("entityId", entityId),
        //            new SQLiteParameter("nodeId", nodeId)};
        //        _setEntityNodeCommand.Parameters.AddRange(parameters);
        //    }
        //    else
        //    {
        //        var parameters = _setEntityNodeCommand.Parameters;
        //        parameters[0].Value = entityId;
        //        parameters[1].Value = nodeId;
        //    }
        //    _setEntityNodeCommand.ExecuteNonQuery();
        //    long edgeId = _connection.LastInsertRowId;

        //    SetEntityNode(entityId, pnt, nodeId);

        //    return edgeId;
        //}
    }
}