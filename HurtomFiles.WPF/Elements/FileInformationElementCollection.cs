using System;
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
        }

        public FileInformation this[int i] => values[i];

        public void Add(FileInformation info)
        {
            this.Children.Add(new FileInformationElement(info));
            values.Add(info);
        }

        [Obsolete]
        public void AddRange(string uri) 
        {
            var infoColl = Task.Run(() => new FileInformationCollection(uri)).Result.values;

            foreach (var info in infoColl)
                this.Add(info);
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
            if(this.NextPage == "") return;

            var infoColl = Task.Run(() => new FileInformationCollection(NextPage)).Result;
            NextPage = infoColl.NextPage;

            foreach (var info in infoColl.values)
                this.Add(info);
        }
        
    }
}
