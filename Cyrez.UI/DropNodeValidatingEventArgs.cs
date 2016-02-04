using System;
using System.Windows.Forms;
using System.Drawing;

namespace Cyrez.UI
{
	public class DropNodeValidatingEventArgs : DragEventArgs
	{
		public DropNodeValidatingEventArgs(IDataObject data, int keyState, int x, int y, DragDropEffects allowedEffects, DragDropEffects effect, TreeNode sourceNode, TreeNode targetNode, NodePosition position)
			:base (data, keyState, x, y,  allowedEffects, effect)
		{
			SourceNode = sourceNode;
			TargetNode = targetNode;
			Position = position;
		}


		public TreeNode SourceNode { get; set; }

		public TreeNode TargetNode { get; set; }

		public NodePosition Position { get; set; }
	}
}
