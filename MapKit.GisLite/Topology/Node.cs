using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.GisLite.Topology
{
    public class Node
    {
        public Node(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }
    }
}
