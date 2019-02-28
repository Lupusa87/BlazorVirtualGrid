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
            builder.AddAttribute(k++, "OnPositionChange", new Action<double>(onscroll));
            builder.AddComponentReferenceCapture(k++, (c) =>
            {

                bvgScroll.compBlazorScrollbar = c as CompBlazorScrollbar;
            });

            builder.CloseComponent();

           
        }


        private void onscroll(double ScrollPosition)
        {

            if (bvgScroll.bsbSettings.VerticalOrHorizontal)
            {

                double b = (ScrollPosition + bvgScroll.bvgGrid.VericalScroll.bsbSettings.ScrollVisibleSize)
                        / bvgScroll.bvgGrid.RowHeightOriginal - bvgScroll.bvgGrid.DisplayedRowsCount;

                if (b != bvgScroll.bvgGrid.CurrVerticalScrollPosition)
                {

                    

                    bvgScroll.bvgGrid.CurrVerticalScrollPosition = b;

                    //Console.WriteLine("ScrollPosition2=" + ScrollPosition / bvgScroll.bvgGrid.RowHeightOriginal);

                    bvgScroll.bvgGrid.OnVerticalScroll?.Invoke(b);

                }
            }
            else
            {

                   bvgScroll.bvgGrid.CurrHorizontalScrollPosition = ScrollPosition;
                   bvgScroll.bvgGrid.OnHorizontalScroll?.Invoke(ScrollPosition);

            }

        }



        public void Dispose()
        {

        }
    }
}
