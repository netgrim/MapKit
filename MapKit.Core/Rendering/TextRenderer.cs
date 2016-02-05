using System;
using Ciloci.Flee;
using GeoAPI.Geometries;
using System.Drawing;
using System.Drawing.Drawing2D;

using WinPoint = System.Windows.Point;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class TextRenderer : FeatureRenderer
    {
        private IGenericExpression<double> _angleEvaluator;
        private IDynamicExpression _contentEvaluator;
        private IFeatureRenderer _labelBoxRenderer;
        WinPoint _tempPoint;
        private IGenericExpression<double> _sizeEvaluator;
        private IGenericExpression<ContentAlignment> _alignEvaluator;
        private IGenericExpression<string> _fontEvaluator;
        private IGenericExpression<Color> _colorEvaluator;
        private IGenericExpression<bool> _allowOverlapEvaluator;
        private IGenericExpression<bool> _overlapableEvaluator;
        private IGenericExpression<float> _opacityEvaluator;
        private IGenericExpression<double> _scaleXEvaluator;
        private IGenericExpression<double> _scaleYvaluator;
        //private FeatureVariableResolver _resolver;
        private Text _text;
        private bool _compiled;
        private Color _color = Color.Black;
        private float _opacity = 1;
        private double _angle = 0;
        private double _scaleX = 1;
        private double _scaleY = 1;
        private double _size = 10;
        private ContentAlignment _alignment = ContentAlignment.BottomLeft;
        private object _content;
        private bool _overlapable;
        private bool _allowOverlap;

        public TextRenderer(Renderer renderer, Text text, IBaseRenderer parent)
            : base(renderer, text, parent)
        {
            _text = text;
            _text.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_text_PropertyChanged);
        }

        public override void Compile(bool recursive = false)
        {
            //Debug.Assert(InputFeatureType != null);

            if (_text.LabelBox != null)
            {
                var renderer = _text.LabelBox.Renderer as IFeatureRenderer;
                if (renderer != null)
                {
                    renderer.InputFeatureType = InputFeatureType;
                    if (recursive)
                        renderer.Compile(true);
                }
            }

            var context = Renderer.Context;
            //var colorContext = context;
            //var contentAlignmentContext = context;

            //_resolver = new FeatureVariableResolver(InputFeatureType);
            //_resolver.BindContext(context);
            //_resolver.BindContext(colorContext);
            //_resolver.BindContext(contentAlignmentContext);

            var oldFeatureType = Renderer.FeatureVarResolver.FeatureType;
            Renderer.FeatureVarResolver.FeatureType = InputFeatureType;
            try
            {
                _contentEvaluator = CompileExpression(context, TextStyle.ContentField, _text.Content, ref _content);
                _angleEvaluator = CompileDoubleExpression(context, TextStyle.AngleField, _text.Angle, ref _angle);
                _sizeEvaluator = CompileDoubleExpression(context, TextStyle.SizeField, _text.Size, ref _size);
                _alignEvaluator = CompileEnumExpression(context, TextStyle.AlignmentField, _text.Alignment, ref _alignment);
                _fontEvaluator = CompileExpression<string>(context, TextStyle.FontField, _text.Font);
                _colorEvaluator = CompileColorExpression(context, TextStyle.ColorField, _text.Color, ref _color);
                _scaleXEvaluator = CompileDoubleExpression(context, TextStyle.ScaleXField, _text.ScaleX, ref _scaleX);
                _scaleYvaluator = CompileDoubleExpression(context, TextStyle.ScaleYField, _text.ScaleY, ref _scaleY);
                _opacityEvaluator = CompileFloatExpression(context, TextStyle.OpacityField, _text.Opacity, ref _opacity);
                _overlapableEvaluator = CompileBoolExpression(context, TextStyle.OverlapableField, _text.Overlappable, ref _overlapable);
                _allowOverlapEvaluator = CompileBoolExpression(context, TextStyle.AllowOverlapField, _text.AllowOverlap, ref _allowOverlap);
            }
            finally
            {
                Renderer.FeatureVarResolver.FeatureType = oldFeatureType;
            }

            //_labelBoxRenderer = _text.LabelBox != null && _text.LabelBox.Renderer != null && !string.IsNullOrWhiteSpace(_text.Content)
            //    ? _text.LabelBox.Renderer as IFeatureRenderer
            //    : null;
            _labelBoxRenderer = _text.LabelBox != null ? new ContainerNodeRenderer(Renderer, _text.LabelBox, this) : null;

            _compiled = true;
        }

        void _text_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        public override void Render(Feature feature)
        {
            if (feature == null) return;

            //Renderer.FeatureVarResolver.Feature = feature;

            var size = (float)Evaluate(_sizeEvaluator, _size);
            if (Renderer.Zoom * size < 1 || (_contentEvaluator == null && _content == null)) return; //culling
            var oldMatrix = Renderer.Graphics.Transform;

            var content = Convert.ToString(_contentEvaluator != null ? _contentEvaluator.Evaluate() : _content);
            if (string.IsNullOrWhiteSpace(content)) return;

            var angle = (float)Evaluate(_angleEvaluator, _angle);
            var alignment = Evaluate(_alignEvaluator, _alignment);

            if (_text.KeepUpward && Math.Abs((angle + Node.Map.Angle) % 360) > 90)
            {
                angle += 180;
                alignment = FlipXY(alignment);
            }

            using (var f = new Font(Evaluate(_fontEvaluator, "Browallia New"), size))
            using (var brush = new SolidBrush(GetRenderColor(_opacityEvaluator, _opacity, _colorEvaluator, _color)))
            {
                var stringSize = Renderer.Graphics.MeasureString(content, f, 0, StringFormat.GenericTypographic);
                foreach (var point in Renderer.GetPoint(feature.Geometry))
                    RenderText(feature, point, size, angle, content, f, brush, stringSize, alignment);
            }

            Renderer.Graphics.Transform = oldMatrix;
        }

        private ContentAlignment FlipXY(ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.BottomCenter: return ContentAlignment.TopCenter;
                case ContentAlignment.BottomLeft: return ContentAlignment.TopRight;
                case ContentAlignment.BottomRight: return ContentAlignment.TopLeft;
                case ContentAlignment.MiddleCenter: return ContentAlignment.MiddleCenter;
                case ContentAlignment.MiddleLeft: return ContentAlignment.MiddleRight;
                case ContentAlignment.MiddleRight: return ContentAlignment.MiddleLeft;
                case ContentAlignment.TopCenter: return ContentAlignment.BottomCenter;
                case ContentAlignment.TopLeft: return ContentAlignment.BottomRight;
                case ContentAlignment.TopRight: return ContentAlignment.BottomLeft;
                default: throw new System.ComponentModel.InvalidEnumArgumentException();
            }
        }

        private void RenderText(Feature feature, IPoint geometry, float size, float angle, string content, Font f, Brush brush, SizeF stringSize, ContentAlignment alignment)
        {
            geometry.ToWinPoint(ref _tempPoint);
            var pointF = Renderer.Transform(_tempPoint).ToPointF();
            var basePoint = new PointF(pointF.X, pointF.Y);

            
            pointF.Y -= (float)GetVerticalAlignentOffset(alignment, stringSize.Height);
            pointF.X += (float)GetHorizontalAligmentOffset(alignment, stringSize.Width);



            //using (var pen = new Pen(text.Color, 1 / (float)_scale))
            //{
            //    _g.DrawLine(pen, basePoint.X, basePoint.Y, basePoint.X + stringSize.Width, basePoint.Y);
            //    _g.DrawEllipse(pen, basePoint.X - 2, basePoint.Y - 2, 4, 4);
            //}

            var m = new Matrix();
            m.RotateAt(-angle, basePoint);
            var scaleX = (float)Evaluate(_scaleXEvaluator, _scaleX);
            var scaleY = (float)Evaluate(_scaleXEvaluator, _scaleY);

            m.Multiply(new Matrix(scaleX, 0.0f, 0.0f, scaleY, basePoint.X - (scaleX * basePoint.X), basePoint.Y - (scaleY * basePoint.Y)));
            Renderer.Graphics.MultiplyTransform(m);

            if (_labelBoxRenderer != null && _labelBoxRenderer.Visible) 
            {
                var oldModelView = Renderer.Translate;
                Renderer.Translate = System.Windows.Media.Matrix.Identity;

                var polygon = new RectangleF(pointF, stringSize).ToRectanglePolygon();
                var newFeature = feature;
                newFeature.Geometry = polygon;
                _labelBoxRenderer.Render(feature);

                Renderer.Translate = oldModelView;
            }

            Renderer.Graphics.DrawString(content, f, brush, pointF, StringFormat.GenericTypographic);
        }

        //private static int ReplaceFields(Feature feature, StringBuilder sb, string[] names, int i)
        //{
        //    for (int f = 0; f < names.Length; f++)
        //    {
        //        var token = "[" + names[f] + "]";
        //        if (EqualsAt(sb, token, i))
        //        {
        //            var value = Convert.ToString(feature[f]);
        //            sb.Replace(token, value, i, token.Length);
        //            return i + value.Length;
        //        }
        //    }
        //    return i + 1;
        //}

        //private static bool EqualsAt(StringBuilder sb, string name, int index)
        //{
        //    var length = name.Length;
        //    if (sb.Length - index < length)
        //        return false;

        //    for (int i = 0; i < length; i++)
        //        if (sb[index + i] != name[i])
        //            return false;
        //    return true;
        //}

        //public string FormatContent(Feature feature)
        //{
        //    var first = Content.IndexOf('[');
        //    if (first < 0)
        //        return Content;

        //    var regex = new Regex(@"\[\s*(\w+)\s*\]");
        //    return regex.Replace(Content, match => GetVariableValue(match, feature));

        //    //var sb = new StringBuilder(Content);
        //    //var names = feature.FeatureType.GetNames();

        //    //var i = first;
        //    //while (i < sb.Length)
        //    //{
        //    //    if (sb[i] == '[')

        //    //        //find field
        //    //        i = ReplaceFields(feature, sb, names, i);
        //    //    else
        //    //        i++;
        //    //}

        //    //return sb.ToString();
        //}

        //private string GetVariableValue(Match match, Feature feature)
        //{
        //    var name = match.Groups[1].Value;

        //    return Convert.ToString(feature[name]);
        //    //return match.Value;
        //}

        public override void BeginScene(bool visible)
        {
           base.BeginScene(visible);
           if (Visible && !_compiled)
                Compile();

            if (_labelBoxRenderer != null)
                _labelBoxRenderer.BeginScene(Visible);
        }
    }
}
