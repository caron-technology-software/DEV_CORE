using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Machine.UI.Communication;

using Caron.Cradle.Control.DataCollections;

namespace Caron.Cradle.UI
{
    public partial class FormMachinePlots : FormCradleBase
    {
        public readonly int SampleRate = (int)(1 / Machine.Constants.Intervals.DataCollections.RefreshRate.TotalSeconds);
        public readonly int NumberOfLastPeriodElements = (int)(1 / Machine.Constants.Intervals.DataCollections.RefreshRate.TotalSeconds * Machine.Constants.Intervals.DataCollections.LastPeriodSamplingWindow.TotalSeconds);

        protected override void UpdateUIForm()
        {
            //--
        }

        private bool TryGetMachineContextCollectionElements(ref float[] times, ref float[] values)
        {
            try
            {
                //var dc = Communicator.GetEncodedData<List<MachineDataElement<Control.LowLevel.ControlStatus>>>("time_series", "low_level_control_status/encoded");
                //var dc = Communicator.GetData<List<MachineDataElement<Control.LowLevel.ControlStatus>>>("time_series", "low_level_control_status");
                //var dc = NewCommunicator.GetEncodedData<List<MachineDataElement<Control.LowLevel.ControlStatus>>>(uri);
                var dc = Communicator.GetData<PlotsTimeSeries>("time_series", "plot_time_series");

                if (dc is null)
                {
                    return false;
                }

                var t0 = dc.Timestamp.Last();
                times = dc.Timestamp.Select(x => (float)(x - t0).TotalSeconds).ToArray();

                // 0 $"{Localization.Cradle} ({Localization.Position})",
                // 1 $"{Localization.Cradle} ({Localization.Velocity})",
                // 2 $"{Localization.Cradle} ({Localization.Error})",
                // 3 $"{Localization.Cradle} ({Localization.DriverCommandSpeed})",
                // 4 $"{Localization.Cradle} ({Localization.ProportionalAction})",
                // 5 $"{Localization.Cradle} ({Localization.IntegralAction})",
                // 6 $"{Localization.Cradle} ({Localization.DerivativeAction})",
                // 7 $"{Localization.Cradle} ({Localization.FeedForwardAction})",
                // 8 $"{Localization.Table} ({Localization.Position})",
                // 9 $"{Localization.Table} ({Localization.Velocity})",
                //10 $"{Localization.Dancer} ({Localization.Percentual})"}

                switch (indexComboBoxItems)
                {
                    case 0:
                        values = dc.CradlePosition;
                        break;

                    case 1:
                        values = dc.CradleVelocity;
                        break;

                    case 2:
                        values = dc.CradlePositionError;
                        break;

                    case 3:
                        values = dc.CradleMotorSpeedCommand;
                        break;

                    case 4:
                        values = dc.CradleProportionalAction;
                        break;

                    case 5:
                        values = dc.CradleIntegralAction;
                        break;

                    case 6:
                        values = dc.CradleDerivativeAction;
                        break;

                    case 7:
                        values = dc.CradleFeedForwardAction;
                        break;

                    case 8:
                        values = dc.TablePosition;
                        break;

                    case 9:
                        values = dc.TableVelocity;
                        break;

                    case 10:
                        values = dc.DancerNormalizedValue;
                        break;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ThreadPlotUpdate()
        {
            try
            {
                float[] dataX = null;
                float[] dataY = null;

#pragma warning disable CS0618 // 'FormsPlot.plt' è obsoleto: 'Reference 'Plot' instead of 'plt''
                plot.plt.XLabel("time [s]");
#pragma warning restore CS0618 // 'FormsPlot.plt' è obsoleto: 'Reference 'Plot' instead of 'plt''
#pragma warning disable CS0618 // 'FormsPlot.plt' è obsoleto: 'Reference 'Plot' instead of 'plt''
                plot.plt.YLabel("Signal");
#pragma warning restore CS0618 // 'FormsPlot.plt' è obsoleto: 'Reference 'Plot' instead of 'plt''

                while (threadRunning)
                {
                    if (TryGetMachineContextCollectionElements(ref dataX, ref dataY))
                    {
                        if (showLastPeriod)
                        {
                            int n = dataY.Length;
                            int skip = n - NumberOfLastPeriodElements;

                            if (skip > 0)
                            {
                                dataY = dataY.Skip(skip).Take(n - skip - 1).ToArray();
                            }
                        }

                        double[] ys = dataY.Select(x => (double)x).ToArray();

                        this?.Invoke((MethodInvoker)delegate ()
                        {
#pragma warning disable CS0618 // 'FormsPlot.plt' è obsoleto: 'Reference 'Plot' instead of 'plt''
                            plot.plt.Clear();
#pragma warning restore CS0618 // 'FormsPlot.plt' è obsoleto: 'Reference 'Plot' instead of 'plt''
#pragma warning disable CS0618 // 'FormsPlot.plt' è obsoleto: 'Reference 'Plot' instead of 'plt''
#pragma warning disable CS0618 // 'Plot.PlotSignal(double[], double, double, double, Color?, double, double, string, Color[], int?, int?, LineStyle, bool)' è obsoleto: 'Use AddSignal() and customize the object it returns'
                            plot.plt.PlotSignal(ys, SampleRate, 1, markerSize: 0, lineWidth: 2);
#pragma warning restore CS0618 // 'Plot.PlotSignal(double[], double, double, double, Color?, double, double, string, Color[], int?, int?, LineStyle, bool)' è obsoleto: 'Use AddSignal() and customize the object it returns'
#pragma warning restore CS0618 // 'FormsPlot.plt' è obsoleto: 'Reference 'Plot' instead of 'plt''
                            plot.Render(true, true);
                        });
                    }

                    Thread.Sleep(showLastPeriod ? Machine.Constants.Intervals.Plots.FastRefreshRate : Machine.Constants.Intervals.Plots.SlowRefreshRate);
                }
            }
            catch
            {
                Console.WriteLine($"[{DateTime.Now}] PLOT ERROR");
            }
        }
    }
}