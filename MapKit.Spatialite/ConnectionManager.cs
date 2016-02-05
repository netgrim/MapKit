using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using Cyrez.SqliteUtil;

namespace MapKit.Spatialite
{
    class ConnectionManager
    {
        private Dictionary<string, SQLiteConnection> _connections;

        private static ConnectionManager _default;

        public static ConnectionManager Default
        {
            get
            {
                if (_default == null)
                    _default = new ConnectionManager();
                return _default;
            }
        }

        public SQLiteConnection GetConnection(string file)
        {
            if (_connections == null)
                _connections = new Dictionary<string, SQLiteConnection>(StringComparer.CurrentCultureIgnoreCase);
            
            SQLiteConnection connection;
            if (!_connections.TryGetValue(file, out connection))
                _connections.Add(file, connection = SpatialiteUtil.OpenConnection(file));
            //else if (connection.State != System.Data.ConnectionState.Open)
            //    connection.Open();

            return connection;
        }

        public IEnumerable<SQLiteConnection> Connections
        {
            get
            {
                if (_connections != null)
                    return _connections.Values;
                return new SQLiteConnection[] { };
            }
        }

        public IEnumerable<string> Filenames
        {
            get
            {
                if (_connections != null)
                    return _connections.Keys;
                return new string[] { };
            }
        }
    }
}
