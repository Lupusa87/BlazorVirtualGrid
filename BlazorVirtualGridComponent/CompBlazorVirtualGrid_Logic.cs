using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompBlazorVirtualGrid_Logic: ComponentBase
    {
        [Parameter]
        public BvgGrid bvgGrid { get; set; } = new BvgGrid();

        public BvgSettings GridSettings { get; set; } = new BvgSettings();

        public CompGrid CompGrid1 = new CompGrid();


        private bool FirstLoad = true;

        protected override void OnInit()
        {
            //bvgGrid.OnChange = OnChange;

            base.OnInit();
        }


        protected override void OnAfterRender()
        {
            //bvgGrid.CompReference = CompGrid1;

           

            base.OnAfterRender();
        }

        public void OnChange()
        {
            Refresh();
        }


        public void Refresh()
        {

            bvgGrid.InvokePropertyChanged();
            

            StateHasChanged();
        }


      

    }
}
