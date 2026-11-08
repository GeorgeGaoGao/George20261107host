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
    [ExposedService(Lifetime.Singleton)]
    public class MainModel:ReactiveObject
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
