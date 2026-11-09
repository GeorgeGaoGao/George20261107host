using Company.Application.Share.Main;
using Company.Core.Enums;
using Company.Core.Ioc;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Main.Models
{
    /// <summary>
    /// 继承了一个带观察者的接口，别的模块就可以从IOC中以接口的方法，得到当前类型的实例
    /// </summary>
    [ExposedService(Lifetime.Singleton,typeof(IMouseWorkModeProvider))]
    public class MainModel:ReactiveObject,IMouseWorkModeProvider
    {
        /// <summary>
        /// 鼠标工作模式
        /// </summary>
        [Reactive]
        public MouseWorkMode MouseWorkMode { get; set; } = MouseWorkMode.默认操作;
        /// <summary>
        /// 鼠标工作模式的观察者
        /// </summary>
        public IObservable<MouseWorkMode> MouseWorkObservable { get; }
        public MainModel()
        {
            MouseWorkObservable=this.WhenAnyValue(x=>x.MouseWorkMode);
        }
    }
}
