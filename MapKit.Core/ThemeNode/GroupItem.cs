using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
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
