using System;
using System.Collections.Generic;
using System.Linq;
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


        public string ToString(string styleID)
        {
            StringBuilder sb1 = new StringBuilder();
            foreach (var item in Children.Where(x=>x.StyleID== styleID))
            {
                sb1.Append(item.Selector);
                sb1.Append("{");

                sb1.Append(item.GetRules());

                sb1.Append("}");
            }

            return sb1.ToString().Replace(";}", "}");
        }


        public string GetStyle(string selector)
        {

            if (Children.Any(x => x.Selector.Equals(selector, StringComparison.InvariantCultureIgnoreCase)))
            {
                return Children.Single(x => x.Selector.Equals(selector, StringComparison.InvariantCultureIgnoreCase)).GetRules();
            }

            return string.Empty;
        }



        public string GetStyleWithSelector(string selector)
        {
            return (selector+"{"+GetStyle(selector)+"}").Replace(";}", "}");
        }

        public bool RemoveSelector(string selector)
        {

            if (Children.Any(x => x.Selector.Equals(selector, StringComparison.InvariantCultureIgnoreCase)))
            {
                Children.Remove(Children.Single(x => x.Selector.Equals(selector, StringComparison.InvariantCultureIgnoreCase)));
                return true;

            }

            return false;
        }
    }

    public class BCssItem
    {


        public string Selector { get; private set; }

        public string StyleID { get; private set; }


        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();


        public BCssItem(string s, string id)
        {
            Selector = s;
            StyleID = id;
        }


        public string GetRules()
        {
            StringBuilder sb1 = new StringBuilder();

            foreach (var i in Values)
            {
                sb1.Append(i.Key);
                sb1.Append(":");
                sb1.Append(i.Value);
                sb1.Append(";");
            }


            return sb1.ToString();
        }
    }
}
