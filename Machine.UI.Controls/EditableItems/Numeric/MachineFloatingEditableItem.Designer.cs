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
            this.slPropertyValue = new Machine.UI.Controls.MachineLabel();
            this.slPropertyName = new Machine.UI.Controls.MachineLabel();
            this.machinePanelEdgeRounded = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.slPropertyValueYard = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // slPropertyValue
            // 
            this.slPropertyValue.BackColor = System.Drawing.Color.LightGray;
            this.slPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyValue.Location = new System.Drawing.Point(444, 6);
            this.slPropertyValue.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.slPropertyValue.Name = "slPropertyValue";
            this.slPropertyValue.Size = new System.Drawing.Size(143, 30);
            this.slPropertyValue.TabIndex = 1;
            this.slPropertyValue.Click += new System.EventHandler(this.slPropertyValue_Click);
            this.slPropertyValue.DoubleClick += new System.EventHandler(this.slPropertyValue_DoubleClick);
            this.slPropertyValue.Enter += new System.EventHandler(this.slPropertyValue_Enter);
            this.slPropertyValue.Leave += new System.EventHandler(this.slPropertyValue_Leave);
            // 
            // slPropertyName
            // 
            this.slPropertyName.BackColor = System.Drawing.Color.Transparent;
            this.slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyName.Location = new System.Drawing.Point(7, 6);
            this.slPropertyName.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.slPropertyName.Name = "slPropertyName";
            this.slPropertyName.Size = new System.Drawing.Size(431, 30);
            this.slPropertyName.TabIndex = 0;
            this.slPropertyName.Click += new System.EventHandler(this.SlPropertyName_Click);
            this.slPropertyName.DoubleClick += new System.EventHandler(this.slPropertyName_DoubleClick);
            this.slPropertyName.Enter += new System.EventHandler(this.slPropertyName_Enter);
            this.slPropertyName.Leave += new System.EventHandler(this.slPropertyName_Leave);
            // 
            // machinePanelEdgeRounded
            // 
            this.machinePanelEdgeRounded.LineColor = System.Drawing.Color.LightGray;
            this.machinePanelEdgeRounded.LineWidth = 2;
            this.machinePanelEdgeRounded.Location = new System.Drawing.Point(0, 0);
            this.machinePanelEdgeRounded.Name = "machinePanelEdgeRounded";
            this.machinePanelEdgeRounded.Radius = 4;
            this.machinePanelEdgeRounded.Size = new System.Drawing.Size(595, 42);
            this.machinePanelEdgeRounded.TabIndex = 2;
            // 
            // slPropertyValueYard
            // 
            this.slPropertyValueYard.BackColor = System.Drawing.Color.LightGray;
            this.slPropertyValueYard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyValueYard.Location = new System.Drawing.Point(444, 6);
            this.slPropertyValueYard.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.slPropertyValueYard.Name = "slPropertyValueYard";
            this.slPropertyValueYard.Size = new System.Drawing.Size(143, 30);
            this.slPropertyValueYard.TabIndex = 3;
            this.slPropertyValueYard.Visible = false;
            this.slPropertyValueYard.Click += new System.EventHandler(this.slPropertyValue_Click);
            this.slPropertyValueYard.DoubleClick += new System.EventHandler(this.slPropertyValue_DoubleClick);
            this.slPropertyValueYard.Enter += new System.EventHandler(this.slPropertyValue_Enter);
            this.slPropertyValueYard.Leave += new System.EventHandler(this.slPropertyValue_Leave);
            // 
            // MachineFloatingEditableItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.slPropertyValueYard);
            this.Controls.Add(this.slPropertyValue);
            this.Controls.Add(this.slPropertyName);
            this.Controls.Add(this.machinePanelEdgeRounded);
            this.Name = "MachineFloatingEditableItem";
            this.Size = new System.Drawing.Size(595, 42);
            this.ResumeLayout(false);

        }
        #endregion

        private Machine.UI.Controls.MachineLabel slPropertyName;
        public Machine.UI.Controls.MachineLabel slPropertyValue;
        private Machine.UI.Controls.MachinePanelEdgeRounded machinePanelEdgeRounded;
        public MachineLabel slPropertyValueYard;
    }
}
