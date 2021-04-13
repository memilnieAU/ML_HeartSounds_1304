using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using FirstTryAtML101ML.Model;
using TestPowerSpectrogramCore;

namespace TestPowerSpectrogram
{
    class Program
    {
        private static FileSystemAccess _fileSystem;
        private static string _filePath;

        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            List<double[]> linesA = new List<double[]>();
            _fileSystem = new FileSystemAccess();
            _filePath = _fileSystem.GetCombinePath("AllLines.txt");

            Console.WriteLine("Hello World!");
            TestPowerSpectrogramCore.Class1 testClass;
            testClass = new TestPowerSpectrogramCore.Class1();
            //public static string[] GetFiles (string path, string searchPattern, System.IO.SearchOption searchOption);
            int tæller = 0;
            string[] fileEntries = Directory.GetFiles(_fileSystem.GetCombinePath("Abnormal10Seks"), "*.wav");
            string[] fileEntries2 = Directory.GetFiles(_fileSystem.GetCombinePath("Normal10Seks"), "*.wav");
            string[] fileEntriesA = Directory.GetFiles(_fileSystem.GetCombinePath("\\NewTestData"), "*.wav");

            Stopwatch sw = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();
            sw.Start();
            bool fast = false;
            if (fast)
            {
                for (int i = 0; i < 10; i++)
                {
                    tæller++;
                    Console.WriteLine(tæller);
                    lines.Add(testClass.test2000(fileEntries[i]));
                    tæller++;
                    Console.WriteLine(tæller);
                    lines.Add(testClass.test2000(fileEntries2[i]));
                }
            }
            else
            {
                Console.WriteLine("AbNormal");
                foreach (string entry in fileEntries)
                {
                    tæller++;
                    sw2.Start();
                    lines.Add(testClass.test2000(entry));
                    Console.WriteLine("AbNormal Tæller: " + tæller + "\tTime: " + sw2.ElapsedMilliseconds +
                                      "ms\tTotal Time:" + ((double) sw.ElapsedMilliseconds / 1000.0) + "s");
                    sw2.Reset();
                }

                tæller = 0;
                Console.WriteLine("Normal");
                foreach (string entry in fileEntries2)
                {
                    tæller++;
                    sw2.Start();
                    lines.Add(testClass.test2000(entry));
                    Console.WriteLine("AbNormal Tæller: " + tæller + "\tTime: " + sw2.ElapsedMilliseconds +
                                      "ms\tTotal Time:" + ((double) sw.ElapsedMilliseconds / 1000.0) + "s");
                    sw2.Reset();
                }
            }
            //lines.Add(testClass.test("AbNormal\\AbNormal_002_10s"));
            //lines.Add(testClass.test("AbNormal\\AbNormal_003_10s"));
            //lines.Add(testClass.test("AbNormal\\AbNormal_004_10s"));
            //lines.Add(testClass.test("AbNormal\\AbNormal_005_10s"));

            //lines.Add(testClass.test("Normal\\Normal_007_10s"));
            //lines.Add(testClass.test("Normal\\Normal_011_10s"));
            //lines.Add(testClass.test("Normal\\Normal_012_10s"));
            //lines.Add(testClass.test("Normal\\Normal_016_10s"));
            //lines.Add(testClass.test("Normal\\Normal_019_10s"));
            File.WriteAllLines(_filePath, lines);
            //using (TextWriter tw = new StreamWriter(_filePath))
            //{
            //    foreach (String s in lines)
            //        tw.WriteLine(s + "\n");
            //}
            foreach (var VARIABLE in fileEntriesA)
            {
               double[] thenewdata = (testClass.test2000A(VARIABLE));
           
                ModelInput sampleData = new ModelInput()
                {
                    Col1 = (float) thenewdata[0],
                    Col2 = (float) thenewdata[1],
                    Col3 = (float) thenewdata[2],
                    Col4 = (float) thenewdata[3],
                    Col5 = (float) thenewdata[4],
                    Col6 = (float) thenewdata[5],
                    Col7 = (float) thenewdata[6],
                    Col8 = (float) thenewdata[7],
                    Col9 = (float) thenewdata[8],
                    Col10 = (float) thenewdata[9],
                    Col11 = (float) thenewdata[10],
                    Col12 = (float) thenewdata[11],
                    Col13 = (float) thenewdata[12],
                    Col14 = (float) thenewdata[13],
                    Col15 = (float) thenewdata[14],
                    Col16 = (float) thenewdata[15],
                    Col17 = (float) 0,
                };

                var predictionResult = ConsumeModel.Predict(sampleData);
                // Create single instance of sample data from first line of dataset for model input

                // Make a single prediction on the sample data and print results
                Console.WriteLine(VARIABLE);
                Console.WriteLine(
                    "Using model to make single prediction -- Comparing actual Col0 with predicted Col0 from sample data...\n\n");
                Console.WriteLine($"Col1: {sampleData.Col1}");
                Console.WriteLine($"Col2: {sampleData.Col2}");
                Console.WriteLine($"Col3: {sampleData.Col3}");
                Console.WriteLine($"Col4: {sampleData.Col4}");
                Console.WriteLine($"Col5: {sampleData.Col5}");
                Console.WriteLine($"Col6: {sampleData.Col6}");
                Console.WriteLine($"Col7: {sampleData.Col7}");
                Console.WriteLine($"Col8: {sampleData.Col8}");
                Console.WriteLine($"Col9: {sampleData.Col9}");
                Console.WriteLine($"Col10: {sampleData.Col10}");
                Console.WriteLine($"Col11: {sampleData.Col11}");
                Console.WriteLine($"Col12: {sampleData.Col12}");
                Console.WriteLine($"Col13: {sampleData.Col13}");
                Console.WriteLine($"Col14: {sampleData.Col14}");
                Console.WriteLine($"Col15: {sampleData.Col15}");
                Console.WriteLine($"Col16: {sampleData.Col16}");
                Console.WriteLine($"Col17: {sampleData.Col17}");
                Console.WriteLine(
                    $"\n\nPredicted Col0 value {predictionResult.Prediction} \nPredicted Col0 scores: [{String.Join(",", predictionResult.Score)}]\n\n");
                Console.WriteLine("=============== End of process, hit any key to finish ===============");
                Console.ReadKey();
            }
        }
    }
}
