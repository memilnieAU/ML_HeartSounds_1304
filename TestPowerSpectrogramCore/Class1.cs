using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spectrogram;

namespace TestPowerSpectrogramCore
{
    public class Class1
    {
        private string _filePathwav;
        private string _filePathFig;
        private string _filePathtxt;
        string filename;

        public string test2000(string filenavn)
        {
            filename = filenavn;
            // FileSystemAccess _fileSystem = new FileSystemAccess();
            //_filePathFig = filename.Substring(filename.Length - 4,filename.Length-1);
            int længde = filename.Length - 4;
            _filePathFig =  filename.Substring(0,længde)+".png";
            _filePathtxt =  filename.Substring(0,længde)+".txt";
            _filePathwav =  filename.Substring(0,længde)+".wav";
            
            (double[] audio, int sampleRate) = ReadWAV(_filePathwav);
            // int fftSize1 = 16384;
            int targetWidthPx = 300;
            int fftSize1 = (int)MathF.Pow(2, 5);
            int stepSize1 = 80;
            //int stepSize1 = audio.Length / targetWidthPx;
            var sg = new SpectrogramGenerator(sampleRate, fftSize: fftSize1, stepSize: stepSize1, maxFreq: 1000);
            sg.Add(audio);
            SaveSum(TakeSum(sg.GetFFTs()));
            sg.SaveImage(_filePathFig, intensity: 8, dB: true, roll: true);
            line = line.Replace(',', '.');
            return line;
        }
        public double[] test2000A(string filenavn)
        {
            filename = filenavn;
            // FileSystemAccess _fileSystem = new FileSystemAccess();
            //_filePathFig = filename.Substring(filename.Length - 4,filename.Length-1);
            int længde = filename.Length - 4;
            _filePathFig =  filename.Substring(0,længde)+".png";
            _filePathtxt =  filename.Substring(0,længde)+".txt";
            _filePathwav =  filename.Substring(0,længde)+".wav";
            
            (double[] audio, int sampleRate) = ReadWAV(_filePathwav);
            // int fftSize1 = 16384;
            int targetWidthPx = 300;
            int fftSize1 = (int)MathF.Pow(2, 5);
            int stepSize1 = 80;
            //int stepSize1 = audio.Length / targetWidthPx;
            var sg = new SpectrogramGenerator(sampleRate, fftSize: fftSize1, stepSize: stepSize1, maxFreq: 1000);
            sg.Add(audio);
            double[] testData = TakeSum(sg.GetFFTs());
            sg.SaveImage(_filePathFig, intensity: 8, dB: true, roll: true);
            line = line.Replace(',', '.');
            return testData;
        }
        public string test44100(string filenavn)
        {
            filename = filenavn;
           // FileSystemAccess _fileSystem = new FileSystemAccess();
            //_filePathFig = filename.Substring(filename.Length - 4,filename.Length-1);
            int længde = filename.Length - 4;
            _filePathFig =  filename.Substring(0,længde)+".png";
            _filePathtxt =  filename.Substring(0,længde)+".txt";
            _filePathwav =  filename.Substring(0,længde)+".wav";
            
            (double[] audio, int sampleRate) = ReadWAV(_filePathwav);
            // int fftSize1 = 16384;
            int targetWidthPx = 300;
            //int stepSize1 = audio.Length / targetWidthPx;
            var sg = new SpectrogramGenerator(sampleRate, fftSize: /*fftSize1*/16384, stepSize: 441, maxFreq: 1000);
            sg.Add(audio);
           
            SaveSum(TakeSum(sg.GetFFTs()));
            sg.SaveImage(_filePathFig, intensity: 8, dB: true, roll: true);
            line = line.Replace(',', '.');
            return line;
        }

        (double[] audio, int sampleRate) ReadWAV(string filePath, double multiplier = 16_000)
        {
            using var afr = new NAudio.Wave.AudioFileReader(filePath);
            int sampleRate = afr.WaveFormat.SampleRate;
            int sampleCount = (int)(afr.Length / afr.WaveFormat.BitsPerSample / 8);
            int channelCount = afr.WaveFormat.Channels;
            var audio = new List<double>(sampleCount);
            var buffer = new float[sampleRate * channelCount];
            int samplesRead = 0;
            while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
                audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));
            return (audio.ToArray(), sampleRate);
        }

        double[] TakeSum(List<double[]> input)
        {
            double[] EndeligtArray = new double[input[0].Length];
            for (int i = 0; i < EndeligtArray.Length - 1; i++)
            {
                foreach (double[] doubles in input)
                {
                    EndeligtArray[i] += doubles[i];
                }
            }

            return EndeligtArray;
        }

        string line = "";
        void SaveSum(double[] input)
        {
            string folder= $"C:\\Users\\memil\\source\\repos\\FirstTryAtML101\\TestPowerSpectrogram\\";

            if (filename[folder.Length] == 'A')
            {
                line = "1\t";
            }
            else line = "0\t";

            foreach (double d in input)
            {
                line += d + "\t";
            }

            //File.WriteAllLines(
            //    _filePathtxt // <<== Put the file name here
            //    ,   input.Select(d => d.ToString())
            //);
            File.WriteAllText(_filePathtxt, line);
        }
    }

    public class FileSystemAccess
    {
        public string GetCombinePath(string fileName)
        {
            string folder = "";
            folder= $"C:\\Users\\memil\\source\\repos\\FirstTryAtML101\\TestPowerSpectrogram\\";
            return Path.Combine(folder+ fileName);
        }
    }
}
