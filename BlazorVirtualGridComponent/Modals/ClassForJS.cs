using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.Modals
{
    public class ClassForJS
    {

        public Action<int> CustomOnDragStart { get; set; }
        public Action<int, int> CustomOnDrop { get; set; }

        [JSInvokable]
        public void InvokeDragStartFromJS(int id)
        {
            CustomOnDragStart?.Invoke(id);
        }

        [JSInvokable]
        public void InvokeDropFromJS(object args)
        {
            string[] a = args.ToString().Replace("[", null).Replace("]", null).Replace("\"", null).Split(",");

            int parentID = int.Parse(a[0]);
            int id = int.Parse(a[1]);

            CustomOnDrop?.Invoke(parentID,id);
        }
    }
}
