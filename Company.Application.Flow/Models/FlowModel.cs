using Company.Application.Share.Filter;
using Company.Core.Ioc;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Flow.Models
{
    [ExposedService(Lifetime.Singleton,typeof(IFlowModel))]
    public class FlowModel : ReactiveObject, IFlowModel
    {
        /// <summary>
        /// 当前选中的算法实体
        /// </summary>
        [Reactive]
        public IFilterModel Filter { get; set; }
        /// <summary>
        /// 当前选中的所有的算法实体集合
        /// </summary>
        public ObservableCollection<IFilterModel> Filters { get; set; } = new ObservableCollection<IFilterModel>();
        /// <summary>
        /// 添加一个算法实体
        /// </summary>
        /// <param name="filterModel"></param>
        public void AddFilter(IFilterModel filter)
        {
            Filters.Add(filter);
        }
    }
}
