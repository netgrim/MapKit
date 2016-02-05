using System;

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
