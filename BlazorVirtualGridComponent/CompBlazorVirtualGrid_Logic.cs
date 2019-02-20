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
    public class CompBlazorVirtualGrid_Logic<TItem>: ComponentBase
    {

        [Parameter]
        protected IList<TItem> SourceList { get; set; }

        [Parameter]
        protected string TableName { get; set; }

        [Parameter]
        protected BvgSettings bvgSettings { get; set; } = new BvgSettings();



        public BvgGrid bvgGrid { get; set; }


        protected TItem[] SortedList { get; set; }


        public bool ActualRender { get; set; } =false;


        public bool NeedsRefreshFromDotNet { get; set; } = true;

        

        Timer timer1;

        public bool ForceStateHasChaned { get; set; } = false;


        private bool FirstLoad = true;

        private int LastSkip = -1;

        protected override void OnParametersSet()
        {
       
            bvgGrid = new BvgGrid
            {
                IsReady = true,
                Name = TableName,
                RowsTotalCount = SourceList.Count(),
                bvgSettings = bvgSettings,
            };

            GenericAdapter<TItem>.GetColumns(SourceList.AsQueryable(), bvgGrid);
            SortedList = SourceList.ToArray();
          
            base.OnParametersSet();
        }



   

        public void SortGrid(string s)
        {
            

            SortedList = GenericAdapter<TItem>.GetSortedList(SourceList.AsQueryable(), s).ToArray();

            bvgGrid.CurrScrollPosition = 0;
            LastSkip = -1;
            bvgGrid.VericalScroll.compBlazorScrollbar.SetScrollPosition(0);
            RenderGrid(0);
        }

        public void RenderGrid(int skip)
        {

  
            if (skip != LastSkip)
            {

              
                LastSkip = skip;
                if (skip > 0)
                {

                    GenericAdapter<TItem>.GetRows(SortedList.Skip(skip).Take(bvgGrid.DisplayedRowsCount), bvgGrid);
                }
                else
                {
                    GenericAdapter<TItem>.GetRows(SortedList.Take(bvgGrid.DisplayedRowsCount), bvgGrid);
                }



                if (NeedsRefreshFromDotNet)
                {
                    Console.WriteLine("!!!!!bvgGrid.bvgAreaRows.InvokePropertyChanged()");
                    bvgGrid.bvgAreaRows.InvokePropertyChanged();
                }
            }
        }

        public void Timer1Callback(object o)
        {
            if (ActualRender == false)
            {
                GetActualWidthAndHeight();      
            }
            else
            {

                RenderGrid(0);
                NeedsRefreshFromDotNet = false;
                timer1.Dispose();
            }
        }

        protected override void OnAfterRender()
        {
            if (FirstLoad)
            {
                FirstLoad = false;
                

                if (bvgGrid.OnSort == null)
                {
                    bvgGrid.OnSort = SortGrid;
                }


                if (bvgGrid.OnScroll == null)
                {
                    bvgGrid.OnScroll = RenderGrid;
                }


                timer1 = new Timer(Timer1Callback, null, 1, 1);
            }





            base.OnAfterRender();
        }


        public async void GetActualWidthAndHeight()
        {

            bvgGrid.totalWidth = await BvgJsInterop.GetElementActualWidth(bvgGrid.GridTableElementID)-20;


            double top = await BvgJsInterop.GetElementActualTop(bvgGrid.GridTableElementID);
            double windowHeight = await BvgJsInterop.GetWindowHeight();

            bvgGrid.height = windowHeight - top - 40;



            bvgGrid.AdjustSize();

            bvgGrid.UpdateHorizontalScroll();


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
