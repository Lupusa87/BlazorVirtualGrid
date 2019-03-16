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

        private int _IndexInSource { get; set; }

        public int IndexInSource
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

        public bool IsSelected { get; set; }

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
                    item.CssClass = CellStyle.CellFrozen.ToString();
                }
                else
                {
                    item.CssClass = CellStyle.CellNonFrozen.ToString();
                }

                l.Add(item.ID);
                l.Add(item.CssClass);
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
