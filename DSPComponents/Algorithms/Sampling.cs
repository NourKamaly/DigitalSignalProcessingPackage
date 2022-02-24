using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public override void Run()
        {
            if (M == 0 && L == 0)
            {
                Console.WriteLine("Both interpolation and decimation factors are zero ! Please enter a non-negative value");
                OutputSignal = InputSignal;
                return;
            }
            if (L != 0)
            {
                Signal upsampledSignal = interpolateSignal();
                filterSignal(upsampledSignal);
            }
            if (M != 0)
            {
                if (OutputSignal == null)
                {
                    filterSignal(InputSignal);
                    decimateSignal();
                }
                else
                {
                    decimateSignal();
                }
            }

        }
        private void filterSignal(Signal Input)
        {
            FIR FIR = new FIR();
            FIR.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            FIR.InputFS = 8000;
            FIR.InputStopBandAttenuation = 50;
            FIR.InputCutOffFrequency = 1500;
            FIR.InputTransitionBand = 500;
            FIR.InputTimeDomainSignal = Input;
            FIR.Run();
            OutputSignal = FIR.OutputYn;
        }
        private Signal interpolateSignal()
        {
            Signal upsampledSignal = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);
            int index = -1;
            for (int ctr = 0; ctr < InputSignal.Samples.Count(); ctr++)
            {
                index += 1;
                upsampledSignal.SamplesIndices.Add(index);
                upsampledSignal.Samples.Add(InputSignal.Samples[ctr]);
                for (int i = 0; i < L - 1; i++)
                {
                    index += 1;
                    upsampledSignal.SamplesIndices.Add(index);
                    upsampledSignal.Samples.Add(0);
                }
            }
            return upsampledSignal;
        }
        private void decimateSignal()
        {
            List<float> downsampled = new List<float>();
            for (int ctr = 0; ctr < OutputSignal.Samples.Count(); ctr += M)
            {
                downsampled.Add(OutputSignal.Samples[ctr]);
            }
            OutputSignal.Samples = downsampled;
        }
        
    }
    
}
