using System.Data.Common;
namespace MapKit.Core
{
    public abstract class DBConnectionDataSource : DataSource
    {
        public virtual DbConnection Connection
        {
            get;
            protected set;
        }
    }
}
