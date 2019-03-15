using BlazorSplitterComponent;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{


    public class CompGrid<TItem> : ComponentBase, IDisposable
    {
        [Parameter]
        protected BvgGrid<TItem> bvgGrid { get; set; }

        //bool EnabledRender = true;



        //protected override Task OnParametersSetAsync()
        //{

        //    EnabledRender = true;

        //    return base.OnParametersSetAsync();
        //}

        //protected override bool ShouldRender()
        //{
        //    return EnabledRender;
        //}

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

            //EnabledRender = true;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            //EnabledRender = false;


           


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

            #region GridArea
            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "class", "myGridArea");


            #region FrozenPart

            if (bvgGrid.Columns.Any(x => x.IsFrozen))
            {
  
                builder.OpenElement(k++, "div");
                builder.AddAttribute(k++, "id", "FrozenDiv1");
                builder.AddAttribute(k++, "class", "myContainerFrozen Border1");
               

                builder.OpenComponent<CompAreaColumns<TItem>>(k++);
                builder.AddAttribute(k++, "ForFrozen", true);
                builder.AddAttribute(k++, "bvgAreaColumns", bvgGrid.bvgAreaColumnsFrozen);
                builder.CloseComponent();


                builder.OpenComponent<CompAreaRows<TItem>>(k++);
                builder.AddAttribute(k++, "ForFrozen", true);
                builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRowsFrozen);
                builder.CloseComponent();


                builder.CloseElement(); //div

            }
            #endregion

            #region NonFrozenPart
           
            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "id", "NonFrozenDiv1");
            builder.AddAttribute(k++, "class", "myContainerNonFrozen Border2");


            builder.OpenComponent<CompAreaColumns<TItem>>(k++);
            builder.AddAttribute(k++, "ForFrozen", false);
            builder.AddAttribute(k++, "bvgAreaColumns", bvgGrid.bvgAreaColumnsNonFrozen);
            builder.CloseComponent();


            builder.OpenComponent<CompAreaRows<TItem>>(k++);
            builder.AddAttribute(k++, "ForFrozen", false);
            builder.AddAttribute(k++, "bvgAreaRows", bvgGrid.bvgAreaRowsNonFrozen);
            builder.CloseComponent();


            builder.CloseElement(); //div
            
            #endregion



            builder.CloseElement(); //div

            #endregion


            #region VericalScroll
            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "class", "myVericalScroll");
            //builder.AddAttribute(k++, "style", "padding-top:4px");

            builder.OpenComponent<CompScroll<TItem>>(k++);
            builder.AddAttribute(k++, "bvgScroll", bvgGrid.VerticalScroll);


            builder.CloseComponent();

            builder.CloseElement(); //div
            #endregion


            #region HorizontalScroll


            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "class", "myHorizontalScroll");

           
            builder.OpenComponent<CompScroll<TItem>>(k++);
            builder.AddAttribute(k++, "bvgScroll", bvgGrid.HorizontalScroll);
            builder.CloseComponent();

            builder.CloseElement(); //div

            #endregion


            #region Resizer

            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "class", "myResizer");

            builder.OpenComponent<CompBlazorSplitter>(k++);
            builder.AddAttribute(k++, "bsSettings", bvgGrid.ResizerBsSettings);
            builder.AddAttribute(k++, "OnDragStart", new Action<int,int, int>(OnDiagonalDragStart));
            builder.AddAttribute(k++, "OnDragEnd", new Action<int, int, int>(OnDiagonalDragEnd));
            builder.CloseComponent();

            builder.CloseElement(); //div
            #endregion
        }

        public void OnDiagonalDragStart(int index, int X, int Y)
        {
            bvgGrid.DragStart = new BvgPointInt(X, Y);
            

        }
        public void OnDiagonalDragEnd(int index, int X, int Y)
        {

            int tmpX = X - bvgGrid.DragStart.X;
            int tmpY = Y - bvgGrid.DragStart.Y;
            bvgGrid.DragStart = new BvgPointInt();

            if (Math.Abs(tmpX) > 3 || Math.Abs(tmpY) > 3)
            {
                bvgGrid.bvgSize.W += tmpX;
                bvgGrid.bvgSize.H += tmpY;

                bvgGrid.HasMeasuredRect = true;
                //bvgGrid.compBlazorVirtualGrid.Refresh(true, false);
                bvgGrid.compBlazorVirtualGrid.Refresh(true, false);
            }

        }


        public void Dispose()
        {
           
        }
    }
}
