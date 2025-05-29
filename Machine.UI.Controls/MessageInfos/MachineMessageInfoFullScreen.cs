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
    public partial class MachineMessageInfoFullScreen : Form
    {
        public string Message { get; private set; }
        public string Title { get; private set; }
        public int NumberOfClicks { get; private set; }

        private MachineMessageInfo msgDialog;

        public MachineMessageInfoFullScreen(string title, string message, int numberOfClicks = 0)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;

            NumberOfClicks = numberOfClicks;
        }

        private void FormMachineMessageDialogFullScreen_Load(object sender, EventArgs e)
        {
            msgDialog = new MachineMessageInfo(Title, Message, NumberOfClicks)
            {
#if !TEST
                TopLevel = true,
                TopMost = true
#endif
            };

            this?.Invoke((MethodInvoker)delegate ()
            {
                var mb = msgDialog.ShowDialog();
            });

            Visible = false;

            DialogResult = msgDialog.DialogResult;
        }

        public new void Close()
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                msgDialog.Close();
            });

            base.Close();
        }
    }
}
