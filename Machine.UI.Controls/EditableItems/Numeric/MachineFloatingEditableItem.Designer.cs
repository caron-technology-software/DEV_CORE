namespace Machine.UI.Controls
{
    partial class MachineFloatingEditableItem
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
            slPropertyValue = new MachineLabel();
            slPropertyValueYard = new MachineLabel();
            machinePanelEdgeRounded = new MachinePanelEdgeRounded();
            slPropertyName = new MachineLabel();
            machinePanelEdgeRounded.SuspendLayout();
            SuspendLayout();
            // 
            // slPropertyValue
            // 
            slPropertyValue.BackColor = System.Drawing.Color.LightGray;
            slPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            slPropertyValue.Location = new System.Drawing.Point(426, 7);
            slPropertyValue.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            slPropertyValue.Name = "slPropertyValue";
            slPropertyValue.Size = new System.Drawing.Size(167, 35);
            slPropertyValue.TabIndex = 1;
            slPropertyValue.Click += slPropertyValue_Click;
            slPropertyValue.DoubleClick += slPropertyValue_DoubleClick;
            slPropertyValue.Enter += slPropertyValue_Enter;
            slPropertyValue.Leave += slPropertyValue_Leave;
            // 
            // slPropertyValueYard
            // 
            slPropertyValueYard.BackColor = System.Drawing.Color.LightGray;
            slPropertyValueYard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            slPropertyValueYard.Location = new System.Drawing.Point(426, 7);
            slPropertyValueYard.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            slPropertyValueYard.Name = "slPropertyValueYard";
            slPropertyValueYard.Size = new System.Drawing.Size(167, 35);
            slPropertyValueYard.TabIndex = 3;
            slPropertyValueYard.Visible = false;
            slPropertyValueYard.Click += slPropertyValue_Click;
            slPropertyValueYard.DoubleClick += slPropertyValue_DoubleClick;
            slPropertyValueYard.Enter += slPropertyValue_Enter;
            slPropertyValueYard.Leave += slPropertyValue_Leave;
            // 
            // machinePanelEdgeRounded
            // 
            machinePanelEdgeRounded.Controls.Add(slPropertyValue);
            machinePanelEdgeRounded.Controls.Add(slPropertyValueYard);
            machinePanelEdgeRounded.LineColor = System.Drawing.Color.LightGray;
            machinePanelEdgeRounded.LineWidth = 2;
            machinePanelEdgeRounded.Location = new System.Drawing.Point(0, 0);
            machinePanelEdgeRounded.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            machinePanelEdgeRounded.Name = "machinePanelEdgeRounded";
            machinePanelEdgeRounded.Radius = 4;
            machinePanelEdgeRounded.Size = new System.Drawing.Size(601, 48);
            machinePanelEdgeRounded.TabIndex = 2;
            // 
            // slPropertyName
            // 
            slPropertyName.BackColor = System.Drawing.Color.Transparent;
            slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            slPropertyName.Location = new System.Drawing.Point(8, 7);
            slPropertyName.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            slPropertyName.Name = "slPropertyName";
            slPropertyName.Size = new System.Drawing.Size(410, 35);
            slPropertyName.TabIndex = 0;
            slPropertyName.Click += SlPropertyName_Click;
            slPropertyName.DoubleClick += slPropertyName_DoubleClick;
            slPropertyName.Enter += slPropertyName_Enter;
            slPropertyName.Leave += slPropertyName_Leave;
            // 
            // MachineFloatingEditableItem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(slPropertyName);
            Controls.Add(machinePanelEdgeRounded);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MachineFloatingEditableItem";
            Size = new System.Drawing.Size(630, 48);
            machinePanelEdgeRounded.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public MachineLabel slPropertyValue;
        public MachineLabel slPropertyValueYard;
        private MachinePanelEdgeRounded machinePanelEdgeRounded;
        private MachineLabel slPropertyName;
    }
}
