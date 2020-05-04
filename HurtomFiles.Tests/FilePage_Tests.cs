using NUnit.Framework;
using HurtomFiles.Logic;

namespace HurtomFiles.Tests
{
    [TestFixture]
    public class FilePage_Tests
    {
        [TestCase("https://toloka.to/t108863",
            "1917 / 1917 (2019) AVC Ukr/Eng | sub Eng",
            "Толока » Відео » Українське озвучення » Фільми",
            "https://posters.hurtom.com/cache/movies/1917-2019-v2_550.jpg")]

        [TestCase("https://toloka.to/t97237",
            "Альфа / Alpha (2018) Remux 1080p Ukr/Eng | Sub Ukr/Eng",
            "Толока » Відео » HD українською » Фільми в HD",
            "https://posters.hurtom.com/cache/movies/alpha-2018-_550.jpg")]
        [Category("Create_Tests")]
        public void FilePage_Create(string url, string title, string type, string imgUrl)
        {
            var fileInfo = new FilePage(url);

            Assert.AreEqual(fileInfo.title.fullTitle, title);
            Assert.AreEqual(fileInfo.type, type);
            Assert.AreEqual(fileInfo.imageUri, imgUrl);
        }
    }
}