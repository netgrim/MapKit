using Ciloci.Flee;
using System.Collections.Generic;
using System.Text;

namespace MapKit.Core.Rendering
{
    internal class SvgNodeRenderer : FeatureRenderer
    {
        protected IDynamicExpression _idEvaluator;
        protected List<IDynamicExpression> _attributeEvaluators;
        private SvgNode _svgNode;

        public SvgNodeRenderer(Renderer renderer, SvgGraphicElement node, IBaseRenderer parent)
            : base(renderer, node, parent)
        {
            _svgNode = node;
        }

        public override void Compile(FeatureType featuretype, bool recursive = false)
        {
            base.Compile(featuretype, recursive);

            var context = Renderer.Context;

            _idEvaluator = CompileExpression(context, Animate.IdField, _svgNode.Id);

            _attributeEvaluators = new List<IDynamicExpression>(_svgNode.Attributes.Count);
            foreach (var pair in _svgNode.Attributes)
                _attributeEvaluators.Add(CompileExpression(context, pair.Item1, pair.Item2));
        }
    }
}