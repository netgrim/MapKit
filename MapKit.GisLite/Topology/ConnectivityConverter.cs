using System.Data.SQLite;
using System;
using System.Diagnostics;
using Cyrez.SqliteUtil;

namespace MapKit.GisLite.Topology
{
	public class ConnectivityConverter
	{
        //private readonly SQLiteConnection _connection;
		int _channelCnt = 0;
		int _deviceCnt = 0;
		int _tapCnt = 0;
		int _nodeCreated = 0;
		int _cnt = 0;
        private Session _session;
        internal GraphAdapter _adapter { get; set; }

		public ConnectivityConverter(GraphAdapter adapter, Session session)
		{
            _adapter = adapter;
            _session = session;
		}

		public void ConvertFromConnectivityCSV(string path)
		{
			var cnt = 0;
			using (var reader = new CsvReader(path))
				while (reader.Read())
				{
					cnt++;
					var fromName = Convert.ToString(reader["from_id"]);
					var toName = Convert.ToString(reader["to_id"]);
					var chanName = Convert.ToString(reader["chan_id"]);

                    var fromId = _session.GetOrCreateEntityId(fromName);
                    var toId = fromName == toName ? fromId : _session.GetOrCreateEntityId(toName);
					if (chanName != "0")
                        _session.GetOrCreateEntityId(chanName);
					
					if (cnt % 1000 == 0)
						Console.WriteLine(cnt);
				}
		}


        public static void ConnectivityToEdge(SQLiteConnection connection)
        {
            var converter = new ConnectivityConverter(new GraphAdapter(connection), new Session(connection));
            var transaction = connection.BeginTransaction();

            try
            {
                var _session = new Session(connection);
                var cnt = 0;
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"select c.*, f1.id as from_fid, f2.id as to_fid, f3.id as chan_fid
from connectivity c 
left join feature f1 on f1.name = c.from_id 
left join feature f2 on f2.name = c.to_id
left join feature f3 on f3.name = c.chan_id
";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var fromFidOrdinal = reader.GetOrdinal("from_fid");
                        var toFidOrdinal = reader.GetOrdinal("to_fid");
                        var chanFidOrdinal = reader.GetOrdinal("chan_fid");

                        while (reader.Read())
                        {
                            cnt++;

                            var fromFid = reader.IsDBNull(fromFidOrdinal)
                                ? _session.CreateEntityId(Convert.ToString(reader["from_id"]))
                                : Convert.ToInt64(reader.GetValue(fromFidOrdinal));

                            var toFid = reader.IsDBNull(toFidOrdinal)
                                ? _session.CreateEntityId(Convert.ToString(reader["to_id"]))
                                : Convert.ToInt64(reader.GetValue(toFidOrdinal));

                            long? chanFid;
                            if (!reader.IsDBNull(chanFidOrdinal))
                                chanFid = Convert.ToInt64(reader[chanFidOrdinal]);
                            else
                            {
                                var chanId = Convert.ToString(reader["chan_id"]);
                                chanFid = chanId != "0"
                                    ? _session.CreateEntityId(chanId)
                                    : new long?();
                            }

                            converter.LoadConnectivityRow(
                                fromFid,
                                Convert.ToString(reader["from_pnt"]),
                                toFid,
                                Convert.ToString(reader["to_pnt"]),
                                chanFid,
                                Convert.ToString(reader["flow_info"]));

                            if (cnt % 100 == 0)
                            RefreshStats(converter, cnt);
                        }
                    }
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            converter.PrintOutputs();
        }

        private static void RefreshStats(ConnectivityConverter converter, int cnt)
        {
            Console.Clear();
            Console.WriteLine("Connectivity Row processed: " + cnt);
            converter.PrintStats(cnt);
        }

        private void PrintStats(int cnt)
        {
            Console.WriteLine("Devices: " + _deviceCnt);
            Console.WriteLine("Channels: " + _channelCnt);
            Console.WriteLine("tap: " + _tapCnt);
            Console.WriteLine("node created: " + _nodeCreated);
        }

		public void LoadConnectivity(string path)
		{
			using (var reader = new CsvReader(path))
				while (reader.Read())
				{
					var fromName = Convert.ToString(reader["from_id"]);
					var fromPnt = Convert.ToString(reader["from_pnt"]);
					var toName = Convert.ToString(reader["to_id"]);
					var toPnt = Convert.ToString(reader["to_pnt"]);
                    var chanName = Convert.ToString(reader["chan_id"]);
                    var phase = Convert.ToString(reader["chan_id"]);

					LoadConnectivityRow(fromName, fromPnt, toName, toPnt, chanName, phase);

					_cnt++;
					if (_cnt % 1000 == 0)
						Console.WriteLine(_cnt);
				}

            PrintOutputs();
		}

        private void PrintOutputs()
        {
            Console.WriteLine("normalCnt :" + _channelCnt);
            Console.WriteLine("vlnCnt :" + _deviceCnt);
            Console.WriteLine("tapCnt :" + _tapCnt);
            Console.WriteLine("nodeCreated :" + _nodeCreated);
        }

		private void LoadConnectivityRow(string fromName, string fromPnt, string toName, string toPnt, string chanName, string phase)
		{
            var fromId = _session.GetOrCreateEntityId(fromName);
            var toId = fromName == toName ? fromId : _session.GetOrCreateEntityId(toName);

            long? chanId = chanName != null && chanName != "0"
                ? _session.CreateEntityId(chanName)
                : new long?();
            LoadConnectivityRow(fromId, fromPnt, toId, toPnt, chanId, phase);
		}

        private void LoadConnectivityRow(long fromId, string fromPnt, long toId, string toPnt, long? chanId, string phase)
        {

            //Debug.WriteLine(string.Concat(fromName, ", ", fromId, ", ", fromPnt, ", ", toName, ", ", toId, ", ", toPnt, ", ", chanName));
            //Console.Write(".");

            if (chanId.HasValue) //normal channel
            {
                long fromNodeId = GetOrCreateNode(fromId, fromPnt);
                long toNodeId = GetOrCreateNode(toId, toPnt);
                //Debug.WriteLine(string.Concat("Create edge: ChanId= ", chanId, ", FromNode=", fromNodeId , " ToNode=" ,toNodeId));
                _adapter.AddEdge(chanId.Value, fromNodeId, toNodeId, EdgeDirection.Both, phase ?? _session.GetChannelPhase(chanId.Value), 1, EdgeType.Channel);
                _channelCnt++;
            }
            else if (fromId == toId) //owner is device, vln to self, internal connectivity
            {
                if (fromPnt == toPnt) return;
                long fromNodeId = GetOrCreateNode(fromId, fromPnt);
                long toNodeId = GetOrCreateNode(toId, toPnt);

                //Debug.WriteLine(string.Concat("Create edge: DevId= ", fromId, ", FromNode=", fromNodeId, " ToNode=", toNodeId));
                _adapter.AddEdge(fromId, fromNodeId, toNodeId, EdgeDirection.Both, phase, null, EdgeType.Device);
                _deviceCnt++;
            }
            else //device tap
            {
                long fromNodeId, toNodeId;
                if (_adapter.TryGetNodeId(fromId, fromPnt, out fromNodeId)) //from node exist
                {
                    if (_adapter.TryGetNodeId(toId, toPnt, out toNodeId)) //both node exists
                    {
                        // the node with less occurence is merged into the other one
                        var node1Cnt = _adapter.CountEdgeWith(fromNodeId);
                        var node2Cnt = _adapter.CountEdgeWith(toNodeId);

                        if (node1Cnt < node2Cnt)
                        {
                            _adapter.ReplaceNode(fromNodeId, toNodeId);
                            fromNodeId = toNodeId;
                        }
                        else //replace every occurence of toNodeId by fromNodeId
                        {
                            _adapter.ReplaceNode(toNodeId, fromNodeId);
                            toNodeId = fromNodeId;
                        }

                        if (_adapter.CountEdgeWith(toId, toNodeId) == 0)
                            _adapter.AddSingleNodeEdge(toId, toNodeId);
                    }
                    else // only first node exists
                    {
                        _adapter.SetEntityNode(toId, toPnt, fromNodeId);
                        _adapter.AddSingleNodeEdge(toId, fromNodeId);
                    }

                    if (_adapter.CountEdgeWith(fromId, fromNodeId) == 0)
                        _adapter.AddSingleNodeEdge(fromId, fromNodeId);
                }
                else //first node is null
                {
                    if (_adapter.TryGetNodeId(toId, toPnt, out toNodeId)) //only second node exists
                    {
                        //only first node is null... use second
                        _adapter.SetEntityNode(fromId, fromPnt, toNodeId);
                        _adapter.AddSingleNodeEdge(fromId, toNodeId);

                        if (_adapter.CountEdgeWith(toId, toNodeId) == 0)
                            _adapter.AddSingleNodeEdge(toId, toNodeId);
                    }
                    else //both node are null
                    {
                        toNodeId = CreateNode(toId, toPnt);
                        _adapter.AddSingleNodeEdge(toId, toNodeId);

                        _adapter.SetEntityNode(fromId, fromPnt, toNodeId);
                        _adapter.AddSingleNodeEdge(fromId, toNodeId);
                    }
                }

                _tapCnt++;
            }
        }

		private long GetOrCreateNode(long entityId, string pnt)
		{
			long nodeId;
            if (_adapter.TryGetNodeId(entityId, pnt, out nodeId))
				return nodeId;
			return CreateNode(entityId, pnt);
		}

		private long CreateNode(long entityId, string pnt)
		{
            var nodeId = _adapter.GetNewNodeId();
			_nodeCreated++;
            _adapter.SetEntityNode(entityId, pnt, nodeId);
			return nodeId;
		}

    }
}
