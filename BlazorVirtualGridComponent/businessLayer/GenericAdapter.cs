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
        public static void GetColumns(ColProp[] props, BvgGrid _bvgGrid, T[] list)
        {
            int h = (int)(_bvgGrid.bvgSettings.HeaderHeight - _bvgGrid.bvgSettings.HeaderStyle.BorderWidth * 2);

            if (_bvgGrid.ColumnsDictionary.Any())
            {

                List<BvgColumn> RemovedColumns = _bvgGrid.Columns.Where(x => !props.ToList().Exists(y => y.prop.Name.Equals(x.prop.Name))).ToList();
                _bvgGrid.Columns.RemoveAll(x => !props.ToList().Exists(y => y.prop.Name.Equals(x.prop.Name)));


                Queue<ushort> availableIDsQueue = new Queue<ushort>(RemovedColumns.Select(x => x.ID));


                List<ColProp> AddedProps = props.ToList().Where(x => !_bvgGrid.Columns.Exists(y => y.prop.Name.Equals(x.prop.Name))).ToList();
                foreach (var item in AddedProps)
                {
                    if (availableIDsQueue.Any())
                    {
                        _bvgGrid.Columns.Add(getCol(availableIDsQueue.Dequeue(), item, h, _bvgGrid));
                    }
                    else
                    {
                        _bvgGrid.Columns.Add(getCol((ushort)(_bvgGrid.Columns.Max(x => x.ID) + 1), item, h, _bvgGrid));
                    }

                }


                _bvgGrid.ColumnsDictionary = _bvgGrid.Columns.ToDictionary(x => x.prop.Name, x => x);

                UpdateRows(list, RemovedColumns, AddedProps, _bvgGrid);
                
            }
            else
            {
                for (ushort i = 0; i < props.Count(); i++)
                {
                    _bvgGrid.Columns.Add(getCol(i, props[i], h, _bvgGrid));
                }

                _bvgGrid.ColumnsDictionary = _bvgGrid.Columns.ToDictionary(x => x.prop.Name, x => x);
            }

        }


        private static BvgColumn getCol(ushort id, ColProp p, int h, BvgGrid _bvgGrid)
        {

            return new BvgColumn
            {
                ID = id,
                prop = p.prop,
                type = (p.prop.PropertyType.IsGenericType && p.prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(p.prop.PropertyType) : p.prop.PropertyType),
                SequenceNumber = p.SequenceNumber,
                bvgGrid = _bvgGrid,
                CssClass = HeaderStyle.HeaderRegular.ToString(),
                IsFrozen = p.IsFrozen,
                ColWidth = p.ColWidth,
                ColWidthWithoutBorder = p.ColWidth - _bvgGrid.bvgSettings.NonFrozenCellStyle.BorderWidth,
                bsSettings = new BsSettings
                {
                    VerticalOrHorizontal = false,
                    index = id,
                    width = 5,
                    height = h,
                    //BgColor = item.bvgStyle.BackgroundColor,
                    BgColor = "red",
                }
            };
        }

        public static IEnumerable<T> GetSortedRowsList(IQueryable<T> list, string OrderByClause)
        {
            return list.OrderBy(OrderByClause);
        }



        public static void GetRows(T[] list, BvgGrid _bvgGrid)
        {

            ushort k = 0;
            if (_bvgGrid.Rows.Count == 0)
            {

                foreach (T item in list)
                {

                    BvgRow row = new BvgRow
                    {
                        ID = k++,
                        bvgGrid = _bvgGrid,
                    };


                    row.IsEven = row.ID % 2==0;

                    foreach (BvgColumn col in _bvgGrid.Columns)
                    {
                        row.Cells.Add(GetCell(row, col, item, _bvgGrid,true));
                    }

                    _bvgGrid.Rows.Add(row);

                }

            }
            else
            {
                RefreshRows(list, _bvgGrid);
              
            }

           
        }

        private static BvgCell GetCell(BvgRow row, BvgColumn col, T item, BvgGrid _bvgGrid, bool SetValue)
        {

            BvgCell cell = new BvgCell
            {
                bvgRow = row,
                bvgColumn = col,
                bvgGrid = _bvgGrid,
            };

            if (SetValue)
            {
                cell.Value = col.prop.GetValue(item, null).ToString();
            }

            
            if (col.IsFrozen)
            {
                cell.CssClass = CellStyle.CellFrozen.ToString();
            }
            else
            {
                cell.CssClass = CellStyle.CellNonFrozen.ToString();
            }

            cell.UpdateID();
            
            return cell;
        }


        

        public static void UpdateRows(T[] list,  List<BvgColumn> RemovedColumns, List<ColProp> AddedProps, BvgGrid _bvgGrid)
        {

            int k = 0;

            BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows start", MethodBase.GetCurrentMethod());
          
            foreach (T item in list)
            {
                
                BvgRow row = _bvgGrid.Rows[k];

                row.Cells.RemoveAll(x => RemovedColumns.Exists(y => y == x.bvgColumn));

                foreach (ColProp p in AddedProps)
                {
                    _bvgGrid.ColumnsDictionary.TryGetValue(p.prop.Name, out BvgColumn col);

                    row.Cells.Add(GetCell(row, col, item, _bvgGrid, false));
                }

                k++;
            }


           

            BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows finish", MethodBase.GetCurrentMethod());

        }


        public static void RefreshRows(T[] list, BvgGrid _bvgGrid)
        {
            //BlazorWindowHelper.BlazorTimeAnalyzer.Reset();
            //BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows js approach", MethodBase.GetCurrentMethod());

         

            short i = -1;
            byte g = 0;
            string[] UpdatePkg = new string[_bvgGrid.Rows.Count * _bvgGrid.Rows[0].Cells.Count * 2];

            foreach (T item in list)
            {
                foreach (BvgCell c in _bvgGrid.Rows[g].Cells)
                {

                    c.Value = c.bvgColumn.prop.GetValue(item, null).ToString();

                    if (c.bvgColumn.type.Equals(typeof(bool)))
                    {
                        UpdatePkg[++i] = "ch" + c.ID;
                    }
                    else
                    {
                        UpdatePkg[++i] = "sp" + c.ID;
                    }


                    UpdatePkg[++i] = c.Value;
                }
                g++;
            }


      
            BvgJsInterop.UpdateElementContentBatchMonoString(UpdatePkg);

            //BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows js approach after send", MethodBase.GetCurrentMethod());

            //BlazorWindowHelper.BlazorTimeAnalyzer.Log();
        }
    }
}
