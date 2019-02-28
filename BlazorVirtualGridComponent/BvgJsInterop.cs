using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class BvgJsInterop
    {
        public static Task<bool> Alert(string msg)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "BvgJsFunctions.Alert", msg);
        }

        public static Task<double> GetElementActualWidth(string elementID)
        {
            return JSRuntime.Current.InvokeAsync<double>(
                "BvgJsFunctions.GetElementActualWidth", elementID);
        }

        public static Task<double> GetElementActualHeight(string elementID)
        {
            return JSRuntime.Current.InvokeAsync<double>(
                "BvgJsFunctions.GetElementActualHeight", elementID);
        }

        public static Task<double> GetElementActualTop(string elementID)
        {
            return JSRuntime.Current.InvokeAsync<double>(
                "BvgJsFunctions.GetElementActualTop", elementID);
        }

        public static Task<double> GetWindowHeight()
        {
            return JSRuntime.Current.InvokeAsync<double>(
                "BvgJsFunctions.GetWindowHeight");
        }
        

        public static Task<bool> SetElementScrollLeft(string elementID, double val)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "BvgJsFunctions.SetElementScrollLeft", elementID, val);
        }

        public static Task<double> GetElementScrollLeft(string elementID)
        {
           
            return JSRuntime.Current.InvokeAsync<double>(
                "BvgJsFunctions.GetElementScrollLeft", elementID);
        }

        public static Task<bool> SetFocus(string elementID)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "BvgJsFunctions.SetFocus", elementID);
        }


        public static bool UpdateRowContentBatch(string[] updatepkg)
        {


            if (JSRuntime.Current is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, bool>(
                    "BvgJsFunctions.UpdateRowContentBatch",
                    Json.Serialize(updatepkg));
            }


            return false;
        }


        public static bool UpdateRowWidthsBatch(string[] updatepkg)
        {
            if (JSRuntime.Current is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, bool>(
                    "BvgJsFunctions.UpdateRowWidthsBatch",
                    Json.Serialize(updatepkg));
            }
            return false;
        }


        public static bool UpdateColContentsBatch(string[] updatepkg)
        {


            if (JSRuntime.Current is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, bool>(
                    "BvgJsFunctions.UpdateColContentsBatch",
                    Json.Serialize(updatepkg));
            }


            return false;
        }


        public static bool UpdateElementContentBatchMonoByteArray(string[] updatepkg)
        {

            if (JSRuntime.Current is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<byte[], bool>(
                    "BvgJsFunctions.UpdateElementContentBatchMonoByteArray",
                    Encoding.UTF8.GetBytes(Json.Serialize(updatepkg)));
            }

            return false;
        }

        public static bool SetAttributeBatch(string[] updatepkg, string attr)
        {


            if (JSRuntime.Current is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, string, bool>(
                    "BvgJsFunctions.SetAttributeBatch",
                    Json.Serialize(updatepkg), attr);
            }


            return false;
        }


        public static Task<bool> SetValueToCheckBox(string el, string val)
        {



            return JSRuntime.Current.InvokeAsync<bool>(
                 "BvgJsFunctions.SetValueToCheckBox", el, val);
        }

        public static bool UpdateFrozenNonFrozenWidth(string[] updatepkg)
        {
            if (JSRuntime.Current is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, bool>(
                    "BvgJsFunctions.UpdateFrozenNonFrozenWidth",
                    Json.Serialize(updatepkg));
            }

            return false;
           
        }


    }
}
