using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgAreaRows : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; } = 1;

        public BvgGrid bvgGrid { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void InvokePropertyChanged()
        {

            if (PropertyChanged==null)
            {
                Console.WriteLine("BvgAreaRows InvokePropertyChanged is null");
            }



            PropertyChanged?.Invoke(this, null);
        }
    }
}
