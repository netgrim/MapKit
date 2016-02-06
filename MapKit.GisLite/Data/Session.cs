using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.IO;
using Cyrez.SqliteUtil;
using MapKit.Core;
using MapKit.Spatialite;

namespace MapKit.GisLite
{
	public class Session 
	{
		private SQLiteConnection _connection;
        private SQLiteCommand _findEntityByNameCommand;
        private SQLiteCommand _insertEntityCommand;
        private SQLiteCommand _findFeatureTypeId;

        public Session(SQLiteConnection connection)
		{
			_connection = connection;
		}

		internal long GetOrCreateEntityId(string name)
		{
			if (_findEntityByNameCommand == null)
			{
				_findEntityByNameCommand = _connection.CreateCommand();
				_findEntityByNameCommand.CommandText = @"SELECT id FROM feature WHERE name = :name";
				_findEntityByNameCommand.Parameters.Add(new SQLiteParameter("name", name));
			}
			else
				_findEntityByNameCommand.Parameters[0].Value = name;

            using (var reader = _findEntityByNameCommand.ExecuteReader())
                if (reader.Read())
                    return Convert.ToInt64(reader.GetValue(0));

            return CreateEntityId(name);
		}

        public long CreateEntityId(string name)
        {
            var match = Regex.Match(name, ".*?([CDEF])([0-9]+)");
            if (!match.Success) throw new FormatException();

            if (_insertEntityCommand == null)
            {
                _insertEntityCommand = _connection.CreateCommand();
                _insertEntityCommand.CommandText = "INSERT INTO feature(name, def_type, def_id) VALUES (:name, :def_type, :def_id)";
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


        public long Connect(string entity)
        {
            //            --fill edge table Processing connectivity table
            //--we should migrate this script into connectivity insert trigger
            //DECLARE
            //  normalCnt NUMBER := 0;
            //  vlnCnt NUMBER := 0;
            //  tapCnt NUMBER := 0;
            //  node1Cnt number;
            //  node2Cnt number;
            //  fromNodeId NUMBER;
            //  toNodeId NUMBER;
            //  maxNodeId NUMBER;

            //  errorCnt NUMBER := 0;
            //  nodeAddedCnt NUMBER := 0;
            //  --delete from edge where internal <> 1
            //BEGIN
            //  FOR c IN (SELECT d1.id from_id, from_id from_name, from_pnt, d2.id to_id, to_id to_name, to_pnt, c1.id chan_id, chan_id chan_name, flow_info
            //    FROM connectivity c
            //      left JOIN entity d1 ON d1.name = from_id
            //      left JOIN entity c1 ON c1.name = chan_id
            //      left JOIN entity d2 ON d2.name = to_id)
            //  LOOP
            //    --validate some connectivity table orphans
            //    IF c.from_id IS NULL THEN
            //      DBMS_OUTPUT.Put_Line('Error: Entity ' || c.from_name || ' from Connectivity.From_id does not exists');
            //      errorCnt := errorCnt + 1;
            //    ELSIF c.to_id IS NULL THEN
            //      DBMS_OUTPUT.Put_Line('Error: Entity ' || c.to_name || ' from Connectivity.to_id does not exists');
            //      errorCnt := errorCnt + 1;
            //    ELSIF c.chan_id IS NULL AND c.chan_name <> '0' THEN
            //      DBMS_OUTPUT.Put_Line('Error: Entity ' || c.chan_name || ' from Connectivity.chan_id does not exists');
            //      errorCnt := errorCnt + 1;
            //    ELSE 
            //      --find node1
            //      fromNodeId := NULL; 
            //      SELECT min(decode(c.from_pnt, e.pnt1, e.node1_id, e.pnt2, e.node2_id)), max(decode(c.from_pnt, e.pnt1, e.node1_id, e.pnt2, e.node2_id)) INTO fromNodeId, maxNodeId FROM edge e WHERE e.entity_id = c.from_id AND c.from_pnt IN (e.pnt1, e.pnt2);
            //      IF fromNodeId <> maxNodeId THEN
            //        DBMS_OUTPUT.Put_Line('Error: mismatch ' || c.from_id || ' ' ||  c.from_pnt );
            //        errorCnt := errorCnt + 1;
            //      END IF;

            //      --find node2
            //      toNodeId := NULL;
            //      SELECT min(decode(c.to_pnt, e.pnt1, e.node1_id, e.pnt2, e.node2_id)), max(decode(c.to_pnt, e.pnt1, e.node1_id, e.pnt2, e.node2_id)) INTO toNodeId, maxNodeId FROM edge e WHERE e.entity_id = c.to_id AND c.to_pnt IN (e.pnt1, e.pnt2);
            //      IF toNodeId <> maxNodeId THEN
            //        DBMS_OUTPUT.Put_Line('Error: mismatch ' || c.to_id || ' ' ||  c.to_pnt );
            //        errorCnt := errorCnt + 1;
            //      END IF;

            //      IF fromNodeId IS NULL AND (c.chan_id IS NOT NULL OR c.from_id = c.to_id OR toNodeId IS NULL) THEN
            //        SELECT node_id_seq.NEXTVAL INTO fromNodeId FROM dual;
            //        INSERT INTO edge (id, entity_id, node1_id, pnt1)
            //        VALUES (edge_id_seq.NEXTVAL, c.from_id, fromNodeId, c.from_pnt);
            //        nodeAddedCnt := nodeAddedCnt + 1;
            //      END IF;
            //      IF toNodeId IS NULL AND (c.chan_id IS NOT NULL OR c.from_id = c.to_id)  THEN
            //        SELECT node_id_seq.NEXTVAL INTO toNodeId FROM dual;
            //        INSERT INTO edge (id, entity_id, node1_id, pnt1)
            //        VALUES (edge_id_seq.NEXTVAL, c.to_id, toNodeId, c.to_pnt);
            //        nodeAddedCnt := nodeAddedCnt + 1;
            //      END IF;

            //      IF c.chan_id IS NOT NULL THEN       --  normal, device loopback(0): owner is channel
            //        --undone: manage attached channels
            //        INSERT INTO edge (id, entity_id, node1_id, node2_id, phase, pnt1, pnt2)
            //        VALUES (edge_id_seq.NEXTVAL, c.chan_id, fromNodeId, toNodeId, c.flow_info, 'B', 'E');
            //        normalCnt := normalCnt + 1;
            //      ELSIF c.from_id = c.to_id THEN --virtual link to same device(2): owner is device
            //        INSERT INTO edge (id, entity_id, node1_id, node2_id, phase, pnt1, pnt2, internal)
            //        VALUES (edge_id_seq.NEXTVAL, c.from_id, fromNodeId, toNodeId, c.flow_info, c.from_pnt, c.to_pnt, 2);
            //        vlnCnt := vlnCnt + 1;
            //      ELSE --device tap
            //        IF fromNodeId IS NULL THEN -- no fromNode, use toNode
            //          INSERT INTO edge (id, entity_id, node1_id, pnt1)
            //          VALUES (edge_id_seq.NEXTVAL, c.from_id, toNodeId, c.from_pnt);
            //        ELSIF toNodeId IS NULL THEN -- no toNode, use fromNode
            //          INSERT INTO edge (id, entity_id, node1_id, pnt1)
            //          VALUES (edge_id_seq.NEXTVAL, c.from_id, fromNodeId, c.from_pnt);
            //        ELSE -- nodes are merged
            //          --the node with less occurence is merged into the other one
            //          SELECT COUNT(*) INTO node1Cnt FROM edge WHERE fromNodeId IN (node1_id, node2_id);
            //          SELECT COUNT(*) INTO node2Cnt FROM edge WHERE toNodeId IN (node1_id, node2_id);  
            //          IF node1Cnt < node2Cnt THEN
            //            DBMS_OUTPUT.Put_Line('Merge ' || fromNodeId || ' into ' || toNodeId);
            //            UPDATE edge SET node1_id = DECODE(node1_id, fromNodeId, toNodeId, node1_id), node2_id = DECODE(node2_id, fromNodeId, toNodeId, node2_id) WHERE fromNodeId IN (node1_id, node2_id);
            //          ELSE --replace every occurence of toNodeId by fromNodeId
            //            DBMS_OUTPUT.Put_Line('Merge ' || toNodeId || ' into ' || fromNodeId);  
            //            UPDATE edge SET node1_id = DECODE(node1_id, toNodeId, fromNodeId, node1_id), node2_id = DECODE(node2_id, toNodeId, fromNodeId, node2_id) WHERE toNodeId IN (node1_id, node2_id);
            //          END IF;
            //          tapCnt := tapCnt + 1;
            //        END IF;
            //      END IF;
            //    END IF;    
            //  END LOOP;

            //  DBMS_OUTPUT.Put_Line( 'Errors: ' || errorCnt );  
            //  DBMS_OUTPUT.Put_Line( 'Normal: ' || normalCnt );  
            //  DBMS_OUTPUT.Put_Line( 'Vln: ' || vlnCnt ); 
            //  DBMS_OUTPUT.Put_Line( 'Tap: ' || tapCnt ); 
            //  DBMS_OUTPUT.Put_Line( 'Node added: ' || nodeAddedCnt ); 
            //END;
            return 0;
        }

        public string GetChannelPhase(long chanId)
        {
            return GetFeatureAttribute(chanId, "phase");
        }

        private string GetFeatureAttribute(long chanId, string p)
        {
            var defId = GetFeatuteTypeId(chanId);
            var featureType = GetFeatureType(defId);
            return Convert.ToString(SqliteUtil.GetValue(_connection, string.Format("select phase from {0} where fid = :fid" + featureType.TableName), chanId));
        }

        private SpatialiteFeatureType GetFeatureType(long defId)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = @"SELECT name, table_name FROM end_def WHERE id = :id";
            cmd.Parameters.Add(new SQLiteParameter("defId", defId));

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    return new SpatialiteFeatureType(){ Name = Convert.ToString(reader[0]), TableName = Convert.ToString(reader[1]) };
            return null;
        }

        private long GetFeatuteTypeId(long fid)
        {
            if (_findFeatureTypeId == null)
            {
                _findFeatureTypeId = _connection.CreateCommand();
                _findFeatureTypeId.CommandText = @"SELECT def_id FROM feature WHERE id = :id";
                _findFeatureTypeId.Parameters.Add(new SQLiteParameter("id", fid));
            }
            else
                _findFeatureTypeId.Parameters[0].Value = fid;

            return (long)_findFeatureTypeId.ExecuteScalar();
        }
	}
}
