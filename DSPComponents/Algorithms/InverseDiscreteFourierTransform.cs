using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }
        private Signal readFromFile()
        {
            Signal InputFreqDomainSignalFromFile = new Signal(new List<float>(),false);
            InputFreqDomainSignalFromFile.FrequenciesAmplitudes = new List<float>();
            InputFreqDomainSignalFromFile.FrequenciesPhaseShifts = new List<float>();
            string filePath = "Frequency_Components.txt";
            var Lines = File.ReadAllLines(filePath);
            foreach (var line in Lines)
            {
                string[] numbers = line.Split(' ');
                int i = 0;
                foreach (var number in numbers)
                {
                    float val = float.Parse(number);
                    if (i == 0)
                    {
                        InputFreqDomainSignalFromFile.FrequenciesAmplitudes.Add(val);
                    }
                    else
                    {
                        InputFreqDomainSignalFromFile.FrequenciesPhaseShifts.Add(val);
                    }
                }

            }
            return InputFreqDomainSignalFromFile;
        }
        public override void Run()
        {
            OutputTimeDomainSignal = new Signal(new List<float>(), InputFreqDomainSignal.Periodic);
            Complex j = new Complex(0, 1);
            int num_of_samples = InputFreqDomainSignal.FrequenciesAmplitudes.Count();
            int k = num_of_samples;
            for (int outer_ctr=0;outer_ctr < num_of_samples; outer_ctr++)
            {
                Complex answer = new Complex(0, 0);
                for(int inner_ctr = 0; inner_ctr < k; inner_ctr++)
                {
                    double magnetude = (double)(InputFreqDomainSignal.FrequenciesAmplitudes[inner_ctr]);
                    double phase = (double)(InputFreqDomainSignal.FrequenciesPhaseShifts[inner_ctr]);
                    Complex sample = Complex.FromPolarCoordinates(magnetude, phase);
                    double pre_power = (outer_ctr * 2 * Math.PI * inner_ctr) / num_of_samples;
                    Complex power = Complex.Multiply(pre_power, j);
                    answer = Complex.Add(answer, Complex.Multiply(sample, Complex.Exp(power)));
                }
                float original_sample = (float)(answer.Real);
                original_sample /=  num_of_samples;
                OutputTimeDomainSignal.Samples.Add((float)(original_sample));
                
            }

        }
    }
}
