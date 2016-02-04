using System.ComponentModel;
using MapKit.Core;

namespace MapKit.UI
{
    class LabelBoxWrapper : ContainerNodeWrapper
    {
        private readonly LabelBox _labelBox;

        public LabelBoxWrapper(LabelBox labelBox)
            :base(labelBox)
        {
            _labelBox = labelBox;
        }

        public bool Visible
        {
            get { return _labelBox.Visible; }
            set { _labelBox.Visible = value; }
        }

        public override bool CanMoveTo(ContainerNode target, int oldIndex, int newIndex)
        {
			return false;
        }
        
        [Browsable(false)]
        public LabelBox LabelBox
        {
            get { return _labelBox; }
        }

    }
}
