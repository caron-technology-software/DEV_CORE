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
            machinePanelEdgeRounded = new MachinePanelEdgeRounded();
            slPropertyName = new MachineLabel();
            comboBoxPropertyValue = new System.Windows.Forms.ComboBox();
            machinePanelEdgeRounded.SuspendLayout();
            SuspendLayout();
            // 
            // machinePanelEdgeRounded
            // 
            machinePanelEdgeRounded.Controls.Add(slPropertyName);
            machinePanelEdgeRounded.Controls.Add(comboBoxPropertyValue);
            machinePanelEdgeRounded.LineColor = System.Drawing.Color.LightGray;
            machinePanelEdgeRounded.LineWidth = 2;
            machinePanelEdgeRounded.Location = new System.Drawing.Point(0, 0);
            machinePanelEdgeRounded.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            machinePanelEdgeRounded.Name = "machinePanelEdgeRounded";
            machinePanelEdgeRounded.Radius = 4;
            machinePanelEdgeRounded.Size = new System.Drawing.Size(694, 48);
            machinePanelEdgeRounded.TabIndex = 2;
            // 
            // slPropertyName
            // 
            slPropertyName.BackColor = System.Drawing.Color.Transparent;
            slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            slPropertyName.Location = new System.Drawing.Point(8, 11);
            slPropertyName.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            slPropertyName.Name = "slPropertyName";
            slPropertyName.Size = new System.Drawing.Size(510, 30);
            slPropertyName.TabIndex = 0;
            slPropertyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            slPropertyName.Enter += slPropertyName_Enter;
            slPropertyName.Leave += slPropertyName_Leave;
            // 
            // comboBoxPropertyValue
            // 
            comboBoxPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            comboBoxPropertyValue.FormattingEnabled = true;
            comboBoxPropertyValue.Location = new System.Drawing.Point(519, 13);
            comboBoxPropertyValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxPropertyValue.Name = "comboBoxPropertyValue";
            comboBoxPropertyValue.Size = new System.Drawing.Size(171, 30);
            comboBoxPropertyValue.TabIndex = 0;
            // 
            // MachineComboBoxItem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(machinePanelEdgeRounded);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MachineComboBoxItem";
            Size = new System.Drawing.Size(694, 48);
            machinePanelEdgeRounded.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Machine.UI.Controls.MachineLabel slPropertyName;
        private Machine.UI.Controls.MachinePanelEdgeRounded machinePanelEdgeRounded;
        public System.Windows.Forms.ComboBox comboBoxPropertyValue;
    }
}
