using Company.Application.Filter.Attributes;
using Company.Application.Filter.Views;
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
    [Filter]
    public class SobelFilterModel : FilterModelBase
    {
        [Reactive]

        public ushort Value { get; set; } = 0;
        public override string Name => "Sobel";

        public override string View => nameof(SobelFilterView);

        public override string Icon => "IconSobel";

        public override void Filter(UnmanagedArray2D<ushort> photo)
        {
            //throw new NotImplementedException();
        }

        //public override IObservable<Unit> GetObservable()
        //{
        //    return this.WhenAnyValue(p => p.Name, p => p.IsEnable).Select(p => new Unit());
        //}
    }
}
