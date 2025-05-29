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

using Machine.UI.Controls;

namespace Machine.UI.Controls.Forms
{
    public partial class FormWait : Form
    {
        private const double DeltaPictureRotation = 1;
        private const int IterChangeText = 3;
        private const int IterChangeLabel = 10;

        public static bool ShowUptime = true;
        public static int NumberOfClicksToExit = 3;

        private int iter = 0;

        private Thread threadChecker;
        private Func<bool> checker;
        //GPIx149
        private Func<bool> checker2;
        //GPFx149

        private volatile bool isRunning = true;
        private volatile bool messageBoxVisible = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRunning { get => isRunning; private set => isRunning = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int NumberOfClicksDone { get; private set; } = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan IntervalCheck { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan FinalDelay { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan ShutdownMessageBoxInterval { get; private set; } = TimeSpan.Zero;

        private bool messageBoxShutdownShowed = false;

        private DateTime startTimestamp;
        public TimeSpan Uptime { get => DateTime.UtcNow - startTimestamp; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MessageBoxTitle { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MessageBoxMessage { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Build { get; set; } = "";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Picture
        {
            set
            {
                pictureBox.Image = value;
            }
        }

        private string title = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                labelTitle.Text = title;
            }
        }

        private void ExecuteCheck()
        {
            while (checker() == false && IsRunning)
            {
                Thread.Sleep(IntervalCheck);
            }

            if (checker())
            {
                DialogResult = DialogResult.OK;
                Thread.Sleep(FinalDelay);
            }

            IsRunning = false;
        }

        //GPIx149
        public FormWait(string title, string build, Func<bool> checker, TimeSpan intervalCheck, TimeSpan finalDelay, TimeSpan shutdownMessageBoxInterval, Func<bool> checker2)
        {
            InitializeComponent();

            startTimestamp = DateTime.UtcNow;

            this.Title = title;
            this.Build = build;
            this.checker = checker;
            this.checker2 = checker2;
            this.IntervalCheck = intervalCheck;
            this.FinalDelay = finalDelay;
            this.ShutdownMessageBoxInterval = shutdownMessageBoxInterval;

            DialogResult = DialogResult.None;
        }
        //GPFx149

        public FormWait(string title, string build, Func<bool> checker, TimeSpan intervalCheck, TimeSpan finalDelay, TimeSpan shutdownMessageBoxInterval)
        {
            InitializeComponent();

            startTimestamp = DateTime.UtcNow;

            this.Title = title;
            this.Build = build;
            this.checker = checker;
            this.IntervalCheck = intervalCheck;
            this.FinalDelay = finalDelay;
            this.ShutdownMessageBoxInterval = shutdownMessageBoxInterval;

            DialogResult = DialogResult.None;
        }


        private void FormWait_Load(object sender, EventArgs e)
        {
            threadChecker = new Thread(new ThreadStart(ExecuteCheck));
            threadChecker.Start();

            timerUI.Start();

            var location = pictureBox.Location;
            location.X = this.Width / 2 - pictureBox.Width / 2;

            pictureBox.Location = location;
            labelText.Text = Build;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Console.WriteLine($"CLOSING");

            Opacity = 1.0;
            for (int i = 0; i < 10; i++)
            {
                Opacity -= 0.1;
                Refresh();
                Thread.Sleep(50);
            }

            base.OnClosing(e);
        }

        private void timerUI_Tick(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                if (messageBoxVisible)
                {
                    return;
                }

                iconSpinner.Rotation += DeltaPictureRotation;

                if (iconSpinner.Rotation == 360)
                {
                    iconSpinner.Rotation = 0;
                }

                if (ShowUptime)
                {
                    this?.Invoke((MethodInvoker)delegate { labelUptime.Text = $"{Uptime.ToString(@"mm\:ss")}"; });
                }

                if ((iter % IterChangeLabel) == 0)
                {
                    this?.Invoke((MethodInvoker)delegate
                    {
                        labelTitle.Text = $"{Title}.{ new String('.', iter % IterChangeText)}";
                    });
                }

                if (Uptime > ShutdownMessageBoxInterval && messageBoxShutdownShowed == false)
                {
                    //GPIx149
                    //Thread.Sleep(500);
                    if (checker2 != null) 
                    { 
                        if (checker2() == false)  //////quanto dura questo check? verifica che l'esecuzione continua non dia altri problemi domani 29/08/2023 in test!!!
                        {
                        messageBoxShutdownShowed = true;

                        MachineMessageBox.Show("Warning", "Please, shutdown and restart the machine");

                        //System.Diagnostics.Process.Start("shutdown -s /t 15 /c \"Bye bye..\"");
                        }

                        //GPIx237
                        DialogResult = DialogResult.Abort;
                        //GPFx237
                    }
                    else
                    {
                        messageBoxShutdownShowed = true;

                        MachineMessageBox.Show("Warning", "Please, shutdown and restart the machine");

                        //System.Diagnostics.Process.Start("shutdown -s /t 15 /c \"Bye bye..\"");

                        //GPIx237
                        DialogResult = DialogResult.Abort;
                        //GPFx237
                    }
                    //GPFx149
                }
            }

            iter++;
        }

        private void HandleClick()
        {
            if (++NumberOfClicksDone == NumberOfClicksToExit)
            {
                var msgBox = new MachineMessageBox(MessageBoxTitle, MessageBoxMessage);

                messageBoxVisible = true;

                msgBox.ShowDialog();

                if (msgBox.DialogResult == DialogResult.OK)
                {
                    DialogResult = DialogResult.Abort;

                    isRunning = false;
                }
                else
                {
                    NumberOfClicksDone = 0;
                }

                messageBoxVisible = false;
            }
        }

        private void FormWait_DoubleClick(object sender, EventArgs e)
        {
            HandleClick();
        }

        private void FormWait_Click(object sender, EventArgs e)
        {
            HandleClick();
        }

        private void panelMinimize_DoubleClick(object sender, EventArgs e)
        {
            panelMinimize.BackColor = Color.Red;
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
