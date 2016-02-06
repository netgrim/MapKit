using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.GisLite.Topology
{
    public class Graph
    {
        private GraphAdapter _adapter;
        
        public Graph(GraphAdapter adapter)
        {
            _adapter = adapter;
        }

        public GraphEnumerator DepthFirstSearch(long startNodeId, Predicate<Edge> edgePredicate, Predicate<Node> nodePredicate)
        {
            return new DepthFirstSearch(startNodeId);
        }

        public GraphEnumerator BreadFirstSearch(long startNodeId, Predicate<Edge> edgePredicate, Predicate<Node> nodePredicate)
        {
            return new BreadFirstSearch(startNodeId, _adapter);
        }

    }
}
