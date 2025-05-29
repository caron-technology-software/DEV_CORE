#define AUTO_DROPPED_DOWN

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineComboBoxItem : UserControl
    {
        public Color ColorBackground { get; set; } = Color.FromArgb(240, 240, 240);
        public Color ColorBackgroundSelectedItem { get; set; } = Color.FromArgb(160, 160, 160);

        public string StringID { get; set; }

        private string propertyName;
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                propertyName = value;
                slPropertyName.Text = propertyName;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return comboBoxPropertyValue.SelectedIndex;
            }

            set
            {
                comboBoxPropertyValue.SelectedIndex = value;
            }
        }

        private List<Tuple<int, string>> propertyValues = new List<Tuple<int, string>>();
        public Tuple<int, string>[] PropertiesValues { get => propertyValues.ToArray(); }

        private int propertyValue = -1;
        public int PropertyValue
        {
            get
            {
                return propertyValue;
            }

            set
            {
                propertyValue = value;

                comboBoxPropertyValue.SelectedIndex = propertyValues.FindIndex(x => x.Item1 == value);
            }
        }

        public void SetPropertyValues(IEnumerable<Tuple<int, string>> values)
        {
            propertyValues.Clear();
            propertyValues.AddRange(values);

            comboBoxPropertyValue.Items.Clear();
            comboBoxPropertyValue.Items.AddRange(values.Select(x => x.Item2).ToArray());
        }

        public event EventHandler<EventArgs> PropertyChanged;

        private void OnPropertyChange(EventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public MachineComboBoxItem()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnLoad(EventArgs e)
        {
            comboBoxPropertyValue.SelectionStart = 0;
            comboBoxPropertyValue.SelectionLength = 0;
            comboBoxPropertyValue.TabIndex = 99;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            slPropertyName.TextAlign = ContentAlignment.MiddleLeft;
            slPropertyName.BorderStyle = BorderStyle.None;

            comboBoxPropertyValue.SelectedIndexChanged += ComboBoxPropertyValue_SelectedIndexChanged;

#if AUTO_DROPPED_DOWN
            comboBoxPropertyValue.Click += (sender, args) =>
            {
                //Azione di apertura PopUp in caso di click
                comboBoxPropertyValue.DroppedDown = true;
            };
#endif
        }

        private void ComboBoxPropertyValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxPropertyValue.SelectedIndex;
            propertyValue = propertyValues[index].Item1;

            OnPropertyChange(new EventArgs());
        }

        private void slPropertyName_Enter(object sender, EventArgs e)
        {
            BackColor = ColorBackgroundSelectedItem;
        }

        private void slPropertyName_Leave(object sender, EventArgs e)
        {
            BackColor = ColorBackground;
        }
    }
}
