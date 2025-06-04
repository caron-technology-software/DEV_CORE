namespace Caron.Cradle.UI
{
    partial class FormBroswerInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            try
            {
                base.Dispose(disposing);
            }
            catch
            {
                //--
            }
        }

#pragma warning disable CS0618 //  for: new CefSharp.WinForms.ChromiumWebBrowser();
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browser = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.SuspendLayout();
            // 
            // browser
            // 
            this.browser.CreationProperties = null;
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(0, 0);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(1280, 720);
            this.browser.TabIndex = 0;
            // 
            // FormBroswerInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.browser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBroswerInterface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBroswerInterface_FormClosing);
            this.Load += new System.EventHandler(this.FormBroswerInterface_Load);
            this.ResumeLayout(false);

        }

        #endregion
#pragma warning restore CS0618

        private Microsoft.Web.WebView2.WinForms.WebView2 browser;
    }
}