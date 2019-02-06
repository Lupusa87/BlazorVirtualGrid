using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompCell : ComponentBase, IDisposable
    {
        [Parameter]
        protected ComponentBase parent { get; set; }


        [Parameter]
        protected BvgCell bvgCell { get; set; }

        public CompRow _parent;

        protected override void OnInit()
        {
            _parent = parent as CompRow;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Console.WriteLine("BuildRenderTree for cell");
            int k = -1;
            builder.OpenElement(k++, "td");

            builder.AddAttribute(k++, "style", bvgCell.GetStyle());

            builder.AddAttribute(k++, "onclick", Clicked);
            builder.AddContent(k++, bvgCell.Value.ToString());
            Console.WriteLine(bvgCell.Value.ToString());
            builder.CloseElement();


            base.BuildRenderTree(builder);
        }


        public void Clicked(UIMouseEventArgs e)
        {

            CompRow a = parent as CompRow;
            a._parent._parent.bvgGrid.SelectCell(bvgCell);

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
