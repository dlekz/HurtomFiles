using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HurtomFiles.Logic
{
    public static class HtmlNodeExtensions
    {
        public static List<HtmlNode> FindNode(this HtmlNode parent, params string[] name)
        {
            List<HtmlNode> htmlNodes = new List<HtmlNode>();
            foreach (var el in parent.ChildNodes)
            {
                if (name.Contains(el.Name))
                    htmlNodes.Add(el);

                if (el.HasChildNodes)
                    htmlNodes.AddRange(FindNode(el, name));
            }
            return htmlNodes;
        }
    }
}
