using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;

namespace Caron.Cradle.UI
{
    public partial class FormBroswerInterface : FormCradleBase
    {
        public FormBroswerInterface()
        {
            InitializeComponent();

            //Monitor parent process exit and close subprocesses if parent process exits first
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

            //For Windows 7 and above, best to include relevant app.manifest entries as well
            Cef.EnableHighDPISupport();

            var settings = new CefSettings();

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
        }

        private void FormBroswerInterface_Load(object sender, EventArgs e)
        {
            browser.Visible = true;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                browser.Load(@"http://localhost/pdf/manuals/");
            }
        }

        private void FormBroswerInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("[FormBroswerInterface] Disposing internal broswer..");

            browser.Dispose();

            Cef.Shutdown();
        }

        protected override void UpdateUIForm()
        {
            //--
        }
    }
}
