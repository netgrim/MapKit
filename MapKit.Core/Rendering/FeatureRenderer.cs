using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ciloci.Flee;
using System.Drawing;
using GeoAPI.Geometries;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using MapKit.Core;
using WinPoint = System.Windows.Point;
using System.ComponentModel;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    abstract class FeatureRenderer : IFeatureRenderer
    {
        private const string ExpressionPrefix = "=";

        public FeatureRenderer(Renderer renderer, ThemeNode node, IBaseRenderer parentRenderer)
        {
            Renderer = renderer;
            Node = node;
            Parent = parentRenderer;
        }

        private class RandomColor
        {
            private Random _rnd;
            public RandomColor()
            {
                _rnd = new Random(GetHashCode());
            }

            public Color Random
            {
                get { return Color.FromArgb(_rnd.Next(255), _rnd.Next(255), _rnd.Next(255)); }
            }
        }

        [Browsable(false)]
        public Renderer Renderer { get; set; }

        public virtual FeatureType InputFeatureType { get; set; }

        public bool Visible { get; protected set; }

        //statistics
        [Category(Constants.CatStatistics)]
        public int RenderCount { get; set; }

        public ThemeNode Node { get; private set; }
        
        protected static ExpressionContext CreateColorContext()
        {
            var context = CreateContext(new RandomColor());
            context.Imports.AddType(typeof(Color));
            context.Imports.AddType(typeof(SystemColors));
            context.Imports.AddType(typeof(ColorTranslator));
            return context;
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

        public static IGenericExpression<T> CompileExpression<T>(Renderer renderer, ThemeNode node, ExpressionContext context, string name, string expression, bool nullable = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(expression))
                {
                    return expression.StartsWith(ExpressionPrefix)
                        ? context.CompileGeneric<T>(expression.Substring(1))
                        : context.CompileGeneric<T>("\"" + expression + "\"");
                }
                else if (nullable)
                    return null;
                else
                    throw new RenderingException(string.Format("Field '{0}' cannot be empty", name));
            }
            catch (ExpressionCompileException ex)
            {
                string message = String.Format("Failed to compile field '{0}' in '{1}':\r\n{2}", name, node.NodePath, ex.Message);
                renderer.HandleError(message);
            }
            return null;
        }

        internal IGenericExpression<T> CompileExpression<T>(ThemeNode node, ExpressionContext context, string name, string expression, bool nullable = true)
        {
            return CompileExpression<T>(Renderer, node, context, name, expression, nullable);
        }

        protected IGenericExpression<Color> CompileColorExpression(ExpressionContext context, string name, string expression, ref Color defaultValue, bool nullable = true)
        {
            if (!string.IsNullOrEmpty(expression) && !Util.TryParseColor(expression, out defaultValue))
                return CompileExpression<Color>(Node, context, name, expression, nullable);
            return null;
        }

        protected IGenericExpression<DashStyle> CompileDashStyleExpression(ExpressionContext context, string name, string expression, ref DashStyle defaultValue, bool nullable = true)
        {
            if (!Enum.TryParse(expression, true, out defaultValue))
                return CompileExpression<DashStyle>(Node, context, name, expression, nullable);
            return null;
        }


        protected IGenericExpression<double> CompileDoubleExpression(ExpressionContext context, string name, string expression, ref Double defaultValue, bool nullable = true)
        {
            if (string.IsNullOrEmpty(expression) || double.TryParse(expression, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out defaultValue))
                return null;
            return CompileExpression<double>(Node, context, name, expression, nullable);
        }

        protected IGenericExpression<float> CompileFloatExpression(ExpressionContext context, string name, string expression, ref float defaultValue, bool nullable = true)
        {
            if (string.IsNullOrEmpty(expression) || float.TryParse(expression, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out defaultValue))
                return null;
            return CompileExpression<float>(Node, context, name, expression, nullable);
        }

        public IDynamicExpression CompileExpression(ExpressionContext context, string name, string expression, bool nullable = true)
        {
            return CompileExpression(Node, context, name, expression, nullable);
        }
        
        public static IDynamicExpression CompileExpression(ThemeNode node, ExpressionContext context, string name, string expression, bool nullable = true)
        {
            try
            {
                if (expression != null)
                {
                    return expression.StartsWith(ExpressionPrefix)
                        ? context.CompileDynamic(expression.Substring(1))
                        : context.CompileDynamic("\"" + expression + "\"");
                }
                else if (nullable)
                    return null;
                else
                    throw new RenderingException(string.Format("Field '{0}' cannot be empty", name));
            }
            catch (ExpressionCompileException ex)
            {
                throw new RenderingException(String.Format("Failed to compile field '{0}' in '{1}':\r\n{2}", name, node.NodePath, ex.Message), ex);
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


        public abstract void Render(Feature feature);
        
        protected void AddToPath(ILineString lineString, GraphicsPath path)
        {
            if (lineString.Count > 1)
            {
                path.AddLines(TransformToPointsF(lineString.Vertices));

                if (lineString.IsClosed)
                    path.CloseFigure();
            }
            else
                Trace.WriteLine("LineString is invalid");
        }

        public PointF[] TransformToPointsF(ICoordinateSequence sharpPoints)
        {
            var points = ToWinPointArray(sharpPoints);
            Renderer.Transform(points);
            return points.ToPointFArray();
        }

        public PointF[] TransformToPointsF(Coordinate[] sharpPoints)
        {
            var points = ToWinPointArray(sharpPoints);
            Renderer.Transform(points);
            return points.ToPointFArray();
        }

        public static System.Windows.Point[] ToWinPointArray(ICoordinateSequence list)
        {
            int count = list.Count;
            var points = new WinPoint[count];

            for (int i = 0; i < count; i++)
                list.GetCoordinate(i).ToWinPoint(ref points[i]);

            return points;
        }

        public static System.Windows.Point[] ToWinPointArray(Coordinate[] array)
        {
            int count = array.Length;
            var points = new WinPoint[count];

            for (int i = 0; i < count; i++)
                array[i].ToWinPoint(ref points[i]);

            return points;
        }
        
        public Coordinate[] ToCoordinateArray(WinPoint[] coordinates)
        {
            int count = coordinates.Length;
            var points = new Coordinate[count];

            for (int i = 0; i < count; i++)
                points[i] = coordinates[i].ToCoordinate();

            return points;
        }

        public virtual void BeginScene(bool visible)
        {
            Visible = visible && Node.Visible;
            RenderCount = 0;
        }

        public virtual void Compile(bool recursive)
        {
        }

        public IBaseRenderer Parent { get; set; }

        public virtual IBaseRenderer FindChildByName(string name)
        {
            return null;
        }

    }
}
