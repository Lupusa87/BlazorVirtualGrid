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

        public BvgCell[] Cells { get; set; } = new BvgCell[0];

        public bool IsSelected { get; set; }

        public bool IsEven { get; set; }


        public BvgGrid bvgGrid { get; set; } = new BvgGrid();


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
                l.Add(item.CssClassTD);
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
