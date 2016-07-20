using Ciloci.Flee;
using System;
using System.Text;

namespace MapKit.Core.Rendering
{
    internal class PolylineRenderer : SvgGraphicElementRenderer
    {
        private PolylineNode _polylineNode;

        public PolylineRenderer(Renderer renderer, PolylineNode node, IBaseRenderer parent)
            : base(renderer, node, parent)
        {
            _polylineNode = node;
        }

        public override void Render(Feature feature)
        {
            if (feature == null) return;

            foreach (var lineString in Renderer.GetLineString(feature.Geometry))
                if (lineString.NumPoints > 1)
                {
                    var points = Renderer.TransformToPointsF(lineString.CoordinateSequence);

                    Renderer.Graphics.Transform.TransformPoints(points);
                    Renderer.Svg.WriteStartElement(PolylineNode.ElementName);

                    var pointSvg = new StringBuilder();
                    for (var i = 0; i < lineString.NumPoints; i++)
                    {
                        if (i > 0)
                            pointSvg.Append(" ");
                        pointSvg.Append(points[i].X.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
                        pointSvg.Append(",");
                        pointSvg.Append(points[i].Y.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
                    }

                    foreach (var attribute in new[]{ new { field = SvgNode.IdField, value = Convert.ToString(Evaluate(_idEvaluator, _polylineNode.Id)) },
                        new { field = SvgGraphicElement.ClassField, value = Evaluate(_classEvaluator, _polylineNode.Class) },
                        new { field = "points", value = pointSvg.ToString() },
                        new { field = SvgGraphicElement.StyleField, value = Evaluate(_styleEvaluator, _polylineNode.Style) },
                        new { field = SvgGraphicElement.TransformField, value = Evaluate(_transformEvaluator, _polylineNode.Transform) } })
                    {
                        if (attribute.value != null)
                            Renderer.Svg.WriteAttributeString(attribute.field, attribute.value);
                    }

                    base.Render(feature);

                    //Renderer.Svg.WriteAttributeString("style", "fill:none;stroke:black");
                    Renderer.Svg.WriteEndElement();
                }
        }
    }
}