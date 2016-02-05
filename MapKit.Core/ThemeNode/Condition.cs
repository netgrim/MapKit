using System.Xml;

namespace MapKit.Core
{
    public abstract class ExpressionNode : ContainerNode
	{
        internal const string ExpressionField = "expression";
        internal const string ExpressionProperty = "Expression";
       
        private string _expression;

        public string Expression
        {

            get { return _expression; }
            set
            {
                _expression = value;
                OnFieldChanged(ExpressionProperty);
            }
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Expression))
                writer.WriteAttributeString(ExpressionField, Expression);
            base.WriteXmlAttributes(writer);
        }
    }
}
