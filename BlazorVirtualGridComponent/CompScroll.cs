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
    public class CompScroll<TItem> : ComponentBase, IDisposable
    {
      

        [Parameter]
        protected BvgScroll<TItem> bvgScroll { get; set; }

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

                //BlazorWindowHelper.BlazorTimeAnalyzer.Add("scroll start", MethodBase.GetCurrentMethod());


                double b = ScrollPosition / bvgScroll.bvgGrid.bvgSettings.RowHeight;

                int skip = (int)b;


                if (skip != bvgScroll.bvgGrid.CurrVerticalScrollPosition)
                {
                   
                    bvgScroll.bvgGrid.CurrVerticalScrollPosition = skip;


                    //BlazorWindowHelper.BlazorTimeAnalyzer.Add("OnVerticalScroll invoke start", MethodBase.GetCurrentMethod());
                    bvgScroll.bvgGrid.OnVerticalScroll?.Invoke(skip);
                    //BlazorWindowHelper.BlazorTimeAnalyzer.Add("OnVerticalScroll invoke end", MethodBase.GetCurrentMethod());
                }

              
                if (bvgScroll.bvgGrid.VerticalScroll.compBlazorScrollbar.IsOnMinPosition())
                {
                
                    bvgScroll.bvgGrid.SetScrollTop(0);
                }
                else
                {
                    if (b - skip == 0)
                    {
                        bvgScroll.bvgGrid.SetScrollTop(0);
                    }
                    else
                    {
                        double m = (b - skip) * bvgScroll.bvgGrid.bvgSettings.RowHeight;
                       
                        bvgScroll.bvgGrid.SetScrollTop(m);
                    }
                }

                //BlazorWindowHelper.BlazorTimeAnalyzer.Add("scroll end", MethodBase.GetCurrentMethod());
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
