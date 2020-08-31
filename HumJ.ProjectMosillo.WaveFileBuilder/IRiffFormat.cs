using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HumJ.ProjectMosillo.WaveFileBuilder
{
    public interface IRiffFormat
    {
        IRiffChunk[] Chunks { get; }
    }
}