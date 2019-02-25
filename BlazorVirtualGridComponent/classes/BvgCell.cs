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
    public class BvgCell 
    {
       
        public Action PropertyChanged;

        public string ID { get; set; }
        

        public string Value { get; set; }

        public BvgColumn bvgColumn { get; set; } = new BvgColumn();
        public BvgRow bvgRow { get; set; } = new BvgRow();

        public BvgGrid bvgGrid { get; set; } = new BvgGrid();

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
   

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

        public Type ValueType { get; set; }

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
    }
}
