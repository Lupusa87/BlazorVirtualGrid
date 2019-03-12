using BlazorVirtualGridComponent.businessLayer;
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
    public class CompCell<TItem> : ComponentBase, IDisposable
    {


        [Parameter]
        protected BvgCell<TItem> bvgCell { get; set; }


        //bool EnabledRender = true;


        //protected override Task OnParametersSetAsync()
        //{
            
        //    EnabledRender = true;

        //    return base.OnParametersSetAsync();
        //}

        //protected override bool ShouldRender()
        //{
            
        //    return EnabledRender;
        //}


        protected override void OnInit()
        {
            bvgCell.PropertyChanged = BvgCell_PropertyChanged;

        }


        private void BvgCell_PropertyChanged()
        {
            //EnabledRender = true;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            //EnabledRender = false;

            int k = -1;
           
            

            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "id", string.Concat("divCell", bvgCell.ID));
            builder.AddAttribute(k++, "class", bvgCell.CssClass);
            builder.AddAttribute(k++, "tabindex", 0); // without this div can't get focus and don't fires keyboard events
            //builder.AddAttribute(k++, "style", string.Concat("width:", bvgCell.bvgColumn.ColWidth, "px"));
            builder.AddAttribute(k++, "onclick", Clicked);
            builder.AddAttribute(k++, "onkeydown", OnKeyDown);


            builder.OpenElement(k++, "input");
            
            builder.AddAttribute(k++, "id", string.Concat("chCell", bvgCell.ID));
            builder.AddAttribute(k++, "type", "checkbox");
            if (bvgCell.bvgColumn.prop.PropertyType.Equals(typeof(bool)))
            {
                if (!string.IsNullOrEmpty(bvgCell.Value))
                {
                    if (bvgCell.Value.ToLower() == "true")
                    {
                        builder.AddAttribute(k++, "checked", string.Empty);
                    }
                }
            }
            else
            {
                builder.AddAttribute(k++, "hidden", string.Empty);
            }
            //builder.AddAttribute(k++, "style", string.Concat("zoom:", bvgCell.bvgGrid.bvgSettings.CheckBoxZoom));
            builder.AddAttribute(k++, "style", string.Concat("transform:scale(", bvgCell.bvgGrid.bvgSettings.CheckBoxZoom,")"));
       
            builder.AddAttribute(k++, "onclick", CheckboxClicked);
            builder.CloseElement(); //input



            builder.OpenElement(k++, "span");
            if (bvgCell.bvgColumn.prop.PropertyType.Equals(typeof(bool)))
            {
                builder.AddAttribute(k++, "hidden", string.Empty);
            }
            builder.AddAttribute(k++, "id", string.Concat("spCell", bvgCell.ID));
            builder.AddContent(k++, bvgCell.Value);
            builder.CloseElement(); //span



            builder.CloseElement(); //div

            base.BuildRenderTree(builder);
        }


        
        public void CheckboxClicked(UIMouseEventArgs e)
        {
            BvgJsInterop.SetValueToCheckBox(string.Concat("chCell", bvgCell.ID), bvgCell.Value);
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

            BvgCell<TItem> result = new BvgCell<TItem>();
            int sn = 0;

            switch (d)
            {
                case MoveDirection.right:
                    //if (HasCtrl)
                    //{
                    //    if (!bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.IsOnMaxPosition())
                    //    {
                    //        //bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(
                    //        //    NavigationHelper.GetScrollPositionForColumn(
                    //        //        bvgCell.bvgGrid.ColumnsOrderedList.Last().prop.Name,
                    //        //        false,
                    //        //        bvgCell.bvgGrid));

                    //        bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(
                    //            NavigationHelper.GetScrollPositionForColumn(
                    //                "C15",
                    //                true,
                    //                bvgCell.bvgGrid));
                    //    }

                    //    //if (bvgCell.bvgColumn.ID < bvgCell.bvgGrid.Columns.Max(x => x.ID))
                    //    //{

                    //    //    BvgCell c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgGrid.Columns.Max(x2 => x2.ID));

                    //    //    bvgCell.bvgGrid.SelectCell(c, true);

                    //    //}
                    //}
                    //else
                    //{
                        
                    //    sn = bvgCell.bvgColumn.ID + 1;


                    //    if (bvgCell.bvgRow.Cells.Where(x => x.HasCol).Any(x => x.bvgColumn.ID == sn))
                    //    {
                    //        BvgCell c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.ID == sn);
                    //        bvgCell.bvgGrid.SelectCell(c, true);

                    //        bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.ThumbMove(c.bvgColumn.ColWidth);

                            
         
                    //        BvgJsInterop.SetElementScrollLeft("NonFrozenDiv1", 0);
                    //    }
                    //    else
                    //    {
                    //        if (!bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.IsOnMaxPosition())
                    //        {

                    //            ColProp a = bvgCell.bvgGrid.ColumnsOrderedList.Single(x => x.prop.Name.Equals(bvgCell.bvgColumn.prop.Name));
                              
                    //            int index = bvgCell.bvgGrid.ColumnsOrderedList.ToList().IndexOf(a);
                              
                    //            if (bvgCell.bvgGrid.ColumnsOrderedList.Count() > index + 1)
                    //            {
                    //                ColProp b = bvgCell.bvgGrid.ColumnsOrderedList[index + 1];

                                   
                    //                bvgCell.bvgGrid.ShouldSelectCell = Tuple.Create(bvgCell.bvgRow.ID, b.prop.Name);

                                   
                    //                bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.ThumbMove(b.ColWidth+20);

                    //            }

                    //        }
                    //    }
                        
                    //}
                    break;
                case MoveDirection.left:
                    //if (HasCtrl)
                    //{
                    //    if (!bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.IsOnMinPosition())
                    //    {
                    //        bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(0);
                    //    }

                    //    if (bvgCell.bvgColumn.ID >0)
                    //    {
                    //        BvgCell c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.ID == 0);

                    //        bvgCell.bvgGrid.SelectCell(c, true);
                    //    }
                    //}
                    //else
                    //{
                        
                    //        sn = bvgCell.bvgColumn.ID - 1;

                    //        if (bvgCell.bvgRow.Cells.Where(x => x.HasCol).Any(x => x.bvgColumn.ID == sn))
                    //        {
                    //            BvgCell c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.ID == sn);

                    //            bvgCell.bvgGrid.SelectCell(c, true);
                    //        }
                    //        else
                    //        {
                    //            if (!bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.IsOnMinPosition())
                    //            {

                    //               // bvgCell.bvgGrid.ShouldSelectCell = Tuple.Create(bvgCell.bvgRow.ID, (ushort)sn);

                    //            bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(0);
                    //            //bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar
                    //            //    .ThumbMove(-bvgCell.bvgGrid.bvgSettings.ColWidthMax);

                    //            //BvgCell c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.ID == sn);
                    //            //bvgCell.bvgGrid.SelectCell(c, true);
                    //        }
                    //        }
                        
                    //}
                    break;
                case MoveDirection.up:
                    if (HasCtrl)
                    {
                        if (!bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.IsOnMinPosition())
                        {
                            bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(0);
                        }

                        if (bvgCell.bvgRow.ID > 0)
                        {
                            BvgCell<TItem> c = bvgCell.bvgGrid.Rows.Single(x => x.ID == 0).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                            bvgCell.bvgGrid.SelectCell(c, true);
                        }

                    }
                    else
                    {
                        
                            sn = bvgCell.bvgRow.ID - 1;

                            if (bvgCell.bvgGrid.Rows.Any(x => x.ID == sn))
                            {
                                BvgCell<TItem> c = bvgCell.bvgGrid.Rows.Single(x => x.ID == sn).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                                bvgCell.bvgGrid.SelectCell(c, true);

                            }
                            else
                            {
                                if (!bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.IsOnMinPosition())
                                {
                                    bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar
                                        .ThumbMove(-bvgCell.bvgGrid.bvgSettings.RowHeight);
                                }
                            }
                        
                    }

                    break;
                case MoveDirection.down:
                    if (HasCtrl)
                    {
                        if (!bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.IsOnMaxPosition())
                        {
                            bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(bvgCell.bvgGrid.RowsTotalCount * bvgCell.bvgGrid.RowHeightAdjusted);
                        }

                        if (bvgCell.bvgRow.ID < bvgCell.bvgGrid.Rows.Max(x => x.ID))
                        {
                            BvgCell<TItem> c = bvgCell.bvgGrid.Rows.Single(x => x.ID == bvgCell.bvgGrid.Rows.Max(x2 => x2.ID)).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                            bvgCell.bvgGrid.SelectCell(c, true);
                        }
                    }
                    else
                    {

                        

                            sn = bvgCell.bvgRow.ID + 1;

                            if (bvgCell.bvgGrid.Rows.Any(x => x.ID == sn))
                            {
                                BvgCell<TItem> c = bvgCell.bvgGrid.Rows.Single(x => x.ID == sn).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                                bvgCell.bvgGrid.SelectCell(c, true);
                            }
                            else
                            {
                                if (!bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar.IsOnMaxPosition())
                                {
                                    bvgCell.bvgGrid.VericalScroll.compBlazorScrollbar
                                        .ThumbMove(bvgCell.bvgGrid.bvgSettings.RowHeight);

                                }
                            }
                        
                    }

                    break;
                default:
                    break;
            }



        }


        public void Dispose()
        {
         
        }
    }
}
