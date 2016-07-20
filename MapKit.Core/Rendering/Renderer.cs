using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

using WinPoint = System.Windows.Point;
using WinMatrix = System.Windows.Media.Matrix;
using GeoAPI.Geometries;
using GeoAPI;
using NetTopologySuite;
using Ciloci.Flee;
using System.Xml;

namespace MapKit.Core.Rendering
{
    public class Renderer
	{
        private const long PROGRESS_INTERVAL = 100;
        internal const string FEATURE_VAR = "feature";

		//long _fidSequence;
		private bool _cancelPending;
		private Envelope _window;
		private Graphics _g;
        private WinMatrix _translate;
        private int _renderedLayer;
        private long _lastProgressTick;
        private IGroupRenderer _mapRenderer;

        public Renderer(Map map)
        {
            GeometryServiceProvider.Instance = new NtsGeometryServices();
            //LineStyles = new Dictionary<string, Style>(StringComparer.OrdinalIgnoreCase);
            Context = new ExpressionContext(this);
            Context.Variables.DefineVariable(FEATURE_VAR, typeof(Feature));
            Context.Options.ParseCulture = System.Globalization.CultureInfo.InvariantCulture;
            Context.Imports.AddType(typeof(Math), "math");
            Context.Imports.AddType(typeof(Drawing), "color");

            FeatureVarResolver = new FeatureVariableResolver();
            FeatureVarResolver.BindContext(Context);

            Map = map;

            map.Renderer = _mapRenderer = new MapRenderer(this, map);
            map.Changed += new EventHandler(map_Changed);

            _window = new Envelope();
        }

        public IDictionary<string, LineStyle> LineStyles { get; private set; }

        void map_Changed(object sender, EventArgs e)
        {
            _cascaded = false;
            //InitializeRenderers(Map);
            //Map.CascadeStyles();
        }

        #region event

        public event EventHandler Rendered;
        private void OnRendered(EventArgs e)
        {
            if (Rendered != null)
                Rendered(this, e);
        }
				
		internal event EventHandler<LayerEventArgs> LayerRendered;
		private void OnLayerRendered(LayerEventArgs e)
		{
			if (LayerRendered != null)
				LayerRendered(this, e);
		}

		public event EventHandler<LayerFailedEventArgs> LayerFailed;
		private void OnLayerFailed(LayerFailedEventArgs e)
		{
			if (LayerFailed != null)
				LayerFailed(this, e);
		}

		public event ProgressChangedEventHandler ProgressChanged;
        private bool _cascaded;
		private void OnProgressChanged(ProgressChangedEventArgs e)
		{
			if (ProgressChanged != null)
				ProgressChanged(this, e);
		}

        #endregion


        public Map Map { get; private set; }

        public bool GatherStatistics { get; set; }

        public Graphics Graphics
        {
            get { return _g; }
        }

        public bool CancelPending
        {
            get { return _cancelPending; }
        }

        public Envelope Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public WinMatrix Translate
        {
            get { return _translate; }
            set { _translate = value; }
        }

        public int LayerToRender { get; set; }

		public void BeginScene(Graphics g)
		{
			_g = g;
			LayerToRender = 0;
			_renderedLayer = 0;
			_cancelPending = false;

            if (!_cascaded)
                CascadeStyles(Map);
            _cascaded = true;

            _mapRenderer.BeginScene(true);

			FrameNumber++;
			
			if (SmallQueryWindow)
			{
				var margin = _window.Width / 8;
				_window.ExpandBy(-margin);
			}

            if (Svg != null)
                Svg.Close();
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            Svg = XmlWriter.Create("output.svg", settings);
            Svg.WriteProcessingInstruction("xml-stylesheet", "href = \"main.css\" type = \"text/css\"");
                 Svg.WriteStartElement("svg", "http://www.w3.org/2000/svg");
            Svg.WriteAttributeString("width", "2000");
            Svg.WriteAttributeString("height", "2000");
            Svg.WriteAttributeString("xmlns", "xlink", null, "http://www.w3.org/1999/xlink");
        }

		public void EndScene()
		{
            Svg.WriteEndElement();

            Svg.Close();
            Svg = null;
		}

		public void Render()
		{
			Progress();

            
			_mapRenderer.Render(null);

            if (ShowQueryMBR)
            {
                var points = new[] { new WinPoint(_window.MinX, _window.MinY), new WinPoint(_window.MaxX, _window.MaxY) };
                Transform(points);

                var v = points[1] - points[0];
                using (var brush = new SolidBrush(Color.FromArgb(50, Color.Green)))
                    _g.FillRectangle(brush, (float)points[0].X, (float)points[0].Y, (float)v.X, (float)v.Y);
            }

			OnRendered(EventArgs.Empty);
		}

        public void CascadeStyles(ContainerNode container)
        {
            var nodes = container.Nodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                var style = nodes[i] as Style;
                if (style != null)
                    for (int j = i + 1; j < nodes.Count; j++)
                        if (nodes[j].Cascade(style))
                            return;

                var childContainer = nodes[i] as ContainerNode;
                if (childContainer != null)
                    CascadeStyles(childContainer);
            }
        }

		private void Progress()
		{
			var tick = Environment.TickCount;
			if (tick - _lastProgressTick > PROGRESS_INTERVAL)
					OnProgressChanged(new ProgressChangedEventArgs((int)((float)_renderedLayer / LayerToRender * 100), null));
			_lastProgressTick = tick;
		}
        
		public static IEnumerable<IPoint> GetPoint(IGeometry geometry)
		{
			switch (geometry.OgcGeometryType)
			{
                case OgcGeometryType.Point:
                //case OgcGeometryType.PointZ:
                //case OgcGeometryType.PointM:
                //case OgcGeometryType.PointZM:
                    yield return (IPoint)geometry;
					break;
				case OgcGeometryType.MultiPoint:
					var multiPoint = (IMultiPoint)geometry;
					foreach (IPoint point in multiPoint.Geometries)
						yield return point;
					break;
				case OgcGeometryType.GeometryCollection:
					foreach (var geometryItem in ((IGeometryCollection)geometry))
						switch (geometryItem.OgcGeometryType)
						{
                            case OgcGeometryType.Point:
                            //case OgcGeometryType.PointZ:
                            //case OgcGeometryType.PointM:
                            //case OgcGeometryType.PointZM:
								yield return (IPoint)geometry;
								break;
							case OgcGeometryType.MultiPoint:
								foreach (IPoint point in ((IMultiPoint)geometryItem).Geometries)
									yield return point;
								break;
						}
					break;
				default:
					throw new Exception("Unsuported geometry type for GetPoint: " + geometry.OgcGeometryType);
			}
		}

		public static IEnumerable<ILineString> GetLineString(IGeometry geometry)
		{
			switch (geometry.OgcGeometryType)
			{
				case OgcGeometryType.LineString:
					yield return  (ILineString)geometry;
					break;
				case OgcGeometryType.Polygon:
					var polygon = (IPolygon)geometry;
					yield return polygon.ExteriorRing;
					foreach (var linearRing in polygon.InteriorRings)
						yield return linearRing;
					break;
				case OgcGeometryType.MultiPolygon:
					foreach (IPolygon polygonItem in ((IMultiPolygon)geometry).Geometries)
					{
						yield return (ILineString)polygonItem.ExteriorRing;
						foreach (var linearRing in polygonItem.InteriorRings)
							yield return linearRing;
					}
					break;
				case OgcGeometryType.MultiLineString:
					foreach (ILineString lineString in ((IMultiLineString)geometry).Geometries)
						yield return lineString;
					break;
				case OgcGeometryType.GeometryCollection:
					foreach (var geometryItem in ((IGeometryCollection)geometry).Geometries)
						foreach(var lineString in GetLineString(geometryItem))
							yield return lineString;
					break;
				default:
					throw new Exception("Unsuported geometry type for GetLineString: " + geometry.OgcGeometryType);
			}
		}

		public void Cancel()
		{
			_cancelPending = true;
		}

		public bool ShowQueryMBR { get; set; }

		public bool ShowGeometryMBR { get; set; }

		public bool SmallQueryWindow { get; set; }

		public int FrameNumber { get; private set; }

        internal FeatureVariableResolver FeatureVarResolver { get; private set; }

        public ExpressionContext Context { get; set; }

        public Double Zoom { get; set; }

        //public Double Angle { get; set; }

        internal void PerformLayerRendered(LayerEventArgs e)
        {
            _renderedLayer++;
            if (LayerRendered != null)
                OnLayerRendered(e);

            Progress();
        }

        internal void PerformLayerFailed(LayerFailedEventArgs e)
        {
            OnLayerFailed(e);
            if (e.Cancel)
                Cancel();
        }

        internal void Render(ContainerNode container, Feature feature)
        {
            var nodes = container.Nodes;
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                var childRenderer = (IFeatureRenderer)nodes[i].Renderer;
                if (childRenderer != null && childRenderer.Visible)
                {
                    childRenderer.Render(feature);
                    if (CancelPending)
                        break;
                }
            }
        }
        
        internal void Render(IList<IBaseRenderer> renderers, Feature feature)
        {
            foreach(var renderer in renderers)
            {
                if (renderer != null && renderer.Visible)
                {
                    renderer.Render(feature);
                    if (CancelPending)
                        break;
                }
            }
        }

        //internal void InitializeRenderer(ThemeNode node, ExpressionContext context)
        //{
        //    if (node.Renderer == null)
        //        node.Renderer = CreateRenderer(node, context);

        //    if (node is Text)
        //    {
        //        var text = (Text)node;
        //        if (text.LabelBox != null && text.LabelBox.Renderer == null)
        //            text.LabelBox.Renderer = CreateRenderer(text.LabelBox, context);
        //    }
        //}

        internal IBaseRenderer CreateRenderer(ThemeNode node, ExpressionContext context, IBaseRenderer parent)
        {
            if (node is Group)
                return new LayerGroupRenderer(this, (Group)node, parent);
            if (node is Layer)
                return new LayerRenderer(this, (Layer)node, parent);
            if (node is Case)
                return new CaseRenderer(this, (Case)node, parent);
            if (node is LinearCalibration)
                return new LinearCalibrationRenderer(this, (LinearCalibration)node, parent);
            if (node is Marker)
                return  new MarkerRenderer(this, (Marker)node, parent);
            if (node is PointExtractor)
                return new PointExtractorRenderer(this, (PointExtractor)node, parent);
            if (node is Stroke)
                return new StrokeRenderer(this, (Stroke)node, parent);
            if (node is Text)
                return new TextRenderer(this, (Text)node, parent);
            if (node is VerticesEnumerator)
                return new VerticesEnumeratorRenderer(this, (VerticesEnumerator)node, parent);
            if (node is SolidFill)
                return new SolidFillRenderer(this, (SolidFill)node, parent);
            if (node is LineStyle)
                return new LineStyleRenderer(this, (LineStyle)node, parent);
            if (node is Style)
                return null;
            if (node is Window)
                return new WindowRenderer(this, (Window)node, parent);
            if (node is PolygonNode)
                return new PolygonRenderer(this, (PolygonNode)node, parent);
            if (node is PolylineNode)
                return new PolylineRenderer(this, (PolylineNode)node, parent);
            if (node is Animate)
                return new AnimateRenderer(this, (Animate)node,  parent);
            if (node is When)
                return new FeatureRenderer(this, (When)node, parent);
            if (node is Else)
                return new FeatureRenderer(this, (Else)node, parent);
            if (node is ContainerNode)
                return new ContainerNodeRenderer(this, (ContainerNode)node, parent);
            if (node is Variable)
                return new VarRenderer(this, (Variable)node, parent);
            if (node is Run)
                return new MacroExecuter(this, (Run)node, parent);

            Debug.Fail("No renderer");
            return null;
        }

        //private void StyleNodeFound(string p)
        //{
        //    throw new NotImplementedException();
        //}

        //private void InitializeRenderers(ContainerNode container, ExpressionContext context)
        //{
        //    foreach (var child in container.Nodes)
        //        InitializeRenderer(child, context);
        //}

        public void Compile()
        {
            _mapRenderer.Compile(true);
        }


        internal void HandleError(string p)
        {
            Trace.WriteLine(p);
            //throw new RenderingException(p);
        }


        internal IEnumerable<IBaseRenderer> GetRenderers(ContainerNode node, IBaseRenderer parent)
        {
            var nodes = node.Nodes;
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                var child = nodes[i];
                
                var childStyle = child as Style;
                if(childStyle != null)
                    foreach(var childSubRenderer in GetRenderers(childStyle, parent))
                        yield return childSubRenderer;

                var renderer = child.Renderer ?? (child.Renderer = CreateRenderer(child, null, parent));
                if (renderer != null) 
                    yield return renderer;
            }
        }

        public void Transform(WinPoint[] points)
        {
            _translate.Transform(points);
        }

        public WinPoint Transform(WinPoint point)
        {
            return _translate.Transform(point);
        }

        public PointF[] TransformToPointsF(ICoordinateSequence sharpPoints)
        {
            var points = TransformUtil.ToWinPointArray(sharpPoints);
            Transform(points);
            return points.ToPointFArray();
        }

        public PointF[] TransformToPointsF(Coordinate[] sharpPoints)
        {
            var points = TransformUtil.ToWinPointArray(sharpPoints);
            Transform(points);
            return points.ToPointFArray();
        }

        public WinMatrix Matrix { get; set; }

        public XmlWriter Svg { get; set; }

    }
}
