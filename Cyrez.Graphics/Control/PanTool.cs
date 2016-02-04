namespace Cyrez.Graphics.Control
{
	public class PanTool: DragTool
	{
		public override void PointerMove(ToolEventArgs e)
		{
			if (IsDragging) 
				InternalMove(e, WindowChangedEventType.Pan);
			base.PointerMove(e);
		}

		private void InternalMove(ToolEventArgs e, WindowChangedEventType type)
		{
			// Get the start and end points of the drag.

			// Keep track of the displacement for optimization purposes.
			//var panDelta = p2 - p1; ;

			// Move the view and redraw.
			var delta = e.LocationWCS - Viewport.ClientToWorld(LastPosition);
			Viewport.TranslatePrepend(delta.X, delta.Y);

			Viewport.PerformWindowChanged(type);
			Viewport.Invalidate();
		}

		protected override void OnDragStart(ToolEventArgs e)
		{
			InternalMove(e, WindowChangedEventType.PanStart);
			base.OnDragStart(e);
		}

		public override void PointerUp(ToolEventArgs e)
		{
            if (IsDragging)
                InternalMove(e, WindowChangedEventType.PanEnd);
            else if (e.Clicks > 1)
                Viewport.ZoomExtent();
            else
                Viewport.CenterView(LastDownWCS);

			base.PointerUp(e);

		}
	}
}
