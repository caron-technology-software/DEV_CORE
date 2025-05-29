namespace Machine.UI.Controls
{
    partial class MachineMultiTwoButtons
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineMultiTwoButtons));
            this.b2 = new MachineButton();
            this.b1 = new MachineButton();
            this.SuspendLayout();
            // 
            // sb2
            // 
            this.b2.Active = false;
            this.b2.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("b2.ActiveBackgroundImage")));
            this.b2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.b2.BackColor = System.Drawing.Color.Transparent;
            this.b2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b2.BackgroundImage")));
            this.b2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.b2.ButtonSize = 102;
            this.b2.FlatAppearance.BorderSize = 0;
            this.b2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.b2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.b2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b2.ForeColor = System.Drawing.Color.Transparent;
            this.b2.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("b2.InactiveBackgroundImage")));
            this.b2.Location = new System.Drawing.Point(118, 12);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(102, 102);
            this.b2.StateChangeActivated = true;
            this.b2.TabIndex = 1;
            this.b2.TabStop = false;
            this.b2.UseVisualStyleBackColor = false;
            this.b2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.b2_MouseUp);
            // 
            // sb1
            // 
            this.b1.Active = false;
            this.b1.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("b1.ActiveBackgroundImage")));
            this.b1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.b1.BackColor = System.Drawing.Color.Transparent;
            this.b1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("b1.BackgroundImage")));
            this.b1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.b1.ButtonSize = 102;
            this.b1.FlatAppearance.BorderSize = 0;
            this.b1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.b1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.b1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b1.ForeColor = System.Drawing.Color.Transparent;
            this.b1.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("b1.InactiveBackgroundImage")));
            this.b1.Location = new System.Drawing.Point(10, 12);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(102, 102);
            this.b1.StateChangeActivated = true;
            this.b1.TabIndex = 0;
            this.b1.TabStop = false;
            this.b1.UseVisualStyleBackColor = false;
            this.b1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.b1_MouseUp);
            // 
            // MachineMultiTwoButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.b2);
            this.Controls.Add(this.b1);
            this.DoubleBuffered = true;
            this.Name = "MachineMultiTwoButtons";
            this.Size = new System.Drawing.Size(230, 125);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineButton b1;
        private Machine.UI.Controls.MachineButton b2;
    }
}
