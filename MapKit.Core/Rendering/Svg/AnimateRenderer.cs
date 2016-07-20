using System;
using Ciloci.Flee;

namespace MapKit.Core.Rendering
{
    internal class AnimateRenderer : FeatureRenderer
    {
        private Animate _animate;
        private IGenericExpression<string> _attributeNameEvaluator;
        private IGenericExpression<string> _beginEvaluator;
        private IGenericExpression<string> _durationEvaluator;
        private IGenericExpression<string> _fromEvaluator;
        private IGenericExpression<string> _idEvaluator;
        private IGenericExpression<string> _repeatCountEvaluator;
        private IGenericExpression<string> _toEvaluator;
        private IGenericExpression<string> _valuesEvaluator;
        private IGenericExpression<string> _fillEvaluator;

        public AnimateRenderer(Renderer renderer, Animate animate, IBaseRenderer parent)
            : base (renderer, animate, parent)
        {
            _animate = animate;
        }

        private int _counter;

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            _counter = 0;
        }

        public override void Compile(FeatureType featuretype, bool recursive = false)
        {
            base.Compile(featuretype, recursive);

            var context = Renderer.Context;
            if(!context.Variables.ContainsKey("counter"))
                context.Variables.Add("counter", _counter);

            _idEvaluator = CompileStringExpression(context, Animate.IdField, _animate.Id);
            _attributeNameEvaluator = CompileStringExpression(context, Animate.AttributeNameField, _animate.AttributeName);
            _fromEvaluator = CompileStringExpression(context, Animate.FromField, _animate.From);
            _toEvaluator = CompileStringExpression(context, Animate.ToField, _animate.To);
            _valuesEvaluator = CompileStringExpression(context, Animate.ValuesField, _animate.Values);
            _durationEvaluator = CompileStringExpression(context, Animate.DurField, _animate.Duration);
            _beginEvaluator = CompileStringExpression(context, Animate.BeginField, _animate.Begin);
            _repeatCountEvaluator = CompileStringExpression(context, Animate.RepeatCountField, _animate.RepeatCount);
            _fillEvaluator = CompileStringExpression(context, Animate.FillField, _animate.Fill);
        }

        public override void Render(Feature feature)
        {
            Renderer.Svg.WriteStartElement("animate");
            _counter++;
            Renderer.Context.Variables["counter"] = _counter;

            base.Render(feature);
            foreach (var attribute in new[]{ new { field = Animate.IdField, value = Evaluate(_idEvaluator, _animate.Id) },
                new { field = Animate.AttributeNameField, value = Evaluate(_attributeNameEvaluator, _animate.AttributeName) },
                new { field = Animate.FromField, value = Evaluate(_fromEvaluator, _animate.From) },
                new { field = Animate.ToField, value = Evaluate(_toEvaluator, _animate.To) },
                new { field = Animate.ValuesField, value = Evaluate(_valuesEvaluator, _animate.Values) },
                new { field = Animate.DurField, value = Evaluate(_durationEvaluator, _animate.Duration) },
                new { field = Animate.BeginField, value = Evaluate(_beginEvaluator, _animate.Begin) },
                new { field = Animate.FillField, value = Evaluate(_fillEvaluator, _animate.Fill) },
                new { field = Animate.RepeatCountField, value = Evaluate(_repeatCountEvaluator, _animate.RepeatCount) } })
            {
                if (attribute.value != null)
                    Renderer.Svg.WriteAttributeString(attribute.field, attribute.value);
            }

            //Renderer.Svg.WriteAttributeString("style", "fill:none;stroke:black");
            Renderer.Svg.WriteEndElement();
        }
    }
}