using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent
{
    public static class JsEventReceiver
    {

        public static Action OnResize { get; set; }



        public static void Initialize()
        {
            //it is doing nothing but withhout this call browser is giving error, because it can not find active BlazorWindowHelper
        }

        [JSInvokable]
        public static void InvokeOnResize()
        {
            OnResize?.Invoke();
        }
    }


}
