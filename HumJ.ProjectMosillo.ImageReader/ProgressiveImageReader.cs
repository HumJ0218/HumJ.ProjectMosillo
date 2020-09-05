using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HumJ.ProjectMosillo.ImageReader
{
    public class ProgressiveImageReader : IImageReader
    {
        public Bitmap OriginalImage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ushort Channels => throw new NotImplementedException();

        public Span<double[]> Samples => throw new NotImplementedException();
    }
}
