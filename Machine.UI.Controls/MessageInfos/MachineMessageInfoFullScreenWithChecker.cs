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
    public partial class MachineMessageInfoFullScreenWithChecker : Form
    {
        public string Message { get; private set; }
        public string Title { get; private set; }

        public TimeSpan IntervalCheck { get; private set; }
        public Func<bool> Checker { get; private set; }
        public Action ActionClicksExecuter { get; private set; }

        private MachineMessageInfoWithChecker msgDialog;

        public MachineMessageInfoFullScreenWithChecker(string title, string message, Func<bool> checker = null, TimeSpan? intervalCheck = null, Action actionClicksExecuter = null)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;

            Checker = checker;
            IntervalCheck = intervalCheck ?? TimeSpan.FromMilliseconds(100);
            ActionClicksExecuter = actionClicksExecuter;
        }

        private void FormMachineMessageDialogFullScreen_Load(object sender, EventArgs e)
        {
            msgDialog = new MachineMessageInfoWithChecker(Title, Message, Checker, IntervalCheck, ActionClicksExecuter)
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
