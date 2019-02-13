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
        public BvgColumn bvgColumn { get; set; }



        protected override void OnInit()
        {
           
        }

        private void BvgColumn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {


            bvgColumn.PropertyChanged += BvgColumn_PropertyChanged;


            int k = -1;

            builder.OpenElement(k++, "th");

            builder.AddAttribute(k++, "style", bvgColumn.GetStyleTh());
            builder.AddAttribute(k++, "onclick", Clicked);



            builder.OpenElement(k++, "div");
            builder.AddAttribute(k++, "style", bvgColumn.GetStyleDiv());


            builder.OpenElement(k++, "span");
            builder.AddAttribute(k++, "style", bvgColumn.GetStyleSpan());
            builder.AddContent(k++, bvgColumn.Name);
            builder.CloseElement(); //span

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


            base.BuildRenderTree(builder);
        }


        public void Clicked(UIMouseEventArgs e)
        {

            bvgColumn.bvgGrid.SelectColumn(bvgColumn);
           
        }




        public void OnPositionChange(bool b, int index, int p)
        {
            if (!b)
            {


                double old_Value_col = bvgColumn.ColWidth;

                bvgColumn.ColWidth += p;


                if (bvgColumn.ColWidth < bvgColumn.ColWidthMin)
                {
                    bvgColumn.ColWidth = bvgColumn.ColWidthMin;
                }
                if (bvgColumn.ColWidth > bvgColumn.ColWidthMax)
                {
                    bvgColumn.ColWidth = bvgColumn.ColWidthMax;
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
            bvgColumn.PropertyChanged -= BvgColumn_PropertyChanged;
        }
    }
}
