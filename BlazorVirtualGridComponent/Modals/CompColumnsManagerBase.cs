using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlazorVirtualGridComponent.Modals
{
    public class CompColumnsManagerBase:ComponentBase
    {
        [Parameter]
        protected BvgGrid bvgGrid { get; set; }


        protected override void OnInit()
        {

            

            base.OnInit();
        }


        protected override void OnParametersSet()
        {

            //if (bvgModal.bvgGrid is null)
            //{
            //    Console.WriteLine("bvgModal.bvgGrid is null");
            //}
            //else
            //{
                
            //    Console.WriteLine("bvgModal.bvgGrid is not null");
            //    Console.WriteLine(bvgModal.bvgGrid.Columns.Count());
            //    Console.WriteLine(bvgModal.bvgGrid.Name);
            //}

          
            base.OnParametersSet();
        }

    }
}
