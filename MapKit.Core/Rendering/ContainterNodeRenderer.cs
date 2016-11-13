using System.Collections.Generic;
using System.ComponentModel;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class ContainerNodeRenderer : FeatureRenderer
    {
        public ContainerNodeRenderer(Renderer renderer, ContainerNode node, IBaseRenderer parent)
            : base (renderer, node, parent)
        {
            ContainerNode = node;
            Node.PropertyChanged += _containerNode_PropertyChanged;
        }
        
        //public FeatureType OutputFeatureType { get; protected set; }

        public IList<IBaseRenderer> Renderers { get; set; }

        public IList<IBaseRenderer> Declarations { get; set; }

        public ContainerNode ContainerNode { get; private set; }

        void _containerNode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ThemeNode.VisiblePropertyName) return;
            Compiled = false;
        }

        public override void Render(Feature feature)
        {
            Renderer.Render(Renderers, feature);
            base.Render(feature);
        }

        public override void BeginScene(bool visible)
        {
            visible &= ContainerNode.IsVisibleAt(Renderer.Zoom);
            base.BeginScene(visible);

            if (Compiled)
            {
                foreach (var renderer in Declarations)
                    renderer.BeginScene(visible);
                foreach (var renderer in Renderers)
                    renderer.BeginScene(visible);
            }

            RenderCount = 0;
        }

        public override void Compile(bool recursive = false)
        {
            Renderers = new List<IBaseRenderer>();
            Declarations = new List<IBaseRenderer>();

            foreach (var renderer in Renderer.GetRenderers(ContainerNode, this))
                if (renderer.Node is Macro)
                    Declarations.Add(renderer);
                else
                {
                    Renderers.Add(renderer);

                    if (recursive)
                        renderer.Compile(true);
                }

            base.Compile(recursive);
        }

        public override IBaseRenderer FindChildByName(string name)
        {
            foreach (var child in Declarations)
            {
                var container = child.Node as ContainerNode;
                if (container != null && string.Compare(container.Name, name, true) == 0)
                    return child;
            }
            return null;
        }

    }
}
