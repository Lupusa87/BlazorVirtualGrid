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


        private BvgStyle _SelectedRowStyle { get; set; } = new BvgStyle();
        public BvgStyle SelectedRowStyle
        {
            get
            {
                return _SelectedRowStyle;

            }
            set
            {
                _SelectedRowStyle = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public void Invoke_PropertyChanged(string Par_PropertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Par_PropertyName));
        //}

        public void Invoke_PropertyChanged_For_All()
        {
            foreach (PropertyInfo item in this.GetType().GetProperties())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item.Name));
            }
        }
    }
}
