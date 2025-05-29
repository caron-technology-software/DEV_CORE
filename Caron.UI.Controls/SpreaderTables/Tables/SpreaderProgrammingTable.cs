using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caron.UI.Controls
{
    public partial class SpreaderProgrammingTable : UserControl
    {
        public int ButtonSize = 75;

        public class ButtonEventArgs : EventArgs
        {
            public string Name { get; set; }

            public ButtonEventArgs(string name)
            {
                Name = name;
            }
        }

        public event EventHandler<ButtonEventArgs> ButtonPressed;

        private void OnButtonPressed(ButtonEventArgs e)
        {
            ButtonPressed?.Invoke(this, e);
        }

        public event EventHandler<SpreaderCellTableProgramming.CellEventArgs> TableChanged;

        private void OnTableChanged(SpreaderCellTableProgramming.CellEventArgs e)
        {
            TableChanged?.Invoke(this, e);
        }

        public string ResourceMarkerChangeChecker { get; set; } = string.Empty;

        public SpreaderProgrammingTable()
        {
            Console.WriteLine("SpreaderProgrammingTable()");

            InitializeComponent();

            spreaderTable.TableCellChanged += TableChanged;
        }

        public SpreaderTableProgrammingInternal GetSpreaderTableInstance()
        {
            return spreaderTable;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            sbUp.Size = new Size(ButtonSize, ButtonSize);
            sbDown.Size = new Size(ButtonSize, ButtonSize);
            sbLeft.Size = new Size(ButtonSize, ButtonSize);
            sbRight.Size = new Size(ButtonSize, ButtonSize);
            sbTrash.Size = new Size(ButtonSize, ButtonSize);
            sbUpUp.Size = new Size(ButtonSize, ButtonSize);
            sbDownDown.Size = new Size(ButtonSize, ButtonSize);
            sbLeftLeft.Size = new Size(ButtonSize, ButtonSize);
            sbRightRight.Size = new Size(ButtonSize, ButtonSize);

            //Disattivazione funzionalità cambiamento stato 
            sbUp.StateChangeActivated = false;
            sbDown.StateChangeActivated = false;
            sbLeft.StateChangeActivated = false;
            sbRight.StateChangeActivated = false;
            sbTrash.StateChangeActivated = false;
            sbUpUp.StateChangeActivated = false;
            sbDownDown.StateChangeActivated = false;
            sbLeftLeft.StateChangeActivated = false;
            sbRightRight.StateChangeActivated = false;

            Refresh();
        }

        private void sbTrash_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("trash"));
        }

        private void sbUp_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("up"));
        }

        private void sbDown_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("down"));
        }

        private void sbLeft_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("left"));
        }

        private void sbRight_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("right"));
        }

        private void sbUpUp_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("up_up"));
        }

        private void sbDownDown_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("down_down"));
        }

        private void sbRightRight_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("right_right"));
        }

        private void sbLeftLeft_MouseDown(object sender, MouseEventArgs e)
        {
            OnButtonPressed(new ButtonEventArgs("left_left"));
        }
    }
}