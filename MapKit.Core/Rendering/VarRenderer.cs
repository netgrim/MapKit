using System;
using System.Diagnostics;
using System.ComponentModel;
using MapKit.Core.Rendering;

namespace MapKit.Core
{
    class VarRenderer : IBaseRenderer
    {
        private Variable _variable;
        private bool _compiled;
        private Renderer _renderer;
        private object _value;

        public VarRenderer(Renderer renderer, Variable variable, IBaseRenderer parent)
        {
            Debug.Assert(renderer != null);
            Debug.Assert(variable != null);
            _variable = variable;
            _variable.PropertyChanged += new PropertyChangedEventHandler(_node_PropertyChanged);
            _renderer = renderer;
            Parent = parent;
        }

        [Browsable(false)]
        public ThemeNode Node
        {
            get { return _variable; }
        }

        void _node_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _compiled = false;
        }

        public void Render(Feature feature)
        {
            _renderer.Context.Variables[_variable.Name] = _value;
            RenderCount++;
        }

        public void BeginScene(bool enabled)
        {
            Visible = _variable.Visible && enabled;

            if (Visible && !_compiled)
                Compile();

            RenderCount = 0;
        }

        public void Compile(bool recursive = false)
        {
            _value = Convert.ChangeType(_variable.Value, _variable.Type);
            
            _compiled = true;
        }

        public int RenderCount { get; set; }

        public bool Visible { get; private set; }

        public IBaseRenderer Parent { get; set; }

        public IBaseRenderer FindChildByName(string name)
        {
            return null;
        }
    }
}
