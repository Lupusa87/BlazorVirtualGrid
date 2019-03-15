using BlazorScrollbarComponent;
using BlazorVirtualGridComponent.businessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgClasses
    {
    }

    public class BvgPointInt
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BvgPointInt(int pX = 0, int pY = 0)
        {
            X = pX;
            Y = pY;
        }
    }

    public class BvgPointDouble
    {
        private double _X { get; set; }

        public double X {
            get
            {
                return _X;
            }
            set
            {
                _X = Math.Round(value, 3);
            }
        }

        private double _Y { get; set; }

        public double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = Math.Round(value, 3);
            }
        }

        public BvgPointDouble(double pX = 0, double pY = 0)
        {
            _X = Math.Round(pX,3);
            _Y = Math.Round(pY,3);
        }
    }

    public class BvgSizeInt
    {
        public int W { get; set; }
        public int H { get; set; }

        public BvgSizeInt(int pW = 0, int pH = 0)
        {
            W = pW;
            H = pH;
        }
    }

    public class BvgSizeDouble
    {
        private double _W { get; set; }

        public double W
        {
            get
            {
                return _W;
            }
            set
            {
                _W = Math.Round(value, 3);
            }
        }

        private double _H { get; set; }

        public double H
        {
            get
            {
                return _H;
            }
            set
            {
                _H = Math.Round(value, 3);
            }
        }


        public BvgSizeDouble(double pW = 0, double pH = 0)
        {
            _W = Math.Round(pW,3);
            _H = Math.Round(pH,3);
        }
    }


    public class BvgGridTransferableState<TItem>
    {
        public bool ContaintState { get; set; } = false;

        public bool HasMeasuredRect { get; set; } = false;
        public BvgSizeDouble bvgSize { get; set; } = new BvgSizeDouble();

        public ColProp[] ColumnsOrderedList { get; set; }
        public ColProp[] ColumnsOrderedListFrozen { get; set; }
        public ColProp[] ColumnsOrderedListNonFrozen { get; set; }



        public CompBlazorScrollbar compBlazorScrollbarHorizontal { get; set; }
        public CompBlazorScrollbar compBlazorScrollbarVerical { get; set; }

        public CssHelper<TItem> cssHelper { get; set; }

        public BvgGridTransferableState()
        {

        }

        public BvgGridTransferableState(BvgGrid<TItem> bvgGrid, bool SaveColumns = true)
        {
            ContaintState = true;

            HasMeasuredRect = bvgGrid.HasMeasuredRect;
            if (bvgGrid.HasMeasuredRect)
            {
                bvgSize = bvgGrid.bvgSize;
            }

            if (SaveColumns)
            {
                ColumnsOrderedList = bvgGrid.ColumnsOrderedList;
                ColumnsOrderedListFrozen = bvgGrid.ColumnsOrderedListFrozen;
                ColumnsOrderedListNonFrozen = bvgGrid.ColumnsOrderedListNonFrozen;
            }

            cssHelper = bvgGrid.cssHelper;


            if (bvgGrid.HorizontalScroll.compBlazorScrollbar != null)
            {
                compBlazorScrollbarHorizontal = bvgGrid.HorizontalScroll.compBlazorScrollbar;
            }


            if (bvgGrid.VerticalScroll.compBlazorScrollbar != null)
            {
                compBlazorScrollbarVerical = bvgGrid.VerticalScroll.compBlazorScrollbar;
            }
        }

        
    }
}
