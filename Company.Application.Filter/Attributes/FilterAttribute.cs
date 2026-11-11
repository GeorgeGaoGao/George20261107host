using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Filter.Attributes
{
    /// <summary>
    /// 标注的对象应为滤波算法的实体模型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class FilterAttribute:Attribute
    {
        public int Index { get; set; } = 0;
        public bool IsEnable { get; set; } = true;
    }
}
