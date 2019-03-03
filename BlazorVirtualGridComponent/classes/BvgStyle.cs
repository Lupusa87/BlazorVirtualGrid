using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgStyle
    {

        public string OutlineColor { get; set; } = "#0000FF";
        public string BorderColor { get; set; } = "#000000";
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public string ForeColor { get; set; } = "#0000FF";
        public sbyte BorderWidth { get; set; } = 1;

        public sbyte OutlineWidth { get; set; } = 2;
    }
}
