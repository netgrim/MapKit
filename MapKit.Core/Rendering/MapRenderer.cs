using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class MapRenderer : LayerGroupRenderer
    {
        private Map _map;

        public MapRenderer(Renderer renderer, Map map)
            : base(renderer, map, null)
        {
            _map = map;
        }

        protected override void RenderChildren()
        {
            Renderer.Graphics.Clear(_map.BackgroundColor);
            base.RenderChildren();
        }


    }
}
