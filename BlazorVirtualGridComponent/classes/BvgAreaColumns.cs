using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{

    public class BvgAreaColumns<TItem>
    {
        public Action PropertyChanged;


        public BvgGrid<TItem> bvgGrid { get; set; }

      

        public void InvokePropertyChanged()
        {

            //if (PropertyChanged == null)
            //{
        
            //}



            PropertyChanged?.Invoke();
        }
    }
}
