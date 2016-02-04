using System.ComponentModel;
using MapKit.Core;

namespace MapKit.UI
{
    class MapWrapper : ThemeNodeWrapper
    {
        private readonly Map _map;

        public MapWrapper(Map map)
            :base(map)
        {
            _map = map;
        }

        public override bool CanMoveTo(ContainerNode target, int oldIndex, int newIndex)
        {
            return false;
        }
        
        [Browsable(false)]
        public Map map
        {
            get { return _map; }
        }

        public override string Label
        {
            get { return _map.Name; }
            set { _map.Name = value; }
        }

        public override bool CanCopyTo(object destination)
        {
            return false;
        }
    }
}
