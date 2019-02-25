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
                List<BvgColumn> RemovedColumns = _bvgGrid.Columns.Where(x => !props.ToList().Exists(y => y.prop.Name.Equals(x.Name))).ToList();
                _bvgGrid.Columns.RemoveAll(x => !props.ToList().Exists(y => y.prop.Name.Equals(x.Name)));


                List<ColProp> AddedProps = props.ToList().Where(x => !_bvgGrid.Columns.Exists(y => y.Name.Equals(x.prop.Name))).ToList();
                foreach (var item in AddedProps)
                {
                    _bvgGrid.Columns.Add(getCol((ushort)_bvgGrid.Columns.Count(), item, h, _bvgGrid));
                }


                _bvgGrid.ColumnsDictionary = _bvgGrid.Columns.ToDictionary(x => x.Name, x => x);

                UpdateRows(list, RemovedColumns, AddedProps, _bvgGrid);
                
            }
            else
            {
                for (ushort i = 0; i < props.Count(); i++)
                {
                    _bvgGrid.Columns.Add(getCol(i, props[i], h, _bvgGrid));
                }

                _bvgGrid.ColumnsDictionary = _bvgGrid.Columns.ToDictionary(x => x.Name, x => x);
            }

        }


        private static BvgColumn getCol(ushort id, ColProp p, int h, BvgGrid _bvgGrid)
        {

            return new BvgColumn
            {
                ID = id,
                Name = p.prop.Name,
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

                    foreach (PropertyInfo p in _bvgGrid.ActiveProps)
                    {
                        row.Cells.Add(GetCell(row, p, item, _bvgGrid));
                    }

                    _bvgGrid.Rows.Add(row);

                }

            }
            else
            {

                //BlazorWindowHelper.BlazorTimeAnalyzer.Reset();
                //BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows js approach", MethodBase.GetCurrentMethod());

                k = 0;
                ushort j;
                short i = -1;

                string[] UpdatePkg = new string[_bvgGrid.Rows.Count * _bvgGrid.Rows[0].Cells.Count * 2];

                foreach (T item in list)
                {
                    j = 0;
                    foreach (PropertyInfo p in _bvgGrid.ActiveProps)
                    {

                        _bvgGrid.Rows[k].Cells[j].Value = p.GetValue(item, null).ToString();


                       

                        if (_bvgGrid.Rows[k].Cells[j].bvgColumn.type.Equals(typeof(bool)))
                        {
                            UpdatePkg[++i] ="checkbox" +  _bvgGrid.Rows[k].Cells[j].ID;
                        }
                        else
                        {
                            UpdatePkg[++i] ="span" + _bvgGrid.Rows[k].Cells[j].ID;
                        }

                        
                        UpdatePkg[++i] = _bvgGrid.Rows[k].Cells[j].Value;

                        j++;
                    }
                    k++;
                }



                BvgJsInterop.UpdateElementContentBatchMonoString(UpdatePkg);

                //BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows js approach after send", MethodBase.GetCurrentMethod());

                //BlazorWindowHelper.BlazorTimeAnalyzer.Log();
            }


        }

        private static BvgCell GetCell(BvgRow row, PropertyInfo p, T item, BvgGrid _bvgGrid)
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
                    row.Cells.Add(GetCell(row, p.prop, item, _bvgGrid));
                }

                k++;
            }


            BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows finish", MethodBase.GetCurrentMethod());

        }
    }
}
