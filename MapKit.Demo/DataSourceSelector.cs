using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapKit.Core;

namespace MapKit.Demo
{
    public partial class DataSourceSelector : Form
    {
        private List<DataSourceType> _dataSources;
        public DataSourceSelector()
        {
            InitializeComponent();
        }

        public List<DataSourceType> DataSources {
            get
            {
                return _dataSources;
            }
            set
            {
                _dataSources = value;
                listView.Items.Clear();
                foreach (var source in _dataSources)
                    AddItem(source);
                
                if (listView.Items.Count > 0)
                    listView.SelectedIndices.Add(0);
            }
        }

        private void AddItem(DataSourceType source)
        {
            var item = listView.Items.Add(source.Name);
            item.Tag = source;
        }

        public DataSourceType SelectedDataSource
        {
            get
            {
                return listView.SelectedItems.Count > 0
                    ? listView.SelectedItems[0].Tag as DataSourceType
                    : null;
            }
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedDataSource != null)
                OkButton.PerformClick();
        }
    }
}
