﻿using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace HurtomFiles.Logic
{
    public struct FileInformation
    {
        public Title title;
        public string type;
        public string imageUri;
        public string information;
        public string source;

        public FileInformation(string uri)
        {
            this.title = new Title();
            this.type = "";
            this.imageUri = "";
            this.information = "";
            this.source = uri;

            var htmlDocument = new HtmlDocument().HtmlDocumentLoadAsync(uri).Result;
            SetFields(htmlDocument);
        }

        public void SetFields(HtmlDocument htmlDocument) 
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

        public override bool Equals(object obj)
        {
            if (obj is FileInformation fi) 
            {
                if (fi.title.ToString() == this.title.ToString())
                    return true;
            }
            return false;
        }
    }
}