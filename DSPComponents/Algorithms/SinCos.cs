using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            //formula is -> A cos (2*PI *f*n)
            /* according to sampling theorem : samplying frequency must be at least double the maximum analog
               frequency to avoid aliasing and in this case sampling freq is called the nyquist frequency */
            if (SamplingFrequency< AnalogFrequency*2)
            {
                return;
            }
            int boundary = (int)SamplingFrequency;
            float frequency = (float)(AnalogFrequency) / (float)(SamplingFrequency);
            if (type == "sin")
            {
                samples = calculateSamples(boundary, frequency, Math.Sin);
            }
            else if (type == "cos")
            {
                samples = calculateSamples(boundary, frequency, Math.Cos);
            }
        }
        private List<float> calculateSamples(int end,float frequency,Func<double,double> wave)
        {
            List<float> samples = new List<float>();
            double result;
            for (int ctr = 0;ctr < end; ctr++)
            {
                result = A * wave((2 * Math.PI * frequency * ctr) + PhaseShift);
                samples.Add((float)result);
            }
            return samples;
        }
    }
}
