using Company.Application.Filter.Attributes;
using Company.Application.Share.Filter;
using Company.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Filter.Services
{
    [ExposedService(Lifetime.Singleton,typeof(IFilterFactory ))]
    public class FilterFactory : IFilterFactory
    {
        public List<IFilterModel> CreateFilterModels()
        {
            List<IFilterModel> result=new List<IFilterModel>();
            var assembly=Assembly.GetExecutingAssembly();
            var types=assembly.GetTypes().Where(t=>typeof(IFilterModel).IsAssignableFrom(t));
            foreach (var type in types)
            {
                FilterAttribute filterAttribute = type.GetCustomAttribute<FilterAttribute>();
                if (filterAttribute!=null&&filterAttribute.IsEnable==true)
                {
                    //实例化对象的条件是，有该特性且使能
                    object obj=Activator.CreateInstance(type);
                    if (obj is IFilterModel model)
                    {
                        result.Add(model);
                    }
                }
            }


            return result;
        }
    }
}
