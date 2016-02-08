using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapKit.UI
{
    public partial class TraceDialog : Form
    {

        public event EventHandler StartClicked;
        public event EventHandler NextClicked;

        public TraceDialog()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            OnStart(EventArgs.Empty);
        }

        private void OnStart(EventArgs e)
        {
            if (StartClicked != null)
                StartClicked(this, e);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            OnNextClicked(EventArgs.Empty);
        }

        private void OnNextClicked(EventArgs e)
        {
            if (NextClicked != null)
                NextClicked(this, e);
        }

        public int StartNodeId 
        {
            get
            {
                int startNodeId;
                int.TryParse(txtStartNode.Text, out startNodeId);
                return startNodeId;
            }
            set
            {
                txtStartNode.Text = value.ToString();
            }
        }

        private void txtStartNode_Validated(object sender, EventArgs e)
        {
            int startNodeId;
            bool valid = StartButton.Enabled = int.TryParse(txtStartNode.Text, out startNodeId);

            errorProvider1.SetError(txtStartNode, valid ? string.Empty : "Should be numeric");
        }
        
        public void EnableStart(bool enabled)
        {
            StartButton.Enabled = enabled;
        }

        public void EnableNext(bool enabled)
        {
            NextButton.Enabled = enabled;
        }
    }
}
