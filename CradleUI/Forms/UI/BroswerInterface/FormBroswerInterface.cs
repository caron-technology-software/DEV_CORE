using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Web.WebView2.WinForms;

namespace Caron.Cradle.UI
{
    public partial class FormBroswerInterface : FormCradleBase
    {
        public FormBroswerInterface()
        {
            InitializeComponent();
        }

        private async void FormBroswerInterface_Load(object sender, EventArgs e)
        {
            try
            {
                await browser.EnsureCoreWebView2Async();
                browser.Source = new Uri("http://localhost/pdf/manuals/");
                browser.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'inizializzazione del browser: " + ex.Message);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            // In caso tu voglia ricaricare ogni volta che diventa visibile
            // (opzionale, puoi ometterlo se non serve)
            if (Visible && browser?.CoreWebView2 != null)
            {
                browser.Reload();
            }
        }

        private void FormBroswerInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("[FormBroswerInterface] Disposing internal browser...");
            browser?.Dispose();
        }

        protected override void UpdateUIForm()
        {
            //-- eventuali aggiornamenti UI
        }
    }


}
