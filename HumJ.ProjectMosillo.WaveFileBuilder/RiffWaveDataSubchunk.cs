using System;
using System.Linq;

namespace HumJ.ProjectMosillo.WaveFileBuilder
{
    public class RiffWaveDataSubchunk : IRiffChunk
    {
        public RiffWaveDataSubchunk(uint size)
        {
            ChunkID = BitConverter.GetBytes((uint)RiffChunkId.data).Reverse().ToArray();
            ChunkSize = BitConverter.GetBytes(size);
            Data = new byte[size];
        }

        public byte[] ChunkID { get; private set; }
        public byte[] ChunkSize { get; private set; }
        public byte[] Data { get; private set; }
    }
}