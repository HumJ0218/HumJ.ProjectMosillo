using System;
using System.Linq;

namespace HumJ.ProjectMosillo.WaveFileBuilder
{
    public class RiffDescriptorChunk : IRiffChunk
    {
        public RiffDescriptorChunk(RiffFormat format = RiffFormat.WAVE, uint fileLength = 36)
        {
            ChunkID = BitConverter.GetBytes((uint)RiffChunkId.RIFF).Reverse().ToArray();
            ChunkSize = BitConverter.GetBytes(fileLength);
            Data = BitConverter.GetBytes((uint)format).Reverse().ToArray();
        }

        public byte[] ChunkID { get; private set; }

        public byte[] ChunkSize { get; private set; }

        public byte[] Data { get; private set; }

        public uint FileLength
        {
            get => BitConverter.ToUInt32(ChunkSize);
            internal set => ChunkSize = BitConverter.GetBytes(value);
        }
    }
}