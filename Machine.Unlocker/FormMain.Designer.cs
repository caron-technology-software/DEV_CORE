namespace Machine.Unlocker
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
            this.labelCode = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.textBoxUnlockCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUnlockCode = new System.Windows.Forms.Label();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCode.Location = new System.Drawing.Point(37, 29);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(186, 20);
            this.labelCode.TabIndex = 0;
            this.labelCode.Text = "Machine request code";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCode.Location = new System.Drawing.Point(41, 52);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(406, 45);
            this.textBoxCode.TabIndex = 1;
            // 
            // textBoxHours
            // 
            this.textBoxHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxHours.Location = new System.Drawing.Point(41, 133);
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(406, 44);
            this.textBoxHours.TabIndex = 3;
            // 
            // textBoxUnlockCode
            // 
            this.textBoxUnlockCode.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUnlockCode.ForeColor = System.Drawing.Color.Red;
            this.textBoxUnlockCode.Location = new System.Drawing.Point(41, 283);
            this.textBoxUnlockCode.Name = "textBoxUnlockCode";
            this.textBoxUnlockCode.ReadOnly = true;
            this.textBoxUnlockCode.Size = new System.Drawing.Size(406, 45);
            this.textBoxUnlockCode.TabIndex = 5;
            this.textBoxUnlockCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxUnlockCode.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxUnlockCode_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hours";
            // 
            // labelUnlockCode
            // 
            this.labelUnlockCode.AutoSize = true;
            this.labelUnlockCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUnlockCode.ForeColor = System.Drawing.Color.Red;
            this.labelUnlockCode.Location = new System.Drawing.Point(37, 260);
            this.labelUnlockCode.Name = "labelUnlockCode";
            this.labelUnlockCode.Size = new System.Drawing.Size(108, 20);
            this.labelUnlockCode.TabIndex = 7;
            this.labelUnlockCode.Text = "Unlock code";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(130, 204);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(226, 38);
            this.buttonGenerate.TabIndex = 8;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 353);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(486, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 375);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.labelUnlockCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxUnlockCode);
            this.Controls.Add(this.textBoxHours);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.labelCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Machine Unlocker";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.TextBox textBoxHours;
        private System.Windows.Forms.TextBox textBoxUnlockCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUnlockCode;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.StatusStrip statusStrip;
    }
}

