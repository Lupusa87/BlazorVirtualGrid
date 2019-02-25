using BlazorScrollbarComponent;
using BlazorScrollbarComponent.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{

    public class BvgScroll 
    {
        public Action PropertyChanged;

        public string ID { get; set; }


        public BvgGrid bvgGrid { get; set; } = new BvgGrid();

        public bool IsVisible { get; set; } = false;
        

        public BsbSettings bsbSettings { get; set; } = new BsbSettings();

        public CompBlazorScrollbar compBlazorScrollbar { get; set; } = new CompBlazorScrollbar();

     

        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }
    }
}
