using BlazorScrollbarComponent.classes;
using BlazorSplitterComponent;
using BlazorVirtualGridComponent.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace BlazorVirtualGridComponent.businessLayer
{
    public static class GenericAdapter<T> 
    {

        public static void GetColumns(IQueryable<T> GenericList, BvgGrid result)
        {
            result.ColumnsDictionary = new Dictionary<string, BvgColumn>();
            result.Columns = new List<BvgColumn>();
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            ushort k = 0;
            foreach (PropertyInfo prop in Props)
            {

                var t = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);

                BvgColumn col = new BvgColumn
                {
                    ID = k++,
                    Name = prop.Name,
                    type = t,
                    SequenceNumber = (byte)k,
                    bvgGrid = result,
                };
                result.ColumnsDictionary.Add(prop.Name, col);
                result.Columns.Add(col);
            }



            foreach (var item in result.Columns)
            {
                item.bsSettings = new BsSettings
                {
                    VerticalOrHorizontal = false,
                    index = result.Columns.Count,
                    width = 5,
                    height = result.HeaderHeight - item.bvgStyle.BorderWidth * 2,
                    //BgColor = item.bvgStyle.BackgroundColor,
                    BgColor = "red",
                };
            }

        }

        public static IEnumerable<T> GetSortedList(IQueryable<T> GenericList, string OrderByClause)
        {
            return GenericList.OrderBy(OrderByClause);
        }


        public static void GetRows(IEnumerable<T> GenericList, BvgGrid result)
        {

            result.Rows = new List<BvgRow>();


            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            ushort k = 0;
            BvgColumn col;

            foreach (T item in GenericList)
            {

                BvgRow row = new BvgRow
                {
                    ID = k++,
                    bvgGrid = result,
                };

                foreach (PropertyInfo p in Props)
                {

                    result.ColumnsDictionary.TryGetValue(p.Name, out col);

                    BvgCell cell = new BvgCell
                    {
                        Value = p.GetValue(item, null),
                        bvgRow = row,
                        bvgColumn = col,
                        bvgGrid = result,
                    };

                    cell.ID = "C" + col.ID + "R" + row.ID;

                    row.Cells.Add(cell);
                }

                result.Rows.Add(row);
            }



           
        }

    }
}
