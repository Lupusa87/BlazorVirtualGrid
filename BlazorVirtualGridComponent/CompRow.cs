using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompRow : ComponentBase, IDisposable
    {
        [Parameter]
        protected ComponentBase parent { get; set; }


        [Parameter]
        protected BvgRow bvgRow { get; set; }


        [Parameter]
        protected bool ForFrozen { get; set; }


        public CompBlazorVirtualGrid _parent;

        protected override void OnInit()
        {
            
            _parent = parent as CompBlazorVirtualGrid;
        }

        private void BvgRow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {


            bvgRow.PropertyChanged += BvgRow_PropertyChanged;
            int k = -1;
            builder.OpenElement(k++, "tr");

            foreach (var cell in bvgRow.Cells.OrderBy(x=>x.bvgColumn.SequenceNumber))
            {
                if (cell.bvgColumn.IsFrozen == ForFrozen)
                {
                    builder.OpenComponent<CompCell>(k++);
                    builder.AddAttribute(k++, "bvgCell", cell);
                    builder.AddAttribute(k++, "parent", parent);
                    builder.CloseComponent();
                }
            }

            builder.CloseElement(); //tr


            base.BuildRenderTree(builder);
        }


        public void Clicked(UIMouseEventArgs e)
        {
        }


        public void Dispose()
        {
            bvgRow.PropertyChanged -= BvgRow_PropertyChanged;
        }
    }
}
