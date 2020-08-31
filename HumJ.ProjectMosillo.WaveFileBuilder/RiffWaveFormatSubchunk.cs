using System;
using System.Linq;

namespace HumJ.ProjectMosillo.WaveFileBuilder
{
    public class RiffWaveFormatSubchunk : IRiffChunk
    {
        public RiffWaveFormatSubchunk(ushort audioFormat = 1, ushort numChannels = 2, uint sampleRate = 44100, ushort bitsPerSample = 16)
        {
            AudioFormat = audioFormat;
            NumChannels = numChannels;
            SampleRate = sampleRate;
            ByteRate = sampleRate * numChannels * bitsPerSample / 8;
            BlockAlign = (ushort)(numChannels * bitsPerSample / 8);
            BitsPerSample = bitsPerSample;

            ChunkID = BitConverter.GetBytes((uint)RiffChunkId.fmt).Reverse().ToArray();
            ChunkSize = BitConverter.GetBytes((uint)16);
            Data = new byte[0]
                .Concat(BitConverter.GetBytes(AudioFormat))
                .Concat(BitConverter.GetBytes(NumChannels))
                .Concat(BitConverter.GetBytes(SampleRate))
                .Concat(BitConverter.GetBytes(ByteRate))
                .Concat(BitConverter.GetBytes(BlockAlign))
                .Concat(BitConverter.GetBytes(BitsPerSample))
                .ToArray();
        }

        public byte[] ChunkID { get; private set; }
        public byte[] ChunkSize { get; private set; }
        public byte[] Data { get; private set; }

        public ushort AudioFormat { get; private set; }
        public ushort NumChannels { get; private set; }
        public uint SampleRate { get; private set; }
        public uint ByteRate { get; private set; }
        public ushort BlockAlign { get; private set; }
        public ushort BitsPerSample { get; private set; }
    }
}