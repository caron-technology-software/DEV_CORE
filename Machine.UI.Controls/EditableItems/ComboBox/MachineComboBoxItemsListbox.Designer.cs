namespace Machine.UI.Controls
{
    partial class MachineComboBoxItemsListbox
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
            this.panelListbox = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelListbox
            // 
            this.panelListbox.Location = new System.Drawing.Point(0, 0);
            this.panelListbox.Name = "panelListbox";
            this.panelListbox.Size = new System.Drawing.Size(97, 73);
            this.panelListbox.TabIndex = 0;
            // 
            // MachineSettingsListbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelListbox);
            this.Name = "MachineSettingsListbox";
            this.Size = new System.Drawing.Size(533, 444);
            this.Load += new System.EventHandler(this.SpreaderSettingsTable_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panelListbox;
    }
}
