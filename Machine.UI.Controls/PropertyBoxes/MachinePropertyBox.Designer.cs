namespace Machine.UI.Controls
{
    partial class MachinePropertyBox
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
            this.slPropertyName = new System.Windows.Forms.Label();
            this.slPropertyValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // slPropertyName
            // 
            this.slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyName.Location = new System.Drawing.Point(0, 0);
            this.slPropertyName.Name = "slPropertyName";
            this.slPropertyName.Size = new System.Drawing.Size(200, 50);
            this.slPropertyName.TabIndex = 5;
            this.slPropertyName.Text = "PropertyName";
            this.slPropertyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // slPropertyValue
            // 
            this.slPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyValue.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.slPropertyValue.Location = new System.Drawing.Point(0, 54);
            this.slPropertyValue.Name = "slPropertyValue";
            this.slPropertyValue.Size = new System.Drawing.Size(200, 28);
            this.slPropertyValue.TabIndex = 6;
            this.slPropertyValue.Text = "Value";
            this.slPropertyValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MachinePropertyBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slPropertyValue);
            this.Controls.Add(this.slPropertyName);
            this.Name = "MachinePropertyBox";
            this.Size = new System.Drawing.Size(200, 85);
            this.Load += new System.EventHandler(this.SpreaderPropertyBox_Load);
            this.SizeChanged += new System.EventHandler(this.MachinePropertyBox_SizeChanged);
            this.Resize += new System.EventHandler(this.SpreaderPropertyBox_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label slPropertyName;
        private System.Windows.Forms.Label slPropertyValue;
    }
}
