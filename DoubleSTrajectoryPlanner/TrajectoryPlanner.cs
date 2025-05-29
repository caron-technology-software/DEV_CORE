#undef MATLAB_HELPER
#undef PLOT_TRAJECTORY
#undef TRAJECTORY_LOG_ENABLED

#if DEBUG
#define TRAJECTORY_LOG_ENABLED
#endif

using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using ProRob.Extensions.Object;
using ProRob.Motion.Models;

namespace ProRob.Motion
{
    public static partial class TrajectoryPlanner
    {
        private const float MaxDeltaVelocity = 10.0f; //[mm/s]

#if MATLAB_HELPER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trajectory PlanTrajectory(TrajectoryParameters parameters)
        {
            return ProMatlabHelper.PlanTrajectory(parameters);
        }
#if PLOT_TRAJECTORY
        public static void PlotTrajectory(Trajectory trajectory)
        {
            ProMatlabHelper.PlotTrajectory(trajectory);
        }
#endif
#else

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trajectory PlanTrajectory(TrajectoryParameters parameters)
        {
            var trajectory = DoubleSTrajectoryPlanner.PlanTrajectory(parameters);

#if TRAJECTORY_LOG_ENABLED

            Console.WriteLine($"[Trajectory] NumberOfElements: {trajectory.NumberOfElements}");
            File.WriteAllText(@"C:\CARON\tmp\normal_trajectory.txt", trajectory.ToString().Replace(',', '.'));
#endif

            return trajectory;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trajectory PlanTrajectoryWithSlowStart(TrajectoryParameters parameters, float slowStartDistance, float scaleNormalFactor, float scaleFactorComponentSlowStart)
        {
            ProConsole.WriteLine($"PlanTrajectoryWithSlowStart", ConsoleColor.Red);
            Console.WriteLine(parameters);
            Console.WriteLine($"slowStartDistance:{slowStartDistance}");
            Console.WriteLine($"scaleNormalFactor:{scaleNormalFactor}");
            Console.WriteLine($"scaleFactorComponentSlowStart:{scaleFactorComponentSlowStart}");

            slowStartDistance = Math.Abs(slowStartDistance);

#if TRAJECTORY_LOG_ENABLED
            File.Delete(@"C:\CARON\tmp\slow_start_trajectory.txt");
            File.Delete(@"C:\CARON\tmp\normal_trajectory.txt");
            File.Delete(@"C:\CARON\tmp\status.txt");
#endif
            float slowStartScaleFactor = scaleNormalFactor * scaleFactorComponentSlowStart;

            var trajectory = DoubleSTrajectoryPlanner.PlanTrajectory(parameters);

            if (slowStartDistance < 1)
            {
#if TRAJECTORY_LOG_ENABLED
                File.WriteAllText(@"C:\CARON\tmp\slow_start_trajectory.txt", trajectory.ToString().Replace(',', '.'));
                File.WriteAllText(@"C:\CARON\tmp\normal_trajectory.txt", trajectory.ToString().Replace(',', '.'));
#endif
                return trajectory;
            }

            //---------------------------------------------------------
            // GESTIONE SCALATURA
            //---------------------------------------------------------

            //if (accelerationScaleFactor < thresholtAutomaticRamp)
            //{
            //    accelerationScaleFactor = thresholtAutomaticRamp;
            //}

            var parametersTrajectoryNoSlowStartLowVelocity = parameters.Clone();
            parametersTrajectoryNoSlowStartLowVelocity.VelocityScaleFactor = slowStartScaleFactor;
            parametersTrajectoryNoSlowStartLowVelocity.AccelerationScaleFactor = slowStartScaleFactor;

            var halfDistance = Math.Abs(parameters.Qfin - parameters.Qin) / 2.0f;

            if (slowStartDistance > halfDistance)
            {
                slowStartDistance = halfDistance;
            }

            var trajectoryNoSlowStartWithLowVelocity = DoubleSTrajectoryPlanner.PlanTrajectory(parametersTrajectoryNoSlowStartLowVelocity);

            try
            {
                Trajectory trj1 = null;
                Trajectory trj2 = null;
                Trajectory trj3 = null;
                Trajectory trj4 = null;

                bool negateTrajectory = false;
                if (trajectory.Q.First() > trajectory.Q.Last())
                {
                    negateTrajectory = true;
                }

                if (parameters.VelocityScaleFactor < slowStartScaleFactor ||
                    Math.Abs(parameters.Qfin - parameters.Qin) < slowStartDistance)
                {
                    ProConsole.WriteLine($"[PlanTrajectoryWithSlowStart] TrajectoryNoSlowStartWithLowVelocity", ConsoleColor.Yellow);
                    return trajectoryNoSlowStartWithLowVelocity;
                }
                else
                {
                    var maxVelocity = parameters.AxisSettings.MaxQp * parameters.VelocityScaleFactor;
                    bool saturationReached = false;
                    bool forceSecondConstructionStrategy = false;

                    trj1 = Trajectory.ComposeConstantAccelerationSegment(
                                           //Inputs
                                           slowStartDistance,
                                           maxVelocity,
                                           parameters.AxisSettings.MaxQpp * slowStartScaleFactor,
                                           parameters.SampleTime,

                                           //Outputs
                                           out saturationReached);
#if TRAJECTORY_LOG_ENABLED
                    ProConsole.WriteLine($"trj1.Q.Last(): {trj1.Q.Last()}", ConsoleColor.Yellow);

                    ProConsole.WriteLine($"saturationReached: {saturationReached}", ConsoleColor.Yellow);
#endif

                    float maxQp = trajectory.Qp.Select(x => Math.Abs(x)).Max();
                    float distanceTrj4 = Math.Abs(trajectory.Q.Last() / 2.0f);

                    float q0 = trj1.Q.Last();
                    float qp0 = trj1.Qp.Last();

                    //Composizione traiettoria con accelerazione massima
                    trj2 = Trajectory.ComposeConstantAccelerationSegmentWithInitialVelocity(
                                              q0, qp0,
                                              distanceTrj4,
                                              maxQp, parameters.AxisSettings.MaxQpp * parameters.AccelerationScaleFactor,
                                              parameters.SampleTime);
#if TRAJECTORY_LOG_ENABLED

                    if (trj2.NumberOfElements > 0)
                    {
                        ProConsole.WriteLine($"trj2.Q.Last(): {trj2.Q.Last()}", ConsoleColor.Yellow);
                    }
#endif
                    //------------------------------------------------
                    // CASE 1
                    //------------------------------------------------
                    Console.WriteLine("CASE 1");

                    float distanceTrj3 = (trj2.NumberOfElements > 0) ? distanceTrj4 - trj2.Q.Last() : distanceTrj4;

                    var q0Trj3 = (trj2.NumberOfElements > 0) ? trj2.Q.Last() : trj1.Q.Last();
                    var qp0Trj3 = (trj2.NumberOfElements > 0) ? trj2.Qp.Last() : trj1.Qp.Last();

                    trj3 = Trajectory.ComposeConstantVelocitySegment(
                                            distanceTrj3,
                                            q0Trj3, qp0Trj3,
                                            parameters.SampleTime);

                    if (trj3.NumberOfElements == 0)
                    {
                        forceSecondConstructionStrategy = true;
                    }

                    trj4 = Trajectory.TakeElementsFromStartIndex(trajectory, (int)Math.Round(trajectory.NumberOfElements / 2.0));

                    if (negateTrajectory)
                    {
                        trj4 = Trajectory.Negate(trj4);
                    }

                    var testTrajectory = Trajectory.ComposeTrajectories(Trajectory.ComposeTrajectories(Trajectory.ComposeTrajectories(trj1, trj2), trj3), trj4);

                    if (trj3.NumberOfElements > 0)
                    {
                        Console.WriteLine($"CONTINUITY {trj3.Qp.Last()} {trj4.Qp.First()}");
                    }

                    if (trj3.NumberOfElements > 0 && Math.Abs(trj3.Qp.Last() - trj4.Qp.First()) > MaxDeltaVelocity)
                    {
                        forceSecondConstructionStrategy = true;
                    }

                    Console.WriteLine($"forceSecondConstructionStrategy: {forceSecondConstructionStrategy}");

                    var c1 = Math.Abs(trajectoryNoSlowStartWithLowVelocity.Length - trj4.Q.Last()) > 1.0;
                    var c2 = Trajectory.CheckPositionContinuity(testTrajectory) == false;
#if TRAJECTORY_LOG_ENABLED
                    ProConsole.WriteLine($"c1:{c1} ({trajectoryNoSlowStartWithLowVelocity.Length} {trj4.Q.Last()}) c2:{c2}", ConsoleColor.Yellow);
                    File.WriteAllText(@"C:\CARON\tmp\test_trajectory.txt", testTrajectory.ToString().Replace(',', '.'));
#endif
                    if (forceSecondConstructionStrategy || c1 || c2)
                    {
                        //------------------------------------------------
                        // CASE 2
                        //------------------------------------------------
                        Console.WriteLine("CASE 2");

                        trj3 = new Trajectory(0);

                        var lastQ = (trj2.NumberOfElements > 0) ? trj2.Q.Last() : trj1.Q.Last();
                        var lastQp = (trj2.NumberOfElements > 0) ? trj2.Qp.Last() : trj1.Qp.Last();

                        float remainingDistance = Math.Abs(parameters.Qfin) - Math.Abs(lastQ);

#if TRAJECTORY_LOG_ENABLED
                        ProConsole.WriteLine($"remainingDistance: {remainingDistance}", ConsoleColor.Yellow);
#endif
                        var paramsTrj4 = parameters.Clone();

                        //paramsTrj4.Qfin = 2 * trj2.Q.Last();
                        paramsTrj4.Qfin = 2 * remainingDistance;

                        //paramsTrj4.VelocityScaleFactor = trj2.Qp.Last() / paramsTrj4.AxisSettings.MaxQp;
                        paramsTrj4.VelocityScaleFactor = lastQp / paramsTrj4.AxisSettings.MaxQp;

                        trj4 = DoubleSTrajectoryPlanner.PlanTrajectory(paramsTrj4);
                        trj4 = Trajectory.TakeElementsFromStartIndex(trj4, (int)Math.Round(trj4.NumberOfElements / 2.0));
                    }
#if DEBUG
                    File.WriteAllText(@"C:\TMP\trj1.txt", trj1.ToString().Replace(',', '.'));
                    File.WriteAllText(@"C:\TMP\trj2.txt", trj2.ToString().Replace(',', '.'));
                    File.WriteAllText(@"C:\TMP\trj3.txt", trj3.ToString().Replace(',', '.'));
                    File.WriteAllText(@"C:\TMP\trj4.txt", trj4.ToString().Replace(',', '.'));
                    File.WriteAllText(@"C:\TMP\trj4.txt", trj4.ToString().Replace(',', '.'));
#endif

                    //Console.WriteLine($"trj1: {trj1.NumberOfElements}");
                    //Console.WriteLine($"trj2: {trj2.NumberOfElements}");
                    //Console.WriteLine($"trj3: {trj3.NumberOfElements}");
                    //Console.WriteLine($"trj4: {trj4.NumberOfElements}");
                    //Console.WriteLine($"trj1-4: {trj1.NumberOfElements + trj2.NumberOfElements + trj3.NumberOfElements + trj4.NumberOfElements}");

                    var slowStartTrajectory = Trajectory.ComposeTrajectories(Trajectory.ComposeTrajectories(Trajectory.ComposeTrajectories(trj1, trj2), trj3), trj4);

                    if (!Trajectory.CheckContinuity(slowStartTrajectory) || !Trajectory.CheckPositionContinuity(slowStartTrajectory))
                    {
                        ProConsole.WriteLine($"[PlanTrajectoryWithSlowStart] FAILED (Checks Continuity)", ConsoleColor.Red);

#if TRAJECTORY_LOG_ENABLED
                        File.WriteAllText(@"C:\CARON\tmp\status.txt", $"[PlanTrajectoryWithSlowStart] FAILED (CheckContinuity)");
#endif
                        return trajectoryNoSlowStartWithLowVelocity;
                    }

                    if (negateTrajectory)
                    {
                        slowStartTrajectory = Trajectory.Negate(slowStartTrajectory);
                    }

#if TRAJECTORY_LOG_ENABLED

                    File.WriteAllText(@"C:\CARON\tmp\slow_start_trajectory.txt", slowStartTrajectory.ToString().Replace(',', '.'));
                    File.WriteAllText(@"C:\CARON\tmp\normal_trajectory.txt", trajectory.ToString().Replace(',', '.'));
#endif
                    float deltaPositionError = Math.Abs(slowStartTrajectory.Q.Last() - trajectory.Q.Last());
                    if (deltaPositionError > 2.0)
                    {
                        ProConsole.WriteLine($"[PlanTrajectoryWithSlowStart] FAILED (DeltaPositionError:{deltaPositionError}mm)");

#if TRAJECTORY_LOG_ENABLED
                        File.WriteAllText(@"C:\CARON\tmp\status.txt", $"[PlanTrajectoryWithSlowStart] FAILED (DeltaPositionError:{deltaPositionError}mm)");
#endif
                        return trajectoryNoSlowStartWithLowVelocity;
                    }

                    return slowStartTrajectory;
                }
            }
            catch (Exception ex)
            {
                ProConsole.WriteLine($"[EXCEPTION] PlanTrajectoryWithSlowStart] TrajectoryNoSlowStartWithLowVelocity\n\tMessage:{ex.Message}\n\tSource:{ex.Source}", ConsoleColor.Yellow);

#if TRAJECTORY_LOG_ENABLED
                File.WriteAllText(@"C:\CARON\tmp\status.txt", $"[EXCEPTION] PlanTrajectoryWithSlowStart] TrajectoryNoSlowStartWithLowVelocity");
#endif
                return trajectoryNoSlowStartWithLowVelocity;
            }
        }

#if PLOT_TRAJECTORY
        public static void PlotTrajectory(Trajectory trajectory)
        {
            Task.Run(() =>
            {
                try
                {
                    string json = JsonConvert.SerializeObject(trajectory);
                    var data = Encoding.UTF8.GetBytes(json);

                    TcpClient client = new TcpClient(IPAddress.Parse("127.0.0.1").ToString(), 11000);

                    NetworkStream stream = client.GetStream();

                    // Send the message to the connected TcpServer. 
                    stream.Write(data, 0, data.Length);
                    //Console.WriteLine(data.Length);
                    stream.Close();
                    client.Close();
                }
                catch
                {

                }
            });
        }
#endif
#endif
    }
}
