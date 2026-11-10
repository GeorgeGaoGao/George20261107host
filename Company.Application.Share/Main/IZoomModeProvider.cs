using Company.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Main
{
    /// <summary>
    /// 缩放模式提供者
    /// </summary>
    public interface IZoomModeProvider
    {
        /// <summary>
        /// 缩放模式
        /// </summary>
        ZoomMode ZoomMode { get; set; }
        /// <summary>
        /// 缩放模式观察者
        /// </summary>
        IObservable<ZoomMode> ZoomModeObservable { get; }
    }
}
