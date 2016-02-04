using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cyrez.Graphics.Control
{
	public class RectangleTool : DragTool
	{

		public event EventHandler DragRectangleComplete;
		protected virtual void OnComplete()
		{
			if (DragRectangleComplete != null)
				DragRectangleComplete(this, EventArgs.Empty);
		}

		public event EventHandler DragRectangleBegin;
		protected virtual void OnBegin()
		{
			if (DragRectangleBegin != null)
				DragRectangleBegin(this, EventArgs.Empty);
		}

		public event EventHandler DragRectangleChanged;
		protected virtual void OnChanged()
		{
			if (DragRectangleChanged != null)
				DragRectangleChanged(this, EventArgs.Empty);
		}

		private Rectangle _dragRectangle;
		
		public Rectangle DragRectangle
		{
			get { return _dragRectangle; }
			set { _dragRectangle = value; }
		}

		public override void Paint(PaintEventArgs e)
		{
			if (!_dragRectangle.IsEmpty)
			{
				e.Graphics.ResetTransform();
				var rect = FixRectangle(_dragRectangle);
				rect.Size -= new Size(1, 1);
				e.Graphics.DrawRectangle(Pens.LightBlue, rect);
				using (var brush = new SolidBrush(Color.FromArgb(100, Color.RoyalBlue)))
					e.Graphics.FillRectangle(brush, rect);
			}

			base.Paint(e);
		}

		public override void PointerUp(ToolEventArgs e)
		{
			if (!_dragRectangle.IsEmpty)
			{
				OnComplete();


				Viewport.Invalidate(FixRectangle(_dragRectangle));
				_dragRectangle = Rectangle.Empty;
			}
			base.PointerUp(e);
		}

		public override void PointerMove(ToolEventArgs e)
		{
			if (IsDragging)
				InternalPointerMove(new Point(e.X,e.Y));
			base.PointerMove(e);
		}

		private void InternalPointerMove(Point p)
		{
			if (_dragRectangle.IsEmpty)
				OnBegin();
			else
			{
				Viewport.Invalidate(FixRectangle(_dragRectangle));
				OnChanged();
			}

			_dragRectangle = new Rectangle(LastDown, (Size)p - (Size)LastDown);
			Viewport.Invalidate(FixRectangle(_dragRectangle));

			//if (m_dragPoint == null)
			//    return;

			//Point2D pt = Device.ClientToWorld(new Point(e.X, e.Y));

			//if (_selectionBox != null)
			//    _frontBuffer.Shapes.Remove(_selectionBox);

			//_selectionBox = new CompoundPolygonShape();
			//_selectionBox.Color = Color.FromArgb(100, Color.DeepSkyBlue);
			//_selectionBox.Polygon.AddVertex(new Point2D(_dragPoint.X, _dragPoint.Y));
			//_selectionBox.Polygon.AddVertex(new Point2D(_dragPoint.X, pt.Y));
			//_selectionBox.Polygon.AddVertex(new Point2D(pt.X, pt.Y));
			//_selectionBox.Polygon.AddVertex(new Point2D(pt.X, _dragPoint.Y));
			//_selectionBox.Polygon.AddVertex(new Point2D(_dragPoint.X, _dragPoint.Y));

			//_frontBuffer.Shapes.Add(_selectionBox);
			//_fullRedraw = true;
			//this.Invalidate();
		}

		protected override void OnDragStart(ToolEventArgs e)
		{
			InternalPointerMove(new Point(e.X, e.Y));
			base.OnDragStart(e);
		}


		private Rectangle FixRectangle(Rectangle r)
		{
			if (r.Width < 0) r.X += r.Width;
			if (r.Height < 0) r.Y += r.Height;
			r.Width = Math.Abs(r.Width);
			r.Height = Math.Abs(r.Height);
			return r;
		}
	}
}
