using System;

namespace MapKit.Core
{
    public class HatchFill : Fill
	{
        private const string ElementName = "hatchFill";

		public override object Clone()
		{
			return MemberwiseClone();
		}

		public HatchFillFields Fields { get; set; }

		internal override bool Cascade(Style style)
		{
            //var hatchFill = style as HatchFill;
            //if (hatchFill != null && hatchFill.Id == Id)
            //{
            //    Cascade(hatchFill);
            //    return true;
            //}
			return false;
		}

		private void Cascade(HatchFill fill)
		{
			var missing = fill.Fields & ~Fields;
			if (missing == HatchFillFields.None) return;

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
	public enum HatchFillFields
	{
		None = 0,
	}
}
