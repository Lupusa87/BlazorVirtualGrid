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


        public CssHelper(BvgGrid<TItem> _bvgGrid)
        {
            bvgGrid = _bvgGrid;
            GenerateCSS();
        }


        public string GetStyle(string selector)
        {
            return blazorCSS.GetStyle(selector);
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

            GenerateCSSGridCSS();

            GenerateModalCSS();

            BCssItem c = new BCssItem("td");
            c.Values.Add("text-align", "center");
            c.Values.Add("vertical-align", "middle");
            blazorCSS.Children.Add(c);

            c = new BCssItem(".CellDiv");
            c.Values.Add("overflow", "hidden");
            c.Values.Add("white-space", "nowrap");
            c.Values.Add("text-overflow", "ellipsis");
            c.Values.Add("height", (bvgGrid.RowHeightAdjusted - bvgGrid.bvgSettings.NonFrozenCellStyle.BorderWidth) + "px");
            c.Values.Add("line-height", (bvgGrid.RowHeightAdjusted - bvgGrid.bvgSettings.NonFrozenCellStyle.BorderWidth) + "px");
            blazorCSS.Children.Add(c);



            c = new BCssItem("th");
            c.Values.Add("text-align", "center");
            c.Values.Add("border-style", "solid");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".ColumnDiv");
            c.Values.Add("display", "flex");
            c.Values.Add("flex-direction", "row");
            c.Values.Add("height", (bvgGrid.bvgSettings.HeaderHeight - bvgGrid.bvgSettings.HeaderStyle.BorderWidth) + "px");
            c.Values.Add("line-height", (bvgGrid.bvgSettings.HeaderHeight - bvgGrid.bvgSettings.HeaderStyle.BorderWidth) + "px");
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

            GenerateCellCSS();

            GenerateHeaderCSS();

           
        }


        private void GenerateCellCSS()
        {

            BCssItem c = new BCssItem(".CellNonFrozen");
            c.Values.Add("border-style", "solid");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("background-color", bvgGrid.bvgSettings.NonFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.NonFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.NonFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.NonFrozenCellStyle.BorderWidth + "px;");
            c.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".CellNonFrozenAlternated");
            c.Values.Add("border-style", "solid");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("background-color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BorderWidth + "px;");
            c.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(c);

            c = new BCssItem(".CellFrozen");
            c.Values.Add("border-style", "solid");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("background-color", bvgGrid.bvgSettings.FrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.FrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.FrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.FrozenCellStyle.BorderWidth + "px;");
            c.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".CellFrozenAlternated");
            c.Values.Add("border-style", "solid");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("background-color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BorderWidth + "px;");
            c.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(c);

            c = new BCssItem(".CellSelected");
            c.Values.Add("border-style", "solid");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("background-color", bvgGrid.bvgSettings.SelectedCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.SelectedCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.SelectedCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.SelectedCellStyle.BorderWidth + "px");
            c.Values.Add("cursor", "pointer");
            blazorCSS.Children.Add(c);



            c = new BCssItem(".CellActive");
            c.Values.Add("border-style", "solid");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("background-color", bvgGrid.bvgSettings.ActiveCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.ActiveCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.ActiveCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.ActiveCellStyle.BorderWidth + "px");
            c.Values.Add("cursor", "pointer");
            c.Values.Add("outline", bvgGrid.bvgSettings.ActiveCellStyle.OutlineWidth + "px solid " + bvgGrid.bvgSettings.ActiveCellStyle.OutlineColor);
            blazorCSS.Children.Add(c);


        }


        private void GenerateHeaderCSS()
        {

            BCssItem c = new BCssItem(".HeaderRegular");
            c.Values.Add("border-style", "solid");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("background-color", bvgGrid.bvgSettings.HeaderStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.HeaderStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.HeaderStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.HeaderStyle.BorderWidth + "px;");
            c.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".HeaderActive");
            c.Values.Add("border-style", "solid");
            c.Values.Add("height", bvgGrid.RowHeightAdjusted + "px");
            c.Values.Add("background-color", bvgGrid.bvgSettings.ActiveHeaderStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.ActiveHeaderStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.ActiveHeaderStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.ActiveHeaderStyle.BorderWidth + "px;");
            c.Values.Add("cursor", "pointer");
            blazorCSS.Children.Add(c);

        }

        private void GenerateModalCSS()
        {

            BCssItem c = new BCssItem(".bm-container");
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

        private void GenerateCSSGridCSS()
        {

            BCssItem c = new BCssItem(".myContainer");
            c.Values.Add("display", "grid");
            c.Values.Add("position", "relative");

            if (bvgGrid.HasMeasuredRect)
            {
                c.Values.Add("width", bvgGrid.bvgSize.w +"px");
                c.Values.Add("height", bvgGrid.bvgSize.h + "px");
            }

            c.Values.Add("grid-template-columns", "auto "+ bvgGrid.bvgSettings.ScrollSize + "px");
            c.Values.Add("grid-template-rows", "auto "+ bvgGrid.bvgSettings.ScrollSize + "px");
            //c.Values.Add("overflow", "auto");
            blazorCSS.Children.Add(c);

            
            c = new BCssItem(".myGridArea");
            c.Values.Add("grid-column", "1/1");
            c.Values.Add("grid-row", "1/1");
            blazorCSS.Children.Add(c);

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

        private void GenerateGlobalCSS()
        {

            BCssItem c = new BCssItem("*");
            c.Values.Add("box-sizing", "border-box");
            c.Values.Add("padding", "0");
            c.Values.Add("margin", "0");
            blazorCSS.Children.Add(c);

            c = new BCssItem("body");
            c.Values.Add("line-height", "0");
            blazorCSS.Children.Add(c);
        }

    }
}
