using System;
using System.Data;
using System.Windows.Forms;

namespace MapKit.UI
{
    public partial class frmExpressionEditor : Form
    {
        public frmExpressionEditor()
        {
            InitializeComponent();
        }


        private DataTable _table;
        public DataTable DataTable
        {
            get
            {
                return _table;
            }
            set
            {
                _table = value;
                LoadFields(_table);
                LoadSelect(_table);
            }
        }

        private void LoadSelect(DataTable table)
        {
            lblSelect.Text = "SELECT * FROM [" + table.TableName + "] WHERE";
        }

        private void LoadFields(DataTable table)
        {
            if (table == null) throw new ArgumentNullException("table");
            lstFields.BeginUpdate();
            lstFields.Items.Clear();
            foreach (DataColumn column in table.Columns)
                lstFields.Items.Add(column.ColumnName);

            lstFields.EndUpdate();

            if (lstFields.Items.Count > 0)
                lstFields.SelectedIndex = 0;
        }

 

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            txtExpression.SelectedText = ((Button)sender).Text + " ";
        }

        private void cmdParentisis_Click(object sender, EventArgs e)
        {
            txtExpression.SelectedText = "(" + txtExpression.SelectedText + ") ";
        }

        private void cmdGetUniqueValues_Click(object sender, EventArgs e)
        {
            if (lstFields.SelectedIndex != -1)
                LoadUniqueValues(lstFields.SelectedItem.ToString());
        }

        private void LoadUniqueValues(string field)
        {
            lstUniqueValues.BeginUpdate();
            lstUniqueValues.Items.Clear();
            foreach (DataRow row in new DataView(_table).ToTable(true, new[] { field }).Rows)
            {
                object value = row[field];
                if (value is string)
                    lstUniqueValues.Items.Add("'" + value.ToString().Replace("'", "''") + "'");
                else
                    lstUniqueValues.Items.Add(value);
            }
            lstUniqueValues.EndUpdate();
                           
        }

        private void list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var list = (ListBox)sender;
            if (list.SelectedIndex != -1)
                txtExpression.SelectedText = list.SelectedItem + " ";
        }

        
        private void LoadMinMax(string p)
        {
            //todo p unused
            txtMinimum.Text = "";
            txtMaximum.Text = "";
        }

        private void lstFields_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstFields.SelectedIndex != -1)
                LoadMinMax(lstFields.SelectedItem.ToString());
            lstUniqueValues.Items.Clear();
        }

    }
}
