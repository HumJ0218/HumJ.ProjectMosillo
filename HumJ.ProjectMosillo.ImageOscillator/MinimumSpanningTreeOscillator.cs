using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace HumJ.ProjectMosillo.ImageOscillator
{
    /// <summary>
    /// 最小生成树方式生成波形（WIP）
    /// </summary>
    public class MinimumSpanningTreeOscillator : IImageReader
    {
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
            var pixels = new Dictionary<int, Complex>();
            for (var y = 0; y < OriginalImage.Height; y++)
            {
                for (var x = 0; x < OriginalImage.Width; x++)
                {
                    if (OriginalImage.GetPixel(x, y).GetBrightness() > Threshold)
                    {
                        var l = (double)x / OriginalImage.Width * 2 - 1;
                        var r = (double)y / OriginalImage.Height * 2 - 1;

                        pixels.Add(y * OriginalImage.Width + x, new Complex(l, r));
                    }
                }
            }

            var distance = new Dictionary<int, Dictionary<int, double>>();
            foreach (var p0 in pixels)
            {
                var dp0 = new Dictionary<int, double>();
                foreach (var p1 in pixels.Where(m => m.Key > p0.Key))
                {
                    var d = (p1.Value - p0.Value).Magnitude;
                    dp0.Add(p1.Key, d);
                }
                distance.Add(p0.Key, dp0);
            }
        }
    }
}