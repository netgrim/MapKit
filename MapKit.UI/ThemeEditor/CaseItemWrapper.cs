using System;
using MapKit.Core;
using System.ComponentModel;

namespace MapKit.UI
{
	class CaseItemWrapper : ContainerNodeWrapper
	{
        public CaseItemWrapper(ContainerNode node)
            : base(node)
		{
		}

        public override bool CanMoveTo(ContainerNode destination, int oldIndex, int newIndex)
		{
            if (destination is Case)
            {
                var prev = newIndex > 0 ? destination.Nodes[newIndex -1] : null;
                var next = newIndex < destination.Nodes.Count ? destination.Nodes[newIndex] : null;

                if(Node is Else)
                    return !(prev is Else) && next == null;
                else //when
                    return !(prev is Else);
            }
            return false;
		}
	}
}
