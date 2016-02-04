using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
