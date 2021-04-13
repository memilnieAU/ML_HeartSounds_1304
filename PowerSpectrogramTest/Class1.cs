using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spectrogram;

namespace PowerSpectrogramTest
{
    public class Class1
    {
        private string _filePath;
        private string _filePathFig;

        void test()
        {
            FileSystemAccess _fileSystem = new FileSystemAccess();


            _filePath = _fileSystem.GetCombinePath("TestH1.wav");
            _filePathFig = _fileSystem.GetCombinePath("TestH1Fig.png");

            (double[] audio, int sampleRate) = ReadWAV(_filePath);
            var sg = new SpectrogramGenerator(sampleRate, fftSize: 4096, stepSize: 500, maxFreq: 3000);
            sg.Add(audio);

            sg.SaveImage(_filePathFig);
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
    }



    public class FileSystemAccess
    {
        public string GetCombinePath(string fileName)
        {
            return Path.Combine(fileName);
        }
    }
}
