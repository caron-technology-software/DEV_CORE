using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineMessageInfo : Form
    {
        public const int ButtonSize = 95;

        private int NumberOfClicks { get; set; }
        private int currentNumberOfClicks = 0;

        private volatile bool isRunning = true;
        public bool IsRunning { get => isRunning; private set => isRunning = value; }

        public void Show(string title, string message, Func<bool> checker, TimeSpan intervalCheck, Action clicksExecuter = null)
        {
            var msgBox = new MachineMessageInfo(title, message);

            msgBox.Show();
        }

        public static DialogResult ShowDialog(string title, string message, Func<bool> checker, TimeSpan intervalCheck, Action clicksExecuter = null)
        {
            var msgBox = new MachineMessageInfo(title, message);

            return msgBox.ShowDialog();
        }

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

        public MachineMessageInfo(string title, string message, int numberOfClicks = 1)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;

            this.NumberOfClicks = numberOfClicks;

            //panelExit.BackColor = Color.FromArgb(25, 36, 43);
            //panelExit.BackgroundImage = IconChar.PowerOff.ToBitmap(Color.WhiteSmoke);
            //panelExit.BackgroundImageLayout = ImageLayout.Center;
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            currentNumberOfClicks++;

            if (currentNumberOfClicks >= NumberOfClicks)
            {
                Close();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            currentNumberOfClicks++;

            if (currentNumberOfClicks >= NumberOfClicks)
            {
                Close();
            }
        }

        private void FormMachineMessageDialog_Load(object sender, EventArgs e)
        {
            //Per transparenza Form
            BackColor = Color.FromArgb(221, 221, 221);
            TransparencyKey = Color.FromArgb(221, 221, 221);

            slTitle.ForeColor = Color.FromArgb(153, 204, 0);

            TopMost = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsRunning == false)
            {
                Close();
            }
        }

        private void SlTitle_Click(object sender, EventArgs e)
        {
            currentNumberOfClicks++;

            if (currentNumberOfClicks >= NumberOfClicks)
            {
                Close();
            }
        }

        private void SlMessage_Click(object sender, EventArgs e)
        {
            currentNumberOfClicks++;

            if (currentNumberOfClicks >= NumberOfClicks)
            {
                Close();
            }
        }
    }
}
