using BlazorVirtualGridComponent;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorVirtualGrid.Pages
{
    public class Index1Base : ComponentBase
    {


        Random rnd1 = new Random();

        public string TableName1 { get; set; } = "Table 1";

        public IList<MyItemVD> list1 { get; set; } = new List<MyItemVD>();

        public BvgSettings bvgSettings1 { get; set; } = new BvgSettings();
    

        protected override void OnInit()
        {

            FillList(30);


            bvgSettings1 = new BvgSettings()
            {
                LayoutFixedOrAuto = true,
                CompHeight = 300,
                CompWidth = 700,
            };

            ConfigureBvgSettings1();

            bvgSettings1.SortedColumn = Tuple.Create(true, nameof(MyItemVD.ID), true);

            bvgSettings1.FrozenColumnsListOrdered
                .Add(nameof(MyItemVD.ID))
                .Add(nameof(MyItemVD.Date));

            bvgSettings1.ColumnWidthsDictionary
                .Add(Tuple.Create(nameof(MyItemVD.ID), (ushort)100))
                .Add(Tuple.Create(nameof(MyItemVD.Date), (ushort)100));

            base.OnInit();
        }


        public void ConfigureBvgSettings1()
        {

            bvgSettings1.NonFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#cccccc",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings1.AlternatedNonFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#a7f1a7",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings1.FrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "silver",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings1.AlternatedFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "lightgreen",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings1.SelectedCellStyle = new BvgStyle
            {
                BackgroundColor = "#4d88ff",
                ForeColor = "white",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings1.ActiveCellStyle = new BvgStyle
            {
                BackgroundColor = "#4d88ff",
                ForeColor = "white",
                BorderColor = "black",
                BorderWidth = 1,
                OutlineColor = "blue",
                OutlineWidth = 3,
            };
            bvgSettings1.HeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "blue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings1.ActiveHeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings1.RowHeight = 40;
            bvgSettings1.HeaderHeight = 50;


            //bvgSettings1.VerticalScrollStyle = new BvgStyleScroll
            //{
            //    ButtonColor = "green",
            //    ThumbColor = "red",
            //    ThumbWayColor = "lightgreen",
            //};

            //bvgSettings1.HorizontalScrollStyle = new BvgStyleScroll
            //{
            //    ButtonColor = "green",
            //    ThumbColor = "red",
            //    ThumbWayColor = "lightgreen",
            //};

        }

        private void FillList(int c)
        {

            list1 = new List<MyItemVD>();
            for (int i = 1; i <= c; i++)
            {
                list1.Add(new MyItemVD
                {
                    ID = (ushort)i,
                    FrozenCol = "Item " + i,

                    SomeBool = rnd1.Next(0, 5) > 1,
                    Date = DateTime.Now.AddDays(-rnd1.Next(1, 5000)).AddHours(-rnd1.Next(1, 5000)).AddSeconds(-rnd1.Next(1, 5000)),
                    Col1 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    Col2 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    Col3 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    Col4 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    Col5 = Guid.NewGuid().ToString("d").Substring(1, 4),

                });
            }
        }

        public void Cmd1()
        {
            bvgSettings1.bSortStyle.SortedDirectionColor = "green";
            StateHasChanged();
        }

    
        public class MyItemVD
        {
            public int ID { get; set; }
            public string FrozenCol { get; set; }
           
            public DateTime Date { get; set; }
            public bool SomeBool { get; set; }

            public string Col1 { get; set; }

            public string Col2 { get; set; }
            public string Col3 { get; set; }

            public string Col4 { get; set; }
            public string Col5 { get; set; }
        }


      
    }
}
