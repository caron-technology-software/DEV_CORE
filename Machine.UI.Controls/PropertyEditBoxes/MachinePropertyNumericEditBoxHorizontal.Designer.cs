namespace Machine.UI.Controls
{
    partial class MachinePropertyNumericEditBoxHorizontal
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
            this.slPropertyName = new Machine.UI.Controls.MachineLabel();
            this.slPropertyValue = new Machine.UI.Controls.MachineLabel();
            this.slPropertyValueYard = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // slPropertyName
            // 
            this.slPropertyName.BackColor = System.Drawing.Color.Transparent;
            this.slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyName.Location = new System.Drawing.Point(6, 10);
            this.slPropertyName.Margin = new System.Windows.Forms.Padding(6);
            this.slPropertyName.Name = "slPropertyName";
            this.slPropertyName.Size = new System.Drawing.Size(399, 32);
            this.slPropertyName.TabIndex = 4;
            this.slPropertyName.Click += new System.EventHandler(this.SlPropertyName_Click);
            this.slPropertyName.DoubleClick += new System.EventHandler(this.slPropertyName_DoubleClick);
            this.slPropertyName.Enter += new System.EventHandler(this.slPropertyName_Enter);
            this.slPropertyName.Leave += new System.EventHandler(this.slPropertyName_Leave);
            // 
            // slPropertyValue
            // 
            this.slPropertyValue.BackColor = System.Drawing.Color.White;
            this.slPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyValue.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.slPropertyValue.Location = new System.Drawing.Point(417, 5);
            this.slPropertyValue.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.slPropertyValue.Name = "slPropertyValue";
            this.slPropertyValue.Size = new System.Drawing.Size(154, 41);
            this.slPropertyValue.TabIndex = 3;
            this.slPropertyValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slPropertyValue.Click += new System.EventHandler(this.SlPropertyValue_Click);
            this.slPropertyValue.DoubleClick += new System.EventHandler(this.slPropertyValue_DoubleClick);
            this.slPropertyValue.Enter += new System.EventHandler(this.slPropertyValue_Enter);
            this.slPropertyValue.Leave += new System.EventHandler(this.slPropertyValue_Leave);
            // 
            // slPropertyValueYard
            // 
            this.slPropertyValueYard.BackColor = System.Drawing.Color.White;
            this.slPropertyValueYard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyValueYard.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.slPropertyValueYard.Location = new System.Drawing.Point(417, 5);
            this.slPropertyValueYard.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.slPropertyValueYard.Name = "slPropertyValueYard";
            this.slPropertyValueYard.Size = new System.Drawing.Size(154, 41);
            this.slPropertyValueYard.TabIndex = 5;
            this.slPropertyValueYard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slPropertyValueYard.Visible = false;
            this.slPropertyValueYard.Click += new System.EventHandler(this.SlPropertyValue_Click);
            this.slPropertyValueYard.DoubleClick += new System.EventHandler(this.slPropertyValue_DoubleClick);
            this.slPropertyValueYard.Enter += new System.EventHandler(this.slPropertyValue_Enter);
            this.slPropertyValueYard.Leave += new System.EventHandler(this.slPropertyValue_Leave);
            // 
            // MachinePropertyNumericEditBoxHorizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.slPropertyValueYard);
            this.Controls.Add(this.slPropertyName);
            this.Controls.Add(this.slPropertyValue);
            this.Name = "MachinePropertyNumericEditBoxHorizontal";
            this.Size = new System.Drawing.Size(577, 51);
            this.Load += new System.EventHandler(this.SpreaderEditBoxHorizontal_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public Machine.UI.Controls.MachineLabel slPropertyValue;
        private Machine.UI.Controls.MachineLabel slPropertyName;
        public MachineLabel slPropertyValueYard;
    }
}
