using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), InputSignal.Periodic);
            float sum = 0, mean;
            int count = InputSignal.Samples.Count(), ctr;
            for (ctr = 0; ctr < count; ctr++)
            {
                sum += InputSignal.Samples[ctr];
            }
            if (sum != 0)
            {
                mean = sum / (float)(count);
                for (ctr = 0; ctr < count; ctr++)
                {
                    OutputSignal.Samples.Add(InputSignal.Samples[ctr] - mean);
                }
            }
            else
            {
                OutputSignal = InputSignal;
            }
        }
    }
}
