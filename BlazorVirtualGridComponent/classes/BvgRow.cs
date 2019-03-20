using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgRow<TItem>
    {
        public Action PropertyChanged { get; set; }

        private ushort _IndexInSource { get; set; }

        public ushort IndexInSource
        {
            get
            {
                return _IndexInSource;
            }
            set
            {
                _IndexInSource = value;
                IsEven =_IndexInSource % 2 == 0;
            }
        }

        

        public ushort ID { get; set; }

        public BvgCell<TItem>[] Cells { get; set; } = new BvgCell<TItem>[0];


        public bool IsEven { get; set; }


        public BvgGrid<TItem> bvgGrid { get; set; } = new BvgGrid<TItem>();


        public List<string> Cmd_Clear_Selection()
        {
            List<string> l = new List<string>();


            foreach (var item in Cells.Where(x=>x.IsSelected))
            {
                item.IsSelected = false;
                item.IsActive = false;

                if (item.bvgColumn.IsFrozen)
                {
                    item.CssClassBase = CellStyle.CF.ToString();
                }
                else
                {
                    item.CssClassBase = CellStyle.CNF.ToString();
                }

                l.Add(item.ID);
                l.Add(item.CssClassFull);
                //item.InvokePropertyChanged();

            }

            return l;

        }



        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }
    }
}
