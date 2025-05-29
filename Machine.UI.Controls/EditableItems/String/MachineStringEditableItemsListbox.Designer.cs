namespace Machine.UI.Controls
{
    partial class MachineStringEditableItemsListbox
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
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.panelListbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelListbox
            // 
            this.panelListbox.Controls.Add(this.vScrollBar);
            this.panelListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListbox.Location = new System.Drawing.Point(0, 0);
            this.panelListbox.Name = "panelListbox";
            this.panelListbox.Size = new System.Drawing.Size(554, 347);
            this.panelListbox.TabIndex = 0;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Location = new System.Drawing.Point(516, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(35, 347);
            this.vScrollBar.TabIndex = 2;
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // MachineStringEditableItemsListbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelListbox);
            this.Name = "MachineStringEditableItemsListbox";
            this.Size = new System.Drawing.Size(554, 347);
            this.Load += new System.EventHandler(this.MachineStringSettingsListbox_Load);
            this.panelListbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelListbox;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}
