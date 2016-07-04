using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("Polygon: Label = {Label}, Path={NodePath}")]
    public class PolygonNode : SvgGraphicElement
    {
        public const string ElementName = "polygon";

        public PolygonNode()
        {
        }

        public static string Text
        {
            get { return "Polygon"; }
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override NodeType GetNodeType()
        {
            return null;
        }
    }
}
