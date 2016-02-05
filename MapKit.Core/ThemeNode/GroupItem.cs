using System.ComponentModel;
using System.Xml;

namespace MapKit.Core
{
    public abstract class GroupItem : ContainerNode
    {
        private const bool DefaultSmoothingMode = true;

        public GroupItem()
        {
            SmoothingMode = DefaultSmoothingMode;
        }

        [Category(Constants.CatAppearance)]
        [DefaultValue(true)]
        public bool SmoothingMode { get; set; }

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
    }
}
