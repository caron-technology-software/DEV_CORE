using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineBigMessageBoxFullScreen : Form
    {
        public string Message { get; private set; }
        public string Title { get; private set; }

        private MachineBigMessageBox msgBox;

        public MachineBigMessageBoxFullScreen(string title, string message)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;
        }

        private void MachineMessageBoxFullScreen_Load(object sender, EventArgs e)
        {
            msgBox = new MachineBigMessageBox(Title, Message)
            {
                TopLevel = true,
                TopMost = true
            };

            this?.Invoke((MethodInvoker)delegate ()
            {
                msgBox.ShowDialog();
            });

            Visible = false;

            DialogResult = msgBox.DialogResult;
        }

        public new void Close()
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                msgBox.Close();
            });
        }
    }
}
