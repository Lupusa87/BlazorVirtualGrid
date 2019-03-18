using BlazorScrollbarComponent.classes;
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
using BlazorSplitterComponent;

namespace BlazorVirtualGridComponent.businessLayer
{
    public static class GenericAdapter<T>
    {
        public static void GetColumns(ColProp[] props, BvgGrid<T> _bvgGrid, T[] list, bool UpdateUI)
        {
            int h = _bvgGrid.bvgSettings.HeaderHeight - 10;

            if (!_bvgGrid.Columns.Any())
            {
                _bvgGrid.Columns = new BvgColumn<T>[props.Count()];
                for (ushort i = 0; i < props.Count(); i++)
                {
                    _bvgGrid.Columns[i] =getCol(i, props[i], h, _bvgGrid);
                }
            }
            else
            {


                _bvgGrid.Columns = new BvgColumn<T>[props.Count()];
                string[] UpdatePkg = new string[props.Count() * 4];
                short j = -1;
                for (ushort i = 0; i < props.Count(); i++)
                {
                    _bvgGrid.Columns[i] = getCol(i, props[i], h, _bvgGrid);

                    UpdatePkg[++j] = i.ToString();
                    UpdatePkg[++j] = props[i].prop.Name;
                    UpdatePkg[++j] = _bvgGrid.Columns[i].ColWidth.ToString();
                    UpdatePkg[++j] = _bvgGrid.Columns[i].ColWidthSpan.ToString();
                }

                if (_bvgGrid.Rows.Any())
                {
                   
                    foreach (BvgRow<T> r in _bvgGrid.Rows)
                    {
                        foreach (BvgCell<T> c in r.Cells)
                        {
                          c.bvgColumn = _bvgGrid.Columns[c.bvgColumn.ID];                           
                        }
                    }
                }


                if (UpdateUI)
                {
                    BvgJsInterop.UpdateColContentsBatch(UpdatePkg);
                    RefreshRows(list, _bvgGrid, true, 0);
                }
            }


          
            if (_bvgGrid.SortState.Item1)
            {
                if (_bvgGrid.Columns.Any(x => x.prop.Name.Equals(_bvgGrid.SortState.Item2)))
                {
                    BvgColumn<T> sortedCol2 = _bvgGrid.Columns.Single(x => x.prop.Name.Equals(_bvgGrid.SortState.Item2));
                   
                    sortedCol2.IsSorted = true;
                    sortedCol2.IsAscendingOrDescending = _bvgGrid.SortState.Item3;
                }
            }
        }


        private static BvgColumn<T> getCol(ushort id, ColProp p, int h, BvgGrid<T> _bvgGrid)
        {

            return new BvgColumn<T>
            {
                ID = id,
                prop = p.prop,
                SequenceNumber = p.SequenceNumber,
                bvgGrid = _bvgGrid,
                CssClass = HeaderStyle.HeaderRegular.ToString(),
                IsFrozen = p.IsFrozen,
                ColWidth = p.ColWidth,
                bsSettings = new BsSettings(string.Concat("Splitter" +p.prop.Name))
                {
                    VerticalOrHorizontal = false,
                    index = id,
                    width = 5,
                    height = h,
                    BgColor = _bvgGrid.bvgSettings.HeaderStyle.BackgroundColor,
                    //BgColor = "red",
                }
            };
        }

        public static IEnumerable<T> GetSortedRowsList(IQueryable<T> list, string OrderByClause)
        {
            return list.OrderBy(OrderByClause);
        }


        public static void GetRows(T[] list, BvgGrid<T> _bvgGrid, ushort skip)
        {
 
            ushort k = 0;
            if (!_bvgGrid.Rows.Any())
            {
                _bvgGrid.Rows = new BvgRow<T>[list.Count()];

                ushort j = 0;
                ushort g = 0;
                foreach (T item in list)
                {

                    BvgRow<T> row = new BvgRow<T>
                    {
                        ID = k++,
                        IndexInSource = (ushort)(k + skip),
                        bvgGrid = _bvgGrid,
                        Cells = new BvgCell<T>[_bvgGrid.Columns.Count()],
                    };

                    g = 0;
                    foreach (BvgColumn<T> col in _bvgGrid.Columns)
                    {
                        row.Cells[g] = GetCell(row, col, item, _bvgGrid,true);
                        g++;
                    }

                    _bvgGrid.Rows[j]=row;
                    j++;
                }

            }
            else
            {
                RefreshRows(list, _bvgGrid, false, skip);
              
            }

           
        }

        private static BvgCell<T> GetCell(BvgRow<T> row, BvgColumn<T> col, T item, BvgGrid<T> _bvgGrid, bool SetValue)
        {

            BvgCell<T> cell = new BvgCell<T>
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

        private static void RefreshRows(T[] list, BvgGrid<T> _bvgGrid, bool updateWidths, int skip)
        {

            short i = -1;
            short i2 = -1;
            short i3 = -1;
            byte g = 0;
            string[] UpdatePkg = new string[_bvgGrid.Rows.Count() * _bvgGrid.Rows[0].Cells.Count() * 3];

           

            string[] UpdatePkg2 = new string[1];
            if (updateWidths)
            {
                UpdatePkg2 = new string[_bvgGrid.Rows.Count() * _bvgGrid.Rows[0].Cells.Count() * 2];
            }

            string[] UpdatePkgClass = new string[1];
            if (!updateWidths)
            {
                UpdatePkgClass = new string[_bvgGrid.Rows.Count() * _bvgGrid.Rows[0].Cells.Count() * 2];
            }


            foreach (T item in list)
            {
                if (!updateWidths)
                {
                    _bvgGrid.Rows[g].IndexInSource = (ushort)(g + skip+1);
                }

                foreach (BvgCell<T> c in _bvgGrid.Rows[g].Cells)
                {
                   
                    c.Value = c.bvgColumn.prop.GetValue(item, null).ToString();
                   
                    UpdatePkg[++i] = c.ID;
                    UpdatePkg[++i] = c.Value;

                    if (!updateWidths)
                    {
                        c.UpdateCssClass();
                        UpdatePkgClass[++i3] = c.ID;
                        UpdatePkgClass[++i3] = c.CssClass;
                    }

                    if (c.bvgColumn.prop.PropertyType.Equals(typeof(bool)))
                    {
                        UpdatePkg[++i] = "b";
                    }
                    else
                    {
                        UpdatePkg[++i] = "s";
                    }

                    if (updateWidths)
                    {
                        UpdatePkg2[++i2] = c.ID;
                        UpdatePkg2[++i2] = c.bvgColumn.ColWidth.ToString();
                    }

                }
                g++;
            }



            BvgJsInterop.UpdateRowContentBatch(UpdatePkg);

            if (!updateWidths)
            {
                BvgJsInterop.UpdateCellClassBatch(UpdatePkgClass);
            }

            if (updateWidths)
            {
                BvgJsInterop.UpdateRowWidthsBatch(UpdatePkg2);
            }

        }
    }
}
