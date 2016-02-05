using System.Collections.Generic;
using System.ComponentModel;
using GeoAPI.Geometries;
using System.Xml;

namespace MapKit.Core
{

    [TypeConverter( typeof(FeatureSourceConverter))]
    [Editor(typeof(FeatureSourceEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public abstract class FeatureSource 
	{
        [Browsable(false)]
        public Map Map { get; internal set; }

		public abstract Envelope GetBoundingBox();

		public abstract IEnumerable<Feature> GetFeatures(FeatureType featureType, Layer layer, Envelope window);

		public abstract int HitCount(Envelope window, double scale);

        public abstract FeatureType GetFeatureType(Layer layer);

        public abstract void ReadXml(XmlReader reader);

        public abstract void WriteXml(XmlWriter writer);

        public virtual void Open() { }

        public virtual void Close() { }

    }
}
