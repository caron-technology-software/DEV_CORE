using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Machine.Shell
{
    public partial class FormShell : Form
    {
        private const bool ShowUptime = true;
        private const int NumberOfClicksToExit = 3;
        private const double DeltaPictureRotation = 1;
        private readonly TimeSpan UiRefreshTimeout = TimeSpan.FromMilliseconds(50);

        private static Thread threadUpdateUI;

        private volatile bool isRunning = false;
        public bool IsRunning { get => isRunning; private set => isRunning = value; }

        public TimeSpan WaitBeforeExit { get; set; } = new TimeSpan(0, 0, 0, 1);

        public int NumberOfClicksDone { get; private set; } = 0;
        public CommandType CommandType { get; private set; } = CommandType.CommandPrompt;
        public string Command { get; private set; } = "";
        public string Arguments { get; private set; } = "";

        private DateTime startTimestamp = DateTime.UtcNow;
        public TimeSpan Uptime { get => DateTime.UtcNow - startTimestamp; }

        public string Title
        {
            get => labelTitle.Text;
            set => labelTitle.Text = value;
        }

        private static Process process;

        private void UpdateUI()
        {
            while (IsRunning)
            {
                iconPictureBox.Rotation += DeltaPictureRotation;

                if (iconPictureBox.Rotation == 360)
                {
                    iconPictureBox.Rotation = 0;
                }

                if (ShowUptime)
                {
                    labelUptime.Invoke((MethodInvoker)delegate { labelUptime.Text = $"{Uptime.ToString(@"mm\:ss")}"; });
                }

                Thread.Sleep(UiRefreshTimeout);
            }
        }

        public FormShell(string command, CommandType commandType, string arguments = "")
        {
            InitializeComponent();

            Command = command;
            CommandType = commandType;
            Arguments = arguments;

            labelTitle.Text = string.Empty;

            TopLevel = true;
            TopMost = true;
        }

        private void FormShell_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            //TopMost = true;

            IsRunning = true;

            threadUpdateUI = new Thread(new ThreadStart(UpdateUI));
            threadUpdateUI.Start();

            if (CommandType == CommandType.CommandPrompt)
            {
                process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = @"C:\Windows\System32\cmd.exe",
                        Arguments = $"/c {Command}",
                        WorkingDirectory = @"C:\Windows\System32",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                    },

                    EnableRaisingEvents = true
                };
            }
            else
            {
                process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = Command,
                        Arguments = Arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                    },

                    EnableRaisingEvents = true
                };
            }

            process.ErrorDataReceived += WriteToTextBox;
            process.OutputDataReceived += WriteToTextBox;

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.Exited += Process_Exited;
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            HandleExit();
        }

        private void HandleExit()
        {
            IsRunning = false;

            process.CancelOutputRead();
            process.CancelErrorRead();

            if (!process.HasExited)
            {
                process.CloseMainWindow();
                process.Kill();
            }

            process.Close();
            process.Dispose();

            Thread.Sleep(WaitBeforeExit);

            this?.Invoke((MethodInvoker)delegate ()
            {
                for (int i = 0; i < 10; i++)
                {
                    Opacity -= 0.1;
                    Thread.Sleep(UiRefreshTimeout);
                    Refresh();
                }

                Close();
            });

        }

        private void HandleClick()
        {
            NumberOfClicksDone++;

            if (NumberOfClicksDone > NumberOfClicksToExit)
            {
                HandleExit();
            }
        }

        private void labelTitle_Click(object sender, EventArgs e)
        {
            HandleClick();
        }

        private void FormShell_Click(object sender, EventArgs e)
        {
            HandleClick();
        }

        private void cmdOutput_MouseClick(object sender, MouseEventArgs e)
        {
            HandleClick();
        }

        #region Write to RichTextBox
        void WriteToTextBox(object sender, DataReceivedEventArgs e)
        {
            string str = e.Data;
            this.SetText(str);
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.cmdOutput.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.cmdOutput.AppendText(String.Format("{0}\n", text));
                    //this.cmdOutput.ScrollToCaret();
                }
            }
            catch
            {
                System.Console.WriteLine("Errors on SetText()");
            }
        }
        #endregion
    }
}
