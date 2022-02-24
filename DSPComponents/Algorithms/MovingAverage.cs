using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            OutputAverageSignal = new Signal(new List<float>(), InputSignal.Periodic);
            int count = InputSignal.Samples.Count();
            for (int index = 0; index < count; index++)
            {
                float average = 0;
                average += InputSignal.Samples[index];
                int looping_index = index-1, inner_loop_ctr;
                bool outOfRange = false;
                if (looping_index < 0)
                {
                    continue;
                }
                for (inner_loop_ctr = 0; inner_loop_ctr < InputWindowSize / 2; inner_loop_ctr++)
                {
                    if (looping_index < 0)
                    {
                        outOfRange = true;
                        break;
                    }
                    average += InputSignal.Samples[looping_index];
                    looping_index--;
                }
                looping_index = index + 1;
                for (inner_loop_ctr = 0; inner_loop_ctr < InputWindowSize / 2; inner_loop_ctr++)
                {
                    if (looping_index > count - 1)
                    {
                        outOfRange = true;
                        break;
                    }
                    average += InputSignal.Samples[looping_index];
                    looping_index++;
                }
                if (outOfRange)
                {
                    continue;
                }
                var answer = average / InputWindowSize;
                string str = answer.ToString("0.#######");
                float last = float.Parse(str);
                OutputAverageSignal.Samples.Add(last);
                
            }

        }
    }
}
