using Ciloci.Flee;
using System.ComponentModel;
using System.Diagnostics;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class MacroExecuter : FeatureRenderer
    {
        private Run _run;

        private IBaseRenderer _childRenderer;

        public MacroExecuter(Renderer renderer, Run run, IBaseRenderer parent)
            :base(renderer, run, parent)
        {
            _run = run;
            run.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(run_PropertyChanged);
        }

        public override void BeginScene(bool visible)
        {
            base.BeginScene(visible);
            //if (Visible && !_compiled)
            //    Compile();
        }

        public override void Compile(bool recursive = false)
        {
            _childRenderer = FindByName(_run.Macro) as IBaseRenderer;
            if (_childRenderer == null)
                Trace.WriteLine(_run.NodePath + ": Cannot find node named '" + _run.Macro + "')");
            Debug.Assert(_childRenderer != null);
        }

        void run_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Stroke.VisiblePropertyName) return;
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
