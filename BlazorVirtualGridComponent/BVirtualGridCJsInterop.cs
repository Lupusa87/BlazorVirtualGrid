using BlazorVirtualGridComponent.Modals;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class BVirtualGridCJsInterop
    {
        
        public static IJSRuntime jsRuntime;
        public static MonoWebAssemblyJSRuntime monoWebAssemblyJSRuntime = new MonoWebAssemblyJSRuntime();

        public static ValueTask<bool> Alert(string msg)
        {

            return jsRuntime.InvokeAsync<bool>(
                "BVirtualGridCJsFunctions.Alert", msg);
        }

        public static ValueTask<double> GetElementActualWidth(string elementID)
        {
            return jsRuntime.InvokeAsync<double>(
                "BVirtualGridCJsFunctions.GetElementActualWidth", elementID);
        }

        public static ValueTask<double> GetElementActualHeight(string elementID)
        {
            return jsRuntime.InvokeAsync<double>(
                "BVirtualGridCJsFunctions.GetElementActualHeight", elementID);
        }

        public static ValueTask<double> GetElementActualTop(string elementID)
        {
            return jsRuntime.InvokeAsync<double>(
                "BVirtualGridCJsFunctions.GetElementActualTop", elementID);
        }

        public static ValueTask<double> GetWindowHeight()
        {
            return jsRuntime.InvokeAsync<double>(
                "BVirtualGridCJsFunctions.GetWindowHeight");
        }
        

        public static ValueTask<bool> SetElementScrollLeft(string elementID, double val)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BVirtualGridCJsFunctions.SetElementScrollLeft", elementID, val);
        }

        public static ValueTask<bool> SetElementScrollTop(string elementID, double val)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BVirtualGridCJsFunctions.SetElementScrollTop", elementID, val);
        }


        public static ValueTask<bool> SetDivsScrollTop(double val)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BVirtualGridCJsFunctions.SetDivsScrollTop", val);
        }

        public static ValueTask<double> GetElementScrollLeft(string elementID)
        {
           
            return jsRuntime.InvokeAsync<double>(
                "BVirtualGridCJsFunctions.GetElementScrollLeft", elementID);
        }

        public static ValueTask<double> GetElementScrollTop(string elementID)
        {

            return jsRuntime.InvokeAsync<double>(
                "BVirtualGridCJsFunctions.GetElementScrollTop", elementID);
        }


        public static ValueTask<bool> SetFocus(string elementID)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BVirtualGridCJsFunctions.SetFocus", elementID);
        }

        internal static ValueTask<bool> HandleDrag(string elementID, int id, DotNetObjectReference<ClassForJS> dotnetHelper)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BVirtualGridCJsFunctions.handleDragStart", elementID, id, dotnetHelper);
        }


        internal static ValueTask<bool> HandleDrop(string elementID, int id, DotNetObjectReference<ClassForJS> dotnetHelper)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BVirtualGridCJsFunctions.handleDrop", elementID, id, dotnetHelper);
        }



        public static bool UpdateRowContentBatch(string[] updatepkg)
        {

                return monoWebAssemblyJSRuntime.InvokeUnmarshalled<string, bool>(
                    "BVirtualGridCJsFunctions.UpdateRowContentBatch",
                    JsonSerializer.Serialize(updatepkg));

        }


        public static bool UpdateCellClassBatch(string[] updatepkg)
        {

                return monoWebAssemblyJSRuntime.InvokeUnmarshalled<string, bool>(
                    "BVirtualGridCJsFunctions.UpdateCellClassBatch",
                    JsonSerializer.Serialize(updatepkg));
           
        }


        public static bool UpdateCellClassBatchMonoByteArray(string[] pkgIDs, string[] updatepkg)
        {
                return monoWebAssemblyJSRuntime.InvokeUnmarshalled<string, string, bool>(
                    "BVirtualGridCJsFunctions.UpdateCellClassBatchMonoByteArray",
                     JsonSerializer.Serialize(pkgIDs),
                     JsonSerializer.Serialize(updatepkg));
        }


        public static bool UpdateRowWidthsBatch(string[] pkgIDs, string[] updatepkg)
        {

                return monoWebAssemblyJSRuntime.InvokeUnmarshalled<string, string, bool>(
                    "BVirtualGridCJsFunctions.UpdateRowWidthsBatch",
                    JsonSerializer.Serialize(pkgIDs),
                    JsonSerializer.Serialize(updatepkg));
           
        }


        public static bool UpdateColContentsBatch(string[] updatepkg)
        {

                return monoWebAssemblyJSRuntime.InvokeUnmarshalled<string, bool>(
                    "BVirtualGridCJsFunctions.UpdateColContentsBatch",
                    JsonSerializer.Serialize(updatepkg));
           
        }


        public static bool UpdateElementContentBatchMonoByteArray(string[] pkgIDs, string[] updatepkg)
        {

           
                return monoWebAssemblyJSRuntime.InvokeUnmarshalled<string, string, bool>(
                    "BVirtualGridCJsFunctions.UpdateRowContentBatchMonoByteArray",
                    JsonSerializer.Serialize(pkgIDs),
                    JsonSerializer.Serialize(updatepkg));
           
        }

        public static bool SetAttributeBatch(string[] updatepkg, string attr)
        {

                return monoWebAssemblyJSRuntime.InvokeUnmarshalled<string, string, bool>(
                    "BVirtualGridCJsFunctions.SetAttributeBatch",
                    JsonSerializer.Serialize(updatepkg), attr);
          
        }


        public static ValueTask<bool> SetValueToCheckBox(string el, string val)
        {

            return jsRuntime.InvokeAsync<bool>(
                 "BVirtualGridCJsFunctions.SetValueToCheckBox", el, val);
        }

        

        public static bool UpdateStyle(string el, string val)
        {
           
                return monoWebAssemblyJSRuntime.InvokeUnmarshalled<string,string, bool>(
                    "BVirtualGridCJsFunctions.UpdateStyle",
                    el, val);
           

        }

     


    }
}
