using Machine.UI.Controls;

namespace Caron.UI.Controls
{
    partial class SpreaderCellTableProgramming
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.machineTextBoxSheetsToDo = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // machineTextBoxSheetsToDo
            // 
            this.machineTextBoxSheetsToDo.BackColor = System.Drawing.Color.Transparent;
            this.machineTextBoxSheetsToDo.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.machineTextBoxSheetsToDo.Location = new System.Drawing.Point(15, 9);
            this.machineTextBoxSheetsToDo.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.machineTextBoxSheetsToDo.Name = "machineTextBoxSheetsToDo";
            this.machineTextBoxSheetsToDo.Size = new System.Drawing.Size(90, 40);
            this.machineTextBoxSheetsToDo.TabIndex = 0;
            this.machineTextBoxSheetsToDo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.machineTextBoxSheetsToDo.Click += new System.EventHandler(this.spreaderTextBoxSheetsToDo_Click);
            this.machineTextBoxSheetsToDo.DoubleClick += new System.EventHandler(this.spreaderTextSheetsToDo_DoubleClick);
            // 
            // SpreaderCellTableProgramming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.machineTextBoxSheetsToDo);
            this.Name = "SpreaderCellTableProgramming";
            this.Size = new System.Drawing.Size(120, 60);
            this.Load += new System.EventHandler(this.SpreaderCellTable_Load);
            this.Click += new System.EventHandler(this.SpreaderCellTableProgramming_Click);
            this.DoubleClick += new System.EventHandler(this.SpreaderCellTableProgramming_DoubleClick);
            this.Resize += new System.EventHandler(this.SpreaderCellTable_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineLabel machineTextBoxSheetsToDo;
    }
}
