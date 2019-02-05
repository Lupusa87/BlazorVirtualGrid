using BlazorVirtualGridComponent;
using BlazorVirtualGridComponent.businessLayer;
using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVirtualGrid.Pages
{
    public class Index_Logic:BlazorComponent
    {

        Random rnd1 = new Random();


        GenericAdapter<MyItem> GenericAdapter1 = new GenericAdapter<MyItem>();

        public CompBlazorVirtualGrid CurrBVG = new CompBlazorVirtualGrid();


        public BvgGrid bvgGrid = new BvgGrid();





        private List<MyItem> list1 = new List<MyItem>();

        bool FirstLoad = true;
      

        protected override void OnInit()
        {
            FillList();

            bvgGrid = GenericAdapter1.Convert(list1, "Table1");

            base.OnInit();
        }

        protected override void OnAfterRender()
        {
            if (FirstLoad)
            {
                FirstLoad = false;
                

                CurrBVG.Refresh();
                
            }


            base.OnAfterRender();
        }



        public void CmdRefresh()
        {

            CurrBVG.Refresh();
        }



        private void FillList()
        {

            for (int i = 1; i <= 10; i++)
            {
                list1.Add(new MyItem
                {
                    ID = i,
                    Name = "Item " + i,

                    SomeBool = rnd1.Next(0, 5) == 0,
                    Date = DateTime.Now,
                    N1 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N2 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N3 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N4 = Guid.NewGuid().ToString("d").Substring(1, 4),
                    N5 = Guid.NewGuid().ToString("d").Substring(1, 4),


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
    }
}
