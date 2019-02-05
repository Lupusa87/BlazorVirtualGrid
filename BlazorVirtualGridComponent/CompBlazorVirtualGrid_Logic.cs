using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompBlazorVirtualGrid_Logic:BlazorComponent 
    {
        [Parameter]
        public BvgGrid bvgGrid { get; set; }


        //[Parameter]
        //public List<T> ItemsSource { get; set; }


        public CompGrid CompGrid1 = new CompGrid();


        protected override void OnInit()
        {
            //bvgGrid.OnChange = OnChange;

            base.OnInit();
        }


        protected override void OnAfterRender()
        {
            bvgGrid.CompReference = CompGrid1;
            base.OnAfterRender();
        }

        public void OnChange()
        {
            Refresh();
        }


        public void Refresh()
        {
            
            CompGrid1.Refresh();
            StateHasChanged();
        }


      

    }
}
