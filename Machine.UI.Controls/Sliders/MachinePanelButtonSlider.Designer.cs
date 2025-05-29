namespace Machine.UI.Controls
{
    partial class MachinePanelButtonSlider
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachinePanelButtonSlider));
            this.mlSliderValue = new Machine.UI.Controls.MachineLabel();
            this.mbSlider = new Machine.UI.Controls.MachineButtonSlider();
            this.SuspendLayout();
            // 
            // mlSliderValue
            // 
            this.mlSliderValue.BackColor = System.Drawing.Color.Transparent;
            this.mlSliderValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlSliderValue.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mlSliderValue.Location = new System.Drawing.Point(0, 139);
            this.mlSliderValue.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.mlSliderValue.Name = "mlSliderValue";
            this.mlSliderValue.Size = new System.Drawing.Size(140, 53);
            this.mlSliderValue.TabIndex = 1;
            this.mlSliderValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mlSliderValue.Click += new System.EventHandler(this.mlSliderValue_Click);
            // 
            // mbSlider
            // 
            this.mbSlider.BackColor = System.Drawing.Color.Transparent;
            this.mbSlider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbSlider.BackgroundImage")));
            this.mbSlider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbSlider.FlatAppearance.BorderSize = 0;
            this.mbSlider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbSlider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbSlider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbSlider.ForeColor = System.Drawing.Color.Transparent;
            this.mbSlider.Location = new System.Drawing.Point(7, 6);
            this.mbSlider.MaxValue = 100F;
            this.mbSlider.MinValue = 0F;
            this.mbSlider.Name = "mbSlider";
            this.mbSlider.PropertyName = "";
            this.mbSlider.Size = new System.Drawing.Size(125, 125);
            this.mbSlider.TabIndex = 0;
            this.mbSlider.UseVisualStyleBackColor = false;
            this.mbSlider.Value = 0F;
            this.mbSlider.ValueChangedEventEnabled = false;
            // 
            // MachinePanelButtonSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mlSliderValue);
            this.Controls.Add(this.mbSlider);
            this.Name = "MachinePanelButtonSlider";
            this.Size = new System.Drawing.Size(140, 195);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineButtonSlider mbSlider;
        private Machine.UI.Controls.MachineLabel mlSliderValue;
    }
}
