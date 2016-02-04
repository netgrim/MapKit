using System;
using System.Windows.Forms;

namespace MapKit.UI
{
	public partial class QueryDialog : Form
	{
		public QueryDialog()
		{
			InitializeComponent();
		}

		public string SQL
		{
			get { return txtSql.Text; }
			set { txtSql.Text = value; }
		}

		public bool QueryEnabled
		{
			get { return chkEnabled.Checked; }
			set { chkEnabled.Checked = value; }
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
