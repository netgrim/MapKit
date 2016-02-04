using System.Windows.Forms;

namespace Cyrez.Graphics.Control
{
	public class ToolEventArgs : MouseEventArgs
	{

		public ToolEventArgs(MouseEventArgs e, System.Windows.Point locationWCS)
			 : base (e.Button, e.Clicks, e.X, e.Y, e.Delta)
		{
			LocationWCS = locationWCS;
		}

		public System.Windows.Point LocationWCS { get; private set; }

	}
}
