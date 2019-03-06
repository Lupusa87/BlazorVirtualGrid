using BlazorVirtualGridComponent.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorVirtualGridComponent.businessLayer
{
    public static class NavigationHelper<TItem>
    {

        public static double GetScrollPositionForColumn(string ColName, bool AlignLeftOrRight, BvgGrid<TItem> _bvgGrid)
        {
            double result = 0;

      
            int b = 0;
            if (!AlignLeftOrRight)
            {
                b = _bvgGrid.DisplayedColumnsCount;
            }




            ColProp a = _bvgGrid.ColumnsOrderedList.Single(x => x.prop.Name.Equals(ColName));
            int index = _bvgGrid.ColumnsOrderedList.ToList().IndexOf(a)-b+1;



            if (AlignLeftOrRight)
            {
                result = _bvgGrid.NonFrozenColwidthSumsByElement[index] - a.ColWidth;
            }
            else
            {
                result = _bvgGrid.NonFrozenColwidthSumsByElement[index] - a.ColWidth;
            }
          

            return result;
        }
    }
}
