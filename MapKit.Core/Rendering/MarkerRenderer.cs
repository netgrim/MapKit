using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GeoAPI.Geometries;
using MapKit.Core;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using Ciloci.Flee;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class MarkerRenderer : FeatureRenderer
    {
        private Marker _marker;
        private IGenericExpression<double> _angleEvaluator;
        private IGenericExpression<double> _scaleXEvaluator;
        private IGenericExpression<double> _scaleYEvaluator;
        private IGenericExpression<float> _opacityEvaluator;
        private IGenericExpression<bool> _overlappableEvaluator;
        private IGenericExpression<ContentAlignment> _alignEvaluator;
        private IGenericExpression<bool> _allowOverlapEvaluator;
        private IGenericExpression<string> _fileEvaluator;
        private IGenericExpression<Color> _colorEvaluator;
        private bool _compiled;
        private Color _color = Color.Black;
        private float _opacity = 1;
        private double _scaleX = 1;
        private double _scaleY = 1;
        private double _angle = 0;

        public MarkerRenderer(Renderer renderer, Marker marker, IBaseRenderer parent)
            : base(renderer, marker, parent)
        {
            _marker = marker;
            _marker.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_marker_PropertyChanged);
        }

        void _marker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        public override void Render(Feature feature)
        {
            //_resolver.Feature = feature;
            var image = string.IsNullOrEmpty(_marker.File) ? null : GetImage(_marker.File.ToLower());

            foreach (var point in Renderer.GetPoint(feature.Geometry))
                DrawPoint(point, image);
        }

        private void DrawPoint(IPoint point, Image image)
        {
            var g = Renderer.Graphics;

            var winPoint = point.ToWinPoint();
            var width = (float)Evaluate(_scaleXEvaluator, _scaleX);
            var height = (float)Evaluate(_scaleYEvaluator, _scaleY);
            var alignment = Evaluate(_alignEvaluator, ContentAlignment.MiddleCenter);
            winPoint.Y += GetVerticalAlignentOffset(alignment, height);
            winPoint.X += GetHorizontalAligmentOffset(alignment, width);


            if (image == null)
            {
                var pScreenF = Renderer.Transform(winPoint).ToPointF();
                var color = GetRenderColor(_opacityEvaluator, _opacity, _colorEvaluator, _color);
                using (var brush = new SolidBrush(color))
                    g.FillEllipse(brush, pScreenF.X, pScreenF.Y, width, height);
            }
            else
            {
                float scaleX = _marker.ScaleX != null ? float.Parse(_marker.ScaleX, System.Globalization.NumberFormatInfo.InvariantInfo) : 1;
                float scaleY = _marker.ScaleY != null ? float.Parse(_marker.ScaleY, System.Globalization.NumberFormatInfo.InvariantInfo) : 1;
                float angle = _marker.Angle != null ? float.Parse(_marker.Angle, System.Globalization.NumberFormatInfo.InvariantInfo) : 0;

                float opacity = _marker.Opacity != null ? float.Parse(_marker.Opacity, System.Globalization.NumberFormatInfo.InvariantInfo) : 1;

                float[][] matrixAlpha =
					  {
					   new float[] {1, 0, 0, 0, 0},
					   new float[] {0, 1, 0, 0, 0},
					   new float[] {0, 0, 1, 0, 0},
					   new float[] {0, 0, 0, opacity, 0}, 
					   new float[] {0, 0, 0, 0, 1}
					  };
                ColorMatrix colorMatrix = new ColorMatrix(matrixAlpha);

                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(
                 colorMatrix,
                 ColorMatrixFlag.Default,
                 ColorAdjustType.Bitmap);

                var dest = new Rectangle((int)(point.X), (int)(point.Y), 10, 10);
                var src = new Rectangle(0, 0, image.Width, image.Height);

                //_g.DrawImage(image, dest, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes, null, IntPtr.Zero);
                var oldMatrix = (Matrix)g.Transform.Clone();

                using (var m = new Matrix())
                {
                    m.Scale(scaleX, scaleY);
                    //m.Rotate(angle);
                    //m.Translate(offsetX, offsetY);
                    //_g.MultiplyTransform(m, MatrixOrder.Append);

                    using (var f = new Font("Sppc", 10))
                        g.DrawString("B", f, Brushes.Red, point.ToPointF());

                    using (var pen = new Pen(Color.Black, 0.1f))
                        g.DrawLine(pen, (float)point.X, (float)point.Y, (float)point.X + 10, (float)point.Y);

                    g.Transform = oldMatrix;
                }
            }
        }

        private Image GetImage(string path)
        {
            return null;
            //Image image;
            //if (!_markerFiles.TryGetValue(path, out image))
            //{
            //    var fullPath = Path.IsPathRooted(path)
            //        ? path
            //        : Path.Combine(_marker.Map.Base, path);

            //    var size = 0;
            //    if (File.Exists(fullPath))
            //    {
            //        image = Image.FromFile(fullPath);
            //        size = (int)new FileInfo(fullPath).Length;
            //    }
            //    else
            //        Debug.WriteLine("File does not exists: " + fullPath);

            //    _markerFiles.Add(path, image, size);
            //}

            //return image;
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);

            if (Visible && !_compiled)
                Compile();
        }

        public override void Compile(bool recursive = false)
        {
            //Debug.Assert(InputFeatureType != null);

            //var context = CreateContext(Renderer);
            //var colorContext = CreateColorContext();
            //var contentAlignmentContext = CreateContentAlignmentContext();

            //_resolver = new FeatureVariableResolver(InputFeatureType);
            //_resolver.BindContext(context);
            //_resolver.BindContext(colorContext);
            //_resolver.BindContext(contentAlignmentContext);

            var context = Renderer.Context;
            var colorContext = context;
            var contentAlignmentContext = context;
            
            _angleEvaluator = CompileDoubleExpression(context, Marker.AnglePropertyName, _marker.Angle, ref _angle);
            _scaleXEvaluator = CompileDoubleExpression(context, Marker.ScaleXPropertyName, _marker.ScaleX, ref _scaleX);
            _scaleYEvaluator = CompileDoubleExpression(context, Marker.ScaleYPropertyName, _marker.ScaleY, ref _scaleY);

            _opacityEvaluator = CompileFloatExpression(context, Marker.OpacityPropertyName, _marker.Opacity, ref _opacity);
            _overlappableEvaluator = CompileExpression<bool>(context, Marker.OverlappablePropertyName, _marker.Overlappable);
            _alignEvaluator = CompileExpression<ContentAlignment>(contentAlignmentContext, Marker.AlignmentPropertyName, _marker.Alignment);
            _allowOverlapEvaluator = CompileExpression<bool>(context, Marker.AllowOverlapPropertyName, _marker.AllowOverlap);
            _fileEvaluator = CompileExpression<string>(context, Marker.FilePropertyName, _marker.File);
            _colorEvaluator = CompileColorExpression(colorContext, Marker.FilePropertyName, _marker.Color, ref _color);
         
            _compiled = true;
        }

    }
}
