using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;

namespace MapKit.Core
{
    public class InheritableField<T>
    {
        private bool _inherited;
        private InheritableField<T> _parent;
        private string _expression;

        public event EventHandler Changed;

        public InheritableField()
        {
            _inherited = true;
        }

        [Browsable(false)]
        public InheritableField<T> Parent
        {
            get
            { return _parent; }
            set
            {
                //bind to parent.changed
                if (_parent == value) return;

                if (_parent != null)
                    _parent.Changed -= new EventHandler(Parent_Changed);

                _parent = value;

                if (_parent != null)
                {
                    _parent.Changed += new EventHandler(Parent_Changed);
                    if (_inherited)
                        _expression = _parent.Expression;
                }
                //cannot inherit when no parent, but cannot change _inherited to false because when a node is moved, its parent is set to null before setting it to the new parent 
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public bool Inherited
        {
            get { return _inherited; }
            set
            {
                _inherited = value;

                if (value && Parent != null)
                    _expression = Parent.Expression;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public string Expression
        {
            get { return _expression; }
            set
            {

                if (_expression == value) return;
                _expression = value;

                //if (Parent != null && Equals(_expression, Parent._expression))
                //    _inherited = true;

                OnChanged(EventArgs.Empty);
            }
        }

        void Parent_Changed(object sender, EventArgs e)
        {
            if (_inherited)
                Expression = Parent.Expression;
        }

        private void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }
    }


    [TypeConverter(typeof(InheritableColorTypeConverter))]
    [Editor(typeof(InheritableColorEditor), typeof(UITypeEditor))]
    public class InheritableColor : InheritableField<Color>
    {
        private const string DefColor = "Black";

        public InheritableColor()
        {
            Expression = DefColor;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color Value
        {
            get
            {
                Color color;
                return Util.TryParseColor(Expression, out color) ? color : Color.FromName(Expression ?? string.Empty);
            }
            set
            {
                if (value.IsKnownColor)
                    Expression = value.ToKnownColor().ToString();
                else
                    Expression = ColorTranslator.ToHtml(value);
                Inherited = false;
            }
        }

        public override string ToString()
        {
            return Expression;
        }
    }


}
