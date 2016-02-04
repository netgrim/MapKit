using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.Core
{
	class NodeEventArgs : EventArgs
	{
		public NodeEventArgs(ThemeNode node)
		{
			Node = node;
		}

		public ThemeNode Node { get; private set; }
	}
}
