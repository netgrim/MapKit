using GeoAPI.Geometries;
using Ciloci.Flee;
using NetTopologySuite.LinearReferencing;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class LinearCalibrationRenderer : ContainerNodeRenderer
    {
        private LinearCalibration _linearCalibration;
        private IGenericExpression<double> _startMeasureEvaluator;
        private IGenericExpression<double> _endMeasureEvaluator;
        private bool _compiled;
        private double _startMeasure = 0;
        private double _endMeasure = 0;


        //private FeatureVariableResolver _resolver;

        public LinearCalibrationRenderer(Renderer renderer, LinearCalibration linearCalibration, IBaseRenderer parent)
            : base(renderer, linearCalibration, parent)
        {
            _linearCalibration = linearCalibration;
            _linearCalibration.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_linearCalibration_PropertyChanged);
        }

        void _linearCalibration_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        public override void Render(Feature feature)
        {
            var newFeature = new Feature(feature.FeatureType, feature.Fid, feature.Values);
            newFeature.Geometry = CalibrateGeometry(feature.Geometry);

            //_resolver.Feature = newFeature;
            Renderer.FeatureVarResolver.Feature = newFeature;
            try
            {
                //RenderPath(node, newFeature, path);
                base.Render(newFeature);
            }
            finally
            {
                Renderer.FeatureVarResolver.Feature = feature;
            }
        }

        public IGeometry CalibrateGeometry(IGeometry geometry)
        {
            var startMeasure = Evaluate(_startMeasureEvaluator, _startMeasure);
            var endMeasure = string.IsNullOrWhiteSpace(_linearCalibration.EndMeasure)
                ? geometry.Length
                : Evaluate(_endMeasureEvaluator, _endMeasure);
            return LinearGeometryBuilder.DefineMeasures(geometry, startMeasure, endMeasure);
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            if (!_compiled && Visible)
                Compile();
        }

        public override void Compile(bool recursive = false)
        {
            base.Compile(recursive);

            //var context = CreateContext(Renderer);
            //_resolver = new FeatureVariableResolver(OutputFeatureType);
            //_resolver.BindContext(context);

            _startMeasureEvaluator = CompileDoubleExpression(LinearCalibration.StartMeasureField, _linearCalibration.StartMeasure, ref _startMeasure);
            _endMeasureEvaluator = CompileDoubleExpression(LinearCalibration.EndMeasureField, _linearCalibration.EndMeasure, ref _endMeasure);
            _compiled = true;
        }
    }
}
