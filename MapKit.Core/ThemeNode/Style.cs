using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;

namespace MapKit.Core
{
	public abstract class Style : Group
	{
        internal const string ReferenceFieldName = "ref";
        internal const string NameFieldName = "name";
        
        private string _name;
        private string _ref;

		public Style()
		{
            _ref = string.Empty;
		}

        public string Name
        {

            get { return _name; }
            set
            {
                _name = value;
                OnFieldChanged(NameFieldName);
            }
        }

        public string Reference
        {
            get { return _ref; }
            set
            {
                _ref = value;
                OnFieldChanged(ReferenceFieldName);
            }
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == Style.NameFieldName) Name = reader.Value;
            else if (reader.LocalName == Style.ReferenceFieldName) Reference = reader.Value;
            else if (!base.TryReadXmlAttribute(reader)) return false;
            return true;
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Name))
                writer.WriteAttributeString(NameFieldName, Name);
            if (!string.IsNullOrEmpty(Reference))
                writer.WriteAttributeString(ReferenceFieldName, Name);

            base.WriteXmlAttributes(writer);
        }
    }
}
