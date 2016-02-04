using System.ComponentModel;
using MapKit.Core;

namespace MapKit.UI
{
    class LayerGroupWrapper : ContainerNodeWrapper
    {
        private readonly Group _group;

        public LayerGroupWrapper(Group group)
            :base(group)
        {
            _group = group;
        }

        public bool Visible
        {
            get { return _group.Visible; }
            set { _group.Visible = value; }
        }

		public double MinScale
		{
			get { return _group.MinScale; }
		}

		public string MaxScale
		{
			get { return !_group.MaxScale.HasValue ? "Max" : _group.MaxScale.ToString(); }
		}

        public override bool CanMoveTo(ContainerNode target, int oldIndex, int newIndex)
        {
			return target is Group;
        }
        
        [Browsable(false)]
        public Group Group
        {
            get { return _group; }
        }

    }
}
