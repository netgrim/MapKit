using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Cyrez.SqliteUtil;

namespace MapKit.Spatialite
{
    public partial class NewSpatialiteLayerDialog : Form
    {
        const int colColumnNameIdx = 0;
        const int colGeometryTypeIdx = 1;
        const int colSridIdx = 2;
        const int colIndexIdx = 3;

        public NewSpatialiteLayerDialog()
        {
            InitializeComponent();
            FillConnections();
        }

        public IEnumerable<SpatialColumnInfo> SelectedSpatialColumns
        {
            get
            {
                foreach (ListViewItem item in lvwTables.SelectedItems)
                    yield return (SpatialColumnInfo)item.Tag;
            }
        }

        public string File
        {
            get { return (string)comboBox.SelectedItem; }
        }

        private void FillConnections()
        {
            comboBox.BeginUpdate();
            comboBox.Items.Clear();

            foreach (var filename in ConnectionManager.Default.Filenames)
                comboBox.Items.Add(filename);

            comboBox.EndUpdate();
        }
        
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    if (!comboBox.Items.Contains(openFileDialog.FileName))
                        comboBox.Items.Add(openFileDialog.FileName);

                    comboBox.SelectedItem = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                FillList((string)comboBox.SelectedItem);
            }            
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        private void FillList(string filename)
        {
            var connection = ConnectionManager.Default.GetConnection(filename);

            lvwTables.BeginUpdate();
            try
            {
                lvwTables.Items.Clear();
                foreach (var tableName in SqliteUtil.GetTableNames(connection))
                    foreach (var columnName in SpatialiteUtil.GetSpatialColumns(connection, tableName))
                        AddRow(SpatialiteUtil.GetColumnInfo(connection, tableName, columnName));
            }
            finally
            {
                lvwTables.EndUpdate();
            }
            //connection.Close();
        }

        private void AddRow(SpatialColumnInfo spatialColumnInfo)
        {
            var item = lvwTables.Items.Add(spatialColumnInfo.TableName);
            item.SubItems.Add(spatialColumnInfo.ColumnName);
            item.SubItems.Add(spatialColumnInfo.Type);
            item.SubItems.Add(spatialColumnInfo.SRID.ToString());
            item.SubItems.Add(spatialColumnInfo.SpatialIndex.ToString());
            item.Tag = spatialColumnInfo;
        }

        private void lvwTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdOK.Enabled = lvwTables.SelectedItems.Count > 0;
        }

        private void lvwTables_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }


    }
}
