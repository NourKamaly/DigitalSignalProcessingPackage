using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }
        public override void Run()
        {
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
           // List<float> samples = new List<float>();
            float squared1=0, squared2=0, correlation=0, final_answer=0,normalization=0;
            int ctr,index,boundary;
            bool autoCorrelation = false;
            if (InputSignal2 == null)
            {
                autoCorrelation = true;
                InputSignal2 = new Signal(new List<float>(), InputSignal1.Periodic);
                for (ctr = 0; ctr < InputSignal1.Samples.Count(); ctr++)
                {
                    InputSignal2.Samples.Add(InputSignal1.Samples[ctr]);
                }
          
            }
            if (autoCorrelation)
            {
                boundary = InputSignal1.Samples.Count();
                for (ctr = 0; ctr < InputSignal1.Samples.Count(); ctr++)
                {
                    squared1 += (float)(Math.Pow((double)InputSignal1.Samples[ctr], 2));
                }
                normalization = squared1 * squared1;
                normalization = (float)Math.Sqrt((double)(normalization));
                normalization = normalization / boundary;
            }
            else
            {
                boundary = InputSignal1.Samples.Count() + InputSignal2.Samples.Count() -1 ;
                for (ctr = 0; ctr < InputSignal2.Samples.Count(); ctr++)
                {
                   squared2 += (float)(Math.Pow((double)InputSignal2.Samples[ctr], 2));
                }
                for (ctr = 0; ctr < InputSignal1.Samples.Count(); ctr++)
                {
                    squared1 += (float)(Math.Pow((double)InputSignal1.Samples[ctr], 2));
                }
                normalization = squared1 * squared2;
                normalization = (float)Math.Sqrt((double)(normalization));
                normalization = normalization / boundary;
                while (InputSignal1.Samples.Count() != boundary)
                {
                    InputSignal1.Samples.Add(0);
                }
                while (InputSignal2.Samples.Count() != boundary)
                {
                    InputSignal2.Samples.Add(0);
                }

            }
            if (InputSignal2.Periodic)
            {
                for (ctr = 0; ctr < boundary; ctr++)
                {
                    correlation = 0;
                    final_answer = 0;
                    for (index = 0; index < InputSignal2.Samples.Count(); index++)
                    {
                        correlation += InputSignal1.Samples[index] * InputSignal2.Samples[index];
                    }
                    final_answer = correlation / boundary;
                    OutputNonNormalizedCorrelation.Add(final_answer);
                    OutputNormalizedCorrelation.Add(final_answer / normalization);
                    float circular_shift = InputSignal2.Samples[0];
                    for (index = 0; index < InputSignal2.Samples.Count() - 1; index++)
                    {
                        InputSignal2.Samples[index] = InputSignal2.Samples[index + 1];
                    }
                    InputSignal2.Samples[InputSignal2.Samples.Count() - 1] = circular_shift;
                }
            }
            else
            {
                for (ctr = 0;ctr < boundary; ctr++)
                {
                    correlation = 0;
                    final_answer = 0;
                    for(index = 0; index < InputSignal2.Samples.Count(); index++)
                    {
                        correlation += InputSignal1.Samples[index] * InputSignal2.Samples[index];
                    }
                    final_answer = correlation / boundary;
                    OutputNonNormalizedCorrelation.Add(final_answer);
                    OutputNormalizedCorrelation.Add(final_answer / normalization);
                    for (index = 0; index < InputSignal2.Samples.Count() - 1; index++)
                    {
                        InputSignal2.Samples[index] = InputSignal2.Samples[index + 1];
                    }
                    InputSignal2.Samples[InputSignal2.Samples.Count() - 1] = 0;
                }
            }

        }
    }
}