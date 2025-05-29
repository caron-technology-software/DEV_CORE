using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob.Motion.Models;

using MathWorks.MATLAB.NET.Arrays;
using MatlabHelper;

namespace ProRob.Motion
{
    public static class ProMatlabHelper
    {
        private static readonly DoubleSTrajectoryPlannerHelper trajPlanner = new DoubleSTrajectoryPlannerHelper();
        private static readonly PlotHelper plotHelper = new PlotHelper();

        public static Trajectory PlanTrajectory(TrajectoryParameters parameters)
        {
            var ret = (MWStructArray)trajPlanner.double_s_trajectory_planner(
                parameters.Qin,
                (MWArray)parameters.Qfin,
                (MWArray)parameters.SampleTime,
                (MWArray)parameters.AxisSettings.MaxQp,
                (MWArray)parameters.AxisSettings.MaxQpp,
                (MWArray)parameters.AxisSettings.MaxQppp,
                (MWArray)parameters.VelocityScaleFactor,
                (MWArray)parameters.AccelerationScaleFactor,
                (MWArray)parameters.JerkScaleFactor);

            return new Trajectory()
            {
                Time = ((double[])((MWNumericArray)ret["t", 1]).ToVector(MWArrayComponent.Real)).Select(x => (float)x).ToArray(),
                Q = ((double[])((MWNumericArray)ret["q", 1]).ToVector(MWArrayComponent.Real)).Select(x => (float)x).ToArray(),
                Qp = ((double[])((MWNumericArray)ret["qp", 1]).ToVector(MWArrayComponent.Real)).Select(x => (float)x).ToArray(),
                Qpp = ((double[])((MWNumericArray)ret["qpp", 1]).ToVector(MWArrayComponent.Real)).Select(x => (float)x).ToArray(),
                Qppp = ((double[])((MWNumericArray)ret["qppp", 1]).ToVector(MWArrayComponent.Real)).Select(x => (float)x).ToArray(),
            };

        }

        public static void PlotTrajectory(Trajectory trajectory)
        {
            plotHelper.plot_trajectory(
                new MWNumericArray(trajectory.Time),
                new MWNumericArray(trajectory.Q),
                new MWNumericArray(trajectory.Qp),
                new MWNumericArray(trajectory.Qpp),
                new MWNumericArray(trajectory.Qppp));
        }
    }
}
