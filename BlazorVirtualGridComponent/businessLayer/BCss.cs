using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.businessLayer
{
    public class BCss
    {
        public List<BCssItem> Children = new List<BCssItem>();

        public override string ToString()
        {


            StringBuilder sb1 = new StringBuilder();


            foreach (var item in Children)
            {
                sb1.Append(item.Selector);
                sb1.Append("{");


                foreach (var i in item.Values)
                {
                    sb1.Append(i.Key);
                    sb1.Append(":");
                    sb1.Append(i.Value);
                    sb1.Append(";");
                }


                sb1.Append("}");

            }


            return sb1.ToString().Replace(";}", "}");

        }
    }

    public class BCssItem
    {


        public string Selector { get; set; }


        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();


        public BCssItem(string s)
        {
            Selector = s;
        }

    }
}
