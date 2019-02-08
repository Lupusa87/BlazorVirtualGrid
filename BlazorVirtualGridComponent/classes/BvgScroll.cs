using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{

    public class BvgScroll : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ID { get; set; }


        public bool IsVerticalOrHorizontal { get; set; } = true;

        public double ScrollWidth { get; set; }

        public double ScrollHeight { get; set; }

        public double ContentSize { get; set; }

        public BvgStyle bvgStyle { get; set; } = new BvgStyle();



        //public CompCell CompReference { get; set; }

        //           < div style = "overflow-y:auto;overflow-x:scroll;width:10px;height:200px" >
        //< canvas style = "width:10px;height:2000px" >
        public string GetStyleDiv()
        {

            StringBuilder sb1 = new StringBuilder();

            if (IsVerticalOrHorizontal)
            {

                sb1.Append("overflow-y:scroll;overflow-x:auto;width:" + ScrollWidth + "px;height:" + ScrollHeight + "px;");


            }
            else
            {
                sb1.Append("overflow-y:auto;overflow-x:scroll;width:" + ScrollWidth + "px;height:" + ScrollHeight + "px;");


            }


            return sb1.ToString();

        }


        public string GetStyleCanvas()
        {

            StringBuilder sb1 = new StringBuilder();


            if (IsVerticalOrHorizontal)
            {

                sb1.Append("width:" + ScrollWidth + "px;height:" + ContentSize + "px;");

            }
            else
            {
                sb1.Append("width:" + ContentSize + "px;height:" + ScrollHeight + "px;");

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
