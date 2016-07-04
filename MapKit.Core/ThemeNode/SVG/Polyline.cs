using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("Polyline: Label = {Label}, Path={NodePath}")]
    public class PolylineNode : SvgGraphicElement
    {
        public const string ElementName = "polyline";

        public PolylineNode()
        {
        }

        public static string Text
        {
            get { return "Polyline"; }
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
