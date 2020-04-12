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
        private readonly List<FileInformationElement> values = new List<FileInformationElement>();
        public FileInformationElementCollection() => this.Set();

        public void Add(string uri)
        {
            var value = new FileInformationElement(uri);
            values.Add(value);
            this.Children.Add(value);
        }

        public void Add(FileInformation info)
        {
            var value = new FileInformationElement(info);
            values.Add(value);
            this.Children.Add(value);
        }

        public void AddRange(string uri) 
        {
            var infoColl = Task.Run(() => new FileInformationCollection(uri)).Result.values;

            foreach (var info in infoColl)
            {
                this.Add(info);
            }

        }

        private void Set() 
        {
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.Background = Brushes.LightGray;
            this.Margin = new Thickness(0);
        }
    }
}
