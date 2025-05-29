using Machine.UI.Controls;

namespace Caron.UI.Controls
{
    partial class SpreaderCellTableColorHeader
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
            return $"[SpreaderCellTableColorHeader] ColorId:{slColorId.Text} ColorCode:{slColorCode.Text}";
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.slColorId = new Machine.UI.Controls.MachineLabel();
            this.slColorCode = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // slColorId
            // 
            this.slColorId.BackColor = System.Drawing.Color.Transparent;
            this.slColorId.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slColorId.Location = new System.Drawing.Point(0, 6);
            this.slColorId.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.slColorId.Name = "slColorId";
            this.slColorId.Size = new System.Drawing.Size(120, 18);
            this.slColorId.TabIndex = 0;
            this.slColorId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // slColorCode
            // 
            this.slColorCode.BackColor = System.Drawing.Color.Transparent;
            this.slColorCode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slColorCode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.slColorCode.Location = new System.Drawing.Point(0, 33);
            this.slColorCode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.slColorCode.Name = "slColorCode";
            this.slColorCode.Size = new System.Drawing.Size(120, 18);
            this.slColorCode.TabIndex = 1;
            this.slColorCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SpreaderCellTableColorHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.slColorCode);
            this.Controls.Add(this.slColorId);
            this.Name = "SpreaderCellTableColorHeader";
            this.Size = new System.Drawing.Size(120, 60);
            this.Resize += new System.EventHandler(this.SpreaderCellTableHeader_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineLabel slColorId;
        private Machine.UI.Controls.MachineLabel slColorCode;
    }
}
