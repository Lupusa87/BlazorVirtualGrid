using BlazorScrollbarComponent;
using BlazorScrollbarComponent.classes;
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

        private bool FirstLoad = true;

        protected override void OnInit()
        {
            bvgScroll.PropertyChanged += BvgScroll_PropertyChanged;
            _parent = parent as CompBlazorVirtualGrid;
        }

        protected override void OnAfterRender()
        {

            if (FirstLoad)
            {
                FirstLoad = false;
                
                if (!bvgScroll.bsbSettings.VerticalOrHorizontal)
                {
                    Console.WriteLine("abc");
                    _parent.bvgGrid.UpdateHorizontalScroll();
                }
            }

            base.OnAfterRender();
        }



        private void BvgScroll_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Console.WriteLine("BuildRenderTree scroll");

            int k = -1;
            builder.OpenComponent<CompBlazorScrollbar>(k++);
            builder.AddAttribute(k++, "bsbSettings", RuntimeHelpers.TypeCheck<BsbSettings>(bvgScroll.bsbSettings));
            builder.AddAttribute(k++, "OnPositionChange", new Action<int>(onscroll));
            builder.AddComponentReferenceCapture(k++, (c) =>
            {
                Console.WriteLine("component reference captured");
                bvgScroll.compBlazorScrollbar = c as CompBlazorScrollbar;
            });

            builder.CloseComponent();

            base.BuildRenderTree(builder);
        }


        private void onscroll(int ScrollPosition)
        {

            if (bvgScroll.bsbSettings.VerticalOrHorizontal)
            {
             
                if (Math.Abs(ScrollPosition - _parent.bvgGrid.CurrScrollPosition) > _parent.bvgGrid.RowHeight)
                {
                    _parent.bvgGrid.CurrScrollPosition = ScrollPosition;
                    _parent.bvgGrid.OnVerticalScroll();
                }
            }
            else
            {
                BvgJsInterop.SetElementScrollLeft(_parent.bvgGrid.GridDivElementID, ScrollPosition * _parent.bvgGrid.HorizontalScrollBarScale);
            }

        }



        public void Dispose()
        {
            bvgScroll.PropertyChanged -= BvgScroll_PropertyChanged;
        }
    }
}
