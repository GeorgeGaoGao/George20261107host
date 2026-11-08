using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Model
{
    public class BindableSourceItem<T>
    {
        public BindableSourceItem(string descritpion, T value)
        {
            Descritpion = descritpion;
            Value = value;
        }
        public BindableSourceItem()
        {
            
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Descritpion { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public T  Value { get; set; }
       
    }
}
