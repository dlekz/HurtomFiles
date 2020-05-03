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

        private FileInformationBuffer Buffer { set; get; } = new FileInformationBuffer();

        [Obsolete("Use Buffer.Page")] public string NextPage { private set; get; }

        public FileInformationElementCollection()
        {
            this.Set();
        }

        public FileInformationElementCollection(string uri)
        {
            this.Set();

            var infoColl = Task.Run(() => new FileInformationLinkPage(uri)).Result;

            foreach (var info in infoColl.GetFileInformationCollection())
                this.Add(info);

            Buffer.Page = infoColl.NextPage;
            //Buffer.Page = this.NextPage;
            Buffer.Buffering();
        }

        public FileInformation this[int i] => values[i];

        public void Add(FileInformation info)
        {
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

        public void AddPage()
        {

            var page = Buffer.Page;

            if (Buffer.Page == "") return;

            var links = Task.Run(() => new FileInformationLinkPage(Buffer.Page)).Result;

            if (!Buffer.IsBuffering || Buffer.Count == 0)
            {
                MessageBox.Show("Buffer is not available");
                return;
            }

            //Buffer.IsBuffering = true;

            for (int i = 0; i < Buffer.Count; i++)
            {
                var value = Buffer[i];
                Application.Current.Dispatcher.Invoke(() => this.Children.Add(new FileInformationElement(value)));
                values.Add(value);
            }

            Buffer.Buffering();
        }
    }
}
