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
    public class BvgColumn : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }
        public int Index { get; set; }

        public string Name { get; set; }

        public Type type { get; set; }
        public bool IsSelected { get; set; }


        public bool IsFrozen { get; set; }


        public int SequenceNumber { get; set; }


        public BvgGrid bvgGrid { get; set; } = new BvgGrid();

        public BvgStyle bvgStyle { get; set; } = new BvgStyle();


        public BsSettings bsSettings { get; set; } = new BsSettings();

        public CompBlazorSplitter BSplitter { get; set; } = new CompBlazorSplitter();

        public double ColWidth { get; set; } = 100;

        public double ColWidthMin { get; set; } = 50;
        public double ColWidthMax { get; set; } = 300;

        public string GetStyleTh()
        {

            StringBuilder sb1 = new StringBuilder();

            sb1.Append("margin:0px;padding:0px;");
            sb1.Append("text-align:center;border-style:solid;");

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


            return sb1.ToString();

        }


        public string GetStyleDiv()
        {

            StringBuilder sb1 = new StringBuilder();


            sb1.Append("display:flex;flex-direction:row;width:" + (ColWidth - bvgStyle.BorderWidth)+"px;");
            sb1.Append("height:" + (bvgGrid.HeaderHeight- bvgStyle.BorderWidth) + "px;line-height:" + (bvgGrid.HeaderHeight - bvgStyle.BorderWidth) + "px;");
            sb1.Append("margin:0px;padding:0px;");

            return sb1.ToString();

        }


        public string GetStyleSpan()
        {

            StringBuilder sb1 = new StringBuilder();


            sb1.Append("color:blue;width:" + (ColWidth - 5) + "px;height:" + bvgGrid.HeaderHeight + "px;text-align:center;");
       

            return sb1.ToString();

        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke(this, null);
        }

    }
}
