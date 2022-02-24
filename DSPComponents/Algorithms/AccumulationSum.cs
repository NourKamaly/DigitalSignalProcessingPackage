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
            OutputSignal = new Signal(new List<float>(), InputSignal.Periodic);
            float sum = 0;
            int count = InputSignal.Samples.Count();
            for(int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    sum += InputSignal.Samples[i];
                    OutputSignal.Samples.Add(sum);
                }
                else
                {
                    sum = InputSignal.Samples[i];
                    OutputSignal.Samples.Add(sum);
                }
            }
            
        }
    }
}
