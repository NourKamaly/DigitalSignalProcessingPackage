using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; } // done
        public List<int> OutputIntervalIndices { get; set; } //done
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; } // dah done
        public override void Run()
        {
            OutputQuantizedSignal = new Signal(new List<float>(), InputSignal.Periodic);
            OutputIntervalIndices = new List<int>();
            OutputEncodedSignal = new List<string>();
            OutputSamplesError = new List<float>();
            if (InputNumBits != 0)
            {
                double temporary = (double)(InputNumBits);
                InputLevel =(int)( Math.Pow(2,temporary));
            }

            var encoded_levels = new List<string>();

            if (InputLevel <= 4)
            {
                encoded_levels.Add("00"); encoded_levels.Add("01"); encoded_levels.Add("10");
                encoded_levels.Add("11");
            }
           else if (InputLevel <=8 )
            {
                encoded_levels.Add("000"); encoded_levels.Add("001"); encoded_levels.Add("010");
                encoded_levels.Add("011"); encoded_levels.Add("100"); encoded_levels.Add("101");
                encoded_levels.Add("110"); encoded_levels.Add("111");
            }
            InputNumBits = encoded_levels[0].Length;

            float min_val = InputSignal.Samples.Min(), max_val = InputSignal.Samples.Max();
            int boundary = InputSignal.Samples.Count();
            float delta = (max_val - min_val) / (float)(InputLevel);
            float []interval = new float [InputLevel*2];

            interval[0] = min_val;
            interval[1] = min_val + delta;

            for (int ctr = 2; ctr <= (InputLevel * 2) - 2; ctr += 2)
            {
                interval[ctr] = interval[ctr - 1];
                interval[ctr + 1] = interval[ctr] + delta;
            }
            for(int ctr=0; ctr < boundary;ctr++)
            {
                int which_interval = 0;
                OutputQuantizedSignal.SamplesIndices.Add(ctr);
                for (int index =0;index<= (InputLevel * 2) - 2; index+=2)
                {
                    which_interval++;
                    if (InputSignal.Samples[ctr]>=interval[index]&& InputSignal.Samples[ctr]< interval[index + 1])
                    {
                        float midpoint = ((float)interval[index] + (float)interval[index + 1]) /(float)(2),sample_error;
                        OutputQuantizedSignal.Samples.Add(midpoint);
                        sample_error = (float)midpoint - (float)InputSignal.Samples[ctr] ;
                        OutputIntervalIndices.Add(which_interval);
                        OutputSamplesError.Add(sample_error);
                        OutputEncodedSignal.Add(encoded_levels[which_interval - 1]);
                        break;
                    }
                    else if (InputSignal.Samples[ctr] == InputSignal.Samples.Max())
                    {
                        float midpoint = ((float)interval[(InputLevel * 2)-2] + (float)(interval[(InputLevel * 2)-1])) / (float)(2), sample_error;
                        OutputQuantizedSignal.Samples.Add(midpoint);
                        sample_error = (float)midpoint - (float)InputSignal.Samples[ctr];
                        OutputIntervalIndices.Add(InputLevel);
                        OutputSamplesError.Add(sample_error);
                        OutputEncodedSignal.Add(encoded_levels[InputLevel - 1]);
                        break;
                    }
                }
            }
        }
    }
}
