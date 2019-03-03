using BlazorVirtualGridComponent.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGrid.Services
{
    public class GridSettingsService
    {
        private bool Isconfigured1 = false;
        private bool Isconfigured2 = false;

        public BvgSettings bvgSettings1 { get; set; } = new BvgSettings();
        public BvgSettings bvgSettings2 { get; set; } = new BvgSettings();

        public void ConfigureBvgSettings1()
        {
            if (Isconfigured1)
            {
                return;
            }

            Isconfigured1 = true;

            bvgSettings1.NonFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#cccccc",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings1.AlternatedNonFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#a7f1a7",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings1.FrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#C0C0C0",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings1.AlternatedFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#90EE90",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings1.SelectedCellStyle = new BvgStyle
            {
                BackgroundColor = "#4d88ff",
                ForeColor = "#FFFFFF",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings1.ActiveCellStyle = new BvgStyle
            {
                BackgroundColor = "#4d88ff",
                ForeColor = "#FFFFFF",
                BorderColor = "#000000",
                BorderWidth = 1,
                OutlineColor = "#0000FF",
                OutlineWidth = 3,
            };
            bvgSettings1.HeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "#0000FF",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings1.ActiveHeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings1.RowHeight = 40;
            bvgSettings1.HeaderHeight = 50;


            //bvgSettings1.VerticalScrollStyle = new BvgStyleScroll
            //{
            //    ButtonColor = "#008000",
            //    ThumbColor = "#FF0000",
            //    ThumbWayColor = "#90EE90",
            //};

            //bvgSettings1.HorizontalScrollStyle = new BvgStyleScroll
            //{
            //    ButtonColor = "#008000",
            //    ThumbColor = "#FF0000",
            //    ThumbWayColor = "#90EE90",
            //};

        }

        public void ConfigureBvgSettings2()
        {

            if (Isconfigured2)
            {
                return;
            }

            Isconfigured2 = true;

            bvgSettings2.NonFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#cccccc",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings2.AlternatedNonFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#a7f1a7",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings2.FrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#C0C0C0",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings2.AlternatedFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#90EE90",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings2.SelectedCellStyle = new BvgStyle
            {
                BackgroundColor = "#4d88ff",
                ForeColor = "#FFFFFF",
                BorderColor = "#000000",
                BorderWidth = 1,
            };
            bvgSettings2.ActiveCellStyle = new BvgStyle
            {
                BackgroundColor = "#4d88ff",
                ForeColor = "#FFFFFF",
                BorderColor = "#000000",
                BorderWidth = 1,
                OutlineColor = "#0000FF",
                OutlineWidth = 3,
            };
            bvgSettings2.HeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "#0000FF",
                BorderColor = "brown",
                BorderWidth = 2,
            };
            bvgSettings2.ActiveHeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "#00008B",
                BorderColor = "#000000",
                BorderWidth = 2,
            };
            bvgSettings2.RowHeight = 40;
            bvgSettings2.HeaderHeight = 50;

        }

    }
}
