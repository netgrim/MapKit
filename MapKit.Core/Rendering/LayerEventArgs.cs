using System;

namespace MapKit.Core
{
    public class LayerEventArgs : EventArgs
	{
		public LayerEventArgs(Layer layer)
		{
			Layer = layer;
		}

		public Layer Layer { get; private set; }

	}
}
