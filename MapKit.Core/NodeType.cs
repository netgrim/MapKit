using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("{Name}")]
    public class NodeType
    {
        public NodeType(string name, string elementName, Type type)
        {
            if(name == null) throw new ArgumentNullException("name");
            if(elementName == null) throw new ArgumentNullException("elementName");
            if(type == null) throw new ArgumentNullException("type");

            Name = name;
            ElementName = elementName;
            Type = type;
            NodeTypes = new List<NodeType>();
        }

        public NodeType(Type type)
            :this(type.Name.Substring(0,1).ToLower() + type.Name.Substring(1), type)
        {
        }

        public NodeType(string elementName, Type type)
            :this(type.Name, elementName, type)
        {
        }

        public string ElementName { get; private set; }

        public string DisplayText { get; set; }
        
        public string Name { get; set; }

        public Type Type { get; private set; }

        public List<NodeType> NodeTypes { get; set; }
        
        public List<NodeType> SubTypes { get; set; }
            
        public virtual IEnumerable<ThemeNode> CreateNew()
        {
            yield return (ThemeNode)Activator.CreateInstance(Type);
        }

        public virtual bool CanAddTo(ThemeNode node)
        {
            return node is ContainerNode;
        }

        public virtual IEnumerable<NodeType> GetSubTypes(ThemeNode context)
        {
            yield break;
        }
    }
}
