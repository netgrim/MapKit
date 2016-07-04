using System;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class LayerRenderer : GroupBaseRenderer
    {
        private Layer _layer;
        private FeatureType _featureType;

        public LayerRenderer(Renderer renderer, Layer layer, IBaseRenderer parent)
            : base(renderer, layer, parent)
        {
            _layer = layer;
        }

        public override void Render(Feature scrapFeature)
        {
            if(!(Visible && _layer.IsVisibleAt(Renderer.Zoom)))
                return;

            var tStart = Environment.TickCount;

            Renderer.Graphics.SmoothingMode = SmoothingMode;

            try
            {
                var cnt = 0;

                Renderer.Svg.WriteStartElement("g");
                Renderer.Svg.WriteAttributeString("id", _layer.Name);
                
                foreach (var feature in _layer.GetFeatures(_featureType, _layer, Renderer.Window))
                {
                    Renderer.Context.Variables[Renderer.FEATURE_VAR] = feature;
                    if (cnt >= _layer.MaxRender)
                        return;

                    Renderer.FeatureVarResolver.Feature = feature;

                    Renderer.Render(Renderers, feature);

                    cnt++;
                }

                Renderer.Svg.WriteEndElement();
                
                FeatureCount = cnt;
                Renderer.PerformLayerRendered(new LayerEventArgs(_layer));
            }
            catch (Exception ex)
            {
                var args = new LayerFailedEventArgs(_layer, ex);
                Renderer.PerformLayerFailed(args);
            }
            finally
            {
                Renderer.FeatureVarResolver.Feature = null; //restore old feature
            }
            
            RenderCount++;
            RenderTime = new TimeSpan((Environment.TickCount - tStart) * 10000);
        }

        public override void Compile(bool recursive)
        {
            _layer.Open();
            _featureType = _layer.GetFeatureType();
            Renderer.FeatureVarResolver.FeatureType = _featureType;

            base.Compile(false);

            foreach (var renderer in Renderers)
            {
                var featureRenderer = renderer as IFeatureRenderer;
                if (featureRenderer != null)
                    featureRenderer.InputFeatureType = _featureType;

                if (recursive)
                    renderer.Compile(true);
            }
        }
    }
}
