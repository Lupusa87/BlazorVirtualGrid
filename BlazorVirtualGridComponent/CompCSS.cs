using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompCSS<TItem> : ComponentBase
    {

        [Parameter]
        protected BvgGrid<TItem> bvgGrid { get; set; }

        bool EnabledRender = true;

       

        protected override Task OnParametersSetAsync()
        {

            EnabledRender = true;

            return base.OnParametersSetAsync();
        }

        protected override bool ShouldRender()
        {
            return EnabledRender;
        }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            EnabledRender = false;

            base.BuildRenderTree(builder);

            int k = 0;


            // builder.AddMarkupContent(k++,"<style>.my {color:red}");


            bvgGrid.cssHelper = new CssHelper<TItem>(bvgGrid);
            



            builder.OpenElement(k++, "style");
            builder.AddAttribute(k++,"id","bvgStyle1");
            builder.AddContent(k++, bvgGrid.cssHelper.GetString("bvgStyle1"));
            builder.CloseElement();

            builder.OpenElement(k++, "style");
            builder.AddAttribute(k++, "id", "bvgStyle2");
            builder.AddContent(k++, bvgGrid.cssHelper.GetString("bvgStyle2"));
            builder.CloseElement();

            //builder.OpenElement(k++, "link");
            //builder.AddAttribute(k++, "rel", "stylesheet");
            //builder.AddAttribute(k++, "href", "data:text/css;base64," + cssHelper.GetBase64String());
            //builder.AddAttribute(k++, "type", "text/css");
            //builder.CloseElement();


        }


      


       



    }
}
