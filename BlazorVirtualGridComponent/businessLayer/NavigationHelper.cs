using BlazorVirtualGridComponent.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorVirtualGridComponent.businessLayer
{
    public static class NavigationHelper<TItem>
    {
        public static void SelectCell(bool AlignLeftOrRight, string ColName, ushort RowID, BvgGrid<TItem> _bvgGrid, double p=-1)
        {
            double p1 = p+5;
            if (p == -1)
            {
                p1 = GetScrollPositionForColumn(ColName, AlignLeftOrRight, _bvgGrid);
            }

            _bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(p1);

            _bvgGrid.ShouldSelectCell = Tuple.Create(true, RowID, ColName);

            _bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(p1 - 5);
        }


        private static double GetScrollPositionForColumn(string ColName, bool AlignLeftOrRight, BvgGrid<TItem> _bvgGrid)
        {
            if (!_bvgGrid.ColumnsOrderedListNonFrozen.Any(x => x.prop.Name.Equals(ColName)))
            {
                return 0;
            }

            double result = 0;
        

            ColProp a = _bvgGrid.ColumnsOrderedListNonFrozen.Single(x => x.prop.Name.Equals(ColName));
            int index = _bvgGrid.ColumnsOrderedListNonFrozen.ToList().IndexOf(a);

            string s=string.Empty;
            for (int i = 0; i < _bvgGrid.NonFrozenColwidthSumsByElement.Count(); i++)
            {
                s += _bvgGrid.NonFrozenColwidthSumsByElement[i] + " ";
            }


            result = _bvgGrid.NonFrozenColwidthSumsByElement[index] - a.ColWidth + 5;

            if (!AlignLeftOrRight)
            {
                result -= _bvgGrid.NonFrozenTableWidth;
            }

            return result;
        }
    }
}
