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
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }
        public override void Run()
        {
            OutputFreqDomainSignal = new Signal(new List<float>(), InputTimeDomainSignal.Periodic);
            OutputFreqDomainSignal.FrequenciesAmplitudes = new List<float>();
            OutputFreqDomainSignal.FrequenciesPhaseShifts = new List<float>();
            OutputFreqDomainSignal.Frequencies = new List<float>();
           Complex j = new Complex(0, -1);
            int k = InputTimeDomainSignal.Samples.Count();
            int num_of_samples = k;
            string frequency_components="";
            //string filePath = @"C:\Users\NOUR\Desktop\Junior year - term 5\Digital signal processing\Task 4 - polar coordinates of a transformed signal\Frequency components.txt";
            for (int outer_ctr = 0; outer_ctr < k; outer_ctr++)
            {
                OutputFreqDomainSignal.Frequencies.Add(outer_ctr);
                Complex answer = new Complex(0, 0);
                for (int inner_ctr=0;inner_ctr < num_of_samples; inner_ctr++)
                {
                    double sample = (double)(InputTimeDomainSignal.Samples[inner_ctr]);
                    double pre_power = (outer_ctr * 2 * Math.PI * inner_ctr) / num_of_samples;
                    Complex power = new Complex();
                    power = Complex.Multiply(pre_power, j);
                    answer += Complex.Multiply(sample, Complex.Exp(power));
                }
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add((float)(answer.Magnitude));
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add((float)(answer.Phase));
                string amplitude = ((float)(answer.Magnitude)).ToString();
                string phase_shift = ((float)(answer.Phase)).ToString();
                frequency_components += amplitude + ' ' + phase_shift+'\n';
            }
            string filePath = "Frequency_Components.txt";
            FileStream frequency_file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter file = new StreamWriter(frequency_file);
            file.BaseStream.Seek(0, SeekOrigin.End);
            file.WriteLine(frequency_components);
            file.Close();
        }
    }
}
