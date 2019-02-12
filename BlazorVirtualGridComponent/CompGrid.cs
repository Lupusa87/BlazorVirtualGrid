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


        bool FirtsLoad = true;

        protected override void OnInit()
        {
            _parent = parent as CompBlazorVirtualGrid;

        }


        protected override void OnAfterRender()
        {
            if (FirtsLoad)
            {
                FirtsLoad = false;
                _parent.bvgGrid.PropertyChanged += BvgGrid_PropertyChanged;

                base.OnAfterRender();
            }
        }

        private void BvgGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            //Console.WriteLine("BuildRenderTree grid");

            Cmd_RenderTable(builder);


            base.BuildRenderTree(builder);
        }


        protected void Cmd_RenderTable(RenderTreeBuilder builder)
        {

            BvgGrid bvgGrid = _parent.bvgGrid;

            if (bvgGrid.Columns.Count == 0)
            {
                return;
            }



            int k = -1;

            builder.OpenElement(k++, "table");
            builder.AddAttribute(k++, "style", "margin:0;padding:0;");

            #region FirstRow
            builder.OpenElement(k++, "tr");
            builder.AddAttribute(k++, "style", "margin:0;padding:0;");

            #region frozenPart

            if (bvgGrid.Columns.Any(x => x.IsFrozen))
            {
                builder.OpenElement(k++, "td");
                builder.AddAttribute(k++, "valign", "top");
                builder.AddAttribute(k++, "style", "margin:0;padding:0;");

                //builder.OpenElement(k++, "div");
                //builder.AddAttribute(k++, "style", bvgGrid.GetStyleDiv());

                builder.OpenElement(k++, "table");
                builder.AddAttribute(k++, "style", bvgGrid.GetStyleTable(true));

                builder.OpenElement(k++, "thead");
                builder.OpenElement(k++, "tr");
                builder.AddAttribute(k++, "style", "margin:0;padding:0;");


                foreach (BvgColumn c in bvgGrid.Columns.Where(x => x.IsFrozen).OrderBy(x => x.SequenceNumber))
                {

                    builder.OpenComponent<CompColumn>(k++);
                    builder.AddAttribute(k++, "bvgColumn", c);
                    builder.AddAttribute(k++, "parent", parent);
                    builder.CloseComponent();

                }


                builder.CloseElement(); //tr
                builder.CloseElement(); //thead

                builder.OpenComponent<CompAreaRows>(k++);
                builder.AddAttribute(k++, "ForFrozen", true);
                builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRows);
                builder.AddAttribute(k++, "parent", parent);
                builder.CloseComponent();

                builder.CloseElement(); //table
                //builder.CloseElement(); //div


                builder.CloseElement(); //td


            }
            #endregion


            #region grid
            builder.OpenElement(k++, "td");
            builder.AddAttribute(k++, "valign", "top");
            builder.AddAttribute(k++, "style", "margin:0;padding:0;");



            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "id", bvgGrid.GridDivElementID);
            builder.AddAttribute(k++, "style", bvgGrid.GetStyleDiv());

            builder.OpenElement(k++, "table");
            builder.AddAttribute(k++, "style", bvgGrid.GetStyleTable(false));

            builder.OpenElement(k++, "thead");
            builder.OpenElement(k++, "tr");
            builder.AddAttribute(k++, "style", "margin:0;padding:0;");



            foreach (BvgColumn c in bvgGrid.Columns.Where(x => x.IsFrozen == false).OrderBy(x => x.SequenceNumber))
            {

                builder.OpenComponent<CompColumn>(k++);
                builder.AddAttribute(k++, "bvgColumn", c);
                builder.AddAttribute(k++, "parent", parent);
                builder.CloseComponent();

            }


            builder.CloseElement(); //tr
            builder.CloseElement(); //thead

            builder.OpenComponent<CompAreaRows>(k++);
            builder.AddAttribute(k++, "ForFrozen", false);
            builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRows);
            builder.AddAttribute(k++, "parent", parent);
            builder.CloseComponent();

            builder.CloseElement(); //table
            builder.CloseElement(); //div
            

            builder.CloseElement(); //td
            #endregion


            builder.OpenElement(k++, "td");

            builder.OpenComponent<CompScroll>(k++);
            builder.AddAttribute(k++, "bvgScroll", bvgGrid.VericalScroll);
            builder.AddAttribute(k++, "parent", parent);

            builder.CloseComponent();

            builder.CloseElement(); //td


            builder.CloseElement(); //tr

            #endregion firstrow


            builder.OpenElement(k++, "tr");
            builder.AddAttribute(k++, "valign", "top");
            builder.AddAttribute(k++, "style", "margin:0;padding:0;");


            builder.OpenElement(k++, "td");
            builder.AddAttribute(k++, "colspan", 2);
            builder.AddAttribute(k++, "valign", "top");
            builder.AddAttribute(k++, "style", "margin:0;padding:0;");


            Console.WriteLine("amerika" + bvgGrid.HorizontalScroll.IsVisible);
            if (bvgGrid.HorizontalScroll.IsVisible)
            {
                builder.OpenComponent<CompScroll>(k++);
                builder.AddAttribute(k++, "bvgScroll", bvgGrid.HorizontalScroll);
                builder.AddAttribute(k++, "parent", parent);
                builder.CloseComponent();
            }


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
