using BlazorSplitterComponent;
using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class CompColumn<TItem> : ComponentBase, IDisposable
    {

      

        [Parameter]
        protected BvgColumn<TItem> bvgColumn { get; set; }

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

        private void BvgColumn_PropertyChanged()
        {
            //EnabledRender = true;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
           
            //EnabledRender = false;

            base.BuildRenderTree(builder);

            if (bvgColumn.PropertyChanged == null)
            {
                bvgColumn.PropertyChanged = BvgColumn_PropertyChanged;
            }

            int k = -1;

          
            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "id", "divCol" + bvgColumn.ID);
            builder.AddAttribute(k++, "class", "ColumnDiv fixedToTop " + bvgColumn.CssClass);


            builder.OpenElement(k++, "div"); //to arrange text in center
            builder.AddAttribute(k++, "style", string.Concat("width:", (bvgColumn.bvgGrid.bvgSettings.bSortStyle.width + 5), "px"));
            builder.CloseElement(); //div

            builder.OpenElement(k++, "span");
            builder.AddAttribute(k++, "id", "spCol" + bvgColumn.ID);
            builder.AddAttribute(k++, "class", "ColumnSpan");
            builder.AddAttribute(k++, "style", string.Concat("width:", bvgColumn.ColWidthSpan, "px"));
            builder.AddAttribute(k++, "onmousedown", Clicked);
            builder.AddContent(k++, bvgColumn.prop.Name);
            builder.CloseElement(); //span


            builder.OpenComponent<CompSort<TItem>>(k++);
            builder.AddAttribute(k++, "bvgColumn", bvgColumn);
            builder.AddAttribute(k++, "IsNotHidden", bvgColumn.IsSorted);
            builder.CloseComponent();



            builder.OpenComponent<CompBlazorSplitter>(k++);
            builder.AddAttribute(k++, "bsSettings", bvgColumn.bsSettings);
            builder.AddAttribute(k++, "OnPositionChange", new Action<bool, int, int>(OnPositionChange));
            builder.AddComponentReferenceCapture(k++, (c) =>
            {
                bvgColumn.BSplitter = c as CompBlazorSplitter;
            });
            builder.CloseComponent();


            builder.CloseElement(); //div

           
        }


        public void Clicked(UIMouseEventArgs e)
        {
            EnsureIdentity();
            // bvgColumn.bvgGrid.SelectColumn(bvgColumn);
            bvgColumn.bvgGrid.SortColumn(bvgColumn);
        }


        public void EnsureIdentity()
        {
            //because updating cols is cheap operation we call it in all case after updating from JS
            //whithout this columns does not update and we need to ensure identity
            //also sort icon does not update and stays in old position

                //////when virtualization updates column in memory and in dom from js we don't do blazor component refresh,
                //////so it will keep old data
                ////if (bvgColumn.prop.Name != bvgColumn.bvgGrid.Columns.Single(x => x.ID == bvgColumn.ID).prop.Name)
                ////{
                ////    bvgColumn = bvgColumn.bvgGrid.Columns.Single(x => x.ID == bvgColumn.ID);
                ////}
        }

        public void OnPositionChange(bool b, int index, int p)
        {

            EnsureIdentity();


            if (!b)
            {



                if (bvgColumn.ColWidth == bvgColumn.bvgGrid.bvgSettings.ColWidthMin && p <= 0)
                {
                    return;
                }


                if (bvgColumn.ColWidth == bvgColumn.bvgGrid.bvgSettings.ColWidthMax && p >= 0)
                {
                    return;
                }

              

                ushort old_Value_col = bvgColumn.ColWidth;

                bvgColumn.ColWidth = (ushort)(bvgColumn.ColWidth + p);


                if (bvgColumn.ColWidth < bvgColumn.bvgGrid.bvgSettings.ColWidthMin)
                {
                    bvgColumn.ColWidth = bvgColumn.bvgGrid.bvgSettings.ColWidthMin;
                }
                if (bvgColumn.ColWidth > bvgColumn.bvgGrid.bvgSettings.ColWidthMax)
                {
                    bvgColumn.ColWidth = bvgColumn.bvgGrid.bvgSettings.ColWidthMax;
                }

               
                if (bvgColumn.ColWidth != old_Value_col)
                {
                    
                    bvgColumn.bvgGrid.ColumnsOrderedList.Single(x => x.prop.Name.Equals(bvgColumn.prop.Name)).ColWidth = bvgColumn.ColWidth;
                  
                    if (bvgColumn.IsFrozen)
                    {
                        bvgColumn.bvgGrid.ColumnsOrderedListFrozen.Single(x => x.prop.Name.Equals(bvgColumn.prop.Name)).ColWidth = bvgColumn.ColWidth;

                    }
                    else
                    {
                        bvgColumn.bvgGrid.ColumnsOrderedListNonFrozen.Single(x => x.prop.Name.Equals(bvgColumn.prop.Name)).ColWidth = bvgColumn.ColWidth;
                        bvgColumn.bvgGrid.UpdateNonFrozenColwidthSumsByElement();
                    }



                    double currScrollPosition = bvgColumn.bvgGrid.HorizontalScroll.compBlazorScrollbar.CurrentPosition;

                    int k = bvgColumn.bvgGrid.DisplayedColumnsCount;

                    bvgColumn.bvgGrid.CalculateWidths();

                    if (k == bvgColumn.bvgGrid.DisplayedColumnsCount)
                    {

                        bvgColumn.bvgGrid.OnColumnResize?.Invoke();

                        bvgColumn.bvgGrid.HorizontalScroll.compBlazorScrollbar.SetScrollPosition(currScrollPosition);
                    }
                    else
                    {
                        bvgColumn.bvgGrid.compBlazorVirtualGrid.Refresh(false, false);
                    }
                }


            }
           
        }




        public void Dispose()
        {

        }
    }

}