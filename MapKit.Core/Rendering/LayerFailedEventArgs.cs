using System;

namespace MapKit.Core
{
    public class LayerFailedEventArgs : LayerEventArgs
	{
		public LayerFailedEventArgs(Layer layer, Exception exception)
			: base(layer)
		{
			Exception = exception;
		}

		public Exception Exception { get; set; }

        public bool Cancel { get; set; }
	}
}
