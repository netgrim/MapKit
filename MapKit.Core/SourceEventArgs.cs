using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

