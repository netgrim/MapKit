using System.Windows.Forms;
using MapKit.Core;

namespace MapKit.UI
{
	public partial class ThemeEditor : UserControl
	{
		public ThemeEditor()
		{
			InitializeComponent();
		}

		public Map Map
		{
			get { return themeEditorComponent.Map; }
			set { themeEditorComponent.Map = value; }
		}
	}
}