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
    public partial class MachineStringEditableItem : MachineEditableItem
    {
        public MachineStringEditableItem()
        {
            InitializeComponent();
        }

        public bool IsSelected { get; set; } = false;

        public event EventHandler<EventArgs> ItemSelected;

        private void OnItemSelected(EventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }

        private string propertyValue;
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

            labelPropertyValue.TextAlign = ContentAlignment.MiddleLeft;
            labelPropertyValue.BorderStyle = BorderStyle.None;
        }

        public void HandlePropertyValue()
        {
            if (IsPropertyEditable == false)
            {
                MachineMessageBox.Show("", MessageBoxText);
                return;
            }

            using (var keyb = new TouchAlphaNumericKeyboard(PropertyValue, ""))
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
            BackColor = ColorBackgroundSelectedItem;
        }

        public void DeselectItem()
        {
            IsSelected = false;
            BackColor = ColorBackground;
        }

        private void LabelPropertyValue_DoubleClick(object sender, EventArgs e)
        {
            //Console.WriteLine("LabelPropertyValue_DoubleClick");

            ///SelectItem();
            //OnItemDoubleClick(new EventArgs());

            //OnItemSelected(new EventArgs());
            //ChangePropertyValue();
        }

        private void LabelPropertyValue_MouseDown(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("LabelPropertyValue_MouseDown");

            SelectItem();
            OnItemSelected(new EventArgs());
        }

        private void LabelPropertyValue_MouseUp(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("LabelPropertyValue_MouseUp");
        }

        private void LabelPropertyValue_Click(object sender, EventArgs e)
        {

        }
    }
}
