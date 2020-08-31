using HumJ.ProjectMosillo.WaveFileBuilder;
using NUnit.Framework;
using System;

namespace NUnitTestProject
{
    public class RiffWaveFormatTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            var rwf = new RiffWaveFormat(1, 2, 44100, 16, 441000);
            for (uint i = 0; i < 441000; i++)
            {
                var sampleL = rwf.GetSampleBytes(0, i);
                var sampleR = rwf.GetSampleBytes(1, i);

                BitConverter.GetBytes((short)(short.MinValue + i)).CopyTo(sampleL);
                BitConverter.GetBytes((short)(Math.Sin(i * Math.PI / 441) * short.MaxValue)).CopyTo(sampleR);
            }

            rwf.SaveAsFile(@"d:\test.wav",true);
            Assert.Pass();
        }
    }
}