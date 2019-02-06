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


        GenericAdapter<MyItem> GenericAdapter1 = new GenericAdapter<MyItem>();
        GenericAdapter<MyItem2> GenericAdapter2 = new GenericAdapter<MyItem2>();

        public CompBlazorVirtualGrid CurrBVG = new CompBlazorVirtualGrid();


        private List<MyItem> list1 = new List<MyItem>();

        private List<MyItem2> list2 = new List<MyItem2>();

        bool FirstLoad = true;

        public BvgGrid _bvgGrid = new BvgGrid();



        protected override void OnInit()
        {
            FillList();
            _bvgGrid = GenericAdapter1.Convert(list1, "Table1");

            base.OnInit();
        }

        //protected override void OnAfterRender()
        //{
        //    if (FirstLoad)
        //    {
        //        FirstLoad = false;

        //    }


        //    base.OnAfterRender();
        //}

        public void CmdNewList1()
        {
            Console.WriteLine("________________");

            FillList();
            _bvgGrid = GenericAdapter1.Convert(list1, "Table 1");

        }


        public void CmdNewList2()
        {
            Console.WriteLine("________________");

            FillList2();
            _bvgGrid = GenericAdapter2.Convert(list2, "persons");

        }

        public void CmdPinColList1()
        {
            _bvgGrid.FreezeColumn(nameof(MyItem.N3));
        }

        public void CmdPinColList2()
        {
            _bvgGrid.FreezeColumn(nameof(MyItem2.LastName));
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

            private void FillList()
        {
            list1 = new List<MyItem>();
            for (int i = 1; i <= rnd1.Next(4, 10); i++)
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


                });
            }
        }

        private void FillList2()
        {
            list2 = new List<MyItem2>();
            for (int i = 1; i <= rnd1.Next(2, 10); i++)
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
