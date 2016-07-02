using System.Drawing;
using Ciloci.Flee;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using MapKit.Core.Rendering;
using System;

namespace MapKit.Core
{
    class LineStyleRenderer : NodeRenderer
    {
        private LineStyle _lineStyle;

        //private IGenericExpression<float> _widthEvaluator;
        private IGenericExpression<Color> _colorEvaluator;
        //private IGenericExpression<DashStyle> _dashStyleEvaluator;
        //private IGenericExpression<float[]> _dashArrayEvaluator;
        //private IGenericExpression<float> _dashOffsetEvaluator;
        //private IGenericExpression<float> _opacityEvaluator;
        //private IGenericExpression<LineCap> _startCapEvaluator;
        //private IGenericExpression<LineCap> _endCapEvaluator;
        //private IGenericExpression<LineJoin> _joinEvaluator;
        //private IGenericExpression<float> _mitterLimitEvaluator;
        //private FeatureVariableResolver _resolver;
        private bool _compiled;

        private Color _defaultColor = Color.Black;

        public LineStyleRenderer(Renderer renderer, LineStyle lineStyle, IBaseRenderer parent)
            : base(renderer, lineStyle, parent)
        {
            _lineStyle = lineStyle;
            lineStyle.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(stroke_PropertyChanged);
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            //if (Visible && !_compiled)
            //    Compile();
        }

        public override void Compile(bool recursive = false)
        {

            //if(_lineStyle.Parent == null) return;
            //var index = _lineStyle.Parent.Nodes.IndexOf(_lineStyle);
            //if (index < 0) return;

            //var nodes = _lineStyle.Parent.Nodes;
            //for (int i = index + 1; i < nodes.Count; i++)
            //    if (nodes[i].Cascade(_lineStyle))
            //        return;
            
        //    Debug.Assert(InputFeatureType != null);

        //    Style referencedStyle;
        //    if (string.IsNullOrEmpty(_lineStyle.Reference))
        //        if (Renderer.Styles.TryGetValue(_lineStyle.Reference, out referencedStyle))
        //            _lineStyle.Cascade(referencedStyle);
        //        else
        //            Trace.WriteLine(_lineStyle.Reference);

        //    var context = CreateContext(Renderer);
        //    var colorContext = CreateColorContext();
        //    var dashStyleContext = CreateDashStyleContext();
        //    var lineCapContext = CreateLineCapContext();
        //    var lineJoinContext = CreateLineJoinContext();
        //    var dashArrayContext = CreateDashArrayContext();

        //    _resolver = new FeatureVariableResolver(InputFeatureType);
        //    _resolver.BindContext(context);
        //    _resolver.BindContext(colorContext);
        //    _resolver.BindContext(dashStyleContext);
        //    _resolver.BindContext(lineCapContext);
        //    _resolver.BindContext(lineJoinContext);
        //    _resolver.BindContext(dashArrayContext);

        //    _widthEvaluator = CompileExpression<float>(context, LineStyle.WidthField, _lineStyle.Width);
        _colorEvaluator = CompileColorExpression(Renderer.Context, LineStyle.ColorField, _lineStyle.Color, ref _defaultColor);

        //    _dashStyleEvaluator = CompileExpression<DashStyle>(dashStyleContext, LineStyle.DashStyleField, _lineStyle.DashStyle);
        //    _dashArrayEvaluator = CompileExpression<float[]>(dashArrayContext, LineStyle.DashArrayField, _lineStyle.DashArray);
        //    _dashOffsetEvaluator = CompileExpression<float>(context, LineStyle.DashOffsetField, _lineStyle.DashOffset);
        //    _opacityEvaluator = CompileExpression<float>(context, LineStyle.OpacityField, _lineStyle.Opacity);
        //    _startCapEvaluator = CompileExpression<LineCap>(lineCapContext, LineStyle.StartCapField, _lineStyle.StartCap);
        //    _endCapEvaluator = CompileExpression<LineCap>(lineCapContext, LineStyle.EndCapField, _lineStyle.EndCap);
        //    _joinEvaluator = CompileExpression<LineJoin>(lineJoinContext, LineStyle.JoinField, _lineStyle.Join);
        //    _mitterLimitEvaluator = CompileExpression<float>(context, LineStyle.MiterLimitField, _lineStyle.Miterlimit);

            _compiled = true;
        }

        void stroke_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Stroke.VisiblePropertyName) return;
            //_compiled = false;
        }

        private ExpressionContext CreateDashArrayContext()
        {
            return CreateContext(new ExpressionOwner<float>());
        }

        private ExpressionContext CreateDashStyleContext()
        {
            var context = CreateContext();
            context.Imports.AddType(typeof(DashStyle));
            return context;
        }
        
        private ExpressionContext CreateLineCapContext()
        {
            var context = CreateContext();
            context.Imports.AddType(typeof(LineCap));
            return context;
        }

        private ExpressionContext CreateLineJoinContext()
        {
            var context = CreateContext();
            context.Imports.AddType(typeof(LineJoin));
            return context;
        }

        public override void Render(Feature feature)
        {
            //LineStyle targetStyle;
            //if (!Renderer.LineStyles.TryGetValue(_lineStyle.Name, out targetStyle))
            //    Renderer.LineStyles.Add(_lineStyle.Name, targetStyle = (LineStyle)_lineStyle.Clone());


            //GetRenderColor(_opacityEvaluator, _colorEvaluator), Evaluate(_widthEvaluator, 1f))

        //    _resolver.Feature = feature;

            //    using (var pen = new Pen(GetRenderColor(_opacityEvaluator, _colorEvaluator), Evaluate(_widthEvaluator, 1f)) )
        //    {
        //        var dashArray = Evaluate<float[]>(_dashArrayEvaluator, null);
        //        if (dashArray != null)
        //        {
        //            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
        //            //var parts = dashArray.Split(' ');
        //            //var dashes = new float[parts.Length];
        //            //for (int i = 0; i < parts.Length; i++)
        //            //    dashes[i] = float.Parse(parts[i], System.Globalization.NumberFormatInfo.InvariantInfo) / _stroke.Width;
        //            pen.DashPattern = dashArray;
        //        }
        //        else                 //dashStyle
        //        pen.DashStyle = Evaluate(_dashStyleEvaluator, DashStyle.Solid);

        //        if (_dashOffsetEvaluator != null)
        //            pen.DashOffset = _dashOffsetEvaluator.Evaluate();

                
        //        foreach (var lineString in Renderer.GetLineString(feature.Geometry))
        //            Renderer.Graphics.DrawLines(pen, TransformToPointsF(lineString.Vertices));
        //    }
        }

        class ExpressionOwner<T>
        {
            public T[] Array(params T[] elements)
            {
                return elements;
            }
        }
    }
}
