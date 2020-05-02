using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;

namespace HurtomFiles.Logic
{
    public class FileInformationLinkPage
    {
        private readonly string[] values;

        public string ThisPage { private set; get; }
        public string NextPage { private set; get; }
        public int Count => values.Count();

        public string this[int i] => values[i];

        public FileInformationLinkPage(string uri) 
        {
            var htmlDocument = new HtmlDocument().HtmlDocumentLoadAsync(uri).Result;

            ThisPage = uri;
            NextPage = GetNextPage(htmlDocument);
            values = GetLinks(htmlDocument);
        }

        public string[] GetLinks(HtmlDocument htmlDoc)
        {
            var topics = htmlDoc.DocumentNode.SelectNodes("//a[@class='topictitle']");

            return topics.Select(x => "https://toloka.to/" + x.GetAttributeValue("href", "")).ToArray();
        }

        private string GetNextPage(HtmlDocument htmlDoc)
        {
            var navigation = htmlDoc.DocumentNode.SelectNodes("//span[@class='navigation']/a");
            var nextPage = navigation.Last().GetAttributeValue("href", "");

            return "https://toloka.to/" + nextPage;
        }
    }
}
