using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.GisLite.Topology
{
    public class Frame
    {

        public Frame(long nodeId)
        {
            ToNodeId = nodeId;
        }

        public Frame(Edge edge, long fromNodeId, Frame parent, int level)
        {
            FromNodeId = fromNodeId;
            Edge = edge;
            Parent = parent;
            Level = level;

            if (edge.Node1Id == fromNodeId)
            {
                ToNodeId = edge.Node2Id;
                Direction = edge.Direction;
            }
            else if (edge.Node2Id == fromNodeId)
            {
                ToNodeId = edge.Node1Id;
                Direction = ReverseDirection(edge.Direction);
            }
        }

        private EdgeDirection ReverseDirection(EdgeDirection edgeDirection)
        {
            switch (edgeDirection)
            {
                case EdgeDirection.Normal: return EdgeDirection.Reverse;
                case EdgeDirection.Reverse: return EdgeDirection.Normal;
                case EdgeDirection.Both: return EdgeDirection.Both;
                default: return EdgeDirection.None;
            }
        }

        public long FromNodeId { get; private set; }

        public long ToNodeId { get; private set; }

        public EdgeDirection Direction { get; private set; }

        public Edge Edge { get; private set; }

        public int Level { get; private set; }

        public Frame Parent { get; private set; }
    }
}
