using System;
using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("ZoomFilter ")]
	public class ZoomFilter : ContainerNode
	{
        internal const string ElementName = "zoomFilter";

		public ZoomFilter()
		{
			Visible = true;
		}

		public override object Clone()
		{
			var zoomFilter = (ZoomFilter)MemberwiseClone();

			zoomFilter.Nodes = new ThemeNodeCollection<ThemeNode>(zoomFilter);
			foreach (var node in Nodes)
				zoomFilter.Nodes.Add((ThemeNode)node.Clone());

			return zoomFilter;
		}
        
        public override NodeType GetNodeType()
        {
            throw new NotImplementedException();
        }
	}
}
