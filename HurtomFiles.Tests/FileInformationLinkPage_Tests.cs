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
        [Category ("CreateTime_Tests")]
        public void FileInformationLinkPage_CreateTime_Test(string uri) 
        {
            var linkPage = new FileInformationLinkPage(uri);
        }
    }
}
