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
            panelListbox = new System.Windows.Forms.Panel();
            vScrollBar = new System.Windows.Forms.VScrollBar();
            panelListbox.SuspendLayout();
            SuspendLayout();
            // 
            // panelListbox
            // 
            panelListbox.Controls.Add(vScrollBar);
            panelListbox.Location = new System.Drawing.Point(0, 0);
            panelListbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelListbox.Name = "panelListbox";
            panelListbox.Size = new System.Drawing.Size(492, 194);
            panelListbox.TabIndex = 0;
            panelListbox.Paint += panelListbox_Paint;
            // 
            // vScrollBar
            // 
            vScrollBar.Location = new System.Drawing.Point(457, 0);
            vScrollBar.Name = "vScrollBar";
            vScrollBar.Size = new System.Drawing.Size(35, 194);
            vScrollBar.TabIndex = 1;
            vScrollBar.ValueChanged += vScrollBar_ValueChanged;
            // 
            // MachineEditableItemsListbox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelListbox);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MachineEditableItemsListbox";
            Size = new System.Drawing.Size(493, 194);
            Paint += MachineFloatingEditableItemsListbox_Paint;
            Resize += MachineEditableItemsListbox_Resize;
            panelListbox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelListbox;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}
