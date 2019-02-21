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


            if (bvgCell.CssClass.Equals(CellStyle.CellRegular.ToString()))
            {
                if (bvgCell.bvgRow.ID % 2 ==0)
                {
                    builder.AddAttribute(k++, "class", "CellAlternated");
                }
                else
                {
                    builder.AddAttribute(k++, "class", bvgCell.CssClass);
                }
                
            }
            else
            {
                builder.AddAttribute(k++, "class", bvgCell.CssClass);
            }




            builder.AddAttribute(k++, "style", "width:" + bvgCell.bvgColumn.ColWidth+"px");


            builder.AddAttribute(k++, "onclick", Clicked);
            

            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "id", bvgCell.ID);
            builder.AddAttribute(k++, "class", "CellDiv");
            builder.AddAttribute(k++, "tabindex",0); // without this div can't get focus and don't fires keyboard events
            builder.AddAttribute(k++, "style", "width:" + (bvgCell.bvgColumn.ColWidth - bvgCell.bvgGrid.bvgSettings.CellStyle.BorderWidth)+"px");
            builder.AddAttribute(k++, "onkeydown", OnKeyDown);

            if (bvgCell.ValueType.Equals(typeof(bool)))
            {
     
                builder.OpenElement(k++, "input");
                builder.AddAttribute(k++, "id", "checkbox" + bvgCell.ID);
                builder.AddAttribute(k++, "type", "checkbox");
                if (bvgCell.Value.ToLower() == "true")
                {
                    builder.AddAttribute(k++, "checked",string.Empty);
                }

                builder.AddAttribute(k++, "style", "zoom:1.5");
                builder.AddAttribute(k++, "onclick", CheckboxClicked);
                builder.CloseElement(); //input
            }
            else
            { 
               builder.AddContent(k++, bvgCell.Value);
            }

            builder.CloseElement(); //div



            builder.CloseElement();


            
        }


        
        public void CheckboxClicked(UIMouseEventArgs e)
        {
            BvgJsInterop.SetValueToCheckBox("checkbox" + bvgCell.ID, bvgCell.Value);
            bvgCell.bvgGrid.SelectCell(bvgCell, false);

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
                        bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(0);

                        BvgCell c = bvgCell.bvgGrid.Rows.Single(x => x.ID == 0).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                        bvgCell.bvgGrid.SelectCell(c, true);

                    }
                    else
                    {
                        sn = bvgCell.bvgRow.ID - 1;

                        if (bvgCell.bvgGrid.Rows.Any(x => x.ID == sn))
                        {
                            BvgCell c = bvgCell.bvgGrid.Rows.Single(x => x.ID == sn).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                            bvgCell.bvgGrid.SelectCell(c, true);

                        }
                        else
                        {
                            bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.bsbScrollbar.CmdWhell(false);


                            BvgCell c = bvgCell.bvgGrid.Rows.Single(x => x.ID == bvgCell.bvgGrid.Rows.Max(x2 => x2.ID)).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                            bvgCell.bvgGrid.SelectCell(c, true);

                        }
                    }
                    break;
                case MoveDirection.down:
                    if (HasCtrl)
                    {
                        bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(bvgCell.bvgGrid.RowsTotalCount * bvgCell.bvgGrid.bvgSettings.RowHeight);

                        BvgCell c = bvgCell.bvgGrid.Rows.Single(x => x.ID == bvgCell.bvgGrid.Rows.Max(x2 => x2.ID)).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                        bvgCell.bvgGrid.SelectCell(c, true);
                    }
                    else
                    {
                        sn = bvgCell.bvgRow.ID + 1;

                        if (bvgCell.bvgGrid.Rows.Any(x => x.ID == sn))
                        {
                            BvgCell c = bvgCell.bvgGrid.Rows.Single(x => x.ID == sn).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                            bvgCell.bvgGrid.SelectCell(c, true);
                        }
                        else
                        {

                            bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.bsbScrollbar.CmdWhell(true);

                            BvgCell c = bvgCell.bvgGrid.Rows.Single(x => x.ID == 0).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                            bvgCell.bvgGrid.SelectCell(c, true);
                        }
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
