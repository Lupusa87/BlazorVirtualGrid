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
    public class BvgGrid : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsReady { get; set; } = false;

        public string GridTableElementID { get; set; } = "GridTable" + Guid.NewGuid().ToString("d").Substring(1, 4);
        public string GridDivElementID { get; set; } = "GridDiv"+ Guid.NewGuid().ToString("d").Substring(1, 4);

        public Dictionary<string, BvgColumn> ColumnsDictionary { get; set; }

        public string Name { get; set; } = "null";

        public PropertyInfo[] AllProps { get; set; }
        public PropertyInfo[] ActiveProps { get; set; }

        public ColProp[] ColumnsOrderedList { get; set; }

        public Action<string> OnSort { get; set; }

        public Action<int> OnVerticalScroll { get; set; }

        public Action<int> OnHorizontalScroll { get; set; }

        public IList<BvgRow> Rows { get; set; } = new List<BvgRow>();
        public IList<BvgColumn> Columns { get; set; } = new List<BvgColumn>();

        public int RowsTotalCount { get; set; }

        public CompGrid compGrid { get; set; }

        public BvgCell ActiveCell;
        public BvgRow ActiveRow;
        public BvgColumn ActiveColumn;


        public BvgSettings bvgSettings { get; set; }

        public BvgScroll VericalScroll { get; set; } = null;
        public BvgScroll HorizontalScroll { get; set; } = null;


        public double totalWidth { get; set; } = 500;
        public double height { get; set; } = 300;

        public double FrozenTableWidth { get; set; } = 0;
        public double NotFrozenTableWidth { get; set; } = 0;


        public int DisplayedRowsCount { get; set; }
        public int DisplayedColumnsCount { get; set; }

        public double CurrVerticalScrollPosition { get; set; } = 0;
        public double CurrHorizontalScrollPosition { get; set; } = 0;

        public BvgAreaRows bvgAreaRowsFrozen { get; set; } = new BvgAreaRows();
        public BvgAreaRows bvgAreaRowsNonFrozen { get; set; } = new BvgAreaRows();

        public BvgAreaColumns bvgAreaColumnsFrozen { get; set; } = new BvgAreaColumns();
        public BvgAreaColumns bvgAreaColumnsNonFrozen { get; set; } = new BvgAreaColumns();

        public string GetStyleTable(bool ForFrozen)
        {
            if (ForFrozen)
            {
                return "width:" + FrozenTableWidth + "px;";
            }
            else
            {
                return "width:" + NotFrozenTableWidth + "px;";
            }

        }


        public string GetStyleDiv()
        {
            return "width:" + (NotFrozenTableWidth + 5) + "px;height:" + height + "px;";
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



        public void SelectRow(BvgRow parRow)
        {
            ActiveCell = null;
            ActiveRow = parRow;
            ActiveColumn = null;

            SelectActiveRow();
        }

        public void SelectColumn(BvgColumn parColumn)
        {
            ActiveCell = null;
            ActiveRow = null;
            ActiveColumn = parColumn;


            SelectActiveColumn();
        }




        private void SelectActiveRow()
        {
            Cmd_Clear_Selection();


            ActiveRow.IsSelected = true;


            foreach (var c in ActiveRow.Cells)
            {
                c.IsSelected = true;
                c.CssClass = CellStyle.CellSelected.ToString();
                c.InvokePropertyChanged();
            }

            ActiveRow.InvokePropertyChanged();
            InvokePropertyChanged();
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

            if (!parColumn.IsAscendingOrDescending)
            {
                OnSort?.Invoke(parColumn.Name);
            }
            else
            {
                OnSort?.Invoke(parColumn.Name + " desc");
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

            ActiveCell.InvokePropertyChanged();
        }


        public void Cmd_Clear_Selection()
        {

          
            foreach (var item in Rows.Where(x=>x.Cells.Any(y=>y.IsSelected)))
            {
                item.Cmd_Clear_Selection();
            }


            foreach (var item in Columns.Where(x => x.IsSelected))
            {
                item.IsSelected = false;
                item.CssClass = HeaderStyle.HeaderRegular.ToString();
                item.BSplitter.SetColor(bvgSettings.HeaderStyle.BackgroundColor);
                item.InvokePropertyChanged();
            }

        }



        public Dictionary<string, int> GetColumnWidths()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var item in ColumnsOrderedList)
            {
                result.Add(item.prop.Name, item.ColWidth);
            }


            return result;
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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


            PropertyChanged?.Invoke(this, null);
        }




        public void UpdateHorizontalScroll()
        {
            double b = ColumnsOrderedList.Where(x => x.IsFrozen == false).Sum(x => x.ColWidth);



            if (b > NotFrozenTableWidth)
            {

                if (!HorizontalScroll.IsVisible)
                {
      
                    HorizontalScroll.IsVisible = true;


                    HorizontalScroll.bsbSettings.ScrollTotalSize=b;
                    HorizontalScroll.bsbSettings.initialize();

                    InvokePropertyChanged();
                }
                else
                {
                    HorizontalScroll.compBlazorScrollbar.SetScrollTotalWidth(b);
                }

            }
            else
            {

                if (HorizontalScroll.IsVisible)
                {
                    HorizontalScroll.IsVisible = false;

                    InvokePropertyChanged();
                }
            }
        }


        public void CalculateWidths()
        {

            FrozenTableWidth = ColumnsOrderedList.Where(x => x.IsFrozen).Sum(x => x.ColWidth);
            NotFrozenTableWidth = totalWidth - FrozenTableWidth;

            HorizontalScroll.bsbSettings.ScrollVisibleSize = NotFrozenTableWidth;

            UpdateHorizontalScroll();
        }


        public void AdjustSize()
        {

            VericalScroll = new BvgScroll
            {
                ID = "VericalScroll"+Guid.NewGuid().ToString("d").Substring(1, 4),
                bvgGrid = this,
                bsbSettings = new BsbSettings
                {
                    VerticalOrHorizontal = true,
                    width = 16,
                    height = height,
                    ScrollVisibleSize = height- bvgSettings.HeaderHeight - bvgSettings.HeaderStyle.BorderWidth*2,
                    ScrollTotalSize = RowsTotalCount * (bvgSettings.RowHeight + bvgSettings.NonFrozenCellStyle.BorderWidth*2)+10,
                }
            };
            VericalScroll.bsbSettings.initialize();

            HorizontalScroll = new BvgScroll
            {

                ID = "HorizontalScroll" + Guid.NewGuid().ToString("d").Substring(1, 4),
                bvgGrid = this,
                bsbSettings = new BsbSettings
                {

                    VerticalOrHorizontal = false,
                    width = totalWidth + 5,
                    height = 16,
                    ScrollVisibleSize = 0,
                    ScrollTotalSize = 0,

                }
            };
            

            DisplayedRowsCount = (int)((height - bvgSettings.HeaderHeight) / bvgSettings.RowHeight);
            bvgSettings.RowHeight = Math.Round((height - bvgSettings.HeaderHeight) / DisplayedRowsCount);



            CalculateWidths();

            bvgAreaRowsFrozen.bvgGrid = this;
            bvgAreaRowsNonFrozen.bvgGrid = this;

            bvgAreaColumnsFrozen.bvgGrid = this;
            bvgAreaColumnsNonFrozen.bvgGrid = this;
        }
    }
}
