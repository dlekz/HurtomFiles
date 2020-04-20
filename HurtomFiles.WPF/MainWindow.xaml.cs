using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HurtomFiles.WPF.Properties;
using HtmlAgilityPack;
using HurtomFiles.Logic;

namespace HurtomFiles.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileInformationElementCollection elements;
        // TODO: it's work, but slow
        public MainWindow()
        {
            InitializeComponent();

            elements = new FileInformationElementCollection();
            elements.AddRange("https://toloka.to/f16");

            var scroll = new ScrollViewer() { Content = elements };

            this.HeaderGrid.Children.Add(new HeaderElement());
            this.SideBar.Children.Add(new SideBarElement());
            this.MainGrid.Children.Add(scroll);
        }
    }
}
