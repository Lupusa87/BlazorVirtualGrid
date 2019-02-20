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

        public PropertyInfo[] Props { get; set; }

        public Action<string> OnSort { get; set; }

        public Action<int> OnScroll { get; set; }

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


        public double CurrScrollPosition { get; set; } = 0;


        public BvgAreaRows bvgAreaRows { get; set; } = new BvgAreaRows();


        public string GetStyleTable(bool ForFrozen)
        {

            StringBuilder sb1 = new StringBuilder();

            //sb1.Append("table-layout:fixed;");
            if (ForFrozen)
            {
                sb1.Append("width:" + FrozenTableWidth + "px;");
            }
            else
            {
                sb1.Append("width:" + NotFrozenTableWidth + "px;");
            }


            return sb1.ToString();

        }


        public string GetStyleDiv()
        {

            StringBuilder sb1 = new StringBuilder();

    
            sb1.Append("width:" + (NotFrozenTableWidth + 5) + "px;height:" + height+ "px;");

            return sb1.ToString();

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
            ActiveColumn.CssClass = "columnActive";

            foreach (var item in Rows)
            {

                BvgCell c = item.Cells.Single(x => x.bvgColumn.ID == ActiveColumn.ID);

                c.IsSelected = true;
                c.CssClass = "CellSelected";
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
                item.CssClass = "ColumnRegular";
                item.BSplitter.SetColor(bvgSettings.HeaderStyle.BackgroundColor);
                item.InvokePropertyChanged();
            }

        }



        public void FreezeColumn(string name, bool par_AffectUI)
        {
            if (Columns.Any())
            {
                if (Columns.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                {

                   
                    Cmd_Clear_Selection();
                   

                    foreach (var item in Columns.Where(x => x.IsFrozen))
                    {
                        item.IsFrozen = false;
                        item.SequenceNumber = (byte)item.ID;
                    }

                    BvgColumn c = Columns.Single(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                    c.IsFrozen = true;
                    c.SequenceNumber = 0;

                    byte k = 0;
                    foreach (var item in Columns.OrderBy(x => x.SequenceNumber))
                    {
                        item.SequenceNumber = k;
                        k++;
                    }

                    if (par_AffectUI)
                    {
                        CalculateWidths();

                        InvokePropertyChanged();
                    }
                }
            }
        }


        public void FreezeColumns(List<string> names, bool par_AffectUI)
        {
            if (Columns.Any())
            {
                Cmd_Clear_Selection();


                for (int i = 0; i < names.Count; i++)
                {
                    if (Columns.Any(x => x.Name.Equals(names[i], StringComparison.InvariantCultureIgnoreCase)))
                    {

                        BvgColumn c = Columns.Single(x => x.Name.Equals(names[i], StringComparison.InvariantCultureIgnoreCase));
                        c.IsFrozen = true;
                        c.SequenceNumber = (byte)(-names.Count+i+1);
                    }
                }


                byte k = 0;
                foreach (var item in Columns.OrderBy(x=>x.SequenceNumber))
                {
                    item.SequenceNumber = k;
                    k++;
                }

                if (par_AffectUI)
                {
                    CalculateWidths();

                    InvokePropertyChanged();
                }

                
            }
        }

        public void UpdateHorizontalScrollbarSettings()
        {
            HorizontalScroll.bsbSettings.ScrollTotalSize = Columns.Where(x => x.IsFrozen == false).Sum(x => x.ColWidth);
            HorizontalScroll.bsbSettings.initialize();
        }


        public void SetWidthToColumn(string name, double Par_Width, bool par_AffectUI)
        {
            if (Columns.Any())
            {
                if (Columns.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                {

                    BvgColumn c = Columns.Single(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                    c.ColWidth = Par_Width;
                    c.HasManualSize = true;

                    if (par_AffectUI)
                    {
                        UpdateHorizontalScrollbarSettings();

                        InvokePropertyChanged();
                    }
                }
            }
        }


        public void SetColumnWidths(Dictionary<string, double> dict, bool par_AffectUI)
        {

            foreach (var item in dict)
            {
                if (Columns.Any())
                {
                    if (Columns.Any(x => x.Name.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        BvgColumn c = Columns.Single(x => x.Name.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase));
                        c.ColWidth = item.Value;
                        c.HasManualSize = true;
                    }
                }
            }

            if (par_AffectUI)
            {
                UpdateHorizontalScrollbarSettings();

                InvokePropertyChanged();
            }
        }


        public Dictionary<string, double> GetColumnWidths()
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            foreach (var item in Columns)
            {
                result.Add(item.Name, item.ColWidth);
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
            double b = Columns.Where(x => x.IsFrozen == false).Sum(x => x.ColWidth);



            if (b > NotFrozenTableWidth)
            {

                if (!HorizontalScroll.IsVisible)
                {
      
                    HorizontalScroll.IsVisible = true;


                    HorizontalScroll.bsbSettings.ScrollTotalSize=b;


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

            FrozenTableWidth = Columns.Where(x => x.IsFrozen).Sum(x => x.ColWidth);
            NotFrozenTableWidth = Columns.Where(x => x.IsFrozen == false).Sum(x => x.ColWidth);

            HorizontalScroll.bsbSettings.ScrollVisibleSize = NotFrozenTableWidth;
            UpdateHorizontalScrollbarSettings();

        }


        public void AdjustSize()
        {
            double t = totalWidth - Columns.Where(x => x.HasManualSize).Sum(x => x.ColWidth);
            double cw = Math.Round(t / Columns.Count(x => x.HasManualSize==false), 2);

            foreach (var item in Columns.Where(x=>x.HasManualSize==false))
            {
                item.ColWidth = cw;
            }

            VericalScroll = new BvgScroll
            {
                ID = Guid.NewGuid().ToString("d").Substring(1, 4),
                bvgGrid = this,
                bsbSettings = new BsbSettings
                {
                    VerticalOrHorizontal = true,
                    width = 16,
                    height = height,
                    ScrollVisibleSize = height- bvgSettings.HeaderHeight - bvgSettings.HeaderStyle.BorderWidth*2,
                    ScrollTotalSize = RowsTotalCount * (bvgSettings.RowHeight + bvgSettings.CellStyle.BorderWidth*2)+10,
                }
            };
            VericalScroll.bsbSettings.initialize();

            HorizontalScroll = new BvgScroll
            {

                ID = Guid.NewGuid().ToString("d").Substring(1, 4),
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

            CalculateWidths();

            DisplayedRowsCount = (int)((height - bvgSettings.HeaderHeight) / bvgSettings.RowHeight);
            bvgSettings.RowHeight = Math.Round((height - bvgSettings.HeaderHeight) / DisplayedRowsCount);




            bvgAreaRows.bvgGrid = this;

           
        }
    }
}
