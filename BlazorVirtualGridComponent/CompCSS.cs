using BlazorVirtualGridComponent.businessLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent
{
    public class CompCSS : ComponentBase
    {

        private string GenerateCSS()
        {


            BCss blazorCSS = new BCss();

            BCssItem classTD = new BCssItem("td");
            classTD.Values.Add("margin", "0px");
            classTD.Values.Add("padding", "0px");
            classTD.Values.Add("text-align", "center");
            classTD.Values.Add("vertical-align", "middle");
            blazorCSS.Children.Add(classTD);

            

            BCssItem classDiv = new BCssItem(".CellDiv");
            classDiv.Values.Add("overflow", "hidden");
            classDiv.Values.Add("white-space", "nowrap");
            classDiv.Values.Add("text-overflow", "ellipsis");
            classDiv.Values.Add("margin", "0px");
            classDiv.Values.Add("padding", "0px");
            blazorCSS.Children.Add(classDiv);



            BCssItem classTH = new BCssItem("th");
            classTH.Values.Add("text-align", "center");
            classTH.Values.Add("border-style", "solid");
            classTH.Values.Add("margin", "0px");
            classTH.Values.Add("padding", "0px");
            blazorCSS.Children.Add(classTH);


            BCssItem classTR = new BCssItem("tr");
            classTR.Values.Add("margin", "0px");
            classTR.Values.Add("padding", "0px");
            blazorCSS.Children.Add(classTR);

            BCssItem classColumnDiv = new BCssItem(".ColumnDiv");
            classColumnDiv.Values.Add("display", "flex");
            classColumnDiv.Values.Add("flex-direction", "row");
            classColumnDiv.Values.Add("margin", "0px");
            classColumnDiv.Values.Add("padding", "0px");
            blazorCSS.Children.Add(classColumnDiv);


            BCssItem classColumnSpan = new BCssItem(".ColumnSpan");
            classColumnSpan.Values.Add("text-align", "center");
            classColumnSpan.Values.Add("color", "blue");
            blazorCSS.Children.Add(classColumnSpan);



            BCssItem classTable = new BCssItem(".BorderedTable");
            classTable.Values.Add("border", "1px solid black");
            classTable.Values.Add("margin", "0px");
            classTable.Values.Add("padding", "0px");
            blazorCSS.Children.Add(classTable);


            BCssItem classGridDiv = new BCssItem(".GridDiv");
            classGridDiv.Values.Add("overflow-x", "hidden");
            classGridDiv.Values.Add("overflow-y", "hidden");
            classGridDiv.Values.Add("margin", "0px");
            classGridDiv.Values.Add("padding", "0px");
            blazorCSS.Children.Add(classGridDiv);

       


            return blazorCSS.ToString();

        }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            int k = 0;

            // builder.AddMarkupContent(k++,"<style>.my {color:red}");

            builder.OpenElement(k++, "style");
            builder.AddContent(k++, GenerateCSS());
            builder.CloseElement();

        }

    }
}
