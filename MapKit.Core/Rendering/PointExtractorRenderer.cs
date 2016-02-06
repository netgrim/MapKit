using System;
using System.Collections.Generic;
using GeoAPI.Geometries;
using System.ComponentModel;
using NetTopologySuite.LinearReferencing;
using Ciloci.Flee;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class PointExtractorRenderer : ContainerNodeRenderer
    {
        private PointExtractor _pointExtractor;
        private int _xIndex;
        private int _yIndex;
        private int _zIndex;
        private int _mIndex;
        private int _angleIndex;
        private IGenericExpression<double> _startEvaluator;
        private IGenericExpression<double> _endEvaluator;
        private IGenericExpression<double> _incEvaluator;
        private IGenericExpression<double> _offsetEvaluator;
        //private FeatureVariableResolver _featureVarResolver;
        private bool _compiled;
        private FeatureType _outputFeatureType;
        private double _start = 0;
        private double _end;
        private double _increment;
        private double _offset;

        public PointExtractorRenderer(Renderer renderer, PointExtractor pointExtractor, IBaseRenderer parent)
            : base(renderer, pointExtractor, parent)
        {
            _pointExtractor = pointExtractor;
            _pointExtractor.PropertyChanged += new PropertyChangedEventHandler(_pointExtractor_PropertyChanged);
        }

        void _pointExtractor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        public override void Compile(bool recursive = false)
        {
            _outputFeatureType = WrapFeatureType(_pointExtractor, InputFeatureType);
            base.Compile(_outputFeatureType, recursive);

            //var context = CreateContext();
            //_featureVarResolver = new FeatureVariableResolver(OutputFeatureType);
            //_featureVarResolver.BindContext(context);

            _startEvaluator = CompileDoubleExpression(PointExtractor.StartField, _pointExtractor.Start, ref _start, false);
            _endEvaluator = CompileDoubleExpression(PointExtractor.EndField, _pointExtractor.End, ref _end, false);
            _incEvaluator = CompileDoubleExpression(PointExtractor.IncField, _pointExtractor.Increment, ref _increment, false);
            _offsetEvaluator = CompileDoubleExpression(PointExtractor.OffsetField, _pointExtractor.Offset, ref _offset);
            
            _compiled = true;
        }

        public FeatureType WrapFeatureType(PointExtractor pointExtractor, FeatureType featureType)
        {
            var newFeatureType = (FeatureType)featureType.Clone();

            _xIndex = !string.IsNullOrEmpty(pointExtractor.XColumn) ? newFeatureType.AddAttributes(pointExtractor.XColumn, typeof(double)).Ordinal : -1;
            _yIndex = !string.IsNullOrEmpty(pointExtractor.YColumn) ? newFeatureType.AddAttributes(pointExtractor.YColumn, typeof(double)).Ordinal : -1;
            _zIndex = !string.IsNullOrEmpty(pointExtractor.ZColumn) ? newFeatureType.AddAttributes(pointExtractor.ZColumn, typeof(double)).Ordinal : -1;
            _mIndex = !string.IsNullOrEmpty(pointExtractor.MColumn) ? newFeatureType.AddAttributes(pointExtractor.MColumn, typeof(double)).Ordinal : -1;
            _angleIndex = !string.IsNullOrEmpty(pointExtractor.AngleColumn) ? newFeatureType.AddAttributes(pointExtractor.AngleColumn, typeof(double)).Ordinal : -1;

            return newFeatureType;
        }

        public override void Render(Feature feature)
        {
            try
            {
                foreach (var newFeature in GetFeatures(feature))
                {
                    Renderer.FeatureVarResolver.Feature = newFeature;

                    base.Render(newFeature);
                }
            }
            finally
            {
                Renderer.FeatureVarResolver.Feature = feature;
            }
        }

        private IEnumerable<Feature> GetFeatures(Feature feature)
        {
            var geometry = feature.Geometry;

            var start = Evaluate(_startEvaluator, _start);
            double end = Evaluate(_endEvaluator, _end);
            var increment = Evaluate(_incEvaluator, _increment);
            var offset = Evaluate(_offsetEvaluator, _offset);

            switch (geometry.OgcGeometryType)
            {
                case OgcGeometryType.LineString:
                case OgcGeometryType.LineStringZ:
                case OgcGeometryType.MultiLineString:
                case OgcGeometryType.MultiLineStringZ:
                    if (start < 0) start += geometry.Length;
                    if(end < 0) end += geometry.Length;
                    return GetFeatures(feature, start, end, increment, offset);
                case OgcGeometryType.LineStringM:
                case OgcGeometryType.LineStringZM:
                case OgcGeometryType.MultiLineStringM:
                case OgcGeometryType.MultiLineStringZM:
                    return GetFeaturesM(feature, start, end, increment, offset);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private IEnumerable<Feature> GetFeaturesM(Feature feature, double start, double end, double increment, double offset)
        {
            var newFeature = _outputFeatureType.NewFeature(feature.Fid, feature.Values);
            var geometry = feature.Geometry;
            var map = new MeasureLocationMap(geometry);

            for (var m = start; m <= end; m += increment)
            {
                var loc = map.GetLocation(m);
                var seg = loc.GetSegment(geometry);
                var coord = _offsetEvaluator != null ? seg.PointAlongOffset(loc.SegmentFraction, offset) : seg.PointAlong(loc.SegmentFraction);

                if (_xIndex > 0) newFeature[_xIndex] = coord.X;
                if (_yIndex > 0) newFeature[_yIndex] = coord.Y;
                if (_zIndex > 0) newFeature[_zIndex] = coord.Z;
                if (_mIndex > 0) newFeature[_mIndex] = coord.M;
                if (_angleIndex >= 0) newFeature[_angleIndex] = seg.Angle / Math.PI * 180;

                newFeature.Geometry = new NetTopologySuite.Geometries.Point(coord) { SRID = geometry.SRID };
                yield return newFeature;
            }
        }

        IEnumerable<Feature> GetFeatures(Feature feature, double start, double end, double Increment, double offset)
        {
            var newFeature = _outputFeatureType.NewFeature(feature.Fid, feature.Values);
            var geometry = feature.Geometry;

            for (var m = start; m <= end; m += Increment)
            {
                var loc = LengthLocationMap.GetLocation(geometry, m);
                var seg = loc.GetSegment(geometry);
                var coord = _offsetEvaluator != null ? seg.PointAlongOffset(loc.SegmentFraction, offset) : seg.PointAlong(loc.SegmentFraction);
                coord = coord.WithMeasure(m);

                if (_xIndex > 0) newFeature[_xIndex] = coord.X;
                if (_yIndex > 0) newFeature[_yIndex] = coord.Y;
                if (_zIndex > 0) newFeature[_zIndex] = coord.Z;
                if (_mIndex > 0) newFeature[_mIndex] = coord.M;
                if (_angleIndex >= 0) newFeature[_angleIndex] = seg.Angle / Math.PI * 180;

                newFeature.Geometry = new NetTopologySuite.Geometries.Point(coord) { SRID = geometry.SRID };
                yield return newFeature;

            }
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            if (Visible && !_compiled)
                Compile();

            foreach (var child in _pointExtractor.Nodes)
            {
                var childRenderer = child.Renderer as IFeatureRenderer;
                if (childRenderer != null)
                    childRenderer.BeginScene(Visible);
            }

            RenderCount = 0;
        }
    }
}
