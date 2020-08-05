using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace HurtomFiles.Logic
{
    // TODO: Check must be working when it need
    public struct FilePage
    {
        public string type;
        public string imageUri;
        public string information;
        public Title title;
        private readonly Link source;
        public readonly string link;

        public FilePage(string uri)
        {
            this.title = new Title();
            this.type = "";
            this.imageUri = "";
            this.information = "";
            this.source = new Link(uri);
            this.link = this.source.ToString();

            var htmlDocument = new HtmlDocument().HtmlDocumentLoadAsync(uri).Result;
            SetFields(htmlDocument);
        }

        public void SetFields(HtmlDocument htmlDocument) 
        {
            try
            {
                var nav = htmlDocument.DocumentNode.SelectNodes("//td[@class='bodyline']/table")[2];
                var postbody = htmlDocument.DocumentNode.SelectNodes("//span[@class='postbody']").First();

                this.title = new Title(htmlDocument.GetHtmlElement("//a[@class='maintitle']"));
                this.type = string.Join(" » ", (nav.FindNode("a")
                    .Skip(2).Select(el => el.InnerText.Trim())));
                this.imageUri = "https:" + postbody.FindNode("img")
                    .First().Attributes["src"].Value;
                this.information = "";
            }
            catch (Exception) 
            {
                this.title = new Title("file not found");
                this.imageUri = "";
                this.type = "";
                this.information = "";
            }

        }

        public override bool Equals(object obj)
        {
            if (obj is FilePage fi) 
            {
                if (fi.title.ToString() == this.title.ToString())
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

    }
}
