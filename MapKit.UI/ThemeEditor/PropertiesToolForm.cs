using System.Windows.Forms;

namespace MapKit.UI
{
	public partial class PropertiesToolForm : Form
	{
		public PropertiesToolForm()
		{
			InitializeComponent();
		}

		public object SelectedObject
		{
			get { return propertyGrid1.SelectedObject; }
			set { propertyGrid1.SelectedObject = value; }
		}

		public object[] SelectedObjects
		{
			get { return propertyGrid1.SelectedObjects; }
			set { propertyGrid1.SelectedObjects = value; }
		}
	}
}
