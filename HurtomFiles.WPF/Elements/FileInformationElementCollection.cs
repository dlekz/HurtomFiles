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
    public class FileInformationElementCollection : WrapPanel
    {
        private readonly List<FileInformation> values = new List<FileInformation>();

        private List<FileInformation> buffer;

        private bool isBuffering = false;

        public string NextPage { private set; get; }

        public FileInformationElementCollection()
        {
            this.Set();
        }

        public FileInformationElementCollection(string uri) 
        {
            this.Set();

            var infoColl = Task.Run(() => new FileInformationCollection(uri)).Result;
            this.NextPage = infoColl.NextPage;
            foreach (var info in infoColl.values)
                this.Add(info);

            Buffering();
        }

        public FileInformation this[int i] => values[i];

        public void Add(FileInformation info)
        {
            //Dispatcher.InvokeAsync(() => this.Children.Add(new FileInformationElement(info)));
            Application.Current.Dispatcher.Invoke(() => this.Children.Add(new FileInformationElement(info)));
            values.Add(info);
        }

        private void Set() 
        {
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.Background = Brushes.Orange;
            this.Margin = new Thickness(0);
        }

        public void _AddPage() 
        {
            if (this.NextPage == "") return;

            var links = Task.Run(() => new FileInformationLinkPage(NextPage)).Result;

            for (int i = 0; i < links.Count; i++) 
            {
                var fileInformation = Task.Run(() => new FileInformation(links[i])).Result;
                this.Add(fileInformation);
            }
        }

        public void AddPage()
        {

                if (this.NextPage == "") return;

                var links = Task.Run(() => new FileInformationLinkPage(NextPage)).Result;

                //var FileIngormationElementsList = new List<FileInformationElement>();
                //foreach ()

                //Dispatcher.InvokeAsync(() => this.Children.Add(new FileInformationElement()));

                //for (int i = 0; i < links.Count; i++)
                //{
                //    var fileInformation = Task.Run(() => new FileInformation(links[i])).Result;

                //    Dispatcher.InvokeAsync(() => this.Children.Add(new FileInformationElement(fileInformation)));

                //    values.Add(fileInformation);
                //}

                if (!isBuffering || buffer.Count == 0)
                {
                    MessageBox.Show("Buffer is not available");
                    return;
                }

                for (int i = 0; i < buffer.Count; i++)
                {
                    Application.Current.Dispatcher.Invoke(() => this.Children.Add(new FileInformationElement(buffer[i])));
                }

                values.AddRange(buffer);

                Buffering();

        }

        public void Buffering() 
        {
            isBuffering = false;
            buffer = new List<FileInformation>();
            if (this.NextPage == "") return;

            var links = Task.Run(() => new FileInformationLinkPage(NextPage)).Result;
            this.NextPage = links.NextPage;

            for (int i = 0; i < links.Count; i++)
            {
                var fileInformation = Task.Run(() => new FileInformation(links[i])).Result;

                buffer.Add(fileInformation);
            }

            //MessageBox.Show(string.Join(",",buffer.Select(x => x.source)));
            isBuffering = true;
        }
    }
}
