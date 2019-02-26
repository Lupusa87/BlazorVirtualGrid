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
        

        public static Task<bool> SetElementScrollLeft(string elementID, int val)
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


        public static bool UpdateElementContentBatchMonoString(string[] updatepkg)
        {


            if (JSRuntime.Current is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, bool>(
                    "BvgJsFunctions.UpdateElementContentBatchMonoString",
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


        public static Task<bool> SetValueToCheckBox(string el, string val)
        {



            return JSRuntime.Current.InvokeAsync<bool>(
                 "BvgJsFunctions.SetValueToCheckBox", el, val);
        }




    }
}
