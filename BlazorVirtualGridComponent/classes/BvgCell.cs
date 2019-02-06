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



        public BvgStyle bvgStyle { get; set; } = new BvgStyle();

        public bool IsSelected { get; set; }


        //public CompCell CompReference { get; set; }


        public string GetStyle()
        {

            StringBuilder sb1 = new StringBuilder();


            sb1.Append("border-style:solid;width:70px;height:35px;margin:1px;padding:2px;");

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
