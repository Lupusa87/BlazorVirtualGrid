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
    public class BvgColumn 
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


        public BvgGrid bvgGrid { get; set; } = new BvgGrid();


        public BsSettings bsSettings { get; set; } = new BsSettings();

        public CompBlazorSplitter BSplitter { get; set; } = new CompBlazorSplitter();


        public ushort ColWidthWithoutBorder { get; set; }

        public ushort _ColWidth { get; set; }
        public ushort ColWidth { get
            {
                return _ColWidth;
            }
            set
            {
                _ColWidth = value;

                ColWidthDiv =(ushort)(_ColWidth - bvgGrid.bvgSettings.HeaderStyle.BorderWidth);
                ColWidthSpan = (ushort)(ColWidth - 5);
            }
        } 


        public ushort ColWidthDiv { get; set; }
        public ushort ColWidthSpan { get; set; } 



        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }

    }
}
