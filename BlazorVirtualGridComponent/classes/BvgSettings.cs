using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent.classes
{

    public class BvgSettings : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string ConfigurationName { get; set; } = "config 1";

       

        private BvgStyle _CellStyle { get; set; } = new BvgStyle();
        public BvgStyle CellStyle
        {
            get
            {
                return _CellStyle;

            }
            set
            {
                _CellStyle = value;
                OnPropertyChanged();
            }
        }

        private BvgStyle _HeaderStyle { get; set; } = new BvgStyle();
        public BvgStyle HeaderStyle
        {
            get
            {
                return _HeaderStyle;

            }
            set
            {
                _HeaderStyle = value;
                OnPropertyChanged();
            }
        }

        private BvgStyle _SelectedCellStyle { get; set; } = new BvgStyle();
        public BvgStyle SelectedCellStyle
        {
            get
            {
                return _SelectedCellStyle;

            }
            set
            {
                _SelectedCellStyle = value;
                OnPropertyChanged();
            }
        }


        private BvgStyle _ActiveCellStyle { get; set; } = new BvgStyle();
        public BvgStyle ActiveCellStyle
        {
            get
            {
                return _ActiveCellStyle;

            }
            set
            {
                _ActiveCellStyle = value;
                OnPropertyChanged();
            }
        }

        private BvgStyle _SelectedHeaderStyle { get; set; } = new BvgStyle();
        public BvgStyle SelectedHeaderStyle
        {
            get
            {
                return _SelectedHeaderStyle;

            }
            set
            {
                _SelectedHeaderStyle = value;
                OnPropertyChanged();
            }
        }


       



        private double _RowHeight { get; set; } = 40;
        public double RowHeight
        {
            get
            {
                return _RowHeight;

            }
            set
            {
                _RowHeight = value;
                OnPropertyChanged();
            }
        }


        private double _HeaderHeight { get; set; } = 50;
        public double HeaderHeight
        {
            get
            {
                return _HeaderHeight;

            }
            set
            {
                _HeaderHeight = value;
                OnPropertyChanged();
            }
        }


       

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void Invoke_PropertyChanged_For_All()
        {
            foreach (PropertyInfo item in this.GetType().GetProperties())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item.Name));
            }
        }
    }
}
