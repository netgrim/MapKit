using System;
using System.Collections.Generic;
using Ciloci.Flee;
using System.Diagnostics;
using System.ComponentModel;
using MapKit.Core.Rendering;
using System.Collections;

namespace MapKit.Core
{
    class CaseRenderer : IFeatureRenderer
    {
        private Case _caseNode;
        private IDynamicExpression _exprEval;
        private List<IExpression> _expressions;
        private bool _compiled;
        private Renderer _renderer;
        private IList<IBaseRenderer> _renderers;
        private object _caseValue;
        private ArrayList _whenValues;

        public CaseRenderer(Renderer renderer, Case caseNode, IBaseRenderer parent)
        {
            Debug.Assert(renderer != null && caseNode != null);

            _caseNode = caseNode;
            _caseNode.PropertyChanged += new PropertyChangedEventHandler(_caseNode_PropertyChanged);
            _caseNode.Nodes.ItemAdded += new EventHandler<ItemEventArgs<ThemeNode>>(Nodes_ItemAdded);
            _caseNode.Nodes.ItemRemoved += new EventHandler<ItemEventArgs<ThemeNode>>(Nodes_ItemRemoved);

            foreach (var child in _caseNode.Nodes)
                AddHandler(child);

            _renderer = renderer;
            Parent = parent;
        }

        public ThemeNode Node
        {
            get { return _caseNode; }
        }

        void Nodes_ItemRemoved(object sender, ItemEventArgs<ThemeNode> e)
        {
            e.Item.PropertyChanged += new PropertyChangedEventHandler(Item_PropertyChanged);
        }

        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ThemeNode.VisiblePropertyName) return;
            _compiled = false;
        }

        void Nodes_ItemAdded(object sender, ItemEventArgs<ThemeNode> e)
        {
            AddHandler(e.Item);
        }

        private void AddHandler(ThemeNode themeNode)
        {
            themeNode.PropertyChanged -= new PropertyChangedEventHandler(Item_PropertyChanged);
        }

        [Category(Constants.CatStatistics)]
        public int RenderCount { get; set; }

        [Category(Constants.CatStatistics)]
        public int FeatureCount { get; private set; }

        public FeatureType InputFeatureType { get; set; }

        void _caseNode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        public void Render(Feature feature)
        {
            //todo get the index
            //object value = feature[feature.Values.Length - 1];
            //if (value is DBNull) return;
            //int index = Convert.ToInt32(value);
            var count = _expressions.Count;
            int index;

            if (_exprEval != null || _caseValue != null)
            {
                index = -1;
                var value = _exprEval != null ? _exprEval.Evaluate() : _caseValue;
                for (int i = 0; i < _expressions.Count; i++)
                    if (Equals(value, _expressions[i] != null ? ((IDynamicExpression)_expressions[i]).Evaluate() : _whenValues[i]))
                    {
                        index = i;
                        break;
                    }
            }
            else
                index = _expressions.FindIndex(e => e != null && ((IGenericExpression<bool>)e).Evaluate());

            ContainerNodeRenderer branchRenderer;
            if (index >= 0)
                branchRenderer = _caseNode.Nodes[index].Renderer as ContainerNodeRenderer;
            else if (_caseNode.Nodes.Count > _expressions.Count)
                branchRenderer = _caseNode.Nodes[count].Renderer as ContainerNodeRenderer;
            else
                return;

            branchRenderer.Render(feature);

            RenderCount++;
        }

        public void BeginScene(bool visible)
        {
            Visible = visible && _caseNode.IsVisibleAt(_renderer.Zoom);

            if (Visible && !_compiled)
                Compile();

            foreach (var renderer in _renderers)
                renderer.BeginScene(Visible);

            RenderCount = 0;
        }

        public void Compile(bool recursive = false)
        {
            //var context = FeatureRenderer.CreateContext();
            //if (InputFeatureType != null)
            //{
            //    _resolver = new FeatureVariableResolver(InputFeatureType);
            //    _resolver.BindContext(context);
            //}
            var _context = _renderer.Context;
            _exprEval = FeatureRenderer.CompileExpression(_caseNode, _context, Case.ExpressionField, _caseNode.Expression, ref _caseValue);
            _expressions = new List<IExpression>(_caseNode.Nodes.Count);
            _whenValues = new ArrayList(_caseNode.Nodes.Count);

            _renderers = new List<IBaseRenderer>(_caseNode.Nodes.Count);
            foreach (ContainerNode child in _caseNode.Nodes)
            {
                var renderer = child.Renderer ?? (child.Renderer = _renderer.CreateRenderer(child, null, this));
                if (renderer == null) continue;

                var when = child as When;
                if (when != null)
                {
                    object whenValue = null;
                    var expr = when.Expression != null ?
                        _exprEval != null || _caseValue != null
                            ? (IExpression)FeatureRenderer.CompileExpression(_caseNode, _context, When.ExpressionField, when.Expression, ref whenValue)
                            : FeatureRenderer.CompileExpression<bool>(_renderer, _caseNode, _context, When.ExpressionField, when.Expression)
                        : null;

                    _expressions.Add(expr);
                    _whenValues.Add(whenValue);
                }
                else if (!(child is Else))
                    continue;
                
                _renderers.Add(renderer);

                if (recursive)
                    renderer.Compile(true);
            }
            
            _compiled = true;
        }

        public bool Visible { get; private set; }

        public IBaseRenderer Parent { get; set; }

        public IBaseRenderer FindChildByName(string name)
        {
            return null;
        }
    }
}
