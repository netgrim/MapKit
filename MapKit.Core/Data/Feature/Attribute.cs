using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("Name = {Name}")]
    public class Attribute
    {
        public Attribute(string name)
            : this(name, typeof(string))
        {}

        public Attribute(string name, Type type)
        {
            Name = name;
            Type = type;
            Ordinal = -1;
        }

        public Type Type { get; private set; }

        public string Name { get; private set; }

        public FeatureType FeatureType { get; internal set; }

        public int Ordinal { get; internal set; }
    }
}
