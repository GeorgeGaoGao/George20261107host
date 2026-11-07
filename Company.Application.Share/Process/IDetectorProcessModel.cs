using Company.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Process
{
    /// <summary>
    /// 16位平板探测器的图像数据接口，管理原始数据并提供一个观察者。
    /// </summary>
    public interface IDetectorProcessModel
    {
        UnmanagedArray2D<ushort> SourcePhoto { get; }

        /// <summary>
        /// 源图像数据的观察者
        /// </summary>
        IObservable<UnmanagedArray2D<ushort>> SourceObservable { get; }
        
        
        UnmanagedArray2D<ushort> TargetPhoto { get; }

        /// <summary>
        /// 目标图像数据的观察者
        /// </summary>
        IObservable<UnmanagedArray2D<ushort>> TargetObservable { get; }
        
    }
}
