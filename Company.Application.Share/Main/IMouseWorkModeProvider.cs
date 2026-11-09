using Company.Core.Enums;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Main
{
    /// <summary>
    /// 鼠标工作模式的提供者
    /// </summary>
    public interface IMouseWorkModeProvider
    {
        /// <summary>
        /// 鼠标工作模式
        /// </summary>
        MouseWorkMode MouseWorkMode { get; set; } 
        /// <summary>
        /// 鼠标工作模式的观察者
        /// </summary>
        IObservable<MouseWorkMode> MouseWorkObservable { get; }
    }
}
