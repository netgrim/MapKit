using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using MapKit.UI;
using Cyrez.SqliteUtil;
using MapKit.GisLite.Topology;
using System.Data;
using MapKit.Core;
using System.Diagnostics;

namespace MapKit.GisLite
{
    public class TraceController
    {
        SQLiteConnection _connection;
        private BreadFirstSearch _tracer;
        private TraceDialog _dialog;
        private  IDbCommand _updateEdgeStatement;
        private ConnectivityContext _context;

        public TraceController()
        {
            _connection = SqliteUtil.OpenConnection(@"H:\Data\Spatialite\sppc.sqlite");
        }

        public TraceController(SQLiteConnection connection)
        {
            _connection = connection;

            _updateEdgeStatement = CreateUpdateEdgeStatement(_connection);
            _context = new ConnectivityContext(_connection);
        }

        private IDbCommand CreateUpdateEdgeStatement(SQLiteConnection connection)
        {
            throw new NotImplementedException();
            //var cmd = connection.CreateCommand();
            //cmd.CommandText = "update edge set visited";
        }

        public TraceDialog Dialog
        {
            get
            {
                if (_dialog == null)
                {
                    _dialog = new TraceDialog();
                    _dialog.EnableNext(false);
                    _dialog.EnableStart(true);
                    _dialog.StartClicked += new EventHandler(_dialog_StartClicked);
                }
                return _dialog;
            }
        }

        void _dialog_StartClicked(object sender, EventArgs e)
        {
            _dialog.EnableStart(false);
            _tracer = new BreadFirstSearch(_dialog.StartNodeId, new GraphAdapter(_connection));

            while (_tracer.MoveNext())
            {
                MarkVisited(_tracer.Current.Edge);
                NotifyVisited(_tracer.Current);
            }
        }

        private void NotifyVisited(Frame frame)
        {
        }

        private void MarkVisited(Edge e)
        {
            var featureType = _context.FeatureTypes.Find(e.DefId);

            var cmd = _connection.CreateCommand();
            cmd.CommandText = "update " + featureType.TableName + " set visited=1";

            var cnt = cmd.ExecuteNonQuery();
            Debug.Assert(cnt == 1);
        }
    }
}
