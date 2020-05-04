using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HurtomFiles.Logic;

namespace HurtomFiles.Tests
{
    [TestFixture]
    class FileLinkPage_Tests
    {
        [TestCase("https://toloka.to/f16")]
        [Category("CreateTime_Tests")]
        public void FileInformationLinkPage_CreateTime(string uri) 
        {
            var linkPage = new FileLinkPage(uri);
        }

        [TestCase("https://toloka.to/f16", 45)]
        [Category("Count_Tests")]
        public void FileLinkPage_Count(string uri, int count)
        {
            var fileInfoCollection = new FileLinkPage(uri);

            Assert.AreEqual(count, fileInfoCollection.Count);
        }

        [TestCase("https://toloka.to/f16", "https://toloka.to/f16-45")]
        [Category("NextPage_Tests")]
        public void FileLinkPage_NextPage(string thisPage, string nextPage)
        {
            var fileInfoCollection = new FileLinkPage(thisPage);

            Assert.AreEqual(nextPage, fileInfoCollection.NextPage.ToString());
        }
    }
}
