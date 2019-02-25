using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgAreaRows 
    {
        public Action PropertyChanged;


        public BvgGrid bvgGrid { get; set; }



        public void InvokePropertyChanged()
        {

            if (PropertyChanged==null)
            {
                Console.WriteLine("BvgAreaRows InvokePropertyChanged is null");
            }



            PropertyChanged?.Invoke();
        }
    }
}
