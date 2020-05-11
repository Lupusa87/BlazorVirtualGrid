using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompLayout<TItem> : ComponentBase, IDisposable
    {
        [Parameter]
        public BvgGrid<TItem> bvgGrid { get; set; }

        [Parameter]
        public bool ActualRender { get; set; }



        bool EnabledRender = true;


        protected override void OnParametersSet()
        {

            EnabledRender = true;

            base.OnParametersSet();
        }

        protected override bool ShouldRender()
        {
           
            //if (bvgGrid.bvgModal.IsDisplayed)
            //{
            //    return false;
            //}

            return EnabledRender;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

 

            base.BuildRenderTree(builder);

          
            int k = -1;

          

            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "class", "myContainer");
            builder.AddAttribute(k++, "id", bvgGrid.DivContainerElementID);


                builder.AddAttribute(k++, "onwheel", EventCallback.Factory.Create(this, OnWheel)); 

                if (ActualRender)
                {
                    builder.OpenComponent<CompGrid<TItem>>(k++);
                    builder.AddAttribute(k++, "bvgGrid", bvgGrid);
                    builder.CloseComponent();

                    // without this after wheel was re-rendering and giving error

                    EnabledRender = false;

                }


                builder.CloseElement(); //div container
           

            
        }


        public void OnWheel(WheelEventArgs e)
        {
            bvgGrid.VerticalScroll.compBlazorScrollbar.DoWheel(e.DeltaY > 0);
        }

        public void Dispose()
        {
           
        }
    }
}
