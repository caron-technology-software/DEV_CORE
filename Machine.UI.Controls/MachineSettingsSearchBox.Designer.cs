namespace Machine.UI.Controls

{
    partial class MachineSettingsSearchBox
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
            this.machinePanelEdgeRounded = new Machine.UI.Controls.MachinePanelEdgeRounded();
            this.buttonSettingsChanged = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonSaveWithName = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxSettings = new System.Windows.Forms.ComboBox();
            this.machinePanelEdgeRounded.SuspendLayout();
            this.SuspendLayout();
            // 
            // machinePanelEdgeRounded
            // 
            this.machinePanelEdgeRounded.Controls.Add(this.buttonSettingsChanged);
            this.machinePanelEdgeRounded.Controls.Add(this.buttonReset);
            this.machinePanelEdgeRounded.Controls.Add(this.buttonSearch);
            this.machinePanelEdgeRounded.Controls.Add(this.buttonSaveWithName);
            this.machinePanelEdgeRounded.Controls.Add(this.buttonSave);
            this.machinePanelEdgeRounded.Controls.Add(this.comboBoxSettings);
            this.machinePanelEdgeRounded.LineColor = System.Drawing.Color.LightGray;
            this.machinePanelEdgeRounded.LineWidth = 5;
            this.machinePanelEdgeRounded.Location = new System.Drawing.Point(0, 0);
            this.machinePanelEdgeRounded.Name = "machinePanelEdgeRounded";
            this.machinePanelEdgeRounded.Radius = 10;
            this.machinePanelEdgeRounded.Size = new System.Drawing.Size(590, 75);
            this.machinePanelEdgeRounded.TabIndex = 3;
            // 
            // buttonSettingsChanged
            // 
            this.buttonSettingsChanged.BackColor = System.Drawing.Color.Transparent;
            this.buttonSettingsChanged.FlatAppearance.BorderSize = 0;
            this.buttonSettingsChanged.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettingsChanged.Location = new System.Drawing.Point(552, 25);
            this.buttonSettingsChanged.Name = "buttonSettingsChanged";
            this.buttonSettingsChanged.Size = new System.Drawing.Size(27, 27);
            this.buttonSettingsChanged.TabIndex = 8;
            this.buttonSettingsChanged.UseVisualStyleBackColor = false;
            // 
            // buttonReset
            // 
            this.buttonReset.BackColor = System.Drawing.Color.Transparent;
            this.buttonReset.FlatAppearance.BorderSize = 0;
            this.buttonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReset.Location = new System.Drawing.Point(120, 14);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(48, 48);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.UseVisualStyleBackColor = false;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.Transparent;
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Location = new System.Drawing.Point(173, 14);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(48, 48);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // buttonSaveWithName
            // 
            this.buttonSaveWithName.BackColor = System.Drawing.Color.Transparent;
            this.buttonSaveWithName.FlatAppearance.BorderSize = 0;
            this.buttonSaveWithName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveWithName.Location = new System.Drawing.Point(14, 14);
            this.buttonSaveWithName.Name = "buttonSaveWithName";
            this.buttonSaveWithName.Size = new System.Drawing.Size(48, 48);
            this.buttonSaveWithName.TabIndex = 4;
            this.buttonSaveWithName.UseVisualStyleBackColor = false;
            this.buttonSaveWithName.Click += new System.EventHandler(this.ButtonSaveWithName_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Transparent;
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Location = new System.Drawing.Point(67, 14);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(48, 48);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // comboBoxSettings
            // 
            this.comboBoxSettings.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSettings.FormattingEnabled = true;
            this.comboBoxSettings.Location = new System.Drawing.Point(226, 22);
            this.comboBoxSettings.Name = "comboBoxSettings";
            this.comboBoxSettings.Size = new System.Drawing.Size(318, 32);
            this.comboBoxSettings.TabIndex = 1;
            // 
            // MachineSettingsSearchBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.machinePanelEdgeRounded);
            this.Name = "MachineSettingsSearchBox";
            this.Size = new System.Drawing.Size(590, 75);
            this.machinePanelEdgeRounded.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxSettings;
        private System.Windows.Forms.Button buttonSearch;
        private MachinePanelEdgeRounded machinePanelEdgeRounded;
        private System.Windows.Forms.Button buttonSaveWithName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonSettingsChanged;
    }
}
