using Company.Application.Flow.Views;
using Company.Application.Process.ViewModels;
using Company.Application.Process.Views;
using Company.Application.Share.Prism;
using Company.Core.Extension;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Process
{
    [Module(ModuleName = ModuleNames.ApplicationProcessModule, OnDemand = true)]
    [ModuleDependency(ModuleNames.ApplicationFlowModule)]
    public class ApplicationProcessModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.InitializeAssembly(Assembly.GetExecutingAssembly());
            containerProvider.Resolve<IRegionManager>().RegisterViewWithRegion<ProcessView>(RegionNames.ProcessRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
