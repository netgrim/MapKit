using System.Drawing;
using System.Drawing.Drawing2D;
using Ciloci.Flee;
using MapKit.Core.Rendering;
using System.Diagnostics;

namespace MapKit.Core
{
    class SolidFillRenderer : FeatureRenderer
    {
        private GraphicsPath _path;
        private IGenericExpression<Color> _colorEvaluator;
        private IGenericExpression<float> _opacityEvaluator;
        private Renderer _renderer;
        private SolidFill _fill;
        private Color _color = Color.Black;
        private float _opacity = 1;

        public SolidFillRenderer(Renderer renderer, SolidFill fill, IBaseRenderer parent)
            : base(renderer, fill, parent)
        {
            _renderer = renderer;
            _fill = fill;
            _fill.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(fill_PropertyChanged);

            _path = new GraphicsPath();
        }

        void fill_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Compiled = false;
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            //if (Visible && !_compiled)
            //    Compile();
        }

        public override void Compile(bool recursive = false)
        {
            Debug.Assert(InputFeatureType != null);
            Renderer.FeatureVarResolver.FeatureType = InputFeatureType;

            //var context = CreateContext(Renderer);
            //var colorContext = CreateColorContext();

            //_resolver = new FeatureVariableResolver(InputFeatureType);
            //_resolver.BindContext(context);
            //_resolver.BindContext(colorContext);

            var context = Renderer.Context;
            var colorContext = context;
            
            _colorEvaluator = CompileColorExpression(colorContext, SolidFill.ColorPropertyName, _fill.Color, ref _color);
            _opacityEvaluator = CompileFloatExpression(context, SolidFill.OpacityPropertyName, _fill.Opacity, ref _opacity);
            
            base.Compile(false);
        }

        public override void Render(Feature feature)
        {
            //_resolver.Feature = feature;

            var color = GetRenderColor(_opacityEvaluator, _opacity, _colorEvaluator, _color);

            if (feature != null)
            {
                _path.Reset();
                foreach (var lineString in Renderer.GetLineString(feature.Geometry))
                    AddToPath(lineString, _path);

                using (var brush = new SolidBrush(color))
                    Renderer.Graphics.FillPath(brush, _path);
            }

            //using (var path = new GraphicsPath())
            //{
            //    switch (geometry.GeometryType)
            //    {
            //        case GeometryType2.Polygon:
            //            FillPolygon(brush, path, (Polygon)geometry);
            //            break;
            //        case  GeometryType2.MultiPolygon:
            //            foreach (var polygon in ((MultiPolygon)geometry).Polygons)
            //            {
            //                FillPolygon(brush, path, polygon);
            //                path.Reset();
            //            }
            //            break;
            //        case GeometryType2.GeometryCollection:
            //            throw new NotImplementedException();
            //        default:
            //            throw new Exception("Unsuported geometry type for solid fill: " + geometry.GeometryType);
            //    }
            //}
        }
    }
}
