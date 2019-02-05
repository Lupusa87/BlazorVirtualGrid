using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgGrid
    {
        public int ID { get; set; }
        public string Name { get; set; }

        //public Action OnChange { get; set; }

        public List<BvgRow> Rows { get; set; } = new List<BvgRow>();
        public List<BvgColumn> Columns { get; set; } = new List<BvgColumn>();

        public CompGrid CompReference { get; set; }

        public BvgCell ActiveCell;
        public BvgRow ActiveRow;
        public BvgColumn ActiveColumn;

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

            Console.WriteLine("ActiveRow " +ActiveRow.ID);
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
                c.CompReference.Refresh();
            }

            ActiveRow.CompReference.Refresh();
            CompReference.Refresh();
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
                
                BvgCell c =  item.Cells.Single(x => x.bvgColumn == ActiveColumn);
                c.IsSelected = true;
                c.bvgStyle = b;
                c.CompReference.Refresh();
            }


            ActiveColumn.CompReference.Refresh();
        }


        private void SelectActiveCell(bool DoClear=true)
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


            ActiveCell.CompReference.Refresh();

        }


        public void Cmd_Clear_Selection()
        {
            foreach (var item in Rows)
            {
                item.Cmd_Clear_Selection();
            }


            foreach (var item in Columns.Where(x=>x.IsSelected))
            {
                item.IsSelected = false;
                item.bvgStyle = new BvgStyle();
                item.CompReference.Refresh();
            }
            
        }

    }
}
