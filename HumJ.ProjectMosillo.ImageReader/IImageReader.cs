using System;
using System.Drawing;

namespace HumJ.ProjectMosillo.ImageReader
{
    public interface IImageReader
    {
        Bitmap OriginalImage { get; set; }

        ushort Channels { get; }

        Span<double[]> Samples { get; }
    }
}