using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{
    public static class BvgEnums
    {
        public enum MoveDirection
        {
            right,
            left,
            up,
            down
        }


        public enum CellStyle
        {
            CellRegular,
            CellSelected,
            CellActive,
        }

    }
}
