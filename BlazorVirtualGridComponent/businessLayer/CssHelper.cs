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

        private BvgGrid<TItem> bvgGrid { get; set; } 

        private BCssItem c = new BCssItem(string.Empty, string.Empty);

        private readonly string StyleID1 = "bvgStyle1";
        private readonly string StyleID2 = "bvgStyle2";

        public CssHelper(BvgGrid<TItem> _bvgGrid)
        {
            bvgGrid = _bvgGrid;

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


        public string GetString(string StyleID)
        {
            return blazorCSS.ToString(StyleID);
        }

        public string GetBase64String()
        {
            return blazorCSS.ToBase64String();
        }


        public void UpdateStyle2(BvgGrid<TItem> _bvgGrid)
        {
            //without this it was generating old data, weird but fact.
            //now we are sure that we have last version of bvgGrid state
            bvgGrid = _bvgGrid;


            blazorCSS.RemoveSelector(".myGridArea");
            blazorCSS.RemoveSelector(".myContainerFrozen");
            blazorCSS.RemoveSelector(".myContainerNonFrozen");

            GenerateCSSGridmyGridArea();

            GenerateCSSGridFrozenCSS();
            GenerateCSSGridNonFrozenCSS();



            BvgJsInterop.UpdateStyle(StyleID2, GetString(StyleID2));

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

           

            c = new BCssItem(".CDiv", StyleID1);
            c.Values.Add("border-style", "solid");
            c.Values.Add("overflow", "hidden");
            c.Values.Add("white-space", "nowrap");
            c.Values.Add("text-overflow", "ellipsis");
            c.Values.Add("text-align", "center");
            c.Values.Add("vertical-align", "middle");
            c.Values.Add("cursor", "cell");
            c.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            c.Values.Add("line-height", bvgGrid.bvgSettings.RowHeight + "px");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".ColumnDiv", StyleID1);
            c.Values.Add("border-style", "solid");
            c.Values.Add("font-weight", "bold");
            c.Values.Add("display", "flex");
            c.Values.Add("flex-direction", "row");
            c.Values.Add("text-align", "center");
            c.Values.Add("cursor", "cell");
            c.Values.Add("height", bvgGrid.bvgSettings.HeaderHeight + "px");
            c.Values.Add("line-height", bvgGrid.bvgSettings.HeaderHeight + "px");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".ColumnSpan", StyleID1);
            c.Values.Add("text-align", "center");
            c.Values.Add("color", bvgGrid.bvgSettings.HeaderStyle.ForeColor);
            c.Values.Add("height", bvgGrid.bvgSettings.HeaderHeight + "px");
            blazorCSS.Children.Add(c);



            c = new BCssItem(".Border1", StyleID1);
            c.Values.Add("border", "1px solid black");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".Border2", StyleID1);
            c.Values.Add("border-style", "solid");
            c.Values.Add("border-color", "black");
            c.Values.Add("border-width", "1px 1px 1px 0");
            blazorCSS.Children.Add(c);

            GenerateCellCSS();

            GenerateHeaderCSS();

           
        }


        private void GenerateCellCSS()
        {

            c = new BCssItem(".CNF", StyleID1);
            c.Values.Add("background-color", bvgGrid.bvgSettings.NonFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.NonFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.NonFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.NonFrozenCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);


            c = new BCssItem(".CNFAlt", StyleID1);
            c.Values.Add("background-color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);

            c = new BCssItem(".CF", StyleID1);
            c.Values.Add("background-color", bvgGrid.bvgSettings.FrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.FrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.FrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.FrozenCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);


            c = new BCssItem(".CFAlt", StyleID1);
            c.Values.Add("background-color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);

            c = new BCssItem(".CS", StyleID1);
            c.Values.Add("background-color", bvgGrid.bvgSettings.SelectedCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.SelectedCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.SelectedCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.SelectedCellStyle.GetBorderWidth());
            blazorCSS.Children.Add(c);



            c = new BCssItem(".CA", StyleID1);
            c.Values.Add("background-color", bvgGrid.bvgSettings.ActiveCellStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.ActiveCellStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.ActiveCellStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.ActiveCellStyle.BorderWidth + "px");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".CA:focus", StyleID1);
            c.Values.Add("outline", "none");
            blazorCSS.Children.Add(c);

        }


        private void GenerateHeaderCSS()
        {

            c = new BCssItem(".HeaderRegular", StyleID1);
            c.Values.Add("background-color", bvgGrid.bvgSettings.HeaderStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.HeaderStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.HeaderStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.HeaderStyle.BorderWidth + "px;");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".HeaderActive", StyleID1);
            c.Values.Add("background-color", bvgGrid.bvgSettings.ActiveHeaderStyle.BackgroundColor);
            c.Values.Add("color", bvgGrid.bvgSettings.ActiveHeaderStyle.ForeColor);
            c.Values.Add("border-color", bvgGrid.bvgSettings.ActiveHeaderStyle.BorderColor);
            c.Values.Add("border-width", bvgGrid.bvgSettings.ActiveHeaderStyle.BorderWidth + "px;");
            c.Values.Add("cursor", "pointer");
            blazorCSS.Children.Add(c);

        }

        private void GenerateModalCSS()
        {

            c = new BCssItem(".bm-container", StyleID1);
            c.Values.Add("display", "none");
            c.Values.Add("align-items", "center");
            c.Values.Add("justify-content", "center");
            c.Values.Add("position", "fixed");
            c.Values.Add("top", "0");
            c.Values.Add("left", "0");
            c.Values.Add("width", "100%");
            c.Values.Add("height", "100%");
            c.Values.Add("z-index", "90");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-overlay", StyleID1);
            c.Values.Add("display", "block");
            c.Values.Add("position", "fixed");
            c.Values.Add("width", "100%");
            c.Values.Add("height", "100%");
            c.Values.Add("z-index", "95");
            c.Values.Add("background-color", "rgba(0,0,0,0.5)");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-active", StyleID1);
            c.Values.Add("display", "flex");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".blazor-modal", StyleID1);
            c.Values.Add("display", "flex");
            c.Values.Add("flex-direction", "column");
            c.Values.Add("width", "auto");
            c.Values.Add("background-color", "#f2f2f2");
            c.Values.Add("border-radius", "10px");
            c.Values.Add("border", "1px solid black");
            c.Values.Add("padding", "0");
            c.Values.Add("z-index", "99");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-header", StyleID1);
            c.Values.Add("display", "flex");
            c.Values.Add("border-top-left-radius", "10px");
            c.Values.Add("border-top-right-radius", "10px");
           // c.Values.Add("border", "1px solid black");
            c.Values.Add("height", "60px");
            c.Values.Add("align-items", "flex-start");
            c.Values.Add("justify-content", "space-between");
            c.Values.Add("padding", "0");
            c.Values.Add("background-color", "lightgray");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-title", StyleID1);
            c.Values.Add("margin", "1rem");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".bm-close", StyleID1);
            c.Values.Add("padding", "1rem");
            c.Values.Add("margin", "1rem");
            c.Values.Add("margin-right", "0");
            c.Values.Add("font-size", "2rem");
            c.Values.Add("background-color", "transparent");
            c.Values.Add("border", "0");
            c.Values.Add("-webkit-appearance", "none");
            c.Values.Add("cursor", "pointer");
            blazorCSS.Children.Add(c);
         

        }

        private void GenerateCSSGridLayoutCSS()
        {

            c = new BCssItem(".myContainer", StyleID1);
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

            GenerateCSSGridmyGridArea();




            c = new BCssItem(".myHorizontalScroll", StyleID1);
            c.Values.Add("grid-column", "1/1");
            c.Values.Add("grid-row", "2/2");
            blazorCSS.Children.Add(c);

            c = new BCssItem(".myVerticalScroll", StyleID1);
            c.Values.Add("grid-column", "2/2");
            c.Values.Add("grid-row", "1/1");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".myResizer", StyleID1);
            c.Values.Add("grid-column", "2/2");
            c.Values.Add("grid-row", "2/2");
            blazorCSS.Children.Add(c);


            c = new BCssItem(".fixedToTop", StyleID1);
            c.Values.Add("position", "sticky");
            c.Values.Add("top", "0");
            c.Values.Add("z-index", "10");
            blazorCSS.Children.Add(c);

        }

        private void GenerateCSSGridmyGridArea()
        {
            if (bvgGrid.Columns.Any(x => x.IsFrozen))
            {
                c = new BCssItem(".myGridArea", StyleID2);
                c.Values.Add("grid-column", "1/1");
                c.Values.Add("grid-row", "1/1");
                c.Values.Add("display", "grid");
                c.Values.Add("position", "relative");
                c.Values.Add("grid-template-columns", bvgGrid.FrozenTableWidth + "px " + bvgGrid.NonFrozenTableWidth + "px");
                blazorCSS.Children.Add(c);
            }
            else
            {
                c = new BCssItem(".myGridArea", StyleID2);
                c.Values.Add("grid-column", "1/1");
                c.Values.Add("grid-row", "1/1");
                blazorCSS.Children.Add(c);
            }
        }

        private void GenerateCSSGridFrozenCSS()
        {

            c = new BCssItem(".myContainerFrozen", StyleID2);
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
            c.Values.Add("grid-template-rows", bvgGrid.bvgSettings.HeaderHeight + "px repeat(" + bvgGrid.DisplayedRowsCount + ", " + bvgGrid.bvgSettings.RowHeight + "px)");
            c.Values.Add("overflow", "hidden");
            //c.Values.Add("overflow-y", "scroll");
            blazorCSS.Children.Add(c);


        }


        private void GenerateCSSGridNonFrozenCSS()
        {

            c = new BCssItem(".myContainerNonFrozen", StyleID2);
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
            c.Values.Add("grid-template-rows", bvgGrid.bvgSettings.HeaderHeight + "px repeat(" + bvgGrid.DisplayedRowsCount + ", " + bvgGrid.bvgSettings.RowHeight + "px)");
            c.Values.Add("overflow", "hidden");
            //c.Values.Add("overflow-y", "scroll");
            blazorCSS.Children.Add(c);

        }

        private void GenerateGlobalCSS()
        {
            c = new BCssItem("html", StyleID1);
            c.Values.Add("box-sizing", "border-box");
            blazorCSS.Children.Add(c);

            c = new BCssItem("*, *:before, *:after", StyleID1);
            c.Values.Add("box-sizing", "inherit");
            c.Values.Add("padding", "0");
            c.Values.Add("margin", "0");
            blazorCSS.Children.Add(c);

            c = new BCssItem("body", StyleID1);
            c.Values.Add("line-height", "0");
            c.Values.Add("padding", "0");
            c.Values.Add("margin", "0");
            blazorCSS.Children.Add(c);


           
        }

    }
}
