using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using HurtomFiles.Logic;
using System.Threading.Tasks;

namespace HurtomFiles.WPF
{
    public class SearchCommand
    {
        private readonly string SearchString;

        public SearchCommand() 
        {
            new DialogWindow("Пошук").ShowDialog();
            this.SearchString = DialogWindow.Value;
        }

        public List<UIElement> Result 
        {
            get 
            {
                List<UIElement> elements = new List<UIElement>();

                if (SearchString == "")
                    return new List<UIElement>();

                var links = Task.Run(() => new SearchPage(SearchString)).Result
                    .Links.Select(x => x).ToArray();

                if (links.Length == 0)
                {
                    Label filesFotFoundLabel = new Label()
                    {
                        Content = "За пошуком нічого не знайдено",
                    };
                    elements.Add(filesFotFoundLabel);
                    return elements;
                }

                foreach (var link in links)
                {
                    var file = Task.Run(() => new FilePage(link.ToString())).Result;
                    elements.Add(new FileElement(file));
                }

                return elements;
            }

        }

    }
}
