using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ID { get; set; }
        

        public string Value { get; set; }

        public BvgColumn bvgColumn { get; set; } = new BvgColumn();
        public BvgRow bvgRow { get; set; } = new BvgRow();

        public BvgGrid bvgGrid { get; set; } = new BvgGrid();

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
   

        public string CssClass { get; set; }

        public Type ValueType { get; set; }

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
