using MapKit.Core;
using System.ComponentModel;

namespace MapKit.UI
{
	class StyleWrapper
	{
        public StyleWrapper(Style style)
		{
            Style = style;
		}

		[Browsable(false)]
        public Style Style { get; set; }

	
	}
}
