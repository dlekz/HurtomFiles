using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using HurtomFiles.Logic;
using HurtomFiles.WPF.Elements;

namespace HurtomFiles.WPF
{
    public class FileElementCollection
    {
        public List<FileElement> Value { set; get; } = new List<FileElement>();

        public FileElementCollection() 
        {
            //this.Value.AddRange(Get());
        }

        public void Add(string uri, bool isFavorite)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                
                var newElement = new FileElement(uri);
                if (isFavorite)
                    newElement.StarColor = Star.StarColors.YELLOW;

                this.Value.Add(newElement);
            });
        }

        public void AddRange(FilePage[] files, List<FileElement> favorites) 
        {
            foreach (var file in files)
            {
                bool isFavorite = false;
                var element = new FileElement(file);
                if (favorites.Exists(x => x.source.source.ToString() == element.source.source.ToString()))
                    isFavorite = true;
                
                this.Add(element.source.source.ToString(), isFavorite);
            }
        }

    }
}
