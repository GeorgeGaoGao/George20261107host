using Company.Application.Filter.Views;
using Company.Application.Share.Prism;
using Company.Core.Extension;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Filter
{
    [Module(ModuleName = ModuleNames.ApplicationFilterModule,OnDemand =true)]
    public class ApplicationFilterModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.InitializeAssembly(Assembly.GetExecutingAssembly());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterAssembly(Assembly.GetExecutingAssembly());
            containerRegistry.RegisterForNavigation<TestFilterView>();
        }
    }
}
