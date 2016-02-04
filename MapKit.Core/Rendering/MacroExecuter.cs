using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Ciloci.Flee;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Diagnostics;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class MacroExecuter : FeatureRenderer
    {
        private Run _run;

        private bool _compiled;
        private IBaseRenderer _childRenderer;
        private ExpressionContext _context;

        public MacroExecuter(Renderer renderer, Run run, ExpressionContext context, IBaseRenderer parent)
            :base(renderer, run, parent)
        {
            _run = run;
            run.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(run_PropertyChanged);
            _context = context;
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            if (Visible && !_compiled)
                Compile();
        }

        public override void Compile(bool recursive = false)
        {
            _childRenderer = FindByName(_run.Macro) as IBaseRenderer;
            if (_childRenderer == null)
                Trace.WriteLine(_run.NodePath + ": Cannot find node named '" + _run.Macro + "')");
            Debug.Assert(_childRenderer != null);

            _compiled = true;
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

        void run_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Stroke.VisiblePropertyName) return;
            _compiled = false;
        }

        public override void Render(Feature feature)
        {
            if (_childRenderer != null)
                _childRenderer.Render(feature);
        }

        class ExpressionOwner<T>
        {
            public T[] Array(params T[] elements)
            {
                return elements;
            }
        }
    }
}
