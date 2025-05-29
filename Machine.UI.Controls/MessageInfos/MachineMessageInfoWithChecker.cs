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
    public partial class MachineMessageInfoWithChecker : Form
    {
        public const int ButtonSize = 95;

        private Thread threadChecker;

        private Func<bool> checker;

        private int NumberOfClicks { get; set; }
        private int currentNumberOfClicks = 0;
        private Action actionClicksExecuter;

        private volatile bool isRunning = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRunning { get => isRunning; private set => isRunning = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan IntervalCheck { get; private set; }

        public void Show(string title, string message, Func<bool> checker, TimeSpan intervalCheck, Action clicksExecuter = null)
        {
            var msgBox = new MachineMessageInfoWithChecker(title, message, checker, intervalCheck, clicksExecuter);

            msgBox.Show();
        }

        public static DialogResult ShowDialog(string title, string message, Func<bool> checker, TimeSpan intervalCheck, Action clicksExecuter = null)
        {
            var msgBox = new MachineMessageInfoWithChecker(title, message, checker, intervalCheck, clicksExecuter);

            return msgBox.ShowDialog();
        }

        private string title;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        private System.Windows.Forms.Timer timerFormCloser;

        private void ExecuteCheck()
        {
            while (checker() == false)
            {
                Thread.Sleep(IntervalCheck);
            }

            IsRunning = false;
        }

        public MachineMessageInfoWithChecker(string title, string message, Func<bool> checker = null, TimeSpan? intervalCheck = null, Action actionClicksExecuter = null, int numberOfClicks = 1)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Title = title;
            Message = message;

            this.checker = checker;
            IntervalCheck = intervalCheck ?? TimeSpan.FromMilliseconds(100);

            this.actionClicksExecuter = actionClicksExecuter;

            this.NumberOfClicks = numberOfClicks;

            //panelExit.BackColor = Color.FromArgb(25, 36, 43);
            //panelExit.BackgroundImage = IconChar.PowerOff.ToBitmap(Color.WhiteSmoke);
            //panelExit.BackgroundImageLayout = ImageLayout.Center;
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            currentNumberOfClicks++;

            if (actionClicksExecuter != null && currentNumberOfClicks >= NumberOfClicks)
            {
                actionClicksExecuter();
                Close();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            currentNumberOfClicks++;

            if (actionClicksExecuter != null && currentNumberOfClicks >= NumberOfClicks)
            {
                actionClicksExecuter();
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

            timerFormCloser = new System.Windows.Forms.Timer();
            timerFormCloser.Interval = (int)IntervalCheck.TotalMilliseconds;
            timerFormCloser.Tick += Timer_Tick;

            timerFormCloser.Start();

            if (checker != null)
            {
                threadChecker = new Thread(new ThreadStart(ExecuteCheck));
                threadChecker.Start();
            }
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

            if (actionClicksExecuter != null && currentNumberOfClicks >= NumberOfClicks)
            {
                actionClicksExecuter();
                Close();
            }
        }

        private void SlMessage_Click(object sender, EventArgs e)
        {
            currentNumberOfClicks++;

            if (actionClicksExecuter != null && currentNumberOfClicks >= NumberOfClicks)
            {
                actionClicksExecuter();
                Close();
            }
        }
    }
}
