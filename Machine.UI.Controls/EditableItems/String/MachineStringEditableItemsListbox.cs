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
    public partial class MachineStringEditableItemsListbox : UserControl
    {
        public int NumberOfElements { get => flowPanel.Controls.Count; }

        private readonly MachineFlowLayoutPanel flowPanel = new MachineFlowLayoutPanel();

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
        public void DisableScrollBar()
        {
            vScrollBar.Visible = false;
        }

        public void SetBorder(BorderStyle borderStyle, Color borderColor)
        {
            flowPanel.BorderStyle = borderStyle;
            flowPanel.BorderColor = borderColor;
        }

        public event EventHandler<EventArgs> PropertyChanged;

        private void OnPropertyChange(EventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public event EventHandler<EventArgs> ItemSelected;

        private void OnItemSelected(EventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }

        private event EventHandler<EventArgs> InternalItemSelected;

        private void OnInternalItemSelected(EventArgs e)
        {
            int n = GetSelectedControlsCount();

            if (n > 0)
            {
                if (n > 1)
                {
                    MachineStringEditableItem item = GetControl(LastSelectedControlIndex);
                    item?.DeselectItem();
                }

                InternalItemSelected?.Invoke(this, e);
                LastSelectedControlIndex = GetSelectedControlIndex();
                OnItemSelected(new EventArgs());
            }

            Console.WriteLine($"[LISTBOX] LastSelectedControlIndex:{LastSelectedControlIndex}");
        }

        public MachineStringEditableItemsListbox()
        {
            InitializeComponent();

            flowPanel.AutoScroll = true;
            flowPanel.FlowDirection = FlowDirection.TopDown;
            flowPanel.WrapContents = false;
            flowPanel.Height = panelListbox.Height;
            flowPanel.Width = panelListbox.Width;
            //BackColor = Color.White,
            //AutoSizeMode = AutoSizeMode.GrowAndShrink,
            flowPanel.BorderStyle = BorderStyle.None;
            flowPanel.VerticalScroll.Enabled = false;
            flowPanel.VerticalScroll.Visible = false;

            flowPanel.Scroll += FlowPanel_Scroll;

            NativeMethods.SetScrollBar(flowPanel.Handle, ScrollBarDirection.Vertical, true);

        }

        private void FlowPanel_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
            Application.DoEvents();
            //Refresh();
        }

        public void ScrollPanelTo(int index)
        {
            flowPanel.SuspendLayout();

            GetControl(LastSelectedControlIndex)?.DeselectItem();

            var ctl = flowPanel.Controls[index];
            var loc = ctl.Location - new Size(flowPanel.AutoScrollPosition);
            loc -= new Size(ctl.Margin.Left, ctl.Margin.Top);
            flowPanel.AutoScrollPosition = loc;

            LastSelectedControlIndex = index;
            GetControl(LastSelectedControlIndex)?.SelectItem();
            vScrollBar.Value = flowPanel.VerticalScroll.Value;
            flowPanel.ResumeLayout();
            flowPanel.Refresh();
        }

        public int GetSelectedControlsCount()
        {
            return flowPanel.Controls.Cast<MachineStringEditableItem>().ToList().Where(x => x.IsSelected).Count();
        }

        public int GetSelectedControlIndex()
        {
            var index = flowPanel.Controls.Cast<MachineStringEditableItem>().ToList().FindIndex(x => x.IsSelected);

            return index;
        }

        private void MachineStringSettingsListbox_Load(object sender, EventArgs e)
        {
            panelListbox.Controls.Add(flowPanel);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            panelListbox.Size = new Size(this.Size.Width, this.Size.Height);

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
            BorderStyle = BorderStyle.FixedSingle;
        }

        public int LastSelectedControlIndex { get; private set; } = -1;

        public MachineStringEditableItem GetControl(int index)
        {
            if (index >= 0 && index < NumberOfElements)
            {
                return ((MachineStringEditableItem)flowPanel.Controls[index]);
            }

            return null;
        }

        public void SelectItem(int index)
        {
            GetControl(index)?.SelectItem();
        }

        public void DeselectItem(int index)
        {
            GetControl(index)?.DeselectItem();
        }

        public void RefreshItems(List<MachineStringEditableItem> settings, int indexItemToSelect)
        {
            Console.WriteLine("RefreshItems");

            SuspendLayout();
            flowPanel.SuspendLayout();

            ClearItems();

            settings.ForEach(setting =>
            {
                setting.PropertyChanged += (sender, e) => OnPropertyChange(new EventArgs());
                setting.ItemSelected += (sender, e) => OnInternalItemSelected(new EventArgs());
            });

            flowPanel.Controls.AddRange(settings.ToArray());

            SelectItem(indexItemToSelect);
            LastSelectedControlIndex = indexItemToSelect;

            flowPanel.ResumeLayout();
            ResumeLayout();

            Refresh();

            UpdateControl();
        }

        /*void AddItem(MachineStringSettingItem setting)
        {
            setting.PropertyChanged += (sender, e) => OnPropertyChange(new EventArgs());
            setting.ItemSelected += (sender, e) => OnInternalItemSelected(new EventArgs());

            flowPanel.Controls.Add(setting);
        }*/

        public void DeleteItem(int index)
        {
            flowPanel.Controls.RemoveAt(index);

            UpdateControl();
        }

        public void ClearItems()
        {
            flowPanel.Controls.Clear();

            UpdateControl();
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            flowPanel.AutoScrollPosition = new Point(0, vScrollBar.Value);
        }
    }
}
