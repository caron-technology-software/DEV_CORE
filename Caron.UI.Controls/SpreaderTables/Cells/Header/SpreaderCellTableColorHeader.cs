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
    public partial class SpreaderCellTableColorHeader : UserControl
    {
        private string id = String.Empty;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                slColorId.Text = id;
            }
        }

        private string code = String.Empty;
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                slColorCode.Text = code;
            }
        }

        public SpreaderCellTableColorHeader()
        {
            InitializeComponent();
        }

        public void SetTexts(string id, string code)
        {
            Id = id;
            Code = code;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            Width = SpreaderCellTableProgramming.CellWidth;
            Height = SpreaderCellTableProgramming.CellHeight;
            Size = new Size(SpreaderCellTableProgramming.CellWidth, SpreaderCellTableProgramming.CellHeight);

            slColorId.Text = string.Empty;
            slColorCode.Text = string.Empty;

            Refresh();
        }

        private void SpreaderCellTableHeader_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(SpreaderCellTableProgramming.CellWidth, SpreaderCellTableProgramming.CellHeight);
        }
    }
}
