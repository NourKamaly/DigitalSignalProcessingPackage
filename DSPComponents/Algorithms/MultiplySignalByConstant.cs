using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {
            OutputMultipliedSignal = new Signal(new List<float>(), false);
            int length = InputSignal.Samples.Count();
            for (int ctr =0;ctr < length; ctr++)
            {
                OutputMultipliedSignal.SamplesIndices.Add(ctr);
                float multiplication = InputConstant * InputSignal.Samples[ctr];
                OutputMultipliedSignal.Samples.Add(multiplication);
            }
        }
    }
}
