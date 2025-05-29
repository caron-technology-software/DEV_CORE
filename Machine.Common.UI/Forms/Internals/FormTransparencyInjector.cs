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
    public partial class FormTransparencyInjector
    {
        public FormTransparencyInjector()
        {
            InitializeComponent();
        }

        public void Show(FormMachineBase form)
        {
            this.Show();

            form.StartPosition = FormStartPosition.CenterScreen;

            form.ShowDialog();
            form.Refresh();
#if DEBUG
            TopLevel = false;
#endif
            this.Close();
        }

        protected override void UpdateUIForm()
        {
            //--
        }
    }
}