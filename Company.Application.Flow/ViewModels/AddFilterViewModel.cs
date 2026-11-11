using Company.Application.Share.Filter;
using Company.Core.Ioc;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Company.Application.Flow.ViewModels
{
    public class AddFilterViewModel:DialogViewModelBase
    {
        public List<IFilterModel> DisplayModels { get; } = new List<IFilterModel>();
        public IFilterModel DisplayModel { get; set; }

        public ICommand SubmitCommand { get;  }
        public AddFilterViewModel(IFilterFactory filterFactory)
        {
            Title = "请选择滤波算法";
            DisplayModels = filterFactory.CreateFilterModels();//todo 后期将系统中所有算法用反射实例化。
            SubmitCommand = new DelegateCommand(Submit);
        }

        private void Submit()
        {
            throw new NotImplementedException();
        }
    }
}
