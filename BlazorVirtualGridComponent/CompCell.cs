using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public CompBlazorVirtualGrid _parent;

        protected override void OnInit()
        {
            
            _parent = parent as CompBlazorVirtualGrid;
        }

        private void BvgCell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {


            bvgCell.PropertyChanged += BvgCell_PropertyChanged;
            int k = -1;
            builder.OpenElement(k++, "td");

            builder.AddAttribute(k++, "style", bvgCell.GetStyle());

            builder.AddAttribute(k++, "onclick", Clicked);
            builder.AddContent(k++, bvgCell.Value.ToString());
           
            builder.CloseElement();


            base.BuildRenderTree(builder);
        }


        public void Clicked(UIMouseEventArgs e)
        {
            _parent.bvgGrid.SelectCell(bvgCell);

        }


        public void Dispose()
        {
            bvgCell.PropertyChanged -= BvgCell_PropertyChanged;
        }
    }
}
