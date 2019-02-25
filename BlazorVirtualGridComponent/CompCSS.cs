using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent
{
    public class CompCSS : ComponentBase
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

        BCss blazorCSS = new BCss();

        private string GenerateCSS()
        {


            blazorCSS = new BCss();

            BCssItem _TD = new BCssItem("td");
            _TD.Values.Add("margin", "0px");
            _TD.Values.Add("padding", "0px");
            _TD.Values.Add("text-align", "center");
            _TD.Values.Add("vertical-align", "middle");
            blazorCSS.Children.Add(_TD);

            

            BCssItem _CellDiv = new BCssItem(".CellDiv");
            _CellDiv.Values.Add("overflow", "hidden");
            _CellDiv.Values.Add("white-space", "nowrap");
            _CellDiv.Values.Add("text-overflow", "ellipsis");
            _CellDiv.Values.Add("margin", "0px");
            _CellDiv.Values.Add("padding", "0px");
            _CellDiv.Values.Add("height", (bvgGrid.bvgSettings.RowHeight - bvgGrid.bvgSettings.NonFrozenCellStyle.BorderWidth) + "px");
            _CellDiv.Values.Add("line-height", (bvgGrid.bvgSettings.RowHeight - bvgGrid.bvgSettings.NonFrozenCellStyle.BorderWidth) + "px");
            blazorCSS.Children.Add(_CellDiv);



            BCssItem _TH = new BCssItem("th");
            _TH.Values.Add("text-align", "center");
            _TH.Values.Add("border-style", "solid");
            _TH.Values.Add("margin", "0px");
            _TH.Values.Add("padding", "0px");
            blazorCSS.Children.Add(_TH);


            BCssItem _TR = new BCssItem("tr");
            _TR.Values.Add("margin", "0px");
            _TR.Values.Add("padding", "0px");
            blazorCSS.Children.Add(_TR);

            BCssItem _ColumnDiv = new BCssItem(".ColumnDiv");
            _ColumnDiv.Values.Add("display", "flex");
            _ColumnDiv.Values.Add("flex-direction", "row");
            _ColumnDiv.Values.Add("margin", "0px");
            _ColumnDiv.Values.Add("padding", "0px");
            _ColumnDiv.Values.Add("height", (bvgGrid.bvgSettings.HeaderHeight - bvgGrid.bvgSettings.HeaderStyle.BorderWidth) + "px");
            _ColumnDiv.Values.Add("line-height", (bvgGrid.bvgSettings.HeaderHeight - bvgGrid.bvgSettings.HeaderStyle.BorderWidth) + "px");
            blazorCSS.Children.Add(_ColumnDiv);


            BCssItem _ColumnSpan = new BCssItem(".ColumnSpan");
            _ColumnSpan.Values.Add("text-align", "center");
            _ColumnSpan.Values.Add("color", "blue");
            _ColumnSpan.Values.Add("height", bvgGrid.bvgSettings.HeaderHeight + "px");
            blazorCSS.Children.Add(_ColumnSpan);



            BCssItem _Table = new BCssItem(".BorderedTable");
            _Table.Values.Add("border", "1px solid black");
            _Table.Values.Add("margin", "0px");
            _Table.Values.Add("padding", "0px");
            blazorCSS.Children.Add(_Table);


            BCssItem _GridDiv = new BCssItem(".GridDiv");
            _GridDiv.Values.Add("overflow-x", "hidden");
            _GridDiv.Values.Add("overflow-y", "hidden");
            _GridDiv.Values.Add("margin", "0px");
            _GridDiv.Values.Add("padding", "0px");
            blazorCSS.Children.Add(_GridDiv);

            GenerateCellCSS();

            GenerateHeaderCSS();

            return blazorCSS.ToBase64String();

        }


        private void GenerateCellCSS()
        {

            BCssItem _CellNonFrozen = new BCssItem(".CellNonFrozen");
            _CellNonFrozen.Values.Add("border-style", "solid");
            _CellNonFrozen.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            _CellNonFrozen.Values.Add("background-color", bvgGrid.bvgSettings.NonFrozenCellStyle.BackgroundColor);
            _CellNonFrozen.Values.Add("color", bvgGrid.bvgSettings.NonFrozenCellStyle.ForeColor);
            _CellNonFrozen.Values.Add("border-color", bvgGrid.bvgSettings.NonFrozenCellStyle.BorderColor);
            _CellNonFrozen.Values.Add("border-width", bvgGrid.bvgSettings.NonFrozenCellStyle.BorderWidth + "px;");
            _CellNonFrozen.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(_CellNonFrozen);


            BCssItem _CellAlternatedNonFrozen = new BCssItem(".CellNonFrozenAlternated");
            _CellAlternatedNonFrozen.Values.Add("border-style", "solid");
            _CellAlternatedNonFrozen.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            _CellAlternatedNonFrozen.Values.Add("background-color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BackgroundColor);
            _CellAlternatedNonFrozen.Values.Add("color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.ForeColor);
            _CellAlternatedNonFrozen.Values.Add("border-color", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BorderColor);
            _CellAlternatedNonFrozen.Values.Add("border-width", bvgGrid.bvgSettings.AlternatedNonFrozenCellStyle.BorderWidth + "px;");
            _CellAlternatedNonFrozen.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(_CellAlternatedNonFrozen);

            BCssItem _CellFrozen = new BCssItem(".CellFrozen");
            _CellFrozen.Values.Add("border-style", "solid");
            _CellFrozen.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            _CellFrozen.Values.Add("background-color", bvgGrid.bvgSettings.FrozenCellStyle.BackgroundColor);
            _CellFrozen.Values.Add("color", bvgGrid.bvgSettings.FrozenCellStyle.ForeColor);
            _CellFrozen.Values.Add("border-color", bvgGrid.bvgSettings.FrozenCellStyle.BorderColor);
            _CellFrozen.Values.Add("border-width", bvgGrid.bvgSettings.FrozenCellStyle.BorderWidth + "px;");
            _CellFrozen.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(_CellFrozen);


            BCssItem _CellAlternatedFrozen = new BCssItem(".CellFrozenAlternated");
            _CellAlternatedFrozen.Values.Add("border-style", "solid");
            _CellAlternatedFrozen.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            _CellAlternatedFrozen.Values.Add("background-color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BackgroundColor);
            _CellAlternatedFrozen.Values.Add("color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.ForeColor);
            _CellAlternatedFrozen.Values.Add("border-color", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BorderColor);
            _CellAlternatedFrozen.Values.Add("border-width", bvgGrid.bvgSettings.AlternatedFrozenCellStyle.BorderWidth + "px;");
            _CellAlternatedFrozen.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(_CellAlternatedFrozen);

            BCssItem _CellSelected = new BCssItem(".CellSelected");
            _CellSelected.Values.Add("border-style", "solid");
            _CellSelected.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            _CellSelected.Values.Add("background-color", bvgGrid.bvgSettings.SelectedCellStyle.BackgroundColor);
            _CellSelected.Values.Add("color", bvgGrid.bvgSettings.SelectedCellStyle.ForeColor);
            _CellSelected.Values.Add("border-color", bvgGrid.bvgSettings.SelectedCellStyle.BorderColor);
            _CellSelected.Values.Add("border-width", bvgGrid.bvgSettings.SelectedCellStyle.BorderWidth + "px");
            _CellSelected.Values.Add("cursor", "pointer");
            blazorCSS.Children.Add(_CellSelected);


         
            BCssItem _CellActive = new BCssItem(".CellActive");
            _CellActive.Values.Add("border-style", "solid");
            _CellActive.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            _CellActive.Values.Add("background-color", bvgGrid.bvgSettings.ActiveCellStyle.BackgroundColor);
            _CellActive.Values.Add("color", bvgGrid.bvgSettings.ActiveCellStyle.ForeColor);
            _CellActive.Values.Add("border-color", bvgGrid.bvgSettings.ActiveCellStyle.BorderColor);
            _CellActive.Values.Add("border-width", bvgGrid.bvgSettings.ActiveCellStyle.BorderWidth + "px");
            _CellActive.Values.Add("cursor", "pointer");
            _CellActive.Values.Add("outline", bvgGrid.bvgSettings.ActiveCellStyle.OutlineWidth + "px solid " + bvgGrid.bvgSettings.ActiveCellStyle.OutlineColor);
            blazorCSS.Children.Add(_CellActive);


        }


        private void GenerateHeaderCSS()
        {

            BCssItem _HeaderRegular = new BCssItem(".HeaderRegular");
            _HeaderRegular.Values.Add("border-style", "solid");
            _HeaderRegular.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            _HeaderRegular.Values.Add("background-color", bvgGrid.bvgSettings.HeaderStyle.BackgroundColor);
            _HeaderRegular.Values.Add("color", bvgGrid.bvgSettings.HeaderStyle.ForeColor);
            _HeaderRegular.Values.Add("border-color", bvgGrid.bvgSettings.HeaderStyle.BorderColor);
            _HeaderRegular.Values.Add("border-width", bvgGrid.bvgSettings.HeaderStyle.BorderWidth + "px;");
            _HeaderRegular.Values.Add("cursor", "cell");
            blazorCSS.Children.Add(_HeaderRegular);


            BCssItem _HeaderActive = new BCssItem(".HeaderActive");
            _HeaderActive.Values.Add("border-style", "solid");
            _HeaderActive.Values.Add("height", bvgGrid.bvgSettings.RowHeight + "px");
            _HeaderActive.Values.Add("background-color", bvgGrid.bvgSettings.ActiveHeaderStyle.BackgroundColor);
            _HeaderActive.Values.Add("color", bvgGrid.bvgSettings.ActiveHeaderStyle.ForeColor);
            _HeaderActive.Values.Add("border-color", bvgGrid.bvgSettings.ActiveHeaderStyle.BorderColor);
            _HeaderActive.Values.Add("border-width", bvgGrid.bvgSettings.ActiveHeaderStyle.BorderWidth + "px;");
            _HeaderActive.Values.Add("cursor", "pointer");
            blazorCSS.Children.Add(_HeaderActive);

        }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            EnabledRender = false;

            base.BuildRenderTree(builder);

            int k = 0;

            // builder.AddMarkupContent(k++,"<style>.my {color:red}");

            //builder.OpenElement(k++, "style");
            //builder.AddContent(k++, GenerateCSS());
            //builder.CloseElement();


            builder.OpenElement(k++, "link");
            builder.AddAttribute(k++, "rel", "stylesheet");
            builder.AddAttribute(k++, "href", "data:text/css;base64," + GenerateCSS());
            builder.AddAttribute(k++, "type", "text/css");
            builder.CloseElement();


        }




    }
}
