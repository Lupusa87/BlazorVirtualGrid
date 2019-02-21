using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.businessLayer
{
    public class BCss
    {
        public List<BCssItem> Children = new List<BCssItem>();

        public string ToBase64String()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(ToString()));
        }
            

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


        public string Selector { get; private set; }


        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();


        public BCssItem(string s)
        {
            Selector = s;
        }

    }
}
