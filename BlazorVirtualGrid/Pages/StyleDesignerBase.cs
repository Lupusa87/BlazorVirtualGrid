using BlazorVirtualGrid.Services;
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
    public class StyleDesignerBase : ComponentBase
    {


        Random rnd1 = new Random();

        public string TableName1 { get; set; } = "Table 1";

        public IList<MyItemVD> list1 { get; set; } = new List<MyItemVD>();


        [Inject]
        protected GridSettingsService gridSettingsService { get; set; }
   


        public Style currStyle { get; set; } = new Style();

        private PropertyInfo[] props = typeof(BvgSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        public List<string> StylePropsList { get; set; } = new List<string>();

        private bool EnableRender = false;


        private int SelectedStyleIndex = 0;

        private int SelectedScrollIndex = 0;

        protected bool FastApplyChanges { get; set; } = true;

        protected override void OnInit()
        {
            LoadStyle();

            StylePropsList = new List<string>();



            foreach (var item in props.Where(x => x.PropertyType.Name.Contains("BvgStyle")))
            {
                StylePropsList.Add(item.Name.Replace("Style",null));
            }


            currStyle.PropertyChanged = OnStyleChanged;

            FillList(30);


            gridSettingsService.bvgSettings1.LayoutFixedOrAuto = true;
            gridSettingsService.bvgSettings1.CompHeight = 300;
            gridSettingsService.bvgSettings1.CompWidth = 1000;
           

            gridSettingsService.ConfigureBvgSettings1();

            gridSettingsService.bvgSettings1.SortedColumn = Tuple.Create(true, nameof(MyItemVD.ID), true);


            gridSettingsService.bvgSettings1.FrozenColumnsListOrdered = new ValuesContainer<string>();
            gridSettingsService.bvgSettings1.FrozenColumnsListOrdered
                .Add(nameof(MyItemVD.ID))
                .Add(nameof(MyItemVD.Date));

            gridSettingsService.bvgSettings1.ColumnWidthsDictionary = new ValuesContainer<Tuple<string, ushort>>();
            gridSettingsService.bvgSettings1.ColumnWidthsDictionary
                .Add(Tuple.Create(nameof(MyItemVD.ID), (ushort)150))
                .Add(Tuple.Create(nameof(MyItemVD.Date), (ushort)150));

            base.OnInit();
        }


        private  void LoadStyle()
        {
            currStyle.HeaderHeight = gridSettingsService.bvgSettings1.HeaderHeight;
            //currStyle.RowHeight = gridSettingsService.bvgSettings1.RowHeight;
            currStyle.CheckBoxZoom = gridSettingsService.bvgSettings1.CheckBoxZoom;

            currStyle.BackgroundColor = gridSettingsService.bvgSettings1.NonFrozenCellStyle.BackgroundColor;
            currStyle.ForeColor = gridSettingsService.bvgSettings1.NonFrozenCellStyle.ForeColor;
            currStyle.BorderColor = gridSettingsService.bvgSettings1.NonFrozenCellStyle.BorderColor;
            currStyle.OutlineColor = gridSettingsService.bvgSettings1.NonFrozenCellStyle.OutlineColor;
            currStyle.BorderWidth = gridSettingsService.bvgSettings1.NonFrozenCellStyle.BorderWidth;
            currStyle.OutlineWidth = gridSettingsService.bvgSettings1.NonFrozenCellStyle.OutlineWidth;


            currStyle.ScrollButtonColor = gridSettingsService.bvgSettings1.HorizontalScrollStyle.ButtonColor;
            currStyle.ScrollThumbColor = gridSettingsService.bvgSettings1.HorizontalScrollStyle.ThumbColor;
            currStyle.ScrollThumbWayColor = gridSettingsService.bvgSettings1.HorizontalScrollStyle.ThumbWayColor;



            currStyle.SortedDirectionColorIsNone = gridSettingsService.bvgSettings1.bSortStyle.SortedDirectionColor.Equals("none", StringComparison.InvariantCultureIgnoreCase);
            if (currStyle.SortedDirectionColorIsNone)
            {
                currStyle.SortedDirectionColor = "#FFF8DC";
            }
            else
            {
                currStyle.SortedDirectionColor = gridSettingsService.bvgSettings1.bSortStyle.SortedDirectionColor;
            }


            currStyle.UnSortedDirectionColorIsNone = gridSettingsService.bvgSettings1.bSortStyle.UnSortedDirectionColor.Equals("none", StringComparison.InvariantCultureIgnoreCase);
            if (currStyle.UnSortedDirectionColorIsNone)
            {
                currStyle.UnSortedDirectionColor = "#FFF8DC";
            }
            else
            {
                currStyle.UnSortedDirectionColor = gridSettingsService.bvgSettings1.bSortStyle.UnSortedDirectionColor;
            }

            currStyle.SortBorderColorIsNone = gridSettingsService.bvgSettings1.bSortStyle.BorderColor.Equals("none", StringComparison.InvariantCultureIgnoreCase);
            if (currStyle.SortBorderColorIsNone)
            {
                currStyle.SortBorderColor = "#FFF8DC";
            }
            else
            {
                currStyle.SortBorderColor = gridSettingsService.bvgSettings1.bSortStyle.BorderColor;
            }
        }

        public void OnStyleChanged(int b)
        {


            switch (b)
            {
                case 0:
                    UpdateStyleObject();
                    break;
                case 1:
                    gridSettingsService.bvgSettings1.HeaderHeight = currStyle.HeaderHeight;
                    //gridSettingsService.bvgSettings1.RowHeight = currStyle.RowHeight;
                    gridSettingsService.bvgSettings1.CheckBoxZoom = currStyle.CheckBoxZoom;
                    break;
                case 2:
                    UpdateScrollObject();
                    break;
                case 3:

                    if (currStyle.SortedDirectionColorIsNone)
                    {
                        gridSettingsService.bvgSettings1.bSortStyle.SortedDirectionColor = "none";
                    }
                    else
                    {
                        gridSettingsService.bvgSettings1.bSortStyle.SortedDirectionColor = currStyle.SortedDirectionColor;
                    }

                    if (currStyle.UnSortedDirectionColorIsNone)
                    {
                        gridSettingsService.bvgSettings1.bSortStyle.UnSortedDirectionColor = "none";
                    }
                    else
                    {
                        gridSettingsService.bvgSettings1.bSortStyle.UnSortedDirectionColor = currStyle.UnSortedDirectionColor;
                    }

                    if (currStyle.SortBorderColorIsNone)
                    {
                        gridSettingsService.bvgSettings1.bSortStyle.BorderColor = "none";
                    }
                    else
                    {
                        gridSettingsService.bvgSettings1.bSortStyle.BorderColor = currStyle.SortBorderColor;
                    }
                    
                    break;
                default:
                    break;
            }


         

            if (FastApplyChanges)
            {
                Refresh();
            }

        }

        public void ComboSelectionChanged(UIChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out int index))
            {
                SelectedStyleIndex = index;
                UpdateStyleUI();

            }
        }


        public void ComboScrollSelectionChanged(UIChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out int index))
            {
               
                SelectedScrollIndex = index;
                UpdateScrollUI();

            }

        }

        public void UpdateStyleObject()
        {

           
            PropertyInfo selectedProp = props.Single(x => x.Name.Equals(StylePropsList[SelectedStyleIndex] + "Style"));


            BvgStyle s = new BvgStyle()
            {
                BackgroundColor = currStyle.BackgroundColor,
                ForeColor = currStyle.ForeColor,
                BorderColor = currStyle.BorderColor,
                OutlineColor = currStyle.OutlineColor,
                BorderWidth = (sbyte)currStyle.BorderWidth,
                OutlineWidth = (sbyte)currStyle.OutlineWidth,
            };


            selectedProp.SetValue(gridSettingsService.bvgSettings1, s);

           
        }

        public void UpdateScrollObject()
        {
            switch (SelectedScrollIndex)
            {
                case 0:
                    gridSettingsService.bvgSettings1.HorizontalScrollStyle = new BvgScrollStyle()
                    {
                        ButtonColor = currStyle.ScrollButtonColor,
                        ThumbColor = currStyle.ScrollThumbColor,
                        ThumbWayColor = currStyle.ScrollThumbWayColor,
                    };
                    gridSettingsService.bvgSettings1.VerticalScrollStyle = new BvgScrollStyle()
                    {
                        ButtonColor = currStyle.ScrollButtonColor,
                        ThumbColor = currStyle.ScrollThumbColor,
                        ThumbWayColor = currStyle.ScrollThumbWayColor,
                    };
                    break;
                case 1:
                    gridSettingsService.bvgSettings1.HorizontalScrollStyle = new BvgScrollStyle()
                    {
                        ButtonColor = currStyle.ScrollButtonColor,
                        ThumbColor = currStyle.ScrollThumbColor,
                        ThumbWayColor = currStyle.ScrollThumbWayColor,
                    };
                    break;
                case 2:
                    gridSettingsService.bvgSettings1.VerticalScrollStyle = new BvgScrollStyle()
                    {
                        ButtonColor = currStyle.ScrollButtonColor,
                        ThumbColor = currStyle.ScrollThumbColor,
                        ThumbWayColor = currStyle.ScrollThumbWayColor,
                    };
                    break;
                default:
                    break;
            }
        }



        public void UpdateStyleUI()
        {

   
           
            PropertyInfo selectedProp = props.Single(x => x.Name.Equals(StylePropsList[SelectedStyleIndex] + "Style"));

            BvgStyle s = selectedProp.GetValue(gridSettingsService.bvgSettings1, null) as BvgStyle;

            currStyle.BackgroundColor = s.BackgroundColor;
            currStyle.ForeColor = s.ForeColor;
            currStyle.BorderColor = s.BorderColor;
            currStyle.OutlineColor = s.OutlineColor;
            currStyle.BorderWidth = s.BorderWidth;
            currStyle.OutlineWidth = s.OutlineWidth;

         
        }


        public void UpdateScrollUI()
        {

            switch (SelectedScrollIndex)
            {
                case 0:
                case 1:
                    currStyle.ScrollButtonColor = gridSettingsService.bvgSettings1.HorizontalScrollStyle.ButtonColor;
                    currStyle.ScrollThumbColor = gridSettingsService.bvgSettings1.HorizontalScrollStyle.ThumbColor;
                    currStyle.ScrollThumbWayColor = gridSettingsService.bvgSettings1.HorizontalScrollStyle.ThumbWayColor;
                    break;
                case 2:
                    currStyle.ScrollButtonColor = gridSettingsService.bvgSettings1.VerticalScrollStyle.ButtonColor;
                    currStyle.ScrollThumbColor = gridSettingsService.bvgSettings1.VerticalScrollStyle.ThumbColor;
                    currStyle.ScrollThumbWayColor = gridSettingsService.bvgSettings1.VerticalScrollStyle.ThumbWayColor;
                    break;
                default:
                    break;
            }


        }

        protected override bool ShouldRender()
        {
            return EnableRender;
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


        public void Refresh()
        {
            EnableRender = true;
            StateHasChanged();
            EnableRender = false;
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

        public class Style
        {
            public Action<int> PropertyChanged;

            private string _OutlineColor { get; set; }
            public string OutlineColor
            {
                get
                {

                    return _OutlineColor;
                }
                set
                {

                    _OutlineColor = value;
                    PropertyChanged?.Invoke(0);
                }
            }

            private string _BorderColor { get; set; }
            public string BorderColor
            {
                get
                {

                    return _BorderColor;
                }
                set
                {

                    _BorderColor = value;
                    PropertyChanged?.Invoke(0);
                }
            }



            private string _BackgroundColor { get; set; }
            public string BackgroundColor
            {
                get
                {

                    return _BackgroundColor;
                }
                set
                {

                    _BackgroundColor = value;
                    PropertyChanged?.Invoke(0);
                }
            }


            private string _ForeColor { get; set; }

            public string ForeColor
            {
                get
                {

                    return _ForeColor;
                }
                set
                {

                    _ForeColor = value;
                    PropertyChanged?.Invoke(0);
                }
            }

            private double _BorderWidth { get; set; }

            public double BorderWidth
            {
                get
                {

                    return _BorderWidth;
                }
                set
                {

                    _BorderWidth = value;
                    PropertyChanged?.Invoke(0);
                }
            }

            private double _OutlineWidth { get; set; }

            public double OutlineWidth
            {
                get
                {

                    return _OutlineWidth;
                }
                set
                {

                    _OutlineWidth = value;
                    PropertyChanged?.Invoke(0);
                }
            }


            //private double _RowHeight { get; set; }

            //public double RowHeight
            //{
            //    get
            //    {

            //        return _RowHeight;
            //    }
            //    set
            //    {

            //        _RowHeight = value;
            //        PropertyChanged?.Invoke(1);
            //    }
            //}


            private int _HeaderHeight { get; set; }
            public int HeaderHeight
            {
                get
                {

                    return _HeaderHeight;
                }
                set
                {

                    _HeaderHeight = value;
                    PropertyChanged?.Invoke(1);
                }
            }

            private double _CheckBoxZoom { get; set; }
            public double CheckBoxZoom
            {
                get
                {

                    return _CheckBoxZoom;
                }
                set
                {

                    _CheckBoxZoom = value;
                    PropertyChanged?.Invoke(1);
                }
            }



            private string _ScrollButtonColor { get; set; }
            public string ScrollButtonColor
            {
                get
                {

                    return _ScrollButtonColor;
                }
                set
                {

                    _ScrollButtonColor = value;
                    PropertyChanged?.Invoke(2);
                }
            }



            private string _ScrollThumbColor { get; set; }
            public string ScrollThumbColor
            {
                get
                {

                    return _ScrollThumbColor;
                }
                set
                {

                    _ScrollThumbColor = value;
                    PropertyChanged?.Invoke(2);
                }
            }

            private string _ScrollThumbWayColor { get; set; }
            public string ScrollThumbWayColor
            {
                get
                {

                    return _ScrollThumbWayColor;
                }
                set
                {

                    _ScrollThumbWayColor = value;
                    PropertyChanged?.Invoke(2);
                }
            }


            private string _SortedDirectionColor { get; set; }
            public string SortedDirectionColor
            {
                get
                {

                    return _SortedDirectionColor;
                }
                set
                {

                    _SortedDirectionColor = value;
                    PropertyChanged?.Invoke(3);
                }
            }

            private string _UnSortedDirectionColor { get; set; }
            public string UnSortedDirectionColor
            {
                get
                {

                    return _UnSortedDirectionColor;
                }
                set
                {

                    _UnSortedDirectionColor = value;
                    PropertyChanged?.Invoke(3);
                }
            }

            private string _SortBorderColor { get; set; }
            public string SortBorderColor
            {
                get
                {

                    return _SortBorderColor;
                }
                set
                {

                    _SortBorderColor = value;
                    PropertyChanged?.Invoke(3);
                }
            }


            private bool _SortedDirectionColorIsNone { get; set; }
            public bool SortedDirectionColorIsNone
            {
                get
                {

                    return _SortedDirectionColorIsNone;
                }
                set
                {

                    _SortedDirectionColorIsNone = value;
                    PropertyChanged?.Invoke(3);
                }
            }

            private bool _UnSortedDirectionColorIsNone { get; set; }
            public bool UnSortedDirectionColorIsNone
            {
                get
                {

                    return _UnSortedDirectionColorIsNone;
                }
                set
                {

                    _UnSortedDirectionColorIsNone = value;
                    PropertyChanged?.Invoke(3);
                }
            }

            private bool _SortBorderColorIsNone { get; set; }
            public bool SortBorderColorIsNone
            {
                get
                {

                    return _SortBorderColorIsNone;
                }
                set
                {

                    _SortBorderColorIsNone = value;
                    PropertyChanged?.Invoke(3);
                }
            }

        }


    }
}
