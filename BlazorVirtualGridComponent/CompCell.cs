using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent
{
    public class CompCell : ComponentBase, IDisposable
    {


        [Parameter]
        protected BvgCell bvgCell { get; set; }


        protected override void OnInit()
        {
            bvgCell.PropertyChanged += BvgCell_PropertyChanged;
            
        }

        private void BvgCell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            //Console.WriteLine("BuildRenderTree cell");


            int k = -1;
            builder.OpenElement(k++, "td");

            builder.AddAttribute(k++, "style", bvgCell.GetStyleTD());


            builder.AddAttribute(k++, "onclick", Clicked);
            

            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "id", bvgCell.ID);
            builder.AddAttribute(k++, "class", "CellDiv");
            builder.AddAttribute(k++, "tabindex",0); // without this div can't get focus and don't fires keyboard events
            builder.AddAttribute(k++, "style", bvgCell.GetStyleDiv());
            builder.AddAttribute(k++, "onkeydown", OnKeyDown);


            builder.AddContent(k++, bvgCell.Value);
            

            builder.CloseElement(); //div



            builder.CloseElement();


            
        }


        public void Clicked(UIMouseEventArgs e)
        {
            bvgCell.bvgGrid.SelectCell(bvgCell, false);

        }



        public void OnKeyDown(UIKeyboardEventArgs e)
        {
            string a = e.Key.ToLower();

            if (a.Contains("arrow"))
            {

                a = a.Replace("arrow", null);

                //Console.WriteLine(e.Key.ToString());
                //Console.WriteLine(bvgCell.Value.ToString() + " " +  bvgCell.bvgColumn.Name);

                switch (a)
                {
                    case "right":
                        SelectNeightbourCell(MoveDirection.right, e.CtrlKey);
                        break;
                    case "left":
                        SelectNeightbourCell(MoveDirection.left,e.CtrlKey);
                        break;
                    case "up":
                        SelectNeightbourCell(MoveDirection.up,e.CtrlKey);
                        break;
                    case "down":
                        SelectNeightbourCell(MoveDirection.down, e.CtrlKey);
                        break;
                    default:
                        break;
                }
            }

            
            
        }
        public void SelectNeightbourCell(MoveDirection d, bool HasCtrl)
        {

            BvgCell result = new BvgCell();
            int sn = 0;

            switch (d)
            {
                case MoveDirection.right:
                    if (HasCtrl)
                    {
                        sn = bvgCell.bvgGrid.Columns.Max(x=>x.SequenceNumber);
                    }
                    else
                    {
                        sn = bvgCell.bvgColumn.SequenceNumber + 1;
                    }
                    

                    if (bvgCell.bvgGrid.Columns.Any(x=>x.SequenceNumber == sn))
                    {
                        BvgCell c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.SequenceNumber == sn);
                          
                        bvgCell.bvgGrid.SelectCell(c, true);
                      
                        if (bvgCell.bvgGrid.HorizontalScroll.IsVisible)
                        {
                            double s = bvgCell.bvgGrid.Columns.Where(x => x.IsFrozen == false).Where(x => x.SequenceNumber <= bvgCell.bvgColumn.SequenceNumber).Sum(x => x.ColWidth);
                            //double s = await BvgJsInterop.GetElementScrollLeft(bvgCell.bvgGrid.GridDivElementID);

                            bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(s);
                        }
                    }

                    break;
                case MoveDirection.left:
                    if (HasCtrl)
                    {
                        sn = bvgCell.bvgGrid.Columns.Min(x => x.SequenceNumber);
                    }
                    else
                    {
                        sn = bvgCell.bvgColumn.SequenceNumber - 1;
                    }

                    if (bvgCell.bvgGrid.Columns.Any(x => x.SequenceNumber == sn))
                    {
                        BvgCell c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.SequenceNumber == sn);

                        bvgCell.bvgGrid.SelectCell(c, true);

                        if (bvgCell.bvgGrid.HorizontalScroll.IsVisible)
                        {
                            double s = bvgCell.bvgGrid.Columns.Where(x => x.IsFrozen == false).Where(x => x.SequenceNumber < bvgCell.bvgColumn.SequenceNumber).Sum(x => x.ColWidth);

                            //double s = await BvgJsInterop.GetElementScrollLeft(bvgCell.bvgGrid.GridDivElementID);
                            bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(s);
                        }
                    }
                    break;
                case MoveDirection.up:
                    if (HasCtrl)
                    {
                        sn = bvgCell.bvgGrid.Rows.Min(x => x.ID);
                    }
                    else
                    {
                        sn = bvgCell.bvgRow.ID - 1;
                    }
                    
                    if (bvgCell.bvgGrid.Rows.Any(x => x.ID == sn))
                    {
                        BvgCell c = bvgCell.bvgGrid.Rows.Single(x=>x.ID==sn).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);
                      

                        //if (!c.bvgRow.IsInView)
                        //{
                        //    c.FocusRequired = true;

                        //    if (HasCtrl)
                        //    {
                        //        bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(0);
                        //    }
                        //    else
                        //    {
                        //        bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.bsbScrollbar.CmdWhell(false);
                        //    }
                            
                        //}




                        bvgCell.bvgGrid.SelectCell(c, true);

                    }

                    break;
                case MoveDirection.down:
                    if (HasCtrl)
                    {
                        sn = bvgCell.bvgGrid.Rows.Max(x => x.ID);
                    }
                    else
                    {
                        sn = bvgCell.bvgRow.ID + 1;
                    }

                    if (bvgCell.bvgGrid.Rows.Any(x => x.ID == sn))
                    {
                        BvgCell c = bvgCell.bvgGrid.Rows.Single(x => x.ID == sn).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);
                        
                       
                        //if (!c.bvgRow.IsInView)
                        //{
                        //    c.FocusRequired = true;



                        //    if (HasCtrl)
                        //    {
                        //        bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(bvgCell.bvgGrid.Rows.Count* bvgCell.bvgGrid.bvgSettings.RowHeight);
                        //    }
                        //    else
                        //    {
                        //        bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.bsbScrollbar.CmdWhell(true);
                        //    }
                            

                        //}
                        bvgCell.bvgGrid.SelectCell(c, true);
                    }

                    break;
                default:
                    break;
            }


            
        }


            public void Dispose()
        {
            bvgCell.PropertyChanged -= BvgCell_PropertyChanged;
        }
    }
}
