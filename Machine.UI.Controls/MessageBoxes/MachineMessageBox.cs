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
    public partial class MachineMessageBox : Form
    {
        public const int ButtonSize = 95;

        public static DialogResult Show(string title, string message)
        {
            var msgBox = new MachineMessageBox(title, message);
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
                    slTitle.Text = title;
                    slTitle.Update();
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
                    slMessage.Text = message;
                    slMessage.Update();
                    Update();
                }
            }
        }

        public MachineMessageBox(string title, string message)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;

            Text = Message;
        }

        public MachineMessageBox(string title, string message, Boolean noCanc)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;

            Text = Message;
            sbCancel.Visible = false;
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

            slTitle.ForeColor = Color.FromArgb(153, 204, 0);
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
