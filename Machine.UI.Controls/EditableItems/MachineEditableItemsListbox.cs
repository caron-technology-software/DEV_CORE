using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineEditableItemsListbox : System.Windows.Forms.UserControl
    {
        public int NumberOfElements { get => flowPanel.Controls.Count; }

        private readonly MachineFlowLayoutPanel flowPanel;

        public event EventHandler<EventArgs> PropertyChanged;

        private void OnPropertyChange(EventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public MachineEditableItemsListbox()
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
                BorderStyle = BorderStyle.FixedSingle,
            };

            flowPanel.Scroll += FlowPanel_Scroll;

            panelListbox.Controls.Add(flowPanel);
        }

        public void DisableScrollBar()
        {
            vScrollBar.Visible = false;
        }

        public void SetBorder(BorderStyle borderStyle, Color borderColor)
        {
            flowPanel.BorderStyle = borderStyle;
            flowPanel.BorderColor = borderColor;
        }

        public void UpdateControl()
        {
            if (flowPanel is null || vScrollBar is null)
            {
                return;
            }

            //flowPanel.AutoScroll = false;

            vScrollBar.Location = new Point(panelListbox.Width - vScrollBar.Width + 1, 0);
            vScrollBar.Height = panelListbox.Height;
            vScrollBar.Minimum = flowPanel.VerticalScroll.Minimum;
            vScrollBar.Maximum = flowPanel.VerticalScroll.Maximum;
            vScrollBar.LargeChange = flowPanel.VerticalScroll.LargeChange;
            vScrollBar.SmallChange = flowPanel.VerticalScroll.SmallChange * 10;

            //flowPanel.AutoScroll = true;
        }

        private void FlowPanel_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
            Application.DoEvents();
            //flowPanel.Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (flowPanel != null)
            {
                flowPanel.Height = this.Size.Height;
                flowPanel.Width = this.Size.Width;
            }

            UpdateControl();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            BorderStyle = BorderStyle.None;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                UpdateControl();
            }
        }

        public void Add(MachineEditableItem item)
        {
            item.PropertyChanged += (sender, e) => OnPropertyChange(new EventArgs());

            flowPanel.Controls.Add(item);

            UpdateControl();
        }

        public MachineEditableItem GetControl(int index)
        {
            return ((MachineEditableItem)flowPanel.Controls[index]);
        }

        public dynamic GetValue(int index)
        {
            return ((MachineEditableItem)flowPanel.Controls[index]).GetValue();
        }

        public void Clear()
        {
            flowPanel.Controls.Clear();
        }

        private void MachineFloatingEditableItemsListbox_Paint(object sender, PaintEventArgs e)
        {
            panelListbox.Height = Height;
            panelListbox.Width = Width;
        }

        private void panelListbox_Paint(object sender, PaintEventArgs e)
        {
            UpdateControl();
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            flowPanel.AutoScrollPosition = new Point(0, vScrollBar.Value);
        }

        private void MachineEditableItemsListbox_Resize(object sender, EventArgs e)
        {
            UpdateControl();
        }
    }
}
