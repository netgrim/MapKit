using Ciloci.Flee;
using System.Text;

namespace MapKit.Core.Rendering
{
    internal class SvgGraphicElementRenderer : SvgNodeRenderer
    {
        private SvgGraphicElement _svgGraphicElement;
        protected IGenericExpression<string> _classEvaluator;
        protected IGenericExpression<string> _styleEvaluator;
        protected IGenericExpression<string> _transformEvaluator;

        public SvgGraphicElementRenderer(Renderer renderer, SvgGraphicElement node, IBaseRenderer parent)
            : base(renderer, node, parent)
        {
            _svgGraphicElement = node;
        }

        public override void Compile(FeatureType featuretype, bool recursive = false)
        {
            base.Compile(featuretype, recursive);

            var context = Renderer.Context;

            _classEvaluator = CompileStringExpression(context, SvgGraphicElement.ClassField, _svgGraphicElement.Class);
            _styleEvaluator = CompileStringExpression(context, SvgGraphicElement.StyleField, _svgGraphicElement.Style);
            _transformEvaluator = CompileStringExpression(context, SvgGraphicElement.TransformField, _svgGraphicElement.Transform);
        }
    }
}