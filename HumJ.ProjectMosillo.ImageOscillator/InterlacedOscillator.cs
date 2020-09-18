using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HumJ.ProjectMosillo.ImageOscillator
{
    /// <summary>
    /// 隔行扫描方式生成波形（WIP）
    /// </summary>
    public class InterlacedImageReader : IImageReader
    {
        /// <inheritdoc/>
        public Bitmap OriginalImage => throw new NotImplementedException();

        /// <inheritdoc/>
        public double Threshold => throw new NotImplementedException();

        /// <inheritdoc/>
        public uint SampleRate => throw new NotImplementedException();

        /// <inheritdoc/>
        public double Frequency => throw new NotImplementedException();

        /// <inheritdoc/>
        public TimeSpan Duration => throw new NotImplementedException();

        /// <inheritdoc/>
        public (double L, double R)[] Output => throw new NotImplementedException();

        /// <inheritdoc/>
        public void MakeWave() => throw new NotImplementedException();
    }
}