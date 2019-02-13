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
        protected BvgRow bvgRow { get; set; }


        [Parameter]
        protected bool ForFrozen { get; set; }


     

        protected override void OnInit()
        {
            
            
        }

        private void BvgRow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Console.WriteLine("BuildRenderTree row");

            bvgRow.PropertyChanged += BvgRow_PropertyChanged;
            int k = -1;
            builder.OpenElement(k++, "tr");

            foreach (var cell in bvgRow.Cells.OrderBy(x=>x.bvgColumn.SequenceNumber))
            {
                if (cell.bvgColumn.IsFrozen == ForFrozen)
                {
                    builder.OpenComponent<CompCell>(k++);
                    builder.AddAttribute(k++, "bvgCell", cell);
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
