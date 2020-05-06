using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using HurtomFiles.Logic;
using System.Threading.Tasks;

namespace HurtomFiles.WPF
{
    public class FileElementPanel : WrapPanel
    {
        private FileBuffer Buffer { set; get; } = new FileBuffer();
        public List<FileElement> Elements { private set; get; } = new List<FileElement>();

        public FileElementPanel(string uri)
        {
            this.Set();

            var infoColl = Task.Run(() => new FileLinkPage(uri)).Result;

            Buffer.Page = new Link(uri);
            Buffer.Buffering();
            this.AddRange(Buffer.Get);

            Buffer.Page = new Link(infoColl.NextPage.ToString());
            //Buffer.Buffering();
        }

        public void Add(FilePage info)
        {
            Application.Current.Dispatcher.Invoke(() => this.Children.Add(new FileElement(info)));
            Application.Current.Dispatcher.Invoke(() => Elements.Add(new FileElement(info)));
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

        public void Buffering() => Buffer.Buffering();

        public void Buffering(int count) => Buffer.Buffering(count);
    }
}
