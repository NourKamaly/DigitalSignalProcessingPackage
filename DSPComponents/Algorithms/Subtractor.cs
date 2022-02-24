using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(),new List<int>(),false);
            int stopper = InputSignal1.Samples.Count();
            for(int iterator = 0; iterator < stopper; iterator++)
            {
                OutputSignal.SamplesIndices.Add(iterator);
                float subtraction = InputSignal1.Samples[iterator] - InputSignal2.Samples[iterator];
                OutputSignal.Samples.Add(subtraction);
            }
        }
    }
}