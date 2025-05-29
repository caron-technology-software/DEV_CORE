using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Machine.UI.Controls;

//GPIx231 XX
using Caron.Spreader;
//GPFx231 XX

namespace Caron.UI.Controls
{
    public partial class SpreaderCellTableProgramming : UserControl
    {

        public const int CellWidth = 120;
        public const int CellHeight = 60;

        //GPIx231 XX
        public bool EnableCellValueModification { get; set; } = true;
        //GPFx231 XX

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

        //public static readonly int MainWidth = 63;
        //public static readonly int MainHeight = 46;

        public int? MinValue { get; set; } = null;
        public int? MaxValue { get; set; } = null;

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
            }
        }

        public SpreaderCellTableProgramming()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.BackColor = Color.Gray;

            machineTextBoxSheetsToDo.BackColor = Color.Gray;
            machineTextBoxSheetsToDo.BorderStyle = BorderStyle.None;

            Width = CellWidth;
            Height = CellHeight;

            this.Size = new Size(Width, Height);

            Refresh();
        }

        private void SpreaderCellTable_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;

            machineTextBoxSheetsToDo.BackColor = Color.Transparent;
            machineTextBoxSheetsToDo.BorderStyle = BorderStyle.None;
        }

        private void SpreaderCellTable_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(CellWidth, CellHeight);
        }

        private void HandleCellEditEvent(object sender, EventArgs e)
        {
            //GPIx231 XX
            if (EnableCellValueModification == false)
            {
                var msgBox = new MachineMessageBox(Localization.Warning, Localization.NotAllowed);
                msgBox.ShowDialog();
                return;
            }
            //GPFx231 XX

            int value = int.Parse(machineTextBoxSheetsToDo.Text);

            using (var keyb = new TouchNumericKeyboard("", value))
            {
                keyb.KeyPlusMinusEnabled = false;
                keyb.KeyCommaEnabled = false;

                keyb.ShowDialog();

                SheetsToDo = (int)(keyb.Value);
            }

            OnValueChanged(new CellEventArgs(Row, Col));
        }

        private void spreaderTextSheetsToDo_DoubleClick(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        private void SpreaderCellTableProgramming_DoubleClick(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        private void spreaderTextBoxSheetsToDo_Click(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        private void SpreaderCellTableProgramming_Click(object sender, EventArgs e)
        {
            HandleCellEditEvent(sender, e);
        }

        public override string ToString()
        {
            return $"[SpreaderCellTableProgramming] SheetToDo:{machineTextBoxSheetsToDo.Text}";
        }
    }
}
