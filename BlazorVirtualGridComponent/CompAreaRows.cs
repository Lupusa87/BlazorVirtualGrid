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
    public class CompAreaRows<TItem> : ComponentBase, IDisposable
    {

        [Parameter]
        protected BvgAreaRows<TItem> bvgAreaRows { get; set; }

        [Parameter]
        protected bool ForFrozen { get; set; }

        //bool EnabledRender = true;


        //protected override Task OnParametersSetAsync()
        //{

        //    EnabledRender = true;

        //    return base.OnParametersSetAsync();
        //}

        //protected override bool ShouldRender()
        //{
        //    return EnabledRender;
        //}

        protected override void OnInit()
        {
            bvgAreaRows.PropertyChanged = BvgAreaRows_PropertyChanged;

        }


        private void BvgAreaRows_PropertyChanged()
        {
            //EnabledRender = true;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            //EnabledRender = false;


            //BlazorWindowHelper.BlazorTimeAnalyzer.Add("BvgAreaRows BuildRenderTree ForFrozen=" + ForFrozen, MethodBase.GetCurrentMethod());

            base.BuildRenderTree(builder);


            int k = -1;

            foreach (var r in bvgAreaRows.bvgGrid.Rows)
            {
                builder.OpenComponent<CompRow<TItem>>(k++);
                builder.AddAttribute(k++, "ForFrozen", ForFrozen);
                builder.AddAttribute(k++, "bvgRow", r);
                builder.CloseComponent();
            }
 
        }


        public void Dispose()
        {
           
        }
    }
}
