using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ciloci.Flee;
using GeoAPI.Geometries;
using System.ComponentModel;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class ContainerNodeRenderer : FeatureRenderer
    {
        private ContainerNode _containerNode;
        private bool _compiled;
        private IList<IBaseRenderer> _renderers;
        private IList<IBaseRenderer> _declarations;

        public ContainerNodeRenderer(Renderer renderer, ContainerNode node, IBaseRenderer parent)
            : base(renderer, node, parent)
        {
            _containerNode = node;
            _containerNode.PropertyChanged += _containerNode_PropertyChanged;
        }

        public override FeatureType InputFeatureType
        {
            get
            {
                return base.InputFeatureType;
            }
            set
            {
                base.InputFeatureType = value;
                _compiled = false;
                OutputFeatureType = value;
            }
        }

        public FeatureType OutputFeatureType { get; protected set; }

        public IList<IBaseRenderer> Renderers
        {
            get { return _renderers; }
        }
        
        void _containerNode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ThemeNode.VisiblePropertyName) return;
            _compiled = false;
        }
    
        public override void Render(Feature feature)
        {
            if (!Visible)
                return;

            Renderer.Render(_renderers, feature);

            RenderCount++;
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible && _containerNode.IsVisibleAt(Renderer.Zoom));
            if (!Visible)
                return;
            
            if (!_compiled)
                Compile();

            foreach (var renderer in _declarations)
                renderer.BeginScene(Visible);
            foreach (var renderer in _renderers)
                renderer.BeginScene(Visible);
        }

        public override void Compile(bool recursive = false)
        {
            //_containerNode.CascadeStyles();

            _renderers = new List<IBaseRenderer>();
            _declarations = new List<IBaseRenderer>();

            foreach (var renderer in Renderer.GetRenderers(_containerNode, this))
                if (renderer.Node is Macro)
                    _declarations.Add(renderer);
                else
                    _renderers.Add(renderer);

            _compiled = true;
        }

        public override IBaseRenderer FindChildByName(string name)
        {
            foreach (var child in _declarations)
            {
                var container = child.Node as ContainerNode;
                if (container != null && string.Compare(container.Name, name, true) == 0)
                    return child;
            }
            return null;
        }
    }
}
