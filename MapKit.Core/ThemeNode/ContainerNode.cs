using System;
using System.Collections.Generic;
using System.ComponentModel;
using GeoAPI.Geometries;
using System.Xml;
using System.Globalization;

namespace MapKit.Core
{
    public abstract class ContainerNode : ThemeNode
	{
        internal const string NameField = "name";
        internal const string NameProperty = "Name";
        internal const string MinScaleField = "minScale";
        internal const string MaxScaleField = "maxScale";
        internal const string AntiAliasField = "antiAlias";

        internal const string NodesPropertyName = "Nodes";

        private Envelope _box;
        private string _name;
        private double? _maxScale;
        private double _minScale;

 		public ContainerNode()
		{
            var nodes = new ThemeNodeCollection<ThemeNode>(this);
            nodes.ItemAdded += Nodes_changed;
            nodes.ItemRemoved += Nodes_changed;
            Nodes = nodes;
		}

        [ParenthesizePropertyName(true)]
        [Category(Constants.CatDesign)]
        public string Name
        {
            get
            { return _name; }
            set
            {
                _name = value;
                OnFieldChanged(NameField);
            }
        }

        [Category(Constants.CatBehavior)]
        [DefaultValue(0d)]
        public double MinScale
        {
            get
            { return _minScale; }
            set
            {
                _minScale = value;
                OnFieldChanged("MinScale");
            }
        }

        [Category(Constants.CatBehavior)]
        public double? MaxScale
        {
            get
            { return _maxScale; }
            set
            {
                _maxScale = value;
                OnFieldChanged("MaxScale");
            }
        }
        
        [Browsable(false)]
        public ThemeNodeCollection<ThemeNode> Nodes { get; internal set; }

        public override Map Map
        {
            get
            {
                return base.Map;
            }
            internal set
            {
                base.Map = value;
                foreach (var node in Nodes)
                    node.Map = value;
            }
        }

        internal override bool Cascade(Style style)
        {
            foreach (var node in Nodes)
                node.Cascade(style);
            return false;
        }

		public bool IsVisibleAt(double scale)
		{
			return Visible && scale >= MinScale && (!MaxScale.HasValue || scale <= MaxScale);
		}

		[Browsable(false)]
		public virtual Envelope BoundingBox
		{
			get
			{
				if (_box == null)
				{
					_box = new Envelope();
					foreach (var child in Nodes)
					{
						var container = child as ContainerNode;
						if (container != null)
							_box.ExpandToInclude(container.BoundingBox);
					}
				}
				return _box;
			}
			set { _box = value; }
        }

        #region tune-up properties
        [Category(Constants.CatStatistics)]
        public int TotalFeatureCount { get; set; }

        [Category(Constants.CatStatistics)]
        public double AvgBoundingBoxArea { get; set; }

        [Category(Constants.CatStatistics)]
        public double MinBoundingBoxArea { get; set; }

        [Category(Constants.CatStatistics)]
        public double MaxBoundingBoxArea { get; set; }

        #endregion

        public virtual int HitCount(Envelope window, double scale)
        {
            if (!IsVisibleAt(scale)) return 0;

			int cnt = 0;
			foreach (var child in Nodes)
                if (child.Visible)
                {
                    var childContainer = child as ContainerNode;
                    if (childContainer != null)
                        cnt += childContainer.HitCount(window, scale);
                }

			return cnt;
		}

		public override object Clone()
		{
			var node = (ContainerNode)MemberwiseClone();

			node.Nodes = new ThemeNodeCollection<ThemeNode>(node);
			foreach (var child in Nodes)
				node.Nodes.Add((ThemeNode)child.Clone());

			return node;
		}

        protected internal override void WriteXmlContent(XmlWriter writer)
        {
            foreach (var node in Nodes)
                node.WriteXml(writer);
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Name))
                writer.WriteAttributeString(NameField, Name);

            if (MinScale > 0)
            {
                writer.WriteStartAttribute(MinScaleField);
                writer.WriteValue(MinScale);
                writer.WriteEndAttribute();
            }

            if (MaxScale.HasValue)
            {
                writer.WriteStartAttribute(MaxScaleField);
                writer.WriteValue(MaxScale.Value);
                writer.WriteEndAttribute();
            }

            base.WriteXmlAttributes(writer);
        }

        protected virtual void Nodes_changed(object sender, ItemEventArgs<ThemeNode> e)
        {
            if (Map != null)
                Map.Change();
        }

        public virtual bool CanContains(ThemeNode node)
        {
            return true;
        }

        public override IEnumerator<ThemeNode> GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == Group.NameField) Name = reader.Value;
            else if (reader.LocalName == MinScaleField) MinScale = double.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
            else if (reader.LocalName == MaxScaleField) MaxScale = double.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }

        public override void ReadXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
                if (!TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();

            if (!reader.IsEmptyElement)
            {
                reader.Read();
                ReadXmlContent(reader);
            }

            reader.Read();
        }

        protected internal void ReadXmlContent(XmlReader reader)
        {
            if(Map == null) throw new InvalidOperationException("Map is null");

            while (reader.MoveToContent() != XmlNodeType.EndElement)
            {
                if (!TryReadXmlElement(reader))
                    MapXmlReader.HandleUnexpectedElement(reader.LocalName);
            }
        }

        protected virtual bool TryReadXmlElement(XmlReader reader)
        {
            NodeType nodeType;
            if (Map.NodeTypes.TryGetValue(reader.LocalName, out nodeType))
            {
                ThemeNode node = (ThemeNode)Activator.CreateInstance(nodeType.Type);
                if (CanContains(node))
                {
                    Nodes.Add(node);
                    node.ReadXml(reader);
                    return true;
                }
            }
            return false;
        }

        public override void PerformNodePathChanged()
        {
            base.PerformNodePathChanged();
            foreach (var child in Nodes)
                child.PerformNodePathChanged();
        }

        public override string GenerateNodeName()
        {
            return Name ?? base.GenerateNodeName();
        }

        protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnNotifyPropertyChanged(e);
            if (e.PropertyName == ThemeNode.LabelProperty
                || (e.PropertyName == ContainerNode.NameProperty
                    && string.IsNullOrWhiteSpace(Label)))
                OnNotifyPropertyChanged(ThemeNode.LabelOrDefaultProperty);
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetNodeType().ElementName);
            WriteXmlAttributes(writer);
            WriteXmlContent(writer);
            writer.WriteEndElement();
        }
    }
}

