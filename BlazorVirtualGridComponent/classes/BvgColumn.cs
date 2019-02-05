using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgColumn
    {
        public int ID { get; set; }
        public int Index { get; set; }
       
        public string Name { get; set; }

        public Type type { get; set; }
        public bool IsSelected { get; set; }

        public BvgStyle bvgStyle { get; set; } = new BvgStyle();


        public CompColumn CompReference { get; set; }


        public string GetStyle()
        {

            StringBuilder sb1 = new StringBuilder();


            sb1.Append("text-align:center;border-style:solid;width:100px;height:35px;margin:1px;padding:2px;");

            if (!string.IsNullOrEmpty(bvgStyle.BackgroundColor))
            {
                sb1.Append("background-color:" + bvgStyle.BackgroundColor + ";");
            }

            if (!string.IsNullOrEmpty(bvgStyle.ForeColor))
            {
                sb1.Append("color:" + bvgStyle.ForeColor + ";");
            }
            if (!string.IsNullOrEmpty(bvgStyle.BorderColor))
            {
                sb1.Append("border-color:" + bvgStyle.BorderColor + ";");
            }

            if (IsSelected)
            {

                sb1.Append("cursor:pointer;");

            }
            else
            {
                sb1.Append("cursor:cell;");

            }


            if (bvgStyle.BorderWidth > -1)
            {
                sb1.Append("border-width:" + bvgStyle.BorderWidth + "px;");
            }

            else
            {
                sb1.Append("border-width:1px;");
            }

            return sb1.ToString();

        }

    }
}
