using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MapKit.GisLite.Topology
{
    public class BreadFirstSearch : GraphEnumerator
    {
        private long _startNodeId;
        private Queue<long> _queue;
        private Queue<Frame> _returnQueue;
        private Dictionary<long, Frame> _visitedEdges;
        private Dictionary<long, Frame> _nodes;
        private GraphAdapter _adapter;

        public BreadFirstSearch(long startNodeId, GraphAdapter adapter)
        {
            _startNodeId = startNodeId;
            _queue = new Queue<long>(new[] { _startNodeId });
            _returnQueue = new Queue<Frame>();
            _visitedEdges = new Dictionary<long, Frame>();
            _nodes = new Dictionary<long, Frame>();
            _adapter = adapter;
        }

        public void Reset()
        {
            _queue.Clear();
            _queue.Enqueue(_startNodeId);

            _visitedEdges.Clear();
        }

        public bool MoveNext()
        {
            while (true)
            {
                while (_returnQueue.Count > 0)
                {
                    Current = _returnQueue.Dequeue();
                    return true;
                }

                if (_queue.Count == 0)
                    return false;

                //Dequeue: try to go deeper
                var nodeId = _queue.Dequeue();

                Frame parentFrame;
                _nodes.TryGetValue(nodeId, out parentFrame);

                //enqueue new edges
                foreach (var edge in _adapter.GetEdgeWithNode(nodeId))
                {
                    //validate visited        
                    if (!_visitedEdges.ContainsKey(edge.Id))
                        _visitedEdges.Add(edge.Id, null);
                    else
                        continue;

                    var frame = new Frame(edge, nodeId, parentFrame, parentFrame != null ? parentFrame.Level + 1 : 0);

                    //evaluate edgePredicate
                    if (EdgePredicate != null && !EdgePredicate(frame))
                        continue; //rejected    

                    _nodes[frame.ToNodeId] = frame;
                    _queue.Enqueue(frame.ToNodeId);

                    _returnQueue.Enqueue(frame);
                }
            }
        }

        public Frame Current { get; private set; }

        public Predicate<Frame> EdgePredicate { get; set; }
    }

    enum ElementType
    {
    }
}
