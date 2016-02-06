using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.GisLite.Topology
{
    class DepthFirstSearch : GraphEnumerator
    {
        private long startNodeId;

        public DepthFirstSearch(long startNodeId)
        {
            // TODO: Complete member initialization
            this.startNodeId = startNodeId;
        }
    }
}
