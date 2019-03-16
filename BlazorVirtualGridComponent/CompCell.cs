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
                    if (HasCtrl)
                    {
                        if (!bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.IsOnMaxPosition())
                        {

                            NavigationHelper<TItem>.SelectCell(true,bvgCell.bvgGrid.ColumnsOrderedListNonFrozen.Last().prop.Name, bvgCell.bvgRow.ID, bvgCell.bvgGrid);
                        }
                        else
                        {
                            if (!bvgCell.bvgGrid.ActiveCell.bvgColumn.prop.Name.Equals(bvgCell.bvgGrid.ColumnsOrderedListNonFrozen.Last().prop.Name))
                            {
                                NavigationHelper<TItem>.SelectCell(true,bvgCell.bvgGrid.ColumnsOrderedListNonFrozen.Last().prop.Name, bvgCell.bvgRow.ID, bvgCell.bvgGrid);
                            }
                        }
                    }
                    else
                    {
                        ColProp a = bvgCell.bvgGrid.ColumnsOrderedList.Single(x => x.prop.Name.Equals(bvgCell.bvgColumn.prop.Name));
                        int index = bvgCell.bvgGrid.ColumnsOrderedList.ToList().IndexOf(a);

                        if (index < bvgCell.bvgGrid.ColumnsOrderedList.Count() - 1)
                        {

                            if (bvgCell.bvgRow.Cells.Any(x => x.bvgColumn.prop.Name.Equals(bvgCell.bvgGrid.ColumnsOrderedList[index + 1].prop.Name)))
                            {
                                BvgCell<TItem> c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.prop.Name.Equals(bvgCell.bvgGrid.ColumnsOrderedList[index + 1].prop.Name));
                                bvgCell.bvgGrid.SelectCell(c, true);

                            }
                            else
                            {
                              NavigationHelper<TItem>.SelectCell(false,bvgCell.bvgGrid.ColumnsOrderedList[index + 1].prop.Name, bvgCell.bvgRow.ID, bvgCell.bvgGrid);
                            }
                        }
                       
                    }
                    break;
                case MoveDirection.left:
                    if (HasCtrl)
                    {
                        if (!bvgCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.IsOnMinPosition())
                        {
                            NavigationHelper<TItem>.SelectCell(true,bvgCell.bvgGrid.ColumnsOrderedList.First().prop.Name, bvgCell.bvgRow.ID, bvgCell.bvgGrid, 0);
                        }
                        else
                        {
                            if (!bvgCell.bvgGrid.ActiveCell.bvgColumn.prop.Name.Equals(bvgCell.bvgGrid.ColumnsOrderedList.First().prop.Name))
                            {
                                NavigationHelper<TItem>.SelectCell(true, bvgCell.bvgGrid.ColumnsOrderedList.First().prop.Name, bvgCell.bvgRow.ID, bvgCell.bvgGrid, 0);
                            }
                        }
                    }
                    else
                    {

                        ColProp a = bvgCell.bvgGrid.ColumnsOrderedList.Single(x => x.prop.Name.Equals(bvgCell.bvgColumn.prop.Name));
                        int index = bvgCell.bvgGrid.ColumnsOrderedList.ToList().IndexOf(a);

                        if (index > 0)
                        {

                            if (bvgCell.bvgRow.Cells.Any(x => x.bvgColumn.prop.Name.Equals(bvgCell.bvgGrid.ColumnsOrderedList[index-1].prop.Name)))
                            {
                                BvgCell<TItem> c = bvgCell.bvgRow.Cells.Single(x => x.bvgColumn.prop.Name.Equals(bvgCell.bvgGrid.ColumnsOrderedList[index - 1].prop.Name));

                                bvgCell.bvgGrid.SelectCell(c, true);
                            }
                            else
                            {
                                NavigationHelper<TItem>.SelectCell(true, bvgCell.bvgGrid.ColumnsOrderedList[index - 1].prop.Name, bvgCell.bvgRow.ID, bvgCell.bvgGrid);
                            }
                        }
                    }
                    break;
                case MoveDirection.up:
                    if (HasCtrl)
                    {
                        if (!bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar.IsOnMinPosition())
                        {
                            bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar.SetScrollPosition(0);
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
                                if (!bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar.IsOnMinPosition())
                                {
                                    bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar
                                        .ThumbMove(-bvgCell.bvgGrid.bvgSettings.RowHeight);
                                }
                            }
                        
                    }

                    break;
                case MoveDirection.down:
                    int MaxID = bvgCell.bvgGrid.Rows.Max(x => x.ID);
                    if (HasCtrl)
                    {
                        if (!bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar.IsOnMaxPosition())
                        {
                            bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar.SetScrollPosition(bvgCell.bvgGrid.RowsTotalCount * bvgCell.bvgGrid.RowHeightAdjusted);
                        }
                        
                        if (bvgCell.bvgRow.ID < MaxID-1)
                        {
                            BvgCell<TItem> c = bvgCell.bvgGrid.Rows.Single(x => x.ID == MaxID-1).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                            bvgCell.bvgGrid.SelectCell(c, true);
                        }
                    }
                    else
                    {

                            sn = bvgCell.bvgRow.ID + 1;

                            if (bvgCell.bvgGrid.Rows.Any(x => x.ID == sn) && sn!=MaxID)
                            {
                                BvgCell<TItem> c = bvgCell.bvgGrid.Rows.Single(x => x.ID == sn).Cells.Single(x => x.bvgColumn.ID == bvgCell.bvgColumn.ID);

                                bvgCell.bvgGrid.SelectCell(c, true);
                            }
                            else
                            {
                                if (!bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar.IsOnMaxPosition())
                                {
                                    bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar
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
