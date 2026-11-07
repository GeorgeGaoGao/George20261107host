using Company.Application.Share.Process;
using Company.Core.Ioc;
using Company.Core.Model;
using Company.Hardware.Camera;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Process.Models
{
    [ExposedService(Lifetime.Singleton,typeof(ICameraProcessModel))]
    public class CameraProcessModel :ReactiveObject, ICameraProcessModel
    {
        /// <summary>
        /// 双缓冲内存区域
        /// </summary>
        private DoubleBuffer<ColorBGRA> Buffer { get; set; }
        /// <summary>
        /// 原始图像
        /// </summary>
        public UnmanagedArray2D<ColorBGRA> Photo => Buffer.Current;
        public IObservable<UnmanagedArray2D<ColorBGRA>> Observable { get; }
        public CameraProcessModel()
        {
            Observable = this.WhenAnyValue(p => p.Buffer.Current);   
        }
        /// <summary>
        /// 将图像写入双缓冲区内存
        /// </summary>
        /// <param name="photo"></param>
        public void Write(Photo photo)
        {
            Buffer.Write(photo.Header, photo.Width, photo.Height);
        }

    }
}
