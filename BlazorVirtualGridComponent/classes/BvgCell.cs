using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgCell<TItem>
    {

        public Action PropertyChanged;


       

        public string ID { get; set; }


        public string Value { get; set; }

        public BvgColumn<TItem> bvgColumn { get; set; } = new BvgColumn<TItem>();
        public BvgRow<TItem> bvgRow { get; set; } = new BvgRow<TItem>();

        public BvgGrid<TItem> bvgGrid { get; set; } = new BvgGrid<TItem>();

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }


        private string _CssClassBase { get; set; }
        public string CssClassBase {
            get
            {
                return _CssClassBase;
            } set
            {
                _CssClassBase = value;
                UpdateCssClass();
            }
        }

        public string CssClassFull { get; set; }

        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }


        public void UpdateCssClass()
        {

            if (IsActive || IsSelected)
            {
                CssClassFull = string.Concat("CDiv ", CssClassBase);
            }
            else
            {

                string a = bvgColumn.IsFrozen ? "F" : "NF";
                if (bvgRow.IsEven)
                {
                    a += "Alt";
                }
                CssClassFull = string.Concat("CDiv C",a);
            }
        }

        public void UpdateID()
        {
            ID = string.Concat("C", bvgColumn.ID, "R" , bvgRow.ID);
        }
    }
}
