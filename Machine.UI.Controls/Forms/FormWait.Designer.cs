namespace Machine.UI.Controls.Forms
{
    partial class FormWait
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWait));
            this.iconSpinner = new FontAwesome.Sharp.IconPictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.timerUI = new System.Windows.Forms.Timer(this.components);
            this.labelUptime = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.labelText = new System.Windows.Forms.Label();
            this.panelMinimize = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.iconSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // iconSpinner
            // 
            this.iconSpinner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iconSpinner.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.iconSpinner.ForeColor = System.Drawing.Color.Gainsboro;
            this.iconSpinner.IconChar = FontAwesome.Sharp.IconChar.Spinner;
            this.iconSpinner.IconColor = System.Drawing.Color.Gainsboro;
            this.iconSpinner.IconSize = 150;
            this.iconSpinner.Location = new System.Drawing.Point(307, 316);
            this.iconSpinner.Name = "iconSpinner";
            this.iconSpinner.Size = new System.Drawing.Size(150, 150);
            this.iconSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconSpinner.TabIndex = 5;
            this.iconSpinner.TabStop = false;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelTitle.Location = new System.Drawing.Point(463, 354);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(553, 76);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "labelTitle";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerUI
            // 
            this.timerUI.Enabled = true;
            this.timerUI.Interval = 20;
            this.timerUI.Tick += new System.EventHandler(this.timerUI_Tick);
            // 
            // labelUptime
            // 
            this.labelUptime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUptime.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUptime.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelUptime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelUptime.Location = new System.Drawing.Point(852, 738);
            this.labelUptime.Name = "labelUptime";
            this.labelUptime.Size = new System.Drawing.Size(416, 44);
            this.labelUptime.TabIndex = 6;
            this.labelUptime.Text = "labelUptime";
            this.labelUptime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(321, 134);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(600, 135);
            this.pictureBox.TabIndex = 7;
            this.pictureBox.TabStop = false;
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelText.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelText.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelText.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelText.Location = new System.Drawing.Point(12, 738);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(416, 44);
            this.labelText.TabIndex = 8;
            this.labelText.Text = "labelText";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelMinimize
            // 
            this.panelMinimize.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelMinimize.Location = new System.Drawing.Point(4, 3);
            this.panelMinimize.Name = "panelMinimize";
            this.panelMinimize.Size = new System.Drawing.Size(98, 84);
            this.panelMinimize.TabIndex = 9;
            this.panelMinimize.DoubleClick += new System.EventHandler(this.panelMinimize_DoubleClick);
            // 
            // FormWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelMinimize);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.labelUptime);
            this.Controls.Add(this.iconSpinner);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWait";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormWait_Load);
            this.Click += new System.EventHandler(this.FormWait_Click);
            this.DoubleClick += new System.EventHandler(this.FormWait_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.iconSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconPictureBox iconSpinner;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Timer timerUI;
        private System.Windows.Forms.Label labelUptime;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Panel panelMinimize;
    }
}