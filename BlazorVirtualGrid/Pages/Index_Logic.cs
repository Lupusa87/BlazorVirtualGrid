using BlazorVirtualGridComponent;
using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorVirtualGrid.Pages
{
    public class Index_Logic:ComponentBase
    {


        Random rnd1 = new Random();

        public CompBlazorVirtualGrid<MyItem> CurrBVG1;
        public CompBlazorVirtualGrid<MyItem2> CurrBVG2;

        public bool FirstOrSecond = true;


        public string TableName1 { get; set; } = "Table 1";
        public string TableName2 { get; set; } = "Table 2";

        public IList<MyItem> list1 { get; set; } = new List<MyItem>();

        public IList<MyItem2> list2 { get; set; } = new List<MyItem2>();

        public BvgSettings bvgSettings1 { get; set; } = new BvgSettings();
        public BvgSettings bvgSettings2 { get; set; } = new BvgSettings();

        Dictionary<string, ValuesContainer<Tuple<string, ushort>>> SavedColumnWitdths_Dict = new Dictionary<string, ValuesContainer<Tuple<string, ushort>>>();


       
        protected override void OnInit()
        {
            

            FillList(200, 300);


            bvgSettings1 = new BvgSettings();

            ConfigureBvgSettings1();


            bvgSettings1.FrozenColumnsListOrdered
                .Add(nameof(MyItem.N3))
                .Add(nameof(MyItem.Date));

            //bvgSettings1.ColumnWidthsDictionary
            //    .Add(Tuple.Create(nameof(MyItem.N3), 200))
            //    .Add(Tuple.Create(nameof(MyItem.Date), 200))
            //    .Add(Tuple.Create(nameof(MyItem.N1), 300));

            PropertyInfo[] props = typeof(MyItem).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var item in props.Where(x=>x.Name!="Date"))
            {
                bvgSettings1.ColumnWidthsDictionary
                .Add(Tuple.Create(item.Name, (ushort)rnd1.Next(60, 200)));
            }

            bvgSettings1.HiddenColumns
                .Add(nameof(MyItem.SomeBool));

            base.OnInit();
        }


        public void GetColumnsWidth1()
        {

            if (SavedColumnWitdths_Dict.ContainsKey(CurrBVG1.bvgGrid.Name))
            {
                SavedColumnWitdths_Dict[CurrBVG1.bvgGrid.Name] = CurrBVG1.bvgGrid.GetColumnWidths();
            }
            else
            {
                SavedColumnWitdths_Dict.Add(CurrBVG1.bvgGrid.Name, CurrBVG1.bvgGrid.GetColumnWidths());
            }
        }


        public void GetColumnsWidth2()
        {

            if (SavedColumnWitdths_Dict.ContainsKey(CurrBVG2.bvgGrid.Name))
            {
                SavedColumnWitdths_Dict[CurrBVG2.bvgGrid.Name] = CurrBVG2.bvgGrid.GetColumnWidths();
            }
            else
            {
                SavedColumnWitdths_Dict.Add(CurrBVG2.bvgGrid.Name, CurrBVG2.bvgGrid.GetColumnWidths());
            }
        }


        public void CmdNewList1()
        {
            FirstOrSecond = true;


            GetColumnsWidth1();

            FillList(200, 300);


            bvgSettings1 = new BvgSettings();

            ConfigureBvgSettings1();

            bvgSettings1.FrozenColumnsListOrdered
                .Add(nameof(MyItem.N3))
                .Add(nameof(MyItem.Date));
           


            if (SavedColumnWitdths_Dict.ContainsKey(TableName1))
            {
                bvgSettings1.ColumnWidthsDictionary = SavedColumnWitdths_Dict[TableName1];
            }


            StateHasChanged();


        }

        public void Cmdqwer()
        {

        }
            

        public void CmdNewList2()
        {
            FirstOrSecond = false;

            GetColumnsWidth2();

            FillList2(200, 300);


            bvgSettings1 = new BvgSettings();

            ConfigureBvgSettings2();

            bvgSettings2.FrozenColumnsListOrdered
                .Add(nameof(MyItem2.Gender));


            bvgSettings2.HiddenColumns
                .Add(nameof(MyItem2.LastName));


            if (SavedColumnWitdths_Dict.ContainsKey(TableName1))
            {
                bvgSettings1.ColumnWidthsDictionary = SavedColumnWitdths_Dict[TableName1];
            }


            StateHasChanged();
        }

        public void CmdPinColList1()
        {
            
        }

        public void CmdPinColList2()
        {
            
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
                BorderColor = "brown",
                BorderWidth = 2,
            };
            bvgSettings1.ActiveHeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 2,
            };
            bvgSettings1.RowHeight = 40;
            bvgSettings1.HeaderHeight = 50;
           
        }

        public void ConfigureBvgSettings2()
        {

            bvgSettings2.NonFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#cccccc",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings2.AlternatedNonFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "#a7f1a7",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings2.FrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "silver",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings2.AlternatedFrozenCellStyle = new BvgStyle
            {
                BackgroundColor = "lightgreen",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings2.SelectedCellStyle = new BvgStyle
            {
                BackgroundColor = "#4d88ff",
                ForeColor = "white",
                BorderColor = "black",
                BorderWidth = 1,
            };
            bvgSettings2.ActiveCellStyle = new BvgStyle
            {
                BackgroundColor = "#4d88ff",
                ForeColor = "white",
                BorderColor = "black",
                BorderWidth = 1,
                OutlineColor = "blue",
                OutlineWidth = 3,
            };
            bvgSettings2.HeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "blue",
                BorderColor = "brown",
                BorderWidth = 2,
            };
            bvgSettings2.ActiveHeaderStyle = new BvgStyle
            {
                BackgroundColor = "#b3b3b3",
                ForeColor = "darkblue",
                BorderColor = "black",
                BorderWidth = 2,
            };
            bvgSettings2.RowHeight = 40;
            bvgSettings2.HeaderHeight = 50;

        }



        private void FillList(int Par_Min, int Par_Max)
        {

            list1 = new List<MyItem>();
            for (int i = 1; i <= rnd1.Next(Par_Min, Par_Max); i++)
            {
                list1.Add(new MyItem
                {
                    ID = (ushort)i,
                    Name = "Item " + i,

                    SomeBool = rnd1.Next(0, 5) == 0,
                    Date = DateTime.Now.AddDays(-rnd1.Next(1, 5000)).AddHours(-rnd1.Next(1, 5000)).AddSeconds(-rnd1.Next(1, 5000)),
                    N1 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N2 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N3 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N4 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N5 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N6 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N7 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N8 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N9 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N10 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N11 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N12 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N13 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N14 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N15 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N16 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N17 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N18 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N19 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N20 = Guid.NewGuid().ToString("d").Substring(1, 4),
                });
            }
        }

        private void FillList2(int Par_Min, int Par_Max)
        {
            list2 = new List<MyItem2>();
            for (int i = 1; i <= rnd1.Next(Par_Min, Par_Max); i++)
            {
                list2.Add(new MyItem2
                {
                    ID = (ushort)i,
                    FirstName = Guid.NewGuid().ToString("d").Substring(1, 4),
                    LastName = Guid.NewGuid().ToString("d").Substring(1, 4),
                    Gender = rnd1.Next(0, 5) == 0,
                    BirthDate = DateTime.Now.AddDays(-rnd1.Next(1, 5000)).AddHours(-rnd1.Next(1, 5000)).AddSeconds(-rnd1.Next(1, 5000)),
                    
                });
            }
        }

        public class MyItem
        {
            public ushort ID { get; set; }
            public string Name { get; set; }
            public bool SomeBool { get; set; }
            public DateTime Date { get; set; }
            public string N1 { get; set; }
            public string N2 { get; set; }
            public string N3 { get; set; }
            public string N4 { get; set; }
            public string N5 { get; set; }
            public string N6 { get; set; }
            public string N7 { get; set; }
            public string N8 { get; set; }
            public string N9 { get; set; }
            public string N10 { get; set; }
            public string N11 { get; set; }
            public string N12 { get; set; }
            public string N13 { get; set; }
            public string N14 { get; set; }
            public string N15 { get; set; }
            public string N16 { get; set; }
            public string N17 { get; set; }
            public string N18 { get; set; }
            public string N19 { get; set; }
            public string N20 { get; set; }


        }


        public class MyItem2
        {
            public ushort ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public bool Gender { get; set; }
        }
    }
}
