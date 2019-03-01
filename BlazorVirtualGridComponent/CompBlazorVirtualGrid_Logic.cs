using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompBlazorVirtualGrid_Logic<TItem> : ComponentBase
    {

        [Parameter]
        protected IList<TItem> SourceList { get; set; }

        [Parameter]
        protected string TableName { get; set; }

        [Parameter]
        protected BvgSettings bvgSettings { get; set; } = new BvgSettings();


        protected TItem[] SortedRowsList { get; set; }
        protected TItem[] SortedRowsListActual { get; set; }

        public BvgGrid bvgGrid { get; set; }


        public bool ActualRender { get; set; } = false;


        Timer timer1;

       
        public bool ForceStateHasChaned { get; set; } = false;


        private bool FirstLoad = true;

        private int LastVerticalSkip = -1;

        private int LastHorizontalSkip = -1;

        private double LastHorizontalScrollPosition = 0;

        //bool EnabledRender = true;

        //protected override bool ShouldRender()
        //{
        //    return EnabledRender;
        //}

        protected override Task OnParametersSetAsync()
        {
            //BlazorWindowHelper.BlazorTimeAnalyzer.LogAllAdd = true;

            bvgGrid = new BvgGrid
            {
                IsReady = true,
                Name = TableName,
                RowsTotalCount = SourceList.Count(),
                bvgSettings = bvgSettings,
                AllProps = typeof(TItem).GetProperties(BindingFlags.Public | BindingFlags.Instance),
                SortState = Tuple.Create(false, new BvgColumn()),
            };


            bvgGrid.ColumnsOrderedList = ColumnsHelper.GetColumnsOrderedList(bvgGrid);
            bvgGrid.ColumnsOrderedListFrozen = bvgGrid.ColumnsOrderedList.Where(x => x.IsFrozen).ToArray();
            bvgGrid.ColumnsOrderedListNonFrozen = bvgGrid.ColumnsOrderedList.Where(x => x.IsFrozen == false).ToArray();


            bvgGrid.UpdateNonFrozenColwidthSumsByElement();


            SortedRowsList = SourceList.ToArray();

            return base.OnParametersSetAsync();
        }



        protected override void OnAfterRender()
        {
            base.OnAfterRender();
            if (FirstLoad)
            {
                FirstLoad = false;


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

        
                timer1 = new Timer(Timer1Callback, null, 1, 1);
                
            }


            //EnabledRender = false;
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
            Console.WriteLine("ScrollPosition="+p);

            RenderGridColumns(p, true);

           
            //if (bvgGrid.ShouldSelectCell == null)
            //{
               
                SetScrollLeftToNonFrozenColumnsDiv(p);

            //}
            //else
            //{
            //    BvgJsInterop.SetElementScrollLeft("NonFrozenDiv1", 10000);

            //    Console.WriteLine(bvgGrid.ShouldSelectCell.Item1 + " " + bvgGrid.ShouldSelectCell.Item2);
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


                if (bvgGrid.SortState.Item1)
                {
                  
                    if (bvgGrid.SortState.Item2.IsFrozen==false)
                    {
                       
                        bvgGrid.bvgAreaColumnsNonFrozen.InvokePropertyChanged();
                    }
                }
            }

        }

        public void Timer1Callback(object o)
        {
            Console.WriteLine("A2");
            GetActualWidthAndHeight();
            timer1.Dispose();
        }



        public async void GetActualWidthAndHeight()
        {

            bvgGrid.totalWidth = await BvgJsInterop.GetElementActualWidth(bvgGrid.GridTableElementID)-20;


            double top = await BvgJsInterop.GetElementActualTop(bvgGrid.GridTableElementID);

            double windowHeight = await BvgJsInterop.GetWindowHeight();

            bvgGrid.height = windowHeight - top - 40;

            if (bvgGrid.height>bvgGrid.RowsTotalCount * bvgGrid.bvgSettings.RowHeight + bvgGrid.bvgSettings.HeaderHeight)
            {
                bvgGrid.height = bvgGrid.RowsTotalCount * bvgGrid.bvgSettings.RowHeight + bvgGrid.bvgSettings.HeaderHeight;
            }

           
            bvgGrid.AdjustSize();

            
            RenderGridColumns(0, false);
            RenderGridRows(0, false);
         
            ActualRender = true;

            //EnabledRender = true;
     

            StateHasChanged();

           
            //EnabledRender = false;
        }


        public void Refresh()
        {

            bvgGrid.InvokePropertyChanged();

            StateHasChanged();
        }



       
    }
}
