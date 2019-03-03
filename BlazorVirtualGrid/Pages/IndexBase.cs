using BlazorVirtualGrid.Services;
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
    public class IndexBase:ComponentBase
    {


        Random rnd1 = new Random();

        public CompBlazorVirtualGrid<MyItem> CurrBVG1;
        public CompBlazorVirtualGrid<MyItem2> CurrBVG2;

        public bool FirstOrSecond = true;


        public string TableName1 { get; set; } = "Table 1";
        public string TableName2 { get; set; } = "Table 2";

        public IList<MyItem> list1 { get; set; } = new List<MyItem>();

        public IList<MyItem2> list2 { get; set; } = new List<MyItem2>();

        [Inject]
        protected GridSettingsService gridSettingsService { get; set; }



        Dictionary<string, ValuesContainer<Tuple<string, ushort>>> SavedColumnWitdths_Dict = new Dictionary<string, ValuesContainer<Tuple<string, ushort>>>();


       
        protected override void OnInit()
        {

            //FillList(5, 7);
            FillList(200, 200);


            gridSettingsService.bvgSettings1.LayoutFixedOrAuto = false;

            gridSettingsService.ConfigureBvgSettings1();

            gridSettingsService.bvgSettings1.FrozenColumnsListOrdered = new ValuesContainer<string>();
            gridSettingsService.bvgSettings1.FrozenColumnsListOrdered
                .Add(nameof(MyItem.C3))
                .Add(nameof(MyItem.Date));

            gridSettingsService.bvgSettings1.ColumnWidthsDictionary = new ValuesContainer<Tuple<string, ushort>>();
            gridSettingsService.bvgSettings1.ColumnWidthsDictionary
                .Add(Tuple.Create(nameof(MyItem.C3), (ushort)200))
                .Add(Tuple.Create(nameof(MyItem.Date), (ushort)200));


            PropertyInfo[] props = typeof(MyItem).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var item in props.Where(x=>x.Name!="Date" && x.Name!="C3"))
            {
                gridSettingsService.bvgSettings1.ColumnWidthsDictionary
                .Add(Tuple.Create(item.Name, (ushort)rnd1.Next(gridSettingsService.bvgSettings1.ColWidthMin, gridSettingsService.bvgSettings1.ColWidthMax)));
            }

            
            //foreach (var item in props.Where(x => x.Name.Contains("C")))
            //{
            //    bvgSettings1.HiddenColumns
            //    .Add(item.Name);
            //}

            //bvgSettings1.HiddenColumns
            //    .Add(nameof(MyItem.C1));

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


            gridSettingsService.bvgSettings1.LayoutFixedOrAuto = false;

            gridSettingsService.ConfigureBvgSettings1();

            gridSettingsService.bvgSettings1.FrozenColumnsListOrdered = new ValuesContainer<string>();
            gridSettingsService.bvgSettings1.FrozenColumnsListOrdered
                .Add(nameof(MyItem.C3))
                .Add(nameof(MyItem.Date));
           


            if (SavedColumnWitdths_Dict.ContainsKey(TableName1))
            {
                gridSettingsService.bvgSettings1.ColumnWidthsDictionary = SavedColumnWitdths_Dict[TableName1];
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


            gridSettingsService.bvgSettings2.LayoutFixedOrAuto = false;

            gridSettingsService.ConfigureBvgSettings2();

            gridSettingsService.bvgSettings2.FrozenColumnsListOrdered = new ValuesContainer<string>();
            gridSettingsService.bvgSettings2.FrozenColumnsListOrdered
                .Add(nameof(MyItem2.Gender));

            gridSettingsService.bvgSettings2.HiddenColumns = new ValuesContainer<string>();
            gridSettingsService.bvgSettings2.HiddenColumns
                .Add(nameof(MyItem2.LastName));


            if (SavedColumnWitdths_Dict.ContainsKey(TableName1))
            {
                gridSettingsService.bvgSettings1.ColumnWidthsDictionary = SavedColumnWitdths_Dict[TableName1];
            }


            StateHasChanged();
        }

        public void CmdPinColList1()
        {
            
        }

        public void CmdPinColList2()
        {
            
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

                    SomeBool = rnd1.Next(0, 5) > 1,
                    Date = DateTime.Now.AddDays(-rnd1.Next(1, 5000)).AddHours(-rnd1.Next(1, 5000)).AddSeconds(-rnd1.Next(1, 5000)),
                    C1 = "C1" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C2 = "C2" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C3 = "C3" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C4 = "C4" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C5 = "C5" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C6 = "C6" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C7 = "C7" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C8 = "C8" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C9 = "C9" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C10 = "C10" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C11 = "C11" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C12 = "C12" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C13 = "C13" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C14 = "C14" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C15 = "C15" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C16 = "C16" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C17 = "C17" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C18 = "C18" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C19 = "C19" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C20 = "C20" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C21 = "C21" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C22 = "C22" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C23 = "C23" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C24 = "C24" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C25 = "C25" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C26 = "C26" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C27 = "C27" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C28 = "C28" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C29 = "C29" + Guid.NewGuid().ToString("d").Substring(1, 4),
                    C30 = "C30" + Guid.NewGuid().ToString("d").Substring(1, 4),
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
            public string C1 { get; set; }
            public string C2 { get; set; }
            public string C3 { get; set; }
            public string C4 { get; set; }
            public string C5 { get; set; }
            public string C6 { get; set; }
            public string C7 { get; set; }
            public string C8 { get; set; }
            public string C9 { get; set; }
            public string C10 { get; set; }
            public string C11 { get; set; }
            public string C12 { get; set; }
            public string C13 { get; set; }
            public string C14 { get; set; }
            public string C15 { get; set; }
            public string C16 { get; set; }
            public string C17 { get; set; }
            public string C18 { get; set; }
            public string C19 { get; set; }
            public string C20 { get; set; }
            public string C21 { get; set; }
            public string C22 { get; set; }
            public string C23 { get; set; }
            public string C24 { get; set; }
            public string C25 { get; set; }
            public string C26 { get; set; }
            public string C27 { get; set; }
            public string C28 { get; set; }
            public string C29 { get; set; }
            public string C30 { get; set; }

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
