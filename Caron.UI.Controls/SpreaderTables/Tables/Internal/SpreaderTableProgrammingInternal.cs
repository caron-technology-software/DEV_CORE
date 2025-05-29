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
    public partial class SpreaderTableProgrammingInternal : UserControl
    {
        public uint EnableImperialYard { get; set; } = 0;
        public string ResourceManagerMarkerChange { get; set; } = string.Empty;

        public int MinValue { get; set; } = int.MinValue;
        public int MaxValue { get; set; } = int.MaxValue;

        public int NumberOfRows { get => Constants.SpreaderWorkingTable.NumberOfRows; }
        public int NumberOfColums { get => Constants.SpreaderWorkingTable.NumberOfColums; }
        public int NumberOfElements { get => NumberOfRows * NumberOfColums; }

        public int SpaceBetweenCells { get => Constants.SpreaderWorkingTable.SpaceBetweenCells; }

        public event EventHandler<SpreaderCellTableProgramming.CellEventArgs> TableCellChanged;
        public event EventHandler<SpreaderCellTableMarkerHeader.MarkerHeaderEventArgs> MarkerChanged;

        private void OnTableChanged(SpreaderCellTableProgramming.CellEventArgs e)
        {
            TableCellChanged?.Invoke(this, e);
        }

        private void OnMarkerChanged(SpreaderCellTableMarkerHeader.MarkerHeaderEventArgs e)
        {
            MarkerChanged?.Invoke(this, e);
        }

        public ref SpreaderCellTableProgramming GetCell(int row, int col)
        {
            return ref Cells[GetIndexFromPosition(row, col)];
        }

        public ref SpreaderCellTableMarkerHeader GetMarker(int index)
        {
            return ref Markers[index];
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

        public void EnableOnlyFirstMarkerLengthModification()
        {
            foreach (var m in Markers)
            {
                m.EnableLengthModification = false;
            }

            Markers[0].EnableLengthModification = true;
        }

        public void EnableMarkersLengthModification(bool value)
        {
            foreach (var m in Markers)
            {
                m.EnableLengthModification = value;
            }
        }

        //GPIx231 XX
        public void EnableCellModification(bool value)
        {
            foreach (var m in Cells)
            {
                m.EnableCellValueModification = value;
            }
        }
        //GPFx231 XX

        public SpreaderTableProgrammingInternal()
        {
            Console.WriteLine("SpreaderTableProgrammingInternal");
            InitializeComponent();
        }

        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public SpreaderCellTableProgramming[] Cells { get; set; } = new SpreaderCellTableProgramming[Constants.SpreaderWorkingTable.NumberOfRows * Constants.SpreaderWorkingTable.NumberOfColums];

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
            Console.WriteLine("SpreaderTableProgrammingInternal:Initialize()");

            this.SuspendLayout();

            panelTable.Location = new Point(0, 0);

            int k = 0;
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColums; col++)
                {
                    Cells[k] = new SpreaderCellTableProgramming();
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
                m.EnableLengthModification = true;
                m.SetTexts("id", "name", -1);
                m.ResourceMarkerChangeChecker = ResourceManagerMarkerChange;
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

            panelTable.Width = (NumberOfColums + 1) * (SpreaderCellTableProgramming.CellWidth + SpaceBetweenCells) - SpaceBetweenCells;
            panelTable.Height = (NumberOfRows + 1) * (SpreaderCellTableProgramming.CellHeight + SpaceBetweenCells) - SpaceBetweenCells;

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

            for (int col = 0; col < NumberOfColums; col++)
            {
                var m = Markers[col];
                SetMarkerHeaderEvent(m, col);
            }

            this.ResumeLayout();
        }

        private void CellModified(object sender, SpreaderCellTableProgramming.CellEventArgs e)
        {
            OnTableChanged(e);
        }

        private void MarkerModified(object sender, SpreaderCellTableMarkerHeader.MarkerHeaderEventArgs e)
        {
            OnMarkerChanged(e);
        }

        private void SetMarkerHeaderEvent(SpreaderCellTableMarkerHeader marker, int index)
        {
            marker.MarkerChanged += (sender, e) => MarkerModified(sender, new SpreaderCellTableMarkerHeader.MarkerHeaderEventArgs(index));
        }

        private void SetCellEvent(SpreaderCellTableProgramming spreaderCellTable, int row, int col)
        {
            spreaderCellTable.ValueChanged += (sender, e) => CellModified(sender, new SpreaderCellTableProgramming.CellEventArgs(row, col));
        }

        public string GetMarkersString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[SpreaderTableProgrammingInternal (Markers)]");

            for (int col = 0; col < NumberOfColums; col++)
            {
                sb.AppendLine($"\t{Markers[col].ToString()}");
            }

            return sb.ToString();
        }

        public string GetTableContentString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[SpreaderTableProgrammingInternal (Table)]");

            int k = 0;
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfColums; col++)
                {
                    sb.AppendLine($"\t{Cells[k].ToString()}");
                }

                k++;
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[SpreaderTableProgrammingInternal]");

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
                }

                k++;
            }

            return sb.ToString();
        }
    }
}
