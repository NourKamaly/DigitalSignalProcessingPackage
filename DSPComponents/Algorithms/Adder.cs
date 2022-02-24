using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), InputSignals[0].Periodic);
            int lengthOfFirst = InputSignals[0].Samples.Count();
            int lengthOfSecond = InputSignals[1].Samples.Count();
            if (lengthOfFirst <= lengthOfSecond)
            {
                addSignals(lengthOfFirst, lengthOfSecond, 1);
            }
            else
            {
                addSignals(lengthOfSecond, lengthOfFirst, 0);
            }
        }
        private void addSignals(int smallerLen,int largerLen,short index)
        {
            int ctr;
            for (ctr = 0; ctr < smallerLen; ctr++)
            {
                OutputSignal.SamplesIndices.Add(ctr);
                OutputSignal.Samples.Add(InputSignals[0].Samples[ctr] + InputSignals[1].Samples[ctr]);
            }
            for (; ctr < largerLen; ctr++)
            {
                OutputSignal.SamplesIndices.Add(ctr);
                OutputSignal.Samples.Add(InputSignals[index].Samples[ctr]);
            }
        }
    }
}