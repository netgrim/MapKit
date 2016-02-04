using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ciloci.Flee;
using GeoAPI.Geometries;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class LayerGroupRenderer : GroupItemRenderer
    {
        public LayerGroupRenderer(Renderer renderer, Group group, IBaseRenderer parent)
            : base(renderer, group, parent)
        {
        }
        
        public override void Render(Feature feature)
        {
            int tStart = Environment.TickCount;

            Renderer.Graphics.SmoothingMode = SmoothingMode;

            RenderChildren();

            RenderCount++;
            RenderTime = new TimeSpan((Environment.TickCount - tStart) * 10000);
        }

        protected virtual void RenderChildren()
        {
            var nodes = GroupItem.Nodes;
            foreach(var childRenderer in Renderers)
                if (!Renderer.CancelPending && childRenderer.Visible)
                {
                    childRenderer.Render(null);

                    var groupRenderer = childRenderer as IGroupRenderer;
                    if (groupRenderer != null)
                        FeatureCount += groupRenderer.FeatureCount;
                }
        }
    }
}
