using BlazorVirtualGridComponent.classes;
using BlazorVirtualGridComponent.ExternalSettings;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlazorVirtualGridComponent.Modals
{
    public class CompStyleDesignerBase<TItem> : ComponentBase
    {
        [Parameter]
        protected BvgGrid<TItem> bvgGrid { get; set; }

        public Style currStyle { get; set; } = new Style();

        private PropertyInfo[] props = typeof(BvgSettings<TItem>).GetProperties(BindingFlags.Public | BindingFlags.Instance);

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
                StylePropsList.Add(item.Name.Replace("Style", null));
            }


            currStyle.PropertyChanged = OnStyleChanged;


            base.OnInit();
        }


        private void LoadStyle()
        {
            currStyle.HeaderHeight = bvgGrid.bvgSettings.HeaderHeight;
            currStyle.RowHeight = bvgGrid.bvgSettings.RowHeight;
            currStyle.CheckBoxZoom = bvgGrid.bvgSettings.CheckBoxZoom;
            currStyle.ScrollSize = bvgGrid.bvgSettings.ScrollSize;

            currStyle.BackgroundColor = bvgGrid.bvgSettings.NonFrozenCellStyle.BackgroundColor;
            currStyle.ForeColor = bvgGrid.bvgSettings.NonFrozenCellStyle.ForeColor;
            currStyle.BorderColor = bvgGrid.bvgSettings.NonFrozenCellStyle.BorderColor;
            currStyle.BorderWidth = bvgGrid.bvgSettings.NonFrozenCellStyle.BorderWidth;
           

            currStyle.ScrollButtonColor = bvgGrid.bvgSettings.HorizontalScrollStyle.ButtonColor;
            currStyle.ScrollThumbColor = bvgGrid.bvgSettings.HorizontalScrollStyle.ThumbColor;
            currStyle.ScrollThumbWayColor = bvgGrid.bvgSettings.HorizontalScrollStyle.ThumbWayColor;



            currStyle.SortedDirectionColorIsNone = bvgGrid.bvgSettings.bSortStyle.SortedDirectionColor.Equals("none", StringComparison.InvariantCultureIgnoreCase);
            if (currStyle.SortedDirectionColorIsNone)
            {
                currStyle.SortedDirectionColor = "#FFF8DC";
            }
            else
            {
                currStyle.SortedDirectionColor = bvgGrid.bvgSettings.bSortStyle.SortedDirectionColor;
            }


            currStyle.UnSortedDirectionColorIsNone = bvgGrid.bvgSettings.bSortStyle.UnSortedDirectionColor.Equals("none", StringComparison.InvariantCultureIgnoreCase);
            if (currStyle.UnSortedDirectionColorIsNone)
            {
                currStyle.UnSortedDirectionColor = "#FFF8DC";
            }
            else
            {
                currStyle.UnSortedDirectionColor = bvgGrid.bvgSettings.bSortStyle.UnSortedDirectionColor;
            }

            currStyle.SortBorderColorIsNone = bvgGrid.bvgSettings.bSortStyle.BorderColor.Equals("none", StringComparison.InvariantCultureIgnoreCase);
            if (currStyle.SortBorderColorIsNone)
            {
                currStyle.SortBorderColor = "#FFF8DC";
            }
            else
            {
                currStyle.SortBorderColor = bvgGrid.bvgSettings.bSortStyle.BorderColor;
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
                    bvgGrid.bvgSettings.HeaderHeight = currStyle.HeaderHeight;
                    bvgGrid.bvgSettings.RowHeight = currStyle.RowHeight;
                    bvgGrid.bvgSettings.CheckBoxZoom = currStyle.CheckBoxZoom;
                    bvgGrid.bvgSettings.ScrollSize = (sbyte)currStyle.ScrollSize;
                    break;
                case 2:
                    UpdateScrollObject();
                    break;
                case 3:

                    if (currStyle.SortedDirectionColorIsNone)
                    {
                        bvgGrid.bvgSettings.bSortStyle.SortedDirectionColor = "none";
                    }
                    else
                    {
                        bvgGrid.bvgSettings.bSortStyle.SortedDirectionColor = currStyle.SortedDirectionColor;
                    }

                    if (currStyle.UnSortedDirectionColorIsNone)
                    {
                        bvgGrid.bvgSettings.bSortStyle.UnSortedDirectionColor = "none";
                    }
                    else
                    {
                        bvgGrid.bvgSettings.bSortStyle.UnSortedDirectionColor = currStyle.UnSortedDirectionColor;
                    }

                    if (currStyle.SortBorderColorIsNone)
                    {
                        bvgGrid.bvgSettings.bSortStyle.BorderColor = "none";
                    }
                    else
                    {
                        bvgGrid.bvgSettings.bSortStyle.BorderColor = currStyle.SortBorderColor;
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
                BorderWidth = (sbyte)currStyle.BorderWidth,
            };


            selectedProp.SetValue(bvgGrid.bvgSettings, s);


        }

        public void UpdateScrollObject()
        {
            switch (SelectedScrollIndex)
            {
                case 0:
                    bvgGrid.bvgSettings.HorizontalScrollStyle = new BvgScrollStyle()
                    {
                        ButtonColor = currStyle.ScrollButtonColor,
                        ThumbColor = currStyle.ScrollThumbColor,
                        ThumbWayColor = currStyle.ScrollThumbWayColor,
                    };
                    bvgGrid.bvgSettings.VerticalScrollStyle = new BvgScrollStyle()
                    {
                        ButtonColor = currStyle.ScrollButtonColor,
                        ThumbColor = currStyle.ScrollThumbColor,
                        ThumbWayColor = currStyle.ScrollThumbWayColor,
                    };
                    break;
                case 1:
                    bvgGrid.bvgSettings.HorizontalScrollStyle = new BvgScrollStyle()
                    {
                        ButtonColor = currStyle.ScrollButtonColor,
                        ThumbColor = currStyle.ScrollThumbColor,
                        ThumbWayColor = currStyle.ScrollThumbWayColor,
                    };
                    break;
                case 2:
                    bvgGrid.bvgSettings.VerticalScrollStyle = new BvgScrollStyle()
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

            BvgStyle s = selectedProp.GetValue(bvgGrid.bvgSettings, null) as BvgStyle;

            currStyle.BackgroundColor = s.BackgroundColor;
            currStyle.ForeColor = s.ForeColor;
            currStyle.BorderColor = s.BorderColor;
            currStyle.BorderWidth = s.BorderWidth;
        

        }


        public void UpdateScrollUI()
        {

            switch (SelectedScrollIndex)
            {
                case 0:
                case 1:
                    currStyle.ScrollButtonColor = bvgGrid.bvgSettings.HorizontalScrollStyle.ButtonColor;
                    currStyle.ScrollThumbColor = bvgGrid.bvgSettings.HorizontalScrollStyle.ThumbColor;
                    currStyle.ScrollThumbWayColor = bvgGrid.bvgSettings.HorizontalScrollStyle.ThumbWayColor;
                    break;
                case 2:
                    currStyle.ScrollButtonColor = bvgGrid.bvgSettings.VerticalScrollStyle.ButtonColor;
                    currStyle.ScrollThumbColor = bvgGrid.bvgSettings.VerticalScrollStyle.ThumbColor;
                    currStyle.ScrollThumbWayColor = bvgGrid.bvgSettings.VerticalScrollStyle.ThumbWayColor;
                    break;
                default:
                    break;
            }


        }

        protected override bool ShouldRender()
        {
            return EnableRender;
        }


        public void Refresh()
        {
            EnableRender = true;
            StateHasChanged();
            EnableRender = false;
        }

      
        public class Style
        {
            public Action<int> PropertyChanged;


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


            private double _RowHeight { get; set; }

            public double RowHeight
            {
                get
                {

                    return _RowHeight;
                }
                set
                {

                    _RowHeight = value;
                    PropertyChanged?.Invoke(1);
                }
            }


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

            private int _ScrollSize { get; set; }
            public int ScrollSize
            {
                get
                {

                    return _ScrollSize;
                }
                set
                {

                    _ScrollSize = value;
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
