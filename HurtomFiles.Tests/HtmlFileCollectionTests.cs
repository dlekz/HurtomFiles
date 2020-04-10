﻿using System;
using System.Collections.Generic;
using System.Text;
using HurtomFiles.Logic;
using NUnit.Framework;
using HtmlAgilityPack;
using System.Linq;

namespace HurtomFiles.Tests
{
    [TestFixture]
    public class HtmlFileCollectionTests
    {
        [TestCase("https://toloka.to/f16")]
        public void FileInformationCollectionTest(string uri) 
        {
            var fileInfoCollection = new FileInformationCollection(uri);
        }

        [TestCase("https://toloka.to/f16", 45)]
        public void FileInformationCollectionCountTest(string uri, int count) 
        {
            var fileInfoCollection = new FileInformationCollection(uri);

            Assert.AreEqual(count, fileInfoCollection.Count);
        }
    }
}