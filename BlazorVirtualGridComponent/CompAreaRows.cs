using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                bvgAreaRows.bvgGrid.ActiveCellFocus();
            }

            base.OnAfterRender();


            if (!ForFrozen)
            {
                //BlazorWindowHelper.BlazorTimeAnalyzer.Log();
            }
        }

        private void BvgAreaRows_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            //BlazorWindowHelper.BlazorTimeAnalyzer.Add("BvgAreaRows BuildRenderTree ForFrozen=" + ForFrozen, MethodBase.GetCurrentMethod());

            base.BuildRenderTree(builder);


            int k = -1;

            foreach (var r in bvgAreaRows.bvgGrid.Rows)
            {
                builder.OpenComponent<CompRow>(k++);
                builder.AddAttribute(k++, "ForFrozen", ForFrozen);
                builder.AddAttribute(k++, "bvgRow", r);
                builder.CloseComponent();
            }
 
        }


        public void Dispose()
        {
            bvgAreaRows.PropertyChanged -= BvgAreaRows_PropertyChanged;
        }
    }
}
