using BlazorVirtualGridComponent.classes;
using BlazorVirtualGridComponent.ExternalSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorVirtualGridComponent.businessLayer
{
    public class CssHelper<TItem>
    {
        private BCss blazorCSS = new BCss();

        private BvgGrid<TItem> bvgGrid = new BvgGrid<TItem>();

        private BCssItem c = new BCssItem(string.Empty);

        public CssHelper(BvgGrid<TItem> _bvgGrid)
        {
            bvgGrid = _bvgGrid;

            bvgGrid.cssGridHelper = new CssGridHelper<TItem>(bvgGrid);

            GenerateCSS();
        }


        public string GetStyle(string selector)
        {
            return blazorCSS.GetStyle(selector);
        }

        public string GetStyleWithSelector(string selector)
        {
            return blazorCSS.GetStyleWithSelector(selector);
        }


        public string GetString()
        {
            return blazorCSS.ToString();
        }

        public string GetBase64String()
        {
            return blazorCSS.ToBase64String();
        }

        public void GenerateCSS()
        {
            GenerateGlobalCSS();

            GenerateCSSGridLayoutCSS();


            if (bvgGrid.HasMeasuredRect)
            {
                GenerateCSSGridFrozenCSS();

                GenerateCSSGridNonFrozenCSS();
            }

            GenerateModalCSS();

           

            c = new BCssItem(".CellDiv");
            c.Values.Add("border-style", "solid");
            c.Values.Add("overflow", "hidden");
            c.Values.Add("white-space", "nowrap");
            c.Values.Add("text-overflow", "ellipsis");
            c.Values.Add("text-align", "center");
            c.Values.Add("vertical-align", "middle");
            c.Values.Add("cursor", "cell");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("line-height", bvgGrid.RowHeightAdjusted + "px");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".ColumnDiv");
            c.Values.Add("border-style", "solid");
            c.Values.Add("font-weight", "bold");
            c.Values.Add("display", "flex");
            c.Values.Add("flex-direction", "row");
            c.Values.Add("text-align", "center");
            c.Values.Add("cursor", "cell");
            c.Values.Add("height", bvgGrid.bvgSettings.HeaderHeight + "px");
            c.Values.Add("line-height", bvgGrid.bvgSettings.HeaderHeight + "px");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".ColumnSpan");
            c.Values.Add("text-align", "center");
            c.Values.Add("color", bvgGrid.bvgSettings.HeaderStyle.ForeColor);
            c.Values.Add("height", bvgGrid.bvgSettings.HeaderHeight + "px");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".GridDiv");
            c.Values.Add("overflow-x", "hidden");
            c.Values.Add("overflow-y", "hidden");
            blazorCSS.Children.Add(c);

            c = new BCssItem(".Border1");
            c.Values.Add("border", "1px solid black");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".Border2");
            c.Values.Add("border-style", "solid");
            c.Values.Add("border-color", "black");
            c.Values.Add("border-width", "1px 1px 1px 0");
            blazorCSS.Children.Add(c);

            GenerateCellCSS();

            GenerateHeaderCSS();

           
        }


        private void GenerateCellCSS()
        {

            c = new BCssItem(".CellNonFrozen");
            c.Values.Add("background-color", bvgGrid.bvgSettings.NonFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.NonFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.NonFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.NonFrozenCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);


            c = new BCssItem(".CellNonFrozenAlternated");
            c.Values.Add("background-color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);

            c = new BCssItem(".CellFrozen");
            c.Values.Add("background-color", bvgGrid.bvgSettings.FrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.FrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.FrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.FrozenCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);


            c = new BCssItem(".CellFrozenAlternated");
            c.Values.Add("background-color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);

            c = new BCssItem(".CellSelected");
            c.Values.Add("background-color", bvgGrid.bvgSettings.SelectedCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.SelectedCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.SelectedCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.SelectedCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);



            c = new BCssItem(".CellActive");
            c.Values.Add("background-color", bvgGrid.bvgSettings.ActiveCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.ActiveCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.ActiveCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.ActiveCellStyle.BorderWidth + "px");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".CellActive:focus");
            c.Values.Add("outline", "none");
            blazorCSS.Children.Add(c);

        }


        private void GenerateHeaderCSS()
        {

            c = new BCssItem(".HeaderRegular");
            c.Values.Add("background-color", bvgGrid.bvgSettings.HeaderStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.HeaderStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.HeaderStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.HeaderStyle.BorderWidth + "px;");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".HeaderActive");
            c.Values.Add("background-color", bvgGrid.bvgSettings.ActiveHeaderStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.ActiveHeaderStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.ActiveHeaderStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.ActiveHeaderStyle.BorderWidth + "px;");
            c.Values.Add("cursor", "pointer");
            blazorCSS.Children.Add(c);

        }

        private void GenerateModalCSS()
        {

            c = new BCssItem(".bm-container");
            c.Values.Add("display", "none");
            c.Values.Add("align-items", "center");
            c.Values.Add("justify-content", "center");
            c.Values.Add("position", "fixed");
            c.Values.Add("top", "0");
            c.Values.Add("left", "0");
            c.Values.Add("width", "100%");
            c.Values.Add("height", "100%");
            c.Values.Add("z-index", "2");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-overlay");
            c.Values.Add("display", "block");
            c.Values.Add("position", "fixed");
            c.Values.Add("width", "100%");
            c.Values.Add("height", "100%");
            c.Values.Add("z-index", "3");
            c.Values.Add("background-color", "rgba(0,0,0,0.5)");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-active");
            c.Values.Add("display", "flex");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".blazor-modal");
            c.Values.Add("display", "flex");
            c.Values.Add("flex-direction", "column");
            c.Values.Add("width", "auto");
            c.Values.Add("background-color", "#fff");
            c.Values.Add("border-radius", "4px");
            c.Values.Add("border", "1px solid #fff");
            c.Values.Add("padding", "1.5rem");
            c.Values.Add("z-index", "3");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-header");
            c.Values.Add("display", "flex");
            c.Values.Add("align-items", "flex-start");
            c.Values.Add("justify-content", "space-between");
            c.Values.Add("padding", "0 0 2rem 0");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-title");
            c.Values.Add("margin-bottom", "0");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-close");
            c.Values.Add("padding", "1rem");
            c.Values.Add("margin", "-1rem -1rem -1rem auto");
            c.Values.Add("background-color", "transparent");
            c.Values.Add("border", "0");
            c.Values.Add("-webkit-appearance", "none");
            c.Values.Add("cursor", "pointer");
            blazorCSS.Children.Add(c);
         

        }

        private void GenerateCSSGridLayoutCSS()
        {

            c = new BCssItem(".myContainer");
            c.Values.Add("display", "grid");
            c.Values.Add("position", "relative");

            if (bvgGrid.HasMeasuredRect)
            {
                c.Values.Add("width", bvgGrid.bvgSize.W +"px");
                c.Values.Add("height", bvgGrid.bvgSize.H + "px");
            }

            c.Values.Add("grid-template-columns", "auto "+ bvgGrid.bvgSettings.ScrollSize + "px");
            c.Values.Add("grid-template-rows", "auto "+ bvgGrid.bvgSettings.ScrollSize + "px");

            blazorCSS.Children.Add(c);

            if (bvgGrid.Columns.Any(x => x.IsFrozen))
            {
                c = new BCssItem(".myGridArea");
                c.Values.Add("grid-column", "1/1");
                c.Values.Add("grid-row", "1/1");
                c.Values.Add("display", "grid");
                c.Values.Add("position", "relative");
                c.Values.Add("grid-template-columns", bvgGrid.FrozenTableWidth + "px " + bvgGrid.NonFrozenTableWidth + "px");
                blazorCSS.Children.Add(c);
            }
            else
            {
                c = new BCssItem(".myGridArea");
                c.Values.Add("grid-column", "1/1");
                c.Values.Add("grid-row", "1/1");
                blazorCSS.Children.Add(c);
            }

            c = new BCssItem(".myHorizontalScroll");
            c.Values.Add("grid-column", "1/1");
            c.Values.Add("grid-row", "2/2");
            blazorCSS.Children.Add(c);

            c = new BCssItem(".myVerticalScroll");
            c.Values.Add("grid-column", "2/2");
            c.Values.Add("grid-row", "1/1");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".myResizer");
            c.Values.Add("grid-column", "2/2");
            c.Values.Add("grid-row", "2/2");
            blazorCSS.Children.Add(c);

            
        }

        private void GenerateCSSGridFrozenCSS()
        {

            c = new BCssItem(".myContainerFrozen");
            c.Values.Add("display", "grid");
            c.Values.Add("position", "relative");

            c.Values.Add("width", bvgGrid.FrozenTableWidth + "px");
            c.Values.Add("height", bvgGrid.bvgSize.H + "px");

            StringBuilder sb1 = new StringBuilder();
            foreach (var item in bvgGrid.Columns.Where(x => x.IsFrozen))
            {
                sb1.Append(item.ColWidth);
                sb1.Append("px ");
            }

            c.Values.Add("grid-template-columns", sb1.ToString().Trim());
            c.Values.Add("grid-template-rows", bvgGrid.bvgSettings.HeaderHeight + "px repeat(" + bvgGrid.DisplayedRowsCount + ", " + bvgGrid.RowHeightAdjusted + "px)");
            c.Values.Add("overflow", "auto");
            blazorCSS.Children.Add(c);


        }


        private void GenerateCSSGridNonFrozenCSS()
        {

            c = new BCssItem(".myContainerNonFrozen");
            c.Values.Add("display", "grid");
            c.Values.Add("position", "relative");


            c.Values.Add("width", bvgGrid.NonFrozenTableWidth + "px");
            c.Values.Add("height", bvgGrid.bvgSize.H + "px");

            StringBuilder sb1 = new StringBuilder();
            foreach (var item in bvgGrid.Columns.Where(x => x.IsFrozen == false))
            {
                sb1.Append(item.ColWidth);
                sb1.Append("px ");
            }

            c.Values.Add("grid-template-columns", sb1.ToString().Trim());
            c.Values.Add("grid-template-rows", bvgGrid.bvgSettings.HeaderHeight + "px repeat(" + bvgGrid.DisplayedRowsCount + ", " + bvgGrid.RowHeightAdjusted + "px)");
            c.Values.Add("overflow", "auto");
            blazorCSS.Children.Add(c);
        }

        private void GenerateGlobalCSS()
        {
            c = new BCssItem("html");
            c.Values.Add("box-sizing", "border-box");
            blazorCSS.Children.Add(c);

            c = new BCssItem("*, *:before, *:after");
            c.Values.Add("box-sizing", "inherit");
            c.Values.Add("padding", "0");
            c.Values.Add("margin", "0");
            blazorCSS.Children.Add(c);

            c = new BCssItem("body");
            c.Values.Add("line-height", "0");
            c.Values.Add("padding", "0");
            c.Values.Add("margin", "0");
            blazorCSS.Children.Add(c);


           
        }

    }
}
