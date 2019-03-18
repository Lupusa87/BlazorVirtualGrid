using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompAreaColumns<TItem> : ComponentBase, IDisposable
    {

        [Parameter]
        protected BvgAreaColumns<TItem> bvgAreaColumns { get; set; }

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
            bvgAreaColumns.PropertyChanged = BvgAreaColumns_PropertyChanged;

        }


        private void BvgAreaColumns_PropertyChanged()
        {
            //EnabledRender = true;
            StateHasChanged();

        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            //EnabledRender = false;
            //BlazorWindowHelper.BlazorTimeAnalyzer.Add("BvgAreaColumns BuildRenderTree ForFrozen="+ ForFrozen, MethodBase.GetCurrentMethod());

            base.BuildRenderTree(builder);


            int k = -1;


            foreach (BvgColumn<TItem> c in bvgAreaColumns.bvgGrid.Columns.Where(x => x.IsFrozen == ForFrozen).OrderBy(x => x.SequenceNumber))
            {
                
                builder.OpenComponent<CompColumn<TItem>>(k++);
                builder.AddAttribute(k++, "bvgColumn", c);
                builder.CloseComponent();

            }

        }


        public void Dispose()
        {
           
        }
    }
}
