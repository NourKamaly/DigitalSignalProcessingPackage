using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            OutputConvolvedSignal = new Signal(new List<float>(),new List<int>(),InputSignal1.Periodic);
            // summation of x(k)h(n-k)
            double response;
            int lower_boundary = InputSignal1.SamplesIndices.Min()+InputSignal2.SamplesIndices.Min();
            int upper_boundary = InputSignal1.SamplesIndices.Max()+InputSignal2.SamplesIndices.Max();
            int n;
            for ( n = lower_boundary; n <= upper_boundary; n++)
            {   
                response = 0;
                for (int k = lower_boundary; k < InputSignal1.Samples.Count(); k++)
                {
                    if (n - k >= InputSignal2.Samples.Count())
                    {
                        continue;
                    }
                    if (n - k < InputSignal2.SamplesIndices.Min()||n-k> InputSignal2.SamplesIndices.Max())
                    {
                        continue;
                    }
                    if (k < InputSignal1.SamplesIndices.Min()|| k>InputSignal1.SamplesIndices.Max())
                    {
                        continue;
                    }
                    int index1 = InputSignal1.SamplesIndices.IndexOf(k);
                    int index2 = InputSignal2.SamplesIndices.IndexOf(n - k);
                    response += (double)InputSignal1.Samples[index1] * (double)InputSignal2.Samples[index2] ;

                }
                if(n==upper_boundary&& response==0.0){
                    continue;
                }
                OutputConvolvedSignal.SamplesIndices.Add(n);
                OutputConvolvedSignal.Samples.Add((float)(Math.Round(response,8)));
            }
        }
    }
}
