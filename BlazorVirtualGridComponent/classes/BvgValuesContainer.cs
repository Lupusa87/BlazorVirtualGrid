using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.classes
{
    public class ValuesContainer<T>
    {
        public List<T> Values = new List<T>();

        public ValuesContainer<T> Add(T NewValue)
        {
            Values.Add(NewValue);

            return this;
        }


        public int Count()
        {
            return Values.Count;
        }

        public void Reset()
        {
            Values = new List<T>();
        }
    }
}
