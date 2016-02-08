using System.Collections.Generic;

namespace MapKit.Core
{
    public abstract class DataSource
    {
        public string Name { get; set; }

        //[Browsable(false)]
        public DataSourceType SourceType { get; set; }

        public abstract IEnumerable<FeatureType> CreateFeatureTypes();
    }
}
