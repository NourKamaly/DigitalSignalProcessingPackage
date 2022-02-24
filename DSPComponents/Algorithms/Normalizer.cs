using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            OutputNormalizedSignal = new Signal(new List<float>(), InputSignal.Periodic);
            int boundary = InputSignal.Samples.Count();
            float max = InputSignal.Samples.Max(), min= InputSignal.Samples.Min(),answer;
           if (InputMinRange == 0)
            {
                for (int ctr = 0; ctr < boundary; ctr++)
                {
                    OutputNormalizedSignal.SamplesIndices.Add(ctr);
                    answer = (InputSignal.Samples[ctr] - min) / (max - min);
                    OutputNormalizedSignal.Samples.Add(answer);
                }
            }
            else
            {
                for (int ctr = 0; ctr < boundary; ctr++)
                {
                    OutputNormalizedSignal.SamplesIndices.Add(ctr);
                    answer = 2*((InputSignal.Samples[ctr] - min) / (max - min)) - 1;
                    OutputNormalizedSignal.Samples.Add(answer);
                }
            }
        }
    }
}
