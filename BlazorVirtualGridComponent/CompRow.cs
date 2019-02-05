﻿using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompRow : BlazorComponent, IDisposable
    {
        [Parameter]
        protected BlazorComponent parent { get; set; }


        [Parameter]
        protected BvgRow bvgRow { get; set; }

        public CompGrid _parent;

        protected override void OnInit()
        {
            _parent = parent as CompGrid;
        }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int k = -1;
            builder.OpenElement(k++, "tr");

            //builder.OpenElement(k++, "td");

            //builder.AddAttribute(k++, "style", "text-align:right;width:20px;height:35px;padding:2px;");
            //builder.AddContent(k++, i + 1);

            //builder.CloseElement();



            foreach (var cell in bvgRow.Cells)
            {
                builder.OpenComponent<CompCell>(k++);
                builder.AddAttribute(k++, "bvgCell", cell);
                builder.AddAttribute(k++, "parent", this);


                builder.AddComponentReferenceCapture(k++, (compReference) =>
                {
                    cell.CompReference = compReference as CompCell;
                });

                builder.CloseComponent();
            }



            builder.CloseElement(); //tr


            base.BuildRenderTree(builder);
        }


        public void Clicked(UIMouseEventArgs e)
        {

            //CompTable a = parent as CompTable;

            //CompBlazorSpreadsheet b = a.parent as CompBlazorSpreadsheet;

            //b.SelectionChange(bcell.ID);
        }

        public void Refresh()
        {
            StateHasChanged();
        }


        public void Dispose()
        {

        }
    }
}