using Machine.UI.Controls;

namespace Caron.UI.Controls
{
    partial class SpreaderCellTableMarkerHeader
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
            this.slMarkerId = new Machine.UI.Controls.MachineLabel();
            this.slMarkerLength = new Machine.UI.Controls.MachineLabel();
            this.slMarkerName = new Machine.UI.Controls.MachineLabel();
            this.slMarkerLengthYard = new Machine.UI.Controls.MachineLabel();
            this.SuspendLayout();
            // 
            // slMarkerId
            // 
            this.slMarkerId.BackColor = System.Drawing.Color.Transparent;
            this.slMarkerId.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slMarkerId.Location = new System.Drawing.Point(0, 3);
            this.slMarkerId.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.slMarkerId.Name = "slMarkerId";
            this.slMarkerId.Size = new System.Drawing.Size(120, 18);
            this.slMarkerId.TabIndex = 2;
            this.slMarkerId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slMarkerId.Click += new System.EventHandler(this.SlMarkerId_Click);
            this.slMarkerId.DoubleClick += new System.EventHandler(this.SlMarkerId_DoubleClick);
            // 
            // slMarkerLength
            // 
            this.slMarkerLength.BackColor = System.Drawing.Color.Transparent;
            this.slMarkerLength.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slMarkerLength.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.slMarkerLength.Location = new System.Drawing.Point(0, 40);
            this.slMarkerLength.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.slMarkerLength.Name = "slMarkerLength";
            this.slMarkerLength.Size = new System.Drawing.Size(120, 18);
            this.slMarkerLength.TabIndex = 1;
            this.slMarkerLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slMarkerLength.Click += new System.EventHandler(this.SlMarkerLength_Click);
            this.slMarkerLength.DoubleClick += new System.EventHandler(this.SlMarkerLength_DoubleClick);
            // 
            // slMarkerName
            // 
            this.slMarkerName.BackColor = System.Drawing.Color.Transparent;
            this.slMarkerName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slMarkerName.ForeColor = System.Drawing.Color.Silver;
            this.slMarkerName.Location = new System.Drawing.Point(0, 23);
            this.slMarkerName.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.slMarkerName.Name = "slMarkerName";
            this.slMarkerName.Size = new System.Drawing.Size(120, 15);
            this.slMarkerName.TabIndex = 0;
            this.slMarkerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slMarkerName.Click += new System.EventHandler(this.SlMarkerName_Click);
            this.slMarkerName.DoubleClick += new System.EventHandler(this.SlMarkerName_DoubleClick);
            // 
            // slMarkerLengthYard
            // 
            this.slMarkerLengthYard.BackColor = System.Drawing.Color.Transparent;
            this.slMarkerLengthYard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slMarkerLengthYard.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.slMarkerLengthYard.Location = new System.Drawing.Point(0, 40);
            this.slMarkerLengthYard.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.slMarkerLengthYard.Name = "slMarkerLengthYard";
            this.slMarkerLengthYard.Size = new System.Drawing.Size(120, 18);
            this.slMarkerLengthYard.TabIndex = 3;
            this.slMarkerLengthYard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.slMarkerLengthYard.Visible = false;
            this.slMarkerLengthYard.Click += new System.EventHandler(this.SlMarkerLength_Click);
            this.slMarkerLengthYard.DoubleClick += new System.EventHandler(this.SlMarkerLength_DoubleClick);
            // 
            // SpreaderCellTableMarkerHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.slMarkerLengthYard);
            this.Controls.Add(this.slMarkerId);
            this.Controls.Add(this.slMarkerLength);
            this.Controls.Add(this.slMarkerName);
            this.Name = "SpreaderCellTableMarkerHeader";
            this.Size = new System.Drawing.Size(120, 60);
            this.Resize += new System.EventHandler(this.SpreaderCellTableHeader_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Machine.UI.Controls.MachineLabel slMarkerName;
        public Machine.UI.Controls.MachineLabel slMarkerLength;
        private Machine.UI.Controls.MachineLabel slMarkerId;
        public MachineLabel slMarkerLengthYard;
    }
}
