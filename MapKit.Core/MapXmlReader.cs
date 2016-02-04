using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Drawing.Drawing2D;

namespace MapKit.Core
{
	class MapXmlReader
	{
		const string Random = "Random";

        public static bool TryReadVisibility(XmlReader reader, ContainerNode container)
        {
            if (reader.LocalName == ThemeNode.VisibleField) container.Visible = Convert.ToBoolean(reader.Value);
            else if (reader.LocalName == ContainerNode.MinScaleField) container.MinScale = double.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
            else if (reader.LocalName == ContainerNode.MaxScaleField) container.MaxScale = double.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
            else
                return false;
            return true;
        }

	    public static void HandleUnexpectedElement(string name)
        {
            throw new Exception("Unexpected element: " + name);
        }

        public static void HandleUnexpectedAttribute(string attribute)
        {
            Trace.WriteLine("Unexpected attribute: " + attribute);
        }
    }
}