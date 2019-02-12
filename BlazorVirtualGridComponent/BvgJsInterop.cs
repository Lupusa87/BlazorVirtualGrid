using Microsoft.JSInterop;
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



        public static Task<bool> SetElementScrollLeft(string elementID, double val)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "BvgJsFunctions.SetElementScrollLeft", elementID, val);
        }

        
    }
}
