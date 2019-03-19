using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgAreaRows<TItem>
    {
        public Action PropertyChanged;


        public BvgGrid<TItem> bvgGrid { get; set; }



        public void InvokePropertyChanged()
        {

            //if (PropertyChanged==null)
            //{
           
            //}



            PropertyChanged?.Invoke();
        }
    }
}
