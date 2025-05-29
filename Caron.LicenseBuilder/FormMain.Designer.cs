
namespace Caron.LicenseBuilder
{
    partial class FormMain
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

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lbMachine = new System.Windows.Forms.Label();
            this.cbMachineType = new System.Windows.Forms.ComboBox();
            this.lbMachineType = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.btnGenerateLicense = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSerial = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // lbMachine
            // 
            this.lbMachine.AutoSize = true;
            this.lbMachine.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMachine.Location = new System.Drawing.Point(18, 13);
            this.lbMachine.Name = "lbMachine";
            this.lbMachine.Size = new System.Drawing.Size(118, 18);
            this.lbMachine.TabIndex = 0;
            this.lbMachine.Text = "Machine Type";
            // 
            // cbMachineType
            // 
            this.cbMachineType.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMachineType.FormattingEnabled = true;
            this.cbMachineType.Items.AddRange(new object[] {
            "Spreader Lion",
            "Spreader Mask",
            "Cradle York"});
            this.cbMachineType.Location = new System.Drawing.Point(16, 34);
            this.cbMachineType.Name = "cbMachineType";
            this.cbMachineType.Size = new System.Drawing.Size(287, 32);
            this.cbMachineType.TabIndex = 1;
            // 
            // lbMachineType
            // 
            this.lbMachineType.AutoSize = true;
            this.lbMachineType.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMachineType.Location = new System.Drawing.Point(18, 101);
            this.lbMachineType.Name = "lbMachineType";
            this.lbMachineType.Size = new System.Drawing.Size(114, 18);
            this.lbMachineType.TabIndex = 2;
            this.lbMachineType.Text = "License Type";
            // 
            // numericUpDown
            // 
            this.numericUpDown.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown.Location = new System.Drawing.Point(18, 123);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(120, 39);
            this.numericUpDown.TabIndex = 3;
            this.numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGenerateLicense
            // 
            this.btnGenerateLicense.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateLicense.Location = new System.Drawing.Point(16, 300);
            this.btnGenerateLicense.Name = "btnGenerateLicense";
            this.btnGenerateLicense.Size = new System.Drawing.Size(287, 138);
            this.btnGenerateLicense.TabIndex = 4;
            this.btnGenerateLicense.Text = "GENERATE";
            this.btnGenerateLicense.UseVisualStyleBackColor = true;
            this.btnGenerateLicense.Click += new System.EventHandler(this.BtnGenerateLicense_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Machine Serial";
            // 
            // textBoxSerial
            // 
            this.textBoxSerial.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSerial.Location = new System.Drawing.Point(15, 219);
            this.textBoxSerial.Name = "textBoxSerial";
            this.textBoxSerial.Size = new System.Drawing.Size(288, 39);
            this.textBoxSerial.TabIndex = 6;
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 465);
            this.Controls.Add(this.textBoxSerial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerateLicense);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.lbMachineType);
            this.Controls.Add(this.cbMachineType);
            this.Controls.Add(this.lbMachine);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "CARON License Builder";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.FormMain_DragOver);
            this.DragLeave += new System.EventHandler(this.FormMain_DragLeave);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMachine;
        private System.Windows.Forms.ComboBox cbMachineType;
        private System.Windows.Forms.Label lbMachineType;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Button btnGenerateLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSerial;
    }
}

