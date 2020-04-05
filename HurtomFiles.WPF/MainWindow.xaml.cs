using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HurtomFiles.Logic;

namespace HurtomFiles.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InformationElementCollection elements;

        public MainWindow()
        {
            InitializeComponent();

            elements = new InformationElementCollection(this.MainGrid, this);
            elements.Add("https://toloka.to/t108863");
            elements.Add("https://toloka.to/t97235");
            elements.Add("https://toloka.to/t97237");
            elements.Add("https://toloka.to/t109121");
            elements.Add("https://toloka.to/t109209");
        }
    }
}
