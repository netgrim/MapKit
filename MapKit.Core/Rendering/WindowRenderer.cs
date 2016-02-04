using Ciloci.Flee;
using System.Drawing;
using GeoAPI.Geometries;
using System.Collections.Generic;
using System.Windows.Media;

using WinPoint = System.Windows.Point;
using WinMatrix = System.Windows.Media.Matrix;
using System.Drawing.Drawing2D;
using MapKit.Core.Rendering;
using NetTopologySuite.Geometries;

namespace MapKit.Core
{
    class WindowRenderer : ContainerNodeRenderer
    {
        private Window _window;
        private IGenericExpression<Color> _bgColorEvaluator;
        private IGenericExpression<double> _centerXEvaluator;
        private IGenericExpression<double> _centerYEvaluator;
        private IGenericExpression<double> _zoomEvaluator;
        private IGenericExpression<double> _angleEvaluator;
        private bool _compiled;
        private Color _bgColor = Color.Gray;
        private double _centerX;
        private double _centerY;
        private double _zoom = 1;
        private double _angle;
        private int _recursion = 0;
        
        public WindowRenderer(Renderer renderer, Window window, IBaseRenderer parent)
            : base(renderer, window, parent)
        {
            _window = window;
            _window.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_window_PropertyChanged);
        }

        void _window_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        public override void Render(Feature feature)
        {
            if (_recursion > _window.MaxRecursion)
                return;
            RenderCount++;

            var geometry = feature.Geometry;
            //geometry = geometry.Envelope;

            if (geometry.OgcGeometryType != OgcGeometryType.Polygon &&
                geometry.OgcGeometryType != OgcGeometryType.MultiPolygon)
                return;

            if (feature.Fid != 6306)
                return;
            
            //evaluate fields
            var centerX = Evaluate(_centerXEvaluator, _centerX);
            var centerY = Evaluate(_centerYEvaluator, _centerY);
            var zoom = Evaluate(_zoomEvaluator, _zoom);
            var angle = Evaluate(_angleEvaluator, _angle);
            
            var centroid = geometry.Centroid.ToWinPoint();

            //temporary debug value
            //_zoom = (_zoom + 10) % 360;
            //centerX = centroid.X + 25;
            //centerY = centroid.Y;
            //zoom = 0.5;
            //angle = -_zoom;
            
            //clip path coordinates      
            var coordinates = ToWinPointArray(geometry.Coordinates);
            Renderer.Transform(coordinates);
            var coordinatesF = coordinates.ToPointFArray();
            var clipPath = new GraphicsPath();
            clipPath.AddPolygon(coordinatesF);

            //camera transform
            var camera = new WinMatrix();
            camera.Translate(-centroid.X, -centroid.Y);
            camera.Scale(1/zoom, 1/zoom);
            camera.Rotate(-angle);
            camera.Translate(centerX, centerY);

            //camera polygon
            coordinates = ToWinPointArray(geometry.Coordinates);
            camera.Transform(coordinates);

            var queryWindow = new Envelope();
            ExpendToInclude(queryWindow, coordinates);

            var oldMatrix = Renderer.Matrix;
            var invCamera = camera;
            invCamera.Invert();
            var newMatrix = invCamera * oldMatrix;

            var newTranslate = newMatrix;
            newTranslate.OffsetX = newTranslate.OffsetY = 0;
            newTranslate.Invert();
            newTranslate = newMatrix * newTranslate;

            Feature queryFeature = null;
            Feature windowTargetFeature = null;
            var newTransform = new System.Drawing.Drawing2D.Matrix((float)newMatrix.M11, (float)newMatrix.M12, (float)newMatrix.M21, (float)newMatrix.M22, 0f, 0f);
            var oldWindow = Renderer.Window;
            var savedState = Renderer.Graphics.Save();
            var oldModelView = Renderer.Translate;
            var oldZoom = Renderer.Zoom;
            var oldAngle = Renderer.Angle;
            
            _recursion++;

            int i = 0;
            while (i < Renderers.Count)
            {
                var renderer = Renderers[i];
                var childNode = renderer.Node;
                if (childNode is WindowQuery)
                {
                    if (queryFeature == null)
                    {
                        queryFeature = new Feature(feature);
                        queryFeature.Geometry = Util.ToPolygon(queryWindow);
                    }

                    do
                        renderer.Render(queryFeature);
                    while (++i < Renderers.Count && (childNode = (renderer = Renderers[i]).Node) is WindowQuery);
                }
                else if (childNode is WindowTarget)
                {
                    if (windowTargetFeature == null)
                    {
                        windowTargetFeature = new Feature(feature);
                        windowTargetFeature.Geometry = new Polygon(new LinearRing(ToCoordinateArray(coordinates)));
                    }

                    do
                        renderer.Render(windowTargetFeature);
                    while (++i < Renderers.Count && (childNode = (renderer = Renderers[i]).Node) is WindowTarget);
                }
                else
                {
                    try
                    {
                        Renderer.Window = queryWindow;
                        if (_window.Clip)
                        {
                            var region = Renderer.Graphics.Clip;
                            if (region != null)
                            {
                                region.Intersect(clipPath);
                                Renderer.Graphics.Clip = region;
                            }
                            else
                                Renderer.Graphics.SetClip(clipPath);
                        }

                        Renderer.Matrix = newMatrix;
                        Renderer.Translate = newTranslate;
                        Renderer.Graphics.Transform = newTransform;
                        Renderer.Zoom *= zoom;
                        Renderer.Angle += angle;

                        //Renderer.Graphics.Clear(Evaluate(_bgColorEvaluator, _bgColor));

                        do
                            renderer.Render(feature);
                        while (++i < Renderers.Count && !((childNode = (renderer = Renderers[i]).Node) is WindowQuery || childNode is WindowTarget));
                    }
                    finally
                    {
                        Renderer.Window = oldWindow;
                        Renderer.Graphics.Restore(savedState);
                        Renderer.Translate = oldModelView;
                        Renderer.Zoom = oldZoom;
                        Renderer.Angle = oldAngle;
                        Renderer.Matrix = oldMatrix;
                        _recursion--;
                    }
                }
            }

            ////debug: render camera polygon
            //Renderer.Transform(coordinates);
            //coordinatesF = coordinates.ToPointFArray();
            //using (var brush = new SolidBrush(Color.FromArgb(50, Color.Red)))
            //    Renderer.Graphics.FillPolygon(brush, coordinatesF);

            ////debug: render camera envelope
            //var points = new[] { new WinPoint(queryWindow.MinX, queryWindow.MinY), new WinPoint(queryWindow.MaxX, queryWindow.MaxY) };
            //Renderer.Transform(points);
            //var size = points[1] - points[0];
            //using (var brush = new SolidBrush(Color.FromArgb(50, Color.Green)))
            //    Renderer.Graphics.FillRectangle(brush, (float)points[0].X, (float)points[0].Y, (float)size.X, (float)size.Y);
        }


        private void ExpendToInclude(Envelope envelope, IEnumerable<WinPoint> points)
        {
            foreach (var point in points)
                ExpendToInclude(envelope, point);
        }

        private void ExpendToInclude(Envelope envelope, WinPoint point)
        {
            envelope.ExpandToInclude(point.X, point.Y);
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);

            if (!_compiled && Visible)
                Compile();
        }

        public override void Compile(bool recursive = false)
        {
            base.Compile(recursive);

            _bgColorEvaluator = CompileColorExpression(Renderer.Context, Window.BgColorField, _window.BackgroundColor, ref _bgColor, true);
            _centerXEvaluator = CompileDoubleExpression(Renderer.Context, Window.CenterXField, _window.CenterX, ref _centerX);
            _centerYEvaluator = CompileDoubleExpression(Renderer.Context, Window.CenterYField, _window.CenterY, ref _centerY);
            _zoomEvaluator = CompileDoubleExpression(Renderer.Context, Window.ZoomField, _window.Zoom, ref _zoom);
            _angleEvaluator = CompileDoubleExpression(Renderer.Context, Window.AngleField, _window.Angle, ref _angle);
            _compiled = true;
        }
    }
}
