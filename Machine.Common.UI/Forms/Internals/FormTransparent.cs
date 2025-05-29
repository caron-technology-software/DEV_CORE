using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Common
{
    public partial class FormTransparent : FormMachineBase
    {
        public FormTransparent()
        {
            InitializeComponent();
        }

        private void FormTransparent_Load(object sender, EventArgs e)
        {
            TopMost = false;
        }

        private void FormTransparent_Click(object sender, EventArgs e)
        {
            //SendToBack();
        }

        protected override void UpdateUIForm()
        {
            //--
        }


    }
}
