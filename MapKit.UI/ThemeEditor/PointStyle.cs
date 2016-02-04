using System.ComponentModel;
using Cyrez.GIS.Data;

namespace Cyrez.GIS.UI
{
    class PointStyle : Style
    {
        public PointStyle(ThemeDs.PointStyleRow pointStyle, ThemeDs.StyleRow style)
            : base(style)
        {
            PointStyleRow = pointStyle;
        }

        [Browsable(false)]
        public ThemeDs.PointStyleRow PointStyleRow { get; private set; }

        public float Scale  
        {
            get { return PointStyleRow.Scale; }
            set { PointStyleRow.Scale = value; }
        } 
         
    }
}
