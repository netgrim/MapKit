using System;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class LayerGroupRenderer : GroupBaseRenderer
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

        //public override void Compile(bool recursive = false)
        //{
        //    //GroupItem.CascadeStyles();


        //    _declarations = new List<IBaseRenderer>();
        //    Renderers = new List<IBaseRenderer>();

        //    foreach (var renderer in Renderer.GetRenderers(GroupItem, this))
        //        if (renderer.Node is Macro)
        //            _declarations.Add(renderer);
        //        else
        //            Renderers.Add(renderer);

        //    if (recursive)
        //        foreach (var renderer in Renderers)

        //            renderer.Compile(recursive);

        //    base.Compile(recursive);
        //}


    }
}
