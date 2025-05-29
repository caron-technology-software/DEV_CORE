#undef WRITE_TXT_TRAJECTORY

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using ProRob.Motion.Models;

namespace ProRob.Motion
{
    public class DoubleSTrajectoryPlanner
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trajectory PlanTrajectory(TrajectoryParameters parameters)
        {
            double ts = parameters.SampleTime;
            double q0 = parameters.Qin;
            double q1 = parameters.Qfin;
            double v0 = 0.0;
            double v1 = 0.0;
            double vMax = +parameters.AxisSettings.MaxQp;
            double vMin = -parameters.AxisSettings.MaxQp;
            double aMax = +parameters.AxisSettings.MaxQpp;
            double aMin = -parameters.AxisSettings.MaxQpp;
            double jMax = +parameters.AxisSettings.MaxQppp;
            double jMin = -parameters.AxisSettings.MaxQppp;

            vMax *= parameters.VelocityScaleFactor;
            vMin *= parameters.VelocityScaleFactor;
            aMax *= parameters.AccelerationScaleFactor;
            aMin *= parameters.AccelerationScaleFactor;
            jMax *= parameters.JerkScaleFactor;
            jMin *= parameters.JerkScaleFactor;

            var trajectory = PlanTrajectory(ts, q0, q1, v0, v1, vMax, vMin, aMax, aMin, jMax, jMin);

            return trajectory;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trajectory PlanTrajectory(
            double ts,
            double qIn, double qFin,
            double vIn, double vFin,
            double vMax, double vMin,
            double aMax, double aMin,
            double jMax, double jMin)
        {
#if DEBUG
            try
            {
#endif
            double[] qd;
            double[] qdp;
            double[] qdpp;
            double[] qdppp;

            double qd0 = qIn; double qd1 = qFin;
            double vd0 = vIn; double vd1 = vFin;

            double vdMax = vMax; double vdMin = vMin;
            double adMax = aMax; double adMin = aMin;
            double jdMax = jMax; double jdMin = jMin;

            double Ta, Td, Tj1, Tj2;

            // Given the initial conditions(qd_0, qd_1, vd_0, vd_1) compute
            // BLOCK 1
            // q_0, q_1, v_0, v_1, where sigma is direction of motion

            double sigma = Math.Sign(qd1 - qd0);

            qIn = sigma * qd0;
            qFin = sigma * qd1;
            vIn = sigma * vd0;
            vFin = sigma * vd1;

            vMax = (sigma + 1) / 2 * vdMax + (sigma - 1) / 2 * vdMin;
            vMin = (sigma + 1) / 2 * vdMin + (sigma - 1) / 2 * vdMax;
            aMax = (sigma + 1) / 2 * adMax + (sigma - 1) / 2 * adMin;
            aMin = (sigma + 1) / 2 * adMin + (sigma - 1) / 2 * adMax;
            jMax = (sigma + 1) / 2 * jdMax + (sigma - 1) / 2 * jdMin;
            jMin = (sigma + 1) / 2 * jdMin + (sigma - 1) / 2 * jdMax;

            // Assuming that v_max and a_max are reached compute the time intervals

            // acceleration part
            if (((vMax - vIn) * jMax) < Math.Pow(aMax, 2))
            {
                Tj1 = Math.Sqrt((vMax - vIn) / jMax);
                Ta = 2 * Tj1;
            }
            else
            {
                Tj1 = aMax / jMax;
                Ta = Tj1 + (vMax - vIn) / aMax;
            }

            //deceleration part
            if (((vMax - vFin) * jMax) < Math.Pow(aMax, 2))
            {
                Tj2 = Math.Sqrt((vMax - vFin) / jMax);
                Td = 2 * Tj2;
            }
            else
            {
                Tj2 = aMax / jMax;
                Td = Tj2 + (vMax - vFin) / aMax;
            }

            // finally it is possible to determine the time duration of the constant
            // velocity phase
            double Tv = (qFin - qIn) / vMax - Ta / 2 * (1 + vIn / vMax) - Td / 2 * (1 + vFin / vMax);

            // Now checking if Tv > 0 or smaller 0

            //v_max is not reached
            if (Tv < 0)
            {
                //set Tv = 0(because of errors in computation later)
                Tv = 0;
                // Reset the loop counter
                double count = 0;

                // Do this loop until the break condition holds
                for (double gamma = 1; gamma >= 0; gamma -= 0.001)
                {

                    aMax = gamma * aMax;
                    aMin = gamma * aMin;

                    // v_max is not reached => check other things

                    Tj1 = aMax / jMax;
                    Tj2 = aMax / jMax;
                    double Tj = aMax / jMax;
                    double delta = Math.Pow(aMax, 4) / Math.Pow(jMax, 2) + 2 * (Math.Pow(vIn, 2) + Math.Pow(vFin, 2)) + aMax * (4 * (qFin - qIn) - 2 * aMax / jMax * (vIn + vFin));
                    Ta = (Math.Pow(aMax, 2) / jMax - 2 * vIn + Math.Sqrt(delta)) / (2 * aMax);
                    Td = (Math.Pow(aMax, 2) / jMax - 2 * vFin + Math.Sqrt(delta)) / (2 * aMax);

                    if (Ta < 0)
                    {
                        Ta = 0;
                        Td = 2 * (qFin - qIn) / (vFin + vIn);
                        Tj2 = (jMax * (qFin - qIn) - Math.Sqrt(jMax * (jMax * Math.Pow((qFin - qIn), 2) + Math.Pow((vFin + vIn), 2) * (vFin - vIn)))) / (jMax * (vFin + vIn));
                    }
                    else if (Td < 0)
                    {
                        Td = 0;
                        Ta = 2 * (qFin - qIn) / (vFin + vIn);
                        Tj1 = (jMax * (qFin - qIn) - Math.Sqrt(jMax * (jMax * Math.Pow((qFin - qIn), 2) - Math.Pow((vFin + vIn), 2) * (vFin - vIn)))) / (jMax * (vFin + vIn));
                    }
                    else
                    {
                        if ((Ta > 2 * Tj) && (Td > 2 * Tj))
                        {
                            break;
                        }
                        else
                        {
                            count = count + 1;
                        }
                    }
                }
            }

            //Now compute the trajectory
            double a_lim_a = jMax * Tj1;
            double a_lim_d = -jMax * Tj2;
            double v_lim = vIn + (Ta - Tj1) * a_lim_a;

            double T = Ta + Tv + Td;

            //round final time to discrete ticks
            T = Math.Round(T * 1000) / 1000;

            //time goes from zero in Ts(sample Time) steps to final time T
            double[] time = new double[(int)(T / ts) + 1];
            for (int j = 0; j < time.Length; j++)
            {
                time[j] = ts * j;
            }

            //reading size of time array
            int timeSize = time.Length;

            //preallocating memoryround
            double[] q = new double[timeSize]; // q = zeros(1, timeSize(1, 2));
            double[] qp = new double[timeSize]; // qp = zeros(1, timeSize(1, 2));
            double[] qpp = new double[timeSize]; // qpp = zeros(1, timeSize(1, 2));
            double[] qppp = new double[timeSize]; // qppp = zeros(1, timeSize(1, 2));

            qd = new double[timeSize];
            qdp = new double[timeSize];
            qdpp = new double[timeSize];
            qdppp = new double[timeSize];

            //calculating the whole trajectory
            Parallel.For(0, timeSize, i =>
            {
                //ACCELERATION PHASE
                //t element of[0, Tj_1]
                if (time[i] <= Tj1)
                {
                    q[i] = qIn + vIn * time[i] + jMax * time[i] * time[i] * time[i] / 6;
                    qp[i] = vIn + jMax * time[i] * time[i] / 2;
                    qpp[i] = jMax * time[i];
                    qppp[i] = jMax;
                }

                //t element of[Tj_1, Ta - Tj_1]
                if ((time[i] > Tj1) && (time[i] <= (Ta - Tj1)))
                {
                    q[i] = qIn + vIn * time[i] + a_lim_a / 6 * (3 * time[i] * time[i] - 3 * Tj1 * time[i] + Tj1 * Tj1);
                    qp[i] = vIn + a_lim_a * (time[i] - Tj1 / 2);
                    qpp[i] = jMax * Tj1;
                    qppp[i] = 0;
                }

                //t element of[Ta - Tj_1, Ta]
                if ((time[i] > (Ta - Tj1)) && (time[i] <= Ta))
                {
                    q[i] = qIn + (v_lim + vIn) * Ta / 2 - v_lim * (Ta - time[i]) - jMin * (Ta - time[i]) * (Ta - time[i]) * (Ta - time[i]) / 6;
                    qp[i] = v_lim + jMin * (Ta - time[i]) * (Ta - time[i]) / 2;
                    qpp[i] = -jMin * (Ta - time[i]);
                    qppp[i] = jMin;
                }

                //CONSTANT VELOCITY PHASE
                //t element of[Ta, Ta + Tv]
                if ((time[i] > Ta) && (time[i] <= (Ta + Tv)))
                {
                    q[i] = qIn + (v_lim + vIn) * Ta / 2 + v_lim * (time[i] - Ta);
                    qp[i] = v_lim;
                    qpp[i] = 0;
                    qppp[i] = 0;
                }

                //DECELERATION PHASE
                //t element of[T - Td, T - Td + Tj_2]
                if ((time[i] > (Ta + Tv)) && (time[i] <= (Ta + Tv + Tj1)))
                {
                    q[i] = qFin - (v_lim + vFin) * Td / 2 + v_lim * (time[i] - T + Td) - jMax * (time[i] - T + Td) * (time[i] - T + Td) * (time[i] - T + Td) / 6;
                    qp[i] = v_lim - jMax * (time[i] - T + Td) * (time[i] - T + Td) / 2;
                    qpp[i] = -jMax * (time[i] - T + Td);
                    qppp[i] = jMin;
                }

                // t element of[T - Td + Tj_2, T - Tj_2]
                if ((time[i] > (Ta + Tv + Tj2)) && (time[i] <= (Ta + Tv + (Td - Tj2))))
                {
                    q[i] = qFin - (v_lim + vFin) * Td / 2 + v_lim * (time[i] - T + Td) + a_lim_d / 6 * (3 * (time[i] - T + Td) * (time[i] - T + Td) - 3 * Tj2 * (time[i] - T + Td) + Tj2 * Tj2);
                    qp[i] = v_lim + a_lim_d * (time[i] - T + Td - Tj2 / 2);
                    qpp[i] = -jMax * Tj2;
                    qppp[i] = 0;
                }

                // t element of[T - Tj_2, T]
                if ((time[i] > (Ta + Tv + (Td - Tj2))) && (time[i] <= T))
                {
                    q[i] = qFin - vFin * (T - time[i]) - jMax * (T - time[i]) * (T - time[i]) * (T - time[i]) / 6;
                    qp[i] = vFin + jMax * (T - time[i]) * (T - time[i]) / 2;
                    qpp[i] = -jMax * (T - time[i]);
                    qppp[i] = jMax;
                }

                // mark the end of trajectory
                if (time[i] > T)
                {
                    q[i] = qFin;
                    qp[i] = vFin;
                    qpp[i] = 0;
                    qppp[i] = 0;
                }

                qd[i] = sigma * q[i];
                qdp[i] = sigma * qp[i];
                qdpp[i] = sigma * qpp[i];
                qdppp[i] = sigma * qppp[i];
            });

#if WRITE_TXT_TRAJECTORY
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            for(int i=0;i<q.Length;i++)
            {
                sb1.AppendLine(qd[i].ToString("0.000"));
                sb2.AppendLine(qdp[i].ToString("0.000"));
            }
            File.WriteAllText($"{DateTime.UtcNow.Ticks}_q.txt",sb1.ToString());
            File.WriteAllText($"{DateTime.UtcNow.Ticks}_qp.txt", sb2.ToString());
#endif

            return new Trajectory()
            {
                Time = time.Select(x => (float)x).ToArray(),
                Q = qd.Select(x => (float)x).ToArray(),
                Qp = qdp.Select(x => (float)x).ToArray(),
                Qpp = qdpp.Select(x => (float)x).ToArray(),
                Qppp = qdppp.Select(x => (float)x).ToArray(),
            };
        }
#if DEBUG
            catch(Exception ex)
            {
                Console.WriteLine($"[PlanTrajectory] EXCEPTION ({ex.Message})");

                return new Trajectory();
            }
           
        }
#endif
    }
}
