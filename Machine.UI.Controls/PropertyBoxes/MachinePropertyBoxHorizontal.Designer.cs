namespace Machine.UI.Controls
{
    partial class MachinePropertyBoxHorizontal
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
            this.slPropertyValue = new MachineLabel();
            this.slPropertyName = new MachineLabel();
            this.SuspendLayout();
            // 
            // slPropertyValue
            // 
            this.slPropertyValue.BackColor = System.Drawing.Color.Transparent;
            this.slPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F);
            this.slPropertyValue.ForeColor = System.Drawing.Color.DimGray;
            this.slPropertyValue.Location = new System.Drawing.Point(451, 3);
            this.slPropertyValue.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.slPropertyValue.Name = "slPropertyValue";
            this.slPropertyValue.Size = new System.Drawing.Size(185, 35);
            this.slPropertyValue.TabIndex = 6;
            this.slPropertyValue.TabStop = false;
            // 
            // slPropertyName
            // 
            this.slPropertyName.BackColor = System.Drawing.Color.Transparent;
            this.slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F);
            this.slPropertyName.Location = new System.Drawing.Point(4, 3);
            this.slPropertyName.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.slPropertyName.Name = "slPropertyName";
            this.slPropertyName.Size = new System.Drawing.Size(435, 35);
            this.slPropertyName.TabIndex = 5;
            this.slPropertyName.TabStop = false;
            // 
            // MachinePropertyBoxHorizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slPropertyValue);
            this.Controls.Add(this.slPropertyName);
            this.Name = "MachinePropertyBoxHorizontal";
            this.Size = new System.Drawing.Size(640, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineLabel slPropertyValue;
        private Machine.UI.Controls.MachineLabel slPropertyName;
    }
}
