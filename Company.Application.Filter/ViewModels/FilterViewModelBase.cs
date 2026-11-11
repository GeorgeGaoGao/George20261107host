using Company.Application.Share.Filter;
using Prism.Regions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Company.Application.Filter.ViewModels
{
    public abstract class FilterViewModelBase<TFilterModel> : ReactiveObject, INavigationAware where TFilterModel:IFilterModel,new()
    {
        [Reactive]
        public TFilterModel FilterModel { get;private set; }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            FilterModel = navigationContext.Parameters.GetValue<TFilterModel>(typeof(TFilterModel).FullName);
        }
    }
}
