using BlazorSplitterComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgColumn 
    {
        public Action PropertyChanged { get; set; }

        public ushort ID { get; set; }

        public string Name { get; set; }

        public Type type { get; set; }
        public bool IsSelected { get; set; }

        public string CssClass { get; set; }

        public bool IsSorted { get; set; }

        public bool IsAscendingOrDescending { get; set; }

        public bool IsFrozen { get; set; }

        public bool HasManualSize { get; set; }
        public byte SequenceNumber { get; set; }


        public BvgGrid bvgGrid { get; set; } = new BvgGrid();


        public BsSettings bsSettings { get; set; } = new BsSettings();

        public CompBlazorSplitter BSplitter { get; set; } = new CompBlazorSplitter();

        public double ColWidth { get; set; } = 100;

        public double ColWidthMin { get; set; } = 50;
        public double ColWidthMax { get; set; } = 300;

        public string GetStyleTh()
        {

            StringBuilder sb1 = new StringBuilder();

           

            if (!string.IsNullOrEmpty(bvgGrid.bvgSettings.HeaderStyle.BackgroundColor))
            {
                sb1.Append("background-color:" + bvgGrid.bvgSettings.HeaderStyle.BackgroundColor + ";");
            }

            if (!string.IsNullOrEmpty(bvgGrid.bvgSettings.HeaderStyle.ForeColor))
            {
                sb1.Append("color:" + bvgGrid.bvgSettings.HeaderStyle.ForeColor + ";");
            }
            if (!string.IsNullOrEmpty(bvgGrid.bvgSettings.HeaderStyle.BorderColor))
            {
                sb1.Append("border-color:" + bvgGrid.bvgSettings.HeaderStyle.BorderColor + ";");
            }

            if (IsSelected)
            {

                sb1.Append("cursor:pointer;");

            }
            else
            {
                sb1.Append("cursor:cell;");

            }


            if (bvgGrid.bvgSettings.HeaderStyle.BorderWidth > -1)
            {
                sb1.Append("border-width:" + bvgGrid.bvgSettings.HeaderStyle.BorderWidth + "px;");
            }


            return sb1.ToString();

        }


        public string GetStyleDiv()
        {

            StringBuilder sb1 = new StringBuilder();

            sb1.Append("width:" + (ColWidth - bvgGrid.bvgSettings.HeaderStyle.BorderWidth)+"px;");
            sb1.Append("height:" + (bvgGrid.bvgSettings.HeaderHeight - bvgGrid.bvgSettings.HeaderStyle.BorderWidth) + "px;line-height:" + (bvgGrid.bvgSettings.HeaderHeight - bvgGrid.bvgSettings.HeaderStyle.BorderWidth) + "px;");

            return sb1.ToString();

        }


        public string GetStyleSpan()
        {

            StringBuilder sb1 = new StringBuilder();


            sb1.Append("width:" + (ColWidth - 5) + "px;height:" + bvgGrid.bvgSettings.HeaderHeight + "px");
       

            return sb1.ToString();

        }





        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }

    }
}
