using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using Cyrez.GIS.UI;
using Cyrez.UI;
using Cyrez.GIS.Data;

namespace Cyrez.Graphics.UI
{
    public partial class ThemeEditor 
    {
        public ThemeEditor()
        {
            InitializeComponent();

            mnuThemesAddNew.Click += mnuThemesAddNew_Click;

            //_treeView.BeforeLabelEdit += _treeView_BeforeLabelEdit;
            //_treeView.AfterLabelEdit += _treeView_AfterLabelEdit;
            //_treeView.AfterSelect += _treeView_AfterSelect;
            //_treeView.MouseDown += _treeView_MouseDown;
            //_treeView.AfterCheck += TreeView_AfterCheck;
            //_treeView.LabelEdit = true;
            //_treeView.ContextMenuStrip = cmnThemes;
            //_treeView.AllowDrop = true;
            //_treeView.DragDrop += treeView_DragDrop;
            //_treeView.DragOver += treeView_DragOver;
            //_treeView.ItemDrag += treeView_ItemDrag;
            
            PropertyGrid = pg;
            DataSet = ds;
            _visibilityScale = 1;

            Bind();
        }

        public void Close()
        {
            _treeView.BeforeLabelEdit -= _treeView_BeforeLabelEdit;
            _treeView.AfterLabelEdit -= _treeView_AfterLabelEdit;
            _treeView.AfterSelect -= _treeView_AfterSelect;
            _treeView.MouseDown -= _treeView_MouseDown;
            _treeView.AfterCheck -= TreeView_AfterCheck;

            DataSet = null;
        }


        void treeView_DragDrop(object sender, DragEventArgs e)
        {
            //var source = e.SourceNode.Tag as IEditItem;
            //if (source != null)
            //{
            //    Debug.Assert(source.CanMoveTo(e.TargetNode.Tag));
            //    source.MoveTo(e.TargetNode.Tag);
            //}
        }

        void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //var node = e.Item as TreeNode;
            //if (node == null || !(node.Tag is Layer || node.Tag is StyleRule || node.Tag is Label))
            //    e.Cancel = true;
        }
        
        void treeView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode).FullName))
            {
                var sourceNode = (TreeNode)e.Data.GetData(typeof(TreeNode).FullName);

                TreeNode targetNode = _treeView.GetNodeAt(_treeView.PointToClient(new Point(e.X, e.Y)));
                if (targetNode != null && sourceNode.Tag is IEditItem && ((IEditItem)sourceNode.Tag).CanMoveTo(targetNode.Tag))
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_updatingCheck) return;
            var r = e.Node.Tag as INodeRow;
            if (r == null) return;
            var layer = r.DataRow as ThemeDS.LayerRow;
            if (layer != null)
                switch (_checkScaleMode)
                {
                    case ScaleMode.Auto:
                        if( Math.Abs(layer.MinScale - _visibilityScale) < Math.Abs((layer.IsMaxScaleNull() ? double.MaxValue : layer.MaxScale) - _visibilityScale))
                            layer.MinScale = _visibilityScale;
                        else
                            layer.MaxScale = _visibilityScale;
                        break;
                    case ScaleMode.Min:
                        layer.MinScale = _visibilityScale;
                        break;
                    case ScaleMode.Max:
                        layer.MaxScale = _visibilityScale;
                        break;
                }
        }

        private double _visibilityScale;
        public double VisibilityScale
        {
            get { return _visibilityScale; }
            set
            {
                _visibilityScale = value;
                UpdateCheck(value);
            }
        }

        private bool _updatingCheck;
        private void UpdateCheck(double scale)
        {
            _updatingCheck = true;
            UpdateCheck(_treeView.Nodes, scale);
            _updatingCheck = false;
        }

        private void UpdateCheck(TreeNodeCollection nodes, double scale)
        {
            foreach (TreeNode node in nodes)
                if (node.Nodes.Count == 0)
                {
                    var r = node.Tag as INodeRow;
                    if (r == null) continue;
                    var layer = r.DataRow as ThemeDS.LayerRow;
                    if (layer != null)
                        node.Checked = scale >= layer.MinScale && scale <= layer.MaxScale;
                }
                else
                    UpdateCheck(node.Nodes, scale);
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
            if (_propertyGrid != null && _treeView.SelectedNode != null)
                _propertyGrid.SelectedObject = _treeView.SelectedNode.Tag;
            else
                _propertyGrid.SelectedObject = null;
        }

        private PropertyGrid _propertyGrid;

        public PropertyGrid PropertyGrid
        {
            get { return _propertyGrid; }
            set
            {
                _propertyGrid = value;
                if (_treeView != null && _treeView.SelectedNode != null)
                    PropertyGrid.SelectedObject = _treeView.SelectedNode.Tag;
            }
        }

        const string NameColumnName = "Name";
        void _treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                DataRow row;
                if (e.Label == null)
                    return;
                //else if (e.Node == _themesNode)
                //    e.CancelEdit = true;
                else if (e.Node.Tag is DataRow)
                    row = (DataRow)e.Node.Tag;
                else if (e.Node.Tag is INodeRow)
                    row = ((INodeRow)e.Node.Tag).DataRow;
                else
                    return;

                if (row != null && row.Table.DataSet == _ds)
                {
                    e.CancelEdit = !row.Table.Columns.Contains(NameColumnName);
                    if (!e.CancelEdit)
                        row[NameColumnName] = e.Label;
                }
            }
            catch (ConstraintException)
            {
                MessageBox.Show("name already exists");
                e.CancelEdit = true;
            }
        }

        void _treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            DataRow row;
            if (e.Node.Tag is DataRow)
                row = (DataRow)e.Node.Tag;
            else if (e.Node.Tag is INodeRow)
                row = ((INodeRow)e.Node.Tag).DataRow;
            else
                return;

            if (row != null && row.Table.DataSet == _ds)
                e.CancelEdit = !row.Table.Columns.Contains(NameColumnName);
        }

        void mnuThemeAddLayer_Click(object sender, EventArgs e)
        {
            TreeNode node = TreeView.SelectedNode;
            var theme = (ThemeDS.ThemeRow)((Theme)node.Tag).DataRow;
            var root = theme.GetOrCreateRoot();

            var display = _ds.Layer.NewLayerRow();
            display.ThemeRow = theme;
            display.LayerGroupRow = root;
            display.Sequence = _ds.Layer.Count(x => x.ThemeId == theme.Id);

            _ds.Layer.Rows.Add(display);
        }

        private ThemeDS _ds;
        public ThemeDS DataSet
        {
            get { return _ds; }
            set
            {
                UnBind();
                if (_ds != null)
                {
                    _ds.Theme.TableNewRow -= Theme_TableNewRow;
                    _ds.Theme.RowChanged -= Theme_RowChanged;
                    _ds.Theme.ColumnChanged -= Theme_ColumnChanged;
                    _ds.Theme.RowDeleted -= Theme_RowDeleted;
                    _ds.Layer.TableNewRow -= Layer_TableNewRow;
                    _ds.Layer.RowChanged -= Layer_RowChanged;
                    _ds.Layer.RowDeleted -= Layer_RowDeleted;
                    _ds.Layer.RowDeleting -= Layer_RowDeleting;
                    _ds.Layer.ColumnChanged -= Layer_ColumnChanged;
                    _ds.LabelRule.RowChanged -= LabelRule_RowChanged;
                    _ds.LabelRule.RowDeleting -= LabelRule_RowDeleting;
                    _ds.LabelRule.TableNewRow -= Layer_TableNewRow;
                    _ds.StyleRule.RowChanged -= StyleRule_RowChanged;
                    _ds.StyleRule.RowDeleting -= StyleRule_RowDeleting;
                    _ds.StyleRule.TableNewRow -= Layer_TableNewRow;
                    _ds.StyleRule.ColumnChanged -= StyleRule_ColumnChanged;
                }

                _ds = value;

                if (_ds != null)
                {
                    _ds.Theme.TableNewRow += Theme_TableNewRow;
                    _ds.Theme.RowChanged += Theme_RowChanged;
                    _ds.Theme.ColumnChanged += Theme_ColumnChanged;
                    _ds.Theme.RowDeleted += Theme_RowDeleted;
                    _ds.Layer.TableNewRow += Layer_TableNewRow;
                    _ds.Layer.RowChanged += Layer_RowChanged;
                    _ds.Layer.RowDeleted += Layer_RowDeleted;
                    _ds.Layer.RowDeleting += Layer_RowDeleting;
                    _ds.Layer.ColumnChanged += Layer_ColumnChanged;
                    _ds.LabelRule.RowChanged += LabelRule_RowChanged;
                    _ds.LabelRule.RowDeleting += LabelRule_RowDeleting;
                    _ds.LabelRule.TableNewRow += Layer_TableNewRow;
                    _ds.StyleRule.RowChanged += StyleRule_RowChanged;
                    _ds.StyleRule.RowDeleting += StyleRule_RowDeleting;
                    _ds.StyleRule.TableNewRow += Layer_TableNewRow;
                    _ds.StyleRule.ColumnChanged += StyleRule_ColumnChanged;
                    Bind();
                }
            }
        }

        private void Bind()
        {
            if ( _ds != null && !DesignMode)
                foreach (ThemeDS.ThemeRow theme in _ds.Theme.Rows)
                    AddThemeNode(theme);
        }

        void Theme_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            TreeNode node;
            if (e.Column == _ds.Theme.NameColumn && _objectsNode.TryGetValue(e.Row, out node))
                node.Text = (string)e.ProposedValue;
        }

        void Theme_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                TreeNode node = AddThemeNode((ThemeDS.ThemeRow)e.Row);
                node.EnsureVisible();
                _treeView.SelectedNode = node;
                if (_treeView.LabelEdit)
                    node.BeginEdit();
            }
        }

        void StyleRule_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            TreeNode node;
            if (e.Column.ColumnName == _ds.StyleRule.NameColumn.ColumnName && _objectsNode.TryGetValue(e.Row, out node))
                node.Text = (string)e.ProposedValue;
        }

        void Layer_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            var row = (ThemeDS.LayerRow)e.Row;

            //if (!row.IsParentIdNull())
            //    if (e.Column == _ds.Layer.MinScaleColumn)
            //    {
            //        if ((double)e.ProposedValue < row.LayerRowParent.MinScale)
            //            row.LayerRowParent.MinScale = (double)e.ProposedValue;
            //        else
            //            row.LayerRowParent.UpdateMinMaxScale();
            //    }
            //    else if (e.Column == _ds.Layer.MaxScaleColumn)
            //    {
            //        if (e.ProposedValue is DBNull)
            //            row.LayerRowParent.SetMaxScaleNull();
            //        else if (!row.LayerRowParent.IsMaxScaleNull() && (double)e.ProposedValue >= row.LayerRowParent.MaxScale)
            //            row.LayerRowParent.MaxScale = (double)e.ProposedValue;
            //        else
            //            row.LayerRowParent.UpdateMinMaxScale();
            //    }
        }

        void LabelRule_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        void LabelRule_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                var label = (ThemeDS.LabelRuleRow)e.Row;

                TreeNode node = AddLabelNode(_objectsNode[label.LayerRow], label);
                node.EnsureVisible();
                if (_treeView.LabelEdit)
                    node.BeginEdit();
            }
        }

        private ScaleMode _checkScaleMode;
        public ScaleMode CheckScaleMode
        {
            get
            {
                return _checkScaleMode;
            }
            set
            {
                _checkScaleMode = value;
                UpdateCheck(_visibilityScale);
            }
        }

        void StyleRule_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
        }

        void StyleRule_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                var style = (ThemeDS.StyleRuleRow)e.Row;
                var layerRow = style.LayerRow;
                if (layerRow == null) return;
                
                TreeNode themeItemNode;
                if (!_objectsNode.TryGetValue(style.LayerRow, out themeItemNode)) return;

                var node = AddStyleRuleNode(themeItemNode, style);
                node.EnsureVisible();
                _treeView.SelectedNode = node;
                if (_treeView.LabelEdit)
                    node.BeginEdit();
            }
        }

        private ThemeDS.LayerRow _layerToUpdateMinMax;

        void Layer_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            //var parent = ((ThemeDS.LayerRow)e.Row).LayerRowParent;
            //if (parent != null && parent.GetLayerRows().Length == 1)
            //    parent.Leaf = true;
            //_layerToUpdateMinMax = parent;

            TreeNode node;
            if (_objectsNode.TryGetValue(e.Row, out node))
            {
                _objectsNode.Remove(e.Row);
                node.Remove();
            }

        }

        void Layer_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            //_layerToUpdateMinMax.UpdateMinMaxScale();
            //_layerToUpdateMinMax = null;
        }

        void Layer_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            var layerRow = (ThemeDS.LayerRow)e.Row;
            if (e.Action == DataRowAction.Add)
            {
                //if (item.LayerRowParent == null) //is root?
                //    return;

                //item.LayerRowParent.UpdateMinMaxScale();

                var theme = layerRow.ThemeRow;
                if (theme == null) return;

                var node = AddLayerNode(_objectsNode[(layerRow.LayerGroupRow.IsParentGroupIdNull() ? (object)theme : layerRow.LayerGroupRow)], layerRow);
                node.EnsureVisible();
                _treeView.SelectedNode = node;
                if (_treeView.LabelEdit)
                    node.BeginEdit();
            }
        }

        void Layer_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            int i = 1;
            string name;
            do
            {
                name = "New " + e.Row.Table.TableName;
                if (i > 1)
                    name += " " + i;
                i++;
            } while (e.Row.Table.Select("name='" + name + "'").Length > 0);
            e.Row["name"] = name;
        }

        public void UnBind()
        {
            //if (_themesNode != null)
            //    _themesNode.Remove();
            //_themesNode = null;
            if (_treeView != null)
                _treeView.Nodes.Clear();
        }

        void Theme_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            TreeNode node = _objectsNode[e.Row];
            _objectsNode.Remove(e.Row);
            node.Remove();
        }

        void Theme_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            int i = 1;
            string name;
            do
            {
                name = "New " + e.Row.Table.TableName;
                if (i > 1)
                    name += " " + i;
                i++;
            } while (e.Row.Table.Select("name='" + name + "'").Length > 0);
            e.Row["Name"] = name;
        }

        private TreeNode AddThemeNode(ThemeDS.ThemeRow theme)
        {
            var node = _treeView.Nodes.Add(theme.Name);
            node.Tag = new Theme(theme);
            node.ContextMenuStrip = cmnTheme;

            _objectsNode.Add(theme, node);
            foreach (var root in theme.GetLayerGroupRows())
                foreach (var item in root.GetLayerRows())
                {
                    foreach (var group in root.GetLayerGroupRows())
                        AddGroupNode(node, group);
                    AddLayerNode(node, item);
                }

            return node;
        }

        private void AddGroupNode(TreeNode node, ThemeDS.LayerGroupRow layerGroupRow)
        {
            
        }

        private TreeNode AddLayerNode(TreeNode parent, ThemeDS.LayerRow layerRow)
        {
            if (parent == null) throw new ArgumentNullException();

            var node = parent.Nodes.Add(layerRow.Name);
            node.Tag = new Layer(layerRow);
            node.ContextMenuStrip = cmnLayer;

            _objectsNode.Add(layerRow, node);

            foreach (var style in layerRow.GetStyleRuleRows())
                AddStyleRuleNode(node, style);

            foreach (var label in layerRow.GetLabelRuleRows())
                AddLabelNode(node, label);

            return node;
        }

        private TreeNode AddLabelNode(TreeNode parent, ThemeDS.LabelRuleRow label)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (label == null) throw new ArgumentNullException("label");

            TreeNode node = parent.Nodes.Add(label.Name);
            node.Tag = new LabelRule(label);
            node.ContextMenuStrip = cmnLayer;

            _objectsNode.Add(label, node);

            return node;
        }

        private TreeNode AddStyleRuleNode(TreeNode parent, ThemeDS.StyleRuleRow styleRule)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (styleRule == null) throw new ArgumentNullException("styleRule");

            TreeNode node = parent.Nodes.Add(styleRule.Name);
            node.Tag = new StyleRule(styleRule);
            //node.ContextMenuStrip = cmnDisplay;

            _objectsNode.Add(styleRule, node);

            return node;
        }

        private void mnuThemesSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileName))
                SaveAs();
            else
                SaveAs(FileName);

            MessageBox.Show("Saved");
        }

        public string FileName { get; set; }

        private void mnuThemesAddNew_Click(object sender, EventArgs e)
        {
            AddNewTheme();
        }

        public void AddNewTheme()
        {
            var row = _ds.Theme.NewRow();
            _ds.Theme.Rows.Add(row);
        }


        private Dictionary<object, TreeNode> _objectsNode = new Dictionary<object, TreeNode>();

        private void mnuThemeRemove_Click(object sender, EventArgs e)
        {
            var node = TreeView.SelectedNode;
            var theme = (ThemeDS.ThemeRow)node.Tag;

            theme.Delete();
        }

        void mnuThemeExpandAll_Click(object sender, System.EventArgs e)
        {
            _treeView.SelectedNode.ExpandAll();
        }

        private void mnuThemeExport_Click(object sender, EventArgs e)
        {
            var node = TreeView.SelectedNode;
            var ds = (ThemeDS)node.Tag;
            var theme = (ThemeDS.ThemeRow)node.Tag;

            var newDs = new ThemeDS();
            newDs.Theme.ImportRow(theme);
            newDs.WriteXml("export theme.xml");
        }

        private void mnuThemeSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        void mnuDisplayRemove_Click(object sender, EventArgs e)
        {
            var node = TreeView.SelectedNode;
            var display = (ThemeDS.LayerRow)(Layer)node.Tag;
            Debug.Assert(display != null);

            display.Delete();
        }
                
        private void SaveAs()
        {
            if (saveFileDialog.ShowDialog(_treeView) == DialogResult.OK)
            {
                FileName = saveFileDialog.FileName;
                SaveAs(saveFileDialog.FileName);
            }
        }

        private void SaveAs(string filename)
        {
            _ds.WriteXml(filename);
        }

        void mnuDisplayAdd_Click(object sender, EventArgs e)
        {
            //TreeNode node = TreeView.SelectedNode;
            //var display = (ThemeDS.LayerRow)(Layer)node.Tag;
            //display.Leaf = false;
            //Debug.Assert(display != null);
            //Debug.Assert(display.ThemeRow != null);
            //var themeId = display.ThemeId;
            //display.Sequence = _ds.Layer.Count(x => x.ThemeId == themeId);

            //ThemeDS.LayerRow newDisplay = _ds.Layer.NewLayerRow();
            //newDisplay.ThemeRow = display.ThemeRow;
            //newDisplay.l = display;
            //newDisplay.MinScale = display.MinScale;
            //newDisplay.MaxScale = display.MaxScale;

            //_ds.Layer.Rows.Add(newDisplay);
        }

        void mnuDisplayAddStyle_Click(object sender, EventArgs e)
        {
            var node = TreeView.SelectedNode;
            var display = (ThemeDS.LayerRow)(Layer)node.Tag;
            Debug.Assert(display != null);
            Debug.Assert(display.ThemeRow != null);

            ThemeDS.StyleRuleRow newRule = _ds.StyleRule.NewStyleRuleRow();
            newRule.LayerRow = display;

            _ds.StyleRule.Rows.Add(newRule);
        }

        void mnuDisplayAddText_Click(object sender, EventArgs e)
        {
            var node = TreeView.SelectedNode;
            var display = (ThemeDS.LayerRow)(Layer)node.Tag;
            Debug.Assert(display != null);
            var newRule = _ds.LabelRule.NewLabelRuleRow();
            newRule.LayerRow = display;

            _ds.LabelRule.Rows.Add(newRule);
        }

        public object Cut()
        {
            if(_treeView.SelectedNode != null && _treeView.SelectedNode.Tag is INodeRow)
            {
               var o = (INodeRow )_treeView.SelectedNode.Tag;
               foreach(DataRelation relation in  o.DataRow.Table.ChildRelations)
                   if (relation.Nested)
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
    }
}