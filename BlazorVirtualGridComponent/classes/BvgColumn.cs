using BlazorSplitterComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgColumn<TItem>
    {
        public Action PropertyChanged { get; set; }

        public ushort ID { get; set; }

        public PropertyInfo prop { get; set; }

       
        public bool IsSelected { get; set; }

        public string CssClass { get; set; }

        public bool IsSorted { get; set; }

        public bool IsAscendingOrDescending { get; set; }

        public bool IsFrozen { get; set; }

      
        public ushort SequenceNumber { get; set; }


        public BvgGrid<TItem> bvgGrid { get; set; } = new BvgGrid<TItem>();


        public BsSettings bsSettings { get; set; } = new BsSettings();

        public CompBlazorSplitter BSplitter { get; set; } = new CompBlazorSplitter();



        public ushort _ColWidth { get; set; }
        public ushort ColWidth { get
            {
                return _ColWidth;
            }
            set
            {
                _ColWidth = value;

                ColWidthSpan = (ushort)(ColWidth - 2 * (5 + bvgGrid.bvgSettings.bSortStyle.width));
            }
        } 



        public ushort ColWidthSpan { get; set; } 



        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }

    }
}
