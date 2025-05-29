namespace Machine.UI.Controls
{
    partial class MachineStringEditableItem
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
            this.labelPropertyValue = new Machine.UI.Controls.MachineLabel();
            this.machinePanelEdgeRounded = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.SuspendLayout();
            // 
            // labelPropertyValue
            // 
            this.labelPropertyValue.BackColor = System.Drawing.Color.Transparent;
            this.labelPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPropertyValue.Location = new System.Drawing.Point(7, 6);
            this.labelPropertyValue.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.labelPropertyValue.Name = "labelPropertyValue";
            this.labelPropertyValue.Size = new System.Drawing.Size(406, 30);
            this.labelPropertyValue.TabIndex = 0;
            this.labelPropertyValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelPropertyValue.Click += new System.EventHandler(this.LabelPropertyValue_Click);
            this.labelPropertyValue.DoubleClick += new System.EventHandler(this.LabelPropertyValue_DoubleClick);
            this.labelPropertyValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LabelPropertyValue_MouseDown);
            this.labelPropertyValue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LabelPropertyValue_MouseUp);
            // 
            // machinePanelEdgeRounded
            // 
            this.machinePanelEdgeRounded.LineColor = System.Drawing.Color.LightGray;
            this.machinePanelEdgeRounded.LineWidth = 2;
            this.machinePanelEdgeRounded.Location = new System.Drawing.Point(0, 0);
            this.machinePanelEdgeRounded.Name = "machinePanelEdgeRounded";
            this.machinePanelEdgeRounded.Radius = 4;
            this.machinePanelEdgeRounded.Size = new System.Drawing.Size(420, 42);
            this.machinePanelEdgeRounded.TabIndex = 2;
            // 
            // MachineStringEditableItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelPropertyValue);
            this.Controls.Add(this.machinePanelEdgeRounded);
            this.Name = "MachineStringEditableItem";
            this.Size = new System.Drawing.Size(420, 42);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineLabel labelPropertyValue;
        private Machine.UI.Controls.MachinePanelEdgeRounded machinePanelEdgeRounded;
    }
}
