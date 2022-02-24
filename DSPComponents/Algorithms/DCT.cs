using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), new List<int>(),InputSignal.Periodic);
            int numberOfSamples,ctr,index;
            numberOfSamples = InputSignal.Samples.Count();
            double sum,factor1,factor2;
            string coeffiecent = "Index"+'\t' + "Value\n";
            factor1 = (double)1 / numberOfSamples;
            factor2 = (double)2 / numberOfSamples;
            for (ctr = 0;ctr < numberOfSamples; ctr++)
            {
                sum = 0;
                //this loop is for n
                for (index = 0;index < numberOfSamples; index++)
                {
                    sum += InputSignal.Samples[index] * cosineEquation(index,ctr,numberOfSamples);
                }
                OutputSignal.SamplesIndices.Add(ctr);
                if (ctr != 0)
                {
                    sum = sum * Math.Sqrt(factor2);
                }
                else
                {
                    sum = sum * Math.Sqrt(factor1);
                }
                OutputSignal.Samples.Add((float)(sum));
                coeffiecent += OutputSignal.SamplesIndices[ctr].ToString() + '\t' + OutputSignal.Samples[ctr].ToString() + '\n';
            }
            string filePath = "DCT_components.txt";
            FileStream DCT_file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter file = new StreamWriter(DCT_file);
            file.BaseStream.Seek(0, SeekOrigin.End);
            file.WriteLine(coeffiecent);
            file.Close();
        }
        private double cosineEquation(int n,int k,int N)
        {
            double nominator, denominator;
            nominator = ((2*n)+1) * k *Math.PI;
            denominator = 2 * N;
            double result = Math.Cos(nominator/denominator);
            return result;
        }
        
    }
}
