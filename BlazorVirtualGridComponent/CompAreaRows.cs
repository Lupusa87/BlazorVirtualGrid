using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompAreaRows : ComponentBase, IDisposable
    {
        [Parameter]
        protected ComponentBase parent { get; set; }

        [Parameter]
        protected BvgAreaRows bvgAreaRows { get; set; }

        public CompBlazorVirtualGrid _parent;

        protected override void OnInit()
        {
            bvgAreaRows.PropertyChanged += BvgAreaRows_PropertyChanged;

            _parent = parent as CompBlazorVirtualGrid;
        }

        private void BvgAreaRows_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int k = -1;

            builder.OpenElement(k++, "tbody");


            foreach (var r in _parent.bvgGrid.Rows.Where(x => x.IsInView))
            {
                builder.OpenComponent<CompRow>(k++);
                builder.AddAttribute(k++, "bvgRow", r);
                builder.AddAttribute(k++, "parent", parent);
                builder.CloseComponent();
            }


            builder.CloseElement(); //tbody


            base.BuildRenderTree(builder);
        }


        public void Dispose()
        {
            bvgAreaRows.PropertyChanged -= BvgAreaRows_PropertyChanged;
        }
    }
}
