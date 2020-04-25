using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            this.MouseMove += CursorChange;

            elements = new FileInformationElementCollection("https://toloka.to/f16");

            var elementsPanel = new StackPanel();
            var addMoreElement = new AddMoreElements_Button();

            addMoreElement.Click += AddMoreElements_Button_Click;

            elementsPanel.Children.Add(elements);
            elementsPanel.Children.Add(addMoreElement);

            var scroll = new ScrollViewer() { Content = elementsPanel };

            this.HeaderGrid.Children.Add(new HeaderElement());
            this.SideBar.Children.Add(new SideBarElement());
            this.MainGrid.Children.Add(scroll);
        }

        private void CursorChange(object sender, EventArgs e) 
        {
            if (FileInformationElement.Focused)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Arrow;
        }

        private void AddMoreElements_Button_Click(object sender, EventArgs e) 
        {
            elements.AddPage();
        }

    }
}
