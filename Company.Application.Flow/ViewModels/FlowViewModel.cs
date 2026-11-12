using Company.Application.Flow.Views;
using Company.Core.Ioc;
using Prism.Services.Dialogs;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Company.Core.Extension;
using Prism.Commands;
using Company.Application.Share.Filter;
using Company.Core.Helper;
using Company.Application.Flow.Models;
using Prism.Ioc;

namespace Company.Application.Flow.ViewModels
{
    public class FlowViewModel:ReactiveObject
    {
        public FlowModel FlowModel { get; }
        public ICommand AddFilterCommand { get;  }

        public FlowViewModel(IContainerProvider containerProvider)
        {
            AddFilterCommand = new DelegateCommand(AddFilter);
            FlowModel=containerProvider.Resolve<IFlowModel>() as FlowModel;
        }

        private void AddFilter()
        {
            PrismProvider.DialogService.ShowDialog<AddFilterView>(para => {
                if (para.Result==ButtonResult.OK)
                {
                    //todo 得到了算法模型
                    var model = para.Parameters.GetValue<IFilterModel>("DisplayModel");
                    Assert.NotNull(model);
                    FlowModel.AddFilter(model);

                }
            
            });
            //todo 添加算法模型 弹出对话框
        }
    }
}
