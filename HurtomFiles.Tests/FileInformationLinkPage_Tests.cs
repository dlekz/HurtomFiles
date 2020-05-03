using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HurtomFiles.Logic;

namespace HurtomFiles.Tests
{
    [TestFixture]
    class FileInformationLinkPage_Tests
    {
        [TestCase("https://toloka.to/f16")]
        [Category("CreateTime_Tests")]
        public void FileInformationLinkPage_CreateTime(string uri) 
        {
            var linkPage = new FileInformationLinkPage(uri);
        }

        [TestCase("https://toloka.to/f16", 45)]
        [Category("Count_Tests")]
        public void FileInformationLinkPage_Count(string uri, int count)
        {
            var fileInfoCollection = new FileInformationLinkPage(uri);

            Assert.AreEqual(count, fileInfoCollection.Count);
        }

        [TestCase("https://toloka.to/f16", "https://toloka.to/f16-45")]
        [Category("NextPage_Tests")]
        public void FileInformationLinkPage_NextPage(string thisPage, string nextPage)
        {
            var fileInfoCollection = new FileInformationLinkPage(thisPage);

            Assert.AreEqual(nextPage, fileInfoCollection.NextPage);
        }
    }
}
