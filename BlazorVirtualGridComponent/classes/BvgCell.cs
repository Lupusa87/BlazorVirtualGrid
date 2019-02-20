using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ID { get; set; }
        

        public string Value { get; set; }

        public bool FocusRequired { get; set; } = false;

        public BvgColumn bvgColumn { get; set; } = new BvgColumn();
        public BvgRow bvgRow { get; set; } = new BvgRow();

        public BvgGrid bvgGrid { get; set; } = new BvgGrid();

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
   

        public string CssClass { get; set; }

        public string StyleTD { get; set; }
        public string StyleDiv { get; set; }

        public string GetStyleTD()
        {

            if (string.IsNullOrEmpty(StyleTD))
            {
             
                StringBuilder sb1 = new StringBuilder();


            
                sb1.Append("border-style:solid;width:" + bvgColumn.ColWidth + "px;height:" + bvgGrid.bvgSettings.RowHeight + "px;");


                if (!string.IsNullOrEmpty(bvgGrid.bvgSettings.CellStyle.BackgroundColor))
                {
                    sb1.Append("background-color:" + bvgGrid.bvgSettings.CellStyle.BackgroundColor + ";");
                }

                if (!string.IsNullOrEmpty(bvgGrid.bvgSettings.CellStyle.ForeColor))
                {
                    sb1.Append("color:" + bvgGrid.bvgSettings.CellStyle.ForeColor + ";");
                }
                if (!string.IsNullOrEmpty(bvgGrid.bvgSettings.CellStyle.BorderColor))
                {
                    sb1.Append("border-color:" + bvgGrid.bvgSettings.CellStyle.BorderColor + ";");
                }

                if (IsSelected)
                {

                    sb1.Append("cursor:pointer;");

                }
                else
                {
                    sb1.Append("cursor:cell;");

                }


                if (bvgGrid.bvgSettings.CellStyle.BorderWidth > -1)
                {
                    sb1.Append("border-width:" + bvgGrid.bvgSettings.CellStyle.BorderWidth + "px;");
                }

                if (IsActive)
                {
                    sb1.Append("outline:" + bvgGrid.bvgSettings.CellStyle.OutlineWidth + "px solid " + bvgGrid.bvgSettings.CellStyle.OutlineColor + ";");

                }

                StyleTD = sb1.ToString();

                return StyleTD;
            }
            else
            {
                return StyleTD;
            }
        }


        public string GetStyleDiv()
        {
            if (string.IsNullOrEmpty(StyleDiv))
            {
                StringBuilder sb1 = new StringBuilder();


                sb1.Append("width:" + (bvgColumn.ColWidth - bvgGrid.bvgSettings.CellStyle.BorderWidth) + "px;height:" + (bvgGrid.bvgSettings.RowHeight - bvgGrid.bvgSettings.CellStyle.BorderWidth) + "px;line-height:" + (bvgGrid.bvgSettings.RowHeight - bvgGrid.bvgSettings.CellStyle.BorderWidth) + "px;");
               

                StyleDiv = sb1.ToString();

                return StyleDiv;
            }
            else
            {
                return StyleDiv;
            }

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke(this, null);
        }
    }
}
