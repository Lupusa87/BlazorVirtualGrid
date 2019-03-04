using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent.Modal
{
    public class CompModalBase:ComponentBase,IDisposable
    {

        [Parameter]
        protected BvgModal bvgModal { get; set; }

        protected string Title { get; set; }

        protected override void OnInit()
        {
            bvgModal.OnShow = ShowModal;
        }

        protected override void OnAfterRender()
        {
            if (bvgModal.OnShow == null)
            {
                bvgModal.OnShow = ShowModal;
            }

            base.OnAfterRender();
        }
        public void ShowModal()
        {
            switch (bvgModal.modalForm)
            {
                case ModalForm.ColumnsManager:
                    Title = "Columns Manager";
                    break;
                case ModalForm.StyleDesigner:
                    Title = "Style Designer";
                    break;
                case ModalForm.FilterManager:
                    Title = "Filter Manager";
                    break;
                default:
                    break;
            }


            bvgModal.IsDisplayed = true;

           // StateHasChanged();
        }

        public void CloseModal()
        {
            Title = string.Empty;
            bvgModal.IsDisplayed = false;


            StateHasChanged();
        }

        public void Dispose()
        {

        }
    }
}
