using System;
using System.Windows.Forms;
using System.Drawing;

namespace Cyrez.Graphics.Control
{
	public class DragTool : Tool
	{

		public override void PointerMove(ToolEventArgs e)
		{
			if (IsDown && !IsDragging && Dragging(LastDown, e.Location))
			{
				IsDragging = true;
				OnDragStart(e);
			}
			if (!IsDown || IsDragging)
				base.PointerMove(e);
			
		}

		public bool IsDragging { get; private  set; }

		private bool Dragging(Point p1, Point p2)
		{
			return Math.Abs(p2.X - p1.X) > SystemInformation.DragSize.Width / 2.0
				|| Math.Abs(p2.Y - p1.Y) > SystemInformation.DragSize.Height / 2.0;
		}

		public override void PointerUp(ToolEventArgs e)
		{
			if (IsDragging)
			{
				IsDragging = false;
				OnDragComplete(e);
			}

			base.PointerUp(e);
		}

		protected virtual void OnDragStart(ToolEventArgs e)
		{ }

		protected virtual void OnDragComplete(ToolEventArgs e)
		{ }
	}
}
