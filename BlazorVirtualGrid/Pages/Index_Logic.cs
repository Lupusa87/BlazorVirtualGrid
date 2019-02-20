using BlazorVirtualGridComponent;
using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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

        Dictionary<string, Dictionary<string, double>> SavedColumnWitdths_Dict = new Dictionary<string, Dictionary<string, double>>();


       
        protected override void OnInit()
        {
            ConfigureBvgSettings1();
            ConfigureBvgSettings2();

            FillList(200, 300);

       

            //List<string> frozenCols = new List<string>
            //{
            //    nameof(MyItem.N3),
            //    nameof(MyItem.Date)
            //};

            //_bvgGrid.FreezeColumns(frozenCols, false);


            //_bvgGrid.SetWidthToColumn(nameof(MyItem.N1), 300, false);

            base.OnInit();
        }


        public void GetColumnsWidth()
        {
            //if (SavedColumnWitdths_Dict.ContainsKey(_bvgGrid.Name))
            //{
            //    SavedColumnWitdths_Dict[_bvgGrid.Name] = _bvgGrid.GetColumnWidths();
            //}
            //else
            //{
            //    SavedColumnWitdths_Dict.Add(_bvgGrid.Name, _bvgGrid.GetColumnWidths());
            //}
        }


        public void CmdNewList1()
        {
            FirstOrSecond = false;

            FillList(200, 300);

            GetColumnsWidth();
            //_bvgGrid = GenericAdapter1.Convert(list1, "Table1");


            if (SavedColumnWitdths_Dict.ContainsKey(TableName1))
            {
                CurrBVG1.bvgGrid.SetColumnWidths(SavedColumnWitdths_Dict[TableName1], false);
            }


        }

        public void Cmdqwer()
        {

            CurrBVG1.bvgGrid.UpdateHorizontalScroll();
        }
            

        public void CmdNewList2()
        {
            FirstOrSecond = true;
            FillList2(200, 300);

            GetColumnsWidth();
            //_bvgGrid = GenericAdapter2.Convert(list2, "persons");


            if (SavedColumnWitdths_Dict.ContainsKey(TableName2))
            {
                CurrBVG2.bvgGrid.SetColumnWidths(SavedColumnWitdths_Dict[TableName2], false);
            }
        }

        public void CmdPinColList1()
        {
            CurrBVG1.bvgGrid.FreezeColumn(nameof(MyItem.N3), true);
        }

        public void CmdPinColList2()
        {
            CurrBVG1.bvgGrid.FreezeColumn(nameof(MyItem2.LastName), true);
        }

        public void ConfigureBvgSettings1()
        {
            bvgSettings1 = new BvgSettings
            {
                CellStyle = new BvgStyle
                {
                    BackgroundColor = "silver",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 1,
                },
                HeaderStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "blue",
                    BorderColor = "brown",
                    BorderWidth = 2,
                },
                SelectedCellStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 1,
                },
                ActiveCellStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 1,
                },
                SelectedHeaderStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 2,
                },
                RowHeight = 40,
                HeaderHeight = 50,
            };
        }



        public void ConfigureBvgSettings2()
        {
            bvgSettings2 = new BvgSettings
            {
                CellStyle = new BvgStyle
                {
                    BackgroundColor = "silver",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 1,
                },
                HeaderStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "blue",
                    BorderColor = "brown",
                    BorderWidth = 2,
                },
                SelectedCellStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 1,
                },
                ActiveCellStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 1,
                },
                SelectedHeaderStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 2,
                },
                RowHeight = 40,
                HeaderHeight = 50,
            };
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
