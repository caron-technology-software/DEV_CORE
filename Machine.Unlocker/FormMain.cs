using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Extensions.Encoding;
using ProRob.Extensions.String;

using Machine.Security;
using System.Runtime.InteropServices;

namespace Machine.Unlocker
{
    public partial class FormMain : Form
    {
        private const string ApplicationVersion = "1.0.0";
        private const int LengthOfUnlockCode = 22;
        private const int LenghtOfCode = 11;


        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            statusStrip.Items.Add($"Version: {ApplicationVersion}");

            textBoxUnlockCode.GotFocus += (obj, evt) =>
            {
                HideCaret(textBoxUnlockCode.Handle);
            };
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            labelUnlockCode.Text = "Unlock code";

            if (string.IsNullOrEmpty(textBoxCode.Text))
            {
                MessageBox.Show("Machine code is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBoxCode.Text.Length != LenghtOfCode)
            {
                MessageBox.Show("Machine code is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                ushort hours = ushort.Parse(textBoxHours.Text);
            }
            catch
            {
                MessageBox.Show("Insert correctly number of hours", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string textToEncrypt = $"#UNLK#:01:{textBoxHours.Text}";
            string unlockCode = Encryption.Encrypt(Encoding.ASCII.GetBytes(textToEncrypt), textBoxCode.Text).ToBase62();

            if (unlockCode.Length != LengthOfUnlockCode)
            {
                MessageBox.Show("Errors occurred while generating the code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //labelUnlockCode.Text = $"Unlock code ({textToEncrypt}) [{unlockCode.Length}]";
            textBoxUnlockCode.Text = unlockCode;
        }

        private void TextBoxUnlockCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUnlockCode.Text))
            {
                return;
            }

            Clipboard.SetText(textBoxUnlockCode.Text);

            textBoxUnlockCode.SelectionStart = 0;
            textBoxUnlockCode.SelectionLength = 0;
        }
    }
}
