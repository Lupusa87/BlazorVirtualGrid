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

      

        protected override void OnInit()
        {

            bvgScroll.PropertyChanged += BvgScroll_PropertyChanged;
          
        }


        private void BvgScroll_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
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

            base.BuildRenderTree(builder);
        }


        private void onscroll(double ScrollPosition)
        {
            

            if (bvgScroll.bsbSettings.VerticalOrHorizontal)
            {

                if (Math.Abs(ScrollPosition - bvgScroll.bvgGrid.CurrScrollPosition) > bvgScroll.bvgGrid.bvgSettings.RowHeight)
                {
                    BlazorWindowHelper.BlazorTimeAnalyzer.Reset();
                    BlazorWindowHelper.BlazorTimeAnalyzer.Add("onscroll fired", MethodBase.GetCurrentMethod());

                    bvgScroll.bvgGrid.Cmd_Clear_Selection();

                    bvgScroll.bvgGrid.CurrScrollPosition = ScrollPosition;
                    bvgScroll.bvgGrid.OnScroll?.Invoke((int)(ScrollPosition / bvgScroll.bvgGrid.bvgSettings.RowHeight));

                }
            }
            else
            {
 
                BvgJsInterop.SetElementScrollLeft(bvgScroll.bvgGrid.GridDivElementID, ScrollPosition);
            }

        }



        public void Dispose()
        {
            bvgScroll.PropertyChanged -= BvgScroll_PropertyChanged;
        }
    }
}
