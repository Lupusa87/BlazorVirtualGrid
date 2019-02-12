using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgGrid : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }

        public bool IsReady { get; set; } = false;

        public string GridDivElementID { get; set; } = "GridDiv"+ Guid.NewGuid().ToString("d").Substring(1, 4);


        public string Name { get; set; } = "null";

        //public Action OnChange { get; set; }

        public List<BvgRow> Rows { get; set; } = new List<BvgRow>();
        public List<BvgColumn> Columns { get; set; } = new List<BvgColumn>();

        //public CompGrid CompReference { get; set; }

        public BvgCell ActiveCell;
        public BvgRow ActiveRow;
        public BvgColumn ActiveColumn;


        public BvgScroll VericalScroll { get; set; } = null;
        public BvgScroll HorizontalScroll { get; set; } = null;


        public double totalWidth { get; set; } = 1000;


        public double FrozenWidth { get; set; } = 0;
        public double NotFrozenWidth { get; set; } = 0;

        public double HorizontalScrollBarScale { get; set; } = 0;

        public double height { get; set; } = 600;

        public double HeaderHeight { get; set; } = 50;
        public double RowHeight { get; set; } = 40;

        public double ColMinWidth { get; set; } = 100;


        public int DisplayedRowsCount { get; set; }


        public int CurrScrollPosition { get; set; } = 0;


        public BvgAreaRows bvgAreaRows { get; set; } = new BvgAreaRows();


        public string GetStyleTable(bool ForFrozen)
        {

            StringBuilder sb1 = new StringBuilder();

            //sb1.Append("table-layout:fixed;width:" + width + "px;");

            if (ForFrozen)
            {
                sb1.Append("border: 1px solid black;width:" + FrozenWidth + "px;");
            }
            else
            {
                sb1.Append("border: 1px solid black;width:" + NotFrozenWidth + "px;");
            }


            sb1.Append("margin:0;padding:0;");


            return sb1.ToString();

        }


        public string GetStyleDiv()
        {

            StringBuilder sb1 = new StringBuilder();

            sb1.Append("margin:0;padding:0;");
            sb1.Append("width:" + (NotFrozenWidth + 5) + "px;height:" + height+ "px;overflow-x:scroll;overflow-y:hidden;");

            return sb1.ToString();

        }

        public void SelectCell(BvgCell parCell)
        {
            ActiveCell = parCell;
            ActiveRow = parCell.bvgRow;
            ActiveColumn = parCell.bvgColumn;

          
            SelectActiveRow();
            SelectActiveCell(false);
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

            ActiveRow.bvgStyle = new BvgStyle()
            {
                BorderColor = "blue",
                BorderWidth = 1,
                BackgroundColor = "wheat",
            };


            foreach (var c in ActiveRow.Cells)
            {
                c.IsSelected = true;
                c.bvgStyle = ActiveRow.bvgStyle;
                //c.CompReference.Refresh();
                c.InvokePropertyChanged();
            }

            ActiveRow.InvokePropertyChanged();
            InvokePropertyChanged();
        }

        private void SelectActiveColumn()
        {
            Cmd_Clear_Selection();


            ActiveColumn.IsSelected = true;

            ActiveColumn.bvgStyle = new BvgStyle()
            {
                BorderColor = "blue",
                BorderWidth = 2,
                BackgroundColor = "wheat",
            };


            BvgStyle b = new BvgStyle()
            {
                BackgroundColor = "wheat",
            };


            foreach (var item in Rows)
            {

                BvgCell c = item.Cells.Single(x => x.bvgColumn.ID == ActiveColumn.ID);

                c.IsSelected = true;
                c.bvgStyle = b;
                c.InvokePropertyChanged();
            }



            ActiveColumn.BSplitter.SetColor(ActiveColumn.bvgStyle.BackgroundColor);

            ActiveColumn.InvokePropertyChanged();

            

        }


        private void SelectActiveCell(bool DoClear = true)
        {
            if (DoClear)
            {
                Cmd_Clear_Selection();
            }

            ActiveCell.IsSelected = true;

            ActiveCell.bvgStyle = new BvgStyle()
            {
                BorderColor = "blue",
                BorderWidth = 2,
                BackgroundColor = "wheat",
            };


            //ActiveCell.CompReference.Refresh();
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
                item.bvgStyle = new BvgStyle();
                item.BSplitter.SetColor(item.bvgStyle.BackgroundColor);
                item.InvokePropertyChanged();
            }

        }



        public void FreezeColumn(string name)
        {
            if (Columns.Any())
            {
                if (Columns.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                {

                   
                    Cmd_Clear_Selection();
                   

                    foreach (var item in Columns.Where(x => x.IsFrozen))
                    {
                        item.IsFrozen = false;
                        item.SequenceNumber = item.ID;
                    }

                    BvgColumn c = Columns.Single(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                    c.IsFrozen = true;
                    c.SequenceNumber = 0;


                    CalculateWidths();

                    InvokePropertyChanged();
                }
            }
        }


        public void FreezeColumns(List<string> names)
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
                        c.SequenceNumber = 0 - names.Count+i;
                    }
                }

                CalculateWidths();

                InvokePropertyChanged();
            }
        }


        public void SetWidthToColumn(string name, double Par_Width)
        {
            if (Columns.Any())
            {
                if (Columns.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                {

                    BvgColumn c = Columns.Single(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                    c.ColWidth = Par_Width;

                    InvokePropertyChanged();
                }
            }
        }


        public void SetColumnWidths(Dictionary<string, double> dict)
        {

            foreach (var item in dict)
            {
                if (Columns.Any())
                {
                    if (Columns.Any(x => x.Name.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        BvgColumn c = Columns.Single(x => x.Name.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase));
                        c.ColWidth = item.Value;
                    }
                }
            }


            InvokePropertyChanged();
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
            PropertyChanged?.Invoke(this, null);
        }



        public void OnVerticalScroll()
        {

            foreach (var item in Rows.Where(x => x.IsInView))
            {
                item.IsInView = false;
            }



            int Curr_Skip = (int)(CurrScrollPosition / RowHeight);


            if (Curr_Skip > 0)
            {

                foreach (var item in Rows.Skip(Curr_Skip).Take(DisplayedRowsCount))
                {
                    item.IsInView = true;
                }
            }
            else
            {
                foreach (var item in Rows.Take(DisplayedRowsCount))
                {
                    item.IsInView = true;
                }

            }

            bvgAreaRows.InvokePropertyChanged();


        }

        public void UpdateHorizontalScroll()
        {
            double b = Columns.Where(x => x.IsFrozen == false).Sum(x => x.ColWidth);

            Console.WriteLine("b " + b);

            Console.WriteLine("HorizontalScrollBarScale " + HorizontalScrollBarScale);

            Console.WriteLine("scrollable width " + HorizontalScroll.bsbSettings.ThumbWaySize);
           
         


            if (b * HorizontalScrollBarScale > HorizontalScroll.bsbSettings.ThumbWaySize)
            {

                Console.WriteLine("inside");

                //HorizontalScroll.compBlazorScrollbar.SetScrollWidth(b * HorizontalScrollBarScale);


                if (!HorizontalScroll.IsVisible)
                {
                    Console.WriteLine("inside 2");
                    HorizontalScroll.IsVisible = true;
                    //HorizontalScroll.compBlazorScrollbar.SetVisibility(HorizontalScroll.bsbSettings.IsVisible);
                    InvokePropertyChanged();
                }

            }
            else
            {

                Console.WriteLine("inside 3");

                if (HorizontalScroll.IsVisible)
                {
                    HorizontalScroll.IsVisible = false;
                   

                    InvokePropertyChanged();
                }
            }
        }


        public void CalculateWidths()
        {
            FrozenWidth = Columns.Where(x => x.IsFrozen).Sum(x => x.ColWidth);
            NotFrozenWidth = Columns.Where(x => x.IsFrozen == false).Sum(x => x.ColWidth);

            HorizontalScroll.bsbSettings.ThumbWaySize = totalWidth + 5 - HorizontalScroll.bsbSettings.ButtonSize * 2;
            HorizontalScrollBarScale = HorizontalScroll.bsbSettings.ThumbWaySize / NotFrozenWidth;
            if (HorizontalScrollBarScale < 1)
            {
                HorizontalScrollBarScale = 1;
            }
        }
    }
}
