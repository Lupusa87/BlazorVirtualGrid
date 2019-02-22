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



        protected override void OnInit()
        {
            bvgAreaColumns.PropertyChanged += BvgAreaColumns_PropertyChanged;

        }



        protected override void OnAfterRender()
        {
            if (bvgAreaColumns.bvgGrid.ActiveCell != null)
            {
                bvgAreaColumns.bvgGrid.ActiveCellFocus();
            }

            base.OnAfterRender();



            // BlazorWindowHelper.BlazorTimeAnalyzer.Log();
        }

        private void BvgAreaColumns_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            BlazorWindowHelper.BlazorTimeAnalyzer.Add("BvgAreaColumns BuildRenderTree", MethodBase.GetCurrentMethod());

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
            bvgAreaColumns.PropertyChanged -= BvgAreaColumns_PropertyChanged;
        }
    }
}
