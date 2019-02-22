using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgRow
    {
        public Action PropertyChanged { get; set; }

        public ushort ID { get; set; }

        public IList<BvgCell> Cells { get; set; } = new List<BvgCell>();

        public bool IsSelected { get; set; }


        public BvgGrid bvgGrid { get; set; } = new BvgGrid();


        public void Cmd_Clear_Selection()
        {
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

                item.InvokePropertyChanged();

            }

        }



        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }
    }
}
