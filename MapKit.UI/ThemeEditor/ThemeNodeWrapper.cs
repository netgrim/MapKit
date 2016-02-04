using System;
using System.ComponentModel;
using MapKit.Core;
using System.Diagnostics;

namespace MapKit.UI
{
	class ThemeNodeWrapper : IEditableNode
	{
        public event EventHandler LabelChanged;

	    public ThemeNodeWrapper(ThemeNode node)
        {
            Node = node;
            node.PropertyChanged += new PropertyChangedEventHandler(node_PropertyChanged);
        }

        void node_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ThemeNode.LabelOrDefaultProperty)
                if (LabelChanged != null)
                    LabelChanged(this, EventArgs.Empty);
        }

		public virtual bool CanShowProperties()
		{
			return true;
		}

        public virtual string Label
        {
            get { return Node.LabelOrDefault; }
            set { Node.Label = value; }
        }

        [Browsable(false)]
        public virtual ThemeNode Node { get; set; }


        public virtual bool CanMoveTo(ContainerNode destination, int oldIndex, int newIndex)
		{
			return destination != null;
		}

		public virtual bool CanCopyTo(object destination)
		{
			return false;
		}

        public virtual void MoveTo(ContainerNode destination, int index)
		{
            var oldParent = Node.Parent;
            if (oldParent != null)
                oldParent.Nodes.Remove(Node);
            else
                Debug.Fail("Parent is null before move");
            Debug.Assert(Node.Parent == null);
            Debug.Assert(CanMoveTo(destination, -1, index));
            
            destination.Nodes.Insert(index, Node);
            Debug.Assert(Node.Parent == destination);
        }

		public virtual void CopyTo(object destination)
		{
            throw new NotImplementedException();
        }

        public virtual bool CanRename()
        {
            return false;
        }

        public virtual bool CanRemove()
        {
            return Node.Parent != null;
        }
    }
}
