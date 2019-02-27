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
    public class CompColumn : ComponentBase, IDisposable
    {



        [Parameter]
        protected BvgColumn bvgColumn { get; set; }

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

            builder.OpenElement(k++, "th");
            builder.AddAttribute(k++, "class", bvgColumn.CssClass);
            builder.AddAttribute(k++, "onclick", Clicked);



            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "id", "divCol" + bvgColumn.ID);
            builder.AddAttribute(k++, "class", "ColumnDiv");
            builder.AddAttribute(k++, "style", "width:" + bvgColumn.ColWidthDiv + "px");


            builder.OpenElement(k++, "span");
            builder.AddAttribute(k++, "id", "spCol"+bvgColumn.ID);
            builder.AddAttribute(k++, "class", "ColumnSpan");
            builder.AddAttribute(k++, "style", "width:" + bvgColumn.ColWidthSpan + "px");
            builder.AddContent(k++, bvgColumn.prop.Name);
            builder.CloseElement(); //span

            if (bvgColumn.IsSorted)
            {
                builder.OpenComponent<CompSort>(k++);
                builder.AddAttribute(k++, "bvgColumn", bvgColumn);
                builder.CloseComponent();
            }


            builder.OpenComponent<CompBlazorSplitter>(k++);
            builder.AddAttribute(k++, "bsSettings", bvgColumn.bsSettings);
            builder.AddAttribute(k++, "OnPositionChange", new Action<bool, int, int>(OnPositionChange));
            builder.AddComponentReferenceCapture(k++, (c) =>
            {
                bvgColumn.BSplitter = c as CompBlazorSplitter;
            });
            builder.CloseComponent();

            
            builder.CloseElement(); //div


            builder.CloseElement(); //th


            
        }


        public void Clicked(UIMouseEventArgs e)
        {

            // bvgColumn.bvgGrid.SelectColumn(bvgColumn);
            bvgColumn.bvgGrid.SortColumn(bvgColumn);
        }




        public void OnPositionChange(bool b, int index, int p)
        {
            if (!b)
            {


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
                    StateHasChanged();

                    foreach (var item in bvgColumn.bvgGrid.Rows)
                    {
                        BvgCell c = item.Cells.Single(x => x.bvgColumn.ID == bvgColumn.ID);
                        c.InvokePropertyChanged();
                    }



                    if (bvgColumn.IsFrozen)
                    {
                        bvgColumn.bvgGrid.FrozenTableWidth += p;
                        bvgColumn.bvgGrid.NotFrozenTableWidth -= p;
                        bvgColumn.bvgGrid.InvokePropertyChanged();
                    }
                    else
                    {
                        bvgColumn.bvgGrid.UpdateHorizontalScroll();
                    }
                }

            }

        }


        public void Dispose()
        {
            
        }
    }
}
