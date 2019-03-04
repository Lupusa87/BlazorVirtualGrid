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


        public enum ModalForm
        {
            ColumnsManager,
            StyleDesigner,
            FilterManager,
        }


        public enum CellStyle
        {
            CellNonFrozen,
            CellFrozen,
            CellSelected,
            CellActive,
        }


        public enum HeaderStyle
        {
            HeaderRegular,
            HeaderActive,
        }
    }
}
