using System;
using System.Drawing;

namespace HumJ.ProjectMosillo.ImageOscillator
{
    public interface IImageReader
    {
        /// <summary>
        /// 原始图片
        /// </summary>
        Bitmap OriginalImage { get; }

        /// <summary>
        /// 采样阈值（亮度）
        /// </summary>
        double Threshold { get; }

        /// <summary>
        /// 采样频率（Hz）
        /// </summary>
        uint SampleRate { get; }

        /// <summary>
        /// 载波频率（Hz）
        /// </summary>
        double Frequency { get; }

        /// <summary>
        /// 持续时间
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// 输出信号
        /// </summary>
        (double L, double R)[] Output { get; }

        /// <summary>
        /// 生成波形
        /// </summary>
        void MakeWave();
    }
}