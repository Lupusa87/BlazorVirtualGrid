using BlazorScrollbarComponent.classes;
using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGrid.Pages
{
    public class StyleDesignerBase:ComponentBase
    {
        [Parameter]
        public BvgSettings bvgSettings { get; set; } = new BvgSettings();



        public BsbSettings bsbSettingsVertical { get; set; } = new BsbSettings();

        public BsbSettings bsbSettingsHorizontal { get; set; } = new BsbSettings();

        protected CssHelper cssHelper;

        protected BvgColumn bvgColumnSortUp { get; set; } = new BvgColumn();

        protected BvgColumn bvgColumnSortDown { get; set; } = new BvgColumn();

        public int Height { get; set; } = 300;

        public int Width { get; set; } = 300;

        public int RowsTotalCount { get; set; } = 6;

        public int RowHeightOriginal { get; set; } = 40;

        protected override void OnInit()
        {
            cssHelper = new CssHelper(bvgSettings);

            bsbSettingsVertical = new BsbSettings("VericalScroll")
            {
                VerticalOrHorizontal = true,
                width = 16,
                height = Height - bvgSettings.HeaderStyle.BorderWidth * 2,
                ScrollVisibleSize = 100,
                ScrollTotalSize = 300,
                bsbStyle = new BsbStyle
                {
                    ButtonColor = bvgSettings.VerticalScrollStyle.ButtonColor,
                    ThumbColor = bvgSettings.VerticalScrollStyle.ThumbColor,
                    ThumbWayColor = bvgSettings.VerticalScrollStyle.ThumbWayColor,
                }
            };
            bsbSettingsVertical.initialize();

            bsbSettingsHorizontal = new BsbSettings("HorizontalScroll")
            {

                VerticalOrHorizontal = false,
                width = Width,
                height = 16,
                ScrollVisibleSize = 100,
                ScrollTotalSize = 300,
                bsbStyle = new BsbStyle
                {
                    ButtonColor = bvgSettings.HorizontalScrollStyle.ButtonColor,
                    ThumbColor = bvgSettings.HorizontalScrollStyle.ThumbColor,
                    ThumbWayColor = bvgSettings.HorizontalScrollStyle.ThumbWayColor,
                }
            };
            bsbSettingsHorizontal.initialize();

            base.OnInit();
        }


        public string getStyleMainTable()
        {
            return string.Concat("table-layout:auto;width:", Width, "px,height:", Height, "px");
        }

        public string getStyleMainTd()
        {
            return string.Concat("width:", Width, "px,height:", Height, "px");
        }

        public string getStyleTh()
        {
            return HeaderStyle.HeaderRegular.ToString();
        }


        
    }
}
