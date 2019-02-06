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
          
            builder.OpenElement(k++, "thead");
            builder.OpenElement(k++, "tr");

            //builder.OpenElement(k++, "th");

            //builder.AddAttribute(k++, "style", "width:20px;height:35px;margin:1px;padding:2px");
            //builder.AddContent(k++, "");

            //builder.CloseElement(); //th




            foreach (BvgColumn c in bvgGrid.Columns.OrderBy(x=>x.SequenceNumber))
            {
          
                builder.OpenComponent<CompColumn>(k++);
                builder.AddAttribute(k++, "bvgColumn", c);
                builder.AddAttribute(k++, "parent", this);
              

                //builder.AddComponentReferenceCapture(k++, (compReference) =>
                //{
                //    c.CompReference = compReference as CompColumn;
                //});

                builder.CloseComponent();
                
            }


            builder.CloseElement(); //tr
            builder.CloseElement(); //thead

            builder.OpenElement(k++, "tbody");


            foreach (var r in bvgGrid.Rows)
            {
                builder.OpenComponent<CompRow>(k++);
                builder.AddAttribute(k++, "bvgRow", r);
                builder.AddAttribute(k++, "parent", this);


                //builder.AddComponentReferenceCapture(k++, (compReference) =>
                //{
                //    r.CompReference = compReference as CompRow;
                //});

                builder.CloseComponent();
            }


            builder.CloseElement(); //tbody

            builder.CloseElement(); //table  

        }

        public void Clicked(UIMouseEventArgs e)
        {
            
            //CompTable a = parent as CompTable;

            //CompBlazorSpreadsheet b = a.parent as CompBlazorSpreadsheet;

            //b.SelectionChange(bcell.ID);
        }




        //public void Refresh()
        //{
        //    StateHasChanged();  
        //}

        public void Dispose()
        {
            _parent.bvgGrid.PropertyChanged -= BvgGrid_PropertyChanged;
        }
    }
}
