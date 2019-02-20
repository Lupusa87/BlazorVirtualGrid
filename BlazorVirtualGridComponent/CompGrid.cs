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
        protected BvgGrid bvgGrid { get; set; }


        protected override void OnInit()
        {
            bvgGrid.compGrid = this;

            Subscribe();
        }

        protected override void OnAfterRender()
        {

            if (bvgGrid.compGrid == null)
            {
                bvgGrid.compGrid = this;
            }

            base.OnAfterRender();
        }



        public void Subscribe()
        {
            bvgGrid.PropertyChanged += BvgGrid_PropertyChanged;

        }


        private void BvgGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            Cmd_RenderTable(builder);

        }


        protected void Cmd_RenderTable(RenderTreeBuilder builder)
        {


            if (bvgGrid.Columns.Count == 0)
            {
                return;
            }



            int k = -1;

            #region FirstRow
            builder.OpenElement(k++, "tr");

            #region frozenPart

            if (bvgGrid.Columns.Any(x => x.IsFrozen))
            {
                builder.OpenElement(k++, "td");
                builder.AddAttribute(k++, "valign", "top");


                builder.OpenElement(k++, "table");
                builder.AddAttribute(k++, "class", "BorderedTable");
                builder.AddAttribute(k++, "style", bvgGrid.GetStyleTable(true));

                builder.OpenElement(k++, "thead");
                builder.OpenElement(k++, "tr");
        

                foreach (BvgColumn c in bvgGrid.Columns.Where(x => x.IsFrozen).OrderBy(x => x.SequenceNumber))
                {

                    builder.OpenComponent<CompColumn>(k++);
                    builder.AddAttribute(k++, "bvgColumn", c);
                    builder.CloseComponent();

                }


                builder.CloseElement(); //tr
                builder.CloseElement(); //thead


                
                builder.OpenComponent<CompAreaRows>(k++);
                builder.AddAttribute(k++, "ForFrozen", true);
                builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRows);
                builder.CloseComponent();

                builder.CloseElement(); //table
              
                builder.CloseElement(); //td


            }
            #endregion


            #region grid
            builder.OpenElement(k++, "td");
            builder.AddAttribute(k++, "valign", "top");


            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "id", bvgGrid.GridDivElementID);
            builder.AddAttribute(k++, "class", "GridDiv");
            builder.AddAttribute(k++, "style", bvgGrid.GetStyleDiv());

            builder.OpenElement(k++, "table"); 
            builder.AddAttribute(k++, "class", "BorderedTable");
            builder.AddAttribute(k++, "style", bvgGrid.GetStyleTable(false));

            builder.OpenElement(k++, "thead");
            builder.OpenElement(k++, "tr");


            foreach (BvgColumn c in bvgGrid.Columns.Where(x => x.IsFrozen == false).OrderBy(x => x.SequenceNumber))
            {

                builder.OpenComponent<CompColumn>(k++);
                builder.AddAttribute(k++, "bvgColumn", c);
                builder.CloseComponent();

            }


            builder.CloseElement(); //tr
            builder.CloseElement(); //thead

            builder.OpenComponent<CompAreaRows>(k++);
            builder.AddAttribute(k++, "ForFrozen", false);
            builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRows);
            builder.CloseComponent();

            builder.CloseElement(); //table
            builder.CloseElement(); //div
            

            builder.CloseElement(); //td
            #endregion


            builder.OpenElement(k++, "td");

            builder.OpenComponent<CompScroll>(k++);
            builder.AddAttribute(k++, "bvgScroll", bvgGrid.VericalScroll);


            builder.CloseComponent();

            builder.CloseElement(); //td


            builder.CloseElement(); //tr

            #endregion firstrow


            #region SecondRow

            builder.OpenElement(k++, "tr");
            builder.AddAttribute(k++, "valign", "top");
        


            builder.OpenElement(k++, "td");
            builder.AddAttribute(k++, "colspan", 2);
            builder.AddAttribute(k++, "valign", "top");
           

            if (bvgGrid.HorizontalScroll.IsVisible)
            {
              
                builder.OpenComponent<CompScroll>(k++);
                builder.AddAttribute(k++, "bvgScroll", bvgGrid.HorizontalScroll);
                builder.CloseComponent();
            }


            builder.CloseElement(); //td

            builder.CloseElement(); //tr

            #endregion


        }




        public void Dispose()
        {
            bvgGrid.PropertyChanged -= BvgGrid_PropertyChanged;
        }
    }
}
