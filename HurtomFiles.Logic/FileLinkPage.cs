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

        private readonly Link[] links;

        public Link ThisPage { private set; get; }
        public Link NextPage { private set; get; }
        public int Count => links.Count();

        public string this[int i] => links[i].ToString();

        public FileLinkPage(string uri) 
        {
            var htmlDocument = new HtmlDocument().HtmlDocumentLoadAsync(uri).Result;
            var topics = htmlDocument.DocumentNode.SelectNodes("//a[@class='topictitle']");
            var navigation = htmlDocument.DocumentNode.SelectNodes("//span[@class='navigation']/a");

            ThisPage = new Link(uri);
            NextPage = new Link(navigation.Last().GetAttributeValue("href", ""));
            links = topics.Select(x => new Link(x.GetAttributeValue("href", ""))).ToArray();
        }

        // TODO: logicaly it is not this class
        public List<FilePage> GetFileCollection()
        {
            var range = new List<FilePage>();
            for (int i = 0; i < this.Count; i++)
            {
                range.Add(Task.Run(() => new FilePage(this[i])).Result);
            }
            return range;
        }
    }
}
