using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using HurtomFiles.Logic;
using System.Threading.Tasks;
using HurtomFiles.WPF.Elements;
using System.Diagnostics;

namespace HurtomFiles.WPF
{
    public class FileElementPanel : WrapPanel
    {
        public bool Focused { set; get; } = false;
        private FileBuffer Buffer { set; get; } = new FileBuffer();
        public List<FileElement> Elements { private set; get; } = new List<FileElement>();

        public List<FileElement> Favorites { set; get; } = new List<FileElement>();

        private JsonFile jFile = new JsonFile("Favorites");

        public FileElementPanel(string uri)
        {
            this.Set();

            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            MouseDown += Element_Click;
            MouseUp += After_Element_Click;

            var infoColl = Task.Run(() => new FileLinkPage(uri)).Result;

            Application.Current.Dispatcher.Invoke(() => 
                this.Favorites.AddRange(GetFavorites()));
            //jFile.Clear();

            Buffer.Page = new Link(uri);
            Buffer.Buffering();
            this.AddRange(Buffer.Get);

            Buffer.Page = new Link(infoColl.NextPage.ToString());
            //Buffer.Buffering();
        }

        public void Add(FilePage info)
        {
            Application.Current.Dispatcher.Invoke(() => 
            {
                var element = new FileElement(info);


                if (Favorites.Exists(x => x.source.source.ToString() == element.source.source.ToString()))
                    element.star.SetColor_Yellow();

                this.Children.Add(element);
                this.Elements.Add(element);

            });
        }

        public void AddFavorite(string uri) 
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    var temp = Favorites.Select(x => x).ToArray();
                    Favorites = new List<FileElement>();

                    var newElement = new FileElement(Task.Run(() => new FilePage(uri)).Result);
                    newElement.star.SetColor_Yellow();

                    Favorites.Add(newElement);
                    Favorites.AddRange(temp);
                });
        }

        public void SetFavorites() 
        {
            jFile.Article = "Links";

            var jValues = Favorites.Select(x => x.source.source.ToString()).ToArray();
            jValues.Reverse();

            jFile.WriteArticle(jValues);
        }

        public FileElement[] GetFavorites() 
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
                element.star.SetColor_Yellow();
                fileElements.Add(element);
            }

            fileElements.Reverse();

            return fileElements.ToArray();
        }

        public void ShowFavorites() 
        {
            Application.Current.Dispatcher.Invoke(() => 
            {
                this.Children.Clear();
                foreach (var el in Favorites)
                    this.Children.Add(el);

            });

           //Application.Current.Dispatcher.Invoke(() => );
           // foreach (var el in Favorites) 
           // {
           //     Application.Current.Dispatcher.Invoke(() => this.Children.Add(el));
           // }
        }

        public void ShowElements()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Children.Clear();
                foreach (var el in Elements)
                    this.Children.Add(el);

            });

            //Application.Current.Dispatcher.Invoke(() => this.Children.Clear());
            //foreach (var el in Elements)
            //{
            //    Application.Current.Dispatcher.Invoke(() => this.Children.Add(el));
            //}
        }

        public void AddRange(FilePage[] files) 
        {
            foreach (var file in files)
                this.Add(file);
        }

        private void Set() 
        {
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.Background = Brushes.Orange;
            this.Margin = new Thickness(0);
        }

        public void AddPage()
        {
            try
            {
                Buffer.Check();
                this.AddRange(Buffer.Get);
                Buffer.Buffering(9);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Element_Click(object sender, EventArgs e)
        {
            foreach (FileElement el in this.Children)
            {
                if (el.Focused)
                {
                    var star = el.star;
                    if (star.Focused)
                    {
                        star.ChangeColor();
                        return;
                    }
                    else
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = el.source.source.ToString(),
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                        return;
                    }
                }
            }
        }

        private void After_Element_Click(object sender, EventArgs e)
        {

            foreach (FileElement el in this.Children)
            {
                if (el.Focused)
                {
                    var star = el.star;
   
                    if (star.Focused)
                    {
                        if (star.Color == "YELLOW")
                            AddFavorite(el.source.source.ToString());
                    }
                }
            }
        }

        private void SetFocus(object sender, EventArgs e)
        {
            Focused = true;
        }

        private void LostFocus(object sender, EventArgs e)
        {
            Focused = false;
        }

        public void Buffering() => Buffer.Buffering();

        public void Buffering(int count) => Buffer.Buffering(count);
    }
}
