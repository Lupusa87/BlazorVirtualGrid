using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }
        public int Index { get; set; }

        public List<BvgCell> Cells { get; set; } = new List<BvgCell>();

        public bool IsSelected { get; set; }

        public BvgStyle bvgStyle { get; set; } = new BvgStyle();

        public void Cmd_Clear_Selection()
        {
            foreach (var item in Cells.Where(x=>x.IsSelected))
            {
                item.IsSelected = false;
                item.bvgStyle = new BvgStyle();
                item.InvokePropertyChanged();
                Console.WriteLine("Cmd_Clear_Selection " + item.Value.ToString());
            }

        }


        //public CompRow CompReference { get; set; }


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
