using NUnit.Framework;
using HurtomFiles.Logic;

namespace HurtomFiles.Tests
{
    [TestFixture]
    public class HtmlFileTests
    {

        [TestCase("https://toloka.to/t97235",
            "Зоряні Війни: Сага / Star Wars: The Complete Saga (1977-2019)",
            "Толока » Відео » Українське озвучення » Фільми",
            "https://img.hurtom.com/i/2016/02/StarWars0.550px.jpg")]

        [TestCase("https://toloka.to/t108863",
            "1917 / 1917 (2019) AVC Ukr/Eng | sub Eng",
            "Толока » Відео » Українське озвучення » Фільми",
            "https://posters.hurtom.com/cache/movies/1917-2019-v2_550.jpg")]

        [TestCase("https://toloka.to/t97237",
            "Альфа / Alpha (2018) Remux 1080p Ukr/Eng | Sub Ukr/Eng",
            "Толока » Відео » HD українською » Фільми в HD",
            "https://posters.hurtom.com/cache/movies/alpha-2018-_550.jpg")]
        public void CopyHtmlByUrl(string url, string title, string type, string imgUrl)
        {
            var fileInfo = new FileInformation(url);

            Assert.AreEqual(fileInfo.title.fullTitle, title);
            Assert.AreEqual(fileInfo.type, type);
            Assert.AreEqual(fileInfo.imageUrl, imgUrl);
        }
    }
}