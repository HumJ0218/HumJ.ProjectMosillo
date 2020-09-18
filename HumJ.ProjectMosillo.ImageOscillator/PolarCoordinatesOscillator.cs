using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace HumJ.ProjectMosillo.ImageOscillator
{
    /// <summary>
    /// 极坐标扫描方式生成波形
    /// </summary>
    public class PolarCoordinatesImageReader : IImageReader
    {
        /// <summary>
        /// 起始相位角
        /// </summary>
        public double StartPhase { get; set; } = 0;

        /// <summary>
        /// 终止相位角（不包含在波形内）
        /// </summary>
        public double EndPhase { get; private set; }

        /// <inheritdoc/>
        public Bitmap OriginalImage { get; set; }

        /// <inheritdoc/>
        public double Threshold { get; set; } = 0.5;

        /// <inheritdoc/>
        public uint SampleRate { get; set; } = 44100;

        /// <inheritdoc/>
        public double Frequency { get; set; } = 440;

        /// <inheritdoc/>
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(10);

        /// <inheritdoc/>
        public (double L, double R)[] Output { get; private set; }

        /// <inheritdoc/>
        public void MakeWave()
        {
            var pixels = new List<Complex>();
            for (var y = 0; y < OriginalImage.Height; y++)
            {
                for (var x = 0; x < OriginalImage.Width; x++)
                {
                    if (OriginalImage.GetPixel(x, y).GetBrightness() > Threshold)
                    {
                        var l = (double)x / OriginalImage.Width * 2 - 1;
                        var r = (double)y / OriginalImage.Height * 2 - 1;

                        pixels.Add(new Complex(l, r));
                    }
                }
            }

            pixels = pixels.OrderBy(m => m.Magnitude).OrderBy(m => m.Phase).ToList();

            var samples = new List<(double L, double R)>();
            for (var i = 0; i < 1000; i++) {
                samples.AddRange(pixels.Select(m => (-m.Real, m.Imaginary)));
            }

            Output = samples.ToArray();
            Duration = TimeSpan.FromSeconds((double)Output.Length / SampleRate);
        }
    }
}