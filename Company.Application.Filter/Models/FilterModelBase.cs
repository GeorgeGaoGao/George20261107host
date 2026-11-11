using Company.Application.Share.Filter;
using Company.Core.Model;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Filter.Models
{
    public abstract class FilterModelBase : ReactiveObject, IFilterModel
    {
        public abstract string Name { get; }

        public abstract string View { get; }

        public abstract string Icon { get; }
        [Reactive]
        public bool IsEnable { get; set; }

        public abstract void Filter(UnmanagedArray2D<ushort> photo);


        public abstract IObservable<Unit> GetObservable();

    }
}
