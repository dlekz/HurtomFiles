using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HurtomFiles.Logic
{
    [Obsolete]
    public class FileInformationCollection
    {
        public readonly List<FileInformation> values = new List<FileInformation>();
        public readonly List<string> umis;

        public string ThisPage { private set; get; }
        public string NextPage { private set; get; }

        public FileInformation this[int i] 
            => values[i];

        public int Count => values.Count;

        public FileInformationCollection(string uri) 
        {
            var htmlDocument = new HtmlDocument().HtmlDocumentLoadAsync(uri).Result;

            ThisPage = uri;
            NextPage = GetNextPage(htmlDocument);

            SetValues(htmlDocument);
            
        }

        public void SetValues(HtmlDocument htmlDoc) 
        {
            var topics = htmlDoc.DocumentNode.SelectNodes("//a[@class='topictitle']");

            var range = topics.Select(x => Task.Run(() => new FileInformation("https://toloka.to/" + x.GetAttributeValue("href", ""))).Result).ToArray();
            this.values.AddRange(range);
        }

        public string GetNextPage(HtmlDocument htmlDoc) 
        {
            var navigation = htmlDoc.DocumentNode.SelectNodes("//span[@class='navigation']/a");
            var nextPage = navigation.Last().GetAttributeValue("href", "");

            return "https://toloka.to/" + nextPage;
        }
    }
}
