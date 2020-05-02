using System;
using System.Collections.Generic;
using System.Text;
using HurtomFiles.Logic;
using NUnit.Framework;
using HtmlAgilityPack;
using System.Linq;

namespace HurtomFiles.Tests
{
    [TestFixture]
    public class FileInformationCollection_Tests
    {
        [TestCase("https://toloka.to/f16")]
        [Category("CreateTime_Tests")]
        public void FileInformationCollection_CreateTime_Test(string uri) 
        {
            var fileInfoCollection = new FileInformationCollection(uri);
        }

        [TestCase("https://toloka.to/f16", 45)]
        public void FileInformationCollectionCountTest(string uri, int count) 
        {
            var fileInfoCollection = new FileInformationCollection(uri);

            Assert.AreEqual(count, fileInfoCollection.Count);
        }

        [TestCase("https://toloka.to/f16","https://toloka.to/f16-45")]
        public void FileInformationCollection_NextPage(string thisPage, string nextPage) 
        {
            var fileInfoCollection = new FileInformationCollection(thisPage);

            Assert.AreEqual(nextPage, fileInfoCollection.NextPage);
        }
    }
}
