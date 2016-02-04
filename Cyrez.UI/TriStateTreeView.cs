using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Cyrez.UI
{
    /// <summary>
    /// TreeView control supporting tri-state checkboxes.
    /// </summary>
    public class TriStateTreeView : TreeView
    {
        private ImageList _stateImageList;
        private bool _checkBoxesVisible;

        private const int UncheckedStateImageIndex = 0;
        private const int CheckedStateImageIndex = 1;
        private const int MixedStateImageIndex = 2;

        public TriStateTreeView()
        {
            _stateImageList = new ImageList(); // first we create our state image

            //render each state
            foreach (var checkBoxState in new[] {CheckBoxState.UncheckedNormal, CheckBoxState.CheckedNormal, CheckBoxState.MixedNormal})
            {
                var bmpCheckBox = new Bitmap(16, 16);
                using (var graphics = Graphics.FromImage(bmpCheckBox))
                    CheckBoxRenderer.DrawCheckBox(graphics, new Point(2, 2), checkBoxState); // ...rendering the checkbox and...
                _stateImageList.Images.Add(bmpCheckBox); // ...adding to sate image list.
            }
        }

        /// <summary>
        /// Gets or sets to display checkboxes in the tree view.
        /// </summary>
        [Category("Appearance")]
        [Description("Sets tree view to display checkboxes or not.")]
        [DefaultValue(false)]
        public new bool CheckBoxes
        {
            get { return _checkBoxesVisible; }
            set
            {
                _checkBoxesVisible = value;
                base.CheckBoxes = _checkBoxesVisible;
                StateImageList = _checkBoxesVisible ? _stateImageList : null;
            }
        }

		/// <summary>
		/// Gets or sets if checkbox state are cascaded 
		/// </summary>
		[Category("Behavior")]
		[Description("Sets tree view to cascade states or not.")]
		[DefaultValue(true)]
		public bool CascadeState { get; set; }

        [Browsable(false)]
        public new ImageList StateImageList
        {
            get { return base.StateImageList; }
            set { base.StateImageList = value; }
        }

        /// <summary>
        /// Refreshes this
        /// control.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            if (!CheckBoxes) // nothing to do here if
                return; // checkboxes are hidden.

            base.CheckBoxes = false; // hide normal checkboxes...

            var stack = new Stack<TreeNode>(Nodes.Count);
            foreach (TreeNode tnCurrent in Nodes) // push each root node.
                stack.Push(tnCurrent);

            while (stack.Count > 0)
            {
                // let's pop node from stack,
                TreeNode tnStacked = stack.Pop();
                if (tnStacked.StateImageIndex == -1) // index if not already done
                    tnStacked.StateImageIndex = tnStacked.Checked ? CheckedStateImageIndex : UncheckedStateImageIndex; // and push each child to stack
                for (int i = 0; i < tnStacked.Nodes.Count; i++) // too until there are no
                    stack.Push(tnStacked.Nodes[i]); // nodes left on stack.
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            Refresh();
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            base.OnAfterExpand(e);

            foreach (TreeNode tnCurrent in e.Node.Nodes) // set tree state image
                if (tnCurrent.StateImageIndex == -1) // to each child node...
                    tnCurrent.StateImageIndex = tnCurrent.Checked ? CheckedStateImageIndex : UncheckedStateImageIndex;
        }

		protected override void OnAfterCheck(TreeViewEventArgs e)
		{
			var state = e.Node.Checked ? (CascadeState && e.Node.Nodes.Count > 0 ? GetDesendantsState(e.Node) : CheckState.Checked) : CheckState.Unchecked;
			SetNodeState(e.Node, state);

			base.OnAfterCheck(e);

			if (_autoStateStack.Count == 0 || _autoStateStack.Peek() != e.Node)
			{
				if (CascadeState)
					RecurseDownSetState(e.Node, e.Node.Checked);
				SetParentState(e.Node);
			}
		}

        Stack<TreeNode> _autoStateStack = new Stack<TreeNode>();

        /// <summary>
        /// Recursively set the state of nodes upward with the aggregated the state of children
        /// </summary>
        /// <param name="node">The start node.</param>
        private void SetParentState(TreeNode node)
        {
            while (node != null)
            {
				if (CascadeState || GetNodeState(node) != CheckState.Unchecked)
					if (node.Nodes.Count > 0)
					{
						var state = GetNodeState(node.Nodes[0]);
						if (CascadeState || state == CheckState.Checked)
						{
							for (int i = 1; i < node.Nodes.Count; i++)
								if (GetNodeState(node.Nodes[i]) != state || GetNodeState(node.Nodes[i]) == CheckState.Indeterminate)
								{
									//set Indeterminate until the root
									while (node != null)
									{
										SetNodeState(node, CheckState.Indeterminate);
										node = node.Parent;
									}
									return;
								}

							if (GetNodeState(node) != state)
								SetNodeChecked(node, state == CheckState.Checked);
						}
						else
							SetNodeState(node, CheckState.Indeterminate);

					}

                node = node.Parent;
            }
        }

        private void SetNodeChecked(TreeNode node, bool @checked)
        {
            _autoStateStack.Push(node);
            node.Checked = @checked;
            var pop = _autoStateStack.Pop();
            Debug.Assert(node == pop);
        }

        private void RecurseDownSetState(TreeNode node, bool checkedState)
        {
            foreach (TreeNode child in node.Nodes)
            {
                if (child.Checked != checkedState)
                    SetNodeChecked(child, checkedState);

                RecurseDownSetState(child, checkedState);
            }
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            
            int spacing = ImageList == null ? 0 : 18;
            if ((e.X > e.Node.Bounds.Left - spacing || // *not* used by the state
                 e.X < e.Node.Bounds.Left - (spacing + 16)) && // image we can leave here.
                e.Button != MouseButtons.None) 
                return;

            var bufferNode = e.Node;
            if (e.Button == MouseButtons.Left)								// flip its check state.
                bufferNode.Checked = !bufferNode.Checked;
        }

        /// <summary>
        /// Gets the state of the node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static CheckState GetNodeState(TreeNode node)
        {
            switch (node.StateImageIndex)
            {
                case CheckedStateImageIndex:
                    return CheckState.Checked;
                case MixedStateImageIndex:
                    return CheckState.Indeterminate;
                default:
                    return CheckState.Unchecked;
            }
        }

		private static CheckState GetDesendantsState(TreeNode node)
		{
			var count = node.Nodes.Count;
			if (count == 0)
				return GetNodeState(node);

			var firstState = GetNodeState(node.Nodes[0]);

			if (firstState == CheckState.Indeterminate)
				return firstState;

			for (int i = 1; i < count; i++)
			{
				var state = GetNodeState(node.Nodes[i]);
				if (state == CheckState.Indeterminate)
					return state;
				if (state != firstState)
					return CheckState.Indeterminate;
			}

			return firstState;
		}

			
        /// <summary>
        /// Sets the state of the node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="checkState">State of the check.</param>
        private static void SetNodeState(TreeNode node, CheckState checkState)
        {
            switch (checkState)
            {
                case CheckState.Unchecked:
                    node.StateImageIndex = UncheckedStateImageIndex;
                    break;
                case CheckState.Checked:
                    node.StateImageIndex = CheckedStateImageIndex;
                    break;
                case CheckState.Indeterminate:
                    node.StateImageIndex = MixedStateImageIndex;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("checkState");
            }
        }
    }
}
