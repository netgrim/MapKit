using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.GisLite.Topology
{
	enum EdgeType
	{
		Static = 0x1, //static internal connectivity
		Channel = 0x2, //Channel connectivity
		Device = 0x4, //Device connectivity (VLN to itself)
		Attached = 0x8, //edge to middle channel node
		Switch = 0x10 | Static, //device swtich
		Single = 0x40, //Single entity node (node2=null)
	}
}
