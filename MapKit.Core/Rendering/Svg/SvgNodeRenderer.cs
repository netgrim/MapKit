﻿using Ciloci.Flee;
using System.Text;

namespace MapKit.Core.Rendering
{
    internal class SvgNodeRenderer : FeatureRenderer
    {
        protected IGenericExpression<string> _idEvaluator;
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

            _idEvaluator = CompileStringExpression(context, Animate.IdField, _svgNode.Id);
        }
    }
}