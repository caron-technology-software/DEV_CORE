namespace Machine.UI.Controls
{
    partial class MachineButtonLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineButtonLabel));
            this.labelButton = new System.Windows.Forms.Label();
            this.machineButton = new Machine.UI.Controls.MachineButton();
            this.SuspendLayout();
            // 
            // labelButton
            // 
            this.labelButton.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelButton.Location = new System.Drawing.Point(126, 6);
            this.labelButton.Name = "labelButton";
            this.labelButton.Size = new System.Drawing.Size(295, 102);
            this.labelButton.TabIndex = 1;
            this.labelButton.Text = "Button Name";
            this.labelButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // machineButton
            // 
            this.machineButton.Active = false;
            this.machineButton.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("machineButton.ActiveBackgroundImage")));
            this.machineButton.BackColor = System.Drawing.Color.Transparent;
            this.machineButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("machineButton.BackgroundImage")));
            this.machineButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.machineButton.ButtonSize = 102;
            this.machineButton.FlatAppearance.BorderSize = 0;
            this.machineButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.machineButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.machineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.machineButton.ForeColor = System.Drawing.Color.Transparent;
            this.machineButton.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("machineButton.InactiveBackgroundImage")));
            this.machineButton.Location = new System.Drawing.Point(7, 6);
            this.machineButton.Name = "machineButton";
            this.machineButton.Size = new System.Drawing.Size(102, 102);
            this.machineButton.StateChangeActivated = true;
            this.machineButton.TabIndex = 0;
            this.machineButton.TabStop = false;
            this.machineButton.UseVisualStyleBackColor = false;
            this.machineButton.Click += new System.EventHandler(this.SpreaderButton_Click);
            this.machineButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SpreaderButton_MouseClick);
            this.machineButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SpreaderButton_MouseDown);
            this.machineButton.MouseEnter += new System.EventHandler(this.SpreaderButton_MouseEnter);
            this.machineButton.MouseLeave += new System.EventHandler(this.SpreaderButton_MouseLeave);
            this.machineButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SpreaderButton_MouseUp);
            // 
            // MachineButtonLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelButton);
            this.Controls.Add(this.machineButton);
            this.Name = "MachineButtonLabel";
            this.Size = new System.Drawing.Size(424, 112);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineButton machineButton;
        private System.Windows.Forms.Label labelButton;
    }
}
