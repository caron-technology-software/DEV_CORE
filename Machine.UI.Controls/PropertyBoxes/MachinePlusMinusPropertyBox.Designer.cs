namespace Machine.UI.Controls
{
    partial class MachinePlusMinusPropertyBox
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
            this.panel = new System.Windows.Forms.Panel();
            this.pictureBoxPlus = new System.Windows.Forms.PictureBox();
            this.pictureBoxMinus = new System.Windows.Forms.PictureBox();
            this.slPropertyValue = new Machine.UI.Controls.MachineLabel();
            this.slPropertyName = new Machine.UI.Controls.MachineLabel();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackgroundImage = Machine.UI.Controls.Properties.Resources.rectangle;
            this.panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel.Controls.Add(this.slPropertyValue);
            this.panel.Controls.Add(this.slPropertyName);
            this.panel.Controls.Add(this.pictureBoxPlus);
            this.panel.Controls.Add(this.pictureBoxMinus);
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(340, 110);
            this.panel.TabIndex = 0;
            // 
            // pictureBoxPlus
            // 
            this.pictureBoxPlus.BackgroundImage = Machine.UI.Controls.Properties.Resources.plus;
            this.pictureBoxPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxPlus.Location = new System.Drawing.Point(252, 23);
            this.pictureBoxPlus.Name = "pictureBoxPlus";
            this.pictureBoxPlus.Size = new System.Drawing.Size(59, 61);
            this.pictureBoxPlus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPlus.TabIndex = 1;
            this.pictureBoxPlus.TabStop = false;
            this.pictureBoxPlus.Click += new System.EventHandler(this.pictureBoxPlus_Click);
            // 
            // pictureBoxMinus
            // 
            this.pictureBoxMinus.BackgroundImage = Machine.UI.Controls.Properties.Resources.minus;
            this.pictureBoxMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxMinus.Location = new System.Drawing.Point(28, 23);
            this.pictureBoxMinus.Name = "pictureBoxMinus";
            this.pictureBoxMinus.Size = new System.Drawing.Size(59, 61);
            this.pictureBoxMinus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxMinus.TabIndex = 0;
            this.pictureBoxMinus.TabStop = false;
            this.pictureBoxMinus.Click += new System.EventHandler(this.pictureBoxMinus_Click);
            // 
            // slPropertyValue
            // 
            this.slPropertyValue.BackColor = System.Drawing.Color.Transparent;
            this.slPropertyValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyValue.ForeColor = System.Drawing.Color.DimGray;
            this.slPropertyValue.Location = new System.Drawing.Point(92, 55);
            this.slPropertyValue.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.slPropertyValue.Name = "slPropertyValue";
            this.slPropertyValue.Size = new System.Drawing.Size(156, 49);
            this.slPropertyValue.TabIndex = 5;
            this.slPropertyValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // slPropertyName
            // 
            this.slPropertyName.BackColor = System.Drawing.Color.Transparent;
            this.slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyName.ForeColor = System.Drawing.Color.Black;
            this.slPropertyName.Location = new System.Drawing.Point(92, 7);
            this.slPropertyName.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.slPropertyName.Name = "slPropertyName";
            this.slPropertyName.Size = new System.Drawing.Size(156, 43);
            this.slPropertyName.TabIndex = 4;
            this.slPropertyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MachinePlusMinusPropertyBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "MachinePlusMinusPropertyBox";
            this.Size = new System.Drawing.Size(340, 110);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox pictureBoxPlus;
        private System.Windows.Forms.PictureBox pictureBoxMinus;
        private Machine.UI.Controls.MachineLabel slPropertyValue;
        private Machine.UI.Controls.MachineLabel slPropertyName;
    }
}
