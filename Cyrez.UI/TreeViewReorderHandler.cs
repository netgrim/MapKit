using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;

namespace Cyrez.UI
{
    public class TreeViewReorderHandler : Component
    {
        private TreeView _treeView;
        private DropPosition _dropPosition;
        private Pen _dropLinePen;

        public event EventHandler<NodeMovedEventArgs> NodeMoved;
        public event TreeViewEventHandler NodeIndexChanged;

        public TreeViewReorderHandler()
        {
            CreateDropLinePen();
        }

        public TreeView TreeView
        {
            get { return _treeView; }
            set
            {
                if (_treeView != null)
                {
                    _treeView.ItemDrag -= _treeView_ItemDrag;
                    _treeView.DragOver -= _treeView_DragOver;
                    _treeView.DragDrop -= _treeView_DragDrop;
                    _treeView.DragLeave -= _treeView_DragLeave;
                }

                _treeView = value;

                if (_treeView != null)
                {
                    _treeView.ItemDrag += _treeView_ItemDrag;
                    _treeView.DragOver += _treeView_DragOver;
                    _treeView.DragDrop += _treeView_DragDrop;
                    _treeView.DragLeave += _treeView_DragLeave;
                    _treeView.AllowDrop = true;
                }
            }
        }

        private float _topEdgeSensivity = 0.3f;
        [DefaultValue(0.3f), Category("Behavior")]
        public float TopEdgeSensivity
        {
            get { return _topEdgeSensivity; }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException();
                _topEdgeSensivity = value;
            }
        }

        private float _bottomEdgeSensivity = 0.3f;
        [DefaultValue(0.3f), Category("Behavior")]
        public float BottomEdgeSensivity
        {
            get { return _bottomEdgeSensivity; }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("value should be from 0 to 1");
                _bottomEdgeSensivity = value;
            }
        }

        private Color _dragDropLineColor = Color.Black;
        [Category("Behavior")]
        public Color DragDropLineColor
        {
            get { return _dragDropLineColor; }
            set
            {
                _dragDropLineColor = value;
                CreateDropLinePen();
            }
        }

        private float _dragDropLineWidth = 2.0f;
        [DefaultValue(3.0f), Category("Behavior")]
        public float DragDropLineWidth
        {
            get { return _dragDropLineWidth; }
            set
            {
                _dragDropLineWidth = value;
                CreateDropLinePen();
            }
        }

        private bool _highlightDropPosition = true;
        [DefaultValue(true), Category("Behavior")]
        public bool HighlightDropPosition
        {
            get { return _highlightDropPosition; }
            set { _highlightDropPosition = value; }
        }

        private HatchStyle _dragDropLineHatchStyle = HatchStyle.Percent50;
        [DefaultValue(HatchStyle.Percent50), Category("Behavior")]
        public HatchStyle DragDropLineHatchStyle
        {
            get { return _dragDropLineHatchStyle; }
            set
            {
                _dragDropLineHatchStyle = value;
                CreateDropLinePen();
            }
        }

        private void CreateDropLinePen()
        {
            var brush = new HatchBrush(_dragDropLineHatchStyle, _dragDropLineColor, Color.Transparent);
            _dropLinePen = new Pen(brush, _dragDropLineWidth);
        }

        void _treeView_DragLeave(object sender, EventArgs e)
        {
            ResetDropMark();
        }

        private void ResetDropMark()
        {
            if (_dropPosition.Node == null) return;
            switch (_dropPosition.Position)
            {
                case NodePosition.Before:
                    _treeView.Invalidate(new Rectangle(0, _dropPosition.Node.Bounds.Top - (int)_dragDropLineWidth, _treeView.Width, (int)(_dragDropLineWidth * 2)));
                    break;
                case NodePosition.After:
                    _treeView.Invalidate(new Rectangle(0, _dropPosition.Node.Bounds.Bottom - (int)_dragDropLineWidth, _treeView.Width, (int)(_dragDropLineWidth * 2)));
                    break;
                case NodePosition.Inside:
                    if (!_highlightDropPosition) break;
                    _dropPosition.Node.BackColor = SystemColors.Window;
                    _dropPosition.Node.ForeColor = SystemColors.WindowText;
                    break;
            }
            _treeView.Update();
            _dropPosition.Node = null;
        }

        void _treeView_DragDrop(object sender, DragEventArgs e)
        {
            var target = _dropPosition.Node;
            var position = _dropPosition.Position;
            ResetDropMark();

            var source = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (source == null) return; // no source
            if (target == null) return; // no target
            if (source == target) return;// same
            if (e.Effect != DragDropEffects.Move) return; //no effect

            _treeView.BeginUpdate();

            var newParent = position == NodePosition.Inside ? target : target.Parent;
            var newParentNodes = newParent != null ? newParent.Nodes : newParent.TreeView.Nodes;
            var oldParent = source.Parent;
            var oldIndex = source.Index;

            if ( oldParent != newParent)
            {
                //moved in different parent
                Remove(source); // remove from old parent

                int newIndex;
                if (position == NodePosition.Inside)
                    newIndex = newParentNodes.Add(source);
                else
                {
                    newIndex = position == NodePosition.Before ? target.Index : target.Index + 1;
                    newParentNodes.Insert(newIndex, source);
                }
                OnNodeMoved(new NodeMovedEventArgs(source, TreeViewAction.ByMouse, oldParent, oldIndex));

                for (int i = newIndex; i < newParentNodes.Count; i++)
                    OnNodeIndexChanged(new TreeViewEventArgs(newParentNodes[i], TreeViewAction.ByMouse));
            }
            else if(position != NodePosition.Inside) //move in same parent, but a different place
            {
                source.Remove();
                var newIndex = position == NodePosition.Before ? target.Index : target.Index + 1;
                if (oldIndex != newIndex)
                {
                    newParentNodes.Insert(newIndex, source);
                    OnNodeMoved(new NodeMovedEventArgs(source, TreeViewAction.ByMouse, source.Parent, oldIndex));

                    if (newIndex < oldIndex) //moved up
                        for (int i = newIndex; i <= oldIndex; i++)
                            OnNodeIndexChanged(new TreeViewEventArgs(newParentNodes[i], TreeViewAction.ByMouse));
                    else  // moved down
                        for (int i = oldIndex; i <= newIndex; i++)
                            OnNodeIndexChanged(new TreeViewEventArgs(newParentNodes[i], TreeViewAction.ByMouse));
                }
            }

            _treeView.EndUpdate();
            _treeView.SelectedNode = source;
        }

        private void OnNodeMoved(NodeMovedEventArgs e)
        {
            if (NodeMoved != null)
                NodeMoved(this, e);
        }

        private void OnNodeIndexChanged(TreeViewEventArgs e)
        {
            if (NodeIndexChanged != null)
                NodeIndexChanged(this, e);
        }

        private void Remove(TreeNode node)
        {
            var nodes = node.Parent != null ? node.Parent.Nodes : node.TreeView.Nodes;
            Debug.Assert(nodes.Contains(node));

            var index = node.Index;
            node.Remove();
            for (int i = index; i < nodes.Count; i++)
                OnNodeIndexChanged(new TreeViewEventArgs(nodes[i], TreeViewAction.ByMouse));
        }

        void _treeView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            var source = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (source == null) return;

            var p = _treeView.PointToClient(new Point(e.X, e.Y));

            var target = _treeView.GetNodeAt(p);
            if (target != null && !IsAscendant(source, target))
            {
                NodePosition position;
                var bounds = target.Bounds;
                float pos = (p.Y - bounds.Y) / (float)bounds.Height;
                if (pos < TopEdgeSensivity)
                    position = NodePosition.Before;
                else if (pos > (1 - BottomEdgeSensivity) && !target.IsExpanded)
                    position = NodePosition.After;
                else
                    position = NodePosition.Inside;

                if ((source.Parent == target && position == NodePosition.Inside) || //cannot move to its own parent
                    (target.NextNode == source && position == NodePosition.After) || //after previous = us
                    (target.PrevNode == source && position == NodePosition.Before)) //before next = us
                {
                    ResetDropMark();
                    return;
                }

                e.Effect = DragDropEffects.Move;
                OnDropNodeValidating(e, source, target, position);

                if (target != _dropPosition.Node || _dropPosition.Position != position)
                    ResetDropMark();
                if (e.Effect != DragDropEffects.None)
                {
                    _dropPosition.Node = target;
                    _dropPosition.Position = position;
                    DrawDropMark();
                }
            }
            else
                ResetDropMark();
        }

        private bool IsAscendant(TreeNode source, TreeNode target)
        {
            while (target != null)
            {
                if (source == target)
                    return true;
                target = target.Parent;
            }
            return false;
        }

        private void DragTimerTick(object state)
        {
            //_dragAutoScrollFlag = true;
        }

        private void DragAutoScroll()
        {
            //_dragAutoScrollFlag = false;
            //Point pt = _treeView.PointToClient(Cursor.Position);

            //if (pt.Y < 20 && _vScrollBar.Value > 0)
            //    _treeView.AutoScrollOffset.Y--;
            //else if (pt.Y > Height - 20 && _vScrollBar.Value <= _vScrollBar.Maximum - _vScrollBar.LargeChange)
            //    _vScrollBar.Value++;
        }


        private void DrawDropMark()
        {
            if (_dropPosition.Position == NodePosition.Inside)
            {
                if (!_highlightDropPosition) return;
                _dropPosition.Node.BackColor = SystemColors.Highlight;
                _dropPosition.Node.ForeColor = SystemColors.HighlightText;
                return;
            }

            using (var gr = _treeView.CreateGraphics())
            {
                var rect = _dropPosition.Node.Bounds;
                int right = _treeView.ClientRectangle.Right; ;
                int y = _dropPosition.Position == NodePosition.After ? rect.Bottom : rect.Y;
                gr.DrawLine(_dropLinePen, rect.X, y, right, y);
            }
        }

        [Category("Drag Drop")]
        public event EventHandler<DropNodeValidatingEventArgs> DropNodeValidating;
        protected virtual void OnDropNodeValidating(DragEventArgs e, TreeNode sourceNode, TreeNode targetNode, NodePosition position)
        {
            if (DropNodeValidating != null)
            {
                var args = new DropNodeValidatingEventArgs(e.Data, e.KeyState, e.X, e.Y, e.AllowedEffect, e.Effect, sourceNode, targetNode, position);
                DropNodeValidating(this, args);
                e.Effect = args.Effect;
            }
        }

        void _treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _treeView.DoDragDrop(e.Item, DragDropEffects.All);
        }

        //private bool _dragAutoScrollFlag = false;
        private System.Threading.Timer _dragTimer;

        private void StartDragTimer()
        {
            if (_dragTimer == null)
                _dragTimer = new System.Threading.Timer(new TimerCallback(DragTimerTick), null, 0, 100);
        }

        private void StopDragTimer()
        {
            if (_dragTimer != null)
            {
                _dragTimer.Dispose();
                _dragTimer = null;
            }
        }
    }

    public class NodeMovedEventArgs : TreeViewEventArgs
    {
        public NodeMovedEventArgs(TreeNode node, TreeViewAction action, TreeNode oldParent, int oldIndex)
            : base(node, action)
        {
            OldParent = oldParent;
            OldIndex = oldIndex;
        }

        public TreeNode OldParent { get; set; }

        public int OldIndex { get; set; }
    }
}
