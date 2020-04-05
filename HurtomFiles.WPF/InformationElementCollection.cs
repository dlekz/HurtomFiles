using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using HurtomFiles.Logic;
namespace HurtomFiles.WPF
{
    public class InformationElementCollection
    {
        private List<InformationElement> values = new List<InformationElement>();
        private WrapPanel bindingGrid;
        private Window bindingWindow;

        public InformationElementCollection(WrapPanel bindingGrid, Window bindingWindow)
        {
            this.bindingGrid = bindingGrid;
            this.bindingWindow = bindingWindow;
        }

        public void Add(string uri)
        {
            var value = new InformationElement(uri);
            values.Add(value);
            bindingGrid.Children.Add(value);
        }
    }
}
