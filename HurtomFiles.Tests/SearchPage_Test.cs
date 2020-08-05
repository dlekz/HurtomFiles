using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HurtomFiles.Logic;

namespace HurtomFiles.Tests
{
    [TestFixture]
    public class SearchPage_Test
    {
        [TestCase("шерлок холмс", 37)]
        [TestCase("dfasgdfsfads", 0)]
        [Category("Count_Tests")]
        public void FileLinkPage_Count(string uri, int count)
        {
            var fileInfoCollection = new  SearchPage(uri);

            Assert.AreEqual(count, fileInfoCollection.Count);
        }
    }
}
