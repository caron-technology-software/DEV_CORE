using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using ProRob.Extensions.Collections;

using Machine;
using Machine.UI.Controls;
using Machine.Utility;

using Caron.Cradle.Control.LowLevel;
using System.IO;

namespace Caron.Cradle.UI
{
    public partial class FormIOSettings : FormCradleBase
    {
        private readonly Point ListBoxLocation = new Point(350, 172);
        private readonly Size ListBoxSize = new Size(645, 584);

        private List<MachineComboBoxItem> digitalInputs = new List<MachineComboBoxItem>();
        private List<MachineComboBoxItem> digitalOutputs = new List<MachineComboBoxItem>();
        private List<MachineComboBoxItem> analogInputs = new List<MachineComboBoxItem>();
        private List<MachineComboBoxItem> encoders = new List<MachineComboBoxItem>();

        private List<MachineButton> buttons = new List<MachineButton>();

        //GPIx21
        private static Dictionary<string, string> DizLowLevDIinput = new Dictionary<string, string>();
        private static Dictionary<string, string> DizLowLevOnOff = new Dictionary<string, string>();
        private static Dictionary<string, string> DizLowLevHighLev = new Dictionary<string, string>();
        private static Dictionary<string, string> DizLowLevDOutput = new Dictionary<string, string>();
        private static Dictionary<string, string> DoDizLowLevHighLev = new Dictionary<string, string>();
        private static Dictionary<string, string> DizLowLevAnInput = new Dictionary<string, string>();
        private static Dictionary<string, string> AnDizLowLevHighLev = new Dictionary<string, string>();

        private static Boolean SettingChanged = false;
        //GPFx21

        public FormIOSettings()
        {
            InitializeComponent();

            //-------------------------------------------------------
            // SuspendLayout
            //-------------------------------------------------------
            SuspendLayout();

            int ch;

            cbReturn.StateChangeActivated = false;

            mbDigitalInputs.StateChangeActivated = false;
            mbDigitalOutputs.StateChangeActivated = false;
            mbAnalogInputs.StateChangeActivated = false;

            labelTitle.Text = Localization.InputOutputSettings;
            mlDigitalInputs.Text = Localization.DigitalInputs;
            mlDigitalOuputs.Text = Localization.DigitalOutputs;
            mlAnalogInputs.Text = Localization.AnalogInputs;

            buttons.Clear();
            buttons.AddRange(new List<MachineButton>()
            {
                mbDigitalInputs,
                mbDigitalOutputs,
                mbAnalogInputs,
            });

            //GPIx21
            #region Inizialize Dictionary
            string[] lines = File.ReadAllLines(Constants.Path.LowLevelControl.DigitalInputsMapFile);
            DizLowLevDIinput = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                int found = 0;
                found.ToString();
                found = line.IndexOf(":");
                Console.WriteLine(line);
                try
                {
                    DizLowLevDIinput.Add(line.Substring(0, found).Trim(), line.Substring(found).Trim());
                }
                catch (Exception e01)
                {
                    Console.WriteLine(e01.Message);
                }
            }

            lines = File.ReadAllLines(Constants.Path.LowLevelControl.DigitalInputsTypeMapFile);
            DizLowLevOnOff = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                int found = 0;
                found.ToString();
                found = line.IndexOf(":");
                Console.WriteLine(line);
                try
                {
                    DizLowLevOnOff.Add(line.Substring(0, found).Trim(), line.Substring(found).Trim());
                }
                catch (Exception e01)
                {
                    Console.WriteLine(e01.Message);
                }
            }

            string str_LowLewHighLevMapFile = @"fuse_check_motors                       :FuseCheckMotors 
titan_limit                             :TitanLimit 
photocell_op_side                       :PhotocellOperatorSide 
photocell_mt_side                       :PhotocellMotorSide 
photocell_material                      :PhotocellMaterialPresence 
limit_cutter_op_side                    :LimitCutterOperatorSide 
limit_cutter_mt_side                    :LimitCutterMotorSide 
limit_dancer                            :LimitDancer 
limit_alignment_op_side                 :LimitAlignmentOperatorSide 
limit_alignment_mt_side                 :LimitAlignmentMotorSide 
limit_overturning_op_side_load          :LimitOverturningOperatorSideLoad 
limit_overturning_op_side_unload        :LimitOverturningOperatorSideUnload 
limit_spoon_up                          :LimitSpoonUp 
limit_spoon_down                        :LimitSpoonDown 
limit_overturning_mt_side_load          :LimitOverturningMotorSideLoad 
limit_overturning_mt_side_unload        :LimitOverturningMotorSideUnload 
zund_enable                             :ZundEnable 
zund_cut_off                            :ZundCutOff 
photocell_roll_presence                 :PhotocellRollPresence 
input_driver04                          :input_driver04";           //GPIx133 cambiato "input_driver03" con "photocell_roll_presence".

            //str_LowLewHighLevMapFile
            string[] stringSeparators = new string[] { "\r\n" };  //\r\n
            string[] arr_LowLewHighLevMapFile = str_LowLewHighLevMapFile.Split(stringSeparators, StringSplitOptions.None); //StringSplitOptions.RemoveEmptyEntries
            //lines = File.ReadAllLines(Constants.Path.LowLevelControl.LowLewHighLevMapFile);
            DizLowLevHighLev = new Dictionary<string, string>();
            foreach (string line in arr_LowLewHighLevMapFile)
            {
                int found = 0;
                found.ToString();
                found = line.IndexOf(":");
                Console.WriteLine(line);
                try
                {
                    //a differenza del nome la chiave è il nome HighLevel e il valore LowLevel:
                    DizLowLevHighLev.Add(line.Substring(found).Trim().Substring(1), line.Substring(0, found).Trim());
                }
                catch (Exception e01)
                {
                    Console.WriteLine(e01.Message);
                }
            }

            string str_DoLowLewHighLevMapFile = @"motor_overturning_op_side_load                  :MotorOverturningOpSideLoad 
motor_overturning_op_side_unload                :MotorOverturningOpSideUnload 
motor_alignment_op_side                         :MotorAlignmentOpSide 
motor_alignment_mt_side                         :MotorAlignmentMtSide 
titan_up                                        :TitanUp 
titan_down                                      :TitanDown 
motor_spoon_up                                  :MotorSpoonUp 
motor_spoon_down                                :MotorSpoonDown 
motor_overturning_mt_side_load                  :MotorOverturningMtSideLoad 
motor_overturning_mt_side_unload                :MotorOverturningMtSideUnload 
march_enabled                                   :MarchEnabled 
axis_cradle_to_cutter_exchange                  :AxisCradleToCutterExchange 
output07                                        :OutFree01 
output08                                        :OutFree02 
output15                                        :OutFree03 
output16                                        :OutFree04 
zund_error                                      :ZundError 
zund_status                                     :ZundStatus 
cradle_cutter_lock                              :CradleCutterLock 
output_driver04                                 :output_driver04";

            //str_DoLowLewHighLevMapFile
            //stringSeparators = new string[] { "\r\n" };  //\r\n
            string[] arr_DoLowLewHighLevMapFile = str_DoLowLewHighLevMapFile.Split(stringSeparators, StringSplitOptions.None); //StringSplitOptions.RemoveEmptyEntries
            //lines = File.ReadAllLines(Constants.Path.LowLevelControl.DoLowLewHighLevMapFile);
            DoDizLowLevHighLev = new Dictionary<string, string>();
            foreach (string line in arr_DoLowLewHighLevMapFile)
            {
                int found = 0;
                found.ToString();
                found = line.IndexOf(":");
                Console.WriteLine(line);
                try
                {
                    //a differenza del nome la chiave è il nome HighLevel e il valore LowLevel:
                    DoDizLowLevHighLev.Add(line.Substring(found).Trim().Substring(1), line.Substring(0, found).Trim());
                }
                catch (Exception e01)
                {
                    Console.WriteLine(e01.Message);
                }
            }

            lines = File.ReadAllLines(Constants.Path.LowLevelControl.DigitalOutputsMapFile);
            DizLowLevDOutput = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                int found = 0;
                found.ToString();
                found = line.IndexOf(":");
                Console.WriteLine(line);
                try
                {
                    DizLowLevDOutput.Add(line.Substring(0, found).Trim(), line.Substring(found).Trim());
                }
                catch (Exception e01)
                {
                    Console.WriteLine(e01.Message);
                }
            }

            string str_AnLowLewHighLevMapFile = @"dancer                            :Dancer";
            //str_AnLowLewHighLevMapFile
            //stringSeparators = new string[] { "\r\n" };  //\r\n
            string[] arr_AnLowLewHighLevMapFile = str_AnLowLewHighLevMapFile.Split(stringSeparators, StringSplitOptions.None); //StringSplitOptions.RemoveEmptyEntries
            //lines = File.ReadAllLines(Constants.Path.LowLevelControl.AnLowLewHighLevMapFile);
            AnDizLowLevHighLev = new Dictionary<string, string>();
            foreach (string line in arr_AnLowLewHighLevMapFile)
            {
                int found = 0;
                found.ToString();
                found = line.IndexOf(":");
                Console.WriteLine(line);
                try
                {
                    //a differenza del nome la chiave è il nome HighLevel e il valore LowLevel:
                    AnDizLowLevHighLev.Add(line.Substring(found).Trim().Substring(1), line.Substring(0, found).Trim());
                }
                catch (Exception e01)
                {
                    Console.WriteLine(e01.Message);
                }
            }

            lines = File.ReadAllLines(Constants.Path.LowLevelControl.AnalogInputsMapFile);
            DizLowLevAnInput = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                int found = 0;
                found.ToString();
                found = line.IndexOf(":");
                Console.WriteLine(line);
                try
                {
                    DizLowLevAnInput.Add(line.Substring(0, found).Trim(), line.Substring(found).Trim());
                }
                catch (Exception e01)
                {
                    Console.WriteLine(e01.Message);
                }
            }

            #endregion
            //GPFx21

            #region Combo boxes items values
            var digitalInputsValues = new List<Tuple<int, string>>();
            for (int i = 0; i < Constants.Hardware.IO.NumberOfDigitalInputs; i++)
            {
            //GPIx21
                //int index = i + 1;
                //digitalInputsValues.Add(new Tuple<int, string>(index, $"Input {index.ToString("00")}"));
                int index = i * 2;
                int index2 = index + 1;
                int index3 = i + 1;
                digitalInputsValues.Add(new Tuple<int, string>(index, $"Input {index3.ToString("00")} CLOSE"));
                digitalInputsValues.Add(new Tuple<int, string>(index2, $"Input {index3.ToString("00")} OPEN"));
            }
            int indexFin = (Constants.Hardware.IO.NumberOfDigitalInputs * 2);
            digitalInputsValues.Add(new Tuple<int, string>(indexFin, $"Always 0"));
            indexFin = indexFin + 1;
            digitalInputsValues.Add(new Tuple<int, string>(indexFin, $"Always 1"));
            //GPFx21

            var digitalOutputsValues = new List<Tuple<int, string>>();
            for (int i = 0; i < Constants.Hardware.IO.NumberOfDigitalOutputs; i++)
            {
                int index = i + 1;
                digitalOutputsValues.Add(new Tuple<int, string>(index, $"Output {index.ToString("00")}"));
            }
            //GPIx21
            indexFin = (Constants.Hardware.IO.NumberOfDigitalOutputs);
            digitalOutputsValues.Add(new Tuple<int, string>(indexFin, $"Always 0"));
            //indexFin = indexFin + 1;
            //digitalOutputsValues.Add(new Tuple<int, string>(indexFin, $"Always 1"));
            //GPFx21

            var analogInputsValues = new List<Tuple<int, string>>();
            for (int i = 0; i < Constants.Hardware.IO.NumberOfAnalogInputs; i++)
            {
                int index = i + 1;
                analogInputsValues.Add(new Tuple<int, string>(index, $"Input {index.ToString("00")}"));
            }
            //GPIx21
            //indexFin = (Constants.Hardware.IO.NumberOfAnalogInputs);
            //analogInputsValues.Add(new Tuple<int, string>(indexFin, $"Always 0"));
            //indexFin = indexFin + 1;
            //analogInputsValues.Add(new Tuple<int, string>(indexFin, $"Always 1"));
            //GPFx21
            #endregion

            #region Digital Inputs
            ch = 0;
            digitalInputs.Clear();
            Enum.GetNames(typeof(DigitalInput)).ForEach((x) =>
            {
                //GPIx21
                var mcbs = new MachineComboBoxItem();
                mcbs.PropertyName = x.Translate();
                mcbs.Name = x;
                mcbs.SetPropertyValues(digitalInputsValues);
                //mcbs.SelectedIndex = (ch * 2);  //perchè ho aggiunto gli CLOSE/OPEN del segnale (INPUT_MODE_NORMALY_ (CLOSE/OPEN))  
                //////DizLowLevHighLev[x]                                           //"FuseCheckMotors "
                //////DizLowLevDIinput[DizLowLevHighLev[x]]                         //"fuse_check_motors"
                //////DizLowLevOnOff[DizLowLevHighLev[x]]
                Console.WriteLine(DizLowLevDIinput[DizLowLevHighLev[x]].Substring(1));
                Console.WriteLine(DizLowLevOnOff[DizLowLevHighLev[x]].Substring(1));
                int iDIinput, iOnOff;
                iDIinput = int.Parse(DizLowLevDIinput[DizLowLevHighLev[x]].Substring(1));
                iOnOff = int.Parse(DizLowLevOnOff[DizLowLevHighLev[x]].Substring(1));
                if (iDIinput <= Constants.Hardware.IO.NumberOfDigitalInputs)
                {
                    if (iOnOff == 0)
                    {
                        //mcbs.SelectedIndex = (iDIinput * 2) - 2;      //giro da close a open
                        mcbs.SelectedIndex = (iDIinput * 2) - 1;
                    }
                    else
                    {
                        //mcbs.SelectedIndex = (iDIinput * 2) - 1;      //giro da open a close
                        mcbs.SelectedIndex = (iDIinput * 2) - 2;
                    }
                }
                else
                {
                    if (iDIinput == (Constants.Hardware.IO.NumberOfDigitalInputs + 1))
                    {
                        mcbs.SelectedIndex = (Constants.Hardware.IO.NumberOfDigitalInputs * 2);
                    }
                    if (iDIinput == (Constants.Hardware.IO.NumberOfDigitalInputs + 2))
                    {
                        mcbs.SelectedIndex = ((Constants.Hardware.IO.NumberOfDigitalInputs * 2) + 1);
                    }
                }
                mcbs.comboBoxPropertyValue.SelectedIndexChanged += ComboBoxPropertyValue_SelectedIndexChanged;
                //private List<MachineComboBoxItem> digitalInputs = new List<MachineComboBoxItem>();
                void ComboBoxPropertyValue_SelectedIndexChanged(object sender, EventArgs e)
                {
                    SettingChanged = true;
                    //MachineComboBoxItem comboBox = (MachineComboBoxItem)sender;
                    //string selected = (string)MachineComboBoxItem.SelectedItem;
                    Console.WriteLine("test");
                    Console.WriteLine(mcbs.Name);
                    Console.WriteLine(mcbs.comboBoxPropertyValue.Text);
                    //listboxDigitalInputs;      //qui c'è tutta la lista e le relative impostazioni!!!

                    string contents = "";
                    string[] lines = File.ReadAllLines(Constants.Path.LowLevelControl.DigitalInputsMapFile);
                    foreach (string line in lines)
                    {
                        int found = 0;
                        found.ToString();
                        found = line.IndexOf(":");
                        Console.WriteLine(line);
                        try
                        {
                            //DizLowLevDIinput.Add(line.Substring(0, found).Trim(), line.Substring(found).Trim());
                            contents = contents + line.Substring(0, found);
                            if (line.Substring(0, found).Trim() == DizLowLevHighLev[mcbs.Name])
                            {
                                if (mcbs.comboBoxPropertyValue.Text.Substring(0, 5) == "Input")
                                {
                                    contents = contents + ":" + mcbs.comboBoxPropertyValue.Text.Substring(6, 2) + "\r\n"; ;
                                }
                                else
                                {
                                    if (mcbs.comboBoxPropertyValue.Text.Trim() == "Always 0")
                                    {
                                        //contents = contents + ":33\r\n";
                                        contents = contents + $":{(Constants.Hardware.IO.NumberOfDigitalInputs + 1).ToString()}\r\n";
                                    }
                                    if (mcbs.comboBoxPropertyValue.Text.Trim() == "Always 1")
                                    {
                                        //contents = contents + ":34\r\n";
                                        contents = contents + $":{(Constants.Hardware.IO.NumberOfDigitalInputs + 2)}.ToString()\r\n";
                                    }
                                }
                            }
                            else
                            {
                                contents = contents + line.Substring(found).Trim() + "\r\n";
                            }
                        }
                        catch (Exception e01)
                        {
                            Console.WriteLine(e01.Message);
                        }
                    }
                    contents = contents.Substring(0, contents.Length - 2);
                    File.WriteAllText(Constants.Path.LowLevelControl.DigitalInputsMapFile, contents);

                    /////////////////////////////////////////////////////
                    contents = "";
                    lines = File.ReadAllLines(Constants.Path.LowLevelControl.DigitalInputsTypeMapFile);
                    foreach (string line in lines)
                    {
                        int found = 0;
                        found.ToString();
                        found = line.IndexOf(":");
                        Console.WriteLine(line);
                        try
                        {
                            //DizLowLevDIinput.Add(line.Substring(0, found).Trim(), line.Substring(found).Trim());
                            contents = contents + line.Substring(0, found);
                            if (line.Substring(0, found).Trim() == DizLowLevHighLev[mcbs.Name])
                            {
                                if (mcbs.comboBoxPropertyValue.Text.Substring(0, 5) == "Input")
                                {
                                    if (mcbs.comboBoxPropertyValue.Text.Substring(9) == "CLOSE")
                                    {
                                        //contents = contents + ":" + "0" + "\r\n";  //giro da close a open
                                        contents = contents + ":" + "1" + "\r\n";
                                    };
                                    if (mcbs.comboBoxPropertyValue.Text.Substring(9) == "OPEN")
                                    {
                                        //contents = contents + ":" + "1" + "\r\n";  //giro da open a close
                                        contents = contents + ":" + "0" + "\r\n";
                                    };
                                }
                                else
                                {
                                    contents = contents + ":" + "0" + "\r\n";
                                }
                            }
                            else
                            {
                                contents = contents + line.Substring(found).Trim() + "\r\n";
                            }
                        }
                        catch (Exception e01)
                        {
                            Console.WriteLine(e01.Message);
                        }
                    }
                    contents = contents.Substring(0, contents.Length - 2);
                    File.WriteAllText(Constants.Path.LowLevelControl.DigitalInputsTypeMapFile, contents);
                    /////////////////////////////////////////////////////

                    //Console.WriteLine($"DizLowLevDIinput For key = \"fuse_check_motors\", value = {DizLowLevDIinput["fuse_check_motors"]}.");
                    //Console.WriteLine($"DizLowLevOnOff For key = \"fuse_check_motors\", value = {DizLowLevOnOff["fuse_check_motors"]}.");
                    //Console.WriteLine($"DizLowLevHighLev For key = \"FuseCheckMotors\", value = {DizLowLevHighLev["FuseCheckMotors"]}.");

                }
                //GPFx21
                digitalInputs.Add(mcbs);

                ch++;
            });

            listboxDigitalInputs.Clear();
            digitalInputs.ForEach(x => listboxDigitalInputs.Add(x));
            #endregion

            #region Digital Outputs
            ch = 0;
            digitalOutputs.Clear();
            Enum.GetNames(typeof(DigitalOutput)).ForEach((x) =>
            {
                //GPIx21
                var mcbs = new MachineComboBoxItem();
                mcbs.PropertyName = x.Translate();
                mcbs.Name = x;
                mcbs.SetPropertyValues(digitalOutputsValues);
                //mcbs.SelectedIndex = ch;
                //////DoDizLowLevHighLev[x]                                           //"MotorOverturningOpSideLoad" -> "motor_overturning_op_side_load"
                //////DizLowLevDOutput[DoDizLowLevHighLev[x]]                         //"motor_overturning_op_side_load" -> ":01"
                Console.WriteLine(DizLowLevDOutput[DoDizLowLevHighLev[x]].Substring(1));
                int iDOutput;
                iDOutput = int.Parse(DizLowLevDOutput[DoDizLowLevHighLev[x]].Substring(1));
                if (iDOutput <= Constants.Hardware.IO.NumberOfDigitalOutputs)
                {
                    mcbs.SelectedIndex = (iDOutput) - 1;
                }
                else
                {
                    if (iDOutput == Constants.Hardware.IO.NumberOfDigitalOutputs + 1)
                    {
                        mcbs.SelectedIndex = Constants.Hardware.IO.NumberOfDigitalOutputs;
                    }
                    if (iDOutput == Constants.Hardware.IO.NumberOfDigitalOutputs + 2)
                    {
                        mcbs.SelectedIndex = Constants.Hardware.IO.NumberOfDigitalOutputs + 1;
                    }
                }
                mcbs.comboBoxPropertyValue.SelectedIndexChanged += ComboBoxPropertyValue_SelectedIndexChanged;
                //private List<MachineComboBoxItem> digitalOutputs = new List<MachineComboBoxItem>();
                void ComboBoxPropertyValue_SelectedIndexChanged(object sender, EventArgs e)
                {
                    SettingChanged = true;
                    //MachineComboBoxItem comboBox = (MachineComboBoxItem)sender;
                    //string selected = (string)MachineComboBoxItem.SelectedItem;
                    Console.WriteLine("test");
                    Console.WriteLine(mcbs.Name);
                    Console.WriteLine(mcbs.comboBoxPropertyValue.Text);
                    //listboxDigitalOutputs;      //qui c'è tuta la lista e le relative impostazioni!!!

                    string contents = "";
                    string[] lines = File.ReadAllLines(Constants.Path.LowLevelControl.DigitalOutputsMapFile);
                    foreach (string line in lines)
                    {
                        int found = 0;
                        found.ToString();
                        found = line.IndexOf(":");
                        Console.WriteLine(line);
                        try
                        {
                            //DizLowLevDIinput.Add(line.Substring(0, found).Trim(), line.Substring(found).Trim());
                            contents = contents + line.Substring(0, found);
                            if (line.Substring(0, found).Trim() == DoDizLowLevHighLev[mcbs.Name])
                            {
                                if (mcbs.comboBoxPropertyValue.Text.Substring(0, 6) == "Output")
                                {
                                    contents = contents + ":" + mcbs.comboBoxPropertyValue.Text.Substring(7, 2) + "\r\n"; ;
                                }
                                else
                                {
                                    if (mcbs.comboBoxPropertyValue.Text.Trim() == "Always 0")
                                    {
                                        //contents = contents + ":33\r\n";
                                        contents = contents + $":{(Constants.Hardware.IO.NumberOfDigitalOutputs + 1).ToString()}\r\n";
                                    }
                                    if (mcbs.comboBoxPropertyValue.Text.Trim() == "Always 1")
                                    {
                                        //contents = contents + ":34\r\n";
                                        contents = contents + $":{(Constants.Hardware.IO.NumberOfDigitalOutputs + 2).ToString()}\r\n";
                                    }
                                }
                            }
                            else
                            {
                                contents = contents + line.Substring(found).Trim() + "\r\n";
                            }
                        }
                        catch (Exception e01)
                        {
                            Console.WriteLine(e01.Message);
                        }
                    }
                    contents = contents.Substring(0, contents.Length - 2);
                    File.WriteAllText(Constants.Path.LowLevelControl.DigitalOutputsMapFile, contents);

                    //Console.WriteLine($"DizLowLevDIinput For key = \"button_cutter\", value = {DizLowLevDIinput["button_cutter"]}.");
                    //Console.WriteLine($"DizLowLevOnOff For key = \"button_cutter\", value = {DizLowLevOnOff["button_cutter"]}.");
                    //Console.WriteLine($"DizLowLevHighLev For key = \"ButtonCutter\", value = {DizLowLevHighLev["ButtonCutter"]}.");

                }
                //GPFx21
                digitalOutputs.Add(mcbs);

                ch++;
            });

            listboxDigitalOutputs.Clear();
            digitalOutputs.ForEach(x => listboxDigitalOutputs.Add(x));
            #endregion

            #region Analog Inputs
            ch = 0;
            analogInputs.Clear();
            Enum.GetNames(typeof(AnalogInput)).ForEach((x) =>
            {
                //GPIx21
                var mcbs = new MachineComboBoxItem();
                mcbs.PropertyName = x.Translate();
                mcbs.SetPropertyValues(analogInputsValues);
                //mcbs.SelectedIndex = ch;
                //////AnDizLowLevHighLev[x]                                           //"Dancer" -> "dancer"
                //////DizLowLevAnInput[AnDizLowLevHighLev[x]]                         //"dancer" -> ":1"
                Console.WriteLine(DizLowLevAnInput[AnDizLowLevHighLev[x]].Substring(1));
                int iDOutput;
                iDOutput = int.Parse(DizLowLevAnInput[AnDizLowLevHighLev[x]].Substring(1));
                if (iDOutput <= Constants.Hardware.IO.NumberOfAnalogInputs)
                {
                    mcbs.SelectedIndex = (iDOutput) - 1;
                }
                else
                {
                    if (iDOutput == Constants.Hardware.IO.NumberOfAnalogInputs + 1)
                    {
                        mcbs.SelectedIndex = Constants.Hardware.IO.NumberOfAnalogInputs;
                    }
                    if (iDOutput == Constants.Hardware.IO.NumberOfAnalogInputs + 2)
                    {
                        mcbs.SelectedIndex = Constants.Hardware.IO.NumberOfAnalogInputs + 1;
                    }
                }
                mcbs.comboBoxPropertyValue.SelectedIndexChanged += ComboBoxPropertyValue_SelectedIndexChanged;
                //private List<MachineComboBoxItem> analogInputs = new List<MachineComboBoxItem>();
                void ComboBoxPropertyValue_SelectedIndexChanged(object sender, EventArgs e)
                {
                    SettingChanged = true;
                    //MachineComboBoxItem comboBox = (MachineComboBoxItem)sender;
                    //string selected = (string)MachineComboBoxItem.SelectedItem;
                    Console.WriteLine("test");
                    Console.WriteLine(mcbs.Name);
                    Console.WriteLine(mcbs.comboBoxPropertyValue.Text);
                    //listboxAnalogInputs;      //qui c'è tuta la lista e le relative impostazioni!!!

                    string contents = "";
                    string[] lines = File.ReadAllLines(Constants.Path.LowLevelControl.AnalogInputsMapFile);
                    foreach (string line in lines)
                    {
                        int found = 0;
                        found.ToString();
                        found = line.IndexOf(":");
                        Console.WriteLine(line);
                        try
                        {
                            //DizLowLevDIinput.Add(line.Substring(0, found).Trim(), line.Substring(found).Trim());
                            contents = contents + line.Substring(0, found);
                            if (line.Substring(0, found).Trim() == AnDizLowLevHighLev[mcbs.Name])
                            {
                                if (mcbs.comboBoxPropertyValue.Text.Substring(0, 5) == "Input")
                                {
                                    //contents = contents + ":" + mcbs.comboBoxPropertyValue.Text.Substring(6, 2) + "\r\n";
                                    //Input 01 ma vuoi scritto solo per esempio: [dancer		:1] e non :01
                                    contents = contents + ":" + mcbs.comboBoxPropertyValue.Text.Substring(7, 1) + "\r\n";
                                }
                                else
                                {
                                    if (mcbs.comboBoxPropertyValue.Text.Trim() == "Always 0")
                                    {
                                        //contents = contents + ":5\r\n";
                                        contents = contents + $":{(Constants.Hardware.IO.NumberOfAnalogInputs + 1).ToString()}\r\n";
                                    }
                                    if (mcbs.comboBoxPropertyValue.Text.Trim() == "Always 1")
                                    {
                                        //contents = contents + ":6\r\n";
                                        contents = contents + $":{(Constants.Hardware.IO.NumberOfAnalogInputs + 2).ToString()}\r\n";
                                    }
                                }
                            }
                            else
                            {
                                contents = contents + line.Substring(found).Trim() + "\r\n";
                            }
                        }
                        catch (Exception e01)
                        {
                            Console.WriteLine(e01.Message);
                        }
                    }
                    contents = contents.Substring(0, contents.Length - 2);
                    File.WriteAllText(Constants.Path.LowLevelControl.AnalogInputsMapFile, contents);
                }
                //GPFx21
                analogInputs.Add(mcbs);

                ch++;
            });

            listboxAnalogInputs.Clear();
            analogInputs.ForEach(x => listboxAnalogInputs.Add(x));
            #endregion

            //-------------------------------------------------------
            // ResumeLayout
            //-------------------------------------------------------
            ResumeLayout();

            //Start with..
            mbDigitalInputs_Click(this, new EventArgs());
        }

        protected override void UpdateUIForm()
        {
            //--
        }

        private void CbReturn_Click(object sender, EventArgs e)
        {
            cbReturn.PulseButton();
            cbReturn.Active = false;

            Close();
        }

        private void DisactiveAllButtons(List<MachineButton> buttons)
        {
            buttons.ForEach(x => x.Active = false);
        }

        private void mbDigitalInputs_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);

            mbDigitalInputs.Active = true;
            mlType.Text = Localization.DigitalInputs;

            listboxDigitalInputs.Location = ListBoxLocation;
            listboxDigitalInputs.Size = ListBoxSize;

            listboxDigitalInputs.Visible = true;
            listboxDigitalOutputs.Visible = false;
            listboxAnalogInputs.Visible = false;
        }

        private void mbDigitalOutputs_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbDigitalOutputs.Active = true;
            mlType.Text = Localization.DigitalOutputs;

            listboxDigitalOutputs.Location = ListBoxLocation;
            listboxDigitalOutputs.Size = ListBoxSize;

            listboxDigitalInputs.Visible = false;
            listboxDigitalOutputs.Visible = true;
            listboxAnalogInputs.Visible = false;
        }

        private void mbAnalogInputs_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbAnalogInputs.Active = true;
            mlType.Text = Localization.AnalogInputs;

            listboxAnalogInputs.Location = ListBoxLocation;
            listboxAnalogInputs.Size = ListBoxSize;

            listboxDigitalInputs.Visible = false;
            listboxDigitalOutputs.Visible = false;
            listboxAnalogInputs.Visible = true;
        }
    }
}
