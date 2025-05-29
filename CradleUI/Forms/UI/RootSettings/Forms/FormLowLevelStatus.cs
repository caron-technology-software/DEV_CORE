using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Extensions.Collections;

using Machine;
using Machine.UI.Controls;

using Caron.Cradle.Control.LowLevel;
using Machine.UI.Communication;

namespace Caron.Cradle.UI
{
    public partial class FormLowLevelStatus : FormCradleBase
    {
        private const int IntervalUpdate = 10; //[ms]

        private List<MachineIOStatus> digitalInputs = new List<MachineIOStatus>();
        private List<MachineIOStatus> digitalOutputs = new List<MachineIOStatus>();
        private List<MachineIOStatus> analogInputs = new List<MachineIOStatus>();
        private List<MachineIOStatus> encoders = new List<MachineIOStatus>();

        private List<MachineButton> buttons = new List<MachineButton>();

        public FormLowLevelStatus()
        {
            InitializeComponent();

            cbReturn.StateChangeActivated = false;

            mbDigitalInputs.StateChangeActivated = false;
            mbDigitalOutputs.StateChangeActivated = false;
            mbEncoders.StateChangeActivated = false;
            mbAnalogInputs.StateChangeActivated = false;

            mlDigitalInputs.Text = Localization.DigitalInputs;
            mlDigitalOuputs.Text = Localization.DigitalOutputs;
            mlAnalogInputs.Text = Localization.AnalogInputs;
            mlEncoders.Text = Localization.Encoders;

            buttons.Clear();
            buttons.AddRange(new List<MachineButton>()
            {
                mbDigitalInputs,
                mbDigitalOutputs,
                mbEncoders,
                mbAnalogInputs,
            });

            int ch;

            #region Digital Inputs
            //GPIx21
            ch = 1;
            //GPFx21
            digitalInputs.Clear();
            Enum.GetNames(typeof(DigitalInput)).ForEach((x) =>
            {
                digitalInputs.Add(new MachineIOStatus()
                {
                    Channel = $"CHANNEL {ch.ToString("00")}",
                    Name = x.Translate(),
                    Value = $""
                });

                ch++;
            });
            #endregion

            #region Digital Outputs
            //GPIx21
            ch = 1;
            //GPFx21
            digitalOutputs.Clear();
            Enum.GetNames(typeof(DigitalOutput)).ForEach((x) =>
            {
                //GPIx21
                //digitalOutputs.Add(new MachineIOStatus()
                //{
                //    Channel = $"CHANNEL {ch.ToString("00")}",
                //    Name = x.Translate(),
                //    Value = $"",
                //    Index = ch - 1
                //});
                string str01;
                //////funziona solo con la SPREADER in quanto per la CRADLE la scritta è "CHANNEL 14 " con spazio dopo:
                if (ch != 14)
                {
                    str01 = ch.ToString("00");
                }
                else
                {
                    //////Anche il trade axis del channel 14 ha lo stesso comportamento di quello della spreader nella configurazione degli stati del digital outputs, corretto il codice.
                    //str01 = ch.ToString("00") + " ";
                    str01 = ch.ToString("00");
                }
                
                var machineIoStatus = new MachineIOStatus()
                {
                    //Channel = $"CHANNEL {ch.ToString("00")}",
                    Channel = $"CHANNEL {str01}",
                    Name = x.Translate(),
                    Value = $"",
                    Index = ch - 1
                };

                machineIoStatus.ActionUp = () => CommandActionUp(machineIoStatus.Index);
                machineIoStatus.ActionDown = () => CommandActionDown(machineIoStatus.Index);

                digitalOutputs.Add(machineIoStatus);
                //GPFx21

                ch++;
            });
            #endregion

            #region Analog Inputs
            //GPIx21
            ch = 1;
            //GPFx21
            analogInputs.Clear();
            Enum.GetNames(typeof(AnalogInput)).ForEach((x) =>
            {
                analogInputs.Add(new MachineIOStatus()
                {
                    Channel = $"CHANNEL {ch.ToString("00")}",
                    Name = x.Translate(),
                    Value = $""
                });

                ch++;
            });
            #endregion

            #region Encoder
            ch = 0;
            encoders.Clear();
            Enum.GetNames(typeof(Encoders)).ForEach((x) =>
            {
                encoders.Add(new MachineIOStatus()
                {
                    Channel = $"ENCODER {ch.ToString("00")}",
                    Name = x.Translate(),
                    Value = $""
                });

                ch++;
            });
            #endregion
        }

        //GPIx21
        private void CommandActionUp(int index)
        {
            Console.WriteLine($"CommandActionUp => {index}");
            Communicator.SendHttpGetRequest($"external_commands", $"set_digital_output?index={index}&value=0");
        }
        private void CommandActionDown(int index)
        {
            Console.WriteLine($"CommandActionDown => {index}");
            Communicator.SendHttpGetRequest($"external_commands", $"set_digital_output?index={index}&value=1");
        }
        //GPFx21

        protected override void UpdateUIForm()
        {
            labelTitle.Text = Localization.LowLevelStatus;
        }

        private void DisactiveAllButtons(List<MachineButton> buttons)
        {
            buttons.ForEach(x => x.Active = false);
        }

        private void FormLowLevelStatus_Load(object sender, EventArgs e)
        {
            //Start with..
            mbDigitalInputs_Click(this, new EventArgs());
        }

        private void cbReturn_Click(object sender, EventArgs e)
        {
            cbReturn.PulseButton();
            cbReturn.Active = false;

            //GPIx21
            #region All Digital Outputs False
            for (int i = 0; i < digitalOutputs.Count; i++)
            {
                bool value = Supervisor.Control.LowLevel.IO.DigitalOutputs[i];
                //digitalOutputs[i].Value = value.ToString();
                if (value)
                {
                    ////con questo comando posso spegnere il digital output in uscita se è true 
                    /// 
                    CommandActionUp(i);
                }

            }
            //int ch = 1;
            //Enum.GetNames(typeof(DigitalOutput)).ForEach((x) =>
            //{
            //    //in uscita mando in false tutti i digital outputs:
            //    CommandActionUp(ch -1);

            //    ch++;
            //});
            #endregion

            //dispatcher_spread_task
            Communicator.SendHttpGetRequest($"external_commands", $"set_enable_io_settings?enable=false");
            Communicator.SetHighLevelControlState("normal");  //diverso dallo Spreader
            //GPFx21

            Close();
        }

        private void mbDigitalInputs_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbDigitalInputs.Active = true;
            mlType.Text = Localization.DigitalInputs;

            tableIO.Controls.Clear();
            tableIO.Controls.AddRange(digitalInputs.ToArray());

            Task.Run(() =>
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                while (mbDigitalInputs.Active)
                {
                    try
                    {
                        this?.Invoke((MethodInvoker)delegate ()
                        {
                            for (int i = 0; i < digitalInputs.Count; i++)
                            {
                                //GPIx21
                                digitalInputs[i].pannelButton.Visible = false;
                                //GPFx21

                                bool value = Supervisor.Control.LowLevel.IO.DigitalInputs[i];
                                digitalInputs[i].Value = value.ToString();

                                if (value)
                                {
                                    digitalInputs[i].SetColorLabel(Color.Green);
                                }
                                else
                                {
                                    digitalInputs[i].SetColorLabel(Color.Red);
                                }
                            }
                        });
                    }
                    catch
                    {
                        //--
                    }

                    Thread.Sleep(IntervalUpdate);
                }
            });
        }

        private void mbDigitalOutputs_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbDigitalOutputs.Active = true;

            mlType.Text = Localization.DigitalOutputs;

            tableIO.Controls.Clear();
            tableIO.Controls.AddRange(digitalOutputs.ToArray());

            Task.Run(() =>
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                while (mbDigitalOutputs.Active)
                {
                    this?.Invoke((MethodInvoker)delegate ()
                    {
                        for (int i = 0; i < digitalOutputs.Count; i++)
                        {
                            bool value = Supervisor.Control.LowLevel.IO.DigitalOutputs[i];
                            digitalOutputs[i].Value = value.ToString();

                            if (value)
                            {
                                digitalOutputs[i].SetColorLabel(Color.Green);
                                //GPIx21
                                digitalOutputs[i].pannelButton.BackgroundImage = digitalOutputs[i].pannelButton.ActiveBackgroundImage;
                                //GPFx21
                            }
                            else
                            {
                                digitalOutputs[i].SetColorLabel(Color.Red);
                                //GPIx21
                                digitalOutputs[i].pannelButton.BackgroundImage = digitalOutputs[i].pannelButton.InactiveBackgroundImage;
                                //GPFx21
                            }
                        }
                    });

                    Thread.Sleep(IntervalUpdate);
                }
                //GPIx21
                if (!mbDigitalOutputs.Active)
                {
                    for (int i = 0; i < digitalOutputs.Count; i++)
                    {
                        CommandActionUp(i);
                    }
                }
                //GPFx21
            });
        }

        private void mbAnalogInputs_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbAnalogInputs.Active = true;

            mlType.Text = Localization.AnalogInputs;

            tableIO.Controls.Clear();
            tableIO.Controls.AddRange(analogInputs.ToArray());

            Task.Run(() =>
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                while (mbAnalogInputs.Active)
                {
                    this?.Invoke((MethodInvoker)delegate ()
                    {
                        for (int i = 0; i < analogInputs.Count; i++)
                        {
                            float value = Supervisor.Control.LowLevel.IO.AnalogInputs[i];
                            //GPIx118
                            //////////float volts = (value / UInt16.MaxValue) * 10.0f;
                            float volts = (value / UInt16.MaxValue) * 20.0f;
                            //GPFx118
                            analogInputs[i].Value = $"{volts.ToString("0.000")} V";
                            //GPIx21
                            analogInputs[i].pannelButton.Visible = false;
                            //GPFx21
                        }
                    });

                    Thread.Sleep(IntervalUpdate);
                }
            });
        }

        private void mbEncoders_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbEncoders.Active = true;

            mlType.Text = Localization.Encoders;

            tableIO.Controls.Clear();
            tableIO.Controls.AddRange(encoders.ToArray());

            Task.Run(() =>
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                while (mbEncoders.Active)
                {
                    this?.Invoke((MethodInvoker)delegate ()
                    {
                        encoders[0].Value = Supervisor.Control.LowLevel.Axes.Cradle.Position.ToString("0.000 mm");
                        encoders[1].Value = Supervisor.Control.LowLevel.Axes.Table.Position.ToString("0.000 mm");
                        //GPIx21
                        encoders[0].pannelButton.Visible = false;
                        encoders[1].pannelButton.Visible = false;
                        //GPFx21
                    });

                    Thread.Sleep(IntervalUpdate);
                }
            });
        }
    }
}
