using System.Xml;

namespace MapKit.Core
{
    public class Run : ThemeNode
    {
        internal const string ElementName = "run";
        private const string MacroField = "macro";
        private const string ExplodeField = "explode";
        public static NodeType NodeType { get; set; }

        private string _macro;
        private bool _explode;
     
        static Run()
        {
            NodeType = new NodeType(typeof(Run).Name, ElementName, typeof(Run));
        }

        public string Macro
        {
            get { return _macro; }
            set
            {
                _macro = value;
                OnFieldChanged(MacroField);
            }
        }

        public bool Explode
        {
            get { return _explode; }
            set
            {
                _explode = value;
                OnFieldChanged(ExplodeField);
            }
        }

        public override string GenerateNodeName()
        {
            return "Run: " + _macro;
        }

        public override NodeType GetNodeType()
        {
            return new NodeType(ElementName, typeof(Run));
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            writer.WriteAttributeString(MacroField, Macro);
            base.WriteXmlAttributes(writer);
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == MacroField) Macro = reader.Value;
            else if (reader.LocalName == ExplodeField) Explode = bool.Parse(reader.Value);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
}
