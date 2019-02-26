using BlazorScrollbarComponent;
using BlazorScrollbarComponent.classes;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompScroll : ComponentBase, IDisposable
    {
      

        [Parameter]
        protected BvgScroll bvgScroll { get; set; }

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

            bvgScroll.PropertyChanged = BvgScroll_PropertyChanged;
          
        }


        private void BvgScroll_PropertyChanged()
        {
            //EnabledRender = true;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            //EnabledRender = false;

            base.BuildRenderTree(builder);
            //Console.WriteLine("BuildRenderTree CompScroll");

            int k = -1;
            builder.OpenComponent<CompBlazorScrollbar>(k++);
            builder.AddAttribute(k++, "bsbSettings", RuntimeHelpers.TypeCheck<BsbSettings>(bvgScroll.bsbSettings));
            builder.AddAttribute(k++, "OnPositionChange", new Action<int>(onscroll));
            builder.AddComponentReferenceCapture(k++, (c) =>
            {

                bvgScroll.compBlazorScrollbar = c as CompBlazorScrollbar;
            });

            builder.CloseComponent();

           
        }


        private void onscroll(int ScrollPosition)
        {

            if (bvgScroll.bsbSettings.VerticalOrHorizontal)
            {

                if (Math.Abs(ScrollPosition - bvgScroll.bvgGrid.CurrVerticalScrollPosition) > bvgScroll.bvgGrid.bvgSettings.RowHeight)
                {

                    //bvgScroll.bvgGrid.Cmd_Clear_Selection();

                    bvgScroll.bvgGrid.CurrVerticalScrollPosition = ScrollPosition;
                    bvgScroll.bvgGrid.OnVerticalScroll?.Invoke((int)(ScrollPosition / bvgScroll.bvgGrid.bvgSettings.RowHeight));

                }
            }
            else
            {

                   bvgScroll.bvgGrid.CurrHorizontalScrollPosition = ScrollPosition;
                   bvgScroll.bvgGrid.OnHorizontalScroll?.Invoke((int)(ScrollPosition));

            }

        }



        public void Dispose()
        {

        }
    }
}
