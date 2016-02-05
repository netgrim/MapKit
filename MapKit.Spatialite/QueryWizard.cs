using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;
using MapKit.Core;
using Cyrez.SqliteUtil;

namespace MapKit.Spatialite
{
	public partial class QueryWizard : Form
	{
		public QueryWizard()
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

	    private SQLiteConnection _connection;

		public SQLiteConnection Connection
		{
			get { return _connection; }
			set
			{
				_connection = value;
				FillSpatialTable();
			}
		}

		private void FillSpatialTable()
		{
			cboTable.BeginUpdate();
			cboTable.Items.Clear();
			foreach (var table in SpatialiteUtil.GetSpatialTable(_connection).ToArray().OrderBy(x => x))
				cboTable.Items.Add(table);
			cboTable.EndUpdate();

			if (cboTable.Items.Count > 0)
				cboTable.SelectedIndex = 0;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			Close();
		}


		private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillColumn(Table);
		}

		private void FillColumn(string table)
		{
			cboColumn.BeginUpdate();
			cboColumn.Items.Clear();
			foreach (var column in SpatialiteUtil.GetSpatialColumns(_connection, table).ToArray().OrderBy(x => x))
				cboColumn.Items.Add(column);
			cboColumn.EndUpdate();

			if (cboColumn.Items.Count > 0)
				cboColumn.SelectedIndex = 0;
			cboColumn.Enabled = cboColumn.Items.Count > 1;
		}

		private void cboColumn_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckSpatialIndex();
		}

		private void CheckSpatialIndex()
		{
			var info = SpatialiteUtil.GetColumnInfo(_connection, Table, Column);
			if (info == null)
			{
				cmdCreateIndex.Enabled =
				cmdDropIndex.Enabled = false;
				lblIndexStatus.Text = "Invalid";
				return;
			}

			cmdCreateIndex.Enabled =  info.SpatialIndex ==  SpatialIndexMode.None;
			cmdDropIndex.Enabled =  !cmdCreateIndex.Enabled;
			
			lblIndexStatus.Text = "Status: " + GetIndexText(info.SpatialIndex);
		}

		private string GetIndexText(SpatialIndexMode spatialIndexMode)
		{
			switch (spatialIndexMode)
			{
				case SpatialIndexMode.None:
				case SpatialIndexMode.RTree:
				case SpatialIndexMode.MBRCache:
					return spatialIndexMode.ToString();
				default:
					throw new InvalidEnumArgumentException();
			}
		}

		public string Table { get { return (string)cboTable.SelectedItem; } }
		public string Column { get { return (string)cboColumn.SelectedItem; } }

		private void cmdCreateIndex_Click(object sender, EventArgs e)
		{
			if (SpatialiteUtil.CreateSpatialIndex(_connection, Table, Column))
				MessageBox.Show("Index created.");
			else
				MessageBox.Show("Index creation failed.");

			CheckSpatialIndex();
		}

		private void cmdDropIndex_Click(object sender, EventArgs e)
		{
			if (SpatialiteUtil.DisableSpatialIndex(_connection, Table, Column))
				MessageBox.Show("Index dropped.");
			else
				MessageBox.Show("Index drop failed.");

			CheckSpatialIndex();
		}
	}
}
