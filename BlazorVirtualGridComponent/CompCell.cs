using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent
{
    public class CompCell<TItem> : ComponentBase, IDisposable
    {


        [Parameter]
        protected BvgCell<TItem> bvgCell { get; set; }

        private PressState keyPressState = new PressState();


        //bool EnabledRender = true;


        //protected override Task OnParametersSetAsync()
        //{

        //    EnabledRender = true;

        //    return base.OnParametersSetAsync();
        //}

        protected override bool ShouldRender()
        {
            return false;
            //return EnabledRender;
        }


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
            builder.AddAttribute(k++, "class", bvgCell.CssClassFull);
            builder.AddAttribute(k++, "tabindex", 0); // without this div can't get focus and don't fires keyboard events
            //builder.AddAttribute(k++, "style", string.Concat("width:", bvgCell.bvgColumn.ColWidth, "px"));
            builder.AddAttribute(k++, "onclick", Clicked);
            builder.AddAttribute(k++, "onkeydown", EventCallback.Factory.Create<UIKeyboardEventArgs>(this, OnKeyDown));
            builder.AddAttribute(k++, "onkeyup", OnKeyUp);


            builder.OpenElement(k++, "input");
            
            builder.AddAttribute(k++, "id", string.Concat("chCell", bvgCell.ID));
            builder.AddAttribute(k++, "type", "checkbox");
            if (bvgCell.bvgColumn.prop.PropertyType.Equals(typeof(bool)))
            {
                if (!string.IsNullOrEmpty(bvgCell.Value))
                {
                    if (bvgCell.Value.ToLower() == "true")
                    {
                        builder.AddAttribute(k++, "checked", "true");
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

            if (e.Repeat && e.ShiftKey)
            {
                return;
            }


            string a = e.Key.ToLower();

            if (a.Contains("arrow"))
            {
                MoveDirection CurrDirection = StringToDirection(a.Replace("arrow", null));


                if (e.CtrlKey)
                {
                    SelectCornerCell(CurrDirection);
                    keyPressState = new PressState();
                }
                else if (e.ShiftKey)
                {

                    keyPressState = new PressState(CurrDirection, 99);
                    TimerHelper.OnTick = TimerOnTick;
                    TimerHelper.Start(1, 1);
                }
                else
                { 

                    if (e.Repeat)
                    {
                        //if (CurrDirection == MoveDirection.up || CurrDirection == MoveDirection.down)
                        //{
                            keyPressState = new PressState(CurrDirection, 99);
                            TimerHelper.OnTick = TimerOnTick2;
                            TimerHelper.Start(1, 1);
                        //}
                        //else
                        //{
                        //    SelectNeightbourCell(CurrDirection);
                        //}

                  
                        //if (keyPressState.Direction == MoveDirection.undefined)
                        //{
                        //    keyPressState = new PressState(CurrDirection, 0);
                        //}

                        //if (CurrDirection == keyPressState.Direction)
                        //{
                        //    keyPressState.Count++;


                        //    if (keyPressState.Count > 5)
                        //    {
                        //        SelectNeightbourCell(CurrDirection);
                        //        keyPressState.Count -= 5;
                        //    }

                        //}
                        //else
                        //{
                        //    keyPressState = new PressState(CurrDirection, 1);
                        //}
                    }
                }
            }
            else
            {
                switch (e.Key.ToLower())
                {
                    case "home":
                        SelectCornerCell(MoveDirection.up);
                        break;
                    case "end":
                        SelectCornerCell(MoveDirection.down);
                        break;
                    case "pageup":
                        bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar.ThumbMove(-(bvgCell.bvgGrid.DisplayedRowsCount-1)* bvgCell.bvgGrid.bvgSettings.RowHeight);
                        break;
                    case "pagedown":
                        bvgCell.bvgGrid.VerticalScroll.compBlazorScrollbar.ThumbMove((bvgCell.bvgGrid.DisplayedRowsCount - 1) * bvgCell.bvgGrid.bvgSettings.RowHeight);
                        break;
                    default:
                        break;
                }
            }
        }


       

        public void OnKeyUp(UIKeyboardEventArgs e)
        {
            if (keyPressState.Count == 99)
            {
                keyPressState = new PressState();
                TimerHelper.Stop();
                return;
            }

            string a = e.Key.ToLower();

            if (a.Contains("arrow"))
            {
                MoveDirection CurrDirection = StringToDirection(a.Replace("arrow", null));

               

                //if (keyPressState.Direction == MoveDirection.undefined)
                //{
                    if (!e.CtrlKey)
                    {
                        SelectNeightbourCell(CurrDirection);
                    }
                //}
                //else
                //{
                //    if (CurrDirection == keyPressState.Direction && keyPressState.Count > 0)
                //    {
                //        SelectNeightbourCell(CurrDirection);
                //    }
                //}
            }

            
        }


        public void SelectNeightbourCell(MoveDirection d)
        {

            //BlazorWindowHelper.BWHJsInterop.jsRuntime = BvgJsInterop.jsRuntime;
            //BlazorWindowHelper.BlazorTimeAnalyzer.Reset();
            //BlazorWindowHelper.BlazorTimeAnalyzer.Add("A1", MethodBase.GetCurrentMethod());

            int StepSize = 1;

            //After refresh from JS bvgCell for this component is old because we don't do component refresh
            //So we will depend Active cell data because it is always up to date.
            BvgCell<TItem> bvgActiveCell = bvgCell.bvgGrid.ActiveBvgCell;

            int sn = 0;
            ColProp a = new ColProp();
            int index = 0;

            switch (d)
            {
                case MoveDirection.right:
                    a = bvgActiveCell.bvgGrid.ColumnsOrderedList.Single(x => x.prop.Name.Equals(bvgActiveCell.bvgColumn.prop.Name));
                    index = bvgActiveCell.bvgGrid.ColumnsOrderedList.ToList().IndexOf(a);

                    if (index < bvgActiveCell.bvgGrid.ColumnsOrderedList.Count() - StepSize)
                    {

                        if (bvgActiveCell.bvgRow.Cells.Any(x => x.bvgColumn.prop.Name.Equals(bvgActiveCell.bvgGrid.ColumnsOrderedList[index + StepSize].prop.Name)))
                        {
                            BvgCell<TItem> c = bvgActiveCell.bvgRow.Cells.Single(x => x.bvgColumn.prop.Name.Equals(bvgActiveCell.bvgGrid.ColumnsOrderedList[index + StepSize].prop.Name));
                            bvgActiveCell.bvgGrid.SelectCell(c, true);

                        }
                        else
                        {
                            NavigationHelper<TItem>.SelectAndScrollIntoViewHorizontal(false, bvgActiveCell.bvgGrid.ColumnsOrderedList[index + StepSize].prop.Name, bvgActiveCell.bvgRow.IndexInSource, bvgActiveCell.bvgGrid);
                        }
                    }
                    break;
                case MoveDirection.left:

                    a = bvgActiveCell.bvgGrid.ColumnsOrderedList.Single(x => x.prop.Name.Equals(bvgActiveCell.bvgColumn.prop.Name));
                    index = bvgActiveCell.bvgGrid.ColumnsOrderedList.ToList().IndexOf(a);

                    if (index > 0)
                    {

                        if (bvgActiveCell.bvgRow.Cells.Any(x => x.bvgColumn.prop.Name.Equals(bvgActiveCell.bvgGrid.ColumnsOrderedList[index - StepSize].prop.Name)))
                        {
                            BvgCell<TItem> c = bvgActiveCell.bvgRow.Cells.Single(x => x.bvgColumn.prop.Name.Equals(bvgActiveCell.bvgGrid.ColumnsOrderedList[index - StepSize].prop.Name));

                            bvgActiveCell.bvgGrid.SelectCell(c, true);
                        }
                        else
                        {
                            NavigationHelper<TItem>.SelectAndScrollIntoViewHorizontal(true, bvgActiveCell.bvgGrid.ColumnsOrderedList[index - StepSize].prop.Name, bvgActiveCell.bvgRow.IndexInSource, bvgActiveCell.bvgGrid);
                        }
                    }

                    break;
                case MoveDirection.up:
                    sn = bvgActiveCell.bvgRow.ID - StepSize;

                    if (bvgActiveCell.bvgGrid.Rows.Any(x => x.ID == sn))
                    {
                        BvgCell<TItem> c = bvgActiveCell.bvgGrid.Rows.Single(x => x.ID == sn).Cells.Single(x => x.bvgColumn.ID == bvgActiveCell.bvgColumn.ID);

                        bvgActiveCell.bvgGrid.SelectCell(c, true);

                    }
                    else
                    {
                        if (bvgActiveCell.bvgRow.IndexInSource>1)
                        {
                            if (bvgActiveCell.bvgRow.IndexInSource > StepSize)
                            {
                                NavigationHelper<TItem>.ScrollIntoViewVertical(true, (ushort)(bvgActiveCell.bvgRow.IndexInSource - StepSize), bvgActiveCell.bvgColumn.prop.Name, bvgActiveCell.bvgGrid);
                            }
                        }
                    }
                    break;
                case MoveDirection.down:
                    int MaxID = bvgActiveCell.bvgGrid.Rows.Max(x => x.ID);
                    sn = bvgActiveCell.bvgRow.ID + StepSize;

                    if (bvgActiveCell.bvgGrid.Rows.Any(x => x.ID == sn) && sn < MaxID-1)
                    {
                        BvgCell<TItem> c = bvgActiveCell.bvgGrid.Rows.Single(x => x.ID == sn).Cells.Single(x => x.bvgColumn.ID == bvgActiveCell.bvgColumn.ID);

                        bvgActiveCell.bvgGrid.SelectCell(c, true);
                    }
                    else
                    {
                        if (bvgActiveCell.bvgRow.IndexInSource < bvgCell.bvgGrid.RowsTotalCount)
                        {
                           
                            int tmpindex = bvgCell.bvgGrid.ActiveBvgCell.bvgRow.IndexInSource;
              
                            NavigationHelper<TItem>.ScrollIntoViewVertical(false, (ushort)(bvgActiveCell.bvgRow.IndexInSource + 1), bvgActiveCell.bvgColumn.prop.Name, bvgActiveCell.bvgGrid);
                     

                            if (tmpindex == bvgCell.bvgGrid.ActiveBvgCell.bvgRow.IndexInSource)
                            {
                           
                                NavigationHelper<TItem>.ScrollIntoViewVertical(false, (ushort)(bvgActiveCell.bvgRow.IndexInSource + 2), bvgActiveCell.bvgColumn.prop.Name, bvgActiveCell.bvgGrid);
                            }

                          
                            if (tmpindex == bvgCell.bvgGrid.ActiveBvgCell.bvgRow.IndexInSource)
                            {
                                SelectCornerCell(d);
                            }
                 
                        }
                    }
                    break;
                default:
                    break;
            }




           // BlazorWindowHelper.BlazorTimeAnalyzer.Log();
        }


        public void TimerOnTick()
        {
          
           SelectNeightbourCell(keyPressState.Direction);
            
        }

        public void TimerOnTick2()
        {

            SelectNeightbourCell(keyPressState.Direction);
            TimerHelper.Stop();
        }


        public void SelectCornerCell(MoveDirection d)
        {
            BvgCell<TItem> bvgActiveCell = bvgCell.bvgGrid.ActiveBvgCell;

            switch (d)
            {
                case MoveDirection.right:
                    if (!bvgActiveCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.IsOnMaxPosition())
                    {
                        NavigationHelper<TItem>.SelectAndScrollIntoViewHorizontal(true, bvgActiveCell.bvgGrid.ColumnsOrderedListNonFrozen.Last().prop.Name, bvgActiveCell.bvgRow.IndexInSource, bvgActiveCell.bvgGrid);
                    }
                    else
                    {
                        if (!bvgActiveCell.bvgColumn.prop.Name.Equals(bvgActiveCell.bvgGrid.ColumnsOrderedListNonFrozen.Last().prop.Name))
                        {
                            NavigationHelper<TItem>.SelectAndScrollIntoViewHorizontal(true, bvgActiveCell.bvgGrid.ColumnsOrderedListNonFrozen.Last().prop.Name, bvgActiveCell.bvgRow.IndexInSource, bvgActiveCell.bvgGrid);
                        }
                    }
                    break;
                case MoveDirection.left:
                    if (!bvgActiveCell.bvgGrid.HorizontalScroll.compBlazorScrollbar.IsOnMinPosition())
                    {
                        NavigationHelper<TItem>.SelectAndScrollIntoViewHorizontal(true, bvgActiveCell.bvgGrid.ColumnsOrderedList.First().prop.Name, bvgActiveCell.bvgRow.IndexInSource, bvgActiveCell.bvgGrid, 0);
                    }
                    else
                    {
                        if (!bvgActiveCell.bvgColumn.prop.Name.Equals(bvgActiveCell.bvgGrid.ColumnsOrderedList.First().prop.Name))
                        {
                            NavigationHelper<TItem>.SelectAndScrollIntoViewHorizontal(true, bvgActiveCell.bvgGrid.ColumnsOrderedList.First().prop.Name, bvgActiveCell.bvgRow.IndexInSource, bvgActiveCell.bvgGrid, 0);
                        }
                    }
                    break;
                case MoveDirection.up:
                    if (bvgActiveCell.bvgRow.IndexInSource > 1)
                    {
                        bvgActiveCell.bvgGrid.VerticalScroll.compBlazorScrollbar.SetScrollPosition(0);
                    }

                    if (bvgActiveCell.bvgRow.IndexInSource > 1)
                    {
                        BvgCell<TItem> c = bvgActiveCell.bvgGrid.Rows.Single(x => x.IndexInSource == 1).Cells.Single(x => x.bvgColumn.ID == bvgActiveCell.bvgColumn.ID);

                        bvgActiveCell.bvgGrid.SelectCell(c, true);
                    }
                    break;
                case MoveDirection.down:
                    int MaxID = bvgActiveCell.bvgGrid.Rows.Max(x => x.ID);

                    if (bvgActiveCell.bvgRow.IndexInSource < bvgActiveCell.bvgGrid.RowsTotalCount)
                    {
                        bvgActiveCell.bvgGrid.VerticalScroll.compBlazorScrollbar.SetScrollPosition(bvgActiveCell.bvgGrid.RowsTotalCount * bvgActiveCell.bvgGrid.bvgSettings.RowHeight);
                    }

                    if (bvgActiveCell.bvgRow.ID < MaxID - 1)
                    {
                        BvgCell<TItem> c = bvgActiveCell.bvgGrid.Rows.Single(x => x.ID == MaxID - 1).Cells.Single(x => x.bvgColumn.ID == bvgActiveCell.bvgColumn.ID);

                        bvgActiveCell.bvgGrid.SelectCell(c, true);
                    }
                    break;
                default:
                    break;
            }
        }


        public void Dispose()
        {
         
        }


        private class PressState
        {
            public MoveDirection Direction { get; set; } = MoveDirection.undefined;
            public int Count { get; set; } = 0;

            public PressState()
            {
            }

            public PressState(MoveDirection direction, int count)
            {
                Direction = direction;
                Count = count; 
            }
        }
    }
}
