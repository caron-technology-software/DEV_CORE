#if TEST
#undef OVERRIDE_WND_PROC
#else
//////versione 3.0.0 della spreader richiede nuovi driver per il touch screen con pressione bottone inpulsiva e non a trascinamento:
//#define OVERRIDE_WND_PROC                 //la versione 1.3.11 della cradle è con questo è importante!!! (non ha nuovi driver touch screen!!!) 
#undef OVERRIDE_WND_PROC
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineButton : Button
    {
        private volatile bool disableOnActiveChange = false;

        const int DefaultButtonSize = 112;

        public int buttonSize = DefaultButtonSize;
        public int ButtonSize
        {
            get
            {
                return buttonSize;
            }

            set
            {
                buttonSize = value;
                this.Size = new Size(ButtonSize, ButtonSize);
            }
        }

        public new System.Drawing.Size Size
        {
            get
            {
                return new Size(ButtonSize, ButtonSize);
            }

            set
            {
                buttonSize = Math.Max(value.Width, value.Height);
            }
        }

        // Prevent Text from being set on the button (since it will be an icon)
        [Browsable(false)]
        public override string Text { get { return ""; } set { base.Text = ""; } }

        [Browsable(false)]
        public override ContentAlignment TextAlign { get { return base.TextAlign; } set { base.TextAlign = value; } }

        public bool StateChangeActivated { get; set; } = true;

        private Bitmap activeBackgroundImage;
        public Bitmap ActiveBackgroundImage
        {
            get
            {
                return activeBackgroundImage;
            }

            set
            {
                if (value != null && activeBackgroundImage != value)
                {
                    Bitmap bmp = new Bitmap(value, new Size(ButtonSize, ButtonSize));
                    bmp.MakeTransparent(bmp.GetPixel(0, 0));

                    activeBackgroundImage = bmp;

                    Draw();
                }
            }
        }

        private Bitmap inactiveBackgroundImage;
        public Bitmap InactiveBackgroundImage
        {
            get
            {
                return inactiveBackgroundImage;
            }

            set
            {
                if (value != null && inactiveBackgroundImage != value)
                {
                    Bitmap bmp = new Bitmap(value, new Size(ButtonSize, ButtonSize));
                    bmp.MakeTransparent(bmp.GetPixel(0, 0));

                    inactiveBackgroundImage = bmp;

                    Draw();
                }
            }
        }

        public event EventHandler<EventArgs> ActiveChanged;

        private void OnActiveChanged(EventArgs e)
        {
            ActiveChanged?.Invoke(this, e);
        }

        public DateTime LastActivation { get; private set; } = DateTime.MinValue;
        public DateTime LastDisactivation { get; private set; } = DateTime.MinValue;

        private bool active;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                if (active != value)
                {
                    active = value;

                    if (active)
                    {
                        LastActivation = DateTime.Now;
                    }
                    else
                    {
                        LastDisactivation = DateTime.Now;
                    }

                    if (disableOnActiveChange == false)
                    {
                        OnActiveChanged(new EventArgs());
                    }

                    Draw();

                    Refresh();
                }
            }
        }

        public void SetActiveWithoutEvent(bool active)
        {
            if (Active != active)
            {
                disableOnActiveChange = true;
                Active = active;
                disableOnActiveChange = false;
            }
        }

        private void Draw()
        {
            if (active)
            {
                this.BackgroundImage = ActiveBackgroundImage;
            }
            else
            {
                this.BackgroundImage = InactiveBackgroundImage;
            }

            BackgroundImageLayout = ImageLayout.Zoom;
            FlatAppearance.BorderSize = 0;
        }

        public MachineButton()
        {
            InitializeComponent();
            Active = false;
            TabStop = false;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            base.Size = new Size(ButtonSize, ButtonSize);
            this.Size = new Size(ButtonSize, ButtonSize);

            ForeColor = Color.Transparent;
            BackColor = Color.Transparent;

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            //FlatAppearance.BorderColor = Color.Transparent;

            if ((ActiveBackgroundImage is null) && (InactiveBackgroundImage is null))
            {
                SetImages(Machine.UI.Controls.Properties.Resources.verified_green, Machine.UI.Controls.Properties.Resources.verified_gray);
            }

            Refresh();

            this.DoubleBuffered = true;
        }

        public void SetImages(Bitmap activeBackgroundImage, Bitmap inactiveBackgroundImage)
        {

            ActiveBackgroundImage = activeBackgroundImage;
            InactiveBackgroundImage = inactiveBackgroundImage;
            Draw();
        }

        public void SetImages(string pathActiveBackgroundImage, string pathInactiveBackgroundImage)
        {

            ActiveBackgroundImage = new Bitmap(pathActiveBackgroundImage);
            InactiveBackgroundImage = new Bitmap(pathInactiveBackgroundImage);
            Draw();
        }

        public override void Refresh()
        {
            try
            {
                base.Refresh();
            }
            catch
            {
                try
                {
                    this?.Invoke((MethodInvoker)delegate ()
                  {
                      base.Refresh();
                  });
                }
                catch
                {
                    //--
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {

        }

        protected override void OnMouseLeave(EventArgs e)
        {

        }

        protected override void OnMouseHover(EventArgs e)
        {

        }

        protected override void OnGotFocus(EventArgs e)
        {
        }

        protected override void OnLostFocus(EventArgs e)
        {

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e); //GPI27

            if (StateChangeActivated)
            {
                Active = !Active;
            }
        }

#if OVERRIDE_WND_PROC
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_POINTERDOWN:
                case Win32.WM_POINTERUP:
                case Win32.WM_POINTERUPDATE:
                case Win32.WM_POINTERCAPTURECHANGED:
                    break;

                default:
                    base.WndProc(ref m);
                    return;
            }

            int pointerID = Win32.GET_POINTER_ID(m.WParam);
            Win32.POINTER_INFO pi = new Win32.POINTER_INFO();
            if (!Win32.GetPointerInfo(pointerID, ref pi))
            {
                Win32.CheckLastError();
            }

            Point pt = PointToClient(pi.PtPixelLocation.ToPoint());
            MouseEventArgs me = new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, pt.X, pt.Y, 0);
            switch (m.Msg)
            {
                case Win32.WM_POINTERDOWN:
                    //Console.WriteLine("Down" + pt);
                    //MessageBox.Show("DOWN");
                    this.OnMouseDown(me);
                    break;

                case Win32.WM_POINTERUP:
                    //Console.WriteLine("Up");
                    this.OnMouseUp(me);
                    //MessageBox.Show("UP");
                    break;
            }
        }
#endif

    }
}