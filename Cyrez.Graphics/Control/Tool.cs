using System;
using System.Windows.Forms;
using System.Drawing;
using WinPoint = System.Windows.Point;

namespace Cyrez.Graphics.Control
{
	public class Tool
	{
		private Point _lastDown;
		private WinPoint _lastDownWCS;
	
		private Point _lastPosition;
		private WinPoint _lastPositionWCS;

		public virtual void PointerDown(ToolEventArgs e)
		{
			_lastDown = _lastPosition = e.Location;
			_lastPositionWCS = _lastDownWCS = e.LocationWCS;
			IsDown = true;
		}

		public virtual void PointerMove(ToolEventArgs e)
		{
			_lastPosition = e.Location;
			_lastPositionWCS = e.LocationWCS;
		}

		public virtual void PointerUp(ToolEventArgs e)
		{
			_lastPosition = e.Location;
			_lastPositionWCS = e.LocationWCS;
			IsDown = false;
		}

		public virtual void KeyDown(KeyEventArgs e){}
		public virtual void KeyUp(KeyEventArgs e) { }
		public virtual void VScroll(MouseEventArgs e) { }

		public Tool ParentTool { get; set; }

		protected Point LastDown
		{
			get{return _lastDown;}
		}

		protected Point LastPosition
		{
			get{return _lastPosition;}
		}
		
		protected WinPoint LastDownWCS
		{
			get{return _lastDownWCS;}
		}

		protected WinPoint LastPositionWCS
		{
			get{return _lastPositionWCS;}
		}

		internal void Activate(Viewport viewport)
		{
			if (Viewport != null)
				throw new InvalidOperationException("already in use");
			Viewport = viewport;
			OnActivated(viewport);
		}

		protected virtual void OnActivated(Viewport viewport) { }

		public bool IsDown { get; private set; }

		

		internal void Deactivate()
		{
			if (Viewport == null)
				throw new InvalidOperationException("Inactive");
			OnDeactivate();
			Viewport = null;
		}

		protected virtual void OnDeactivate() { }

		public Viewport Viewport { get; private set; }

		public virtual void Paint(PaintEventArgs e) { }
	}
}
