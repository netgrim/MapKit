using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using GeoAPI.Geometries;
using Ciloci.Flee;
using System.Xml;
using System.ComponentModel;
using System.Globalization;

namespace MapKit.Core
{
    [DebuggerDisplay("Layer: Name = {Name}, Path={NodePath}")]
    public abstract class Layer : GroupItem
	{
        private const string MaxRenderField = "maxRender";
        internal const string ElementName = "layer";
        
		public Layer()
		{
			MaxRender = int.MaxValue;
        }


        [Browsable(false)]
        public override Map Map
        {
            get { return base.Map; }
            internal set
            {
                base.Map = value;
            }
        }

        [Category(Constants.CatBehavior)]
		public int MaxRender { get; set; }

        public abstract Envelope GetBoundingBox();

        public abstract IEnumerable<Feature> GetFeatures(FeatureType featureType, Layer layer, Envelope window);

        //public abstract int HitCount(Envelope window, double scale);

        public abstract FeatureType GetFeatureType();

        public virtual void Open() { }

        public virtual void Close() { }


		public override object Clone()
		{
			var layer = (Layer)MemberwiseClone();

            layer.Nodes = new ThemeNodeCollection<ThemeNode>(layer);
			foreach (var node in Nodes)
				layer.Nodes.Add((ThemeNode)node.Clone());

			return layer;
		}

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);

            if (MaxRender != int.MaxValue)
            {
                writer.WriteStartAttribute(MaxRenderField);
                writer.WriteValue(MaxRender);
                writer.WriteEndAttribute();
            }
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == Layer.NameField) Name = reader.Value;
            else if (reader.LocalName == Layer.MaxRenderField) MaxRender = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
            else if (reader.LocalName == Layer.AntiAliasField) SmoothingMode = bool.Parse(reader.Value);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
}


