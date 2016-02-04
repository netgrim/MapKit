using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using GeoAPI.Geometries;
using System.ComponentModel;
using Ciloci.Flee;
using System.Xml;

namespace MapKit.Core
{
    [DebuggerDisplay("VerticesEnumerator: Label = {Label}, Path={NodePath}")]
    public class VerticesEnumerator : ContainerNode
	{
        internal const string ElementName = "vertices";
        internal const string StartField = "start";
        internal const string EndField = "end";
        internal const string IncField = "increment";
        internal const string ModeField = "mode";

        private static FeatureProcessorType _nodeType;

        private int _start;
        private int _end;
        private int _increment;
        private VerticesEnumeratorMode _mode;

        static VerticesEnumerator()
        {
            _nodeType = new FeatureProcessorType("Vertices Enumerator", VerticesEnumerator.ElementName, typeof(VerticesEnumerator));
            _nodeType.NodeTypes.Add(Stroke.NodeType);
            _nodeType.NodeTypes.Add(Marker.NodeType);
            _nodeType.NodeTypes.Add(SolidFill.NodeType);
            _nodeType.NodeTypes.Add(Text.NodeType);
            _nodeType.NodeTypes.Add(PointExtractor.NodeType);
            _nodeType.NodeTypes.Add(VerticesEnumerator.NodeType);
            _nodeType.NodeTypes.Add(LinearCalibration.NodeType);
            _nodeType.NodeTypes.Add(LineOffset.NodeType);
            _nodeType.NodeTypes.Add(Case.NodeType);
        }

        public int Start
        {

            get { return _start; }
            set
            {
                _start = value;
                OnFieldChanged("Start");
            }
        }

        public int End
        {

            get { return _end; }
            set
            {
                _end = value;
                OnFieldChanged("End");
            }
        }

        public int Increment
        {

            get { return _increment; }
            set
            {
                _increment = value;
                OnFieldChanged("Increment");
            }
        }

        public VerticesEnumeratorMode Mode
        {

            get { return _mode; }
            set
            {
                _mode = value;
                OnFieldChanged("Mode");
            }
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if (Mode == VerticesEnumeratorMode.Range)
            {
                writer.WriteStartAttribute(StartField);
                writer.WriteValue(Start);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute(EndField);
                writer.WriteValue(End);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute(IncField);
                writer.WriteValue(Increment);
                writer.WriteEndAttribute();
            }
            
            if (Mode != VerticesEnumeratorMode.All)
            {
                writer.WriteStartAttribute(ModeField);
                writer.WriteValue(Mode.ToString());
                writer.WriteEndAttribute();
            }

            base.WriteXmlAttributes(writer);
        }
        
        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == VerticesEnumerator.StartField) Start = int.Parse(reader.Value);
            else if (reader.LocalName == VerticesEnumerator.EndField) End = int.Parse(reader.Value);
            else if (reader.LocalName == VerticesEnumerator.IncField) Increment = int.Parse(reader.Value);
            else if (reader.LocalName == VerticesEnumerator.ModeField) Mode = (VerticesEnumeratorMode)Enum.Parse(typeof(VerticesEnumeratorMode), reader.Value, true);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
    
    public enum VerticesEnumeratorMode
    {
        All,
        Range,
        First,
        Last,
        Outer,
        Inner
    }
}
