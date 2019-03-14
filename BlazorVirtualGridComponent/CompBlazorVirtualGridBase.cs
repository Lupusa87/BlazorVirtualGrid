using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using BlazorVirtualGridComponent.ExternalSettings;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent
{
    public class CompBlazorVirtualGridBase<TItem> : ComponentBase
    {

        [Inject]
        private IJSRuntime jsRuntimeCurrent { get; set; }

        [Parameter]
        protected IList<TItem> SourceList { get; set; }

        [Parameter]
        protected string TableName { get; set; }

        [Parameter]
        protected BvgSettings<TItem> bvgSettings { get; set; } = new BvgSettings<TItem>();


        protected TItem[] SortedRowsList { get; set; }
        protected TItem[] SortedRowsListActual { get; set; }

        public BvgGrid<TItem> bvgGrid { get; set; }

        private BvgGridTransferableState<TItem> bvgGridTransferableState { get; set; }

        public bool ActualRender { get; set; } = false;


        Timer timer1;


        private bool FirstLoad = true;

        private int LastVerticalSkip = -1;

        private int LastHorizontalSkip = -1;

        private double LastHorizontalScrollPosition = 0;

        //bool EnabledRender = true;

        //protected override bool ShouldRender()
        //{
        //    return EnabledRender;
        //}


        protected override void OnInit()
        {

            BvgJsInterop.jsRuntime = jsRuntimeCurrent;
            JsEventReceiver.Initialize();
            JsEventReceiver.OnResize = OnResize;
            base.OnInit();
        }

        private void OnResize()
        {
            bvgGrid.HasMeasuredRect = false;
            Refresh(true);
        }


        protected override void OnParametersSet()
        {
  
            if (bvgGrid != null)
            {
                if (bvgGrid.bvgModal.IsDisplayed)
                {
                    return;
                }
            }

            Reset();
          
            base.OnParametersSetAsync();
        }

        private void Reset()
        {
            FirstLoad = true;
            ActualRender = true;

            LastVerticalSkip = -1;

            LastHorizontalSkip = -1;

            LastHorizontalScrollPosition = 0;



         
            
            if (bvgGrid != null)
            {
                bvgGridTransferableState = new BvgGridTransferableState<TItem>(bvgGrid);
            }
            else
            {
                bvgGridTransferableState = new BvgGridTransferableState<TItem>();
            }

            bvgGrid = new BvgGrid<TItem>
            {
                IsReady = true,
                Name = TableName,
                RowsTotalCount = SourceList.Count(),
                bvgSettings = bvgSettings,
                AllProps = typeof(TItem).GetProperties(BindingFlags.Public | BindingFlags.Instance),
                HasMeasuredRect = bvgGridTransferableState.HasMeasuredRect,
                bvgSize = bvgGridTransferableState.bvgSize,
            };

            bvgGrid.bvgModal.bvgGrid = bvgGrid;



            if (bvgGridTransferableState.ContaintState)
            {
                
                bvgGrid.ColumnsOrderedList = bvgGridTransferableState.ColumnsOrderedList;
                bvgGrid.ColumnsOrderedListFrozen = bvgGridTransferableState.ColumnsOrderedListFrozen;
                bvgGrid.ColumnsOrderedListNonFrozen = bvgGridTransferableState.ColumnsOrderedListNonFrozen;
                //Console.WriteLine(bvgGrid.ColumnsOrderedList.Count());
                //Console.WriteLine(bvgGrid.ColumnsOrderedListFrozen.Count());
                //Console.WriteLine(bvgGrid.ColumnsOrderedListNonFrozen.Count());


                bvgGrid.cssHelper = bvgGridTransferableState.cssHelper;
            }
            else
            {
                bvgGrid.ColumnsOrderedList = ColumnsHelper<TItem>.GetColumnsOrderedList(bvgGrid);
                bvgGrid.ColumnsOrderedListFrozen = bvgGrid.ColumnsOrderedList.Where(x => x.IsFrozen).ToArray();
                bvgGrid.ColumnsOrderedListNonFrozen = bvgGrid.ColumnsOrderedList.Where(x => x.IsFrozen == false).ToArray();

            }


            bvgGrid.UpdateNonFrozenColwidthSumsByElement();

            if (bvgSettings.SortedColumn.Item1)
            {
                bvgGrid.SortState = Tuple.Create(bvgSettings.SortedColumn.Item1, bvgSettings.SortedColumn.Item2, bvgSettings.SortedColumn.Item3);


                if (bvgSettings.SortedColumn.Item3)
                {
                    SortedRowsList = GenericAdapter<TItem>.GetSortedRowsList(SourceList.AsQueryable(), bvgSettings.SortedColumn.Item2).ToArray();
                }
                else
                {
                    SortedRowsList = GenericAdapter<TItem>.GetSortedRowsList(SourceList.AsQueryable(), bvgSettings.SortedColumn.Item2 + " desc").ToArray();
                }
            }
            else
            {
                bvgGrid.SortState = Tuple.Create(false, string.Empty, false);

                SortedRowsList = SourceList.ToArray();
            }


        }

        protected override void OnAfterRender()
        {

            base.OnAfterRender();

            if (FirstLoad)
            {
                FirstLoad = false;


                bvgGridSubscribe();

                timer1 = new Timer(Timer1Callback, null, 1, 1);
                
            }


            //EnabledRender = false;
        }


        public void bvgGridSubscribe()
        {

            if (bvgGrid.compBlazorVirtualGrid == null)
            {
                bvgGrid.compBlazorVirtualGrid = this as CompBlazorVirtualGrid<TItem>;
            }

            if (bvgGrid.OnSort == null)
            {
                bvgGrid.OnSort = SortGrid;
            }

            if (bvgGrid.OnColumnResize == null)
            {
                bvgGrid.OnColumnResize = OnColumnResize;
            }


            if (bvgGrid.OnVerticalScroll == null)
            {
                bvgGrid.OnVerticalScroll = OnVerticalScroll;
            }

            if (bvgGrid.OnHorizontalScroll == null)
            {
                bvgGrid.OnHorizontalScroll = OnHorizontalScroll;
            }
        }

        public void OnColumnResize()
        {
            RenderGridColumns(LastHorizontalScrollPosition, true, true);
        }


        public void OnVerticalScroll(double p)
        {
            RenderGridRows((int)p, true);

        }


        public void SetScrollLeftToNonFrozenColumnsDiv(double p)
        {

            double p2 = p;
            if (LastHorizontalSkip > 0)
            {
                p2 -= bvgGrid.ColumnsOrderedListNonFrozen.Take(LastHorizontalSkip).Sum(x => x.ColWidth);
            }
        
            BvgJsInterop.SetElementScrollLeft("NonFrozenDiv1", p2);
        }

        public void OnHorizontalScroll(double p)
        {

            RenderGridColumns(p, true);

           
            //if (bvgGrid.ShouldSelectCell == null)
            //{
               
                SetScrollLeftToNonFrozenColumnsDiv(p);

            //}
            //else
            //{
            //    BvgJsInterop.SetElementScrollLeft("NonFrozenDiv1", 10000);

       
            //    bvgGrid.SelectCell(bvgGrid.Rows.Single(x=>x.ID == bvgGrid.ShouldSelectCell.Item1).Cells.Single(x=>x.bvgColumn.prop.Name.Equals(bvgGrid.ShouldSelectCell.Item2)), true);
            //    bvgGrid.ShouldSelectCell = null;

               
            //}

            //BlazorWindowHelper.BlazorTimeAnalyzer.Log();

            
        }

        public void SortGrid(string s)
        {

            SortedRowsList = GenericAdapter<TItem>.GetSortedRowsList(SourceList.AsQueryable(), s).ToArray();

            bvgGrid.CurrVerticalScrollPosition = 0;
            LastVerticalSkip = -1;
            bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(0);


            bvgGrid.bvgAreaColumnsFrozen.InvokePropertyChanged();
            bvgGrid.bvgAreaColumnsNonFrozen.InvokePropertyChanged();

            RenderGridRows(0, true);
        }

        public void RenderGridRows(int skip, bool UpdateUI)
        {

            if (skip != LastVerticalSkip)
            {

                LastVerticalSkip = skip;


                if (skip > 0)
                {
                    SortedRowsListActual = SortedRowsList.Skip(skip).Take(bvgGrid.DisplayedRowsCount).ToArray();
                    GenericAdapter<TItem>.GetRows(SortedRowsListActual, bvgGrid);
                    
                }
                else
                {
                    SortedRowsListActual = SortedRowsList.Take(bvgGrid.DisplayedRowsCount).ToArray();
                    GenericAdapter<TItem>.GetRows(SortedRowsListActual, bvgGrid);
                }

            }

        }



        public int GetSkipedColumns(double scrollPosition)
        {

            if (bvgGrid.NonFrozenColwidthSumsByElement.Any(x => x <= scrollPosition))
            { 
                return bvgGrid.NonFrozenColwidthSumsByElement.Where(x => x <= scrollPosition).Count();
            }

            return 0;
        }

        public void RenderGridColumns(double Scrollposition, bool UpdateUI, bool RequestedFromResize=false)
        {
            LastHorizontalScrollPosition = Scrollposition;


            int skip = Scrollposition==0 ? 0 : GetSkipedColumns(Scrollposition);


            if (skip != LastHorizontalSkip || RequestedFromResize)
            {
                
                //BlazorWindowHelper.BlazorTimeAnalyzer.Reset();
                //BlazorWindowHelper.BlazorTimeAnalyzer.Add("prepare cols", MethodBase.GetCurrentMethod());

                LastHorizontalSkip = skip;
               
                IEnumerable<ColProp> ActiveRegularProps;
                if (skip > 0)
                {
                    ActiveRegularProps = bvgGrid.ColumnsOrderedListNonFrozen.Skip(skip).Take(bvgGrid.DisplayedColumnsCount);
                }
                else
                {
                    ActiveRegularProps = bvgGrid.ColumnsOrderedListNonFrozen.Take(bvgGrid.DisplayedColumnsCount);
                }

               

                bvgGrid.ActiveProps = new PropertyInfo[bvgGrid.ColumnsOrderedListFrozen.Count() + ActiveRegularProps.Count()];

                ColProp[] ListProps = new ColProp[bvgGrid.ActiveProps.Count()];

                int j = 0;

                for (int i = 0; i < bvgGrid.ColumnsOrderedListFrozen.Count(); i++)
                {
                    bvgGrid.ActiveProps[j] = bvgGrid.ColumnsOrderedListFrozen[i].prop;
                    ListProps[j] = bvgGrid.ColumnsOrderedListFrozen[i];
                    j++;
                }
               
                foreach (var item in ActiveRegularProps)
                {
                    bvgGrid.ActiveProps[j] = item.prop;
                    ListProps[j] = item;
                    j++;
                }


                //BlazorWindowHelper.BlazorTimeAnalyzer.Add("get columns", MethodBase.GetCurrentMethod());
                GenericAdapter<TItem>.GetColumns(ListProps, bvgGrid, SortedRowsListActual, UpdateUI);

                //BlazorWindowHelper.BlazorTimeAnalyzer.Log();

                Console.WriteLine("B1");
       
                bvgGrid.cssHelper.UpdateStyle2();

                Console.WriteLine("B2");
                if (bvgGrid.SortState.Item1)
                {
                    if (bvgGrid.Columns.Any(x => x.prop.Name.Equals(bvgGrid.SortState.Item2, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        if (bvgGrid.Columns.Single(x => x.prop.Name.Equals(bvgGrid.SortState.Item2, StringComparison.InvariantCultureIgnoreCase)).IsFrozen == false)
                        {

                            bvgGrid.bvgAreaColumnsNonFrozen.InvokePropertyChanged();
                        }
                    }
                }

            }

        }

        public void Timer1Callback(object o)
        {
          
            GetActualWidthAndHeight();
            timer1.Dispose();
        }



        public async void GetActualWidthAndHeight()
        {
            if (!bvgGrid.HasMeasuredRect)
            {
                bvgGrid.bvgSize.W = await BvgJsInterop.GetElementActualWidth(bvgGrid.DivContainerElementID);

                double top = await BvgJsInterop.GetElementActualTop(bvgGrid.DivContainerElementID);

                double windowHeight = await BvgJsInterop.GetWindowHeight();

                bvgGrid.bvgSize.H = windowHeight - top - 30;
                bvgGrid.HasMeasuredRect = true;


                if (bvgGrid.bvgSettings.LayoutFixedOrAuto)
                {

                    if (bvgGrid.bvgSettings.CompWidth < bvgGrid.bvgSize.W)
                    {
                        bvgGrid.bvgSize.W = bvgGrid.bvgSettings.CompWidth;
                    }
                    if (bvgGrid.bvgSettings.CompHeight < bvgGrid.bvgSize.H)
                    {
                        bvgGrid.bvgSize.H = bvgGrid.bvgSettings.CompHeight;
                    }
                }
            }

  


            if (bvgGrid.bvgSize.H > bvgGrid.RowsTotalCount * bvgGrid.bvgSettings.RowHeight + bvgGrid.bvgSettings.HeaderHeight)
            {
                bvgGrid.bvgSize.H = bvgGrid.RowsTotalCount * bvgGrid.bvgSettings.RowHeight + bvgGrid.bvgSettings.HeaderHeight;
            }

            Console.WriteLine("C1");
            CheckSizeLimits();
            Console.WriteLine("C2");
            bvgGrid.AdjustSize();

            Console.WriteLine("C3");
            RenderGridColumns(0, false);
            Console.WriteLine("C4");
            RenderGridRows(0, false);
            Console.WriteLine("C5");
            ActualRender = true;

            //EnabledRender = true;
     

            StateHasChanged();


            //EnabledRender = false;
        }


        public void CheckSizeLimits()
        {


            double minHeight = bvgGrid.bvgSettings.RowHeight + bvgGrid.bvgSettings.HeaderHeight+ bvgGrid.bvgSettings.ScrollSize+10;
            if (minHeight < 50)
            {
                minHeight = 50;
            }
            if (bvgGrid.bvgSize.H < minHeight)
            {
                bvgGrid.bvgSize.H = minHeight;
            }


            double minWidth = bvgGrid.ColumnsOrderedListFrozen.Sum(x=>x.ColWidth) + bvgGrid.bvgSettings.ScrollSize + 10;
            if (minWidth<50)
            {
                minWidth = 50;
            }
            if (bvgGrid.bvgSize.W < minWidth)
            {
                bvgGrid.bvgSize.W = minWidth;
            }
            

        }



        public void Refresh(bool FullReload)
        {
            // full reload is need when window size changes, full reload recalculates available space


            Reset();

            if (FullReload)
            {
                StateHasChanged();
            }
            else
            {
                bvgGridSubscribe();
                GetActualWidthAndHeight();
            }
           
        }


        public void ShowColumnsManager()
        {
            bvgGrid.bvgModal.Show(ModalForm.ColumnsManager);
        }

        public void ShowStyleDesigner()
        {
            bvgGrid.bvgModal.Show(ModalForm.StyleDesigner);
        }

    }
}
