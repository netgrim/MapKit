using System.Drawing;
using System.ComponentModel;
using System.Data;
using Cyrez.GIS.Data;

namespace Cyrez.GIS.UI
{
    class TextStyle : Style, INodeRow
    {
        public TextStyle(ThemeDs.TextStyleRow textStyle, ThemeDs.StyleRow style)
            : base(style)
        {
            TextStyleRow = textStyle;
        }

        [Browsable(false)]
        public ThemeDs.TextStyleRow TextStyleRow { get; private set; }

        public ContentAlignment Alignment
        {
            get { return TextStyleRow.Alignment; }
            set { TextStyleRow.Alignment = value; }
        }

        public Color Color
        {
            get { return Color.FromArgb(TextStyleRow.Color); }
            set { TextStyleRow.Color = value.ToArgb(); }
        }

        #region IDataRowProxy Membres

        DataRow INodeRow.DataRow
        {
            get { return TextStyleRow; }
        }

        #endregion


        public bool CanReceive(object data)
        {
            return false;
        }
    }
}
