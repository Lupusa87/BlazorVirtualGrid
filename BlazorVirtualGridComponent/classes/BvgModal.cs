using System;
using System.Collections.Generic;
using System.Text;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent.classes
{
    public class BvgModal<TItem>
    {

        public BvgGrid<TItem> bvgGrid { get; set; }

        public bool IsDisplayed { get; set; } = false;
        public ModalForm modalForm { get; set; } 
        public Action OnShow;
       
        public void Show(ModalForm _modalForm)
        {
            modalForm = _modalForm;
            OnShow?.Invoke();
        }

    }
}
