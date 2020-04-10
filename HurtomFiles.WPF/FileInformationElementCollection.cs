using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using HurtomFiles.Logic;
using System.Threading.Tasks;

namespace HurtomFiles.WPF
{
    public class FileInformationElementCollection
    {
        private List<FileInformationElement> values = new List<FileInformationElement>();
        private WrapPanel bindingGrid;
        private Window bindingWindow;

        public FileInformationElementCollection(WrapPanel bindingGrid, Window bindingWindow)
        {
            this.bindingGrid = bindingGrid;
            this.bindingWindow = bindingWindow;
        }

        public void Add(string uri)
        {
            var value = new FileInformationElement(uri);
            values.Add(value);
            bindingGrid.Children.Add(value);
        }

        public void Add(FileInformation info)
        {
            var value = new FileInformationElement(info);
            values.Add(value);
            bindingGrid.Children.Add(value);
        }

        public void AddRange(string uri) 
        {
            var infoColl = Task.Run(() => new FileInformationCollection(uri)).Result.values;

            foreach (var info in infoColl)
            {
                this.Add(info);
            }

        }
    }
}
