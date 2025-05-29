using Machine.UI.Controls;

namespace Caron.UI.Controls
{
    partial class SpreaderCellTableVisualization
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

        public override string ToString()
        {
            return $"[SpreaderCellTableVisualization] Done:{machineTextBoxSheetsDone.Text} ToDo:{machineTextBoxSheetsToDo.Text}";
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.machineTextBoxSheetsDone = new Machine.UI.Controls.MachineLabel();
            this.machineTextBoxSheetsToDo = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // machineTextBoxSheetsDone
            // 
            this.machineTextBoxSheetsDone.BackColor = System.Drawing.Color.Transparent;
            this.machineTextBoxSheetsDone.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16F);
            this.machineTextBoxSheetsDone.Location = new System.Drawing.Point(5, 7);
            this.machineTextBoxSheetsDone.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.machineTextBoxSheetsDone.Name = "machineTextBoxSheetsDone";
            this.machineTextBoxSheetsDone.Size = new System.Drawing.Size(79, 44);
            this.machineTextBoxSheetsDone.TabIndex = 0;
            this.machineTextBoxSheetsDone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.machineTextBoxSheetsDone.Click += new System.EventHandler(this.spreaderTextBoxSheetsDone_Click);
            this.machineTextBoxSheetsDone.DoubleClick += new System.EventHandler(this.spreaderTextBoxMain_DoubleClick);
            // 
            // machineTextBoxSheetsToDo
            // 
            this.machineTextBoxSheetsToDo.BackColor = System.Drawing.Color.Transparent;
            this.machineTextBoxSheetsToDo.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.machineTextBoxSheetsToDo.Location = new System.Drawing.Point(89, 7);
            this.machineTextBoxSheetsToDo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.machineTextBoxSheetsToDo.Name = "machineTextBoxSheetsToDo";
            this.machineTextBoxSheetsToDo.Size = new System.Drawing.Size(26, 44);
            this.machineTextBoxSheetsToDo.TabIndex = 1;
            this.machineTextBoxSheetsToDo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.machineTextBoxSheetsToDo.Click += new System.EventHandler(this.spreaderTextBoxSheetsToDo_Click);
            this.machineTextBoxSheetsToDo.DoubleClick += new System.EventHandler(this.spreaderTextBoxSheetsToDo_DoubleClick);
            // 
            // SpreaderCellTableVisualization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.machineTextBoxSheetsDone);
            this.Controls.Add(this.machineTextBoxSheetsToDo);
            this.Name = "SpreaderCellTableVisualization";
            this.Size = new System.Drawing.Size(120, 60);
            this.Load += new System.EventHandler(this.SpreaderCellTable_Load);
            this.Click += new System.EventHandler(this.SpreaderCellTableVisualization_Click);
            this.DoubleClick += new System.EventHandler(this.SpreaderCellTableVisualization_DoubleClick);
            this.Resize += new System.EventHandler(this.SpreaderCellTable_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineLabel machineTextBoxSheetsDone;
        private Machine.UI.Controls.MachineLabel machineTextBoxSheetsToDo;
    }
}
