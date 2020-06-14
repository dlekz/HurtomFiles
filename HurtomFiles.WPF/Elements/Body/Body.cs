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

        private FileElementTypes ActiveElements = FileElementTypes.MAIN;

        public Favorites Favorites { set; get; }

        public Body(string uri)
        {
            this.Set();

            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            MouseDown += Element_Click;
            MouseUp += After_Element_Click;

            var infoColl = Task.Run(() => new FileLinkPage(uri)).Result;

            Application.Current.Dispatcher.Invoke(() => this.Favorites = new Favorites());

            Buffer.Page = new Link(uri);
            Buffer.Buffering();
            //this.AddRange(Buffer.Get);
            Elements.AddRange(Buffer.Get, Favorites.Value.ToList());
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
                this.Elements.AddRange(Buffer.Get, this.Favorites.Value);
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
                    el.MouseClick();
            }
        }

        private void After_Element_Click(object sender, EventArgs e)
        {
            foreach (FileElement el in this.Children)
            {
                if (el.Focused)
                {
                    //var star = el.star;

                    if (el.StarFocused)
                    {
                        if (el.StarColor == Star.StarColors.YELLOW) 
                        {
                            Favorites.Add(el.source.source.ToString());
                            return;
                        }
                        if (el.StarColor == Star.StarColors.WHITE) 
                        {
                            Favorites.Remove(el.source.source.ToString());

                            if (ActiveElements == FileElementTypes.FAVORITES) 
                            {
                                Show(FileElementTypes.FAVORITES);
                                if (Elements.Value.Exists(x => x.source.source.ToString()
                                    == el.source.source.ToString())) 
                                {
                                    var target = Elements.Value.Where(x => x.source.source.ToString()
                                        == el.source.source.ToString()).ToArray().First();
                                    target.StarColor = Star.StarColors.WHITE;
                                }
                                        
                            }

                            return;
                        }

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

        public void Buffering(int count) => Buffer.Buffering(count);
    }
}
