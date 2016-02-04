using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;
using MapKit.Core;
using Cyrez.UI;
using System.ComponentModel;

namespace MapKit.UI
{
    public partial class ThemeEditorComponent : Component
    {
        private Dictionary<ThemeNode, TreeNode> _objectsNode = new Dictionary<ThemeNode, TreeNode>();
        
        public ThemeEditorComponent()
        {
            InitializeComponent();

            cmnLayer.Opening += cmnLayer_Opening;
            cmnLayerRemove.Click += cmnThemeNodeRemove_Click;
            cmnLayerProperties.Click += cmnThemeNodeProperties_Click;

            cmnThemeNode.Opening += cmnThemeNode_Opening;
            cmnThemeNodeExpand.Click += cmnNodeExpandAll_Click;
            cmnThemeNodeCollapse.Click += cmnNodeColapse_Click;
            cmnThemeNodeRemove.Click += cmnThemeNodeRemove_Click;
            cmnThemeNodeRename.Click += cmnRename_Click;
            cmnThemeNodeProperties.Click += cmnThemeNodeProperties_Click;
        }

        void _treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            var wrapper = e.Node.Tag as ThemeNodeWrapper;
            Debug.Assert(wrapper != null);

            var themeNode = wrapper != null ? wrapper.Node : e.Node.Tag as ThemeNode;
            if (themeNode == null) return;

            var state = TriStateTreeView.GetNodeState(e.Node);
            if (state != CheckState.Indeterminate)
                themeNode.Visible = state == CheckState.Checked;
			

			//if (_updatingCheck) return;
			//var r = e.Node.Tag as INodeRow;
			//if (r == null) return;
			//var layer = r.DataRow as ThemeDs.LayerRow;
			//if (layer != null)
			//    switch (_checkScaleMode)
			//    {
			//        case ScaleMode.Auto:
			//            if (Math.Abs(layer.MinScale - _visibilityScale) < Math.Abs((layer.IsMaxScaleNull() ? double.MaxValue : layer.MaxScale) - _visibilityScale))
			//                layer.MinScale = _visibilityScale;
			//            else
			//                layer.MaxScale = _visibilityScale;
			//            break;
			//        case ScaleMode.Min:
			//            layer.MinScale = _visibilityScale;
			//            break;
			//        case ScaleMode.Max:
			//            layer.MaxScale = _visibilityScale;
			//            break;
			//    }
        }

        void _treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var testInfo = _treeView.HitTest(e.X, e.Y);
                if (testInfo.Node != null)
                    _treeView.SelectedNode = testInfo.Node;
            }
        }

        void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (PropertyGrid == null) return;

            var node = _treeView.SelectedNode;
            if (node == null) return;

            var wrapper = node.Tag as ThemeNodeWrapper;

            PropertyGrid.SelectedObject = wrapper != null
                ? wrapper.Node
                : _treeView.SelectedNode.Tag;

            //PropertyGrid.SelectedObject = node.Tag;
        }

        void _treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
                return;

            var editable = e.Node.Tag as IEditableNode;
            if (editable == null)
            {
                e.CancelEdit = true;
                Debug.Fail("Node cannot be renamed");
            }
            else
                try
                {
                    editable.Label = e.Label;
                }
                catch (ConstraintException)
                {
                    MessageBox.Show("Name already exists");
                    e.CancelEdit = true;
                }
        }

        void _treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = !(_treeView.SelectedNode.Tag is IEditableNode);
        }

        public void AddNewLayer(TreeNode parentNode)
        {
            /*var wrapper = (LayerGroupWrapper)parentNode.Tag;
        
            var newLayer = new Layer();
            //newLayer.Sequence = _ds.Layer.Count(x => x.ThemeId == theme.Id);
            newLayer.Name = "New layer";
                        
            var parent = wrapper.Group;
            parent.Nodes.Add(newLayer);

            var node = AddLayerNode(parentNode, newLayer);
            node.EnsureVisible();
            _treeView.SelectedNode = node;
            if (_treeView.LabelEdit)
                node.BeginEdit();*/
        }

		private TriStateTreeView _treeView;
		public TriStateTreeView TreeView 
		{
			get { return _treeView; }
			set
			{
				if (_treeView != null)
				{
					_treeView.AfterCheck -= _treeView_AfterCheck;
					_treeView.AfterLabelEdit -= _treeView_AfterLabelEdit;
					_treeView.AfterSelect -= _treeView_AfterSelect;
					_treeView.BeforeLabelEdit -= _treeView_BeforeLabelEdit;
					_treeView.MouseDoubleClick -= _treeView_MouseDoubleClick;
					_treeView.MouseDown -= _treeView_MouseDown;
				}

				_treeView = value;
				treeViewReorderHandler.TreeView = value;

				if (_treeView != null)
				{
					_treeView.AfterCheck += _treeView_AfterCheck;
					_treeView.AfterLabelEdit +=_treeView_AfterLabelEdit;
					_treeView.AfterSelect += _treeView_AfterSelect;
					_treeView.BeforeLabelEdit += _treeView_BeforeLabelEdit;
					_treeView.MouseDoubleClick += _treeView_MouseDoubleClick;
					_treeView.MouseDown += _treeView_MouseDown;
				}
			}
		}


        private PropertyGrid _propertyGrid;
        public PropertyGrid PropertyGrid
        {
            get { return _propertyGrid; }
            set { _propertyGrid = value; }
        }

		private Map _map;

		public Map Map 
		{
			get { return _map; }
			set
			{
				if (_treeView != null)
					_treeView.Nodes.Clear();

                foreach (var themeNode in _objectsNode.Keys)
                    themeNode.PropertyChanged -= themeNode_PropertyChanged;
                _objectsNode.Clear();
                
				_map = value;

				if (_map != null)
				{
					if (_treeView != null)
						_treeView.AfterCheck -= _treeView_AfterCheck;
					var node = AddMapNode(_map);
					node.Expand();

					if (_treeView != null)
						_treeView.AfterCheck += _treeView_AfterCheck;
				}
			}
		}

        public bool TryGetNode(ThemeNode themeNode, out TreeNode node)
		{
            return _objectsNode.TryGetValue(themeNode, out node);
		}

        private void AddNodes(TreeNode parent, IEnumerable<ThemeNode> nodes)
		{
			foreach (var item in nodes)
				AddNode(parent, item);
		}

		private TreeNode AddNode(TreeNode parent, ThemeNode item)
		{
            var layer = item as Layer;
            if (layer != null)
                return AddLayerNode(parent, layer);
            
            var group = item as Group;
            if (group != null)
                return AddGroupNode(parent, group);

            var text = item as Text;
            if (text != null)
                return AddTextNode(parent, text);

            var style = item as Style;
            if (style != null)
                return AddStyleNode(parent, style);

            if (item is When || item is Else)
                return AddContainterNode(parent, new CaseItemWrapper((ContainerNode)item), cmnThemeNode);

            var containerNode = item as ContainerNode;
            if (containerNode != null)
                return AddContainterNode(parent, new ContainerNodeWrapper(containerNode), cmnThemeNode);
            
            Debug.WriteLine("Unknown type: " + item.GetType().Name);

            var parentNodes = parent != null ? parent.Nodes : _treeView.Nodes;

            var node = parentNodes.Add(item.LabelOrDefault);
            node.Tag = new ThemeNodeWrapper(item);
            node.Checked = item.Visible;

            AttachNode(item, node);

            return node;
		}

        private TreeNode AddMapNode(Map map)
        {
            return AddGroupNode(null, map);
        }

        private TreeNode AddGroupNode(TreeNode parent, Group group)
        {
            var parentNodes = parent != null ? parent.Nodes : _treeView.Nodes;

            var wrapper = new LayerGroupWrapper(group);

            var node = parentNodes.Add(wrapper.Label);
            node.Tag = wrapper;
            node.Checked = group.Visible;
            node.ContextMenuStrip = cmnThemeNode;

            AttachNode(group, node);

            AddNodes(node, group.Nodes);

            return node;
        }

        private TreeNode AddContainterNode(TreeNode parent, ContainerNodeWrapper wrapper, ContextMenuStrip contextMenu)
        {
            var parentNodes = parent != null ? parent.Nodes : _treeView.Nodes;
            var containerNode = wrapper.ContainerNode;

            var treeNode = parentNodes.Add(wrapper.Label);
            treeNode.Tag = wrapper;
            treeNode.Checked = containerNode.Visible;
            treeNode.ContextMenuStrip = contextMenu;

            AttachNode(containerNode, treeNode);

            AddNodes(treeNode, containerNode.Nodes);

            return treeNode;
        }

        private void AttachNode(ThemeNode themeNode, TreeNode treeNode)
        {
            _objectsNode.Add(themeNode, treeNode);
            themeNode.PropertyChanged += new PropertyChangedEventHandler(themeNode_PropertyChanged);
        }

        void themeNode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var themeNode = (ThemeNode)sender;

            TreeNode treeNode;
            if (e.PropertyName == ThemeNode.LabelOrDefaultProperty && _objectsNode.TryGetValue(themeNode, out treeNode))
                treeNode.Text = themeNode.LabelOrDefault;
        }

		private TreeNode AddLayerNode(TreeNode parent, Layer layer)
		{
            return AddContainterNode(parent, new LayerWrapper(layer), cmnLayer);
		}

        private TreeNode AddStyleNode(TreeNode parent, Style style)
        {
            if(parent == null) throw new ArgumentNullException("parent");
            if (style == null) throw new ArgumentNullException("style");

            var wrapper = new StyleWrapper(style);
            var node = parent.Nodes.Add(wrapper.Label);
            node.Tag = wrapper;
            node.Checked = style.Visible;
            node.ContextMenuStrip = cmnThemeNode;

            AttachNode(style, node);

            AddNodes(node, style.Nodes);

            return node;
		}

        private TreeNode AddTextNode(TreeNode parent, Text text)
        {
            if(parent == null) throw new ArgumentNullException("parent");
            if (text == null) throw new ArgumentNullException("text");

            var wrapper = new ThemeNodeWrapper(text);
            var node = parent.Nodes.Add(wrapper.Label);
            node.Tag = wrapper;
            node.Checked = text.Visible;
            node.ContextMenuStrip = cmnThemeNode;

            AttachNode(text, node);

            AddContainterNode(node, new LabelBoxWrapper(text.LabelBox), cmnThemeNode);

            return node;
		}

        void cmnNodeExpandAll_Click(object sender, EventArgs e)
        {
			if (_treeView != null)
				_treeView.SelectedNode.ExpandAll();
        }

        public object Cut()
        {
            if (_treeView.SelectedNode != null && _treeView.SelectedNode.Tag is IEditableNode)
            {
                var o = (IEditableNode)_treeView.SelectedNode.Tag;
                //if (o.DataRow.Table.ChildRelations.Cast<DataRelation>().Any(relation => relation.Nested))
                    throw new NotImplementedException();
            }
            return null;
        }

        public object Copy()
        {
            throw new NotImplementedException();
        }

        public void Paste(object objectToPaste, bool copy)
        {
            throw new NotImplementedException();
        }

        public void AddNewGroup(TreeNode parentNode)
        {
            var wrapper = (LayerGroupWrapper)parentNode.Tag;
            var parentGroup = wrapper.Group;

            var newGroup = new Group();
            newGroup.Name = "New Group";

            parentGroup.Nodes.Add(newGroup);

            var node = AddGroupNode(parentNode, newGroup);
            node.EnsureVisible();
            _treeView.SelectedNode = node;
            if (_treeView.LabelEdit)
                node.BeginEdit();
        }


        private void cmnNodeColapse_Click(object sender, EventArgs e)
        {
            if (_treeView.SelectedNode != null)
                ColapseAll(_treeView.SelectedNode);
        }

        static void ColapseAll(TreeNode node)
        {
            node.Collapse();
            foreach (TreeNode child in node.Nodes)
                ColapseAll(child);

        }

		private void treeViewReorderHandler_NodeMoved(object sender, NodeMovedEventArgs e)
		{
			var editable = e.Node.Tag as IEditableNode;

            if (editable != null)
            {
                var targetWrapper = e.Node.Parent != null ? e.Node.Parent.Tag as ContainerNodeWrapper: null;
                var target = targetWrapper != null ? targetWrapper.ContainerNode : null;
                
                editable.MoveTo(target, e.Node.Index);
            }
            else
                Debug.Fail("node editable");
		}

		private void treeViewReorderHandler_DropNodeValidating(object sender, DropNodeValidatingEventArgs e)
		{
			var nodeRow = e.SourceNode.Tag as IEditableNode;
            if (nodeRow != null)
            {
                var targetWrapper = e.TargetNode.Tag as ThemeNodeWrapper;
                if (targetWrapper == null) return;
                var target = targetWrapper.Node;

                var newParent = e.Position == NodePosition.Inside ? target as ContainerNode : target.Parent;
                if (newParent != null)
                {
                    int newIndex;
                    if (e.Position == NodePosition.Inside)
                        newIndex = newParent.Nodes.Count;
                    else if (e.Position == NodePosition.Before)
                        newIndex = e.TargetNode.Index;
                    else
                        newIndex = e.TargetNode.Index + 1;

                    if (nodeRow.CanMoveTo(newParent, e.SourceNode.Index, newIndex))
                        return;
                }
            }
			e.Effect = DragDropEffects.None;
		}

        //todo properties
		/*private void EditQueryProperties(ThemeDs.QueryRow query)
		{
			using (var f = new QueryDialog())
			{
				f.SQL = query.SQL;
				f.Enabled = query.Visible;

				if (f.ShowDialog() == DialogResult.OK)
				{
					query.SQL = f.SQL;
					query.Visible = f.Enabled;
				}
			}
	
		}*/

		private void cmnThemeNodeProperties_Click(object sender, EventArgs e)
		{
			var wrapper = SelectedWrapper;
            if (wrapper == null) return;

			EditProperties(wrapper.Node);
		}

        void cmnRename_Click(object sender, System.EventArgs e)
        {
            if (_treeView.LabelEdit)
                _treeView.SelectedNode.BeginEdit();
        }

		private void EditProperties(object obj)
		{
			var tool = new PropertiesToolForm();

			var nodeRow = obj as IEditableNode;
			if (nodeRow != null)
				tool.Text = "Properties of " + nodeRow.Label;

			tool.SelectedObject = obj;
			tool.Show();
		}

		private void _treeView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (_treeView.SelectedNode == null || _treeView.SelectedNode.Nodes.Count > 0 ) return;

			var nodeRow = _treeView.SelectedNode.Tag as IEditableNode;
			if (nodeRow != null && nodeRow.CanShowProperties() )
				EditProperties(nodeRow);
		}

        void cmnThemeNodeRemove_Click(object sender, EventArgs e)
        {
            var themeNode = SelectedThemeNode;
            if (themeNode == null) return;
            if (themeNode.Parent == null) return;

            themeNode.Parent.Nodes.Remove(themeNode);

            var selectedNode = _treeView.SelectedNode;
            DetachNode(selectedNode);
            selectedNode.Remove();
        }

        private void DetachNode(TreeNode treeNode)
        {
            //recursively remove descendant
            foreach (TreeNode child in treeNode.Nodes)
                DetachNode(child);

            var themeNode = treeNode.Tag as ThemeNode;
            if (themeNode != null)
            {
                themeNode.PropertyChanged -= this.themeNode_PropertyChanged;
                _objectsNode.Remove(themeNode);
            }
        }

		public ContextMenuStrip LayerContextMenu
		{
			get { return cmnLayer; }
		}

		public ThemeNode SelectedThemeNode
		{
			get
			{
                var wrapper = SelectedWrapper;

                return wrapper != null
                    ? wrapper.Node
                    : null;
			}
		}

        internal ThemeNodeWrapper SelectedWrapper
        {
            get
            {
                if (_treeView == null)
                    return null;
                var node = _treeView.SelectedNode;
                if (node == null)
                    return null;

                return node.Tag as ThemeNodeWrapper;
            }
        }

        void cmnThemeNode_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var wrapper = SelectedWrapper;
            if(wrapper==null) return;

            cmnThemeNodeAdd.DropDownItems.Clear();
            var node = wrapper.Node as ThemeNode;
            if (node == null) return;

            cmnThemeNodeAdd.Visible =
            cmnThemeNodeExpandSep.Visible = AddNodeTypesMenu(cmnThemeNodeAdd, node, node.GetNodeType().NodeTypes) > 0;

            cmnThemeNodeRemoveSep.Visible =
                cmnThemeNodeRemove.Visible = wrapper.CanRemove();

            var visible = cmnThemeNodeProperties.Visible = wrapper.CanShowProperties();
            visible |= cmnThemeNodeRename.Visible = wrapper.CanRename();

            cmnThemeNodeRenameSep.Visible = visible;
        }

        private int AddNodeTypesMenu(ToolStripMenuItem parent, ThemeNode context, IEnumerable<NodeType> nodeTypes)
        {
            var count = 0;
            foreach (var nodeType in nodeTypes)
                if (nodeType.CanAddTo(context))
                {
                    var newMenu = new ToolStripMenuItem(nodeType.DisplayText ?? nodeType.Name);
                    newMenu.Tag = nodeType;
                    parent.DropDownItems.Add(newMenu);

                    if(AddNodeTypesMenu(newMenu, context, nodeType.GetSubTypes(context)) == 0)
                        newMenu.Click += new EventHandler(cmnAddNodes_Click);
                    count++;
                }
            return count;
        }

        void cmnLayer_Opening(object sender, CancelEventArgs e)
        {
            cmnLayerAdd.DropDownItems.Clear();
            var parentNode = SelectedThemeNode as ContainerNode;
            if (parentNode == null) return;

            cmnLayerSep.Visible = AddNodeTypesMenu(cmnLayerAdd, parentNode, parentNode.GetNodeType().NodeTypes) > 0;
        }

        void cmnAddNodes_Click(object sender, EventArgs e)
        {
            var container = SelectedThemeNode as ContainerNode;
            if(container == null) return;

            var menu = ((ToolStripMenuItem)sender);
            var nodeType = (NodeType)menu.Tag;

            foreach (var newNode in nodeType.CreateNew())
            {
                container.Nodes.Add(newNode);

                AddNode(_treeView.SelectedNode, newNode);
            }
        }


	}
}
