using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.GisLite.Topology
{
    public class Edge : GraphElement
    {

        public Edge(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }

        public long Node1Id { get; set; }

        public long Node2Id { get; set; }

        public bool State { get; set; }

        internal EdgeDirection Direction { get; set; }

        public string Phase { get; set; }

        public int DefId { get; set; }
    }
}
