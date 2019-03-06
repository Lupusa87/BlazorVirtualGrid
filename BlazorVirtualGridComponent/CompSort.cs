using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompSort<TItem> : ComponentBase 
    {
        [Parameter]
        protected BvgColumn<TItem> bvgColumn { get; set; }


        [Parameter]
        protected bool IsNotHidden { get; set; }

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

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            //EnabledRender = false;

            base.BuildRenderTree(builder);



            int k = 0;
            builder.OpenElement(k++, "svg");
            builder.AddAttribute(k++, "width", bvgColumn.bvgGrid.bvgSettings.bSortStyle.width);
            builder.AddAttribute(k++, "height", bvgColumn.bvgGrid.bvgSettings.bSortStyle.height);
            builder.AddAttribute(k++, "xmlns", "http://www.w3.org/2000/svg");
           
            if (IsNotHidden)
            {
                PaintUp(builder, k);

                PaintDown(builder, k);
            }

           

            builder.CloseElement();
        }

        private void PaintUp(RenderTreeBuilder builder, int k)
        {
            builder.OpenElement(k++, "polygon");

            builder.AddAttribute(k++, "points", bvgColumn.bvgGrid.bvgSettings.bSortStyle.UpPoligonPoints);

            if (bvgColumn.IsAscendingOrDescending)
            {
                builder.AddAttribute(k++, "style", string.Concat("fill:",
                    bvgColumn.bvgGrid.bvgSettings.bSortStyle.SortedDirectionColor,
                    ";stroke:", bvgColumn.bvgGrid.bvgSettings.bSortStyle.BorderColor));
            }
            else
            {
                builder.AddAttribute(k++, "style", string.Concat("fill:",
                    bvgColumn.bvgGrid.bvgSettings.bSortStyle.UnSortedDirectionColor,
                    ";stroke:", bvgColumn.bvgGrid.bvgSettings.bSortStyle.BorderColor));
            }

            builder.CloseElement();
        }

        private void PaintDown(RenderTreeBuilder builder, int k)
        {
            builder.OpenElement(k++, "polygon");

            builder.AddAttribute(k++, "points", bvgColumn.bvgGrid.bvgSettings.bSortStyle.DownPoligonPoints);

            if (bvgColumn.IsAscendingOrDescending)
            {
                builder.AddAttribute(k++, "style", string.Concat("fill:",
                    bvgColumn.bvgGrid.bvgSettings.bSortStyle.UnSortedDirectionColor,
                    ";stroke:", bvgColumn.bvgGrid.bvgSettings.bSortStyle.BorderColor));
            }
            else
            {
                builder.AddAttribute(k++, "style", string.Concat("fill:",
                    bvgColumn.bvgGrid.bvgSettings.bSortStyle.SortedDirectionColor,
                    ";stroke:", bvgColumn.bvgGrid.bvgSettings.bSortStyle.BorderColor));
            }
            


            builder.CloseElement();
        }
    }
}
