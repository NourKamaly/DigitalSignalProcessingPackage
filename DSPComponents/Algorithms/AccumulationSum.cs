using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(),new List<int>(), InputSignal.Periodic);
            float sum = InputSignal.Samples[0];
            OutputSignal.Samples.Add(sum);
            OutputSignal.SamplesIndices.Add(0);
            int count = InputSignal.Samples.Count();
            for(int index = 1; index < count; index++)
            {
                sum += InputSignal.Samples[index];
                OutputSignal.Samples.Add(sum);
                OutputSignal.SamplesIndices.Add(index);

            }
            
        }
    }
}
