using System;
using Ciloci.Flee;
using System.Drawing;
using GeoAPI.Geometries;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.ComponentModel;
using MapKit.Core.Rendering;
using System.Collections.Generic;

namespace MapKit.Core
{
    class FeatureRenderer : ContainerNodeRenderer, IFeatureRenderer
    {
        private FeatureType _inputFeatureType;

        public FeatureRenderer(Renderer renderer, ContainerNode node, IBaseRenderer parentRenderer)
            : base ( renderer, node, parentRenderer)
        {
            Parent = parentRenderer;
        }

        public virtual FeatureType InputFeatureType
        {
            get
            {
                return _inputFeatureType;
            }
            set
            {
                _inputFeatureType = value;
                Compiled = false;
                //OutputFeatureType = value;
            }
        }

        [Category(Constants.CatStatistics)]
        public int FeatureCount { get; private set; }

        protected void AddToPath(ILineString lineString, GraphicsPath path)
        {
            if (lineString.NumPoints > 1)
            {
                path.AddLines(Renderer.TransformToPointsF(lineString.CoordinateSequence));

                if (lineString.IsClosed)
                    path.CloseFigure();
            }
            else
                Trace.WriteLine("LineString is invalid");
        }

        public override void Compile(bool recursive = false)
        {
            Compile(InputFeatureType, recursive);
        }

        public virtual void Compile(FeatureType featuretype, bool recursive = false)
        {
            //override to set InputFeatureType

            Renderers = new List<IBaseRenderer>();
            Declarations = new List<IBaseRenderer>();

            foreach (IFeatureRenderer renderer in Renderer.GetRenderers(ContainerNode, this))
                if (renderer.Node is Macro)
                    Declarations.Add(renderer);
                else
                {
                    renderer.InputFeatureType = featuretype;
                    Renderers.Add(renderer);

                    if (recursive)
                        renderer.Compile(true);
                }

            Compiled = true;
        }

        public override void Render(Feature feature)
        {
            if (!Visible)
                return;

            foreach (var childRenderer in Renderers)
                if (!Renderer.CancelPending && childRenderer.Visible)
                {
                    childRenderer.Render(feature);

                    var groupRenderer = childRenderer as IGroupRenderer;
                    if (groupRenderer != null)
                        FeatureCount += groupRenderer.FeatureCount;
                }

            RenderCount++;
        }
    }
}
