namespace Machine.Shell
{
    partial class FormShell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShell));
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelUptime = new System.Windows.Forms.Label();
            this.iconPictureBox = new FontAwesome.Sharp.IconPictureBox();
            this.cmdOutput = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelTitle.Location = new System.Drawing.Point(13, 36);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(1255, 58);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "labelTitle";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTitle.Click += new System.EventHandler(this.labelTitle_Click);
            this.labelTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmdOutput_MouseClick);
            // 
            // labelUptime
            // 
            this.labelUptime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUptime.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUptime.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelUptime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelUptime.Location = new System.Drawing.Point(852, 747);
            this.labelUptime.Name = "labelUptime";
            this.labelUptime.Size = new System.Drawing.Size(416, 44);
            this.labelUptime.TabIndex = 2;
            this.labelUptime.Text = "labelUptime";
            this.labelUptime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelUptime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmdOutput_MouseClick);
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.iconPictureBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.iconPictureBox.IconChar = FontAwesome.Sharp.IconChar.Spinner;
            this.iconPictureBox.IconColor = System.Drawing.Color.Gainsboro;
            this.iconPictureBox.IconSize = 85;
            this.iconPictureBox.Location = new System.Drawing.Point(29, 27);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(85, 85);
            this.iconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconPictureBox.TabIndex = 3;
            this.iconPictureBox.TabStop = false;
            this.iconPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmdOutput_MouseClick);
            // 
            // cmdOutput
            // 
            this.cmdOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOutput.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmdOutput.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.cmdOutput.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOutput.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdOutput.Location = new System.Drawing.Point(48, 131);
            this.cmdOutput.Name = "cmdOutput";
            this.cmdOutput.ReadOnly = true;
            this.cmdOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.cmdOutput.Size = new System.Drawing.Size(1186, 613);
            this.cmdOutput.TabIndex = 4;
            this.cmdOutput.Text = " ";
            this.cmdOutput.UseWaitCursor = true;
            this.cmdOutput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmdOutput_MouseClick);
            // 
            // FormShell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.cmdOutput);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.labelUptime);
            this.Controls.Add(this.labelTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShell";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Machine.Bootstrapper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormShell_Load);
            this.Click += new System.EventHandler(this.FormShell_Click);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelUptime;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox;
        private System.Windows.Forms.RichTextBox cmdOutput;
    }
}