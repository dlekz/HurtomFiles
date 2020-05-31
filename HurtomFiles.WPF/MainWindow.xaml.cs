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
        private readonly FileElementPanel elements;
        private readonly AddMoreElements_Button addMoreElements = new AddMoreElements_Button();
        private readonly LoadingElement loadingElement = new LoadingElement();
        private readonly SideBarElement sideBarElement = new SideBarElement();
        private readonly HeaderElement headerElement = new HeaderElement();

        // TODO: it's work, but slow
        // TODO: https://toloka.to/t51625 info not found
        // TODO: https://toloka.to/t109534 image not load
        public MainWindow()
        {
            InitializeComponent();
            this.MouseMove += CursorChange;
            this.addMoreElements.Click += AddMoreElements_Button_Click;
            this.sideBarElement.ShowElements_Button.Click += ShowElements_Button_Click;
            this.sideBarElement.ShowFavorites_Button.Click += ShowFavorites_Button_Click;

            Closing += Before_Close;

            elements = new FileElementPanel("https://toloka.to/f16");

            var elementsPanel = new StackPanel();

            elementsPanel.Children.Add(elements);
            elementsPanel.Children.Add(addMoreElements);
           // elementsPanel.Children.Add(loadingElement);
           // elementsPanel.Children.Add(loadingElement.LoadingElement_Rotate());


            var scroll = new ScrollViewer() { Content = elementsPanel };

            this.HeaderGrid.Children.Add(headerElement);
            this.SideBar.Children.Add(sideBarElement);
            this.MainGrid.Children.Add(scroll);

            Task.Run(() => elements.Buffering(9));
        }

        private void CursorChange(object sender, EventArgs e) 
        {
            if (elements.Focused)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Arrow;
        }

        private void AddMoreElements_Button_Click(object sender, EventArgs e) 
        {
            Task.Run(() => elements.AddPage());
        }

        private void ShowElements_Button_Click(object sender, EventArgs e) 
        {
            elements.ShowElements();
            addMoreElements.Visibility = Visibility.Visible;
        }

        private void ShowFavorites_Button_Click(object sender, EventArgs e)
        {
            elements.ShowFavorites();
            addMoreElements.Visibility = Visibility.Hidden;
        }

        private void Before_Close(object sender, EventArgs e)
        {
            elements.SetFavorites();
        }

    }
}
