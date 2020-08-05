using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using System.Threading;

namespace HurtomFiles.Logic
{
    public class SearchPage
    {
        public List<Link> Links { set; get; } = new List<Link>();
        public int Count => Links.Count;

        private readonly string searchString = "https://toloka.to/tracker.php?nm=";

        public SearchPage(string str) 
        {
            try
            {
                string value = str.Replace(" ", "+");
                var htmlDocument = new HtmlDocument().HtmlDocumentLoadAsync(searchString + value).Result;
                var topics = htmlDocument.DocumentNode.SelectNodes("//a[@class='genmed']");

                var selectAllAttributes = topics.Select(x => new Link(x.GetAttributeValue("href", ""))).ToList();

                Links = topics.Select(x => new Link(x.GetAttributeValue("href", "")))
                    .Where(x => !(x.ToString().Contains("tracker"))
                        && !(x.ToString().Contains("download")))
                    .ToList();
            }

            catch (System.ArgumentNullException ex) 
            {
                //throw new Exception("За пошуком нічого не знайдено" 
                //    + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
