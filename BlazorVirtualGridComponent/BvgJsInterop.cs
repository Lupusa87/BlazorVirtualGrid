using Microsoft.JSInterop;
using System;
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


    }
}
