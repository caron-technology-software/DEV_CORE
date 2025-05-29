namespace Machine.UI.Controls
{
    partial class MachineEditableItemsListbox
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
            this.panelListbox.Size = new System.Drawing.Size(478, 168);
            this.panelListbox.TabIndex = 0;
            this.panelListbox.Paint += new System.Windows.Forms.PaintEventHandler(this.panelListbox_Paint);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Location = new System.Drawing.Point(446, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(35, 168);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // MachineEditableItemsListbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelListbox);
            this.Name = "MachineEditableItemsListbox";
            this.Size = new System.Drawing.Size(478, 168);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MachineFloatingEditableItemsListbox_Paint);
            this.Resize += new System.EventHandler(this.MachineEditableItemsListbox_Resize);
            this.panelListbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelListbox;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}
