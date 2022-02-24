using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            OutputShiftedSignal = new Signal(new List<float>(), false);
            int boundary = InputSignal.Samples.Count();
            for (int i = 0; i < boundary; i++)
            {
                int index = InputSignal.SamplesIndices[i];
                OutputShiftedSignal.SamplesIndices.Add(index - ShiftingValue);
                OutputShiftedSignal.Samples.Add(InputSignal.Samples[i]);
            }
        }
    }
}
