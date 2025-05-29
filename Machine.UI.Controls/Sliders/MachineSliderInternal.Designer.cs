namespace Machine.UI.Controls
{
    partial class MachineSliderInternal
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
            this.panelRectangle = new System.Windows.Forms.Panel();
            this.panelMinus = new System.Windows.Forms.Panel();
            this.panelPlus = new System.Windows.Forms.Panel();
            this.panelSlider = new System.Windows.Forms.Panel();
            this.slider = new ColorSlider.ColorSlider();
            this.panelRectangle.SuspendLayout();
            this.panelSlider.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRectangle
            // 
            this.panelRectangle.BackgroundImage = Machine.UI.Controls.Properties.Resources.rectangle_filled;
            this.panelRectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelRectangle.Controls.Add(this.panelMinus);
            this.panelRectangle.Controls.Add(this.panelPlus);
            this.panelRectangle.Controls.Add(this.panelSlider);
            this.panelRectangle.Location = new System.Drawing.Point(0, 0);
            this.panelRectangle.Name = "panelRectangle";
            this.panelRectangle.Size = new System.Drawing.Size(775, 250);
            this.panelRectangle.TabIndex = 0;
            // 
            // panelMinus
            // 
            this.panelMinus.BackgroundImage = Machine.UI.Controls.Properties.Resources.minus;
            this.panelMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelMinus.Location = new System.Drawing.Point(99, 149);
            this.panelMinus.Name = "panelMinus";
            this.panelMinus.Size = new System.Drawing.Size(70, 70);
            this.panelMinus.TabIndex = 1;
            this.panelMinus.Click += new System.EventHandler(this.PanelMinus_Click);
            this.panelMinus.DoubleClick += new System.EventHandler(this.panelMinus_DoubleClick);
            // 
            // panelPlus
            // 
            this.panelPlus.BackgroundImage = Machine.UI.Controls.Properties.Resources.plus;
            this.panelPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelPlus.Location = new System.Drawing.Point(600, 149);
            this.panelPlus.Name = "panelPlus";
            this.panelPlus.Size = new System.Drawing.Size(70, 70);
            this.panelPlus.TabIndex = 0;
            this.panelPlus.Click += new System.EventHandler(this.PanelPlus_Click);
            this.panelPlus.DoubleClick += new System.EventHandler(this.panelPlus_DoubleClick);
            // 
            // panelSlider
            // 
            this.panelSlider.BackColor = System.Drawing.Color.Transparent;
            this.panelSlider.BackgroundImage = Machine.UI.Controls.Properties.Resources.slider_green;
            this.panelSlider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelSlider.Controls.Add(this.slider);
            this.panelSlider.Location = new System.Drawing.Point(59, 57);
            this.panelSlider.Name = "panelSlider";
            this.panelSlider.Size = new System.Drawing.Size(670, 100);
            this.panelSlider.TabIndex = 4;
            // 
            // slider
            // 
            this.slider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(77)))), ((int)(((byte)(95)))));
            this.slider.BarPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(94)))), ((int)(((byte)(110)))));
            this.slider.BarPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(60)))), ((int)(((byte)(74)))));
            this.slider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.slider.ElapsedInnerColor = System.Drawing.Color.Red;
            this.slider.ElapsedPenColorBottom = System.Drawing.Color.Red;
            this.slider.ElapsedPenColorTop = System.Drawing.Color.Red;
            this.slider.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.slider.ForeColor = System.Drawing.Color.Red;
            this.slider.LargeChange = ((uint)(1u));
            this.slider.Location = new System.Drawing.Point(3, 26);
            this.slider.Maximum = 10000;
            this.slider.MouseEffects = false;
            this.slider.MouseWheelBarPartitions = 1;
            this.slider.Name = "slider";
            this.slider.ScaleDivisions = 1;
            this.slider.ScaleSubDivisions = 1;
            this.slider.ShowDivisionsText = true;
            this.slider.ShowSmallScale = false;
            this.slider.Size = new System.Drawing.Size(664, 48);
            this.slider.SmallChange = ((uint)(1u));
            this.slider.TabIndex = 3;
            this.slider.Text = "colorSlider";
            this.slider.ThumbInnerColor = System.Drawing.Color.Transparent;
            this.slider.ThumbOuterColor = System.Drawing.Color.Transparent;
            this.slider.ThumbPenColor = System.Drawing.Color.Transparent;
            this.slider.ThumbRoundRectSize = new System.Drawing.Size(16, 16);
            this.slider.ThumbSize = new System.Drawing.Size(16, 16);
            this.slider.TickAdd = 0F;
            this.slider.TickColor = System.Drawing.Color.White;
            this.slider.TickDivide = 0F;
            this.slider.ValueChanged += new System.EventHandler(this.Slider_ValueChanged);
            // 
            // MachineSliderInternal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelRectangle);
            this.Name = "MachineSliderInternal";
            this.Size = new System.Drawing.Size(775, 250);
            this.panelRectangle.ResumeLayout(false);
            this.panelSlider.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRectangle;
        private System.Windows.Forms.Panel panelMinus;
        private System.Windows.Forms.Panel panelPlus;
        private ColorSlider.ColorSlider slider;
        private System.Windows.Forms.Panel panelSlider;
    }
}
