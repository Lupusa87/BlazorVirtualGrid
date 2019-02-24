﻿using BlazorVirtualGridComponent.businessLayer;
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
    public class CompBlazorVirtualGrid_Logic<TItem>: ComponentBase
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


        public bool ActualRender { get; set; } =false;


        Timer timer1;

        public bool ForceStateHasChaned { get; set; } = false;


        private bool FirstLoad = true;

        private int LastVerticalSkip = -1;

        private int LastHorizontalSkip = -1;



        protected override void OnParametersSet()
        {
            //BlazorWindowHelper.BlazorTimeAnalyzer.LogAllAdd = true;

            bvgGrid = new BvgGrid
            {
                IsReady = true,
                Name = TableName,
                RowsTotalCount = SourceList.Count(),
                bvgSettings = bvgSettings,
                AllProps = typeof(TItem).GetProperties(BindingFlags.Public | BindingFlags.Instance),
                
            };


            bvgGrid.ColumnsOrderedList = ColumnsHelper.GetColumnsOrderedList(bvgGrid);
            bvgGrid.ColumnsOrderedListFrozen = bvgGrid.ColumnsOrderedList.Where(x=>x.IsFrozen).ToArray();
            bvgGrid.ColumnsOrderedListNonFrozen = bvgGrid.ColumnsOrderedList.Where(x => x.IsFrozen==false).ToArray();


            ColProp[] c = bvgGrid.ColumnsOrderedList.Where(x => x.IsFrozen == false).ToArray();

            bvgGrid.NonFrozenColwidthSumsByElement = new int[c.Count()];
            int j = 0;
            for (int i = 0; i < c.Count(); i++)
            {
                j += c[i].ColWidth;
                bvgGrid.NonFrozenColwidthSumsByElement[i] = j;
               
            }

            c = null;


            SortedRowsList = SourceList.ToArray();
          
            base.OnParametersSet();
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

            


            Console.WriteLine("OnAfterRender main");
        }

        public void OnVerticalScroll(int p)
        {

            RenderGridRows(p, true);

        }


        public void SetScrollLeftToNonFrozenColumnsDiv(int p)
        {

            int p2 = p;
            if (LastHorizontalSkip > 0)
            {
                p2 -= bvgGrid.ColumnsOrderedListNonFrozen.Take(LastHorizontalSkip).Sum(x => x.ColWidth);
            }
        
            BvgJsInterop.SetElementScrollLeft(bvgGrid.GridDivElementID, p2);
        }

        public void OnHorizontalScroll(int p)
        {
            RenderGridColumns(p, true);

            SetScrollLeftToNonFrozenColumnsDiv(p);

            //BlazorWindowHelper.BlazorTimeAnalyzer.Log();

            
        }

        public void SortGrid(string s)
        {

            SortedRowsList = GenericAdapter<TItem>.GetSortedRowsList(SourceList.AsQueryable(), s).ToArray();

            bvgGrid.CurrVerticalScrollPosition = 0;
            LastVerticalSkip = -1;
            bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(0);
            
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


                if (UpdateUI)
                {
                    bvgGrid.bvgAreaRowsNonFrozen.InvokePropertyChanged();
                }
            }

        }

        public int MeasureColumnsCount(int skip)
        {
            int a = bvgGrid.NonFrozenColwidthSumsByElement[skip];
            

            if (bvgGrid.NonFrozenColwidthSumsByElement.Any(x => x - a > bvgGrid.NotFrozenTableWidth))
            {
                int i = bvgGrid.NonFrozenColwidthSumsByElement.Where(x => x - a <= bvgGrid.NotFrozenTableWidth).Count();
                return i + 1 - skip;
            }

            return bvgGrid.NonFrozenColwidthSumsByElement.Count();
        }


        public int GetSkipedColumns(int scrollPosition)
        {

            if (bvgGrid.NonFrozenColwidthSumsByElement.Any(x => x <= scrollPosition))
            { 
                return bvgGrid.NonFrozenColwidthSumsByElement.Where(x => x <= scrollPosition).Count();
            }

            return 0;
        }

        public void RenderGridColumns(int Scrollposition, bool UpdateUI)
        {

          
            int skip = Scrollposition==0 ? 0 : GetSkipedColumns(Scrollposition);

            int take = bvgGrid.DisplayedColumnsCount;


            if (skip != LastHorizontalSkip)
            {
                take = MeasureColumnsCount(skip);
            }


            if (skip != LastHorizontalSkip || take != bvgGrid.DisplayedColumnsCount)
            {
                //BlazorWindowHelper.BlazorTimeAnalyzer.Reset();
                //BlazorWindowHelper.BlazorTimeAnalyzer.Add("prepare cols", MethodBase.GetCurrentMethod());




                LastHorizontalSkip = skip;
                bvgGrid.DisplayedColumnsCount = take;

               
                IEnumerable<ColProp> ActiveRegularProps;
                if (skip > 0)
                {
                    ActiveRegularProps = bvgGrid.ColumnsOrderedListNonFrozen.Skip(skip).Take(take);
                }
                else
                {
                    ActiveRegularProps = bvgGrid.ColumnsOrderedListNonFrozen.Take(take);
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
                GenericAdapter<TItem>.GetColumns(ListProps, bvgGrid, SortedRowsListActual);

               


                if (UpdateUI)
                {

                    //BlazorWindowHelper.BlazorTimeAnalyzer.Add("update cols", MethodBase.GetCurrentMethod());
                    bvgGrid.bvgAreaColumnsNonFrozen.InvokePropertyChanged();


                    //BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows", MethodBase.GetCurrentMethod());


                    
                    bvgGrid.bvgAreaRowsNonFrozen.InvokePropertyChanged();

                    //BlazorWindowHelper.BlazorTimeAnalyzer.Add("update rows done", MethodBase.GetCurrentMethod());


                }

                //BlazorWindowHelper.BlazorTimeAnalyzer.Log();

            }

        }

        public void Timer1Callback(object o)
        {
            GetActualWidthAndHeight();
            timer1.Dispose();
        }

       

        public async void GetActualWidthAndHeight()
        {

            bvgGrid.totalWidth = await BvgJsInterop.GetElementActualWidth(bvgGrid.GridTableElementID)-20;


            double top = await BvgJsInterop.GetElementActualTop(bvgGrid.GridTableElementID);
            double windowHeight = await BvgJsInterop.GetWindowHeight();

            bvgGrid.height = windowHeight - top - 40;


            bvgGrid.AdjustSize();

            
            RenderGridColumns(0, false);
            RenderGridRows(0, false);
         
            ActualRender = true;
            StateHasChanged();

        }


        public void Refresh()
        {

            bvgGrid.InvokePropertyChanged();

            StateHasChanged();
        }



       
    }
}
