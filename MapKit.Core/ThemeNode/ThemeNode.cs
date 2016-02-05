using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("{GetType().Name}: Label = {Label}, Path={NodePath}")]
    public abstract class ThemeNode : ICloneable, IEnumerable<ThemeNode>, IXmlSerializable, INotifyPropertyChanged
	{
        const string pathSeparator = "\\";
        internal const string VisibleField = "visible";
        internal const string VisiblePropertyName = "Visible";
        internal const string ParentPropertyName = "Parent";
        internal const string LabelField = "label";
        internal const string LabelProperty = "Label";
        public const string LabelOrDefaultProperty = "LabelOrDefault";

        private ContainerNode _parent;
        private bool _visible = true;
        private string _label;

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler FieldChanged;
        public event EventHandler NodePathChanged;

        [Browsable(false)]
        public virtual ContainerNode Parent
        {
            get { return _parent; }
            internal set
            {
                if (_parent == value) return;
                
                _parent = value;
                Map = _parent != null ? _parent.Map : null;
                OnNotifyPropertyChanged(ParentPropertyName);
                PerformNodePathChanged();
            }
        }

        [Browsable(false)]
        public IBaseRenderer Renderer { get; set; }
		
        [Browsable(false)]
		public virtual Map Map { get; internal set; }

        public virtual string GenerateNodeName()
        {
            return GetType().Name;
        }

        [Browsable(false)]
        public string LabelOrDefault
        {
            get { return Label ?? GenerateNodeName(); }
        }

        public string Label
        {

            get { return _label; }
            set
            {
                _label = value;
                OnFieldChanged(LabelProperty);
            }
        }

        [DefaultValue(true)]
        [Category(Constants.CatBehavior)]
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    OnFieldChanged(VisiblePropertyName);
                }
            }
        }

        [Browsable(false)]
		public string NodePath
		{
			get
			{
				var stack = new Stack<string>();

				var node = Parent;
				while(node != null)
				{
					stack.Push(node.LabelOrDefault);
					node = node.Parent;
				}

				var sb = new StringBuilder();
				while(stack.Count > 0)
					sb.Append(pathSeparator).Append(stack.Pop());

                sb.Append(pathSeparator).Append(LabelOrDefault);

				return sb.ToString();
			}
		}

        [Browsable(false)]
        public abstract NodeType GetNodeType();
        
        protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        //todo constant
        protected virtual void OnNotifyPropertyChanged(string propertyName)
        {
            OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnFieldChanged(PropertyChangedEventArgs e)
        {
            OnNotifyPropertyChanged(e);
            if (FieldChanged != null)
                FieldChanged(this, e);
            if (Map != null)
                Map.Change();
        }

        //todo constant
        protected void OnFieldChanged(string propertyName)
        {
            OnFieldChanged(new PropertyChangedEventArgs(propertyName));
        }

        public virtual void PerformNodePathChanged()
        {
            OnNodePathChanged(EventArgs.Empty);
        }

        protected virtual void OnNodePathChanged(EventArgs e)
        {
            if (NodePathChanged != null)
                NodePathChanged(this, e);
        }

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }

        internal virtual bool Cascade(Style style) { return true; }

#region IXmlSerializable implementation

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetNodeType().ElementName);
            WriteXmlAttributes(writer) ;
            writer.WriteEndElement();
        }

        public virtual void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            if (reader.NodeType != XmlNodeType.Element || reader.LocalName != GetNodeType().ElementName)
                MapXmlReader.HandleUnexpectedElement(reader.LocalName);

            while (reader.MoveToNextAttribute())
                if (!TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();

            if (!reader.IsEmptyElement && (!reader.Read() || reader.MoveToContent() != XmlNodeType.EndElement))
                MapXmlReader.HandleUnexpectedElement(reader.LocalName);

            reader.Read();
        }

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

#endregion 

        protected internal virtual bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == LabelField) Label = reader.Value;
            else if (reader.LocalName == VisibleField) Visible = bool.Parse(reader.Value);
            else return false;
            return true;
        }

        protected internal virtual void WriteXmlContent(XmlWriter writer)
        {}

        protected internal virtual void WriteXmlAttributes(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Label))
                writer.WriteAttributeString(LabelField, Label);

            if (!Visible)
            {
                writer.WriteStartAttribute(VisibleField);
                writer.WriteValue(Visible);
                writer.WriteEndAttribute();
            }
        }

        public virtual IEnumerator<ThemeNode> GetEnumerator()
        {
            yield break;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
