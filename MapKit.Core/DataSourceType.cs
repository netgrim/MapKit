using System;

namespace MapKit.Core
{
    public abstract class DataSourceType : NodeType
    {
        public DataSourceType(string name, string elementName, Type type)
            : base(name, elementName, type)
        {
        }

        public abstract DataSource CreateDataSource();
    }
}
