using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgGrid : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }

        public bool IsReady { get; set; } = false;


        public string Name { get; set; } = "null";

        //public Action OnChange { get; set; }

        public List<BvgRow> Rows { get; set; } = new List<BvgRow>();
        public List<BvgColumn> Columns { get; set; } = new List<BvgColumn>();

        //public CompGrid CompReference { get; set; }

        public BvgCell ActiveCell;
        public BvgRow ActiveRow;
        public BvgColumn ActiveColumn;

        public void SelectCell(BvgCell parCell)
        {
            ActiveCell = parCell;
            ActiveRow = parCell.bvgRow;
            ActiveColumn = parCell.bvgColumn;

            Console.WriteLine("Select cell " + parCell.Value.ToString());
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

            Console.WriteLine("count " + Rows.Where(x => x.Cells.Any(y => y.IsSelected)).Count());
            foreach (var item in Rows.Where(x=>x.Cells.Any(y=>y.IsSelected)))
            {
                item.Cmd_Clear_Selection();
            }


            foreach (var item in Columns.Where(x => x.IsSelected))
            {
                item.IsSelected = false;
                item.bvgStyle = new BvgStyle();
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


                    InvokePropertyChanged();
                }
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke(this, null);
        }

    }
}
