using BlazorVirtualGridComponent.classes;
using BlazorVirtualGridComponent.Modals;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using static BlazorVirtualGridComponent.classes.BvgEnums;

namespace BlazorVirtualGridComponent.Modal
{
    public partial class CompModal<TItem>: IDisposable
    {

        [Parameter]
        public BvgModal<TItem> bvgModal { get; set; }



        protected CompColumnsManager<TItem> compColumnsManager { get; set; }

        protected string Title { get; set; }

        protected override void OnInitialized()
        {
            bvgModal.OnShow = ShowModal;

            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (bvgModal.OnShow == null)
            {
                bvgModal.OnShow = ShowModal;
            }

            base.OnAfterRender(firstRender);
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

            switch (bvgModal.modalForm)
            {
                case ModalForm.ColumnsManager:
                    compColumnsManager.SaveChanges();
                    bvgModal.bvgGrid.compBlazorVirtualGrid.Refresh(true, true);
                    break;
                case ModalForm.StyleDesigner:
                    bvgModal.bvgGrid.compBlazorVirtualGrid.Refresh(false, false);
                    break;
                case ModalForm.FilterManager:
                   
                    break;
                default:
                    break;
            }

            StateHasChanged();

        }

        public void Dispose()
        {

        }
    }
}
