using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.Core
{
    class ElseNodeType: FeatureProcessorType
    {
        public ElseNodeType()
            : base("Else", Else.ElementName, typeof(When))
        {
            DisplayText = "Else";
        }

        public override bool CanAddTo(ThemeNode container)
        {
            return container is Case;
        }

    }
}
