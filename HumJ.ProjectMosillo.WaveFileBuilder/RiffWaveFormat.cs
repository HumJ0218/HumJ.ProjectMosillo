﻿using System;
using System.Linq;

namespace HumJ.ProjectMosillo.WaveFileBuilder
{
    public class RiffWaveFormat : IRiffFormat
    {
        public IRiffChunk[] Chunks { get; private set; }
        public RiffDescriptorChunk RiffChunk { get; private set; }
        public RiffWaveFormatSubchunk FormatSubchunk { get; private set; }
        public RiffWaveDataSubchunk DataSubchunk { get; private set; }
        public uint FileLength => RiffChunk.FileLength;

        public RiffWaveFormat(ushort audioFormat = 1, ushort numChannels = 2, uint sampleRate = 44100, ushort bitsPerSample = 16, uint numSamples = 0)
        {
            var dataChunkSize = numSamples * numChannels * bitsPerSample / 8;

            RiffChunk = new RiffDescriptorChunk(RiffFormat.WAVE);
            FormatSubchunk = new RiffWaveFormatSubchunk(audioFormat, numChannels, sampleRate, bitsPerSample);
            DataSubchunk = new RiffWaveDataSubchunk(dataChunkSize);

            Chunks = new IRiffChunk[]
            {
                RiffChunk,
                FormatSubchunk,
                DataSubchunk,
            };

            var fileLength = (uint)Chunks.Sum(m =>
            {
                return m.ChunkID.Length + m.ChunkSize.Length + m.Data.Length;
            });
            RiffChunk.FileLength = fileLength;
        }

        public Span<byte> GetSampleBytes(ushort numChannel)
        {
            var channelLength = DataSubchunk.Data.Length / FormatSubchunk.NumChannels;
            var offset = channelLength * (FormatSubchunk.NumChannels - numChannel - 1);
            return DataSubchunk.Data.AsSpan(offset, channelLength);
        }
        public Span<byte> GetSampleBytes(ushort numChannel, uint sampleIndex)
        {
            var bytesPerSample = FormatSubchunk.BitsPerSample / 8;
            var bytes = GetSampleBytes(numChannel);
            var offset = (int)(sampleIndex * bytesPerSample);
            return bytes.Slice(offset, bytesPerSample);
        }

        public void SaveAsFile(string path, bool overwrite = false)
        {
            if (System.IO.File.Exists(path))
            {
                if (overwrite)
                {
                    System.IO.File.Delete(path);
                }
                else
                {
                    throw new Exception("文件已存在");
                }
            }

            using var stream = System.IO.File.OpenWrite(path);
            foreach (var c in Chunks)
            {
                stream.Write(c.ChunkID);
                stream.Write(c.ChunkSize);
                stream.Write(c.Data);
            }
            stream.Close();
        }
    }
}