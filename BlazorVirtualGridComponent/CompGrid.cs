using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{


    public class CompGrid : ComponentBase, IDisposable
    {
        [Parameter]
        protected ComponentBase parent { get; set; }

        public CompBlazorVirtualGrid _parent;



        

        protected override void OnInit()
        {
            _parent = parent as CompBlazorVirtualGrid;
            
        }


        protected override void OnAfterRender()
        {
           // BvgJsInterop.SetScrollTopPosition("VerticalScroll" + _parent.bvgGrid.VericalScroll.ID,0);
           // BvgJsInterop.SetScrollTopPosition("HorizontalScroll" + _parent.bvgGrid.HorizontalScroll.ID, 0);

            base.OnAfterRender();
        }

        private void BvgGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            _parent.bvgGrid.PropertyChanged += BvgGrid_PropertyChanged;
            Cmd_RenderTable(builder);


            base.BuildRenderTree(builder);
        }


        protected void Cmd_RenderTable(RenderTreeBuilder builder)
        {


            

            BvgGrid bvgGrid = _parent.bvgGrid;

            if(bvgGrid.Columns.Count == 0)
            {
                return;
            }



            int k = -1;

            builder.OpenElement(k++, "table");
            builder.OpenElement(k++, "tr");
            builder.OpenElement(k++, "td");
            builder.AddAttribute(k++, "valign", "top");

            builder.OpenElement(k++, "table");
            builder.AddAttribute(k++, "style", bvgGrid.GetStyle());

            builder.OpenElement(k++, "thead");
            builder.OpenElement(k++, "tr");



            foreach (BvgColumn c in bvgGrid.Columns.OrderBy(x=>x.SequenceNumber))
            {
          
                builder.OpenComponent<CompColumn>(k++);
                builder.AddAttribute(k++, "bvgColumn", c);
                builder.AddAttribute(k++, "parent", parent);
                builder.CloseComponent();
                
            }


            builder.CloseElement(); //tr
            builder.CloseElement(); //thead

            builder.OpenComponent<CompAreaRows>(k++);
            builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRows);
            builder.AddAttribute(k++, "parent", parent);
            builder.CloseComponent();

            builder.CloseElement(); //table






            builder.CloseElement(); //td
            builder.OpenElement(k++, "td");

            builder.OpenComponent<CompScroll>(k++);
            builder.AddAttribute(k++, "bvgScroll", bvgGrid.VericalScroll);
            builder.AddAttribute(k++, "parent", parent);

            builder.CloseComponent();

            builder.CloseElement(); //td




            
            builder.CloseElement(); //tr

            builder.OpenElement(k++, "tr");
            builder.OpenElement(k++, "td");


            builder.OpenComponent<CompScroll>(k++);
            builder.AddAttribute(k++, "bvgScroll", bvgGrid.HorizontalScroll);
            builder.AddAttribute(k++, "parent", parent);

            builder.CloseComponent();



            builder.CloseElement(); //td
            builder.CloseElement(); //tr

            builder.CloseElement(); //table


        }

        public void Clicked(UIMouseEventArgs e)
        {
            
            //CompTable a = parent as CompTable;

            //CompBlazorSpreadsheet b = a.parent as CompBlazorSpreadsheet;

            //b.SelectionChange(bcell.ID);
        }


        public void Dispose()
        {
            _parent.bvgGrid.PropertyChanged -= BvgGrid_PropertyChanged;
        }
    }
}
