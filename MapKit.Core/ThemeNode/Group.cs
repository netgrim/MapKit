﻿using System;
using System.Diagnostics;
using System.Xml;

namespace MapKit.Core
{
    [DebuggerDisplay("Group('{Name}'), Path={NodePath}")]
    public class Group : GroupItem
    {
        private const string ElementName = "group";

        static Group()
        {
            NodeType = new NodeType("Group", ElementName, typeof(Group));
        }

        public static NodeType NodeType { get; private set; }

        public override object Clone()
        {
            var group = (Group)MemberwiseClone();

            group.Nodes = new ThemeNodeCollection<ThemeNode>(group);
            foreach (var node in Nodes)
                group.Nodes.Add((ThemeNode)node.Clone());

            throw new NotImplementedException();
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == Group.AntiAliasField) SmoothingMode = bool.Parse(reader.Value);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
}
