using BlazorScrollbarComponent.classes;
using BlazorSplitterComponent;
using BlazorVirtualGridComponent.businessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgGrid 
    {
        public Action PropertyChanged;



        public bool IsReady { get; set; } = false;

        public string GridTableElementID { get; set; } = "GridTable" + Guid.NewGuid().ToString("d").Substring(1, 4);

        public string Name { get; set; } = "null";

        public PropertyInfo[] AllProps { get; set; }
        public PropertyInfo[] ActiveProps { get; set; }

        public ColProp[] ColumnsOrderedList { get; set; }
        public ColProp[] ColumnsOrderedListFrozen { get; set; }
        public ColProp[] ColumnsOrderedListNonFrozen { get; set; }


        public int[] NonFrozenColwidthSumsByElement { get; set; }
        public Action<string> OnSort { get; set; }

        public Action OnColumnResize { get; set; }

        public Action<double> OnVerticalScroll { get; set; }

        public Action<double> OnHorizontalScroll { get; set; }

     

        public BvgRow[] Rows { get; set; } = new BvgRow[0];
        public BvgColumn[] Columns { get; set; } = new BvgColumn[0];

        public int RowsTotalCount { get; set; }

        public CompGrid compGrid { get; set; }

        
        public BvgCell ActiveCell;
        public BvgRow ActiveRow;
        public BvgColumn ActiveColumn;


        public Tuple<bool, BvgColumn> SortState;

        public BvgSettings bvgSettings { get; set; }

        public BvgScroll VericalScroll { get; set; } = null;
        public BvgScroll HorizontalScroll { get; set; } = null;


        public double totalWidth { get; set; } = 500;
        public double height { get; set; } = 300;

        public double FrozenTableWidth { get; set; } = 0;
        public double NonFrozenTableWidth { get; set; } = 0;


        public int DisplayedRowsCount { get; set; }
        public int DisplayedColumnsCount { get; set; }

        public double CurrVerticalScrollPosition { get; set; } = 0;
        public double CurrHorizontalScrollPosition { get; set; } = 0;

        public BvgAreaRows bvgAreaRowsFrozen { get; set; } = new BvgAreaRows();
        public BvgAreaRows bvgAreaRowsNonFrozen { get; set; } = new BvgAreaRows();

        public BvgAreaColumns bvgAreaColumnsFrozen { get; set; } = new BvgAreaColumns();
        public BvgAreaColumns bvgAreaColumnsNonFrozen { get; set; } = new BvgAreaColumns();


        public double RowHeightOriginal { get; set; }

        public string GetStyleTable(bool ForFrozen)
        {
            if (ForFrozen)
            {
                return string.Concat("width:" , FrozenTableWidth , "px;");
            }
            else
            {
                return string.Concat("width:" , NonFrozenTableWidth , "px;");
            }

        }


        public string GetStyleDiv(bool ForFrozen)
        {

            if (ForFrozen)
            {
                return string.Concat("width:", FrozenTableWidth, "px;height:", height, "px;");
            }
            else
            {
                return string.Concat("width:", NonFrozenTableWidth, "px;height:", height, "px;");
            }


            
        }


        public void SelectCell(BvgCell parCell, bool doFocus)
        {
            ActiveCell = parCell;
            ActiveRow = parCell.bvgRow;
            ActiveColumn = parCell.bvgColumn;

          
            SelectActiveRow();
            SelectActiveCell(false);

            if (doFocus)
            {
                ActiveCellFocus();
            }
        }

        public void ActiveCellFocus()
        {
            if (ActiveCell != null)
            {
                BvgJsInterop.SetFocus(ActiveCell.ID);
            }
        }


        private void SelectActiveRow()
        {
            Cmd_Clear_Selection();


            ActiveRow.IsSelected = true;

            short j = -1;
            string[] UpdatePkg = new string[ActiveRow.Cells.Count() * 2];
            
 
            foreach (var c in ActiveRow.Cells)
            {
                c.IsSelected = true;
                c.CssClass = CellStyle.CellSelected.ToString();

                UpdatePkg[++j] = c.ID.ToString();
                UpdatePkg[++j] = c.CssClassTD.ToString();
                //c.InvokePropertyChanged();
            }


            BvgJsInterop.SetAttributeBatch(UpdatePkg, "class");

            //ActiveRow.InvokePropertyChanged();
            //InvokePropertyChanged();
        }

        private void SelectActiveColumn()
        {
            Cmd_Clear_Selection();


            ActiveColumn.IsSelected = true;
            ActiveColumn.CssClass = HeaderStyle.HeaderActive.ToString();

            foreach (var item in Rows)
            {

                BvgCell c = item.Cells.Single(x => x.bvgColumn.ID == ActiveColumn.ID);

                c.IsSelected = true;
                c.CssClass = CellStyle.CellSelected.ToString();
                c.InvokePropertyChanged();
            }



            ActiveColumn.BSplitter.SetColor(bvgSettings.HeaderStyle.BackgroundColor);

            ActiveColumn.InvokePropertyChanged();

            

        }


        public void SortColumn(BvgColumn parColumn)
        {

            
            if (parColumn.IsSorted)
            {
               
                parColumn.IsAscendingOrDescending = !parColumn.IsAscendingOrDescending;

            }
            else
            {
                foreach (var item in Columns.Where(x => x.IsSorted))
                {
                    item.IsSorted = false;
                    item.InvokePropertyChanged();
                }

                parColumn.IsSorted = true;
                parColumn.IsAscendingOrDescending = false;
            }

            parColumn.InvokePropertyChanged();


            SortState = Tuple.Create(true, parColumn);


            if (!parColumn.IsAscendingOrDescending)
            {
                OnSort?.Invoke(parColumn.prop.Name);
            }
            else
            {
                OnSort?.Invoke(parColumn.prop.Name + " desc");
            }

        }

        private void SelectActiveCell(bool DoClear = true)
        {
            if (DoClear)
            {
                Cmd_Clear_Selection();
            }

            ActiveCell.IsSelected = true;
            ActiveCell.IsActive = true;
            ActiveCell.CssClass = CellStyle.CellActive.ToString();


            BvgJsInterop.SetAttributeBatch(new string[] { ActiveCell.ID, ActiveCell.CssClassTD }, "class");


            //ActiveCell.InvokePropertyChanged();
        }


        public void Cmd_Clear_Selection()
        {
            List<string> l = new List<string>();

            foreach (var item in Rows.Where(x=>x.Cells.Any(y=>y.IsSelected)))
            {
               l.AddRange(item.Cmd_Clear_Selection());
            }


            BvgJsInterop.SetAttributeBatch(l.ToArray(), "class");

            //foreach (var item in Columns.Where(x => x.IsSelected))
            //{
            //    item.IsSelected = false;
            //    item.CssClass = HeaderStyle.HeaderRegular.ToString();
            //    item.BSplitter.SetColor(bvgSettings.HeaderStyle.BackgroundColor);
            //    item.InvokePropertyChanged();
            //}

        }



        public ValuesContainer<Tuple<string, ushort>> GetColumnWidths()
        {
            ValuesContainer<Tuple<string, ushort>> result = new ValuesContainer<Tuple<string, ushort>>();

            foreach (var item in ColumnsOrderedList)
            {
                result.Add(Tuple.Create(item.prop.Name, item.ColWidth));
            }


            return result;
        }


      


        public void InvokePropertyChanged()
        {

            if (PropertyChanged == null)
            {

                if (compGrid != null)
                {
                    compGrid.Subscribe();
                }
            }


            PropertyChanged?.Invoke();
        }




        public void UpdateHorizontalScroll()
        {

            int b = ColumnsOrderedList.Where(x => x.IsFrozen == false).Sum(x => x.ColWidth);


            if (HorizontalScroll.compBlazorScrollbar is null)
            {
                HorizontalScroll.bsbSettings.ScrollTotalSize = b;
                HorizontalScroll.bsbSettings.initialize();
            }
            else
            {
                HorizontalScroll.compBlazorScrollbar.SetScrollTotalWidth(b);
            }


        }


        public void CalculateWidths()
        {

            FrozenTableWidth = ColumnsOrderedList.Where(x => x.IsFrozen).Sum(x => x.ColWidth);
            NonFrozenTableWidth = totalWidth - FrozenTableWidth;

            DisplayedColumnsCount = (int)(NonFrozenTableWidth / bvgSettings.ColWidthMin) + 1;

            HorizontalScroll.bsbSettings.ScrollVisibleSize = NonFrozenTableWidth;

            UpdateHorizontalScroll();
        }


        public void AdjustSize()
        {
            RowHeightOriginal = bvgSettings.RowHeight;
            //DisplayedRowsCount =1+(int)Math.Ceiling((height - bvgSettings.HeaderHeight) / bvgSettings.RowHeight);
            DisplayedRowsCount = (int)((height - bvgSettings.HeaderHeight) / bvgSettings.RowHeight);
            bvgSettings.RowHeight = (height - bvgSettings.HeaderHeight) / DisplayedRowsCount;


            VericalScroll = new BvgScroll
            {
                bvgGrid = this,
                bsbSettings = new BsbSettings("VericalScroll")
                {
                    VerticalOrHorizontal = true,
                    width = 16,
                    height = height - bvgSettings.HeaderStyle.BorderWidth * 2,
                    ScrollVisibleSize = height - bvgSettings.HeaderHeight - bvgSettings.HeaderStyle.BorderWidth * 2,
                    ScrollTotalSize = RowsTotalCount * RowHeightOriginal,
                    bsbStyle = new BsbStyle
                    {
                        ButtonColor = bvgSettings.VerticalScrollStyle.ButtonColor,
                        ThumbColor = bvgSettings.VerticalScrollStyle.ThumbColor,
                        ThumbWayColor = bvgSettings.VerticalScrollStyle.ThumbWayColor,
                    }
                }
            };
            VericalScroll.ID = VericalScroll.bsbSettings.ID;
            VericalScroll.bsbSettings.initialize();

            HorizontalScroll = new BvgScroll
            {
                bvgGrid = this,
                bsbSettings = new BsbSettings("HorizontalScroll")
                {

                    VerticalOrHorizontal = false,
                    width = totalWidth,
                    height = 16,
                    ScrollVisibleSize = 0,
                    ScrollTotalSize = 0,
                    bsbStyle = new BsbStyle
                    {
                        ButtonColor = bvgSettings.HorizontalScrollStyle.ButtonColor,
                        ThumbColor = bvgSettings.HorizontalScrollStyle.ThumbColor,
                        ThumbWayColor = bvgSettings.HorizontalScrollStyle.ThumbWayColor,
                    }
                }
            };
            HorizontalScroll.ID = HorizontalScroll.bsbSettings.ID;


            CalculateWidths();


            bvgAreaRowsFrozen.bvgGrid = this;
            bvgAreaRowsNonFrozen.bvgGrid = this;

            bvgAreaColumnsFrozen.bvgGrid = this;
            bvgAreaColumnsNonFrozen.bvgGrid = this;
        }



       
  }
}
