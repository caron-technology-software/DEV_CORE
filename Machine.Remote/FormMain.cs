using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Flurl;
using Flurl.Http;

namespace Machine.Remote
{
    public partial class FormMain : Form
    {
        public int Counter { get; private set; } = 0;

        private volatile bool running = true;
        private volatile float ratio = 1.0f;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            pictureBox.Location = new Point(0, 0);

            Task.Run(() =>
            {
                var start = DateTime.UtcNow;

                while (running)
                {

                    byte[] image = "http://localhost:9010/"
                            .AppendPathSegment("test")
                            .AppendPathSegment("screenshot").GetBytesAsync().Result;

                    Image imgFile = Image.FromStream(new MemoryStream(image));
                    ratio = (float)imgFile.Width / (float)imgFile.Height;

                    try
                    {
                        this?.Invoke((MethodInvoker)delegate ()
                        {

                            pictureBox.Image = imgFile;

                            Text = $"{(Counter / (DateTime.UtcNow - start).TotalSeconds).ToString("0.0")} Fps";

                        });
                    }
                    catch
                    {
                        //--
                    }

                    Counter++;

                    if (Counter == 200)
                    {
                        Counter = 0;
                        start = DateTime.UtcNow;
                    }

                    Thread.Sleep(1);
                }
            });
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
        }

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            int w = Width;
            int h = (int)((float)w / ratio);

            Height = h + 50;

            pictureBox.Width = w;
            pictureBox.Height = h;

            Refresh();
        }
    }
}
