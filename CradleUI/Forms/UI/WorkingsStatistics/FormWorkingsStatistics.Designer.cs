namespace Caron.Cradle.UI
{
    partial class FormWorkingsStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkingsStatistics));
            this.panelForm = new System.Windows.Forms.Panel();
            this.mlTitle = new System.Windows.Forms.Label();
            this.cbReturn = new Machine.UI.Controls.MachineButton();
            this.mpStatistics = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.mlWorkingsStatistics = new Machine.UI.Controls.MachineLabel();
            this.mbWorkingName = new FontAwesome.Sharp.IconButton();
            this.mbPlayPause = new FontAwesome.Sharp.IconButton();
            this.mbSave = new FontAwesome.Sharp.IconButton();
            this.mbMaterialCode = new FontAwesome.Sharp.IconButton();
            this.mbNew = new FontAwesome.Sharp.IconButton();
            this.panelForm.SuspendLayout();
            this.mpStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.panelForm.Controls.Add(this.mlTitle);
            this.panelForm.Controls.Add(this.cbReturn);
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1010, 120);
            this.panelForm.TabIndex = 9;
            // 
            // mlTitle
            // 
            this.mlTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlTitle.ForeColor = System.Drawing.Color.White;
            this.mlTitle.Location = new System.Drawing.Point(119, 8);
            this.mlTitle.Name = "mlTitle";
            this.mlTitle.Size = new System.Drawing.Size(289, 102);
            this.mlTitle.TabIndex = 2;
            this.mlTitle.Text = "WorkingStatistics";
            this.mlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbReturn
            // 
            this.cbReturn.Active = false;
            this.cbReturn.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbReturn.ActiveBackgroundImage")));
            this.cbReturn.BackColor = System.Drawing.Color.Transparent;
            this.cbReturn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbReturn.BackgroundImage")));
            this.cbReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbReturn.ButtonSize = 102;
            this.cbReturn.FlatAppearance.BorderSize = 0;
            this.cbReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cbReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cbReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbReturn.ForeColor = System.Drawing.Color.Transparent;
            this.cbReturn.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("cbReturn.InactiveBackgroundImage")));
            this.cbReturn.Location = new System.Drawing.Point(11, 8);
            this.cbReturn.Name = "cbReturn";
            this.cbReturn.Size = new System.Drawing.Size(102, 102);
            this.cbReturn.StateChangeActivated = true;
            this.cbReturn.TabIndex = 7;
            this.cbReturn.TabStop = false;
            this.cbReturn.UseVisualStyleBackColor = false;
            this.cbReturn.Click += new System.EventHandler(this.cbReturn_Click);
            // 
            // mpStatistics
            // 
            this.mpStatistics.Controls.Add(this.mlWorkingsStatistics);
            this.mpStatistics.LineColor = System.Drawing.Color.LightGray;
            this.mpStatistics.LineWidth = 3;
            this.mpStatistics.Location = new System.Drawing.Point(11, 276);
            this.mpStatistics.Name = "mpStatistics";
            this.mpStatistics.Radius = 5;
            this.mpStatistics.Size = new System.Drawing.Size(774, 337);
            this.mpStatistics.TabIndex = 52;
            // 
            // mlWorkingsStatistics
            // 
            this.mlWorkingsStatistics.BackColor = System.Drawing.Color.Transparent;
            this.mlWorkingsStatistics.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlWorkingsStatistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mlWorkingsStatistics.Location = new System.Drawing.Point(9, 5);
            this.mlWorkingsStatistics.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mlWorkingsStatistics.Name = "mlWorkingsStatistics";
            this.mlWorkingsStatistics.Size = new System.Drawing.Size(759, 325);
            this.mlWorkingsStatistics.TabIndex = 52;
            this.mlWorkingsStatistics.Text = "mlWorkingsStatistics";
            this.mlWorkingsStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mlWorkingsStatistics.Click += new System.EventHandler(this.mlWorkingsStatistics_Click);
            // 
            // mbWorkingName
            // 
            this.mbWorkingName.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mbWorkingName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbWorkingName.IconChar = FontAwesome.Sharp.IconChar.Keyboard;
            this.mbWorkingName.IconColor = System.Drawing.Color.Black;
            this.mbWorkingName.IconSize = 60;
            this.mbWorkingName.Location = new System.Drawing.Point(803, 279);
            this.mbWorkingName.Name = "mbWorkingName";
            this.mbWorkingName.Rotation = 0D;
            this.mbWorkingName.Size = new System.Drawing.Size(195, 83);
            this.mbWorkingName.TabIndex = 54;
            this.mbWorkingName.Text = "Working Name";
            this.mbWorkingName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mbWorkingName.UseVisualStyleBackColor = true;
            this.mbWorkingName.Click += new System.EventHandler(this.mbWorkingName_Click);
            // 
            // mbPlayPause
            // 
            this.mbPlayPause.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mbPlayPause.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbPlayPause.IconChar = FontAwesome.Sharp.IconChar.Play;
            this.mbPlayPause.IconColor = System.Drawing.Color.Black;
            this.mbPlayPause.IconSize = 60;
            this.mbPlayPause.Location = new System.Drawing.Point(297, 150);
            this.mbPlayPause.Name = "mbPlayPause";
            this.mbPlayPause.Rotation = 0D;
            this.mbPlayPause.Size = new System.Drawing.Size(110, 100);
            this.mbPlayPause.TabIndex = 55;
            this.mbPlayPause.TabStop = false;
            this.mbPlayPause.Text = "Play";
            this.mbPlayPause.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mbPlayPause.UseVisualStyleBackColor = true;
            this.mbPlayPause.Click += new System.EventHandler(this.mbPlayPause_Click);
            // 
            // mbSave
            // 
            this.mbSave.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mbSave.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.mbSave.IconColor = System.Drawing.Color.Black;
            this.mbSave.IconSize = 60;
            this.mbSave.Location = new System.Drawing.Point(160, 150);
            this.mbSave.Name = "mbSave";
            this.mbSave.Rotation = 0D;
            this.mbSave.Size = new System.Drawing.Size(110, 100);
            this.mbSave.TabIndex = 57;
            this.mbSave.Text = "Save";
            this.mbSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mbSave.UseVisualStyleBackColor = true;
            this.mbSave.Click += new System.EventHandler(this.mbSave_Click);
            // 
            // mbMaterialCode
            // 
            this.mbMaterialCode.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mbMaterialCode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbMaterialCode.IconChar = FontAwesome.Sharp.IconChar.Keyboard;
            this.mbMaterialCode.IconColor = System.Drawing.Color.Black;
            this.mbMaterialCode.IconSize = 60;
            this.mbMaterialCode.Location = new System.Drawing.Point(803, 386);
            this.mbMaterialCode.Name = "mbMaterialCode";
            this.mbMaterialCode.Rotation = 0D;
            this.mbMaterialCode.Size = new System.Drawing.Size(195, 83);
            this.mbMaterialCode.TabIndex = 58;
            this.mbMaterialCode.Text = "Material Code";
            this.mbMaterialCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mbMaterialCode.UseVisualStyleBackColor = true;
            this.mbMaterialCode.Click += new System.EventHandler(this.mbMaterialCode_Click);
            // 
            // mbNew
            // 
            this.mbNew.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.mbNew.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbNew.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.mbNew.IconColor = System.Drawing.Color.Black;
            this.mbNew.IconSize = 60;
            this.mbNew.Location = new System.Drawing.Point(23, 150);
            this.mbNew.Name = "mbNew";
            this.mbNew.Rotation = 0D;
            this.mbNew.Size = new System.Drawing.Size(110, 100);
            this.mbNew.TabIndex = 59;
            this.mbNew.Text = "New";
            this.mbNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mbNew.UseVisualStyleBackColor = true;
            this.mbNew.Click += new System.EventHandler(this.mbNew_Click);
            // 
            // FormWorkingsStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 650);
            this.Controls.Add(this.mbNew);
            this.Controls.Add(this.mbMaterialCode);
            this.Controls.Add(this.mbSave);
            this.Controls.Add(this.mbWorkingName);
            this.Controls.Add(this.mpStatistics);
            this.Controls.Add(this.mbPlayPause);
            this.Controls.Add(this.panelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWorkingsStatistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormWorkingsStatistics";
            this.Load += new System.EventHandler(this.FormWorkingsStatistics_Load);
            this.panelForm.ResumeLayout(false);
            this.mpStatistics.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label mlTitle;
        private Machine.UI.Controls.MachineButton cbReturn;
        private Machine.UI.Controls.MachinePanelEdgeRounded mpStatistics;
        private Machine.UI.Controls.MachineLabel mlWorkingsStatistics;
        private FontAwesome.Sharp.IconButton mbWorkingName;
        private FontAwesome.Sharp.IconButton mbPlayPause;
        private FontAwesome.Sharp.IconButton mbSave;
        private FontAwesome.Sharp.IconButton mbMaterialCode;
        private FontAwesome.Sharp.IconButton mbNew;
    }
}