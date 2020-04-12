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

           var header = new HeaderElement();
           this.HeaderGrid.Children.Add(header);
           //MessageBox.Show(Resources.Count + "");
            elements = new FileInformationElementCollection();
            elements.AddRange("https://toloka.to/f16");

            var scroll = new ScrollViewer() { Content = elements };

            this.MainGrid.Children.Add(scroll);
            //this.MainGrid.Children.Add(elements);

            //elements.Add("https://toloka.to/t108863");
            //elements.Add("https://toloka.to/t97235");
            //elements.Add("https://toloka.to/t97237");
            //elements.Add("https://toloka.to/t109121");
            //elements.Add("https://toloka.to/t109209");
        }
    }
}
