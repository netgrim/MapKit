using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.Core
{
    class WhenNodeType : FeatureProcessorType
    {
        public WhenNodeType()
            : base("When", When.ElementName, typeof(When))
        {
            DisplayText = "When";
        }

        public override bool CanAddTo(ThemeNode container)
        {
            return container is Case;
        }

    }
}
