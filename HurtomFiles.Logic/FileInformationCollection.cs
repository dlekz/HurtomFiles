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
    public class FileInformationCollection
    {
        public readonly List<FileInformation> values = new List<FileInformation>();
        public readonly List<string> umis;

        public FileInformation this[int i] 
            => values[i];

        public int Count => values.Count;

        public FileInformationCollection(string uri) 
        {
           var htmlDocument = new HtmlDocument().HtmlDocumentLoadAsync(uri).Result;
           SetValues(htmlDocument);
            
        }

        public void SetValues(HtmlDocument htmlDoc) 
        {
            var topics = htmlDoc.DocumentNode.SelectNodes("//a[@class='topictitle']");

            var range = topics.Select(x => Task.Run(() => new FileInformation("https://toloka.to/" + x.GetAttributeValue("href", ""))).Result).ToArray();
            //var range = topics.Select(x => /*"https://toloka.to/" + */x.GetAttributeValue("href", "")).ToArray();
            this.values.AddRange(range);

            //var range = topics.Select(x => "https://toloka.to/" + x.GetAttributeValue("href", ""));
            //this.umis.AddRange(range);
        }
    }
}
