using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MapKit.Core
{
    public abstract class DataSourceType : NodeType
    {
        public DataSourceType(string name, string elementName, Type type)
            : base(name, elementName, type)
        {
        }
    }
}
