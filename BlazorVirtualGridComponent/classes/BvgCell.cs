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

        public bool HasCol { get; set; }

        private string _CssClass { get; set; }
        public string CssClass {
            get
            {
                return _CssClass;
            } set
            {
                _CssClass = value;
                UpdateCssClassTD();
            }
        }

        public string CssClassTD { get; set; }

       

        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke();
        }


        private void UpdateCssClassTD()
        {



            if (bvgColumn.IsFrozen)
            {
                if (CssClass.Equals(CellStyle.CellFrozen.ToString()))
                {
                    if (bvgRow.IsEven)
                    {
                        CssClassTD = "CellFrozenAlternated";
                    }
                    else
                    {
                        CssClassTD = CssClass;
                    }

                }
                else
                {
                    CssClassTD = CssClass;
                }
            }
            else
            {
                if (CssClass.Equals(CellStyle.CellNonFrozen.ToString()))
                {
                    if (bvgRow.IsEven)
                    {
                        CssClassTD = "CellNonFrozenAlternated";
                    }
                    else
                    {
                        CssClassTD = CssClass;
                    }

                }
                else
                {
                    CssClassTD = CssClass;
                }
            }


        }

        public void UpdateID()
        {
            ID =string.Concat("C", bvgColumn.ID, "R" , bvgRow.ID);
        }
    }
}
