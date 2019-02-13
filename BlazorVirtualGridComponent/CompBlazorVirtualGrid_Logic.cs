using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompBlazorVirtualGrid_Logic: ComponentBase
    {
        [Parameter]
        public BvgGrid bvgGrid { get; set; }

        public bool ActualRender { get; set; } =false;


        public BvgSettings GridSettings { get; set; } = new BvgSettings();

        Timer timer1;

        public bool ForceStateHasChaned { get; set; } = false;


        private bool FirstLoad = true;

        protected override void OnInit()
        {

            

            base.OnInit();
        }

        public void Timer1Callback(object o)
        {
            GetActualWidthAndHeight();

            timer1.Dispose();
        }

        protected override void OnAfterRender()
        {
            if (FirstLoad)
            {
                FirstLoad = false;
                timer1 = new Timer(Timer1Callback, null, 1, 1);
            }

            base.OnAfterRender();
        }


        public async void GetActualWidthAndHeight()
        {
            Console.WriteLine("aaa0000");
            bvgGrid.totalWidth = await BvgJsInterop.GetElementActualWidth(bvgGrid.GridTableElementID)-20;


            double top = await BvgJsInterop.GetElementActualTop(bvgGrid.GridTableElementID);
            double windowHeight = await BvgJsInterop.GetWindowHeight();

            bvgGrid.height = windowHeight - top - 40;


            Console.WriteLine("Table width " + bvgGrid.totalWidth);
            Console.WriteLine("Table height " + bvgGrid.height);


            Console.WriteLine("aaaaaaa");
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
