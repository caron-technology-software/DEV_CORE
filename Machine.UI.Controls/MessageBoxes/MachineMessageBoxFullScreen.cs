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
    public partial class MachineMessageBoxFullScreen : Form
    {
        public string Message { get; private set; }
        public string Title { get; private set; }

        private MachineMessageBox msgBox;

        private Boolean noCanc=false;

        public MachineMessageBoxFullScreen(string title, string message)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;
            noCanc = false;
        }
        public MachineMessageBoxFullScreen(string title, string message,Boolean noCancel)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;
            noCanc = noCancel;
        }
        private void MachineMessageBoxFullScreen_Load(object sender, EventArgs e)
        {
            //msgBox = new MachineMessageBox(Title, Message)
            //{
            //    TopLevel = true,
            //    TopMost = true
            //};
            if (!noCanc)
            {
                msgBox = new MachineMessageBox(Title, Message)
                {
                    TopLevel = true,
                    TopMost = true
                };
            }
            else
            {
                msgBox = new MachineMessageBox(Title, Message, false)
                {
                    TopLevel = true,
                    TopMost = true
                };
            }

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
