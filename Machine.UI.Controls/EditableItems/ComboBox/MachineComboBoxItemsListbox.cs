using System;
using System.Drawing;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineComboBoxItemsListbox : UserControl
    {
        public int NumberOfElements { get => flowPanel.Controls.Count; }

        private readonly FlowLayoutPanel flowPanel;

        public event EventHandler<EventArgs> PropertyChanged;

        private void OnPropertyChange(EventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public MachineComboBoxItemsListbox()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            flowPanel = new MachineFlowLayoutPanel()
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Height = panelListbox.Height,
                Width = panelListbox.Width,
                //BackColor = Color.White,
                //AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BorderStyle = BorderStyle.None,

            };

            flowPanel.Scroll += FlowPanel_Scroll;

            panelListbox.Controls.Add(flowPanel);
        }

        private void FlowPanel_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
            Application.DoEvents();
            flowPanel.Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            //MessageBox.Show("OnResize");
            base.OnResize(e);

            panelListbox.Size = new Size(this.Size.Width, this.Size.Height);

            if (flowPanel != null)
            {
                flowPanel.Height = this.Size.Height;
                flowPanel.Width = this.Size.Width;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BorderStyle = BorderStyle.FixedSingle;

        }

        private void SpreaderSettingsTable_Load(object sender, EventArgs e)
        {
        }

        public void Add(MachineComboBoxItem setting)
        {
            setting.PropertyChanged += (sender, e) => OnPropertyChange(new EventArgs());

            flowPanel.Controls.Add(setting);
        }

        public MachineComboBoxItem GetControl(int index)
        {
            return ((MachineComboBoxItem)flowPanel.Controls[index]);
        }

        public void Clear()
        {
            flowPanel.Controls.Clear();
        }

    }
}
