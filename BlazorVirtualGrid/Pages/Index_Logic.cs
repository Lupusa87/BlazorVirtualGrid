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
        readonly GenericAdapter<MyItem> GenericAdapter1 = new GenericAdapter<MyItem>();
        readonly GenericAdapter<MyItem2> GenericAdapter2 = new GenericAdapter<MyItem2>();

        public CompBlazorVirtualGrid CurrBVG;


        public bool FirstOrSecond = false;


        private List<MyItem> list1 = new List<MyItem>();

        private List<MyItem2> list2 = new List<MyItem2>();


        public BvgGrid _bvgGrid;

        Dictionary<string, Dictionary<string, double>> SavedColumnWitdths_Dict = new Dictionary<string, Dictionary<string, double>>();

        protected override void OnInit()
        {
            FillList(100, 200);

          

            _bvgGrid = GenericAdapter1.Convert(list1, "Table1");


            List<string> frozenCols = new List<string>
            {
                nameof(MyItem.N3),
                nameof(MyItem.Date)
            };

            _bvgGrid.FreezeColumns(frozenCols, false);


            _bvgGrid.SetWidthToColumn(nameof(MyItem.N1), 300, false);

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
            _bvgGrid = GenericAdapter1.Convert(list1, "Table1");


            if (SavedColumnWitdths_Dict.ContainsKey(_bvgGrid.Name))
            {
                _bvgGrid.SetColumnWidths(SavedColumnWitdths_Dict[_bvgGrid.Name], false);
            }


        }

        public void Cmdqwer()
        {
           
            
           _bvgGrid.UpdateHorizontalScroll();
        }
            

        public void CmdNewList2()
        {
            FirstOrSecond = true;
            FillList2(200, 300);

            GetColumnsWidth();
            _bvgGrid = GenericAdapter2.Convert(list2, "persons");


            if (SavedColumnWitdths_Dict.ContainsKey(_bvgGrid.Name))
            {
                _bvgGrid.SetColumnWidths(SavedColumnWitdths_Dict[_bvgGrid.Name], false);
            }
        }

        public void CmdPinColList1()
        {
            _bvgGrid.FreezeColumn(nameof(MyItem.N3), true);
        }

        public void CmdPinColList2()
        {
            _bvgGrid.FreezeColumn(nameof(MyItem2.LastName), true);
        }

        public BvgSettings getBvgSettings1()
        {
            return new BvgSettings
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
                SelectedHeaderStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 2,
                },
                SelectedRowStyle = new BvgStyle
                {
                    BackgroundColor = "gray",
                    ForeColor = "darkblue",
                    BorderColor = "black",
                    BorderWidth = 1,
                }
            };
        }

        private void FillList(int Par_Min, int Par_Max)
        {

            list1 = new List<MyItem>();
            for (int i = 1; i <= rnd1.Next(Par_Min, Par_Max); i++)
            {
                list1.Add(new MyItem
                {
                    ID = i,
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
                    ID = i,
                    FirstName = Guid.NewGuid().ToString("d").Substring(1, 4),
                    LastName = Guid.NewGuid().ToString("d").Substring(1, 4),
                    Gender = rnd1.Next(0, 5) == 0,
                    BirthDate = DateTime.Now.AddDays(-rnd1.Next(1, 5000)).AddHours(-rnd1.Next(1, 5000)).AddSeconds(-rnd1.Next(1, 5000)),
                    
                });
            }
        }

        public class MyItem
        {
            public int ID { get; set; }
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
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public bool Gender { get; set; }
        }
    }
}
