using Machine.UI.Controls;

namespace Caron.UI.Controls
{
    partial class SpreaderProgrammingTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpreaderProgrammingTable));
            this.spreaderTable = new Caron.UI.Controls.SpreaderTableProgrammingInternal();
            this.sbTrash = new Machine.UI.Controls.MachineButton();
            this.sbDownDown = new Machine.UI.Controls.MachineButton();
            this.sbUpUp = new Machine.UI.Controls.MachineButton();
            this.sbRightRight = new Machine.UI.Controls.MachineButton();
            this.sbLeftLeft = new Machine.UI.Controls.MachineButton();
            this.sbRight = new Machine.UI.Controls.MachineButton();
            this.sbLeft = new Machine.UI.Controls.MachineButton();
            this.sbDown = new Machine.UI.Controls.MachineButton();
            this.sbUp = new Machine.UI.Controls.MachineButton();
            this.SuspendLayout();
            // 
            // spreaderTable
            // 
            this.spreaderTable.Cells = new Caron.UI.Controls.SpreaderCellTableProgramming[] {
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null};
            this.spreaderTable.Colors = new Caron.UI.Controls.SpreaderCellTableColorHeader[] {
        null,
        null,
        null,
        null,
        null,
        null};
            this.spreaderTable.Location = new System.Drawing.Point(83, 84);
            this.spreaderTable.Markers = new Caron.UI.Controls.SpreaderCellTableMarkerHeader[] {
        null,
        null,
        null,
        null,
        null,
        null,
        null};
            this.spreaderTable.MaxValue = 2147483647;
            this.spreaderTable.MinValue = -2147483648;
            this.spreaderTable.Name = "spreaderTable";
            this.spreaderTable.ResourceManagerMarkerChange = "";
            this.spreaderTable.Size = new System.Drawing.Size(995, 450);
            this.spreaderTable.TabIndex = 0;
            // 
            // sbTrash
            // 
            this.sbTrash.Active = false;
            this.sbTrash.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbTrash.ActiveBackgroundImage")));
            this.sbTrash.BackColor = System.Drawing.Color.Transparent;
            this.sbTrash.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbTrash.BackgroundImage")));
            this.sbTrash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbTrash.ButtonSize = 75;
            this.sbTrash.FlatAppearance.BorderSize = 0;
            this.sbTrash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbTrash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbTrash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbTrash.ForeColor = System.Drawing.Color.Transparent;
            this.sbTrash.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbTrash.InactiveBackgroundImage")));
            this.sbTrash.Location = new System.Drawing.Point(606, 3);
            this.sbTrash.Name = "sbTrash";
            this.sbTrash.Size = new System.Drawing.Size(75, 75);
            this.sbTrash.StateChangeActivated = true;
            this.sbTrash.TabIndex = 10;
            this.sbTrash.TabStop = false;
            this.sbTrash.UseVisualStyleBackColor = false;
            this.sbTrash.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbTrash_MouseDown);
            // 
            // sbDownDown
            // 
            this.sbDownDown.Active = false;
            this.sbDownDown.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbDownDown.ActiveBackgroundImage")));
            this.sbDownDown.BackColor = System.Drawing.Color.Transparent;
            this.sbDownDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbDownDown.BackgroundImage")));
            this.sbDownDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbDownDown.ButtonSize = 75;
            this.sbDownDown.FlatAppearance.BorderSize = 0;
            this.sbDownDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbDownDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbDownDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbDownDown.ForeColor = System.Drawing.Color.Transparent;
            this.sbDownDown.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbDownDown.InactiveBackgroundImage")));
            this.sbDownDown.Location = new System.Drawing.Point(2, 459);
            this.sbDownDown.Name = "sbDownDown";
            this.sbDownDown.Size = new System.Drawing.Size(75, 75);
            this.sbDownDown.StateChangeActivated = true;
            this.sbDownDown.TabIndex = 9;
            this.sbDownDown.TabStop = false;
            this.sbDownDown.UseVisualStyleBackColor = false;
            this.sbDownDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbDownDown_MouseDown);
            // 
            // sbUpUp
            // 
            this.sbUpUp.Active = false;
            this.sbUpUp.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbUpUp.ActiveBackgroundImage")));
            this.sbUpUp.BackColor = System.Drawing.Color.Transparent;
            this.sbUpUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbUpUp.BackgroundImage")));
            this.sbUpUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbUpUp.ButtonSize = 75;
            this.sbUpUp.FlatAppearance.BorderSize = 0;
            this.sbUpUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbUpUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbUpUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbUpUp.ForeColor = System.Drawing.Color.Transparent;
            this.sbUpUp.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbUpUp.InactiveBackgroundImage")));
            this.sbUpUp.Location = new System.Drawing.Point(3, 150);
            this.sbUpUp.Name = "sbUpUp";
            this.sbUpUp.Size = new System.Drawing.Size(75, 75);
            this.sbUpUp.StateChangeActivated = true;
            this.sbUpUp.TabIndex = 8;
            this.sbUpUp.TabStop = false;
            this.sbUpUp.UseVisualStyleBackColor = false;
            this.sbUpUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbUpUp_MouseDown);
            // 
            // sbRightRight
            // 
            this.sbRightRight.Active = false;
            this.sbRightRight.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbRightRight.ActiveBackgroundImage")));
            this.sbRightRight.BackColor = System.Drawing.Color.Transparent;
            this.sbRightRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbRightRight.BackgroundImage")));
            this.sbRightRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbRightRight.ButtonSize = 75;
            this.sbRightRight.FlatAppearance.BorderSize = 0;
            this.sbRightRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbRightRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbRightRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbRightRight.ForeColor = System.Drawing.Color.Transparent;
            this.sbRightRight.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbRightRight.InactiveBackgroundImage")));
            this.sbRightRight.Location = new System.Drawing.Point(1003, 3);
            this.sbRightRight.Name = "sbRightRight";
            this.sbRightRight.Size = new System.Drawing.Size(75, 75);
            this.sbRightRight.StateChangeActivated = true;
            this.sbRightRight.TabIndex = 7;
            this.sbRightRight.TabStop = false;
            this.sbRightRight.UseVisualStyleBackColor = false;
            this.sbRightRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbRightRight_MouseDown);
            // 
            // sbLeftLeft
            // 
            this.sbLeftLeft.Active = false;
            this.sbLeftLeft.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbLeftLeft.ActiveBackgroundImage")));
            this.sbLeftLeft.BackColor = System.Drawing.Color.Transparent;
            this.sbLeftLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbLeftLeft.BackgroundImage")));
            this.sbLeftLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbLeftLeft.ButtonSize = 75;
            this.sbLeftLeft.FlatAppearance.BorderSize = 0;
            this.sbLeftLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbLeftLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbLeftLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbLeftLeft.ForeColor = System.Drawing.Color.Transparent;
            this.sbLeftLeft.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbLeftLeft.InactiveBackgroundImage")));
            this.sbLeftLeft.Location = new System.Drawing.Point(209, 3);
            this.sbLeftLeft.Name = "sbLeftLeft";
            this.sbLeftLeft.Size = new System.Drawing.Size(75, 75);
            this.sbLeftLeft.StateChangeActivated = true;
            this.sbLeftLeft.TabIndex = 6;
            this.sbLeftLeft.TabStop = false;
            this.sbLeftLeft.UseVisualStyleBackColor = false;
            this.sbLeftLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbLeftLeft_MouseDown);
            // 
            // sbRight
            // 
            this.sbRight.Active = false;
            this.sbRight.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbRight.ActiveBackgroundImage")));
            this.sbRight.BackColor = System.Drawing.Color.Transparent;
            this.sbRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbRight.BackgroundImage")));
            this.sbRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbRight.ButtonSize = 75;
            this.sbRight.FlatAppearance.BorderSize = 0;
            this.sbRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbRight.ForeColor = System.Drawing.Color.Transparent;
            this.sbRight.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbRight.InactiveBackgroundImage")));
            this.sbRight.Location = new System.Drawing.Point(922, 3);
            this.sbRight.Name = "sbRight";
            this.sbRight.Size = new System.Drawing.Size(75, 75);
            this.sbRight.StateChangeActivated = true;
            this.sbRight.TabIndex = 4;
            this.sbRight.TabStop = false;
            this.sbRight.UseVisualStyleBackColor = false;
            this.sbRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbRight_MouseDown);
            // 
            // sbLeft
            // 
            this.sbLeft.Active = false;
            this.sbLeft.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbLeft.ActiveBackgroundImage")));
            this.sbLeft.BackColor = System.Drawing.Color.Transparent;
            this.sbLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbLeft.BackgroundImage")));
            this.sbLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbLeft.ButtonSize = 75;
            this.sbLeft.FlatAppearance.BorderSize = 0;
            this.sbLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbLeft.ForeColor = System.Drawing.Color.Transparent;
            this.sbLeft.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbLeft.InactiveBackgroundImage")));
            this.sbLeft.Location = new System.Drawing.Point(290, 3);
            this.sbLeft.Name = "sbLeft";
            this.sbLeft.Size = new System.Drawing.Size(75, 75);
            this.sbLeft.StateChangeActivated = true;
            this.sbLeft.TabIndex = 3;
            this.sbLeft.TabStop = false;
            this.sbLeft.UseVisualStyleBackColor = false;
            this.sbLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbLeft_MouseDown);
            // 
            // sbDown
            // 
            this.sbDown.Active = false;
            this.sbDown.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbDown.ActiveBackgroundImage")));
            this.sbDown.BackColor = System.Drawing.Color.Transparent;
            this.sbDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbDown.BackgroundImage")));
            this.sbDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbDown.ButtonSize = 75;
            this.sbDown.FlatAppearance.BorderSize = 0;
            this.sbDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbDown.ForeColor = System.Drawing.Color.Transparent;
            this.sbDown.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbDown.InactiveBackgroundImage")));
            this.sbDown.Location = new System.Drawing.Point(2, 378);
            this.sbDown.Name = "sbDown";
            this.sbDown.Size = new System.Drawing.Size(75, 75);
            this.sbDown.StateChangeActivated = true;
            this.sbDown.TabIndex = 2;
            this.sbDown.TabStop = false;
            this.sbDown.UseVisualStyleBackColor = false;
            this.sbDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbDown_MouseDown);
            // 
            // sbUp
            // 
            this.sbUp.Active = false;
            this.sbUp.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbUp.ActiveBackgroundImage")));
            this.sbUp.BackColor = System.Drawing.Color.Transparent;
            this.sbUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sbUp.BackgroundImage")));
            this.sbUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbUp.ButtonSize = 75;
            this.sbUp.FlatAppearance.BorderSize = 0;
            this.sbUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sbUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sbUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbUp.ForeColor = System.Drawing.Color.Transparent;
            this.sbUp.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("sbUp.InactiveBackgroundImage")));
            this.sbUp.Location = new System.Drawing.Point(3, 231);
            this.sbUp.Name = "sbUp";
            this.sbUp.Size = new System.Drawing.Size(75, 75);
            this.sbUp.StateChangeActivated = true;
            this.sbUp.TabIndex = 1;
            this.sbUp.TabStop = false;
            this.sbUp.UseVisualStyleBackColor = false;
            this.sbUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sbUp_MouseDown);
            // 
            // SpreaderProgrammingTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sbTrash);
            this.Controls.Add(this.sbDownDown);
            this.Controls.Add(this.sbUpUp);
            this.Controls.Add(this.sbRightRight);
            this.Controls.Add(this.sbLeftLeft);
            this.Controls.Add(this.sbRight);
            this.Controls.Add(this.sbLeft);
            this.Controls.Add(this.sbDown);
            this.Controls.Add(this.sbUp);
            this.Controls.Add(this.spreaderTable);
            this.Name = "SpreaderProgrammingTable";
            this.Size = new System.Drawing.Size(1088, 556);
            this.ResumeLayout(false);

        }

        #endregion

        public SpreaderTableProgrammingInternal spreaderTable;   //GPIx215
        public Machine.UI.Controls.MachineButton sbUp;         //GPIx277
        public Machine.UI.Controls.MachineButton sbDown;       //GPIx277
        public Machine.UI.Controls.MachineButton sbLeft;       //GPIx277
        public Machine.UI.Controls.MachineButton sbRight;      //GPIx277
        public Machine.UI.Controls.MachineButton sbLeftLeft;   //GPIx277
        public Machine.UI.Controls.MachineButton sbRightRight; //GPIx277
        public Machine.UI.Controls.MachineButton sbUpUp;       //GPIx277
        public Machine.UI.Controls.MachineButton sbDownDown;   //GPIx277
        private Machine.UI.Controls.MachineButton sbTrash;
    }
}
