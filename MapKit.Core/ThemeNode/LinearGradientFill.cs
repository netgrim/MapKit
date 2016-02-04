using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MapKit.Core
{
	public class LinearGradientFill : Fill
    {
        private const string ElementName = "linearGradientFill";

		public override object Clone()
		{
			return MemberwiseClone();
		}

		public LinearGradientFillFields Fields { get; set; }

		internal override bool Cascade(Style style)
		{
            //var linearStyle = style as LinearGradientFill;
            //if (linearStyle != null && linearStyle.Id == Id)
            //{
            //    Cascade(linearStyle);
            //    return true;
            //}
			return false;
		}

		private void Cascade(LinearGradientFill fill)
		{
			var missing = fill.Fields & ~Fields;
			if (missing == LinearGradientFillFields.None) return;

			throw new NotImplementedException();
			//foreach (HatchFillFields fields in Enum.GetValues(typeof(HatchFillFields)))
			//    if ((missing & fields) == fields)
			//        switch (fields)
			//        {
			//            case HatchFillFields.Color: Color = fill.Color; break;
			//        }
			//Fields |= missing;
		}

        public override NodeType GetNodeType()
        {
            throw new NotImplementedException();
        }
    }

	[Flags]
	public enum LinearGradientFillFields
	{
		None = 0,
	}
}
