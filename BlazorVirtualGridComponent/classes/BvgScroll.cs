using BlazorScrollbarComponent;
using BlazorScrollbarComponent.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{

    public class BvgScroll<TItem>
    {
        public Action PropertyChanged;

        public string ID { get; set; }


        public BvgGrid<TItem> bvgGrid { get; set; } = new BvgGrid<TItem>();


        public BsbSettings bsbSettings { get; set; } = new BsbSettings();

        public CompBlazorScrollbar compBlazorScrollbar { get; set; } 

        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }
    }
}
