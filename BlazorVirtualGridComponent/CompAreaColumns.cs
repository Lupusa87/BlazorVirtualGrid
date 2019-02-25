using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlazorVirtualGridComponent
{
    public class CompAreaColumns : ComponentBase, IDisposable
    {

        [Parameter]
        protected BvgAreaColumns bvgAreaColumns { get; set; }

        [Parameter]
        protected bool ForFrozen { get; set; }

        bool EnabledRender = true;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            EnabledRender = true;
        }

        protected override bool ShouldRender()
        {
            return EnabledRender;
        }

        protected override void OnInit()
        {
            bvgAreaColumns.PropertyChanged = BvgAreaColumns_PropertyChanged;

        }



        protected override void OnAfterRender()
        {
            if (bvgAreaColumns.bvgGrid.ActiveCell != null)
            {
                bvgAreaColumns.bvgGrid.ActiveCellFocus();
            }

            base.OnAfterRender();

        }

        private void BvgAreaColumns_PropertyChanged()
        {
            EnabledRender = true;
            StateHasChanged();

        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            EnabledRender = false;
            //BlazorWindowHelper.BlazorTimeAnalyzer.Add("BvgAreaColumns BuildRenderTree ForFrozen="+ ForFrozen, MethodBase.GetCurrentMethod());

            base.BuildRenderTree(builder);


            int k = -1;


            foreach (BvgColumn c in bvgAreaColumns.bvgGrid.Columns.Where(x => x.IsFrozen == ForFrozen).OrderBy(x => x.SequenceNumber))
            {

                builder.OpenComponent<CompColumn>(k++);
                builder.AddAttribute(k++, "bvgColumn", c);
                builder.CloseComponent();

            }

        }


        public void Dispose()
        {
           
        }
    }
}
