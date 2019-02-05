using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgRow
    {
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
                item.CompReference.Refresh();
            }

        }


        public CompRow CompReference { get; set; }
    }
}
