namespace Machine.UI.Controls
{
    partial class MachineSlider
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelValue = new System.Windows.Forms.Label();
            this.slPropertyName = new Machine.UI.Controls.MachineLabel();
            this.slider = new Machine.UI.Controls.MachineSliderInternal();
            this.SuspendLayout();
            // 
            // labelValue
            // 
            this.labelValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(46)))));
            this.labelValue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValue.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelValue.Location = new System.Drawing.Point(193, 152);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(387, 67);
            this.labelValue.TabIndex = 2;
            this.labelValue.Text = "labelValue";
            this.labelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelValue.Click += new System.EventHandler(this.labelValue_Click);
            // 
            // slPropertyName
            // 
            this.slPropertyName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.slPropertyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slPropertyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.slPropertyName.Location = new System.Drawing.Point(113, 17);
            this.slPropertyName.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.slPropertyName.Name = "slPropertyName";
            this.slPropertyName.Size = new System.Drawing.Size(549, 61);
            this.slPropertyName.TabIndex = 1;
            this.slPropertyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slPropertyName.Click += new System.EventHandler(this.slPropertyName_Click);
            this.slPropertyName.DoubleClick += new System.EventHandler(this.SlPropertyName_DoubleClick);
            // 
            // slider
            // 
            this.slider.BackColor = System.Drawing.Color.Transparent;
            this.slider.Delta = 15;
            this.slider.Location = new System.Drawing.Point(0, 0);
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(775, 250);
            this.slider.TabIndex = 0;
            this.slider.Value = 5000;
            // 
            // MachineSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(775, 250);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.slPropertyName);
            this.Controls.Add(this.slider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MachineSlider";
            this.Opacity = 0.98D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MachineSlider";
            this.Load += new System.EventHandler(this.SpreaderSlider_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineSliderInternal slider;
        private Machine.UI.Controls.MachineLabel slPropertyName;
        private System.Windows.Forms.Label labelValue;
    }
}