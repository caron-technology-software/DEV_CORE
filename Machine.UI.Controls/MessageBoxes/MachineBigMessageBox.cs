using FontAwesome.Sharp;
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
    public partial class MachineBigMessageBox : Form
    {
        public const int ButtonSize = 95;

        public static DialogResult Show(string title, string message)
        {
            var msgBox = new MachineBigMessageBox(title, message);
            var dialogResult = msgBox.ShowDialog();

            return dialogResult;
        }

        //153;204;0 color green

        private string title;
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                if (title != value)
                {
                    title = value;
                    mlTitle.Text = title;
                    mlTitle.Update();
                    Update();
                }
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                if (message != value)
                {
                    message = value;
                    rtbMessage.Text = message;
                    rtbMessage.Update();
                    Update();
                    //rtbMessage.Enabled = true;
                    //rtbMessage.Enabled = false;
                }
            }
        }

        public MachineBigMessageBox(string title, string message)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;

            Text = Message;
        }

        private void MachineMessageBox_Load(object sender, EventArgs e)
        {
            sbOk.StateChangeActivated = false;
            sbOk.InactiveBackgroundImage = IconChar.CheckCircle.ToBitmap(Color.Gray, ButtonSize);
            sbOk.ActiveBackgroundImage = IconChar.CheckCircle.ToBitmap(Color.DarkGray, ButtonSize);

            sbOk.StateChangeActivated = false;
            sbCancel.InactiveBackgroundImage = IconChar.TimesCircle.ToBitmap(Color.Gray, ButtonSize);
            sbCancel.ActiveBackgroundImage = IconChar.TimesCircle.ToBitmap(Color.DarkGray, ButtonSize);

            //Per transparenza Form
            BackColor = Color.FromArgb(221, 221, 221);
            TransparencyKey = Color.FromArgb(221, 221, 221);

            mlTitle.ForeColor = Color.FromArgb(153, 204, 0);
            rtbMessage.BackColor = Color.FromArgb(25, 36, 43);
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            sbOk.PulseButton(300);
            DialogResult = DialogResult.OK;
        }

        private void sbCancel_MouseClick(object sender, MouseEventArgs e)
        {
            sbCancel.PulseButton(300);
            DialogResult = DialogResult.Cancel;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                BringToFront();
                TopMost = true;
            }
        }
    }
}
