using System.Collections.Generic;
using System.ComponentModel;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class VerticesEnumeratorRenderer : ContainerNodeRenderer
    {
        private VerticesEnumerator _verticesEnumerator;
        private bool _compiled;

        public VerticesEnumeratorRenderer(Renderer renderer, VerticesEnumerator verticesEnumerator, IBaseRenderer parent)
            :base(renderer, verticesEnumerator, parent)
        {
            _verticesEnumerator = verticesEnumerator;
            _verticesEnumerator.PropertyChanged += new PropertyChangedEventHandler(_verticesEnumerator_PropertyChanged);
        }

        void _verticesEnumerator_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        private FeatureType WrapFeatureType(FeatureType featureType)
        {
            var newFeatureType = (FeatureType)featureType.Clone();
            newFeatureType.AddAttributes("vertex_id", typeof(double));
            newFeatureType.AddAttributes("vertex_x", typeof(double));
            newFeatureType.AddAttributes("vertex_y", typeof(double));
            newFeatureType.AddAttributes("vertex_z", typeof(double));
            newFeatureType.AddAttributes("vertex_m", typeof(double));
            
            //newFeatureType.GeometryType = OgcGeometryType.Point;

            return newFeatureType;
        }
        
        public IEnumerable<Feature> GetFeatures(Feature feature)
        {
            var newFeature = OutputFeatureType.NewFeature(feature.Fid, feature.Values);
            var id = feature.Values.Length;
            var x = id + 1;
            var y = x + 1;
            var geometry = feature.Geometry;

            foreach (var lineString in Renderer.GetLineString(geometry))
            {
                int start, end, inc = 1;
                var vertices = lineString.CoordinateSequence;
                switch (_verticesEnumerator.Mode)
                {
                    case VerticesEnumeratorMode.All:
                        start = 0;
                        end = vertices.Count - 1;
                        break;
                    case VerticesEnumeratorMode.Range:
                        start = _verticesEnumerator.Start;
                        end = _verticesEnumerator.End;
                        inc = _verticesEnumerator.Increment;
                        break;
                    case VerticesEnumeratorMode.First:
                        start = end = 0;
                        break;
                    case VerticesEnumeratorMode.Last:
                        start = end = vertices.Count - 1;
                        break;
                    case VerticesEnumeratorMode.Outer:
                        start = 0;
                        inc = end = vertices.Count - 1;
                        break;
                    case VerticesEnumeratorMode.Inner:
                        start = 1;
                        end = vertices.Count - 2;
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }

                for (int i = start; i <= end; i += inc)
                {
                    var vertex = vertices.GetCoordinate(i);
                    newFeature[id] = i;
                    newFeature[x] = vertex.X;
                    newFeature[y] = vertex.Y;
                    newFeature.Geometry = new NetTopologySuite.Geometries.Point(vertex) { SRID = geometry.SRID, M = 0 };

                    yield return newFeature;
                }
            }
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            if (Visible && !_compiled)
                Compile();

            foreach (var child in _verticesEnumerator.Nodes)
            {
                var childRenderer = child.Renderer as IFeatureRenderer;
                if (childRenderer != null)
                    childRenderer.BeginScene(Visible);
            }

            RenderCount = 0;
        }

        public override void Compile(bool recursive = false)
        {
            OutputFeatureType =  WrapFeatureType(InputFeatureType);
            base.Compile(recursive);

            _compiled = true;
        }

        public override void Render(Feature feature)
        {
            foreach (var newFeature in GetFeatures(feature))
                Renderer.Render(_verticesEnumerator, newFeature);
            
            RenderCount++;
        }
        
    }
}
