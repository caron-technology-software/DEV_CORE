namespace Machine.UI.Controls
{
    partial class MachineButtonRectangular
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
            this.machinePanel = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.machineLabel = new Machine.UI.Controls.MachineLabel();
            this.machinePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // machinePanel
            // 
            this.machinePanel.Controls.Add(this.machineLabel);
            this.machinePanel.LineColor = System.Drawing.Color.LightGray;
            this.machinePanel.LineWidth = 3;
            this.machinePanel.Location = new System.Drawing.Point(0, 0);
            this.machinePanel.Name = "machinePanel";
            this.machinePanel.Radius = 5;
            this.machinePanel.Size = new System.Drawing.Size(300, 50);
            this.machinePanel.TabIndex = 0;
            // 
            // machineLabel
            // 
            this.machineLabel.BackColor = System.Drawing.Color.Transparent;
            this.machineLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.machineLabel.Location = new System.Drawing.Point(0, 0);
            this.machineLabel.Name = "machineLabel";
            this.machineLabel.Size = new System.Drawing.Size(300, 50);
            this.machineLabel.TabIndex = 0;
            this.machineLabel.Text = "Text";
            this.machineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.machineLabel.Click += new System.EventHandler(this.machineLabel_Click);
            this.machineLabel.DoubleClick += new System.EventHandler(this.machineLabel_DoubleClick);
            this.machineLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.machineLabel_MouseDown);
            this.machineLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.machineLabel_MouseUp);
            // 
            // MachineButtonRectangular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.machinePanel);
            this.Name = "MachineButtonRectangular";
            this.Size = new System.Drawing.Size(300, 50);
            this.machinePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MachinePanelEdgeRounded machinePanel;
        private MachineLabel machineLabel;
    }
}
