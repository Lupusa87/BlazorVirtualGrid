using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }


        public object Value { get; set; }


        public BvgColumn bvgColumn { get; set; } = new BvgColumn();
        public BvgRow bvgRow { get; set; } = new BvgRow();

        public BvgGrid bvgGrid { get; set; } = new BvgGrid();

        public BvgStyle bvgStyle { get; set; } = new BvgStyle();

        public bool IsSelected { get; set; }


        //public CompCell CompReference { get; set; }


        public string GetStyleTD()
        {

            StringBuilder sb1 = new StringBuilder();

            sb1.Append("margin:0px;padding:0px;");
            sb1.Append("border-style:solid;width:" + bvgColumn.ColWidth + "px;height:" + bvgGrid.RowHeight + "px;");


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

           
            sb1.Append("width:" + (bvgColumn.ColWidth- bvgStyle.BorderWidth) + "px;height:" + (bvgGrid.RowHeight- bvgStyle.BorderWidth) + "px;line-height:" + (bvgGrid.RowHeight - bvgStyle.BorderWidth) + "px;");
            sb1.Append("overflow:hidden;white-space:nowrap;text-overflow:ellipsis;");
            sb1.Append("margin:0px;padding:0px;");

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
