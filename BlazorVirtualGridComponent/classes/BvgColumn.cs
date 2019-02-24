using BlazorSplitterComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgColumn 
    {
        public Action PropertyChanged { get; set; }

        public ushort ID { get; set; }

        public string Name { get; set; }

        public Type type { get; set; }
        public bool IsSelected { get; set; }

        public string CssClass { get; set; }

        public bool IsSorted { get; set; }

        public bool IsAscendingOrDescending { get; set; }

        public bool IsFrozen { get; set; }

      
        public ushort SequenceNumber { get; set; }


        public BvgGrid bvgGrid { get; set; } = new BvgGrid();


        public BsSettings bsSettings { get; set; } = new BsSettings();

        public CompBlazorSplitter BSplitter { get; set; } = new CompBlazorSplitter();



        public double _ColWidth { get; set; }
        public double ColWidth { get
            {
                return _ColWidth;
            }
            set
            {
                _ColWidth = value;

                ColWidthDiv = _ColWidth - bvgGrid.bvgSettings.HeaderStyle.BorderWidth;
                ColWidthSpan = ColWidth - 5;
            }
        } 


        public double ColWidthDiv { get; set; }
        public double ColWidthSpan { get; set; } 



        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }

    }
}
