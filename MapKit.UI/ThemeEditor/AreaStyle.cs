using System.ComponentModel;
using Cyrez.GIS.Data;

namespace Cyrez.GIS.UI
{
    class AreaStyle : Style
    {
        public AreaStyle(ThemeDs.AreaStyleRow areaStyle, ThemeDs.StyleRow style)
            : base(style)
        {
            AreaStyleRow = areaStyle;
        }

        [Browsable(false)]
        public ThemeDs.AreaStyleRow AreaStyleRow { get; private set; }

        
    }
}
