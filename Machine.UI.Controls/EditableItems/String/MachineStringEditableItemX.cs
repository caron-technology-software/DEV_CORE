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
    public partial class MachineStringEditableItemX : MachineEditableItem
    {
        public MachineStringEditableItemX()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        public override object GetValue()
        {
            return PropertyValue;
        }

        protected override void UpdateControl()
        {
            slPropertyName.Text = PropertyName;
        }

        //public bool IsSelected { get; set; } = false;

        //public event EventHandler<EventArgs> ItemSelected;

        //private void OnItemSelected(EventArgs e)
        //{
        //    ItemSelected?.Invoke(this, e);
        //}

        private string propertyValue;
        public string PropertyValue
        {
            get
            {
                return propertyValue;
            }
            set
            {
                //if (propertyValue != value)
                //{
                    propertyValue = value;
                //}

                if (!(PropertyValue is null))
                {
                    slPropertyValue.Text = PropertyValue.ToString();
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            slPropertyName.TextAlign = ContentAlignment.MiddleLeft;
            slPropertyName.BorderStyle = BorderStyle.None;

            slPropertyValue.TextAlign = ContentAlignment.MiddleCenter;
            slPropertyValue.BorderStyle = BorderStyle.FixedSingle;
        }

        public void HandlePropertyValue()
        {
            if (IsPropertyEditable == false)
            {
                MachineMessageBox.Show("", MessageBoxText);
                return;
            }

            string s1;
            if(PropertyValue is null)
            {
                s1 = "";
            }
            else
            {
                s1 = PropertyValue.ToString();
            }
            using (var keyb = new TouchAlphaNumericKeyboard(PropertyName, s1,TouchKeyboardType.AllKeys))
            {
                bool exitCond = false;

                while (exitCond == false)
                {
                    var dialogResult = keyb.ShowDialog();

                    //Console.WriteLine($"dialogResult: {dialogResult}");

                    if (dialogResult == DialogResult.OK)
                    {
                        //if (String.IsNullOrEmpty(keyb.StringValue) == false)
                        //{
                            exitCond = true;
                            PropertyValue = keyb.StringValue;
                            OnPropertyChange(new EventArgs());
                        //}
                    }
                    else
                    {
                        exitCond = true;
                    }
                }//while           
            }//using
        }

        //public void SelectItem()
        //{
        //      IsSelected = true;
        //      BackColor = ColorBackgroundSelectedItem;
        //}

        //public void DeselectItem()
        //{
        //    IsSelected = false;
        //    BackColor = ColorBackground;
        //}

        private void LabelPropertyValue_DoubleClick(object sender, EventArgs e)
        {
            //Console.WriteLine("LabelPropertyValue_DoubleClick");

            ///SelectItem();
            //OnItemDoubleClick(new EventArgs());

            //OnItemSelected(new EventArgs());
            //ChangePropertyValue();
        }

        //private void LabelPropertyValue_MouseDown(object sender, MouseEventArgs e)
        //{
        //    //Console.WriteLine("LabelPropertyValue_MouseDown");

        //    SelectItem();
        //    OnItemSelected(new EventArgs());
        //}

        //private void LabelPropertyValue_MouseUp(object sender, MouseEventArgs e)
        //{
        //    //Console.WriteLine("LabelPropertyValue_MouseUp");
        //}

        //private void LabelPropertyValue_Click(object sender, EventArgs e)
        //{

        //}

        private void slPropertyName_DoubleClick(object sender, EventArgs e)
        {
            HandlePropertyValue();
        }

        private void slPropertyValue_DoubleClick(object sender, EventArgs e)
        {
            HandlePropertyValue();
        }

        private void slPropertyName_Enter(object sender, EventArgs e)
        {
            BackColor = ColorBackgroundSelectedItem;
        }

        private void slPropertyName_Leave(object sender, EventArgs e)
        {
            BackColor = ColorBackground;
        }

        private void slPropertyValue_Enter(object sender, EventArgs e)
        {
            BackColor = ColorBackgroundSelectedItem;
        }

        private void slPropertyValue_Leave(object sender, EventArgs e)
        {
            BackColor = ColorBackground;
        }

        private void SlPropertyValue_Click(object sender, EventArgs e)
        {
            HandlePropertyValue();
        }

        private void SlPropertyName_Click(object sender, EventArgs e)
        {
            HandlePropertyValue();
        }
    }
}
