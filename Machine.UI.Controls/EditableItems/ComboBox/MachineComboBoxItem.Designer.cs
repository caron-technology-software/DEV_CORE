namespace Machine.UI.Controls
{
    partial class MachineComboBoxItem
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
            this.machinePanelEdgeRounded = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.slPropertyName = new Machine.UI.Controls.MachineLabel();
            this.comboBoxPropertyValue = new System.Windows.Forms.ComboBox();
            this.machinePanelEdgeRounded.SuspendLayout();
            this.SuspendLayout();
            // 
            // machinePanelEdgeRounded
            // 
            this.machinePanelEdgeRounded.Controls.Add(this.slPropertyName);
            this.machinePanelEdgeRounded.Controls.Add(this.comboBoxPropertyValue);
            this.machinePanelEdgeRounded.LineColor = System.Drawing.Color.LightGray;
            this.machinePanelEdgeRounded.LineWidth = 2;
            this.machinePanelEdgeRounded.Location = new System.Drawing.Point(0, 0);
            this.machinePanelEdgeRounded.Name = "machinePanelEdgeRounded";
            this.machinePanelEdgeRounded.Radius = 4;
            this.machinePanelEdgeRounded.Size = new System.Drawing.Size(595, 42);
            this.machinePanelEdgeRounded.TabIndex = 2;
            // 
            // slPropertyName
            // 
            this.slPropertyName.BackColor = System.Drawing.Color.Transparent;
            this.slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyName.Location = new System.Drawing.Point(7, 10);
            this.slPropertyName.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.slPropertyName.Name = "slPropertyName";
            this.slPropertyName.Size = new System.Drawing.Size(380, 26);
            this.slPropertyName.TabIndex = 0;
            this.slPropertyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.slPropertyName.Enter += new System.EventHandler(this.slPropertyName_Enter);
            this.slPropertyName.Leave += new System.EventHandler(this.slPropertyName_Leave);
            // 
            // comboBoxPropertyValue
            // 
            this.comboBoxPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPropertyValue.FormattingEnabled = true;
            this.comboBoxPropertyValue.Location = new System.Drawing.Point(397, 6);
            this.comboBoxPropertyValue.Name = "comboBoxPropertyValue";
            this.comboBoxPropertyValue.Size = new System.Drawing.Size(189, 30);
            this.comboBoxPropertyValue.TabIndex = 0;
            // 
            // MachineComboBoxItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.machinePanelEdgeRounded);
            this.Name = "MachineComboBoxItem";
            this.Size = new System.Drawing.Size(595, 42);
            this.machinePanelEdgeRounded.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private Machine.UI.Controls.MachineLabel slPropertyName;
        private Machine.UI.Controls.MachinePanelEdgeRounded machinePanelEdgeRounded;
        public System.Windows.Forms.ComboBox comboBoxPropertyValue;
    }
}
