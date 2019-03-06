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

         
            CssHelper<TItem> cssHelper = new CssHelper<TItem>(bvgGrid.bvgSettings);

            builder.OpenElement(k++, "style");
            builder.AddContent(k++, cssHelper.GetString());
            builder.CloseElement();


     

            //builder.OpenElement(k++, "link");
            //builder.AddAttribute(k++, "rel", "stylesheet");
            //builder.AddAttribute(k++, "href", "data:text/css;base64," + cssHelper.GetBase64String());
            //builder.AddAttribute(k++, "type", "text/css");
            //builder.CloseElement();


        }


      


       



    }
}
