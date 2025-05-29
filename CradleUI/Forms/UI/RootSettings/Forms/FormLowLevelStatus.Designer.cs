namespace Caron.Cradle.UI
{
    partial class FormLowLevelStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLowLevelStatus));
            this.tableIO = new System.Windows.Forms.TableLayoutPanel();
            this.panelForm = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.cbReturn = new Machine.UI.Controls.MachineButton();
            this.mlType = new Machine.UI.Controls.MachineLabel();
            this.mbDigitalInputs = new Machine.UI.Controls.MachineButton();
            this.mbDigitalOutputs = new Machine.UI.Controls.MachineButton();
            this.mbAnalogInputs = new Machine.UI.Controls.MachineButton();
            this.mbEncoders = new Machine.UI.Controls.MachineButton();
            this.mlDigitalInputs = new System.Windows.Forms.Label();
            this.mlDigitalOuputs = new System.Windows.Forms.Label();
            this.mlAnalogInputs = new System.Windows.Forms.Label();
            this.mlEncoders = new System.Windows.Forms.Label();
            this.machinePanelEdgeRounded = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableIO
            // 
            this.tableIO.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableIO.ColumnCount = 4;
            this.tableIO.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableIO.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableIO.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableIO.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableIO.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableIO.Location = new System.Drawing.Point(376, 179);
            this.tableIO.Name = "tableIO";
            this.tableIO.RowCount = 5;
            this.tableIO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableIO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableIO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableIO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableIO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableIO.Size = new System.Drawing.Size(843, 580);
            this.tableIO.TabIndex = 11;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.panelForm.Controls.Add(this.labelTitle);
            this.panelForm.Controls.Add(this.cbReturn);
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1280, 120);
            this.panelForm.TabIndex = 10;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(138, 45);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(255, 33);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Low Level Status";
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
            this.cbReturn.Location = new System.Drawing.Point(12, 9);
            this.cbReturn.Name = "cbReturn";
            this.cbReturn.Size = new System.Drawing.Size(102, 102);
            this.cbReturn.StateChangeActivated = true;
            this.cbReturn.TabIndex = 7;
            this.cbReturn.TabStop = false;
            this.cbReturn.UseVisualStyleBackColor = false;
            this.cbReturn.Click += new System.EventHandler(this.cbReturn_Click);
            // 
            // mlType
            // 
            this.mlType.AutoSize = true;
            this.mlType.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlType.Location = new System.Drawing.Point(380, 137);
            this.mlType.Name = "mlType";
            this.mlType.Size = new System.Drawing.Size(97, 28);
            this.mlType.TabIndex = 12;
            this.mlType.Text = "mlType";
            // 
            // mbDigitalInputs
            // 
            this.mbDigitalInputs.Active = false;
            this.mbDigitalInputs.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDigitalInputs.ActiveBackgroundImage")));
            this.mbDigitalInputs.BackColor = System.Drawing.Color.Transparent;
            this.mbDigitalInputs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbDigitalInputs.BackgroundImage")));
            this.mbDigitalInputs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbDigitalInputs.ButtonSize = 64;
            this.mbDigitalInputs.FlatAppearance.BorderSize = 0;
            this.mbDigitalInputs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbDigitalInputs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbDigitalInputs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbDigitalInputs.ForeColor = System.Drawing.Color.Transparent;
            this.mbDigitalInputs.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDigitalInputs.InactiveBackgroundImage")));
            this.mbDigitalInputs.Location = new System.Drawing.Point(70, 203);
            this.mbDigitalInputs.Name = "mbDigitalInputs";
            this.mbDigitalInputs.Size = new System.Drawing.Size(64, 64);
            this.mbDigitalInputs.StateChangeActivated = true;
            this.mbDigitalInputs.TabIndex = 13;
            this.mbDigitalInputs.TabStop = false;
            this.mbDigitalInputs.UseVisualStyleBackColor = false;
            this.mbDigitalInputs.Click += new System.EventHandler(this.mbDigitalInputs_Click);
            // 
            // mbDigitalOutputs
            // 
            this.mbDigitalOutputs.Active = false;
            this.mbDigitalOutputs.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDigitalOutputs.ActiveBackgroundImage")));
            this.mbDigitalOutputs.BackColor = System.Drawing.Color.Transparent;
            this.mbDigitalOutputs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbDigitalOutputs.BackgroundImage")));
            this.mbDigitalOutputs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbDigitalOutputs.ButtonSize = 64;
            this.mbDigitalOutputs.FlatAppearance.BorderSize = 0;
            this.mbDigitalOutputs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbDigitalOutputs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbDigitalOutputs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbDigitalOutputs.ForeColor = System.Drawing.Color.Transparent;
            this.mbDigitalOutputs.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbDigitalOutputs.InactiveBackgroundImage")));
            this.mbDigitalOutputs.Location = new System.Drawing.Point(70, 283);
            this.mbDigitalOutputs.Name = "mbDigitalOutputs";
            this.mbDigitalOutputs.Size = new System.Drawing.Size(64, 64);
            this.mbDigitalOutputs.StateChangeActivated = true;
            this.mbDigitalOutputs.TabIndex = 14;
            this.mbDigitalOutputs.TabStop = false;
            this.mbDigitalOutputs.UseVisualStyleBackColor = false;
            this.mbDigitalOutputs.Click += new System.EventHandler(this.mbDigitalOutputs_Click);
            // 
            // mbAnalogInputs
            // 
            this.mbAnalogInputs.Active = false;
            this.mbAnalogInputs.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAnalogInputs.ActiveBackgroundImage")));
            this.mbAnalogInputs.BackColor = System.Drawing.Color.Transparent;
            this.mbAnalogInputs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbAnalogInputs.BackgroundImage")));
            this.mbAnalogInputs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbAnalogInputs.ButtonSize = 64;
            this.mbAnalogInputs.FlatAppearance.BorderSize = 0;
            this.mbAnalogInputs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbAnalogInputs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbAnalogInputs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbAnalogInputs.ForeColor = System.Drawing.Color.Transparent;
            this.mbAnalogInputs.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbAnalogInputs.InactiveBackgroundImage")));
            this.mbAnalogInputs.Location = new System.Drawing.Point(70, 364);
            this.mbAnalogInputs.Name = "mbAnalogInputs";
            this.mbAnalogInputs.Size = new System.Drawing.Size(64, 64);
            this.mbAnalogInputs.StateChangeActivated = true;
            this.mbAnalogInputs.TabIndex = 15;
            this.mbAnalogInputs.TabStop = false;
            this.mbAnalogInputs.UseVisualStyleBackColor = false;
            this.mbAnalogInputs.Click += new System.EventHandler(this.mbAnalogInputs_Click);
            // 
            // mbEncoders
            // 
            this.mbEncoders.Active = false;
            this.mbEncoders.ActiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbEncoders.ActiveBackgroundImage")));
            this.mbEncoders.BackColor = System.Drawing.Color.Transparent;
            this.mbEncoders.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbEncoders.BackgroundImage")));
            this.mbEncoders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mbEncoders.ButtonSize = 64;
            this.mbEncoders.FlatAppearance.BorderSize = 0;
            this.mbEncoders.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbEncoders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbEncoders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbEncoders.ForeColor = System.Drawing.Color.Transparent;
            this.mbEncoders.InactiveBackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("mbEncoders.InactiveBackgroundImage")));
            this.mbEncoders.Location = new System.Drawing.Point(70, 443);
            this.mbEncoders.Name = "mbEncoders";
            this.mbEncoders.Size = new System.Drawing.Size(64, 64);
            this.mbEncoders.StateChangeActivated = true;
            this.mbEncoders.TabIndex = 16;
            this.mbEncoders.TabStop = false;
            this.mbEncoders.UseVisualStyleBackColor = false;
            this.mbEncoders.Click += new System.EventHandler(this.mbEncoders_Click);
            // 
            // mlDigitalInputs
            // 
            this.mlDigitalInputs.AutoSize = true;
            this.mlDigitalInputs.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlDigitalInputs.Location = new System.Drawing.Point(143, 224);
            this.mlDigitalInputs.Name = "mlDigitalInputs";
            this.mlDigitalInputs.Size = new System.Drawing.Size(148, 22);
            this.mlDigitalInputs.TabIndex = 17;
            this.mlDigitalInputs.Text = "mlDigitalInputs";
            // 
            // mlDigitalOuputs
            // 
            this.mlDigitalOuputs.AutoSize = true;
            this.mlDigitalOuputs.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlDigitalOuputs.Location = new System.Drawing.Point(143, 304);
            this.mlDigitalOuputs.Name = "mlDigitalOuputs";
            this.mlDigitalOuputs.Size = new System.Drawing.Size(157, 22);
            this.mlDigitalOuputs.TabIndex = 18;
            this.mlDigitalOuputs.Text = "mlDigitalOuputs";
            // 
            // mlAnalogInputs
            // 
            this.mlAnalogInputs.AutoSize = true;
            this.mlAnalogInputs.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlAnalogInputs.Location = new System.Drawing.Point(143, 384);
            this.mlAnalogInputs.Name = "mlAnalogInputs";
            this.mlAnalogInputs.Size = new System.Drawing.Size(153, 22);
            this.mlAnalogInputs.TabIndex = 19;
            this.mlAnalogInputs.Text = "mlAnalogInputs";
            // 
            // mlEncoders
            // 
            this.mlEncoders.AutoSize = true;
            this.mlEncoders.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlEncoders.Location = new System.Drawing.Point(143, 464);
            this.mlEncoders.Name = "mlEncoders";
            this.mlEncoders.Size = new System.Drawing.Size(119, 22);
            this.mlEncoders.TabIndex = 20;
            this.mlEncoders.Text = "mlEncoders";
            // 
            // machinePanelEdgeRounded
            // 
            this.machinePanelEdgeRounded.BackColor = System.Drawing.SystemColors.Control;
            this.machinePanelEdgeRounded.LineColor = System.Drawing.Color.LightGray;
            this.machinePanelEdgeRounded.LineWidth = 3;
            this.machinePanelEdgeRounded.Location = new System.Drawing.Point(57, 179);
            this.machinePanelEdgeRounded.Name = "machinePanelEdgeRounded";
            this.machinePanelEdgeRounded.Radius = 5;
            this.machinePanelEdgeRounded.Size = new System.Drawing.Size(295, 352);
            this.machinePanelEdgeRounded.TabIndex = 21;
            // 
            // FormLowLevelStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.mlEncoders);
            this.Controls.Add(this.mbDigitalInputs);
            this.Controls.Add(this.mlType);
            this.Controls.Add(this.tableIO);
            this.Controls.Add(this.mlAnalogInputs);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.mbDigitalOutputs);
            this.Controls.Add(this.mbEncoders);
            this.Controls.Add(this.mlDigitalOuputs);
            this.Controls.Add(this.mlDigitalInputs);
            this.Controls.Add(this.mbAnalogInputs);
            this.Controls.Add(this.machinePanelEdgeRounded);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLowLevelStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RootSettingsLanguage";
            this.Load += new System.EventHandler(this.FormLowLevelStatus_Load);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Machine.UI.Controls.MachineButton cbReturn;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TableLayoutPanel tableIO;
        private Machine.UI.Controls.MachineLabel mlType;
        private Machine.UI.Controls.MachineButton mbDigitalInputs;
        private Machine.UI.Controls.MachineButton mbDigitalOutputs;
        private Machine.UI.Controls.MachineButton mbAnalogInputs;
        private Machine.UI.Controls.MachineButton mbEncoders;
        private System.Windows.Forms.Label mlDigitalInputs;
        private System.Windows.Forms.Label mlDigitalOuputs;
        private System.Windows.Forms.Label mlAnalogInputs;
        private System.Windows.Forms.Label mlEncoders;
        private Machine.UI.Controls.MachinePanelEdgeRounded machinePanelEdgeRounded;
    }
}