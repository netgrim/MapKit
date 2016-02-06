using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    abstract class GroupBaseRenderer : IGroupRenderer
    {
        private bool _compiled;
        private IList<IBaseRenderer> _declarations;

        public GroupBaseRenderer(Renderer renderer, GroupItem groupItem, IBaseRenderer parent)
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
        
        public IBaseRenderer Parent { get; set; }

        public SmoothingMode SmoothingMode { get; private set; }

        public IList<IBaseRenderer> Renderers { get; set; }
        
        [Browsable(false)]
        public Renderer Renderer { get; set; }

        public abstract void Render(Feature feature);

        public virtual void BeginScene(bool visible)
        {
            Visible = visible && GroupItem.IsVisibleAt(Renderer.Zoom);
            if (Visible && !_compiled)
                Compile();

            if(_compiled)
            {
                foreach (var declaration in _declarations)
                    declaration.BeginScene(Visible);
                foreach (var renderer in Renderers)
                    renderer.BeginScene(Visible);
            }

            FeatureCount = 0;
            RenderTime = new TimeSpan();
            RenderCount = 0;
        }

        public virtual void Compile(bool recursive = false)
        {
            SmoothingMode = GroupItem.SmoothingMode ? SmoothingMode.HighQuality : SmoothingMode.HighSpeed;

            _declarations = new List<IBaseRenderer>();
            Renderers = new List<IBaseRenderer>();

            foreach (var renderer in Renderer.GetRenderers(GroupItem, this))
                if (renderer.Node is Macro)
                    _declarations.Add(renderer);
                else
                {
                    Renderers.Add(renderer);

                    if (recursive)
                        renderer.Compile(true);
                }

            _compiled = true;
        }

        public IBaseRenderer FindChildByName(string name)
        {
            //foreach (var renderers in new[] {_declarations, Renderers})
            //    foreach (var child in renderers)
            //    {
            //        var container = child.Node as ContainerNode;
            //        if (container != null && string.Compare(container.Name, name, true) == 0)
            //            return child;
            //    }
            return null;
        }

        void container_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _compiled = false;
        }
    }
}
