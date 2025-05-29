using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Caron.UI.Controls
{
    public partial class SpreaderTableVisualizationInternal : UserControl
    {
        public uint EnableImperialYard { get; set; } = 0;

        public const int SpaceBetweenCells = 5;

        public int MinValue { get; set; } = int.MinValue;
        public int MaxValue { get; set; } = int.MaxValue;

        public int NumberOfRows { get; private set; } = 6;
        public int NumberOfColums { get; private set; } = 7;
        public int NumberOfElements { get => NumberOfRows * NumberOfColums; }

        public event EventHandler<SpreaderCellTableVisualization.CellEventArgs> TableCellChanged;

        private void OnTableChanged(SpreaderCellTableVisualization.CellEventArgs e)
        {
            TableCellChanged?.Invoke(this, e);
        }

        public ref SpreaderCellTableVisualization GetCell(int row, int col)
        {
            return ref Cells[GetIndexFromPosition(row, col)];
        }

        public int GetIndexFromPosition(int row, int col)
        {
            int index = NumberOfColums * row + col;

            if (index < 0 || index >= NumberOfRows * NumberOfColums)
            {
                return 0;
            }

            return index;
        }

        public SpreaderTableVisualizationInternal()
        {
            Console.WriteLine("SpreaderTableVisualizationInternal()");
            InitializeComponent();
        }

        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public SpreaderCellTableVisualization[] Cells { get; set; } = new SpreaderCellTableVisualization[Constants.SpreaderWorkingTable.NumberOfRows * Constants.SpreaderWorkingTable.NumberOfColums];

        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public SpreaderCellTableColorHeader[] Colors { get; set; } = new SpreaderCellTableColorHeader[Constants.SpreaderWorkingTable.NumberOfRows];

        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public SpreaderCellTableMarkerHeader[] Markers { get; set; } = new SpreaderCellTableMarkerHeader[Constants.SpreaderWorkingTable.NumberOfColums];

        public void SetEnableImperialYard()   //GPIx258  onVisibleChange!
        {
            for (int col = 0; col < NumberOfColums; col++)
            {
                if (Markers != null)
                {
                    var m = Markers[col];
                    //GPIx258 abilita imperial yard nella programming table
                    if (EnableImperialYard == 1)
                    {
                        m.IsImperialYard = true;
                        m.NeedYard = true;
                        m.slMarkerLength.Visible = false;
                        m.slMarkerLengthYard.Visible = true;
                    }
                    else
                    {
                        m.IsImperialYard = false;
                        m.NeedYard = true;
                        m.slMarkerLength.Visible = true;
                        m.slMarkerLengthYard.Visible = false;
                    }
                    //GPFx258
                }
            }
        }

        public void Initialize()
        {
            Console.WriteLine("SpreaderTableVisualizationInternal:Initialize()");

            this.SuspendLayout();

            panelTable.Location = new Point(0, 0);

            int k = 0;
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColums; col++)
                {
                    Cells[k] = new SpreaderCellTableVisualization();
                    var c = Cells[k];

                    c.Location = new Point((col + 1) * (c.Width + SpaceBetweenCells), (row + 1) * (c.Height + SpaceBetweenCells));
                    c.TabStop = false;

                    c.MinValue = MinValue;
                    c.MaxValue = MaxValue;

                    //Position (table)
                    c.Row = row;
                    c.Col = col;

                    k++;
                }
            }

            for (int row = 0; row < NumberOfRows; row++)
            {
                Colors[row] = new SpreaderCellTableColorHeader();
                var c = Colors[row];
                c.Location = new Point(0, (row + 1) * (c.Height + SpaceBetweenCells));
                c.SetTexts("id", "code");
            }

            for (int col = 0; col < NumberOfColums; col++)
            {
                Markers[col] = new SpreaderCellTableMarkerHeader();
                var m = Markers[col];
                m.Location = new Point((col + 1) * (m.Width + SpaceBetweenCells), 0);
                m.Index = col;
                m.SetTexts("id", "name", -1);
                //GPIx258 abilita imperial yard nella programming table
                if (EnableImperialYard == 1)
                {
                    m.IsImperialYard = true;
                    m.NeedYard = true;
                }
                else
                {
                    m.IsImperialYard = false;
                    m.NeedYard = true;
                }
                //GPFx258
            }

            panelTable.Location = new Point(0, 0);
            panelTable.Controls.AddRange(Markers);
            panelTable.Controls.AddRange(Colors);
            panelTable.Controls.AddRange(Cells);

            panelTable.Width = (NumberOfColums + 1) * (SpreaderCellTableVisualization.CellWidth + SpaceBetweenCells) - SpaceBetweenCells;
            panelTable.Height = (NumberOfRows + 1) * (SpreaderCellTableVisualization.CellHeight + SpaceBetweenCells) - SpaceBetweenCells;

            this.Width = panelTable.Width;
            this.Height = panelTable.Height;
            this.Size = new Size(panelTable.Width, panelTable.Height);

            //Events
            k = 0;
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColums; col++)
                {
                    var c = Cells[k];

                    SetCellEvent(c, row, col);

                    k++;
                }
            }

            this.ResumeLayout();
        }

        private void CellModified(object sender, SpreaderCellTableVisualization.CellEventArgs e)
        {
            OnTableChanged(e);
        }

        private void SetCellEvent(SpreaderCellTableVisualization spreaderCellTable, int row, int col)
        {
            spreaderCellTable.ValueChanged += (sender, e) => CellModified(sender, new SpreaderCellTableVisualization.CellEventArgs(row, col));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[SpreaderTableVisualizationInternal]");

            for (int row = 0; row < NumberOfRows; row++)
            {
                sb.AppendLine($"\t{Colors[row].ToString()}");
            }

            for (int col = 0; col < NumberOfColums; col++)
            {
                sb.AppendLine($"\t{Markers[col].ToString()}");
            }

            int k = 0;
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColums; col++)
                {
                    sb.AppendLine($"\t{Cells[k].ToString()}");

                    k++;
                }
            }

            return sb.ToString();
        }
    }
}
