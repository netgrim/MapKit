using System.ComponentModel;
using System.Xml;

namespace MapKit.Core
{
    public abstract class GroupItem : ContainerNode
    {
        internal const string IdField = "id";
        internal const string classField = "class";

        private const bool DefaultSmoothingMode = true;

        public GroupItem()
        {
            SmoothingMode = DefaultSmoothingMode;
        }

        [Category(Constants.CatAppearance)]
        [DefaultValue(true)]
        public bool SmoothingMode { get; set; }

        public string Id { get; set; }

        public string Class { get; set; }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);

            if (SmoothingMode != DefaultSmoothingMode)
            {
                writer.WriteStartAttribute(AntiAliasField);
                writer.WriteValue(SmoothingMode);
                writer.WriteEndAttribute();
            }

        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == IdField) Id = reader.Value;
            else if (reader.LocalName == classField) Class = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
}
