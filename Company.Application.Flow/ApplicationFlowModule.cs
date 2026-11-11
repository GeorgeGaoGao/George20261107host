using Company.Application.Flow.Views;
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

namespace Company.Application.Flow
{
    [Module(ModuleName = ModuleNames.ApplicationFlowModule, OnDemand = true)]
    [ModuleDependency(ModuleNames.ApplicationFilterModule)]
    public class ApplicationFlowModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.InitializeAssembly(Assembly.GetExecutingAssembly());
            containerProvider.Resolve<IRegionManager>().RegisterViewWithRegion<FlowView>(RegionNames.FlowRegion);

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterAssembly(Assembly.GetExecutingAssembly());
            containerRegistry.RegisterDialog<AddFilterView>();//注册对话框
        }
    }
}
