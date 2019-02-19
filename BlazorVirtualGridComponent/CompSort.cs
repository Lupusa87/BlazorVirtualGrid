using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent
{
    public class CompSort:ComponentBase 
    {
        [Parameter]
        protected BvgColumn bvgColumn { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {


            //Console.WriteLine("BuildRenderTree sort " + bvgColumn.Name + " " + bvgColumn.IsAscendingOrDescending);

            base.BuildRenderTree(builder);

            int k = 0;
            builder.OpenElement(k++, "svg");
            builder.AddAttribute(k++, "width", 20);
            builder.AddAttribute(k++, "height", 40);
            builder.AddAttribute(k++, "xmlns", "http://www.w3.org/2000/svg");
            builder.AddAttribute(k++, "style", "fill:none");

            PaintUp(builder,k);

            PaintDown(builder, k);

            builder.CloseElement();
        }

        private void PaintUp(RenderTreeBuilder builder, int k)
        {
            builder.OpenElement(k++, "polygon");

            builder.AddAttribute(k++, "points", "4,20 10,4, 16,20");

            if (bvgColumn.IsAscendingOrDescending)
            {
                builder.AddAttribute(k++, "style", "fill:none;stroke:blue");
            }
            else
            {
                builder.AddAttribute(k++, "style", "fill:red;stroke:blue");
            }

            builder.CloseElement();
        }

        private void PaintDown(RenderTreeBuilder builder, int k)
        {
            builder.OpenElement(k++, "polygon");

            builder.AddAttribute(k++, "points", "4,20 10,36, 16,20");

            if (bvgColumn.IsAscendingOrDescending)
            {
                builder.AddAttribute(k++, "style", "fill:red;stroke:blue");
            }
            else
            {
                builder.AddAttribute(k++, "style", "fill:none;stroke:blue");
            }
            


            builder.CloseElement();
        }
    }
}
