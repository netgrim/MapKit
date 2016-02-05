using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace MapKit.Core
{
    [DebuggerDisplay("Map('{Name}')")]
    public class Map : Group, ICloneable
    {
        private const string ElementName = "map";
        internal const string AngleField = "angle";
        internal const string BgColorField = "bgColor";
        internal const string BaseField = "base";
        internal const string CenterXField = "centerX";
        internal const string CenterYField = "centerY";
        internal const string ZoomField = "zoom";
        internal const string SridField = "srid";

        public event EventHandler Changed;
  
        public Map()
        {
            Initialize();
            base.Map = this;

            NodeTypes = new Dictionary<string, NodeType>()
            {
                {Case.NodeType.ElementName, Case.NodeType},
                {Filter.NodeType.ElementName, Filter.NodeType},
                {Group.NodeType.ElementName, Group.NodeType},
                //{Layer.NodeType.ElementName, Layer.NodeType},
                {LabelBox.NodeType.ElementName, LabelBox.NodeType},
                {LinearCalibration.NodeType.ElementName, LinearCalibration.NodeType},
                {LineStyle.NodeType.ElementName, LineStyle.NodeType},
                {LineOffset.NodeType.ElementName, LineOffset.NodeType},
                {Marker.NodeType.ElementName, Marker.NodeType},
                {PointExtractor.NodeType.ElementName, PointExtractor.NodeType},
                {PointStyle.NodeType.ElementName, PointStyle.NodeType},
                {SolidFill.NodeType.ElementName, SolidFill.NodeType},
                {SolidFillStyle.NodeType.ElementName, SolidFillStyle.NodeType},
                {Stroke.NodeType.ElementName, Stroke.NodeType},
                {Macro.NodeType.ElementName, Macro.NodeType},
                {Text.NodeType.ElementName, Text.NodeType},
                {TextStyle.NodeType.ElementName, TextStyle.NodeType},
                {Variable.NodeType.ElementName, Variable.NodeType},
                {VerticesEnumerator.NodeType.ElementName, VerticesEnumerator.NodeType},
                {Window.ElementName, new NodeType(Window.ElementName, typeof(Window))},
                {Run.NodeType.ElementName, Run.NodeType}};
        }

        private void Initialize()
        {
            BackgroundColor = Color.White;
            Zoom = 1;
            Name = GetType().Name;
        }

        #region properties

        public IDictionary<string, NodeType> NodeTypes;

        [Category(Constants.CatAppearance)]
        public Color BackgroundColor { get; set; }

        [Category(Constants.CatData)]
        public string Base { get; set; }

        [Category(Constants.CatData)]
        public int SRID { get; set; }

        #endregion

        protected void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        internal void Change()
        {
            OnChanged(EventArgs.Empty);
        }

        public override object Clone()
        {
            var map = new Map();
            map.BackgroundColor = BackgroundColor;
            map.Base = Base;

            foreach (var node in Nodes)
                map.Nodes.Add((ThemeNode)node.Clone());

            return map;
        }

        [Category(Constants.CatLayout)]
        public Double CenterX { get; set; }

        [Category(Constants.CatLayout)]
        public Double CenterY { get; set; }

        [Category(Constants.CatLayout)]
        public Double Zoom { get; set; }

        [Category(Constants.CatLayout)]
        public Double Angle { get; set; }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(ElementName, "http://www.cyrez.com/theme");
            WriteXmlAttributes(writer);
            WriteXmlContent(writer);
            writer.WriteEndElement();
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if (BackgroundColor.ToArgb() != Color.White.ToArgb())
            {
                writer.WriteStartAttribute(BgColorField);
                writer.WriteValue(BackgroundColor.IsNamedColor ? BackgroundColor.Name : ColorTranslator.ToHtml(BackgroundColor));
                writer.WriteEndAttribute();
            }

            if (CenterX != 0)
            {
                writer.WriteStartAttribute(CenterXField);
                writer.WriteValue(CenterX);
                writer.WriteEndAttribute();
            }

            if (CenterY != 0)
            {
                writer.WriteStartAttribute(CenterYField);
                writer.WriteValue(CenterY);
                writer.WriteEndAttribute();
            }

            if (Zoom != 1)
            {
                writer.WriteStartAttribute(ZoomField);
                writer.WriteValue(Zoom);
                writer.WriteEndAttribute();
            }

            if (Angle != 0)
            {
                writer.WriteStartAttribute(AngleField);
                writer.WriteValue(Angle);
                writer.WriteEndAttribute();
            }

            if (SRID > 0)
            {
                writer.WriteStartAttribute(SridField);
                writer.WriteValue(SRID);
                writer.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(Base))
                writer.WriteAttributeString(BaseField, Base);

            base.WriteXmlAttributes(writer);
        }

        public void WriteXml(string filename)
        {
            using (var writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                writer.IndentChar = ' ';
                writer.Indentation = 2;
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                WriteXml(writer);
                writer.WriteEndDocument();
            }
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            switch (reader.LocalName)
            {
                case Map.BgColorField:
                    Color parsedColor;
                    if(Util.TryParseColor(reader.Value, out parsedColor))
                        BackgroundColor = parsedColor; //MapXmlReader.DecodeColor(reader.Value);;
                    break;
                case Map.BaseField:
                    Base = Path.IsPathRooted(reader.Value)
                        ? reader.Value
                        : Path.Combine(Base, reader.Value);

                    break;
                case Map.CenterXField:
                    CenterX = double.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                    break;
                case Map.CenterYField:
                    CenterY = double.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                    break;
                case ZoomField:
                    Zoom = double.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                    break;
                case Map.SridField:
                    SRID = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                    break;
                case Map.AngleField:
                    Angle = double.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                    break;
                case Map.AntiAliasField:
                    SmoothingMode = bool.Parse(reader.Value);
                    break;
                case Map.NameField:
                    Name = reader.Value;
                    break;
                case "xmlns": break;
                default:
                    return base.TryReadXmlAttribute(reader);
            }
            return true;
        }

        public override void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            if (!reader.IsStartElement(Map.ElementName))
            {
                MapXmlReader.HandleUnexpectedElement(reader.LocalName);
                return;
            }

            while (reader.MoveToNextAttribute())
                if (!TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();
            if (!reader.IsEmptyElement)
            {
                reader.Read();
                base.ReadXmlContent(reader);
            }

            reader.Read();
        }

        public void ReadXml(string filename)
        {
            Nodes.Clear();
            Initialize();

            using (var sr = new StreamReader(filename))
            using (var reader = XmlReader.Create(sr))
            {
                Base = Path.GetDirectoryName(filename);
                ReadXml(reader);
            }
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }
    }
}
