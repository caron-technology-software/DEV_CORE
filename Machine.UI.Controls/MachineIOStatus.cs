using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using FontAwesome.Sharp;

namespace Machine.UI.Controls
{
    public partial class MachineIOStatus : UserControl
    {
        private string name = string.Empty;
        public new string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    mlName.Text = name;
                }
            }
        }

        private string channel = string.Empty;
        public string Channel
        {
            get => channel;
            set
            {
                if (this.channel != value)
                {
                    this.channel = value;
                    mlChannel.Text = this.channel;
                }
            }
        }

        private string channelDecode = string.Empty;
        public string ChannelDecode
        {
            get => channelDecode;
            set
            {
                if (this.channelDecode != value)
                {
                    this.channelDecode = value;
                    mlChannelDecode.Text = this.channelDecode;
                }
            }
        }

        public void SetColorLabel(Color color)
        {
            if (mlValue.ForeColor != color)
            {
                mlValue.ForeColor = color;
            }
        }

        private string value = string.Empty;
        public string Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    mlValue.Text = this.value;
                }
            }
        }

        private string valueExchangeAxis = string.Empty;
        public string ValueExchangeAxis
        {
            get => valueExchangeAxis;
            set
            {
                if (this.valueExchangeAxis != value)
                {
                    this.valueExchangeAxis = value;
                }
            }
        }

        public Action ActionUp { get; set; } = null;
        public Action ActionDown { get; set; } = null;

        public Action ActionUpEncoder { get; set; } = null;
        public Action ActionDownEncoder { get; set; } = null;

        public int Index { get; set; }

        public MachineIOStatus()
        {
            InitializeComponent();

            panelIcon.BackgroundImage = IconChar.SyncAlt.ToBitmap(Color.Gray, 20);
            panelIcon.BackgroundImageLayout = ImageLayout.Center;
        }

        private void MachineIOStatus_Load(object sender, EventArgs e)
        {
            // --
        }

        private void mlValue_Click(object sender, EventArgs e)
        {
            // --
        }

        private void panelIcon_MouseUp(object sender, MouseEventArgs e)
        {
            //GPIx21
            //////funziona solo con la SPREADER in quanto per la CRADLE la scritta è "CHANNEL 14 " con spazio dopo:
            //////si fa anche per la Cradle!
            if (!(mlChannel.Text.Equals("CHANNEL 14")))
            //Console.WriteLine($"mlName.Text: {mlName.Text}.");
            //GPFx21
            {
                if (ActionUp != null)
                {
                    //in realtà non serve è il task lanciato ogni 10 millisecondi che setta il colore e il valore
                    //mlValue.Text = "False";
                    Value = "False";
                    Task.Run(ActionUp);
                }
            }
        }

        private void panelIcon_MouseDown(object sender, MouseEventArgs e)
        {

            if (!(mlChannel.Text.Equals("ENCODER 01")))
            {
                if (ActionDownEncoder != null)
                {
                    Task.Run(ActionDownEncoder);
                }
            }
            if (!(mlChannel.Text.Equals("ENCODER 02")))
            {
                if (ActionUpEncoder != null)
                {
                    Task.Run(ActionUpEncoder);
                }
            }

            //GPIx21
            //////funziona solo con la SPREADER in quanto per la CRADLE la scritta è "CHANNEL 14 " con spazio dopo:
            //////si fa anche per la Cradle!
            if (!(mlChannel.Text.Equals("CHANNEL 14")))
            //GPFx21
            {
                if (ActionDown != null)
                {
                    //mlValue.Text = "True";
                    Value = "True";
                    Task.Run(ActionDown);
                }
            }
            else
            {
                if (Value.Equals("True"))
                {
                    if (ActionUp != null)
                    {
                        //mlValue.Text = "False";
                        Value = "False";
                        Task.Run(ActionUp);
                    }
                }
                else
                {
                    if (ActionDown != null)
                    {
                        //mlValue.Text = "True";
                        Value = "True";
                        Task.Run(ActionDown);
                    }
                }
            }
        }

        private void MachineIOStatus_MouseDown(object sender, MouseEventArgs e)
        {
            // --
        }

        private void MachineIOStatus_MouseUp(object sender, MouseEventArgs e)
        {
            // --
        }

        private void mlValue_MouseDown(object sender, MouseEventArgs e)
        {
            // --
        }

        private void mlValue_MouseUp(object sender, MouseEventArgs e)
        {
            // --
        }

        private void mlName_MouseDown(object sender, MouseEventArgs e)
        {
            // --
        }

        private void mlName_MouseUp(object sender, MouseEventArgs e)
        {
            // --
        }

        private void pannelButton_Click(object sender, EventArgs e)
        {
            //--
        }
    }
}
