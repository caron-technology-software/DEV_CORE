using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Machine.UI.Controls;

namespace Caron.UI.Controls
{
    public partial class SpreaderCellTableVisualization : UserControl
    {

        public const int CellWidth = 120;
        public const int CellHeight = 60;

        public class CellEventArgs : EventArgs
        {
            public int Row { get; set; }
            public int Col { get; set; }

            public CellEventArgs(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }

        public event EventHandler<CellEventArgs> ValueChanged;

        private void OnValueChanged(CellEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public int Row { get; set; }
        public int Col { get; set; }

        public int? MinValue { get; set; } = null;
        public int? MaxValue { get; set; } = null;

        private int sheetsDone = int.MinValue;
        public int SheetsDone
        {
            get
            {
                return sheetsDone;
            }

            set
            {
                if (sheetsDone == value)
                {
                    return;
                }

                if ((MaxValue != null) && (MinValue != null))
                {
                    if (value > MaxValue)
                    {
                        sheetsDone = (int)MaxValue;
                    }
                    else if (value < MinValue)
                    {
                        sheetsDone = (int)MinValue;
                    }
                    else
                    {
                        sheetsDone = value;
                    }
                }
                else
                {
                    sheetsDone = value;
                }

                machineTextBoxSheetsDone.Text = sheetsDone.ToString();
                SetDoneBackgroundColor();
            }
        }

        private int sheetsToDo = int.MinValue;
        public int SheetsToDo
        {
            get
            {
                return sheetsToDo;
            }

            set
            {
                if (sheetsToDo == value)
                {
                    return;
                }

                if ((MaxValue != null) && (MinValue != null))
                {
                    if (value > MaxValue)
                    {
                        sheetsToDo = (int)MaxValue;
                    }
                    else if (value < MinValue)
                    {
                        sheetsToDo = (int)MinValue;
                    }
                    else
                    {
                        sheetsToDo = value;
                    }
                }
                else
                {
                    sheetsToDo = value;
                }

                machineTextBoxSheetsToDo.Text = sheetsToDo.ToString();
                SetDoneBackgroundColor();
            }
        }

        public SpreaderCellTableVisualization()
        {
            InitializeComponent();
        }

        public void SetDoneBackgroundColor()
        {
            if (SheetsDone == 0 && SheetsToDo == 0)
            {
                machineTextBoxSheetsToDo.BackColor = Color.Gray;
            }
            else if (SheetsDone >= SheetsToDo)
            {
                machineTextBoxSheetsToDo.BackColor = Color.Green;
            }
            else
            {
                machineTextBoxSheetsToDo.BackColor = Color.Red;
            }

            Refresh();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.BackColor = Color.Gray;

            //spreaderTextBoxMain.Text = "000";

            //spreaderTextBoxMain.MaxWidth = MainWidth;
            //spreaderTextBoxMain.MaxHeight = MainHeight;
            //spreaderTextBoxMain.Size = new Size(MainWidth, MainHeight);


            machineTextBoxSheetsToDo.BackColor = Color.Green;
            machineTextBoxSheetsToDo.BorderStyle = BorderStyle.FixedSingle;

            //spreaderTextBoxLeft.Text = "000";
            machineTextBoxSheetsDone.BackColor = Color.Gray;
            machineTextBoxSheetsDone.BorderStyle = BorderStyle.None;

            Width = CellWidth;
            Height = CellHeight;

            this.Size = new Size(Width, Height);

            Refresh();
        }

        private void SpreaderCellTable_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent; ;

            machineTextBoxSheetsToDo.BackColor = Color.Transparent;
            machineTextBoxSheetsToDo.BorderStyle = BorderStyle.None;

            machineTextBoxSheetsDone.BorderStyle = BorderStyle.None;
        }

        private void SpreaderCellTable_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(CellWidth, CellHeight);
        }

        private void HandleCellEditEvent(object sender, EventArgs e)
        {
            int value = int.Parse(machineTextBoxSheetsDone.Text);

            using (var keyb = new TouchNumericKeyboard("", value))
            {
                keyb.KeyPlusMinusEnabled = false;
                keyb.KeyCommaEnabled = false;

                keyb.ShowDialog();

                SheetsDone = (int)(keyb.Value);
            }

            OnValueChanged(new CellEventArgs(Row, Col));
        }

        private void spreaderTextBoxMain_DoubleClick(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        private void SpreaderCellTableVisualization_DoubleClick(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        private void spreaderTextBoxSheetsToDo_DoubleClick(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        private void spreaderTextBoxSheetsToDo_Click(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        private void spreaderTextBoxSheetsDone_Click(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        private void SpreaderCellTableVisualization_Click(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }
    }
}
