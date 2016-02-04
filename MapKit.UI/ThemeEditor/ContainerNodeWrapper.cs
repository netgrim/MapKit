using System.ComponentModel;
using MapKit.Core;

namespace MapKit.UI
{
    class ContainerNodeWrapper : ThemeNodeWrapper
    {
        private readonly ContainerNode _containerNode;

        public ContainerNodeWrapper(ContainerNode containerNode)
            :base(containerNode)
        {
            _containerNode = containerNode;
        }
        
        [Browsable(false)]
        public ContainerNode ContainerNode
        {
            get { return _containerNode; }
        }

    }
}
