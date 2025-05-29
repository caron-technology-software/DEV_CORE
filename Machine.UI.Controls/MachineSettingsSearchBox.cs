using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FontAwesome.Sharp;

namespace Machine.UI.Controls
{
    public partial class MachineSettingsSearchBox : UserControl
    {
        private const int ButtonSize = 48;
        private const float ScalingFactor = 0.7f;

        public Color ButtonColor { get; set; } = Color.DimGray;

        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    comboBoxSettings.SelectedIndex = selectedIndex;
                    OnSelectedItemChanged(new EventArgs());
                }
            }
        }

        public string[] Items { get => comboBoxSettings.Items.Cast<string>().ToArray(); }

        public bool SettingsChanged
        {
            set
            {
                if (value)
                {
                    buttonSettingsChanged.Image = IconChar.ExclamationCircle.ToBitmap(Color.Red, (int)(ButtonSize * ScalingFactor));
                }
                else
                {
                    buttonSettingsChanged.Image = IconChar.CheckCircle.ToBitmap(Color.Green, (int)(ButtonSize * ScalingFactor));
                }
            }
        }

        public void SetIndexWithoutEvent(int index)
        {
            if (index < comboBoxSettings.Items.Count)
            {
                selectedIndex = index;

            }
            else if (index > 0)
            {
                selectedIndex = comboBoxSettings.Items.Count - 1;
            }
            else
            {
                selectedIndex = 0;
            }

            if (comboBoxSettings.Items.Count > selectedIndex)
            {
                comboBoxSettings.SelectedIndex = selectedIndex;
            }
        }

        #region Events
        public event EventHandler SearchButtonPressed;
        protected virtual void OnSearchButtonPressed(EventArgs e)
        {
            EventHandler handler = SearchButtonPressed;
            handler?.Invoke(this, e);
        }

        public event EventHandler SaveButtonPressed;
        protected virtual void OnSaveButtonPressed(EventArgs e)
        {
            EventHandler handler = SaveButtonPressed;
            handler?.Invoke(this, e);
        }

        public event EventHandler SaveWithNameButtonPressed;
        protected virtual void OnSaveWithNameButtonPressed(EventArgs e)
        {
            EventHandler handler = SaveWithNameButtonPressed;
            handler?.Invoke(this, e);
        }

        public event EventHandler ResetButtonPressed;
        protected virtual void OnResetButtonPressed(EventArgs e)
        {
            EventHandler handler = ResetButtonPressed;
            handler?.Invoke(this, e);
        }

        public event EventHandler SelectedItemChanged;

        protected virtual void OnSelectedItemChanged(EventArgs e)
        {
            EventHandler handler = SelectedItemChanged;
            handler?.Invoke(this, e);
        }
        #endregion

        public MachineSettingsSearchBox()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            buttonSearch.Image = IconChar.Search.ToBitmap(ButtonColor, ButtonSize);
            buttonSearch.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonSearch.FlatAppearance.BorderColor = SystemColors.Control;
            buttonSearch.FlatStyle = FlatStyle.Flat;

            buttonSave.Image = IconChar.Paste.ToBitmap(ButtonColor, ButtonSize);
            buttonSave.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonSave.FlatAppearance.BorderColor = SystemColors.Control;
            buttonSave.FlatStyle = FlatStyle.Flat;

            buttonSaveWithName.Image = IconChar.Save.ToBitmap(ButtonColor, ButtonSize);
            buttonSaveWithName.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonSaveWithName.FlatAppearance.BorderColor = SystemColors.Control;
            buttonSaveWithName.FlatStyle = FlatStyle.Flat;

            buttonReset.Image = IconChar.Rev.ToBitmap(ButtonColor, ButtonSize);
            buttonReset.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonReset.FlatAppearance.BorderColor = SystemColors.Control;
            buttonReset.FlatStyle = FlatStyle.Flat;

            buttonSettingsChanged.Image = IconChar.ExclamationCircle.ToBitmap(ButtonColor, (int)(ButtonSize * ScalingFactor));
            buttonSettingsChanged.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonSettingsChanged.FlatAppearance.BorderColor = SystemColors.Control;
            buttonSettingsChanged.FlatStyle = FlatStyle.Flat;

            comboBoxSettings.SelectedIndexChanged += ComboboxSettings_SelectedItemChanged;
            comboBoxSettings.DropDownStyle = ComboBoxStyle.DropDown;

            //Azione di apertura PopUp in caso di click
            comboBoxSettings.Click += (sender, args) => comboBoxSettings.DroppedDown = true;
        }

        public string ItemText { get => comboBoxSettings.Text; set => comboBoxSettings.Text = value; }

        public string SearchText { get => comboBoxSettings.Text; set => comboBoxSettings.Text = value; }

        public void AddItem(string settingName)
        {
            comboBoxSettings.BeginUpdate();
            comboBoxSettings.Items.Add(settingName);
            comboBoxSettings.SelectedIndex = 0;
            comboBoxSettings.EndUpdate();
        }

        public void ClearItems()
        {
            comboBoxSettings.BeginUpdate();
            comboBoxSettings.Items.Clear();
            comboBoxSettings.EndUpdate();
        }

        public void AddItemsRange(string[] settingsName, int selectedIndex)
        {
            comboBoxSettings.BeginUpdate();
            comboBoxSettings.Items.AddRange(settingsName);
            comboBoxSettings.EndUpdate();

            if (comboBoxSettings.Items.Count > 0)
            {
                comboBoxSettings.SelectedIndex = selectedIndex;
            }
        }

        #region Callbacks
        private void ComboboxSettings_SelectedItemChanged(object sender, EventArgs e)
        {
            SelectedIndex = comboBoxSettings.SelectedIndex;
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ButtonSearch_Click");
            OnSearchButtonPressed(new EventArgs());
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ButtonSave_Click");
            OnSaveButtonPressed(new EventArgs());
        }

        private void ButtonSaveWithName_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ButtonSaveWithName_Click");
            OnSaveWithNameButtonPressed(new EventArgs());
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ButtonReset_Click");
            OnResetButtonPressed(new EventArgs());
        }
        #endregion
    }
}
