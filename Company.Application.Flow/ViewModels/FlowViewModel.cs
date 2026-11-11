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

namespace Company.Application.Flow.ViewModels
{
    public class FlowViewModel:ReactiveObject
    {
        public ICommand AddFilterCommand { get;  }

        public FlowViewModel()
        {
            AddFilterCommand = new DelegateCommand(AddFilter);
        }

        private void AddFilter()
        {
            PrismProvider.DialogService.ShowDialog<AddFilterView>(parameters => {
                if (parameters.Result==ButtonResult.OK)
                {
                    //todo 得到了算法模型
                }
            
            });
            //todo 添加算法模型 弹出对话框
        }
    }
}
