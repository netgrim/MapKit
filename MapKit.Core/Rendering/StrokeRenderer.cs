using System.Drawing;
using Ciloci.Flee;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using MapKit.Core.Rendering;
using System.Text;
using System.Diagnostics;

namespace MapKit.Core
{
    class StrokeRenderer : FeatureRenderer
    {
        private Stroke _stroke;

        private IGenericExpression<double> _widthEvaluator;
        private IGenericExpression<Color> _colorEvaluator;
        private IGenericExpression<DashStyle> _dashStyleEvaluator;
        private IGenericExpression<float[]> _dashArrayEvaluator;
        private IGenericExpression<float> _dashOffsetEvaluator;
        private IGenericExpression<float> _opacityEvaluator;
        private IGenericExpression<LineCap> _startCapEvaluator;
        private IGenericExpression<LineCap> _endCapEvaluator;
        private IGenericExpression<LineJoin> _joinEvaluator;
        private IGenericExpression<float> _mitterLimitEvaluator;
        private Color _color = Color.Black;
        private float _opacity = 1;
        private double _width = 1;
        private DashStyle _dash = DashStyle.Solid;
        private int _invalidGeometry;
        private LineCap _startCap;
        private LineCap _endCap;

        public StrokeRenderer(Renderer renderer, Stroke stroke, IBaseRenderer parent)
            :base(renderer, stroke, parent)
        {
            _stroke = stroke;
            stroke.PropertyChanged += new PropertyChangedEventHandler(stroke_PropertyChanged);
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            //if (Visible && !_compiled)
            //    Compile();
            _invalidGeometry = 0;
        }


        public override void Compile(bool recursive = false)
        {
            Debug.Assert(InputFeatureType != null);
            Renderer.FeatureVarResolver.FeatureType = InputFeatureType;

            //var context = CreateContext(Renderer);
            //var colorContext = CreateColorContext();
            //var dashStyleContext = CreateDashStyleContext();
            //var lineCapContext = CreateLineCapContext();
            //var lineJoinContext = CreateLineJoinContext();
            //var dashArrayContext = CreateDashArrayContext();

            //_resolver = new FeatureVariableResolver(InputFeatureType);
            //_resolver.BindContext(context);
            //_resolver.BindContext(colorContext);
            //_resolver.BindContext(dashStyleContext);
            //_resolver.BindContext(lineCapContext);
            //_resolver.BindContext(lineJoinContext);
            //_resolver.BindContext(dashArrayContext);
            var context = Renderer.Context;
            var colorContext = Renderer.Context;
            var dashStyleContext = Renderer.Context;
            var dashArrayContext = Renderer.Context;
            var lineCapContext = Renderer.Context;
            var lineJoinContext = Renderer.Context;
     
            _widthEvaluator = CompileDoubleExpression(context, LineStyle.WidthField, _stroke.Width, ref _width);
            _colorEvaluator = CompileColorExpression(colorContext, LineStyle.ColorField, _stroke.Color, ref _color);
            _dashStyleEvaluator = CompileEnumExpression(dashStyleContext, LineStyle.DashStyleField, _stroke.DashStyle, ref _dash);
            _dashArrayEvaluator = CompileExpression<float[]>(dashArrayContext, LineStyle.DashArrayField, _stroke.DashArray);
            _dashOffsetEvaluator = CompileExpression<float>(context, LineStyle.DashOffsetField, _stroke.DashOffset);
            _opacityEvaluator = CompileFloatExpression(context, LineStyle.OpacityField, _stroke.Opacity, ref _opacity);
            _startCapEvaluator = CompileEnumExpression(lineCapContext, LineStyle.StartCapField, _stroke.StartCap, ref _startCap);
            _endCapEvaluator = CompileEnumExpression(lineCapContext, LineStyle.EndCapField, _stroke.EndCap, ref _endCap);
            _joinEvaluator = CompileExpression<LineJoin>(lineJoinContext, LineStyle.JoinField, _stroke.Join);
            _mitterLimitEvaluator = CompileExpression<float>(context, LineStyle.MiterLimitField, _stroke.Miterlimit);

            base.Compile(recursive);
        }

        void stroke_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Stroke.VisiblePropertyName) return;
            Compiled = false;
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
            if (feature == null) return;

            using (var pen = new Pen( GetRenderColor(_opacityEvaluator, _opacity, _colorEvaluator, _color), (float)Evaluate(_widthEvaluator, _width)))
            {
                var dashArray = Evaluate(_dashArrayEvaluator, null);
                if (dashArray != null)
                {
                    pen.DashStyle = DashStyle.Custom;
                    //var parts = dashArray.Split(' ');
                    //var dashes = new float[parts.Length];
                    //for (int i = 0; i < parts.Length; i++)
                    //    dashes[i] = float.Parse(parts[i], System.Globalization.NumberFormatInfo.InvariantInfo) / _stroke.Width;
                    pen.DashPattern = dashArray;
                }
                else                 //dashStyle
                    pen.DashStyle = Evaluate(_dashStyleEvaluator, _dash);

                if (_dashOffsetEvaluator != null)
                    pen.DashOffset = _dashOffsetEvaluator.Evaluate();

                foreach (var lineString in Renderer.GetLineString(feature.Geometry))
                    if (lineString.NumPoints > 1)
                    {
                        var points = Renderer.TransformToPointsF(lineString.CoordinateSequence);
                        Renderer.Graphics.DrawLines(pen, points);

                        //Renderer.Graphics.Transform.TransformPoints(points);
                        //Renderer.Svg.WriteStartElement("polyline");
                        //var pointSvg = new StringBuilder();
                        //for (var i = 0; i < lineString.NumPoints; i++)
                        //{
                        //    if (i > 0)
                        //        pointSvg.Append(" ");
                        //    pointSvg.Append(points[i].X.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
                        //    pointSvg.Append(",");
                        //    pointSvg.Append(points[i].Y.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
                        //}

                        //Renderer.Svg.WriteAttributeString("points", pointSvg.ToString());
                        ////Renderer.Svg.WriteAttributeString("style", "fill:none;stroke:black");
                        //Renderer.Svg.WriteEndElement();
                    }
                    else
                        _invalidGeometry++;
            }
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
