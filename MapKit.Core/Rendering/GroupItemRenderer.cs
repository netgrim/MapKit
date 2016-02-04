using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ciloci.Flee;
using GeoAPI.Geometries;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    abstract class GroupItemRenderer : IGroupRenderer
    {
        private bool _compiled;
        private IList<IBaseRenderer> _declarations;
        
        public GroupItemRenderer(Renderer renderer, GroupItem groupItem, IBaseRenderer parent)
        {
            Renderer = renderer;
            GroupItem = groupItem;
            GroupItem.PropertyChanged += new PropertyChangedEventHandler(container_PropertyChanged);
            Parent = parent;
        }

        public GroupItem GroupItem { get; private set; }

        [Browsable(false)]
        public ThemeNode Node
        {
            get { return GroupItem; }
        }

        [Category(Constants.CatStatistics)]
        public int FeatureCount { get; protected set; }

        [Category(Constants.CatStatistics)]
        public TimeSpan RenderTime { get; set; }

        [Category(Constants.CatStatistics)]
        public int RenderCount { get; set; }

        [Category(Constants.CatBehavior)]
        public bool Visible { get; protected set; }

        [Browsable(false)]
        public FeatureType FeatureType
        {
            get { return null; }
        }

        public SmoothingMode SmoothingMode { get; private set; }
        
        [Browsable(false)]
        public Renderer Renderer { get; set; }
        
        public abstract void Render(Feature feature);

        public virtual void BeginScene(bool visible)
        {
            Visible = visible && GroupItem.IsVisibleAt(Renderer.Zoom);
            if (Visible && !_compiled)
                Compile();

            foreach (var renderer in _declarations)
                renderer.BeginScene(Visible);
            foreach (var childRenderer in Renderers)
                childRenderer.BeginScene(Visible);

            Renderer.Graphics.SmoothingMode = SmoothingMode;

            FeatureCount = 0;
            RenderTime = new TimeSpan();
            RenderCount = 0;
        }

        public virtual void Compile(bool recursive = false)
        {
            //GroupItem.CascadeStyles();

            SmoothingMode = GroupItem.SmoothingMode ? SmoothingMode.HighQuality : SmoothingMode.HighSpeed;

            _declarations = new List<IBaseRenderer>();
            Renderers = new List<IBaseRenderer>();

            foreach (var renderer in Renderer.GetRenderers(GroupItem, this))
                if (renderer.Node is Macro)
                    _declarations.Add(renderer);
                else
                    Renderers.Add(renderer);
            
            if (recursive)
                foreach (var renderer in Renderers)

                    renderer.Compile(recursive);

            _compiled = true;
        }

        void container_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        public List<IBaseRenderer> Renderers { get; set; }

        public IBaseRenderer Parent { get; set; }

        public IBaseRenderer FindChildByName(string name)
        {
            foreach (var renderers in new[] {_declarations, Renderers})
                foreach (var child in renderers)
                {
                    var container = child.Node as ContainerNode;
                    if (container != null && string.Compare(container.Name, name, true) == 0)
                        return child;
                }
            return null;
        }
    }
}
