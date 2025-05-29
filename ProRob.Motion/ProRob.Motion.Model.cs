using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Motion.Models
{
    public class Pid
    {
        public float Kp { get; set; }
        public float Ki { get; set; }
        public float Kd { get; set; }

        public Pid()
        {
            //--
        }
    }

    public class AxisSettings
    {
        public float MaxQp { get; set; }
        public float MaxQpp { get; set; }
        public float MaxQppp { get; set; }

        public AxisSettings()
        {
            //--
        }

        public override string ToString()
        {
            return $"[AxisSettings] MaxQp:{MaxQp}  MaxQpp:{MaxQpp}  MaxQppp:{MaxQppp}";
        }
    }

    public class TrajectoryParameters
    {
        public float SampleTime { get; set; }
        public float Qin { get; set; }
        public float Qfin { get; set; }
        public float VelocityScaleFactor { get; set; }
        public float AccelerationScaleFactor { get; set; }
        public float JerkScaleFactor { get; set; }
        public AxisSettings AxisSettings { get; set; }

        public TrajectoryParameters()
        {
            //--
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"[TrajectoryParameters]");
            sb.AppendLine($"\tSampleTime:{SampleTime}");
            sb.AppendLine($"\tQin:{Qin}");
            sb.AppendLine($"\tQfin:{Qfin}");
            sb.AppendLine($"\tVelocityScaleFactor:{VelocityScaleFactor}");
            sb.AppendLine($"\tAccelerationScaleFactor:{AccelerationScaleFactor}");
            sb.AppendLine($"\tJerkScaleFactor:{JerkScaleFactor}");
            sb.AppendLine($"\tAxisSettings:{AxisSettings.ToString()}");

            return sb.ToString();
        }
    }

    public class Trajectory
    {
        public float[] Time { get; set; }
        public float[] Q { get; set; }
        public float[] Qp { get; set; }
        public float[] Qpp { get; set; }
        public float[] Qppp { get; set; }

        public int NumberOfElements { get => (Q is null) ? 0 : Q.Length; }
        public float Length { get => (Q is null) ? 0 : Math.Abs(Q.Last() - Q.First()); }

        public Trajectory()
        {
            // --
        }

        public Trajectory(int n)
        {
            Time = new float[n];
            Q = new float[n];
            Qp = new float[n];
            Qpp = new float[n];
            Qppp = new float[n];
        }

        public static void SetTimeVector(Trajectory trajectory, float dt)
        {
            float t = 0.0f;

            for (int i = 0; i < trajectory.Time.Length; i++)
            {
                trajectory.Time[i] = t;
                t += dt;
            }
        }

        public static Trajectory Negate(Trajectory trajectory)
        {
            int n = trajectory.Time.Length;

            var trj = new Trajectory(n);

            for (int i = 0; i < n; i++)
            {
                trj.Q[i] = -trajectory.Q[i];
                trj.Qp[i] = -trajectory.Qp[i];
                trj.Qpp[i] = -trajectory.Qpp[i];
                trj.Qppp[i] = -trajectory.Qppp[i];
            }

            return trj;
        }

        public static Trajectory TakeElementsFromStartIndex(Trajectory trajectory, int startIndex)
        {
            int n = trajectory.Time.Length - startIndex;

            var trj = new Trajectory(n);

            for (int i = 0; i < n; i++)
            {
                int k = i + startIndex;

                trj.Q[i] = trajectory.Q[k];
                trj.Qp[i] = trajectory.Qp[k];
                trj.Qpp[i] = trajectory.Qpp[k];
                trj.Qppp[i] = trajectory.Qppp[k];
            }

            return trj;
        }

        public static bool CheckIfTrajectoriesLengthAreEquals(Trajectory trj1, Trajectory trj2, float maxDelta = 1.0f)
        {
            var d1 = Math.Abs(trj1.Q.Last() - trj1.Q.First());
            var d2 = Math.Abs(trj2.Q.Last() - trj2.Q.First());

            return Math.Abs(d2 - d1) < maxDelta;
        }

        public static Trajectory ComposeConstantVelocitySegment(float distance, float q0, float qp0, float dt)
        {
            int n = (int)Math.Round((distance / qp0) / dt);

            if (n <= 0)
            {
                return new Trajectory(0);
            }

            var trj = new Trajectory(n);

            float q = q0;
            float qp = qp0;

            for (int i = 0; i < n; i++)
            {
                q += qp * dt;

                trj.Q[i] = q;
                trj.Qp[i] = qp;
                trj.Qpp[i] = 0;
                trj.Qppp[i] = 0;
            }

            SetTimeVector(trj, dt);

            return trj;
        }

        public static Trajectory ComposeConstantAccelerationSegment(float distance, float maxVelocity, float acceleration, float dt, out bool saturationReached)
        {
            saturationReached = false;

            float totalTime = (float)Math.Sqrt(2.0f * distance / acceleration);
            int n = (int)Math.Round(totalTime / dt);
            var trj = new Trajectory(n);

            float q = 0;
            float qp = 0;

            trj.Q[0] = q;
            trj.Qp[0] = qp;
            trj.Qpp[0] = acceleration;
            trj.Qppp[0] = acceleration / dt;

            for (int i = 1; i < n; i++)
            {
                qp += acceleration * dt;

                if (qp >= maxVelocity)
                {
                    saturationReached = true;
                    qp = maxVelocity;
                }

                q += qp * dt;

                trj.Q[i] = q;
                trj.Qp[i] = qp;
                trj.Qpp[i] = acceleration;
                trj.Qppp[i] = 0.0f;
            }

            SetTimeVector(trj, dt);

            return trj;
        }

        public static Trajectory ComposeConstantAccelerationSegmentWithInitialVelocity(
            float q0,
            float qp0,
            float maxQ,
            float maxQp,
            float maxQpp,
            float dt)
        {
            var listQ = new List<float>();
            var listQp = new List<float>();

            qp0 += maxQpp * dt;
            q0 += qp0 * dt;

            qp0 += maxQpp * dt;
            q0 += qp0 * dt;

            float q = q0;
            float qp = qp0;

            while (qp < maxQp && q < maxQ)
            {
                qp += maxQpp * dt;
                q += qp * dt;

                listQ.Add(q);
                listQp.Add(qp);
            }

            int n = listQ.Count;
            var trj = new Trajectory(n);

            if (n == 0)
            {
                return trj;
            }

            trj.Q[0] = q0;
            trj.Qp[0] = qp0;
            trj.Qpp[0] = maxQpp;
            trj.Qppp[0] = maxQpp / dt;

            for (int i = 1; i < n; i++)
            {
                trj.Q[i] = listQ[i];
                trj.Qp[i] = listQp[i];
                trj.Qpp[i] = maxQp;
                trj.Qppp[i] = 0.0f;
            }

            SetTimeVector(trj, dt);

            return trj;
        }

        public static bool CheckContinuity(Trajectory trj)
        {
            float dt = trj.Time[1] - trj.Time[0];

            float maxQppTrj = float.MinValue;
            float maxDeltaQ = float.MinValue;

            for (int i = 1; i < trj.NumberOfElements - 1; i++)
            {
                float deltaQ = trj.Q[i] - trj.Q[i - 1];

                float qpp = (trj.Q[i + 1] - 2.0f * trj.Q[i] + trj.Q[i - 1]) / dt;

                if (Math.Abs(qpp) > maxQppTrj)
                {
                    maxQppTrj = Math.Abs(qpp);
                }

                if (Math.Abs(deltaQ) > maxDeltaQ)
                {
                    maxDeltaQ = Math.Abs(deltaQ);
                }
            }

            Console.WriteLine($"[CheckContinuity] dt:{dt * 1000.0f:0.0}ms maxDeltaQ: {maxDeltaQ}mm  maxQppTrj:{maxQppTrj}mm/s^2");

            return true;
        }

        public static bool CheckPositionContinuity(Trajectory trajectory, float maxDelta = 10.0f)
        {
            int n = trajectory.Q.Length;

            if (n == 0)
            {
                return false;
            }

            var v1 = trajectory.Q.Skip(1);
            var v2 = trajectory.Q.Take(n - 1);

            var diff = v1.Zip(v2, (first, second) => Math.Abs(first) - Math.Abs(second));
#if DEBUG
            float maxDiff = diff.Max();
            Console.WriteLine($"maxDiff: {maxDiff}");
#endif
            if (diff.Any(x => x > maxDelta))
            {
                return false;
            }

            return true;
        }
        public static Trajectory ComposeTrajectories(Trajectory trjA, Trajectory trjB)
        {
            int nA = trjA.Time.Length;
            int nB = trjB.Time.Length;
            int n = nA + nB;

            float dt = trjA.Time[1] - trjA.Time[0];

            var trj = new Trajectory(n);

            for (int i = 0; i < nA; i++)
            {
                trj.Q[i] = trjA.Q[i];
                trj.Qp[i] = trjA.Qp[i];
                trj.Qpp[i] = trjA.Qpp[i];
                trj.Qppp[i] = trjA.Qppp[i];
            }

            for (int i = 0; i < nB; i++)
            {
                int k = i + nA;
                trj.Q[k] = trjB.Q[i];
                trj.Qp[k] = trjB.Qp[i];
                trj.Qpp[k] = trjB.Qpp[i];
                trj.Qppp[k] = trjB.Qppp[i];
            }

            SetTimeVector(trj, dt);

            return trj;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            Q.ToList().ForEach(x => sb.AppendLine($"{x:0.000}"));

            return sb.ToString();
        }
    }
}
