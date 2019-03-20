using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{
    public static class BvgEnums
    {
        public enum MoveDirection
        {
            undefined,
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
            CNF, //CellNonFrozen
            CF, //CellFrozen
            CS, //CellSelected
            CA, //CellActive
        }


        public enum HeaderStyle
        {
            HeaderRegular,
            HeaderActive,
        }


        public static MoveDirection StringToDirection(string par_Direction)
        {

            if (string.IsNullOrEmpty(par_Direction))
            {
                return MoveDirection.undefined;
            }

            switch (par_Direction.ToLower())
            {
                case "right":
                    return MoveDirection.right;
                case "left":
                    return MoveDirection.left;
                case "up":
                    return MoveDirection.up;
                case "down":
                    return MoveDirection.down;
                default:
                    return MoveDirection.undefined;
            }
        }
    }
}
