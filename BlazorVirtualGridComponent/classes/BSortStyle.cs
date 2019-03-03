using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{
    public class BSortStyle
    {

        public int width { get; set; } 

        internal int height { get; private set; }

        public int PaddingHorizontal { get; private set; } 

        public int PaddingVertical { get; set; } 

        public string SortedDirectionColor { get; set; } = "#FF0000";

        public string UnSortedDirectionColor { get; set; } = "none";

        public string BorderColor { get; set; } = "none";

        internal string UpPoligonPoints { get; private set; }
        internal string DownPoligonPoints { get; private set; }


        public BSortStyle(int w=20, int h=40, int ph=3, int pv=8)
        {
            if (w > 0 && h > 0)
            {
                width = w;
                height = h;
                PaddingHorizontal = ph;
                PaddingVertical = pv;
                CalculatePoligonPoints();
            }
            else
            {
                BvgJsInterop.Alert("Not set Sort icon width and/or height!");
            }
        }


        private void CalculatePoligonPoints()
        {

            if (width > 0 && height > 0)
            {
                // builder.AddAttribute(k++, "points", "4,20 10,4, 16,20");

                double halfHeight = height / 2.0;
                double halfWidth = width / 2.0;

                UpPoligonPoints = string.Concat(PaddingHorizontal, ",", halfHeight,
                    " ", halfWidth, ",", PaddingVertical,
                    " ", width - PaddingHorizontal, ",", halfHeight);

                DownPoligonPoints = string.Concat(PaddingHorizontal, ",", halfHeight,
                    " ", halfWidth, ",", height - PaddingVertical,
                    " ", width - PaddingHorizontal, ",", halfHeight);

                //builder.AddAttribute(k++, "points", "4,20 10,36, 16,20");
            }
        }
    }
}
