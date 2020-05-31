using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using System.Threading.Tasks;

namespace HurtomFiles.Logic
{
    public class FileLinkPage
    {

        public List<Link> Links { set; get; } = new List<Link>();

        public Link ThisPage { private set; get; }
        public Link NextPage { private set; get; }
        public int Count => Links.Count();
        // TODO: if i > links.Count
        public string this[int i] => Links[i].ToString();

        public FileLinkPage(string uri) 
        {
            var htmlDocument = new HtmlDocument().HtmlDocumentLoadAsync(uri).Result;
            var topics = htmlDocument.DocumentNode.SelectNodes("//a[@class='topictitle']");
            var navigation = htmlDocument.DocumentNode.SelectNodes("//span[@class='navigation']/a");

            ThisPage = new Link(uri);
            NextPage = new Link(navigation.Last().GetAttributeValue("href", ""));
            Links = topics.Select(x => new Link(x.GetAttributeValue("href", ""))).ToList();
        }

    }
}
