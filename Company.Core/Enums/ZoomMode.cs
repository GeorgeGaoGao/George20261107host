using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Enums
{
    public enum ZoomMode
    {
        [Description("自适应")]
        Uniform,
        [Description("100%")]
        Original,
        [Description("200%")]

        Percent200,
        [Description("300%")]

        Percent300,
        [Description("400%")]

        Percent400,
        [Description("500%")]

        Percent500,
        [Description("600%")]

        Percent600,
        [Description("700%")]

        Percent700,
        [Description("800%")]

        Percent800

    }
}
