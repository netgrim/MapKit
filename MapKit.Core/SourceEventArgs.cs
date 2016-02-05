using System;

namespace MapKit.Core
{
    public class SourceEventArgs :EventArgs
	{
		public SourceEventArgs(FeatureSource source)
		{
			Source = source;
		}

		public FeatureSource Source { get; set; }
	}
}

