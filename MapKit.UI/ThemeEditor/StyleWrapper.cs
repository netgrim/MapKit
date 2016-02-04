using MapKit.Core;
using System.ComponentModel;

namespace MapKit.UI
{
	class StyleWrapper : LayerGroupWrapper 
	{
        public StyleWrapper(Style style)
            : base(style)
		{
            Style = style;
		}

		[Browsable(false)]
        public Style Style { get; set; }

	
	}
}
