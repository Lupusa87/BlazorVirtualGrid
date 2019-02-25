using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent
{
    public class CompTable : ComponentBase, IDisposable
    {
        [Parameter]
        protected BvgGrid bvgGrid { get; set; }


        [Parameter]
        protected bool ActualRender { get; set; }

        bool EnabledRender = true;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            EnabledRender = true;
        }

        protected override bool ShouldRender()
        {
            return EnabledRender;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            //Console.WriteLine("Comptable BuildRenderTree");
            int k = -1;

            builder.OpenElement(k++, "table");
            builder.AddAttribute(k++, "id", bvgGrid.GridTableElementID);
            builder.AddAttribute(k++, "style", "table-layout:auto;width:100%;");
            builder.AddAttribute(k++, "onwheel", OnWheel);

            if (ActualRender)
            {
                builder.OpenComponent<CompGrid>(k++);
                builder.AddAttribute(k++, "bvgGrid", bvgGrid);
                builder.CloseComponent();

                // without this after wheel was re-rendering and giving error
                EnabledRender = false;
                
            }

        
            builder.CloseElement(); //table


            
        }


        public void OnWheel(UIWheelEventArgs e)
        {
            bvgGrid.VericalScroll.compBlazorScrollbar.bsbScrollbar.CmdWhell(e.DeltaY > 0);
        }

        public void Dispose()
        {
           
        }
    }
}
