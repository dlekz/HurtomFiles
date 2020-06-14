using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Collections.Generic;
using HurtomFiles.Logic;

namespace HurtomFiles.WPF.Elements
{
    public class Favorites
    {
        public List<FileElement> Value { set; get; } = new List<FileElement>();
        private readonly JsonFile jFile = new JsonFile("Favorites");

        public Favorites() 
        {
            this.Value.AddRange(Get());
        }

        public void Add(string uri) 
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var temp = this.Value.Select(x => x).ToArray();
                this.Value = new List<FileElement>();

                var newElement = new FileElement(uri);
                newElement.StarColor =Star.StarColors.YELLOW;

                this.Value.Add(newElement);
                this.Value.AddRange(temp);
            });
        }

        public void Remove(string uri) 
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var found = this.Value.Find(x => x.source.source.ToString() == uri);
                this.Value.Remove(found);
            });
        }

        public void Set() 
        {
            jFile.Article = "Links";
            this.Value.Reverse();
            var jValues = this.Value.Select(x => x.source.source.ToString()).ToArray();

            jFile.WriteArticle(jValues);
        }

        public FileElement[] Get() 
        {
            jFile.Article = "Links";
            var jRead = jFile.ReadArticle("Links")
                .Select(x => x.ToObject<string>())
                .ToArray();

            var fileElements = new List<FileElement>();
            foreach (var jR in jRead)
            {
                var page = Task.Run(() => new FilePage(jR)).Result;
                var element = new FileElement(page);
                //element.star.SetColor_Yellow();
                element.StarColor = Star.StarColors.YELLOW;
                fileElements.Add(element);
            }

            fileElements.Reverse();

            return fileElements.ToArray();
        }
    }
}
