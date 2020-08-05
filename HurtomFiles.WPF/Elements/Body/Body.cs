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
    public class Body : WrapPanel
    {
        public bool Focused { set; get; } = false;
        private FileBuffer Buffer { set; get; } = new FileBuffer();

        public FileElementCollection Elements { private set; get; } = new FileElementCollection();

        public FileElementTypes ActiveElements = FileElementTypes.MAIN;

        public Favorites Favorites { set; get; }

        public Body(string uri)
        {
            this.Style = App.Styles.BodyStyle;

            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            MouseDown += Element_Click;
            MouseUp += After_Element_Click;

            var infoColl = Task.Run(() => new FileLinkPage(uri)).Result;

            Application.Current.Dispatcher.Invoke(() => this.Favorites = new Favorites());

            Buffer.Page = new Link(uri);
            Buffer.Buffering();

            AddElements();
            Show(FileElementTypes.MAIN);

            Buffer.Page = new Link(infoColl.NextPage.ToString());
            //Buffer.Buffering();
        }

        public void Show(FileElementTypes elementsType)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Children.Clear();

                var elements = new List<FileElement>();
                switch (elementsType) 
                {
                    case FileElementTypes.MAIN :
                        elements.AddRange(Elements.Value);
                        break;
                    case FileElementTypes.FAVORITES :
                        elements.AddRange(Favorites.Value);
                        break;
                };

                foreach (var el in elements)
                    this.Children.Add(el);

                ActiveElements = elementsType;
            });
        }

        public void AddPage()
        {
            try
            {
                Buffer.Check();
                AddElements();

                Buffer.Buffering(9);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void AddElements() 
        {
            foreach (var page in Buffer.Get)
            {
                var element = Application.Current.Dispatcher.Invoke(() => new FileElement(page));

                if (Favorites.Value.Exists(x => x.source.link == element.source.link))
                    Application.Current.Dispatcher.Invoke(() 
                        => element.StarColor = Star.StarColors.YELLOW);

                Elements.Add(element);
                Application.Current.Dispatcher.Invoke(() => this.Children.Add(element));
            }
        }

        private void Element_Click(object sender, EventArgs e)
        {
            foreach (FileElement el in this.Children)
            {
                if (el.Focused) 
                    el.MouseClick();
            }
        }

        private void After_Element_Click(object sender, EventArgs e)
        {
            foreach (FileElement el in this.Children)
            {
                //if (!el.StarFocused)
                //    return;

                if (el.Focused && el.StarFocused)
                {
                    if (el.StarColor == Star.StarColors.YELLOW) 
                    {
                        Favorites.Add(el.source.link);
                        return;
                    }
                    if (el.StarColor == Star.StarColors.WHITE) 
                    {
                        Favorites.Remove(el.source.link);

                        if (ActiveElements != FileElementTypes.FAVORITES)
                            return;

                        Show(FileElementTypes.FAVORITES);
                        if (Elements.Value.Exists(x => x.source.link == el.source.link)) 
                        {
                            var target = Elements.Value.Where(x => x.source.link == el.source.link)
                                .ToArray().First();
                            target.StarColor = Star.StarColors.WHITE;
                        }

                        return;
                    }
                }
            }
        }

        private void SetFocus(object sender, EventArgs e) => Focused = true;

        private void LostFocus(object sender, EventArgs e) => Focused = false;

        public void Buffering(int count) => Buffer.Buffering(count);

    }
}
