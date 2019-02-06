using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompColumn : ComponentBase, IDisposable
    {
        [Parameter]
        protected ComponentBase parent { get; set; }


        [Parameter]
        public BvgColumn bvgColumn { get; set; }

        public CompGrid _parent;

        protected override void OnInit()
        {
            Console.WriteLine("OnInit " + bvgColumn.Name);
            
            _parent = parent as CompGrid;
        }

        private void BvgColumn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            bvgColumn.PropertyChanged += BvgColumn_PropertyChanged;

            Console.WriteLine("BuildRenderTree " + bvgColumn.Name);
            int k = -1;

            builder.OpenElement(k++, "th");

            builder.AddAttribute(k++, "style",bvgColumn.GetStyle());
            builder.AddAttribute(k++, "onclick", Clicked);
            
            builder.AddContent(k++, bvgColumn.Name);
            
            
            //builder.AddElementReferenceCapture(k++, (elementReference) =>
            //{

            //    Elementreferences_Dictionary.Add(_value_id, elementReference);

            //});


            builder.CloseElement(); //th


            base.BuildRenderTree(builder);
        }


        public void Clicked(UIMouseEventArgs e)
        {
            CompGrid a = parent as CompGrid;
            a._parent.bvgGrid.SelectColumn(bvgColumn);
           
        }


        //public void Refresh()
        //{
        //    StateHasChanged();
        //}


        public void Dispose()
        {
            bvgColumn.PropertyChanged -= BvgColumn_PropertyChanged;
        }
    }
}
