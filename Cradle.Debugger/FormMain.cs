using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cradle.Debugger
{
    public partial class FormMain : Form
    {
        public const float MaxAbsScalingFactor = 5.0f;

        public float ScalingFactor { get; private set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            UpdateModel();
            UpdateUI();

            Communicator.SetScalingFactor(ScalingFactor);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateModel();
            UpdateUI();
        }

        private void UpdateModel()
        {
            ScalingFactor = (float)trackBar.Value / 1000.0f * MaxAbsScalingFactor;
        }

        private void UpdateUI()
        {
            labelScalingFactor.Text = $"Scaling factor: {ScalingFactor.ToString("0.00").Replace(',', '.')}";
        }
    }
}
