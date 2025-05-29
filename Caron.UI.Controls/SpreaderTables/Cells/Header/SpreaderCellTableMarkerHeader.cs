using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob;

using Machine.UI.Controls;
using Machine.UI.Communication;

using Caron.Spreader;
using Caron.Spreader.Control.HighLevel;

namespace Caron.UI.Controls
{
    public partial class SpreaderCellTableMarkerHeader : UserControl
    {
        //GPIx258
        public bool IsImperialYard { get; set; } = false;
        public bool NeedYard { get; set; } = false;
        //GPFx258
        public string ResourceMarkerChangeChecker = string.Empty;
        public int Index { get; set; }
        public bool EnableLengthModification { get; set; } = false;

        public const int CellWidth = 120;
        public const int CellHeight = 60;

        public class MarkerHeaderEventArgs : EventArgs
        {
            public int Index { get; set; }
            public MarkerHeaderEventArgs(int index)
            {
                Index = index;
            }
        }

        public event EventHandler<MarkerHeaderEventArgs> MarkerChanged;

        private void OnMarkerChanged(MarkerHeaderEventArgs e)
        {
            MarkerChanged?.Invoke(this, e);
        }

        public int MarkerIndex { get; set; }

        private string markerId = String.Empty;
        public string MarkerId
        {
            get
            {
                return markerId;
            }

            set
            {
                markerId = value;
                slMarkerId.Text = $"{markerId}";
            }
        }

        private string markerName = String.Empty;
        public string MarkerName
        {
            get
            {
                return markerName;
            }

            set
            {
                markerName = value;
                slMarkerName.Text = markerName;
            }
        }

        private int markerLength = int.MinValue;
        public int MarkerLength
        {
            get
            {
                return markerLength;
            }
            set
            {
                markerLength = value;
                slMarkerLength.Text = markerLength.ToString();

                //GPIx258 -> da convertire stringa yard
                decimal propertyValueInInch = (decimal)(markerLength / 25.4f);
                decimal propertyValueInInYard = Math.Truncate((decimal)((markerLength / 25.4f) / 36f));
                decimal propertyValueInInchRemaining = (decimal)(markerLength / 25.4f) - (Math.Truncate((decimal)((markerLength / 25.4f) / 36f)) * 36m);
                decimal propertyValueInInchRemainingIntPart = Math.Truncate(propertyValueInInchRemaining);
                decimal propertyValueInInchRemainingDecimalPart = propertyValueInInchRemaining - Math.Truncate(propertyValueInInchRemaining);

                propertyValueInInch = Math.Round(propertyValueInInch, 5, MidpointRounding.AwayFromZero);
                string str01 = $"{propertyValueInInYard:0}yd{propertyValueInInchRemainingIntPart:0}\"{propertyValueInInchRemainingDecimalPart:.000}";
                str01 = str01.Replace(".", "").Replace(",", "");
                slMarkerLengthYard.Text = str01;
                //slPropertyValueYard.Text = $"{propertyValueInInch:0.000} inch";
                //GPFx258

            }
        }

        public SpreaderCellTableMarkerHeader()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            Width = CellWidth;
            Height = CellHeight;
            Size = new Size(CellWidth, CellHeight);

            slMarkerId.Text = String.Empty;
            slMarkerName.Text = String.Empty;
            slMarkerLength.Text = String.Empty;

            //GPIx258
            slMarkerLengthYard.Text = String.Empty;
            //GPFx258

            Refresh();
        }

        public void SetTexts(string id, string name, int length)
        {
            MarkerId = id;
            MarkerName = name;
            MarkerLength = length;
        }

        //GPIx215
        private static StreamHID streamHIDX = null;
        public static void SetStreamHIDX(StreamHID Sid)
        {
            streamHIDX = Sid;
        }
        //GPFx215

        private void SpreaderCellTableHeader_Resize(object sender, EventArgs e)
        {
            Size = new Size(CellWidth, CellHeight);
        }

        private void ModifyMarkerLength(object sender)
        {
            //this.Visible = false;
            if (EnableLengthModification == false)
            {
                var msgBox = new MachineMessageBox(Localization.Warning, Localization.NotAllowed);
                msgBox.ShowDialog();
                return;
            }

            bool permitOnlyLenghtlessMarkers = false;

            if (EnableLengthModification)
            {
                //-------------------------------
                // Marker checker
                //-------------------------------
                var json = Communicator.SetVariable(ResourceMarkerChangeChecker, "markerId", MarkerIndex);

                var check = Json.Deserialize<WorkingTableChangesWarnings>(json);

                Console.WriteLine($"check: {check}");

                if (check == WorkingTableChangesWarnings.RemovedAllMarkersSurmounted)
                {
                    var msgBox = new MachineMessageBox(Localization.Warning, Localization.RemoveAllSourmontedMarkers);

                    if (msgBox.ShowDialog() == DialogResult.OK)
                    {
                        permitOnlyLenghtlessMarkers = true;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            //GPIx258
            int value = 0;
            if (((MachineLabel)sender).Name == "slMarkerLengthYard")
            {
                value = int.Parse(slMarkerLength.Text);
            }
            else
            {
                value = int.Parse(((MachineLabel)sender).Text);
            }
            //GPFx258

            //GPIx258
            if (!IsImperialYard)
            {
                //GPIx215
                //using (var keyb = new TouchNumericKeyboard("", permitOnlyLenghtlessMarkers ? 0 : value))
                using (var keyb = new TouchNumericKeyboard("", permitOnlyLenghtlessMarkers ? 0 : value, "", streamHIDX))
                //GPFx215
                {
                    keyb.KeyPlusMinusEnabled = false;
                    keyb.KeyCommaEnabled = false;
                    
                    keyb.ShowDialog();

                    value = (int)(keyb.Value);
                }
            }
            else
            {
                //int yard = 1;
                //decimal valueInMm = (decimal)(yard * 36f * 25.4f);
                decimal valueInMm = 0;
                float propertyValue = value;
                decimal valueInInch = (decimal)(propertyValue / 25.4f);
                decimal valueInYard = Math.Truncate((decimal)((propertyValue / 25.4f) / 36f));
                decimal valueInInchRemaining = (decimal)(propertyValue / 25.4f) - (Math.Truncate((decimal)((propertyValue / 25.4f) / 36f)) * 36m);
                valueInInchRemaining = Math.Round(valueInInchRemaining, 5, MidpointRounding.AwayFromZero);
                decimal valueInInchRemainingIntPart = Math.Truncate(valueInInchRemaining);
                decimal valueInInchIntPart = Math.Truncate(valueInInch);
                decimal valueInInchRemainingDecimalPart = valueInInchRemaining - Math.Truncate(valueInInchRemaining);

                if (NeedYard)
                {
                    //using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                    using (var keyb = new TouchNumericKeyboard("", (float)valueInYard))
                    {
                        keyb.KeyCommaEnabled = false;
                        keyb.KeyPlusMinusEnabled = false;

                        keyb.UnitMeasure = "Yard";

                        DialogResult dialogResult;
                        dialogResult = keyb.ShowDialog();


                        //PropertyValue = keyb.Value;
                        valueInMm = (decimal)(keyb.Value * 36f * 25.4f);
                    }
                }

                float chooseValue01 = 0;
                if (NeedYard)
                {
                    chooseValue01 = (float)valueInInchRemainingIntPart;
                }
                else
                {
                    chooseValue01 = (float)valueInInchIntPart;
                }
                if (true)
                {
                    //using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                    using (var keyb = new TouchNumericKeyboard("", chooseValue01))
                    {
                        keyb.KeyCommaEnabled = false;
                        keyb.KeyPlusMinusEnabled = false;

                        keyb.UnitMeasure = "Inch";

                        keyb.ShowDialog();

                        //PropertyValue = keyb.Value;
                        valueInMm = valueInMm + (decimal)(keyb.Value * 25.4f);
                    }
                }

                if (true)
                {
                    //using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                    using (var keyb = new TouchNumericKeyboard("", (float)valueInInchRemainingDecimalPart))
                    {
                        keyb.KeyCommaEnabled = true;
                        keyb.KeyPlusMinusEnabled = false;

                        if (true)
                        {
                            keyb.MaxValue = 1f;
                        }

                        if (true)
                        {
                            keyb.MinValue = 0f;
                        }

                        keyb.UnitMeasure = "Dec. Inch";

                        keyb.ShowDialog();


                        //PropertyValue = keyb.Value;
                        valueInMm = valueInMm + (decimal)(keyb.Value * 25.4f);

                        valueInMm = Math.Round(valueInMm, 0, MidpointRounding.AwayFromZero);
                        value = (int)valueInMm;
                    }
                }

            }
            //GPFx258

            if (permitOnlyLenghtlessMarkers)
            {
                if (value != 0)
                {
                    var msgBox = new MachineMessageBox(Localization.Warning, Localization.MarkerLengthNotNull);
                    msgBox.ShowDialog();
                    return;
                }
            }

            MarkerLength = value;

            OnMarkerChanged(new MarkerHeaderEventArgs(Index));
        }

        private void SlMarkerLength_DoubleClick(object sender, EventArgs e)
        {
            ModifyMarkerLength(sender);
        }

        private void SlMarkerLength_Click(object sender, EventArgs e)
        {
            ModifyMarkerLength(sender);
        }

        public override string ToString()
        {
            return $"[SpreaderCellTableMarkerHeader] MarkerId:{slMarkerId.Text} MarkerName:{slMarkerName.Text} MarkerLength:{slMarkerLength.Text}";
        }

        private void SlMarkerId_Click(object sender, EventArgs e)
        {
            ModifyMarkerLength(slMarkerLength);
        }

        private void SlMarkerId_DoubleClick(object sender, EventArgs e)
        {
            ModifyMarkerLength(slMarkerLength);
        }

        private void SlMarkerName_Click(object sender, EventArgs e)
        {
            ModifyMarkerLength(slMarkerLength);
        }

        private void SlMarkerName_DoubleClick(object sender, EventArgs e)
        {
            ModifyMarkerLength(slMarkerLength);
        }
    }
}
