using Company.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Process
{
    /// <summary>
    /// 工业相机的图像处理模型接口，管理原始图像数据和提供一个观察者
    /// </summary>
    public interface ICameraProcessModel
    {
        /// <summary>
        /// 图像数据的观察者
        /// </summary>
        IObservable<UnmanagedArray2D<ColorBGRA>> Observable { get; }
        /// <summary>
        /// 图像数据
        /// </summary>
        UnmanagedArray2D<ColorBGRA> Photo { get; }
    }
}
