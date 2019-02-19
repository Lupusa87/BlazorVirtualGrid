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
        protected BvgAreaRows bvgAreaRows { get; set; }

        [Parameter]
        protected bool ForFrozen { get; set; }



        protected override void OnInit()
        {
            bvgAreaRows.PropertyChanged += BvgAreaRows_PropertyChanged;

        }



        protected override void OnAfterRender()
        {
            if (bvgAreaRows.bvgGrid.ActiveCell != null)
            {
                if (bvgAreaRows.bvgGrid.ActiveCell.FocusRequired)
                {
                    bvgAreaRows.bvgGrid.ActiveCell.FocusRequired = false;
                   
                    bvgAreaRows.bvgGrid.ActiveCellFocus();
                }
            }
            base.OnAfterRender();
        }

        private void BvgAreaRows_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int k = -1;

            builder.OpenElement(k++, "tbody");


            foreach (var r in bvgAreaRows.bvgGrid.Rows)
            {
                builder.OpenComponent<CompRow>(k++);
                builder.AddAttribute(k++, "ForFrozen", ForFrozen);
                builder.AddAttribute(k++, "bvgRow", r);
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
