using System;
using System.Windows.Forms;

namespace Cyrez.GIS.UI
{
    internal partial class NewStyle : Form
    {
        public NewStyle()
        {
            InitializeComponent();
        }

        public string StyleName
        {
            get { return textBox.Text; }
            set{ textBox.Text = value; }
        }

        public StyleType StyleType
        {
            get { return (StyleType)cboType.SelectedValue; }
            set { cboType.SelectedValue = value; }
        }
        
        private void NewStyle_Load(object sender, EventArgs e)
        {
            cboType.DataSource = Enum.GetValues(typeof(StyleType));
        }

    }
}
