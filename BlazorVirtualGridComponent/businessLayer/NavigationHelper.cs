using BlazorVirtualGridComponent.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorVirtualGridComponent.businessLayer
{
    public static class NavigationHelper
    {

        public static double GetScrollPositionForColumn(string ColName, bool AlignLeftOrRight, BvgGrid _bvgGrid)
        {
            double result = 0;

            Console.WriteLine("ColName=" + ColName);

            int b = 0;
            if (!AlignLeftOrRight)
            {
                b = _bvgGrid.DisplayedColumnsCount;
            }




            ColProp a = _bvgGrid.ColumnsOrderedList.Single(x => x.prop.Name.Equals(ColName));
            int index = _bvgGrid.ColumnsOrderedList.ToList().IndexOf(a)-b+1;


            Console.WriteLine("Col width=" + a.ColWidth);
            Console.WriteLine("index=" + index);
            Console.WriteLine("ColumnsCount=" + _bvgGrid.DisplayedColumnsCount);
            if (AlignLeftOrRight)
            {
                result = _bvgGrid.NonFrozenColwidthSumsByElement[index] - a.ColWidth;
            }
            else
            {
                result = _bvgGrid.NonFrozenColwidthSumsByElement[index] - a.ColWidth;
            }
            Console.WriteLine("result=" + result);

            return result;
        }
    }
}
