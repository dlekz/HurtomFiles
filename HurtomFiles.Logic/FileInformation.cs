using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace HurtomFiles.Logic
{
    public struct FileInformation
    {
        public Title title;
        public string type;
        public string imageUrl;
        public string information;

        public FileInformation(string url)
        {
            this.title = new Title();
            this.type = "";
            this.imageUrl = "";
            this.information = "";

            var htmlDocument = HtmlDocumentLoadAsync(url).Result;
            SetFields(htmlDocument);
        }

        public void SetFields(HtmlDocument htmlDocument) 
        {
            var nav = htmlDocument.DocumentNode.SelectNodes("//td[@class='bodyline']/table")[2];
            var postbody = htmlDocument.DocumentNode.SelectNodes("//span[@class='postbody']").First();

            this.title = new Title(GetHtmlElement(htmlDocument, "//a[@class='maintitle']"));
            this.type = string.Join(" » ", (FindNode(nav, new string[] { "a" })
                .Skip(2).Select(el => el.InnerText.Trim())));
            this.imageUrl = "https:" + FindNode(postbody, new string[] { "img" })
                .First().Attributes["src"].Value;
            this.information = "";
        }

        public async Task<HtmlDocument> HtmlDocumentLoadAsync(string url) 
        {
            var client = new HttpClient();
            var htmlDoc = new HtmlDocument();
            var response = await client.GetAsync(url);

            Stream stream = await response.Content.ReadAsStreamAsync();

            htmlDoc.Load(stream);

            return htmlDoc;
        }

        public static string GetHtmlElement(HtmlDocument htmlDoc, string pattern) =>
            htmlDoc.DocumentNode.SelectNodes(pattern).First().InnerText.Trim();

        public static List<HtmlNode> FindNode(HtmlNode parent, string[] name)
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
