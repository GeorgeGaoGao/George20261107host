using Company.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Filter
{
    public interface IFilterModel
    {
        /// <summary>
        /// 算法名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 界面名称
        /// </summary>
        string View { get; }
        /// <summary>
        /// 图标
        /// </summary>
        string Icon { get; }
        /// <summary>
        /// 是否可用
        /// </summary>
        bool IsEnable { get; }
        /// <summary>
        /// 执行当前算法
        /// </summary>
        /// <param name="photo"></param>
        void Filter(UnmanagedArray2D<ushort> photo);
        /// <summary>
        /// 获取当前算法的观察者
        /// </summary>
        /// <returns></returns>
        //IObservable<Unit> GetObservable();
    }
    public class Unit { }
}
