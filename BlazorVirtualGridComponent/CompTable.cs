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
        public BvgGrid bvgGrid { get; set; }


        [Parameter]
        public bool ActualRender { get; set; }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            int k = -1;

            builder.OpenElement(k++, "table");
            builder.AddAttribute(k++, "id", bvgGrid.GridTableElementID);
            builder.AddAttribute(k++, "style", "table-layout:auto;width:100%;margin:0;padding:0;");
            builder.AddAttribute(k++, "onwheel", OnWheel);

            if (ActualRender)
            {
                builder.OpenComponent<CompGrid>(k++);
                builder.AddAttribute(k++, "bvgGrid", bvgGrid);
                builder.CloseComponent();
            }

        
            builder.CloseElement(); //table


            base.BuildRenderTree(builder);
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
