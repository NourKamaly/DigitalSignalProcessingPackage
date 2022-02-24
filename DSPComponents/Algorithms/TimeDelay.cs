using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay:Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }

        public override void Run()
        {
            DirectCorrelation directCorr = new DirectCorrelation();
            directCorr.InputSignal1 = new Signal(InputSignal1.Samples,InputSignal1.Periodic);
            directCorr.InputSignal2 = new Signal(InputSignal2.Samples,InputSignal2.Periodic);
            directCorr.Run();
            int index=0;
            float abs_max = Math.Abs(directCorr.OutputNormalizedCorrelation[0]);
            for(int lag = 0; lag < directCorr.OutputNormalizedCorrelation.Count(); lag++)
            {
                if (Math.Abs(directCorr.OutputNormalizedCorrelation[lag]) > abs_max)
                {
                    abs_max = Math.Abs(directCorr.OutputNormalizedCorrelation[lag]);
                    index = lag;
                }
            }
            OutputTimeDelay = InputSamplingPeriod * index;
        }
    }
}
