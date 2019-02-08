using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorVirtualGridComponent
{
    public class BvgJsInterop
    {
        public static Task<string> Prompt(string message)
        {
            return JSRuntime.Current.InvokeAsync<string>(
                "BvgJsFunctions.showPrompt",
                message);
        }



        public static Task<double> GetElementActualWidth(string elementID)
        {
            return JSRuntime.Current.InvokeAsync<double>(
                "BvgJsFunctions.GetElementActualWidth", elementID);
        }


        public static Task<int> GetScrollLeftPosition(string elementID)
        {
            return JSRuntime.Current.InvokeAsync<int>(
                "BvgJsFunctions.GetScrollLeftPosition", elementID);
        }

        public static Task<int> GetScrollTopPosition(string elementID)
        {
            return JSRuntime.Current.InvokeAsync<int>(
                "BvgJsFunctions.GetScrollTopPosition", elementID);
        }


        public static Task<bool> SetScrollLeftPosition(string elementID, int val)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "BvgJsFunctions.SetScrollLeftPosition", elementID, val);
        }

        public static Task<bool> SetScrollTopPosition(string elementID, int val)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "BvgJsFunctions.SetScrollTopPosition", elementID, val);
        }

    }
}
