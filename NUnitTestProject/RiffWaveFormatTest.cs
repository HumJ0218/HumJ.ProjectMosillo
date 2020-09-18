using HumJ.ProjectMosillo.WaveFileBuilder;
using NUnit.Framework;
using System;
using HumJ.ProjectMosillo.ImageOscillator;
using System.Drawing;

namespace NUnitTestProject
{
    public class RiffWaveFormatTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            uint sampleRate = 44100;
            uint length = 10;

            static (double L, double R) wave(double time)
            {
                return (0, 0);
            }

            var rwf = new RiffWaveFormat(1, 2, sampleRate, 16, sampleRate* length);
            for (uint i = 0; i < sampleRate * length; i++)
            {
                var sampleL = rwf.GetFrameBytes(i, 0);
                var sampleR = rwf.GetFrameBytes(i, 1);

                var (L, R) = wave((double)i / sampleRate);

                BitConverter.GetBytes((short)(L * short.MaxValue)).CopyTo(sampleL);
                BitConverter.GetBytes((short)(R * short.MaxValue)).CopyTo(sampleR);
            }

            rwf.SaveAsFile(@"./test.wav", true);
            Assert.Pass();
        }

        [Test]
        public void Test2() {
            var pco = new PolarCoordinatesImageReader();
            pco.OriginalImage = new Bitmap(@"./test.png");
            pco.MakeWave();

            var rwf = new RiffWaveFormat(1, 2, pco.SampleRate, 16, (uint)(pco.SampleRate * pco.Duration.TotalSeconds));
            for (uint i = 0; i < (uint)(pco.SampleRate * pco.Duration.TotalSeconds); i++)
            {
                var sampleL = rwf.GetFrameBytes(i, 0);
                var sampleR = rwf.GetFrameBytes(i, 1);

                var (L, R) = pco.Output[i];

                BitConverter.GetBytes((short)(L * short.MaxValue)).CopyTo(sampleL);
                BitConverter.GetBytes((short)(R * short.MaxValue)).CopyTo(sampleR);
            }

            rwf.SaveAsFile(@"./test.wav", true);
            Assert.Pass();
        }
    }
}