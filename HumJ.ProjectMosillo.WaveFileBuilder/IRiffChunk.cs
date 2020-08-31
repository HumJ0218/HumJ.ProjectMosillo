namespace HumJ.ProjectMosillo.WaveFileBuilder
{
    public interface IRiffChunk
    {
        byte[] ChunkID { get; }
        byte[] ChunkSize { get; }
        byte[] Data { get; }
    }
}