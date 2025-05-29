using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.DataCollections
{
    public class PlotsTimeSeries
    {
        public DateTime[] Timestamp { get; set; }
        public float[] CradlePosition { get; set; }
        public float[] CradlePositionError { get; set; }
        public float[] CradleMotorSpeedCommand { get; set; }
        public float[] CradleProportionalAction { get; set; }
        public float[] CradleDerivativeAction { get; set; }
        public float[] CradleIntegralAction { get; set; }
        public float[] CradleFeedForwardAction { get; set; }
        public float[] CradleVelocity { get; set; }
        public float[] TablePosition { get; set; }
        public float[] TableVelocity { get; set; }
        public float[] DancerNormalizedValue { get; set; }
        public PlotsTimeSeries()
        {
            //--
        }
    }
}
