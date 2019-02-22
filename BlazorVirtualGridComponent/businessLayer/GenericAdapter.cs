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
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent.businessLayer
{
    public static class GenericAdapter<T> 
    {


        public static void GetColumns(IEnumerable<ColProp> props, BvgGrid _bvgGrid)
        {
            _bvgGrid.ColumnsDictionary = new Dictionary<string, BvgColumn>();
            _bvgGrid.Columns = new List<BvgColumn>();
           
            ushort k = 0;
            foreach (ColProp p in props)
            {

                var t = (p.prop.PropertyType.IsGenericType && p.prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(p.prop.PropertyType) : p.prop.PropertyType);

                BvgColumn col = new BvgColumn
                {
                    ID = k++,
                    Name = p.prop.Name,
                    type = t,
                    SequenceNumber = (byte)k,
                    bvgGrid = _bvgGrid,
                    CssClass = HeaderStyle.HeaderRegular.ToString(),
                    IsFrozen = p.IsFrozen,
                    ColWidth = p.ColWidth,
                };
                _bvgGrid.ColumnsDictionary.Add(p.prop.Name, col);
                _bvgGrid.Columns.Add(col);
            }


            foreach (var item in _bvgGrid.Columns)
            {
                item.bsSettings = new BsSettings
                {
                    VerticalOrHorizontal = false,
                    index = _bvgGrid.Columns.Count,
                    width = 5,
                    height = _bvgGrid.bvgSettings.HeaderHeight - item.bvgGrid.bvgSettings.HeaderStyle.BorderWidth * 2,
                    //BgColor = item.bvgStyle.BackgroundColor,
                    BgColor = "red",
                };
            }

        }

        public static IEnumerable<T> GetSortedRowsList(IQueryable<T> list, string OrderByClause)
        {
            return list.OrderBy(OrderByClause);
        }


        public static void GetRows(IEnumerable<T> list, BvgGrid _bvgGrid, bool DotNetOrJsUpdate)
        {

            ushort k = 0;

            if (DotNetOrJsUpdate)
            {
                if (_bvgGrid.Rows.Count > 0)
                {
                    _bvgGrid.Rows = new List<BvgRow>();
                }
            }

            if (_bvgGrid.Rows.Count == 0 )
            {

                foreach (T item in list)
                {

                    BvgRow row = new BvgRow
                    {
                        ID = k++,
                        bvgGrid = _bvgGrid,
                    };

                   
                    foreach (PropertyInfo p in _bvgGrid.ActiveProps)
                    {
                       
                        _bvgGrid.ColumnsDictionary.TryGetValue(p.Name, out BvgColumn col);
                       
                        BvgCell cell = new BvgCell
                        {
                            Value = p.GetValue(item, null).ToString(),
                            bvgRow = row,
                            bvgColumn = col,
                            bvgGrid = _bvgGrid,
                            ValueType = col.type,
                        };
                       
                        if (col.IsFrozen)
                        {
                            cell.CssClass = CellStyle.CellFrozen.ToString();
                        }
                        else
                        {
                            cell.CssClass = CellStyle.CellNonFrozen.ToString();
                        }

                        cell.ID = "C" + col.ID + "R" + row.ID;

                        row.Cells.Add(cell);
                    }

                    _bvgGrid.Rows.Add(row);
                    
                }
            }
            else
            {



                k = 0;
                ushort j;
                short i = -1;

                string[] UpdatePkg = new string[_bvgGrid.Rows.Count * _bvgGrid.Rows[0].Cells.Count * 3];

                foreach (T item in list)
                {
                    j = 0;
                    foreach (PropertyInfo p in _bvgGrid.ActiveProps)
                    {

                        _bvgGrid.Rows[k].Cells[j].Value = p.GetValue(item, null).ToString();

                        
                        UpdatePkg[++i] = _bvgGrid.Rows[k].Cells[j].ID;
                        UpdatePkg[++i] = _bvgGrid.Rows[k].Cells[j].Value;

                        if (_bvgGrid.Rows[k].Cells[j].bvgColumn.type.Equals(typeof(bool)))
                        {
                            UpdatePkg[++i] = "b";
                        }
                        else
                        {
                            UpdatePkg[++i] = string.Empty;
                        }
                        j++;
                    }
                    k++;
                }


                BvgJsInterop.UpdateElementContentBatchMonoString(UpdatePkg);


                BlazorWindowHelper.BlazorTimeAnalyzer.Log();
            }


        }

    }
}
