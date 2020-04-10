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
    public static class HtmlDocumentExtensions
    {
        public static async Task<HtmlDocument> HtmlDocumentLoadAsync
            (this HtmlDocument htmlDoc, string uri) 
        {
            HtmlNode.ElementsFlags.Remove("form");
            var client = new HttpClient();
            var html = new HtmlDocument();
            var response = await client.GetAsync(uri);

            Stream stream = await response.Content.ReadAsStreamAsync();

            html.Load(stream);
            return html;
        }

        public static string GetHtmlElement(this HtmlDocument htmlDoc, string pattern) =>
            htmlDoc.DocumentNode.SelectNodes(pattern).First().InnerText.Trim();
    }
}
