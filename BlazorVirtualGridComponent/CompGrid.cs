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

        bool EnabledRender = true;


        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            EnabledRender = true;
        }

        protected override bool ShouldRender()
        {
            return EnabledRender;
        }

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
            bvgGrid.PropertyChanged = BvgGrid_PropertyChanged;

        }


        private void BvgGrid_PropertyChanged()
        {

            EnabledRender = true;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            EnabledRender = false;
            //Console.WriteLine("grid BuildRenderTree");

            base.BuildRenderTree(builder);

            Cmd_RenderTable(builder);

        }


        protected void Cmd_RenderTable(RenderTreeBuilder builder)
        {


            if (bvgGrid.Columns.Count() == 0)
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


                builder.OpenComponent<CompAreaColumns>(k++);
                builder.AddAttribute(k++, "ForFrozen", true);
                builder.AddAttribute(k++, "bvgAreaColumns", bvgGrid.bvgAreaColumnsFrozen);
                builder.CloseComponent();
                


                builder.CloseElement(); //tr
                builder.CloseElement(); //thead

                builder.OpenElement(k++, "tbody");

                builder.OpenComponent<CompAreaRows>(k++);
                builder.AddAttribute(k++, "ForFrozen", true);
                builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRowsFrozen);
                builder.CloseComponent();

                builder.CloseElement(); //tbody

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


            builder.OpenComponent<CompAreaColumns>(k++);
            builder.AddAttribute(k++, "ForFrozen", false);
            builder.AddAttribute(k++, "bvgAreaColumns", bvgGrid.bvgAreaColumnsNonFrozen);
            builder.CloseComponent();


            builder.CloseElement(); //tr
            builder.CloseElement(); //thead

            builder.OpenElement(k++, "tbody");
            builder.AddAttribute(k++, "id", "tbodyNonFrozenRows1");

            builder.OpenComponent<CompAreaRows>(k++);
            builder.AddAttribute(k++, "ForFrozen", false);
            builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRowsNonFrozen);
            builder.CloseComponent();

            builder.CloseElement(); //tbody

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
           
        }
    }
}
