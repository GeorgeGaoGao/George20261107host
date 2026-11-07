using Company.Application.Share.Process;
using Company.Core.Ioc;
using Company.Core.Model;
using Company.Hardware.Detector;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Process.Models
{
    /// <summary>
    /// 16位FPD图像处理模型
    /// </summary>
     
    [ExposedService(Lifetime.Singleton,typeof(IDetectorProcessModel))]
    public class DetectorProcessModel : ReactiveObject, IDetectorProcessModel
    {
        /// <summary>
        /// 原图 双缓冲内存区域
        /// </summary>
        private DoubleBuffer<ushort> SourceBuffer { get; set; } = new DoubleBuffer<ushort>();
        /// <summary>
        /// 目标 双缓冲内存区域
        /// </summary>
        private DoubleBuffer<ushort> TargetBuffer { get; set; } = new DoubleBuffer<ushort>();




        public UnmanagedArray2D<ushort> SourcePhoto => SourceBuffer.Current;

        public IObservable<UnmanagedArray2D<ushort>> SourceObservable { get; }

        public UnmanagedArray2D<ushort> TargetPhoto => TargetBuffer.Current;

        public IObservable<UnmanagedArray2D<ushort>> TargetObservable {  get; }

        public DetectorProcessModel()
        {
            SourceObservable=this.WhenAnyValue(p=>p.SourceBuffer.Current);
            TargetObservable = this.WhenAnyValue(p => p.TargetBuffer.Current);


        }
        /// <summary>
        /// 将图像写入目标双缓冲内存
        /// </summary>
        /// <param name="photo"></param>
        public void Write(UnmanagedArray2D<ushort> photo)
        {
            TargetBuffer.Write(photo);
        }
        /// <summary>
        /// 将原图写入源双缓冲内存
        /// </summary>
        /// <param name="image"></param>
        public void Write(DetectorImage image)
        {
            SourceBuffer.Write(image.Header, image.Width, image.Height);
        }
    }
}
