using System;
using System.Windows.Forms;
using System.Windows.Media;

namespace MapKit.Demo
{
    public partial class MatrixWindow : Form
    {
        private Matrix _matrix;

        public event EventHandler MatrixChanged;

        public MatrixWindow()
        {
            InitializeComponent();
        }

        public Matrix Matrix
        {
            get { return _matrix; }
            set
            {
                _matrix = value;
                lblM00.Text = value.M11.ToString("N");
                lblM01.Text = value.M12.ToString("N");
                lblM10.Text = value.M21.ToString("N");
                lblM11.Text = value.M22.ToString("N");
                lblM02.Text = value.OffsetX.ToString("N");
                lblM12.Text = value.OffsetY.ToString("N");
            }
        }

        private void lblM00_Validated(object sender, System.EventArgs e)
        {
            double m11, m12, m21, m22, offsetX, offsetY;
            if (double.TryParse(lblM00.Text, out m11) &&
                double.TryParse(lblM01.Text, out m12) &&
                double.TryParse(lblM10.Text, out m21) &&
                double.TryParse(lblM11.Text, out m22) &&
                double.TryParse(lblM02.Text, out offsetX) &&
                double.TryParse(lblM12.Text, out offsetY)
                )
            {
                  _matrix = new Matrix(m11, m12, m21, m22, offsetX, offsetY);
                if (MatrixChanged != null)
                    MatrixChanged(this, EventArgs.Empty);
            }
            else
                Matrix = _matrix;
        }
    }
}
