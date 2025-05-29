namespace Machine.UI.Controls
{
    partial class MachinePropertyNumericEditBox
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
            this.slPropertyName = new System.Windows.Forms.Label();
            this.slPropertyValueYard = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // slPropertyValue
            // 
            this.slPropertyValue.BackColor = System.Drawing.Color.White;
            this.slPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyValue.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.slPropertyValue.Location = new System.Drawing.Point(10, 61);
            this.slPropertyValue.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.slPropertyValue.Name = "slPropertyValue";
            this.slPropertyValue.Size = new System.Drawing.Size(170, 32);
            this.slPropertyValue.TabIndex = 3;
            this.slPropertyValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slPropertyValue.Click += new System.EventHandler(this.slPropertyValue_Click);
            this.slPropertyValue.DoubleClick += new System.EventHandler(this.slPropertyValue_DoubleClick);
            // 
            // slPropertyName
            // 
            this.slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyName.ForeColor = System.Drawing.Color.Black;
            this.slPropertyName.Location = new System.Drawing.Point(0, 0);
            this.slPropertyName.Name = "slPropertyName";
            this.slPropertyName.Size = new System.Drawing.Size(190, 56);
            this.slPropertyName.TabIndex = 5;
            this.slPropertyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // slPropertyValueYard
            // 
            this.slPropertyValueYard.BackColor = System.Drawing.Color.White;
            this.slPropertyValueYard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyValueYard.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.slPropertyValueYard.Location = new System.Drawing.Point(10, 61);
            this.slPropertyValueYard.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.slPropertyValueYard.Name = "slPropertyValueYard";
            this.slPropertyValueYard.Size = new System.Drawing.Size(170, 32);
            this.slPropertyValueYard.TabIndex = 6;
            this.slPropertyValueYard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slPropertyValueYard.Visible = false;
            this.slPropertyValueYard.Click += new System.EventHandler(this.slPropertyValue_Click);
            this.slPropertyValueYard.DoubleClick += new System.EventHandler(this.slPropertyValue_DoubleClick);
            // 
            // MachinePropertyNumericEditBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.slPropertyValueYard);
            this.Controls.Add(this.slPropertyName);
            this.Controls.Add(this.slPropertyValue);
            this.ForeColor = System.Drawing.Color.DarkGray;
            this.Name = "MachinePropertyNumericEditBox";
            this.Size = new System.Drawing.Size(190, 105);
            this.Load += new System.EventHandler(this.SpreaderEditBox_Load);
            this.Click += new System.EventHandler(this.SpreaderPropertyEditBox_Click);
            this.DoubleClick += new System.EventHandler(this.SpreaderPropertyEditBox_DoubleClick);
            this.ResumeLayout(false);

        }

        #endregion
        public Machine.UI.Controls.MachineLabel slPropertyValue;
        private System.Windows.Forms.Label slPropertyName;
        public MachineLabel slPropertyValueYard;
    }
}
