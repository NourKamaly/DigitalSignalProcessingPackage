using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            OutputFoldedSignal = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);
            int max = InputSignal.Samples.Count();
            int[] indecies = new int[max];
            float[] values = new float[max];
            for (int i = 0; i < max; i++)
            {
                int sample_index = -(InputSignal.SamplesIndices[i]);
                indecies[i] = sample_index;
                values[i] = InputSignal.Samples[i];
             
            }
            for (int i = max - 1; i >= 0; i--)
            {
                OutputFoldedSignal.SamplesIndices.Add(indecies[i]);
                OutputFoldedSignal.Samples.Add(values[i]);
            }
        }
    }
}
