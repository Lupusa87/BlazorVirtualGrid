using BlazorScrollbarComponent.classes;
using BlazorVirtualGridComponent.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.ExternalSettings
{

    public class BvgSettings<TItem>
    {

        public Action PropertyChanged;

        public string ConfigurationName { get; set; } = "config 1";


        public bool LayoutFixedOrAuto { get; set; }

        public double CompWidth { get; set; } = 0;

        public double CompHeight { get; set; } = 0;


        public BvgStyle NonFrozenCellStyle { get; set; } = new BvgStyle();
        public BvgStyle AlternatedNonFrozenCellStyle { get; set; } = new BvgStyle();

        public BvgStyle FrozenCellStyle { get; set; } = new BvgStyle();
        public BvgStyle AlternatedFrozenCellStyle { get; set; } = new BvgStyle();

        public BvgStyle SelectedCellStyle { get; set; } = new BvgStyle();

        public BvgStyle ActiveCellStyle { get; set; } = new BvgStyle();

        public BvgStyle HeaderStyle { get; set; } = new BvgStyle();


        public BvgStyle ActiveHeaderStyle { get; set; } = new BvgStyle();

        public double RowHeight { get; set; } = 40;
        
        public int HeaderHeight { get; set; } = 50;


        public ushort ColWidthDefault { get; set; } = 150;
        public ushort ColWidthMin { get; set; } = 100;
        public ushort ColWidthMax { get; set; } = 300;

        public double CheckBoxZoom { get; set; } = 1.5;

        public sbyte ScrollSize { get; set; } = 16;

        public BvgScrollStyle VerticalScrollStyle { get; set; } = new BvgScrollStyle();
        public BvgScrollStyle HorizontalScrollStyle { get; set; } = new BvgScrollStyle();

        public BSortStyle bSortStyle { get; set; } = new BSortStyle();

        public ValuesContainer<string> NonFrozenColumnsListOrdered { get; set; } = new ValuesContainer<string>();

        public ValuesContainer<string> FrozenColumnsListOrdered { get; set; } = new ValuesContainer<string>();

        public ValuesContainer<string> HiddenColumns { get; set; } = new ValuesContainer<string>();

        public ValuesContainer<Tuple<string, ushort>> ColumnWidthsDictionary { get; set; } = new ValuesContainer<Tuple<string, ushort>>();


        public Tuple<bool, string, bool> SortedColumn = Tuple.Create(false,string.Empty, false);

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}



        //public void Invoke_PropertyChanged_For_All()
        //{
        //    foreach (PropertyInfo item in this.GetType().GetProperties())
        //    {
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item.Name));
        //    }
        //}
    }
}
