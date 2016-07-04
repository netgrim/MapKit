using Ciloci.Flee;
using System;
using System.ComponentModel;
using System.Drawing;

namespace MapKit.Core.Rendering
{
    public abstract class NodeRenderer : IBaseRenderer
    {
        private const string ExpressionPrefix = "=";

        public NodeRenderer(Renderer renderer, ThemeNode node, IBaseRenderer parent)
        {
            if (renderer == null) throw new ArgumentNullException("renderer");
            if (node == null) throw new ArgumentNullException("node");
            if (parent == null) throw new ArgumentNullException("parent");
            Renderer = renderer;
            Node = node;
            Parent = parent;
        }

        public ThemeNode Node { get; private set; }

        public IBaseRenderer Parent { get; set; }
        
        public bool Visible { get; protected set; }

        //statistics
        [Category(Constants.CatStatistics)]

        public int RenderCount { get; set; }

        [Browsable(false)]
        public Renderer Renderer { get; set; }

        protected bool Compiled { get; set; }

        public virtual void BeginScene(bool visible)
        {
            Visible = visible && Node.Visible;
            if (Visible && !Compiled)
                Compile(false);

            RenderCount = 0;
        }

        public virtual void Compile(bool recursive)
        {
            Compiled = true;
        }

        public IBaseRenderer FindByName(string name)
        {
            var parent = Parent;
            while (parent != null)
            {
                var container = parent.Node as ContainerNode;
                if (container != null && string.Compare(container.Name, name, true) == 0)
                    return parent;

                var child = parent.FindChildByName(name);
                if (child != null)
                    return child;

                parent = parent.Parent;
            }

            return null;
        }

        public virtual void Render(Feature feature)
        {
            RenderCount++;
        }

        public virtual IBaseRenderer FindChildByName(string name)
        {
            return null;
        }

        protected static ExpressionContext CreateContentAlignmentContext()
        {
            var context = CreateContext();
            context.Imports.AddType(typeof(ContentAlignment));
            return context;
        }

        public static ExpressionContext CreateContext()
        {
            var context = new ExpressionContext();
            context.Options.RealLiteralDataType = RealLiteralDataType.Single;
            context.Options.ParseCulture = System.Globalization.CultureInfo.InvariantCulture;

            return context;
        }

        public static ExpressionContext CreateContext(object owner)
        {
            var context = new ExpressionContext(owner);
            //context.Imports.AddType(typeof(Math));
            //context.Imports.AddType(typeof(Convert));
            context.Options.RealLiteralDataType = RealLiteralDataType.Single;
            context.Options.ParseCulture = System.Globalization.CultureInfo.InvariantCulture;

            return context;
        }

        public IGenericExpression<T> CompileExpression<T>(ExpressionContext context, string name, string expression, bool nullable = true)
        {
            return CompileExpression<T>(Node, context, name, expression, nullable);
        }

        protected IGenericExpression<T> CompileExpression<T>(ThemeNode node, ExpressionContext context, string name, string expression, bool nullable = true)
        {
            return CompileExpression<T>(Renderer, node, context, name, expression, nullable);
        }

        /// <summary>
        /// compile text directly if does not have the prefix
        /// </summary>
        public static IGenericExpression<T> CompileExpression<T>(Renderer renderer, ThemeNode node, ExpressionContext context, string name, string expression, bool nullable = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(expression))
                {
                    return expression.StartsWith(ExpressionPrefix)
                        ? context.CompileGeneric<T>(expression.Substring(1))
                        : context.CompileGeneric<T>(expression);
                }
                else if (nullable)
                    return null;
                else
                    throw new RenderingException(string.Format("Field '{0}' cannot be empty", name));
            }
            catch (ExpressionCompileException ex)
            {
                string message = string.Format("Failed to compile field '{0}' in '{1}':\r\n{2}", name, node.NodePath, ex.Message);
                renderer.HandleError(message);
            }
            return null;
        }

        protected IGenericExpression<string> CompileStringExpression(ExpressionContext context, string name, string expression)
        {
            string defaultValue = null;
            return CompileExpression(context, name, expression, ref defaultValue, x => x, true);
        }

        protected IGenericExpression<string> CompileStringExpression(ExpressionContext context, string name, string expression, ref string defaultValue, bool nullable = true)
        {
            return CompileExpression(context, name, expression, ref defaultValue, x => x, nullable);
        }

        protected IGenericExpression<Color> CompileColorExpression(ExpressionContext context, string name, string expression, ref Color defaultValue, bool nullable = true)
        {
            Color parsedColor;
            if (string.Compare(expression, "random", true) == 0)
                return CompileExpression<Color>(Node, context, name, "=color.Random", nullable);
            else if (Util.TryParseColor(expression, out parsedColor))
            {
                defaultValue = parsedColor;
                return null;
            }

            return CompileExpression<Color>(Node, context, name, expression, nullable);
        }

        protected IGenericExpression<TEnum> CompileEnumExpression<TEnum>(ExpressionContext context, string name, string expression, ref TEnum defaultValue, bool nullable = true) where TEnum : struct
        {
            TEnum parsedValue;
            if (Enum.TryParse(expression, true, out parsedValue))
            {
                defaultValue = parsedValue;
                return null;
            }

            return CompileExpression<TEnum>(Node, context, name, expression, nullable);
        }

        protected IGenericExpression<double> CompileDoubleExpression(string name, string expression, ref double defaultValue, bool nullable = true)
        {
            return CompileDoubleExpression(Renderer.Context, name, expression, ref defaultValue, nullable);
        }

        protected IGenericExpression<double> CompileDoubleExpression(ExpressionContext context, string name, string expression, ref double defaultValue, bool nullable = true)
        {
            return CompileExpression(context, name, expression, ref defaultValue, e => double.Parse(e, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo), nullable);
        }

        protected IGenericExpression<bool> CompileBoolExpression(ExpressionContext context, string name, string expression, ref bool defaultValue, bool nullable = true)
        {
            return CompileExpression(context, name, expression, ref defaultValue, e => bool.Parse(expression), nullable);
        }

        protected delegate T Parse<T>(string expression);

        protected IGenericExpression<T> CompileExpression<T>(ExpressionContext context, string name, string expression, ref T defaultValue, Parse<T> parser, bool nullable = true)
        {
            return CompileExpression(Renderer, Node, context, name, expression, ref defaultValue, parser, nullable);
        }

        /// <summary>
        /// Use the parser if it does not have the prefix
        /// </summary>
        protected static IGenericExpression<T> CompileExpression<T>(Renderer renderer, ThemeNode node, ExpressionContext context, string name, string expression, ref T defaultValue, Parse<T> parser, bool nullable = true)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                try
                {
                    if (expression.StartsWith(ExpressionPrefix))
                        return context.CompileGeneric<T>(expression.Substring(1));
                }
                catch (ExpressionCompileException ex)
                {
                    string message = string.Format("Failed to compile field '{0}' in '{1}':\r\n{2}", name, node.NodePath, ex.Message);
                    renderer.HandleError(message);
                }
                defaultValue = parser(expression);
            }
            else if (!nullable)
                throw new RenderingException(string.Format("Field '{0}' cannot be empty", name));

            return null;
        }

        protected IGenericExpression<float> CompileFloatExpression(ExpressionContext context, string name, string expression, ref float defaultValue, bool nullable = true)
        {
            return CompileExpression(context, name, expression, ref defaultValue, e => float.Parse(expression, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo), nullable);
        }

        public IDynamicExpression CompileExpression(ExpressionContext context, string name, string expression, ref object defaultValue, bool nullable = true)
        {
            return CompileExpression(Node, context, name, expression, ref defaultValue, nullable);
        }

        public static IDynamicExpression CompileExpression(ThemeNode node, ExpressionContext context, string name, string expression, ref object defaultValue, bool nullable = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(expression))
                {
                    if (expression.StartsWith(ExpressionPrefix))
                        return context.CompileDynamic(expression.Substring(ExpressionPrefix.Length));

                    defaultValue = expression;
                    return null;
                }
                else if (nullable)
                    return null;
                else
                    throw new RenderingException(string.Format("Field '{0}' cannot be empty", name));
            }
            catch (ExpressionCompileException ex)
            {
                throw new RenderingException(string.Format("Failed to compile field '{0}' in '{1}':\r\n{2}", name, node.NodePath, ex.Message), ex);
            }
        }

        public static T Evaluate<T>(IGenericExpression<T> evaluator, T defaultValue)
        {
            return evaluator != null ? evaluator.Evaluate() : defaultValue;
        }

        protected static Color GetRenderColor(IGenericExpression<float> opacityEvaluator, float defaultOpacity, IGenericExpression<Color> colorEvaluator, Color defaultColor)
        {
            return Util.GetColor(Evaluate(opacityEvaluator, defaultOpacity), Evaluate(colorEvaluator, defaultColor));
        }

        public static double GetHorizontalAligmentOffset(ContentAlignment alignment, double width)
        {
            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                    return -width / 2;
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                    return -width;
            }
            return 0;
        }

        public static double GetVerticalAlignentOffset(ContentAlignment alignment, double height)
        {
            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    return height;
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                    return height / 2;
            }
            return 0;
        }
    }
}
