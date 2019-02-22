using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{

    public class BvgAreaColumns : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public BvgGrid bvgGrid { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void InvokePropertyChanged()
        {

            if (PropertyChanged == null)
            {
                Console.WriteLine("BvgAreaColumns InvokePropertyChanged is null");
            }



            PropertyChanged?.Invoke(this, null);
        }
    }
}
