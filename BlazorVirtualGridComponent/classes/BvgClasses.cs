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
        public int x { get; set; }
        public int y { get; set; }

        public BvgPointInt(int _x = 0, int _y = 0)
        {
            x = _x;
            y = _y;
        }
    }

    public class BvgPointDouble
    {
        public double x { get; set; }
        public double y { get; set; }

        public BvgPointDouble(double _x = 0, double _y = 0)
        {
            x = _x;
            y = _y;
        }
    }

    public class BvgSizeInt
    {
        public int w { get; set; }
        public int h { get; set; }

        public BvgSizeInt(int _w = 0, int _h = 0)
        {
            w = _w;
            h = _h;
        }
    }

    public class BvgSizeDouble
    {
        public double w { get; set; }
        public double h { get; set; }

        public BvgSizeDouble(double _w = 0, double _h = 0)
        {
            w = _w;
            h = _h;
        }
    }


    public class BvgGridTransferableState<T>
    {
        public bool ContaintState { get; set; } = false;

        public bool HasMeasuredRect { get; set; } = false;
        public BvgSizeDouble bvgSize { get; set; } = new BvgSizeDouble();


        public BvgGridTransferableState()
        {

        }

        public BvgGridTransferableState(BvgGrid<T> bvgGrid)
        {
            ContaintState = true;

            HasMeasuredRect = bvgGrid.HasMeasuredRect;
            if (bvgGrid.HasMeasuredRect)
            {
                bvgSize = bvgGrid.bvgSize;
            }


            
        }

        
    }
}
