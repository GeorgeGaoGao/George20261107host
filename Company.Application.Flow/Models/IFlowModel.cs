using Company.Application.Share.Filter;
using System.Collections.ObjectModel;

namespace Company.Application.Flow.Models
{
    public interface IFlowModel
    {
        ObservableCollection<IFilterModel> Filters { get; set; }

        void AddFilter(IFilterModel filterModel);
    }
}