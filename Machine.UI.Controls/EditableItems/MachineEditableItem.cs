using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    //[REFACTOR]
    public class MachineEditableItem : UserControl
    {
        private string propertyName = string.Empty;
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                propertyName = value;
                UpdateControl();
            }
        }

        public Color ColorBackground { get; set; } = Color.FromArgb(240, 240, 240);
        public Color ColorBackgroundSelectedItem { get; set; } = Color.FromArgb(160, 160, 160);

        public string StringID { get; set; }

        public bool IsPropertyEditable { get; set; } = true;
        public string MessageBoxText { get; set; } = "";

        public event EventHandler<EventArgs> PropertyChanged;

        protected void OnPropertyChange(EventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public virtual object GetValue()
        {
            return this;
        }

        protected virtual void UpdateControl()
        {

        }

        public MachineEditableItem()
        {
            //--
        }
    }
}
