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
    public partial class MachineStringEditableItemWithName : UserControl
    {
        public MachineStringEditableItemWithName()
        {
            InitializeComponent();
        }

        public bool IsSelected { get; set; } = false;

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

        public Color ColorBackground { get; set; } = Color.FromArgb(240, 240, 240);
        public Color ColorBackgroundSelectedItem { get; set; } = Color.DarkGray;

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
                if (propertyName != value)
                {
                    propertyName = value;
                    labelPropertyName.Text = propertyName;
                }
            }
        }

        private string propertyValue = "";
        public string PropertyValue
        {
            get
            {
                return propertyValue;
            }
            set
            {
                if (propertyValue != value)
                {
                    propertyValue = value;
                    labelPropertyValue.Text = propertyValue;
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            labelPropertyValue.TextAlign = ContentAlignment.MiddleCenter;
            labelPropertyValue.BorderStyle = BorderStyle.None;
        }

        public void ChangePropertyValue()
        {
            using (var keyb = new TouchAlphaNumericKeyboard(PropertyValue, "", TouchKeyboardType.AllKeys))
            {
                bool exitCond = false;

                while (exitCond == false)
                {
                    var dialogResult = keyb.ShowDialog();

                    //Console.WriteLine($"dialogResult: {dialogResult}");

                    if (dialogResult == DialogResult.OK)
                    {
                        if (String.IsNullOrEmpty(keyb.StringValue) == false)
                        {
                            exitCond = true;
                            PropertyValue = keyb.StringValue;
                            OnPropertyChange(new EventArgs());
                        }
                    }
                    else
                    {
                        exitCond = true;
                    }
                }//while           
            }//using
        }

        public void SelectItem()
        {
            IsSelected = true;
            //BackColor = ColorBackgroundSelectedItem;
        }

        public void DeselectItem()
        {
            IsSelected = false;
            //BackColor = ColorBackground;
        }

        private void LabelPropertyValue_MouseDown(object sender, MouseEventArgs e)
        {
            SelectItem();
            OnItemSelected(new EventArgs());
            ChangePropertyValue();
        }
    }
}
