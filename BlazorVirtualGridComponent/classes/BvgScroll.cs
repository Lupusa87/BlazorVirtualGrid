using BlazorScrollbarComponent;
using BlazorScrollbarComponent.classes;
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

        public bool IsVisible { get; set; }
        

        public BsbSettings bsbSettings { get; set; } = new BsbSettings();

        public CompBlazorScrollbar compBlazorScrollbar { get; set; } = new CompBlazorScrollbar();

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
