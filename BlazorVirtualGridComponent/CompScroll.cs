using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompScroll : ComponentBase, IDisposable
    {
        [Parameter]
        protected ComponentBase parent { get; set; }


        [Parameter]
        protected BvgScroll bvgScroll { get; set; }

        public CompBlazorVirtualGrid _parent;

        protected override void OnInit()
        {

            _parent = parent as CompBlazorVirtualGrid;
        }

        private void BvgScroll_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            bvgScroll.PropertyChanged += BvgScroll_PropertyChanged;

            int k = -1;
            builder.OpenElement(k++, "div");


            if (bvgScroll.IsVerticalOrHorizontal)
            {
                builder.AddAttribute(k++, "id", "VerticalScroll" + bvgScroll.ID);
            }
            else
            {
                builder.AddAttribute(k++, "id", "HorizontalScroll" + bvgScroll.ID);
            }

            builder.AddAttribute(k++, "style", bvgScroll.GetStyleDiv());
            builder.AddAttribute(k++, "onscroll", onscroll);




            builder.OpenElement(k++, "canvas");
            builder.AddAttribute(k++, "style", bvgScroll.GetStyleCanvas());
            builder.CloseElement();
            
            

            builder.CloseElement();

            base.BuildRenderTree(builder);
        }


        public async void onscroll(UIEventArgs e)
        {
            int ScrollPosition = 0;
            if (bvgScroll.IsVerticalOrHorizontal)
            {
                ScrollPosition = await BvgJsInterop.GetScrollTopPosition("VerticalScroll" + bvgScroll.ID);

                if (Math.Abs(ScrollPosition - _parent.bvgGrid.CurrScrollPosition) > _parent.bvgGrid.RowHeight)
                {
                    _parent.bvgGrid.CurrScrollPosition = ScrollPosition;
                    _parent.bvgGrid.OnVerticalScroll();
                }
            }
            else
            {
                ScrollPosition = await BvgJsInterop.GetScrollLeftPosition("HorizontalScroll" + bvgScroll.ID);
            }

        }



        public void Dispose()
        {
            bvgScroll.PropertyChanged -= BvgScroll_PropertyChanged;
        }
    }
}
