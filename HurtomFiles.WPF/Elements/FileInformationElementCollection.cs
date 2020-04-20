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

        public FileInformationElementCollection()
        {
            this.Set();
        }

        public FileInformation this[int i] => values[i];

        public void Add(FileInformation info)
        {
            this.Children.Add(new FileInformationElement(info));
            values.Add(info);
        }

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

        
    }
}
